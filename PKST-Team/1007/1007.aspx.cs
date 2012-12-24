//---------------------------------------------------------------------------- 
//程式功能	使用者登入紀錄查詢
//---------------------------------------------------------------------------- 
using System;

public partial class _1007 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 檢查使用者權限並存入使用紀錄
            //Check_Power("1007", true);

            sds_Mg_Log.SelectParameters["btime"].DefaultValue = "1800/01/01";
            sds_Mg_Log.SelectParameters["etime"].DefaultValue = DateTime.MaxValue.ToString("yyyy/MM/dd HH:mm:ss");
            sds_Mg_Log.SelectParameters["mg_sid1"].DefaultValue = Int32.MinValue.ToString();
            sds_Mg_Log.SelectParameters["mg_sid2"].DefaultValue = Int32.MaxValue.ToString();
            sds_Mg_Log.SelectParameters["mg_name"].DefaultValue = "%%";
            sds_Mg_Log.SelectParameters["fi_name1"].DefaultValue = "%%";
            sds_Mg_Log.SelectParameters["fi_name2"].DefaultValue = "%%";
            sds_Mg_Log.SelectParameters["lg_ip"].DefaultValue = "%%";
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

    protected void Btn_Set_Click(object sender, EventArgs e)
    {
        // 檢查查詢條件是否改變
        Chk_Filter();
    }

    // 檢查查詢條件是否改變
    private void Chk_Filter()
    {
        Common_Func cfc = new Common_Func();

        int ckint = 0;
        DateTime ckbtime, cketime;
        string tmpstr = "";


        // 有輸入編號，則設定條件
        if (int.TryParse(tb_mg_sid.Text.Trim(), out ckint))
        {
            sds_Mg_Log.SelectParameters["mg_sid1"].DefaultValue = ckint.ToString();
            sds_Mg_Log.SelectParameters["mg_sid2"].DefaultValue = ckint.ToString();
        }
        else
        {
            sds_Mg_Log.SelectParameters["mg_sid1"].DefaultValue = Int32.MinValue.ToString();
            sds_Mg_Log.SelectParameters["mg_sid2"].DefaultValue = Int32.MaxValue.ToString();
        }

        // 有輸入開始時間範圍，則設定條件
        if (DateTime.TryParse(tb_btime.Text.Trim(), out ckbtime))
            sds_Mg_Log.SelectParameters["btime"].DefaultValue = ckbtime.ToString("yyyy/MM/dd HH:mm:ss");
        else
            sds_Mg_Log.SelectParameters["btime"].DefaultValue = "1800/01/01";

        // 有輸入結束時間範圍，則設定條件
        if (DateTime.TryParse(tb_etime.Text.Trim(), out cketime))
            sds_Mg_Log.SelectParameters["etime"].DefaultValue = cketime.ToString("yyyy/MM/dd HH:mm:ss");
        else
            sds_Mg_Log.SelectParameters["etime"].DefaultValue = DateTime.MaxValue.ToString("yyyy/MM/dd HH:mm:ss");

        // 有輸入姓名，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
        tmpstr = cfc.CleanSQL(tb_mg_name.Text.Trim());
        if (tmpstr != "")
            sds_Mg_Log.SelectParameters["mg_name"].DefaultValue = "%" + tmpstr + "%";
        else
            sds_Mg_Log.SelectParameters["mg_name"].DefaultValue = "%%";

        // 有輸入主功能，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
        tmpstr = cfc.CleanSQL(tb_fi_name1.Text.Trim());
        if (tmpstr != "")
            sds_Mg_Log.SelectParameters["fi_name1"].DefaultValue = "%" + tmpstr + "%";
        else
            sds_Mg_Log.SelectParameters["fi_name1"].DefaultValue = "%%";

        // 有輸入子功能，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
        tmpstr = cfc.CleanSQL(tb_fi_name2.Text.Trim());
        if (tmpstr != "")
            sds_Mg_Log.SelectParameters["fi_name2"].DefaultValue = "%" + tmpstr + "%";
        else
            sds_Mg_Log.SelectParameters["fi_name2"].DefaultValue = "%%";

        // 有輸入使用者 IP，則設定條件 (cfc.CleanSQL() => 移除可能為 SQL 隱碼攻擊的字串)
        tmpstr = cfc.CleanSQL(tb_lg_ip.Text.Trim());
        if (tmpstr != "")
            sds_Mg_Log.SelectParameters["lg_ip"].DefaultValue = "%" + tmpstr + "%";
        else
            sds_Mg_Log.SelectParameters["lg_ip"].DefaultValue = "%%";

        gv_Mg_Log.DataBind();
    }
}
