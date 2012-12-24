//---------------------------------------------------------------------------- 
//專案名稱	公用函數
//程式功能	產生驗證圖形
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;

public class BuildImage
{
	// GenerateImage 以圖檔產生驗證字圖形
	//函數功能:	GenerateImage 以圖檔產生驗證文字圖形
	//傳入參數:	
	//			img_width		圖形寬度
	//			img_height		圖形高度
	//			confirm_str		驗證字串
	//傳回數值:
	//			MemoryStream	圖形資料
	//備註說明:	本範例僅有 0 ~ 9 的數字圖檔，故驗證字串僅可輸入 0 ~ 9 的數字
	public MemoryStream GenerateImage(int img_width, int img_height, string confirm_str)
	{
		int wlen = 0, cnt = 0, tmpwidth, tmpheight;
		string tmpfile = "";

		// 取得網站存放圖檔的位置
		string gpath = HttpContext.Current.Request.MapPath("~/images/confirm/");

		// 取得字串長度
		wlen = confirm_str.Length;

		// 建立圖片元件
		Bitmap img_work = new System.Drawing.Bitmap(img_width, img_height);

		// 建立繪圖元件
		Graphics gh_work = Graphics.FromImage(img_work);

		// 擷取字串內容對映的圖檔，並填入 img_work 圖形物件
		for (cnt = 0; cnt < wlen; cnt++)
		{
			// 取得對映圖檔的名稱
			tmpfile = gpath + confirm_str.Substring(cnt, 1) + ".gif";

			// 從檔案取得圖片物件
			using (System.Drawing.Image img_tmp = System.Drawing.Image.FromFile(tmpfile, true))
			{
				tmpwidth = img_tmp.Width;
				tmpheight = img_tmp.Height;

				// 將圖形填入繪圖元件
				gh_work.DrawImage(img_tmp, new Rectangle(cnt * tmpwidth, 0, tmpwidth, tmpheight), 0, 0, tmpwidth, tmpheight, GraphicsUnit.Pixel);
			}
		}

		// 建立繪圖輸出串流
		MemoryStream ms_work = new MemoryStream();

		//將圖片儲存到輸出串流
		img_work.Save(ms_work, System.Drawing.Imaging.ImageFormat.Png);

		gh_work.Dispose();
		img_work.Dispose();

		return ms_work;
	}

	// GenerateWord 以系統字型產生驗證圖形
	//函數功能:	GenerateWord 以系統字型產生驗證圖形
	//傳入參數:	
	//			img_width		圖形寬度
	//			img_height		圖形高度
	//			confirm_str		驗證字串
	//傳回數值:
	//			MemoryStream	圖形資料
	//備註說明:
	public MemoryStream GenerateWord(int img_width, int img_height, string confirm_str)
	{
		int wlen = 0, cnt = 0, fcnt = 0, tmpwidth = 0, tmpheight1 = 0, tmpheight2 = 0;
		Font ft_work;
		Pen pn_work;
		Color cr_work;
		Brush bh_work;
		Random rnd = new Random((int)DateTime.Now.Ticks);

		// 取得字串長度
		wlen = confirm_str.Length;

		// 分配每個字的寬度
		tmpwidth = img_width / wlen;

		// 建立圖片元件
		Bitmap img_work = new System.Drawing.Bitmap(img_width, img_height);

		// 建立繪圖元件
		Graphics gh_work = Graphics.FromImage(img_work);

		// 設定繪圖元件背景顏色
		gh_work.Clear(Color.DarkGreen);

		// 以系統字體於 img_work 圖形物件繪製字型
		for (cnt = 0; cnt < wlen; cnt++)
		{
			// 隨機取得顏色
			switch (rnd.Next(5))
			{
				case 0:
					cr_work = Color.Yellow;
					break;
				case 1:
					cr_work = Color.White;
					break;
				case 2:
					cr_work = Color.Tomato;
					break;
				case 3:
					cr_work = Color.LightBlue;
					break;
				default:
					cr_work = Color.Lime;
					break;
			}

			// 設定字型筆刷的顏色
			bh_work = new SolidBrush(cr_work);

			// 隨機設定 16 ~ 40 之間的字型大小
			fcnt = rnd.Next(16, 41);

			ft_work = new Font("Arial", fcnt, FontStyle.Bold);

			gh_work.DrawString(confirm_str.Substring(cnt, 1), ft_work, bh_work, cnt * tmpwidth, 3);
		}
	
		// 背景隨機畫6條線
		for (cnt = 0; cnt < 6; cnt++)
		{
			// 隨機取得顏色
			switch (rnd.Next(5))
			{
				case 0:
					cr_work = Color.Blue;
					break;
				case 1:
					cr_work = Color.Orange;
					break;
				case 2:
					cr_work = Color.Red;
					break;
				case 3:
					cr_work = Color.Sienna;
					break;
				default:
					cr_work = Color.Pink;
					break;
			}

			// 隨機設定筆刷粗細
			fcnt = rnd.Next(3);

			// 設定筆刷元件
			pn_work = new Pen(cr_work, fcnt);

			tmpheight1 = rnd.Next(img_height);
			tmpheight2 = rnd.Next(img_height);
			gh_work.DrawLine(pn_work, 0, tmpheight1, img_width, tmpheight2);
		}

		// 建立繪圖輸出串流
		MemoryStream ms_work = new MemoryStream();

		//將圖片儲存到輸出串流
		img_work.Save(ms_work, System.Drawing.Imaging.ImageFormat.Png);

		gh_work.Dispose();
		img_work.Dispose();

		return ms_work;
	}
}
