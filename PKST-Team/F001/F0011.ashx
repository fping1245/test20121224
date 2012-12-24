<%@ WebHandler Language="C#" Class="_F0011" %>
//---------------------------------------------------------------------------- 
//程式功能	產生 Word 檔案  > 產生空白試卷
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _F0011 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "", tp_title = "", tp_desc = "", tq_sid = "", tq_type = "";
		int tp_sid = -1, tp_question = 0, tp_score = 0;
		DateTime b_time = DateTime.Now, e_time = DateTime.Now;
		SqlDataReader Sql_Reader;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("F001", false, context);

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
						SqlString = "Select Top 1 tp_title, tp_desc, tp_score, tp_question, b_time, e_time From Ts_Paper Where tp_sid = @tp_sid";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("tp_sid", tp_sid);

						Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							tp_title = Sql_Reader["tp_title"].ToString();
							tp_desc = Sql_Reader["tp_desc"].ToString();
							tp_score = int.Parse(Sql_Reader["tp_score"].ToString());
							tp_question = int.Parse(Sql_Reader["tp_question"].ToString());
							b_time = DateTime.Parse(Sql_Reader["b_time"].ToString());
							e_time = DateTime.Parse(Sql_Reader["e_time"].ToString());
						}
						else
							mErr = "找不到試卷主題!\\n";

						Sql_Reader.Close();			
						#endregion

						#region 產生 Word 檔
						if (mErr == "")
						{
							#region 設定 Word 格式
							Build_Word bwd = new Build_Word();

							bwd.Author = "陸士權";				// 作者
							bwd.Company = "章立民研究室";		// 公司
							bwd.Title = tp_title;				// 標題
							bwd.SetPaper("A4", "S");			// 設定紙張為 A4, 直式
							bwd.SetMagin(2, 2, 2, 2, "cm");		// 紙張邊界
							bwd.FontSize = 12;					// 預設字形大小
							bwd.FontName = "新細明體";			// 預設中文字型
							bwd.FontEName = "Times New Roman";	// 預設英文字型

							bwd.SetHeadString(tp_title, "新細明體", "Times New Roman", 6, "left");
							bwd.SetFootString(tp_title, "新細明體", "Times New Roman", 6, "right");
							#endregion

							#region 設定檔名，並建立 Word 表頭
							context.Response.Clear();
							context.Response.Charset = "utf-8";
							
							// 檔名要先編碼，中文檔名才不會有問題
							context.Response.AddHeader("Content-Disposition", "inline;filename=" + context.Server.UrlEncode(tp_title.Replace(" ","") + ".doc"));

							// 設定檔案格式為 MS Word
							context.Response.ContentType = "application/msword";
							
							context.Response.Write(bwd.HeadData());		// 產生 Word 表頭							
							#endregion

							#region 建立報表內容
							
							#region 產生試卷主題文字
							context.Response.Write("<p style=3D'font-size:14pt; font-family:標楷體; text-align:center; margin: 0pt 0pt 2pt 0pt'>" + tp_title + "</p>\n");
							context.Response.Write("<table border=3D1 cellspacing=3D0 cellpadding=3D4 style=3D'width:100%; font-size:9pt; border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-yfti-tbllook:480;mso-padding-alt:0cm 5.4pt 0cm 5.4pt;mso-border-insideh:.5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>\n");
							context.Response.Write("<tr><td style=3D'width:12%; text-align:center'>考生姓名</td>\n");
							context.Response.Write("<td style=3D'width:38%; text-align:left'>&nbsp;</td>\n");
							context.Response.Write("<td style=3D'width:12%; text-align:center'>考生學號</td>\n");
							context.Response.Write("<td style=3D'width:38%; text-align:left'>&nbsp;</td></tr>\n");
							context.Response.Write("<tr><td style=3D'width:12%; text-align:center'>試卷說明</td>\n");
							context.Response.Write("<td colspan=3D3 style=3D'width:88%; text-align:left'>" + tp_desc.Replace("\r\n","<br>") + "</td></tr>\n");
							context.Response.Write("<tr><td style=3D'width:12%; text-align:center'>試卷題數</td>\n");
							context.Response.Write("<td style=3D'width:38%; text-align:left'>" + tp_question.ToString() + " 題</td>\n");
							context.Response.Write("<td style=3D'width:12%; text-align:center'>試卷總分</td>\n");
							context.Response.Write("<td style=3D'width:38%; text-align:left'>" + tp_score.ToString() + " 分</td></tr>\n");
							context.Response.Write("<tr><td style=3D'width:12%; text-align:center'>開始時間</td>\n");
							context.Response.Write("<td style=3D'width:38%; text-align:left'>" + b_time.ToString("yyyy/MM/dd HH:mm") + "</td>\n");
							context.Response.Write("<td style=3D'width:12%; text-align:center'>截止時間</td>\n");
							context.Response.Write("<td style=3D'width:38%; text-align:left'>" + e_time.ToString("yyyy/MM/dd HH:mm") + "</td></tr>\n");
							context.Response.Write("</table>\n<br>");
							#endregion

							#region 取得試卷題目及選項，同時開始產生報表明細內容
							SqlString = "Select q.tq_sid, q.tq_sort, q.tq_desc, q.tq_type, q.tq_score, i.ti_sort, i.ti_desc From Ts_Question q";
							SqlString += " Inner Join Ts_Item i On q.tp_sid = i.tp_sid And q.tq_sid = i.tq_sid";
							SqlString += " Where q.tp_sid = @tp_sid Order by q.tq_sort, i.ti_sort";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("tp_sid", tp_sid);

							Sql_Reader = Sql_Command.ExecuteReader();

							context.Response.Write("<table border=3D1 cellspacing=3D0 cellpadding=3D4 style=3D'width:100%; font-size:9pt; border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;mso-yfti-tbllook:480;mso-padding-alt:0cm 5.4pt 0cm 5.4pt;mso-border-insideh:.5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>\n");
							if (Sql_Reader.Read())
							{
								tq_sid = "";
								do
								{
									if (tq_sid == Sql_Reader["tq_sid"].ToString())
									{
										context.Response.Write("(" + Sql_Reader["ti_sort"].ToString() + ") " + Sql_Reader["ti_desc"].ToString() + "<br>");
									}
									else
									{
										if (tq_sid != "")
										{
											context.Response.Write("</td></tr>\n");
										}

										tq_type = Sql_Reader["tq_type"].ToString();
										if (tq_type == "0")
											tq_type = "單選";
										else if (tq_type == "1")
											tq_type = "複選<br>全部";
										else
											tq_type = "複選<br>" + tq_type + " 題";

										context.Response.Write("<tr><td style=3D'width:8%'>&nbsp;</td>\n");
										context.Response.Write("<td style=3D'width:8%; text-align:center'>" + Sql_Reader["tq_sort"].ToString() + "<br><font color=3DBlue>" + tq_type + "</font></td>\n");
										context.Response.Write("<td style=3D'width:84%; text-align:left'>" + Sql_Reader["tq_desc"].ToString().Replace("\r\n", "<br>") + "\n<br>");
										context.Response.Write("(" + Sql_Reader["ti_sort"].ToString() + ") " + Sql_Reader["ti_desc"].ToString() + "<br>");

										tq_sid = Sql_Reader["tq_sid"].ToString();
									}
								} while (Sql_Reader.Read());
								context.Response.Write("</td></tr>\n");
							}
							else
								context.Response.Write("<tr><td style=3D'text-align:center'>※ 目前無任何資料 ※</td></tr>");
							
							context.Response.Write("</table>");

							Sql_Reader.Close();
							#endregion

							#endregion

							// 建立 Word 表尾
							context.Response.Write(bwd.FootData());
							context.Response.End();
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