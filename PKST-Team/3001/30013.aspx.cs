//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 刪除子目錄
//備註說明	使用資料庫儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class _30013 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = -1;
			string mErr = "";

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("3001", false);

			if (Request["al_sid"] != null)
			{
				if (int.TryParse(Request["al_sid"], out ckint))
				{
					if (ckint == 0)
						mErr = "根目錄不可刪除\\n";
					else
					{
						lb_al_sid.Text = ckint.ToString();

						#region 取得目前目錄的名稱
						using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
						{
							string SqlString = "Select Top 1 al_name From Al_List Where al_sid = @al_sid";
							using (SqlCommand Sql_Command = new SqlCommand())
							{
								Sql_Command.Connection = Sql_Conn;
								Sql_Command.CommandText = SqlString;
								Sql_Command.Parameters.AddWithValue("@al_sid", ckint.ToString());

								Sql_Conn.Open();

								SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

								if (Sql_Reader.Read())
									lb_al_name.Text = Sql_Reader["al_name"].ToString().Trim();
								else
									lt_show.Text = "<script language=javascript>alert(\"找不到指定的路徑\\n\");</script>";
							}
						}
						#endregion
					}
				}
				else
					mErr = "參數傳送錯誤！\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";

			if (mErr != "")
				lt_show.Text = "<script language=\"javascript\">alert(\"" + mErr + "\");parent.close_all();parent.clean_win();</script>";
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

	// 刪除目錄
	protected void bn_rddir_ok_Click(object sender, EventArgs e)
	{
		string mErr = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			Sql_Conn.Open();

			string SqlString = "", up_al_sid = "";

			using (SqlCommand Sql_Command = new SqlCommand())
			{
				SqlDataReader Sql_Reader;

				#region 檢查目錄內是否有檔案或子目錄

				// 檔案
				SqlString = "Select Top 1 al_sid From Al_Content Where al_sid = @al_sid";
				SqlString = SqlString + " Union";
				SqlString = SqlString + " Select Top 1 al_sid From Al_List Where up_al_sid = @al_sid";

				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;

				Sql_Command.Parameters.AddWithValue("al_sid", lb_al_sid.Text);

				Sql_Reader = Sql_Command.ExecuteReader();

				if (Sql_Reader.Read())
					mErr = "目錄中尚有檔案或子目錄，不允許刪除!\\n";

				Sql_Reader.Close();
				Sql_Reader.Dispose();
				#endregion

				if (mErr == "")
				{
					#region 取得上一層目錄編號
					SqlString = "Select Top 1 up_al_sid From Al_List Where al_sid = @al_sid";

					Sql_Command.Parameters.Clear();
					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("al_sid", lb_al_sid.Text);

					Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
						up_al_sid = Sql_Reader["up_al_sid"].ToString();
					else
						mErr = "找不到這個目錄!\\n";

					Sql_Reader.Close();
					Sql_Reader.Dispose();
					#endregion
				}

				if (mErr == "")
				{
					#region 刪除目錄
					SqlString = "Delete Al_List Where al_sid = @al_sid";

					Sql_Command.Parameters.Clear();
					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.AddWithValue("al_sid", lb_al_sid.Text);

					Sql_Command.ExecuteNonQuery();
					#endregion
				}
			}

			if (mErr == "")
				lt_show.Text = "<script language=\"javascript\">alert(\"目錄「" + lb_al_name.Text + "」已經刪除!\\n\");parent.location.replace(\"3001.aspx?al_sid=" + up_al_sid.ToString() + "\");parent.clean_win();</script>";
			else
				lt_show.Text = "<script language=\"javascript\">alert(\"" + mErr + "\\n\");parent.clean_win();</script>";
		}
	}
}
