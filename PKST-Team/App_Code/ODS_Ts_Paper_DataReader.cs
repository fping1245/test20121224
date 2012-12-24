//---------------------------------------------------------------------------- 
//程式功能	取得 Ts_Paper 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Ts_Paper_DataReader
{
	private string Sql_ConnString = "";
	private string ParaString = "";

	public ODS_Ts_Paper_DataReader()
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

	public SqlDataReader Select_Ts_Paper(string SortColumn, int startRowIndex, int maximumRows,
		string tp_sid, string tp_title, string is_show, string btime, string etime)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select tp_sid, tp_title, is_show, b_time, e_time, tp_question, tp_score, tp_member, tp_total, init_time";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "tp_sid";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Ts_Paper";

		// 產生 Where 字串內容
		SqlString += GetSqlString(tp_sid, tp_title, is_show, btime, etime) + ") as MLog";

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
		if (ParaString.Contains("@tp_title"))
			Sql_Command.Parameters.AddWithValue("tp_title", tp_title);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Ts_Paper(string SortColumn, int startRowIndex, int maximumRows,
		string tp_sid, string tp_title, string is_show, string btime, string etime)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Ts_Paper";
		SqlString += GetSqlString(tp_sid, tp_title, is_show, btime, etime);

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			if (ParaString.Contains("@tp_title"))
				Sql_Command.Parameters.AddWithValue("tp_title", tp_title);
			#endregion

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Ts_Paper"] = nRows;

		return (int)context.Cache["GetCount_Ts_Paper"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string tp_sid, string tp_title, string is_show, string btime, string etime)
	{
		StringBuilder sbstring = new StringBuilder();
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;
		DateTime cktime;

		// 檢查 tp_sid 是否有值
		if (int.TryParse(tp_sid, out ckint))
		{
			subSql += " And tp_sid = " + ckint.ToString();
		}

		// 檢查 tp_title 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(tp_title);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@he_title+'%」 的方式
			subSql += " And tp_title Like '%'+@tp_title+'%'";
			sbstring.Append("@tp_title");
		}

		// 檢查 is_show 是否有值
		if (int.TryParse(is_show, out ckint))
		{
			if (ckint == 0)
				subSql += " And is_show= " + ckint.ToString();
			else
				subSql += " And is_show >= " + ckint.ToString();
		}
	
		// 檢查 bh_time 開始範圍是否有值
		if (DateTime.TryParse(btime, out cktime))
			subSql += " And ('" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "' Between b_time And e_time)";

		// 檢查 bh_time 結束範圍是否有值
		if (DateTime.TryParse(etime, out cktime))
			subSql += " And ('" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "' Between b_time And e_time)";

		if (subSql != "")
			subSql = " Where" + subSql.Substring(4);

		ParaString = sbstring.ToString();

		return subSql;
	}
}
