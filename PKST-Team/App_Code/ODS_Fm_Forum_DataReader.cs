//---------------------------------------------------------------------------- 
//程式功能	取得 Fm_Forum 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Fm_Forum_DataReader
{
	private string Sql_ConnString = "";
	private string ParaString = "";

	public ODS_Fm_Forum_DataReader()
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

	public SqlDataReader Select_Fm_Forum(string SortColumn, int startRowIndex, int maximumRows,
		string is_close, string ff_name, string ff_topic, string ff_desc, string btime, string etime)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select ff_sid, ff_symbol, ff_name, ff_sex, ff_email, ff_time, ff_ip, ff_topic, ff_desc, ff_response";
		SqlString += ", is_show, instead, is_close, Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "ff_top DESC, ff_sid DESC";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Fm_Forum";

		// 產生 Where 字串內容
		SqlString += GetSqlString(is_close, ff_name, ff_topic, ff_desc, btime, etime) + ") as MLog";

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
		if (ParaString.Contains("@ff_name"))
			Sql_Command.Parameters.AddWithValue("ff_name", ff_name);

		if (ParaString.Contains("@ff_topic"))
			Sql_Command.Parameters.AddWithValue("ff_topic", ff_topic);

		if (ParaString.Contains("@ff_desc"))
			Sql_Command.Parameters.AddWithValue("ff_desc", ff_desc);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Fm_Forum(string SortColumn, int startRowIndex, int maximumRows,
		string is_close, string ff_name, string ff_topic, string ff_desc, string btime, string etime)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Fm_Forum";
		SqlString += GetSqlString(is_close, ff_name, ff_topic, ff_desc, btime, etime);

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			if (ParaString.Contains("@ff_name"))
				Sql_Command.Parameters.AddWithValue("ff_name", ff_name);

			if (ParaString.Contains("@ff_topic"))
				Sql_Command.Parameters.AddWithValue("ff_topic", ff_topic);

			if (ParaString.Contains("@ff_desc"))
				Sql_Command.Parameters.AddWithValue("ff_desc", ff_desc);
			#endregion

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Fm_Forum"] = nRows;

		return (int)context.Cache["GetCount_Fm_Forum"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string is_close, string ff_name, string ff_topic, string ff_desc, string btime, string etime)
	{
		StringBuilder sbstring = new StringBuilder();
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;
		DateTime cktime;

		// 檢查 is_close 是否有值
		if (int.TryParse(is_close, out ckint))
			subSql += " And is_close = " + ckint.ToString();

		// 檢查 ff_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ff_name);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@ff_name+'%」 的方式
			subSql += " And ff_name Like '%'+@ff_name+'%'";
			sbstring.Append("@ff_name");
		}

		// 檢查 ff_topic 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ff_topic);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@ff_topic+'%」 的方式
			subSql += " And ff_topic Like '%'+@ff_topic+'%'";
			sbstring.Append("@ff_topic");
		}

		// 檢查 ff_desc 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ff_desc);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@ff_desc+'%」 的方式
			subSql += " And ff_desc Like '%'+@ff_desc+'%'";
			sbstring.Append("@ff_desc");
		}

		// 檢查 ff_time 開始範圍是否有值
		if (DateTime.TryParse(btime, out cktime))
			subSql += " And ff_time >= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		// 檢查 bh_time 結束範圍是否有值
		if (DateTime.TryParse(etime, out cktime))
			subSql += " And ff_time <= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		ParaString = sbstring.ToString();

		if (subSql != "")
			subSql = " Where" + subSql.Substring(4);

		return subSql;
	}
}
