//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 目錄更名
//備註說明	使用資料庫儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class _30024 : System.Web.UI.Page
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
				if (lb_fl_url.Text == Album.Root)
					mErr = "根目錄不可變更!\\n";
				else
				{
					lb_path.Text = Server.MapPath(lb_fl_url.Text);

					if (Directory.Exists(lb_path.Text))
					{
						// 取得上一層目錄
						DirectoryInfo pPath = Directory.GetParent(lb_path.Text);
						lb_ppath.Text = pPath.FullName;

						tb_al_name.Text = lb_path.Text.Replace(lb_ppath.Text, "").Replace("\\", "");
					}
					else
						mErr = "找不到指定的路徑\\n";
				}
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

	protected void bn_rndir_ok_Click(object sender, EventArgs e)
	{
		Decoder dcode = new Decoder();
		string smkdir = "", mErr = "", sPath = "";

		smkdir = tb_al_name.Text.Trim();
		if (smkdir == "")
			mErr = "請輸入子目錄的名稱!\\n";
		{
			smkdir = lb_ppath.Text + "\\" + tb_al_name.Text.Trim();

			// 檢查是否有同名的目錄
			if (Directory.Exists(smkdir))
				mErr = "已有相同名稱的目錄!\\n";
			else
			{
				sPath = Album.Root + smkdir.Replace(Server.MapPath(Album.Root), "").Replace("\\", "/");

				try
				{
					Directory.Move(lb_path.Text, smkdir);

					if (!Directory.Exists(smkdir))
						mErr = "目錄更名失敗!\\n";
				}
				catch
				{
					mErr = "目錄無法更名!\\n";
				}
			}
		}

		if (mErr == "")
			lt_show.Text = "<script language=\"javascript\">alert(\"目錄完成變更!\\n\");parent.location.replace(\"3002.aspx?fl_url=" + Server.UrlEncode(dcode.EnCode(sPath)) + "\");</script>";
		else
			lt_show.Text = "<script language=\"javascript\">resize();alert(\"" + mErr + "\");parent.clean_win();</script>";

	}
}
