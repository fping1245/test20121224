//---------------------------------------------------------------------------- 
//專案名稱	公用函數
//---------------------------------------------------------------------------- 
using System;

public class Decoder
{
	#region 原始對應字串
	private string std_str = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ!@$^*()_+-/|`:{}[]";
	#endregion

	#region 密碼查表字串
	private string[] en_str = new string[10]
		{
			"{}[]16Z!@$7CDY^*ESJKTU)_+-V(W8F2GHIL345MNOPQR90ABX/|`:",
			"$7GHIL3{QR9BX/|}[16Z!@CDTU(Y^*ESJ:K)_+-VW8F2]45MNOP`0A",
			"FGP`0A2]45M$7HIL3{QR916^*ESJ:K)_+-VWBX/|}[8NOZ!@CDTU(Y",
			"8F2]HI45MN`0$7DT/|}[16Z!XCU(Y^@L3{QR9B*EGASJ:K)_+-VWOP",
			"XCWU(Y^6Z!EGA5MS8F2]HIB*OP4N`0$7DT/|}[1J:K)_+-V@L3{QR9",
			"`0$7DT/|}[1XCW8QR9U(Y^F2]HIB*OP45MNJ:K)_+-V@L3{6Z!EGAS",
			"NJ:K)_+-V@L3`0$8QR]HIB*O9U(Y^F245M{6Z!EGAPS7DT/|}[1XCW",
			"^F24NR]HIB*OJ:K)_+-V@L3`0$8Q9U(S7D1XCWT/|}[Y5M{6Z!EGAP",
			"CWT/|}[Y5M{6Z24N$8Q9U(S7D1XA!EG^FPR]HIB*OJ:K)_+-V@L3`0",
			"T3`09U(S7|}[Y5M8QG^FPR]HIB*OJ:K)_2/LCW{6Z4N$+-V@D1XA!E"
		};
	#endregion

	#region 補英數字16位元的字元（英數字轉成16位元時，只有兩位，不足之處以此補足）、由起始用字串
	private string st_str = "acdhjklmnopqrstuvwxy";
	#endregion

	#region 每7字插入的字元
	private string in_str = "befgiz";
	#endregion

	#region 密碼表順序字元
	private string dc_sort = "LXQPAZDBCH";
	#endregion

	#region EnCode() 字串加密
	//函數功能	EnCode() 字串加密
	//傳入參數	scode	string	原始字串
	//傳回數值	ecode	string	加密字串
	//備註說明
	public string EnCode(string scode)
	{
		string ecode = "", tmpstr = "";
		Random rnd = new Random();
		int hcnt = 0, encnt = 0, cnt = 0, incnt = 0, zcnt = 0;

		//取得起始要加入的字串數( 1 ~ 3 個)
		hcnt = rnd.Next(1, 4);

		//隨機由起始字串中取得字元
		for (cnt = 0; cnt < hcnt; cnt++)
		{
			encnt = rnd.Next(0, 20);
			ecode += st_str.Substring(encnt, 1);
		}

		//隨機決定要用那一個密碼表
		encnt = rnd.Next(0, 10);
		ecode += dc_sort.Substring(encnt, 1);

		//轉換原始字串成為16進位字元
		cnt = 0;
		foreach (char cdata in scode)
		{
			zcnt = cnt % 54;
			cnt++;

			//每7字插入字元
			if (cnt % 7 == 0)
			{
				hcnt = rnd.Next(0, 6);
				ecode += in_str.Substring(hcnt, 1);

				if (rnd.Next(0, 10) > 4)		//決定是否要加第二個字元
				{
					hcnt = rnd.Next(0, 6);
					ecode += in_str.Substring(hcnt, 1);
				}
			}

			tmpstr = string.Format("{0:X2}", (int)cdata);
			// 不足4個字元，補2個字元
			if (tmpstr.Length < 4)
			{
				hcnt = rnd.Next(0, 20);
				ecode += st_str.Substring(hcnt, 1);

				hcnt = rnd.Next(0, 20);
				ecode += st_str.Substring(hcnt, 1);
			}

			foreach (char mchar in tmpstr)
			{
				incnt = std_str.IndexOf(mchar);
				incnt = (incnt + zcnt) % 54;

				ecode += en_str[encnt].Substring(incnt, 1);
			}
		}
		return ecode;
	}
	#endregion

	#region DeCode() 字串解密
	//函數功能	DeCode() 字串解密
	//傳入參數	ecode	string	加密字串
	//傳回數值	scode	string	原始字串
	//備註說明
	public string DeCode(string ecode)
	{
		string scode = "", tmpstr = "", workstr = "", codestr = "";
		int hcnt = 0, cnt = 0, encnt = 0, xcnt = 0, ycnt = 0, zcnt = 0;

		//判斷起始字元位置
		if (ecode.Length > 3)
		{
			tmpstr = ecode.Substring(0, 4);
			foreach (char mchar in tmpstr)
			{
				cnt++;
				if (st_str.IndexOf(mchar) < 0 && hcnt == 0)
				{
					hcnt = cnt;							//取得資料起始位置
					encnt = dc_sort.IndexOf(mchar);		//取得使用那一組密碼表
				}
			}

			if (hcnt != 0 && encnt > -1 && encnt < 10)	//確定有找到正確的密碼表
			{

				//取得去除起始字位的加密資料
				tmpstr = ecode.Substring(hcnt);

				foreach (char mchar in in_str)
				{
					tmpstr = tmpstr.Replace(mchar.ToString(), "");
				}

				//每個字為4個字元組成，故取4的整數，以預防解碼錯誤
				hcnt = Convert.ToInt32(tmpstr.Length / 4) * 4;

				ycnt = 0;

				for (cnt = 0; cnt < hcnt; cnt += 4)
				{
					zcnt = ycnt % 54;
					codestr = "";
					workstr = tmpstr.Substring(cnt, 4);

					foreach (char mchar in workstr)
					{
						if (st_str.IndexOf(mchar) < 0)
						{
							xcnt = en_str[encnt].IndexOf(mchar);
							if (xcnt > -1)
							{
								if (xcnt >= zcnt)
									xcnt = xcnt - zcnt;
								else
									xcnt = 54 + xcnt - zcnt;
								codestr += std_str.Substring(xcnt, 1);
							}
						}
					}

					//解碼後不為正確的16進位數字，代表加密字串有誤，中斷解碼，以空白字串回應
					try
					{
						scode += Convert.ToChar(Convert.ToInt32("0x" + codestr, 16));
					}
					catch
					{
						scode = "";
						cnt = hcnt;
					}
					ycnt++;
				}
			}
		}

		return scode;
	}
	#endregion
}