//---------------------------------------------------------------------------- 
//程式功能	人員資料管理 > 明細內容
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _10051 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0, mg_sid = -1;
			string SqlString = "", mErr = "";

            // 檢查使用者權限但不存入使用紀錄
			//Check_Power("1005", false);

			#region 承接上一頁的查詢條件設定

			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"].ToString(), out ckint))
					lb_page.Text = "?pageid=" + ckint.ToString();
				else
					lb_page.Text = "?pageid=" + "0";
			}

			if (Request["mg_sid"] != null)
				lb_page.Text = lb_page.Text + "&mg_sid=" + Request["mg_sid"];

			if (Request["mg_name"] != null)
				lb_page.Text = lb_page.Text + "&mg_name=" + Server.UrlEncode(Request["mg_name"]);

			if (Request["mg_nike"] != null)
				lb_page.Text = lb_page.Text + "&mg_nike=" + Server.UrlEncode(Request["mg_nike"]);

			if (Request["btime"] != null)
				lb_page.Text = lb_page.Text + "&btime=" + Server.UrlEncode(Request["btime"]);

			if (Request["etime"] != null)
				lb_page.Text = lb_page.Text + "&etime=" + Server.UrlEncode(Request["etime"]);

			#endregion

			#region 檢查傳入參數
			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"].ToString(), out mg_sid))
				{
					// 取得相關資料
					using (SqlConnection sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
					{
						sql_conn.Open();

						#region 取得人員基本資料
						SqlString = "Select Top 1 mg_sid, mg_name, mg_nike, mg_id, mg_unit, mg_desc, last_date, init_time";
						SqlString = SqlString + " From Manager Where mg_sid = " + mg_sid.ToString();

						using (SqlCommand Sql_Command = new SqlCommand(SqlString, sql_conn))
						{
							using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
							{
								if (Sql_Reader.Read())
								{
									lb_mg_sid.Text = Sql_Reader["mg_sid"].ToString();
									lb_mg_id.Text = Sql_Reader["mg_id"].ToString();
									lb_mg_name.Text = Sql_Reader["mg_name"].ToString().Trim();
									lb_mg_nike.Text = Sql_Reader["mg_nike"].ToString().Trim();
									lb_mg_unit.Text = Sql_Reader["mg_unit"].ToString().Trim();
									lb_mg_desc.Text = Sql_Reader["mg_desc"].ToString().Trim();
									if (Sql_Reader.IsDBNull(6))
										lb_last_date.Text = "[還沒有登入過]";
									else
										lb_last_date.Text = ((DateTime)Sql_Reader["last_date"]).ToString("yyyy/MM/dd hh:mm");
									lb_init_time.Text = ((DateTime)Sql_Reader["init_time"]).ToString("yyyy/MM/dd hh:mm");
								}
								else
									mErr = "找不到指定的人員資料!\\n";
							}
						}
						#endregion

						#region 取得人員權限資料
						if (mg_sid == 0)
						{
							// 關閉權限設定選項
							lk_power.Visible = false;
							lk_del.Visible = false;

							// 若為系統總管理者，擁有全部的功能執行權限
							SqlString = "Select f1.fi_name1, f2.fi_name2 From Func_Item2 f2";
							SqlString = SqlString + " Left Outer Join Func_Item1 f1 On f2.fi_no1 = f1.fi_no1";
							SqlString = SqlString + " Where f2.is_visible = 1";
						}
						else
						{
							// 一般使用者，由人員系統權限 Func_Power 資料表取得可執行的權限
							SqlString = "Select f1.fi_name1, f2.fi_name2 From Func_Power p";
							SqlString = SqlString + " Left Outer Join Func_Item2 f2 On p.fi_no2 = f2.fi_no2";
							SqlString = SqlString + " Left Outer Join Func_Item1 f1 On f2.fi_no1 = f1.fi_no1";
							SqlString = SqlString + " Where p.mg_sid = @mg_sid And p.is_enable = 1";
						}

						using (SqlCommand Sql_Command = new SqlCommand(SqlString, sql_conn))
						{
							Sql_Command.Parameters.AddWithValue("@mg_sid", mg_sid);

							using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
							{
								string spower = "", bgcolor = "", fi_name1 = "", sfi_name1;

								while (Sql_Reader.Read())
								{
									if (fi_name1 == Sql_Reader["fi_name1"].ToString().Trim())
										sfi_name1 = "";
									else
									{
										sfi_name1 = Sql_Reader["fi_name1"].ToString().Trim();
										fi_name1 = sfi_name1;

										if (bgcolor == "")
											bgcolor = " style='background-color:#99FF99'";
										else
											bgcolor = "";
									}

									spower = spower + "<tr align=left" + bgcolor + ">";
									spower = spower + "<td class=text9pt>" + sfi_name1 + "</td>";
									spower = spower + "<td class=text9pt>" + Sql_Reader["fi_name2"].ToString().Trim() + "</td>";
									spower = spower + "<td class=text9pt align=center>開放</td></tr>";
								}

								if (spower == "")
									spower = "<tr><td align=center colspan=3 class=text9pt style='height:24pt'>無任何可執行的權限!</td></tr>";

								lt_power.Text = "<table cellspacing=0 cellpadding=4 rules=all border=0 style='background-color:#F7F7DE;border-color:#003366;border-width:1px;border-style:Double;width:580pt;border-collapse:collapse;'>";
								lt_power.Text = lt_power.Text + "<tr align=center bgcolor=#FF6A04>";
								lt_power.Text = lt_power.Text + "<td class=text9pt style='color:white'>主功能</td>";
								lt_power.Text = lt_power.Text + "<td class=text9pt style='color:white'>子功能</td>";
								lt_power.Text = lt_power.Text + "<td class=text9pt style='color:white'>權限</td>";
								lt_power.Text = lt_power.Text + "</tr>";
								lt_power.Text = lt_power.Text + spower;
								lt_power.Text = lt_power.Text + "</table>";	
							}
						}
						#endregion
					}
				}
				else
					mErr = "網頁參數傳送錯誤!\\n";

				if (mErr != "")
					lt_show.Text = "<script language='javascript'>alert('" + mErr + "');history.go(-1);</script>";
				else
					lb_pg_mg_sid.Text = mg_sid.ToString();
			#endregion
			}
		}
	}

    // Check_Power() 檢查使用者權限並存入使用紀錄
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

	protected void lk_power_Click(object sender, EventArgs e)
	{
		Response.Redirect("100511.aspx" + lb_page.Text + "&sid=" + lb_pg_mg_sid.Text);
	}
}
