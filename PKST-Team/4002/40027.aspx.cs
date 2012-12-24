//---------------------------------------------------------------------------- 
//程式功能	曆法函數模組 > 以西元日期時間換算農曆日期時間
//---------------------------------------------------------------------------- 
using System;

public partial class _40027: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("4002", false);

			tb_GetLunarDate_datetime.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
			tb_GetLunarDate_format.Text = "yMdhms";
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

	// 以西元日期時間換算農曆日期時間
	protected void bn_GetLunarDate_Click(object sender, EventArgs e)
	{
		Calendar_Func dfc = new Calendar_Func();

		DateTime cktime = DateTime.Now;

		if (! DateTime.TryParse(tb_GetLunarDate_datetime.Text, out cktime))
		{
			cktime = DateTime.Now;
			tb_GetLunarDate_datetime.Text = cktime.ToString("yyyy/MM/dd HH:mm:ss");
		}

		lb_GetLunarDate.Text = dfc.GetLunarDate(cktime, tb_GetLunarDate_format.Text);
	}
}
