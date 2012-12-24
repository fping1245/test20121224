//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 縮圖顯示 > 全圖顯示 > 修改相片名稱
//備註說明	使用實體路徑儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.IO;

public partial class _3002624 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			Decoder dcode = new Decoder();
			string mErr = "";

			// 檢查使用者權限，但不存登入紀錄
			//Check_Power("3002", false);

			#region 檢查傳入參數
			if (Request["fl_url"] == null || Request["fl_name"] == null)
				mErr = "參數傳送錯誤!\\n";
			else
			{
				tb_ac_name.Text = Request["fl_name"].Trim().ToLower();
				lb_fl_name.Text = tb_ac_name.Text;
				lb_fl_url.Text = dcode.DeCode(Request["fl_url"].Trim());

				if (lb_fl_url.Text == "" || lb_fl_name.Text == "")
					mErr = "參數傳送錯誤!\\n";
				else
				{
					lb_path.Text = Server.MapPath(lb_fl_url.Text);
					if (lb_path.Text.Substring(lb_path.Text.Length-1,1) != "\\")
						lb_path.Text += "\\";

					if (File.Exists(lb_path.Text + lb_fl_name.Text))
						lb_fl_url_encode.Text = Server.UrlEncode(dcode.EnCode(lb_fl_url.Text));
					else
						mErr = "找不到這相片!\\n";
				}
			}
			#endregion

			if (mErr == "")
			{
				#region 取得相片資訊
				if (mErr == "")
				{
					string[] mFiles = Directory.GetFiles(lb_path.Text, lb_fl_name.Text);
					if (mFiles.Length > 0)
					{
						FileInfo fi_obj = new FileInfo(mFiles[0].ToString());

						lb_ac_size.Text = fi_obj.Length.ToString("N0");
						lb_init_time.Text = fi_obj.LastWriteTime.ToString("yyyy/MM/dd HH:mm:ss");
						lb_ac_type.Text = fi_obj.Extension.ToLower();

						#region 讀取圖檔資料
						using (System.Drawing.Image img_obj = System.Drawing.Image.FromFile(lb_path.Text + lb_fl_name.Text))
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

	// 確定修改
	protected void bn_ok_Click(object sender, EventArgs e)
	{
		string mErr = "", sfname = "", nfname = "", fext = "";
		string file_ext = ".jpg.gif.png.bmp.wmf";		// 允許使用的檔案副檔名

		if (tb_ac_name.Text == "")
			mErr = "相片名稱一定要填寫!\\n";
		else
		{
			tb_ac_name.Text = tb_ac_name.Text.ToLower();
			if (tb_ac_name.Text == lb_fl_name.Text)
				mErr = "新舊相片名稱相同!\\n";
			else
			{
				// 檢查副檔名
				fext = Path.GetExtension(tb_ac_name.Text).ToString().ToLower();
				if (! file_ext.Contains(fext))
					mErr = "不接受這種檔案格式!\\n";
			}
		}

		if (mErr == "")
		{
			#region 修改名稱
			try
			{
				#region 修改縮圖名稱
				sfname = lb_path.Text + "_thumb\\" + lb_fl_name.Text + ".jpg";
				nfname = lb_path.Text + "_thumb\\" + tb_ac_name.Text + ".jpg";

				File.Move(sfname, nfname);
				#endregion

				#region 修改相片名稱
				sfname = lb_path.Text + lb_fl_name.Text;
				nfname = lb_path.Text + tb_ac_name.Text;

				File.Move(sfname, nfname);
				#endregion
			}
			catch
			{
				mErr = "相片更名失敗!\\n";
			}
			#endregion
		}

		if (mErr == "")
		{
			lt_show.Text = "<script language=javascript>alert(\"相片名稱修改完成!\");parent.thumb_reload();";
			lt_show.Text += "parent.location.replace(\"300262.aspx?fl_url=" + lb_fl_url_encode.Text + "&fname=" + Server.UrlEncode(tb_ac_name.Text) + "\");</script>";
		}
		else
			lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");</script>";

	}
}
