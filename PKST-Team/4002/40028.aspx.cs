//---------------------------------------------------------------------------- 
//程式功能	曆法函數模組 > 以西元日期取得農曆生肖
//---------------------------------------------------------------------------- 
using System;

public partial class _40028: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("4002", false);

			tb_GetDateLunarZodiac.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
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

	// 以西元日期取得農曆生肖
	protected void bn_GetDateLunarZodiac_Click(object sender, EventArgs e)
	{
		Calendar_Func dfc = new Calendar_Func();

		DateTime cktime = DateTime.Now;

		if (!DateTime.TryParse(tb_GetDateLunarZodiac.Text, out cktime))
		{
			cktime = DateTime.Now;
			tb_GetDateLunarZodiac.Text = cktime.ToString("yyyy/MM/dd HH:mm:ss");
		}

		lb_GetDateLunarZodiac.Text = dfc.GetDateLunarZodiac(cktime);
	}
}
