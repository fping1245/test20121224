//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 建立子目錄
//備註說明	使用實體路徑儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class _30022 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			Decoder dcode = new Decoder();
			string mErr = "";

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("3002", false);

			if (Request["fl_url"] != null)
			{
				lb_fl_url.Text = dcode.DeCode(Request["fl_url"].Trim());
			}
			else
				mErr = "參數傳送錯誤!\\n";

			if (mErr != "")
				lt_show.Text = "<script language=\"javascript\">alert(\"" + mErr + "\");parent.close_all();parent.clean_win();</script>";
		}
    }

	// 檢查使用者權限並存入登入紀錄
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

	// 建立子目錄
	protected void bn_mkdir_ok_Click(object sender, EventArgs e)
	{
		Decoder dcode = new Decoder();
		string smkdir = "", mErr = "", fpath = "";

		smkdir = tb_al_name.Text.Trim();
		if (smkdir == "")
			mErr = "請輸入子目錄的名稱!\\n";
		else
		{
			fpath = Server.MapPath(lb_fl_url.Text);
			if (Directory.Exists(fpath))
			{
				fpath = fpath + "\\" + smkdir + "\\";
				if (Directory.Exists(fpath))
					mErr = "這個子目錄名稱已經存在!\\n";
				else
				{
					try
					{
						Directory.CreateDirectory(fpath);

						if (!Directory.Exists(fpath))
							mErr = "目錄建立失敗!\\n";
						else
						{
							#region 建立縮圖目錄
							fpath = fpath + "_thumb\\";
							Directory.CreateDirectory(fpath);
							#endregion
						}
					}
					catch (Exception ex)
					{
						mErr = ex.ToString() + "\\n";
					}
				}
			}
			else
				mErr = "找不到現在的目錄!\\n";
		}

		if (mErr == "")
			lt_show.Text = "<script language=\"javascript\">alert(\"目錄建立完成!\\n\");parent.tree_reload('" + Server.UrlEncode(dcode.EnCode(lb_fl_url.Text)) + "');parent.clean_win();</script>";
		else
			lt_show.Text = "<script language=\"javascript\">alert(\"" + mErr + "\");parent.clean_win();</script>";	
	}
}
