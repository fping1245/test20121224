//---------------------------------------------------------------------------- 
//程式功能	POP3收信處理 > POP3 主機設定
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _90021 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		int ppa_sid = -1;
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限不存入登入紀錄
			//Check_Power("9002", false);

			if (Request["sid"] == null)
				mErr = "參數傳錯誤!\\n";
			else if (int.TryParse(Request["sid"], out ppa_sid))
			{
				lb_ppa_sid.Text = ppa_sid.ToString();
			}
			else
				mErr = "參數傳錯誤!\\n";

			if (mErr == "")
			{
				//  由 POP3_Account 取得 SMTP 伺服器相關資料
				Get_Data();
			}
			else
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");parent.close_all()", true);
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

	//  由 POP3_Account 取得 SMTP 伺服器相關資料
	private void Get_Data()
	{
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 ppa_host, ppa_port, ppa_id, ppa_pw, init_time From POP3_Account Where ppa_sid = @ppa_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();

				Sql_Command.Parameters.AddWithValue("ppa_sid",lb_ppa_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						tb_ppa_host.Text = Sql_Reader["ppa_host"].ToString().Trim();
						tb_ppa_port.Text = Sql_Reader["ppa_port"].ToString();
						tb_ppa_id.Text = Sql_Reader["ppa_id"].ToString().Trim();
						tb_ppa_pw.Text = Sql_Reader["ppa_pw"].ToString().Trim();
						lb_init_time.Text = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
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

		if (cknet.Check_Host(tb_ppa_host.Text.Trim()) != 0)
			mErr += "主機名稱錯誤!\\n";

		if (int.TryParse(tb_ppa_port.Text, out ckint))
		{
			if (ckint < 1 || ckint > 65534)
				mErr += "通訊埠請輸入 1 ~ 65534 之間的數字!\\n";
		}
		else
			mErr += "通訊埠請輸入數字!\\n";

		if (tb_ppa_id.Text.Trim() == "")
			mErr += "豋入帳號不可空白!\\n";

		if (tb_ppa_pw.Text.Trim() == "")
			mErr += "豋入密碼不可空白!\\n";

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				SqlString = "Update POP3_Account Set ppa_host = @ppa_host, ppa_port = @ppa_port, ppa_id = @ppa_id";
				SqlString += ", ppa_pw = @ppa_pw, init_time = getdate() Where ppa_sid = @ppa_sid";

				using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
				{
					Sql_Conn.Open();

					Sql_Command.Parameters.AddWithValue("ppa_host", tb_ppa_host.Text.Trim());
					Sql_Command.Parameters.AddWithValue("ppa_port", tb_ppa_port.Text.Trim());
					Sql_Command.Parameters.AddWithValue("ppa_id", tb_ppa_id.Text.Trim());
					Sql_Command.Parameters.AddWithValue("ppa_pw", tb_ppa_pw.Text.Trim());
					Sql_Command.Parameters.AddWithValue("ppa_sid", lb_ppa_sid.Text);

					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();
				}
			}
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"儲存完成!\\n\");parent.location.replace('9002.aspx');", true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}
}
