//---------------------------------------------------------------------------- 
//程式功能	線上考試(限定身份)(試卷清單) > 
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _B003 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0;
			Common_Func cfc = new Common_Func();
			string tmpstr = "";

			// 檢查使用者權限並存入登入紀錄
			//Check_Power("B003", true);

			#region 接受下一頁返回時的舊查詢條件
			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"], out ckint))
				{
					if (ckint > gv_Ts_Paper.PageCount)
						ckint = gv_Ts_Paper.PageCount;

					gv_Ts_Paper.PageIndex = ckint;
				}
				else
					lb_pageid.Text = "0";
			}

			if (Request["tp_sid"] != null)
			{
				if (int.TryParse(Request["tp_sid"], out ckint))
				{
					tb_tp_sid.Text = ckint.ToString();
					ods_Ts_Paper.SelectParameters["tp_sid"].DefaultValue = ckint.ToString();
				}
			}

			if (Request["tp_title"] != null)
			{
				tmpstr = cfc.CleanSQL(Request["tp_title"].Trim());
				if (tmpstr != "")
				{
					tb_tp_title.Text = tmpstr;
					ods_Ts_Paper.SelectParameters["tp_title"].DefaultValue = tmpstr;
				}
				else
				{
					tb_tp_title.Text = "";
					ods_Ts_Paper.SelectParameters["tp_title"].DefaultValue = "";
				}
			}

			// 限制開放及截止時間在範圍內
			ods_Ts_Paper.SelectParameters["btime"].DefaultValue = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
			ods_Ts_Paper.SelectParameters["etime"].DefaultValue = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

			// 限制顯示旗標為 1 的才出現
			ods_Ts_Paper.SelectParameters["is_show"].DefaultValue = "1";
			#endregion
		}

		#region 檢查頁數是否超過
		ods_Ts_Paper.DataBind();
		gv_Ts_Paper.DataBind();
		if (gv_Ts_Paper.PageCount < gv_Ts_Paper.PageIndex)
		{
			gv_Ts_Paper.PageIndex = gv_Ts_Paper.PageCount;
			gv_Ts_Paper.DataBind();
		}

		lb_pageid.Text = gv_Ts_Paper.PageIndex.ToString();
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

	// 換頁
	protected void gv_Ts_Paper_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid.Text = gv_Ts_Paper.PageIndex.ToString();
	}

	// 條件設定
	protected void btn_Set_Click(object sender, EventArgs e)
	{
		// 檢查查詢條件是否改變
		Chk_Filter();
	}

	// 檢查查詢條件是否改變
	private void Chk_Filter()
	{
		Common_Func cfc = new Common_Func();

		int ckint = 0;
		string tmpstr = "";

		// 有輸入編號，則設定條件
		if (int.TryParse(tb_tp_sid.Text.Trim(), out ckint))
			ods_Ts_Paper.SelectParameters["tp_sid"].DefaultValue = ckint.ToString();
		else
		{
			tb_tp_sid.Text = "";
			ods_Ts_Paper.SelectParameters["tp_sid"].DefaultValue = "";
		}

		// 有輸入 tp_title，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_tp_title.Text.Trim());
		if (tmpstr != "")
			ods_Ts_Paper.SelectParameters["tp_title"].DefaultValue = tmpstr;
		else
		{
			tb_tp_title.Text = "";
			ods_Ts_Paper.SelectParameters["tp_title"].DefaultValue = "";
		}

		gv_Ts_Paper.DataBind();
		if (gv_Ts_Paper.PageCount - 1 < gv_Ts_Paper.PageIndex)
		{
			gv_Ts_Paper.PageIndex = gv_Ts_Paper.PageCount;
			gv_Ts_Paper.DataBind();
		}
	}
}
