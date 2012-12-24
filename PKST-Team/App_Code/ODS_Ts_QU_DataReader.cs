//---------------------------------------------------------------------------- 
//程式功能	取得 Ts_UQuest 與 Ts_Question 配合的資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Ts_QU_DataReader
{
	private string Sql_ConnString = "";

	public ODS_Ts_QU_DataReader()
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

	public SqlDataReader Select_Ts_QU(string SortColumn, int startRowIndex, int maximumRows,
		int tu_sid, int tp_sid)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select q.tq_sid, q.tq_sort, q.tq_desc, q.tq_type, q.tq_item, q.tq_score";
		SqlString += ", IsNull(u.tuq_score,0) as tuq_score, u.init_time, IsNull(u.tu_sid, -1) as tu_sid";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "q.tq_sort";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Ts_Question q";
		SqlString += " Left Outer Join Ts_UQuest u On u.tu_sid = @tu_sid And q.tp_sid = u.tp_sid And q.tq_sid = u.tq_sid";

		// 產生 Where 字串內容
		SqlString += " Where q.tp_sid = @tp_sid) as MLog";

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
		Sql_Command.Parameters.AddWithValue("tu_sid", tu_sid);
		Sql_Command.Parameters.AddWithValue("tp_sid", tp_sid);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Ts_QU(string SortColumn, int startRowIndex, int maximumRows,
		int tu_sid, int tp_sid)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Ts_Question q";
		SqlString += " Where q.tp_sid = @tp_sid";

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			Sql_Command.Parameters.AddWithValue("tu_sid", tu_sid);
			Sql_Command.Parameters.AddWithValue("tp_sid", tp_sid);
			#endregion

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Ts_QU"] = nRows;

		return (int)context.Cache["GetCount_Ts_QU"];
	}
}
