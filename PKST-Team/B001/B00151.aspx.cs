//---------------------------------------------------------------------------- 
//程式功能	考試題庫管理 > 考試紀錄 > 新增考生資料
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _B00151 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int tp_sid = -1;
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限，不存入登入紀錄
			//Check_Power("B001", false);

			if (Request["tp_sid"] != null && Request["tp_title"] != null)
			{
				if (int.TryParse(Request["tp_sid"], out tp_sid))
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

	// 存檔
	protected void lk_save_Click(object sender, EventArgs e)
	{
		string mErr = "", SqlString = "";
		string tu_sid = "";

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

					// 新增考生試卷資料
					SqlString = "Insert Into Ts_User (tp_sid, tu_name, tu_no, tu_ip, is_test) Values (@tp_sid, @tu_name, @tu_no, '', 0);";
					SqlString += "Select @tu_sid = Scope_Identity()";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
					Sql_Command.Parameters.AddWithValue("tu_name", tb_tu_name.Text);
					Sql_Command.Parameters.AddWithValue("tu_no", tb_tu_no.Text);

					SqlParameter spt_tu_sid = Sql_Command.Parameters.Add("tu_sid", SqlDbType.Int);
					spt_tu_sid.Direction = ParameterDirection.Output;

					Sql_Command.ExecuteNonQuery();

					tu_sid = spt_tu_sid.Value.ToString();

					// 更新試卷的參加人數
					SqlString = "Update Ts_Paper Set tp_member = (Select Count(*) From Ts_User Where tp_sid = @tp_sid)";
					SqlString += " Where tp_sid = @tp_sid;";

					Sql_Command.Parameters.Clear();
					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);

					Sql_Command.ExecuteNonQuery();

					mErr = "alert(\"新增完成!\\n\");parent.location.reload(true);";
				}
			}
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", mErr, true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}
}
