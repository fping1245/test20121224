//---------------------------------------------------------------------------- 
//程式功能	HTML編輯器 (網頁清單) > 編輯頁面
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;

public partial class _80011 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			string mErr = "";
			int he_sid = 0, ckint = 0;

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("8001", false);

			#region 判斷修改或是新增的狀況
			if (Request["md"] != null)
			{
				if (Request["md"] == "a" || Request["md"] == "e") {

					lb_md.Text = Request["md"];

					// 修改
					if (Request["md"] == "e")
					{
						lb_title.Text = "修改網頁";

						if (Request["sid"] == null)
						{
							mErr = "「網頁編號」傳入錯誤!\\n";
						}
						else
						{
							if (int.TryParse(Request["sid"], out he_sid))
							{
								lb_he_sid.Text = he_sid.ToString();
								lt_she_sid.Text = lb_he_sid.Text;

								// 取得原網頁資料
								if (!Get_Data())
								{
									mErr = "找不到指定的網頁資料!\\n";
								}
							}
						}
					}
					else
					{
						lb_title.Text = "新增網頁";
						lb_he_sid.Text = "0";
						lt_she_sid.Text = "新增";
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

						if (Request["he_sid"] != null)
							lb_page.Text += "&he_sid=" + Server.UrlEncode(Request["he_sid"]);

						if (Request["he_title"] != null)
							lb_page.Text += "&he_title=" + Server.UrlEncode(Request["he_title"]);

						if (Request["he_desc"] != null)
							lb_page.Text += "&he_desc=" + Server.UrlEncode(Request["he_desc"]);

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

	// Get_Data() 取得網頁資料
	private bool Get_Data()
	{
		bool blck = false;
		string SqlString = "";
		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				Sql_Conn.Open();

				SqlString = "Select Top 1 he_title, he_content, he_desc From Html_Edit Where he_sid = @he_sid";

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;

				Sql_Command.Parameters.AddWithValue("he_sid", lb_he_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						tb_he_title.Text = Sql_Reader["he_title"].ToString().Trim();
						tb_he_desc.Text = Sql_Reader["he_desc"].ToString().Trim();
						tb_preview.Text = Sql_Reader["he_content"].ToString().Trim();
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
		string mErr = "", he_title = "", he_content = "", he_desc = "", SqlString = "";

		he_title = tb_he_title.Text.Trim();
		he_content = tb_preview.Text.Trim();

		if (he_title == "" || he_title == "_none")
			mErr += "請填寫網頁標題!\\n";
		else if (he_content == "")
		{
			mErr += "沒有網頁內容!\\n";
		}
		
		if (mErr == "")
		{
			// 清除網頁內的資料
			he_content = Clean_Content(he_content);

			he_desc = tb_he_desc.Text.Trim();

			if (lb_md.Text == "a" && lb_he_sid.Text == "0")
			{
				#region 新增資料
				SqlString = "Insert Into Html_Edit (he_title, he_content, he_desc) Values (@he_title, @he_content, @he_desc)";

				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
					{
						Sql_Conn.Open();

						Sql_Command.Parameters.AddWithValue("he_title", he_title);
						Sql_Command.Parameters.AddWithValue("he_content", he_content);
						Sql_Command.Parameters.AddWithValue("he_desc", he_desc);
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
						SqlString = "Select Top 1 he_sid From Html_Edit Where he_sid = @he_sid";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("he_sid", lb_he_sid.Text);

						using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
						{
							if (Sql_Reader.Read())
							{
								if (Sql_Reader["he_sid"].ToString() != lb_he_sid.Text)	// 有可能用 SQL 隱碼或其它方式進入
									mErr = "請使用正確方式處理!\\n";
							}
							else
								mErr = "找不到指定的網頁編號!\\n";

							Sql_Reader.Close();
						}

						if (mErr == "")
						{
							// 更新資料 (呼叫 SQL Server 的 p_Ck_Html_Files 函數檢查是否有附加檔案)
							SqlString = "Update Html_Edit Set he_title = @he_title, he_content = @he_content,  he_desc = @he_desc";
							SqlString += ", is_attach = dbo.p_Ck_Html_Files(he_sid), init_time = getdate() Where he_sid = @he_sid;";

							Sql_Command.Parameters.Clear();
							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.AddWithValue("he_sid", lb_he_sid.Text);
							Sql_Command.Parameters.AddWithValue("he_title", he_title);
							Sql_Command.Parameters.AddWithValue("he_content", he_content);
							Sql_Command.Parameters.AddWithValue("he_desc", he_desc);
							Sql_Command.ExecuteNonQuery();
						}

						Sql_Conn.Close();
					}
				}
				#endregion
			}
		}

		if (mErr == "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"網頁儲存完成!\\n\");location.replace(\"8001.aspx" + lb_page.Text + "\");", true);
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	// 不存檔結束離開
	protected void bn_leave_Click(object sender, EventArgs e)
	{
		Response.Redirect("8001.aspx" + lb_page.Text);
	}

	// 上傳圖檔
	protected void bn_send_Click(object sender, EventArgs e)
	{
		int hf_size = 0, ckint = 0;
		string SqlString = "", mErr = "";
		string hf_name = "", hf_ext = "", hf_type = "", he_content = "";
		string file_ext = ".jpg.gif.png.bmp.wmf";		// 允許上傳的檔案副檔名

		if (fu_file.HasFile)
		{
			// 處理上傳檔案，檔案內容存入資料庫
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					Sql_Conn.Open();

					Sql_Command.Connection = Sql_Conn;

					hf_name = fu_file.FileName;
					hf_ext = Path.GetExtension(hf_name).ToString().ToLower();

					if (file_ext.Contains(hf_ext))
					{
						hf_size = fu_file.PostedFile.ContentLength;
						hf_type = fu_file.PostedFile.ContentType;

						#region 檔案存入資料庫

						if (tb_he_title.Text.Trim() == "")
							tb_he_title.Text = "_none";

						if (lb_he_sid.Text == "0")
						{
							// 新增資料，先建立 Html_Edit 的資料

							// 清除網頁內的資料
							he_content = Clean_Content(tb_preview.Text.Trim());

							SqlString = "Insert Into Html_Edit (he_title, is_attach, he_content) Values (@he_title, 1, @he_content);";
							SqlString += "Select @he_sid = Scope_Identity()";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.AddWithValue("he_title", tb_he_title.Text.Trim());
							Sql_Command.Parameters.AddWithValue("he_content", he_content);


							SqlParameter spt_he_sid = Sql_Command.Parameters.Add("@he_sid", SqlDbType.Int);
							spt_he_sid.Direction = ParameterDirection.Output;

							Sql_Command.ExecuteNonQuery();

							// 取得新增資料的主鍵值
							ckint = (int)spt_he_sid.Value;
							lb_he_sid.Text = ckint.ToString();
							lt_she_sid.Text = lb_he_sid.Text;
						}
						else
						{
							// 修改資料
							SqlString = "Update Html_Edit Set he_title = @he_title, he_content = @he_content, is_attach = 1";
							SqlString += " Where he_sid = @he_sid";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.AddWithValue("he_sid", lb_he_sid.Text);
							Sql_Command.Parameters.AddWithValue("he_title", tb_he_title.Text.Trim());
							Sql_Command.Parameters.AddWithValue("he_content", he_content);

							Sql_Command.ExecuteNonQuery();
						}

						// 圖檔存入 Html_Files，並更新 Html_Edit 的附檔旗標
						SqlString = "Insert Into Html_Files (he_sid, hf_name, hf_size, hf_type, hf_content)";
						SqlString += " Values (@he_sid, @hf_name, @hf_size, @hf_type, @hf_content);";
						SqlString += "Update Html_Edit Set is_attach = 1 Where he_sid = @he_sid;";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("he_sid", lb_he_sid.Text);
						Sql_Command.Parameters.AddWithValue("hf_name", hf_name);
						Sql_Command.Parameters.AddWithValue("hf_ext", hf_ext);
						Sql_Command.Parameters.AddWithValue("hf_size", hf_size);
						Sql_Command.Parameters.AddWithValue("hf_type", hf_type);
						Sql_Command.Parameters.AddWithValue("hf_content", fu_file.FileBytes);

						Sql_Command.ExecuteNonQuery();

						//預設上傳檔案大小為 4096KB, 執行時間 120秒, 如要修改，要到 Web.Config 處修改下列數據
						//<system.web>
						//<httpRuntime maxRequestLength="4096" executionTimeout="120"/>
						//</system.web>

						#endregion
					}
					else
						mErr = "不接受「" + hf_name + "」的檔案格式!\\n(僅接受「" + file_ext + "」等格式)\\n";

					Sql_Conn.Close();
				}
			}
		}

		if (mErr == "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "Renew_Image("+ lb_he_sid.Text +");", true);
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	// 清除網頁內的資料
	private string Clean_Content(string he_content)
	{
		string svr_name = "";

		// 將 image src 的絕對位置換成相對位置
		svr_name = "src=\"http://" + Request.ServerVariables["SERVER_NAME"].ToString() + "/";
		he_content = he_content.Replace(svr_name, "src=\"../");

		// 將 image 裡的 javascript 移除
		he_content = he_content.Replace(" onload=img_resize(this)", "");

		return he_content;
	}
}
