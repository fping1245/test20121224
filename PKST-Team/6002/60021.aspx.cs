//---------------------------------------------------------------------------- 
//程式功能	通訊錄管理 > 詳細內容
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _60021 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0, ab_sid = -1, pageid = 0;
			string mErr = "", SqlString = "";
			string ab_name = "", ab_nike = "", ab_company = "", ag_name = "", ag_attrib = "";

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("6002", false);

			#region 承接上一頁的查詢條件設定
			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out ab_sid))
				{
					if (Request["pageid"] != null)
						int.TryParse(Request["pageid"].ToString(), out pageid);

					if (Request["ab_name"] != null)
						ab_name = Server.UrlEncode(Request["ab_name"]);

					if (Request["ab_nike"] != null)
						ab_nike = Server.UrlEncode(Request["ab_nike"]);

					if (Request["ab_company"] != null)
						ab_company = Server.UrlEncode(Request["ab_company"]);

					if (Request["ag_name"] != null)
						ag_name = Server.UrlEncode(Request["ag_name"]);

					if (Request["ag_attrib"] != null)
						ag_attrib = Server.UrlEncode(Request["ag_attrib"]);

					if (Request["sort"] != null)
					{
						if (Request["sort"] == "")
							lb_sort.Text = "ab_name";
						else
							lb_sort.Text = Request["sort"];
					}
					else
						lb_sort.Text = "ab_name";

					if (Request["row"] != null)
					{
						if (int.TryParse(Request["row"], out ckint))
							lb_row.Text = ckint.ToString();
						else
							lb_row.Text = "1";
					}
					else
						lb_row.Text = "1";
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
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						Sql_Conn.Open();

						#region 取得最大筆數
						SqlString = "Select Count(*) as Cnt From As_Book b";
						SqlString += " Inner Join As_Group g On b.ag_sid = g.ag_sid";
						SqlString += " Where b.mg_sid = @mg_sid";
						SqlString += GetSqlString(ab_name, ab_nike, ab_company, ag_name, ag_attrib);

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());

						lb_maxrow.Text = Sql_Command.ExecuteScalar().ToString();
						#endregion

						#region 取得人員資料
						SqlString = "Select * From (";
						SqlString += "Select b.ab_sid, g.ag_name, g.ag_attrib, b.ab_name, b.ab_nike, b.ab_zipcode";
						SqlString += ", b.ab_address, b.ab_tel_h, b.ab_tel_o, b.ab_mobil, b.ab_fax, b.ab_email";
						SqlString += ", b.ab_posit, b.ab_company, b.ab_desc, b.init_time";
						SqlString += ", (Case When ab_photo Is Null Then 0 Else 1 End) as is_photo";
						SqlString += ", Row_Number() Over (Order by " + lb_sort.Text + ") as rownum";
						SqlString += " From As_Book b";
						SqlString += " Inner Join As_Group g On b.ag_sid = g.ag_sid";
						SqlString += " Where b.mg_sid = @mg_sid";
						SqlString += GetSqlString(ab_name, ab_nike, ab_company, ag_name, ag_attrib);
						SqlString += ") as MLog";

						if (ab_sid > 0)
							SqlString += " Where ab_sid = @ab_sid";
						else
							SqlString += " Where rownum = @rownum";

						Sql_Command.Parameters.Clear();
						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());

						if (ab_sid > 0)
							Sql_Command.Parameters.AddWithValue("ab_sid", ab_sid.ToString());
						else
							Sql_Command.Parameters.AddWithValue("rownum", lb_row.Text);

						using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
						{
							if (Sql_Reader.Read())
							{
								lb_ab_sid.Text = Sql_Reader["ab_sid"].ToString();
								lb_ag_name.Text = Sql_Reader["ag_name"].ToString().Trim() + " (" + Sql_Reader["ag_attrib"].ToString().Trim() + ")";
								lb_ab_name.Text = Sql_Reader["ab_name"].ToString().Trim();
								lb_ab_nike.Text = Sql_Reader["ab_nike"].ToString().Trim();
								lb_ab_zipcode.Text = Sql_Reader["ab_zipcode"].ToString().Trim();
								lb_ab_address.Text = Sql_Reader["ab_address"].ToString().Trim();
								lb_ab_tel_h.Text = Sql_Reader["ab_tel_h"].ToString().Trim();
								lb_ab_tel_o.Text = Sql_Reader["ab_tel_o"].ToString().Trim();
								lb_ab_mobil.Text = Sql_Reader["ab_mobil"].ToString().Trim();
								lb_ab_fax.Text = Sql_Reader["ab_fax"].ToString().Trim();
								lb_ab_email.Text = Sql_Reader["ab_email"].ToString().Trim();
								lb_ab_posit.Text = Sql_Reader["ab_posit"].ToString().Trim();
								lb_ab_company.Text = Sql_Reader["ab_company"].ToString().Trim();
								lb_ab_desc.Text = Sql_Reader["ab_desc"].ToString().Trim().Replace("\n","<br>");
								lb_init_time.Text = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
								lb_row.Text = Sql_Reader["rownum"].ToString();

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

						// 重新計算頁數（1 頁 10 筆)
						pageid = int.Parse(lb_row.Text) / 10;

						#endregion
					}
				}
			}
			#endregion

			lb_page.Text = "?pageid=" + pageid.ToString();
			lb_page.Text += "&ab_name=" + ab_name;
			lb_page.Text += "&ab_nike=" + ab_nike;
			lb_page.Text += "&ab_company=" + ab_company;
			lb_page.Text += "&ag_name=" + ag_name;
			lb_page.Text += "&ag_attrib=" + ag_attrib;
			lb_page.Text += "&sort=" + lb_sort.Text;

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

	// 產生對應的 Sql Where 字串
	private string GetSqlString(string ab_name, string ab_nike, string ab_company, string ag_name, string ag_attrib)
	{
		Common_Func cfc = new Common_Func();
		string subSql = "", tmpstr = "";

		// 檢查 ab_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ab_name);
		if (tmpstr != "")
			subSql = subSql + " And b.ab_name Like '%" + tmpstr + "%'";

		// 檢查 ab_nike 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ab_nike);
		if (tmpstr != "")
			subSql = subSql + " And b.ab_nike Like '%" + tmpstr + "%'";

		// 檢查 ab_company 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ab_company);
		if (tmpstr != "")
			subSql = subSql + " And b.ab_company Like '%" + tmpstr + "%'";

		// 檢查 ag_name 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ag_name);
		if (tmpstr != "")
			subSql = subSql + " And b.ag_name Like '%" + tmpstr + "%'";

		// 檢查 ag_attrib 是否有值，並清除 SQL 隱碼攻擊的字元
		tmpstr = cfc.CleanSQL(ag_attrib);
		if (tmpstr != "")
			subSql = subSql + " And b.ag_attrib Like '%" + tmpstr + "%'";

		return subSql;
	}
}
