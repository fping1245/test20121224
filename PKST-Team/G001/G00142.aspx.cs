//---------------------------------------------------------------------------- 
//程式功能	資料庫規格管理 > > 資料表 > 修改資料表
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _G00142 : System.Web.UI.Page
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

				if (!GetData())
					mErr = "找不到指定的表格!\\n";
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

	// 取得資料
	private bool GetData()
	{
		bool ckbool = false;
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 dt_sort, dt_name, dt_caption, dt_area, dt_desc, dt_index, dt_modi,init_time";
			SqlString += " From Db_Table Where dt_sid = @dt_sid And ds_sid = @ds_sid;";
			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();

				Sql_Command.Parameters.AddWithValue("dt_sid", lb_dt_sid.Text);
				Sql_Command.Parameters.AddWithValue("ds_sid", lb_ds_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						tb_dt_sort.Text = (int.Parse(Sql_Reader["dt_sort"].ToString()) / 10).ToString();
						tb_dt_name.Text = Sql_Reader["dt_name"].ToString().Trim();
						tb_dt_caption.Text = Sql_Reader["dt_caption"].ToString().Trim();
						tb_dt_area.Text = Sql_Reader["dt_area"].ToString().Trim();
						tb_dt_desc.Text = Sql_Reader["dt_desc"].ToString().Trim();
						tb_dt_index.Text = Sql_Reader["dt_index"].ToString().Trim();
						tb_dt_modi.Text = Sql_Reader["dt_modi"].ToString().Trim();
						lb_init_time.Text = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss"); 

						ckbool = true;
					}

					Sql_Reader.Close();
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
		int dt_sort = -1;
		SqlDataReader Sql_Reader;

		#region 檢查資料
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
			mErr += "「中文標題」請輸入兩個字以上!\\n"; ;
		#endregion

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					Sql_Command.Connection = Sql_Conn;

					#region 檢查表格是否有相同的表格名稱
					SqlString = "Select Top 1 dt_sid From Db_Table Where ds_sid = @ds_sid And dt_name = @dt_name And dt_sid <> @dt_sid";
					Sql_Conn.Open();

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("ds_sid", lb_ds_sid.Text);
					Sql_Command.Parameters.AddWithValue("dt_name", tb_dt_name.Text);
					Sql_Command.Parameters.AddWithValue("dt_sid", lb_dt_sid.Text);

					Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
						mErr += "「表格名稱(" + tb_dt_name.Text + ")」已經存在，不允許重覆輸入!\\n";

					Sql_Reader.Close();

					Sql_Conn.Close();
					#endregion

					#region 修改資料
					if (mErr == "")
					{
						Sql_Conn.Open();

						SqlString = "Update Db_Table Set dt_sort = @dt_sort, dt_name = @dt_name, dt_caption = @dt_caption, dt_area = @dt_area";
						SqlString += ", dt_desc = @dt_desc, dt_index = @dt_index, dt_modi = @dt_modi, init_time = getdate()";
						SqlString += " Where dt_sid = @dt_sid And ds_sid = @ds_sid;";
						SqlString += "Execute dbo.p_Db_Table_ReSort @ds_sid;";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("dt_sid", lb_dt_sid.Text);
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
