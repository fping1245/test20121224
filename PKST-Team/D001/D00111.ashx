<%@ WebHandler Language="C#" Class="_D00111" %>
//---------------------------------------------------------------------------- 
//程式功能	論壇前端 > 新增主題 > 產生圖型
//備註說明	需先產生 Session["D001"]
//			範例圖檔僅有 0 ~ 9 的圖型
//---------------------------------------------------------------------------- 
using System;
using System.Web;
using System.IO;
using System.Web.SessionState; // 要使用 Session 必需加入此命名空間

public class _D00111 : IHttpHandler, IRequiresSessionState
{
	// class 加入「IRequiresSessionState」可讀寫 Session
	// class 加入「IReadOnlySessionState」可讀寫 Session，但寫入的狀況無法傳給其它頁面使用
    public void ProcessRequest (HttpContext context) {
		MemoryStream gdata = new MemoryStream();
		BuildImage bd_img = new BuildImage();
		string mdata = "";

		// Session、Request、Response 之前均要加入 context. 才能使用
		mdata = context.Session["D001"].ToString();
		
		// 取得驗證圖型的資料（設定驗證圖型尺寸及驗證字串)
		gdata = bd_img.GenerateImage(200, 54, mdata);

		// 設定輸出格式
		context.Response.ContentType = "image/png";

		// 送出資料內容
		context.Response.BinaryWrite(gdata.ToArray());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}