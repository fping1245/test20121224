<%@ WebHandler Language="C#" Class="_G0013" %>
//---------------------------------------------------------------------------- 
//程式功能	資料庫規格管理 > 刪除系統規格資料
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _G0013 : IHttpHandler, IRequiresSessionState
{
	// class 加入「IRequiresSessionState」可讀寫 Session
	// class 加入「IReadOnlySessionState」可讀寫 Session，但寫入的狀況無法傳給其它頁面使用
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "";
		int ds_sid = -1;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("G001", false, context);

		if (context.Request["ds_sid"] != null)
		{
			if (int.TryParse(context.Request["ds_sid"], out ds_sid))
			{
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						Sql_Command.Connection = Sql_Conn;

						#region 檢查系統內是否還有表格資料
						Sql_Conn.Open();
						if (mErr == "")
						{
							SqlString = "Select Top 1 dt_sid From Db_Table Where ds_sid = @ds_sid;";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("ds_sid", ds_sid);

							using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
							{
								if (Sql_Reader.Read())
									mErr = "請先將裡面的表格資料全部刪除，才能刪除系統規格!\\n";
								
								Sql_Reader.Close();
							}
						}
						Sql_Conn.Close();
						#endregion

						#region 刪除系統規格
						if (mErr == "")
						{
							SqlString = "Delete From Db_Sys Where ds_sid = @ds_sid;";
							Sql_Conn.Open();

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("ds_sid", ds_sid);

							Sql_Command.ExecuteNonQuery();

							Sql_Conn.Close();
							mErr = "刪除成功!\\n";
						}
						#endregion
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