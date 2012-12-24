//---------------------------------------------------------------------------- 
//程式功能	POP3 收信處理 > 查看郵件內容（導引頁)
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _90026 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		int ppm_sid = -1, ppa_sid = -1;
		string SqlString = "", mErr = "", is_decode = "0";

		if (Request["sid"] != null && Request["ppa_sid"] != null)
		{
			if (int.TryParse(Request["sid"], out ppm_sid) && int.TryParse(Request["ppa_sid"], out ppa_sid))
			{
				#region 檢查是否已經經過解碼處理

				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					Sql_Conn.Open();

					SqlString = "Select Top 1 is_decode From POP3_Mail Where ppa_sid = @ppa_sid and ppm_sid = @ppm_sid";

					using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
					{
						Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
						Sql_Command.Parameters.AddWithValue("ppm_sid", ppm_sid);

						using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
						{
							if (Sql_Reader.Read())
								is_decode = Sql_Reader["is_decode"].ToString();
							else
								mErr = "找不到指定的郵件!\\n";

							Sql_Reader.Close();
						}
					}

					Sql_Conn.Close();
				}
				#endregion
			}
			else
				mErr = "參數格式錯誤!\\n";
		}
		else
			mErr = "參數傳遞錯誤!\\n";

		if (mErr == "")
		{
			if (is_decode == "0")
			{
				// 轉到 900261.ashx 解析郵件內容資料
				lb_iframe.Text="900261.ashx?sid=" + Request["sid"] + "&ppa_sid=" + Request["ppa_sid"];
			}
			else
			{
				// 轉到 900262.aspx 顯示郵件內容資料
				mErr = "900262.aspx?sid=" + Request["sid"] + "&ppa_sid=" + Request["ppa_sid"] + "&timestamp=" + DateTime.Now.ToString("mmssms");
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "parent.show_win(\"" + mErr + "\",760, 450)", true);
			}
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");parent.close_all();", true);
    }
}
