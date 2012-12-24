//---------------------------------------------------------------------------- 
//程式功能	線上客服-客服人員端 > 對話視窗 > 顯示訊息
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _700212 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
    {
		string mErr = "", SqlString = "", Cs_Message = "";
		int cu_sid = -1;

		if (!IsPostBack)
		{
			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("7002", false);

			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out cu_sid))
				{
					lb_cu_sid.Text = cu_sid.ToString();
					lb_mg_sid.Text = Session["mg_sid"].ToString();

					using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
					{
						using (SqlCommand Sql_Command = new SqlCommand())
						{
							Sql_Conn.Open();
							SqlString = "Select Top 1 u.cu_name, m.mg_name From Cs_User u";
							SqlString += " Inner Join Manager m On u.mg_sid = m.mg_sid";
							SqlString += " Where u.cu_sid = @cu_sid And u.mg_sid = @mg_sid";

							Sql_Command.Connection = Sql_Conn;
							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.AddWithValue("cu_sid", cu_sid);
							Sql_Command.Parameters.AddWithValue("mg_sid", lb_mg_sid.Text);

							using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
							{
								if (Sql_Reader.Read())
								{
									lb_cu_name.Text = Sql_Reader["cu_name"].ToString().Trim();
									lb_mg_name.Text = Sql_Reader["mg_name"].ToString().Trim();
									mErr = "";
								}
								else
									mErr = "找不到客服要求紀錄!\\n";

								Sql_Reader.Close();
							}
						}
					}

					#region 取得所有客服交談紀錄
					if (mErr == "")
					{
						Cs_Message = Get_Cs_Message();
						lt_data.Text = Cs_Message.Substring(1, Cs_Message.Length - 1);

						if (Cs_Message.Substring(0, 1) == "2")
							mErr = "交談已結束!\\n";
					}
					#endregion
				}
			}

			if (mErr != "")
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");location.replace(\"7002.aspx\");", true);
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

	// 定期輪詢取得資料
	protected void ti_getdata_Tick(object sender, EventArgs e)
	{
		String_Func sfc = new String_Func();
		string Cs_Message = "";
		int ckcs = 0;
		int iCnt = int.Parse(lb_count.Text) + 1;

		Cs_Message = Get_Cs_Message();
		if (Cs_Message.Length > 2)
		{
			ckcs = int.Parse(sfc.Left(Cs_Message, 1));
			lt_data.Text = Cs_Message.Substring(1, Cs_Message.Length - 1) + lt_data.Text;
		}

		if (ckcs != 2)
		{
			if (iCnt > 80)
			{
				// 每輪詢 80次 (1.5秒 * 80)，更新客戶服務要求時間，以確定客戶確實在線上
				// 同時並確定客服人員也在線上
				ckcs = Renew_Cs_User();
				iCnt = 0;
			}
			lb_count.Text = iCnt.ToString();
		}

		if (ckcs != 0)
		{
			if (ckcs == 1)
			{
				lt_data.Text = "<p align=\"center\" style=\"margin:5px 0px 5px 0px\">※ 客戶已無回應，返回客戶狀況監控 ※<br><br><br><a href=\"7002.aspx\" class=\"abtn\" target=\"_parent\">&nbsp;返回客戶狀況監控&nbsp;</a></p><hr>" + lt_data.Text;
			}
			else
			{
				lt_data.Text = "<p align=\"center\" style=\"margin:5px 0px 5px 0px\">※ 對話已經結束，返回客戶狀況監控 ※<br><br><br><a href=\"7002.aspx\" class=\"abtn\" target=\"_parent\">&nbsp;返回客戶狀況監控&nbsp;</a></p><hr>" + lt_data.Text;
			}

			// 結束本次交談
			End_Cs_User();

			// 停止輪詢
			ti_getdata.Enabled = false;
		}
	}

	// 取得客服交談紀錄
	private string Get_Cs_Message()
	{
		string mData = "", SqlString = "", cm_sid = "";
		bool ckend = false;

		cm_sid = lb_cm_sid.Text;

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				Sql_Conn.Open();
				SqlString = "Select cm_sid, cm_time, cm_object, cm_desc, cm_fname, cm_fsize, cm_end From Cs_Message";
				SqlString += " Where cu_sid = @cu_sid And cm_sid > @cm_sid Order by cm_sid DESC";

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;
				Sql_Command.Parameters.AddWithValue("cu_sid", lb_cu_sid.Text);
				Sql_Command.Parameters.AddWithValue("cm_sid", cm_sid);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						lb_cm_sid.Text = Sql_Reader["cm_sid"].ToString();
						do
						{
							cm_sid = Sql_Reader["cm_sid"].ToString();

							if (Sql_Reader["cm_object"].ToString() == "1")
								mData += "<b style=\"color:blue\">" + lb_cu_name.Text + "</b>";
							else
								mData += "<b style=\"color:blue\">" + lb_mg_name.Text + "</b>";

							mData += "&nbsp;說(" + DateTime.Parse(Sql_Reader["cm_time"].ToString()).ToString("HH:mm:ss") + ")：<br>";

							if (Sql_Reader["cm_fname"].ToString() == "")
							{
								mData += Sql_Reader["cm_desc"].ToString() + "<hr>";
							}
							else
							{
								mData += "檔案：<a href=\"700213.ashx?sid=" + cm_sid + "&cu_sid=" + lb_cu_sid.Text;
								mData += "\" title=\"請點我下載檔案\" target=\"_blank\">" + Sql_Reader["cm_fname"].ToString();
								mData += "&nbsp;(" + int.Parse(Sql_Reader["cm_fsize"].ToString()).ToString("N0") + "bytes)</a><hr>";
							}

							if (Sql_Reader["cm_end"].ToString() == "1")
								ckend = true;

						} while (Sql_Reader.Read());

						// 結束旗標
						if (ckend)
							mData = "2" + mData;
						else
							mData = "0" + mData;
					}

					Sql_Reader.Close();
				}
			}
		}

		return mData;
	}

	// 更新客戶服務要求時間，以確定客服確實在線上，同時並確定客戶也在線上
	private int Renew_Cs_User()
	{
		int p_wait_time = 600;		// 等待客服回應時間(秒)，超過代表客服已無連線
		int ckcs = 0;
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				Sql_Conn.Open();
				SqlString = "Update Cs_User Set cu_stime = getdate() Where cu_sid = @cu_sid And mg_sid = @mg_sid;";
				SqlString += "Select Top 1 cu_rtn, DateDiff(s, cu_utime, getdate()) as sec From Cs_User Where cu_sid = @cu_sid And mg_sid = @mg_sid;";

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;
				Sql_Command.Parameters.AddWithValue("cu_sid", lb_cu_sid.Text);
				Sql_Command.Parameters.AddWithValue("mg_sid", lb_mg_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						if (int.Parse(Sql_Reader["sec"].ToString()) > p_wait_time)
						{
							// 等待時間超過
							ckcs = 1;
						}
						else if (Sql_Reader["cu_rtn"].ToString() == "2")
						{
							// 已回應結束
							ckcs = 2;
						}
					}

					Sql_Reader.Close();
					Sql_Reader.Dispose();
				}				

				Sql_Command.Dispose();
				Sql_Conn.Close();
			}
		}

		return ckcs;
	}

	// 結束本次交談
	private void End_Cs_User()
	{
		string SqlString = "";

		#region 更新回應狀態旗標
		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				Sql_Conn.Open();
				SqlString = "Update Cs_User Set cu_rtn = 2 Where cu_sid = @cu_sid And mg_sid = @mg_sid";

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;
				Sql_Command.Parameters.AddWithValue("cu_sid", lb_cu_sid.Text);
				Sql_Command.Parameters.AddWithValue("mg_sid", lb_mg_sid.Text);

				Sql_Command.ExecuteNonQuery();
				Sql_Command.Dispose();
			}
		}
		#endregion
	}
}
