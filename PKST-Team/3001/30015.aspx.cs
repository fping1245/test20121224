//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 上傳檔案
//備註說明	使用資料庫儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Configuration;

public partial class _30015 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			int ckint = -1;
			string mErr = "";

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("3001", false);

			if (Request["al_sid"] != null)
			{
				if (int.TryParse(Request["al_sid"], out ckint))
					lb_al_sid.Text = ckint.ToString();
				else
					mErr = "參數傳送錯誤！\\n";
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
		double fCnt = 1.0;
		int ac_size = 0, ac_width = 0, ac_height = 0, s_width = 0, s_height = 0;
		string SqlString = "", mErr = "";
		string ac_name = "", ac_ext = "", ac_desc = "", ac_type = "";
		string file_ext = ".jpg.gif.png.bmp.wmf";		// 允許上傳的檔案副檔名

		#region 儲存檔案
		if (fu_upfile.HasFile)
		{
			// 處理上傳檔案，說明及檔案內容存入資料庫
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				Sql_Conn.Open();

				using (SqlCommand Sql_Command = new SqlCommand())
				{
					MemoryStream ms_tmp = new MemoryStream();

					ac_name = fu_upfile.FileName;
					ac_ext = Path.GetExtension(ac_name).ToString().ToLower();

					if (file_ext.Contains(ac_ext))
					{
						ac_size = fu_upfile.PostedFile.ContentLength;
						ac_type = fu_upfile.PostedFile.ContentType;
						ac_desc = tb_ac_desc.Text.Trim();

						#region 取得圖型資料及縮圖處理
						// FileUpload 的檔案內容存入 Image
						using (System.Drawing.Image img_tmp = System.Drawing.Image.FromStream(fu_upfile.PostedFile.InputStream, true))
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
								// 縮圖的壓縮比為 75%
								EncoderParameters eps = new EncoderParameters();
								eps.Param[0] = new EncoderParameter(Encoder.Quality, (long)75);

								img_thumb.Save(ms_tmp, GetEncoderInfo("image/jpeg"), eps);

								// 以預設壓縮比儲存 jpeg (75%)
								// img_thumb.Save(ms_tmp, ImageFormat.Jpeg);
							}
							#endregion
						}
						#endregion

						#region 檔案存入資料庫
						SqlString = "Insert Into Al_Content (al_sid, ac_name, ac_ext, ac_size, ac_type, ac_desc";
						SqlString = SqlString + " , ac_content, ac_width, ac_height, ac_thumb)";
						SqlString = SqlString + " Values (@al_sid, @ac_name, @ac_ext, @ac_size, @ac_type, @ac_desc";
						SqlString = SqlString + " , @ac_content, @ac_width, @ac_height, @ac_thumb)";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Connection = Sql_Conn;
						Sql_Command.Parameters.AddWithValue("al_sid", lb_al_sid.Text);
						Sql_Command.Parameters.AddWithValue("ac_name", ac_name);
						Sql_Command.Parameters.AddWithValue("ac_ext", ac_ext);
						Sql_Command.Parameters.AddWithValue("ac_size", ac_size);
						Sql_Command.Parameters.AddWithValue("ac_type", ac_type);
						Sql_Command.Parameters.AddWithValue("ac_desc", ac_desc);
						Sql_Command.Parameters.AddWithValue("ac_content", fu_upfile.FileBytes);
						Sql_Command.Parameters.AddWithValue("ac_width", ac_width);
						Sql_Command.Parameters.AddWithValue("ac_height", ac_height);
						Sql_Command.Parameters.AddWithValue("ac_thumb", ms_tmp.ToArray());

						Sql_Command.ExecuteNonQuery();

						ms_tmp.Close();
						ms_tmp.Dispose();

						//預設上傳檔案大小為 4096KB, 執行時間 120秒, 如要修改，要到 Web.Config 處修改下列數據
						//<system.web>
						//<httpRuntime maxRequestLength="4096" executionTimeout="120"/>
						//</system.web>

						#endregion
					}
					else
						mErr = "不接受「" + ac_name + "」的檔案格式!\\n(僅接受「" + file_ext + "」等格式)\\n";
				}
			}
		}
		else
			mErr = "沒有選擇任何上傳的檔案!\\n";
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
