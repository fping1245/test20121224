//---------------------------------------------------------------------------- 
//程式功能	廣告信發送管理 (廣告信清單) > 發送處理
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _90012 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		string mErr = "";
		int ckint = 0;
		Common_Func cfc = new Common_Func();
		DateTime ckbtime, cketime;

		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("9001", true);

			#region 檢查接收參數
			if (Request["sid"] == null)
			{
				mErr = "參數傳送錯誤!\\n";
			}
			else
			{
				if (int.TryParse(Request["sid"], out ckint))
				{
					lb_adm_sid.Text = ckint.ToString();
					ods_Ad_List.SelectParameters["adm_sid"].DefaultValue = ckint.ToString();
					ods_Ad_List.UpdateParameters["adm_sid"].DefaultValue = ckint.ToString();
					ods_Ad_List.InsertParameters["adm_sid"].DefaultValue = ckint.ToString();

					Get_Data();
				}
				else
					mErr = "參數傳送錯誤!\\n";
			}
			#endregion

			if (mErr == "")
			{
				#region 承接上一頁的查詢條件設定
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
				else
					lb_page.Text = "?pageid=0";

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
				#endregion

				#region 接受下一頁返回時的舊查詢條件
				lb_page.Text += "&sid=" + lb_adm_sid.Text;

				if (Request["pageid1"] != null)
				{
					if (int.TryParse(Request["pageid1"], out ckint))
					{
						if (ckint > gv_Ad_List.PageCount)
							ckint = gv_Ad_List.PageCount;

						gv_Ad_List.PageIndex = ckint;

						lb_page.Text += "&pageid1=" + ckint.ToString();
					}
					else
						lb_pageid1.Text = "0";
				}

				if (Request["adl_email"] != null)
				{
					if (int.TryParse(Request["adl_email"], out ckint))
					{
						tb_adl_email.Text = ckint.ToString();
						ods_Ad_List.SelectParameters["adl_email"].DefaultValue = ckint.ToString();
					}
				}

				if (Request["adb_ibtime"] != null)
				{
					if (DateTime.TryParse(Request["adb_ibtime"], out ckbtime))
					{
						tb_ibtime.Text = ckint.ToString();
						ods_Ad_List.SelectParameters["btime"].DefaultValue = ckbtime.ToString();
					}
				}

				if (Request["adb_ietime"] != null)
				{
					if (DateTime.TryParse(Request["adb_ietime"], out cketime))
					{
						tb_ietime.Text = ckint.ToString();
						ods_Ad_List.SelectParameters["etime"].DefaultValue = cketime.ToString();
					}
				}
				#endregion

				ods_Ad_List.DataBind();
				gv_Ad_List.DataBind();

				#region 檢查頁數是否超過
				if (gv_Ad_List.PageCount < gv_Ad_List.PageIndex + 1)
				{
					gv_Ad_List.PageIndex = gv_Ad_List.PageCount;
					gv_Ad_List.DataBind();
				}

				lb_pageid1.Text = gv_Ad_List.PageIndex.ToString();
				#endregion
			}
		}

		if (mErr != "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");location.replace(\"9001.aspx\");", true);
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
	private void Get_Data()
	{
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			// 由 Ad_Member 匯入信箱資料，信箱若相同時，不重覆匯入
			SqlString = "Select Top 1 adm_title, adm_type, adm_total, adm_send, adm_error, adm_fname, adm_fmail, adm_batch";
			SqlString += ", adm_wait, send_time From Ad_Mail Where adm_sid = @adm_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();

				Sql_Command.Parameters.AddWithValue("adm_sid", lb_adm_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						lb_adm_title.Text = Sql_Reader["adm_title"].ToString().Trim();

						switch (Sql_Reader["adm_type"].ToString())
						{
							case "0":
								lb_adm_type.Text = "TEXT";
								break;
							case "1":
								lb_adm_type.Text = "HTML";
								break;
							default:
								lb_adm_type.Text = "不明";
								break;
						}

						lb_adm_total.Text = Sql_Reader["adm_total"].ToString();
						lb_adm_send.Text = Sql_Reader["adm_send"].ToString();
						lb_adm_error.Text = Sql_Reader["adm_error"].ToString();

						if (Sql_Reader["send_time"].ToString() != "")
							lb_send_time.Text = DateTime.Parse(Sql_Reader["send_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

						tb_adm_fname.Text = Sql_Reader["adm_fname"].ToString().Trim();
						tb_adm_fmail.Text = Sql_Reader["adm_fmail"].ToString().Trim();
						tb_adm_batch.Text = Sql_Reader["adm_batch"].ToString();
						tb_adm_wait.Text = Sql_Reader["adm_wait"].ToString();
					}

					Sql_Reader.Close();
				}

				Sql_Conn.Close();
			}
		}
	}

	// 條件設定
	protected void Btn_Set_Click(object sender, EventArgs e)
	{
		// 檢查查詢條件是否改變
		Chk_Filter();
	}

	// 檢查查詢條件是否改變
	private void Chk_Filter()
	{
		Common_Func cfc = new Common_Func();

		DateTime ckbtime, cketime;
		string tmpstr = "";

		// 有輸入 adl_email，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_adl_email.Text.Trim());
		if (tmpstr != "")
			ods_Ad_List.SelectParameters["adl_email"].DefaultValue = tmpstr;
		else
		{
			tb_adl_email.Text = "";
			ods_Ad_List.SelectParameters["adl_email"].DefaultValue = "";
		}

		// 有輸入異動時間開始範圍，則設定條件
		if (DateTime.TryParse(tb_ibtime.Text.Trim(), out ckbtime))
			ods_Ad_List.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_ibtime.Text = "";
			ods_Ad_List.SelectParameters["btime"].DefaultValue = "";
		}

		// 有輸入異動時間結束範圍，則設定條件
		if (DateTime.TryParse(tb_ietime.Text.Trim(), out cketime))
			ods_Ad_List.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_ietime.Text = "";
			ods_Ad_List.SelectParameters["etime"].DefaultValue = "";
		}

		gv_Ad_List.DataBind();
		if (gv_Ad_List.PageCount - 1 < gv_Ad_List.PageIndex)
		{
			gv_Ad_List.PageIndex = gv_Ad_List.PageCount;
			gv_Ad_List.DataBind();

			lb_pageid1.Text = gv_Ad_List.PageIndex.ToString();
		}
	}

	// 新增存檔
	protected void tb_save_Click(object sender, EventArgs e)
	{
		ods_Ad_List.InsertParameters["adm_sid"].DefaultValue = lb_adm_sid.Text;
		ods_Ad_List.InsertParameters["adl_email"].DefaultValue = tb_email.Text.Trim();
		ods_Ad_List.InsertParameters["adl_send"].DefaultValue = "0";

		ods_Ad_List.Insert();
	}

	// 換頁
	protected void gv_Ad_List_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid1.Text = gv_Ad_List.PageIndex.ToString();
	}

	// 資料連結
	protected void gv_Ad_List_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			Label lb_adl_send = (Label)e.Row.FindControl("lb_adl_send");

			if (lb_adl_send == null)
			{
				// GridView 在編輯狀態
				RadioButton rb_adl_send0 = (RadioButton)e.Row.FindControl("rb_adl_send0");
				RadioButton rb_adl_send1 = (RadioButton)e.Row.FindControl("rb_adl_send1");

				if (rb_adl_send0 != null && rb_adl_send1 != null)
				{
					switch (DataBinder.Eval(e.Row.DataItem, "adl_send").ToString())
					{
						case "0":
							rb_adl_send0.Checked = true;
							rb_adl_send1.Checked = false;
							break;
						case "1":
							rb_adl_send0.Checked = false;
							rb_adl_send1.Checked = true;
							break;
					}
				}
			}
			else
			{
				// GridView 在瀏覽狀態
				switch (DataBinder.Eval(e.Row.DataItem, "adl_send").ToString())
				{
					case "0":
						lb_adl_send.Text = "未發送";
						break;
					case "1":
						lb_adl_send.Text = "<font color=red><b>已發送</b></font>";
						break;
					default:
						lb_adl_send.Text = "錯誤！";
						break;
				}
			}
		}
	}

	// GridView 資料更新前
	protected void gv_Ad_List_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		RadioButton rb_adl_send0 = (RadioButton)gv_Ad_List.Rows[e.RowIndex].FindControl("rb_adl_send0");
		RadioButton rb_adl_send1 = (RadioButton)gv_Ad_List.Rows[e.RowIndex].FindControl("rb_adl_send1");

		if (rb_adl_send0 != null && rb_adl_send1 != null)
		{
			if (rb_adl_send0.Checked)
				ods_Ad_List.UpdateParameters["adl_send"].DefaultValue = "0";
			else
				ods_Ad_List.UpdateParameters["adl_send"].DefaultValue = "1";
		}
	}

	// 資料連結後處理
	protected void gv_Ad_List_DataBound(object sender, EventArgs e)
	{
		string adm_total = "0", adm_send = "0", adm_error = "0";
		string SqlString = "";

		#region 統計並更新郵件發送數量
		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				// 統計郵件發送數量
				SqlString = "Select Count(*) As ltotal, Sum(Case adl_send When 1 Then 1 Else 0 End) As lsend";
				SqlString += ", Sum(Case adl_send When 2 Then 1 Else 0 End) As lerror From Ad_List";
				SqlString += " Where adm_sid = @adm_sid";

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;
				Sql_Command.Parameters.AddWithValue("adm_sid", lb_adm_sid.Text);

				Sql_Conn.Open();

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						adm_total = Sql_Reader["ltotal"].ToString();
						adm_send = Sql_Reader["lsend"].ToString();
						adm_error = Sql_Reader["lerror"].ToString();
					}

					Sql_Reader.Close();
				}

				Sql_Conn.Close();

				// 更新郵件發送數量
				lb_adm_total.Text = adm_total;
				lb_adm_send.Text = adm_send;
				lb_adm_error.Text = adm_error;

				SqlString = "Update Ad_Mail Set adm_total = @adm_total, adm_send = @adm_send, adm_error = @adm_error";
				SqlString += " Where adm_sid = @adm_sid";

				Sql_Command.Parameters.Clear();
				Sql_Command.CommandText = SqlString;
				Sql_Command.Parameters.AddWithValue("adm_sid", lb_adm_sid.Text);
				Sql_Command.Parameters.AddWithValue("adm_total", adm_total);
				Sql_Command.Parameters.AddWithValue("adm_send", adm_send);
				Sql_Command.Parameters.AddWithValue("adm_error", adm_error);

				Sql_Conn.Open();

				Sql_Command.ExecuteNonQuery();

				Sql_Conn.Close();
			}
		}
		#endregion
	}

	// 新增事件處理常式
	protected void ods_Ad_List_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
	{
		string mErr = "";
		if ((int)e.ReturnValue > 0)
			tb_email.Text = "";
		else
		{
			mErr = "資料新增失敗！\\n";
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
		}
	}

	// 修改事件處理常式
	protected void ods_Ad_List_Updated(object sender, ObjectDataSourceStatusEventArgs e)
	{
		string mErr = "";

		if ((int)e.ReturnValue == 0)
		{
			mErr = "資料修改失敗!\\n";
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
		}
	}

	// 刪除事件處理常式
	protected void ods_Ad_List_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
	{
		string mErr = "";
		if ((int)e.ReturnValue == 0)
		{
			mErr = "資料刪除失敗!\\n";
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
		}
	}

	// 修改前的判斷及處理
	protected void ods_Ad_List_Updating(object sender, ObjectDataSourceMethodEventArgs e)
	{
		Check_Internet cknet = new Check_Internet();
		string mErr = "", adl_email = "";

		if (e.InputParameters["adl_email"] == null)
			mErr = "請輸入電子郵件信箱!\\n";
		else
		{
			// 電子信箱一律轉成小寫
			adl_email = e.InputParameters["adl_email"].ToString().Trim().ToLower();
			if (adl_email == "")
				mErr += "請輸入電子郵件信箱!\\n";
			else
			{
				if (cknet.Check_Email(adl_email) != 0)
					mErr = "電子郵件信箱錯誤!!\\n" + adl_email;
				else
					e.InputParameters["adl_email"] = adl_email;
			}
		}

		if (mErr == "")
			e.InputParameters["adm_sid"] = lb_adm_sid.Text;
		else {
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
			e.Cancel = true;		// 取消修改動作
		}
	}

	// 新增前判斷及處理
	protected void ods_Ad_List_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
	{
		Check_Internet cknet = new Check_Internet();
		string mErr = "", adl_email = "";

		// 電子信箱一律轉成小寫
		adl_email = e.InputParameters["adl_email"].ToString().Trim().ToLower();
		if (adl_email == "")
			mErr += "請輸入電子郵件信箱!\\n";
		else
		{
			if (cknet.Check_Email(adl_email) != 0)
				mErr = "電子郵件信箱錯誤!\\n";
			else
				e.InputParameters["adl_email"] = adl_email;
		}

		if (mErr != "")
		{
			mErr += "無法新增，請重新輸入!\\n";

			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");add_display();", true);
			e.Cancel = true;		// 取消新增動件
		}
	}

	// 刪除前的判斷及處理
	protected void ods_Ad_List_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
	{
		e.InputParameters["adm_sid"] = lb_adm_sid.Text;
	}

	// 由會員信箱匯入
	protected void lk_import_Click(object sender, EventArgs e)
	{
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			// 由 Ad_Member 匯入信箱資料，信箱若相同時，不重覆匯入
			SqlString = "Insert Into Ad_List (adm_sid, adl_email) Select @adm_sid As adm_sid, adb_email From Ad_Member";
			SqlString += " Where adb_email Not in (Select adl_email From Ad_List Where adm_sid = @adm_sid)";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();

				Sql_Command.Parameters.AddWithValue("adm_sid", lb_adm_sid.Text);

				Sql_Command.ExecuteNonQuery();

				Sql_Conn.Close();
			}
		}

		gv_Ad_List.DataBind();
	}

	// 清除已發送旗標
	protected void lk_renew_Click(object sender, EventArgs e)
	{
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Update Ad_List Set adl_send = 0, send_time = null Where adm_sid = @adm_sid;";
			SqlString += "Update Ad_Mail Set adm_send = 0, adm_error = 0 Where adm_sid = @adm_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();

				Sql_Command.Parameters.AddWithValue("adm_sid", lb_adm_sid.Text);

				Sql_Command.ExecuteNonQuery();

				Sql_Conn.Close();
			}
		}

		gv_Ad_List.DataBind();
	}

	// 資料刪除處理
	protected void gv_Ad_List_RowDeleted(object sender, GridViewDeletedEventArgs e)
	{
		int mpage = gv_Ad_List.PageCount - 1;

		gv_Ad_List.DataBind();
		if (gv_Ad_List.PageIndex + 1 > gv_Ad_List.PageCount)
			gv_Ad_List.PageIndex = mpage - 1;
	}
}
