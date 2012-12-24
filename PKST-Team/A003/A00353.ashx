<%@ WebHandler Language="C#" Class="_A00353" %>
//---------------------------------------------------------------------------- 
//程式功能	票選資料管理 > 問卷項目明細處理 > 刪除問卷項目
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
		int bi_sid = -1, bh_sid = -1;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("A003", false, context);

		if (context.Request["sid"] != null && context.Request["bh_sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out bi_sid) && int.TryParse(context.Request["bh_sid"], out bh_sid))
			{
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						Sql_Conn.Open();
						Sql_Command.Connection = Sql_Conn;

						#region 刪除 Bt_Item 及 Bt_Ballot 的紀錄
						if (mErr == "")
						{
							SqlString = "Delete From Bt_Item Where bi_sid = @bi_sid And bh_sid = @bh_sid;";
							SqlString += "Delete From Bt_Ballot Where bh_sid = @bh_sid And bi_sid = @bi_sid;";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("bh_sid", bh_sid);
							Sql_Command.Parameters.AddWithValue("bi_sid", bi_sid);

							Sql_Command.ExecuteNonQuery();
							Sql_Conn.Close();

							#region 更新 Bt_Head 的統計
							Sql_Conn.Open();

							// 更新 Bt_Head 統計
							SqlString += "Update Bt_Head Set bh_total = (Select Count(*) From Bt_Ballot Where bh_sid = @bh_sid)";
							SqlString += ", bh_acnt = (Select Count(*) From (Select bb_ip From Bt_Ballot Where bh_sid = @bh_sid Group by Convert(Char(19),init_time, 20), bb_ip) as tmp)";
							SqlString += " Where bh_sid = @bh_sid;";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("bh_sid", bh_sid);

							Sql_Command.ExecuteNonQuery();
							#endregion

							mErr = "刪除成功!\\n";
						}
						#endregion

						Sql_Conn.Close();
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