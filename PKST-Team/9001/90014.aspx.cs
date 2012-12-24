//---------------------------------------------------------------------------- 
//程式功能	廣告信發送管理 (廣告信清單) > 會員名單
//---------------------------------------------------------------------------- 
using System;
using System.Web.UI.WebControls;

public partial class _90014 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		int ckint = 0;

		if (!IsPostBack)
		{
			Common_Func cfc = new Common_Func();
			DateTime ckbtime, cketime;

			// 檢查使用者權限並存入登入紀錄
			//Check_Power("9001", true);

			#region 接受下一頁返回時的舊查詢條件
			if (Request["pageid1"] != null)
			{
				if (int.TryParse(Request["pageid1"], out ckint))
				{
					if (ckint > gv_Ad_Member.PageCount)
						ckint = gv_Ad_Member.PageCount;

					gv_Ad_Member.PageIndex = ckint;
				}
				else
					lb_pageid1.Text = "0";
			}

			if (Request["adb_sid"] != null)
			{
				if (int.TryParse(Request["adb_sid"], out ckint))
				{
					tb_adb_sid.Text = ckint.ToString();
					ods_Ad_Member.SelectParameters["adb_sid"].DefaultValue = ckint.ToString();
				}
			}

			if (Request["adb_email"] != null)
			{
				if (int.TryParse(Request["adb_email"], out ckint))
				{
					tb_adb_email.Text = ckint.ToString();
					ods_Ad_Member.SelectParameters["adb_email"].DefaultValue = ckint.ToString();
				}
			}

			if (Request["adb_ibtime"] != null)
			{
				if (DateTime.TryParse(Request["adb_ibtime"], out ckbtime))
				{
					tb_ibtime.Text = ckint.ToString();
					ods_Ad_Member.SelectParameters["btime"].DefaultValue = ckbtime.ToString();
				}
			}

			if (Request["adb_ietime"] != null)
			{
				if (DateTime.TryParse(Request["adb_ietime"], out cketime))
				{
					tb_ietime.Text = ckint.ToString();
					ods_Ad_Member.SelectParameters["etime"].DefaultValue = cketime.ToString();
				}
			}
			#endregion

			#region 承接上一頁的查詢條件設定
			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"].ToString(), out ckint))
				{
					lb_page.Text = "?pageid=" + ckint.ToString();
				}
				else
				{
					lb_page.Text = "?pageid=0";
				}
			}

			if (Request["adm_sid"] != null)
				lb_page.Text += "&adm_sid=" + Server.UrlEncode(Request["adm_sid"]);

			if (Request["adm_title"] != null)
				lb_page.Text += "&adm_title=" + Server.UrlEncode(Request["adm_title"]);

			if (Request["adm_fname"] != null)
				lb_page.Text += "&adm_fname=" + Server.UrlEncode(Request["adm_fname"]);

			if (Request["adm_fmail"] != null)
				lb_page.Text += "&adm_fmail=" + Server.UrlEncode(Request["adm_fmail"]);

			if (Request["btime"] != null)
				lb_page.Text += "&btime=" + Server.UrlEncode(Request["btime"]);

			if (Request["etime"] != null)
				lb_page.Text += "&etime=" + Server.UrlEncode(Request["etime"]);
			#endregion

			ods_Ad_Member.DataBind();
			gv_Ad_Member.DataBind();

			#region 檢查頁數是否超過
			if (gv_Ad_Member.PageCount < gv_Ad_Member.PageIndex + 1)
			{
				gv_Ad_Member.PageIndex = gv_Ad_Member.PageCount - 1;
				gv_Ad_Member.DataBind();
			}

			lb_pageid1.Text = gv_Ad_Member.PageIndex.ToString();
			#endregion
		}
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
	protected void gv_Ad_Member_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid1.Text = gv_Ad_Member.PageIndex.ToString();
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
		if (int.TryParse(tb_adb_sid.Text.Trim(), out ckint))
			ods_Ad_Member.SelectParameters["adb_sid"].DefaultValue = ckint.ToString();
		else
		{
			tb_adb_sid.Text = "";
			ods_Ad_Member.SelectParameters["adb_sid"].DefaultValue = "";
		}

		// 有輸入 adb_name，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_adb_name.Text.Trim());
		if (tmpstr != "")
			ods_Ad_Member.SelectParameters["adb_name"].DefaultValue = tmpstr;
		else
		{
			tb_adb_name.Text = "";
			ods_Ad_Member.SelectParameters["adb_name"].DefaultValue = "";
		}

		// 有輸入 adb_email，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_adb_email.Text.Trim());
		if (tmpstr != "")
			ods_Ad_Member.SelectParameters["adb_email"].DefaultValue = tmpstr;
		else
		{
			tb_adb_email.Text = "";
			ods_Ad_Member.SelectParameters["adb_email"].DefaultValue = "";
		}

		// 有輸入異動時間開始範圍，則設定條件
		if (DateTime.TryParse(tb_ibtime.Text.Trim(), out ckbtime))
			ods_Ad_Member.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_ibtime.Text = "";
			ods_Ad_Member.SelectParameters["btime"].DefaultValue = "";
		}

		// 有輸入異動時間結束範圍，則設定條件
		if (DateTime.TryParse(tb_ietime.Text.Trim(), out cketime))
			ods_Ad_Member.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_ietime.Text = "";
			ods_Ad_Member.SelectParameters["etime"].DefaultValue = "";
		}

		gv_Ad_Member.DataBind();
		if (gv_Ad_Member.PageCount - 1 < gv_Ad_Member.PageIndex)
		{
			gv_Ad_Member.PageIndex = gv_Ad_Member.PageCount;
			gv_Ad_Member.DataBind();
			lb_pageid1.Text = gv_Ad_Member.PageIndex.ToString();
		}
	}

	// 新增存檔
	protected void tb_save_Click(object sender, EventArgs e)
	{
		ods_Ad_Member.InsertParameters["adb_email"].DefaultValue = tb_email.Text.Trim();
		ods_Ad_Member.InsertParameters["adb_name"].DefaultValue = tb_name.Text.Trim();

		ods_Ad_Member.Insert();
	}

	// 新增事件處理常式
	protected void ods_Ad_Member_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
	{
		string mErr = "";
		if ((int)e.ReturnValue > 0)
		{
			mErr = "資料新增成功！\\n新的會員編號是「" + e.ReturnValue.ToString() + "」。\\n";

			tb_email.Text = "";
			tb_name.Text = "";
		}
		else
			mErr = "資料新增失敗！\\n";

		ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	// 修改事件處理常式
	protected void ods_Ad_Member_Updated(object sender, ObjectDataSourceStatusEventArgs e)
	{
		string mErr = "";

		if ((int)e.ReturnValue == 0)
			mErr = "資料修改失敗!\\n";
		//else
		//    mErr = "資料更新成功!\\n";

		if (mErr != "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	// 刪除事件處理常式
	protected void ods_Ad_Member_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
	{
		string mErr = "";
		if ((int)e.ReturnValue == 0)
		{
			mErr = "資料刪除失敗!\\n";
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
		}
	}

	// 修改前的判斷及處理
	protected void ods_Ad_Member_Updating(object sender, ObjectDataSourceMethodEventArgs e)
	{
		Check_Internet cknet = new Check_Internet();
		string mErr = "", adb_email = "";

		if (e.InputParameters["adb_name"] == null)
			mErr += "請輸入會員姓名!\\n";
		else
		{
			if (e.InputParameters["adb_name"].ToString().Trim() == "")
				mErr += "請輸入會員姓名!\\n";
		}

		if (e.InputParameters["adb_email"] != null)
		{
			// 電子信箱一律轉成小寫
			adb_email = e.InputParameters["adb_email"].ToString().Trim().ToLower();
		}

		if (adb_email == "")
			mErr += "請輸入電子郵件信箱!\\n";
		else
		{
			if (cknet.Check_Email(adb_email) != 0)
				mErr = "電子郵件信箱格式錯誤!\\n";
			else
				e.InputParameters["adb_email"] = adb_email;
		}

		if (mErr != "") {
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
			e.Cancel = true;		// 取消修改動作
		}
	}

	// 新增前判斷及處理
	protected void ods_Ad_Member_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
	{
		Check_Internet cknet = new Check_Internet();
		string mErr = "", adb_email = "";

		if (e.InputParameters["adb_name"] != null)
		{
			if (e.InputParameters["adb_name"].ToString().Trim() == "")
				mErr += "請輸入會員姓名!\\n";
		}
		else
			mErr += "請輸入會員姓名!\\n";

		if (e.InputParameters["adb_email"] != null)
		{
			// 電子信箱一律轉成小寫
			adb_email = e.InputParameters["adb_email"].ToString().Trim().ToLower();
		}

		if (adb_email == "")
			mErr += "請輸入電子郵件信箱!\\n";
		else
		{
			if (cknet.Check_Email(adb_email) != 0)
				mErr = "電子郵件信箱格式錯誤!\\n";
			else
				e.InputParameters["adb_email"] = adb_email;
		}

		if (mErr != "")
		{
			mErr += "無法新增，請重新輸入!\\n";

			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");add_display();", true);
			e.Cancel = true;		// 取消新增動件
		}
	}

	// 資料刪除處理
	protected void gv_Ad_Member_RowDeleted(object sender, GridViewDeletedEventArgs e)
	{
		int mpage = gv_Ad_Member.PageCount - 1;

		gv_Ad_Member.DataBind();
		if (gv_Ad_Member.PageIndex + 1 > gv_Ad_Member.PageCount)
			gv_Ad_Member.PageIndex = mpage - 1;
	}
}
