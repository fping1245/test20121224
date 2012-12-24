//---------------------------------------------------------------------------- 
//程式功能	資料庫規格管理 > 資料表清單
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _G0014 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		string mErr = "";

		if (!IsPostBack)
		{
			int ds_sid = -1;

			// 檢查使用者權限不存入登入紀錄
			//Check_Power("G001", false);

			if (Request["ds_sid"] != null)
			{
				if (int.TryParse(Request["ds_sid"], out ds_sid))
				{
					int ckint = 0;
					string tmpstr = "";
					Common_Func cfc = new Common_Func();

					lb_ds_sid.Text = ds_sid.ToString();

					if (GetData())
					{
						ods_Db_Table.SelectParameters["ds_sid"].DefaultValue = ds_sid.ToString();

						#region 接受下一頁返回時的舊查詢條件
						if (Request["dt_name"] != null)
						{
							tb_dt_name.Text = cfc.CleanSQL(Request["dt_name"]);
							ods_Db_Table.SelectParameters["dt_name"].DefaultValue = tb_dt_name.Text;
						}

						if (Request["dt_caption"] != null)
						{
							tb_dt_caption.Text = cfc.CleanSQL(Request["dt_caption"]);
							ods_Db_Table.SelectParameters["dt_caption"].DefaultValue = tb_dt_caption.Text;
						}

						if (Request["dt_area"] != null)
						{
							tb_dt_area.Text = cfc.CleanSQL(Request["dt_area"]);
							ods_Db_Table.SelectParameters["dt_area"].DefaultValue = tb_dt_area.Text;
						}

						if (Request["sort1"] != null)
						{
							tmpstr = cfc.CleanSQL(Request["sort1"]);

							switch (tmpstr)
							{
								case "dt_sort":
									lk_st_dt_sort.CssClass = "";
									lk_st_dt_name.CssClass = "abtn";
									lk_st_dt_caption.CssClass = "abtn";
									break;
								case "dt_name":
									lk_st_dt_sort.CssClass = "abtn";
									lk_st_dt_name.CssClass = "";
									lk_st_dt_caption.CssClass = "abtn";
									break;
								case "dt_caption":
									lk_st_dt_sort.CssClass = "abtn";
									lk_st_dt_name.CssClass = "abtn";
									lk_st_dt_caption.CssClass = "";
									break;
								default:
									tmpstr = "dt_sort";
									lk_st_dt_sort.CssClass = "";
									lk_st_dt_name.CssClass = "abtn";
									lk_st_dt_caption.CssClass = "abtn";
									break;
							}

							gv_Db_Table.Sort(tmpstr, SortDirection.Ascending);
						}

						if (Request["pageid1"] != null)
						{
							if (int.TryParse(Request["pageid1"], out ckint))
							{
								gv_Db_Table.PageIndex = ckint;
								lb_pageid1.Text = ckint.ToString();
							}
							else
								lb_pageid1.Text = "0";
						}
						#endregion
					}
					else
						mErr = "找不到指定的資料！\\n";

					#region 接受上一頁的資料參數
					if (Request["pageid"] == null)
						lb_page.Text = "?pageid=0";
					else
						lb_page.Text = "?pageid=" + Request["pageid"];

					if (Request["ds_code"] != null)
						lb_page.Text += "&ds_code=" + Server.UrlEncode(Request["ds_code"]);

					if (Request["ds_name"] != null)
						lb_page.Text += "&ds_name=" + Server.UrlEncode(Request["ds_name"]);

					if (Request["ds_database"] != null)
						lb_page.Text += "&ds_database=" + Server.UrlEncode(Request["ds_database"]);

					if (Request["sort"] != null)
						lb_page.Text += "&sort=" + Server.UrlEncode(Request["sort"]);
					#endregion
				}
				else
					mErr = "參數格式錯誤!\\n";
			}
			else
				mErr = "參數傳遞錯誤!\\n";
		}

		if (mErr == "")
		{
			#region 檢查頁數是否超過
			ods_Db_Table.DataBind();
			gv_Db_Table.DataBind();
			if (gv_Db_Table.PageCount < gv_Db_Table.PageIndex)
			{
				gv_Db_Table.PageIndex = gv_Db_Table.PageCount;
				gv_Db_Table.DataBind();
			}

			lb_pageid1.Text = gv_Db_Table.PageIndex.ToString();
			#endregion
		}
		
		if (mErr != "")
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

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			using (SqlCommand Sql_Command = new SqlCommand())
			{
				string SqlString = "Select Top 1 ds_code, ds_name, ds_database, ds_id, ds_pass, ds_desc From Db_Sys Where ds_sid = @ds_sid";

				Sql_Conn.Open();
				Sql_Command.Connection = Sql_Conn;
				Sql_Command.CommandText = SqlString;

				Sql_Command.Parameters.AddWithValue("ds_sid", lb_ds_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						lb_ds_code.Text = Sql_Reader["ds_code"].ToString().Trim();
						lb_ds_name.Text = Sql_Reader["ds_name"].ToString().Trim();
						lb_ds_database.Text = Sql_Reader["ds_database"].ToString().Trim();
						lb_ds_id.Text = Sql_Reader["ds_id"].ToString().Trim();
						lb_ds_pass.Text = Sql_Reader["ds_pass"].ToString().Trim();
						lb_ds_desc.Text = Sql_Reader["ds_desc"].ToString().Trim();

						ckbool = true;
					}

					Sql_Reader.Close();
				}

				Sql_Conn.Close();
			}
		}

		return ckbool;
	}

	// 條件設定
	protected void btn_Set_Click(object sender, EventArgs e)
	{
		// 檢查查詢條件是否改變
		Chk_Filter();
	}

	// 檢查查詢條件是否改變
	private void Chk_Filter()
	{
		Common_Func cfc = new Common_Func();

		string tmpstr = "";

		// 有輸入 dt_name，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_dt_name.Text.Trim());
		if (tmpstr != "")
			ods_Db_Table.SelectParameters["dt_name"].DefaultValue = tmpstr;
		else
		{
			tb_dt_name.Text = "";
			ods_Db_Table.SelectParameters["dt_name"].DefaultValue = "";
		}

		// 有輸入 dt_caption，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_dt_caption.Text.Trim());
		if (tmpstr != "")
			ods_Db_Table.SelectParameters["dt_caption"].DefaultValue = tmpstr;
		else
		{
			tb_dt_caption.Text = "";
			ods_Db_Table.SelectParameters["dt_caption"].DefaultValue = "";
		}
	
		// 有輸入 dt_area，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_dt_area.Text.Trim());
		if (tmpstr != "")
			ods_Db_Table.SelectParameters["dt_area"].DefaultValue = tmpstr;
		else
		{
			tb_dt_area.Text = "";
			ods_Db_Table.SelectParameters["dt_area"].DefaultValue = "";
		}

		gv_Db_Table.DataBind();
		if (gv_Db_Table.PageCount - 1 < gv_Db_Table.PageIndex)
		{
			gv_Db_Table.PageIndex = gv_Db_Table.PageCount;
			gv_Db_Table.DataBind();
		}
	}

	// 換頁處理
	protected void gv_Db_Table_PageIndexChanged(object sender, EventArgs e)
	{
		lb_pageid1.Text = gv_Db_Table.PageIndex.ToString();
	}

	// 依照顯示順序排序
	protected void lk_st_dt_sort_Click(object sender, EventArgs e)
	{
		gv_Db_Table.Sort("dt_sort", SortDirection.Ascending);

		lk_st_dt_sort.CssClass = "";
		lk_st_dt_name.CssClass = "abtn";
		lk_st_dt_caption.CssClass = "abtn";
	}

	// 依照表格名稱排序
	protected void lk_st_dt_name_Click(object sender, EventArgs e)
	{
		gv_Db_Table.Sort("dt_name", SortDirection.Ascending);

		lk_st_dt_sort.CssClass = "abtn";
		lk_st_dt_name.CssClass = "";
		lk_st_dt_caption.CssClass = "abtn";
	}

	// 依照中文標題排序
	protected void lk_st_dt_caption_Click(object sender, EventArgs e)
	{
		gv_Db_Table.Sort("dt_caption", SortDirection.Ascending);
		gv_Db_Table.DataBind();

		lk_st_dt_sort.CssClass = "abtn";
		lk_st_dt_name.CssClass = "abtn";
		lk_st_dt_caption.CssClass = "";
	}

	// 資料顯示修改
	protected void gv_Db_Table_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if ((e.Row.RowType == DataControlRowType.DataRow))
		{
			int dt_sort = int.Parse(DataBinder.Eval(e.Row.DataItem, "dt_sort").ToString());

			Label lb_tmp = (Label)e.Row.Cells[0].FindControl("lb_dt_sort");
			lb_tmp.Text = (dt_sort / 10).ToString();

			Label lb_index = (Label)e.Row.Cells[0].FindControl("lb_dt_index");
			lb_index.Text = DataBinder.Eval(e.Row.DataItem, "dt_index").ToString().Replace("\r\n", "<br>");

			Label lb_init_time = (Label)e.Row.Cells[0].FindControl("lb_init_time");
			lb_init_time.Text = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "init_time").ToString()).ToString("yyyy/MM/dd HH:mm:ss");
		}
	}

	// 換頁按鍵處理
	protected void gv_Db_Table_DataBound(object sender, EventArgs e)
	{
		int icnt = 0, acnt = 0, ecnt = 0;

		acnt = gv_Db_Table.PageIndex / 10 * 10;
		if (acnt < 0)
			acnt = 0;

		ecnt = acnt + 10;
		if (ecnt >= gv_Db_Table.PageCount)
			ecnt = gv_Db_Table.PageCount;

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
			if (gv_Db_Table.PageIndex == icnt)
			{
				mi_page.Text = string.Format("[{0}]", icnt + 1);
				mi_page.Selected = true;
			}
			else
				mi_page.Text = string.Format("&nbsp;{0}&nbsp;", icnt + 1);

			mu_page.Items.Add(mi_page);
		}

		if (ecnt < gv_Db_Table.PageCount)
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
		if (gv_Db_Table.PageIndex == 0)
		{
			ib_first.Enabled = false;
			ib_prev.Enabled = false;
			ib_first.ToolTip="已經是在第一頁了";
			ib_prev.ToolTip = "已經是在第一頁了";
		}
		else
		{
			ib_first.Enabled = true;
			ib_prev.Enabled = true;
			ib_first.ToolTip = "第一頁";
			ib_prev.ToolTip = "下一頁";
		}
		if (gv_Db_Table.PageIndex + 1 >= gv_Db_Table.PageCount)
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
		gv_Db_Table.PageIndex = 0;
		gv_Db_Table.DataBind();
	}

	// 上一頁
	protected void ib_prev_Click(object sender, ImageClickEventArgs e)
	{
		if (gv_Db_Table.PageIndex > 0)
			gv_Db_Table.PageIndex = gv_Db_Table.PageIndex - 1;
		gv_Db_Table.DataBind();
	}

	// 下一頁
	protected void ib_next_Click(object sender, ImageClickEventArgs e)
	{
		if (gv_Db_Table.PageIndex + 1 < gv_Db_Table.PageCount)
			gv_Db_Table.PageIndex = gv_Db_Table.PageIndex + 1;
		gv_Db_Table.DataBind();
	}

	// 最末頁
	protected void ib_last_Click(object sender, ImageClickEventArgs e)
	{
		gv_Db_Table.PageIndex = gv_Db_Table.PageCount - 1;
		gv_Db_Table.DataBind();
	}

	// 選頁處理
	protected void mu_page_MenuItemClick(object sender, MenuEventArgs e)
	{
		int ckint = 0;
		if (int.TryParse(e.Item.Value, out ckint))
		{
			gv_Db_Table.PageIndex = ckint;
		}
	}
}
