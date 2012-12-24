<%@ WebHandler Language="C#" Class="_50021_del" %>
//---------------------------------------------------------------------------- 
//程式功能	行事曆管理 > 新增/修改資料 > 刪除附加檔案
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間

public class _50021_del : IHttpHandler, IRequiresSessionState
{
    
	public void ProcessRequest (HttpContext context)
	{
		string mErr = "";
		int cf_sid = -1 , ca_sid = -1, is_attach = 0;

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

					#region 刪除指定的資料
					SqlString = "Delete From Ca_Files Where mg_sid=@mg_sid And ca_sid = @ca_sid And cf_sid = @cf_sid ";

					Sql_Command.Connection = Sql_Conn;
					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("mg_sid", context.Session["mg_sid"].ToString());
					Sql_Command.Parameters.AddWithValue("ca_sid", ca_sid);
					Sql_Command.Parameters.AddWithValue("cf_sid", cf_sid);

					Sql_Command.ExecuteNonQuery();
					#endregion

					#region 判斷是否還有資料
					SqlString = "Select Top 1 cf_sid From Ca_Files Where mg_sid=@mg_sid And ca_sid = @ca_sid";
					Sql_Command.Parameters.Clear();
					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("mg_sid", context.Session["mg_sid"].ToString());
					Sql_Command.Parameters.AddWithValue("ca_sid", ca_sid);

					SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
						is_attach = 1;
					else
						is_attach = 0;

					Sql_Reader.Close();
					Sql_Reader.Dispose();
					#endregion

					#region 更新是否有附檔的旗標
					SqlString = "Update Ca_Calendar Set is_attach = @is_attach Where mg_sid=@mg_sid And ca_sid = @ca_sid";

					Sql_Command.Parameters.Clear();
					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("mg_sid", context.Session["mg_sid"].ToString());
					Sql_Command.Parameters.AddWithValue("ca_sid", ca_sid);
					Sql_Command.Parameters.AddWithValue("is_attach", is_attach);

					Sql_Command.ExecuteNonQuery();
					#endregion

					Sql_Command.Dispose();
				}
			}
		}

		if (mErr == "")
			mErr = "<script language=javascript>alert(\"資料刪除完成!\\n\");parent.location.replace(\"50021.aspx?sid=" + ca_sid.ToString() + "&reload=1\");</script>";
		else
			mErr = "<script language=javascript>alert(\"" + mErr + "\");</script>";

		context.Response.ContentType = "text/html";
			context.Response.Write(mErr);
			context.Response.End();
    }
 
    public bool IsReusable
	{
        get {
            return false;
        }
    }

}