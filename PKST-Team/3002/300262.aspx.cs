//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 縮圖顯示 > 全圖顯示
//備註說明	使用實體路徑儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.IO;

public partial class _300262 : System.Web.UI.Page
{
	public string ac_pic = "", fl_url = "", fl_url_encode, fl_name = "";
	public int show_effect = 0, tb_width = 870, tb_height = 600, maxrow = 0, rownum = 1;
	Decoder dcode = new Decoder();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			int ckint = -1, iCnt = 0;
			string mErr = "", lb_path = "", fname = "", fext = "";
			string file_ext = ".jpg.gif.png.bmp.wmf";		// 允許使用的檔案副檔名

			// 檢查使用者權限，但不存登入紀錄
			//Check_Power("3002", false);

			#region 傳入參數處理
			// 上下一筆時處理用的指標
			if (Request["rownum"] != null)
			{
				if (int.TryParse(Request["rownum"], out ckint))
					rownum = ckint;
				else
					rownum = 1;
			}
			else
				rownum = 1;

			// 取得本頁最大筆數
			if (Request["maxrow"] != null)
			{
				if (int.TryParse(Request["maxrow"], out ckint))
					maxrow = ckint;
				else
					maxrow = 1;
			}
			else
				maxrow = 1;

			// 顯示效果
			if (Request["effect"] != null)
				if (int.TryParse(Request["effect"], out ckint))
					show_effect = ckint;

			// 畫布區域寬度
			if (Request["tbw"] != null)
				if (int.TryParse(Request["tbw"], out ckint))
					tb_width = ckint;

			// 畫布區域高度
			if (Request["tbh"] != null)
				if (int.TryParse(Request["tbh"], out ckint))
					tb_height = ckint;
			#endregion

			// 相片名稱處理
			if (Request["fname"] == null)
				fl_name = "";
			else
				fl_name = Request["fname"].Trim().ToLower();

			if (Request["fl_url"] != null)
			{
				#region 相片列表處理
				fl_url = dcode.DeCode(Request["fl_url"].Trim());
				fl_url_encode = Server.UrlEncode(dcode.EnCode(fl_url));

				if (fl_url.Substring(fl_url.Length - 1, 1) != "/")
					fl_url += "/";
				
				lb_path = Server.MapPath(fl_url);
				if (lb_path.Substring(lb_path.Length - 1, 1) != "\\")
					lb_path += "\\";

				string[] mFiles = Directory.GetFiles(lb_path, "*");
				if (mFiles.Length > 0)
				{
					Array.Sort(mFiles);

					maxrow = 0;

					if (fl_name != "")
					{
						// 有指定檔名時，以檔名搜尋
						for (iCnt = 0; iCnt < mFiles.Length; iCnt++)
						{
							fname = mFiles[iCnt].Replace(lb_path, "").Replace("\\", "").ToLower();
							fext = Path.GetExtension(fname).ToString().ToLower();

							// 檢查副檔名，非允許的檔案不顯示
							if (file_ext.Contains(fext))
							{
								maxrow++;

								if (fname == fl_name)
								{
									rownum = maxrow;
									ac_pic = fl_url + fname;
									fl_name = fname;
								}
							}
						}
					}
					else
					{
						// 未指定檔名時，以順序搜尋圖檔
						for (iCnt = 0; iCnt < mFiles.Length; iCnt++)
						{
							fname = mFiles[iCnt].Replace(lb_path, "").Replace("\\", "").ToLower();
							fext = Path.GetExtension(fname).ToString().ToLower();

							// 檢查副檔名，非允許的檔案不顯示
							if (file_ext.Contains(fext))
							{
								maxrow++;

								if (rownum == maxrow)
								{
									rownum = maxrow;
									ac_pic = fl_url + fname;
									fl_name = fname;
								}
							}
						}
					}
				}
				else
				{
					ac_pic = "../images/blank.gif";
					mErr = "找不到任何檔案!\\n";
				}
				mFiles = null;
				#endregion
			}
			else
			{
				ac_pic = "../images/blank.gif";
				mErr = "相片參數傳送錯誤!\\n";
			}

			if (mErr != "")
			{
				lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");window.close();</script>";
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
