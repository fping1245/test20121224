//---------------------------------------------------------------------------- 
//程式功能	取得 Db_Table 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Db_Table_DataReader
{
	private string Sql_ConnString = "";
	private string ParaString = "";

	public ODS_Db_Table_DataReader()
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

	public SqlDataReader Select_Db_Table(string SortColumn, int startRowIndex, int maximumRows,
		int ds_sid, string dt_name, string dt_caption, string dt_area)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select dt_sid, ds_sid, dt_sort, dt_name, dt_caption, dt_area, dt_desc, dt_index, dt_modi, init_time";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "dt_sort";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Db_Table";

		// 產生 Where 字串內容
		SqlString += " Where ds_sid = @ds_sid" + GetSqlString(dt_name, dt_caption, dt_area) + ") as MLog";

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
		Sql_Command.Parameters.AddWithValue("ds_sid", ds_sid);
		if (ParaString.Contains("@dt_name"))
			Sql_Command.Parameters.AddWithValue("dt_name", dt_name);
		if (ParaString.Contains("@dt_caption"))
			Sql_Command.Parameters.AddWithValue("dt_caption", dt_caption);
		if (ParaString.Contains("@dt_area"))
			Sql_Command.Parameters.AddWithValue("dt_area", dt_area);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Db_Table(string SortColumn, int startRowIndex, int maximumRows,
		int ds_sid, string dt_name, string dt_caption, string dt_area)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Db_Table";
		SqlString += " Where ds_sid = @ds_sid" + GetSqlString(dt_name, dt_caption, dt_area);

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			Sql_Command.Parameters.AddWithValue("ds_sid", ds_sid);
			if (ParaString.Contains("@dt_name"))
				Sql_Command.Parameters.AddWithValue("dt_name", dt_name);
			if (ParaString.Contains("@dt_caption"))
				Sql_Command.Parameters.AddWithValue("dt_caption", dt_caption);
			if (ParaString.Contains("@dt_area"))
				Sql_Command.Parameters.AddWithValue("dt_caption", dt_area);
			#endregion

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Db_Table"] = nRows;

		return (int)context.Cache["GetCount_Db_Table"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string dt_name, string dt_caption, string dt_area)
	{
		StringBuilder sbstring = new StringBuilder();
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";

		// 檢查 dt_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(dt_name);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@dt_name+'%」 的方式
			subSql += " And dt_name Like '%'+@dt_name+'%'";
			sbstring.Append("@dt_name");
		}

		// 檢查 dt_caption 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(dt_caption);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@dt_caption+'%」 的方式
			subSql += " And dt_caption Like '%'+@dt_caption+'%'";
			sbstring.Append("@dt_caption");
		}

		// 檢查 dt_area 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(dt_area);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@dt_area+'%」 的方式
			subSql += " And dt_area Like '%'+@dt_area+'%'";
			sbstring.Append("@dt_area");
		}

		ParaString = sbstring.ToString();

		return subSql;
	}
}
