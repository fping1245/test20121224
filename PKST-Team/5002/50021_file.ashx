<%@ WebHandler Language="C#" Class="_50021_file" %>
//---------------------------------------------------------------------------- 
//程式功能	行事曆管理 > 新增/修改資料 > 檔案下載
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間

public class _50021_file : IHttpHandler, IRequiresSessionState
{
    
	public void ProcessRequest (HttpContext context)
	{
		string mErr = "";
		int cf_sid = -1 , ca_sid = -1;

		if (context.Request["sid"] == null || context.Request["ca_sid"] == null)
			mErr = "參數傳送有問題!\\n";
		else
		{
			if (int.TryParse(context.Request["sid"].Trim(), out cf_sid))
			{
				if (! int.TryParse(context.Request["ca_sid"].Trim(), out ca_sid))
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

					SqlString = "Select Top 1 cf_name, cf_type, cf_content From Ca_Files Where mg_sid=@mg_sid And cf_sid = @cf_sid And ca_sid = @ca_sid ";

					Sql_Command.Connection = Sql_Conn;
					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("mg_sid", context.Session["mg_sid"].ToString());
					Sql_Command.Parameters.AddWithValue("ca_sid", ca_sid);
					Sql_Command.Parameters.AddWithValue("cf_sid", cf_sid);

					SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
					{
						byte[] cf_content = (byte[])Sql_Reader["cf_content"];

						context.Response.Clear();
						context.Response.Charset = "utf-8";

						// 檔名要先編碼，中文檔名才不會有問題
						context.Response.AddHeader("Content-Disposition", "inline;filename=" + context.Server.UrlEncode(Sql_Reader["cf_name"].ToString().Trim()));
						context.Response.ContentType = Sql_Reader["cf_type"].ToString();
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