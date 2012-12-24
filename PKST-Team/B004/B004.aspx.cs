//---------------------------------------------------------------------------- 
//程式功能	考試成績統計
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _B004 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0;
			Common_Func cfc = new Common_Func();
			DateTime ckbtime, cketime;
			string tmpstr = "";

			// 檢查使用者權限並存入登入紀錄
			//Check_Power("B004", true);

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

			if (Request["is_show"] != null)
			{
				if (Request["is_show"] == "0")
				{
					rb_is_show0.Checked = true;
					rb_is_show1.Checked = false;
					rb_is_show_all.Checked = false;
					lb_is_show.Text = "0";
				}
				else if (Request["is_show"] == "1")
				{
					rb_is_show0.Checked = false;
					rb_is_show1.Checked = true;
					rb_is_show_all.Checked = false;
					lb_is_show.Text = "1";
				}
				else
				{
					rb_is_show0.Checked = false;
					rb_is_show1.Checked = false;
					rb_is_show_all.Checked = true;
					lb_is_show.Text = "";
				}
			}
			else
			{
				rb_is_show0.Checked = false;
				rb_is_show1.Checked = false;
				rb_is_show_all.Checked = true;
				lb_is_show.Text = "";
			}

			if (Request["btime"] != null)
			{
				if (DateTime.TryParse(Request["btime"], out ckbtime))
				{
					tb_btime.Text = Request["btime"];
					ods_Ts_Paper.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
				}
			}

			if (Request["etime"] != null)
			{
				if (DateTime.TryParse(Request["etime"], out cketime))
				{
					tb_btime.Text = Request["etime"];
					ods_Ts_Paper.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
				}
			}
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

	protected void gv_Ts_Paper_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		float tp_avg = 0, tp_total = 0, tp_member = 0;
		string is_show = "";

		if ((e.Row.RowType == DataControlRowType.DataRow))
		{
			is_show = DataBinder.Eval(e.Row.DataItem, "is_show").ToString();

			if (is_show == "0")
				e.Row.Cells[2].Text = "隱藏";
			else if (is_show == "1")
				e.Row.Cells[2].Text = "顯示";
			else
				e.Row.Cells[2].Text = "不明";

			tp_total = float.Parse(DataBinder.Eval(e.Row.DataItem, "tp_total").ToString());
			tp_member = float.Parse(DataBinder.Eval(e.Row.DataItem, "tp_member").ToString());

			if (tp_member == 0)
				e.Row.Cells[7].Text = "0.0000";
			else
			{
				tp_avg = tp_total / tp_member;
				e.Row.Cells[8].Text = tp_avg.ToString("F4");
			}
		}
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
		DateTime ckbtime, cketime;
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

		// 檢查 rb_is_show
		if (rb_is_show_all.Checked)
		{
			ods_Ts_Paper.SelectParameters["is_show"].DefaultValue = "";
			lb_is_show.Text = "";
		}
		else
		{
			if (rb_is_show0.Checked)
			{
				ods_Ts_Paper.SelectParameters["is_show"].DefaultValue = "0";
				lb_is_show.Text = "0";
			}
			else
			{
				ods_Ts_Paper.SelectParameters["is_show"].DefaultValue = "1";
				lb_is_show.Text = "1";
			}
		}

		// 有輸入最後投票時間開始範圍，則設定條件
		if (DateTime.TryParse(tb_btime.Text.Trim(), out ckbtime))
			ods_Ts_Paper.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_btime.Text = "";
			ods_Ts_Paper.SelectParameters["btime"].DefaultValue = "";
		}

		// 有輸入最後投票時間結束範圍，則設定條件
		if (DateTime.TryParse(tb_etime.Text.Trim(), out cketime))
			ods_Ts_Paper.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_etime.Text = "";
			ods_Ts_Paper.SelectParameters["etime"].DefaultValue = "";
		}

		gv_Ts_Paper.DataBind();
		if (gv_Ts_Paper.PageCount - 1 < gv_Ts_Paper.PageIndex)
		{
			gv_Ts_Paper.PageIndex = gv_Ts_Paper.PageCount;
			gv_Ts_Paper.DataBind();
		}
	}
}
