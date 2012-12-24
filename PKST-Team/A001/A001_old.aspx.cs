//---------------------------------------------------------------------------- 
//程式功能	票選活動(循序)
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public partial class _A001 : System.Web.UI.Page
{
	static CheckBox[] cb_tmp;
	static RadioButton[] rb_tmp;
	static string[] bi_sid;

    protected void Page_Load(object sender, EventArgs e)
    {
		string mErr = "";

		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("A001", true);

			if (!Get_bh_sid())
				mErr = "資料取得錯誤!\\n";
		}
        
		if (mErr == "")
		{
			// 取得資料
			GetData();
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
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

	// 取得問卷主題編號
	private bool Get_bh_sid()
	{
		bool ckbool = false;
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				Sql_Conn.Open();
				Sql_Command.Connection = Sql_Conn;

				// 讀取問卷主題資料
				SqlString = "Execute dbo.p_Get_Bt_Head;";

				Sql_Command.CommandText = SqlString;

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						lb_bh_sid.Text = Sql_Reader["bh_sid"].ToString();
						lb_item_num.Text = Sql_Reader["item_num"].ToString();
						ckbool = true;
					}
				}
			}
		}

		return ckbool;
	}

	// 取得資料，並建立選項
	private void GetData()
	{
		string SqlString = "", mErr = "";
		int icnt = 0, item_num = 0;
		SqlDataReader Sql_Reader;

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				Sql_Conn.Open();
				Sql_Command.Connection = Sql_Conn;

				#region 讀取問卷主題資料
				SqlString = "Select Top 1 bh_title, bh_topic, is_check From Bt_Head Where bh_sid=@bh_sid;";

				Sql_Command.CommandText = SqlString;
				Sql_Command.Parameters.AddWithValue("bh_sid", lb_bh_sid.Text);

				Sql_Reader = Sql_Command.ExecuteReader();

				if (Sql_Reader.Read())
				{
					lb_bh_title.Text = Sql_Reader["bh_title"].ToString().Trim();
					lb_bh_topic.Text = Sql_Reader["bh_topic"].ToString().Trim().Replace("\r\n", "<br>");
					lb_is_check.Text = Sql_Reader["is_check"].ToString();
					if (lb_is_check.Text == "0")
						lt_is_check.Text = "您可從以下項目，圈選<b>1</b>個最適合的答案。(單選)";
					else if (lb_is_check.Text == "1")
						lt_is_check.Text = "您可從以下項目，不限數量隨意圈選答案。(複選)";
					else
						lt_is_check.Text = "您可以從以下項目，圈選 1 ~ " + lb_is_check.Text + " 項的答案。(複選)";
				}
				else
					mErr = "找不到問卷標題資料!\\n";

				Sql_Reader.Close();

				#endregion

				#region 取得問卷項目資料
				if (mErr == "")
				{
					item_num = int.Parse(lb_item_num.Text);
					bi_sid = new string[item_num];

					SqlString = "Select bi_sid, bi_desc From Bt_Item Where bh_sid = @bh_sid;";

					Sql_Command.Parameters.Clear();
					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("bh_sid", lb_bh_sid.Text);

					Sql_Reader = Sql_Command.ExecuteReader();

					switch (lb_is_check.Text)
					{
						case "0":
							// 單選
							rb_tmp = new RadioButton[item_num];

							while (Sql_Reader.Read())
							{
								bi_sid[icnt] = Sql_Reader["bi_sid"].ToString();

								rb_tmp[icnt] = new RadioButton();
								rb_tmp[icnt].Text = Sql_Reader["bi_desc"].ToString().Trim() + "<br>";
								rb_tmp[icnt].EnableViewState = true;
								rb_tmp[icnt].EnableTheming = true;
								rb_tmp[icnt].GroupName = "rb_group";
								pl_is_check.Controls.Add(rb_tmp[icnt]);

								icnt++;
							}
							break;
						default:
							// 複選
							cb_tmp = new CheckBox[item_num];

							while (Sql_Reader.Read())
							{
								bi_sid[icnt] = Sql_Reader["bi_sid"].ToString();

								cb_tmp[icnt] = new CheckBox();
								cb_tmp[icnt].Text = Sql_Reader["bi_desc"].ToString().Trim() + "<br>";
								cb_tmp[icnt].EnableViewState = true;
								cb_tmp[icnt].EnableTheming = true;
								pl_is_check.Controls.Add(cb_tmp[icnt]);

								icnt++;
							}
							break;
					}
					Sql_Reader.Close();
				}
				#endregion

				Sql_Conn.Close();
			}
		}

		if (mErr != "")
		{
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
		}
	}

	// 確定完成 (存檔)
	protected void lk_ok_Click(object sender, EventArgs e)
	{
		int item_num = int.Parse(lb_item_num.Text);
		int icnt = 0, scnt = 0, ckint = 0;
		string sbi_sid = "", mErr = "", SqlString = "";

		#region 判斷問卷型式及選擇的答案
		if (lb_is_check.Text == "0")
		{
			// 單選
			scnt = 1;	// 項目選擇上限

			// 檢查是否有選擇
			for (icnt = 0; icnt < item_num; icnt++)
			{
				if (rb_tmp[icnt].Checked)
				{
					sbi_sid = bi_sid[icnt];
					ckint++;
				}
			}
		}
		else if (lb_is_check.Text == "1")
		{
			// 複選全部
			scnt = item_num;	// 項目選擇上限
			for (icnt = 0; icnt < item_num; icnt++)
			{
				if (cb_tmp[icnt].Checked)
				{
					sbi_sid += bi_sid[icnt] + ";";
					ckint++;
				}
			}	
		}
		else
		{
			// 限制數量複選
			scnt = int.Parse(lb_is_check.Text);	// 項目選擇上限

			for (icnt = 0; icnt < item_num; icnt++)
			{
				if (cb_tmp[icnt].Checked)
				{
					sbi_sid += bi_sid[icnt] + ";";
					ckint++;
				}
			}
		}
		#endregion

		if (ckint > scnt || ckint < 1)
		{
			// 判斷選擇項目數量
			if (scnt == 1)
				mErr = "請選擇 1 個項目!\\n";
			else
				mErr = "請選擇 1 ~ " + scnt.ToString() + " 個項目!\\n";
		}
		else
		{
			string[] stmp = sbi_sid.Split(';');

			#region 答案存檔
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					Sql_Command.Connection = Sql_Conn;

					Sql_Conn.Open();
					for (icnt = 0; icnt < ckint; icnt++)
					{
						// 存入 Bt_Ballot
						SqlString = "Insert Into Bt_Ballot (bh_sid, bi_sid, bb_ip) Values (@bh_sid, @bi_sid, @bb_ip);";

						// 重新統計 Bt_Item
						SqlString += "Update Bt_Item Set bi_total = (Select Count(*) From Bt_Ballot Where bh_sid = @bh_sid And bi_sid = @bi_sid)";
						SqlString += ", bi_time = getdate() Where bi_sid = @bi_sid And bh_sid = @bh_sid;";

						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.Clear();
						Sql_Command.Parameters.AddWithValue("bh_sid", lb_bh_sid.Text);
						Sql_Command.Parameters.AddWithValue("bi_sid", stmp[icnt]);
						Sql_Command.Parameters.AddWithValue("bb_ip", Request.ServerVariables["REMOTE_ADDR"]);

						Sql_Command.ExecuteNonQuery();
					}
					Sql_Conn.Close();

					Sql_Conn.Open();
					SqlString = "Update Bt_Head Set bh_acnt = bh_acnt + 1, bh_time = getdate()";
					SqlString += ",bh_total = (Select Count(*) From Bt_Ballot Where bh_sid = @bh_sid)";
					SqlString += " Where bh_sid = @bh_sid;";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("bh_sid", lb_bh_sid.Text);
					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();
				}
			}
			#endregion
		}

		if (mErr == "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "location.reload(\"A0011.aspx?sid=" + lb_bh_sid.Text + "\");", true);
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}
}
