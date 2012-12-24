//---------------------------------------------------------------------------- 
//程式功能	取得 Html_Edit 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Html_Edit_DataReader
{
	private string Sql_ConnString = "";
	private string ParaString = "";

	public ODS_Html_Edit_DataReader()
	{
		Initialize();
	}

	public void Initialize()
	{
		if (WebConfigurationManager.ConnectionStrings["AppSysConnectionString"] == null ||
			WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString.Trim() == "")
		{
			throw new Exception("Web.Config 的 <connectionStrings> 區段中，要有一個名稱為 AppSysConnectionString 資料庫連接字串。");
		}
		else
			Sql_ConnString = WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString;
	}

	public SqlDataReader Select_Html_Edit(string SortColumn, int startRowIndex, int maximumRows,
		string he_sid, string he_title, string he_desc, string btime, string etime)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select he_sid, he_title, he_desc, (Case is_attach When 1 Then '有' Else '無' End) As is_attach, init_time";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "he_sid";
		else
			SqlString += SortColumn;

		SqlString = SqlString + ") as rownum From Html_Edit";

		// 產生 Where 字串內容
		SqlString += GetSqlString(he_sid, he_title, he_desc, btime, etime) + ") as MLog";

		SqlString += " Where rownum Between " + (startRowIndex + 1).ToString() + " And " + (startRowIndex + maximumRows).ToString();

		// 排序設定
		SqlString += " Order by rownum";

		// 建立資料庫連結
		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);

		// 建立命令物件
		SqlCommand Sql_Command = new SqlCommand();

		Sql_Command.Connection = Sql_Conn;
		Sql_Command.CommandText = SqlString;

		#region 加入條件參數
		if (ParaString.Contains("@he_title"))
			Sql_Command.Parameters.AddWithValue("he_title", he_title);

		if (ParaString.Contains("@he_desc"))
			Sql_Command.Parameters.AddWithValue("he_desc", he_desc);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Html_Edit(string SortColumn, int startRowIndex, int maximumRows,
		string he_sid, string he_title, string he_desc, string btime, string etime)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Html_Edit";
		SqlString = SqlString + GetSqlString(he_sid, he_title, he_desc,btime, etime);

		using (Sql_conn)
		{
			Sql_Command.Connection = Sql_conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			if (ParaString.Contains("@he_title"))
				Sql_Command.Parameters.AddWithValue("he_title", he_title);

			if (ParaString.Contains("@he_desc"))
				Sql_Command.Parameters.AddWithValue("he_desc", he_desc);
			#endregion

			Sql_conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Html_Edit"] = nRows;

		return (int)context.Cache["GetCount_Html_Edit"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string he_sid, string he_title, string he_desc, string btime, string etime)
	{
		StringBuilder sbstring = new StringBuilder();
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;
		DateTime cktime;

		// 檢查 he_sid 是否有值
		if (int.TryParse(he_sid, out ckint))
		{
			subSql += " And he_sid = " + ckint.ToString();
		}

		// 檢查 he_title 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(he_title);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@he_title+'%」 的方式
			subSql += " And he_title Like '%'+@he_title+'%'";
			sbstring.Append("@he_title");
		}

		// 檢查 he_desc 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(he_desc);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@he_desc+'%」 的方式
			subSql += " And he_desc Like '%'+@he_desc+'%'";
			sbstring.Append("@he_desc");
		}

		// 檢查異動時間開始範圍是否有值
		if (DateTime.TryParse(btime, out cktime))
			subSql += " And init_time >= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		// 檢查異動時間結束範圍是否有值
		if (DateTime.TryParse(etime, out cktime))
			subSql += " And init_time <= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		if (subSql != "")
			subSql = " Where" + subSql.Substring(4);

		ParaString = sbstring.ToString();

		return subSql;
	}
}
