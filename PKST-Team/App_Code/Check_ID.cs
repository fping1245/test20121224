//---------------------------------------------------------------------------- 
//專案名稱	公用函數
//程式功能	證件相關檢查
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

public class Check_ID
{
	#region Check_TW_ID() 驗證台灣身分證字號(10碼)
	public int Check_TW_ID(string strID)
	{
		string nFirst = "";
		int nTol = 0, nCount = 0, chk_value = 9, ckint = -1;

		strID = strID.Trim();

		// 確定台灣身分證號碼有10碼，最後一碼為檢查碼
		if (strID.Length == 10)
		{
			nFirst = strID.Substring(2, 1);
			if (nFirst == "1" || nFirst == "2")
			{
				if (int.TryParse(strID.Substring(1, 9), out ckint))
				{
					nFirst = strID.Substring(0, 1).ToUpper();
					switch (nFirst)
					{
						case "B":
						case "N":
						case "Z":
							nTol = 0;
							break;
						case "A":
						case "M":
						case "W":
							nTol = 1;
							break;
						case "K":
						case "L":
						case "Y":
							nTol = 2;
							break;
						case "J":
						case "V":
						case "X":
							nTol = 3;
							break;
						case "H":
						case "U":
							nTol = 4;
							break;
						case "G":
						case "T":
							nTol = 5;
							break;
						case "F":
						case "S":
							nTol = 6;
							break;
						case "E":
						case "R":
							nTol = 7;
							break;
						case "D":
						case "O":
						case "Q":
							nTol = 8;
							break;
						case "C":
						case "I":
						case "P":
							nTol = 9;
							break;
						default:
							nTol = 99;
							break;
					}

					if (nTol != 99)
					{
						for (nCount = 1; nCount < 10; nCount++)
						{
							nTol += int.Parse(strID.Substring(nCount, 1)) * (9 - nCount);
						}

						nTol += int.Parse(strID.Substring(9, 1));

						if ((nTol % 10) == 0)
							chk_value = 0;
						else
							chk_value = 1;
					}
					else
						chk_value = 2;
				}
				else
					chk_value = 4;
			}
			else
				chk_value = 5;
		}
		else
			chk_value = 3;

		return chk_value;
	}
	#endregion

	#region Check_CN_ID() 驗證大陸公民身份編號 (15碼及18碼)
	public int Check_CN_ID(string strID)
	{
		int intIndex = 0, intSum = 0;
		Int64 ckint = -1;
		DateTime ckdt;
		int[] intTotal = new int[17];			// 乘積
		int[] intNumber = new int[18];			// 編碼
		int[] intWeight = new int[18]			// 權數
		{
			7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2,1
		};
		string[] strTable = new string[11]		// 相對值
		{
			"1","0","X","9","8","7","6","5","4","3","2"
		};
	
		string strCheck_Src = "";				// 來自 strID 之檢驗碼 
		string strCheck_Dst = "";				// 來算計算後之檢驗碼
		int chk_value = 0;

		chk_value = 9;
		strID = strID.Trim().ToUpper();
		if (strID.Length == 18)
		{
			// 確定第 1-17碼是數字。 
			if (Int64.TryParse(strID.Substring(0, 16), out ckint))
			{
				strCheck_Src = strID.Substring(17, 1);

				intSum = 0;

				// Sum(Ai×Wi)
				for (intIndex = 0; intIndex < 17; intIndex++)
				{
					intSum = intSum + int.Parse(strID.Substring(intIndex, 1)) * intWeight[intIndex];
				}

				// Sum(Ai×Wi) Mod 11
				intSum = intSum % 11;

				// 查表，尋找其相對值
				strCheck_Dst = strTable[intSum];

				// 比較檢查碼與相對值是否相同。 
				if (strCheck_Dst == strCheck_Src)
					chk_value = 0;
				else
					chk_value = 1;
			}
			else
				chk_value = 2;
		}
		else
		{
			if (strID.Length == 15)
			{
				// 確定1-15碼均是數字。 
				if (Int64.TryParse(strID, out ckint))
				{
					//檢查生日(7~12碼) 格式是否正確 
					strCheck_Src = "19" + strID.Substring(6, 2) + "/" + strID.Substring( 8, 2) + "/" + strID.Substring(10, 2);
					if (DateTime.TryParse(strCheck_Src, out ckdt))
						chk_value = 0;
					else
						chk_value = 4;
				}
				else
					chk_value = 2;
			}
			else
				chk_value = 3;
		}

		return chk_value;
	}
	#endregion

	#region Check_HK_ID() 驗證香港身份證號碼 (8碼)
	public int Check_HK_ID(string strID)
	{
		int chk_value = 0, ckint = -1;
		int intCheckSum = 0;			// 檢核碼變數 
		int intCount = 0;				// 計數變數 
		int ckcode = 0;					// 檢查碼
		char[] strAreaCode;				// 區域碼變數 

		strID = strID.Trim().ToUpper().Replace("(","").Replace(")","");

		// 確定香港身份證號碼有8碼，最後一碼為檢查碼
		if (strID.Length == 8)
		{
			// 取得首碼字母。 
			strAreaCode = strID.Substring(0, 1).ToCharArray();

			// 確定首碼在A-Z之間
			if (strAreaCode[0] < 65 || strAreaCode[0] > 90)
			{
				chk_value = 2;
			}

			// 確定2-7碼是數字
			if (chk_value == 0)
			{
				if (! int.TryParse(strID.Substring(1, 6), out ckint))
					chk_value = 3;

			}

			// 取得檢查碼
			if (chk_value == 0)
			{
				if (strID.Substring(7, 1) == "A")
					ckcode = 10;
				else if (!int.TryParse(strID.Substring(7, 1), out ckcode))
					chk_value = 4;
			}

			// 計算檢查碼
			if (chk_value == 0)
			{
				// 取得首碼之積
				intCheckSum = (strAreaCode[0] - 64) * 8;

				// 計算第二碼至第七碼之積。 
				for (intCount = 1; intCount < 7; intCount++)
				{
					intCheckSum += int.Parse(strID.Substring(intCount, 1)) * (8 - intCount);
				}

				// 加上檢查碼
				intCheckSum += ckcode;

				// 檢查是否為11整除。 
				if ((intCheckSum % 11 == 0))
					chk_value = 0;
				else
					chk_value = 4;
			}
		}
		else
			chk_value = 1;
		return chk_value;

	} 
	#endregion

	#region Check_TW_INV() 驗證台灣營利事業統一編號 (8碼)
	public int Check_TW_INV(string strID)
	{
		int chk_value = 0, ckint = 0;
		int[] intX = new int[8];
		int[] intY = new int[8];
		int intMod = 0;				// 餘數變數 
		int intSum = 0;				// 合計數變數 

		chk_value = 9;

		// 營利事業統一編號
		if (strID.Length == 8)
		{
			if (int.TryParse(strID, out ckint))
			{
				intX[0] = int.Parse(strID.Substring(0, 1)) * 1;		// 第 1位數 * 1 
				intX[1] = int.Parse(strID.Substring(1, 1)) * 2;		// 第 2位數 * 2 
				intX[2] = int.Parse(strID.Substring(2, 1)) * 1;		// 第 3位數 * 1 
				intX[3] = int.Parse(strID.Substring(3, 1)) * 2;		// 第 4位數 * 2 
				intX[4] = int.Parse(strID.Substring(4, 1)) * 1;		// 第 5位數 * 1 
				intX[5] = int.Parse(strID.Substring(5, 1)) * 2;		// 第 6位數 * 2 
				intX[6] = int.Parse(strID.Substring(6, 1)) * 4;		// 第 7位數 * 4 
				intX[7] = int.Parse(strID.Substring(7, 1)) * 1;		// 第 8位數 * 1 

				intY[0] = intX[1] / 10;								// 第 2位數的乘積可能大於10, 除以10, 取其整數 
				intY[1] = intX[1] % 10;								// 第 2位數的乘積可能大於10, 除以10, 取其餘數 
				intY[2] = intX[3] / 10;								// 第 4位數的乘積可能大於10, 除以10, 取其整數 
				intY[3] = intX[3] % 10;								// 第 4位數的乘積可能大於10, 除以10, 取其餘數 
				intY[4] = intX[5] / 10;								// 第 6位數的乘積可能大於10, 除以10, 取其整數 
				intY[5] = intX[5] % 10;								// 第 6位數的乘積可能大於10, 除以10, 取其餘數 
				intY[6] = intX[6] / 10;								// 第 7位數的乘積可能大於10, 除以10, 取其整數 
				intY[7] = intX[6] % 10;								// 第 7位數的乘積可能大於10, 除以10, 取其餘數 

				intSum = intX[0] + intX[2] + intX[4] + intX[7] + intY[0] + intY[1] + intY[2] + intY[3] + intY[4] + intY[5] + intY[6] + intY[7];
				intMod = intSum % 10;

				// 判斷 1: 第 7 位數是否為 7 時
				if (strID.Substring(6, 1) == "7")
				{
					// 判斷 2: 餘數是否為 0 
					if (intMod == 0)
						chk_value = 0;
					else
					{
						intSum = intSum + 1;

						// 再行計算 1999/11/19 修正 
						intMod = intSum % 10;
						if (intMod == 0)
							chk_value = 0;
						else
							chk_value = 1;
					}
				}
				else
				{
					if (intMod == 0)
						chk_value = 0;
					else
						chk_value = 1;
				}
			}
			else
				chk_value = 2;
		}
		else
			chk_value = 3;

		return chk_value;
	}
	#endregion

	#region Check_ISSN() 驗證國際標準期刊號碼 (ISSN) (13碼)
	public int Check_ISSN(string strISSN)
	{
		return Check_EAN13(strISSN);
	}
	#endregion

	#region Check_ISSN8() 驗證國際標準期刊號碼 (ISSN) (8碼)
	public int Check_ISSN8(string strISSN)
	{
		return Check_EAN8(strISSN);
	}
	#endregion

	#region Check_ISBN() 驗證國際標準書籍號碼 (ISBN) (13碼)
	public int Check_ISBN(string strISBN)
	{
		return Check_EAN13(strISBN);
	}
	#endregion

	#region Check_ISMN() 驗證國際標準樂譜號碼 (ISMN) (13碼)
	public int Check_ISMN(string strISMN)
	{
		return Check_EAN13(strISMN);
	}
	#endregion

	#region Check_EAN13() EAN-13碼
	public int Check_EAN13(string eancode)
	{
		int chk_value = 9, icnt = 0, cksum = 0;
		Int64 ckint = 0;

		if (eancode.Length == 13)
		{
			if (Int64.TryParse(eancode.Substring(0, 13), out ckint))
			{
				for (icnt = 1; icnt < 12; icnt += 2)
				{
					cksum += int.Parse(eancode.Substring(icnt, 1));
				}

				cksum *= 3;

				for (icnt = 0; icnt < 12; icnt += 2)
				{
					cksum += int.Parse(eancode.Substring(icnt, 1));
				}

				cksum = 10 - cksum % 10;
				if (cksum == 10)
					cksum = 0;

				if (cksum == int.Parse(eancode.Substring(12, 1)))
					chk_value = 0;
				else
					chk_value = 3;
			}
			else
				chk_value = 2;
		}
		else
			chk_value = 1;

		return chk_value;
	}
	#endregion

	#region Check_EAN8() EAN-8碼
	public int Check_EAN8(string eancode)
	{
		int chk_value = 9, icnt = 0, cksum = 0;
		Int64 ckint = 0;

		if (eancode.Length == 8)
		{
			if (Int64.TryParse(eancode.Substring(0, 8), out ckint))
			{
				for (icnt = 0; icnt < 7; icnt += 2)
				{
					cksum += int.Parse(eancode.Substring(icnt, 1));
				}

				cksum *= 3;

				for (icnt = 1; icnt < 7; icnt += 2)
				{
					cksum += int.Parse(eancode.Substring(icnt, 1));
				}

				cksum = 10 - cksum % 10;
				if (cksum == 10)
					cksum = 0;

				if (cksum == int.Parse(eancode.Substring( 7, 1)))
					chk_value = 0;
				else
					chk_value = 3;
			}
			else
				chk_value = 2;
		}
		else
			chk_value = 1;

		return chk_value;
	}
	#endregion
}