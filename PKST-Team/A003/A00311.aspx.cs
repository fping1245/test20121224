//---------------------------------------------------------------------------- 
//程式功能	票選資料管理 > 票選排程處理 > 修改問卷排程
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _A00311 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int bs_sid = -1;
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("A003", false);

			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out bs_sid))
				{
					lb_bs_sid.Text = bs_sid.ToString();

					// 取得資料
					if (!GetData())
						mErr = "找不到相關資料!\\n";
				}
				else
					mErr = "參數傳入錯誤!\\n";
			}
			else
				mErr = "參數傳入錯誤!\\n";

			if (mErr != "")
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");history.go(-1)", true);
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
		if (!int.TryParse(tb_bs_sort.Text, out bs_sort))
		{
			mErr += "「顯示順序」請輸入數字!\\n";
		}

		tb_s_time.Text = tb_s_time.Text.Trim();
		if (!DateTime.TryParse(tb_s_time.Text, out s_time))
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

					// 修改排程
					SqlString = "Update Bt_Schedule Set bs_sort = @bs_sort, s_time = @s_time, e_time = @e_time";
					SqlString += ", is_show = @is_show, init_time = getdate() Where bs_sid = @bs_sid;";

					// 重新排序，並更新系統變數 A01
					SqlString += "Execute dbo.p_Bt_Schedule_ReSort;";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("bs_sid", lb_bs_sid.Text);
					Sql_Command.Parameters.AddWithValue("bs_sort", bs_sort);
					Sql_Command.Parameters.AddWithValue("s_time", s_time);
					Sql_Command.Parameters.AddWithValue("e_time", e_time);
					Sql_Command.Parameters.AddWithValue("is_show", is_show);

					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();
				}
			}
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"「票選排程」修改完成!\\n\");parent.location.reload(true);", true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	// 取得資料
	private bool GetData()
	{
		bool ckbool = false;
		string SqlString = "", is_show = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 s.bs_sort,s.s_time, s.e_time, s.is_show, s.init_time, h.bh_title From Bt_Schedule s";
			SqlString += " Left Outer Join Bt_Head h On s.bh_sid = h.bh_sid";
			SqlString += " Where s.bs_sid = @bs_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("bs_sid", lb_bs_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						lb_bh_title.Text = Sql_Reader["bh_title"].ToString().Trim();
						tb_bs_sort.Text = Sql_Reader["bs_sort"].ToString();
						tb_s_time.Text = DateTime.Parse(Sql_Reader["s_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
						tb_e_time.Text = DateTime.Parse(Sql_Reader["e_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
						is_show = Sql_Reader["is_show"].ToString();

						if (is_show == "0")
						{
							rb_is_show0.Checked = true;
							rb_is_show1.Checked = false;
						}
						else
						{
							rb_is_show0.Checked = false;
							rb_is_show1.Checked = true;
						}

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
