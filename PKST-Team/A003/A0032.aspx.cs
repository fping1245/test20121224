//---------------------------------------------------------------------------- 
//程式功能	票選資料管理 > 新增票選主題
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _A0032 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("A003", false);
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

	// 存檔並進行下一步
	protected void lk_save_Click(object sender, EventArgs e)
	{
		string SqlString = "", mErr = "";
		int is_check = 0;

		tb_bh_title.Text = tb_bh_title.Text.Trim();
		if (tb_bh_title.Text.Length < 3)
		{
			mErr += "請正確輸入「票選標題」\\n";
		}

		tb_bh_topic.Text = tb_bh_topic.Text.Trim();
		if (tb_bh_topic.Text.Length < 6)
		{
			mErr += "請正確輸入「主題內容」\\n";
		}

		if (rb_is_check0.Checked)
			is_check = 0;
		else
		{
			if (!int.TryParse(tb_is_check.Text, out is_check))
				is_check = 1;
		}

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				SqlString = "Insert Into Bt_Head (bh_title, is_check, bh_topic)";
				SqlString += " Values (@bh_title, @is_check, @bh_topic);";
				SqlString += "Select @bh_sid = Scope_Identity()";

				using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
				{
					Sql_Conn.Open();

					Sql_Command.Parameters.AddWithValue("bh_title", tb_bh_title.Text.Trim());
					Sql_Command.Parameters.AddWithValue("is_check", is_check);
					Sql_Command.Parameters.AddWithValue("bh_topic", tb_bh_topic.Text.Trim());

					SqlParameter spt_bh_sid = Sql_Command.Parameters.Add("bh_sid", SqlDbType.Int);
					spt_bh_sid.Direction = ParameterDirection.Output;

					Sql_Command.ExecuteNonQuery();

					lb_bh_sid.Text = spt_bh_sid.Value.ToString();

					Sql_Conn.Close();
				}
			}
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"「票選主題」新增完成!\\n請繼續處理「票選項目」!\\n\");parent.show_win(\"A0035.aspx?sid=" + lb_bh_sid.Text + "\", -1, -1);", true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	protected void rb_is_check0_CheckedChanged(object sender, EventArgs e)
	{
		lt_is_check.Visible = false;
		lt_is_check_desc.Visible = false;
		tb_is_check.Visible = false;
	}

	protected void rb_is_check1_CheckedChanged(object sender, EventArgs e)
	{

		lt_is_check.Visible = true;
		lt_is_check_desc.Visible = true;
		tb_is_check.Visible = true;
	}
}
