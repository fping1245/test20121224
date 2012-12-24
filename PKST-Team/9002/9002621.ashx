<%@ WebHandler Language="C#" Class="_9002621" %>
//---------------------------------------------------------------------------- 
//程式功能	POP3收信處理 > 查看郵件內容 > 顯示郵件內文
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _9002621 : IHttpHandler, IRequiresSessionState
{
	// class 加入「IRequiresSessionState」可讀寫 Session
	// class 加入「IReadOnlySessionState」可讀寫 Session，但寫入的狀況無法傳給其它頁面使用
    public void ProcessRequest (HttpContext context)
	{
		string SqlString = "", mErr = "", ppm_body = "";
		int ppm_sid = -1, ppa_sid = -1, ppm_type = -1;
		SqlDataReader Sql_Reader;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("9002", false, context);

		if (context.Request["sid"] != null && context.Request["ppa_sid"] != null && context.Request["ppm_type"] != null)
		{
			if (int.TryParse(context.Request["sid"], out ppm_sid) && int.TryParse(context.Request["ppa_sid"], out ppa_sid)
				&& int.TryParse(context.Request["ppm_type"], out ppm_type))
			{
				if (ppm_type == 1 || ppm_type == 2 || ppm_type == 4)
				{
					using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
					{
						using (SqlCommand Sql_Command = new SqlCommand())
						{
							Sql_Command.Connection = Sql_Conn;

							#region 取得郵件內文
							Sql_Conn.Open();

							switch (ppm_type)
							{
								case 1:	// TEXT
									SqlString = "Select Top 1 ppm_text as ppm_body From POP3_Mail Where ppa_sid = @ppa_sid and ppm_sid = @ppm_sid;";
									break;
								case 2: // HTML
									SqlString = "Select Top 1 ppm_html as ppm_body From POP3_Mail Where ppa_sid = @ppa_sid and ppm_sid = @ppm_sid;";
									break;
								case 4:	// MIXED
									SqlString = "Select Top 1 ppm_html as ppm_body From POP3_Mail Where ppa_sid = @ppa_sid and ppm_sid = @ppm_sid;";
									break;
							}

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
							Sql_Command.Parameters.AddWithValue("ppm_sid", ppm_sid);

							Sql_Reader = Sql_Command.ExecuteReader();

							if (Sql_Reader.Read())
							{
								ppm_body = Sql_Reader["ppm_body"].ToString().Trim();
								if (ppm_type == 1)
									ppm_body = ppm_body.Replace("\r\n", "<br>");
							}
							else
								mErr = "找不到指定的郵件內容!";

							Sql_Conn.Close();
							#endregion

							Sql_Reader.Dispose();
						}
					}
				}
				else
					mErr = "郵件內文參數錯誤！";
			}
			else
				mErr = "參數格式錯誤!";
			
		}
		else
			mErr = "參數傳遞錯誤!";
		

		// 設定輸出格式
		context.Response.ContentType = "text/html";
		context.Response.ContentEncoding = System.Text.Encoding.GetEncoding(65001);
		if (mErr == "")
		{
			context.Response.Write(ppm_body);
		}
		else
		{
			context.Response.Write("<p align=\"center\">" + mErr + "</p><script type=\"text/javascript\">alert(\"" + mErr + "\\n\");</script>");
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