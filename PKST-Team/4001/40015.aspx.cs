//---------------------------------------------------------------------------- 
//程式功能	字串函數模組 > 右方填滿字元
//---------------------------------------------------------------------------- 
using System;

public partial class _40015 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		// 檢查使用者權限但不存入登入紀錄
		//Check_Power("4001", false);
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

	// 右方填滿字元
	protected void bn_FillRight_Click(object sender, EventArgs e)
	{
		String_Func sfc = new String_Func();

		int ckint = tb_FillRight_str1.Text.Length;

		if (tb_FillRight_int.Text == "" || !int.TryParse(tb_FillRight_int.Text, out ckint))
			tb_FillRight_int.Text = tb_FillRight_str1.Text.Length.ToString();

		if (tb_FillRight_str2.Text.Trim() == "")
			lb_FillRight.Text = sfc.FillRight(tb_FillRight_str1.Text, ckint);
		else
		{
			tb_FillRight_str2.Text = tb_FillRight_str2.Text.Substring(0, 1);
			lb_FillRight.Text = sfc.FillRight(tb_FillRight_str1.Text, ckint, tb_FillRight_str2.Text);
		}
	}
}
