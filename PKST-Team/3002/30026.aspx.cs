//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 縮圖顯示
//備註說明	使用實體路徑儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.IO;

public partial class _30026 : System.Web.UI.Page
{
	public string[] ac_src = new string[20];
	public string[] ac_name = new string[20];
	public int[] rownum = new int[20];
	public int maxpage = 0, pageid = 0, maxrow = 0;

	Decoder decode = new Decoder();

    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限，但不存登入紀錄
			//Check_Power("3002", false);

			string mErr = "", fname = "", fext = "", tname = "", turl = "";
			int iCnt = 0, bCnt = 0, eCnt = 0, mCnt = 0, ac_width = 0, ac_height = 0, s_width = 0, s_height = 0;
			double fCnt = 0.0;
			string file_ext = ".jpg.gif.png.bmp.wmf";		// 允許使用的檔案副檔名

			for (iCnt = 0; iCnt < 20; iCnt++)
			{
				rownum[iCnt] = 0;
				ac_src[iCnt] = "../images/blank.gif";
				ac_name[iCnt] = "沒有相片";
			}

			if (Request["fl_url"] == null)
				mErr = "參數接收有誤!\\n";
			else
			{
				lb_fl_url.Text = decode.DeCode(Request["fl_url"].Trim());
				if (lb_fl_url.Text.Substring(lb_fl_url.Text.Length - 1, 1) == "/")
					lb_fl_url.Text = lb_fl_url.Text.Substring(0, lb_fl_url.Text.Length - 1);

				lb_fl_url_encode.Text = Server.UrlEncode(decode.EnCode(lb_fl_url.Text));

				lb_path.Text = Server.MapPath(lb_fl_url.Text);
				if (lb_path.Text.Substring(lb_path.Text.Length-1, 1) != "\\")
					lb_path.Text = lb_path.Text + "\\";

				// 檢查目錄及縮圖目錄
				if (Directory.Exists(lb_path.Text))
				{
					if (! Directory.Exists(lb_path.Text + "_thumb\\"))
					{
						try
						{
							Directory.CreateDirectory(lb_path.Text + "_thumb\\");
						}
						catch
						{
							mErr = "縮圖目錄無法處理!\n";
						}
					}
				}
				else
					mErr = "找不到這個目錄!\\n" + lb_path.Text.Replace("\\","\\\\");
			}

			if (mErr == "")
			{
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

				bCnt = pageid * 20;
				eCnt = bCnt + 20;

				string[] mFiles = Directory.GetFiles(lb_path.Text, "*");

				if (mFiles.Length > 0)
				{
					Array.Sort(mFiles);

					mCnt = 0;
					maxrow = 0;
					for (iCnt = 0; iCnt < mFiles.Length; iCnt++)
					{
						fname = mFiles[iCnt].Replace(lb_path.Text, "").Replace("\\", "");
						fext = Path.GetExtension(fname).ToString().ToLower();

						// 檢查副檔名，非允許的檔案不顯示
						if (file_ext.Contains(fext))
						{
							maxrow++;

							turl = lb_fl_url.Text + "/_thumb/" + fname + ".jpg";
							tname = lb_path.Text + "_thumb\\" + fname + ".jpg";
							if (iCnt >= bCnt && iCnt < eCnt)
							{
								// 檢查是否有縮圖
								if (!File.Exists(tname))
								{
									#region 建立縮圖
									using (System.Drawing.Image img_obj = System.Drawing.Image.FromFile(lb_path.Text + fname))
									{
										ac_height = img_obj.Height;		// 實際高度
										ac_width = img_obj.Width;		// 實際寬度

										// 維持圖檔比例的方式，計算與縮圖 120 * 120 的比例
										if (ac_width > ac_height)
											fCnt = ac_width / 120.0;
										else
											fCnt = ac_height / 120.0;

										// 實際圖比縮圖大時才要處理，否則仍為原圖檔尺寸
										if (fCnt > 1)
										{
											s_width = (int)(ac_width / fCnt);		// 縮圖寬度
											s_height = (int)(ac_height / fCnt);		// 縮圖高度
										}
										else
										{
											s_width = ac_width;						// 縮圖寬度
											s_height = ac_height;					// 縮圖高度
										}

										#region 呼叫 Bitmap 物件的 GetThumbnailImage 方法來建立一個縮圖。
										using (System.Drawing.Image img_thumb = img_obj.GetThumbnailImage(s_width, s_height,
											new System.Drawing.Image.GetThumbnailImageAbort(img_Abort), IntPtr.Zero))
										{
											img_thumb.Save(tname, System.Drawing.Imaging.ImageFormat.Jpeg);
										}
										#endregion
									}
									#endregion
								}
								rownum[mCnt] = iCnt + 1;
								ac_src[mCnt] = turl;
								ac_name[mCnt] = fname;
								mCnt++;
							}
						}
					}

					if (mCnt < 19)
					{
						for (iCnt = mCnt; iCnt < 20; iCnt++)
						{
							rownum[iCnt] = 0;
							ac_src[iCnt] = "../images/blank.gif";
							ac_name[iCnt] = "沒有相片";
						}
					}

					#region 產生頁面筆數
					maxpage = (maxrow + 19) / 20 - 1;

					for (iCnt = 0; iCnt <= maxpage; iCnt++)
					{
						if (pageid == iCnt)
							lt_button.Text = lt_button.Text + "&nbsp;<a href=\"javascript:goPage(" + iCnt.ToString() + ")\" style=\"font-size:11pt\">&nbsp;[" + (iCnt + 1).ToString() + "]&nbsp;</a>";
						else
							lt_button.Text = lt_button.Text + "&nbsp;<a href=\"javascript:goPage(" + iCnt.ToString() + ")\" style=\"font-size:11pt\">&nbsp;" + (iCnt + 1).ToString() + "&nbsp;</a>";
					}
					#endregion
				}
				mFiles = null;
			}

			if (mErr != "")
				lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");</script>";
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

	private bool img_Abort()
	{
		return false;
	}
}
