//---------------------------------------------------------------------------- 
//程式功能	票選結果統計
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _A002 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int ckint = 0;
		Common_Func cfc = new Common_Func();
		DateTime ckbtime, cketime;

		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("A002", true);

			#region 接受下一頁返回時的舊查詢條件
			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"], out ckint))
				{
					if (ckint > gv_Bt_Head.PageCount)
						ckint = gv_Bt_Head.PageCount;

					gv_Bt_Head.PageIndex = ckint;
				}
				else
					lb_pageid.Text = "0";
			}

			if (Request["bh_sid"] != null)
			{
				if (int.TryParse(Request["bh_sid"], out ckint))
				{
					tb_bh_sid.Text = ckint.ToString();
					ods_Bt_Head.SelectParameters["bh_sid"].DefaultValue = ckint.ToString();
				}
			}

			if (Request["is_check"] != null)
			{
				if (Request["is_check"] == "0")
				{
					rb_is_check0.Checked = true;
					rb_is_check1.Checked = false;
					rb_is_check_all.Checked = false;
				}
				else if (Request["is_check"] == "1")
				{
					rb_is_check0.Checked = false;
					rb_is_check1.Checked = true;
					rb_is_check_all.Checked = false;
				}
				else
				{
					rb_is_check0.Checked = false;
					rb_is_check1.Checked = false;
					rb_is_check_all.Checked = true;
				}
			}
			else
			{
				rb_is_check0.Checked = false;
				rb_is_check1.Checked = false;
				rb_is_check_all.Checked = true;
			}

			if (Request["btime"] != null)
			{
				if (DateTime.TryParse(Request["btime"], out ckbtime))
				{
					tb_btime.Text = Request["btime"];
					ods_Bt_Head.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
				}
			}

			if (Request["etime"] != null)
			{
				if (DateTime.TryParse(Request["etime"], out cketime))
				{
					tb_btime.Text = Request["etime"];
					ods_Bt_Head.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
				}
			}
			#endregion
		}

		#region 檢查頁數是否超過
		ods_Bt_Head.DataBind();
		gv_Bt_Head.DataBind();
		if (gv_Bt_Head.PageCount < gv_Bt_Head.PageIndex)
		{
			gv_Bt_Head.PageIndex = gv_Bt_Head.PageCount;
			gv_Bt_Head.DataBind();
		}

		lb_pageid.Text = gv_Bt_Head.PageIndex.ToString();
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
	protected void gv_Bt_Head_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid.Text = gv_Bt_Head.PageIndex.ToString();
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
		if (int.TryParse(tb_bh_sid.Text.Trim(), out ckint))
			ods_Bt_Head.SelectParameters["bh_sid"].DefaultValue = ckint.ToString();
		else
		{
			tb_bh_sid.Text = "";
			ods_Bt_Head.SelectParameters["bh_sid"].DefaultValue = "";
		}

		// 有輸入 bh_title，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_bh_title.Text.Trim());
		if (tmpstr != "")
			ods_Bt_Head.SelectParameters["bh_title"].DefaultValue = tmpstr;
		else
		{
			tb_bh_title.Text = "";
			ods_Bt_Head.SelectParameters["bh_title"].DefaultValue = "";
		}

		// 檢查 rb_is_check
		if (rb_is_check_all.Checked)
			ods_Bt_Head.SelectParameters["is_check"].DefaultValue = "";
		else
		{
			if (rb_is_check0.Checked)
				ods_Bt_Head.SelectParameters["is_check"].DefaultValue = "0";
			else
				ods_Bt_Head.SelectParameters["is_check"].DefaultValue = "1";
		}

		// 有輸入最後投票時間開始範圍，則設定條件
		if (DateTime.TryParse(tb_btime.Text.Trim(), out ckbtime))
			ods_Bt_Head.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_btime.Text = "";
			ods_Bt_Head.SelectParameters["btime"].DefaultValue = "";
		}

		// 有輸入最後投票時間結束範圍，則設定條件
		if (DateTime.TryParse(tb_etime.Text.Trim(), out cketime))
			ods_Bt_Head.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_etime.Text = "";
			ods_Bt_Head.SelectParameters["etime"].DefaultValue = "";
		}

		gv_Bt_Head.DataBind();
		if (gv_Bt_Head.PageCount - 1 < gv_Bt_Head.PageIndex)
		{
			gv_Bt_Head.PageIndex = gv_Bt_Head.PageCount;
			gv_Bt_Head.DataBind();
		}
	}

	protected void gv_Bt_Head_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		string is_check = "", is_show = "";

		if ((e.Row.RowType == DataControlRowType.DataRow))
		{
			is_check = DataBinder.Eval(e.Row.DataItem, "is_check").ToString();

			if (is_check == "0")
				e.Row.Cells[2].Text = "單選";
			else if (is_check == "1")
				e.Row.Cells[2].Text = "複選全部";
			else
				e.Row.Cells[2].Text = "複選 " + is_check + " 題";

			is_show = DataBinder.Eval(e.Row.DataItem, "is_show").ToString();

			if (is_show == "0")
				e.Row.Cells[7].Text = "－";
			else
				e.Row.Cells[7].Text = "☆";

		}
	}
}
