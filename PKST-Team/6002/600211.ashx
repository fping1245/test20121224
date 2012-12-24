<%@ WebHandler Language="C#" Class="_600211" %>
//---------------------------------------------------------------------------- 
//程式功能	通訊錄管理 > 詳細內容 > 產生相片
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _600211 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context)
	{
		int ab_sid = -1;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("6002", false, context);

		if (context.Request["sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out ab_sid))
			{
				// 處理下載檔案
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					Sql_Conn.Open();

					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						SqlString = "Select Top 1 ab_photo From As_Book Where ab_sid = @ab_sid And mg_sid = @mg_sid";

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("ab_sid", ab_sid);
						Sql_Command.Parameters.AddWithValue("mg_sid", context.Session["mg_sid"].ToString());

						SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							byte[] ab_photo = (byte[])Sql_Reader["ab_photo"];

							context.Response.Clear();
							context.Response.Charset = "utf-8";

							// 設定輸出格式
							context.Response.ContentType = "image/pjpeg";
							context.Response.BinaryWrite(ab_photo);
							context.Response.End();
						}
						else
							ab_sid = -1;
						
						Sql_Reader.Close();
						Sql_Reader.Dispose();
					}
				}
			}
			else
				ab_sid = -1;
			
		}

		if (ab_sid == -1)
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