//---------------------------------------------------------------------------- 
//程式功能	論壇管理 > 修改討論主題
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _D0021 : System.Web.UI.Page
{
	static int ff_sid = 0;
	
    protected void Page_Load(object sender, EventArgs e)
    {
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限不存入登入紀錄
			//Check_Power("D002", false);

			// 檢查傳入參數
			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out ff_sid))
				{
					// 取得資料
					if (! GetData())
						mErr = "找不到指定的資料!\\n";
				}
				else
					mErr = "參數格式錯誤!\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";
		}

		if (mErr != "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");parent.close_all();", true);
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

	// 取得資料
	private bool GetData()
	{
		bool ckbool = false;
		string SqlString = "", tmpstr = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 ff_top, ff_symbol, ff_name, ff_sex, ff_email, ff_time, ff_ip, ff_topic, ff_desc, ff_response";
			SqlString += ", is_show, instead, is_close From Fm_Forum Where ff_sid = @ff_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();

				Sql_Command.Parameters.AddWithValue("ff_sid", ff_sid);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						tb_ff_top.Text = Sql_Reader["ff_top"].ToString().Trim();
						tb_ff_name.Text = Sql_Reader["ff_name"].ToString().Trim();
						tb_ff_email.Text = Sql_Reader["ff_email"].ToString().Trim();
						tb_ff_topic.Text = Sql_Reader["ff_topic"].ToString().Trim();
						tb_ff_desc.Text = Sql_Reader["ff_desc"].ToString().Trim().Replace("<br>","\r\n");
						tb_instead.Text = Sql_Reader["instead"].ToString();
						lb_ff_time.Text = DateTime.Parse(Sql_Reader["ff_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
						lb_ff_ip.Text = Sql_Reader["ff_ip"].ToString();
						lb_response.Text = Sql_Reader["ff_response"].ToString();

						tmpstr = "rb_ff_symbol" + Sql_Reader["ff_symbol"].ToString().PadLeft(2,'0');
						RadioButton ff_symbol = (RadioButton)Page.FindControl(tmpstr);
						if (ff_symbol != null)
							ff_symbol.Checked = true;

						if (Sql_Reader["ff_sex"].ToString() == "1")
						{
							rb_ff_sex1.Checked = true;
							rb_ff_sex2.Checked = false;
						}
						else
						{
							rb_ff_sex1.Checked = false;
							rb_ff_sex2.Checked = true;
						}

						if (Sql_Reader["is_show"].ToString() == "0")
						{
							rb_is_show0.Checked = true;
							rb_is_show1.Checked = false;
						}
						else
						{
							rb_is_show0.Checked = false;
							rb_is_show1.Checked = true;
						}

						if (Sql_Reader["is_close"].ToString() == "0")
						{
							rb_is_close0.Checked = true;
							rb_is_close1.Checked = false;
						}
						else
						{
							rb_is_close0.Checked = false;
							rb_is_close1.Checked = true;
						}

						ckbool = true;
					}

					Sql_Reader.Close();
				}

				Sql_Conn.Close();
			}
		}

		return ckbool;
	}

	// 存檔
	protected void lk_save_Click(object sender, EventArgs e)
	{
		string mErr = "", SqlString = "", tmpstr = "";
		int ff_sex = 0, ff_symbol = 0, icnt = 0, is_show = 0, is_close = 0, ff_top = 0;
		Check_Internet cki = new Check_Internet();

		tb_ff_top.Text = tb_ff_top.Text.Trim();
		if (int.TryParse(tb_ff_top.Text, out ff_top))
		{
			if (ff_top < 0 || ff_top > 255)
				mErr = "「優先順序」請輸入 0 ~ 255 的數字，0:普通；255:置頂，數字越大越上面!\\n";
		}
		else
		{
			mErr = "「優先順序」請輸入 0 ~ 255 的數字，0:普通；255:置頂，數字越大越上面!\\n";
			tb_ff_top.Text = "0";
		}

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
		if (tb_ff_topic.Text.Length < 2)
			mErr += "請輸入正確的「主題」文字!\\n";

		tb_ff_desc.Text = tb_ff_desc.Text.Trim();
		if (tb_ff_desc.Text.Length < 4)
			mErr += "請輸入正確的「內容」文字!\\n";

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

		tb_instead.Text = tb_instead.Text.Trim();
		if (rb_is_show0.Checked)
		{
			is_show = 0;
			if (tb_instead.Text == "")
				mErr = "請輸入「替代文字」!\\n";
		}
		else
			is_show = 1;

		if (rb_is_close0.Checked)
			is_close = 0;
		else
			is_close = 1;

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				SqlString = "Update Fm_Forum Set ff_top = @ff_top, ff_symbol = @ff_symbol, ff_name = @ff_name, ff_sex = @ff_sex";
				SqlString += ", ff_email = @ff_email, ff_topic = @ff_topic, ff_desc = @ff_desc, is_show = @is_show";
				SqlString += ", instead = @instead, is_close = @is_close Where ff_sid = @ff_sid;";

				using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
				{
					Sql_Conn.Open();

					Sql_Command.Parameters.AddWithValue("ff_sid", ff_sid);
					Sql_Command.Parameters.AddWithValue("ff_top", ff_top);
					Sql_Command.Parameters.AddWithValue("ff_symbol", ff_symbol);
					Sql_Command.Parameters.AddWithValue("ff_name", tb_ff_name.Text);
					Sql_Command.Parameters.AddWithValue("ff_sex", ff_sex);
					Sql_Command.Parameters.AddWithValue("ff_email", tb_ff_email.Text);
					Sql_Command.Parameters.AddWithValue("ff_topic", tb_ff_topic.Text);
					Sql_Command.Parameters.AddWithValue("ff_desc", tb_ff_desc.Text.Replace("\r\n","<br>"));
					Sql_Command.Parameters.AddWithValue("is_show", is_show);
					Sql_Command.Parameters.AddWithValue("instead", tb_instead.Text);
					Sql_Command.Parameters.AddWithValue("is_close", is_close);

					Sql_Command.ExecuteNonQuery();
				}
			}
		}

		if (mErr == "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "parent.location.reload(true);", true);
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	// 填入 instead 資料
	protected void rb_is_show0_CheckedChanged(object sender, EventArgs e)
	{
		if (rb_is_show0.Checked)
		{
			tb_instead.Text = tb_instead.Text.Trim();
			if (tb_instead.Text == "")
				tb_instead.Text = "發言不當，禁止顯示。";
		}
	}
}
