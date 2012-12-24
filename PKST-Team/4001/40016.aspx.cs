//---------------------------------------------------------------------------- 
//程式功能	字串函數模組 > 產生重覆字串
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _40016 : System.Web.UI.Page
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

	// 產生重複字串
	protected void bn_Dulicate_Click(object sender, EventArgs e)
	{
		String_Func sfc = new String_Func();

		int ckint = 1;

		if (tb_Dulicate_int.Text == "" || !int.TryParse(tb_Dulicate_int.Text, out ckint))
			tb_Dulicate_int.Text = "1";

		lb_Dulicate.Text = sfc.Duplicate(tb_Dulicate_str.Text, ckint);
	}
}
