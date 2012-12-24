//---------------------------------------------------------------------------- 
//程式功能	取得 Func_Power 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public class ODS_Func_Power_DataReader
{
	private string Sql_ConnString = "";

	public ODS_Func_Power_DataReader()
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

	public SqlDataReader Select_Func_Power(string SortColumn, int startRowIndex, int maximumRows,
		string fi_no1, string fi_no2, string mg_sid, string mg_id, string mg_name, string mg_nike, string is_enable)
	{
		string SqlString = "";

		switch (is_enable)
		{
			case "-1":
				#region SQL 語法，全部人員 (但不顯示總管理)
				SqlString = "Select * From (";
				SqlString = SqlString + "Select m.mg_sid, m.mg_id, m.mg_name, m.mg_nike, IsNull(f.is_enable,0) as is_enable";
				SqlString = SqlString + ", Row_Number() Over (Order by ";

				// 排序設定
				if (SortColumn.Trim() == "")
					SqlString = SqlString + "m.mg_sid";
				else
					SqlString = SqlString + SortColumn;

				SqlString = SqlString + ") as rownum From Manager m";
				SqlString = SqlString + " Left Outer Join Func_Power f On m.mg_sid = f.mg_sid And (f.fi_no1 = @fi_no1 And f.fi_no2 = @fi_no2)";

				// 產生 Where 字串內容
				SqlString = SqlString + " Where m.mg_sid <> 0";
				SqlString = SqlString + GetSqlString(mg_sid, mg_id, mg_name, mg_nike) + ") as mTable";

				SqlString = SqlString + " Where rownum Between " + (startRowIndex + 1).ToString() + " And " + (startRowIndex + maximumRows).ToString();

				// 排序設定
				SqlString = SqlString + " Order by rownum";
				#endregion
				break;

			case "0":
				#region SQL 語法，無權限人員
				SqlString = "Select * From (";
				SqlString = SqlString + "Select m.mg_sid, m.mg_id, m.mg_name, m.mg_nike, 0 as is_enable";
				SqlString = SqlString + ", Row_Number() Over (Order by ";

				// 排序設定
				if (SortColumn.Trim() == "")
					SqlString = SqlString + "m.mg_sid";
				else
					SqlString = SqlString + SortColumn;

				SqlString = SqlString + ") as rownum From Manager m";

				// 產生 Where 字串內容
				SqlString = SqlString + " Where m.mg_sid Not In (Select mg_sid From Func_Power f Where f.fi_no1 = @fi_no1 And f.fi_no2 = @fi_no2 And f.is_enable = 1)";
				SqlString = SqlString + " And m.mg_sid <> 0";
				SqlString = SqlString + GetSqlString(mg_sid, mg_id, mg_name, mg_nike) + ") as mTable";

				SqlString = SqlString + " Where rownum Between " + (startRowIndex + 1).ToString() + " And " + (startRowIndex + maximumRows).ToString();

				// 排序設定
				SqlString = SqlString + " Order by rownum";
				#endregion
				break;

			default:
				#region SQL 語法，有權限人員
				SqlString = "Select * From (";
				SqlString = SqlString + "Select m.mg_sid, m.mg_id, m.mg_name, m.mg_nike, f.is_enable";
				SqlString = SqlString + ", Row_Number() Over (Order by ";

				// 排序設定
				if (SortColumn.Trim() == "")
					SqlString = SqlString + "m.mg_sid";
				else
					SqlString = SqlString + SortColumn;

				SqlString = SqlString + ") as rownum From Func_Power f";
				SqlString = SqlString + " Inner Join Manager m On f.mg_sid = m.mg_sid";

				// 產生 Where 字串內容
				SqlString = SqlString + " Where f.fi_no1 = @fi_no1 And f.fi_no2 = @fi_no2 And f.is_enable = 1 And m.mg_sid <> 0";
				SqlString = SqlString + GetSqlString(mg_sid, mg_id, mg_name, mg_nike) + ") as mTable";

				SqlString = SqlString + " Where rownum Between " + (startRowIndex + 1).ToString() + " And " + (startRowIndex + maximumRows).ToString();

				// 排序設定
				SqlString = SqlString + " Order by rownum";
				#endregion
				break;
		}

		// 建立資料庫連結
		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);

		// 建立命令物件
		SqlCommand Sql_Command = new SqlCommand();

		Sql_Command.Connection = Sql_Conn;
		Sql_Command.CommandText = SqlString;
		Sql_Command.Parameters.AddWithValue("fi_no1", fi_no1);
		Sql_Command.Parameters.AddWithValue("fi_no2", fi_no2);

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Func_Power(string SortColumn, int startRowIndex, int maximumRows,
		string fi_no1, string fi_no2, string mg_sid, string mg_id, string mg_name, string mg_nike, string is_enable)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		switch (is_enable)
		{
			case "-1":
				#region SQL 語法，全部人員
				SqlString = GetSqlString(mg_sid, mg_id, mg_name, mg_nike);

				if (SqlString != "")
					SqlString = "Select Count(*) as Cnt From Manager m Where" + SqlString.Substring(4);
				else
					SqlString = "Select Count(*) as Cnt From Manager";

				#endregion
				break;

			case "0":
				#region SQL 語法，無權限人員
				SqlString = "Select Count(*) as Cnt From Manager m";
				SqlString = SqlString + " Where m.mg_sid Not In (Select mg_sid From Func_Power f Where f.fi_no1 = @fi_no1 And f.fi_no2 = @fi_no2 And f.is_enable = 1)";
				SqlString = SqlString + GetSqlString(mg_sid, mg_id, mg_name, mg_nike);
				#endregion
				break;

			default:
				#region SQL 語法，有權限人員
				SqlString = "Select Count(*) as Cnt From Func_Power f";
				SqlString = SqlString + " Inner Join Manager m On f.mg_sid = m.mg_sid";
				SqlString = SqlString + " Where f.fi_no1 = @fi_no1 And f.fi_no2 = @fi_no2 And f.is_enable = 1";
				SqlString = SqlString + GetSqlString(mg_sid, mg_id, mg_name, mg_nike);
				#endregion
				break;
		}

		using (Sql_conn)
		{
			Sql_Command.Connection = Sql_conn;
			Sql_Command.CommandText = SqlString;

			if (is_enable != "-1")
			{
				Sql_Command.Parameters.AddWithValue("fi_no1", fi_no1);
				Sql_Command.Parameters.AddWithValue("fi_no2", fi_no2);
			}

			Sql_conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Func_Power"] = nRows;

		return (int)context.Cache["GetCount_Func_Power"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string mg_sid, string mg_id, string mg_name, string mg_nike)
	{
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;

		// 檢查 mg_sid 是否有值
		if (int.TryParse(mg_sid, out ckint))
			subSql += " And m.mg_sid = " + ckint.ToString();

		// 檢查 mg_id 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(mg_id);
		if (tmpstr != "")
			subSql += " And m.mg_id Like '%" + tmpstr + "%'";

		// 檢查 mg_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(mg_name);
		if (tmpstr != "")
			subSql += " And m.mg_name Like '%" + tmpstr + "%'";

		// 檢查 mg_nike 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(mg_nike);
		if (tmpstr != "")
			subSql += " And m.mg_nike Like '%" + tmpstr + "%'";

		return subSql;
	}
}