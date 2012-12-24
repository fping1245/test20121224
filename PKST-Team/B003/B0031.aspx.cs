//---------------------------------------------------------------------------- 
//程式功能	線上考試(限定身份) (試卷清單) > 填寫考生資料
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _B0031 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int tp_sid = -1;
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限，不存入登入紀錄
			//Check_Power("B003", false);

			if (Request["sid"] != null && Request["tp_title"] != null)
			{
				if (int.TryParse(Request["sid"], out tp_sid))
				{
					lb_tp_sid.Text = tp_sid.ToString();
					lb_tp_title.Text = Request["tp_title"].Trim();
				}
				else
					mErr = "參數格式錯誤!\\n";
			}
			else
				mErr = "參數傳遞錯誤!\\n";

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

	// 確認身份並開始作答
	protected void lk_confirm_Click(object sender, EventArgs e)
	{
		string mErr = "", SqlString = "";
		string tu_ip = "", tu_sid = "", is_test = "";

		// 取得考生 IP
		tu_ip = Request.ServerVariables["REMOTE_ADDR"];

		tb_tu_name.Text = tb_tu_name.Text.Trim();
		if (tb_tu_name.Text.Length < 2 || tb_tu_name.Text.Length > 20)
			mErr += "「姓名」請填入2～20個字!\\n";

		tb_tu_no.Text = tb_tu_no.Text.Trim();
		if (tb_tu_no.Text.Length < 4 || tb_tu_no.Text.Length > 10)
			mErr += "「學號」請填入4～10個字!\\n";

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					Sql_Conn.Open();
					Sql_Command.Connection = Sql_Conn;

					#region 取得考生試卷資料
					SqlString = "Select Top 1 tu_sid, is_test From Ts_User";
					SqlString += " Where tp_sid = @tp_sid And tu_name = @tu_name And tu_no = @tu_no";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
					Sql_Command.Parameters.AddWithValue("tu_name", tb_tu_name.Text);
					Sql_Command.Parameters.AddWithValue("tu_no", tb_tu_no.Text);

					using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
					{
						if (Sql_Reader.Read())
						{
							tu_sid = Sql_Reader["tu_sid"].ToString();
							is_test = Sql_Reader["is_test"].ToString();

							// 若限定使用者在考試期限內，只能登入一次，則把下列判斷註解取消
							//if (is_test == "1")
							//    mErr = "您已經考過試了，不允許重覆再參加這次考試!\\n";							
						}
						else
							mErr = "您輸入的「姓名」及「學號」不在此次考試的名單中，不允許參加考試!\\n";

						Sql_Reader.Close();
					}
					#endregion

					#region 更新考生試卷資料時間，並註記已經作答
					if (mErr == "")
					{
						if (is_test == "0")
						{
							// 考生第一次作答
							SqlString = "Update Ts_User Set b_time = getdate(), e_time = getdate(), tu_ip = @tu_ip, is_test = 1";
						}
						else
						{
							// 考生重覆登入
							SqlString = "Update Ts_User Set e_time = getdate(), tu_ip = @tu_ip, is_test = 1";
						}
						SqlString += " Where tp_sid = @tp_sid And tu_sid = @tu_sid;";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
						Sql_Command.Parameters.AddWithValue("tu_sid", tu_sid);
						Sql_Command.Parameters.AddWithValue("tu_ip", tu_ip);

						Sql_Command.ExecuteNonQuery();

						mErr = "parent.location.replace(\"B00311.aspx?tu_sid=" + tu_sid + "&tp_sid=" + lb_tp_sid.Text + "\")";
						ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", mErr, true);
					}
					#endregion

					Sql_Conn.Close();
				}
			}			
		}
		
		if (mErr != "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}
}
