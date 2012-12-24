//---------------------------------------------------------------------------- 
//程式功能	廣告信發送管理 (廣告信選單) > 編輯郵件
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _90011 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			string mErr = "";
			int adm_sid = 0, ckint = 0;

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("9001", false);

			#region 判斷修改或是新增的狀況
			if (Request["md"] != null)
			{
				if (Request["md"] == "a" || Request["md"] == "e") {

					lb_md.Text = Request["md"];

					// 修改
					if (Request["md"] == "e")
					{
						lb_title.Text = "修改郵件";

						if (Request["sid"] == null)
						{
							mErr = "「郵件編號」傳入錯誤!\\n";
						}
						else
						{
							if (int.TryParse(Request["sid"], out adm_sid))
							{
								lb_adm_sid.Text = adm_sid.ToString();
								lt_sadm_sid.Text = lb_adm_sid.Text;

								// 取得原郵件資料
								if (!Get_Data())
								{
									mErr = "找不到指定的郵件資料!\\n";
								}
							}
						}
					}
					else
					{
						lb_title.Text = "新增郵件";
						lb_adm_sid.Text = "0";
						lt_sadm_sid.Text = "新增";
					}

					#region 承接上一頁的查詢條件設定
					if (mErr == "")
					{
						if (Request["pageid"] != null)
						{
							if (int.TryParse(Request["pageid"].ToString(), out ckint))
							{
								lb_page.Text = "?pageid=" + ckint.ToString();
							}
							else
							{
								lb_page.Text = "?pageid=0";
							}
						}

						if (Request["adm_sid"] != null)
							lb_page.Text += "&adm_sid=" + Server.UrlEncode(Request["adm_sid"]);

						if (Request["adm_title"] != null)
							lb_page.Text += "&adm_title=" + Server.UrlEncode(Request["adm_title"]);

						if (Request["adm_fname"] != null)
							lb_page.Text += "&adm_fname=" + Server.UrlEncode(Request["adm_fname"]);

						if (Request["adm_fmail"] != null)
							lb_page.Text += "&adm_fmail=" + Server.UrlEncode(Request["adm_fmail"]);

						if (Request["btime"] != null)
							lb_page.Text += "&btime=" + Server.UrlEncode(Request["btime"]);

						if (Request["etime"] != null)
							lb_page.Text += "&etime=" + Server.UrlEncode(Request["etime"]);
					}
					#endregion
				}
				else
					mErr = "參數型態有誤!\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";
			#endregion

			if (mErr != "")
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");history.go(-1);", true);
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

	// Get_Data() 取得郵件資料
	private bool Get_Data()
	{
		bool blck = false;
		string SqlString = "";
		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				Sql_Conn.Open();

				SqlString = "Select Top 1 adm_title, adm_content, adm_fname, adm_fmail, adm_type From Ad_Mail Where adm_sid = @adm_sid";

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;

				Sql_Command.Parameters.AddWithValue("adm_sid", lb_adm_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						tb_adm_title.Text = Sql_Reader["adm_title"].ToString().Trim();
						tb_adm_fname.Text = Sql_Reader["adm_fname"].ToString().Trim();
						tb_adm_fmail.Text = Sql_Reader["adm_fmail"].ToString().Trim();
						tb_preview.Text = Sql_Reader["adm_content"].ToString().Trim();

						if (Sql_Reader["adm_type"].ToString() == "1")
							rb_adm_type_1.Checked = true;
						else
							rb_adm_type_2.Checked = true;

						blck = true;
					}
					else
					{
						blck = false;
					}
					Sql_Reader.Close();
				}

				Sql_Conn.Close();
			}
		}
		return blck;
	}

	// 檔案存檔
	protected void bn_save_Click(object sender, EventArgs e)
	{
		Check_Internet cknet = new Check_Internet();
		string mErr = "", adm_title = "", adm_content = "", adm_fname = "", adm_fmail = "", SqlString = "";

		adm_title = tb_adm_title.Text.Trim();
		adm_fname = tb_adm_fname.Text.Trim();
		adm_fmail = tb_adm_fmail.Text.Trim();
		adm_content = tb_preview.Text.Trim();

		#region 檢查輸入的資料
		if (adm_title == "" || adm_title == "_none")
			mErr += "請填寫郵件標題!\\n";
		
		if (adm_fmail == "")
			mErr += "請填寫發信者郵箱\\n";
		else if (cknet.Check_Email(adm_fmail) != 0)
			mErr += "發信者郵箱格式錯誤\\n";
		
		if (adm_content == "")
			mErr += "沒有郵件內容!\\n";
		#endregion

		if (mErr == "")
		{
			if (lb_md.Text == "a" && lb_adm_sid.Text == "0")
			{
				#region 新增資料
				SqlString = "Insert Into Ad_Mail (adm_title, adm_content, adm_fname, adm_fmail, adm_type)";
				SqlString += " Values (@adm_title, @adm_content, @adm_fname, @adm_fmail, @adm_type)";

				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
					{
						Sql_Conn.Open();

						Sql_Command.Parameters.AddWithValue("adm_title", adm_title);
						Sql_Command.Parameters.AddWithValue("adm_content", adm_content);
						Sql_Command.Parameters.AddWithValue("adm_fname", adm_fname);
						Sql_Command.Parameters.AddWithValue("adm_fmail", adm_fmail);

						if (rb_adm_type_1.Checked)
							Sql_Command.Parameters.AddWithValue("adm_type", 1);
						else
							Sql_Command.Parameters.AddWithValue("adm_type", 2);

						Sql_Command.ExecuteNonQuery();

						Sql_Conn.Close();
					}
				}
				#endregion
			}
			else
			{
				#region 修改資料
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						Sql_Conn.Open();

						Sql_Command.Connection = Sql_Conn;

						// 檢查編號
						SqlString = "Select Top 1 adm_sid From Ad_Mail Where adm_sid = @adm_sid";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("adm_sid", lb_adm_sid.Text);

						using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
						{
							if (Sql_Reader.Read())
							{
								if (Sql_Reader["adm_sid"].ToString() != lb_adm_sid.Text)	// 有可能用 SQL 隱碼或其它方式進入
									mErr = "請使用正確方式處理!\\n";
							}
							else
								mErr = "找不到指定的郵件編號!\\n";

							Sql_Reader.Close();
						}

						if (mErr == "")
						{
							// 更新資料 (套用 Sql 的 p_Ck_Html_Files 函數檢查是否有附加檔案)
							SqlString = "Update Ad_Mail Set adm_title = @adm_title, adm_content = @adm_content,  adm_fmail = @adm_fmail";
							SqlString += ", adm_fname = @adm_fname, adm_type = @adm_type, init_time = getdate() Where adm_sid = @adm_sid;";

							Sql_Command.Parameters.Clear();
							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.AddWithValue("adm_sid", lb_adm_sid.Text);
							Sql_Command.Parameters.AddWithValue("adm_title", adm_title);
							Sql_Command.Parameters.AddWithValue("adm_content", adm_content);
							Sql_Command.Parameters.AddWithValue("adm_fname", adm_fname);
							Sql_Command.Parameters.AddWithValue("adm_fmail", adm_fmail);

							if (rb_adm_type_1.Checked)
								Sql_Command.Parameters.AddWithValue("adm_type", 1);
							else
								Sql_Command.Parameters.AddWithValue("adm_type", 2);

							Sql_Command.ExecuteNonQuery();
						}

						Sql_Conn.Close();
					}
				}
				#endregion
			}
		}

		if (mErr == "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"郵件儲存完成!\\n\");location.replace(\"9001.aspx" + lb_page.Text + "\");", true);
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	// 不存檔結束離開
	protected void bn_leave_Click(object sender, EventArgs e)
	{
		Response.Redirect("9001.aspx" + lb_page.Text);
	}
}
