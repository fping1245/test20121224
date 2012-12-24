//---------------------------------------------------------------------------- 
//程式功能	連絡人群組管理 > 新增資料
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _6001_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0;

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("6001", false);

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

		// 載入字串函數
		String_Func sfc = new String_Func();

		tb_ag_name.Text = tb_ag_name.Text.Trim();
		if (tb_ag_name.Text == "")
			mErr += "「群組名稱」沒有輸入!\\n";
		else
			if (tb_ag_name.Text.Length > 50)
				mErr += "「群組名稱」最多只能輸入50個字!\\n";

		tb_ag_attrib.Text = tb_ag_attrib.Text.Trim();
		if (tb_ag_attrib.Text == "")
			mErr += "「群組屬性」沒有輸入!\\n";
		else
			if (tb_ag_attrib.Text.Length > 50)
				mErr += "「群組屬性」最多只能輸入50個字!\\n";

		tb_ag_desc.Text = sfc.Left(tb_ag_desc.Text.Trim(), 500);	

		if (mErr == "")
		{
			using (SqlConnection Sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				string SqlString = "";

				// 建立 SQL 的語法
				SqlString = "Insert Into As_Group (mg_sid, ag_name, ag_attrib, ag_desc)";
				SqlString = SqlString + " Values (@mg_sid, @ag_name, @ag_attrib, @ag_desc);";
 
				SqlCommand Sql_Command = new SqlCommand();

				Sql_Command.Connection = Sql_conn;
				Sql_Command.CommandText = SqlString;

				// 擷取字串到資料庫所規範的大小 cfc.Left(string mdata, int leng)
				Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
				Sql_Command.Parameters.AddWithValue("ag_name", tb_ag_name.Text);
				Sql_Command.Parameters.AddWithValue("ag_attrib", tb_ag_attrib.Text);
				Sql_Command.Parameters.AddWithValue("ag_desc", tb_ag_desc.Text);

				Sql_conn.Open();
				Sql_Command.ExecuteNonQuery();
				Sql_Command.Dispose();
			}
		}

		if (mErr == "")
			mErr = "alert('存檔完成!\\n');location.replace(\"6001.aspx" + lb_page.Text + "\");";
		else
			mErr = "alert('" + mErr + "')";

		lt_show.Text = "<script language=javascript>" + mErr + "</script>";
	}
}
