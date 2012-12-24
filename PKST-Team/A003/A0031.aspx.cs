//---------------------------------------------------------------------------- 
//程式功能	票選資料管理 > 問卷排程處理
//---------------------------------------------------------------------------- 

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _A0031 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("A003", true);
		}
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

	// 換頁
	protected void gv_Bt_Schedule_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid.Text = gv_Bt_Schedule.PageIndex.ToString();
	}

	// 格式轉換
	protected void gv_Bt_Schedule_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		string is_show = "", now_use = "";

		if ((e.Row.RowType == DataControlRowType.DataRow))
		{
			is_show = DataBinder.Eval(e.Row.DataItem, "is_show").ToString();

			if (is_show == "0")
				e.Row.Cells[0].Text = "隱藏";
			else
				e.Row.Cells[0].Text = "顯示";

			now_use = DataBinder.Eval(e.Row.DataItem, "now_use").ToString();

			if (now_use == "1")
				e.Row.Cells[1].Text = "啟用";
			else
				e.Row.Cells[1].Text = "<font color=red>停用</font>";	
		}
	}
}
