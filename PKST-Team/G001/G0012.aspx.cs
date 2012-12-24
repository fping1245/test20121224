//---------------------------------------------------------------------------- 
//程式功能	資料庫規格管理 > 修改系統規格資料
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _G0012 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string mErr = "";

		if (!IsPostBack)
		{
			int ds_sid = -1;

			// 檢查使用者權限不存入登入紀錄
			//Check_Power("G001", false);

			if (Request["ds_sid"] != null)
			{
				if (int.TryParse(Request["ds_sid"], out ds_sid))
				{
					lb_ds_sid.Text = ds_sid.ToString();

					mErr = GetData();
				}
				else
					mErr = "參數格式錯誤!\\n";
			}
			else
				mErr = "參數傳遞錯誤!\\n";
		}

		if (mErr != "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");parent.close_all();", true);
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

	// 取得資料
	private string GetData()
	{
		string mErr = "", SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 ds_code, ds_name, ds_database, ds_id, ds_pass, ds_desc, init_time From Db_Sys Where ds_sid = @ds_sid;";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();

				Sql_Command.Parameters.AddWithValue("ds_sid", lb_ds_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						tb_ds_code.Text = Sql_Reader["ds_code"].ToString().Trim();
						tb_ds_name.Text = Sql_Reader["ds_name"].ToString().Trim();
						tb_ds_database.Text = Sql_Reader["ds_database"].ToString().Trim();
						tb_ds_id.Text = Sql_Reader["ds_id"].ToString().Trim();
						tb_ds_pass.Text = Sql_Reader["ds_pass"].ToString().Trim();
						tb_ds_desc.Text = Sql_Reader["ds_desc"].ToString().Trim();
						lb_init_time.Text = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
					}
					else
						mErr = "找不到指定的資料庫系統!\\n";

					Sql_Reader.Close();
				}

				Sql_Conn.Close();
			}
		}

		return mErr;
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
					SqlString = "Select Top 1 ds_code From Db_Sys Where ds_sid <> @ds_sid And ds_code = @ds_code";
					Sql_Conn.Open();

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("ds_sid", lb_ds_sid.Text);
					Sql_Command.Parameters.AddWithValue("ds_code", tb_ds_code.Text);

					Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
						mErr += "系統「代號(" + tb_ds_code.Text + ")」已經存在，不允許以相同名稱修改!\\n";

					Sql_Reader.Close();

					Sql_Conn.Close();
					#endregion

					#region 修改資料
					if (mErr == "")
					{
						Sql_Conn.Open();

						SqlString = "Update Db_Sys Set ds_code = @ds_code, ds_name = @ds_name, ds_database = @ds_database, ds_id = @ds_id";
						SqlString += ", ds_pass = @ds_pass, ds_desc = @ds_desc, init_time = getdate() Where ds_sid = @ds_sid";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("ds_sid", lb_ds_sid.Text);
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
