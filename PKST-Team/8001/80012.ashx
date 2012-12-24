<%@ WebHandler Language="C#" Class="_80012" %>
//---------------------------------------------------------------------------- 
//程式功能	HTML編輯器 (網頁清單) > 網頁預覽
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _80012 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context)
	{
		int he_sid = -1;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("8001", false, context);

		if (context.Request["sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out he_sid))
			{
				// 處理下載檔案
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					Sql_Conn.Open();

					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						SqlString = "Select Top 1 he_title, he_content From Html_Edit Where he_sid = @he_sid";

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("he_sid", he_sid);

						using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
						{

							if (Sql_Reader.Read())
							{
								// 設定輸出格式
								context.Response.Clear();
								context.Response.Charset = "utf-8";
								context.Response.ContentType = "text/html";
								context.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
								context.Response.Write("<head>\n");
								context.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF8\" />\n");
								context.Response.Write("<title>" + Sql_Reader["he_title"].ToString() + "</title>\n");
								context.Response.Write("</head>\n<body>\n");
								context.Response.Write(Sql_Reader["he_content"].ToString());
								context.Response.Write("\n</body>\n</html>\n");
								context.Response.End();
							}
							else
								he_sid = -1;

							Sql_Reader.Close();
						}
					}
				}
			}
			else
				he_sid = -1;
			
		}

		if (he_sid == -1)
		{
			// 設定輸出格式
			context.Response.Clear();
			context.Response.Charset = "utf-8";
			context.Response.ContentType = "text/html";
			context.Response.Write("找不到指定的網頁!\\n");
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