<%@ WebHandler Language="C#" Class="_60021_del" %>
//---------------------------------------------------------------------------- 
//程式功能	通訊錄管理 > 詳細內容 > 刪除資料
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間

public class _60021_del : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

		int ckint = 0, ab_sid = -1;
		string mpage = "", mErr = "";
		
		// 檢查使用者權限但不存入登入紀錄
		//Check_Power("6002", false, context);

		#region 承接上一頁的查詢條件設定
		if (context.Request["sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out ab_sid))
			{
				if (context.Request["pageid"] != null)
				{
					if (int.TryParse(context.Request["pageid"].ToString(), out ckint))
						mpage = "?pageid=" + ckint.ToString();
					else
						mpage = "?pageid=0";
				}

				if (context.Request["ab_name"] != null)
					mpage += "&ab_name=" + context.Server.UrlEncode(context.Request["ab_name"]);

				if (context.Request["ab_nike"] != null)
					mpage += "&ab_nike=" + context.Server.UrlEncode(context.Request["ab_nike"]);

				if (context.Request["ab_company"] != null)
					mpage += "&ab_company=" + context.Server.UrlEncode(context.Request["ab_company"]);

				if (context.Request["ag_name"] != null)
					mpage += "&ag_name=" + context.Server.UrlEncode(context.Request["ag_name"]);

				if (context.Request["ag_attrib"] != null)
					mpage += "&ag_attrib=" + context.Server.UrlEncode(context.Request["ag_attrib"]);

				if (context.Request["sort"] != null)
					mpage += "&sort=" + context.Request["sort"];

				if (context.Request["row"] != null)
					mpage += "&row=" + context.Request["row"];
			}
			else
				mErr = "參數型態有誤!\\n";
		}
		else
			mErr = "參數傳送錯誤!\\n";
		#endregion
		
		#region 檢查傳入參數
		if (mErr == "")
		{
			// 取得相關資料
			using (SqlConnection Sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				Sql_conn.Open();

				string SqlString = "Delete From As_Book Where ab_sid = @ab_sid And mg_sid = @mg_sid;";

				using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_conn))
				{
					Sql_Command.Parameters.AddWithValue("mg_sid", context.Session["mg_sid"].ToString());
					Sql_Command.Parameters.AddWithValue("ab_sid", ab_sid);

					Sql_Command.ExecuteNonQuery();
				}
			}
		}
		#endregion

		if (mErr == "")
			mErr = "<script language=javascript>alert('資料刪除成功!\\n');parent.location.replace('6002.aspx" + mpage + "');</script>";
		else
			mErr = "<script language=javascript>alert('" + mErr + "');</script>";
			
        context.Response.ContentType = "text/html";
        context.Response.Write(mErr);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

	#region Check_Power() 檢查使用者權限並存入登入紀錄
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