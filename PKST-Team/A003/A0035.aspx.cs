//---------------------------------------------------------------------------- 
//程式功能	票選資料管理 > 票選問卷項目處理
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public partial class _A0035 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int bh_sid = -1;
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("A003", false);

			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out bh_sid))
				{
					lb_bh_sid.Text = bh_sid.ToString();
					ods_Bt_Item.SelectParameters["bh_sid"].DefaultValue = bh_sid.ToString();

					// 取得資料
					if (!GetData())
						mErr = "找不到相關資料!\\n";
				}
				else
					mErr = "參數格式錯誤!\\n";
			}
			else
				mErr = "參數傳入錯誤!\\n";
		}

		if (mErr == "")
		{
			#region 檢查頁數是否超過
			ods_Bt_Item.DataBind();
			gv_Bt_Item.DataBind();
			if (gv_Bt_Item.PageCount < gv_Bt_Item.PageIndex)
			{
				gv_Bt_Item.PageIndex = gv_Bt_Item.PageCount;
				gv_Bt_Item.DataBind();
			}

			lb_pageid.Text = gv_Bt_Item.PageIndex.ToString();
			#endregion
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");parent.close_all();", true);
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
		string SqlString = "", is_check = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 bh_title, is_check, bh_topic, bh_scnt, bh_acnt, bh_total, bh_time, init_time";
			SqlString += " From Bt_Head Where bh_sid = @bh_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("bh_sid", lb_bh_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						lb_bh_title.Text = Sql_Reader["bh_title"].ToString().Trim();
						lb_bh_topic.Text = Sql_Reader["bh_topic"].ToString().Trim().Replace("\r\n","<br>");
						is_check = Sql_Reader["is_check"].ToString();

						if (is_check == "0")
							lb_is_check.Text = "單選";
						else if (is_check == "1")
							lb_is_check.Text = "複選全部";
						else
							lb_is_check.Text = "複選 " + is_check + " 題";

						lb_bh_scnt.Text = int.Parse(Sql_Reader["bh_scnt"].ToString()).ToString("N0");
						lb_bh_acnt.Text = int.Parse(Sql_Reader["bh_acnt"].ToString()).ToString("N0");
						lb_bh_total.Text = int.Parse(Sql_Reader["bh_total"].ToString()).ToString("N0");
						if (Sql_Reader["bh_time"].ToString() == "")
							lb_bh_time.Text = "&nbsp;";
						else
							lb_bh_time.Text = DateTime.Parse(Sql_Reader["bh_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
						lb_init_time.Text = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

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

	// 換頁
	protected void gv_Bt_Item_PageIndexChanged(object sender, GridViewPageEventArgs e)
	{
		lb_pageid.Text = gv_Bt_Item.PageIndex.ToString();
	}
}
