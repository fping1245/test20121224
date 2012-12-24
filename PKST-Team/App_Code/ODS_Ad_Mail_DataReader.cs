//---------------------------------------------------------------------------- 
//程式功能	取得 Ad_Mail 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Ad_Mail_DataReader
{
	private string Sql_ConnString = "";
	private string ParaString = "";

	public ODS_Ad_Mail_DataReader()
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

	public SqlDataReader Select_Ad_Mail(string SortColumn, int startRowIndex, int maximumRows,
		string adm_sid, string adm_title, string adm_fname, string adm_fmail, string btime, string etime)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select adm_sid, adm_title, (Case adm_type When 1 Then 'Text' When 2 Then 'HTML' Else 'None' End) As adm_type";
		SqlString += ", adm_fname, adm_fmail, adm_total, adm_send, adm_error, send_time, init_time";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "adm_sid";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Ad_Mail";

		// 產生 Where 字串內容
		SqlString += GetSqlString(adm_sid, adm_title, adm_fname, adm_fmail, btime, etime) + ") as MLog";

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
		if (ParaString.Contains("@adm_title"))
			Sql_Command.Parameters.AddWithValue("adm_title", adm_title);

		if (ParaString.Contains("@adm_fname"))
			Sql_Command.Parameters.AddWithValue("adm_fname", adm_fname);

		if (ParaString.Contains("@adm_fmail"))
			Sql_Command.Parameters.AddWithValue("adm_fmail", adm_fmail);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Ad_Mail(string SortColumn, int startRowIndex, int maximumRows,
		string adm_sid, string adm_title, string adm_fname, string adm_fmail, string btime, string etime)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Ad_Mail";
		SqlString = SqlString + GetSqlString(adm_sid, adm_title, adm_fname, adm_fmail, btime, etime);

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			if (ParaString.Contains("@adm_title"))
				Sql_Command.Parameters.AddWithValue("adm_title", adm_title);

			if (ParaString.Contains("@adm_fname"))
				Sql_Command.Parameters.AddWithValue("adm_fname", adm_fname);

			if (ParaString.Contains("@adm_fmail"))
				Sql_Command.Parameters.AddWithValue("adm_fmail", adm_fmail);
			#endregion

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Ad_Mail"] = nRows;

		return (int)context.Cache["GetCount_Ad_Mail"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string adm_sid, string adm_title, string adm_fname, string adm_fmail, string btime, string etime)
	{
		StringBuilder sbstring = new StringBuilder();
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;
		DateTime cktime;

		// 檢查 adm_sid 是否有值
		if (int.TryParse(adm_sid, out ckint))
		{
			subSql += " And adm_sid = " + ckint.ToString();
		}

		// 檢查 adm_title 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(adm_title);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@he_title+'%」 的方式
			subSql += " And adm_title Like '%'+@adm_title+'%'";
			sbstring.Append("@adm_title");
		}

		// 檢查 adm_fname 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(adm_fname);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@adm_fname+'%」 的方式
			subSql += " And adm_fname Like '%'+@adm_fname+'%'";
			sbstring.Append("@adm_fname");
		}

		// 檢查 adm_fmail 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(adm_fmail);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@adm_fmail+'%」 的方式
			subSql += " And adm_fmail Like '%'+@adm_fmail+'%'";
			sbstring.Append("@adm_fmail");
		}

		// 檢查 send_time 開始範圍是否有值
		if (DateTime.TryParse(btime, out cktime))
			subSql += " And send_time >= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		// 檢查 send_time 結束範圍是否有值
		if (DateTime.TryParse(etime, out cktime))
			subSql += " And send_time <= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		if (subSql != "")
			subSql = " Where" + subSql.Substring(4);

		ParaString = sbstring.ToString();

		return subSql;
	}
}
