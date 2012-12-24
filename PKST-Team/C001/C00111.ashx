<%@ WebHandler Language="C#" Class="_C00111" %>
//---------------------------------------------------------------------------- 
//程式功能	留言板前端 > 新增留言 > 產生圖型
//備註說明	需先產生 Session["c001"]
//			範例圖檔僅有 0 ~ 9 的圖型
//---------------------------------------------------------------------------- 
using System;
using System.Web;
using System.IO;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間

public class _C00111 : IHttpHandler, IRequiresSessionState
{
	// class 要加入 「, IRequiresSessionState」才能使用 Session
    public void ProcessRequest (HttpContext context) {
		MemoryStream gdata = new MemoryStream();
		BuildImage bd_img = new BuildImage();
		string mdata = "";

		// Session、Request、Response 之前均要加入 context. 才能使用
		mdata = context.Session["C001"].ToString();
		
		// 取得驗證圖型的資料（設定驗證圖型尺寸及驗證字串)
		gdata = bd_img.GenerateImage(200, 54, mdata);

		// 設定輸出格式
		context.Response.ContentType = "image/png";

		// 送出資料內容
		context.Response.BinaryWrite(gdata.ToArray());
    }
 
    public bool IsReusable {
        get {
			// Session 是可讀寫的物件，如果程式中需要寫入 Session，要改成 true 才能寫入，如果只是要讀取的話，就可保持原來的 false
            return false;
        }
    }

}