//---------------------------------------------------------------------------- 
//程式功能	資料庫規格管理 > 資料表清單 > 欄位清單 > 移動欄位順序
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _G001444 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string mErr = "";
		int ds_sid = -1, dt_sid = -1, dr_sid = -1;

		if (!IsPostBack)
		{
			// 檢查使用者權限不存入登入紀錄
			//Check_Power("G001", false);

			if (Request["ds_sid"] == null || Request["dt_sid"] == null || Request["dr_sid"] == null)
				mErr = "參數傳送錯誤!\\n";
			else if (int.TryParse(Request["ds_sid"], out ds_sid) && int.TryParse(Request["dt_sid"], out dt_sid)
				 && int.TryParse(Request["dr_sid"], out dr_sid))
			{
				lb_ds_sid.Text = ds_sid.ToString();
				lb_dt_sid.Text = dt_sid.ToString();
				lb_dr_sid.Text = dr_sid.ToString();

				if (!GetData())
					mErr = "找不到指定的資料!\\n";
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

	// 請取資料
	private bool GetData()
	{
		bool ckbool = false;
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 dr_sort From Db_Record Where dr_sid = @dr_sid And ds_sid = @ds_sid And dt_sid = @dt_sid;";
			
			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("dr_sid", lb_dr_sid.Text);
				Sql_Command.Parameters.AddWithValue("ds_sid", lb_ds_sid.Text);
				Sql_Command.Parameters.AddWithValue("dt_sid", lb_dt_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						tb_s_sort.Text = (int.Parse(Sql_Reader["dr_sort"].ToString()) / 10).ToString();
						tb_t_sort.Text = (int.Parse(Sql_Reader["dr_sort"].ToString()) / 10).ToString();

						ckbool = true;
					}
				}

				Sql_Conn.Close();
			}
		}

		return ckbool;
	}

	// 儲存資料
	protected void lk_save_Click(object sender, EventArgs e)
	{
		string mErr = "", SqlString = "";
		int t_sort = 0, s_sort = 0;

		if (int.TryParse(tb_s_sort.Text, out s_sort))
		{
			if (s_sort < 1 || s_sort > 3275)
				mErr = "「原來順序」請輸入 1 ~ 3275 之間的數字\\n";
			else
				s_sort *= 10;
		}
		else
			mErr = "「原來順序」請輸入 1 ~ 3275 之間的數字\\n";

		if (int.TryParse(tb_t_sort.Text, out t_sort))
		{
			if (t_sort < 1 || t_sort > 3275)
				mErr = "「插入位置」請輸入 1 ~ 3275 之間的數字\\n";
			else
				t_sort = t_sort * 10 - 5;
		}
		else
			mErr = "「插入位置」請輸入 1 ~ 3275 之間的數字\\n";


		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					Sql_Command.Connection = Sql_Conn;

					#region 修改資料
					if (mErr == "")
					{
						Sql_Conn.Open();

						SqlString = "Update Db_Record Set dr_sort = @t_sort, init_time = getdate()";
						SqlString += " Where ds_sid = @ds_sid And dt_sid = @dt_sid And dr_sort = @s_sort;";
						SqlString += "Execute dbo.p_Db_Record_ReSort @ds_sid, @dt_sid;";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("ds_sid", lb_ds_sid.Text);
						Sql_Command.Parameters.AddWithValue("dt_sid", lb_dt_sid.Text);
						Sql_Command.Parameters.AddWithValue("t_sort", t_sort);
						Sql_Command.Parameters.AddWithValue("s_sort", s_sort);

						Sql_Command.ExecuteNonQuery();

						Sql_Conn.Close();
					}
					#endregion
				}
			}
		}

		if (mErr == "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "parent.location.reload(true);", true);
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}
}
