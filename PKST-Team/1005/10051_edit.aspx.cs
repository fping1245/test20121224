//---------------------------------------------------------------------------- 
//程式功能	人員資料管理 > 明細內容 > 修改資料
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _10051_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		int mg_sid = -1;
		string SqlString = "", mErr = "";

		if (!IsPostBack)
		{
			int ckint = 0;

            // 檢查使用者權限但不存入使用紀錄
			//Check_Power("1005", false);

			#region 承接上一頁的查詢條件設定
			if (Request["pageid"] != null)
			{
				if (int.TryParse(Request["pageid"].ToString(), out ckint))
					lb_page.Text = "?pageid=" + ckint.ToString();
				else
					lb_page.Text = "?pageid=0";
			}

			if (Request["mg_sid"] != null)
				lb_page.Text = lb_page.Text + "&mg_sid=" + Request["mg_sid"];

			if (Request["mg_name"] != null)
				lb_page.Text = lb_page.Text + "&mg_name=" + Server.UrlEncode(Request["mg_name"]);

			if (Request["mg_nike"] != null)
				lb_page.Text = lb_page.Text + "&mg_nike=" + Server.UrlEncode(Request["mg_nike"]);

			if (Request["btime"] != null)
				lb_page.Text = lb_page.Text + "&btime=" + Server.UrlEncode(Request["btime"]);

			if (Request["etime"] != null)
				lb_page.Text = lb_page.Text + "&etime=" + Server.UrlEncode(Request["etime"]);

			#endregion

			#region 檢查傳入參數
			if (Request["sid"] != null)
			{
				#region 取得相關資料
				if (int.TryParse(Request["sid"].ToString(), out mg_sid))
				{
					lb_page.Text = lb_page.Text + "&sid=" + mg_sid.ToString();

					using (SqlConnection sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
					{
						sql_conn.Open();

						#region 取得人員基本資料
						SqlString = "Select Top 1 mg_sid, mg_name, mg_nike, mg_id, mg_unit, mg_desc, last_date, init_time";
						SqlString = SqlString + " From Manager Where mg_sid = " + mg_sid.ToString();

						using (SqlCommand Sql_Command = new SqlCommand(SqlString, sql_conn))
						{
							using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
							{
								if (Sql_Reader.Read())
								{
									lb_mg_sid.Text = Sql_Reader["mg_sid"].ToString();
									tb_mg_id.Text = Sql_Reader["mg_id"].ToString();
									tb_mg_name.Text = Sql_Reader["mg_name"].ToString().Trim();
									tb_mg_nike.Text = Sql_Reader["mg_nike"].ToString().Trim();
									tb_mg_unit.Text = Sql_Reader["mg_unit"].ToString().Trim();
									tb_mg_desc.Text = Sql_Reader["mg_desc"].ToString().Trim();
									if (Sql_Reader.IsDBNull(6))
										lb_last_date.Text = "[還沒有登入過]";
									else
										lb_last_date.Text = ((DateTime)Sql_Reader["last_date"]).ToString("yyyy/MM/dd hh:mm");
									lb_init_time.Text = ((DateTime)Sql_Reader["init_time"]).ToString("yyyy/MM/dd hh:mm");

									lb_pg_mg_sid.Text = mg_sid.ToString();

									if (mg_sid == 0)
									{
										tb_mg_name.ReadOnly = true;
										tb_mg_name.Enabled = false;
									}
								}
								else
									mErr = "找不到指定的人員資料!\\n";
							}
						}
						#endregion
					}
				}
				else
					mErr = "網頁參數傳送錯誤!\\n";

				#endregion
			}
			else
				mErr = "網頁參數傳送錯誤!\\n";

			if (mErr != "")
				lt_show.Text = "<script language='javascript'>alert('" + mErr + "');history.go(-1);</script>";

			#endregion
		}
    }

    // Check_Power() 檢查使用者權限並存入使用紀錄
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

	protected void lb_ok_Click(object sender, EventArgs e)
	{
		string mErr = "";

		// 載入字串函數
		String_Func sfc = new String_Func();

		// 載入公用函數
		Common_Func cfc = new Common_Func();

		if (tb_mg_id.Text.Trim() == "")
			mErr = mErr + "「登入帳號」沒有輸入!\\n";
		else
			if (cfc.CheckSQL(tb_mg_id.Text.Trim()))
				mErr = mErr + "「登入帳號」請勿使用特殊符號!\\n";

		if (tb_mg_name.Text.Trim() == "")
			mErr = mErr + "「姓名」沒有輸入!\\n";

		if (tb_mg_nike.Text.Trim() == "")
			mErr = mErr + "「暱稱」沒有輸入!\\n";

		if (tb_mg_unit.Text.Trim() == "")
			mErr = mErr + "「單位」沒有輸入!\\n";

		if (mErr == "")
		{
			using (SqlConnection Sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				string SqlString = "";

				Sql_conn.Open();

				// 檢查「帳號」是否有其它人用過 (帳號不允許重覆)
				SqlString = "Select Top 1 mg_id From Manager Where mg_id = @mg_id And mg_sid <> @mg_sid";

				using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_conn))
				{

					Sql_Command.Parameters.AddWithValue("@mg_id", sfc.Left(tb_mg_id.Text,12));
					Sql_Command.Parameters.AddWithValue("@mg_sid", lb_pg_mg_sid.Text);

					SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

					if (Sql_Reader.Read())
						mErr = mErr + "此「登入帳號」已經有人使用過了，請重新設定!\\n";

					Sql_Reader.Close();
					Sql_Reader.Dispose();
				}

				if (mErr == "")
				{
					// 建立 SQL 修改資料的語法
					SqlString = "Update Manager Set mg_name = @mg_name, mg_nike = @mg_nike, mg_id = @mg_id";
					SqlString = SqlString + ", mg_unit = @mg_unit, mg_desc = @mg_desc, init_time = getdate()";
					SqlString = SqlString + " Where mg_sid = @mg_sid";


					using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_conn))
					{
						Sql_Command.Parameters.Clear();

						// 擷取字串到資料庫所規範的大小 sfc.Left(string mdata, int leng)
						Sql_Command.Parameters.AddWithValue("@mg_sid", lb_pg_mg_sid.Text);
						Sql_Command.Parameters.AddWithValue("@mg_name", sfc.Left(tb_mg_name.Text, 12));
						Sql_Command.Parameters.AddWithValue("@mg_nike", sfc.Left(tb_mg_nike.Text, 12));
						Sql_Command.Parameters.AddWithValue("@mg_id", sfc.Left(tb_mg_id.Text, 12));
						Sql_Command.Parameters.AddWithValue("@mg_unit", sfc.Left(tb_mg_unit.Text, 50));
						Sql_Command.Parameters.AddWithValue("@mg_desc", sfc.Left(tb_mg_desc.Text, 1000));

						Sql_Command.ExecuteNonQuery();
					}
				}
			}
		}


		if (mErr == "")
		{
			mErr = "alert('資料修改完成!\\n');location.replace('10051.aspx" + lb_page.Text + "');";
		}
		else
			mErr = "alert('" + mErr + "')";

		lt_show.Text = "<script language=javascript>" + mErr + "</script>";
	}
}
