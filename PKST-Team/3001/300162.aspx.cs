//---------------------------------------------------------------------------- 
//程式功能	相簿管理 > 縮圖顯示 > 全圖顯示
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

public partial class _300162 : System.Web.UI.Page
{
    public string ac_pic = "", al_sid = "0", ac_sid = "0", rownum = "1", maxrow = "0", ac_desc = "";
    public int ac_swidth = 120, ac_sheight = 120, tb_height = 600, tb_width = 870, show_effect = 0;
    public double ac_width = 870.0, ac_height = 600.0, tmpval = 1.0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int ckint = -1;
            string mErr = "";

            // 檢查使用者權限，但不存登入紀錄
            //Check_Power("3001", false);

            #region 傳入參數處理
            // 上下一筆時處理用的指標
            if (Request["rownum"] != null)
            {
                if (int.TryParse(Request["rownum"], out ckint))
                    rownum = ckint.ToString();
                else
                    rownum = "1";
            }
            else
                rownum = "1";

            // 取得本頁最大筆數
            if (Request["maxrow"] != null)
            {
                if (int.TryParse(Request["maxrow"], out ckint))
                    maxrow = ckint.ToString();
                else
                    maxrow = "1";
            }
            else
                maxrow = "1";

            // 顯示效果
            if (Request["effect"] != null)
                if (int.TryParse(Request["effect"], out ckint))
                    show_effect = ckint;

            // 畫布區域寬度
            if (Request["tbw"] != null)
                if (double.TryParse(Request["tbw"], out tmpval))
                {
                    ac_width = tmpval;
                    tb_width = (int)tmpval;
                }

            // 畫布區域高度
            if (Request["tbh"] != null)
                if (double.TryParse(Request["tbh"], out tmpval))
                {
                    ac_height = tmpval;
                    tb_height = (int)tmpval;
                }
            #endregion

            if (Request["ac_sid"] != null && Request["al_sid"] != null)
            {
                if (int.TryParse(Request["ac_sid"], out ckint))
                {
                    ac_sid = ckint.ToString();

                    if (int.TryParse(Request["al_sid"], out ckint))
                        al_sid = ckint.ToString();
                }
                else
                    mErr = "相片參數傳送錯誤!\\n";

                if (mErr == "")
                {
                    #region 處理圖形大小
                    using (SqlConnection Sql_Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
                    {
                        Sql_Conn.Open();

                        using (SqlCommand Sql_Command = new SqlCommand())
                        {
                            string SqlString = "";
                            double fCnt = 0.0, fwidth = 0.0, fheight = 0.0;

                            Sql_Command.Connection = Sql_Conn;

                            if (ac_sid == "0")
                            {
                                // 用上下頁方式選擇的處理方式
                                SqlString = "Select Top 1 * From (Select ac_sid, ac_width, ac_height, ac_desc";
                                SqlString = SqlString + ", Row_Number() Over (Order by ac_name) as rownum From Al_Content";
                                SqlString = SqlString + " Where al_sid = @al_sid) as mTable";
                                SqlString = SqlString + " Where rownum = @rownum";

                                Sql_Command.CommandText = SqlString;
                                Sql_Command.Parameters.AddWithValue("al_sid", al_sid);
                                Sql_Command.Parameters.AddWithValue("rownum", rownum);
                            }
                            else
                            {
                                // 指定相片編號的處理方式
                                SqlString = "Select Top 1 ac_sid, ac_width, ac_height, ac_desc From Al_Content Where ac_sid = @ac_sid";
                                Sql_Command.CommandText = SqlString;
                                Sql_Command.Parameters.AddWithValue("ac_sid", ac_sid);
                            }

                            SqlDataReader Sql_Reader = Sql_Command.ExecuteReader();

                            if (Sql_Reader.Read())
                            {
                                ac_sid = Sql_Reader["ac_sid"].ToString();
                                ac_pic = "3001621.ashx?ac_sid=" + ac_sid;
                                ac_swidth = int.Parse(Sql_Reader["ac_width"].ToString());
                                ac_sheight = int.Parse(Sql_Reader["ac_height"].ToString());

                                fheight = ac_sheight / ac_height;
                                fwidth = ac_swidth / ac_width;

                                if (fwidth > fheight)
                                {
                                    if (ac_swidth > ac_width)
                                    {
                                        fCnt = fwidth;
                                        ac_height = (int)(ac_sheight / fCnt);
                                    }
                                    else
                                    {
                                        ac_width = ac_swidth;
                                        ac_height = ac_sheight;
                                    }
                                }
                                else
                                {
                                    if (ac_sheight > ac_height)
                                    {
                                        fCnt = fheight;
                                        ac_width = (int)(ac_swidth / fCnt);
                                    }
                                    else
                                    {
                                        ac_width = ac_swidth;
                                        ac_height = ac_sheight;
                                    }
                                }

                                ac_desc = Sql_Reader["ac_desc"].ToString().Trim();

                                img_show.Height = (int)ac_height;
                                img_show.Width = (int)ac_width;
                                img_show.ToolTip = ac_desc;
                            }
                            else
                            {
                                ac_pic = "../images/blank.gif";
                                mErr = "找不到這張相片的資料!\\n";
                            }
                        }
                    }
                    #endregion
                }
            }
            else
            {
                ac_pic = "../images/blank.gif";
                mErr = "相片參數傳送錯誤!\\n";
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
