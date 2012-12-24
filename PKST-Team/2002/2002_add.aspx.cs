//---------------------------------------------------------------------------- 
//程式功能	檔案上傳下載 (以資料庫存放檔案) > 上傳檔案
//---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _2002_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int ckint = 0;

        // 檢查使用者權限但不存入登入紀錄
        //Check_Power("2002", false);

        #region 承接上一頁的查詢條件設定

        if (Request["pageid"] != null)
        {
            if (int.TryParse(Request["pageid"].ToString(), out ckint))
                lb_page.Text = "?pageid=" + ckint.ToString();
            else
                lb_page.Text = "?pageid=" + "0";
        }

        if (Request["fc_name"] != null)
            lb_page.Text += "&fc_name=" + Server.UrlEncode(Request["fc_name"]);

        if (Request["fc_ext"] != null)
            lb_page.Text += "&fc_ext=" + Server.UrlEncode(Request["fc_ext"]);

        if (Request["fc_desc"] != null)
            lb_page.Text += "&fc_desc=" + Server.UrlEncode(Request["fc_desc"]);

        #endregion
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

    protected void bn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("2002.aspx" + lb_page.Text);
    }

    protected void bn_upload_Click(object sender, EventArgs e)
    {
        int iCnt = 0, fc_size = 0, jCnt = 0;
        string SqlString = "", mErr = "";
        string fc_name = "", fc_ext = "", fc_desc = "", fc_type = "";

        // 處理上傳檔案，說明及檔案內容存入資料庫
        using (SqlConnection Sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
        {
            Sql_conn.Open();

            using (SqlCommand Sql_Command = new SqlCommand())
            {
                #region 儲存檔案
                for (iCnt = 1; iCnt <= Request.Files.Count; iCnt++)
                {
                    FileUpload fu_file = (FileUpload)Page.FindControl("fu_file" + iCnt.ToString());
                    TextBox tb_file = (TextBox)Page.FindControl("tb_file" + iCnt.ToString());

                    if (fu_file.HasFile)
                    {
                        jCnt = jCnt + 1;

                        fc_name = fu_file.FileName;
                        fc_ext = Path.GetExtension(fc_name).ToString();
                        fc_size = fu_file.PostedFile.ContentLength;
                        fc_type = fu_file.PostedFile.ContentType;
                        fc_desc = tb_file.Text.Trim();

                        #region 檔案存入資料庫
                        SqlString = "Insert Into Fi_Content (fl_no, fc_name, fc_ext, fc_type, fc_size, fc_desc, fc_content)";
                        SqlString = SqlString + " Values (2, @fc_name, @fc_ext, @fc_type, @fc_size, @fc_desc, @fc_content)";
                        Sql_Command.Parameters.Clear();
                        Sql_Command.CommandText = SqlString;
                        Sql_Command.Connection = Sql_conn;
                        Sql_Command.Parameters.AddWithValue("fc_name", fc_name);
                        Sql_Command.Parameters.AddWithValue("fc_ext", fc_ext.ToLower());
                        Sql_Command.Parameters.AddWithValue("fc_type", fc_type);
                        Sql_Command.Parameters.AddWithValue("fc_size", fc_size);
                        Sql_Command.Parameters.AddWithValue("fc_desc", fc_desc);
                        Sql_Command.Parameters.AddWithValue("fc_content", fu_file.FileBytes);
                        //預設上傳檔案大小為 4096KB, 執行時間 120秒, 如要修改，要到 Web.Config 處修改下列數據
                        //<system.web>
                        //<httpRuntime maxRequestLength="4096" executionTimeout="120"/>
                        //</system.web>

                        Sql_Command.ExecuteNonQuery();
                        #endregion
                    }
                }
                #endregion

                if (jCnt == 0)
                    mErr = "沒有選擇任何上傳的檔案!\\n";
            }
        }

        if (mErr != "")
        {
            // 顯示錯誤訊息
            lt_show.Text = "<script language=javascript>msg_close();alert(\"" + mErr + "\");</script>";
        }
        else
        {
            // 完成上傳，返回瀏覽頁
            lt_show.Text = "<script language=javascript>msg_close();alert(\"資料上傳完成!\\n\");";
            lt_show.Text = lt_show.Text + "location.replace(\"2002.aspx" + lb_page.Text + "\");</script>";
        }
    }
}
