<%@ WebHandler Language="C#" Class="_900261" %>
//---------------------------------------------------------------------------- 
//程式功能	POP3收信處理 > 查看郵件內容 > 解析郵件內容
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _900261 : IHttpHandler, IRequiresSessionState
{
	// class 加入「IRequiresSessionState」可讀寫 Session
	// class 加入「IReadOnlySessionState」可讀寫 Session，但寫入的狀況無法傳給其它頁面使用
    public void ProcessRequest (HttpContext context)
	{
		string SqlString = "", mErr = "", ppm_content = "";
		string body_text = "", body_html = "", body_type = "";
		int ppm_sid = -1, ppa_sid = -1, fcnt = 0, icnt = 0;
		SqlDataReader Sql_Reader;
		Email_Decode email_dc = new Email_Decode();
		
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
						Sql_Command.Connection = Sql_Conn;

						#region 取得郵件原始內容
						Sql_Conn.Open();

						SqlString = "Select Top 1 ppm_content From POP3_Mail Where ppa_sid = @ppa_sid and ppm_sid = @ppm_sid;";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
						Sql_Command.Parameters.AddWithValue("ppm_sid", ppm_sid);

						Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							// 讀取資料庫
							ppm_content = Sql_Reader["ppm_content"].ToString();
						}
						else
							mErr = "找不到指定的郵件資料!\\n";
						
						Sql_Reader.Close();
						Sql_Conn.Close();
						#endregion

						#region 郵件內文解碼及存檔
						if (mErr == "")
						{
							// 設定郵件原始資料
							email_dc.Mail_Source = ppm_content.Trim();

							#region 郵件內文處理
							body_type = email_dc.Mail_Body_Type;
							body_text = email_dc.Mail_Body_TEXT;
							body_html = email_dc.Mail_Body_HTML;
							fcnt = email_dc.Attach_Num;

							switch (body_type)
							{
								case "TEXT":
									body_type = "1";
									break;
								case "HTML":
									body_type = "2";
									break;
								case "MIXED":
									body_type = "4";
									break;
								default:
									body_type = "0";
									break;
							}
						
							if (body_html == "" && body_type == "4")
								body_type = "1";

							SqlString = "Update POP3_Mail Set ppm_text = @ppm_text, ppm_html = @ppm_html, ppm_type = @ppm_type";
							SqlString += ", ppm_attach = @ppm_attach, is_decode = 1 Where ppa_sid = @ppa_sid And ppm_sid = @ppm_sid;";

							Sql_Conn.Open();

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
							Sql_Command.Parameters.AddWithValue("ppm_sid", ppm_sid);
							Sql_Command.Parameters.AddWithValue("ppm_text", body_text);
							Sql_Command.Parameters.AddWithValue("ppm_html", body_html);
							Sql_Command.Parameters.AddWithValue("ppm_type", body_type);
							Sql_Command.Parameters.AddWithValue("ppm_attach", fcnt);

							Sql_Command.ExecuteNonQuery();

							Sql_Conn.Close();
							#endregion

							#region 清除資料庫中的附加檔案 (避免檔案重覆)
							Sql_Conn.Open();
							SqlString = "Delete POP3_Attach Where ppa_sid = @ppa_sid And ppm_sid = @ppm_sid;";

							Sql_Command.CommandText = SqlString;					
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
							Sql_Command.Parameters.AddWithValue("ppm_sid", ppm_sid);

							Sql_Command.ExecuteNonQuery();

							Sql_Conn.Close();
							#endregion

							#region 儲存附件
							if (fcnt > 0)
							{
								Sql_Conn.Open();
								
								SqlString = "Insert Into POP3_Attach (ppa_sid, ppm_sid, ppt_name, ppt_type, ppt_content)";
								SqlString += " Values (@ppa_sid, @ppm_sid, @ppt_name, @ppt_type, @ppt_content);";
								Sql_Command.CommandText = SqlString;
								
								for (icnt = 0; icnt < fcnt; icnt++)
								{
									Sql_Command.Parameters.Clear();
									Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
									Sql_Command.Parameters.AddWithValue("ppm_sid", ppm_sid);
									Sql_Command.Parameters.AddWithValue("ppt_name", email_dc.FileName(icnt));
									Sql_Command.Parameters.AddWithValue("ppt_type", email_dc.FileType(icnt));

									if (email_dc.File(icnt) == null)
										Sql_Command.Parameters.AddWithValue("ppt_content", "");
									else
										Sql_Command.Parameters.AddWithValue("ppt_content", email_dc.File(icnt));

									Sql_Command.ExecuteNonQuery();
								}
								Sql_Conn.Close();
							}
							#endregion

						}
						#endregion

						Sql_Reader.Dispose();
					}
				}
			}
			else
				mErr = "參數格式錯誤!\\n";
			
		}
		else
			mErr = "參數傳遞錯誤!\\n";
		

		// 設定輸出格式
		context.Response.ContentType = "text/html";
		if (mErr == "")
		{
			// 移到查看郵件頁面
			context.Response.Write("<script type=\"text/javascript\">parent.parent.show_win(\"900262.aspx?sid="
				+ ppm_sid.ToString() + "&ppa_sid=" + ppa_sid.ToString() + "\",760, 450);</script>");
		}
		else
		{
			context.Response.Write("<script type=\"text/javascript\">alert(\"" + mErr + "\");parent.parent.close_all();</script>");
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