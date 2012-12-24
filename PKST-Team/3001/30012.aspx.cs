//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 建立子目錄
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

public partial class _30012 : System.Web.UI.Page
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
					lb_al_sid.Text = ckint.ToString();
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

	// 建立子目錄
	protected void bn_mkdir_ok_Click(object sender, EventArgs e)
	{
		string smkdir = "", mErr = "";
		int al_sid = -1;

		smkdir = tb_al_name.Text.Trim();
		if (smkdir == "")
			mErr = "請輸入子目錄的名稱!\\n";
		else
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				Sql_Conn.Open();

				string SqlString = "";

				using (SqlCommand Sql_Command = new SqlCommand())
				{
					#region 檢查是否有同名的目錄
					SqlString = "Select Top 1 al_sid From Al_List Where up_al_sid = @up_al_sid And al_name = @al_name";
					Sql_Command.Connection = Sql_Conn;
					Sql_Command.CommandText = SqlString;

					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("up_al_sid", lb_al_sid.Text);
					Sql_Command.Parameters.AddWithValue("al_name", tb_al_name.Text.Trim());

					SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();
					if (Sql_Reader.Read())
						mErr = "已有相同名稱的目錄!\\n";

					Sql_Reader.Close();
					Sql_Reader.Dispose();

					#endregion
					
					if (mErr == "")
					{
						#region 加入新目錄
						SqlString = "Insert Into Al_List (up_al_sid, al_name, al_desc, al_sort) Values (@up_al_sid, @al_name, @al_desc, 0);";
						SqlString = SqlString + "Select @al_sid = Scope_Identity()";

						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;

						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("up_al_sid", lb_al_sid.Text);
						Sql_Command.Parameters.AddWithValue("al_name", tb_al_name.Text.Trim());
						Sql_Command.Parameters.AddWithValue("al_desc", tb_al_desc.Text.Trim());

						SqlParameter spt_al_sid = Sql_Command.Parameters.Add("al_sid", SqlDbType.Int);
						spt_al_sid.Direction = ParameterDirection.Output;

						Sql_Command.ExecuteNonQuery();

						// 取得新建資料的 al_sid
						al_sid = (int)spt_al_sid.Value;
						#endregion

						#region 設定排序
						SqlString = "Update Al_List Set al_sort = (Select Max(al_sort) + 1 From Al_List Where up_al_sid = @up_al_sid)";
						SqlString = SqlString + " Where al_sid = @al_sid";

						Sql_Command.Parameters.Clear();
						Sql_Command.Connection = Sql_Conn;
						Sql_Command.CommandText = SqlString;

						Sql_Command.Parameters.AddWithValue("up_al_sid", lb_al_sid.Text);
						Sql_Command.Parameters.AddWithValue("al_sid", al_sid.ToString());

						Sql_Command.ExecuteNonQuery();
						#endregion
					}
				}
				Sql_Conn.Close();
			}
		}

		if (mErr == "")
			lt_show.Text = "<script language=\"javascript\">alert(\"目錄建立完成!\\n\");parent.tree_reload(" + lb_al_sid.Text + ");parent.clean_win();</script>";
		else
			lt_show.Text = "<script language=\"javascript\">alert(\"" + mErr + "\");parent.clean_win();</script>";	
	}
}
