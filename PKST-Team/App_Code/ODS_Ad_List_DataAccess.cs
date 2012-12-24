//---------------------------------------------------------------------------- 
//程式功能	存取 Ad_List 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class ODS_Ad_List_DataAccess
{
	private string Sql_ConnString = "";
	private string ParaString = "";

	public ODS_Ad_List_DataAccess()
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
	public SqlDataReader Select_Ad_List(string SortColumn, int startRowIndex, int maximumRows,
		string adm_sid, string adl_sid, string adl_email, string adl_send, string btime, string etime)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		//SqlString += "Select adm_sid, adl_sid, adl_email, (Case adl_send When 0 Then '未發送' When 1 Then '已發送' Else '錯誤！' End) As adl_send";
		SqlString += "Select adm_sid, adl_sid, adl_email, adl_send";
		SqlString += ", send_time, Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "adl_email";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Ad_List";

		// 產生 Where 字串內容
		SqlString += GetSqlString(adm_sid, adl_sid, adl_email, adl_send, btime, etime) + ") as MLog";

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
		if (ParaString.Contains("@adl_email"))
			Sql_Command.Parameters.AddWithValue("adl_email", adl_email);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	// 取得資料筆數
	public int GetCount_Ad_List(string SortColumn, int startRowIndex, int maximumRows,
		string adm_sid, string adl_sid, string adl_email, string adl_send, string btime, string etime)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Ad_List";
		SqlString = SqlString + GetSqlString(adm_sid, adl_sid, adl_email, adl_send, btime, etime);

		using (Sql_Conn)
		{
			Sql_Command.Connection = Sql_Conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			if (ParaString.Contains("@adl_email"))
				Sql_Command.Parameters.AddWithValue("adl_email", adl_email);
			#endregion

			Sql_Conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Ad_List"] = nRows;

		return (int)context.Cache["GetCount_Ad_List"];
	}

	// 更新資料
	public int Update_Ad_List(int adm_sid, int adl_sid, string adl_email, int adl_send)
	{
		int rtn_value = 0;

		string SqlString = "";

		SqlString = "Update Ad_List Set adl_email = @adl_email, adl_send = @adl_send Where adm_sid = @adm_sid And adl_sid = @adl_sid";

		using (SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString))
		{
			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("adm_sid", adm_sid);
				Sql_Command.Parameters.AddWithValue("adl_sid", adl_sid);
				Sql_Command.Parameters.AddWithValue("adl_email", adl_email);
				Sql_Command.Parameters.AddWithValue("adl_send", adl_send);

				rtn_value = Sql_Command.ExecuteNonQuery();

				Sql_Conn.Close();
			}
		}

		return rtn_value;
	}

	// 新增資料
	public int Insert_Ad_List(int adm_sid, string adl_email, int adl_send)
	{
		int adl_sid = -1;
		string SqlString = "";

		SqlString = "Insert Into Ad_List (adm_sid, adl_email, adl_send) Values (@adm_sid, @adl_email, @adl_send);";
		SqlString += "Select @adl_sid = Scope_Identity()";

		using (SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString))
		{
			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("adm_sid", adm_sid);
				Sql_Command.Parameters.AddWithValue("adl_email", adl_email);
				Sql_Command.Parameters.AddWithValue("adl_send", adl_send);

				SqlParameter spt_adl_sid = Sql_Command.Parameters.Add("adl_sid", SqlDbType.Int);
				spt_adl_sid.Direction = ParameterDirection.Output;

				Sql_Command.ExecuteNonQuery();

				if (spt_adl_sid.Value != null)
					adl_sid = (int)spt_adl_sid.Value;

				Sql_Conn.Close();
			}
		}

		return adl_sid;
	}

	// 刪除資料
	public int Delete_Ad_List(int adl_sid, int adm_sid)
	{
		int rtn_value = -1;
		string SqlString = "";

		SqlString = "Delete Ad_List Where adm_sid = @adm_sid And adl_sid = @adl_sid";

		using (SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString))
		{
			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("adl_sid", adl_sid);
				Sql_Command.Parameters.AddWithValue("adm_sid", adm_sid);

				rtn_value = Sql_Command.ExecuteNonQuery();

				Sql_Conn.Close();
			}
		}

		return rtn_value;
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string adm_sid, string adl_sid, string adl_email, string adl_send, string btime, string etime)
	{
		StringBuilder sbstring = new StringBuilder();
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;
		DateTime cktime;

		// 檢查 adm_sid 是否有值
		if (int.TryParse(adm_sid, out ckint))
		{
			subSql += " And adm_sid = " + ckint.ToString();
		}

		// 檢查 adl_sid 是否有值
		if (int.TryParse(adl_sid, out ckint))
		{
			subSql += " And adl_sid = " + ckint.ToString();
		}

		// 檢查 adl_send 是否有值
		if (int.TryParse(adl_send, out ckint))
		{
			subSql += " And adl_send= " + ckint.ToString();
		}

		// 檢查 adl_email 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(adl_email);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@adl_email+'%」 的方式
			subSql += " And adl_email Like '%'+@adl_email+'%'";
			sbstring.Append("@adl_email");
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
