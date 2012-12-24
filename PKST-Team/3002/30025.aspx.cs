//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 上傳檔案
//備註說明	使用實體路徑儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.Drawing.Imaging;
using System.IO;

public partial class _30025 : System.Web.UI.Page
{
	Decoder dcode = new Decoder();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			string mErr = "";

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("3002", false);

			if (Request["fl_url"] != null)
			{
				lb_fl_url.Text = dcode.DeCode(Request["fl_url"].Trim());
				lb_path.Text = Server.MapPath(lb_fl_url.Text);
				if (lb_path.Text.Substring(lb_path.Text.Length - 1, 1) != "\\")
					lb_path.Text = lb_path.Text + "\\";

				if (!Directory.Exists(lb_path.Text))
					mErr = "目錄不存在!\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";

			if (mErr != "")
				lt_show.Text = "<script language=\"javascript\">alert(\"" + mErr + "\");parent.close_all();parent.clean_win();</script>";
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

	// 檔案存檔
	protected void bn_upfile_ok_Click(object sender, EventArgs e)
	{
		int iCnt = 0, ac_width = 0, ac_height = 0, s_width = 0, s_height = 0;
		double fCnt = 0.0;
		string mErr = "";
		string fname = "", fext = "", tmpstr = "", fullname = "";
		string file_ext = ".jpg.gif.png.bmp.wmf";		// 允許上傳的檔案副檔名

		#region 儲存檔案
		if (fu_upfile.HasFile)
		{
			fname = fu_upfile.FileName;
			fext = Path.GetExtension(fname).ToString().ToLower();

			// 確認副檔名是否為允許上傳的檔案
			if (file_ext.Contains(fext))
			{
				#region 檢查檔案是否存在，存在時要修改檔名 xxxx1.ext、xxxx2.ext、xxxx3.ext ....
				tmpstr = fname.Replace(fext, "");
				while (File.Exists(lb_path.Text + fname))
				{
					iCnt++;
					fname = tmpstr + fCnt.ToString() + fext;
				}
				#endregion

				#region 開始存檔
				fullname = lb_path.Text + fname;

				try
				{
					fu_upfile.SaveAs(fullname);

					if (!File.Exists(fullname))
						mErr = "檔案儲存失敗!\\n";
					else
					{
						#region 取得圖型資料及縮圖處理
						// FileUpload 的檔案內容存入 Image
						using (System.Drawing.Image img_tmp = System.Drawing.Image.FromFile(fullname))
						{
							ac_height = img_tmp.Height;		// 實際高度
							ac_width = img_tmp.Width;		// 實際寬度

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

							#region 呼叫 Bitmap 物件的 GetThumbnailImage 方法來建立一個縮圖
							using (System.Drawing.Image img_thumb = img_tmp.GetThumbnailImage(s_width, s_height,
								new System.Drawing.Image.GetThumbnailImageAbort(img_Abort), IntPtr.Zero))
							{
								fullname = lb_path.Text + "_thumb\\" + fname + ".jpg";

								// 縮圖的壓縮比為 75%
								EncoderParameters eps = new EncoderParameters();
								eps.Param[0] = new EncoderParameter(Encoder.Quality, (long)75);

								img_thumb.Save(fullname, GetEncoderInfo("image/jpeg"), eps);

								// 以預設壓縮比儲存 jpeg (75%)
								// img_thumb.Save(fullname, System.Drawing.Imaging.ImageFormat.Jpeg);
							}
							#endregion
						}
						#endregion
					}
				}
				catch
				{
					mErr = "檔案無法儲存!\\n";
				}
				#endregion
			}
		}
		#endregion

		if (mErr == "")
			lt_show.Text = "<script language=\"javascript\">alert(\"檔案上傳完成!\\n\");parent.close_all();parent.thumb_reload();</script>";
		else
			lt_show.Text = "<script language=\"javascript\">alert(\"" + mErr + "\");parent.close_all();parent.clean_win();</script>";	
	}

	private bool img_Abort()
	{
		return false;
	}

	// 取得圖形編碼器
	private ImageCodecInfo GetEncoderInfo(string strmime)
	{
		ImageCodecInfo ici = null;
		ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

		foreach (ImageCodecInfo codec in codecs)
		{
			if (codec.MimeType == strmime)
				ici = codec;
		}

		return ici;
	}

}
