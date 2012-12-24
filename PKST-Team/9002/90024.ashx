<%@ WebHandler Language="C#" Class="_90023" %>
//---------------------------------------------------------------------------- 
//程式功能	POP3收信處理 > 接收郵件並對標題、收信人、寄信人及日期解碼
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _90023 : IHttpHandler, IRequiresSessionState
{
	// class 加入「IRequiresSessionState」可讀寫 Session
	// class 加入「IReadOnlySessionState」可讀寫 Session，但寫入的狀況無法傳給其它頁面使用
    public void ProcessRequest (HttpContext context)
	{
		bool is_receive = false;
		string mErr = "", ppm_content = "";
		int ppa_sid = -1, mg_sid = -1, mail_num = 0, ppm_sn = 0;
		string r_name = "", r_email = "", r_time = "", s_name = "", s_email = "", s_time = "", ppm_subject = "";
		string SqlString = "";
		Email_POP3 email_pop3 = new Email_POP3();		// 自訂類別 App_Code/Email_POP3.cs
		String_Func sfc = new String_Func();
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("9002", false, context);

		int.TryParse(context.Request["num"], out mail_num);
		int.TryParse(context.Request["cnt"], out ppm_sn);

		if (mail_num < 1 || ppm_sn > mail_num)
			mErr = "參數傳送錯誤!\\n";
		else if (mail_num == ppm_sn)
			mErr = "郵件已接收完畢!\\n";

		if (mErr == "")
		{
			if (int.TryParse(context.Session["ppa_sid"].ToString(), out ppa_sid))
			{
				ppm_sn++;		// 接收下一筆郵件
				
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						SqlDataReader Sql_Reader;
						
						Sql_Command.Connection = Sql_Conn;
						
						#region 檢查郵件是否已接收過
						Sql_Conn.Open();
						
						SqlString = "Select Top 1 s_name From POP3_Mail Where ppa_sid = @ppa_sid And ppm_sn = @ppm_sn;";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
						Sql_Command.Parameters.AddWithValue("ppm_sn", ppm_sn);

						Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							if (Sql_Reader["s_name"].ToString() == "待處理...")
								is_receive = true;
							else
								is_receive = false;
						}
						Sql_Reader.Close();
						
						Sql_Conn.Close();
						#endregion

						// 尚未接收郵件內容，向郵件主機取得郵件資料
						if (is_receive)
						{
							mg_sid = int.Parse(context.Session["mg_sid"].ToString());

							#region 取得郵件主機資料

							email_pop3.POP3_Host = context.Session["ppa_host"].ToString();
							email_pop3.POP3_Port = int.Parse(context.Session["ppa_port"].ToString());
							email_pop3.POP3_ID = context.Session["ppa_id"].ToString();
							email_pop3.POP3_PW = context.Session["ppa_pw"].ToString();

							// 建立郵件主機連線，並取得資料
							if (email_pop3.Connect() == 0)
							{
								// 登入郵件主機
								if (email_pop3.Login() != 0)
									mErr = "郵件主機登入失敗!\\n";
							}
							else
								mErr = "郵件主機連線失敗!\\n";
							#endregion

							#region 取得郵件全部內容及解析郵件檔頭資料
							if (mErr == "")
							{
								// 郵件內容
								ppm_content = email_pop3.Get_Mail(ppm_sn);
								
								if (ppm_content.Contains("<Error "))
									ppm_content = "";
								
								// 解析郵件檔頭
								Email_Decode email_dc = new Email_Decode();

								if (ppm_content == "")
									ppm_subject = "";
								else
								{
									email_dc.Topic_Analytic(ppm_content);
									ppm_subject = email_dc.Mail_Subject;

									r_name = sfc.Left(email_dc.Mail_To_Name,100);
									r_email = sfc.Left(email_dc.Mail_To_EMail,100);
									r_time = email_dc.Mail_To_Time;

									s_name = sfc.Left(email_dc.Mail_From_Name,100);
									s_email = sfc.Left(email_dc.Mail_From_EMail,100);
									s_time = email_dc.Mail_From_Time;
								}	
							}
							#endregion

							#region 存入資料庫
							if (mErr == "")
							{
								Sql_Conn.Open();

								SqlString = "Update POP3_Mail Set ppm_subject = @ppm_subject, ppm_content = @ppm_content";
								SqlString += ", r_name = @r_name, r_email = @r_email, r_time = @r_time";
								SqlString += ", s_name = @s_name, s_email = @s_email, s_time = @s_time, init_time = getdate()";
								SqlString += " Where ppa_sid = @ppa_sid And ppm_sn = @ppm_sn";

								Sql_Command.CommandText = SqlString;
								Sql_Command.Parameters.Clear();
								Sql_Command.Parameters.AddWithValue("ppm_subject", ppm_subject);
								Sql_Command.Parameters.AddWithValue("ppm_content", ppm_content);
								Sql_Command.Parameters.AddWithValue("r_name", r_name);
								Sql_Command.Parameters.AddWithValue("r_email", r_email);
								Sql_Command.Parameters.AddWithValue("r_time", r_time);
								Sql_Command.Parameters.AddWithValue("s_name", s_name);
								Sql_Command.Parameters.AddWithValue("s_email", s_email);
								Sql_Command.Parameters.AddWithValue("s_time", s_time);
								Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
								Sql_Command.Parameters.AddWithValue("ppm_sn", ppm_sn);

								Sql_Command.ExecuteNonQuery();

								Sql_Conn.Close();
							}
							#endregion

							email_pop3.Close();
						}
					}
				}
			}
			else
				mErr = "參數傳遞錯誤!\\n";
		}

		// 設定輸出格式
		context.Response.ContentType = "text/html";
		if (mErr == "")
		{
			if (mail_num == 0 || (mail_num == ppm_sn))
			{
				// 沒有郵件
				if (mail_num == 0)
					context.Response.Write("<script type=\"text/javascript\">alert(\"郵件主機內已無任何信件可收!\\n\");parent.close_all();parent.rece_close();</script>");
				else
					context.Response.Write("<script type=\"text/javascript\">parent.rece_process(" + mail_num.ToString() + "," + mail_num.ToString() + ", \"全部接收完成!\");location.replace(\"90025.htm\");</script>");
			}
			else
			{
				// 開始接收郵件
				context.Response.Write("<script type=\"text/javascript\">parent.rece_process(" + mail_num.ToString() + "," + ppm_sn.ToString() + ", \"開始接收第 " + (ppm_sn + 1).ToString() + " 封郵件......\");location.replace(\"90024.ashx?num=" + mail_num.ToString() + "&cnt=" + ppm_sn.ToString() + "\");</script>");
			}
		}
		else
		{
			context.Response.Write("<script type=\"text/javascript\">alert(\"" + mErr + "\");parent.close_all();parent.rece_close();</script>");
		}
		context.Response.End();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

	#region 檢查使用者權限，且存入登入紀錄
	private void Check_Power(string f_power, bool bl_save, HttpContext context)
	{
		// 載入公用函數
		Common_Func cfc = new Common_Func();

		// 若 Session 不存在則直接顯示錯誤訊息
		try
		{
			if (cfc.Check_Power(context.Session["mg_sid"].ToString(), context.Session["mg_name"].ToString(), context.Session["mg_power"].ToString(), f_power, context.Request.ServerVariables["REMOTE_ADDR"], bl_save) > 0)
				context.Response.Redirect("../Error.aspx?ErrCode=1");
		}
		catch
		{
			context.Response.Redirect("../Error.aspx?ErrCode=2");
		}
	}
	#endregion
}