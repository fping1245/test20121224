//---------------------------------------------------------------------------- 
//程式功能	考試題庫管理 > 修改試卷
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _B0012 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			string mErr = "";
			int tp_sid = -1;

			// 檢查使用者權限並存入登入紀錄
			//Check_Power("B001", false);

			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out tp_sid))
				{
					lb_tp_sid.Text = tp_sid.ToString();

					if (!GetData())
						mErr = "找不到指定的資料!\\n";
				}
				else
					mErr = "參數格式錯誤!\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";

			if (mErr != "")
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");parent.close_all();", true);
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

	// 取得資料
	private bool GetData()
	{
		bool ckbool = false;
		string SqlString = "";
		DateTime tmptime;

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 tp_title, tp_desc, is_show, b_time, e_time From Ts_Paper Where tp_sid = @tp_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();

				Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						tb_tp_title.Text = Sql_Reader["tp_title"].ToString().Trim();
						tb_tp_desc.Text = Sql_Reader["tp_desc"].ToString().Trim();
						if (Sql_Reader["is_show"].ToString() == "1")
						{
							rb_is_show0.Checked = false;
							rb_is_show1.Checked = true;
						}
						else
						{
							rb_is_show0.Checked = true;
							rb_is_show1.Checked = false;
						}

						tmptime = DateTime.Parse(Sql_Reader["b_time"].ToString());

						tb_b_date.Text = tmptime.ToString("yyyy/MM/dd");
						tb_b_hour.Text = tmptime.ToString("HH");
						tb_b_min.Text = tmptime.ToString("mm");

						tmptime = DateTime.Parse(Sql_Reader["e_time"].ToString());

						tb_e_date.Text = tmptime.ToString("yyyy/MM/dd");
						tb_e_hour.Text = tmptime.ToString("HH");
						tb_e_min.Text = tmptime.ToString("mm");

						ckbool = true;
					}
					else
						ckbool = false;

					Sql_Reader.Close();
				}

				Sql_Conn.Close();
			}
		}
		return ckbool;
	}

	// 存檔並進行下一步
	protected void lk_save_Click(object sender, EventArgs e)
	{
		string SqlString = "", mErr = "", tmpstr = "";
		int is_show = 0;
		DateTime b_time, e_time;
		
		if (rb_is_show0.Checked)
			is_show = 0;
		else
			is_show = 1;

		tb_tp_title.Text = tb_tp_title.Text.Trim();
		if (tb_tp_title.Text.Length < 3)
		{
			mErr += "請正確輸入「試卷標題」\\n";
		}

		tb_tp_desc.Text = tb_tp_desc.Text.Trim();
		if (tb_tp_desc.Text.Length < 6)
		{
			mErr += "請正確輸入「試卷說明」\\n";
		}

		tmpstr = tb_b_date.Text + " " + tb_b_hour.Text + ":" + tb_b_min.Text;
		if (!DateTime.TryParse(tmpstr, out b_time))
			mErr += "「開放進入時間」輸入格式錯誤!\\n";

		tmpstr = tb_e_date.Text + " " + tb_e_hour.Text + ":" + tb_e_min.Text;
		if (!DateTime.TryParse(tmpstr, out e_time))
			mErr += "「截止進入時間」輸入格式錯誤!\\n";

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				SqlString = "Update Ts_Paper Set tp_title = @tp_title, is_show = @is_show, b_time = @b_time";
				SqlString += ", e_time = @e_time, tp_desc = @tp_desc Where tp_sid = @tp_sid";

				using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
				{
					Sql_Conn.Open();

					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
					Sql_Command.Parameters.AddWithValue("tp_title", tb_tp_title.Text.Trim());
					Sql_Command.Parameters.AddWithValue("is_show", is_show);
					Sql_Command.Parameters.AddWithValue("b_time", b_time);
					Sql_Command.Parameters.AddWithValue("e_time", e_time);
					Sql_Command.Parameters.AddWithValue("tp_desc", tb_tp_desc.Text.Trim());

					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();
				}
			}
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"「試卷主題」修改完成!\\n請繼續處理「考試題目」!\\n\");parent.location.replace(\"B0014.aspx?sid=" + lb_tp_sid.Text + "\");", true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}
}
