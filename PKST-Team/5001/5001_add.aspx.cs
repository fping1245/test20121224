//---------------------------------------------------------------------------- 
//程式功能	工作類型管理 > 新增資料
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _5001_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0;

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("5001", false);

			#region 承接上一頁的查詢條件設定
			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"].ToString(), out ckint))
					lb_page.Text = "?pageid=" + ckint.ToString();
				else
					lb_page.Text = "?pageid=0";
			}
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
		int cg_sort = -1;

		// 載入字串函數
		String_Func sfc = new String_Func();

		tb_cg_name.Text = tb_cg_name.Text.Trim();
		if (tb_cg_name.Text == "")
			mErr += "「類型名稱」沒有輸入!\\n";
		else
			if (tb_cg_name.Text.Length > 10)
				mErr += "「類型名稱」最多只能輸入10個字!\\n";

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
				SqlString = "Insert Into Ca_Group (mg_sid, cg_name, cg_sort, cg_desc)";
				SqlString = SqlString + " Values (@mg_sid, @cg_name, @cg_sort, @cg_desc);";

				using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
				{
					// 擷取字串到資料庫所規範的大小 cfc.Left(string mdata, int leng)
					Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
					Sql_Command.Parameters.AddWithValue("cg_name", tb_cg_name.Text);
					Sql_Command.Parameters.AddWithValue("cg_sort", tb_cg_sort.Text);
					Sql_Command.Parameters.AddWithValue("cg_desc", tb_cg_desc.Text);

					Sql_Conn.Open();
					Sql_Command.ExecuteNonQuery();
					Sql_Conn.Close();
				}
			}

            // 呼叫 Sql Server 的預存程序來重新設定 cg_sort 的順序
			ReSort();
		}

		if (mErr == "")
			mErr = "alert('存檔完成!\\n');location.replace(\"5001.aspx" + lb_page.Text + "\");";
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
