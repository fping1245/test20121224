//---------------------------------------------------------------------------- 
//程式功能	相簿管理
//備註說明	使用實體路徑儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.IO;

public partial class _3002 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		Decoder dcode = new Decoder();

		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("3002", true);

			if (Request["fl_url"] != null)
			{
				lb_fl_url.Text =  dcode.DeCode(Request["fl_url"].Trim());
				lb_fl_url_encode.Text = Server.UrlEncode(dcode.EnCode(lb_fl_url.Text));
				lb_path.Text = Server.MapPath(lb_fl_url.Text);

				if (lb_fl_url.Text == Album.Root)
				{
					lb_show_path.Text = "根目錄";
				}
				else
				{
					// 僅顯示 Album.Root 以後的目錄名稱
					lb_show_path.Text = lb_fl_url.Text.Replace(Album.Root, "");

					// 檢查目錄是否存在
					if (! Directory.Exists(lb_path.Text))
						lt_show.Text = "<script language=javascript>alert(\"找不到指定的路徑\\n\");location.replace(\"3002.aspx\");</script>";
				}
			}
			else
			{
				lb_fl_url.Text = Album.Root;
				lb_fl_url_encode.Text = Server.UrlEncode(dcode.EnCode(lb_fl_url.Text));
				lb_path.Text = Server.MapPath(lb_fl_url.Text);
				lb_show_path.Text = "根目錄";
			}
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

	// 回到根目錄
	protected void bn_go_root_Click(object sender, EventArgs e)
	{
		Response.Redirect("3002.aspx");
	}
}
