//---------------------------------------------------------------------------- 
//程式功能	圖文驗證模組範例
//---------------------------------------------------------------------------- 
using System;

public partial class _1002 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("1002", true);

			//取得隨機產生四碼的驗證數字，並存入 Session
			Session["confirm"] = getConfirmCode();
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

	//隨機產生四碼的驗證數字 (範例圖檔僅有 0 ~ 9 的數字圖檔，故僅產生 0 ~ 9 的驗證數字)
	public string getConfirmCode()
	{
		Random rnd;
		int cnt = 0;
		string confirm = "";

		rnd = new Random(((int)DateTime.Now.Ticks));

		//隨機產生四碼的驗證數字
		for (cnt = 0; cnt < 4; cnt++)
		{
			confirm = confirm + rnd.Next(10).ToString();
		}

		return confirm;
	}

	protected void bn_ok_Click(object sender, EventArgs e)
	{
		lb_msg.Visible = true;
		if (tb_pw.Text.Trim() == Session["confirm"].ToString())
			lb_msg.Text = "[ 輸入正確 ]";
		else if (tb_pw.Text.Trim() != "")
			lb_msg.Text = "[ 輸入錯誤 ]<br>正確答案是 [" + Session["confirm"].ToString() + "]";

	}

	protected void bn_rebuild_Click(object sender, EventArgs e)
	{
		Session["confirm"] = getConfirmCode();
		lb_msg.Visible = false;
		lb_msg.Text = "";
		tb_pw.Text = "";
		img_pw.ImageUrl = "10021.ashx?timestamp=" + DateTime.Now.ToString("mmssms");
		img_text.ImageUrl = "10022.ashx?timestamp=" + DateTime.Now.ToString("mmssms");
	}
}
