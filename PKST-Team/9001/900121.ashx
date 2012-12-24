<%@ WebHandler Language="C#" Class="_900121" %>
//---------------------------------------------------------------------------- 
//程式功能	廣告信發送管理 (廣告信清單) > 發送處理 > 發送郵件
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間

public class _900121 : IHttpHandler, IRequiresSessionState
{
	public void ProcessRequest(HttpContext context)
	{
		int adm_sid = 0, adm_type = 0, adm_total = 0, adm_batch = 10, adm_wait = 1, adm_send = 0, adm_error = 0, ecnt = 0, gcnt = 0;
		string mErr = "", SqlString = "", adm_title = "", adm_content = "", adm_fname = "", adm_fmail = "";
		string listmail = "", badmail = "", goodmail = "";
		string[] mailto = null;
		int smtp_Port = 0;
		string smtp_Host = "", smtp_ID = "", smtp_PW = "";
		Check_Internet cknet = new Check_Internet();

		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("9001", false, context);

		if (context.Request["adm_sid"] == null)
		{
			mErr = "參數傳送錯誤!\\n";
		}
		else if (int.TryParse(context.Request["adm_sid"], out adm_sid))
		{
			if (context.Request["adm_batch"] == null)
				mErr += "請輸入批次發送數量!\\n";
			else if (int.TryParse(context.Request["adm_batch"], out adm_batch))
			{
				if (adm_batch < 1 || adm_batch > 254)
					mErr += "批次發送數量請輸入 1 ~ 254 之間的數字!\\n";
			}
			else
				mErr += "請輸入正確的批次發送數量!\\n";

			if (context.Request["adm_fname"] == null)
				mErr += "請輸入正確的發信者姓名!\\n";
			else
			{
				adm_fname = context.Request["adm_fname"].ToString().Trim();

				if (adm_fname == "")
					mErr += "發信姓名不可為空白!\\n";
			}

			if (context.Request["adm_fmail"] == null)
				mErr += "請輸入正確的發信者信箱!\\n";
			else
			{
				adm_fmail = context.Request["adm_fmail"].ToString().Trim();
				if (adm_fmail == "")
					mErr += "發信信箱不可為空白!\\n";
			}

			if (context.Request["adm_wait"] == null)
				mErr += "請輸入每批發送遲延秒數!\\n";
			else if (int.TryParse(context.Request["adm_wait"], out adm_wait))
			{
				if (adm_wait < 0 || adm_wait > 254)
					mErr += "每批發送遲延秒數請輸入 0 ~ 254 之間的秒數!\\n";
			}
			else
				mErr += "請輸入正確的每批發送遲延秒數!\\n";

			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					SqlDataReader Sql_Reader;

					Sql_Command.Connection = Sql_Conn;

					#region 取得郵件相關資料
					SqlString = "Select Top 1 adm_title, adm_type, adm_content, adm_total, adm_send, adm_error";
					SqlString += " From Ad_Mail Where adm_sid = @adm_sid";

					Sql_Conn.Open();
					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("adm_sid", adm_sid);
					Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
					{
						adm_title = Sql_Reader["adm_title"].ToString().Trim();
						adm_content = Sql_Reader["adm_content"].ToString().Trim();
						adm_type = int.Parse(Sql_Reader["adm_type"].ToString());
						adm_total = int.Parse(Sql_Reader["adm_total"].ToString());
						adm_send = int.Parse(Sql_Reader["adm_send"].ToString());
						adm_error = int.Parse(Sql_Reader["adm_error"].ToString());

						if (adm_total == 0)
							mErr = "發送電子信箱尚未設定!\\n";
					}
					else
						mErr = "找不到指定的郵件!\\n";

					Sql_Reader.Close();
					Sql_Reader.Dispose();
					#endregion


					if (mErr == "")
					{
						#region 依指定數量取得郵件清單
						SqlString = "Select Top " + adm_batch.ToString() + " adl_sid, adl_email From Ad_List Where adm_sid = @adm_sid And adl_send = 0";

						Sql_Command.Parameters.Clear();
						Sql_Command.CommandText = SqlString;

						Sql_Command.Parameters.AddWithValue("adm_sid", adm_sid);

						Sql_Reader = Sql_Command.ExecuteReader();

						while (Sql_Reader.Read())
						{
							if (cknet.Check_Email(Sql_Reader["adl_email"].ToString().Trim()) == 0)
							{
								listmail += Sql_Reader["adl_email"].ToString() + ",";
								goodmail += Sql_Reader["adl_sid"].ToString() + ",";
								gcnt++;
							}
							else
							{
								badmail += Sql_Reader["adl_sid"].ToString() + ",";
								ecnt++;
							}
						}

						if (badmail != "")
							badmail = badmail.Substring(0, badmail.Length - 1);

						if (goodmail != "")
							goodmail = goodmail.Substring(0, goodmail.Length - 1);

						if (listmail != "")
						{
							listmail = listmail.Substring(0, listmail.Length - 1);
							mailto = listmail.Split(',');
						}

						Sql_Reader.Close();
						Sql_Reader.Dispose();
						#endregion


						if ((gcnt + ecnt) == 0)
							mErr = "郵件已全部發送完畢!\\n";
						else
						{
							#region 由 Sys_Param取得 SMTP 伺服器相關資料
							SqlString = "Select sp_no, sp_num, sp_str From Sys_Param Where sp_gp = '9' And (sp_no between '901' And '904')";
							Sql_Command.Parameters.Clear();
							Sql_Command.CommandText = SqlString;

							Sql_Reader = Sql_Command.ExecuteReader();

							while (Sql_Reader.Read())
							{
								switch (Sql_Reader["sp_no"].ToString())
								{
									case "901":		// smtp 伺服器名稱
										smtp_Host = Sql_Reader["sp_str"].ToString().Trim();
										break;
									case "902":		// smtp Port
										smtp_Port = int.Parse(Sql_Reader["sp_num"].ToString());
										break;
									case "903":		// 登入帳號
										smtp_ID = Sql_Reader["sp_str"].ToString().Trim();
										break;
									case "904":		// 登入密碼
										smtp_PW = Sql_Reader["sp_str"].ToString().Trim();
										break;
								}
							}
							Sql_Reader.Close();
							Sql_Reader.Dispose();
							#endregion

							#region 開始發送郵件 (有合格的信箱才發送)
							if (gcnt > 0)
							{
								using (MailMessage mailmsg = new MailMessage())
								{

									mailmsg.From = new MailAddress(adm_fname + " <" + adm_fmail + ">");
									mailmsg.BodyEncoding = Encoding.GetEncoding(65001);
									mailmsg.Subject = adm_title;
									mailmsg.Body = adm_content;
									if (adm_type == 1)
										mailmsg.IsBodyHtml = false;		// TEXT
									else
										mailmsg.IsBodyHtml = true;		// HTML

									// 以密件發送
									foreach (string email in mailto)
									{
										mailmsg.Bcc.Add(email);
									}

									try
									{
										// 設定 smtp mail 主機
										SmtpClient smtpclt = new SmtpClient(smtp_Host, smtp_Port);

										// 設定 smtp mail 帳號密碼
										smtpclt.Credentials = new NetworkCredential(smtp_ID, smtp_PW);

										// 發送郵件
										smtpclt.Send(mailmsg);
									}
									catch
									{
										mErr = "郵件發送錯誤！\\n可能是郵件主機、帳號、密碼設定有誤!\\n";
									}
								}
							}
							#endregion

							#region 發信狀況回寫資料庫
							if (mErr == "")
							{
								if (goodmail != "")
								{
									SqlString = "Update Ad_List Set adl_send = 1, send_time = getdate() Where adm_sid = @adm_sid And adl_sid in (" + goodmail + ");";
								}
								if (badmail != "")
								{
									SqlString += "Update Ad_List Set adl_send = 2, send_time = getdate() Where adm_sid = @adm_sid And adl_sid in (" + badmail + ");";
								}
								SqlString += "Update Ad_Mail Set adm_send = adm_send + @gcnt, adm_error = adm_error + @ecnt, send_time = getdate()";
								SqlString += ",adm_batch = @adm_batch, adm_fname = @adm_fname, adm_fmail = @adm_fmail, adm_wait = @adm_wait";
								SqlString += " Where adm_sid = @adm_sid";

								Sql_Command.Parameters.Clear();
								Sql_Command.CommandText = SqlString;
								Sql_Command.Parameters.AddWithValue("adm_sid", adm_sid);
								Sql_Command.Parameters.AddWithValue("gcnt", gcnt);
								Sql_Command.Parameters.AddWithValue("ecnt", ecnt);
								Sql_Command.Parameters.AddWithValue("adm_batch", adm_batch);
								Sql_Command.Parameters.AddWithValue("adm_fname", adm_fname);
								Sql_Command.Parameters.AddWithValue("adm_fmail", adm_fmail);
								Sql_Command.Parameters.AddWithValue("adm_wait", adm_wait);

								Sql_Command.ExecuteNonQuery();
							}
							#endregion
						}
					}

					Sql_Conn.Close();
				}
			}
		}
		else
		{
			mErr = "參數傳送錯誤!\\n";
		}

		// javascript 輸出頁面
		context.Response.ContentType = "text/html";
		
		if (mErr == "")
		{
			adm_send += gcnt;
			adm_error += ecnt;
			if (adm_wait > 0)
			{
				context.Response.Write("<head>");
				context.Response.Write("<meta http-equiv=\"REFRESH\" content=\"" + adm_wait.ToString() + "\">");
				context.Response.Write("</head>");
			}
			context.Response.Write("<script language=javascript>");	
			context.Response.Write("parent.send_process(" + adm_total.ToString() + "," + adm_send.ToString() + "," + adm_error.ToString() + ");");
			
			if (adm_wait < 1)
				context.Response.Write("location.reload();");
			context.Response.Write("</script>");
		}
		else
		{
			context.Response.Write("<script language=javascript>");
			context.Response.Write("alert(\"" + mErr + "\");");
			context.Response.Write("parent.page_renew();");
			context.Response.Write("</script>");
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