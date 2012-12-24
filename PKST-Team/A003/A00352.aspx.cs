//---------------------------------------------------------------------------- 
//程式功能	票選資料管理 > 票選問卷項目處理 > 修改問卷項目
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _A00352 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int bh_sid = 0, bi_sid = 0;
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("A003", false);

			if (Request["sid"] != null && Request["bh_sid"] != null)
			{
				if (int.TryParse(Request["sid"], out bi_sid) && int.TryParse(Request["bh_sid"], out bh_sid))
				{
					lb_bh_sid.Text = bh_sid.ToString();
					lb_bi_sid.Text = bi_sid.ToString();

					if (!GetData())
						mErr = "找不到指定的資料!\\n";
				}
				else
					mErr = "參數傳送錯誤!\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";

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
		string SqlString = "", mErr = "";
		int bi_sort = 255;

		tb_bi_sort.Text = tb_bi_sort.Text.Trim();
		if (int.TryParse(tb_bi_sort.Text, out bi_sort))
		{
			if (bi_sort < 0 && bi_sort > 255)
				mErr += "顯示順序」請輸入(0 ~ 255)的數字!\\n";
		}
		else
			mErr += "顯示順序」請輸入(0 ~ 255)的數字!\\n";

		tb_bi_desc.Text = tb_bi_desc.Text.Trim();
		if (tb_bi_desc.Text == "")
		{
			mErr += "請正確輸入「項目文字」!\\n";
		}

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				SqlString = "Update Bt_Item Set bi_sort = @bi_sort, bi_desc = @bi_desc, init_time = getdate()";
				SqlString += " Where bi_sid = @bi_sid And bh_sid = @bh_sid";

				using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
				{
					Sql_Conn.Open();

					Sql_Command.Parameters.AddWithValue("bh_sid", lb_bh_sid.Text);
					Sql_Command.Parameters.AddWithValue("bi_sid", lb_bi_sid.Text);
					Sql_Command.Parameters.AddWithValue("bi_sort", bi_sort);
					Sql_Command.Parameters.AddWithValue("bi_desc", tb_bi_desc.Text);

					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();
				}
			}
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "parent.location.reload(true);", true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	// 取得資料
	private bool GetData()
	{
		bool ckbool = false;
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 bi_sort, bi_desc From Bt_Item Where bi_sid = @bi_sid And bh_sid = @bh_sid";
			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("bh_sid", lb_bh_sid.Text);
				Sql_Command.Parameters.AddWithValue("bi_sid", lb_bi_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						tb_bi_sort.Text = Sql_Reader["bi_sort"].ToString();
						tb_bi_desc.Text = Sql_Reader["bi_desc"].ToString().Trim();

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
}
