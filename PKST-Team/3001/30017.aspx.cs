//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 播幻燈片
//備註說明	使用資料庫儲存圖檔
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class _30017 : System.Web.UI.Page
{
	public int show_effect = 0, ac_width = 870, ac_height = 600, rownum = 1, maxrow = 0;
	public string al_sid = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
		string mErr = "", ac_sid = "0", ac_pic = "", ac_desc = "";
		int ckint = -1;

		if (!IsPostBack)
		{
			// 檢查使用者權限，但不存登入紀錄
			//Check_Power("3001", false);

			// 上下一筆時處理用的指標
			if (Request["rownum"] != null)
			{
				if (int.TryParse(Request["rownum"], out ckint))
					rownum = ckint;
				else
					rownum = 1;
			}
			else
				rownum = 1;

			// 顯示效果
			if (Request["effect"] != null)
				if (int.TryParse(Request["effect"], out ckint))
					show_effect = ckint;
				else
					show_effect = 0;
			else
				show_effect = 0;

			if (Request["al_sid"] != null)
			{
				if (int.TryParse(Request["al_sid"], out ckint))
					al_sid = ckint.ToString();
				else
					mErr = "參數傳送錯誤!\\n";
			}
			else
				mErr = "參數傳送錯誤!\\n";

			if (mErr == "") {
				#region 處理圖形資料
				using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
				{
					Sql_Conn.Open();

					using (SqlCommand Sql_Command = new SqlCommand())
					{
						string SqlString = "";
						SqlDataReader Sql_Reader;

						Sql_Command.Connection = Sql_Conn;

						#region 取得筆數
						SqlString = "Select Count(*) as Cnt From Al_Content Where al_sid = @al_sid";
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("al_sid", al_sid);

						Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
							maxrow = int.Parse(Sql_Reader["Cnt"].ToString());
						else
							maxrow = 0;

						Sql_Reader.Close();
						Sql_Reader.Dispose();
						#endregion

						#region 取得相片資料
						if (rownum > maxrow)
							rownum = 1;

						SqlString = "Select Top 1 * From (Select ac_sid, ac_width, ac_height, ac_desc";
						SqlString = SqlString + ", Row_Number() Over (Order by ac_name) as rownum From Al_Content";
						SqlString = SqlString + " Where al_sid = @al_sid) as mTable";
						SqlString = SqlString + " Where rownum = @rownum";

						Sql_Command.Parameters.Clear();
						Sql_Command.CommandText = SqlString;
						Sql_Command.Parameters.AddWithValue("al_sid", al_sid);
						Sql_Command.Parameters.AddWithValue("rownum", rownum);

						Sql_Reader = Sql_Command.ExecuteReader();

						if (Sql_Reader.Read())
						{
							ac_sid = Sql_Reader["ac_sid"].ToString();
							ac_pic = "3001621.ashx?ac_sid=" + ac_sid;
							ac_width = int.Parse(Sql_Reader["ac_width"].ToString());
							ac_height = int.Parse(Sql_Reader["ac_height"].ToString());

							ac_desc = Sql_Reader["ac_desc"].ToString().Trim();

							img_show.Height = (int)ac_height;
							img_show.Width = (int)ac_width;
							img_show.ToolTip = ac_desc;
						}
						else
						{
							ac_pic = "../images/blank.gif";
							mErr = "找不到這個圖形的資料!\\n";
						}

						Sql_Reader.Close();
						Sql_Reader.Dispose();
						#endregion
					}
				}
				#endregion
			}

			img_show.ImageUrl = ac_pic;

			if (mErr != "")
				lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");window.close();</script>";
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
}
