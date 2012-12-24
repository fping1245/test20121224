//---------------------------------------------------------------------------- 
//程式功能	取得 Db_Sys 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Db_Sys_DataReader
{
	private string Sql_ConnString = "";
	private string ParaString = "";

	public ODS_Db_Sys_DataReader()
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

	public SqlDataReader Select_Db_Sys(string SortColumn, int startRowIndex, int maximumRows,
		string ds_sid, string ds_code, string ds_name, string ds_database)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select ds_sid, ds_code, ds_name, ds_database, ds_id, ds_pass, ds_desc, init_time";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "ds_code";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Db_Sys";

		// 產生 Where 字串內容
		SqlString += GetSqlString(ds_sid, ds_code, ds_name, ds_database) + ") as MLog";

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
		if (ParaString.Contains("@ds_code"))
			Sql_Command.Parameters.AddWithValue("ds_code", ds_code);
		if (ParaString.Contains("@ds_name"))
			Sql_Command.Parameters.AddWithValue("ds_name", ds_name);
		if (ParaString.Contains("@ds_database"))
			Sql_Command.Parameters.AddWithValue("ds_database", ds_database);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Db_Sys(string SortColumn, int startRowIndex, int maximumRows,
		string ds_sid, string ds_code, string ds_name, string ds_database)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Db_Sys";
		SqlString += GetSqlString(ds_sid, ds_code, ds_name, ds_database);

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			if (ParaString.Contains("@ds_code"))
				Sql_Command.Parameters.AddWithValue("ds_code", ds_code);
			if (ParaString.Contains("@ds_name"))
				Sql_Command.Parameters.AddWithValue("ds_name", ds_name);
			if (ParaString.Contains("@ds_database"))
				Sql_Command.Parameters.AddWithValue("ds_name", ds_database);
			#endregion

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Db_Sys"] = nRows;

		return (int)context.Cache["GetCount_Db_Sys"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string ds_sid, string ds_code, string ds_name, string ds_database)
	{
		StringBuilder sbstring = new StringBuilder();
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;

		// 檢查 ds_sid 是否有值
		if (int.TryParse(ds_sid, out ckint))
		{
			subSql += " And ds_sid = " + ckint.ToString();
		}

		// 檢查 ds_code 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ds_code);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@ds_code+'%」 的方式
			subSql += " And ds_code Like '%'+@ds_code+'%'";
			sbstring.Append("@ds_code");
		}

		// 檢查 ds_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ds_name);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@ds_name+'%」 的方式
			subSql += " And ds_name Like '%'+@ds_name+'%'";
			sbstring.Append("@ds_name");
		}

		// 檢查 ds_database 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ds_database);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@ds_database+'%」 的方式
			subSql += " And ds_database Like '%'+@ds_database+'%'";
			sbstring.Append("@ds_database");
		}

		if (subSql != "")
			subSql = " Where" + subSql.Substring(4);

		ParaString = sbstring.ToString();

		return subSql;
	}
}
