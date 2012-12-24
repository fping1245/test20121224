//---------------------------------------------------------------------------- 
//程式功能	票選結果統計 > 目前票選結果
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _A0021 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int bh_sid = -1;
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限不存入登入紀錄
			//Check_Power("A001", false);

			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out bh_sid))
				{
					lb_bh_sid.Text = bh_sid.ToString();

					// 取得問卷主題資料
					if (!GetData())
						mErr = "找不到相關資料!\\n";
				}
				else
					mErr = "參數格式錯誤!\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";

			if (mErr == "")
				Build_BarChart();
			else
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
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

	// 取得問卷主題資料
	private bool GetData()
	{
		bool ckbool = false;
		string SqlString = "", is_check = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 bh_title, is_check, bh_topic, bh_scnt, bh_acnt, bh_total, bh_time";
			SqlString += " From Bt_Head Where bh_sid = @bh_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("bh_sid", lb_bh_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						lb_bh_title.Text = Sql_Reader["bh_title"].ToString().Trim();
						lb_bh_topic.Text = Sql_Reader["bh_topic"].ToString().Trim().Replace("\r\n", "<br>");
						is_check = Sql_Reader["is_check"].ToString();

						if (is_check == "0")
							lb_is_check.Text = "單選";
						else if (is_check == "1")
							lb_is_check.Text = "任意複選";
						else
							lb_is_check.Text = "複選 1 ~ " + is_check + " 題";

						lb_bh_scnt.Text = int.Parse(Sql_Reader["bh_scnt"].ToString()).ToString("N0");
						lb_bh_acnt.Text = int.Parse(Sql_Reader["bh_acnt"].ToString()).ToString("N0");
						lb_bh_total.Text = int.Parse(Sql_Reader["bh_total"].ToString()).ToString("N0");
						if (Sql_Reader["bh_time"].ToString() == "")
							lb_bh_time.Text = "&nbsp;";
						else
							lb_bh_time.Text = DateTime.Parse(Sql_Reader["bh_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

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

	// 產生票選項目直條圖
	private void Build_BarChart()
	{
		string SqlString = "";
		int bi_total = 0, bh_total = 0, max_num = 0, tbwidth = 0;
		float bi_percent = 0.0f;

		lt_stat.Text = "<table border=\"1\" cellpadding=\"6\" cellspacing=\"0\" style=\"width:100%; background-color:#FFFFE0; margin-bottom:5pt\">";
		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select bi_desc, bi_total From Bt_Item Where bh_sid = @bh_sid Order by bi_total DESC";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("bh_sid", lb_bh_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						bh_total = int.Parse(lb_bh_total.Text);
						if (bh_total == 0)
							bh_total = 1;

						max_num = int.Parse(Sql_Reader["bi_total"].ToString());
						if (max_num == 0)
							max_num = 1;

						do
						{
							bi_total = int.Parse(Sql_Reader["bi_total"].ToString());
							bi_percent = 100.0f * bi_total / bh_total;
							tbwidth = 100 * bi_total / max_num;
							if (tbwidth < 1)
								tbwidth = 1;

							lt_stat.Text += "<tr><td align=left>‧" + Sql_Reader["bi_desc"].ToString().Trim() + "：" + bi_total.ToString("N0") + " 票 (" + bi_percent.ToString("N2") + "%)<br>" ;
							lt_stat.Text += "<table border=2 cellpadding=0 cellspacing=0 style=\"width:" + tbwidth.ToString() + "%; background-color:#808000; border-bottom-color:Black; border-left-color:White; border-right-color:Black; border-top-color:White;\">";
							lt_stat.Text += "<tr><td style=\"height:10pt;\"></td></tr></table>";

							lt_stat.Text += "</td></tr>";
						} while (Sql_Reader.Read());
					}
					else
						lt_stat.Text += "<tr><td align=center><font color=red><b>找不到票選項目資料!</b></font></td></tr>";
				}
				Sql_Conn.Close();
			}
		}

		lt_stat.Text += "</table>";
	}
}
