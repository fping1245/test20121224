<%@ WebHandler Language="C#" Class="_D00232" %>
//---------------------------------------------------------------------------- 
//程式功能	論壇管理 > 主題回應列表 > 刪除回應
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _D00232 : IHttpHandler, IRequiresSessionState
{
	// class 加入「IRequiresSessionState」可讀寫 Session
	// class 加入「IReadOnlySessionState」可讀寫 Session，但寫入的狀況無法傳給其它頁面使用
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "";
		int fr_sid = -1, ff_sid = -1;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("D002", false, context);

		if (context.Request["sid"] != null && context.Request["ff_sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out fr_sid) && int.TryParse(context.Request["ff_sid"], out ff_sid))
			{
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						Sql_Command.Connection = Sql_Conn;

						#region 刪除 Fm_Response 紀錄
						Sql_Conn.Open();
						
						if (mErr == "")
						{
							SqlString = "Delete From Fm_Response Where fr_sid = @fr_sid;";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("fr_sid", fr_sid);
							
							Sql_Command.ExecuteNonQuery();
						}
						
						Sql_Conn.Close();
						#endregion

						#region 重新統計回應篇數
						Sql_Conn.Open();

						SqlString = "Update Fm_Forum Set ff_response = (Select Count(*) From Fm_Response Where ff_sid = @ff_sid And is_close = 1)";
						SqlString += " Where ff_sid = @ff_sid";

						Sql_Command.CommandText = SqlString;

						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("ff_sid", ff_sid);

						Sql_Command.ExecuteNonQuery();

						Sql_Conn.Close();
						#endregion

						mErr = "刪除成功!\\n";
					}
				}
			}
			else
				mErr = "刪除錯誤!\\n";
			
		}
		else
			mErr = "刪除錯誤!\\n";

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