//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 縮圖顯示 > 全圖顯示 > 修改相片說明
//備註說明	使用資料庫儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class _3001624 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ac_sid = -1, al_sid = -1, rownum = 0;
			string mErr = "";

			// 檢查使用者權限，但不存登入紀錄
			//Check_Power("3001", false);

			#region 檢查傳入參數
			if (Request["al_sid"] == null || Request["ac_sid"] == null)
				mErr = "參數傳送錯誤!\\n";
			else if (int.TryParse(Request["ac_sid"], out ac_sid))
			{
				if (! int.TryParse(Request["al_sid"], out al_sid))
					mErr = "參數傳送錯誤!\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";
			#endregion

			if (mErr == "")
			{
				#region 取得相片資訊
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					Sql_Conn.Open();

					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";

						SqlString = "Select Top 1 ac_name, ac_size, ac_width, ac_height, ac_type, ac_desc, init_time";
						SqlString = SqlString + " From Al_Content Where ac_sid = @ac_sid And al_sid = @al_sid";

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("ac_sid", ac_sid.ToString());
						Sql_Command.Parameters.AddWithValue("al_sid", al_sid.ToString());

						using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
						{
							if (Sql_Reader.Read())
							{
								tb_ac_name.Text = Sql_Reader["ac_name"].ToString().Trim();
								lb_ac_size.Text = int.Parse(Sql_Reader["ac_size"].ToString()).ToString("N0");
								lb_ac_type.Text = Sql_Reader["ac_type"].ToString();
								lb_ac_wh.Text = Sql_Reader["ac_width"].ToString() + "&nbsp;×&nbsp;" + Sql_Reader["ac_height"].ToString();
								tb_ac_desc.Text = Sql_Reader["ac_desc"].ToString();
								lb_init_time.Text = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

								lb_ac_sid.Text = ac_sid.ToString();
								lb_al_sid.Text = al_sid.ToString();
								lb_rownum.Text = rownum.ToString();
							}
							else
								mErr = "找不到指定的相片!\\n";

							Sql_Reader.Close();
						}
					}
				}
				#endregion
			}

			if (mErr != "")
				lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");parent.close_all();parent.clean_win();</script>";
		}
    }

	// 檢查使用者權限並存入登入紀錄
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

	// 確定修改
	protected void bn_ok_Click(object sender, EventArgs e)
	{
		string mErr = "";

		if (tb_ac_name.Text == "")
			mErr = "相片名稱一定要填寫!\\n";

		if (mErr == "")
		{
			// 修改相片說明
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				Sql_Conn.Open();

				using (SqlCommand Sql_Command = new SqlCommand())
				{
					string SqlString = "";

					SqlString = "Update Al_Content Set ac_name = @ac_name, ac_desc = @ac_desc";
					SqlString = SqlString + " Where ac_sid = @ac_sid And al_sid = @al_sid";

					Sql_Command.Connection = Sql_Conn;
					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("ac_name", tb_ac_name.Text.Trim());
					Sql_Command.Parameters.AddWithValue("ac_desc", tb_ac_desc.Text.Trim());
					Sql_Command.Parameters.AddWithValue("ac_sid", lb_ac_sid.Text);
					Sql_Command.Parameters.AddWithValue("al_sid", lb_al_sid.Text);

					Sql_Command.ExecuteNonQuery();
				}
			}
		}

		if (mErr == "")
		{
			lt_show.Text = "<script language=javascript>alert(\"資料修改完成!\");parent.thumb_reload();";
			lt_show.Text = lt_show.Text + "parent.location.reload();</script>";
		}
		else
			lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");</script>";

	}
}
