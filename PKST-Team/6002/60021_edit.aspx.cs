//---------------------------------------------------------------------------- 
//程式功能	通訊錄管理 > 明細內容 > 修改資料
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _60021_edit : System.Web.UI.Page
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
					if (Request["pageid"] != null)
					{
						if (int.TryParse(Request["pageid"].ToString(), out ckint))
							lb_page.Text = "?pageid=" + ckint.ToString();
						else
							lb_page.Text = "?pageid=0";
					}

					if (Request["ab_name"] != null)
						lb_page.Text += "&ab_name=" + Server.UrlEncode(Request["ab_name"]);

					if (Request["ab_nike"] != null)
						lb_page.Text += "&ab_nike=" + Server.UrlEncode(Request["ab_nike"]);

					if (Request["ab_company"] != null)
						lb_page.Text += "&ab_company=" + Server.UrlEncode(Request["ab_company"]);

					if (Request["ag_name"] != null)
						lb_page.Text += "&ag_name=" + Server.UrlEncode(Request["ag_name"]);

					if (Request["ag_attrib"] != null)
						lb_page.Text += "&ag_attrib=" + Server.UrlEncode(Request["ag_attrib"]);

					if (Request["sort"] != null)
						lb_page.Text += "&sort=" + Request["sort"];
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
					SqlString = "Select Top 1 b.ag_sid, b.ab_name, b.ab_nike, b.ab_zipcode, b.ab_address";
					SqlString += ", b.ab_tel_h, b.ab_tel_o, b.ab_mobil, b.ab_fax, b.ab_email";
					SqlString += ", b.ab_posit, b.ab_company, b.ab_desc, b.init_time";
					SqlString += " From As_Book b";
					SqlString += " Where b.ab_sid = @ab_sid And b.mg_sid = @mg_sid";

					using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
					{
						Sql_Conn.Open();

						Sql_Command.Parameters.AddWithValue("ab_sid", ab_sid);
						Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());

						using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
						{
							if (Sql_Reader.Read())
							{
								lb_ab_sid.Text = ab_sid.ToString();
								ddl_As_Group.SelectedValue = Sql_Reader["ag_sid"].ToString();
								tb_ab_name.Text = Sql_Reader["ab_name"].ToString().Trim();
								tb_ab_nike.Text = Sql_Reader["ab_nike"].ToString().Trim();
								tb_ab_zipcode.Text = Sql_Reader["ab_zipcode"].ToString().Trim();
								tb_ab_address.Text = Sql_Reader["ab_address"].ToString().Trim();
								tb_ab_tel_h.Text = Sql_Reader["ab_tel_h"].ToString().Trim();
								tb_ab_tel_o.Text = Sql_Reader["ab_tel_o"].ToString().Trim();
								tb_ab_mobil.Text = Sql_Reader["ab_mobil"].ToString().Trim();
								tb_ab_fax.Text = Sql_Reader["ab_fax"].ToString().Trim();
								tb_ab_email.Text = Sql_Reader["ab_email"].ToString().Trim();
								tb_ab_posit.Text = Sql_Reader["ab_posit"].ToString().Trim();
								tb_ab_company.Text = Sql_Reader["ab_company"].ToString().Trim();
								tb_ab_desc.Text = Sql_Reader["ab_desc"].ToString().Trim();
								lb_init_time.Text = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
							}
							else
								mErr = "找不到連絡人資料!\\n";
						}
					}
				}
			}
			#endregion

			if (mErr != "")
				lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");history.go(-1);</script>";
		}
    }

	// Check_Power() 檢查使用者權限並存入登入紀錄
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

	protected void lb_ok_Click(object sender, EventArgs e)
	{
		string mErr = "";

		// 載入字串函數
		String_Func sfc = new String_Func();

		if (tb_ab_name.Text.Trim() == "")
			mErr = mErr + "「姓名」沒有輸入!\\n";

		if (tb_ab_nike.Text.Trim() == "")
			mErr = mErr + "「暱稱」沒有輸入!\\n";

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				string SqlString = "";
				Decoder decoder = new Decoder();

				// 建立 SQL 的語法
				SqlString = "Update As_Book Set ag_sid = @ag_sid, ab_name = @ab_name, ab_nike = @ab_nike";
				SqlString += ", ab_zipcode = @ab_zipcode, ab_address = @ab_address, ab_tel_h = @ab_tel_h";
				SqlString += ", ab_tel_o = @ab_tel_o, ab_mobil = @ab_mobil, ab_fax = @ab_fax, ab_email = @ab_email";
				SqlString += ", ab_posit = @ab_posit, ab_company = @ab_company, ab_desc = @ab_desc, init_time = getdate()";
				SqlString += " Where ab_sid = @ab_sid And mg_sid = @mg_sid";

				SqlCommand Sql_Command = new SqlCommand();

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;

				// 擷取字串到資料庫所規範的大小 cfc.Left(string mdata, int leng)
				Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
				Sql_Command.Parameters.AddWithValue("ab_sid", lb_ab_sid.Text);
				Sql_Command.Parameters.AddWithValue("ag_sid", ddl_As_Group.SelectedValue.ToString());
				Sql_Command.Parameters.AddWithValue("ab_name", sfc.Left(tb_ab_name.Text, 50));
				Sql_Command.Parameters.AddWithValue("ab_nike", sfc.Left(tb_ab_nike.Text, 50));
				Sql_Command.Parameters.AddWithValue("ab_zipcode", sfc.Left(tb_ab_zipcode.Text, 5));
				Sql_Command.Parameters.AddWithValue("ab_address", sfc.Left(tb_ab_address.Text, 150));
				Sql_Command.Parameters.AddWithValue("ab_tel_h", sfc.Left(tb_ab_tel_h.Text, 50));
				Sql_Command.Parameters.AddWithValue("ab_tel_o", sfc.Left(tb_ab_tel_o.Text, 50));
				Sql_Command.Parameters.AddWithValue("ab_mobil", sfc.Left(tb_ab_mobil.Text, 50));
				Sql_Command.Parameters.AddWithValue("ab_fax", sfc.Left(tb_ab_fax.Text, 50));
				Sql_Command.Parameters.AddWithValue("ab_email", sfc.Left(tb_ab_email.Text, 100));
				Sql_Command.Parameters.AddWithValue("ab_posit", sfc.Left(tb_ab_posit.Text, 50));
				Sql_Command.Parameters.AddWithValue("ab_company", sfc.Left(tb_ab_company.Text, 50));
				Sql_Command.Parameters.AddWithValue("ab_desc", sfc.Left(tb_ab_desc.Text, 500));

				Sql_Conn.Open();

				Sql_Command.ExecuteNonQuery();

				Sql_Command.Dispose();
			}
		}

		if (mErr == "")
		{
			mErr = "alert(\"存檔完成!\\n\");location.replace(\"60021.aspx" + lb_page.Text + "&sid=" + lb_ab_sid.Text + "\");";
		}
		else
			mErr = "alert('" + mErr + "')";

		lt_show.Text = "<script language=javascript>" + mErr + "</script>";
	}
}
