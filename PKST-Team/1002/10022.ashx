<%@ WebHandler Language="C#" Class="_10022" %>
//---------------------------------------------------------------------------- 
//程式功能	圖文驗證模組範例 - 產生文字圖形
//備註說明	需先產生 Session["confirm"]
//---------------------------------------------------------------------------- 
using System;
using System.IO;
using System.Web;
using System.Web.SessionState; // 要使用工作階段變數必需加入此命名空間

public class _10022 : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        MemoryStream gdata = new MemoryStream();
        BuildImage bd_img = new BuildImage();

        // 取得驗證圖形的資料（設定驗證圖形尺寸及驗證字串)
        gdata = bd_img.GenerateWord(200, 54, context.Session["confirm"].ToString());

        // Session、Request、Response 之前均要加入 context. 才能使用。
        // 設定輸出格式。
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