//---------------------------------------------------------------------------- 
//程式功能	票選資料管理 > 票選問卷加入排程
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _A0036 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int bh_sid = -1;
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("A003", false);

			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out bh_sid))
				{
					lb_bh_sid.Text = bh_sid.ToString();

					// 取得資料
					if (GetData())
					{
						// 檢查排程是否已有此主題資料
						if (CheckData())
							mErr = "排程中已有此主題資料，請使用排程設定來進行修改或刪除!\\n";
					}
					else
						mErr = "資料取得有誤!\\n";
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

	// 存檔
	protected void lk_save_Click(object sender, EventArgs e)
	{
		string SqlString = "", mErr = "";
		int bs_sort = 0, is_show = 1;
		DateTime s_time, e_time;

		tb_bs_sort.Text = tb_bs_sort.Text.Trim();
		if (! int.TryParse(tb_bs_sort.Text, out bs_sort))
		{
			mErr += "「顯示順序」請輸入數字!\\n";
		}

		tb_s_time.Text = tb_s_time.Text.Trim();
		if (! DateTime.TryParse(tb_s_time.Text,out s_time))
		{
			mErr += "請正確輸入「開始時間」(yyyy/MM/dd HH:mm:ss)!\\n";
		}

		tb_e_time.Text = tb_e_time.Text.Trim();
		if (!DateTime.TryParse(tb_e_time.Text, out e_time))
		{
			mErr += "請正確輸入「結束時間」(yyyy/MM/dd HH:mm:ss)!\\n";
		}

		if (rb_is_show0.Checked)
			is_show = 0;
		else
			is_show = 1;

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					Sql_Conn.Open();
					Sql_Command.Connection = Sql_Conn;

					// 加入排程
					SqlString = "Insert Into Bt_Schedule (bh_sid, bs_sort, s_time, e_time, is_show)";
					SqlString += " Values (@bh_sid, @bs_sort, @s_time, @e_time, @is_show);";

					// 重新排序，並更新系統變數 A01
					SqlString += "Execute dbo.p_Bt_Schedule_ReSort;";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("bh_sid", lb_bh_sid.Text);
					Sql_Command.Parameters.AddWithValue("bs_sort", bs_sort);
					Sql_Command.Parameters.AddWithValue("s_time", s_time);
					Sql_Command.Parameters.AddWithValue("e_time", e_time);
					Sql_Command.Parameters.AddWithValue("is_show", is_show);

					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();
				}
			}
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"「票選主題排程」設定完成!\\n如要修改，請使用「排程設定」功能。\");parent.location.reload(true);", true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	// 檢查排程是否已有此主題資料
	private bool CheckData()
	{
		bool ckbool = false;
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 bs_sid From Bt_Schedule Where bh_sid = @bh_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();

				Sql_Command.Parameters.AddWithValue("bh_sid", lb_bh_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						tb_s_time.Text = "";
						tb_e_time.Text = "";
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

	// 取得資料
	private bool GetData()
	{
		bool ckbool = false;
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 bh_title From Bt_Head Where bh_sid = @bh_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();

				Sql_Command.Parameters.AddWithValue("bh_sid", lb_bh_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						lb_bh_title.Text = Sql_Reader["bh_title"].ToString().Trim();

						tb_s_time.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
						tb_e_time.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

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
}
