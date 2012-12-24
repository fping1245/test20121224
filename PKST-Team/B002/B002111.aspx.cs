//---------------------------------------------------------------------------- 
//程式功能	線上考試 (自由參加) > 開始考試 > 成績公佈
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _B002111 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		string mErr = "";

		if (!IsPostBack)
		{
			int tp_sid = -1, tu_sid = -1;

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("B002", false);

			if (Request["sid"] != null && Request["tp_sid"] != null)
			{
				if (int.TryParse(Request["sid"], out tu_sid) && int.TryParse(Request["tp_sid"], out tp_sid))
				{
					lb_tu_sid.Text = tu_sid.ToString();
					lb_tp_sid.Text = tp_sid.ToString();
					ods_Ts_QU.SelectParameters["tu_sid"].DefaultValue = tu_sid.ToString();
					ods_Ts_QU.SelectParameters["tp_sid"].DefaultValue = tp_sid.ToString();

					// 取得資料
					if (!GetData())
						mErr = "找不到相關資料!\\n";
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
			ods_Ts_QU.DataBind();
			gv_Ts_QU.DataBind();
			if (gv_Ts_QU.PageCount < gv_Ts_QU.PageIndex)
			{
				gv_Ts_QU.PageIndex = gv_Ts_QU.PageCount;
				gv_Ts_QU.DataBind();
			}

			lb_pageid.Text = gv_Ts_QU.PageIndex.ToString();
			#endregion
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");location.replace(\"B002.aspx\");", true);
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
		float tp_ave = 1;

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 u.tu_name, u.tu_no, u.tu_ip, u.tu_sort, u.tu_score, u.tu_question, u.b_time, u.e_time";
			SqlString += ", p.tp_title, p.tp_desc, p.tp_member, p.tp_question, p.tp_total, p.tp_score";
			SqlString += " From Ts_User u Left Outer Join Ts_Paper p On u.tp_sid = p.tp_sid";
			SqlString += " Where u.tp_sid = @tp_sid And u.tu_sid = @tu_sid;";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
				Sql_Command.Parameters.AddWithValue("tu_sid", lb_tu_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						lb_tp_title.Text = Sql_Reader["tp_title"].ToString().Trim();
						lb_tp_desc.Text = Sql_Reader["tp_desc"].ToString().Trim().Replace("\r\n","<br>");

						lb_tu_name.Text = Sql_Reader["tu_name"].ToString().Trim();
						lb_tu_no.Text = Sql_Reader["tu_no"].ToString().Trim();
						lb_tu_ip.Text = Sql_Reader["tu_ip"].ToString().Trim();
						lb_tu_sort.Text = Sql_Reader["tu_sort"].ToString();
						lb_tu_question.Text = Sql_Reader["tu_question"].ToString();
						lb_tu_score.Text = Sql_Reader["tu_score"].ToString();
						lb_b_time.Text = DateTime.Parse(Sql_Reader["b_time"].ToString()).ToString("yyyy/MM/dd HH:mm");
						lb_e_time.Text = DateTime.Parse(Sql_Reader["e_time"].ToString()).ToString("yyyy/MM/dd HH:mm");

						lb_tp_member.Text = int.Parse(Sql_Reader["tp_member"].ToString()).ToString("N0");
						lb_tp_question.Text = int.Parse(Sql_Reader["tp_question"].ToString()).ToString("N0");
						tp_ave = float.Parse(Sql_Reader["tp_total"].ToString()) / float.Parse(Sql_Reader["tp_member"].ToString());
						lb_tp_avg.Text = Math.Round(tp_ave, 4).ToString();
						lb_tp_score.Text = int.Parse(Sql_Reader["tp_score"].ToString()).ToString("N0");

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
	protected void gv_Ts_QU_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid.Text = gv_Ts_QU.PageIndex.ToString();
	}

	// 每列資料的處理
	protected void gv_Ts_QU_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		string tq_type = "", tq_sid = "", tq_sort ="", tq_desc = "", tuq_score = "";

		if ((e.Row.RowType == DataControlRowType.DataRow))
		{
			tq_sort = DataBinder.Eval(e.Row.DataItem, "tq_sort").ToString();
			tq_desc = DataBinder.Eval(e.Row.DataItem, "tq_desc").ToString();
			tq_type = DataBinder.Eval(e.Row.DataItem, "tq_type").ToString();
			tuq_score = DataBinder.Eval(e.Row.DataItem, "tuq_score").ToString();

			if (tuq_score == "0")
				e.Row.Cells[0].Text = "<font color=red><b>╳</b></font>";
			else
				e.Row.Cells[0].Text = "○";

			if (tq_type == "0")
				e.Row.Cells[4].Text = "單選";
			else if (tq_type == "1")
				e.Row.Cells[4].Text = "複選全部";
			else
				e.Row.Cells[4].Text = "複選 " + tq_type + " 題";

			Literal lt_temp = (Literal)e.Row.Cells[3].FindControl("lt_tq_desc");
			tq_sid = DataBinder.Eval(e.Row.DataItem, "tq_sid").ToString();
			lt_temp.Text = Get_Item(lb_tp_sid.Text, tq_sid, tq_type, tq_sort, tq_desc);
		}
	}

	// 產生答案項目
	private string Get_Item(string tp_sid, string tq_sid, string tq_type, string tq_sort, string tq_desc)
	{
		string tq_item = "", SqlString = "", imgsrc = "", img = "", img_title = "", bgcolor = "", simg = "";
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
			SqlString = "Select i.ti_sid, i.ti_sort, i.ti_desc, i.ti_correct, IsNull(u.tu_sid, 0) as tu_sid From Ts_Item i ";
			SqlString += "Left Outer Join Ts_UAns u On u.tu_sid = @tu_sid And i.tp_sid = u.tp_sid And i.tq_sid = u.tq_sid And i.ti_sid = u.ti_sid";
			SqlString += " Where i.tp_sid = @tp_sid And i.tq_sid = @tq_sid Order by i.tp_sid, i.tq_sid, i.ti_sort";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("tu_sid", lb_tu_sid.Text);
				Sql_Command.Parameters.AddWithValue("tp_sid", tp_sid);
				Sql_Command.Parameters.AddWithValue("tq_sid", tq_sid);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					
					if (Sql_Reader.Read())
					{
						tq_item = "<table width=100% border=0 cellpadding=2 cellspacing=0>\n";
						tq_item += "<tr valign=top>";
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

							if (Sql_Reader["tu_sid"].ToString() == "0")
								simg = "../images/ico/normal.gif";
							else
								simg = "../images/ico/correct-r.gif";

							ti_sid = Sql_Reader["ti_sid"].ToString();
							ti_sort = Sql_Reader["ti_sort"].ToString();
							ti_desc = Sql_Reader["ti_desc"].ToString().Trim();

							tq_item += "<table width=100% border=0 cellpadding=2 cellspacing=0 style=\"background-color:" + bgcolor + "\">\n";
							tq_item += "<tr><td align=center style=\"width:32pt\"><img src=\"" + simg + "\" title=\"考生選擇的答案\"><img src=\"" + img + "\" title=\"" + title + "\"></td>\n";
							tq_item += "<td align=left>" + ti_desc + "</td>\n";
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
