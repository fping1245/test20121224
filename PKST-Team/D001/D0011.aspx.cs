//---------------------------------------------------------------------------- 
//程式功能	論壇前端 > 發表主題
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _D0011 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限不存入登入紀錄
			//Check_Power("D001", false);

			// 產生驗證碼
			Session["D001"] = getConfirmCode();
			img_confirm.ImageUrl = "D00111.ashx?timestamp=" + DateTime.Now.ToString("mmssms");
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

	// 隨機產生四碼的驗證數字 (範例圖檔僅有 0 ~ 9 的數字圖檔，故僅產生 0 ~ 9 的驗證數字)
	public string getConfirmCode()
	{
		Random rnd;
		int cnt = 0;
		string confirm = "";

		rnd = new Random(((int)DateTime.Now.Ticks));

		// 隨機產生四碼的驗證數字
		for (cnt = 0; cnt < 4; cnt++)
		{
			confirm = confirm + rnd.Next(10).ToString();
		}

		return confirm;
	}

	// 重新產生驗證碼
	protected void bn_new_confirm_Click(object sender, EventArgs e)
	{
		// 產生驗證碼
		Session["D001"] = getConfirmCode();

		img_confirm.ImageUrl = "D00111.ashx?ti=" + DateTime.Now.ToString("HHmmss");
	}

	// 存檔
	protected void lk_save_Click(object sender, EventArgs e)
	{
		string mErr = "", SqlString = "", tmpstr = "";
		int ff_sex = 0, ff_symbol = 0, icnt = 0;
		Check_Internet cki = new Check_Internet();

		tb_ff_name.Text = tb_ff_name.Text.Trim();
		if (tb_ff_name.Text.Length < 2)
			mErr += "請輸入「姓名」!\\n";

		if (rb_ff_sex1.Checked || rb_ff_sex2.Checked)
		{
			if (rb_ff_sex1.Checked)
				ff_sex = 1;
			else
				ff_sex = 2;
		}
		else
			mErr += "請選擇「性別」!\\n";

		tb_ff_email.Text = tb_ff_email.Text.Trim();
		if (cki.Check_Email(tb_ff_email.Text) != 0)
			mErr += "請輸入正確的「E-Mail」格式!\\n";

		tb_ff_topic.Text = tb_ff_topic.Text.Trim();
		if (tb_ff_topic.Text.Length < 4)
			mErr += "請輸入正確的「討論主題」文字!\\n";

		tb_ff_desc.Text = tb_ff_desc.Text.Trim();
		if (tb_ff_desc.Text.Length < 2)
			mErr += "請輸入正確的「主題內容」文字!\\n";

		tb_confirm.Text = tb_confirm.Text.Trim();
		if (tb_confirm.Text != Session["D001"].ToString())
		{
			mErr += "驗證碼輸入錯誤！\\n";
			tb_confirm.Text = "";

			// 重新產生驗證碼
			Session["D001"] = getConfirmCode();
		}

		// 心情符號
		for (icnt = 0; icnt < 18; icnt++)
		{
			tmpstr = "rb_ff_symbol" + icnt.ToString().PadLeft(2, '0');

			RadioButton rb_tmp = (RadioButton)Page.FindControl(tmpstr);
			if (rb_tmp != null)
			{
				if (rb_tmp.Checked)
					ff_symbol = icnt;
			}
		}

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				SqlString = "Insert Into Fm_Forum (ff_symbol, ff_name, ff_sex, ff_email, ff_ip, ff_topic, ff_desc)";
				SqlString += " Values (@ff_symbol, @ff_name, @ff_sex, @ff_email, @ff_ip, @ff_topic, @ff_desc);";

				using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
				{
					Sql_Conn.Open();

					Sql_Command.Parameters.AddWithValue("ff_symbol", ff_symbol);
					Sql_Command.Parameters.AddWithValue("ff_name", tb_ff_name.Text);
					Sql_Command.Parameters.AddWithValue("ff_sex", ff_sex);
					Sql_Command.Parameters.AddWithValue("ff_email", tb_ff_email.Text);
					Sql_Command.Parameters.AddWithValue("ff_ip", Request.ServerVariables["REMOTE_ADDR"]);
					Sql_Command.Parameters.AddWithValue("ff_topic", tb_ff_topic.Text);
					Sql_Command.Parameters.AddWithValue("ff_desc", tb_ff_desc.Text.Replace("\r\n", "<br>"));

					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();
				}
			}
		}

		if (mErr == "")
		{
			// 移除 Session
			Session.Contents.Remove("D001");

			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "parent.location.replace(\"D001.aspx\");", true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}
}
