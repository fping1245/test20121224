<%@ WebHandler Language="C#" Class="_G00145" %>
//---------------------------------------------------------------------------- 
//程式功能	資料庫規格管理 > 資料表清單 > 產生資料庫資料表清單
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _G00145 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "", ds_name = "", bgcolor = "", dt_time = "";
		int ds_sid = -1, dt_sort = -1;
		SqlDataReader Sql_Reader;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("G001", false, context);

		if (context.Request["ds_sid"] != null)
		{
			if (int.TryParse(context.Request["ds_sid"], out ds_sid))
			{
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						Sql_Conn.Open();
						Sql_Command.Connection = Sql_Conn;

						#region 取得系統名稱
						SqlString = "Select Top 1 ds_name From Db_Sys Where ds_sid = @ds_sid";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("ds_sid", ds_sid);

						Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							ds_name = "「" + Sql_Reader["ds_name"].ToString().Trim() + "」資料庫表格清單";
						}
						else
							mErr = "找不到系統名稱!\\n";

						Sql_Reader.Close();			
						#endregion

						#region 產生 Word 檔
						if (mErr == "")
						{
							#region 設定 Word 格式
							Build_Word bwd = new Build_Word();

							bwd.Author = "陸士權";				// 作者
							bwd.Company = "章立民研究室";		// 公司
							bwd.Title = ds_name;				// 標題
							bwd.SetPaper("A4", "S");			// 設定紙張為 A4, 直式
							bwd.SetMagin(2, 2, 2, 2, "cm");		// 紙張邊界
							bwd.FontSize = 9;					// 預設字形大小
							bwd.FontName = "新細明體";			// 預設中文字型
							bwd.FontEName = "Times New Roman";	// 預設英文字型

							bwd.SetHeadString(ds_name, "新細明體", "Times New Roman", 6, "left");
							bwd.SetFootString("列印時間：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), "新細明體", "Times New Roman", 6, "right");
							#endregion

							#region 設定檔名，並建立 Word 表頭
							context.Response.Clear();
							context.Response.Charset = "utf-8";
							
							// 檔名要先編碼，中文檔名才不會有問題
							context.Response.AddHeader("Content-Disposition", "inline;filename=" + context.Server.UrlEncode(ds_name.Replace(" ","") + ".doc"));

							// 設定檔案格式為 MS Word
							context.Response.ContentType = "application/msword";
							
							context.Response.Write(bwd.HeadData());		// 產生 Word 表頭							
							#endregion

							#region 建立報表內容
							
							#region 產生標題文字
							context.Response.Write("<p class=3DMsoNormal align=3Dcenter style=3D'margin-bottom:3.0pt;");
							context.Response.Write("mso-para-margin-bottom:.25gd;text-align:center;font-size:16.0pt;font-family:標楷體;'>" + ds_name + "</p>\n");
							#endregion

							#region 取得表格說明資料
							SqlString = "Select dt_sort, dt_name, dt_caption, dt_desc, dt_modi, init_time From Db_Table";
							SqlString += " Where ds_sid = @ds_sid Order by dt_sort";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("ds_sid", ds_sid);

							Sql_Reader = Sql_Command.ExecuteReader();

							#region 標題文字
							context.Response.Write("<table class=3DMsoTableGrid border=3D1 cellspacing=3D0 cellpadding=3D0 width=3D\"100%\"");
							context.Response.Write(" style=3D'width:100.0%;border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;");
							context.Response.Write(" mso-yfti-tbllook:480;mso-padding-alt:0cm 5.4pt 0cm 5.4pt;mso-border-insideh:");
							context.Response.Write(" .5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>\n");
							context.Response.Write("<thead>\n");
							context.Response.Write("<tr style=3D'font-size:9.0pt;background:#CCFFCC;text-align:center'>\n");
							context.Response.Write("<td width=3D40 style=3D'width:23.75pt'>序</td>\n");
							context.Response.Write("<td width=3D149 valign=3Dtop style=3D'width:89.65pt'>表格名稱</td>\n");
							context.Response.Write("<td width=3D227 valign=3Dtop style=3D'width:136.0pt'>中文標題</td>\n");
							context.Response.Write("<td width=3D214 valign=3Dtop style=3D'width:128.1pt'>功能說明</td>\n");
							context.Response.Write("<td width=3D112 style=3D'width:67.1pt'>最終修訂者</td>\n");
							context.Response.Write("<td width=3D80 style=3D'width:48.1pt'>修訂日期</td>\n");
							context.Response.Write("</tr>\n");
							context.Response.Write("</thead>\n");
							#endregion

							if (Sql_Reader.Read())
							{
								bgcolor = "background:#E0E0E0;";

								do
								{
									if (bgcolor == "")
										bgcolor = "background:#E0E0E0;";
									else
										bgcolor = "";

									dt_sort = int.Parse(Sql_Reader["dt_sort"].ToString()) / 10;
									dt_time = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd");

									context.Response.Write("<tr style=3D'font-size:9.0pt;" + bgcolor + "'>\n");
									context.Response.Write("<td align=3D'center'>" + dt_sort.ToString() + "</td>\n");
									context.Response.Write("<td>" + Sql_Reader["dt_name"].ToString().Trim() + "</td>\n");
									context.Response.Write("<td>" + Sql_Reader["dt_caption"].ToString().Trim() + "</td>\n");
									context.Response.Write("<td>" + Sql_Reader["dt_desc"].ToString().Trim() + "</td>\n");
									context.Response.Write("<td align=3D'center'>" + Sql_Reader["dt_modi"].ToString().Trim() + "</td>\n");
									context.Response.Write("<td align=3D'center'>" + dt_time + "</td>\n");
									context.Response.Write("</tr>\n");

								} while (Sql_Reader.Read());

								context.Response.Write("</table>");
							}
							else
							{
								context.Response.Write("</table>");
								context.Response.Write("<p style=3D'text-align:center'>※ 目前無任何資料 ※</p>");
							}

							Sql_Reader.Close();
							#endregion

							#endregion

							// 建立 Word 表尾
							context.Response.Write(bwd.FootData());
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
		}

		context.Response.End();
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