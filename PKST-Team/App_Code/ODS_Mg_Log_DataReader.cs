//---------------------------------------------------------------------------- 
//程式功能	取得 Mg_Log 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public class ODS_Mg_Log_DataReader
{
	private string Sql_ConnString = "";
	private string ParaString = "";

	public ODS_Mg_Log_DataReader()
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

	public SqlDataReader Select_Mg_Log(string SortColumn, int startRowIndex, int maximumRows,
		string btime, string etime, string mg_sid, string mg_name, string fi_name1, string fi_name2, string lg_ip)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString += "Select l.mg_sid, m.mg_name, f1.fi_name1, f2.fi_name2, l.lg_time, l.lg_ip";
		SqlString += ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString += "l.lg_time DESC";
		else
			SqlString += SortColumn;

		SqlString += ") as rownum From Mg_Log l";
		SqlString += " Left Outer Join Manager m On l.mg_sid = m.mg_sid";
		SqlString += " Left Outer Join Func_Item2 f2 On l.fi_no2 = f2.fi_no2";
		SqlString += " Left Outer Join Func_Item1 f1 On f2.fi_no1 = f1.fi_no1";

		// 產生 Where 字串內容
		SqlString += GetSqlString(btime, etime, mg_sid, mg_name, fi_name1, fi_name2, lg_ip) +") as MLog";

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
		if (ParaString.Contains("@mg_sid"))
			Sql_Command.Parameters.AddWithValue("mg_sid", mg_sid);

		if (ParaString.Contains("@mg_name"))
			Sql_Command.Parameters.AddWithValue("mg_name", mg_name);

		if (ParaString.Contains("@fi_name1"))
			Sql_Command.Parameters.AddWithValue("fi_name1", fi_name1);

		if (ParaString.Contains("@fi_name2"))
			Sql_Command.Parameters.AddWithValue("fi_name2", fi_name2);

		if (ParaString.Contains("@lg_ip"))
			Sql_Command.Parameters.AddWithValue("lg_ip", lg_ip);
		#endregion

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Mg_Log(string SortColumn, int startRowIndex, int maximumRows,
		string btime, string etime, string mg_sid, string mg_name, string fi_name1, string fi_name2, string lg_ip)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Mg_Log l";
		SqlString += " Left Outer Join Manager m On l.mg_sid = m.mg_sid";
		SqlString += " Left Outer Join Func_Item2 f2 On l.fi_no2 = f2.fi_no2";
		SqlString += " Left Outer Join Func_Item1 f1 On f2.fi_no1 = f1.fi_no1";
		SqlString += GetSqlString(btime, etime, mg_sid, mg_name, fi_name1, fi_name2, lg_ip);

		using (Sql_conn)
		{
			Sql_Command.Connection = Sql_conn;
			Sql_Command.CommandText = SqlString;

			#region 加入條件參數
			if (ParaString.Contains("@mg_sid"))
				Sql_Command.Parameters.AddWithValue("mg_sid", mg_sid);

			if (ParaString.Contains("@mg_name"))
				Sql_Command.Parameters.AddWithValue("mg_name", mg_name);

			if (ParaString.Contains("@fi_name1"))
				Sql_Command.Parameters.AddWithValue("fi_name1", fi_name1);

			if (ParaString.Contains("@fi_name2"))
				Sql_Command.Parameters.AddWithValue("fi_name2", fi_name2);

			if (ParaString.Contains("@lg_ip"))
				Sql_Command.Parameters.AddWithValue("lg_ip", lg_ip);
			#endregion

			Sql_conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Mg_Log"] = nRows;

		return (int)context.Cache["GetCount_Mg_Log"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string btime, string etime, string mg_sid, string mg_name, string fi_name1, string fi_name2, string lg_ip)
	{
		StringBuilder sbstring = new StringBuilder();
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;
		DateTime cktime;

		// 檢查開始時間是否有值
		if (DateTime.TryParse(btime, out cktime))
		{
			subSql += " And l.lg_time >= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";
		}

		// 檢查結束時間是否有值
		if (DateTime.TryParse(etime, out cktime))
		{
			subSql += " And l.lg_time <= '" + cktime.ToString("yyyy/MM/dd HH:mm:ss") + "'";
		}

		if (int.TryParse(mg_sid, out ckint))
		{
			subSql += " And l.mg_sid = @mg_sid";
			sbstring.Append("@mg_sid");
		}

		// 檢查 mg_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(mg_name);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@mg_name+'%」 的方式
			subSql += " And m.mg_name Like '%'+@mg_name+'%'";
			sbstring.Append("@mg_name");
		}

		// 檢查 fi_name1 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(fi_name1);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@fi_name1+'%」 的方式
			subSql += " And f1.fi_name1 Like '%'+@fi_name1+'%'";
			sbstring.Append("@fi_name1");
		}

		// 檢查 fi_name2 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(fi_name2);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@fi_name2+'%」 的方式
			subSql += " And f2.fi_name2 Like '%'+@fi_name2+'%'";
			sbstring.Append("@fi_name2");
		}

		// 檢查 lg_ip 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(lg_ip);
		if (tmpstr != "")
		{
			// 使用 like 時 要用 「%'+@lg_ip+'%」 的方式
			subSql += " And l.lg_ip Like '%'+@lg_ip+'%'";
			sbstring.Append("@lg_ip");
		}

		if (subSql != "")
			subSql = " Where" + subSql.Substring(4);

		ParaString = sbstring.ToString();

		return subSql;
	}
}
