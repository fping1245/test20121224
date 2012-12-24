<%@ WebHandler Language="C#" Class="_700213" %>
//---------------------------------------------------------------------------- 
//程式功能	線上客服-客服人員端 > 對話視窗 > 檔案下載
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間

public class _700213 : IHttpHandler, IRequiresSessionState
{
    
	public void ProcessRequest (HttpContext context)
	{
		string mErr = "";
		int cm_sid = -1 , cu_sid = -1;

		if (context.Request["sid"] == null || context.Request["cu_sid"] == null)
			mErr = "參數傳送有問題!\\n";
		else
		{
			if (int.TryParse(context.Request["sid"].Trim(), out cm_sid))
			{
				if (! int.TryParse(context.Request["cu_sid"].Trim(), out cu_sid))
					mErr = "參數傳送有問題!\\n";
			}
			else
				mErr = "參數傳送有問題!\\n";
		}

		if (mErr == "")
		{
			// 處理下載檔案
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				Sql_Conn.Open();

				using (SqlCommand Sql_Command = new SqlCommand())
				{
					string SqlString = "";

					SqlString = "Select Top 1 cm_fname, cm_ftype, cm_fcontent From Cs_Message Where cm_sid = @cm_sid And cu_sid = @cu_sid";

					Sql_Command.Connection = Sql_Conn;
					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("cm_sid", cm_sid);
					Sql_Command.Parameters.AddWithValue("cu_sid", cu_sid);

					SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
					{
						byte[] cf_content = (byte[])Sql_Reader["cm_fcontent"];

						context.Response.Clear();
						context.Response.Charset = "utf-8";

						// 檔名要先編碼，中文檔名才不會有問題
						context.Response.AddHeader("Content-Disposition", "inline;filename=" + context.Server.UrlEncode(Sql_Reader["cm_fname"].ToString().Trim()));
						context.Response.ContentType = Sql_Reader["cm_ftype"].ToString();
						context.Response.BinaryWrite(cf_content);
						context.Response.End();
					}
					else
						mErr = "找不到這個檔案的資料!\\n";

					Sql_Reader.Close();
					Sql_Reader.Dispose();
					Sql_Command.Dispose();
				}
			}
		}

		if (mErr != "")
		{
			mErr = "<script language=javascript>alert(\"" + mErr + "\");</script>";
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