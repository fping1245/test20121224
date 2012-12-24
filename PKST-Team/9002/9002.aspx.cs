//---------------------------------------------------------------------------- 
//程式功能	POP3 收信處理
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _9002 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int mg_sid = -1;
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("9002", true);

			CleanSession();		// 清除使用的 Session

			mg_sid = int.Parse(Session["mg_sid"].ToString());

			// 取得個人POP3帳戶資料
			if (!Get_Data(mg_sid))
				mErr = "請設定 POP3 郵件主機的資料!\\n";
		}

		if (mErr == "")
		{
			ods_POP3_Mail.SelectParameters["ppa_sid"].DefaultValue = lb_ppa_sid.Text;
			ods_POP3_Mail.DataBind();
			gv_POP3_Mail.DataBind();
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");host_set();", true);
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

	// 換頁
	protected void gv_POP3_Mail_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid.Text = gv_POP3_Mail.PageIndex.ToString();
	}

	// 取得個人POP3帳戶資料
	private bool Get_Data(int mg_sid)
	{
		string SqlString = "";
		bool ckfg = false, ckfind = false;

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 ppa_sid, ppa_host, ppa_port, ppa_id, ppa_pw, ppa_num, ppa_size, get_time From POP3_Account Where mg_sid = @mg_sid";

			using (SqlCommand Sql_Command = new SqlCommand())
			{
				Sql_Conn.Open();

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;

				Sql_Command.Parameters.AddWithValue("mg_sid", mg_sid);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						ckfind = true;

						lb_ppa_sid.Text = Sql_Reader["ppa_sid"].ToString();
						lb_ppa_host.Text = Sql_Reader["ppa_host"].ToString();
						lb_ppa_port.Text = Sql_Reader["ppa_port"].ToString();
						lb_ppa_num.Text = int.Parse(Sql_Reader["ppa_num"].ToString()).ToString("N0");
						lb_ppa_size.Text = int.Parse(Sql_Reader["ppa_size"].ToString()).ToString("N0");

						ods_POP3_Mail.SelectParameters["ppa_sid"].DefaultValue = lb_ppa_sid.Text;

						if (Sql_Reader["get_time"] != null)
							lb_get_time.Text = DateTime.Parse(Sql_Reader["get_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

						if (lb_ppa_host.Text != "" && Sql_Reader["ppa_id"].ToString().Trim() != "" &&
							Sql_Reader["ppa_pw"].ToString().Trim() != "")
							ckfg = true;

					}

					Sql_Reader.Close();
				}

				// 新增資料
				if (! ckfind)
				{
					SqlString = "Insert Into POP3_Account (mg_sid) Values (@mg_sid)";
					SqlString += "Select @ppa_sid = Scope_Identity()";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("mg_sid", mg_sid);

					SqlParameter spt_ppa_sid = Sql_Command.Parameters.Add("ppa_sid", SqlDbType.Int);
					spt_ppa_sid.Direction = ParameterDirection.Output;

					Sql_Command.ExecuteNonQuery();

					if (spt_ppa_sid.Value != null)
					{
						lb_ppa_sid.Text = spt_ppa_sid.Value.ToString();
						ods_POP3_Mail.SelectParameters["ppa_sid"].DefaultValue = lb_ppa_sid.Text;
					}
				}

				Sql_Conn.Close();
			}
		}

		return ckfg;
	}

	// 清除使用的 Session
	private void CleanSession()
	{
		Session.Remove("ppa_sid");
		Session.Remove("ppa_host");
		Session.Remove("ppa_port");
		Session.Remove("ppa_id");
		Session.Remove("ppa_pw");
	}
}
