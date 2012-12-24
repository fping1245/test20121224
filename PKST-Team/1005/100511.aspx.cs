//---------------------------------------------------------------------------- 
//程式功能	人員資料管理 > 明細內容 > 設定權限
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

public partial class _100511 : System.Web.UI.Page
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
                        SqlString = "Select Top 1 mg_sid, mg_name, mg_nike, mg_id From Manager Where mg_sid = " + mg_sid.ToString();

                        using (SqlCommand Sql_Command = new SqlCommand(SqlString, sql_conn))
                        {
                            using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
                            {
                                if (Sql_Reader.Read())
                                {
                                    lb_mg_sid.Text = Sql_Reader["mg_sid"].ToString();
                                    lb_mg_id.Text = Sql_Reader["mg_id"].ToString();
                                    lb_mg_name.Text = Sql_Reader["mg_name"].ToString().Trim();
                                    lb_mg_nike.Text = Sql_Reader["mg_nike"].ToString().Trim();

                                    lb_pg_mg_sid.Text = mg_sid.ToString();

                                    sqs_Func_Power.SelectParameters["mg_sid"].DefaultValue = Sql_Reader["mg_sid"].ToString();
                                    gv_Func_Power.DataBind();
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

    protected void gv_Func_Power_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            DataRowView DataRV = (DataRowView)e.Row.DataItem;

            RadioButton rb_en = (RadioButton)e.Row.Cells[2].FindControl("rb_open");
            RadioButton rb_di = (RadioButton)e.Row.Cells[2].FindControl("rb_close");
            Label lb_fi_no1 = (Label)e.Row.Cells[2].FindControl("lb_fi_no1");
            Label lb_fi_no2 = (Label)e.Row.Cells[2].FindControl("lb_fi_no2");

            // 將主功能代碼及子功能代碼存入隱藏的 Label 控制項，
            // 因為 DataItem 在 RowDataBound 事件之後，不會被保存。
            lb_fi_no1.Text = DataRV["fi_no1"].ToString();
            lb_fi_no2.Text = DataRV["fi_no2"].ToString();

            switch (lb_enable.Text)
            {
                case "0":		// 狀態為「全部禁止」
                    rb_en.Checked = false;
                    rb_di.Checked = true;
                    break;
                case "1":		// 狀態為「全部開放」
                    rb_en.Checked = true;
                    rb_di.Checked = false;
                    break;
                default:		// 狀態為「自訂」
                    if (DataRV["is_enable"].ToString() == "0")
                    {
                        rb_en.Checked = false;
                        rb_di.Checked = true;
                    }
                    else
                    {
                        rb_en.Checked = true;
                        rb_di.Checked = false;
                    }
                    break;
            }
        }
    }

    // 「確定儲存」按鈕的 Click 事件處理常式。
    protected void lb_ok_Click(object sender, EventArgs e)
    {
        string SqlString = "";
        string fi_no1 = "", fi_no2 = "";
        SqlCommand Sql_Command = new SqlCommand();

        using (SqlConnection sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
        {
            sql_conn.Open();

            // 先清除該位人員所有的權限，然後再重新新增。
            SqlString = "Delete Func_Power Where mg_sid = @mg_sid";

            Sql_Command.Connection = sql_conn;
            Sql_Command.CommandText = SqlString;
            Sql_Command.Parameters.AddWithValue("mg_sid", lb_pg_mg_sid.Text);

            Sql_Command.ExecuteNonQuery();

            Sql_Command.Dispose();

            #region 處理 GridView 裡面的每一筆資料是否賦予使用權限
            foreach (GridViewRow row in gv_Func_Power.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    RadioButton rb_en = (RadioButton)row.FindControl("rb_open");

                    // 有「開放」權限時，才要存入資料庫。
                    if (rb_en.Checked)
                    {
                        Label lb_fi_no1 = (Label)row.FindControl("lb_fi_no1");
                        Label lb_fi_no2 = (Label)row.FindControl("lb_fi_no2");

                        fi_no1 = lb_fi_no1.Text;
                        fi_no2 = lb_fi_no2.Text;

                        SqlString = "Insert Into Func_Power (mg_sid, fi_no1, fi_no2, is_enable)";
                        SqlString = SqlString + " Values (@mg_sid, @fi_no1, @fi_no2, 1);";

                        Sql_Command.Parameters.Clear();
                        Sql_Command.CommandText = SqlString;
                        Sql_Command.Parameters.AddWithValue("mg_sid", lb_pg_mg_sid.Text);
                        Sql_Command.Parameters.AddWithValue("fi_no1", fi_no1);
                        Sql_Command.Parameters.AddWithValue("fi_no2", fi_no2);

                        Sql_Command.ExecuteNonQuery();

                        Sql_Command.Dispose();
                    }
                }
            }
            #endregion
        }

        lt_show.Text = "<script language=javascript>alert('權限設定完成！');location.replace('10051.aspx" + lb_page.Text + "');</script>";
    }

    protected void bn_all_open_Click(object sender, EventArgs e)
    {
        // 狀態設為「全部開放」
        lb_enable.Text = "1";
        gv_Func_Power.DataBind();
    }

    protected void bn_all_close_Click(object sender, EventArgs e)
    {
        // 狀態設為「全部禁止」
        lb_enable.Text = "0";
        gv_Func_Power.DataBind();
    }

    protected void rb_open_CheckedChanged(object sender, EventArgs e)
    {
        // 狀態設為「自訂」
        lb_enable.Text = "2";
    }

    protected void rb_close_CheckedChanged(object sender, EventArgs e)
    {
        // 狀態設為「自訂」
        lb_enable.Text = "2";
    }
}
