//---------------------------------------------------------------------------- 
//程式功能	行事曆管理 > 新增/修改資料
//---------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _50021 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0;
			string mErr = "";
			DateTime ckdatetime;

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("5002", false);

			if (Request["dtm"] == null && Request["sid"] == null)
				mErr = "參數傳遞錯誤!\\n";
			else
			{
				ods_Ca_Group.SelectParameters["mg_sid"].DefaultValue = Session["mg_sid"].ToString();
				ddl_cg_sid.DataBind();

				if (Request["sid"] == null)
				{
					lb_ca_sid.Text = "0";

					if (DateTime.TryParse(Request["dtm"], out ckdatetime))
					{
						tb_ca_bdate.Text = ckdatetime.ToString("yyyy/MM/dd");
						tb_ca_edate.Text = ckdatetime.ToString("yyyy/MM/dd");
					}
					else
						mErr = "參數格式錯誤!\\n";
				}
				else
				{
					if (int.TryParse(Request["sid"], out ckint))
						lb_ca_sid.Text = ckint.ToString();

					else
						lb_ca_sid.Text = "0";
				}

				if (mErr == "")
				{
					// 填入預設資料
					mErr = init_data();
				}
			}

			if (Request["reload"] != null)
			{
				if (Request["reload"] == "1")
					lb_return.Text = "parent.location.replace('5002.aspx?sid=" + lb_ca_sid.Text + "&dtm=" + tb_ca_bdate.Text + "');";
			}

			if (mErr != "")
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");parent.close_all();", true);
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

	// 填入顯示及輸入的資料
	private string init_data()
	{
		string mErr = "", SqlString = "", cf_name = "", cf_sid = "0";
		DateTime ca_btime, ca_etime;
		int fCnt = 0, cf_size = 0;

		if (lb_ca_sid.Text == "0")
		{
			lt_title.Text = "新增";
			lbk_del.Visible = false;

			ddl_ca_class.SelectedValue = "2";
			tb_ca_bhour.Text = "08";
			tb_ca_bmin.Text = "30";
			tb_ca_ehour.Text = "17";
			tb_ca_emin.Text = "30";
		}
		else
		{
			lt_title.Text = "修改";
			lbk_del.Visible = true;

			SqlString = "Select Top 1 ca_btime, ca_etime, ca_class, cg_sid, ca_subject, ca_area, ca_desc, is_attach, init_time";
			SqlString += " From Ca_Calendar Where ca_sid = @ca_sid And mg_sid = @mg_sid";

			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				Sql_Conn.Open();
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					SqlDataReader Sql_Reader;

					Sql_Command.Connection = Sql_Conn;
					Sql_Command.CommandText = SqlString;

					Sql_Command.Parameters.AddWithValue("ca_sid", lb_ca_sid.Text);
					Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());

					Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
					{
						ca_btime = DateTime.Parse(Sql_Reader["ca_btime"].ToString());
						ca_etime = DateTime.Parse(Sql_Reader["ca_etime"].ToString());

						ddl_ca_class.SelectedValue = Sql_Reader["ca_class"].ToString();
						ddl_cg_sid.SelectedValue = Sql_Reader["cg_sid"].ToString();
						tb_ca_subject.Text = Sql_Reader["ca_subject"].ToString().Trim();
						tb_ca_area.Text = Sql_Reader["ca_area"].ToString().Trim();
						tb_ca_desc.Text = Sql_Reader["ca_desc"].ToString().Trim();
						lb_is_attach.Text = Sql_Reader["is_attach"].ToString();
						lb_init_time.Text = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
						tb_ca_bdate.Text = ca_btime.ToString("yyyy/MM/dd");
						tb_ca_bhour.Text = ca_btime.ToString("HH");
						tb_ca_bmin.Text = ca_btime.ToString("mm");
						tb_ca_edate.Text = ca_etime.ToString("yyyy/MM/dd");
						tb_ca_ehour.Text = ca_etime.ToString("HH");
						tb_ca_emin.Text = ca_etime.ToString("mm");
					}
					else
						mErr = "找不到指定的資料!\\n";

					Sql_Reader.Close();
					Sql_Reader.Dispose();

					// 尋找附加檔案
					if (mErr == "" && lb_is_attach.Text == "1")
					{
						SqlString = "Select cf_sid, cf_name, cf_size From Ca_Files Where mg_sid = @mg_sid And ca_sid = @ca_sid";

						Sql_Command.CommandText = SqlString;
						Sql_Reader = Sql_Command.ExecuteReader();

						fCnt = 0;
						lt_file.Text = "<br>";

						while (Sql_Reader.Read())
						{
							fCnt++;
							cf_sid = Sql_Reader["cf_sid"].ToString();
							cf_name = Sql_Reader["cf_name"].ToString().Trim();
							cf_size = int.Parse(Sql_Reader["cf_size"].ToString());

							lt_file.Text += "<a href=\"javascript:mdel(" + cf_sid + ",'" + cf_name + "')\" title=\"刪除檔案\"><img src=\"../images/button/delete.gif\" border=0></a>&nbsp;&nbsp;";
							lt_file.Text += "<a href=\"50021_file.ashx?sid=" + cf_sid + "&ca_sid=" + lb_ca_sid.Text + "\" title=\"下載檔案\n" + cf_size.ToString("N0") + " bytes\" target=\"_blank\">";
							lt_file.Text += "<img src=\"../images/ico/file.gif\" border=0>&nbsp;";
							lt_file.Text += cf_name + "</a><br />";
						}

						Sql_Reader.Close();
						Sql_Reader.Dispose();
						
						if (fCnt == 0)
						{
							lb_is_attach.Text = "0";

							SqlString = "Update Ca_Calendar Set is_attach = 0 Where ca_sid = @ca_sid And mg_sid = @mg_sid";

							Sql_Command.CommandText = SqlString;
							Sql_Command.ExecuteNonQuery();
						}
					}
				}
			}
		}
		return mErr;
	}

	// 存檔
	protected void lbk_ok_Click(object sender, EventArgs e)
	{
		string mErr = "";

		// 儲存資料
		mErr = save_data();

		if (mErr == "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"儲存完成！\\n\");parent.location.replace(\"5002.aspx?dtm=" + tb_ca_bdate.Text + "\");", true);
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");close_msg_wait();", true);
	}

	// 刪除
	protected void lbk_del_Click(object sender, EventArgs e)
	{
		string mErr = "", SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			Sql_Conn.Open();
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				#region 刪除附屬檔案
				SqlString = "Delete Ca_Files Where mg_sid = @mg_sid And ca_sid = @ca_sid";

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;
				Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
				Sql_Command.Parameters.AddWithValue("ca_sid", lb_ca_sid.Text);

				Sql_Command.ExecuteNonQuery();
				#endregion

				#region 刪除行事曆
				SqlString = "Delete Ca_Calendar Where ca_sid = @ca_sid And mg_sid = @mg_sid";
				Sql_Command.CommandText = SqlString;
				Sql_Command.ExecuteNonQuery();
				#endregion

				Sql_Command.Dispose();


				SqlString = "Select Top 1 ca_sid From Ca_Calendar Where ca_sid = @ca_sid And mg_sid = @mg_sid";
				Sql_Command.CommandText = SqlString;

				SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

				if (Sql_Reader.Read())
					mErr = "資料刪除失敗!\\n";

				Sql_Reader.Close();
				Sql_Reader.Dispose();
			}
		}

		if (mErr == "")
		{
			lb_return.Text = "parent.location.reload()";
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"刪除完成！\\n\");parent.location.replace(\"5002.aspx?dtm=" + tb_ca_bdate.Text + "\");", true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	// 上傳檔案
	protected void bn_save_Click(object sender, EventArgs e)
	{
		string mErr = "";

		if (fu_file.HasFile)
		{
			// 儲存資料
			mErr = save_data();
		}
		else
		{
			mErr = "請選擇上傳的檔案!\\n";
		}

		if (mErr == "")
		{
			// 重新填入資料
			lb_return.Text = "parent.location.replace('5002.aspx?sid=" + lb_ca_sid.Text + "&dtm=" + tb_ca_bdate.Text +"');";
			init_data();
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"檔案上傳完成！\\n\");parent.close_msg_wait();", true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");parent.close_msg_wait();", true);
	}

	// 儲存資料
	private string save_data()
	{
		string mErr = "", SqlString = "";
		int ca_sid = 0;

		String_Func sfc = new String_Func();
		int ck_int = 0;
		string ca_hour = "", ca_min = "";
		DateTime ca_btime = DateTime.Today;
		DateTime ca_etime = DateTime.Today;

		#region 開始時間，資料合併及檢查
		int.TryParse(tb_ca_bhour.Text, out ck_int);
		ca_hour = sfc.FillLeft(ck_int.ToString(), 2, "0");

		int.TryParse(tb_ca_bmin.Text, out ck_int);
		ca_min = sfc.FillLeft(ck_int.ToString(), 2, "0");

		if (! DateTime.TryParse(tb_ca_bdate.Text + " " + ca_hour + ":" + ca_min, out ca_btime))
			mErr += "開始時間有誤!\\n";
		#endregion

		#region 結束時間，資料合併及檢查
		int.TryParse(tb_ca_ehour.Text, out ck_int);
		ca_hour = sfc.FillLeft(ck_int.ToString(), 2, "0");

		int.TryParse(tb_ca_emin.Text, out ck_int);
		ca_min = sfc.FillLeft(ck_int.ToString(), 2, "0");

		if (! DateTime.TryParse(tb_ca_edate.Text + " " + ca_hour + ":" + ca_min, out ca_etime))
			mErr += "結束時間有誤!\\n";
		#endregion

		#region 自動判斷為新增或修改，儲存資料
		if (mErr == "")
		{
			int.TryParse(lb_ca_sid.Text, out ca_sid);

			// 有上傳檔案
			if (fu_file.HasFile && fu_file.PostedFile.ContentLength <= 4194304)
				lb_is_attach.Text = "1";

			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				Sql_Conn.Open();
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					SqlParameter spt_ca_sid = new SqlParameter();

					Sql_Command.Connection = Sql_Conn;

					if (ca_sid == 0)
					{
						// 新增資料
						SqlString = "Insert Into Ca_Calendar (mg_sid, ca_btime, ca_etime, ca_class, cg_sid, ca_subject";
						SqlString += ", ca_area, ca_desc, is_attach) Values (@mg_sid, @ca_btime, @ca_etime, @ca_class";
						SqlString += ", @cg_sid, @ca_subject, @ca_area, @ca_desc, @is_attach);";
						SqlString += "Select @ca_sid = Scope_Identity()";

						spt_ca_sid = Sql_Command.Parameters.Add("ca_sid", SqlDbType.Int);
						spt_ca_sid.Direction = ParameterDirection.Output;
					}
					else
					{
						// 修改資料
						SqlString = "Update Ca_Calendar Set ca_btime = @ca_btime, ca_etime = @ca_etime, ca_class = @ca_class";
						SqlString += ", cg_sid = @cg_sid, ca_subject = @ca_subject, ca_area = @ca_area, ca_desc = @ca_desc";
						SqlString += ", is_attach = @is_attach, init_time = getdate()";
						SqlString += " Where ca_sid = @ca_sid And mg_sid = @mg_sid";

						Sql_Command.Parameters.AddWithValue("ca_sid", ca_sid);
					}

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
					Sql_Command.Parameters.AddWithValue("ca_btime", ca_btime);
					Sql_Command.Parameters.AddWithValue("ca_etime", ca_etime);
					Sql_Command.Parameters.AddWithValue("ca_class", ddl_ca_class.SelectedValue);
					Sql_Command.Parameters.AddWithValue("cg_sid", ddl_cg_sid.SelectedValue);
					Sql_Command.Parameters.AddWithValue("ca_subject", tb_ca_subject.Text.Trim());
					Sql_Command.Parameters.AddWithValue("ca_area", tb_ca_area.Text.Trim());
					Sql_Command.Parameters.AddWithValue("ca_desc", tb_ca_desc.Text.Trim());
					Sql_Command.Parameters.AddWithValue("is_attach", lb_is_attach.Text);

					Sql_Command.ExecuteNonQuery();

					if (ca_sid == 0)
						ca_sid = (int)spt_ca_sid.Value;

					Sql_Conn.Close();
					Sql_Command.Dispose();

					#region 有上傳檔案時，處理檔案的儲存
					if (fu_file.HasFile)
					{
						if (fu_file.PostedFile.ContentLength > 4194304)
							mErr = "上傳檔案太大，請小於 4096KB\\n";
						else
						{
							SqlString = "Insert Into Ca_Files (mg_sid, ca_sid, cf_name, cf_content, cf_size, cf_type)";
							SqlString += " Values (@mg_sid, @ca_sid, @cf_name, @cf_content, @cf_size, @cf_type)";

							Sql_Conn.Open();
							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
							Sql_Command.Parameters.AddWithValue("ca_sid", ca_sid);
							Sql_Command.Parameters.AddWithValue("cf_name", fu_file.FileName);
							Sql_Command.Parameters.AddWithValue("cf_size", fu_file.PostedFile.ContentLength);
							Sql_Command.Parameters.AddWithValue("cf_type", fu_file.PostedFile.ContentType);
							Sql_Command.Parameters.AddWithValue("cf_content", fu_file.FileBytes);

							//預設上傳檔案大小為 4096KB, 執行時間 120秒, 如要修改，要到 Web.Config 處修改下列數據
							//<system.web>
							//<httpRuntime maxRequestLength="4096" executionTimeout="120"/>
							//</system.web>

							Sql_Command.ExecuteNonQuery();
						}
					}
					#endregion
				}
			}
			lb_ca_sid.Text = ca_sid.ToString();
		}
		#endregion

		return mErr;
	}
}