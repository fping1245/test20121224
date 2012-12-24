//---------------------------------------------------------------------------- 
//程式功能	線上客服-客戶使用端 > 對話視窗 > 發送訊息
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _700111 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string mErr = "";
		int cu_sid = -1, mg_sid = -1;

		if (!IsPostBack)
		{
			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("7001", false);


			mErr = "參數傳送錯誤!返回主頁面\\n";

			if (Request["sid"] != null && Request["mg_sid"] != null)
			{
				if (int.TryParse(Request["sid"], out cu_sid) && int.TryParse(Request["mg_sid"], out mg_sid))
				{
					lb_cu_sid.Text = cu_sid.ToString();
					lb_mg_sid.Text = mg_sid.ToString();

					if (Chk_Cs_User(cu_sid, mg_sid))
						mErr = "";
					else
						mErr = "找不到客服要求紀錄!\\n";
				}
			}

			if (mErr != "")
				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");parent.location.replace(\"7001.aspx\");", true);
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

	// 傳送訊息 (存入客服交談紀錄)
	protected void bn_smsg_Click(object sender, EventArgs e)
	{
		String_Func sfc = new String_Func();
		string SqlString = "", cu_rtn = "0";

		if (tb_cm_desc.Text.Trim() != "")
		{
			cu_rtn = Chk_Talk();

			if (cu_rtn == "2")
			{
				// 已結束就不可上傳
				bn_smsg.Enabled = false;
				bn_sfile.Enabled = false;

				ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"交談已結束，請重新提出客服要求！\");parent.location.replace(\"7001.aspx\");", true);
			}
			else
			{
				// 處理換行字元，並取得左方1000個字，以附超過資料庫限制
				tb_cm_desc.Text = sfc.Left(tb_cm_desc.Text.Replace("\n", "<br>").Trim(), 1000);

				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					using (SqlCommand Sql_Command = new SqlCommand())
					{
						Sql_Conn.Open();

						SqlString = "Insert Into Cs_Message (cu_sid, cm_time, cm_object, cm_desc) Values ";
						SqlString += "(@cu_sid, getdate(), 1, @cm_desc)";

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("cu_sid", lb_cu_sid.Text);
						Sql_Command.Parameters.AddWithValue("cm_desc", tb_cm_desc.Text);

						Sql_Command.ExecuteNonQuery();

						Sql_Command.Dispose();

						Sql_Conn.Close();
					}
				}
			}
		}

		tb_cm_desc.Text = "";
		tb_cm_desc.Focus();
	}

	// 上傳檔案 (存入客服交談紀錄)
	protected void bn_sfile_Click(object sender, EventArgs e)
	{
		string SqlString = "", mErr = "", cu_rtn = "0";

		if (fu_file.HasFile)
		{
			if (fu_file.PostedFile.ContentLength < 4194304)
			{
				cu_rtn = Chk_Talk();

				if (cu_rtn == "2")
				{
					// 已結束就不可上傳
					bn_smsg.Enabled = false;
					bn_sfile.Enabled = false;

					ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"交談已結束，請重新提出客服要求！\");parent.location.replace(\"7001.aspx\");", true);
				}
				else
				{
					using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
					{
						using (SqlCommand Sql_Command = new SqlCommand())
						{
							Sql_Conn.Open();

							SqlString = "Insert Into Cs_Message (cu_sid, cm_time, cm_object, cm_fname, cm_fsize, cm_ftype, cm_fcontent) Values ";
							SqlString += "(@cu_sid, getdate(), 1, @cm_fname, @cm_fsize, @cm_ftype, @cm_fcontent)";

							Sql_Command.Connection = Sql_Conn;
							Sql_Command.CommandText = SqlString;
							Sql_Command.Parameters.AddWithValue("cu_sid", lb_cu_sid.Text);
							Sql_Command.Parameters.AddWithValue("cm_fname", fu_file.FileName);
							Sql_Command.Parameters.AddWithValue("cm_fsize", fu_file.PostedFile.ContentLength);
							Sql_Command.Parameters.AddWithValue("cm_ftype", fu_file.PostedFile.ContentType);
							Sql_Command.Parameters.AddWithValue("cm_fcontent", fu_file.FileBytes);

							Sql_Command.ExecuteNonQuery();

							Sql_Command.Dispose();
						}
					}
				}
			}
			else
				mErr = "上傳檔案不可超過 4M bytes!\\n";
		}

		if (mErr != "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	// 檢查是否有客戶服務要求
	private bool Chk_Cs_User(int cu_sid, int mg_sid)
	{
		string SqlString = "";
		bool cktf = false;

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				Sql_Conn.Open();
				SqlString = "Select Top 1 cu_sid From Cs_User u Where u.cu_sid = @cu_sid And u.mg_sid = @mg_sid";

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;
				Sql_Command.Parameters.AddWithValue("cu_sid", cu_sid);
				Sql_Command.Parameters.AddWithValue("mg_sid", mg_sid);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
						cktf = true;
					else
						cktf = false;

					Sql_Reader.Close();
				}
			}
		}

		return cktf;
	}

	// 檢查交談是否已結束
	private string Chk_Talk()
	{
		string SqlString = "", cu_rtn = "0";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				Sql_Conn.Open();
				Sql_Command.Connection = Sql_Conn;

				SqlString = "Select Top 1 cu_rtn From Cs_User Where cu_sid = @cu_sid";
				Sql_Command.CommandText = SqlString;
				Sql_Command.Parameters.AddWithValue("cu_sid", lb_cu_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						cu_rtn = Sql_Reader["cu_rtn"].ToString();
					}
					Sql_Reader.Close();
					Sql_Reader.Dispose();
				}

				Sql_Conn.Close();
			}
		}

		return cu_rtn;
	}
}
