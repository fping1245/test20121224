//---------------------------------------------------------------------------- 
//程式功能	曆法函數模組 > 以小時對映農曆時辰
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _40026: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("4002", false);

			tb_GetChHour_int.Text = DateTime.Now.Hour.ToString();
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

	// 以小時對映農曆時辰
	protected void bn_GetChHour_Click(object sender, EventArgs e)
	{
		Calendar_Func dfc = new Calendar_Func();

		int ckint = 1;

		int.TryParse(tb_GetChHour_int.Text, out ckint);

		lb_GetChHour.Text = dfc.GetChHour(ckint);
	}
}
