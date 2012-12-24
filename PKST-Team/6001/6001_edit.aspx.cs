//---------------------------------------------------------------------------- 
//程式功能	連絡人群組管理 > 修改資料
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _6001_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = 0, ag_sid = -1;
			string mErr = "";

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("6001", false);

			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out ckint))
					ag_sid = ckint;
				else
					mErr = "參數格式有誤!\\n";

				if (mErr == "")
				{
					#region 取得要修改的資料
					using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
					{
						string SqlString = "";

						SqlString = "Select Top 1 ag_sid, ag_name, ag_attrib, ag_desc, init_time";
						SqlString = SqlString + " From As_Group Where ag_sid = @ag_sid And mg_sid = @mg_sid";

						using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
						{
							Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
							Sql_Command.Parameters.AddWithValue("ag_sid", ag_sid);

							Sql_Conn.Open();

							SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

							if (Sql_Reader.Read())
							{
								lb_ag_sid.Text = ag_sid.ToString();
								lb_init_time.Text = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

								tb_ag_name.Text = Sql_Reader["ag_name"].ToString();
								tb_ag_attrib.Text = Sql_Reader["ag_attrib"].ToString();
								tb_ag_desc.Text = Sql_Reader["ag_desc"].ToString();
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

		// 載入字串函數
		String_Func sfc = new String_Func();

		tb_ag_name.Text = tb_ag_name.Text.Trim();
		if (tb_ag_name.Text == "")
			mErr = mErr + "「群組名稱」沒有輸入!\\n";
		else
			if (tb_ag_name.Text.Length > 50)
				mErr = mErr + "「群組名稱」最多只能輸入50個字!\\n";

		tb_ag_attrib.Text = tb_ag_attrib.Text.Trim();
		if (tb_ag_attrib.Text == "")
			mErr = mErr + "「群組屬性」沒有輸入!\\n";
		else
			if (tb_ag_attrib.Text.Length > 50)
				mErr = mErr + "「群組屬性」最多只能輸入50個字!\\n";

		tb_ag_desc.Text = sfc.Left(tb_ag_desc.Text.Trim(), 500);	

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				string SqlString = "";

				// 建立 SQL 的語法
				SqlString = "Update As_Group Set ag_name = @ag_name";
				SqlString = SqlString + ", ag_attrib = @ag_attrib";
				SqlString = SqlString + ", ag_desc = @ag_desc";
				SqlString = SqlString + " Where ag_sid = @ag_sid And mg_sid = @mg_sid";

				SqlCommand Sql_Command = new SqlCommand();

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;

				// 擷取字串到資料庫所規範的大小 cfc.Left(string mdata, int leng)
				Sql_Command.Parameters.AddWithValue("mg_sid", Session["mg_sid"].ToString());
				Sql_Command.Parameters.AddWithValue("ag_name", tb_ag_name.Text);
				Sql_Command.Parameters.AddWithValue("ag_attrib", tb_ag_attrib.Text);
				Sql_Command.Parameters.AddWithValue("ag_desc", tb_ag_desc.Text);
				Sql_Command.Parameters.AddWithValue("ag_sid", lb_ag_sid.Text);

				Sql_Conn.Open();
				Sql_Command.ExecuteNonQuery();
				Sql_Command.Dispose();
			}
		}

		if (mErr == "")
			mErr = "alert('修改完成!\\n');location.replace(\"6001.aspx" + lb_page.Text + "\");";
		else
			mErr = "alert('" + mErr + "')";

		lt_show.Text = "<script language=javascript>" + mErr + "</script>";
	}
}
