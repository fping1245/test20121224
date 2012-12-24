<%@ WebHandler Language="C#" Class="_10051_del" %>
//---------------------------------------------------------------------------- 
//程式功能	人員資料管理 > 明細內容 > 刪除資料
//禁止刪除「系統總管理」
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間

public class _10051_del : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

		int ckint = 0, mg_sid = -1;
		string mpage = "", mErr = "";

        // 檢查使用者權限但不存入使用紀錄
		//Check_Power("1005", false, context);

		#region 承接上一頁的查詢條件設定

		// Session、Request、Response 之前均要加入 context. 才能使用
		if (context.Request["pageid"] != null)
		{
			if (int.TryParse(context.Request["pageid"].ToString(), out ckint))
				mpage = "?pageid=" + ckint.ToString();
			else
				mpage = "?pageid=0";
		}

		if (context.Request["mg_sid"] != null)
			mpage = mpage + "&mg_sid=" + context.Request["mg_sid"];

		if (context.Request["mg_name"] != null)
			mpage = mpage + "&mg_name=" + context.Server.UrlEncode(context.Request["mg_name"]);

		if (context.Request["mg_nike"] != null)
			mpage = mpage + "&mg_nike=" + context.Server.UrlEncode(context.Request["mg_nike"]);

		if (context.Request["btime"] != null)
			mpage = mpage + "&btime=" + context.Server.UrlEncode(context.Request["btime"]);

		if (context.Request["etime"] != null)
			mpage = mpage + "&etime=" + context.Server.UrlEncode(context.Request["etime"]);

		#endregion
		
		#region 檢查傳入參數
		if (context.Request["sid"] != null)
		{
			if (int.TryParse(context.Request["sid"].ToString(), out mg_sid))
			{
				if (mg_sid != 0)
				{
					// 刪除相關資料
					using (SqlConnection Sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
					{
						Sql_conn.Open();

						string SqlString = "";

						// 刪除登入紀錄
						SqlString = "Delete From Mg_Log Where mg_sid = @mg_sid;";

						// 刪除權限紀錄
						SqlString = SqlString + "Delete From Func_Power Where mg_sid = @mg_sid;";

						// 刪除人員資料
						SqlString = SqlString + "Delete From Manager Where mg_sid = @mg_sid;";

						using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_conn))
						{
							Sql_Command.Parameters.AddWithValue("@mg_sid", mg_sid);

							Sql_Command.ExecuteNonQuery();
						}
					}
				}
				else
					mErr = "「系統總管理」的帳號不能刪除!\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";
		}
		else
			mErr = "參數傳送錯誤!\\n";
		#endregion

		if (mErr == "")
			mErr = "<script language=javascript>alert('資料刪除成功!\\n');parent.location.replace('1005.aspx" + mpage + "');</script>";
		else
			mErr = "<script language=javascript>alert('" + mErr + "');</script>";
			
        context.Response.ContentType = "text/html";
        context.Response.Write(mErr);
    }
 
    public bool IsReusable {
        get {
			// Session 是可讀寫的物件，如果程式中需要寫入 Session，要改成 true 才能寫入，如果只是要讀取的話，就可保持原來的 false	
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