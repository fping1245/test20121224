//---------------------------------------------------------------------------- 
//程式功能	論壇管理
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _D002 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0;
			Common_Func cfc = new Common_Func();
			DateTime ckbtime, cketime;

			// 檢查使用者權限並存入登入紀錄
			//Check_Power("D002", true);

			ods_Fm_Forum.SelectParameters["is_close"].DefaultValue = "";
			
			#region 接受下一頁返回時的舊查詢條件
			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"], out ckint))
					gv_Fm_Forum.PageIndex = ckint;
				else
					lb_pageid.Text = "0";
			}

			if (Request["ff_topic"] != null)
			{
				tb_ff_topic.Text = cfc.CleanSQL(Request["ff_topic"]);
				ods_Fm_Forum.SelectParameters["ff_topic"].DefaultValue = tb_ff_topic.Text;
			}

			if (Request["ff_desc"] != null)
			{
				tb_ff_desc.Text = cfc.CleanSQL(Request["ff_desc"]);
				ods_Fm_Forum.SelectParameters["ff_desc"].DefaultValue = tb_ff_desc.Text;
			}

			if (Request["ff_name"] != null)
			{
				tb_ff_name.Text = cfc.CleanSQL(Request["ff_name"]);
				ods_Fm_Forum.SelectParameters["ff_name"].DefaultValue = tb_ff_name.Text;
			}

			if (Request["btime"] != null)
			{
				if (DateTime.TryParse(Request["btime"], out ckbtime))
				{
					tb_btime.Text = Request["btime"];
					ods_Fm_Forum.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
				}
			}

			if (Request["etime"] != null)
			{
				if (DateTime.TryParse(Request["etime"], out cketime))
				{
					tb_btime.Text = Request["etime"];
					ods_Fm_Forum.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
				}
			}
			#endregion
		}

		#region 檢查頁數是否超過
		ods_Fm_Forum.DataBind();
		gv_Fm_Forum.DataBind();
		if (gv_Fm_Forum.PageCount < gv_Fm_Forum.PageIndex)
		{
			gv_Fm_Forum.PageIndex = gv_Fm_Forum.PageCount;
			gv_Fm_Forum.DataBind();
		}

		lb_pageid.Text = gv_Fm_Forum.PageIndex.ToString();
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

		DateTime ckbtime, cketime;
		string tmpstr = "";

		// 有輸入 ff_desc，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_ff_desc.Text.Trim());
		if (tmpstr != "")
			ods_Fm_Forum.SelectParameters["ff_desc"].DefaultValue = tmpstr;
		else
		{
			tb_ff_desc.Text = "";
			ods_Fm_Forum.SelectParameters["ff_desc"].DefaultValue = "";
		}

		// 有輸入 ff_name，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_ff_name.Text.Trim());
		if (tmpstr != "")
			ods_Fm_Forum.SelectParameters["ff_name"].DefaultValue = tmpstr;
		else
		{
			tb_ff_name.Text = "";
			ods_Fm_Forum.SelectParameters["ff_name"].DefaultValue = "";
		}

		// 有輸入 ff_topic，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_ff_topic.Text.Trim());
		if (tmpstr != "")
			ods_Fm_Forum.SelectParameters["ff_topic"].DefaultValue = tmpstr;
		else
		{
			tb_ff_topic.Text = "";
			ods_Fm_Forum.SelectParameters["ff_topic"].DefaultValue = "";
		}

		// 有輸入 btime 範圍，則設定條件
		if (DateTime.TryParse(tb_btime.Text.Trim(), out ckbtime))
			ods_Fm_Forum.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_btime.Text = "";
			ods_Fm_Forum.SelectParameters["btime"].DefaultValue = "";
		}

		// 有輸入 etime 範圍，則設定條件
		if (DateTime.TryParse(tb_etime.Text.Trim(), out cketime))
			ods_Fm_Forum.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_etime.Text = "";
			ods_Fm_Forum.SelectParameters["etime"].DefaultValue = "";
		}

		gv_Fm_Forum.DataBind();
		if (gv_Fm_Forum.PageCount - 1 < gv_Fm_Forum.PageIndex)
		{
			gv_Fm_Forum.PageIndex = gv_Fm_Forum.PageCount;
			gv_Fm_Forum.DataBind();
		}
	}

	// 換頁處理
	protected void gv_Fm_Forum_PageIndexChanged(object sender, EventArgs e)
	{
		lb_pageid.Text = gv_Fm_Forum.PageIndex.ToString();
	}

	// 更新顯示的資料格式
	protected void gv_Fm_Forum_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		Image img_tmp;

		if ((e.Row.RowType == DataControlRowType.DataRow))
		{
			#region 性別處理
			string ff_sex = DataBinder.Eval(e.Row.DataItem, "ff_sex").ToString();

			img_tmp = (Image)e.Row.Cells[0].FindControl("img_ff_sex");

			if (img_tmp != null)
			{
				switch (ff_sex)
				{
					case "1":
						img_tmp.ImageUrl = "../images/symbol/man.gif";
						img_tmp.ToolTip = "男性";
						img_tmp.AlternateText = "男性";
						break;
					case "2":
						img_tmp.ImageUrl = "../images/symbol/woman.gif";
						img_tmp.ToolTip = "女性";
						img_tmp.AlternateText = "女性";
						break;
					default:
						img_tmp.ImageUrl = "../images/symbol/unknow.gif";
						img_tmp.ToolTip = "未填性別";
						img_tmp.AlternateText = "未填性別";
						break;
				}
			}
			#endregion

			#region 心情符號處理
			string ff_symbol = DataBinder.Eval(e.Row.DataItem, "ff_symbol").ToString();
			img_tmp = (Image)e.Row.Cells[0].FindControl("img_ff_symbol");

			Common_Func.ImageSymbol img_symbo = new Common_Func.ImageSymbol();

			img_symbo.code = int.Parse(ff_symbol);
			img_tmp.ImageUrl = img_symbo.image;
			img_tmp.ToolTip = img_symbo.name;
			img_tmp.AlternateText = img_symbo.name;
			#endregion

			#region 內容隱藏處理 (隱藏內容但開放留言的狀況，使用替代文字)
			if (DataBinder.Eval(e.Row.DataItem, "is_show").ToString() == "0"
				&& DataBinder.Eval(e.Row.DataItem, "is_close").ToString() == "1")
			{
				Label ff_desc = (Label)e.Row.Cells[0].FindControl("lb_ff_desc");

				ff_desc.Text = "<font color=red><b>××× 隱藏  ××× " + DataBinder.Eval(e.Row.DataItem, "instead").ToString() + "</b></font>";

				Label ff_topic = (Label)e.Row.Cells[0].FindControl("lb_ff_topic");

				ff_topic.Text = "<font color=red><b>××× 隱藏  ××× " + "</b></font>" + ff_topic.Text;

			}
			#endregion

			#region 隱藏內容
			Label is_show = (Label)e.Row.Cells[0].FindControl("lb_is_show");
			if (DataBinder.Eval(e.Row.DataItem, "is_show").ToString() == "0")
				is_show.Text = "<font color=blue><b>隱藏<b></font>";
			else
				is_show.Text = "顯示";
			#endregion

			#region 開放內容
			Label is_close = (Label)e.Row.Cells[0].FindControl("lb_is_close");
			if (DataBinder.Eval(e.Row.DataItem, "is_close").ToString() == "0")
				is_close.Text = "<font color=blue><b>關閉<b></font>";
			else
				is_close.Text = "開放";
			#endregion

			#region 內容關閉處理
			if (DataBinder.Eval(e.Row.DataItem, "is_close").ToString() == "0")
			{
				Label ff_desc = (Label)e.Row.Cells[0].FindControl("lb_ff_desc");

				ff_desc.Text = "<p style=\"background-color:#F09AF4; margin:0pt 0pt 0pt 0pt\"><b>××× 關閉 ×××</b><br>" + ff_desc.Text + "</p>";

				Label ff_topic = (Label)e.Row.Cells[0].FindControl("lb_ff_topic");

				ff_topic.Text = "<p style=\"background-color:#F09AF4; margin:0pt 0pt 0pt 0pt\"><b>××× 關閉 ×××</b>" + ff_topic.Text + "</p>";
			}
			#endregion
		}
	}
}
