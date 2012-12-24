//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 刪除子目錄
//備註說明	使用實體路徑儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.IO;

public partial class _30023 : System.Web.UI.Page
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
					mErr = "根目錄不可刪除\\n";
				else
				{
					lb_path.Text = Server.MapPath(lb_fl_url.Text);

					#region 取得目前目錄的名稱
					lb_al_name.Text = lb_fl_url.Text.Replace(Album.Root,"").Replace("//","");

					if (!Directory.Exists(lb_path.Text))
						mErr = "找不到指定的路徑\\n";
					#endregion
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

	// 刪除目錄
	protected void bn_rddir_ok_Click(object sender, EventArgs e)
	{
		Decoder dcode = new Decoder();
		string mErr = "", pPath = "";

		if (Directory.Exists(lb_path.Text))
		{
			// 取得上層目錄
			DirectoryInfo pDir = Directory.GetParent(lb_path.Text);

			pPath = pDir.FullName.ToString();
			if (pPath.Substring(pPath.Length - 1, 1) != "\\")
				pPath += "\\";

			pPath = Album.Root + pPath.Replace(Server.MapPath(Album.Root), "").Replace("\\", "/");

			try
			{
				// 刪除縮圖目錄
				Directory.Delete(lb_path.Text + "\\_thumb");

				Directory.Delete(lb_path.Text);
				if (Directory.Exists(lb_path.Text))
					mErr = "目錄無法刪除！\\n";
			}
			catch
			{
				mErr = "目錄無法刪除！\\n可能還有子目錄或檔案\\n";
			}
		}
		else
			mErr = "目錄已經不存在!\\n";

		if (mErr == "")
		{
			lt_show.Text = "<script language=\"javascript\">alert(\"目錄「" + lb_al_name.Text + "]」已經刪除!\\n\");parent.location.replace(\"3002.aspx?fl_url=" + Server.UrlEncode(dcode.EnCode(pPath)) + "\");</script>";
		}
		else
			lt_show.Text = "<script language=\"javascript\">alert(\"" + mErr + "\\n\");parent.clean_win();</script>";
	}
}
