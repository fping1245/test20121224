//---------------------------------------------------------------------------- 
//程式功能	工作類型管理
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _5001 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0;

			// 檢查使用者權限並存入登入紀錄
			//Check_Power("5001", true);

			#region 接受下一頁返回時的舊查詢條件
			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"], out ckint))
				{
					if (ckint > gv_Ca_Group.PageCount)
						ckint = gv_Ca_Group.PageCount;

					gv_Ca_Group.PageIndex = ckint;
				}
				else
					lb_pageid.Text = "0";
			}

			// 設定條件為屬於登入者的群組
			ods_Ca_Group.SelectParameters["mg_sid"].DefaultValue = Session["mg_sid"].ToString();

			#endregion
		}

		#region 檢查頁數是否超過
		ods_Ca_Group.DataBind();
		gv_Ca_Group.DataBind();
		if (gv_Ca_Group.PageCount < gv_Ca_Group.PageIndex)
		{
			gv_Ca_Group.PageIndex = gv_Ca_Group.PageCount;
			gv_Ca_Group.DataBind();
		}

		lb_pageid.Text = gv_Ca_Group.PageIndex.ToString();
		#endregion
	}

	// 檢查使用者權限並存入登入紀錄
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

	// 頁面變更
	protected void gv_Ca_Group_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid.Text = gv_Ca_Group.PageIndex.ToString();
	}
}
