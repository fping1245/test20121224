<%@ WebHandler Language="C#" Class="_G00147" %>
//---------------------------------------------------------------------------- 
//程式功能	資料庫規格管理 > 資料表清單 > 產生單一資料表
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間
using System.Web.Configuration;

public class _G00147 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context)
	{
		string mErr = "", SqlString = "", ds_code = "", ds_name = "", dr_name = "";
		string dt_time = "", dt_modi = "", dt_name = "", dt_caption = "", dt_area = "", dt_desc = "", dt_index = "";
		int ds_sid = -1, dt_sid = -1, dr_sort =-1;
		SqlDataReader Sql_Reader;
		
		// 檢查使用者權限，但不存入登入紀錄
		//Check_Power("G001", false, context);

		if (context.Request["ds_sid"] != null && context.Request["dt_sid"] != null)
		{
			if (int.TryParse(context.Request["ds_sid"], out ds_sid) && int.TryParse(context.Request["dt_sid"], out dt_sid))
			{
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						Sql_Conn.Open();
						Sql_Command.Connection = Sql_Conn;

						#region 取得系統名稱、代號及表格說明資料
						SqlString = "Select Top 1 s.ds_code, s.ds_name, t.init_time, t.dt_modi, t.dt_name, t.dt_caption";
						SqlString += ", t.dt_area, t.dt_desc, t.dt_index From Db_Table t";
						SqlString += " Inner Join Db_Sys s on t.ds_sid = s.ds_sid Where t.dt_sid = @dt_sid And t.ds_sid = @ds_sid";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("ds_sid", ds_sid);
						Sql_Command.Parameters.AddWithValue("dt_sid", dt_sid);

						Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							ds_code = Sql_Reader["ds_code"].ToString().Trim();
							ds_name = Sql_Reader["ds_name"].ToString().Trim();
							dt_time = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd");
							dt_modi = Sql_Reader["dt_modi"].ToString().Trim();
							dt_name = Sql_Reader["dt_name"].ToString().Trim();
							dt_caption = Sql_Reader["dt_caption"].ToString().Trim();
							dt_area = Sql_Reader["dt_area"].ToString().Trim();
							dt_desc = Sql_Reader["dt_desc"].ToString().Trim();
							dt_index = Sql_Reader["dt_index"].ToString().Trim().Replace("\r\n","<br>");
						}
						else
							mErr = "找不到資料庫表格說明資料!\\n";

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
							context.Response.AddHeader("Content-Disposition", "inline;filename=" + context.Server.UrlEncode(dt_name.Replace(" ","") + ".doc"));

							// 設定檔案格式為 MS Word
							context.Response.ContentType = "application/msword";
							
							context.Response.Write(bwd.HeadData());		// 產生 Word 表頭							
							#endregion

							#region 建立報表內容
							
							#region 產生表格標題
							context.Response.Write("<table class=3DMsoTableGrid border=3D1 cellspacing=3D0 cellpadding=3D0 width=3D\"100%\"");
							context.Response.Write("style=3D'width:100.0%;border-collapse:collapse;border:none;mso-border-alt:solid windowtext .5pt;");
							context.Response.Write("mso-yfti-tbllook:480;mso-padding-alt:0cm 5.4pt 0cm 5.4pt;mso-border-insideh:");
							context.Response.Write(".5pt solid windowtext;mso-border-insidev:.5pt solid windowtext'>\n");
							context.Response.Write("<thead>\n");
							context.Response.Write("<tr style=3D'font-size:9.0pt;margin:.25gd 0gd .25gd 0gd'>\n");
							context.Response.Write("<td width=3D80 colspan=3D2 style=3D'width:48.15pt;text-align:center'>系統代號</td>\n");
							context.Response.Write("<td width=3D335 colspan=3D5 style=3D'width:201.25pt'>" + ds_code + "</td>\n");
							context.Response.Write("<td width=3D92 colspan=3D2 style=3D'width:54.9pt;text-align:center'>系統名稱</td>\n");
							context.Response.Write("<td width=3D314 colspan=3D2 style=3D'width:188.4pt'>" + ds_name + "</td>\n");
							context.Response.Write("</tr>\n");
							context.Response.Write("<tr style=3D'font-size:9.0pt;margin:.25gd 0gd .25gd 0gd'>\n");
							context.Response.Write("<td width=3D80 colspan=3D2 style=3D'width:48.15pt;text-align:center'>修定日期</td>\n");
							context.Response.Write("<td width=3D335 colspan=3D5 style=3D'width:201.25pt'>" + dt_time + "</td>\n");
							context.Response.Write("<td width=3D92 colspan=3D2 style=3D'width:54.9pt;text-align:center'>修定人員</td>\n");
							context.Response.Write("<td width=3D314 colspan=3D2 style=3D'width:188.4pt'>" + dt_modi + "</td>\n");
							context.Response.Write("</tr>\n");
							context.Response.Write("<tr style=3D'font-size:9.0pt;margin:.25gd 0gd .25gd 0gd'>\n");
							context.Response.Write("<td width=3D80 colspan=3D2 style=3D'width:48.15pt;text-align:center'>表格名稱</td>\n");
							context.Response.Write("<td width=3D335 colspan=3D5 style=3D'width:201.25pt'>" + dt_name + "</td>\n");
							context.Response.Write("<td width=3D92 colspan=3D2 style=3D'width:54.9pt;text-align:center'>中文標題</td>\n");
							context.Response.Write("<td width=3D314 colspan=3D2 style=3D'width:188.4pt'>" + dt_caption + "</td>\n");
							context.Response.Write("</tr>\n");
							context.Response.Write("<tr style=3D'font-size:9.0pt;margin:.25gd 0gd .25gd 0gd'>\n");
							context.Response.Write("<td width=3D80 colspan=3D2 style=3D'width:48.15pt;text-align:center'>所在位置</td>\n");
							context.Response.Write("<td width=3D335 colspan=3D5 style=3D'width:201.25pt'>" + dt_area + "</td>\n");
							context.Response.Write("<td width=3D92 colspan=3D2 style=3D'width:54.9pt;text-align:center'>功能說明</td>\n");
							context.Response.Write("<td width=3D314 colspan=3D2 style=3D'width:188.4pt'>" + dt_desc + "</td>\n");
							context.Response.Write("</tr>\n");
							context.Response.Write("<tr style=3D'font-size:9.0pt;margin:.25gd 0gd .25gd 0gd'>\n");
							context.Response.Write("<td width=3D80 colspan=3D2 style=3D'width:48.15pt;text-align:center'>索引欄位</td>\n");
							context.Response.Write("<td width=3D741 colspan=3D9 style=3D'width:444.55pt'>" + dt_index + "</td>\n");
							context.Response.Write("</tr>\n");
							#endregion

							#region 取得表格欄位資料
							SqlString = "Select dr_sort, dr_name, dr_caption, dr_type, dr_len, dr_point, dr_default, dr_desc";
							SqlString += " From Db_Record Where ds_sid = @ds_sid And dt_sid = @dt_sid Order by dr_sort";

							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.Clear();
							Sql_Command.Parameters.AddWithValue("ds_sid", ds_sid);
							Sql_Command.Parameters.AddWithValue("dt_sid", dt_sid);

							Sql_Reader = Sql_Command.ExecuteReader();

							#region 欄位標題
							context.Response.Write("<tr style=3D'font-size:9.0pt;text-align:center;margin:.25gd 0gd .25gd 0gd'>\n");
							context.Response.Write("<td width=3D33 rowspan=3D2 style=3D'width:19.85pt'>序<br>號</td>\n");
							context.Response.Write("<td width=3D115 colspan=3D2 rowspan=3D2 style=3D'width:68.95pt'>欄位代號</td>\n");
							context.Response.Write("<td width=3D158 rowspan=3D2 style=3D'width:95.0pt'>欄位名稱</td>\n");
							context.Response.Write("<td width=3D53 rowspan=3D2 style=3D'width:31.6pt'>型<br>態</td>\n");
							context.Response.Write("<td width=3D100 colspan=3D3 style=3D'width:60.0pt'>長度</td>\n");
							context.Response.Write("<td width=3D143 colspan=3D2 rowspan=3D2 style=3D'width:85.5pt'>規格限定<br>(預設值)</td>\n");
							context.Response.Write("<td width=3D220 rowspan=3D2 style=3D'width:131.8pt'>內容說明</td>\n");
							context.Response.Write("</tr>\n");
							context.Response.Write("<tr style=3D'font-size:9.0pt;text-align:center;margin:.25gd 0gd .25gd 0gd'>\n");
							context.Response.Write("<td width=3D50 style=3D'width:30.0pt'>總寬</td>\n");
							context.Response.Write("<td width=3D50 colspan=3D2 style=3D'width:30.0pt'>小數</td>\n");
							context.Response.Write("</tr>\n");
							context.Response.Write("</thead>\n");
							#endregion
							
							if (Sql_Reader.Read())
							{
								do
								{
									dr_sort = int.Parse(Sql_Reader["dr_sort"].ToString()) / 10;
									dr_name = Sql_Reader["dr_name"].ToString().Trim();

									context.Response.Write("<tr style=3D'font-size:9.0pt;margin:.25gd 0gd .25gd 0gd'>\n");
									context.Response.Write("<td style=3D'text-align:right'>" + dr_sort.ToString() + "</td>\n");
									context.Response.Write("<td colspan=3D2>" + dr_name + "</td>\n");
									context.Response.Write("<td>" + Sql_Reader["dr_caption"].ToString().Trim() + "</td>\n");
									context.Response.Write("<td>" + Sql_Reader["dr_type"].ToString().Trim() + "</td>\n");
									if (dr_name == "")
									{
										context.Response.Write("<td style=3D'text-align:right'></td>\n");
										context.Response.Write("<td colspan=3D2 style=3D'text-align:right'></td>\n");
									}
									else
									{
										context.Response.Write("<td style=3D'text-align:right'>" + Sql_Reader["dr_len"].ToString().Trim() + "</td>\n");
										context.Response.Write("<td colspan=3D2 style=3D'text-align:right'>" + Sql_Reader["dr_point"].ToString().Trim() + "</td>\n");
									}
									context.Response.Write("<td colspan=3D2>" + Sql_Reader["dr_default"].ToString().Trim() + "</td>\n");
									context.Response.Write("<td>" + Sql_Reader["dr_desc"].ToString().Trim() + "</td>\n");
									context.Response.Write("</tr>\n");
		
								} while (Sql_Reader.Read());
								
								context.Response.Write("</table>\n");
							}
							else
							{
								context.Response.Write("</table>\n");
								context.Response.Write("<p class=3DMsoNormal align=3Dright style=3D'text-align:center;font-size:9.0pt'>※ 目前無任何資料 ※</p>\n");
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