//---------------------------------------------------------------------------- 
//程式功能	字串函數模組 > 擷取右側字元
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _40013 : System.Web.UI.Page
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

	// 擷取右方字元
	protected void bn_Right_Click(object sender, EventArgs e)
	{
		String_Func sfc = new String_Func();

		int ckint = 1;

		if (tb_Right_int.Text == "" || !int.TryParse(tb_Right_int.Text, out ckint))
			tb_Right_int.Text = "1";

		lb_Right.Text = sfc.Right(tb_Right_str.Text, ckint);
	}
}
