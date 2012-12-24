//---------------------------------------------------------------------------- 
//程式功能	存取 Ad_Member 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Ad_Member_DataAccess
{
	private string Sql_ConnString = "";
	private string ParaString = "";

	public ODS_Ad_Member_DataAccess()
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

	// 取得資料
	public SqlDataReader Select_Ad_Member(string SortColumn, int startRowIndex, int maximumRows,
		string adb_sid, string adb_name, string adb_email, string btime, string etime)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select adb_sid, adb_name, adb_email, init_time";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "adb_sid";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Ad_Member";

		// 產生 Where 字串內容
		SqlString += GetSqlString(adb_sid, adb_name, adb_email, btime, etime) + ") as MLog";

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
		if (ParaString.Contains("@adb_name"))
			Sql_Command.Parameters.AddWithValue("adb_name", adb_name);

		if (ParaString.Contains("@adb_email"))
			Sql_Command.Parameters.AddWithValue("adb_email", adb_email);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	// 取得資料筆數
	public int GetCount_Ad_Member(string SortColumn, int startRowIndex, int maximumRows,
		string adb_sid, string adb_name, string adb_email, string btime, string etime)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Ad_Member";
		SqlString = SqlString + GetSqlString(adb_sid, adb_name, adb_email, btime, etime);

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			if (ParaString.Contains("@adb_name"))
				Sql_Command.Parameters.AddWithValue("adb_name", adb_name);

			if (ParaString.Contains("@adb_email"))
				Sql_Command.Parameters.AddWithValue("adb_email", adb_email);
			#endregion

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Ad_Member"] = nRows;

		return (int)context.Cache["GetCount_Ad_Member"];
	}

	// 更新資料
	public int Update_Ad_Member(int adb_sid, string adb_name, string adb_email)
	{
		int rtn_value = 0;

		string SqlString = "";

		SqlString = "Update Ad_Member Set adb_name = @adb_name, adb_email = @adb_email Where adb_sid = @adb_sid";

		using (SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString))
		{
			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("adb_sid", adb_sid);
				Sql_Command.Parameters.AddWithValue("adb_name", adb_name);
				Sql_Command.Parameters.AddWithValue("adb_email", adb_email);

				rtn_value = Sql_Command.ExecuteNonQuery();

				Sql_Conn.Close();
			}
		}

		return rtn_value;
	}

	// 新增資料
	public int Insert_Ad_Member(string adb_name, string adb_email)
	{
		int adb_sid = -1;
		string SqlString = "";

		SqlString = "Insert Into Ad_Member (adb_email, adb_name) Values (@adb_email, @adb_name);";
		SqlString += "Select @adb_sid = Scope_Identity()";

		using (SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString))
		{
			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("adb_email", adb_email);
				Sql_Command.Parameters.AddWithValue("adb_name", adb_name);

				SqlParameter spt_adb_sid = Sql_Command.Parameters.Add("adb_sid", SqlDbType.Int);
				spt_adb_sid.Direction = ParameterDirection.Output;

				Sql_Command.ExecuteNonQuery();

				if (spt_adb_sid.Value != null)
					adb_sid = (int)spt_adb_sid.Value;

				Sql_Conn.Close();
			}
		}

		return adb_sid;
	}

	// 刪除資料
	public int Delete_Ad_Member(int adb_sid)
	{
		int rtn_value = -1;
		string SqlString = "";

		SqlString = "Delete Ad_Member Where adb_sid = @adb_sid";

		using (SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString))
		{
			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("adb_sid", adb_sid);

				rtn_value = Sql_Command.ExecuteNonQuery();

				Sql_Conn.Close();
			}
		}

		return rtn_value;
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string adb_sid, string adb_name, string adb_email, string btime, string etime)
	{
		StringBuilder sbstring = new StringBuilder();
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;
		DateTime cktime;

		// 檢查 adb_sid 是否有值
		if (int.TryParse(adb_sid, out ckint))
		{
			subSql += " And adb_sid = " + ckint.ToString();
		}

		// 檢查 adb_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(adb_name);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@he_title+'%」 的方式
			subSql += " And adb_name Like '%'+@adb_name+'%'";
			sbstring.Append("@adb_name");
		}

		// 檢查 adb_email 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(adb_email);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@adb_email+'%」 的方式
			subSql += " And adb_email Like '%'+@adb_email+'%'";
			sbstring.Append("@adb_email");
		}

		// 檢查 send_time 開始範圍是否有值
		if (DateTime.TryParse(btime, out cktime))
			subSql += " And send_time >= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		// 檢查 send_time 結束範圍是否有值
		if (DateTime.TryParse(etime, out cktime))
			subSql += " And send_time <= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";

		if (subSql != "")
			subSql = " Where" + subSql.Substring(4);

		ParaString = sbstring.ToString();

		return subSql;
	}
}
