<%@ WebHandler Language="C#" Class="_90023" %>
//---------------------------------------------------------------------------- 
//程式功能	POP3收信處理 > 整理資料庫並取得主機訊息
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _90023 : IHttpHandler, IRequiresSessionState
{
	// class 加入「IRequiresSessionState」可讀寫 Session
	// class 加入「IReadOnlySessionState」可讀寫 Session，但寫入的狀況無法傳給其它頁面使用
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "";
		int ppa_sid = -1, mg_sid = -1, mail_num = 0, mail_total = 0, iCnt = 0;
		Email_POP3 email_pop3 = new Email_POP3();		// 自訂類別 \App_Code\Email_POP3.cs
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("9002", false, context);

		if (int.TryParse(context.Session["ppa_sid"].ToString(), out ppa_sid))
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
				if (email_pop3.Login() == 0)
				{
					// 取得目前郵件狀態及明細
					iCnt = email_pop3.StatAll();
					if (iCnt == 0)
					{
						mail_num = email_pop3.Mail_Num;
						mail_total = email_pop3.Mail_Total;
					}
					else
						mErr = iCnt.ToString() + "取得資料錯誤!\\n";
				}
				else
					mErr = "郵件主機登入失敗!\\n" ;
			}
			else
				mErr = "郵件主機連線失敗!\\n";
			#endregion

			if (mErr == "")
			{
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						#region 更新資料庫郵件接收旗標
						Sql_Conn.Open();

						SqlString = "Update POP3_Mail Set is_receive = 0 Where ppa_sid = @ppa_sid";

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);

						Sql_Command.ExecuteNonQuery();

						Sql_Conn.Close();
						#endregion

						#region 收到的郵件訊息存入 POP3_Account、POP3_Mail
						Sql_Conn.Open();
						
						SqlString = "Update POP3_Account Set ppa_num = @ppa_num, ppa_size = @ppa_size, get_time = getdate()";
						SqlString += " Where ppa_sid = @ppa_sid And mg_sid = @mg_sid;";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("mg_sid", mg_sid);
						Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
						Sql_Command.Parameters.AddWithValue("ppa_num", mail_num);
						Sql_Command.Parameters.AddWithValue("ppa_size", mail_total);

						Sql_Command.ExecuteNonQuery();
						
						if (mail_num > 0)
						{
							#region 檢查資料庫中是否已有此郵件識別碼。若無則新增，若有則更新旗標臨時序號
							SqlString = "Execute dbo.p_POP3_Mail_Flag @ppa_sid, @ppm_id, @ppm_size, @ppm_sn;";

							Sql_Command.CommandText = SqlString;

							for (iCnt = 0; iCnt < mail_num; iCnt++)
							{
								Sql_Command.Parameters.Clear();
								Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
								Sql_Command.Parameters.AddWithValue("ppm_id", email_pop3.Mail_UIDL(iCnt));
								Sql_Command.Parameters.AddWithValue("ppm_size", email_pop3.Mail_Size(iCnt));
								Sql_Command.Parameters.AddWithValue("ppm_sn", iCnt + 1);

								Sql_Command.ExecuteNonQuery();
							}
							#endregion
						}
						
						Sql_Conn.Close();
						#endregion

						#region 清除資料庫中過期的郵件資料
						Sql_Conn.Open();

						SqlString = "Delete From POP3_Mail Where ppa_sid = @ppa_sid And is_receive <> 1;";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);

						Sql_Command.ExecuteNonQuery();

						Sql_Conn.Close();
						#endregion

						#region 清除資料庫中不存在的附件資料
						Sql_Conn.Open();

						SqlString = "Delete From POP3_Attach Where ppa_sid = @ppa_sid And ppm_sid Not in (Select ppm_sid From POP3_Mail Where ppa_sid = @ppa_sid);";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);

						Sql_Command.ExecuteNonQuery();

						Sql_Conn.Close();
						#endregion
					}
				}
			}
		}
		else
			mErr = "參數傳遞錯誤!\\n";

		email_pop3.Close();
		

		// 設定輸出格式
		context.Response.ContentType = "text/html";
		if (mErr == "")
		{
			if (mail_num == 0)
			{
				// 沒有郵件
				context.Response.Write("<script type=\"text/javascript\">alert(\"郵件主機內無任何信件可收!\\n\");parent.close_all();parent.rece_close();</script>");
			}
			else
			{
				// 開始接收郵件
				context.Response.Write("<script type=\"text/javascript\">parent.rece_process(" + mail_num.ToString() + ", 0, \"開始接收第 1 封郵件...\");location.replace(\"90024.ashx?num=" + mail_num.ToString() + "&cnt=0\");</script>");
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