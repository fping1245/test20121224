//---------------------------------------------------------------------------- 
//程式功能	取得 Manager 表格資料
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public class ODS_Manager_DataReader
{
	private string Sql_ConnString = "";

	public ODS_Manager_DataReader()
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

	public SqlDataReader Select_Manager(string SortColumn, int startRowIndex, int maximumRows,
		string mg_sid, string mg_name, string mg_nike, string btime, string etime)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select mg_sid, mg_name, mg_nike, mg_unit, mg_id, last_date, init_time";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "mg_sid";
		else
			SqlString += SortColumn;
		SqlString += ") as rownum  From Manager";

		// 產生 Where 字串內容
		SqlString += GetSqlString(mg_sid, mg_name, mg_nike, btime, etime) + ") as Mg";

		SqlString += " Where rownum Between " + (startRowIndex + 1).ToString() + " And " + (startRowIndex + maximumRows).ToString();

		// 排序設定
		SqlString += "Order by rownum";

		// 建立資料庫連結
		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);

		// 建立命令物件
		SqlCommand Sql_Command = new SqlCommand();

		Sql_Command.Connection = Sql_Conn;
		Sql_Command.CommandText = SqlString;

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Manager(string SortColumn, int startRowIndex, int maximumRows,
		string mg_sid, string mg_name, string mg_nike, string btime, string etime)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Manager";
		SqlString += GetSqlString(mg_sid, mg_name, mg_nike, btime, etime);

		using (Sql_conn)
		{
			Sql_Command.Connection = Sql_conn;
			Sql_Command.CommandText = SqlString;

			Sql_conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Manager"] = nRows;

		return (int)context.Cache["GetCount_Manager"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string mg_sid, string mg_name, string mg_nike, string btime, string etime)
	{
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;
		DateTime cktime;

		if (int.TryParse(mg_sid, out ckint))
			subSql += " And mg_sid = " + ckint.ToString();

		// 檢查 mg_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(mg_name);
		if (tmpstr != "")
			subSql += " And mg_name Like '%" + tmpstr + "%'";

		// 檢查 mg_nike 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(mg_nike);
		if (tmpstr != "")
			subSql += " And mg_nike Like '%" + tmpstr + "%'";

		// 檢查開始時間是否有值
		if (DateTime.TryParse(btime, out cktime))
			subSql += " And last_date >= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		// 檢查結束時間是否有值
		if (DateTime.TryParse(etime, out cktime))
			subSql += " And last_date <= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		if (subSql != "")
			subSql = " Where" + subSql.Substring(4);

		return subSql;
	}
}
