<%@ WebHandler Language="C#" Class="_90027" %>
//---------------------------------------------------------------------------- 
//程式功能	POP3收信處理 > 刪除郵件
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _90027 : IHttpHandler, IRequiresSessionState
{
	// class 加入「IRequiresSessionState」可讀寫 Session
	// class 加入「IReadOnlySessionState」可讀寫 Session，但寫入的狀況無法傳給其它頁面使用
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "";
		int ppm_sid = -1, ppa_sid = -1, ckint = 0;
		string ppa_host = "", ppa_port = "", ppa_id = "", ppa_pw = "", ppm_id = "";
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("9002", false, context);

		if (context.Request["sid"] != null && context.Request["ppa_sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out ppm_sid) && int.TryParse(context.Request["ppa_sid"], out ppa_sid))
			{
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						SqlDataReader Sql_Reader;
						string SqlString = "";

						Sql_Conn.Open();
						Sql_Command.Connection = Sql_Conn;

						#region 取得郵件識別碼
						SqlString = "Select Top 1 ppm_id From POP3_Mail Where ppa_sid = @ppa_sid And ppm_sid = @ppm_sid";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
						Sql_Command.Parameters.AddWithValue("ppm_sid", ppm_sid);

						Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							// 讀取資料庫
							ppm_id = Sql_Reader["ppm_id"].ToString().Trim();

							if (ppm_id.Length != 16)
								mErr = "郵件識別碼格式有誤";
						}
						else
							mErr = "找不到郵件識別碼!\\n";

						Sql_Reader.Close();
						#endregion

						#region 刪除 POP3_Mail 及 POP3_Attach 的紀錄
						if (mErr == "")
						{
							SqlString = "Delete From POP3_Mail Where ppa_sid = @ppa_sid And ppm_sid = @ppm_sid;";
							SqlString += "Delete From POP3_Attach Where ppa_sid = @ppa_sid And ppm_sid = @ppm_sid;";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
							Sql_Command.Parameters.AddWithValue("ppm_sid", ppm_sid);

							Sql_Command.ExecuteNonQuery();
						}
						#endregion

						#region 重新計算郵件數量及大小 POP3_Account
						if (mErr == "")
						{
							SqlString = "Execute dbo.p_POP3_Account_Calc @ppa_sid, @mg_sid";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
							Sql_Command.Parameters.AddWithValue("mg_sid", context.Session["mg_sid"].ToString());
							
							Sql_Command.ExecuteNonQuery();
						}
						#endregion

						#region 取得主機名稱及帳號密碼
						if (mErr == "")
						{
							SqlString = "Select Top 1 ppa_host, ppa_port, ppa_id, ppa_pw, is_delete From POP3_Account Where ppa_sid = @ppa_sid And mg_sid = @mg_sid";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
							Sql_Command.Parameters.AddWithValue("mg_sid", context.Session["mg_sid"].ToString());

							Sql_Reader = Sql_Command.ExecuteReader();

							if (Sql_Reader.Read())
							{
								// 讀取資料庫
								ppa_host = Sql_Reader["ppa_host"].ToString().Trim();
								ppa_port = Sql_Reader["ppa_port"].ToString();
								ppa_id = Sql_Reader["ppa_id"].ToString().Trim();
								ppa_pw = Sql_Reader["ppa_pw"].ToString().Trim();

								if (ppa_host == "" || ppa_port == "" || ppa_id == "" || ppa_pw == "")
									mErr = "主機資料不完整，請重新設定!\\n";
							}
							else
								mErr = "找不到指定的主機設定資料!\\n";

							Sql_Reader.Close();
						}
						#endregion

						#region 刪除主機上的郵件
						if (mErr == "")
						{
							Email_POP3 email_pop3 = new Email_POP3();		// 自訂類別 \App_Code\Email_POP3.cs

							email_pop3.POP3_Host = ppa_host;
							email_pop3.POP3_Port = int.Parse(ppa_port);
							email_pop3.POP3_ID = ppa_id;
							email_pop3.POP3_PW = ppa_pw;
							
							// 建立郵件主機連線，並取得資料
							if (email_pop3.Connect() == 0)
							{
								// 登入郵件主機
								if (email_pop3.Login() == 0)
								{
									// 刪除郵件
									ckint = email_pop3.Dele_UIDL(ppm_id);
									if (ckint == 0)
										mErr = "郵件刪除完成!\\n";
									else
										mErr = ckint.ToString() + "主機郵件刪除錯誤!\\n";
								}
								else
									mErr = "郵件主機登入失敗!\\n";
							}
							else
								mErr = "郵件主機連線失敗!\\n";

							email_pop3.Close();
						}
						#endregion

						Sql_Reader.Dispose();
						Sql_Conn.Close();
					}
				}
			}
			else
				mErr = "郵件刪除錯誤!\\n";		
		}
		else
			mErr = "郵件刪除錯誤!\\n";

		// 設定輸出格式
		context.Response.ContentType = "text/html";
		context.Response.Write("<script type=\"text/javascript\">alert(\"" + mErr + "\");parent.location.reload(true);</script>");
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