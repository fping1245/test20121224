//---------------------------------------------------------------------------- 
//程式功能	人員資料管理
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _1005 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int ckint = 0;
            Common_Func cfc = new Common_Func();
            DateTime ckbtime, cketime;

            // 檢查使用者權限並存入使用紀錄。
            //Check_Power("1005", true);

            #region 接受下一頁返回時的舊查詢條件
            if (Request["pageid"] != null)
            {
                if (int.TryParse(Request["pageid"], out ckint))
                    gv_Manager.PageIndex = ckint;
                else
                    lb_pageid.Text = "0";
            }

            if (Request["mg_sid"] != null)
            {
                if (int.TryParse(Request["mg_sid"], out ckint))
                {
                    tb_mg_sid.Text = ckint.ToString();
                    ods_Manager.SelectParameters["mg_sid"].DefaultValue = ckint.ToString();
                }
            }

            if (Request["mg_name"] != null)
            {
                tb_mg_name.Text = cfc.CleanSQL(Request["mg_name"]);
                ods_Manager.SelectParameters["mg_name"].DefaultValue = tb_mg_name.Text;
            }

            if (Request["mg_nike"] != null)
            {
                tb_mg_nike.Text = cfc.CleanSQL(Request["mg_nike"]);
                ods_Manager.SelectParameters["mg_nike"].DefaultValue = tb_mg_nike.Text;
            }

            if (Request["btime"] != null)
                if (DateTime.TryParse(Request["btime"], out ckbtime))
                {
                    tb_btime.Text = Request["btime"];
                    ods_Manager.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
                }

            if (Request["etime"] != null)
                if (DateTime.TryParse(Request["etime"], out cketime))
                {
                    tb_btime.Text = Request["etime"];
                    ods_Manager.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
                }
            #endregion
        }


        #region 檢查頁數是否超過
        ods_Manager.DataBind();
        gv_Manager.DataBind();
        if (gv_Manager.PageCount < gv_Manager.PageIndex)
        {
            gv_Manager.PageIndex = gv_Manager.PageCount;
            gv_Manager.DataBind();
        }

        lb_pageid.Text = gv_Manager.PageIndex.ToString();
        #endregion
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

    protected void gv_Manager_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
        lb_pageid.Text = gv_Manager.PageIndex.ToString();
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

        int ckint = 0;
        DateTime ckbtime, cketime;
        string tmpstr = "";

        // 有輸入編號，則設定條件
        if (int.TryParse(tb_mg_sid.Text.Trim(), out ckint))
            ods_Manager.SelectParameters["mg_sid"].DefaultValue = ckint.ToString();
        else
        {
            tb_mg_sid.Text = "";
            ods_Manager.SelectParameters["mg_sid"].DefaultValue = "";
        }

        // 有輸入姓名，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
        tmpstr = cfc.CleanSQL(tb_mg_name.Text.Trim());
        if (tmpstr != "")
            ods_Manager.SelectParameters["mg_name"].DefaultValue = tmpstr;
        else
        {
            tb_mg_name.Text = "";
            ods_Manager.SelectParameters["mg_name"].DefaultValue = "";
        }

        // 有輸入暱稱，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
        tmpstr = cfc.CleanSQL(tb_mg_nike.Text.Trim());
        if (tmpstr != "")
            ods_Manager.SelectParameters["mg_nike"].DefaultValue = tmpstr;
        else
        {
            tb_mg_nike.Text = "";
            ods_Manager.SelectParameters["mg_nike"].DefaultValue = "";
        }

        // 有輸入開始時間範圍，則設定條件
        if (DateTime.TryParse(tb_btime.Text.Trim(), out ckbtime))
            ods_Manager.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
        else
        {
            tb_btime.Text = "";
            ods_Manager.SelectParameters["btime"].DefaultValue = "";
        }

        // 有輸入結束時間範圍，則設定條件
        if (DateTime.TryParse(tb_etime.Text.Trim(), out cketime))
            ods_Manager.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
        else
        {
            tb_etime.Text = "";
            ods_Manager.SelectParameters["etime"].DefaultValue = "";
        }

        gv_Manager.DataBind();
		if (gv_Manager.PageCount -1 < gv_Manager.PageIndex)
		{
			gv_Manager.PageIndex = gv_Manager.PageCount;
			gv_Manager.DataBind();
		}

		lb_pageid.Text = gv_Manager.PageIndex.ToString();
    }
}
