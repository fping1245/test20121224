//---------------------------------------------------------------------------- 
//程式功能	人員資料管理 > 明細內容 > 變更密碼
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

public partial class _10051_pass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int mg_sid = -1;
        string SqlString = "", mErr = "";

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
                lb_page.Text = lb_page.Text + "&mg_sid=" + Request["mg_sid"];

            if (Request["mg_name"] != null)
                lb_page.Text = lb_page.Text + "&mg_name=" + Server.UrlEncode(Request["mg_name"]);

            if (Request["mg_nike"] != null)
                lb_page.Text = lb_page.Text + "&mg_nike=" + Server.UrlEncode(Request["mg_nike"]);

            if (Request["btime"] != null)
                lb_page.Text = lb_page.Text + "&btime=" + Server.UrlEncode(Request["btime"]);

            if (Request["etime"] != null)
                lb_page.Text = lb_page.Text + "&etime=" + Server.UrlEncode(Request["etime"]);

            #endregion

            #region 檢查傳入參數
            if (Request["sid"] != null)
            {
                #region 取得相關資料
                if (int.TryParse(Request["sid"].ToString(), out mg_sid))
                {
                    lb_page.Text = lb_page.Text + "&sid=" + mg_sid.ToString();

                    using (SqlConnection sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
                    {
                        sql_conn.Open();

                        #region 取得人員基本資料
                        SqlString = "Select Top 1 mg_sid, mg_name, mg_id";
                        SqlString = SqlString + " From Manager Where mg_sid = " + mg_sid.ToString();

                        using (SqlCommand Sql_Command = new SqlCommand(SqlString, sql_conn))
                        {
                            using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
                            {
                                if (Sql_Reader.Read())
                                {
                                    lb_mg_sid.Text = Sql_Reader["mg_sid"].ToString();
                                    lb_mg_id.Text = Sql_Reader["mg_id"].ToString();
                                    lb_mg_name.Text = Sql_Reader["mg_name"].ToString().Trim();

                                    lb_pg_mg_sid.Text = mg_sid.ToString();
                                }
                                else
                                    mErr = "找不到指定的人員資料!\\n";
                            }
                        }
                        #endregion
                    }
                }
                else
                    mErr = "網頁參數傳送錯誤!\\n";

                #endregion
            }
            else
                mErr = "網頁參數傳送錯誤!\\n";

            if (mErr != "")
                lt_show.Text = "<script language='javascript'>alert('" + mErr + "');history.go(-1);</script>";

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
        string mg_pass, mg_pass1;

        // 載入公用函數
        Common_Func cfc = new Common_Func();

        mg_pass = tb_mg_pass.Text.Trim();
        mg_pass1 = tb_mg_pass1.Text.Trim();

        if (mg_pass == "")
            mErr = mErr + "「新登入密碼」沒有輸入!\\n";
        else
            if (cfc.CheckSQL(mg_pass))
                mErr = mErr + "「新登入密碼」請勿使用特殊符號!\\n";
            else if (mg_pass.Length > 12 || mg_pass.Length < 4)
                mErr = mErr + "「新登入密碼」長度為4~12個字!\\n";

        if (mg_pass != mg_pass1)
            mErr = mErr + "「新登入密碼」與「新密碼確認」不相同!\\n";

        if (mErr == "")
        {
            using (SqlConnection Sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
            {
                string SqlString = "";
                Decoder decoder = new Decoder();

                Sql_conn.Open();

                // 建立 SQL 修改資料的語法
                SqlString = "Update Manager Set mg_pass = @mg_pass";
                SqlString = SqlString + " Where mg_sid = @mg_sid";

                using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_conn))
                {
                    Sql_Command.Parameters.AddWithValue("@mg_pass", decoder.EnCode(mg_pass));
                    Sql_Command.Parameters.AddWithValue("@mg_sid", lb_pg_mg_sid.Text);

                    Sql_Command.ExecuteNonQuery();
                }
            }
        }

        if (mErr == "")
        {
            mErr = "alert('密碼變更完成，新密碼該員於下次登入時生效!\\n');location.replace('10051.aspx" + lb_page.Text + "');";
        }
        else
            mErr = "alert('" + mErr + "')";

        lt_show.Text = "<script language=javascript>" + mErr + "</script>";
    }
}
