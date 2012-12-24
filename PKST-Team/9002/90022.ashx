<%@ WebHandler Language="C#" Class="_90022" %>
//---------------------------------------------------------------------------- 
//程式功能	POP3收信處理 > 讀取主機相關資料
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _90022 : IHttpHandler, IRequiresSessionState
{
	// class 加入「IRequiresSessionState」可讀寫 Session
	// class 加入「IReadOnlySessionState」可讀寫 Session，但寫入的狀況無法傳給其它頁面使用
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "";
		int ppa_sid = -1, mg_sid = -1;
		string ppa_host = "", ppa_port = "", ppa_id = "", ppa_pw = "";
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("9002", false, context);

		if (context.Request["sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out ppa_sid))
			{
				mg_sid = int.Parse(context.Session["mg_sid"].ToString());

				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						SqlDataReader Sql_Reader;
						
						string SqlString = "";

						Sql_Conn.Open();

						#region 取得主機名稱及帳號密碼
						SqlString = "Select Top 1 ppa_host, ppa_port, ppa_id, ppa_pw, is_delete From POP3_Account Where ppa_sid = @ppa_sid And mg_sid = @mg_sid";

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
						Sql_Command.Parameters.AddWithValue("mg_sid", mg_sid);

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
							else
							{
								// 建立 Session
								context.Session["ppa_sid"] = ppa_sid.ToString();
								context.Session["ppa_host"] = ppa_host;
								context.Session["ppa_port"] = ppa_port;
								context.Session["ppa_id"] = ppa_id;
								context.Session["ppa_pw"] = ppa_pw;
								context.Session["is_delete"] = Sql_Reader["is_delete"].ToString().Trim();
							}
						}
						else
							mErr = "找不到指定的主機設定資料!\\n";

						Sql_Reader.Close();
						#endregion

						Sql_Reader.Dispose();
						Sql_Conn.Close();
					}
				}
			}
			else
				mErr = "參數傳遞錯誤!\\n";
			
		}
		else
			mErr = "參數傳遞錯誤!\\n";
		

		// 設定輸出格式
		context.Response.ContentType = "text/html";
		if (mErr == "")
		{
			// 整理資料庫，開始連結郵件主機
			context.Response.Write("<script type=\"text/javascript\">parent.rece_process(0, 0, \"整理資料庫，開始連結郵件主機...\");location.replace(\"90023.ashx\");</script>");
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