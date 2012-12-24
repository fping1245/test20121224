//---------------------------------------------------------------------------- 
//程式功能	驗證函數模組 > 驗證台灣營利事業統一編號
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _40044 : System.Web.UI.Page
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

    // 驗證台灣營利事業統一編號
	protected void bn_TW_INV_Click(object sender, EventArgs e)
	{
		Check_ID ckid = new Check_ID();
		int ckint = -1;

		tb_TW_INV.Text = tb_TW_INV.Text.Trim();

		ckint = ckid.Check_TW_INV(tb_TW_INV.Text);
		if (ckint == 0)
			lb_TW_INV.Text = "正確";
		else
			lb_TW_INV.Text = "錯誤 (錯誤代碼：" + ckint.ToString() + ")";
	}
}
