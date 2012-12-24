//---------------------------------------------------------------------------- 
//程式功能	資料庫規格管理 > > 資料表清單 > 新增資料表
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _G00141 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string mErr = "";
		int ds_sid = -1;

		if (!IsPostBack)
		{
			// 檢查使用者權限不存入登入紀錄
			//Check_Power("G001", false);

			if (Request["ds_sid"] == null)
				mErr = "參數傳送錯誤!\\n";
			else if (int.TryParse(Request["ds_sid"], out ds_sid))
			{
				lb_ds_sid.Text = ds_sid.ToString();
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
		int dt_sort = -1;
		SqlDataReader Sql_Reader;

		#region 檢查資料格式
		tb_dt_name.Text = tb_dt_name.Text.Trim();
		if (tb_dt_name.Text.Length < 2)
			mErr += "「表格名稱」請輸入兩個字以上!\\n";

		tb_dt_sort.Text = tb_dt_sort.Text.Trim();
		if (int.TryParse(tb_dt_sort.Text, out dt_sort))
		{
			if (dt_sort < 0 || dt_sort > 3275)
				mErr += "「顯示順序」請輸入 0 ~ 3275 的數字!\\n"; ;
		}
		else
			mErr += "「顯示順序」請輸入 0 ~ 3275 的數字!\\n"; ;

		tb_dt_caption.Text = tb_dt_caption.Text.Trim();
		if (tb_dt_caption.Text.Length < 2)
			mErr += "「中文標題」請輸入兩個字以上!\\n";
		#endregion

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					Sql_Command.Connection = Sql_Conn;

					#region 檢查表格是否曾經輸入過
					SqlString = "Select Top 1 dt_sid From Db_Table Where ds_sid = @ds_sid And dt_name = @dt_name";
					Sql_Conn.Open();

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("ds_sid", lb_ds_sid.Text);
					Sql_Command.Parameters.AddWithValue("dt_name", tb_dt_name.Text);

					Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
						mErr += "「表格名稱(" + tb_dt_name.Text + ")」已經存在，不允許重覆輸入!\\n";

					Sql_Reader.Close();

					Sql_Conn.Close();
					#endregion

					#region 新增資料
					if (mErr == "")
					{
						Sql_Conn.Open();

						SqlString = "Insert Into Db_Table (ds_sid, dt_sort, dt_name, dt_caption, dt_area, dt_desc, dt_index, dt_modi)";
						SqlString += " Values (@ds_sid, @dt_sort, @dt_name, @dt_caption, @dt_area, @dt_desc, @dt_index, @dt_modi);";
						SqlString += "Execute dbo.p_Db_Table_ReSort @ds_sid;";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("ds_sid", lb_ds_sid.Text);
						Sql_Command.Parameters.AddWithValue("dt_sort", dt_sort * 10);
						Sql_Command.Parameters.AddWithValue("dt_name", tb_dt_name.Text);
						Sql_Command.Parameters.AddWithValue("dt_caption", tb_dt_caption.Text);
						Sql_Command.Parameters.AddWithValue("dt_area", tb_dt_area.Text);
						Sql_Command.Parameters.AddWithValue("dt_desc", tb_dt_desc.Text);
						Sql_Command.Parameters.AddWithValue("dt_index", tb_dt_index.Text);
						Sql_Command.Parameters.AddWithValue("dt_modi", tb_dt_modi.Text);

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
