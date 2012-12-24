//---------------------------------------------------------------------------- 
//程式功能	取得 As_Book 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public class ODS_As_Book_DataReader
{
	private string Sql_ConnString = "";

	public ODS_As_Book_DataReader()
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

	public SqlDataReader Select_As_Book(string SortColumn, int startRowIndex, int maximumRows,
		int mg_sid, string ab_name, string ab_nike, string ab_company, string ag_name, string ag_attrib)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString = SqlString + "Select b.ab_sid, b.ab_name, b.ab_nike, b.ab_zipcode, b.ab_address, b.ab_tel_h, b.ab_tel_o";
		SqlString = SqlString + ", b.ab_mobil, b.ab_fax, b.ab_email, b.ab_company, b.ab_posit, b.init_time";
		SqlString = SqlString + ", g.ag_name, g.ag_attrib, (Case When ab_photo Is Null Then 0 Else 1 End) as is_photo";
		SqlString = SqlString + ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString = SqlString + "b.ab_name";
		else
			SqlString = SqlString + SortColumn;

		SqlString = SqlString + ") as rownum From As_Book b";
		SqlString = SqlString + " Left Outer Join As_Group g On b.ag_sid = g.ag_sid";

		// 產生 Where 字串內容
		SqlString = SqlString + " Where b.mg_sid = @mg_sid" + GetSqlString(ab_name, ab_nike, ab_company, ag_name, ag_attrib) +") as MLog";

		SqlString = SqlString + " Where rownum Between " + (startRowIndex + 1).ToString() + " And " + (startRowIndex + maximumRows).ToString();

		// 排序設定
		SqlString = SqlString + " Order by rownum";

		// 建立資料庫連結
		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);

		// 建立命令物件
		SqlCommand Sql_Command = new SqlCommand();

		Sql_Command.Connection = Sql_Conn;
		Sql_Command.CommandText = SqlString;
		Sql_Command.Parameters.AddWithValue("mg_sid", mg_sid);

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_As_Book(string SortColumn, int startRowIndex, int maximumRows,
		int mg_sid, string ab_name, string ab_nike, string ab_company, string ag_name, string ag_attrib)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From As_Book b";
		SqlString = SqlString + " Left Outer Join As_Group g On b.ag_sid = g.ag_sid";
		SqlString = SqlString + " Where b.mg_sid = @mg_sid" + GetSqlString(ab_name, ab_nike, ab_company, ag_name, ag_attrib);

		using (Sql_conn)
		{
			Sql_Command.Connection = Sql_conn;
			Sql_Command.CommandText = SqlString;
			Sql_Command.Parameters.AddWithValue("mg_sid", mg_sid);

			Sql_conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_As_Book"] = nRows;

		return (int)context.Cache["GetCount_As_Book"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string ab_name, string ab_nike, string ab_company, string ag_name, string ag_attrib)
	{
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";

		// 檢查 ab_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ab_name);
		if (tmpstr != "")
			subSql += " And b.ab_name Like '%" + tmpstr + "%'";

		// 檢查 ab_nike 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ab_nike);
		if (tmpstr != "")
			subSql += " And b.ab_nike Like '%" + tmpstr + "%'";

		// 檢查 ab_company 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ab_company);
		if (tmpstr != "")
			subSql += " And b.ab_company Like '%" + tmpstr + "%'";

		// 檢查 ag_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ag_name);
		if (tmpstr != "")
			subSql += " And g.ag_name Like '%" + tmpstr + "%'";

		// 檢查 ag_attrib 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ag_attrib);
		if (tmpstr != "")
			subSql += " And g.ag_attrib Like '%" + tmpstr + "%'";

		return subSql;
	}
}
