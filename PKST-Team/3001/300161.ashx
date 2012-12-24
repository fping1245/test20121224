<%@ WebHandler Language="C#" Class="_300161" %>
//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 縮圖顯示 > 產生縮圖
//備註說明
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _300161 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context)
	{
		int ac_sid = -1;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("3001", false, context);

		if (context.Request["ac_sid"] != null)
		{
			if (int.TryParse(context.Request["ac_sid"], out ac_sid))
			{
				// 處理下載檔案
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					Sql_Conn.Open();

					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						SqlString = "Select Top 1 ac_thumb From Al_Content Where ac_sid = @ac_sid";

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("ac_sid", ac_sid);

						SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							byte[] ac_thumb = (byte[])Sql_Reader["ac_thumb"];

							context.Response.Clear();
							context.Response.Charset = "utf-8";

							// 設定輸出格式
							context.Response.ContentType = "image/pjpeg";
							context.Response.BinaryWrite(ac_thumb);
							context.Response.End();
						}
						else
							ac_sid = -1;
							
						Sql_Reader.Close();
						Sql_Reader.Dispose();
					}
				}
			}
			else
				ac_sid = -1;
			
		}

		if (ac_sid == -1)
		{
			// 設定輸出格式
			context.Response.ContentType = "text/plain";
			context.Response.End();
		}
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