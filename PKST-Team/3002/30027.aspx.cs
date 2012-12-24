//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 播幻燈片
//備註說明	使用實體路徑儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.IO;

public partial class _30027 : System.Web.UI.Page
{
	public int show_effect = 0, ac_width = 870, ac_height = 600, rownum = 1, maxrow = 0;
	public string fl_url = "", fl_url_encode, fl_name = "", ac_pic = "";

    protected void Page_Load(object sender, EventArgs e)
    {
		Decoder dcode = new Decoder();
		string mErr = "", fpath = "", fext = "", fname = "";
		string file_ext = ".jpg.gif.png.bmp.wmf";		// 允許使用的檔案副檔名
		int ckint = -1, iCnt = 0, rCnt = 0;

		if (!IsPostBack)
		{
			// 檢查使用者權限，但不存登入紀錄
			//Check_Power("3002", false);

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

			// 顯示效果
			if (Request["effect"] != null)
				if (int.TryParse(Request["effect"], out ckint))
					show_effect = ckint;
				else
					show_effect = 0;
			else
				show_effect = 0;

			if (Request["fl_url"] != null)
			{
				fl_url = dcode.DeCode(Request["fl_url"].Trim());
				if (fl_url.Substring(fl_url.Length - 1, 1) != "/")
					fl_url += "/";

				fpath = Server.MapPath(fl_url);
				if (fpath.Substring(fpath.Length - 1, 1) != "\\")
					fpath = fpath + "\\";

				if (Directory.Exists(fpath))
					fl_url_encode = Server.UrlEncode(dcode.EnCode(fl_url));
				else
					mErr = "找不到這個目錄!\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";

			if (mErr == "") {
				#region 處理圖形資料

				string[] mFiles = Directory.GetFiles(fpath, "*");

				if (mFiles.Length > 0)
				{
					Array.Sort(mFiles);

					maxrow = 0;
					rCnt = 0;

					for (iCnt = 0; iCnt < mFiles.Length; iCnt++)
					{
						fname = mFiles[iCnt].Replace(fpath, "").Replace("\\", "").ToLower();
						fext = Path.GetExtension(fname).ToString().ToLower();

						if (file_ext.Contains(fext))
						{
							maxrow++;

							if (rownum == maxrow)
							{
								rCnt = maxrow;
								ac_pic = fl_url + fname;
								fl_name = fname;
							}
							else if (maxrow == 1)
							{
								ac_pic = fl_url + fname;
								fl_name = fname;
							}
						}
					}

					if (maxrow == 0)
						mErr = "這個目錄已經沒有相片檔案了！\\n";
					else
					{
						#region 找不到指定順序的圖形
						if (rCnt == 0)
							rCnt = 1;
						#endregion
						rownum = rCnt;
					}
				}
				else
					mErr = "這個目錄已經沒有相片了！\\n";

				#endregion
			}

			if (mErr != "")
				lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");window.close();</script>";
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
