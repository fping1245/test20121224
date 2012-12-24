//---------------------------------------------------------------------------- 
//程式功能	取得 Bt_Schedule 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Bt_Schedule_DataReader
{
	private string Sql_ConnString = "";

	public ODS_Bt_Schedule_DataReader()
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

	public SqlDataReader Select_Bt_Schedule(string SortColumn, int startRowIndex, int maximumRows
		, string now_use, string is_show)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select s.bs_sid, s.bs_sort, s.bh_sid, s.s_time, s.e_time, s.is_show, s.now_use, s.init_time, h.bh_title";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "s.is_show, s.bs_sort";
		else
			SqlString += "s.is_show, " + SortColumn;

		SqlString += ") as rownum From Bt_Schedule s";
		SqlString += " Left Outer Join Bt_Head h On s.bh_sid = h.bh_sid";
		
		SqlString += GetSqlString(now_use, is_show) + ") as MLog";

		// 產生 Where 字串內容
		SqlString += " Where rownum Between " + (startRowIndex + 1).ToString() + " And " + (startRowIndex + maximumRows).ToString();

		// 排序設定
		SqlString += " Order by rownum";

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

	public int GetCount_Bt_Schedule(string SortColumn, int startRowIndex, int maximumRows
		, string now_use, string is_show)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Bt_Schedule" + GetSqlString(now_use, is_show);

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Bt_Schedule"] = nRows;

		return (int)context.Cache["GetCount_Bt_Schedule"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string now_use, string is_show)
	{
		string subSql = "";
		int ckint = 0;

		// 檢查 now_use 是否有值
		if (int.TryParse(now_use, out ckint))
		{
			subSql += " And s.now_use = " + ckint.ToString();
		}

		// 檢查 is_show 是否有值
		if (int.TryParse(is_show, out ckint))
		{
			subSql += " And s.is_show = " + ckint.ToString();
		}

		if (subSql != "")
			subSql = " Where" + subSql.Substring(4);

		return subSql;
	}
}
