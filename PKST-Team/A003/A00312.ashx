<%@ WebHandler Language="C#" Class="_A00353" %>
//---------------------------------------------------------------------------- 
//程式功能	票選資料管理 > 票選排程處理 > 刪除排程資料
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _A00353 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "";
		int bs_sid = -1, bh_sid = -1;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("A003", false, context);

		if (context.Request["sid"] != null && context.Request["bh_sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out bs_sid) && int.TryParse(context.Request["bh_sid"], out bh_sid))
			{
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						Sql_Conn.Open();
						Sql_Command.Connection = Sql_Conn;

						#region 刪除 Bt_Schedule 的紀錄
						if (mErr == "")
						{
							// 刪除 Bt_Schedule 的紀錄
							SqlString = "Delete From Bt_Schedule Where bs_sid = @bs_sid And bh_sid = @bh_sid;";

							// 重新排序，並更新系統變數 A01
							SqlString += "Execute dbo.p_Bt_Schedule_ReSort;";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("bh_sid", bh_sid);
							Sql_Command.Parameters.AddWithValue("bs_sid", bs_sid);
							
							Sql_Command.ExecuteNonQuery();
						}
						#endregion

						Sql_Conn.Close();

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