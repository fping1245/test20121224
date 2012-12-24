//---------------------------------------------------------------------------- 
//程式功能	通訊錄管理
//---------------------------------------------------------------------------- 
using System;
using System.Web.UI.WebControls;

public partial class _6002 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0;
			Common_Func cfc = new Common_Func();

			// 檢查使用者權限並存入登入紀錄
			//Check_Power("6002", true);

			#region 接受下一頁返回時的舊查詢條件
			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"], out ckint))
					gv_As_Book.PageIndex = ckint;
				else
					lb_pageid.Text = "0";
			}

			if (Request["ab_name"] != null)
			{
				tb_ag_name.Text = cfc.CleanSQL(Request["ab_name"]);
				ods_As_Book.SelectParameters["ab_name"].DefaultValue = tb_ab_name.Text;
			}

			if (Request["ab_nike"] != null)
			{
				tb_ab_nike.Text = cfc.CleanSQL(Request["ab_nike"]);
				ods_As_Book.SelectParameters["ab_nike"].DefaultValue = tb_ab_nike.Text;
			}

			if (Request["ab_company"] != null)
			{
				tb_ab_company.Text = cfc.CleanSQL(Request["ab_company"]);
				ods_As_Book.SelectParameters["ab_company"].DefaultValue = tb_ab_company.Text;
			}

			if (Request["ag_name"] != null)
			{
				tb_ag_name.Text = cfc.CleanSQL(Request["ag_name"]);
				ods_As_Book.SelectParameters["ag_name"].DefaultValue = tb_ag_name.Text;
			}

			if (Request["ag_attrib"] != null)
			{
				tb_ag_name.Text = cfc.CleanSQL(Request["ag_attrib"]);
				ods_As_Book.SelectParameters["ag_attrib"].DefaultValue = tb_ag_attrib.Text;
			}

			ods_As_Book.SelectParameters["mg_sid"].DefaultValue = Session["mg_sid"].ToString();

			#endregion
		}

		#region 檢查頁數是否超過
		ods_As_Book.DataBind();
		gv_As_Book.DataBind();
		if (gv_As_Book.PageCount < gv_As_Book.PageIndex)
		{
			gv_As_Book.PageIndex = gv_As_Book.PageCount;
			gv_As_Book.DataBind();
		}

		lb_pageid.Text = gv_As_Book.PageIndex.ToString();
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

	protected void gv_As_Book_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid.Text = gv_As_Book.PageIndex.ToString();
	}

	protected void Btn_Set_Click(object sender, EventArgs e)
	{
		// 檢查查詢條件是否改變
		Chk_Filter();
	}

	// 檢查查詢條件是否改變
	private void Chk_Filter() {
		Common_Func cfc = new Common_Func();

		string tmpstr = "";

		// 有輸入 ab_name 設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_ab_name.Text.Trim());
		if (tmpstr != "")
			ods_As_Book.SelectParameters["ab_name"].DefaultValue = tmpstr;
		else
		{
			tb_ab_name.Text = "";
			ods_As_Book.SelectParameters["ab_name"].DefaultValue = "";
		}

		// 有輸入 ab_nike 設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_ab_nike.Text.Trim());
		if (tmpstr != "")
			ods_As_Book.SelectParameters["ab_nike"].DefaultValue = tmpstr;
		else
		{
			tb_ab_nike.Text = "";
			ods_As_Book.SelectParameters["ab_nike"].DefaultValue = "";
		}

		// 有輸入 ab_company 設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_ab_company.Text.Trim());
		if (tmpstr != "")
			ods_As_Book.SelectParameters["ab_company"].DefaultValue = tmpstr;
		else
		{
			tb_ab_company.Text = "";
			ods_As_Book.SelectParameters["ab_company"].DefaultValue = "";
		}

		// 有輸入 ag_name 設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_ag_name.Text.Trim());
		if (tmpstr != "")
			ods_As_Book.SelectParameters["ag_name"].DefaultValue = tmpstr;
		else
		{
			tb_ag_name.Text = "";
			ods_As_Book.SelectParameters["ag_name"].DefaultValue = "";
		}

		// 有輸入 ag_attrib 設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_ag_attrib.Text.Trim());
		if (tmpstr != "")
			ods_As_Book.SelectParameters["ag_attrib"].DefaultValue = tmpstr;
		else
		{
			tb_ag_attrib.Text = "";
			ods_As_Book.SelectParameters["ag_attrib"].DefaultValue = "";
		}

		gv_As_Book.DataBind();
		if (gv_As_Book.PageCount - 1 < gv_As_Book.PageIndex)
		{
			gv_As_Book.PageIndex = gv_As_Book.PageCount;
			gv_As_Book.DataBind();
		}
	}
}
