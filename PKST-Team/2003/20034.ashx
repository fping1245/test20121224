﻿<%@ WebHandler Language="C#" Class="_20034" %>
//---------------------------------------------------------------------------- 
//程式功能	網站檔案管理 > 更改目錄名稱
//---------------------------------------------------------------------------- 

using System;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間

public class _20034 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        string mErr = "";
        string fpath = "", nfpath = "", f_path = "", nf_path = "", furl = "";

        Decoder dcoder = new Decoder();

        // 檢查使用者權限，但不存入登入紀錄
        //Check_Power("2003", false, context);

        if (context.Request["fpath"] != null)
            fpath = context.Request["fpath"].Trim();

        if (context.Request["nfpath"] != null)
            nfpath = context.Request["nfpath"].Trim();

        if (context.Request["furl"] != null)
            furl = dcoder.DeCode(context.Request["furl"].ToString());

        if (nfpath == "")
            mErr = "請輸入新的目錄名稱!\\n";
        else
        {
            f_path = context.Server.MapPath(furl + fpath + "\\");
            nf_path = context.Server.MapPath(furl + nfpath + "\\");

            if (Directory.Exists(f_path))
            {
                try
                {
                    Directory.Move(f_path, nf_path);

                    if (Directory.Exists(f_path))
                        mErr = " 無法更改「" + fpath + "」的名稱!\\n";
                }
                catch (Exception ex)
                {
                    mErr = ex.Message.Replace("\\", "\\\\");
                }
            }
            else
                mErr = "找不到「" + f_path.Replace("\\", "\\\\") + "」這個目錄!\\n";
        }

        // 使用 javascript 時，要用 "text/html" 的格式
        context.Response.ContentType = "text/html";

        if (mErr == "")
        {
            furl = context.Server.UrlEncode(dcoder.EnCode(furl));
            context.Response.Write("<script language=javascript>alert(\"「" + fpath + "」→「" + nfpath + "」更名完成!\\n\");parent.location.replace(\"2003.aspx?fl_url=" + furl + "\");</script>");
        }
        else
            context.Response.Write("<script language=javascript>alert(\"" + mErr + "\");</script>");

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private void Check_Power(string f_power, bool bl_save, HttpContext context)
    {
        // 載入公用函數
        Common_Func cfc = new Common_Func();

        // 若 Session 不存在則直接顯示錯誤訊息
        try
        {
            if (cfc.Check_Power(context.Session["mg_sid"].ToString(), context.Session["mg_name"].ToString(), context.Session["mg_power"].ToString(), f_power, context.Request.ServerVariables["REMOTE_ADDR"], bl_save) > 0)
                context.Response.Redirect("../Error.aspx?ErrCode=1");
        }
        catch
        {
            context.Response.Redirect("../Error.aspx?ErrCode=2");
        }
    }
}