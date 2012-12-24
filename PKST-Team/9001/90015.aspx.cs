//---------------------------------------------------------------------------- 
//程式功能	廣告信發送管理 (廣告信清單) > SMTP 伺服器參數設定
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _90015 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限不存入登入紀錄
			//Check_Power("9001", false);

			//  由 Sys_Param取得 SMTP 伺服器相關資料
			Get_Data();
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

	//  由 Sys_Param取得 SMTP 伺服器相關資料
	private void Get_Data()
	{
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select sp_no, sp_num, sp_str From Sys_Param Where sp_gp = '9' And (sp_no between '901' And '904');";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					while (Sql_Reader.Read())
					{
						switch (Sql_Reader["sp_no"].ToString())
						{
							case "901":		// smtp 伺服器名稱
								tb_host.Text = Sql_Reader["sp_str"].ToString().Trim();
								break;
							case "902":		// smtp Port
								tb_port.Text = Sql_Reader["sp_num"].ToString();
								break;
							case "903":		// 登入帳號
								tb_id.Text = Sql_Reader["sp_str"].ToString().Trim();
								break;
							case "904":		// 登入密碼
								tb_pw.Text = Sql_Reader["sp_str"].ToString().Trim();
								break;
						}
					}
					Sql_Reader.Close();
				}

				Sql_Conn.Close();
			}
		}
	}

	// 存檔
	protected void lk_save_Click(object sender, EventArgs e)
	{
		int ckint = 0;
		string SqlString = "", mErr = "";
		Check_Internet cknet = new Check_Internet();

		if (cknet.Check_Host(tb_host.Text.Trim()) != 0)
			mErr += "主機名稱錯誤!\\n";

		if (int.TryParse(tb_port.Text, out ckint))
		{
			if (ckint < 1 || ckint > 65534)
				mErr += "通訊 Port 請輸入 1 ~ 65534 之間的數字!\\n";
		}
		else
			mErr += "通訊 Port 請輸入數字!\\n";

		if (tb_id.Text.Trim() == "")
			mErr += "豋入帳號不可空白!\\n";

		if (tb_pw.Text.Trim() == "")
			mErr += "豋入密碼不可空白!\\n";

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				SqlString = "Update Sys_Param Set sp_str = @host Where sp_no = '901';";
				SqlString += "Update Sys_Param Set sp_num = @port Where sp_no = '902';";
				SqlString += "Update Sys_Param Set sp_str = @id Where sp_no = '903';";
				SqlString += "Update Sys_Param Set sp_str = @pw Where sp_no = '904';";

				using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
				{
					Sql_Conn.Open();

					Sql_Command.Parameters.AddWithValue("host", tb_host.Text.Trim());
					Sql_Command.Parameters.AddWithValue("port", tb_port.Text.Trim());
					Sql_Command.Parameters.AddWithValue("id", tb_id.Text.Trim());
					Sql_Command.Parameters.AddWithValue("pw", tb_pw.Text.Trim());

					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();
				}
			}
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"儲存完成!\\n\");parent.close_all();", true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}
}
