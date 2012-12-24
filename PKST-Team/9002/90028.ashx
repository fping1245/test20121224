<%@ WebHandler Language="C#" Class="_90028" %>
//---------------------------------------------------------------------------- 
//程式功能	POP3收信處理 > 匯出郵件
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _90028 : IHttpHandler, IRequiresSessionState
{
	// class 加入「IRequiresSessionState」可讀寫 Session
	// class 加入「IReadOnlySessionState」可讀寫 Session，但寫入的狀況無法傳給其它頁面使用
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "", ppm_content = "", ppm_id = "";
		int ppm_sid = -1, ppa_sid = -1;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("9002", false, context);

		if (context.Request["sid"] != null && context.Request["ppa_sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out ppm_sid) && int.TryParse(context.Request["ppa_sid"], out ppa_sid))
			{

				#region 匯出 POP3_Mail 的紀錄
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						Sql_Conn.Open();

						SqlString = "Select Top 1 ppm_id, ppm_content From POP3_Mail Where ppa_sid = @ppa_sid And ppm_sid = @ppm_sid;";

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
						Sql_Command.Parameters.AddWithValue("ppm_sid", ppm_sid);

						using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
						{
							if (Sql_Reader.Read())
							{
								ppm_id = Sql_Reader["ppm_id"].ToString().Trim();
								ppm_content = Sql_Reader["ppm_content"].ToString().Trim();
							}
							else
								mErr = "郵件匯出完成!\\n";
							
							Sql_Reader.Close();
						}

						Sql_Conn.Close();
					}
				}
				#endregion
			}
			else
				mErr = "郵件匯出錯誤!\\n";
			
		}

		// 設定輸出格式
		if (mErr == "")
		{
			context.Response.ContentEncoding = Encoding.Default;
			context.Response.ContentType = "text/plain";
			context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + ppm_id+ ".eml\"");
			context.Response.Write(ppm_content);
		}
		else
		{
			context.Response.ContentType = "text/html";
			context.Response.Write("<script type=\"text/javascript\">alert(\"" + mErr + "\");</script>");
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