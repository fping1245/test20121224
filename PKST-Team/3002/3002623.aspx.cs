//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 縮圖顯示 > 全圖顯示 > 刪除相片
//備註說明	使用實體路徑儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.IO;

public partial class _3002623 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			Decoder dcode = new Decoder();
			string fl_name = "", fl_url = "", fpath = "";
			int rownum = 1;
			string mErr = "";

			// 檢查使用者權限，但不存登入紀錄
			//Check_Power("3002", false);

			#region 檢查傳入參數
			if (Request["fl_url"] == null || Request["fl_name"] == null || Request["rownum"] == null)
				mErr = "參數傳送錯誤!\\n";
			else
			{
				if (!int.TryParse(Request["rownum"], out rownum))
					rownum = 1;

				fl_name = Request["fl_name"].Trim().ToLower();
				fl_url = dcode.DeCode(Request["fl_url"].Trim());

				if (fl_name == "" || fl_url == "")
					mErr = "參數傳送錯誤!\\n";
			}
			#endregion

			if (mErr == "")
			{
				#region 取得相片資訊
				if (mErr == "")
				{
					fpath = Server.MapPath(fl_url);

					if (fpath.Substring(fpath.Length - 1, 1) != "\\")
						fpath += "\\";

					string[] mFiles = Directory.GetFiles(fpath, fl_name);
					if (mFiles.Length > 0)
					{
						FileInfo fi_obj = new FileInfo(mFiles[0].ToString());

						lb_path.Text = fpath;
						lb_rownum.Text = rownum.ToString();
						lb_fl_url_encode.Text = Server.UrlEncode(dcode.EnCode(fl_url));
						lb_ac_name.Text = fl_name;
						lb_ac_size.Text = fi_obj.Length.ToString("N0");
						lb_init_time.Text = fi_obj.LastWriteTime.ToString("yyyy/MM/dd HH:mm:ss");
						lb_ac_type.Text = fi_obj.Extension.ToLower();

						#region 讀取圖檔資料
						using (System.Drawing.Image img_obj = System.Drawing.Image.FromFile(fpath + fl_name))
						{
							lb_ac_wh.Text = img_obj.Width.ToString() + " × " + img_obj.Height.ToString();
						}
						#endregion
					}
					else
						mErr = "找不到指定的相片!\\n";

					mFiles = null;
				}
				#endregion
			}

			if (mErr != "")
				lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");parent.close_all();parent.clean_win();</script>";
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

	// 確定刪除
	protected void bn_ok_Click(object sender, EventArgs e)
	{
		string mErr = "", fullname = "", fname = "", fext = "";
		string file_ext = ".jpg.gif.png.bmp.wmf";		// 允許使用的檔案副檔名
		int iCnt = 0, maxrow = 0;

		#region 刪除相片
		fullname = lb_path.Text + lb_ac_name.Text;

		if (File.Exists(fullname))
		{
			try
			{
				// 刪除相片縮圖
				File.Delete(lb_path.Text + "_thumb\\" + lb_ac_name.Text + ".jpg");

				// 刪除相片
				File.Delete(fullname);

				if (File.Exists(fullname))
					mErr = "檔案無法刪除!\\n";
				else
				{
					#region 檢查該目錄允許使用的檔案是否全部清光
					string[] mFiles = Directory.GetFiles(lb_path.Text, "*");
					if (mFiles.Length > 0)
					{
						for (iCnt = 0; iCnt < mFiles.Length; iCnt++)
						{
							fname = mFiles[iCnt].Replace(lb_path.Text, "").Replace("\\", "").ToLower();
							fext = Path.GetExtension(fname).ToString().ToLower();

							// 檢查副檔名，非允許的檔案不顯示
							if (file_ext.Contains(fext))
								maxrow++;
						}
					}
					else
						maxrow = 0;

					if (maxrow > 0)
					{
						//	返回的相片順序比現有檔案數還大的處理
						if (maxrow < int.Parse(lb_rownum.Text))
							lb_rownum.Text = maxrow.ToString();
					}
					else
					{
						// 已無檔案
						mErr = "本目錄已無檔案，將結束檢視功能!\\n";
						lb_rownum.Text = "0";
					}

					mFiles = null;
					#endregion
				}
			}
			catch (Exception ex)
			{
				mErr = "檔案刪除失敗!\\n" + ex.Message.ToString().Replace("\\","\\\\");
			}
		}
		#endregion

		if (mErr == "")
		{
			lt_show.Text = "<script language=javascript>alert(\"資料已刪除!\");parent.thumb_reload();";
			lt_show.Text = lt_show.Text + "parent.location.href=\"300262.aspx?fl_url=" + lb_fl_url_encode.Text + "&rownum=" + lb_rownum.Text + "\";</script>";
		}
		else
		{
			if (lb_rownum.Text == "0")
				lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");parent.thumb_reload();parent.window.close();</script>";
			else
				lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");</script>";
		}
	}
}
