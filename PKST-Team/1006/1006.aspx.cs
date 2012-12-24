//---------------------------------------------------------------------------- 
//程式功能	權限設定管理
//---------------------------------------------------------------------------- 
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _1006 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0;
			Common_Func cfc = new Common_Func();

            // 檢查使用者權限並存入使用紀錄
			//Check_Power("1006", true);

			#region 接受下一頁返回時的舊查詢條件
			lb_page.Text = "";

			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"], out ckint))
					gv_Func_Item.PageIndex = ckint;
			}

			if (Request["fi_no1"] != null)
			{
				tb_fi_no1.Text = cfc.CleanSQL(Request["fi_no1"]);
				ods_Func_Item.SelectParameters["fi_no1"].DefaultValue = tb_fi_no1.Text;
				lb_page.Text = lb_page.Text + "&fi_no1=" + tb_fi_no1.Text;
			}

			if (Request["fi_name1"] != null)
			{
				tb_fi_name1.Text = cfc.CleanSQL(Request["fi_name1"]);
				ods_Func_Item.SelectParameters["fi_name1"].DefaultValue = tb_fi_name1.Text;
				lb_page.Text = lb_page.Text + "&fi_name1=" + Server.UrlEncode(tb_fi_name1.Text);
			}

			if (Request["visible1"] != null)
			{
				if (int.TryParse(Request["visible1"], out ckint))
				{
					ddl_visible1.SelectedValue = ckint.ToString();
					ods_Func_Item.SelectParameters["visible1"].DefaultValue = ckint.ToString();
					lb_page.Text = lb_page.Text + "&visible1=" + ckint.ToString();
				}
			}

			if (Request["fi_no2"] != null)
			{
				tb_fi_no2.Text = cfc.CleanSQL(Request["fi_no2"]);
				ods_Func_Item.SelectParameters["fi_no2"].DefaultValue = tb_fi_no2.Text;
				lb_page.Text = lb_page.Text + "&fi_no2=" + tb_fi_no2.Text;
			}

			if (Request["fi_name2"] != null)
			{
				tb_fi_name1.Text = cfc.CleanSQL(Request["fi_name2"]);
				ods_Func_Item.SelectParameters["fi_name2"].DefaultValue = tb_fi_name2.Text;
				lb_page.Text = lb_page.Text + "&fi_name2=" + Server.UrlEncode(tb_fi_name2.Text);
			}

			if (Request["visible2"] != null)
			{
				if (int.TryParse(Request["visible2"], out ckint))
				{
					ddl_visible2.SelectedValue = ckint.ToString();
					ods_Func_Item.SelectParameters["visible2"].DefaultValue = ckint.ToString();
					lb_page.Text = lb_page.Text + "&visible2=" + ckint.ToString();
				}
			}
			#endregion
		}

		#region 檢查頁數是否超過
		ods_Func_Item.DataBind();
		gv_Func_Item.DataBind();
		if (gv_Func_Item.PageCount < gv_Func_Item.PageIndex)
		{
			gv_Func_Item.PageIndex = gv_Func_Item.PageCount;
			gv_Func_Item.DataBind();
		}

		lb_pageid.Text = "?pageid=" + gv_Func_Item.PageIndex;
		#endregion
	}

    // Check_Power() 檢查使用者權限並存入使用紀錄
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

	protected void gv_Func_Item_PageIndexChanged(object sender, EventArgs e)
	{
		lb_pageid.Text = "?pageid=" + gv_Func_Item.PageIndex.ToString();
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
		lb_page.Text = "";

		// 有輸入 fi_no1，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_fi_no1.Text.Trim());
		if (tmpstr != "")
		{
			tb_fi_no1.Text = tmpstr;
			ods_Func_Item.SelectParameters["fi_no1"].DefaultValue = tmpstr;
			lb_page.Text = lb_page.Text + "&fi_no1=" + tb_fi_no1.Text;
		}
		else
		{
			tb_fi_no1.Text = "";
			ods_Func_Item.SelectParameters["fi_no1"].DefaultValue = "";
		}

		// 有輸入 fi_name1，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_fi_name1.Text.Trim());
		if (tmpstr != "")
		{
			tb_fi_name1.Text = tmpstr;
			ods_Func_Item.SelectParameters["fi_name1"].DefaultValue = tmpstr;
			lb_page.Text = lb_page.Text + "&fi_name1=" + Server.UrlEncode(tb_fi_name1.Text);
		}
		else
		{
			tb_fi_name1.Text = "";
			ods_Func_Item.SelectParameters["fi_name1"].DefaultValue = "";
		}

		// 有輸入 visible1，則設定條件
		ods_Func_Item.SelectParameters["visible1"].DefaultValue = ddl_visible1.SelectedValue;
		lb_page.Text = lb_page.Text + "&visible1=" + ddl_visible1.SelectedValue.ToString();

		// 有輸入 fi_no2，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_fi_no2.Text.Trim());
		if (tmpstr != "")
		{
			tb_fi_no2.Text = tmpstr;
			ods_Func_Item.SelectParameters["fi_no2"].DefaultValue = tmpstr;
			lb_page.Text = lb_page.Text + "&fi_no2=" + tb_fi_no2.Text;
		}
		else
		{
			tb_fi_no2.Text = "";
			ods_Func_Item.SelectParameters["fi_no2"].DefaultValue = "";
		}

		// 有輸入 fi_name2，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_fi_name2.Text.Trim());
		if (tmpstr != "")
		{
			tb_fi_name2.Text = tmpstr;
			ods_Func_Item.SelectParameters["fi_name2"].DefaultValue = tmpstr;
			lb_page.Text = lb_page.Text + "&fi_name2=" + Server.UrlEncode(tb_fi_name2.Text);
		}
		else
		{
			tb_fi_name2.Text = "";
			ods_Func_Item.SelectParameters["fi_name2"].DefaultValue = "";
		}

		// 有輸入 visible2，則設定條件
		ods_Func_Item.SelectParameters["visible2"].DefaultValue = ddl_visible2.SelectedValue;
		lb_page.Text = lb_page.Text + "&visible2=" + ddl_visible2.SelectedValue.ToString();

		ods_Func_Item.DataBind();
		gv_Func_Item.DataBind();
        if (gv_Func_Item.PageCount -1 < gv_Func_Item.PageIndex)
        {
            gv_Func_Item.PageIndex = gv_Func_Item.PageCount;
            gv_Func_Item.DataBind();
        }

		lb_pageid.Text = "?pageid=" + gv_Func_Item.PageIndex;
	}

	protected void gv_Func_Item_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if ((e.Row.RowType == DataControlRowType.DataRow))
		{
			switch (DataBinder.Eval(e.Row.DataItem , "visible1").ToString())
			{
				case "0":
					e.Row.Cells[2].Text = "隱藏";
					break;
				case "1":
					e.Row.Cells[2].Text = "顯示";
					break;
				case "2":
					e.Row.Cells[2].Text = "系統";
					break;
				default:
					e.Row.Cells[2].Text = "不明";
					break;
			}

			switch (DataBinder.Eval(e.Row.DataItem, "visible2").ToString())
			{
				case "0":
					e.Row.Cells[5].Text = "隱藏";
					break;
				case "1":
					e.Row.Cells[5].Text = "顯示";
					break;
				case "2":
					e.Row.Cells[5].Text = "系統";
					break;
				default:
					e.Row.Cells[5].Text = "不明";
					break;
			}
		}
	}
}
