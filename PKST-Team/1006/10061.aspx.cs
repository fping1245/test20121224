//---------------------------------------------------------------------------- 
//程式功能	權限設定管理 > 顯示人員
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _10061 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string mErr = "";

            // 檢查使用者權限並存入使用紀錄
            //Check_Power("1006", false);

            #region 檢查傳入的參數 no1, no2，並取得相關資料填入抬頭文字
            if (Request["no1"] == null)
            {
                mErr = "參數1傳遞錯誤!\\n";
                ods_Func_Power.SelectParameters["fi_no1"].DefaultValue = "";
            }
            else
            {
                lb_fi_no1.Text = Request["no1"];
                ods_Func_Power.SelectParameters["fi_no1"].DefaultValue = lb_fi_no1.Text;
            }

            if (Request["no2"] == null)
            {
                mErr = mErr + "參數2傳遞錯誤!\\n";
                ods_Func_Power.SelectParameters["fi_no2"].DefaultValue = "";
            }
            else
            {
                lb_fi_no2.Text = Request["no2"];
                ods_Func_Power.SelectParameters["fi_no2"].DefaultValue = lb_fi_no2.Text;
            }

            if (mErr == "")
            {
                #region 取得相關資料填入抬頭文字
                using (SqlConnection sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
                {
                    string SqlString = "";

                    SqlString = "Select Top 1 f1.fi_name1, f1.is_visible as visible1, f2.fi_name2, f2.is_visible as visible2";
                    SqlString = SqlString + " From Func_Item1 f1";
                    SqlString = SqlString + " Inner Join Func_Item2 f2 On f2.fi_no1 = f1.fi_no1";
                    SqlString = SqlString + " Where f2.fi_no1 = @fi_no1 and f2.fi_no2 = @fi_no2";

                    using (SqlCommand Sql_Command = new SqlCommand())
                    {
                        Sql_Command.CommandText = SqlString;
                        Sql_Command.Connection = sql_conn;
                        Sql_Command.Parameters.AddWithValue("fi_no1", lb_fi_no1.Text.Trim());
                        Sql_Command.Parameters.AddWithValue("fi_no2", lb_fi_no2.Text.Trim());

                        sql_conn.Open();

                        using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
                        {
                            if (Sql_Reader.Read())
                            {
                                lb_fi_name1.Text = Sql_Reader["fi_name1"].ToString();
                                lb_fi_name2.Text = Sql_Reader["fi_name2"].ToString();
                                switch (Sql_Reader["visible1"].ToString())
                                {
                                    case "0":
                                        lb_visible1.Text = "隱藏";
                                        break;
                                    case "1":
                                        lb_visible1.Text = "顯示";
                                        break;
                                    case "2":
                                        lb_visible1.Text = "系統";
                                        break;
                                    default:
                                        lb_visible1.Text = "不明";
                                        break;
                                }

                                switch (Sql_Reader["visible2"].ToString())
                                {
                                    case "0":
                                        lb_visible2.Text = "隱藏";
                                        break;
                                    case "1":
                                        lb_visible2.Text = "顯示";
                                        break;
                                    case "2":
                                        lb_visible2.Text = "系統";
                                        break;
                                    default:
                                        lb_visible2.Text = "不明";
                                        break;
                                }
                            }
                            else
                                mErr = "找不到指定的功能!\\n";
                        }

                    }
                }
                #endregion
            }

            #endregion

            if (mErr == "")
            {
                #region 接受下一頁返回時的舊查詢條件
                if (Request["pageid"] != null)
                    lb_page.Text = "?pageid=" + Request["pageid"];
                else
                    lb_page.Text = "?pageid=0";

                if (Request["fi_no1"] != null)
                    lb_page.Text = lb_page.Text + "&fi_no1=" + Request["fi_no1"];

                if (Request["fi_name1"] != null)
                    lb_page.Text = lb_page.Text + "&fi_name1=" + Server.UrlEncode(Request["fi_name1"]);

                if (Request["visible1"] != null)
                    lb_page.Text = lb_page.Text + "&visible1=" + Request["visible1"];

                if (Request["fi_no2"] != null)
                    lb_page.Text = lb_page.Text + "&fi_no2=" + Request["fi_no2"];

                if (Request["fi_name2"] != null)
                    lb_page.Text = lb_page.Text + "&fi_name2=" + Server.UrlEncode(Request["fi_name2"]);

                if (Request["visible2"] != null)
                    lb_page.Text = lb_page.Text + "&visible2=" + Request["visible2"];
                #endregion
            }
            else
            {
                // 顯示錯誤訊息
                lt_show.Text = "<script language=javascript>alert('" + mErr + "');history.go(-1);</script>";
            }
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

    protected void rb_is_open_CheckedChanged(object sender, EventArgs e)
    {
        // 更改人員權限
        ChangePower((RadioButton)sender, 1);
    }

    protected void rb_is_close_CheckedChanged(object sender, EventArgs e)
    {
        // 更改人員權限
        ChangePower((RadioButton)sender, 0);
    }

    // 更改人員權限
    private void ChangePower(RadioButton sender, int is_enable)
    {
        int mg_sid;

        GridViewRow s_sid = (GridViewRow)sender.Parent.Parent;

        #region 找到人員編號，並更改資料庫內容
        if (int.TryParse(s_sid.Cells[0].Text, out mg_sid))
        {
            #region 更改資料庫
            using (SqlConnection sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
            {
                using (SqlCommand Sql_Command = new SqlCommand())
                {
                    string SqlString = "";

                    // 刪除原有資料
                    SqlString = "Delete Func_Power Where mg_sid = @mg_sid And fi_no1 = @fi_no1 And fi_no2 = @fi_no2;";

                    // 若設定「開放」權限，則加入一筆資料
                    if (is_enable == 1)
                    {
                        SqlString = SqlString + "Insert Into Func_Power (mg_sid, fi_no1, fi_no2, is_enable) Values";
                        SqlString = SqlString + " (@mg_sid, @fi_no1, @fi_no2, 1);";
                    }

                    Sql_Command.CommandText = SqlString;
                    Sql_Command.Connection = sql_conn;
                    Sql_Command.Parameters.AddWithValue("mg_sid", mg_sid);
                    Sql_Command.Parameters.AddWithValue("fi_no1", lb_fi_no1.Text);
                    Sql_Command.Parameters.AddWithValue("fi_no2", lb_fi_no2.Text);

                    sql_conn.Open();

                    Sql_Command.ExecuteNonQuery();
                }
            }
            #endregion
        }
        #endregion
    }

    protected void gv_Func_Power_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            RadioButton rb_en = (RadioButton)e.Row.Cells[4].FindControl("rb_is_open");
            RadioButton rb_di = (RadioButton)e.Row.Cells[4].FindControl("rb_is_close");

            if (DataBinder.Eval(e.Row.DataItem, "is_enable").ToString() == "0")
            {
                rb_en.Checked = false;
                rb_di.Checked = true;
            }
            else
            {
                rb_en.Checked = true;
                rb_di.Checked = false;
            }
        }
    }

    // 顯示條件範圍設定
    protected void Btn_Set_Click(object sender, EventArgs e)
    {
        Common_Func cfc = new Common_Func();

        string tmpstr = "";
        int ckint = -1;

        // 有輸入 mg_sid，則設定條件
        if (int.TryParse(tb_mg_sid.Text.Trim(), out ckint))
        {
            tb_mg_sid.Text = ckint.ToString();
            ods_Func_Power.SelectParameters["mg_sid"].DefaultValue = ckint.ToString();
        }
        else
        {
            tb_mg_sid.Text = "";
            ods_Func_Power.SelectParameters["mg_sid"].DefaultValue = "";
        }

        // 有輸入 mg_id，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
        tmpstr = cfc.CleanSQL(tb_mg_id.Text.Trim());
        if (tmpstr != "")
        {
            tb_mg_id.Text = tmpstr;
            ods_Func_Power.SelectParameters["mg_id"].DefaultValue = tmpstr;
        }
        else
        {
            tb_mg_id.Text = "";
            ods_Func_Power.SelectParameters["mg_id"].DefaultValue = "";
        }

        // 有輸入 mg_name，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
        tmpstr = cfc.CleanSQL(tb_mg_name.Text.Trim());
        if (tmpstr != "")
        {
            tb_mg_name.Text = tmpstr;
            ods_Func_Power.SelectParameters["mg_name"].DefaultValue = tmpstr;
        }
        else
        {
            tb_mg_name.Text = "";
            ods_Func_Power.SelectParameters["mg_name"].DefaultValue = "";
        }

        // 有輸入 mg_nike，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
        tmpstr = cfc.CleanSQL(tb_mg_nike.Text.Trim());
        if (tmpstr != "")
        {
            tb_mg_nike.Text = tmpstr;
            ods_Func_Power.SelectParameters["mg_nike"].DefaultValue = tmpstr;
        }
        else
        {
            tb_mg_nike.Text = "";
            ods_Func_Power.SelectParameters["mg_nike"].DefaultValue = "";
        }

        // 檢查權限
        if (rb_open.Checked)
            ods_Func_Power.SelectParameters["is_enable"].DefaultValue = "1";
        else if (rb_close.Checked)
            ods_Func_Power.SelectParameters["is_enable"].DefaultValue = "0";
        else
        {
            ods_Func_Power.SelectParameters["is_enable"].DefaultValue = "-1";
            rb_all.Checked = true;
        }

        gv_Func_Power.DataBind();
        if (gv_Func_Power.PageCount - 1 < gv_Func_Power.PageIndex)
        {
            gv_Func_Power.PageIndex = gv_Func_Power.PageCount;
            gv_Func_Power.DataBind();
        }
    }

    protected void gv_Func_Power_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.Header))
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                // GridView 設定有排序時，自訂的 TemplateField 才會有 cell.Text，其它可為排序的標題文字 cell.Text == ""
                if (cell.Text == "")
                {
                    // 設定 GridView 排序部份的字型大小及顏色
                    LinkButton lk_bn = (LinkButton)cell.Controls[0];
                    lk_bn.Style.Add("color", "Blue");
                    lk_bn.Style.Add("font-size", "10pt");
                }
            }
        }
    }
}
