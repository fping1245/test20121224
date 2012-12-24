<%@ WebHandler Language="C#" Class="_F0021" %>
//---------------------------------------------------------------------------- 
//程式功能	產生 Excel 檔案  > 產生成績單
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _F0021 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "", tp_title = "", tp_desc = "";
		int tp_sid = -1, tp_question = 0, tp_score = 0;
		float tp_avg = 0, tp_member = 0;
		StringBuilder tmpstr = new StringBuilder();
		SqlDataReader Sql_Reader;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("F002", false, context);

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
						SqlString = "Select Top 1 tp_title, tp_desc, tp_score, tp_question, tp_total, tp_member From Ts_Paper Where tp_sid = @tp_sid";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("tp_sid", tp_sid);

						Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							tp_title = Sql_Reader["tp_title"].ToString();
							tp_desc = Sql_Reader["tp_desc"].ToString();
							tp_score = int.Parse(Sql_Reader["tp_score"].ToString());
							tp_question = int.Parse(Sql_Reader["tp_question"].ToString());
							tp_member = float.Parse(Sql_Reader["tp_member"].ToString());
							
							if (tp_member == 0)
								tp_avg = 0;
							else
								tp_avg = float.Parse(Sql_Reader["tp_total"].ToString()) / tp_member;
						}
						else
							mErr = "找不到試卷主題!\\n";

						Sql_Reader.Close();			
						#endregion

						#region 產生 Excel 檔
						if (mErr == "")
						{
							#region 設定 Excel 格式
							Build_Excel bexcel = new Build_Excel();

							bexcel.Author = "陸士權";				// 作者
							bexcel.Company = "章立民研究室";		// 公司
							bexcel.Title = tp_title;				// 標題
							bexcel.SetPaper("A4", "S");				// 設定紙張為 A4, 直式
							bexcel.SetMagin(2, 2, 2, 2, "cm");		// 紙張邊界
							bexcel.FontSize = 12;					// 預設字形大小

							bexcel.SetHeadString(tp_title, "新細明體", 6, "left");
							bexcel.SetFootString("第 &P 頁 / 共 &N 頁", "新細明體", 6, "center");
							bexcel.Sheet = "成績清單";
							
							#region 設定自定 CSS 格式
							// 文字置中 (含框)
							tmpstr.AppendLine(".a_center");
							tmpstr.AppendLine("{mso-style-parent:style0;");
							tmpstr.AppendLine("border:.5pt solid windowtext;");
							tmpstr.AppendLine("text-align:center;}");
							
							// 文字靠右 (含框)
							tmpstr.AppendLine(".a_right");
							tmpstr.AppendLine("{mso-style-parent:style0;");
							tmpstr.AppendLine("border:.5pt solid windowtext;");
							tmpstr.AppendLine("text-align:right;}");

							// 文字靠右 (含框有背景色)
							tmpstr.AppendLine(".a_right_b");
							tmpstr.AppendLine("{mso-style-parent:style0;");
							tmpstr.AppendLine("border:.5pt solid windowtext;");
							tmpstr.AppendLine("background:#CCFFCC;");			// 填入背景色1 (兩行都要)
							tmpstr.AppendLine("mso-pattern:auto none;");		// 填入背景色2 (兩行都要)
							tmpstr.AppendLine("text-align:right;}");
							
							// 標楷體 18pt 置中
							tmpstr.AppendLine(".k18");
							tmpstr.AppendLine("{mso-style-parent:style0;");
							tmpstr.AppendLine("font-size:18.0pt;");
							tmpstr.AppendLine("font-family:標楷體, cursive;");
							tmpstr.AppendLine("text-align:center;}");

							// 14pt 置中 (含框)
							tmpstr.AppendLine(".a_c14");
							tmpstr.AppendLine("{mso-style-parent:style0;");
							tmpstr.AppendLine("font-size:14.0pt;");
							tmpstr.AppendLine("border:.5pt solid windowtext;");
							tmpstr.AppendLine("background:#CCFFCC;");			// 填入背景色1 (兩行都要)
							tmpstr.AppendLine("mso-pattern:auto none;");		// 填入背景色2 (兩行都要)
							tmpstr.AppendLine("text-align:center;}");							

							// 8pt 置右
							tmpstr.AppendLine(".a_r08");
							tmpstr.AppendLine("{mso-style-parent:style0;");
							tmpstr.AppendLine("font-size:8.0pt;");
							tmpstr.AppendLine("text-align:right;}");
							
							// 整數
							tmpstr.AppendLine(".num01");
							tmpstr.AppendLine("{mso-style-parent:style0;");
							tmpstr.AppendLine("mso-number-format:\"\\@\";}");

							// 四位小數 (含框)	
							tmpstr.AppendLine(".num02");
							tmpstr.AppendLine("{mso-style-parent:style0;");
							tmpstr.AppendLine("border:.5pt solid windowtext;");
							tmpstr.AppendLine("mso-number-format:\"\\#\\,\\#\\#0\\.0000_ \";}");
							
							bexcel.DefineStyle = tmpstr.ToString();
							#endregion

							#endregion

							#region 設定檔名，並建立 Excel 表頭
							context.Response.Clear();
							context.Response.Charset = "utf-8";
							
							// 檔名要先編碼，中文檔名才不會有問題
							context.Response.AddHeader("Content-Disposition", "inline;filename=" + context.Server.UrlEncode(tp_title.Replace(" ","") + ".xls"));

							// 設定檔案格式為 MS Excel
							context.Response.ContentType = "application/vnd.ms-excel";
							
							context.Response.Write(bexcel.HeadData());		// 產生 Excel 表頭							
							#endregion

							#region 建立報表內容
							
							#region 產生試卷主題文字
							context.Response.Write("<table x:str border=0 cellpadding=0 cellspacing=0 width=671 style='border-collapse:");
							context.Response.Write("collapse;table-layout:fixed;width:505pt'>\n");
							context.Response.Write("<col width=93 style='mso-width-source:userset;mso-width-alt:2976;width:70pt'>\n");
 							context.Response.Write("<col width=124 style='mso-width-source:userset;mso-width-alt:3968;width:93pt'>\n");
							context.Response.Write("<col width=162 span=2 style='mso-width-source:userset;mso-width-alt:5184;width:122pt'>\n");
  							context.Response.Write("<col width=130 style='mso-width-source:userset;mso-width-alt:4160;width:98pt'>\n");
  							context.Response.Write("<tr height=34 style='height:25.5pt'>\n");
							context.Response.Write("<td colspan=5 height=34 class=k18 width=671 style='height:25.5pt;width:505pt'>" + tp_title + "</td>\n");
							context.Response.Write("</tr>\n");
							context.Response.Write("<tr height=22 style='height:16.5pt'>\n");
							context.Response.Write("<td colspan=5 height=22 class=a_r08 style='height:16.5pt'>列印時間：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "</td>\n");
							context.Response.Write("</tr>\n");
							context.Response.Write("<tr height=22 style='height:16.5pt'>\n");
							context.Response.Write("<td height=22 class=a_right_b style='height:16.5pt'>試卷總分：</td>\n");
							context.Response.Write("<td class=a_right x:num>" + tp_score.ToString() + "</td>\n");
							context.Response.Write("<td class=a_right ></td>\n");
							context.Response.Write("<td class=a_right_b>試卷題數：</td>\n");
							context.Response.Write("<td class=a_right x:num>" + tp_question.ToString() + "</td>\n");
							context.Response.Write("</tr>\n");
							context.Response.Write("<tr height=22 style='height:16.5pt'>\n");
							context.Response.Write("<td height=22 class=a_right_b style='height:16.5pt'>平均分數：</td>\n");
							context.Response.Write("<td class=num02 x:num=\"" + tp_avg.ToString() + "\">" + tp_avg.ToString() + "</td>\n");
							context.Response.Write("<td class=a_right></td>\n");
							context.Response.Write("<td class=a_right_b>參加人數：</td>\n");
							context.Response.Write("<td class=a_right x:num>" + tp_member.ToString() + "</td>\n");
							context.Response.Write("</tr>\n");
							context.Response.Write("<tr height=22 style='height:16.5pt'>\n");
							context.Response.Write("<td colspan=5 height=22 style='height:16.5pt; font-size:18.0pt; text-align:center;'>成績清單</td>\n");
							context.Response.Write("</tr>\n");
							context.Response.Write("<tr height=28 style='height:21.0pt'>\n");
							context.Response.Write("<td height=28 class=a_c14 style='height:21.0pt'>名次</td>\n");
							context.Response.Write("<td class=a_c14>成績</td>\n");
							context.Response.Write("<td class=a_c14>姓名</td>\n");
							context.Response.Write("<td class=a_c14>學號</td>\n");
							context.Response.Write("<td class=a_c14>答對題數</td>\n");
							context.Response.Write("</tr>\n");
							#endregion

							#region 取得考生排名及成績，同時開始產生報表明細內容
							SqlString = "Select tu_sort, tu_score, tu_name, tu_no, tu_question From Ts_User";
							SqlString += " Where tp_sid = @tp_sid Order by tu_sort";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("tp_sid", tp_sid);

							Sql_Reader = Sql_Command.ExecuteReader();

							if (Sql_Reader.Read())
							{
								do
								{
									context.Response.Write("<tr height=22 style='height:16.5pt'>\n");
									context.Response.Write("<td height=22 class=a_center style='height:16.5pt' x:num>" + Sql_Reader["tu_sort"].ToString() + "</td>\n");
									context.Response.Write("<td class=a_right x:num>" + Sql_Reader["tu_score"].ToString() + "</td>\n");
									context.Response.Write("<td class=a_center>" + Sql_Reader["tu_name"].ToString().Trim() + "</td>\n");
									context.Response.Write("<td class=a_center>" + Sql_Reader["tu_no"].ToString().Trim() + "</td>\n");
									context.Response.Write("<td class=a_right x:num>" + Sql_Reader["tu_question"].ToString() + "</td>\n");
									context.Response.Write("</tr>\n");
								} while (Sql_Reader.Read());
							}
							else
								context.Response.Write("<tr><td colspan=5 align=center>※ 目前無任何資料 ※</td></tr>\n");
							
							context.Response.Write("</table>");

							Sql_Reader.Close();
							#endregion

							#endregion

							// 建立 Word 表尾
							context.Response.Write(bexcel.FootData());
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