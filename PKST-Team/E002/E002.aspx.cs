using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _E002 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("E002", true);
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

	protected void bn_togb_Click(object sender, EventArgs e)
	{
		Literal txtMsg = new Literal();
		// Session["CodePage"] = "936";
		HttpCookie cookie = new HttpCookie("CodePage");
		cookie["Code"] = "936";
		cookie.Expires = DateTime.Now.AddDays(1);
		Response.Cookies.Add(cookie);

		txtMsg.Text = "<script language=javascript>frame_reload();</script>";
		Page.Controls.Add(txtMsg);
	}

	protected void bn_tobig5_Click(object sender, EventArgs e)
	{
		Literal txtMsg = new Literal();
		// Session["CodePage"] = "950";

		HttpCookie cookie = new HttpCookie("CodePage");
		cookie["Code"] = "950";
		cookie.Expires = DateTime.Now.AddDays(1);
		Response.Cookies.Add(cookie);

		txtMsg.Text = "<script language=javascript>frame_reload();</script>";
		Page.Controls.Add(txtMsg);
	}
}
