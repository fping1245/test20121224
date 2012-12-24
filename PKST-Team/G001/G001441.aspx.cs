//---------------------------------------------------------------------------- 
//程式功能	資料庫規格管理 > 資料表清單 > 欄位清單 > 新增欄位
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _G001441 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string mErr = "";
		int ds_sid = -1, dt_sid = -1;

		if (!IsPostBack)
		{
			// 檢查使用者權限不存入登入紀錄
			//Check_Power("G001", false);

			if (Request["ds_sid"] == null || Request["dt_sid"] == null)
				mErr = "參數傳送錯誤!\\n";
			else if (int.TryParse(Request["ds_sid"], out ds_sid) && int.TryParse(Request["dt_sid"], out dt_sid))
			{
				lb_ds_sid.Text = ds_sid.ToString();
				lb_dt_sid.Text = dt_sid.ToString();
			}
			else
				mErr = "參數格式錯誤!\\n";
		}

		if (mErr != "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");history.go(-1);", true);
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

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					Sql_Command.Connection = Sql_Conn;

					#region 新增資料
					if (mErr == "")
					{
						Sql_Conn.Open();

						SqlString = "Insert Into Db_Record (ds_sid, dt_sid, dr_sort, dr_name, dr_caption, dr_type, dr_len, dr_point, dr_default, dr_desc)";
						SqlString += " Values (@ds_sid, @dt_sid, @dr_sort, @dr_name, @dr_caption, @dr_type, @dr_len, @dr_point, @dr_default, @dr_desc);";
						SqlString += "Execute dbo.p_Db_Record_ReSort @ds_sid, @dt_sid;";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("ds_sid", lb_ds_sid.Text);
						Sql_Command.Parameters.AddWithValue("dt_sid", lb_dt_sid.Text);
						Sql_Command.Parameters.AddWithValue("dr_sort", 32767);
						Sql_Command.Parameters.AddWithValue("dr_name", tb_dr_name.Text);
						Sql_Command.Parameters.AddWithValue("dr_caption", tb_dr_caption.Text);
						Sql_Command.Parameters.AddWithValue("dr_type", tb_dr_type.Text);
						Sql_Command.Parameters.AddWithValue("dr_len", tb_dr_len.Text);
						Sql_Command.Parameters.AddWithValue("dr_point", tb_dr_point.Text);
						Sql_Command.Parameters.AddWithValue("dr_default", tb_dr_default.Text);
						Sql_Command.Parameters.AddWithValue("dr_desc", tb_dr_desc.Text);

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
