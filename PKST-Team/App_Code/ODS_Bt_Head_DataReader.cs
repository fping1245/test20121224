//---------------------------------------------------------------------------- 
//程式功能	取得 Bt_Head 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Bt_Head_DataReader
{
	private string Sql_ConnString = "";
	private string ParaString = "";

	public ODS_Bt_Head_DataReader()
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

	public SqlDataReader Select_Bt_Head(string SortColumn, int startRowIndex, int maximumRows,
		string bh_sid, string bh_title, string is_check, string btime, string etime, string is_show, string now_use)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select h.bh_sid, h.bh_title, h.is_check, h.bh_acnt, h.bh_scnt, h.bh_total, h.bh_time, h.init_time";
		SqlString += ", IsNull(s.is_show, 0) As is_show, s.now_use";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "h.bh_sid";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Bt_Head h";
		SqlString += " Left Outer Join Bt_Schedule s On h.bh_sid = s.bh_sid";

		// 產生 Where 字串內容
		SqlString += GetSqlString(bh_sid, bh_title, is_check, btime, etime, is_show, now_use) + ") as MLog";

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
		if (ParaString.Contains("@bh_title"))
			Sql_Command.Parameters.AddWithValue("bh_title", bh_title);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Bt_Head(string SortColumn, int startRowIndex, int maximumRows,
		string bh_sid, string bh_title, string is_check, string btime, string etime, string is_show, string now_use)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Bt_Head h";
		SqlString += " Left Outer Join Bt_Schedule s On h.bh_sid = s.bh_sid";
		SqlString += GetSqlString(bh_sid, bh_title, is_check, btime, etime, is_show, now_use);

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			if (ParaString.Contains("@bh_title"))
				Sql_Command.Parameters.AddWithValue("bh_title", bh_title);
			#endregion

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Bt_Head"] = nRows;

		return (int)context.Cache["GetCount_Bt_Head"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string bh_sid, string bh_title, string is_check, string btime, string etime, string is_show, string now_use)
	{
		StringBuilder sbstring = new StringBuilder();
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;
		DateTime cktime;

		// 檢查 bh_sid 是否有值
		if (int.TryParse(bh_sid, out ckint))
		{
			subSql += " And h.bh_sid = " + ckint.ToString();
		}

		// 檢查 bh_title 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(bh_title);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@he_title+'%」 的方式
			subSql += " And h.bh_title Like '%'+@bh_title+'%'";
			sbstring.Append("@bh_title");
		}

		// 檢查 is_check 是否有值
		if (int.TryParse(is_check, out ckint))
		{
			if (ckint == 0)
				subSql += " And h.is_check= " + ckint.ToString();
			else
				subSql += " And h.is_check >= " + ckint.ToString();
		}
	
		// 檢查 bh_time 開始範圍是否有值
		if (DateTime.TryParse(btime, out cktime))
			subSql += " And h.bh_time >= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		// 檢查 bh_time 結束範圍是否有值
		if (DateTime.TryParse(etime, out cktime))
			subSql += " And h.bh_time <= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

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

		ParaString = sbstring.ToString();

		return subSql;
	}
}
