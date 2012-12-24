//---------------------------------------------------------------------------- 
//程式功能	字串加密解密範例
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _1001 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 檢查使用者權限並存入使用紀錄
            ////Check_Power("1001", true);
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

    protected void bn_encode_Click(object sender, EventArgs e)
    {
        Decoder dcode = new Decoder();
        lb_encrypt.Text = dcode.EnCode(tb_source.Text);
    }

    protected void bn_decode_Click(object sender, EventArgs e)
    {
        Decoder dcode = new Decoder();
        lb_source.Text = dcode.DeCode(tb_encrypt.Text);

    }
}
