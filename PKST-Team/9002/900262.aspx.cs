//---------------------------------------------------------------------------- 
//程式功能	POP3收信處理 > 查看郵件內容 > 顯示資料
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _900262 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		int ppm_sid = -1, ppa_sid = -1;
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限不存入登入紀錄
			//Check_Power("9002", false);

			if (Request["sid"] == null || Request["ppa_sid"] == null)
				mErr = "參數傳錯誤!\\n";
			else if (int.TryParse(Request["sid"], out ppm_sid) && int.TryParse(Request["ppa_sid"], out ppa_sid))
			{
				lb_ppm_sid.Text = ppm_sid.ToString();
				lb_ppa_sid.Text = ppa_sid.ToString();
			}
			else
				mErr = "參數傳錯誤!\\n";

			if (mErr == "")
			{
				//  由 POP3_Mail 取得 SMTP 伺服器相關資料
				Get_Data();
			}
			else
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");history.go(-1)", true);
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

	//  由 POP3_Mail 取得 SMTP 伺服器相關資料
	private void Get_Data()
	{
		string SqlString = "", str_name = "";
		int ppm_attach = 0;

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				SqlDataReader Sql_Reader;
				Sql_Command.Connection = Sql_Conn;

				#region 取得郵件資料
				Sql_Conn.Open();

				SqlString = "Select Top 1 ppm_id, ppm_sn, ppm_subject, ppm_size, r_name, r_email, r_time, s_name, s_email, s_time";
				SqlString += ", ppm_content, ppm_type, ppm_attach, init_time From POP3_Mail Where ppa_sid = @ppa_sid And ppm_sid = @ppm_sid";

				Sql_Command.CommandText = SqlString;
				Sql_Command.Parameters.Clear();
				Sql_Command.Parameters.AddWithValue("ppm_sid", lb_ppm_sid.Text);
				Sql_Command.Parameters.AddWithValue("ppa_sid", lb_ppa_sid.Text);

				Sql_Reader = Sql_Command.ExecuteReader();

				if (Sql_Reader.Read())
				{
					lb_ppm_sn.Text = Sql_Reader["ppm_sn"].ToString();
					lb_ppm_id.Text = Sql_Reader["ppm_id"].ToString().Trim();
					lb_ppm_size.Text = int.Parse(Sql_Reader["ppm_size"].ToString()).ToString("N0");
					lb_init_time.Text = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
					lb_r_time.Text = Sql_Reader["r_time"].ToString().Trim();
					lb_s_time.Text = Sql_Reader["s_time"].ToString().Trim();
					ppm_attach = int.Parse(Sql_Reader["ppm_attach"].ToString());

					str_name = Sql_Reader["r_name"].ToString().Trim();
					lb_r_name.Text = Sql_Reader["r_email"].ToString().Trim();
					if (lb_r_name.Text == str_name)
						lb_r_name.Text = "<a href=\"mailto:" + lb_r_name.Text + "\">[" + lb_r_name.Text + "]</a>";
					else
						lb_r_name.Text = "<a href=\"mailto:" + lb_r_name.Text + "\">" + str_name + "&nbsp;[" + lb_r_name.Text + "]</a>";

					str_name = Sql_Reader["s_name"].ToString().Trim();
					lb_s_name.Text = Sql_Reader["s_email"].ToString().Trim();
					if (lb_s_name.Text == str_name)
						lb_s_name.Text = "<a href=\"mailto:" + lb_s_name.Text + "\">[" + lb_s_name.Text + "]</a>";
					else
						lb_s_name.Text = "<a href=\"mailto:" + lb_s_name.Text + "\">" + str_name + "&nbsp;[" + lb_s_name.Text + "]</a>";

					tb_ppm_subject.Text = Sql_Reader["ppm_subject"].ToString().Trim();
					tb_ppm_content.Text = Sql_Reader["ppm_content"].ToString().Trim();

					lb_iframe.Text = "9002621.ashx?sid=" + lb_ppm_sid.Text + "&ppa_sid=" + lb_ppa_sid.Text + "&ppm_type=" + Sql_Reader["ppm_type"].ToString();
				}
				Sql_Reader.Close();

				Sql_Conn.Close();
				#endregion

				#region 附加檔案處理
				lt_attach.Text = "";

				if (ppm_attach > 0)
				{
					Sql_Conn.Open();

					SqlString = "Select ppt_sid, ppt_name From POP3_Attach Where ppa_sid = @ppa_sid And ppm_sid = @ppm_sid";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("ppm_sid", lb_ppm_sid.Text);
					Sql_Command.Parameters.AddWithValue("ppa_sid", lb_ppa_sid.Text);

					Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
					{
						do
						{
							lt_attach.Text += "<a href=\"9002622.ashx?sid=" + Sql_Reader["ppt_sid"].ToString()
								+ "&ppa_sid=" + lb_ppa_sid.Text + "&ppm_sid=" + lb_ppm_sid.Text + "\" target=\"_blank\">"
								+ "<img src=\"../images/button/save.gif\" border=0>&nbsp;" + Sql_Reader["ppt_name"].ToString().Trim() + "</a>&nbsp;&nbsp;";
						} while (Sql_Reader.Read());
					}
					else
						lt_attach.Text = "[無附加檔案]";

					Sql_Reader.Close();
					Sql_Conn.Close();
				}
				else
					lt_attach.Text = "[無附加檔案]";
				#endregion

				Sql_Reader.Dispose();
			}
		}
	}

	// 切換到郵件內文顯示
	protected void lk_body_Click(object sender, EventArgs e)
	{
		mv_content.ActiveViewIndex = 0;
	}

	// 切換到原始資料顯示
	protected void lk_source_Click(object sender, EventArgs e)
	{
		mv_content.ActiveViewIndex = 1;
	}
}
