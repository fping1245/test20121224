//---------------------------------------------------------------------------- 
//程式功能	工作類型管理 > 修改資料
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _5001_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0, cg_sid = -1;
			string mErr = "";

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("5001", false);

			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out ckint))
					cg_sid = ckint;
				else
					mErr = "參數格式有誤!\\n";

				if (mErr == "")
				{
					#region 取得要修改的資料
					using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
					{
						string SqlString = "";

						SqlString = "Select Top 1 cg_sid, cg_name, cg_sort, cg_desc";
						SqlString = SqlString + " From Ca_Group Where cg_sid = @cg_sid And mg_sid = @mg_sid";

						using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
						{
							Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
							Sql_Command.Parameters.AddWithValue("cg_sid", cg_sid);

							Sql_Conn.Open();

							SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

							if (Sql_Reader.Read())
							{
								lb_cg_sid.Text = cg_sid.ToString();
								tb_cg_name.Text = Sql_Reader["cg_name"].ToString();
								tb_cg_sort.Text = Sql_Reader["cg_sort"].ToString();
								tb_cg_desc.Text = Sql_Reader["cg_desc"].ToString();
							}
							else
								mErr = "找不到要修改的資料!\\n";

							Sql_Reader.Close();
							Sql_Reader.Dispose();
						}
					}
					#endregion
				}
			}
			else
				mErr = "參數傳送錯誤!\\n";

			#region 承接上一頁的查詢條件設定
			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"].ToString(), out ckint))
					lb_page.Text = "?pageid=" + ckint.ToString();
				else
					lb_page.Text = "?pageid=0";
			}
			#endregion

			if (mErr != "")
				lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");";
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
			if (cfc.Check_Power(Session["mg_sid"].ToString(), Session["mg_name"].ToString(), Session["mg_power"].ToString(), f_power, Request.ServerVariables["REMOTE_editR"], bl_save) > 0)
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
		int cg_sort = -1;

		// 載入字串函數
		String_Func sfc = new String_Func();

		tb_cg_name.Text = tb_cg_name.Text.Trim();
		if (tb_cg_name.Text == "")
			mErr = mErr + "「群組名稱」沒有輸入!\\n";
		else
			if (tb_cg_name.Text.Length > 10)
				mErr = mErr + "「群組名稱」最多只能輸入10個字!\\n";

		tb_cg_sort.Text = tb_cg_sort.Text.Trim();
		if (tb_cg_sort.Text == "")
			mErr += "「顯示順序」沒有輸入!\\n";
		else
			if (int.TryParse(tb_cg_sort.Text, out cg_sort))
			{
				if (cg_sort < 0 || cg_sort > 32767)
					mErr += "「顯示順序」請輸入介於 0 ~ 32767 的數字!\\n";
			}
			else
				mErr += "「顯示順序」請輸入 0 ~ 32767 的數字!\\n";

		tb_cg_desc.Text = sfc.Left(tb_cg_desc.Text.Trim(), 500);

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				string SqlString = "";

				// 建立 SQL 的語法
				SqlString = "Update Ca_Group Set cg_name = @cg_name";
				SqlString = SqlString + ", cg_sort = @cg_sort";
				SqlString = SqlString + ", cg_desc = @cg_desc";
				SqlString = SqlString + " Where cg_sid = @cg_sid And mg_sid = @mg_sid";

				SqlCommand Sql_Command = new SqlCommand();

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;

				// 擷取字串到資料庫所規範的大小 cfc.Left(string mdata, int leng)
				Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
				Sql_Command.Parameters.AddWithValue("cg_name", tb_cg_name.Text);
				Sql_Command.Parameters.AddWithValue("cg_sort", tb_cg_sort.Text);
				Sql_Command.Parameters.AddWithValue("cg_desc", tb_cg_desc.Text);
				Sql_Command.Parameters.AddWithValue("cg_sid", lb_cg_sid.Text);

				Sql_Conn.Open();
				Sql_Command.ExecuteNonQuery();
				Sql_Command.Dispose();
				Sql_Conn.Close();
			}

            // 呼叫 Sql Server 的預存程序來重新設定 cg_sort 的順序
			ReSort();
		}

		if (mErr == "")
			mErr = "alert('修改完成!\\n');location.replace(\"5001.aspx" + lb_page.Text + "\");";
		else
			mErr = "alert('" + mErr + "')";

		lt_show.Text = "<script language=javascript>" + mErr + "</script>";
	}

    // 呼叫 Sql Server 的預存程序來重新設定 cg_sort 的順序
	private void ReSort()
	{
		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			string SqlString = "";
			SqlString = "Execute dbo.p_Ca_Group_ReSort @mg_sid;";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());

				Sql_Conn.Open();
				Sql_Command.ExecuteNonQuery();
				Sql_Conn.Close();
			}
		}
	}
}
