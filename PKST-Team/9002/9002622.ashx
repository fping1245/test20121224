<%@ WebHandler Language="C#" Class="_9002622" %>
//---------------------------------------------------------------------------- 
//程式功能	檔案上傳下載 (以資料庫存放檔案) > 檔案下載
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;

public class _9002622 : IHttpHandler {
    
	public void ProcessRequest (HttpContext context)
	{
		string mErr = "", ppt_name = "";
		int ppt_sid = -1, ppa_sid = -1, ppm_sid = -1;

		if (context.Request["sid"] != null && context.Request["ppa_sid"] != null && context.Request["ppm_sid"] != null)
		{
			if (int.TryParse(context.Request["sid"].Trim(), out ppt_sid) && int.TryParse(context.Request["ppa_sid"].Trim(), out ppa_sid)
				&& int.TryParse(context.Request["ppm_sid"].Trim(), out ppm_sid))
			{
				// 處理下載檔案
				using (SqlConnection Sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					Sql_conn.Open();

					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						SqlString = "Select Top 1 ppt_name, ppt_type, ppt_content From POP3_Attach Where ppa_sid = @ppa_sid And ppm_sid = @ppm_sid And ppt_sid = @ppt_sid";

						Sql_Command.Connection = Sql_conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("ppa_sid", ppa_sid);
						Sql_Command.Parameters.AddWithValue("ppm_sid", ppm_sid);
						Sql_Command.Parameters.AddWithValue("ppt_sid", ppt_sid);

						SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							byte[] ppt_content = (byte[])Sql_Reader["ppt_content"];
							ppt_name = Sql_Reader["ppt_name"].ToString().Trim();
							if (ppt_name == "")
								ppt_name = ppt_sid.ToString();

							context.Response.Clear();
							context.Response.Charset = "utf-8";

							// 檔名要先編碼，中文檔名才不會有問題
							context.Response.AddHeader("Content-Disposition", "inline;filename=" + context.Server.UrlEncode(ppt_name));
							context.Response.ContentType = Sql_Reader["ppt_type"].ToString();
							context.Response.BinaryWrite(ppt_content);
							context.Response.End();
						}
						else
							mErr = "找不到這個檔案的資料!\\n";
					}
				}
			}
			else
				mErr = "參數格式有問題!\\n";
		}
		else
			mErr = "參數傳送有問題!\\n";

		if (mErr != "")
		{
			context.Response.ContentType = "text/html";
			context.Response.Write(mErr);
			context.Response.End();
		}       
    }
 
    public bool IsReusable
	{
        get {
            return false;
        }
    }

}