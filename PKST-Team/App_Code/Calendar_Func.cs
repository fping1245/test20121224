//---------------------------------------------------------------------------- 
//程式功能	曆法函數
//備註說明	需配合 Common_Func.cs 使用
//---------------------------------------------------------------------------- 
using System;
using System.Globalization;

public class Calendar_Func
{
	#region GetHeavenlyStem() 以數字取得天干 (甲、乙、丙...)
	// 函數清單 GetHeavenlyStem(int)
	// 傳入參數	int		順序
	// 傳回參數	string	天干
	public string GetHeavenlyStem(int iStem)
	{
		string HeavenlyStem = "甲乙丙丁戊己庚辛壬癸";			// 10 天干
		string cStem = "";

		if (iStem < 1 || iStem > 10)
			cStem = "？";
		else
			cStem = HeavenlyStem.Substring(iStem - 1, 1);

		return cStem;
	}
	#endregion

	#region GetEarthlyBranch() 以數字取地支 (子、丑、寅...)
	// 函數清單 GetEarthlyBranch(int)
	// 傳入參數	int		順序
	// 傳回參數	string	地支
	public string GetEarthlyBranch(int iBranch)
	{
		string EarthlyBranch = "子丑寅卯辰巳午未申酉戌亥";		// 12 地支
		string cBranch = "";

		if (iBranch < 1 || iBranch > 12)
			cBranch = "？";
		else
			cBranch = EarthlyBranch.Substring(iBranch - 1, 1);

		return cBranch;
	}
	#endregion

	#region GetLunarZodiac() 以數字取生肖 (鼠、牛、虎...)
	// 函數清單 GetLunarZodiac(int)
	// 傳入參數	int		順序
	// 傳回參數	string	生肖
	public string GetLunarZodiac(int iNum)
	{
		string LunarZodiac = "鼠牛虎兔龍蛇馬羊猴雞狗豬";
		string cZodiac = "";

		if (iNum < 1 || iNum > 12)
			cZodiac = "？";
		else
			cZodiac = LunarZodiac.Substring(iNum - 1, 1);

		return cZodiac;
	}
	#endregion

	#region GetChMonth() 以數字取得農曆月份 (正、二....)
	// 函數清單 GetChMonth(int)
	// 傳入參數	int		順序
	// 傳回參數	string	月份
	public string GetChMonth(int iMonth)
	{
		String_Func sfc = new String_Func();

		string cMonth = "";

		if (iMonth < 1 || iMonth > 12)
		{
			cMonth = "？";
		}
		else
		{
			if (iMonth == 1)
			{
				cMonth = "正";
			}
			else
			{
				cMonth = sfc.GetChNumber(iMonth);

				if (iMonth < 11)
				{
					cMonth = sfc.Right(cMonth, 1);
				}
				else
				{
					cMonth = sfc.Right(cMonth, 2);
				}
			}
		}

		return cMonth;
	}
	#endregion

	#region GetChDay() 以數字取得農曆日期 (初一、十一、廿二、三十....)
	// 函數清單 GetChDay(int)
	// 傳入參數	int		日期數字
	// 傳回參數	string	農曆日期
	public string GetChDay(int iDay)
	{
		String_Func sfc = new String_Func();

		string cDay = "";

		if (iDay < 1 || iDay > 31)
		{
			cDay = "不明";
		}
		else
		{
			cDay = sfc.GetChNumber((ulong)iDay);

			if (iDay < 10)
			{
				cDay = "初" + sfc.Left(cDay, 1);
			}
			else if (iDay == 10)
			{
				cDay = "初十";
			}
			else if (iDay > 10 && iDay < 20)
			{
				cDay = cDay.Replace("一十", "十");
			}
			else if (iDay > 20 && iDay < 30)
			{
				cDay = cDay.Replace("二十", "廿");
			}
			else if (iDay > 30)
				cDay = cDay.Replace("三十", "卅");
		}

		return cDay;
	}
	#endregion

	#region GetChHour() 以小時對映農曆時辰 (子、丑、寅....)
	// 函數清單 GetChHour(int)
	// 傳入參數	int		小時
	// 傳回參數	string	農曆時辰
	// 備註說明	子時是否為一日之始，眾說紛云。有所謂以子時為一日之始，亦有夜子時歸前一日及早子時歸次一日的說法，
	//			此處用夜子時及早子時之的分法，以便與西洋的24小時制接軌。
	public string GetChHour(int iHour)
	{
		string cHour = "";

		if (iHour < 0 || iHour > 23)
		{
			cHour = "不明";
		}
		else
		{
			if (iHour >= 23)
			{
				iHour = 1;
			}
			else
			{
				iHour = (iHour + 1) / 2 + 1;
			}

			cHour = GetEarthlyBranch(iHour);
		}

		return cHour;
	}
	#endregion

	#region GetChNHour() 以數字取得小時 (一、二、十一....)
	// 函數清單 GetChNHour(int)
	// 傳入參數	int		順序
	// 傳回參數	string	月份
	public string GetChNHour(int iHour)
	{
		String_Func sfc = new String_Func();

		string cHour = "";

		if (iHour < 0 || iHour > 23)
		{
			cHour = "？";
		}
		else
		{
			cHour = sfc.GetChNumber(iHour);
			cHour.Replace("一十","十");
		}

		return cHour;
	}
	#endregion

	#region GetLunarDate() 以西元日期時間換算農曆日期時間
	// 多載清單 GetLunarDate()					今天的完整農曆日期時間
	//			GetLunarDate(string)			今天的指定格式農曆日期時間
	//			GetLunarDate(DateTime)			指定日期的完整農曆日期時間
	//			GetLunarDate(DateTime, string)	指定日期及格式的農曆日期時間
	// 輸入參數	DateTime	西元日期
	//			string		格式
	//						y:年	例：甲子年
	//						M:月	例：十月
	//						d:日	例：廿一日
	//						H:時	例：辰時
	//						h:時	例：十一時
	//						k:刻	例：上三刻、下四刻
	//						m:分	例：十二分
	//						s:秒	例：三十秒
	// 傳回數值	string		農曆日期字串 (例：甲子年十月廿一日辰時十二分三十秒)
	// 備註說明	時辰跟分秒混用會造成時間的混亂，請自行注意。(一個時辰＝兩個小時，而此處的「分」是60分鐘的表示法)
	//			子時是否為一日之始，眾說紛云。有所謂以子時為一日之始，亦有夜子時歸前一日及早子時歸次一日的說法，
	//			此處用夜子時及早子時之的分法，以便與西洋的24小時制接軌。
	public string GetLunarDate()
	{
		return GetLunarDate(DateTime.Now, "yMdhms");
	}

	public string GetLunarDate(string mtype)
	{
		return GetLunarDate(DateTime.Now, mtype);
	}

	public string GetLunarDate(DateTime mdate)
	{
		return GetLunarDate(mdate, "yMdhms");
	}

	public string GetLunarDate(DateTime mdate, string mtype)
	{
		String_Func sfc = new String_Func();
		TaiwanLunisolarCalendar tlc = new TaiwanLunisolarCalendar();

		string ldate = "";
		int LunarYear = 0;										// 農曆年
		int LunarMonth = 0;										// 月份
		int LunarDay = 0;										// 日期
		int LunarHour = 0;										// 時
		int LunarMin = 0;										// 分
		int LunarSec = 0;										// 秒
		int LeapMonth = 0;										// 潤月

		LunarYear = tlc.GetSexagenaryYear(mdate);				// 取得西元年

		#region 農曆年
		if (mtype.Contains("y"))
		{
			ldate = GetHeavenlyStem(tlc.GetCelestialStem(LunarYear));					// 年 - 天干
			ldate += GetEarthlyBranch(tlc.GetTerrestrialBranch(LunarYear)) + "年";		// 年 - 地支
		}
		#endregion

		#region 農曆月
		if (mtype.Contains("M"))
		{
			LunarMonth = tlc.GetMonth(mdate);						// 取得月份
			LeapMonth = tlc.GetLeapMonth(tlc.GetYear(mdate));		// 取得潤月

			if (LeapMonth > 0)
			{
				// 當年有潤月，月份會出現13個月，在潤月之後的月分要減一。
				if (LeapMonth == LunarMonth)
				{
					ldate += "閏" + GetChMonth(LeapMonth - 1) + "月";
				}
				else if (LunarMonth > LeapMonth)
				{
					ldate += GetChMonth(LunarMonth - 1) + "月";
				}
				else
					ldate += GetChMonth(LunarMonth) + "月";
			}
			else
				ldate += GetChMonth(LunarMonth) + "月";
		}
		#endregion

		#region 農曆日
		if (mtype.Contains("d"))
		{
			LunarDay = tlc.GetDayOfMonth(mdate);

			ldate += GetChDay(LunarDay) + "日";
		}
		#endregion

		#region 農曆時 (子、丑...)
		if (mtype.Contains("H"))
		{
			LunarHour = tlc.GetHour(mdate);
			ldate += GetChHour(LunarHour) + "時";
		}
		#endregion

		#region 中文數字時 (五、十一...)
		if (mtype.Contains("h"))
		{
			LunarHour = tlc.GetHour(mdate);
			ldate += GetChNHour(LunarHour) + "時";
		}
		#endregion

		#region 農曆分
		if (mtype.Contains("m"))
		{
			LunarMin = tlc.GetMinute(mdate);
			ldate += sfc.GetChNumber((ulong)LunarMin).Replace("一十", "十") + "分";
		}
		#endregion

		#region 農曆秒
		if (mtype.Contains("s"))
		{
			LunarSec = tlc.GetSecond(mdate);
			if (LunarSec == 0)
			{
				ldate += "整";
			}
			else
			{
				ldate += sfc.GetChNumber((ulong)LunarSec).Replace("一十", "十") + "秒";
			}
		}
		#endregion

		return ldate;
	}
	#endregion

	#region GetDateLunarZodiac() 以西元日期取得農曆生肖
	// 多載清單 GetDateLunarZodiac()			今天的農曆生肖
	//			GetDateLunarZodiac(DateTime)	指定日期的農曆生肖
	// 輸入參數	DateTime	西元日期
	// 傳回數值	string		農曆生肖
	public string GetDateLunarZodiac()
	{
		return GetDateLunarZodiac(DateTime.Now);
	}

	public string GetDateLunarZodiac(DateTime mdate)
	{
		TaiwanLunisolarCalendar tlc = new TaiwanLunisolarCalendar();

		return GetLunarZodiac(tlc.GetTerrestrialBranch(tlc.GetSexagenaryYear(mdate)));
	}
	#endregion

	#region GetConstellation() 以西元日期取得西洋星座
	// 多載清單 GetConstellation()			今天的西洋星座
	//			GetConstellation(DateTime)	指定日期的西洋星座
	// 輸入參數	DateTime	日期
	// 傳回數值	string		西洋星座
	public string GetConstellation()
	{
		return GetConstellation(DateTime.Now);
	}

	public string GetConstellation(DateTime mDate)
	{
		string cConstellation = "";
		string[] Constellation = {"牡羊座","金牛座","雙子座","巨蟹座","獅子座","處女座"
									 ,"天秤座","天蠍座","射手座","魔羯座","寶瓶座","雙魚座","不明"};
		int mCnt = 0;

		mCnt = mDate.Month * 100 + mDate.Day;
		if (mCnt > 320 && mCnt < 421)
		{
			cConstellation = Constellation[0];
		}
		else if (mCnt > 420 && mCnt < 522)
		{
			cConstellation = Constellation[1];
		}
		else if (mCnt > 521 && mCnt < 622)
		{
			cConstellation = Constellation[2];
		}
		else if (mCnt > 621 && mCnt < 724)
		{
			cConstellation = Constellation[3];
		}
		else if (mCnt > 723 && mCnt < 824)
		{
			cConstellation = Constellation[4];
		}
		else if (mCnt > 823 && mCnt < 924)
		{
			cConstellation = Constellation[5];
		}
		else if (mCnt > 923 && mCnt < 1024)
		{
			cConstellation = Constellation[6];
		}
		else if (mCnt > 1023 && mCnt < 1123)
		{
			cConstellation = Constellation[7];
		}
		else if (mCnt > 1123 && mCnt < 1223)
		{
			cConstellation = Constellation[8];
		}
		else if (mCnt > 1222 || mCnt < 121)
		{
			cConstellation = Constellation[9];
		}
		else if (mCnt > 120 && mCnt < 220)
		{
			cConstellation = Constellation[10];
		}
		else if (mCnt > 219 && mCnt < 321)
		{
			cConstellation = Constellation[11];
		}
		else
		{
			cConstellation = Constellation[12];
		}

		return cConstellation;
	}
	#endregion
}
