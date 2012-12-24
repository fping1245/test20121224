//---------------------------------------------------------------------------- 
//程式功能	留言板管理
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _C002 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			// 檢查使用者權限並存入登入紀錄
			//Check_Power("C002", true);

			ods_Ms_Board.SelectParameters["is_close"].DefaultValue = "";
		}

		ods_Ms_Board.DataBind();
		lv_Ms_Board.DataBind();

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

		DateTime ckbtime, cketime;
		string tmpstr = "";

		// 有輸入 mb_desc，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_mb_desc.Text.Trim());
		if (tmpstr != "")
			ods_Ms_Board.SelectParameters["mb_desc"].DefaultValue = tmpstr;
		else
		{
			tb_mb_desc.Text = "";
			ods_Ms_Board.SelectParameters["mb_desc"].DefaultValue = "";
		}

		// 有輸入 mb_name，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_mb_name.Text.Trim());
		if (tmpstr != "")
			ods_Ms_Board.SelectParameters["mb_name"].DefaultValue = tmpstr;
		else
		{
			tb_mb_name.Text = "";
			ods_Ms_Board.SelectParameters["mb_name"].DefaultValue = "";
		}

		// 有輸入 mb_email，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
		tmpstr = cfc.CleanSQL(tb_mb_email.Text.Trim());
		if (tmpstr != "")
			ods_Ms_Board.SelectParameters["mb_email"].DefaultValue = tmpstr;
		else
		{
			tb_mb_email.Text = "";
			ods_Ms_Board.SelectParameters["mb_email"].DefaultValue = "";
		}

		// 有輸入 btime 範圍，則設定條件
		if (DateTime.TryParse(tb_btime.Text.Trim(), out ckbtime))
			ods_Ms_Board.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_btime.Text = "";
			ods_Ms_Board.SelectParameters["btime"].DefaultValue = "";
		}

		// 有輸入 etime 範圍，則設定條件
		if (DateTime.TryParse(tb_etime.Text.Trim(), out cketime))
			ods_Ms_Board.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
		else
		{
			tb_etime.Text = "";
			ods_Ms_Board.SelectParameters["etime"].DefaultValue = "";
		}

		lv_Ms_Board.DataBind();
	}

	// 更新顯示的資料格式
	protected void lv_Ms_Board_ItemDataBound(object sender, ListViewItemEventArgs e)
	{
		Image img_tmp;

		if (e.Item.ItemType == ListViewItemType.DataItem)
		{
			ListViewDataItem LVDI = (ListViewDataItem)e.Item;

			DbDataRecord DDR = (DbDataRecord)LVDI.DataItem;

			#region 性別處理
			string mb_sex = DDR["mb_sex"].ToString();

			img_tmp = (Image)LVDI.FindControl("img_mb_sex");

			if (img_tmp != null)
			{
				switch (mb_sex)
				{
					case "1":
						img_tmp.ImageUrl = "../images/symbol/man.gif";
						img_tmp.ToolTip = "男性";
						img_tmp.AlternateText = "男性";
						break;
					case "2":
						img_tmp.ImageUrl = "../images/symbol/woman.gif";
						img_tmp.ToolTip = "女性";
						img_tmp.AlternateText = "女性";
						break;
					default:
						img_tmp.ImageUrl = "../images/symbol/unknow.gif";
						img_tmp.ToolTip = "未填性別";
						img_tmp.AlternateText = "未填性別";
						break;
				}
			}
			#endregion

			#region 心情符號處理
			string mb_symbol = DDR["mb_symbol"].ToString();
			img_tmp = (Image)LVDI.FindControl("img_mb_symbol");

			Common_Func.ImageSymbol img_symbo = new Common_Func.ImageSymbol();

			img_symbo.code = int.Parse(mb_symbol);
			img_tmp.ImageUrl = img_symbo.image;
			img_tmp.ToolTip = img_symbo.name;
			img_tmp.AlternateText = img_symbo.name;
			#endregion

			#region 隱藏內容
			Label is_show = (Label)LVDI.FindControl("lb_is_show");
			if (DDR["is_show"].ToString() == "0")
				is_show.Text = "<font color=blue><b>隱藏<b></font>";
			else
				is_show.Text = "顯示";
			#endregion

			#region 開放內容
			Label is_close = (Label)LVDI.FindControl("lb_is_close");
			if (DDR["is_close"].ToString() == "0")
				is_close.Text = "<font color=blue><b>關閉<b></font>";
			else
				is_close.Text = "開放";
			#endregion


			#region 內容隱藏處理 (隱藏內容但開放留言的狀況，使用替代文字)
			if (DDR["is_show"].ToString() == "0" && DDR["is_close"].ToString() == "1")
			{
				Label mb_desc = (Label)LVDI.FindControl("lb_mb_desc");

				mb_desc.Text = "<font color=red><b>××× 隱藏  ××× " + DDR["instead"].ToString() + "</b></font>";
			}
			#endregion

			#region 內容關閉處理
			if (DDR["is_close"].ToString() == "0")
			{
				Label mb_desc = (Label)LVDI.FindControl("lb_mb_desc");

				mb_desc.Text = "<p style=\"background-color:#F09AF4;; margin:0pt 0pt 0pt 0pt\"><b>××× 關閉 ×××</b><br>" + mb_desc.Text + "</p>";
			}
			#endregion
		}
	}
}
