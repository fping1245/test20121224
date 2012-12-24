//---------------------------------------------------------------------------- 
//程式功能	考試題庫管理 > 考試紀錄 > 修改考生資料
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _B00152 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int tp_sid = -1, tu_sid = -1;
		string mErr = "", SqlString = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限，不存入登入紀錄
			//Check_Power("B001", false);

			if (Request["sid"] != null && Request["tp_sid"] != null && Request["tp_title"] != null)
			{
				if (int.TryParse(Request["tp_sid"], out tp_sid) && int.TryParse(Request["sid"], out tu_sid))
				{
					lb_tu_sid.Text = tu_sid.ToString();
					lb_tp_sid.Text = tp_sid.ToString();
					lb_tp_title.Text = Request["tp_title"].Trim();

					// 取得資料
					using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
					{
						SqlString = "Select Top 1 tu_name, tu_no, tu_ip, is_test From Ts_User Where tp_sid = @tp_sid And tu_sid = @tu_sid";
						using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
						{
							Sql_Conn.Open();

							Sql_Command.Parameters.AddWithValue("tp_sid", tp_sid);
							Sql_Command.Parameters.AddWithValue("tu_sid", tu_sid);

							using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
							{
								if (Sql_Reader.Read())
								{
									tb_tu_name.Text = Sql_Reader["tu_name"].ToString().Trim();
									tb_tu_no.Text = Sql_Reader["tu_no"].ToString().Trim();
									tb_tu_ip.Text = Sql_Reader["tu_ip"].ToString().Trim();

									if (Sql_Reader["is_test"].ToString() == "1")
										lb_is_test.Text = "<font color=red><b>※ 已作答 ※</b></font>";
									else
										lb_is_test.Text = "(未作答)";
								}
								else
									mErr = "找不到指定的資料!\\n";
							}

							Sql_Conn.Close();
						}
					}
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

	// 存檔
	protected void lk_save_Click(object sender, EventArgs e)
	{
		string mErr = "", SqlString = "";
		String_Func sfc = new String_Func();

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

					// 修改考生試卷資料
					SqlString = "Update Ts_User Set tu_name = @tu_name, tu_no = @tu_no, tu_ip = @tu_ip";
					SqlString += " Where tp_sid = @tp_sid And tu_sid = @tu_sid;";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("tu_sid", lb_tu_sid.Text);
					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
					Sql_Command.Parameters.AddWithValue("tu_name", tb_tu_name.Text);
					Sql_Command.Parameters.AddWithValue("tu_no", tb_tu_no.Text);
					Sql_Command.Parameters.AddWithValue("tu_ip", sfc.Left(tb_tu_ip.Text, 15));

					Sql_Command.ExecuteNonQuery();

					mErr = "alert(\"修改完成!\\n\");parent.location.reload(true);";
				}
			}
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", mErr, true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}
}
