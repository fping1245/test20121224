//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 縮圖顯示 > 全圖顯示 > 相片內容說明
//備註說明	使用實體路徑儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.IO;

public partial class _3002622 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			Decoder dcode = new Decoder();
			string mErr = "", fl_name = "", fl_url = "", fpath = "";

			// 檢查使用者權限，但不存登入紀錄
			//Check_Power("3002", false);

			if (Request["fl_name"] == null || Request["fl_url"] == null)
				mErr = "參數傳送錯誤!\\n";
			else
			{
				fl_name = Request["fl_name"].Trim().ToLower();
				fl_url = dcode.DeCode(Request["fl_url"].Trim());

				if (fl_name == "" || fl_url == "")
					mErr = "參數傳送錯誤!\\n";
			}

			#region 取得相片資訊
			if (mErr == "") {
				fpath = Server.MapPath(fl_url);

				if (fpath.Substring(fpath.Length - 1, 1) != "\\")
					fpath += "\\";

				string[] mFiles = Directory.GetFiles(fpath, fl_name);
				if (mFiles.Length > 0)
				{
					FileInfo fi_obj = new FileInfo(mFiles[0].ToString());

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
}
