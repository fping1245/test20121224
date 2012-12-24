﻿<%@ WebHandler Language="C#" Class="_B00143" %>
//---------------------------------------------------------------------------- 
//程式功能	考試題庫管理 > 試題處理 > 刪除試題及答案
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _B00143 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "";
		int tq_sid = -1, tp_sid = -1;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("B001", false, context);

		if (context.Request["sid"] != null && context.Request["tp_sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out tq_sid) && int.TryParse(context.Request["tp_sid"], out tp_sid))
			{
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						Sql_Conn.Open();
						Sql_Command.Connection = Sql_Conn;

						#region 刪除 Ts_Item 及 Ts_Question 及 Ts_UQuest 及 Ts_UAns 的紀錄
						if (mErr == "")
						{
							// 刪除 Ts_Item 的紀錄
							SqlString = "Delete From Ts_Item Where tp_sid = @tp_sid And tq_sid = @tq_sid;";
							
							// 刪除 Ts_Question 的紀錄
							SqlString += "Delete From Ts_Question Where tq_sid = @tq_sid And tp_sid = @tp_sid;";

							// 刪除 Ts_UAns 的紀錄
							SqlString += "Delete From Ts_UAns Where tq_sid = @tq_sid And tp_sid = @tp_sid;";

							// 刪除 Ts_UQuest 的紀錄
							SqlString += "Delete From Ts_UQuest Where tq_sid = @tq_sid And tp_sid = @tp_sid;";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("tp_sid", tp_sid);
							Sql_Command.Parameters.AddWithValue("tq_sid", tq_sid);
							
							Sql_Command.ExecuteNonQuery();
						}
						#endregion

						#region 重新編排題號，並更新試題總數及總分
						SqlString = "Execute dbo.p_Ts_Question_ReSort @tp_sid;";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("tp_sid", tp_sid);

						Sql_Command.ExecuteNonQuery();
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