//---------------------------------------------------------------------------- 
//程式功能	HTML編輯器 (網頁清單) > 編輯頁面 > 圖檔列表
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _800111 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			int he_sid = 0;

			// 檢查使用者權限但不存入登入紀錄
			//Check_Power("8001", false);

			if (Request["sid"] != null)
			{
				if (int.TryParse(Request["sid"], out he_sid))
				{
					lb_he_sid.Text = he_sid.ToString();
				}
			}
		}

		// 建立顯示資料
		if (lb_he_sid.Text != "0")
			Build_List();
		else
			lt_image.Text = "<tr style=\"height:100px\"><td>沒有任何圖檔</td></tr>";
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

	// 建立顯示資料
	private void Build_List()
	{
		string SqlString = "", hf_name = "", hf_sid = "";

		using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
		{
			SqlString = "Select hf_sid, hf_name, hf_size From Html_Files Where he_sid = @he_sid";

			using (SqlCommand Sql_Command = new SqlCommand(SqlString, Sql_Conn))
			{
				Sql_Conn.Open();
				Sql_Command.Parameters.AddWithValue("he_sid", lb_he_sid.Text);

				using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
				{
					if (Sql_Reader.Read())
					{
						lt_image.Text += "<tr style=\"height:100px; text-align:center\" valign=\"top\">";
						do
						{
							hf_sid = Sql_Reader["hf_sid"].ToString();
							hf_name = Sql_Reader["hf_name"].ToString().Trim();

							lt_image.Text += "<td><p style=\"margin:0px 0px 5px 0px\"><a href=\"javascript:mdel(" + hf_sid + ",'" + hf_name + "');";
							lt_image.Text += "\" class=\"abtn\" style=\"font-size:9pt\">&nbsp;刪除&nbsp;</a></p>";
							lt_image.Text += "<img  src=\"8001111.ashx?sid=" + hf_sid + "\" onload=\"img_resize(this)\" alt=\"";
							lt_image.Text += hf_name + "\" title=\"" + hf_name + "\n" + int.Parse(Sql_Reader["hf_size"].ToString()).ToString("N0");
							lt_image.Text += " bytes\"></td>\n";

						} while (Sql_Reader.Read());
						lt_image.Text += "</tr>";
					}

					Sql_Reader.Close();
				}
				Sql_Conn.Close();
			}
		}

		if (lt_image.Text == "")
			lt_image.Text = "<tr style=\"height:100px\"><td>沒有任何圖檔</td></tr>";
	}
}
