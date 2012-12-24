//---------------------------------------------------------------------------- 
//程式功能	論壇前端 > 主題回應列表 > 回應討論主題
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _D00121 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int ff_sid = 0;
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限不存入登入紀錄
			//Check_Power("D001", false);

			// 產生驗證碼
			Session["D001"] = getConfirmCode();
			img_confirm.ImageUrl = "D00111.ashx?timestamp=" + DateTime.Now.ToString("mmssms");
		}

		if (Request["sid"] != null)
		{
			if (int.TryParse(Request["sid"], out ff_sid))
			{
				lb_ff_sid.Text = ff_sid.ToString();

				// 取得抬頭資料
				mErr = GetData();
			}
			else
				mErr = "參數格式錯誤!\\n";
		}
		else
			mErr = "參數傳送錯誤!\\n";

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
	private string GetData()
	{
		string mErr = "";

		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 ff_symbol, ff_name, ff_sex, ff_email, ff_time, ff_ip, ff_topic, ff_desc, is_show, is_close";
			SqlString += " From Fm_Forum Where ff_sid = @ff_sid And is_show = 1 And is_close = 1";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("ff_sid", lb_ff_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						if (Sql_Reader["is_show"].ToString() == "0")
							mErr = "本項討論已「隱藏」，不允許進入!\\n";
						else if (Sql_Reader["is_close"].ToString() == "0")
							mErr = "本項討論已「關閉」，不允許進入!\\n";

						if (mErr == "")
						{
							if (Sql_Reader["ff_sex"].ToString() == "1")
							{
								img_ff_sex.ImageUrl = "~/images/symbol/man.gif";
								img_ff_sex.ToolTip = "男性";
								img_ff_sex.AlternateText = "男性";
							}
							else
							{
								img_ff_sex.ImageUrl = "~/images/symbol/woman.gif";
								img_ff_sex.ToolTip = "女性";
								img_ff_sex.AlternateText = "女性";
							}

							#region 心情符號處理
							Common_Func.ImageSymbol img_symbo = new Common_Func.ImageSymbol();

							img_symbo.code = int.Parse(Sql_Reader["ff_symbol"].ToString());
							img_ff_symbol.ImageUrl = img_symbo.image;
							img_ff_symbol.ToolTip = img_symbo.name;
							img_ff_symbol.AlternateText = img_symbo.name;
							#endregion

							lb_ff_topic.Text = Sql_Reader["ff_topic"].ToString();
							lb_ff_name.Text = Sql_Reader["ff_name"].ToString();
							lt_ff_email.Text = "<a href=\"mailto:" + Sql_Reader["ff_email"].ToString()
								+ "\">" + Sql_Reader["ff_email"].ToString() + "</a>";
							lb_ff_time.Text = DateTime.Parse(Sql_Reader["ff_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss")
								+ " .. ( IP:" + Sql_Reader["ff_ip"].ToString() + " )";
							lb_ff_desc.Text = Sql_Reader["ff_desc"].ToString();
						}
					}
					else
						mErr = "找不到指定的討論主題!\\n";
				}
			}
		}
		return mErr;
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
		int fr_sex = 0, fr_symbol = 0, icnt = 0;
		Check_Internet cki = new Check_Internet();

		tb_fr_name.Text = tb_fr_name.Text.Trim();
		if (tb_fr_name.Text.Length < 2)
			mErr += "請輸入「姓名」!\\n";

		if (rb_fr_sex1.Checked || rb_fr_sex2.Checked)
		{
			if (rb_fr_sex1.Checked)
				fr_sex = 1;
			else
				fr_sex = 2;
		}
		else
			mErr += "請選擇「性別」!\\n";

		tb_fr_email.Text = tb_fr_email.Text.Trim();
		if (cki.Check_Email(tb_fr_email.Text) != 0)
			mErr += "請輸入正確的「E-Mail」格式!\\n";

		tb_fr_desc.Text = tb_fr_desc.Text.Trim();
		if (tb_fr_desc.Text.Length < 2)
			mErr += "請輸入正確的「回應內容」文字!\\n";

		if (tb_confirm.Text.Trim() != Session["D001"].ToString())
		{
			mErr += "驗證碼輸入錯誤！\\n";
			tb_confirm.Text = "";

			// 重新產生驗證碼
			Session["D001"] = getConfirmCode();
		}			

		// 心情符號
		for (icnt = 0; icnt < 18; icnt++)
		{
			tmpstr = "rb_fr_symbol" + icnt.ToString().PadLeft(2, '0');

			RadioButton rb_tmp = (RadioButton)Page.FindControl(tmpstr);
			if (rb_tmp != null)
			{
				if (rb_tmp.Checked)
					fr_symbol = icnt;
			}
		}

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{


				using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
				{
					Sql_Command.Connection = Sql_Conn;

					#region 儲存資料
					Sql_Conn.Open();

					SqlString = "Insert Into Fm_Response (ff_sid, fr_symbol, fr_name, fr_sex, fr_email, fr_ip, fr_desc)";
					SqlString += " Values (@ff_sid, @fr_symbol, @fr_name, @fr_sex, @fr_email, @fr_ip, @fr_desc);";

					Sql_Command.CommandText = SqlString;

					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("ff_sid", lb_ff_sid.Text);
					Sql_Command.Parameters.AddWithValue("fr_symbol", fr_symbol);
					Sql_Command.Parameters.AddWithValue("fr_name", tb_fr_name.Text);
					Sql_Command.Parameters.AddWithValue("fr_sex", fr_sex);
					Sql_Command.Parameters.AddWithValue("fr_email", tb_fr_email.Text);
					Sql_Command.Parameters.AddWithValue("fr_ip", Request.ServerVariables["REMOTE_ADDR"]);
					Sql_Command.Parameters.AddWithValue("fr_desc", tb_fr_desc.Text.Replace("\r\n","<br>"));

					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();
					#endregion

					#region 重新統計回應篇數
					Sql_Conn.Open();

					SqlString = "Update Fm_Forum Set ff_response = (Select Count(*) From Fm_Response Where ff_sid = @ff_sid And is_close = 1)";
					SqlString += " Where ff_sid = @ff_sid";

					Sql_Command.CommandText = SqlString;

					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("ff_sid", lb_ff_sid.Text);

					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();
					#endregion
				}
			}
		}

		if (mErr == "")
		{
			// 移除 Session
			Session.Contents.Remove("D001");

			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "parent.location.reload(true);", true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}
}
