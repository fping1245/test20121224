//---------------------------------------------------------------------------- 
//程式功能	相簿管理
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

public partial class _3001 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int ckint = -1;

			// 檢查使用者權限並存入登入紀錄
			//Check_Power("3001", true);

			if (Request["al_sid"] != null)
			{
				if (int.TryParse(Request["al_sid"], out ckint))
				{
					lb_al_sid.Text = ckint.ToString();

					if (ckint == 0)
						lb_show_path.Text = "根目錄";
					else
					{
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
									lb_show_path.Text = Sql_Reader["al_name"].ToString().Trim();
								else
									lt_show.Text = "<script language=javascript>alert(\"找不到指定的路徑\\n\");location.replace(\"3001.aspx?al_sid=0\");</script>";
							}
						}
						#endregion
					}
				}
				else
				{
					lb_al_sid.Text = "0";
					lb_show_path.Text = "根目錄";
				}
			}
			else
			{
				lb_al_sid.Text = "0";
				lb_show_path.Text = "根目錄";
			}


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

	// 回到根目錄
	protected void bn_go_root_Click(object sender, EventArgs e)
	{
		Response.Redirect("3001.aspx?al_sid=0");
	}
}
