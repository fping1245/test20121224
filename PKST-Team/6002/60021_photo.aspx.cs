//---------------------------------------------------------------------------- 
//程式功能	通訊錄管理 > 詳細內容 > 相片處理
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;

public partial class _60021_photo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0, ab_sid = -1;
			string mErr = "", SqlString = "";

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("6002", false);

			#region 承接上一頁的查詢條件設定
			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out ab_sid))
				{
					lb_ab_sid.Text = ab_sid.ToString();

					if (Request["pageid"] != null)
					{
						if (int.TryParse(Request["pageid"].ToString(), out ckint))
							lb_page.Text = "?pageid=" + ckint.ToString();
						else
							lb_page.Text = "?pageid=0";
					}

					if (Request["ab_name"] != null)
						lb_page.Text = lb_page.Text + "&ab_name=" + Server.UrlEncode(Request["ab_name"]);

					if (Request["ab_nike"] != null)
						lb_page.Text = lb_page.Text + "&ab_nike=" + Server.UrlEncode(Request["ab_nike"]);

					if (Request["ab_company"] != null)
						lb_page.Text = lb_page.Text + "&ab_company=" + Server.UrlEncode(Request["ab_company"]);

					if (Request["ag_name"] != null)
						lb_page.Text = lb_page.Text + "&ag_name=" + Server.UrlEncode(Request["ag_name"]);

					if (Request["ag_attrib"] != null)
						lb_page.Text = lb_page.Text + "&ag_attrib=" + Server.UrlEncode(Request["ag_attrib"]);

					if (Request["sort"] != null)
						lb_page.Text += "&sort=" + Request["sort"];

					if (Request["row"] != null)
						lb_page.Text += "&row=" + Request["row"];

					lb_page.Text = lb_page.Text + "&sid=" + ab_sid.ToString();
				}
				else
					mErr = "參數型態有誤!\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";
			#endregion

			#region 取得連絡人資料
			if (mErr == "")
			{
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					SqlString = "Select Top 1 g.ag_name, g.ag_attrib, b.ab_name, b.ab_nike";
					SqlString += ", (Case When ab_photo Is Null Then 0 Else 1 End) as is_photo";
					SqlString += " From As_Book b";
					SqlString += " Inner Join As_Group g On b.ag_sid = g.ag_sid";
					SqlString += " Where b.ab_sid = @ab_sid And b.mg_sid = @mg_sid";

					using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
					{
						Sql_Conn.Open();

						Sql_Command.Parameters.AddWithValue("ab_sid", lb_ab_sid.Text);
						Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());

						using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
						{
							if (Sql_Reader.Read())
							{
								lb_ag_name.Text = Sql_Reader["ag_name"].ToString().Trim() + " (" + Sql_Reader["ag_attrib"].ToString().Trim() + ")";
								lb_ab_name.Text = Sql_Reader["ab_name"].ToString().Trim();
								lb_ab_nike.Text = Sql_Reader["ab_nike"].ToString().Trim();

								if (Sql_Reader["is_photo"].ToString() == "0")
									img_ab_photo.ImageUrl = "~/images/ico/no_photo.gif";
								else
									img_ab_photo.ImageUrl = "600211.ashx?sid=" + lb_ab_sid.Text;
							}
							else
								mErr = "找不到連絡人資料!\\n";

							Sql_Reader.Close();
							Sql_Reader.Dispose();
						}
					}
				}
			}
			#endregion

			if (mErr != "")
				lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");history.go(-1);</script>";
		}
    }

	#region Check_Power() 檢查使用者權限並存入登入紀錄
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
	#endregion

	// 上傳相片 (並將圖檔縮成 120 * 120 存在資料庫內)
	protected void bn_send_Click(object sender, EventArgs e)
	{
		double fCnt = 1.0;
		int ac_width = 0, ac_height = 0, s_width = 0, s_height = 0;
		string SqlString = "", mErr = "";
		string f_name = "", f_ext = "";
		string file_ext = ".jpg.gif.png.bmp.wmf";		// 允許上傳的檔案副檔名

		#region 儲存檔案
		if (fu_file.HasFile)
		{
			// 處理上傳檔案，說明及檔案內容存入資料庫
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				Sql_Conn.Open();

				using (SqlCommand Sql_Command = new SqlCommand())
				{
					MemoryStream ms_tmp = new MemoryStream();

					f_name = fu_file.FileName;
					f_ext = Path.GetExtension(f_name).ToString().ToLower();

					if (file_ext.Contains(f_ext))
					{
						#region 取得圖型資料及縮圖處理
						// FileUpload 的檔案內容存入 Image
						using (System.Drawing.Image img_tmp = System.Drawing.Image.FromStream(fu_file.PostedFile.InputStream, true))
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

							#region 呼叫 Bitmap 物件的 GetThumbnailImage 方法來建立一個縮圖。
							using (System.Drawing.Image img_thumb = img_tmp.GetThumbnailImage(s_width, s_height,
								new System.Drawing.Image.GetThumbnailImageAbort(img_Abort), IntPtr.Zero))
							{
								img_thumb.Save(ms_tmp, System.Drawing.Imaging.ImageFormat.Jpeg);
							}
							#endregion
						}
						#endregion

						#region 檔案存入資料庫
						SqlString = "Update As_Book Set ab_photo = @ab_photo Where ab_sid = @ab_sid And mg_sid = @mg_sid";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Connection = Sql_Conn;

						Sql_Command.Parameters.AddWithValue("ab_sid", lb_ab_sid.Text);
						Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
						Sql_Command.Parameters.AddWithValue("ab_photo", ms_tmp.ToArray());

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
						mErr = "不接受「" + f_name + "」的檔案格式!\\n(僅接受「" + file_ext + "」等格式)\\n";
				}
			}
		}
		else
			mErr = "請選擇上傳的檔案!\\n";
		#endregion

		if (mErr == "")
			lt_show.Text = "<script language=\"javascript\">alert(\"相片上傳完成!\\n\");location.replace(\"60021_photo.aspx" + lb_page.Text + "\");</script>";
		else
			lt_show.Text = "<script language=\"javascript\">alert(\"" + mErr + "\\n\");close_msg_wait();</script>";
	}

	private bool img_Abort()
	{
		return false;
	}

	// 刪除相片
	protected void bn_del_Click(object sender, EventArgs e)
	{
		string SqlString = "", mErr = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Update As_Book Set ab_photo = null Where ab_sid = @ab_sid And mg_sid = @mg_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
	
				Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
				Sql_Command.Parameters.AddWithValue("ab_sid", lb_ab_sid.Text);

				Sql_Command.ExecuteNonQuery();
			}
		}

		if (mErr == "")
			lt_show.Text = "<script language=\"javascript\">alert(\"相片完成刪除!\\n\");location.replace(\"60021_photo.aspx" + lb_page.Text + "\");</script>";
		else
			lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");</script>";
	}
}
