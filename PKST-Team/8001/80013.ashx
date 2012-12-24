<%@ WebHandler Language="C#" Class="_80013" %>
//---------------------------------------------------------------------------- 
//程式功能	HTML編輯器 (網頁清單) > 刪除頁面
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _80013 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "";
		int he_sid = -1;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("8001", false, context);

		if (context.Request["sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out he_sid))
			{
				// 刪除 Html_Edit及 Html_Files 紀錄
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						Sql_Conn.Open();

						SqlString = "Delete From Html_Files Where he_sid = @he_sid;";
						SqlString += "Delete From Html_Edit Where he_sid = @he_sid;";

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("he_sid", he_sid);

						Sql_Command.ExecuteNonQuery();

						Sql_Conn.Close();
					}
				}
			}
			else
				he_sid = -1;
			
		}

		// 設定輸出格式

		if (he_sid == -1)
			mErr = "網頁及附檔刪除錯誤!\\n";
		else
			mErr = "網頁及附檔刪除完成!\\n";
		context.Response.ContentType = "text/html";
		context.Response.Write("<script language=javascript>alert(\"" + mErr + "\");parent.location.reload(true);</script>");
		context.Response.End();
    }
 
    public bool IsReusable {
        get {
			// Session 是可讀寫的物件，如果程式中需要寫入 Session，要改成 true 才能寫入，如果只是要讀取的話，就可保持原來的 false
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