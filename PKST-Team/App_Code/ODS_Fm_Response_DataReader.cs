//---------------------------------------------------------------------------- 
//程式功能	取得 Fm_Response 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Fm_Response_DataReader
{
	private string Sql_ConnString = "";
	private string ParaString = "";

	public ODS_Fm_Response_DataReader()
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

	public SqlDataReader Select_Fm_Response(string SortColumn, int startRowIndex, int maximumRows,
		string ff_sid, string is_close, string fr_name, string fr_email, string fr_desc, string btime, string etime)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select ff_sid, fr_sid, fr_symbol, fr_name, fr_sex, fr_email, fr_time, fr_ip, fr_desc";
		SqlString += ", is_show, instead, is_close, Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "fr_sid DESC";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Fm_Response";

		// 產生 Where 字串內容
		SqlString += GetSqlString(ff_sid, is_close, fr_name, fr_email, fr_desc, btime, etime) + ") as MLog";

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
		if (ParaString.Contains("@fr_name"))
			Sql_Command.Parameters.AddWithValue("fr_name", fr_name);

		if (ParaString.Contains("@fr_email"))
			Sql_Command.Parameters.AddWithValue("fr_email", fr_email);

		if (ParaString.Contains("@fr_desc"))
			Sql_Command.Parameters.AddWithValue("fr_desc", fr_desc);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Fm_Response(string SortColumn, int startRowIndex, int maximumRows,
		string ff_sid, string is_close, string fr_name, string fr_email, string fr_desc, string btime, string etime)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Fm_Response";
		SqlString += GetSqlString(ff_sid, is_close, fr_name, fr_email, fr_desc, btime, etime);

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			if (ParaString.Contains("@fr_name"))
				Sql_Command.Parameters.AddWithValue("fr_name", fr_name);

			if (ParaString.Contains("@fr_email"))
				Sql_Command.Parameters.AddWithValue("fr_email", fr_email);

			if (ParaString.Contains("@fr_desc"))
				Sql_Command.Parameters.AddWithValue("fr_desc", fr_desc);
			#endregion

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Fm_Response"] = nRows;

		return (int)context.Cache["GetCount_Fm_Response"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string ff_sid, string is_close, string fr_name, string fr_email, string fr_desc, string btime, string etime)
	{
		StringBuilder sbstring = new StringBuilder();
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;
		DateTime cktime;

		// 檢查 ff_sid 是否有值
		if (int.TryParse(ff_sid, out ckint))
			subSql += " And ff_sid = " + ckint.ToString();

		// 檢查 is_close 是否有值
		if (int.TryParse(is_close, out ckint))
			subSql += " And is_close = " + ckint.ToString();

		// 檢查 fr_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(fr_name);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@fr_name+'%」 的方式
			subSql += " And fr_name Like '%'+@fr_name+'%'";
			sbstring.Append("@fr_name");
		}

		// 檢查 fr_email 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(fr_email);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@fr_email+'%」 的方式
			subSql += " And fr_email Like '%'+@fr_email+'%'";
			sbstring.Append("@fr_email");
		}

		// 檢查 fr_desc 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(fr_desc);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@fr_desc+'%」 的方式
			subSql += " And fr_desc Like '%'+@fr_desc+'%'";
			sbstring.Append("@fr_desc");
		}

		// 檢查 fr_time 開始範圍是否有值
		if (DateTime.TryParse(btime, out cktime))
			subSql += " And fr_time >= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		// 檢查 bh_time 結束範圍是否有值
		if (DateTime.TryParse(etime, out cktime))
			subSql += " And fr_time <= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		ParaString = sbstring.ToString();

		if (subSql != "")
			subSql = " Where" + subSql.Substring(4);

		return subSql;
	}
}
