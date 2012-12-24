//---------------------------------------------------------------------------- 
//程式功能	考試題庫管理 > 考試紀錄
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _B0015 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		string mErr = "";

		if (!IsPostBack)
		{
			int tp_sid = -1, ckint = -1;
			string tmpstr = "";
			Common_Func cfc = new Common_Func();

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("B001", false);

			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out tp_sid))
				{
					lb_tp_sid.Text = tp_sid.ToString();
					ods_Ts_User.SelectParameters["tp_sid"].DefaultValue = tp_sid.ToString();

					// 取得資料
					if (!GetData())
						mErr = "找不到相關資料!\\n";

					#region 接收下一頁傳來的參數
					if (Request["pageid1"] != null)
					{
						if (int.TryParse(Request["pageid1"], out ckint))
						{
							if (ckint > gv_Ts_User.PageCount)
								ckint = gv_Ts_User.PageCount;

							gv_Ts_User.PageIndex = ckint;
						}
						else
							lb_pageid1.Text = "0";
					}

					if (Request["tu_name"] != null)
					{
						tmpstr = cfc.CleanSQL(Request["tu_name"].Trim());
						if (tmpstr != "")
						{
							tb_tu_name.Text = tmpstr;
							ods_Ts_User.SelectParameters["tu_name"].DefaultValue = tmpstr;
						}
						else
						{
							tb_tu_name.Text = "";
							ods_Ts_User.SelectParameters["tu_name"].DefaultValue = "";
						}
					}

					if (Request["tu_no"] != null)
					{
						tmpstr = cfc.CleanSQL(Request["tu_no"].Trim());
						if (tmpstr != "")
						{
							tb_tu_no.Text = tmpstr;
							ods_Ts_User.SelectParameters["tu_no"].DefaultValue = tmpstr;
						}
						else
						{
							tb_tu_no.Text = "";
							ods_Ts_User.SelectParameters["tu_no"].DefaultValue = "";
						}
					}

					if (Request["tu_ip"] != null)
					{
						tmpstr = cfc.CleanSQL(Request["tu_ip"].Trim());
						if (tmpstr != "")
						{
							tb_tu_ip.Text = tmpstr;
							ods_Ts_User.SelectParameters["tu_ip"].DefaultValue = tmpstr;
						}
						else
						{
							tb_tu_ip.Text = "";
							ods_Ts_User.SelectParameters["tu_ip"].DefaultValue = "";
						}
					}

					#endregion

					#region 接收上一頁傳來的參數
					if (Request["pageid"] != null)
						lb_page.Text = "?pageid=" + Request["pageid"].Trim();
					else
						lb_page.Text = "?pageid=0";

					if (Request["tp_sid"] != null)
						lb_page.Text += "&tp_sid=" + Request["tp_sid"].Trim();

					if (Request["tp_title"] != null)
						lb_page.Text += "&tp_title=" + Server.UrlEncode(Request["tp_title"].Trim());

					if (Request["is_show"] != null)
						lb_page.Text += "&is_show=" + Request["is_show"].Trim();

					if (Request["b_time"] != null)
						lb_page.Text += "&btime=" + Server.UrlEncode(Request["btime"].Trim());

					if (Request["b_time"] != null)
						lb_page.Text += "&etime=" + Server.UrlEncode(Request["etime"].Trim());
					#endregion
				}
				else
					mErr = "參數格式錯誤!\\n";
			}
			else
				mErr = "參數傳入錯誤!\\n";
		}

		if (mErr == "")
		{
			#region 檢查頁數是否超過
			ods_Ts_User.DataBind();
			gv_Ts_User.DataBind();
			if (gv_Ts_User.PageCount < gv_Ts_User.PageIndex)
			{
				gv_Ts_User.PageIndex = gv_Ts_User.PageCount;
				gv_Ts_User.DataBind();
			}

			lb_pageid1.Text = gv_Ts_User.PageIndex.ToString();
			#endregion
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");location.replace(\"B001.aspx" + lb_page.Text + "\");", true);
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
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 tp_title, is_show, b_time, e_time, tp_desc, tp_question, tp_score, tp_member, tp_total, init_time";
			SqlString += " From Ts_Paper Where tp_sid = @tp_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						lb_tp_title.Text = Sql_Reader["tp_title"].ToString().Trim();
						lb_tp_desc.Text = Sql_Reader["tp_desc"].ToString().Trim().Replace("\r\n","<br>");

						if (Sql_Reader["is_show"].ToString() == "0")
							lb_is_show.Text = "隱藏";
						else
							lb_is_show.Text = "顯示";

						lb_b_time.Text = DateTime.Parse(Sql_Reader["b_time"].ToString()).ToString("yyyy/MM/dd HH:mm");
						lb_e_time.Text = DateTime.Parse(Sql_Reader["e_time"].ToString()).ToString("yyyy/MM/dd HH:mm");

						lb_tb_question.Text = int.Parse(Sql_Reader["tp_question"].ToString()).ToString("N0");
						lb_tb_member.Text = int.Parse(Sql_Reader["tp_member"].ToString()).ToString("N0");
						lb_tb_score.Text = int.Parse(Sql_Reader["tp_score"].ToString()).ToString("N0");
						lb_tb_total.Text = int.Parse(Sql_Reader["tp_total"].ToString()).ToString("N0");
						lb_init_time.Text = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

						ckbool = true;
					}
					else
						ckbool = false;

					Sql_Reader.Close();
				}				

				Sql_Conn.Close();
			}
		}

		return ckbool;
	}

	// 換頁
	protected void gv_Ts_User_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid1.Text = gv_Ts_User.PageIndex.ToString();
	}

	// 每列資料的處理
	protected void gv_Ts_User_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		string is_test = "";

		if ((e.Row.RowType == DataControlRowType.DataRow))
		{
			is_test = DataBinder.Eval(e.Row.DataItem, "is_test").ToString();

			if (is_test == "1")
				e.Row.Cells[6].Text = "已作答";
			else
				e.Row.Cells[6].Text = "－－－";
		}
	}

	// 條件設定
	protected void btn_Set_Click(object sender, EventArgs e)
	{
		// 檢查查詢條件是否改變
		Chk_Filter();
	}

	// 檢查查詢條件是否改變
	private void Chk_Filter()
	{
		Common_Func cfc = new Common_Func();

		string tmpstr = "";

		// 有輸入 tu_name，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_tu_name.Text.Trim());
		if (tmpstr != "")
			ods_Ts_User.SelectParameters["tu_name"].DefaultValue = tmpstr;
		else
		{
			tb_tu_name.Text = "";
			ods_Ts_User.SelectParameters["tu_name"].DefaultValue = "";
		}

		// 有輸入 tu_no，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_tu_no.Text.Trim());
		if (tmpstr != "")
			ods_Ts_User.SelectParameters["tu_no"].DefaultValue = tmpstr;
		else
		{
			tb_tu_no.Text = "";
			ods_Ts_User.SelectParameters["tu_no"].DefaultValue = "";
		}

		// 有輸入 tu_ip，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_tu_ip.Text.Trim());
		if (tmpstr != "")
			ods_Ts_User.SelectParameters["tu_ip"].DefaultValue = tmpstr;
		else
		{
			tb_tu_ip.Text = "";
			ods_Ts_User.SelectParameters["tu_ip"].DefaultValue = "";
		}

		gv_Ts_User.DataBind();
		if (gv_Ts_User.PageCount - 1 < gv_Ts_User.PageIndex)
		{
			gv_Ts_User.PageIndex = gv_Ts_User.PageCount;
			gv_Ts_User.DataBind();
		}
	}

	// 重新排名
	protected void lk_sort_Click(object sender, EventArgs e)
	{
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Execute dbo.p_Ts_User_ReSort @tp_sid;";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);

				Sql_Conn.Open();

				Sql_Command.ExecuteNonQuery();

				Sql_Conn.Close();
			}
		}

		ClientScript.RegisterStartupScript(this.GetType(), "ClientScript",
			"alert(\"排名處理結束!\\n\");location.replace(\"B0015.aspx" + lb_page.Text + "&sid=" + lb_tp_sid.Text + "\");", true);
	}

	// 重新計算成績並排名
	protected void lk_score_Click(object sender, EventArgs e)
	{
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Execute dbo.p_Ts_User_ReScore @tp_sid;";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);

				Sql_Conn.Open();

				Sql_Command.ExecuteNonQuery();

				Sql_Conn.Close();
			}
		}

		ClientScript.RegisterStartupScript(this.GetType(), "ClientScript",
			"alert(\"成績及排名處理結束!\\n\");location.replace(\"B0015.aspx" + lb_page.Text + "&sid=" + lb_tp_sid.Text + "\");", true);
	}
}
