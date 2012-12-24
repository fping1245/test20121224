<%@ WebHandler Language="C#" Class="_90013" %>
//---------------------------------------------------------------------------- 
//程式功能	廣告信發送管理 ((廣告信選單) > 刪除郵件
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _90013 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "";
		int adm_sid = -1;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("9001", false, context);

		if (context.Request["sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out adm_sid))
			{
				// 刪除 Ad_Mai、Ad_List的紀錄
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						Sql_Conn.Open();

						SqlString = "Delete From Ad_List Where adm_sid = @adm_sid;";
						SqlString += "Delete From Ad_Mail Where adm_sid = @adm_sid;";

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("adm_sid", adm_sid);

						Sql_Command.ExecuteNonQuery();

						Sql_Conn.Close();
					}
				}
			}
			else
				adm_sid = -1;
			
		}

		// 設定輸出格式

		if (adm_sid == -1)
			mErr = "郵件及發送名單刪除錯誤!\\n";
		else
			mErr = "郵件及發送名單刪除完成!\\n";
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