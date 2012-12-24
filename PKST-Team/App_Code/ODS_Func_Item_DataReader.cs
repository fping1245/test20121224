//---------------------------------------------------------------------------- 
//程式功能	取得 Func_Item 表格資料
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public class ODS_Func_Item_DataReader
{
	private string Sql_ConnString = "";

	public ODS_Func_Item_DataReader()
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

	public SqlDataReader Select_Func_Item(string SortColumn, int startRowIndex, int maximumRows,
		string fi_no1, string fi_name1, string visible1, string fi_no2, string fi_name2, string visible2)
	{
		string SqlString = "";

		SqlString = "Select * From (";
		SqlString = SqlString + "Select f2.fi_no1, f2.fi_no2, f2.fi_name2, f2.fi_sort2, f2.is_visible as visible1";
		SqlString = SqlString + ", f1.fi_name1, f1.fi_sort1, f1.is_visible as visible2 ";
		SqlString = SqlString + ", Row_Number() Over (Order by ";

		// 排序設定
		if (SortColumn.Trim() == "")
			SqlString = SqlString + "f1.fi_sort1, f2.fi_sort2";
		else
			SqlString = SqlString + SortColumn;

		SqlString = SqlString + ") as rownum From Func_Item2 f2";
		SqlString = SqlString + " Inner Join Func_Item1 f1 On f2.fi_no1 = f1.fi_no1";

		// 產生 Where 字串內容
		SqlString = SqlString + GetSqlString(fi_no1, fi_name1, visible1, fi_no2, fi_name2, visible2) + ") as mTable";

		SqlString = SqlString + " Where rownum Between " + (startRowIndex + 1).ToString() + " And " + (startRowIndex + maximumRows).ToString();

		// 排序設定
		SqlString = SqlString + " Order by rownum";

		// 建立資料庫連結
		SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);

		// 建立命令物件
		SqlCommand Sql_Command = new SqlCommand();

		Sql_Command.Connection = Sql_Conn;
		Sql_Command.CommandText = SqlString;

		// 開啟連結
		Sql_Conn.Open();

		// 傳回 SqlDataReader
		return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
	}

	public int GetCount_Func_Item(string SortColumn, int startRowIndex, int maximumRows,
		string fi_no1, string fi_name1, string visible1, string fi_no2, string fi_name2, string visible2)
	{
		int nRows = 0;
		string SqlString = "";
		HttpContext context = HttpContext.Current;

		SqlConnection Sql_conn = new SqlConnection(Sql_ConnString);
		SqlCommand Sql_Command = new SqlCommand();

		// 由資料庫中取得筆數
		SqlString = "Select Count(*) as Cnt From Func_Item2 f2";
		SqlString = SqlString + " Inner Join Func_Item1 f1 On f2.fi_no1 = f1.fi_no1";
		SqlString = SqlString + GetSqlString(fi_no1, fi_name1, visible1, fi_no2, fi_name2, visible2);

		using (Sql_conn)
		{
			Sql_Command.Connection = Sql_conn;
			Sql_Command.CommandText = SqlString;

			Sql_conn.Open();
			nRows = (int)Sql_Command.ExecuteScalar();
		}

		Sql_Command.Dispose();

		context.Cache["GetCount_Func_Item"] = nRows;

		return (int)context.Cache["GetCount_Func_Item"];
	}

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string fi_no1, string fi_name1, string visible1, string fi_no2, string fi_name2, string visible2)
	{
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";
		int ckint = 0;

		// 檢查 fi_no1 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(fi_no1);
		if (tmpstr != "")
			subSql += " And f2.fi_no1 = '" + tmpstr + "'";

		// 檢查 fi_name1 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(fi_name1);
		if (tmpstr != "")
			subSql += " And f1.fi_name1 Like '%" + tmpstr + "%'";

		// 檢查 visible1 是否有值
		if (int.TryParse(visible1, out ckint))
			if (ckint == 0 || ckint == 1)
				subSql += " And f1.is_visible = " + ckint.ToString();
			else
				subSql += " And f1.is_visible <> 2";
		else
			subSql += " And f1.is_visible <> 2";

		// 檢查 fi_no2 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(fi_no2);
		if (tmpstr != "")
			subSql += " And f2.fi_no2 = '" + tmpstr + "'";

		// 檢查 fi_name2 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(fi_name2);
		if (tmpstr != "")
			subSql += " And f2.fi_name2 Like '%" + tmpstr + "%'";

		// 檢查 visible2 是否有值
		if (int.TryParse(visible2, out ckint))
			if (ckint == 0 || ckint == 1)
				subSql += " And f2.is_visible = " + ckint.ToString();
			else
				subSql += " And f2.is_visible <> 2";
		else
			subSql += " And f1.is_visible <> 2";

		if (subSql != "")
			subSql = " Where" + subSql.Substring(4);

		return subSql;
	}
}
