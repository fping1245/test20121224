//---------------------------------------------------------------------------- 
//程式功能	取得 Ca_Group 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public class ODS_Ca_Group_DataReader
{
	private string Sql_ConnString = "";

	public ODS_Ca_Group_DataReader()
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

	public SqlDataReader Select_Ca_Group(string SortColumn, int startRowIndex, int maximumRows, int mg_sid)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString = SqlString + "Select cg_sid, cg_sort, cg_name, cg_desc";
		SqlString = SqlString + ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString = SqlString + "cg_sort";
		else
			SqlString = SqlString + SortColumn;
		SqlString = SqlString + ") as rownum  From Ca_Group";

		// 產生 Where 字串內容
		SqlString = SqlString + " Where mg_sid = @mg_sid) as Mg";

		SqlString = SqlString + " Where rownum Between " + (startRowIndex + 1).ToString() + " And " + (startRowIndex + maximumRows).ToString();

		// 排序設定
		SqlString = SqlString + "Order by rownum";

		// 建立資料庫連結
		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);

		// 建立命令物件
		SqlCommand Sql_Command = new SqlCommand();
		Sql_Command.Parameters.AddWithValue("mg_sid", mg_sid);

		Sql_Command.Connection = Sql_Conn;
		Sql_Command.CommandText = SqlString;

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Ca_Group(string SortColumn, int startRowIndex, int maximumRows, int mg_sid)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Ca_Group";
		SqlString = SqlString + " Where mg_sid = @mg_sid";

		using (Sql_conn)
		{
			Sql_Command.Connection = Sql_conn;
			Sql_Command.CommandText = SqlString;
			Sql_Command.Parameters.AddWithValue("mg_sid", mg_sid);

			Sql_conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Ca_Group"] = nRows;

		return (int)context.Cache["GetCount_Ca_Group"];
	}
}