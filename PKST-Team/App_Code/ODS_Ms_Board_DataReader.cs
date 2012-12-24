//---------------------------------------------------------------------------- 
//程式功能	取得 Ms_Board 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Ms_Board_DataReader
{
	private string Sql_ConnString = "";
	private string ParaString = "";

	public ODS_Ms_Board_DataReader()
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

	public SqlDataReader Select_Ms_Board(string SortColumn, int startRowIndex, int maximumRows,
		string is_close, string mb_name, string mb_email, string mb_desc, string btime, string etime)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select mb_sid, mb_symbol, mb_name, mb_sex, mb_email, mb_time, mb_ip, mb_desc";
		SqlString += ", is_show, instead, is_close, Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "mb_sid DESC";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Ms_Board";

		// 產生 Where 字串內容
		SqlString += GetSqlString(is_close, mb_name, mb_email, mb_desc, btime, etime) + ") as MLog";

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
		if (ParaString.Contains("@mb_name"))
			Sql_Command.Parameters.AddWithValue("mb_name", mb_name);

		if (ParaString.Contains("@mb_email"))
			Sql_Command.Parameters.AddWithValue("mb_email", mb_email);

		if (ParaString.Contains("@mb_desc"))
			Sql_Command.Parameters.AddWithValue("mb_desc", mb_desc);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Ms_Board(string SortColumn, int startRowIndex, int maximumRows,
		string is_close, string mb_name, string mb_email, string mb_desc, string btime, string etime)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Ms_Board";
		SqlString += GetSqlString(is_close, mb_name, mb_email, mb_desc, btime, etime);

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			if (ParaString.Contains("@mb_name"))
				Sql_Command.Parameters.AddWithValue("mb_name", mb_name);

			if (ParaString.Contains("@mb_email"))
				Sql_Command.Parameters.AddWithValue("mb_email", mb_email);

			if (ParaString.Contains("@mb_desc"))
				Sql_Command.Parameters.AddWithValue("mb_desc", mb_desc);
			#endregion

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Ms_Board"] = nRows;

		return (int)context.Cache["GetCount_Ms_Board"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string is_close, string mb_name, string mb_email, string mb_desc, string btime, string etime)
	{
		StringBuilder sbstring = new StringBuilder();
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;
		DateTime cktime;

		// 檢查 is_close 是否有值
		if (int.TryParse(is_close, out ckint))
			subSql += " And is_close = " + ckint.ToString();

		// 檢查 mb_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(mb_name);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@mb_name+'%」 的方式
			subSql += " And mb_name Like '%'+@mb_name+'%'";
			sbstring.Append("@mb_name");
		}

		// 檢查 mb_email 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(mb_email);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@mb_email+'%」 的方式
			subSql += " And mb_email Like '%'+@mb_email+'%'";
			sbstring.Append("@mb_email");
		}

		// 檢查 mb_desc 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(mb_desc);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@mb_desc+'%」 的方式
			subSql += " And mb_desc Like '%'+@mb_desc+'%'";
			sbstring.Append("@mb_desc");
		}

		// 檢查 mb_time 開始範圍是否有值
		if (DateTime.TryParse(btime, out cktime))
			subSql += " And mb_time >= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		// 檢查 bh_time 結束範圍是否有值
		if (DateTime.TryParse(etime, out cktime))
			subSql += " And mb_time <= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		ParaString = sbstring.ToString();

		if (subSql != "")
			subSql = " Where" + subSql.Substring(4);

		return subSql;
	}
}
