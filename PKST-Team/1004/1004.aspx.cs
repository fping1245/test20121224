//---------------------------------------------------------------------------- 
//程式功能	個人密碼修改範例
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class _1004 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 檢查使用者權限並存入使用紀錄
            //Check_Power("1004", true);
        }
    }

    // Check_Power() 檢查使用者權限並存入使用紀錄
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

    protected void bn_ok_Click(object sender, EventArgs e)
    {
        Common_Func cfc = new Common_Func();

        string mErr = "", mg_npass = "";

        mg_npass = tb_npass.Text.Trim();

        if (tb_spass.Text.Trim() == "")
            mErr = mErr + "請輸入「原登入密碼」!\\n";

        if (mg_npass == "")
            mErr = mErr + "請輸入「新登入密碼」!\\n";
        else if (cfc.CheckSQL(mg_npass))
            mErr = mErr + "「新登入密碼」請勿使用特殊符號!\\n";
        else if (mg_npass.Length > 12 || mg_npass.Length < 4)
            mErr = mErr + "「新登入密碼」長度為4~12個字!\\n";

        if (mg_npass != tb_rpass.Text.Trim())
            mErr = mErr + "「新登入密碼」與「新密碼確認」輸入的資料不同!\\n";
        else
        {
            if (tb_spass.Text.Trim() == tb_npass.Text.Trim())
                mErr = mErr + "「原登入密碼」與「新登入密碼」不可相同!\\n";
        }

        if (mErr == "")
        {
            string mg_pass = "", mg_id = "";
            string SqlString = "";
            SqlConnection Sql_conn;
            SqlCommand Sql_command;
            SqlDataReader Sql_reader;
            Decoder dcd = new Decoder();

            SqlString = "Select Top 1 mg_id, mg_pass From Manager Where mg_sid = @mg_sid";

            Sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString);
            Sql_conn.Open();
            Sql_command = new SqlCommand(SqlString, Sql_conn);
            Sql_command.Parameters.AddWithValue("@mg_sid", Session["mg_sid"].ToString());
            Sql_reader = Sql_command.ExecuteReader();
            if (Sql_reader.Read())
            {
                mg_id = Sql_reader["mg_id"].ToString().Trim();

                // 取得登入者於 mg_pass 欄位中的密碼並加以解密。
                mg_pass = dcd.DeCode(Sql_reader["mg_pass"].ToString().Trim());
            }
            Sql_reader.Close();

            // 比對資料表中的帳號和密碼是否與使用者所輸入者相符。
            if (mg_id == tb_id.Text.Trim() && mg_pass == tb_spass.Text.Trim())
            {
                // 加密使用者所輸入的新密碼。
                mg_pass = dcd.EnCode(tb_npass.Text.Trim());

                // 更新密碼。
                SqlString = "Update Manager Set mg_pass = @mg_pass Where mg_sid = @mg_sid and mg_id = @mg_id";
                Sql_command.Parameters.Clear();

                Sql_command = new SqlCommand(SqlString, Sql_conn);
                Sql_command.Parameters.AddWithValue("@mg_sid", Session["mg_sid"].ToString());
                Sql_command.Parameters.AddWithValue("@mg_id", mg_id);
                Sql_command.Parameters.AddWithValue("@mg_pass", mg_pass);

                Sql_command.ExecuteNonQuery();

                mErr = "密碼已更新完成，會在下一次登入時生效!\\n";
            }
            else
            {
                // 為避免有駭客入侵，不可明確表示是那個欄位輸入錯誤的訊息。
                mErr = mErr + "「使用者帳號」或「原登入密碼」輸入錯誤!\\n";
            }

            Sql_command.Dispose();
            Sql_conn.Close();
        }

        Literal txtMsg = new Literal();

        // 傳送錯誤訊息
        txtMsg.Text = "<script language=javascript>alert('" + mErr + "');</script>";

        // 利用 javascript 傳送錯誤訊息或進入功能頁面
        Page.Controls.Add(txtMsg);
    }
}
