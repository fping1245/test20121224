//---------------------------------------------------------------------------- 
//程式功能	人員資料管理 > 明細內容 > 新增資料
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _10051_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int ckint = 0;

            // 檢查使用者權限但不存入使用紀錄
            //Check_Power("1005", false);

            #region 承接上一頁的查詢條件設定
            if (Request["pageid"] != null)
            {
                if (int.TryParse(Request["pageid"].ToString(), out ckint))
                    lb_page.Text = "?pageid=" + ckint.ToString();
                else
                    lb_page.Text = "?pageid=0";
            }

            if (Request["mg_sid"] != null)
                lb_page.Text += "&mg_sid=" + Request["mg_sid"];

            if (Request["mg_name"] != null)
                lb_page.Text += "&mg_name=" + Server.UrlEncode(Request["mg_name"]);

            if (Request["mg_nike"] != null)
                lb_page.Text += "&mg_nike=" + Server.UrlEncode(Request["mg_nike"]);

            if (Request["btime"] != null)
                lb_page.Text += "&btime=" + Server.UrlEncode(Request["btime"]);

            if (Request["etime"] != null)
                lb_page.Text += "&etime=" + Server.UrlEncode(Request["etime"]);

            #endregion
        }
    }

    // Check_Power() 檢查使用者權限並存入使用紀錄
    private void Check_Power(string f_power, bool bl_save)
    {
        // 載入公用函數
        Common_Func cfc = new Common_Func();

        // 若 Session 不存在則直接顯示錯誤訊息
        try
        {
            if (cfc.Check_Power(Session["mg_sid"].ToString(), Session["mg_name"].ToString(), Session["mg_power"].ToString(), f_power, Request.ServerVariables["REMOTE_ADDR"], bl_save) > 0)
                Response.Redirect("../Error.aspx?ErrCode=1");
        }
        catch
        {
            Response.Redirect("../Error.aspx?ErrCode=2");
        }
    }

    protected void lb_ok_Click(object sender, EventArgs e)
    {
        string mErr = "";
        int mg_sid = -1;

        // 載入字串函數
        String_Func sfc = new String_Func();

        // 載入公用函數
        Common_Func cfc = new Common_Func();

        if (tb_mg_id.Text.Trim() == "")
            mErr += "「登入帳號」沒有輸入!\\n";
        else
            if (cfc.CheckSQL(tb_mg_id.Text.Trim()))
                mErr += "「登入帳號」請勿使用特殊符號!\\n";

        if (tb_mg_pass.Text.Trim() == "")
            mErr += "「登入密碼」沒有輸入!\\n";
        else
            if (cfc.CheckSQL(tb_mg_pass.Text.Trim()))
                mErr += "「登入密碼」請勿使用特殊符號!\\n";
            else if (tb_mg_pass.Text.Trim().Length > 12 || tb_mg_pass.Text.Trim().Length < 4)
                mErr += "「登入密碼」長度為4~12個字!!\\n";

        if (tb_mg_pass.Text != tb_mg_pass1.Text)
            mErr += "「登入密碼」與「密碼確認」不相同!\\n";

        if (tb_mg_name.Text.Trim() == "")
            mErr += "「姓名」沒有輸入!\\n";

        if (tb_mg_nike.Text.Trim() == "")
            mErr += "「暱稱」沒有輸入!\\n";

        if (tb_mg_unit.Text.Trim() == "")
            mErr += "「單位」沒有輸入!\\n";

        if (mErr == "")
        {
            using (SqlConnection Sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
            {
                string SqlString = "";
                Decoder decoder = new Decoder();

                // 建立 SQL 的語法
                SqlString = "Insert Into Manager (mg_name, mg_nike, mg_id, mg_pass, mg_unit, mg_desc)";
                SqlString += " Values (@mg_name, @mg_nike, @mg_id, @mg_pass, @mg_unit, @mg_desc);";
                SqlString += "Select @mg_sid = Scope_Identity()";

                using (SqlCommand Sql_Command = new SqlCommand())
                {
                    Sql_Command.Connection = Sql_conn;
                    Sql_Command.CommandText = SqlString;

                    // 擷取字串到資料庫所規範的大小 sfc.Left(string mdata, int leng)
                    Sql_Command.Parameters.AddWithValue("@mg_name", sfc.Left(tb_mg_name.Text, 12));
                    Sql_Command.Parameters.AddWithValue("@mg_nike", sfc.Left(tb_mg_nike.Text, 12));
                    Sql_Command.Parameters.AddWithValue("@mg_id", sfc.Left(tb_mg_id.Text, 12));
                    Sql_Command.Parameters.AddWithValue("@mg_pass", decoder.EnCode(sfc.Left(tb_mg_pass.Text, 12)));
                    Sql_Command.Parameters.AddWithValue("@mg_unit", sfc.Left(tb_mg_unit.Text, 50));
                    Sql_Command.Parameters.AddWithValue("@mg_desc", sfc.Left(tb_mg_desc.Text, 1000));

                    SqlParameter spt_mg_sid = Sql_Command.Parameters.Add("@mg_sid", SqlDbType.Int);
                    spt_mg_sid.Direction = ParameterDirection.Output;

                    Sql_conn.Open();

                    Sql_Command.ExecuteNonQuery();

                    // 取得新增資料的主鍵值
                    mg_sid = (int)spt_mg_sid.Value;
                }
            }
        }

        if (mErr == "")
        {
            mErr = "alert('存檔完成!\\n請繼續設定該員的權限.....\\n');location.replace('10051.aspx" + lb_page.Text + "&sid=" + mg_sid.ToString() + "');";
        }
        else
            mErr = "alert('" + mErr + "')";

        lt_show.Text = "<script language=javascript>" + mErr + "</script>";
    }
}
