//---------------------------------------------------------------------------- 
//程式功能	行事曆管理
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class _5002 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("5002", true);

			int dWeek = 0;
			DateTime fDay = DateTime.Today;

			if (Request["dtm"] != null)
			{
				DateTime.TryParse(Request["dtm"], out fDay);
			}

			// 取得本週第一天
			dWeek = (int)fDay.DayOfWeek;
			fDay = fDay.AddDays(-1 * dWeek);
		
			lb_now.Text = fDay.ToString("yyyy/MM/dd");	// 記錄本週第一天

			cdr1.VisibleDate = fDay;					// 本月
			cdr2.VisibleDate = fDay.AddMonths(1);		// 下一個月
			cdr3.VisibleDate = fDay.AddMonths(2);		// 下二個月

			// 設定右側行事曆內容
			Set_List(fDay);
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

	// 設定右側行事曆內容
	private void Set_List(DateTime fDay)
	{
		Calendar_Func dfc = new Calendar_Func();
		int iCnt = 0;
		string SqlString = "";


		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			Sql_Conn.Open();

			using (SqlCommand Sql_Command = new SqlCommand())
			{
				SqlDataReader Sql_Reader;

				SqlString = "Select c.ca_btime, c.ca_sid, c.ca_class, g.cg_name, c.ca_subject, c.is_attach, c.init_time";
				SqlString += " From Ca_Calendar c Inner Join Ca_Group g On c.cg_sid = g.cg_sid";
				SqlString += " Where c.mg_sid = @mg_sid And Convert(NChar(10), c.ca_btime, 111) = @ca_btime";
				SqlString += " Order by c.ca_btime";

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;

				for (iCnt = 0; iCnt < 7; iCnt++)
				{
					Label lb_wk = (Label)Page.FindControl("lb_wk" + iCnt.ToString());
					Literal lt_wk = (Literal)Page.FindControl("lt_wk" + iCnt.ToString());
					DateTime nday = fDay.AddDays(iCnt);

					lb_wk.Text = nday.ToString("yyyy/MM/dd") + "<br>" + nday.ToString("dddd") + "<br><br>" + dfc.GetLunarDate(nday, "Md");

					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
					Sql_Command.Parameters.AddWithValue("ca_btime", nday.ToString("yyyy/MM/dd"));

					Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
					{
						lt_wk.Text = "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";

						do
						{
							lt_wk.Text += "<tr valign=\"top\" onclick=\"show_win('50021.aspx?sid=" + Sql_Reader["ca_sid"].ToString();
							lt_wk.Text += "&dtm=" + nday.ToString("yyyy/MM/dd") + "', 450, 600)\" onMouseOver=\"this.bgColor='#00CCFF'\" onMouseOut=\"this.bgColor='#FAFAD2'\"><td align=left style=\"width:36px\">";

							switch (Sql_Reader["ca_class"].ToString())
							{
								case "1":
									lt_wk.Text += "<img src=\"../images/ico/important.gif\" alt=\"重要\" title=\"重要\" border=0>";
									break;

								case "2":
									lt_wk.Text += "<img src=\"../images/ico/minus.gif\" alt=\"不重要\" title=\"不重要\" border=0>";
									break;

								default:
									lt_wk.Text += "<img src=\"../images/ico/normal.gif\" alt=\"普通\" title=\"普通\" border=0>";
									break;
							}

							if (Sql_Reader["is_attach"].ToString() == "1")
								lt_wk.Text += "<img src=\"../images/ico/clip.gif\" alt=\"附加檔案\" title=\"附加檔案\" border=0>";
							else
								lt_wk.Text += "<img src=\"../images/ico/normal.gif\" alt=\"無附加檔案\" title=\"無附加檔案\" border=0>";

							lt_wk.Text += "</td>";

							lt_wk.Text += "<td align=\"left\" style=\"width:60px\">" + DateTime.Parse(Sql_Reader["ca_btime"].ToString()).ToString("HH:mm") + "</td>";
							lt_wk.Text += "<td align=\"left\">" + Sql_Reader["ca_subject"].ToString().Trim() + "&nbsp;</td>";
							lt_wk.Text += "<td align=\"center\" style=\"width:60px\">" + DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd") + "</td>";
							lt_wk.Text += "<td align=\"center\" style=\"width:60px\">" + Sql_Reader["cg_name"].ToString().Trim() + "</td>";

							lt_wk.Text += "</tr>";
						} while (Sql_Reader.Read());

						lt_wk.Text += "</table>";
					}
					else
						lt_wk.Text = "&nbsp;";

					Sql_Reader.Close();
					Sql_Reader.Dispose();

					Sql_Command.Dispose();
				}
			}
		}
	}

	// 第一個月曆變更
	protected void cdr1_SelectionChanged(object sender, EventArgs e)
	{
		DateTime fDay = cdr1.SelectedDate;
		int dWeek = (int)fDay.DayOfWeek;

		cdr1.SelectedDates.Clear();
		cdr2.SelectedDates.Clear();
		cdr3.SelectedDates.Clear();
		cdr1.SelectedDate = fDay;

		// 取得本週第一天
		fDay = fDay.AddDays(-1 * dWeek);

		if (fDay.ToString() != lb_now.Text)
		{
			lb_now.Text = fDay.ToString("yyyy/MM/dd");	// 記錄本週第一天

			// 設定右側行事曆內容
			Set_List(fDay);
		}
	}

	// 第二個月曆變更
	protected void cdr2_SelectionChanged(object sender, EventArgs e)
	{
		DateTime fDay = cdr2.SelectedDate;
		int dWeek = (int)fDay.DayOfWeek;

		cdr1.SelectedDates.Clear();
		cdr2.SelectedDates.Clear();
		cdr3.SelectedDates.Clear();

		cdr2.SelectedDate = fDay;

		// 取得本週第一天
		fDay = fDay.AddDays(-1 * dWeek);

		if (fDay.ToString() != lb_now.Text)
		{
			lb_now.Text = fDay.ToString("yyyy/MM/dd");	// 記錄本週第一天

			// 設定右側行事曆內容
			Set_List(fDay);
		}
	}

	// 第三個月曆變更
	protected void cdr3_SelectionChanged(object sender, EventArgs e)
	{
		DateTime fDay = cdr3.SelectedDate;
		int dWeek = (int)fDay.DayOfWeek;

		cdr1.SelectedDates.Clear();
		cdr2.SelectedDates.Clear();
		cdr3.SelectedDates.Clear();

		cdr3.SelectedDate = fDay;

		// 取得本週第一天
		fDay = fDay.AddDays(-1 * dWeek);

		if (fDay.ToString() != lb_now.Text)
		{
			lb_now.Text = fDay.ToString("yyyy/MM/dd");	// 記錄本週第一天

			// 設定右側行事曆內容
			Set_List(fDay);
		}
	}

	// 第一個月曆月份變更
	protected void cdr1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
	{
		cdr2.VisibleDate = e.NewDate.AddMonths(1);
		cdr3.VisibleDate = e.NewDate.AddMonths(2);
	}

	// 第二個月曆月份變更
	protected void cdr2_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
	{
		cdr1.VisibleDate = e.NewDate.AddMonths(-1);
		cdr3.VisibleDate = e.NewDate.AddMonths(1);
	}

	// 第三個月曆月份變更
	protected void cdr3_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
	{
		cdr1.VisibleDate = e.NewDate.AddMonths(-2);
		cdr2.VisibleDate = e.NewDate.AddMonths(-1);

	}
}
