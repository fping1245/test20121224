//---------------------------------------------------------------------------- 
//程式功能	取得 Fi_Content 資料表資料
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public class ODS_Fi_Content_DataReader
{
    private string Sql_ConnString = "";

    public ODS_Fi_Content_DataReader()
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

    public SqlDataReader Select_Fi_Content(string SortColumn, int startRowIndex, int maximumRows,
        int fl_no, string fc_name, string fc_ext, string fc_desc)
    {
        string SqlString = "";

        SqlString = "Select * From (";
        SqlString = SqlString + "Select c.fc_sid, c.fc_name, c.fc_ext, c.fc_size, c.fc_type, c.fc_desc, c.up_time, l.fl_url";
        SqlString = SqlString + ", Row_Number() Over (Order by ";

        // 排序設定
        if (SortColumn.Trim() == "")
            SqlString = SqlString + "c.fl_no, c.fc_name";
        else
            SqlString = SqlString + SortColumn;

        SqlString = SqlString + ") as rownum From Fi_Content c";
        SqlString = SqlString + " Inner Join Fi_Location l On c.fl_no = l.fl_no";

        // 產生 Where 字串內容
        SqlString = SqlString + " Where c.fl_no = @fl_no";
        SqlString = SqlString + GetSqlString(fc_name, fc_ext, fc_desc) + ") as mTable";

        SqlString = SqlString + " Where rownum Between " + (startRowIndex + 1).ToString() + " And " + (startRowIndex + maximumRows).ToString();

        // 排序設定
        SqlString = SqlString + " Order by rownum";

        // 建立資料庫連結
        SqlConnection Sql_Conn = new SqlConnection(Sql_ConnString);

        // 建立命令物件
        SqlCommand Sql_Command = new SqlCommand();

        Sql_Command.Connection = Sql_Conn;
        Sql_Command.CommandText = SqlString;
        Sql_Command.Parameters.AddWithValue("fl_no", fl_no);

        // 開啟連結
        Sql_Conn.Open();

        // 傳回 SqlDataReader
        return Sql_Command.ExecuteReader(CommandBehavior.CloseConnection);
    }

    public int GetCount_Fi_Content(string SortColumn, int startRowIndex, int maximumRows,
        int fl_no, string fc_name, string fc_ext, string fc_desc)
    {
        int nRows = 0;
        string SqlString = "";
        HttpContext context = HttpContext.Current;

        SqlConnection Sql_conn = new SqlConnection(Sql_ConnString);
        SqlCommand Sql_Command = new SqlCommand();

        // 由資料庫中取得筆數
        SqlString = "Select Count(*) as Cnt From Fi_Content c";
        SqlString = SqlString + " Inner Join Fi_Location l On c.fl_no = l.fl_no";
        SqlString = SqlString + " Where c.fl_no = @fl_no";
        SqlString = SqlString + GetSqlString(fc_name, fc_ext, fc_desc);

        using (Sql_conn)
        {
            Sql_Command.Connection = Sql_conn;
            Sql_Command.CommandText = SqlString;
            Sql_Command.Parameters.AddWithValue("fl_no", fl_no);

            Sql_conn.Open();
            nRows = (int)Sql_Command.ExecuteScalar();
        }

        Sql_Command.Dispose();

        context.Cache["GetCount_Fi_Content"] = nRows;

        return (int)context.Cache["GetCount_Fi_Content"];
    }

    // 產生對應的 Sql Where 字串
    private string GetSqlString(string fc_name, string fc_ext, string fc_desc)
    {
        Common_Func cfc = new Common_Func();
        string subSql = "", tmpstr = "";

        // 檢查 fc_name 是否有值，並清除 SQL 隱碼攻擊的字元
        tmpstr = cfc.CleanSQL(fc_name);
        if (tmpstr != "")
            subSql += " And c.fc_name Like '%" + tmpstr + "%'";

        // 檢查 fc_ext 是否有值，並清除 SQL 隱碼攻擊的字元
        tmpstr = cfc.CleanSQL(fc_ext);
        if (tmpstr != "")
            subSql += " And c.fc_ext Like '%" + tmpstr + "%'";

        // 檢查 fc_desc 是否有值，並清除 SQL 隱碼攻擊的字元
        tmpstr = cfc.CleanSQL(fc_desc);
        if (tmpstr != "")
            subSql += " And c.fc_desc Like '%" + tmpstr + "%'";

        return subSql;
    }
}
