//---------------------------------------------------------------------------- 
//程式功能	考試成績統計 > 試題成績分佈
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _B0042 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string mErr = "";

		if (!IsPostBack)
		{
			int tp_sid = -1;	

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("B004", false);

			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out tp_sid))
				{
					lb_tp_sid.Text = tp_sid.ToString();

					// 取得資料
					if (! GetData())
						mErr = "找不到相關資料!\\n";

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
					
					if (Request["btime"] != null)
						lb_page.Text += "&btime=" + Server.UrlEncode(Request["btime"].Trim());

					if (Request["btime"] != null)
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
			Build_BarChart();
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");location.replace(\"B004.aspx" + lb_page.Text + "\");", true);

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

						if (lb_tp_member.Text == "0")
							lb_tp_avg.Text = "0.0000";
						else
							lb_tp_avg.Text = (float.Parse(lb_tp_total.Text) / float.Parse(lb_tp_member.Text)).ToString("F4");

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

	// 產生統計圖表
	private void Build_BarChart()
	{
		string SqlString = "", mErr = "";
		int maxcnt = 0, icnt = 0;
		SqlDataReader Sql_Reader;

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				Sql_Command.Connection = Sql_Conn;

				Sql_Conn.Open();

				#region 取得最大值
				SqlString = "Select IsNull(Max(tq_total/tq_score), 0) as umax From Ts_Question";
				SqlString += " Where tp_sid = @tp_sid;";

				Sql_Command.CommandText = SqlString;
				Sql_Command.Parameters.Clear();
				Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
				Sql_Reader = Sql_Command.ExecuteReader();

				if (Sql_Reader.Read())
				{
					maxcnt = int.Parse(Sql_Reader["umax"].ToString());
				}
				else
					mErr = "※ 目前還沒有考試資料 ※";

				Sql_Reader.Close();
				#endregion

				#region 產生 Bar Char
				if (mErr == "")
				{
					SqlString = "Select tq_sort, tq_desc, tq_member, tq_total/tq_score as cnt From Ts_Question";
					SqlString += " Where tp_sid = @tp_sid Order by tq_total/tq_score DESC, tq_sort";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
					Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
					{
						do
						{
							Table tbl_tmp = new Table();
							TableRow tr_name = new TableRow();
							TableRow tr_bar = new TableRow();
							TableCell tr_sort_cell = new TableCell();
							TableCell tr_name_cell = new TableCell();
							TableCell tr_bar_cell_empty = new TableCell();
							TableCell tr_bar_cell = new TableCell();

							Table tbl_bar = new Table();
							TableRow tr_bar_tmp = new TableRow();
							TableCell tr_bar_cell_tmp = new TableCell();

							icnt = int.Parse(Sql_Reader["cnt"].ToString());

							tbl_tmp.Style.Add("width", "100%");
							tbl_tmp.Style.Add("margin", "4pt 0pt 6pt 0pt");

							tr_sort_cell.Text = Sql_Reader["tq_sort"].ToString() + ". ";
							tr_sort_cell.HorizontalAlign = HorizontalAlign.Right;
							tr_sort_cell.VerticalAlign = VerticalAlign.Top;
							tr_sort_cell.Width = 25;

							tr_name_cell.Text = Sql_Reader["tq_desc"].ToString().Replace("\r\n","<br>") + "<br>　";
							tr_name_cell.Text += "答題人數：" + Sql_Reader["tq_member"].ToString() + " 人，";
							tr_name_cell.Text += "答對人數：" + Sql_Reader["cnt"].ToString() + " 人。";
							tr_name_cell.HorizontalAlign = HorizontalAlign.Left;

							tr_name.Controls.Add(tr_sort_cell);
							tr_name.Controls.Add(tr_name_cell);

							tbl_bar.Style.Add("width", (100 * icnt / maxcnt).ToString() + "%");
							tbl_bar.Style.Add("border-bottom-color", "Black");
							tbl_bar.Style.Add("border-left-color", "White");
							tbl_bar.Style.Add("border-right-color", "Black");
							tbl_bar.Style.Add("border-top-color", "White");
							tbl_bar.BackColor = Color.FromArgb(128, 128, 00);
							tbl_bar.BorderWidth = 2;
							tbl_bar.CellPadding = 0;
							tbl_bar.CellSpacing = 0;

							tr_bar_cell_tmp.Style.Add("height", "10pt");
							tr_bar_tmp.Controls.Add(tr_bar_cell_tmp);
							tbl_bar.Controls.Add(tr_bar_tmp);

							//tr_bar_cell.Text = "<table border=2 cellpadding=0 cellspacing=0 style=\"width:" + (100 * icnt / maxcnt).ToString() + "%; background-color:#808000; border-bottom-color:Black; border-left-color:White; border-right-color:Black; border-top-color:White;\">";
							//tr_bar_cell.Text += "<tr><td style=\"height:10pt;\"></td></tr></table>";

							tr_bar_cell.Controls.Add(tbl_bar);
							tr_bar_cell.HorizontalAlign = HorizontalAlign.Left;

							tr_bar_cell_empty.Text = "&nbsp;";

							tr_bar.Controls.Add(tr_bar_cell_empty);
							tr_bar.Controls.Add(tr_bar_cell);

							tbl_tmp.Controls.Add(tr_name);
							tbl_tmp.Controls.Add(tr_bar);
							pl_show.Controls.Add(tbl_tmp);
						} while (Sql_Reader.Read());
					}
					else
						mErr = "※ 目前還沒有考試資料 ※";

					Sql_Reader.Close();
				}
				#endregion

				Sql_Conn.Close();
			}
		}

		if (mErr != "")
		{
			Label lb_tmp = new Label();

			lb_tmp.Text = mErr;

			pl_show.Controls.Add(lb_tmp);
			pl_show.HorizontalAlign = HorizontalAlign.Center;
		}
	}
}
