//---------------------------------------------------------------------------- 
//程式功能	使用者登入紀錄查詢
//---------------------------------------------------------------------------- 
using System;

public partial class _1008 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("1008", true);
		}
	}

	// Check_Power() 檢查使用者權限並存入登入紀錄
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

	protected void Btn_Set_Click(object sender, EventArgs e)
	{
		// 檢查查詢條件是否改變
		Chk_Filter();
	}

	// 檢查查詢條件是否改變
	private void Chk_Filter()
	{
		Common_Func cfc = new Common_Func();
		DateTime cktime;
		int ckint;

		if (! DateTime.TryParse(tb_btime.Text, out cktime))
			tb_btime.Text = "";
		ods_Mg_Log.SelectParameters["btime"].DefaultValue = tb_btime.Text;
			
		if (! DateTime.TryParse(tb_etime.Text, out cktime))
			tb_etime.Text = "";
		ods_Mg_Log.SelectParameters["etime"].DefaultValue = tb_etime.Text;

		if (! int.TryParse(tb_mg_sid.Text,out ckint))
			tb_mg_sid.Text = "";
		ods_Mg_Log.SelectParameters["mg_sid"].DefaultValue = tb_mg_sid.Text;

		tb_mg_name.Text = cfc.CleanSQL(tb_mg_name.Text);
		ods_Mg_Log.SelectParameters["mg_name"].DefaultValue = tb_mg_name.Text;

		tb_fi_name1.Text = cfc.CleanSQL(tb_fi_name1.Text);
		ods_Mg_Log.SelectParameters["fi_name1"].DefaultValue = tb_fi_name1.Text;

		tb_fi_name2.Text = cfc.CleanSQL(tb_fi_name2.Text);
		ods_Mg_Log.SelectParameters["fi_name2"].DefaultValue = tb_fi_name2.Text;

		tb_lg_ip.Text = cfc.CleanSQL(tb_lg_ip.Text);
		ods_Mg_Log.SelectParameters["lg_ip"].DefaultValue = tb_lg_ip.Text;

		gv_Mg_Log.DataBind();
		if (gv_Mg_Log.PageCount - 1 < gv_Mg_Log.PageIndex)
		{
			gv_Mg_Log.PageIndex = gv_Mg_Log.PageCount;
			gv_Mg_Log.DataBind();
		}
	}
}
