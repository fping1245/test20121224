//---------------------------------------------------------------------------- 
//程式功能	通訊錄管理 > 明細內容 > 新增資料
//---------------------------------------------------------------------------- 

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _60021_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0;

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("6002", false);

			#region 承接上一頁的查詢條件設定
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

			#endregion
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

	// 確定存檔
	protected void lb_ok_Click(object sender, EventArgs e)
	{
		string mErr = "";
		int ab_sid = -1;

		// 載入字串函數
		String_Func sfc = new String_Func();

		if (tb_ab_name.Text.Trim() == "")
			mErr += "「姓名」沒有輸入!\\n";

		if (tb_ab_nike.Text.Trim() == "")
			mErr += "「暱稱」沒有輸入!\\n";

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				string SqlString = "";
				Decoder decoder = new Decoder();

				// 建立 SQL 的語法
				SqlString = "Insert Into As_Book (mg_sid, ag_sid, ab_name, ab_nike, ab_zipcode, ab_address, ab_tel_h";
				SqlString += ", ab_tel_o, ab_mobil, ab_fax, ab_email, ab_posit, ab_company, ab_desc)";
				SqlString += " Values (@mg_sid, @ag_sid, @ab_name, @ab_nike, @ab_zipcode, @ab_address, @ab_tel_h";
				SqlString += ", @ab_tel_o, @ab_mobil, @ab_fax, @ab_email, @ab_posit, @ab_company, @ab_desc);";
				SqlString += "Select @ab_sid = Scope_Identity()";

				SqlCommand Sql_Command = new SqlCommand();

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;

				// 擷取字串到資料庫所規範的大小 sfc.Left(string mdata, int leng)
				Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
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

				SqlParameter spt_ab_sid = Sql_Command.Parameters.Add("ab_sid", SqlDbType.Int);
				spt_ab_sid.Direction = ParameterDirection.Output;

				Sql_Conn.Open();
				Sql_Command.ExecuteNonQuery();

				// 取得新增資料的主鍵值
				ab_sid = (int)spt_ab_sid.Value;

				Sql_Command.Dispose();
			}
		}

		if (mErr == "")
		{
			mErr = "alert(\"存檔完成!\\n\");location.replace(\"60021.aspx" + lb_page.Text + "&sid=" + ab_sid.ToString() + "\");";
		}
		else
			mErr = "alert('" + mErr + "')";

		lt_show.Text = "<script language=javascript>" + mErr + "</script>";
	}
}
