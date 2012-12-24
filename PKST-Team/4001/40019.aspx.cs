//---------------------------------------------------------------------------- 
//程式功能	字串函數模組 > 取得每四位數的中文位數字
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _40019 : System.Web.UI.Page
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

	// 取得每四位數的中文位數字
	protected void bn_GetFourChNumber_Click(object sender, EventArgs e)
	{
		String_Func sfc = new String_Func();

		int ckint = 0;

		int.TryParse(tb_GetFourChNumber_int.Text, out ckint);

		lb_GetFourChNumber.Text = sfc.GetFourChNumber(ckint);
	}
}
