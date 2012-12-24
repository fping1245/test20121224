//---------------------------------------------------------------------------- 
//程式功能	資料庫規格管理 > 新增系統規格資料
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _G0011 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限不存入登入紀錄
			//Check_Power("G001", false);
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

	// 儲存資料
	protected void lk_save_Click(object sender, EventArgs e)
	{
		string mErr = "", SqlString = "";
		SqlDataReader Sql_Reader;

		#region 檢查輸入資料
		tb_ds_code.Text = tb_ds_code.Text.Trim();
		if (tb_ds_code.Text.Length < 2)
			mErr += "「代號」請輸入兩個字以上!\\n";

		tb_ds_name.Text = tb_ds_name.Text.Trim();
		if (tb_ds_name.Text.Length < 2)
			mErr += "「名稱」請輸入兩個字以上!\\n";

		tb_ds_database.Text = tb_ds_database.Text.Trim();
		if (tb_ds_database.Text.Length < 2)
			mErr += "「資料庫」請輸入兩個字以上!\\n";
		#endregion

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					Sql_Command.Connection = Sql_Conn;

					#region 檢查系統代號是否曾經輸入過
					SqlString = "Select Top 1 ds_code From Db_Sys Where ds_code = @ds_code";
					Sql_Conn.Open();

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("ds_code", tb_ds_code.Text);

					Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
						mErr += "系統「代號(" + tb_ds_code.Text + ")」已經存在，不允許重覆輸入!\\n";

					Sql_Reader.Close();

					Sql_Conn.Close();
					#endregion

					#region 新增資料
					if (mErr == "")
					{
						Sql_Conn.Open();

						SqlString = "Insert Into Db_Sys (ds_code, ds_name, ds_database, ds_id, ds_pass, ds_desc)";
						SqlString += " Values (@ds_code, @ds_name, @ds_database, @ds_id, @ds_pass, @ds_desc);";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("ds_code", tb_ds_code.Text);
						Sql_Command.Parameters.AddWithValue("ds_name", tb_ds_name.Text);
						Sql_Command.Parameters.AddWithValue("ds_database", tb_ds_database.Text);
						Sql_Command.Parameters.AddWithValue("ds_id", tb_ds_id.Text);
						Sql_Command.Parameters.AddWithValue("ds_pass", tb_ds_pass.Text);
						Sql_Command.Parameters.AddWithValue("ds_desc", tb_ds_desc.Text);

						Sql_Command.ExecuteNonQuery();

						Sql_Conn.Close();
					}
					#endregion
				}
			}
		}

		if (mErr == "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"存檔成功！\");parent.location.reload(true);", true);
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}
}
