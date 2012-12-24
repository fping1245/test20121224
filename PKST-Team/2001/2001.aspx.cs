//---------------------------------------------------------------------------- 
//程式功能	檔案上傳下載 (實體路徑存放檔案)
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _2001 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int ckint = 0;
            Common_Func cfc = new Common_Func();

            // 檢查使用者權限並存入登入紀錄
            //Check_Power("2001", true);

            #region 接受下一頁返回時的舊查詢條件
            if (Request["pageid"] != null)
            {
                if (int.TryParse(Request["pageid"], out ckint))
                {
                    if (ckint > gv_Fi_Content.PageCount)
                        ckint = gv_Fi_Content.PageCount;

                    gv_Fi_Content.PageIndex = ckint;
                }
                else
                    lb_pageid.Text = "0";
            }

            ods_Fi_Content.SelectParameters["fl_no"].DefaultValue = "1";

            if (Request["fc_name"] != null)
            {
                tb_fc_name.Text = cfc.CleanSQL(Request["fc_name"]);
                ods_Fi_Content.SelectParameters["fc_name"].DefaultValue = tb_fc_name.Text;
            }

            if (Request["fc_ext"] != null)
            {
                tb_fc_ext.Text = cfc.CleanSQL(Request["fc_ext"]);
                ods_Fi_Content.SelectParameters["fc_ext"].DefaultValue = tb_fc_ext.Text;
            }

            if (Request["fc_desc"] != null)
            {
                tb_fc_desc.Text = cfc.CleanSQL(Request["fc_desc"]);
                ods_Fi_Content.SelectParameters["fc_desc"].DefaultValue = tb_fc_desc.Text;
            }
            #endregion
        }

        #region 檢查頁數是否超過
        ods_Fi_Content.DataBind();
        gv_Fi_Content.DataBind();
        if (gv_Fi_Content.PageCount < gv_Fi_Content.PageIndex)
        {
            gv_Fi_Content.PageIndex = gv_Fi_Content.PageCount;
            gv_Fi_Content.DataBind();
        }

        lb_pageid.Text = gv_Fi_Content.PageIndex.ToString();
        #endregion
    }

    // Check_Power() 檢查使用者權限並存入登入紀錄
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

    protected void gv_Fi_Content_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
        lb_pageid.Text = gv_Fi_Content.PageIndex.ToString();
    }

    protected void Btn_Set_Click(object sender, EventArgs e)
    {
        // 檢查查詢條件是否改變
        Chk_Filter();
    }

    // 檢查查詢條件是否改變
    private void Chk_Filter()
    {
        Common_Func cfc = new Common_Func();

        string tmpstr = "";

        // 有輸入 fc_name，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
        tmpstr = cfc.CleanSQL(tb_fc_name.Text.Trim());
        if (tmpstr != "")
        {
            tb_fc_name.Text = tmpstr;
            ods_Fi_Content.SelectParameters["fc_name"].DefaultValue = tmpstr;
        }
        else
        {
            tb_fc_name.Text = "";
            ods_Fi_Content.SelectParameters["fc_name"].DefaultValue = "";
        }

        // 有輸入 fc_exy，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
        tmpstr = cfc.CleanSQL(tb_fc_ext.Text.Trim());
        if (tmpstr != "")
        {
            tb_fc_ext.Text = tmpstr;
            ods_Fi_Content.SelectParameters["fc_ext"].DefaultValue = tmpstr;
        }
        else
        {
            tb_fc_ext.Text = "";
            ods_Fi_Content.SelectParameters["fc_ext"].DefaultValue = "";
        }

        // 有輸入 fc_desc，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
        tmpstr = cfc.CleanSQL(tb_fc_desc.Text.Trim());
        if (tmpstr != "")
        {
            tb_fc_desc.Text = tmpstr;
            ods_Fi_Content.SelectParameters["fc_desc"].DefaultValue = tmpstr;
        }
        else
        {
            tb_fc_desc.Text = "";
            ods_Fi_Content.SelectParameters["fc_desc"].DefaultValue = "";
        }

        gv_Fi_Content.DataBind();
        if (gv_Fi_Content.PageCount - 1 < gv_Fi_Content.PageIndex)
        {
            gv_Fi_Content.PageIndex = gv_Fi_Content.PageCount;
            gv_Fi_Content.DataBind();
        }
    }

    // 處理 GridView 的資料行
    protected void gv_Fi_Content_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ckint;
            e.Row.Cells[0].RowSpan = 2;
            e.Row.Cells[6].RowSpan = 2;
            e.Row.Cells[7].RowSpan = 2;

            Literal lt_row = (Literal)e.Row.Cells[7].FindControl("lt_table");
            lt_row.Text = "</td></tr><tr>";

            ckint = int.Parse(DataBinder.Eval(e.Row.DataItem, "rownum").ToString());
            if ((ckint % 2) == 1)
                e.Row.Cells[8].Attributes.Add("bgcolor", "#F7F7DE");
            e.Row.Cells[8].ColumnSpan = 5;
        }
    }

    // 處理 GridView 的抬頭折行
    protected void gv_Fi_Content_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].RowSpan = 2;
            e.Row.Cells[6].RowSpan = 2;
            e.Row.Cells[7].RowSpan = 2;
            e.Row.Cells[7].Text += "</th></tr><tr>";
            e.Row.Cells[8].Attributes.Add("bgcolor", "#99FF99");
            e.Row.Cells[8].ColumnSpan = 5;
        }
    }
}
