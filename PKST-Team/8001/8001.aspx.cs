//---------------------------------------------------------------------------- 
//程式功能	HTML編輯器 (網頁清單)
//---------------------------------------------------------------------------- 
using System;
using System.Web.UI.WebControls;

public partial class _8001 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0;
			Common_Func cfc = new Common_Func();
			DateTime ckbtime, cketime;

			// 檢查使用者權限並存入登入紀錄
			//Check_Power("8001", true);

			#region 接受下一頁返回時的舊查詢條件
			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"], out ckint))
				{
					if (ckint > gv_Html_Edit.PageCount)
						ckint = gv_Html_Edit.PageCount;

					gv_Html_Edit.PageIndex = ckint;
				}
				else
					lb_pageid.Text = "0";
			}

			if (Request["he_sid"] != null)
			{
				if (int.TryParse(Request["he_sid"], out ckint))
				{
					tb_he_sid.Text = ckint.ToString();
					ods_Html_Edit.SelectParameters["he_sid"].DefaultValue = ckint.ToString();
				}
			}

			if (Request["he_title"] != null)
			{
				tb_he_title.Text = cfc.CleanSQL(Request["he_title"]);
				ods_Html_Edit.SelectParameters["he_title"].DefaultValue = tb_he_title.Text;
			}

			if (Request["he_desc"] != null)
			{
				tb_he_desc.Text = cfc.CleanSQL(Request["he_desc"]);
				ods_Html_Edit.SelectParameters["he_desc"].DefaultValue = tb_he_desc.Text;
			}

			if (Request["btime"] != null)
				if (DateTime.TryParse(Request["btime"], out ckbtime))
				{
					tb_btime.Text = Request["btime"];
					ods_Html_Edit.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
				}

			if (Request["etime"] != null)
				if (DateTime.TryParse(Request["etime"], out cketime))
				{
					tb_btime.Text = Request["etime"];
					ods_Html_Edit.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
				}

			#endregion
		}

		#region 檢查頁數是否超過
		ods_Html_Edit.DataBind();
		gv_Html_Edit.DataBind();
		if (gv_Html_Edit.PageCount < gv_Html_Edit.PageIndex)
		{
			gv_Html_Edit.PageIndex = gv_Html_Edit.PageCount;
			gv_Html_Edit.DataBind();
		}

		lb_pageid.Text = gv_Html_Edit.PageIndex.ToString();
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
	protected void gv_Html_Edit_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid.Text = gv_Html_Edit.PageIndex.ToString();
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
		if (int.TryParse(tb_he_sid.Text.Trim(), out ckint))
			ods_Html_Edit.SelectParameters["he_sid"].DefaultValue = ckint.ToString();
		else
		{
			tb_he_sid.Text = "";
			ods_Html_Edit.SelectParameters["he_sid"].DefaultValue = "";
		}

		// 有輸入 he_title，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_he_title.Text.Trim());
		if (tmpstr != "")
			ods_Html_Edit.SelectParameters["he_title"].DefaultValue = tmpstr;
		else
		{
			tb_he_title.Text = "";
			ods_Html_Edit.SelectParameters["he_title"].DefaultValue = "";
		}

		// 有輸入 he_desc，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_he_desc.Text.Trim());
		if (tmpstr != "")
			ods_Html_Edit.SelectParameters["he_desc"].DefaultValue = tmpstr;
		else
		{
			tb_he_desc.Text = "";
			ods_Html_Edit.SelectParameters["he_desc"].DefaultValue = "";
		}

		// 有輸入異動時間開始範圍，則設定條件
		if (DateTime.TryParse(tb_btime.Text.Trim(), out ckbtime))
			ods_Html_Edit.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_btime.Text = "";
			ods_Html_Edit.SelectParameters["btime"].DefaultValue = "";
		}

		// 有輸入異動時間結束範圍，則設定條件
		if (DateTime.TryParse(tb_etime.Text.Trim(), out cketime))
			ods_Html_Edit.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_etime.Text = "";
			ods_Html_Edit.SelectParameters["etime"].DefaultValue = "";
		}

		gv_Html_Edit.DataBind();
		if (gv_Html_Edit.PageCount - 1 < gv_Html_Edit.PageIndex)
		{
			gv_Html_Edit.PageIndex = gv_Html_Edit.PageCount;
			gv_Html_Edit.DataBind();
		}
	}
}
