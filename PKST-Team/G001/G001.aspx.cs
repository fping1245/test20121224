//---------------------------------------------------------------------------- 
//程式功能	資料庫規格管理
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _G001 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			int ckint = 0;
			string tmpstr = "";
			Common_Func cfc = new Common_Func();

			// 檢查使用者權限並存入登入紀錄
			//Check_Power("G001", true);

			#region 接受下一頁返回時的舊查詢條件
			if (Request["ds_code"] != null)
			{
				tb_ds_code.Text = cfc.CleanSQL(Request["ds_code"]);
				ods_Db_Sys.SelectParameters["ds_code"].DefaultValue = tb_ds_code.Text;
			}

			if (Request["ds_name"] != null)
			{
				tb_ds_name.Text = cfc.CleanSQL(Request["ds_name"]);
				ods_Db_Sys.SelectParameters["ds_name"].DefaultValue = tb_ds_name.Text;
			}

			if (Request["ds_database"] != null)
			{
				tb_ds_database.Text = cfc.CleanSQL(Request["ds_database"]);
				ods_Db_Sys.SelectParameters["ds_database"].DefaultValue = tb_ds_database.Text;
			}

			if (Request["sort"] != null)
			{
				tmpstr = cfc.CleanSQL(Request["sort"]);

				switch (tmpstr)
				{
					case "ds_code":
						lk_st_ds_code.CssClass = "";
						lk_st_ds_name.CssClass = "abtn";
						lk_st_ds_database.CssClass = "abtn";
						break;
					case "ds_name":
						lk_st_ds_code.CssClass = "abtn";
						lk_st_ds_name.CssClass = "";
						lk_st_ds_database.CssClass = "abtn";
						break;
					case "ds_database":
						lk_st_ds_code.CssClass = "abtn";
						lk_st_ds_name.CssClass = "abtn";
						lk_st_ds_database.CssClass = "";
						break;
					default:
						tmpstr = "ds_code";
						lk_st_ds_code.CssClass = "";
						lk_st_ds_name.CssClass = "abtn";
						lk_st_ds_database.CssClass = "abtn";
						break;
				}

				gv_Db_Sys.Sort(tmpstr, SortDirection.Ascending);
			}

			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"], out ckint))
					gv_Db_Sys.PageIndex = ckint;
				else
					lb_pageid.Text = "0";
			}
			#endregion
		}

		#region 檢查頁數是否超過
		ods_Db_Sys.DataBind();
		gv_Db_Sys.DataBind();
		if (gv_Db_Sys.PageCount < gv_Db_Sys.PageIndex)
		{
			gv_Db_Sys.PageIndex = gv_Db_Sys.PageCount;
			gv_Db_Sys.DataBind();
		}

		lb_pageid.Text = gv_Db_Sys.PageIndex.ToString();
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

		string tmpstr = "";

		// 有輸入 ds_code，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_ds_code.Text.Trim());
		if (tmpstr != "")
			ods_Db_Sys.SelectParameters["ds_code"].DefaultValue = tmpstr;
		else
		{
			tb_ds_code.Text = "";
			ods_Db_Sys.SelectParameters["ds_code"].DefaultValue = "";
		}

		// 有輸入 ds_name，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_ds_name.Text.Trim());
		if (tmpstr != "")
			ods_Db_Sys.SelectParameters["ds_name"].DefaultValue = tmpstr;
		else
		{
			tb_ds_name.Text = "";
			ods_Db_Sys.SelectParameters["ds_name"].DefaultValue = "";
		}

		// 有輸入 ds_database，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_ds_database.Text.Trim());
		if (tmpstr != "")
			ods_Db_Sys.SelectParameters["ds_database"].DefaultValue = tmpstr;
		else
		{
			tb_ds_database.Text = "";
			ods_Db_Sys.SelectParameters["ds_database"].DefaultValue = "";
		}

		gv_Db_Sys.DataBind();
		if (gv_Db_Sys.PageCount - 1 < gv_Db_Sys.PageIndex)
		{
			gv_Db_Sys.PageIndex = gv_Db_Sys.PageCount;
			gv_Db_Sys.DataBind();
		}
	}

	// 換頁處理
	protected void gv_Db_Sys_PageIndexChanged(object sender, EventArgs e)
	{
		lb_pageid.Text = gv_Db_Sys.PageIndex.ToString();
	}

	// 依照代號排序
	protected void lk_st_ds_code_Click(object sender, EventArgs e)
	{
		gv_Db_Sys.Sort("ds_code", SortDirection.Ascending);

		lk_st_ds_code.CssClass = "";
		lk_st_ds_database.CssClass = "abtn";
		lk_st_ds_name.CssClass = "abtn";
	}

	// 依照名稱排序
	protected void lk_st_ds_name_Click(object sender, EventArgs e)
	{
		gv_Db_Sys.Sort("ds_name", SortDirection.Ascending);

		lk_st_ds_code.CssClass = "abtn";
		lk_st_ds_database.CssClass = "abtn";
		lk_st_ds_name.CssClass = "";
	}

	// 依照資料庫排序
	protected void lk_st_ds_database_Click(object sender, EventArgs e)
	{
		gv_Db_Sys.Sort("ds_database", SortDirection.Ascending);

		lk_st_ds_code.CssClass = "abtn";
		lk_st_ds_database.CssClass = "";
		lk_st_ds_name.CssClass = "abtn";
	}

	// 資料顯示格式轉換
	protected void gv_Db_Sys_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if ((e.Row.RowType == DataControlRowType.DataRow))
		{
			Label lb_tmp = (Label)e.Row.Cells[0].FindControl("lb_ds_desc");
			lb_tmp.Text = DataBinder.Eval(e.Row.DataItem, "ds_desc").ToString().Replace("\r\n", "<br>");
		}
	}
	
	// 換頁按鍵處理
	protected void gv_Db_Sys_DataBound(object sender, EventArgs e)
	{
		int icnt = 0, acnt = 0, ecnt = 0;

		acnt = gv_Db_Sys.PageIndex / 10 * 10;
		if (acnt < 0)
			acnt = 0;

		ecnt = acnt + 10;
		if (ecnt >= gv_Db_Sys.PageCount)
			ecnt = gv_Db_Sys.PageCount;

		#region 選項頁面
		mu_page.Items.Clear();

		MenuItem mi_tmp1 = new MenuItem();

		mi_tmp1.Value = "X";
		mi_tmp1.Text = "| ";
		mi_tmp1.Enabled = false;

		mu_page.Items.Add(mi_tmp1);

		if (acnt > 0)
		{
			MenuItem mi_tmp3 = new MenuItem();
			mi_tmp3.Value = (acnt - 1).ToString();
			mi_tmp3.Text = "<<&nbsp;";

			mu_page.Items.Add(mi_tmp3);
		}

		for (icnt = acnt; icnt < ecnt; icnt++)
		{
			MenuItem mi_page = new MenuItem();

			mi_page.Value = icnt.ToString();
			if (gv_Db_Sys.PageIndex == icnt)
			{
				mi_page.Text = string.Format("[{0}]", icnt + 1);
				mi_page.Selected = true;
			}
			else
				mi_page.Text = string.Format("&nbsp;{0}&nbsp;", icnt + 1);

			mu_page.Items.Add(mi_page);
		}

		if (ecnt < gv_Db_Sys.PageCount)
		{
			MenuItem mi_tmp4 = new MenuItem();
			mi_tmp4.Value = (ecnt).ToString();
			mi_tmp4.Text = "&nbsp;>>";

			mu_page.Items.Add(mi_tmp4);
		}

		MenuItem mi_tmp2 = new MenuItem();

		mi_tmp2.Value = "X";
		mi_tmp2.Text = " |";
		mi_tmp2.Enabled = false;

		mu_page.Items.Add(mi_tmp2);

		#endregion

		#region 換頁按鍵處理
		if (gv_Db_Sys.PageIndex == 0)
		{
			ib_first.Enabled = false;
			ib_prev.Enabled = false;
			ib_first.ToolTip = "已經是在第一頁了";
			ib_prev.ToolTip = "已經是在第一頁了";
		}
		else
		{
			ib_first.Enabled = true;
			ib_prev.Enabled = true;
			ib_first.ToolTip = "第一頁";
			ib_prev.ToolTip = "下一頁";
		}
		if (gv_Db_Sys.PageIndex + 1 >= gv_Db_Sys.PageCount)
		{
			ib_next.Enabled = false;
			ib_last.Enabled = false;
			ib_next.ToolTip = "已經是最後一頁了";
			ib_last.ToolTip = "已經是最後一頁了";
		}
		else
		{
			ib_next.Enabled = true;
			ib_last.Enabled = true;
			ib_next.ToolTip = "下一頁";
			ib_last.ToolTip = "最末頁";
		}
		#endregion
	}

	// 回到首頁
	protected void ib_first_Click(object sender, ImageClickEventArgs e)
	{
		gv_Db_Sys.PageIndex = 0;
		gv_Db_Sys.DataBind();
	}

	// 上一頁
	protected void ib_prev_Click(object sender, ImageClickEventArgs e)
	{
		if (gv_Db_Sys.PageIndex > 0)
			gv_Db_Sys.PageIndex = gv_Db_Sys.PageIndex - 1;
		gv_Db_Sys.DataBind();
	}

	// 下一頁
	protected void ib_next_Click(object sender, ImageClickEventArgs e)
	{
		if (gv_Db_Sys.PageIndex + 1 < gv_Db_Sys.PageCount)
			gv_Db_Sys.PageIndex = gv_Db_Sys.PageIndex + 1;
		gv_Db_Sys.DataBind();
	}

	// 最末頁
	protected void ib_last_Click(object sender, ImageClickEventArgs e)
	{
		gv_Db_Sys.PageIndex = gv_Db_Sys.PageCount - 1;
		gv_Db_Sys.DataBind();
	}

	// 選頁處理
	protected void mu_page_MenuItemClick(object sender, MenuEventArgs e)
	{
		int ckint = 0;
		if (int.TryParse(e.Item.Value, out ckint))
		{
			gv_Db_Sys.PageIndex = ckint;
		}
	}
}
