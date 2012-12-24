<%@ WebHandler Language="C#" Class="confirm" %>
//---------------------------------------------------------------------------- 
//程式功能	圖文驗證模組範例 - 產生圖型
//
//備註說明	需先產生 Session["confirm"]
//			範例圖檔僅有 0 ~ 9 的圖型
//---------------------------------------------------------------------------- 
using System;
using System.Web;
using System.IO;
using System.Web.SessionState;

public class confirm : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        MemoryStream gdata = new MemoryStream();
        BuildImage bd_img = new BuildImage();
        string mdata = "";

        mdata = context.Session["confirm"].ToString();

        // 取得驗證圖型的資料（設定驗證圖型尺寸及驗證字串)
        gdata = bd_img.GenerateImage(200, 54, mdata);

        // 設定輸出格式
        context.Response.ContentType = "image/png";

        // 送出資料內容
        context.Response.BinaryWrite(gdata.ToArray());
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}