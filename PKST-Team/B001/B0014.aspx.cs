//---------------------------------------------------------------------------- 
//程式功能	考試題庫管理 > 試題處理
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _B0014 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		string mErr = "";

		if (!IsPostBack)
		{
			int tp_sid = -1;
			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("B001", false);

			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out tp_sid))
				{
					lb_tp_sid.Text = tp_sid.ToString();
					ods_Ts_Question.SelectParameters["tp_sid"].DefaultValue = tp_sid.ToString();

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
			// 取得資料
			if (!GetData())
				mErr = "找不到相關資料!\\n";
			else
			{
				#region 檢查頁數是否超過
				ods_Ts_Question.DataBind();
				gv_Ts_Question.DataBind();
				if (gv_Ts_Question.PageCount < gv_Ts_Question.PageIndex)
				{
					gv_Ts_Question.PageIndex = gv_Ts_Question.PageCount;
					gv_Ts_Question.DataBind();
				}

				lb_pageid.Text = gv_Ts_Question.PageIndex.ToString();
				#endregion
			}
		}

		if (mErr != "")
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

						lb_tp_question.Text = int.Parse(Sql_Reader["tp_question"].ToString()).ToString("N0");
						lb_tp_member.Text = int.Parse(Sql_Reader["tp_member"].ToString()).ToString("N0");
						lb_tp_score.Text = int.Parse(Sql_Reader["tp_score"].ToString()).ToString("N0");
						lb_tp_total.Text = int.Parse(Sql_Reader["tp_total"].ToString()).ToString("N0");
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
	protected void gv_Ts_Question_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid.Text = gv_Ts_Question.PageIndex.ToString();
	}

	// 每列資料的處理
	protected void gv_Ts_Question_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		string tq_type = "", tq_sid = "", tq_sort ="", tq_desc = "";

		if ((e.Row.RowType == DataControlRowType.DataRow))
		{
			tq_sort = DataBinder.Eval(e.Row.DataItem, "tq_sort").ToString();
			tq_desc = DataBinder.Eval(e.Row.DataItem, "tq_desc").ToString();
			tq_type = DataBinder.Eval(e.Row.DataItem, "tq_type").ToString();

			if (tq_type == "0")
				e.Row.Cells[2].Text = "單選";
			else if (tq_type == "1")
				e.Row.Cells[2].Text = "複選全部";
			else
				e.Row.Cells[2].Text = "複選 " + tq_type + " 題";

			Literal lt_temp = (Literal)e.Row.Cells[1].FindControl("lt_tq_desc");
			tq_sid = DataBinder.Eval(e.Row.DataItem, "tq_sid").ToString();
			lt_temp.Text = Get_Item(lb_tp_sid.Text, tq_sid, tq_type, tq_sort, tq_desc);

			Label lb_temp = (Label)e.Row.Cells[1].FindControl("lb_tq_desc");
			lb_temp.Text = tq_desc.Replace("\r\n", "<br>");
		}
	}

	// 產生答案項目
	private string Get_Item(string tp_sid, string tq_sid, string tq_type, string tq_sort, string tq_desc)
	{
		string tq_item = "", SqlString = "", imgsrc = "", img = "", img_title = "", bgcolor = "";
		string title = "", ti_sid = "", ti_desc = "", ti_sort = "";

		if (tq_type == "0") {
			imgsrc = "../images/ico/radio.gif";
			img_title = "單選";
		}
		else {
			imgsrc = "../images/ico/check.gif";

			if (tq_type == "1")
				img_title = "複選";
			else
				img_title = "複選 " + tq_type + "個答案";
		}

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select ti_sid, ti_sort, ti_desc, ti_correct From Ts_Item Where tp_sid = @tp_sid And tq_sid = @tq_sid";
			SqlString += " Order by tp_sid, tq_sid, ti_sort";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("tp_sid", tp_sid);
				Sql_Command.Parameters.AddWithValue("tq_sid", tq_sid);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					
					if (Sql_Reader.Read())
					{
						tq_item = "<table width=100% border=0 cellpadding=2 cellspacing=0>\n";
						tq_item += "<tr valign=top><td align=center style=\"width:20pt\">";
						tq_item += "<input type=button class=text9pt value=\"＋\" style=\"height:14pt\" title=\"新增答案項目\" onclick=\"add_item(" + tq_sid + "," + tq_sort + ",'" + Server.UrlEncode(tq_desc) + "'" + ")\"></td>\n";
						tq_item += "<td align=left>\n";

						do
						{
							if (bgcolor == "#E6E6FA")
								bgcolor = "#AFEEEE";
							else
								bgcolor = "#E6E6FA";

							if (Sql_Reader["ti_correct"].ToString() == "1")
							{
								img = "../images/ico/correct.gif";
								title = "正確答案";
							}
							else
							{
								img = imgsrc;
								title = img_title;
							}

							ti_sid = Sql_Reader["ti_sid"].ToString();
							ti_sort = Sql_Reader["ti_sort"].ToString();
							ti_desc = Sql_Reader["ti_desc"].ToString().Trim();

							tq_item += "<table width=100% border=0 cellpadding=2 cellspacing=0 style=\"background-color:" + bgcolor + "\">\n";
							tq_item += "<tr><td align=center style=\"width:15pt\"><img src=\"" + img + "\" title=\"" + title + "\"></td>\n";
							tq_item += "<td align=left>" + ti_desc + "</td>\n";
							tq_item += "<td align=center style=\"width:45pt\">\n";
							tq_item += "<input type=button class=text9pt value=\"⊿\" style=\"height:14pt\" title=\"修改答案項目\" onclick=\"mod_item(" + ti_sid + "," + tq_sid + "," + tq_sort + ",'" + Server.UrlEncode(tq_desc) + "'" + ")\">&nbsp;\n";
							tq_item += "<input type=button class=text9pt value=\"Ｘ\" style=\"height:14pt\" title=\"刪除答案項目\" onclick=\"del_item(" + ti_sid + "," + tq_sid + "," + ti_sort + ",'" + ti_desc + "'" + ")\">&nbsp;\n";
							tq_item += "</td>\n";
							tq_item += "</tr></table>\n";
						} while (Sql_Reader.Read());
						tq_item += "</td></tr></table>";
					}
					else
					{
						tq_item = "<table width=100% border=0 cellpadding=2 cellspacing=0>\n";
						tq_item += "<tr><td align=center style=\"width:20pt\"><input type=button class=text9pt value=\"＋\" style=\"height:14pt\" title=\"新增答案項目\" onclick=\"add_item(" + tq_sid + "," + tq_sort + ",'" + Server.UrlEncode(tq_desc) + "'" + ")\"></td>\n";
						tq_item += "<td><font color=red>※ 尚未輸入答案項目!</font></td>\n</tr></table>\n";
					}
	

					Sql_Reader.Close();
				}

				Sql_Conn.Close();
			}
		}

		return tq_item;
	}
}
