<%@ WebHandler Language="C#" Class="_F0031" %>
//---------------------------------------------------------------------------- 
//程式功能	產生 Excel 檔案  > 產生成績分佈統計
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _F0031 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "", tp_title = "", tfile = "", sfile = "", wpath = "";
		int tp_sid = -1, icnt = 0;
		int[] ts_member = new int[11];
		SqlDataReader Sql_Reader;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("F003", false, context);

		if (context.Request["sid"] != null)
		{
			if (int.TryParse(context.Request["sid"], out tp_sid))
			{
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						Sql_Conn.Open();
						Sql_Command.Connection = Sql_Conn;

						#region 取得試卷主題主題
						SqlString = "Select Top 1 tp_title From Ts_Paper Where tp_sid = @tp_sid";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("tp_sid", tp_sid);

						Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							tp_title = Sql_Reader["tp_title"].ToString();
						}
						else
							mErr = "找不到試卷主題!\\n";

						Sql_Reader.Close();			
						#endregion

						#region 取得考生成績分佈
						for (icnt = 0; icnt < 11; icnt++)
						{
							ts_member[icnt] = 0;
						}

						SqlString = "Select Floor(tu_score/10) as score, Count(*) as cnt From Ts_User";
						SqlString += " Where tp_sid = @tp_sid Group by Floor(tu_score/10) Order by Floor(tu_score/10) DESC";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("tp_sid", tp_sid);
						Sql_Reader = Sql_Command.ExecuteReader();

						while (Sql_Reader.Read())
						{
							icnt = 10 - int.Parse(Sql_Reader["score"].ToString());
							ts_member[icnt] = int.Parse(Sql_Reader["cnt"].ToString());
						}
						#endregion

						#region 複製 Excel 檔，以解決共用問題
						wpath = context.Server.MapPath("../F003/Define/");
						sfile = wpath + "F0031.xls";

						wpath = context.Server.MapPath("../F003/WorkTemp/");
						tfile = wpath + context.Session["mg_sid"].ToString() + "_F0031.xls";

						File.Delete(tfile);
						if (File.Exists(tfile))
							mErr = "檔案無法產生!\\n";
						else
							File.Copy(sfile, tfile, true);
						#endregion

						#region 開啟新的 Excel 檔
						if (mErr == "")
						{
							//   Excel的第一列為欄位名稱
							string xls_conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
								+ tfile + ";Extended Properties=\"Excel 8.0;HDR=YES\"";

							//   Excel的第一列沒有欄位名稱
							// string xls_conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + tfile + ";Extended Properties=\"Excel 8.0;HDR=NO\"";

							using (OleDbConnection oledb_conn = new OleDbConnection(xls_conn))
							{
								using (OleDbCommand oledb_command = new OleDbCommand())
								{
									oledb_conn.Open();
									oledb_command.Connection = oledb_conn;

									#region 更新「試卷名稱」
									SqlString = "Update [Results$] Set remark = @remark Where remark = '試卷名稱';";


									oledb_command.CommandText = SqlString;
									oledb_command.Parameters.Clear();
									oledb_command.Parameters.AddWithValue("remark", tp_title);
									oledb_command.ExecuteNonQuery();
									#endregion

									#region 更新「製表日期」
									SqlString = "Update [Results$] Set remark = @remark Where remark = '製表日期';";

									oledb_command.CommandText = SqlString;
									oledb_command.Parameters.Clear();
									oledb_command.Parameters.AddWithValue("remark", "製表日期：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
									oledb_command.ExecuteNonQuery();
									#endregion

									SqlString = "Update [Results$] Set F1=@f1, F2=@f2, F3=@f3, F4=@f4, F5=@f5, F6=@f6";
									SqlString += ", F7=@f7, F8=@f8, F9=@f9, FA=@fa, FB=@fb Where remark = '人數';";

									oledb_command.CommandText = SqlString;
									oledb_command.Parameters.Clear();
									oledb_command.Parameters.AddWithValue("F1", ts_member[0]);
									oledb_command.Parameters.AddWithValue("F2", ts_member[1]);
									oledb_command.Parameters.AddWithValue("F3", ts_member[2]);
									oledb_command.Parameters.AddWithValue("F4", ts_member[3]);
									oledb_command.Parameters.AddWithValue("F5", ts_member[4]);
									oledb_command.Parameters.AddWithValue("F6", ts_member[5]);
									oledb_command.Parameters.AddWithValue("F7", ts_member[6]);
									oledb_command.Parameters.AddWithValue("F8", ts_member[7]);
									oledb_command.Parameters.AddWithValue("F9", ts_member[8]);
									oledb_command.Parameters.AddWithValue("FA", ts_member[9]);
									oledb_command.Parameters.AddWithValue("FB", ts_member[10]);

									oledb_command.ExecuteNonQuery();

									oledb_conn.Close();

									context.Response.Redirect("../F003/WorkTemp/" + context.Session["mg_sid"].ToString() + "_F0031.xls?xtemp=" + DateTime.Now.ToString("HHmmss"));
								}
							}
						}
						#endregion


						Sql_Conn.Close();
					}
				}
			}
			else
				mErr = "參數格式錯誤!\\n";
			
		}
		else
			mErr = "參數傳送錯誤!\\n";

		if (mErr != "")
		{
			// 設定輸出格式
			context.Response.ContentType = "text/html";
			context.Response.Write("<script type=\"text/javascript\">alert(\"" + mErr + "\");parent.location.reload(true);</script>");
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