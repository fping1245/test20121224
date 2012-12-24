//---------------------------------------------------------------------------- 
//程式功能	檔案上傳下載 (實體路徑存放檔案) > 刪除檔案及資料
//---------------------------------------------------------------------------- 
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Configuration;

public partial class _2001_del : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string mErr = "";
        int ckint = 0;

        // 檢查使用者權限但不存入登入紀錄
        //Check_Power("2001", false);

        if (Request["sid"] != null)
        {
            if (int.TryParse(Request["sid"].Trim(), out ckint))
            {
                string SqlString = "", tmpstr = "";
                string fl_url = "", fl_path = "", fc_name = "";

                #region 刪除檔案及刪除資料庫紀錄
                using (SqlConnection Sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
                {
                    Sql_conn.Open();

                    SqlString = "Select Top 1 l.fl_url, c.fc_name From Fi_Content c";
                    SqlString = SqlString + " Inner Join Fi_Location l On c.fl_no = l.fl_no";
                    SqlString = SqlString + " Where c.fc_sid = @fc_sid";

                    using (SqlCommand Sql_Command = new SqlCommand())
                    {
                        #region 尋找檔案資料
                        Sql_Command.Connection = Sql_conn;
                        Sql_Command.CommandText = SqlString;
                        Sql_Command.Parameters.AddWithValue("fc_sid", ckint);

                        SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

                        if (Sql_Reader.Read())
                        {
                            fc_name = Sql_Reader["fc_name"].ToString().Trim();
                            fl_url = Sql_Reader["fl_url"].ToString().Trim();
                            fl_path = Server.MapPath(fl_url);
                        }
                        else
                            mErr = "找不到要刪除的資料!\\n";

                        Sql_Reader.Close();
                        Sql_Reader.Dispose();
                        #endregion

                        #region 刪除檔案
                        if (mErr == "")
                        {
                            try
                            {
                                File.Delete(fl_path + fc_name);
                                if (File.Exists(fl_path + fc_name))
                                    mErr = "檔案無法刪除!\\n";
                            }
                            catch (Exception ex)
                            {
                                tmpstr = ex.Message;
                                tmpstr = tmpstr.Replace("\\", "\\\\");
                                mErr = mErr + fc_name + "檔案刪除失敗!\\n" + tmpstr + "\\n";
                            }
                        }
                        #endregion

                        #region 刪除資料庫紀錄
                        if (mErr == "")
                        {
                            SqlString = "Delete From Fi_Content Where fc_sid = @fc_sid";
                            Sql_Command.Parameters.Clear();
                            Sql_Command.CommandText = SqlString;
                            Sql_Command.Parameters.AddWithValue("fc_sid", ckint);

                            Sql_Command.ExecuteNonQuery();
                        }
                        #endregion

                        Sql_Command.Dispose();
                    }
                }
                #endregion
            }
            else
                mErr = "參數傳送錯誤!\\n";
        }
        else
            mErr = "參數傳送錯誤!\\n";

        if (mErr == "")
        {
            lt_show.Text = "<script language=javascript>alert(\"資料刪除成功!\\n\");";
            lt_show.Text = lt_show.Text + "parent.location.reload();</script>";
        }
        else
            lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");</script>";
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
}
