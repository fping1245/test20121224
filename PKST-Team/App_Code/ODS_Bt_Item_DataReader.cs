//---------------------------------------------------------------------------- 
//程式功能	取得 Bt_Item 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Bt_Item_DataReader
{
	private string Sql_ConnString = "";

	public ODS_Bt_Item_DataReader()
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

	public SqlDataReader Select_Bt_Item(string SortColumn, int startRowIndex, int maximumRows,
		int bh_sid)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select bh_sid, bi_sid, bi_sort, bi_desc, bi_total, bi_time, init_time";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "bi_sort";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Bt_Item Where bh_sid = @bh_sid) as MLog";

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

		#region 加入條件參數
		Sql_Command.Parameters.AddWithValue("bh_sid", bh_sid);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Bt_Item(string SortColumn, int startRowIndex, int maximumRows,
		int bh_sid)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Bt_Item Where bh_sid = @bh_sid";

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			Sql_Command.Parameters.AddWithValue("bh_sid", bh_sid);
			#endregion

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Bt_Item"] = nRows;

		return (int)context.Cache["GetCount_Bt_Item"];
	}
}
