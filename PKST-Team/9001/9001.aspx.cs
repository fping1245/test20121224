//---------------------------------------------------------------------------- 
//程式功能	廣告信發送管理 (廣告信清單)
//---------------------------------------------------------------------------- 
using System;
using System.Web.UI.WebControls;

public partial class _9001 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int ckint = 0;
		Common_Func cfc = new Common_Func();
		DateTime ckbtime, cketime;

		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("9001", true);

			#region 接受下一頁返回時的舊查詢條件
			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"], out ckint))
					gv_Ad_Mail.PageIndex = ckint;
				else
					lb_pageid.Text = "0";
			}

			if (Request["adm_sid"] != null)
			{
				if (int.TryParse(Request["adm_sid"], out ckint))
				{
					tb_adm_sid.Text = ckint.ToString();
					ods_Ad_Mail.SelectParameters["adm_sid"].DefaultValue = ckint.ToString();
				}
			}

			if (Request["adm_title"] != null)
			{
				tb_adm_title.Text = cfc.CleanSQL(Request["adm_title"]);
				ods_Ad_Mail.SelectParameters["adm_title"].DefaultValue = tb_adm_title.Text;
			}

			if (Request["adm_fmail"] != null)
			{
				tb_adm_fmail.Text = cfc.CleanSQL(Request["adm_fmail"]);
				ods_Ad_Mail.SelectParameters["adm_fmail"].DefaultValue = tb_adm_fmail.Text;
			}

			if (Request["btime"] != null)
			{
				if (DateTime.TryParse(Request["btime"], out ckbtime))
				{
					tb_btime.Text = Request["btime"];
					ods_Ad_Mail.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
				}
			}

			if (Request["etime"] != null)
			{
				if (DateTime.TryParse(Request["etime"], out cketime))
				{
					tb_btime.Text = Request["etime"];
					ods_Ad_Mail.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
				}
			}
			#endregion
		}

		#region 檢查頁數是否超過
		ods_Ad_Mail.DataBind();
		gv_Ad_Mail.DataBind();
		if (gv_Ad_Mail.PageCount < gv_Ad_Mail.PageIndex)
		{
			gv_Ad_Mail.PageIndex = gv_Ad_Mail.PageCount;
			gv_Ad_Mail.DataBind();
		}

		lb_pageid.Text = gv_Ad_Mail.PageIndex.ToString();
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
	protected void gv_Ad_Mail_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid.Text = gv_Ad_Mail.PageIndex.ToString();
	}

	// 條件設定
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
		if (int.TryParse(tb_adm_sid.Text.Trim(), out ckint))
			ods_Ad_Mail.SelectParameters["adm_sid"].DefaultValue = ckint.ToString();
		else
		{
			tb_adm_sid.Text = "";
			ods_Ad_Mail.SelectParameters["adm_sid"].DefaultValue = "";
		}

		// 有輸入 adm_title，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_adm_title.Text.Trim());
		if (tmpstr != "")
			ods_Ad_Mail.SelectParameters["adm_title"].DefaultValue = tmpstr;
		else
		{
			tb_adm_title.Text = "";
			ods_Ad_Mail.SelectParameters["adm_title"].DefaultValue = "";
		}

		// 有輸入 adm_fname，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_adm_fname.Text.Trim());
		if (tmpstr != "")
			ods_Ad_Mail.SelectParameters["adm_fname"].DefaultValue = tmpstr;
		else
		{
			tb_adm_fname.Text = "";
			ods_Ad_Mail.SelectParameters["adm_fname"].DefaultValue = "";
		}

		// 有輸入 adm_fmail，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_adm_fmail.Text.Trim());
		if (tmpstr != "")
			ods_Ad_Mail.SelectParameters["adm_fmail"].DefaultValue = tmpstr;
		else
		{
			tb_adm_fmail.Text = "";
			ods_Ad_Mail.SelectParameters["adm_fmail"].DefaultValue = "";
		}

		// 有輸入異動時間開始範圍，則設定條件
		if (DateTime.TryParse(tb_btime.Text.Trim(), out ckbtime))
			ods_Ad_Mail.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_btime.Text = "";
			ods_Ad_Mail.SelectParameters["btime"].DefaultValue = "";
		}

		// 有輸入異動時間結束範圍，則設定條件
		if (DateTime.TryParse(tb_etime.Text.Trim(), out cketime))
			ods_Ad_Mail.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_etime.Text = "";
			ods_Ad_Mail.SelectParameters["etime"].DefaultValue = "";
		}

		gv_Ad_Mail.DataBind();
		if (gv_Ad_Mail.PageCount - 1 < gv_Ad_Mail.PageIndex)
		{
			gv_Ad_Mail.PageIndex = gv_Ad_Mail.PageCount;
			gv_Ad_Mail.DataBind();
		}
	}
}
