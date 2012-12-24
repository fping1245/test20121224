//---------------------------------------------------------------------------- 
//程式功能	網站檔案管理  
//---------------------------------------------------------------------------- 

using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class _2003 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Decoder dcode = new Decoder();
            string mErr = "", fl_url = "";

            // 檢查使用者權限並存入登入紀錄
            //Check_Power("2003", true);

            #region 取得所屬的實體位置
            using (SqlConnection Sql_conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AppSysConnectionString"].ConnectionString))
            {
                using (SqlCommand Sql_Command = new SqlCommand())
                {
                    string SqlString = "";
                    SqlString = "Select Top 1 fl_url From Fi_Location Where fl_no = 3";

                    Sql_Command.Connection = Sql_conn;
                    Sql_Command.CommandText = SqlString;

                    Sql_conn.Open();

                    using (SqlDataReader Sql_Reader = Sql_Command.ExecuteReader())
                    {
                        if (Sql_Reader.Read())
                        {
                            lb_fl_url.Text = Sql_Reader["fl_url"].ToString().Trim();
                            bn_go_root.ToolTip = "回到 " + lb_fl_url.Text + " ";
                        }
                        else
                            mErr = "找不到指定的路徑\\n";

                        Sql_Reader.Close();
                    }
                }
            #endregion
            }

            if (mErr == "")
            {
                #region 判斷是否有傳入值
                if (Request["fl_url"] == null)
                    lb_url.Text = lb_fl_url.Text;
                else if (Request["fl_url"].Trim() == "")
                    lb_url.Text = lb_fl_url.Text;
                else
                {
                    fl_url = dcode.DeCode(Request["fl_url"].Trim());

                    // 檢查是否有人使用入侵方式進入
                    if (fl_url.Length < lb_fl_url.Text.Length)
                        lb_url.Text = lb_fl_url.Text;
                    else if (fl_url.Substring(0, lb_fl_url.Text.Length) == lb_fl_url.Text)
                        lb_url.Text = fl_url;
                    else
                        lb_url.Text = lb_fl_url.Text;
                }

                lb_path.Text = Server.MapPath(lb_url.Text);

                // 加密編碼，傳送時以防入侵
                lb_url_encode.Text = Server.UrlEncode(dcode.EnCode(lb_url.Text));
                lb_fl_url_encode.Text = Server.UrlEncode(dcode.EnCode(lb_fl_url.Text));
                #endregion

                // 取得路徑內的子目錄及檔案清單
                Get_PathFile();
            }

            // 顯示錯誤訊息
            if (mErr != "")
                lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\");</script>";
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

    // 取得路徑內的子目錄及檔案清單，並指派給 lt_data 。
    private void Get_PathFile()
    {
        Decoder dcoder = new Decoder();
        string fname = "", fpath = "", subpath = "";

        lt_data.Text = "<table width='95%' border='1' cellpadding='4' cellspacing='0'>\n";
        lt_data.Text = lt_data.Text + "<tr align='center' bgcolor='#FF6A04' height=24>\n";
        lt_data.Text = lt_data.Text + "<td><font color=#FFFFFF>檔案名稱</font></td>\n";
        lt_data.Text = lt_data.Text + "<td><font color=#FFFFFF>檔案大小</font></td>\n";
        lt_data.Text = lt_data.Text + "<td><font color=#FFFFFF>副檔名</font></td>\n";
        lt_data.Text = lt_data.Text + "<td style=\"width:120pt\"><font color=#FFFFFF>建立時間</font></td>\n";
        lt_data.Text = lt_data.Text + "<td style=\"width:50pt\"><font color=#FFFFFF>更名</font></td>\n";
        lt_data.Text = lt_data.Text + "<td style=\"width:50pt\"><font color=#FFFFFF>刪除</font></td>\n";
        lt_data.Text = lt_data.Text + "</tr>\n";

        #region 若非最頂層，則產生回到上一層目錄
        if (lb_url.Text != lb_fl_url.Text)
        {
            // 取得上一層目錄
            subpath = Server.UrlEncode(dcoder.EnCode((My.Computer.FileSystem.GetParentPath(lb_url.Text) + "\\").Replace("\\", "/")));

            lt_data.Text = lt_data.Text + "<tr align=center>\n";
            lt_data.Text = lt_data.Text + "<td align=left>" + "<a href=\"2003.aspx?fl_url=" + subpath + "\">回上一層..</a>" + "</td>\n";
            lt_data.Text = lt_data.Text + "<td>...</td>\n";
            lt_data.Text = lt_data.Text + "<td>&lt;DIR&gt;</td>\n";
            lt_data.Text = lt_data.Text + "<td>&nbsp;</td>\n";
            lt_data.Text = lt_data.Text + "<td>&nbsp;</td>\n";
            lt_data.Text = lt_data.Text + "<td>&nbsp;</td>\n";
            lt_data.Text = lt_data.Text + "</tr>\n";
        }
        #endregion

        #region 找尋子目錄
        foreach (string mpath in My.Computer.FileSystem.GetDirectories(lb_path.Text))
        {
            fpath = My.Computer.FileSystem.GetDirectoryInfo(mpath).Name;
            subpath = Server.UrlEncode(dcoder.EnCode(lb_url.Text + fpath + "/"));

            lt_data.Text = lt_data.Text + "<tr align=center>\n";
            lt_data.Text = lt_data.Text + "<td align=left>" + "<a href=\"2003.aspx?fl_url=" + subpath + "\">\\" + fpath + "</a>" + "</td>\n";
            lt_data.Text = lt_data.Text + "<td>...</td>\n";
            lt_data.Text = lt_data.Text + "<td>&lt;DIR&gt;</td>\n";
            lt_data.Text = lt_data.Text + "<td>" + My.Computer.FileSystem.GetDirectoryInfo(mpath).CreationTime.ToString("yyyy/MM/dd HH:mm:ss") + "</td>\n";
            lt_data.Text = lt_data.Text + "<td><a href=\"javascript:renpath('" + fpath + "')\" class=\"abtn\">&nbsp;更名&nbsp;</a></td>\n";
            lt_data.Text = lt_data.Text + "<td><a href=\"javascript:delpath('" + fpath + "')\" class=\"abtn\">&nbsp;刪除&nbsp;</a></td>\n";
            lt_data.Text = lt_data.Text + "</tr>\n";
        }
        #endregion

        #region 找尋檔案
        foreach (string mfile in My.Computer.FileSystem.GetFiles(lb_path.Text))
        {
            fname = My.Computer.FileSystem.GetFileInfo(mfile).Name;

            lt_data.Text = lt_data.Text + "<tr align=center>\n";
            lt_data.Text = lt_data.Text + "<td align=left>" + "<a href=\"" + lb_url.Text + fname + "\" target=\"_blank\">" + fname + "</a>" + "</td>\n";
            lt_data.Text = lt_data.Text + "<td align=right>" + My.Computer.FileSystem.GetFileInfo(mfile).Length.ToString("N0") + "&nbsp;</td>\n";
            lt_data.Text = lt_data.Text + "<td>" + My.Computer.FileSystem.GetFileInfo(mfile).Extension.ToUpper().Replace(".", "") + "</td>\n";
            lt_data.Text = lt_data.Text + "<td>" + My.Computer.FileSystem.GetFileInfo(mfile).CreationTime.ToString("yyyy/MM/dd HH:mm:ss") + "</td>\n";
            lt_data.Text = lt_data.Text + "<td><a href=\"javascript:renfile('" + fname + "')\" class=\"abtn\">&nbsp;更名&nbsp;</a></td>\n";
            lt_data.Text = lt_data.Text + "<td><a href=\"javascript:delfile('" + fname + "')\" class=\"abtn\">&nbsp;刪除&nbsp;</a></td>\n";
            lt_data.Text = lt_data.Text + "</tr>\n";
        }
        #endregion

        // 沒有任何資料
        if (fname == "" && fpath == "")
            lt_data.Text = lt_data.Text + "<tr align=center><td colspan=6>沒有任何檔案或子目錄</td></tr>\n";

        lt_data.Text = lt_data.Text + "</table>\n";
    }

    // 回到根目錄
    protected void bn_go_root_Click(object sender, EventArgs e)
    {
        Server.Transfer("2003.aspx?fl_url=" + lb_fl_url_encode.Text);
    }

    // 顯示建立子目錄元件
    protected void bn_mk_dir_Click(object sender, EventArgs e)
    {
        // 隱藏上傳檔案相關元件
        Close_File();

        #region 顯示建立子目錄相關元件
        tb_dir.Text = "";
        tb_dir.Visible = true;
        lt_mk_dir.Visible = true;
        bn_mk_dir_ok.Visible = true;

        bn_mk_dir.Visible = false;
        #endregion

        bn_cancel.Visible = true;
    }

    // 顯示上傳檔案元件
    protected void bn_fu_file_Click(object sender, EventArgs e)
    {
        // 隱藏建立子目錄相關元件
        Close_Dir();

        #region 開啟上傳檔案相關元件
        lt_upfile.Visible = true;
        fu_file.Visible = true;
        bn_fu_file_ok.Visible = true;

        bn_fu_file.Visible = false;
        #endregion

        bn_cancel.Visible = true;
    }

    // 隱藏子目錄及上傳檔案相關元件
    protected void bn_cancel_Click(object sender, EventArgs e)
    {
        // 隱藏建立子目錄相關元件
        Close_Dir();

        // 隱藏上傳檔案相關元件
        Close_File();

        bn_cancel.Visible = false;
    }

    // 隱藏建立子目錄相關元件
    private void Close_Dir()
    {
        tb_dir.Text = "";
        tb_dir.Visible = false;
        lt_mk_dir.Visible = false;
        bn_mk_dir_ok.Visible = false;

        bn_mk_dir.Visible = true;
    }

    // 隱藏上傳檔案相關元件
    private void Close_File()
    {
        lt_upfile.Visible = false;
        fu_file.Visible = false;
        bn_fu_file_ok.Visible = false;

        bn_fu_file.Visible = true;
    }

    // 建立子目錄
    protected void bn_mk_dir_ok_Click(object sender, EventArgs e)
    {
        string mErr = "", fpath = "";

        if (tb_dir.Text != "")
        {
            fpath = lb_path.Text + tb_dir.Text + "\\";
            if (My.Computer.FileSystem.DirectoryExists(fpath))
                mErr = "子目錄已存在，不用再建立!\\n";
            else
            {
                try
                {
                    My.Computer.FileSystem.CreateDirectory(fpath);
                }
                catch (Exception ex)
                {
                    mErr = ex.Message.Replace("\\", "\\\\");
                }
            }
        }
        else
            mErr = "請輸入子目錄的名稱!\\n";

        if (mErr == "")
        {
            lt_show.Text = "<script language=javascript>alert(\"「" + tb_dir.Text + "」目錄建立完成!\");";

            // 以 javascript 重新導向的方式，重新整理本頁，以防止使用者按瀏覽器重新整理，而重複建子目錄
            lt_show.Text = lt_show.Text + "location.replace(\"2003.aspx?fl_url=" + lb_url_encode.Text + "\");</script>";
        }
        else
            lt_show.Text = "<script language=javascript>alert(\"" + mErr + "\"\n);</script>";

        Close_Dir();
    }

    // 上傳檔案
    protected void bn_fu_file_ok_Click(object sender, EventArgs e)
    {
        string mErr = "", fname = "", fext = "", tmpstr = "";
        int fCnt = 0;

        // 有收到檔案
        if (fu_file.HasFile)
        {
            fname = fu_file.FileName;
            fext = My.Computer.FileSystem.GetFileInfo(fname).Extension.ToString();

            #region 檢查檔案是否存在，存在時要修改檔名 xxxx1.ext、xxxx2.ext、xxxx3.ext ....
            tmpstr = fname.Replace(fext, "");
            while (My.Computer.FileSystem.FileExists(lb_path.Text + fname))
            {
                fCnt++;
                fname = tmpstr + fCnt.ToString() + fext;
            }
            #endregion

            #region 開始存檔
            try
            {
                fu_file.SaveAs(lb_path.Text + fname);
            }
            catch (Exception ex)
            {
                mErr = ex.Message.Replace("\\", "\\\\");
            }

            #endregion
        }
        else
            mErr = "請選擇要上傳的檔案!\\n";

        if (mErr == "")
        {
            lt_show.Text = "<script language=javascript>msg_close();alert(\"「" + fname + "」檔案上傳成功!\");";

            // 以 javascript 重新導向的方式，解決上傳檔案或建子目錄時，按下瀏覽器重新整理，會重複上傳或建目錄的問題
            lt_show.Text = lt_show.Text + "location.replace(\"2003.aspx?fl_url=" + lb_url_encode.Text + "\");</script>";
        }
        else
            lt_show.Text = "<script language=javascript>msg_close();alert(\"" + mErr + "\");</script>";

        Close_File();
    }
}
