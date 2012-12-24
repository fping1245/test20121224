//---------------------------------------------------------------------------- 
//程式功能	取得 POP3_Mail 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;

public class ODS_POP3_Mail_DataReader
{
	private string Sql_ConnString = "";

	public ODS_POP3_Mail_DataReader()
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

	public SqlDataReader Select_POP3_Mail(string SortColumn, int startRowIndex, int maximumRows,
		int ppa_sid)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select ppa_sid, ppm_sid, ppm_subject, ppm_size, r_name, r_email, r_time, s_name, s_email, s_time, init_time";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "ppm_sn";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From POP3_Mail Where ppa_sid = @ppa_sid) as MLog";

		SqlString += " Where rownum Between " + (startRowIndex + 1).ToString() + " And " + (startRowIndex + maximumRows).ToString();

		// 排序設定
		SqlString += " Order by rownum";

		// 建立資料庫連結
		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);

		// 建立命令物件
		SqlCommand Sql_Command = new SqlCommand();

		Sql_Command.Connection = Sql_Conn;
		Sql_Command.CommandText = SqlString;
		Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_POP3_Mail(string SortColumn, int startRowIndex, int maximumRows,
		int ppa_sid)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From POP3_Mail Where ppa_sid = @ppa_sid";

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_POP3_Mail"] = nRows;

		return (int)context.Cache["GetCount_POP3_Mail"];
	}
}
