//---------------------------------------------------------------------------- 
//程式功能	登入畫面
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 清除工作階段變數。
            Session.Remove("mg_sid");
            Session.Remove("mg_name");
            Session.Remove("mg_power");

            // 取得隨機產生四碼的驗證數字，並存入工作階段變數 confirm 中。
            Session["confirm"] = getConfirmCode();
        }
    }

    public string getConfirmCode()
    {
        Random rnd;
        int cnt = 0;
        string confirm = "";

        rnd = new Random(((int)DateTime.Now.Ticks));

        // 隨機產生四碼的驗證數字
        for (cnt = 0; cnt < 4; cnt++)
        {
            confirm = confirm + rnd.Next(10).ToString();
        }

        return confirm;
    }

    protected void bn_new_confirm_Click(object sender, EventArgs e)
    {
        Session["confirm"] = getConfirmCode();

        // 使用 Opera 瀏覽器時，要呼叫此 javascript 更新方式，才會更新驗證圖檔的顯示
        lt_show.Text = "<script language=javascript>renew_img();</script>";
    }

    protected void bn_reset_Click(object sender, EventArgs e)
    {
        tb_id.Text = "";
        tb_pass.Text = "";
        tb_confirm.Text = "";
        Session["confirm"] = getConfirmCode();
    }

    protected void bn_ok_Click(object sender, EventArgs e)
    {
        Common_Func cfc = new Common_Func();
        String_Func sfc = new String_Func();
        string mg_id = "", mg_pass = "", confirm = "", mErr = "", tmpstr = "";
        string[] tmparray;
        string[] strsplit = new string[] { "\t\n" };		// 分隔分辦用字串

        mg_id = tb_id.Text.Trim();
        mg_pass = tb_pass.Text.Trim();
        confirm = tb_confirm.Text.Trim();

        if (mg_id == "")
            mErr = mErr + "請填寫「帳號」!\\n";

        if (mg_pass == "")
            mErr = mErr + "請填寫「密碼」!\\n";

        if (Session["confirm"] == null)
            mErr = mErr + "驗證碼無法確認!\\n";
        else
            if (confirm != Session["confirm"].ToString())
                mErr = mErr + "驗證碼輸入錯誤!\\n";

        if (mErr == "")
        {
            tmpstr = cfc.Check_ID(mg_id, mg_pass, Request.ServerVariables["REMOTE_ADDR"]);

            if (sfc.Left(tmpstr, 1) == "*")
            {
                mErr = tmpstr.Substring(1);
            }
            else
            {
                tmparray = tmpstr.Split(strsplit, StringSplitOptions.None);

                Session["mg_sid"] = tmparray[0];
                Session["mg_name"] = tmparray[1];
                Session["mg_power"] = tmparray[2];
            }
        }

        if (mErr == "")
        {	// 全部驗證都正確

            // 清除驗證碼的 Session 值
            Session.Remove("confirm");

            // 重新導向至主畫面
            Response.Redirect("index.aspx");
        }
        else
        {	// 有錯誤

            // 重新產生驗證碼
            bn_reset_Click(sender, e);

            // 利用 javascript 顯示錯誤訊息
            lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");</script>";
        }
    }
}