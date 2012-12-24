//---------------------------------------------------------------------------- 
//程式功能	考試題庫管理 > 試題處理 > 新增答案項目
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _B00144 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int tp_sid = -1, tq_sid = -1, tq_sort = -1;
			string mErr = "";

			// 檢查使用者權限並存入登入紀錄
			//Check_Power("B001", false);

			if (Request["tp_sid"] != null && Request["sid"] != null && Request["tq_sort"] != null && Request["tq_desc"] != null)
			{
				if (int.TryParse(Request["tp_sid"], out tp_sid) && int.TryParse(Request["sid"], out tq_sid)
					&& int.TryParse(Request["tq_sort"], out tq_sort))
				{
					lb_tp_sid.Text = tp_sid.ToString();
					lb_tq_sid.Text = tq_sid.ToString();
					lb_tq_sort.Text = tq_sort.ToString() + ".";
					lb_tq_desc.Text = Request["tq_desc"].ToString().Trim();

					// 取得下一個選項的編號
					tb_ti_sort.Text = GetNextSort();
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

	// 取得下一個選項的編號
	private string GetNextSort()
	{
		int ti_sort = 1;
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select IsNull(Max(ti_sort) + 1, 1) as ti_sort From Ts_Item Where tp_sid = @tp_sid And tq_sid = @tq_sid";
			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();

				Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
				Sql_Command.Parameters.AddWithValue("tq_sid", lb_tq_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
						ti_sort = int.Parse(Sql_Reader["ti_sort"].ToString());
					else
						ti_sort = 1;

					Sql_Reader.Close();
				}

				Sql_Conn.Close();
			}
		}
		return ti_sort.ToString();
	}

	// 存檔後即結束
	protected void lk_save_Click(object sender, EventArgs e)
	{
		string mErr = "";

		mErr = DataSave();

		if (mErr == "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"完成存檔!\\n\");parent.location.reload(true);", true);
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	// 存檔並進行下一步
	protected void lk_save_again_Click(object sender, EventArgs e)
	{
		string mErr = "";

		mErr = DataSave();

		if (mErr == "")
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"完成存檔!請繼續輸入...\\n\");parent.add_item(" + lb_tq_sid.Text
				+ "," + lb_tq_sort.Text + ",\"" + Server.UrlEncode(lb_tq_desc.Text) + "\"" + ");", true);
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");", true);
	}

	// 資料存檔
	private string DataSave()
	{
		string SqlString = "", mErr = "", ti_sid = "";
		int ti_correct = 0, ti_sort = 0;

		if (rb_ti_correct0.Checked)
			ti_correct = 0;
		else
			ti_correct = 1;

		if (int.TryParse(tb_ti_sort.Text, out ti_sort))
		{
			if (ti_sort < 1 || ti_sort > 100)
				mErr += "「順序」請輸入 1 ~ 100 的數字!\\n";
		}
		else
		{
			mErr += "「順序」請輸入 1 ~ 100 的數字!\\n";
		}

		tb_ti_desc.Text = tb_ti_desc.Text.Trim();
		if (tb_ti_desc.Text.Length < 1)
		{
			mErr += "請正確輸入「選項文字」!\\n";
		}

		if (mErr == "")
		{
			using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
			{
				using (SqlCommand Sql_Command = new SqlCommand())
				{
					// 新增選項
					SqlString = "Insert Into Ts_Item (tp_sid, tq_sid, ti_sort, ti_desc, ti_correct)";
					SqlString += " Values (@tp_sid, @tq_sid, @ti_sort, @ti_desc, @ti_correct);";
					SqlString += "Select @ti_sid = Scope_Identity();";

					Sql_Conn.Open();

					Sql_Command.Connection = Sql_Conn;
					Sql_Command.CommandText = SqlString;

					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
					Sql_Command.Parameters.AddWithValue("tq_sid", lb_tq_sid.Text);
					Sql_Command.Parameters.AddWithValue("ti_sort", ti_sort);
					Sql_Command.Parameters.AddWithValue("ti_correct", ti_correct);
					Sql_Command.Parameters.AddWithValue("ti_desc", tb_ti_desc.Text);

					SqlParameter spt_ti_sid = Sql_Command.Parameters.Add("ti_sid", SqlDbType.Int);
					spt_ti_sid.Direction = ParameterDirection.Output;

					Sql_Command.ExecuteNonQuery();

					ti_sid = spt_ti_sid.Value.ToString();

					// 重新排序並更新答案選項總數
					SqlString = "Execute dbo.p_Ts_Item_ReSort @tp_sid, @tq_sid";

					Sql_Command.CommandText = SqlString;
					Sql_Command.Parameters.Clear();
					Sql_Command.Parameters.AddWithValue("tp_sid", lb_tp_sid.Text);
					Sql_Command.Parameters.AddWithValue("tq_sid", lb_tq_sid.Text);

					Sql_Command.ExecuteNonQuery();

					Sql_Conn.Close();
				}
			}
		}
		return mErr;
	}
}
