//---------------------------------------------------------------------------- 
//程式功能	線上考試(限定身份) > 填寫考生資料 > 試題清單
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _B00311 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int tp_sid = -1, tu_sid = -1;
			string mErr = "";

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("B003", false);

			if (Request["tu_sid"] != null && Request["tp_sid"] != null)
			{
				if (int.TryParse(Request["tu_sid"], out tu_sid) && int.TryParse(Request["tp_sid"], out tp_sid))
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

			if (mErr == "")
			{
				#region 檢查頁數是否超過
				ods_Ts_QU.DataBind();
				gv_Ts_QU.DataBind();
				if (gv_Ts_QU.PageCount < gv_Ts_QU.PageIndex)
					gv_Ts_QU.PageIndex = gv_Ts_QU.PageCount;

				lb_pageid2.Text = gv_Ts_QU.PageIndex.ToString();
				#endregion
			}
			else
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");location.replace(\"B003.aspx\");", true);
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

	// 取得資料
	private bool GetData()
	{
		bool ckbool = false;
		string SqlString = "";

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
						lb_b_time.Text = DateTime.Parse(Sql_Reader["b_time"].ToString()).ToString("yyyy/MM/dd HH:mm");
						lb_e_time.Text = DateTime.Parse(Sql_Reader["e_time"].ToString()).ToString("yyyy/MM/dd HH:mm");
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
		lb_pageid2.Text = gv_Ts_QU.PageIndex.ToString();
	}

	// 每列資料的處理
	protected void gv_Ts_QU_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		string tu_sid = "", tq_type = "";

		if ((e.Row.RowType == DataControlRowType.DataRow))
		{
			tu_sid = DataBinder.Eval(e.Row.DataItem, "tu_sid").ToString();
			tq_type = DataBinder.Eval(e.Row.DataItem, "tq_type").ToString();

			if (tq_type == "0")
				e.Row.Cells[3].Text = "單選";
			else if (tq_type == "1")
				e.Row.Cells[3].Text = "複選全部";
			else
				e.Row.Cells[3].Text = "複選 " + tq_type + " 題";

			Label lb_temp = (Label)e.Row.Cells[5].FindControl("lb_is_ans");
			if (tu_sid == "-1")
			{
				lb_temp.Text = "－";
				lb_b_time.ToolTip = "尚未作答";
			}
			else
			{
				lb_temp.Text = "★";
				lb_b_time.ToolTip = "已經回答";
			}

		}
	}
}
