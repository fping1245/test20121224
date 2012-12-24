//---------------------------------------------------------------------------- 
//程式功能	驗證函數模組 > 伺服器位址(網域名稱)驗證
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _4004a : System.Web.UI.Page
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

	// 伺服器位址(網域名稱)驗證
	protected void bn_TopLevelDomain_Click(object sender, EventArgs e)
	{
		Check_Internet cknet = new Check_Internet();
		int ckint = -1;

		tb_TopLevelDomain.Text = tb_TopLevelDomain.Text.Trim();

		ckint = cknet.Check_TopLevelDomain(tb_TopLevelDomain.Text);
		if (ckint == 0)
		    lb_TopLevelDomain.Text = "正確";
		else
		    lb_TopLevelDomain.Text = "錯誤 (錯誤代碼：" + ckint.ToString() + ")";
	}
}
