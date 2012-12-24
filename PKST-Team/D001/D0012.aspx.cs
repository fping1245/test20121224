//---------------------------------------------------------------------------- 
//程式功能	論壇前端 > 主題回應列表
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _D0012 : System.Web.UI.Page
{
	public static int ff_sid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限，但不存入登入紀錄
			//Check_Power("D001", false);

			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out ff_sid))
				{
					// 取得抬頭資料
					mErr = GetData();

					if (mErr == "")
					{
						ods_Fm_Response.SelectParameters["ff_sid"].DefaultValue = ff_sid.ToString();
						ods_Fm_Response.SelectParameters["is_close"].DefaultValue = "1";

						#region 接受上一頁的查詢條件
						if (Request["pageid"] != null)
							lb_page.Text = "?pageid=" + Request["pageid"];
						else
							lb_page.Text = "?pageid=0";

						if (Request["ff_topic"] != null)
							lb_page.Text += "&ff_topic=" + Server.UrlEncode(Request["ff_topic"]);

						if (Request["ff_desc"] != null)
							lb_page.Text += "&ff_desc=" + Server.UrlEncode(Request["ff_desc"]);

						if (Request["ff_name"] != null)
							lb_page.Text += "&ff_name=" + Server.UrlEncode(Request["ff_name"]);

						if (Request["btime"] != null)
							lb_page.Text += "&btime=" + Server.UrlEncode(Request["btime"]);

						if (Request["etime"] != null)
							lb_page.Text += "&etime=" + Server.UrlEncode(Request["etime"]);
						#endregion
					}
					else
					{
						ods_Fm_Response.SelectParameters["ff_sid"].DefaultValue = "0";
						ods_Fm_Response.SelectParameters["is_close"].DefaultValue = "255";
					}
				}
				else
					mErr = "參數格式錯誤!\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";
		}

		if (mErr == "")
		{
			#region 檢查頁數是否超過
			ods_Fm_Response.DataBind();
			gv_Fm_Response.DataBind();
			//if (gv_Fm_Response.PageCount < gv_Fm_Response.PageIndex)
			//{
			//    gv_Fm_Response.PageIndex = gv_Fm_Response.PageCount;
			//    gv_Fm_Response.DataBind();
			//}

			//lb_pageid.Text = gv_Fm_Response.PageIndex.ToString();
			#endregion
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");location.replace(\"D001.aspx" + lb_page.Text + "\");", true);
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
			SqlString += " From Fm_Forum Where ff_sid = @ff_sid And is_close = 1";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("ff_sid", ff_sid);

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

		DateTime ckbtime, cketime;
		string tmpstr = "";

		// 有輸入 fr_desc，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_fr_desc.Text.Trim());
		if (tmpstr != "")
			ods_Fm_Response.SelectParameters["fr_desc"].DefaultValue = tmpstr;
		else
		{
			tb_fr_desc.Text = "";
			ods_Fm_Response.SelectParameters["fr_desc"].DefaultValue = "";
		}

		// 有輸入 fr_name，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_fr_name.Text.Trim());
		if (tmpstr != "")
			ods_Fm_Response.SelectParameters["fr_name"].DefaultValue = tmpstr;
		else
		{
			tb_fr_name.Text = "";
			ods_Fm_Response.SelectParameters["fr_name"].DefaultValue = "";
		}

		// 有輸入 btime 範圍，則設定條件
		if (DateTime.TryParse(tb_btime.Text.Trim(), out ckbtime))
			ods_Fm_Response.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_btime.Text = "";
			ods_Fm_Response.SelectParameters["btime"].DefaultValue = "";
		}

		// 有輸入 etime 範圍，則設定條件
		if (DateTime.TryParse(tb_etime.Text.Trim(), out cketime))
			ods_Fm_Response.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_etime.Text = "";
			ods_Fm_Response.SelectParameters["etime"].DefaultValue = "";
		}

		gv_Fm_Response.DataBind();
		if (gv_Fm_Response.PageCount - 1 < gv_Fm_Response.PageIndex)
		{
			gv_Fm_Response.PageIndex = gv_Fm_Response.PageCount;
			gv_Fm_Response.DataBind();
		}
	}

	// 換頁處理
	protected void gv_Fm_Response_PageIndexChanged(object sender, EventArgs e)
	{
		lb_pageid.Text = gv_Fm_Response.PageIndex.ToString();
	}

	// 更新顯示的資料格式
	protected void gv_Fm_Response_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		Image img_tmp;

		if ((e.Row.RowType == DataControlRowType.DataRow))
		{
			#region 性別處理
			string ff_sex = DataBinder.Eval(e.Row.DataItem, "fr_sex").ToString();

			img_tmp = (Image)e.Row.Cells[0].FindControl("img_fr_sex");

			if (img_tmp != null)
			{
				switch (ff_sex)
				{
					case "1":
						img_tmp.ImageUrl = "~/images/symbol/man.gif";
						img_tmp.ToolTip = "男性";
						img_tmp.AlternateText = "男性";
						break;
					case "2":
						img_tmp.ImageUrl = "~/images/symbol/woman.gif";
						img_tmp.ToolTip = "女性";
						img_tmp.AlternateText = "女性";
						break;
					default:
						img_tmp.ImageUrl = "~/images/symbol/unknow.gif";
						img_tmp.ToolTip = "未填性別";
						img_tmp.AlternateText = "未填性別";
						break;
				}
			}
			#endregion

			#region 心情符號處理
			string ff_symbol = DataBinder.Eval(e.Row.DataItem, "fr_symbol").ToString();
			img_tmp = (Image)e.Row.Cells[0].FindControl("img_fr_symbol");

			Common_Func.ImageSymbol img_symbo = new Common_Func.ImageSymbol();

			img_symbo.code = int.Parse(ff_symbol);
			img_tmp.ImageUrl = img_symbo.image;
			img_tmp.ToolTip = img_symbo.name;
			img_tmp.AlternateText = img_symbo.name;
			#endregion

			#region 內容隱藏處理 (隱藏內容但開放留言的狀況，使用替代文字)
			if (DataBinder.Eval(e.Row.DataItem, "is_show").ToString() == "0"
				&& DataBinder.Eval(e.Row.DataItem, "is_close").ToString() == "1")
			{
				Label fr_desc = (Label)e.Row.Cells[0].FindControl("lb_fr_desc");

				fr_desc.Text = "<font color=red><b>××× 隱藏  ××× " + DataBinder.Eval(e.Row.DataItem, "instead").ToString() + "</b></font>";
			}
			#endregion
		}
	}
}
