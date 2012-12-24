//---------------------------------------------------------------------------- 
//程式功能	驗證函數模組 > 驗證電子郵件信箱
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _40048 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		// 檢查使用者權限但不存入登入紀錄
		//Check_Power("4004", false);
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

	// 驗證電子郵件信箱
	protected void bn_Email_Click(object sender, EventArgs e)
	{
		Check_Internet cknet = new Check_Internet();
		int ckint = -1;

		tb_Email.Text = tb_Email.Text.Trim();

		ckint = cknet.Check_Email(tb_Email.Text);
		if (ckint == 0)
			lb_Email.Text = "正確";
		else
			lb_Email.Text = "錯誤 (錯誤代碼：" + ckint.ToString() + ")";
	}
}
