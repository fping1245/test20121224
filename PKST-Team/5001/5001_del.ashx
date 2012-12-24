<%@ WebHandler Language="C#" Class="_5001_del" %>
//---------------------------------------------------------------------------- 
//程式功能	工作類型管理 > 刪除資料
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間

public class _5001_del : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

		int cg_sid = -1;
		string mErr = "", SqlString = "";

		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("5001", false, context);
		
		#region 檢查傳入參數
		if (context.Request["sid"] != null)
		{
			if (int.TryParse(context.Request["sid"].ToString(), out cg_sid))
			{
				// 刪除資料
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					Sql_Conn.Open();
					
					using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
					{
						#region 檢查是否尚有通訊錄資料，如果有的話，則不能刪除
						SqlString = "Select Top 1 cg_sid From Ca_Calendar Where cg_sid = @cg_sid And mg_sid = @mg_sid";
						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("cg_sid", cg_sid);
						Sql_Command.Parameters.AddWithValue("mg_sid", context.Session["mg_sid"].ToString());
		
						using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
						{
							if (Sql_Reader.Read())
								mErr = "通訊錄中尚有此群組的資料，不能夠刪除!\\n";

							Sql_Reader.Close();
							Sql_Reader.Dispose();
						}						
						#endregion

						#region 刪除資料
						if (mErr == "")
						{
							SqlString = "Delete Ca_Group Where cg_sid = @cg_sid And mg_sid = @mg_sid";

							Sql_Command.Parameters.Clear();
							Sql_Command.Connection = Sql_Conn;
							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.AddWithValue("cg_sid", cg_sid);
							Sql_Command.Parameters.AddWithValue("mg_sid", context.Session["mg_sid"].ToString());

							Sql_Command.ExecuteNonQuery();
						}
						#endregion
					}

					Sql_Conn.Close();
				}

                // 呼叫 Sql Server 的預存程序來重新設定 cg_sort 的順序
				ReSort(context.Session["mg_sid"].ToString());
			}
			else
				mErr = "參數傳送錯誤!\\n";
		}
		else
			mErr = "參數傳送錯誤!\\n";
		#endregion

		if (mErr == "")
			mErr = "<script language=javascript>alert('資料刪除成功!\\n');parent.location.reload();</script>";
		else
			mErr = "<script language=javascript>alert('" + mErr + "');</script>";
			
        context.Response.ContentType = "text/html";
        context.Response.Write(mErr);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

	#region Check_Power() 檢查使用者權限並存入登入紀錄
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

	// 呼叫 Sql Server 的預存程序來重新設定 cg_sort 的順序
	private void ReSort(string mg_sid)
	{
		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			string SqlString = "";
			SqlString = "Execute dbo.p_Ca_Group_ReSort @mg_sid;";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Command.Parameters.AddWithValue("mg_sid", mg_sid);

				Sql_Conn.Open();
				Sql_Command.ExecuteNonQuery();
				Sql_Conn.Close();
			}
		}
	}
}