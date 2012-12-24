//---------------------------------------------------------------------------- 
//程式功能	權限設定管理 > 資料表清單 > 欄位清單
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _G00144 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string mErr = "";

		if (!IsPostBack)
		{
			int ds_sid = -1, dt_sid = -1;
			Common_Func cfc = new Common_Func();

			// 檢查使用者權限不存入登入紀錄
			//Check_Power("G001", false);

			if (Request["dt_sid"] != null && Request["ds_sid"] != null)
			{
				if (int.TryParse(Request["dt_sid"], out dt_sid) && int.TryParse(Request["ds_sid"], out ds_sid))
				{
					lb_ds_sid.Text = ds_sid.ToString();
					lb_dt_sid.Text = dt_sid.ToString();

					ods_Db_Record.SelectParameters["ds_sid"].DefaultValue = ds_sid.ToString();
					ods_Db_Record.SelectParameters["dt_sid"].DefaultValue = dt_sid.ToString();

					if (GetData())
					{
						#region 接受上一頁查詢條件
						lb_page.Text = "?ds_sid=" + ds_sid.ToString();
						if (Request["pageid"] == null)
							lb_page.Text += "&pageid=0";
						else
							lb_page.Text += "&pageid=" + Request["pageid"];

						if (Request["ds_code"] != null)
							lb_page.Text += "&ds_code=" + Server.UrlEncode(Request["ds_code"]);

						if (Request["ds_name"] != null)
							lb_page.Text += "&ds_name=" + Server.UrlEncode(Request["ds_name"]);

						if (Request["ds_database"] != null)
							lb_page.Text += "&ds_database=" + Server.UrlEncode(Request["ds_database"]);

						if (Request["sort"] != null)
							lb_page.Text += "&sort=" + Server.UrlEncode(Request["sort"]);

						if (Request["pageid1"] != null)
						{
							lb_page.Text += "&pageid1=" + Request["pageid1"];
						}

						if (Request["dt_name"] != null)
						{
							lb_page.Text += "&dt_name=" + Server.UrlEncode(Request["dt_name"]);
						}

						if (Request["dt_caption"] != null)
						{
							lb_page.Text += "&dt_caption=" + Server.UrlEncode(Request["dt_caption"]);
						}

						if (Request["dt_area"] != null)
						{
							lb_page.Text += "&dt_area=" + Server.UrlEncode(Request["dt_area"]);
						}

						if (Request["sort1"] != null)
						{
							lb_page.Text += "&sort1=" + Server.UrlEncode(Request["sort1"]);
						}
						#endregion
					}
				}
				else
					mErr = "參數格式錯誤!\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";
		}

		if (mErr == "")
		{
			#region 檢查頁數是否超過
			ods_Db_Record.DataBind();
			gv_Db_Record.DataBind();
			if (gv_Db_Record.PageCount < gv_Db_Record.PageIndex)
			{
				gv_Db_Record.PageIndex = gv_Db_Record.PageCount;
				gv_Db_Record.DataBind();
			}

			lb_pageid2.Text = gv_Db_Record.PageIndex.ToString();
			#endregion
		}
		else
			ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", "alert(\"" + mErr + "\");history.go(-1);", true);
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

	// 取得標題資料
	private bool GetData()
	{
		bool ckbool = false;
		string SqlString = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				SqlString = "Select Top 1 dt_sort, dt_name, dt_caption, dt_area, dt_desc, dt_index, dt_modi, init_time";
				SqlString += " From Db_Table Where dt_sid = @dt_sid And ds_sid = @ds_sid";

				Sql_Conn.Open();
				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;

				Sql_Command.Parameters.AddWithValue("ds_sid", lb_ds_sid.Text);
				Sql_Command.Parameters.AddWithValue("dt_sid", lb_dt_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						lb_dt_sort.Text = (int.Parse(Sql_Reader["dt_sort"].ToString()) / 10).ToString();
						lb_dt_name.Text = Sql_Reader["dt_name"].ToString().Trim();
						lb_dt_caption.Text = Sql_Reader["dt_caption"].ToString().Trim();
						lb_dt_area.Text = Sql_Reader["dt_area"].ToString().Trim();
						lb_dt_desc.Text = Sql_Reader["dt_desc"].ToString().Trim();
						lb_dt_index.Text = Sql_Reader["dt_index"].ToString().Trim().Replace("\r\n","<br>");
						lb_dt_modi.Text = Sql_Reader["dt_modi"].ToString().Trim();
						lb_init_time.Text = DateTime.Parse(Sql_Reader["init_time"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

						ckbool = true;
					}

					Sql_Reader.Close();
				}

				Sql_Conn.Close();
			}
		}

		return ckbool;
	}

	protected void gv_Db_Record_PageIndexChanged(object sender, EventArgs e)
	{
		lb_pageid2.Text = gv_Db_Record.PageIndex.ToString();
	}

	// 換頁按鍵處理
	protected void gv_Db_Record_DataBound(object sender, EventArgs e)
	{
		int icnt = 0, acnt = 0, ecnt = 0;

		acnt = gv_Db_Record.PageIndex / 10 * 10;
		if (acnt < 0)
			acnt = 0;

		ecnt = acnt + 10;
		if (ecnt >= gv_Db_Record.PageCount)
			ecnt = gv_Db_Record.PageCount;

		#region 選項頁面
		mu_page.Items.Clear();

		MenuItem mi_tmp1 = new MenuItem();

		mi_tmp1.Value = "X";
		mi_tmp1.Text = "| ";
		mi_tmp1.Enabled = false;

		mu_page.Items.Add(mi_tmp1);

		if (acnt > 0)
		{
			MenuItem mi_tmp3 = new MenuItem();
			mi_tmp3.Value = (acnt - 1).ToString();
			mi_tmp3.Text = "<<&nbsp;";

			mu_page.Items.Add(mi_tmp3);
		}

		for (icnt = acnt; icnt < ecnt; icnt++)
		{
			MenuItem mi_page = new MenuItem();

			mi_page.Value = icnt.ToString();
			if (gv_Db_Record.PageIndex == icnt)
			{
				mi_page.Text = string.Format("[{0}]", icnt + 1);
				mi_page.Selected = true;
			}
			else
				mi_page.Text = string.Format("&nbsp;{0}&nbsp;", icnt + 1);

			mu_page.Items.Add(mi_page);
		}

		if (ecnt < gv_Db_Record.PageCount)
		{
			MenuItem mi_tmp4 = new MenuItem();
			mi_tmp4.Value = (ecnt).ToString();
			mi_tmp4.Text = "&nbsp;>>";

			mu_page.Items.Add(mi_tmp4);
		}

		MenuItem mi_tmp2 = new MenuItem();

		mi_tmp2.Value = "X";
		mi_tmp2.Text = " |";
		mi_tmp2.Enabled = false;

		mu_page.Items.Add(mi_tmp2);

		#endregion

		#region 換頁按鍵處理
		if (gv_Db_Record.PageIndex == 0)
		{
			ib_first.Enabled = false;
			ib_prev.Enabled = false;
			ib_first.ToolTip = "已經是在第一頁了";
			ib_prev.ToolTip = "已經是在第一頁了";
		}
		else
		{
			ib_first.Enabled = true;
			ib_prev.Enabled = true;
			ib_first.ToolTip = "第一頁";
			ib_prev.ToolTip = "下一頁";
		}
		if (gv_Db_Record.PageIndex + 1 >= gv_Db_Record.PageCount)
		{
			ib_next.Enabled = false;
			ib_last.Enabled = false;
			ib_next.ToolTip = "已經是最後一頁了";
			ib_last.ToolTip = "已經是最後一頁了";
		}
		else
		{
			ib_next.Enabled = true;
			ib_last.Enabled = true;
			ib_next.ToolTip = "下一頁";
			ib_last.ToolTip = "最末頁";
		}
		#endregion
	}

	// 回到首頁
	protected void ib_first_Click(object sender, ImageClickEventArgs e)
	{
		gv_Db_Record.PageIndex = 0;
		gv_Db_Record.DataBind();
	}

	// 上一頁
	protected void ib_prev_Click(object sender, ImageClickEventArgs e)
	{
		if (gv_Db_Record.PageIndex > 0)
			gv_Db_Record.PageIndex = gv_Db_Record.PageIndex - 1;
		gv_Db_Record.DataBind();
	}

	// 下一頁
	protected void ib_next_Click(object sender, ImageClickEventArgs e)
	{
		if (gv_Db_Record.PageIndex + 1 < gv_Db_Record.PageCount)
			gv_Db_Record.PageIndex = gv_Db_Record.PageIndex + 1;
		gv_Db_Record.DataBind();
	}

	// 最末頁
	protected void ib_last_Click(object sender, ImageClickEventArgs e)
	{
		gv_Db_Record.PageIndex = gv_Db_Record.PageCount - 1;
		gv_Db_Record.DataBind();
	}

	// 選頁處理
	protected void mu_page_MenuItemClick(object sender, MenuEventArgs e)
	{
		int ckint = 0;
		if (int.TryParse(e.Item.Value, out ckint))
		{
			gv_Db_Record.PageIndex = ckint;
		}
	}

	// 資料顯示修改
	protected void gv_Db_Record_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if ((e.Row.RowType == DataControlRowType.DataRow))
		{
			int dr_sort = int.Parse(DataBinder.Eval(e.Row.DataItem, "dr_sort").ToString());

			e.Row.Cells[0].Text = (dr_sort / 10).ToString();
		}
	}
}
