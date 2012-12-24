//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 縮圖顯示
//備註說明	使用資料庫儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class _30016 : System.Web.UI.Page
{
	public string[] ac_src = new string[20];
	public string[] ac_name = new string[20];
	public int[] ac_sid = new int[20];
	public int[] rownum = new int[20];
	public int maxpage = 0, pageid = 0, maxrow = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限，但不存登入紀錄
			//Check_Power("3001", false);

			string mErr = "";
			int iCnt = 0, ckint = 0, bCnt = 0, eCnt = 0;

			if (Request["al_sid"] == null)
				mErr = "參數接收有誤!\\n";
			else if (int.TryParse(Request["al_sid"], out ckint))
			{
				lb_al_sid.Text = ckint.ToString();

				if (Request["pageid"] == null)
				{
					lb_pageid.Text = "0";
					pageid = 0;
				}
				else if (int.TryParse(Request["pageid"], out pageid))
					lb_pageid.Text = pageid.ToString();
				else
				{
					lb_pageid.Text = "0";
					pageid = 0;
				}

				bCnt = pageid * 20 + 1;
				eCnt = bCnt + 19;

				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					Sql_Conn.Open();

					using (SqlCommand Sql_Command = new SqlCommand())
					{

						string SqlString = "";

						SqlDataReader Sql_Reader;

						#region 產生畫面使用參數
						SqlString = "Select * From (";
						SqlString = SqlString + "Select ac_sid, ac_name, ac_desc, Row_Number() Over (Order by ac_name) as rownum From Al_Content";
						SqlString = SqlString + " Where al_sid = @al_sid) as mTable";
						SqlString = SqlString + " Where rownum Between " + bCnt.ToString() + " And " + eCnt.ToString();

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("al_sid", lb_al_sid.Text);

						Sql_Reader = Sql_Command.ExecuteReader();

						for (iCnt = 0; iCnt < 20; iCnt++)
						{
							if (Sql_Reader.Read())
							{
								rownum[iCnt] = int.Parse(Sql_Reader["rownum"].ToString());
								ac_sid[iCnt] = int.Parse(Sql_Reader["ac_sid"].ToString());
								ac_src[iCnt] = "300161.ashx?ac_sid=" + Sql_Reader["ac_sid"].ToString();
								ac_name[iCnt] = Sql_Reader["ac_name"].ToString().Trim() + "\n" + Sql_Reader["ac_desc"].ToString().Trim().Replace("\"", "\\\"");
							}
							else
							{
								rownum[iCnt] = 0;
								ac_sid[iCnt] = 0;
								ac_src[iCnt] = "../images/blank.gif";
								ac_name[iCnt] = "沒有相片";
							}
						}

						Sql_Reader.Close();
						Sql_Reader.Dispose();
						#endregion

						#region 取得頁面筆數
						SqlString = "Select Count(*) as Cnt From Al_Content Where al_sid = @al_sid";

						Sql_Command.Parameters.Clear();
						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("al_sid", lb_al_sid.Text);

						Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							maxpage = (int.Parse(Sql_Reader["Cnt"].ToString()) + 19) / 20 - 1;
							maxrow = int.Parse(Sql_Reader["Cnt"].ToString());

							for (iCnt = 0; iCnt <= maxpage; iCnt++)
							{
								if (pageid == iCnt)
									lt_button.Text = lt_button.Text + "&nbsp;<a href=\"javascript:goPage(" + iCnt.ToString() + ")\" style=\"font-size:11pt\">&nbsp;[" + (iCnt + 1).ToString() + "]&nbsp;</a>";
								else
									lt_button.Text = lt_button.Text + "&nbsp;<a href=\"javascript:goPage(" + iCnt.ToString() + ")\" style=\"font-size:11pt\">&nbsp;" + (iCnt + 1).ToString() + "&nbsp;</a>";
							}
						}
						else
							maxpage = 0;
						#endregion
					}
				}
			}
			else
				mErr = "參數格式有問題!\\n";


			if (mErr != "")
			{
				for (iCnt = 0; iCnt < 20; iCnt++)
				{
					rownum[iCnt] = 0;
					ac_sid[iCnt] = 0;
					ac_src[iCnt] = "../images/blank.gif";
					ac_name[iCnt] = "沒有相片";
				}

				lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");</script>";
			}
		}
    }

	// 檢查使用者權限並存入登入紀錄
	private void Check_Power(string f_power, bool bl_save)
	{
		// 載入公用函數
		Common_Func cfc = new Common_Func();

		// 若 Session 不存在則直接顯示錯誤訊息
		try
		{
			if (cfc.Check_Power(Session["mg_sid"].ToString(), Session["mg_name"].ToString(), Session["mg_power"].ToString(), f_power, Request.ServerVariables["REMOTE_ADDR"], bl_save) > 0)
				Response.Redirect("../Error.aspx?ErrCode=1");
		}
		catch
		{
			Response.Redirect("../Error.aspx?ErrCode=2");
		}
	}
}
