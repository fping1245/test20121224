<%@ WebHandler Language="C#" Class="_8001111" %>
//---------------------------------------------------------------------------- 
//程式功能	HTML編輯器 (網頁清單) > 編輯頁面 > 圖檔列表 > 產生圖形
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _8001111 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context)
	{
		int hf_sid = -1;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("8001", false, context);

		if (context.Request["sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out hf_sid))
			{
				// 處理下載檔案
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					Sql_Conn.Open();

					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						SqlString = "Select Top 1 hf_content, hf_type From Html_Files Where hf_sid = @hf_sid";

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("hf_sid", hf_sid);

						SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							byte[] hf_content = (byte[])Sql_Reader["hf_content"];

							context.Response.Clear();
							context.Response.Charset = "utf-8";

							// 設定輸出格式
							context.Response.ContentType = Sql_Reader["hf_type"].ToString();
							context.Response.BinaryWrite(hf_content);
							context.Response.End();
						}
						else
							hf_sid = -1;
						
						Sql_Reader.Close();
						Sql_Reader.Dispose();
					}
				}
			}
			else
				hf_sid = -1;
			
		}

		if (hf_sid == -1)
		{
			// 設定輸出格式
			context.Response.ContentType = "text/plain";
			context.Response.End();
		}
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