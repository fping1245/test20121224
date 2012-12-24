//---------------------------------------------------------------------------- 
//程式功能	取得 Ts_User 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Ts_User_DataReader
{
	private string Sql_ConnString = "";
	private string ParaString = "";

	public ODS_Ts_User_DataReader()
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

	public SqlDataReader Select_Ts_User(string SortColumn, int startRowIndex, int maximumRows,
		string tp_sid, string tu_name, string tu_no, string tu_ip)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select tu_sid, tu_name, tu_no, tu_ip, tu_sort, tu_score, tu_question, b_time, e_time, is_test";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "tu_sort";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Ts_User";

		// 產生 Where 字串內容
		SqlString += GetSqlString(tp_sid, tu_name, tu_no, tu_ip) + ") as MLog";

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
		if (ParaString.Contains("@tu_name"))
			Sql_Command.Parameters.AddWithValue("tu_name", tu_name);

		if (ParaString.Contains("@tu_no"))
			Sql_Command.Parameters.AddWithValue("tu_no", tu_no);

		if (ParaString.Contains("@tu_ip"))
			Sql_Command.Parameters.AddWithValue("tu_ip", tu_ip);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Ts_User(string SortColumn, int startRowIndex, int maximumRows,
		string tp_sid, string tu_name, string tu_no, string tu_ip)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Ts_User";
		SqlString += GetSqlString(tp_sid, tu_name, tu_no, tu_ip);

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			if (ParaString.Contains("@tu_name"))
				Sql_Command.Parameters.AddWithValue("tu_name", tu_name);

			if (ParaString.Contains("@tu_no"))
				Sql_Command.Parameters.AddWithValue("tu_no", tu_no);

			if (ParaString.Contains("@tu_ip"))
				Sql_Command.Parameters.AddWithValue("tu_ip", tu_ip);
			#endregion

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Ts_User"] = nRows;

		return (int)context.Cache["GetCount_Ts_User"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string tp_sid, string tu_name, string tu_no, string tu_ip)
	{
		StringBuilder sbstring = new StringBuilder();
		Common_Func cfc = new Common_Func();
		int ckint = -1;
		string subSql = "", tmpstr = "";

		// 檢查 tp_sid 是否有值
		if (int.TryParse(tp_sid, out ckint))
			subSql += " And tp_sid = " + ckint.ToString();
		else
			subSql += " And tp_sid = -1";

		// 檢查 tu_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(tu_name);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@tu_name+'%」 的方式
			subSql += " And tu_name Like '%'+@tu_name+'%'";
			sbstring.Append("@tu_name");
		}

		// 檢查 tu_no 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(tu_no);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@tu_no+'%」 的方式
			subSql += " And tu_no Like '%'+@tu_no+'%'";
			sbstring.Append("@tu_no");
		}

		// 檢查 tu_ip 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(tu_ip);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@tu_ip+'%」 的方式
			subSql += " And tu_ip Like '%'+@tu_ip+'%'";
			sbstring.Append("@tu_ip");
		}

		if (subSql != "")
			subSql = " Where" + subSql.Substring(4);

		ParaString = sbstring.ToString();

		return subSql;
	}
}
