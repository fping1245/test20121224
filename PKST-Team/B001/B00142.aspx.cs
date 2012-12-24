//---------------------------------------------------------------------------- 
//程式功能	考試題庫管理 > 試題處理 > 修改試題
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _B00142 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int tp_sid = -1, tq_sid = -1;
			string mErr = "";

			// 檢查使用者權限並存入登入紀錄
			//Check_Power("B001", false);

			if (Request["sid"] != null && Request["tp_sid"] != null && Request["tp_title"] != null)
			{
				if (int.TryParse(Request["tp_sid"], out tp_sid) && int.TryParse(Request["sid"], out tq_sid))
				{
					lb_tq_sid.Text = tq_sid.ToString();
					lb_tp_sid.Text = tp_sid.ToString();
					lb_tp_title.Text = Request["tp_title"].Trim();

					// 取得題目資料
					if (!GetData())
						mErr = "找不到指定的資料!\\n";
				}
				else
					mErr = "參數格式錯誤!\\n";
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

	// 取得題目資料
	private bool GetData()
	{
		bool ckbool = false;
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select Top 1 tq_sort, tq_desc, tq_type, tq_score, init_time From Ts_Question";
			SqlString += " Where tq_sid = @tq_sid And tp_sid = @tp_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();

				Sql_Command.Parameters.AddWithValue("tq_sid", lb_tq_sid.Text);
				Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						tb_tq_sort.Text = Sql_Reader["tq_sort"].ToString();
						tb_tq_desc.Text = Sql_Reader["tq_desc"].ToString().Trim();
						tb_tq_score.Text = Sql_Reader["tq_score"].ToString();
						tb_tq_type.Text = Sql_Reader["tq_type"].ToString();
						lb_init_time.Text = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

						if (tb_tq_type.Text == "0")
						{
							rb_tq_type0.Checked = true;
							rb_tq_type1.Checked = false;
							tb_tq_type.Text = "2";
							tb_tq_type.Visible = false;
							lt_tq_type.Visible = false;
							lt_tq_type_desc.Visible = false;
						}
						else
						{
							rb_tq_type0.Checked = false;
							rb_tq_type1.Checked = true;
							tb_tq_type.Visible = true;
							lt_tq_type.Visible = true;
							lt_tq_type_desc.Visible = true;
						}

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

	// 存檔
	protected void lk_save_Click(object sender, EventArgs e)
	{
		string SqlString = "", mErr = "";
		int tq_type = 0, tq_sort = 0, tq_score = 0;

		if (rb_tq_type0.Checked)
			tq_type = 0;
		else
		{
			if (!int.TryParse(tb_tq_type.Text, out tq_type))
				tq_type = 1;
		}

		if (!int.TryParse(tb_tq_sort.Text, out tq_sort))
		{
			mErr += "「題號」請輸入數字!\\n";
		}

		if (!int.TryParse(tb_tq_score.Text, out tq_score))
		{
			mErr += "「試題分數」請輸入數字!\\n";
		}

		tb_tq_desc.Text = tb_tq_desc.Text.Trim();
		if (tb_tq_desc.Text.Length < 1)
		{
			mErr += "請正確輸入「試卷文字」!\\n";
		}

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					// 修改題目
					SqlString = "Update Ts_Question Set tq_sort = @tq_sort, tq_desc = @tq_desc, tq_type = @tq_type";
					SqlString += ", tq_score = @tq_score, init_time = getdate() Where tq_sid = @tq_sid And tp_sid = @tp_sid";

					Sql_Conn.Open();

					Sql_Command.Connection = Sql_Conn;
					Sql_Command.CommandText = SqlString;

					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
					Sql_Command.Parameters.AddWithValue("tq_sid", lb_tq_sid.Text);
					Sql_Command.Parameters.AddWithValue("tq_sort", tq_sort);
					Sql_Command.Parameters.AddWithValue("tq_score", tq_score);
					Sql_Command.Parameters.AddWithValue("tq_type", tq_type);
					Sql_Command.Parameters.AddWithValue("tq_desc", tb_tq_desc.Text);

					Sql_Command.ExecuteNonQuery();

					// 重新編排題號，並更新試題總數及總分
					SqlString = "Execute dbo.p_Ts_Question_ReSort @tp_sid;";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);

					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();

					mErr = "alert(\"「試卷題目」修改完成!\\n\");parent.location.reload(true);";
				}
			}

			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", mErr, true);
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	protected void rb_tq_type0_CheckedChanged(object sender, EventArgs e)
	{
		lt_tq_type.Visible = false;
		lt_tq_type_desc.Visible = false;
		tb_tq_type.Visible = false;
	}

	protected void rb_tq_type1_CheckedChanged(object sender, EventArgs e)
	{

		lt_tq_type.Visible = true;
		lt_tq_type_desc.Visible = true;
		tb_tq_type.Visible = true;
	}
}
