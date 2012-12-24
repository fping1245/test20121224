//---------------------------------------------------------------------------- 
//專案名稱	公用函數
//程式功能	Email 格式解碼 (eml 格式解析)
//備註說明	需配合 CodeBase64.cs、QuotedPrintable.cs 使用
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

public class Email_Decode
{
	#region 變數定義
	private CodeBase64 cb64 = new CodeBase64();
	private QuotedPrintable qupr = new QuotedPrintable();

	private string _mail_source = "";					// 郵件原始資料
	private string _mail_subject = "";					// 郵件標題
	private string _mail_from = "";						// 寄件者姓名
	private string _mail_fmail = "";					// 寄件者郵箱
	private string _mail_ftime = "";					// 寄信時間
	private string _mail_to = "";						// 收件者姓名
	private string _mail_tmail = "";					// 收件者郵箱
	private string _mail_ttime = "";					// 收信時間
	private string _mail_body_TEXT = "";				// 郵件TEXT內容
	private string _mail_body_HTML = "";				// 郵件HTML內容
	private string _mail_body_type = "";				// 郵件內文型態 HTML or TEXT or MIXED

	private int _mail_attach_num = 0;					// 郵件附件數量
	private string[] _mail_attach_name;					// 郵件附件檔名
	private string[] _mail_attach_type;					// 郵件附件格式
	private string[] _mail_attach_encode;				// 郵件附件編碼方式
	private string[] _mail_attach_codepage;				// 郵件附件CodePage
	private string[] _mail_attach_begin;				// 郵附件起始位置
	private string[] _mail_attach_end;					// 郵附件結束位置

	#endregion

	public string Mail_Source
	{
		set
		{
			this._mail_source = value.Trim();
			Body_Analytic();
		}
		get
		{
			return _mail_source;
		}
	}

	public string Mail_Topic
	{
		set
		{
			Topic_Analytic(value);	// 設定完成後即進行解析
		}
	}

	public string Mail_Subject
	{
		get
		{
			return _mail_subject;
		}
	}

	public string Mail_From_Name
	{
		get
		{
			return _mail_from;
		}
	}

	public string Mail_From_EMail
	{
		get
		{
			return _mail_fmail;
		}
	}

	public string Mail_From_Time
	{
		get
		{
			return _mail_ftime;
		}
	}

	public string Mail_To_Name
	{
		get
		{
			return _mail_to;
		}
	}

	public string Mail_To_EMail
	{
		get
		{
			return _mail_tmail;
		}
	}

	public string Mail_To_Time
	{
		get
		{
			return _mail_ttime;
		}
	}

	public string Mail_Body_Type
	{
		get
		{
			return _mail_body_type;
		}
	}

	public string Mail_Body
	{
		get
		{
			if (_mail_body_HTML != "")
				return _mail_body_HTML;
			else
				return _mail_body_TEXT;
		}
	}

	public string Mail_Body_TEXT
	{
		get
		{
			return _mail_body_TEXT;
		}
	}

	public string Mail_Body_HTML
	{
		get
		{
			return _mail_body_HTML;
		}
	}

	public int Attach_Num
	{
		get
		{
			return _mail_attach_num;
		}
	}

	public string FileName(int fcnt)
	{
		string fstr = "";

		if (_mail_attach_name != null)
		{
			if (fcnt <= _mail_attach_name.Length && fcnt > -1)
			{
				fstr = _mail_attach_name[fcnt];
			}
		}
		return fstr;
	}

	public string FileType(int fcnt)
	{
		string fstr = "";

		if (_mail_attach_type != null)
		{
			if (fcnt <= _mail_attach_type.Length && fcnt > -1)
			{
				fstr = _mail_attach_type[fcnt];
			}
		}
		return fstr;
	}

	public string FileEnCode(int fcnt)
	{
		string fstr = "";

		if (_mail_attach_encode != null)
		{
			if (fcnt <= _mail_attach_encode.Length && fcnt > -1)
			{
				fstr = _mail_attach_encode[fcnt];
			}
		}
		return fstr;
	}

	public int FileCodePage(int fcnt)
	{
		int fint = 0;

		if (_mail_attach_codepage != null)
		{
			if (fcnt <= _mail_attach_codepage.Length && fcnt > -1)
			{
				fint = int.Parse(_mail_attach_codepage[fcnt]);
			}
		}
		return fint;
	}

	public int FileBegin(int fcnt)
	{
		int fint = 0;

		if (_mail_attach_begin != null)
		{
			if (fcnt <= _mail_attach_begin.Length && fcnt > -1)
			{
				fint = int.Parse(_mail_attach_begin[fcnt]);
			}
		}
		return fint;
	}

	public int FileEnd(int fcnt)
	{
		int fint = 0;

		if (_mail_attach_end != null)
		{
			if (fcnt <= _mail_attach_end.Length && fcnt > -1)
			{
				fint = int.Parse(_mail_attach_end[fcnt]);
			}
		}
		return fint;
	}

	public byte[] File(int fcnt)
	{
		int bcnt = 0, ecnt = 0, codepage = 0;
		string fdata = "";
		byte[] fbyte = null;

		if (_mail_attach_begin != null && _mail_attach_end != null && _mail_attach_codepage != null && _mail_attach_encode != null)
		{
			if (fcnt > -1 && _mail_attach_begin.Length >= fcnt && _mail_attach_end.Length >= fcnt
				&& _mail_attach_codepage.Length >= fcnt && _mail_attach_encode.Length >= fcnt)
			{
				if (_mail_attach_codepage[fcnt] == "")
					codepage = 65001;
				else
					codepage = int.Parse(_mail_attach_codepage[fcnt]);
				bcnt = int.Parse(_mail_attach_begin[fcnt]);
				ecnt = int.Parse(_mail_attach_end[fcnt]);

				if (ecnt > bcnt)
				{
					fdata = _mail_source.Substring(bcnt, ecnt - bcnt);

					switch (_mail_attach_encode[fcnt])
					{
						case "base64":
							fbyte = Convert.FromBase64String(fdata);
							break;
						case "quoted-printable":
							fdata = qupr.DeCodeQuoted(codepage, fdata);
							fbyte = System.Text.Encoding.Default.GetBytes(fdata);
							break;
						default:
							fbyte = System.Text.Encoding.Default.GetBytes("");
							break;
					}
				}
			}
		}

		return fbyte;
	}

	private string ToDTStr(string s_source)
	{
		string s_target = "";
		DateTime dt_target;

		string[] tmp_array = s_source.Split(' ');

		if (tmp_array.Length > 5)			// 有星期的日期格式
		{
			s_target = tmp_array[3] + "/" + MonthToNum(tmp_array[2]).ToString() + "/" + tmp_array[1] + " " + tmp_array[4];
			if (DateTime.TryParse(s_target, out dt_target))
			{
				s_target = dt_target.ToString("yyyy/MM/dd HH:mm:ss");
			}
			else
				s_target = "";
		}
		else if (tmp_array.Length > 4)		// 沒有星期的日期格式
		{
			s_target = tmp_array[2] + "/" + MonthToNum(tmp_array[1]).ToString() + "/" + tmp_array[0] + " " + tmp_array[3];
			if (DateTime.TryParse(s_target, out dt_target))
			{
				s_target = dt_target.ToString("yyyy/MM/dd HH:mm:ss");
			}
			else
				s_target = "";
		}

		return s_target;
	}

	private int MonthToNum(string str_month)
	{
		int int_month = 0;

		if (str_month.Length >= 3)
		{
			str_month = str_month.Substring(0, 3).ToLower();

			switch (str_month)
			{
				case "jan":
					int_month = 1;
					break;
				case "feb":
					int_month = 2;
					break;
				case "mar":
					int_month = 3;
					break;
				case "apr":
					int_month = 4;
					break;
				case "may":
					int_month = 5;
					break;
				case "jun":
					int_month = 6;
					break;
				case "jul":
					int_month = 7;
					break;
				case "aug":
					int_month = 8;
					break;
				case "sep":
					int_month = 9;
					break;
				case "oct":
					int_month = 10;
					break;
				case "nov":
					int_month = 11;
					break;
				case "dec":
					int_month = 12;
					break;
				default:
					int_month = 0;
					break;
			}
		}
		else
			int_month = 0;

		return int_month;
	}

	public void Topic_Analytic(string mail_topic)
	{
		int bcnt = 0, ecnt = 0, scnt = 0, code_page = 0;
		string tmpstr = "", tmpstr2 = "", page_name = "", codetype = "B";

		#region 擷取寄信時間
		_mail_ftime = "";

		bcnt = mail_topic.IndexOf("\r\nDate: ");
		if (bcnt > -1)
		{
			bcnt += 8;
			ecnt = mail_topic.IndexOf("\r\n", bcnt);

			if (ecnt > -1)
				_mail_ftime = ToDTStr(mail_topic.Substring(bcnt, ecnt - bcnt));
		}
		#endregion
	
		#region 擷取寄件者姓名及信箱
		_mail_from = "";
		_mail_fmail = "";

		bcnt = mail_topic.IndexOf("\r\nFrom: ");
		if (bcnt > -1)
		{
			bcnt += 8;
			ecnt = mail_topic.IndexOf("\r\n", bcnt);
			if (ecnt > -1)
			{
				tmpstr = mail_topic.Substring(bcnt, ecnt - bcnt);

				#region 取得寄件者信箱
				bcnt = tmpstr.IndexOf("<");
				ecnt = tmpstr.IndexOf(">");
				if (bcnt > -1 && ecnt > -1)
				{
					bcnt++;
					ecnt--;
					if (ecnt > bcnt)
						_mail_fmail = tmpstr.Substring(bcnt, ecnt - bcnt + 1);
					bcnt -= 2;
					if (bcnt > 0 && ecnt > (bcnt + 2))
						tmpstr = tmpstr.Remove(bcnt, ecnt - bcnt + 2);
					else
						tmpstr = "";
				}
				#endregion

				#region 取得寄件者姓名
				_mail_from = _mail_fmail;

				if (tmpstr.Length > 2)
				{
					if (tmpstr.Substring(0, 2) == "=?")
					{
						// 寄件者姓名用 Base64 編碼
						bcnt = 2;
						ecnt = tmpstr.IndexOf("?B?");
						if (ecnt > -1)
						{
							page_name = tmpstr.Substring(bcnt, ecnt - 2);

							code_page = cb64.GetCodePage(page_name);

							// CodePage 正確
							if (code_page > 0)
							{
								bcnt = ecnt + 3;
								ecnt = tmpstr.IndexOf("?=", bcnt);

								if (ecnt > 0)
								{
									tmpstr = tmpstr.Substring(bcnt, ecnt - bcnt);

									tmpstr = cb64.DeCodeBase64(code_page, tmpstr);

									if (tmpstr != "")
										_mail_from = tmpstr;
								}
							}
						}
					}
					else
					{
						if (tmpstr.Substring(0, 1) == "\"")
						{
							// 沒有編碼
							ecnt = tmpstr.IndexOf("\"", 1);
							if (ecnt > -1)
								_mail_from = tmpstr.Substring(1, ecnt - 1);
						}
						else if (tmpstr.Trim() != "")
							_mail_from = tmpstr.Trim();
					}
				}
				#endregion
			}
		}
		#endregion

		#region 擷取收信時間
		_mail_ttime = "";

		bcnt = mail_topic.IndexOf("\r\n\tfor ");
		if (bcnt > -1)
		{
			bcnt = mail_topic.IndexOf("; ", bcnt);

			if (bcnt > -1)
			{
				bcnt += 2;
				ecnt = mail_topic.IndexOf(")\r\n", bcnt);
				if (ecnt > -1)
				{
					_mail_ttime = ToDTStr(mail_topic.Substring(bcnt, ecnt - bcnt + 1));
				}
			}
		}
		#endregion

		#region 擷取收件者姓名及信箱
		_mail_to = "";
		_mail_tmail = "";

		bcnt = mail_topic.IndexOf("\r\nTo: ");
		if (bcnt > -1)
		{
			bcnt += 6;
			ecnt = mail_topic.IndexOf("\r\n", bcnt);
			if (ecnt > -1)
			{
				tmpstr = mail_topic.Substring(bcnt, ecnt - bcnt);

				#region 取得收件者信箱
				bcnt = tmpstr.IndexOf("<");
				ecnt = tmpstr.IndexOf(">");
				if (bcnt > -1 && ecnt > -1)
				{
					bcnt++;
					ecnt--;
					if (ecnt > bcnt)
						_mail_tmail = tmpstr.Substring(bcnt, ecnt - bcnt + 1);
					bcnt -= 2;
					if (bcnt > 0 && ecnt > (bcnt + 2))
						tmpstr = tmpstr.Remove(bcnt, ecnt - bcnt + 2);
					else
						tmpstr = "";
				}
				#endregion

				#region 取得收件者姓名
				_mail_to = _mail_tmail;

				if (tmpstr.Length > 2)
				{
					if (tmpstr.Substring(0, 2) == "=?")
					{
						// 收件者姓名用 Base64 編碼
						bcnt = 2;
						ecnt = tmpstr.IndexOf("?B?");
						if (ecnt > -1)
						{
							page_name = tmpstr.Substring(bcnt, ecnt - 2);

							code_page = cb64.GetCodePage(page_name);

							// CodePage 正確
							if (code_page > 0)
							{
								bcnt = ecnt + 3;
								ecnt = tmpstr.IndexOf("?=", bcnt);

								if (ecnt > 0)
								{
									tmpstr = tmpstr.Substring(bcnt, ecnt - bcnt);

									tmpstr = cb64.DeCodeBase64(code_page, tmpstr);

									if (tmpstr != "")
										_mail_to = tmpstr;
								}
							}
						}
					}
					else
					{
						if (tmpstr.Substring(0, 1) == "\"")
						{
							// 沒有編碼
							ecnt = tmpstr.IndexOf("\"", 1);
							if (ecnt > -1)
								_mail_to = tmpstr.Substring(1, ecnt - 1);
						}
						else if (tmpstr.Trim() != "")
							_mail_to = tmpstr.Trim();
					}
				}
				#endregion
			}
		}
		#endregion

		#region 取得郵件標題
		_mail_subject = "";

		bcnt = mail_topic.IndexOf("\r\nSubject: ");
		if (bcnt > -1)
		{
			bcnt += 11;
			scnt = mail_topic.IndexOf("\r\n", bcnt);
			tmpstr = mail_topic.Substring(bcnt, scnt - bcnt);

			if (tmpstr.Substring(0, 2) == "=?")
			{
				while (tmpstr.Substring(0, 2) == "=?")
				{
					// 檢查郵件標題用 Base64 編碼 或 QuotedPrintable 編碼

					bcnt = 2;
					codetype = "B";
					ecnt = tmpstr.IndexOf("?B?");
					if (ecnt < 0)
					{
						codetype = "Q";
						ecnt = tmpstr.IndexOf("?Q?");
					}

					if (ecnt > -1)
					{
						page_name = tmpstr.Substring(bcnt, ecnt - bcnt);

						if (codetype == "B")
							code_page = cb64.GetCodePage(page_name);
						else
							code_page = qupr.GetCodePage(page_name);

						// CodePage 正確
						if (code_page > 0)
						{
							bcnt = ecnt + 3;
							ecnt = tmpstr.IndexOf("?=", bcnt);

							if (ecnt > 0)
							{
								tmpstr2 = tmpstr.Substring(bcnt, ecnt - bcnt).Replace("====","==");

								if (codetype == "B")
									tmpstr2 = cb64.DeCodeBase64(code_page, tmpstr2);
								else
								{
									tmpstr2 = qupr.DeCodeQuoted(code_page, tmpstr2);
									tmpstr2 = tmpstr2.Replace("_", " ");
								}

								if (tmpstr2 != "")
									_mail_subject += tmpstr2;
							}
							else
								_mail_subject += tmpstr.Substring(bcnt, ecnt - bcnt).Trim();
						}
						else
						{
							if ((ecnt - bcnt) > 0)
								_mail_subject += tmpstr.Substring(bcnt, ecnt - bcnt).Trim();
							else
								_mail_subject += "(none)";
						}

						if (mail_topic.Substring(scnt + 2, 3) == "\t=?" || mail_topic.Substring(scnt + 2, 3) == " =?")
						{
							bcnt = scnt + 3;
							scnt = mail_topic.IndexOf("\r\n", bcnt);
							if (scnt > bcnt)
							{
								tmpstr = mail_topic.Substring(bcnt, scnt - bcnt).Trim();
							}
							else
								tmpstr = "xx1";
						}
						else
							tmpstr = "xx2";
					}
				}
			}
			else
			{
				// 沒有編碼
				_mail_subject = tmpstr;
			}
		}
		#endregion
	}

	public void Body_Analytic()
	{
		int scnt = 0, bcnt = 0, ecnt = 0, fcnt = 0, icnt = 0;
		string page_name = "", dcode = "", fmark = "", tmpdata = "", boundstr = "";
		string fname = "", fbegin = "", fend = "", ftype = "", fencode = "", fcodepage = "";

		#region 郵件內容相關資料先清空
		_mail_body_HTML = "";
		_mail_body_TEXT = "";
		_mail_body_type = "";
		_mail_attach_num = 0;
		#endregion

		#region 尋找是否有附件，若有則找出附件起始標籤
		scnt = _mail_source.IndexOf("Content-Type: multipart/mixed");

		if (scnt > 0)
		{
			scnt += 29;
			bcnt = _mail_source.IndexOf("boundary=", scnt);
			if (bcnt > 0)
			{
				bcnt += 9;
				ecnt = _mail_source.IndexOf("\r\n", bcnt);
				if (ecnt - bcnt > 0)
				{
					fmark = _mail_source.Substring(bcnt, ecnt - bcnt).Replace("\"","").Replace(" ","");
				}
			}
		}
		#endregion

		#region 尋找郵件格式 HTML or TEXT or MIXED
		scnt = _mail_source.IndexOf("Content-Type: multipart/alternative");

		if (scnt > 0)
		{
			scnt += 35;

			_mail_body_type = "MIXED";

			bcnt = _mail_source.IndexOf("boundary=", scnt);
			if (bcnt > 0)
			{
				bcnt += 9;
				ecnt = _mail_source.IndexOf("\r\n", bcnt);

				if (ecnt - bcnt > 0)
				{
					boundstr = _mail_source.Substring(bcnt, ecnt - bcnt);
					boundstr = boundstr.Replace(" ", "").Replace("\"", "");
				}

				scnt = ecnt + 2;
			}
			else
				boundstr = "--";
		}
		else
		{
			scnt = _mail_source.IndexOf("\r\nContent-Type: ");

			if (_mail_source.Substring(scnt + 16, 15) == "multipart/mixed")
			{
				// 非所要找的資料，繼續找下一個符合的條件
				scnt = _mail_source.IndexOf("\r\nContent-Type: ", scnt + 16);
			}

			if (scnt > 0)
			{
				ecnt = _mail_source.IndexOf("\r\n", scnt + 2);

				if (ecnt - scnt > 16)
				{
					bcnt = scnt + 16;

					#region 取得郵件內容格式、CodePage
					if (_mail_source.Substring(bcnt, 9) == "text/html")
					{
						_mail_body_type = "HTML";

						bcnt = _mail_source.IndexOf("charset", bcnt);
						if (bcnt > 0 && ecnt - bcnt > 8)
						{
							bcnt += 8;
							page_name = _mail_source.Substring(bcnt, ecnt - bcnt).Replace("=", "").Replace(" ", "").Replace("\"", "");
						}						
					}
					else if (_mail_source.Substring(bcnt, 10) == "text/plain")
					{
						_mail_body_type = "TEXT";

						bcnt = _mail_source.IndexOf("charset", bcnt);
						if (bcnt > 0 && ecnt - bcnt > 8)
						{
							bcnt += 8;
							page_name = _mail_source.Substring(bcnt, ecnt - bcnt).Replace("=","").Replace(" ","").Replace("\"","");
						}
					}
					#endregion
				}
			}
		}
		#endregion

		#region 解析郵件內容
		if (_mail_body_type == "TEXT" || _mail_body_type == "HTML")
		{
			#region 內文格式為 TEXT 和 HTML 的處理方式

			#region 取得編碼方式
			bcnt = _mail_source.IndexOf("Content-Transfer-Encoding:");

			if (bcnt > 0)
			{
				ecnt = _mail_source.IndexOf("\r\n", bcnt);
				if (ecnt > 0)
				{
					bcnt += 27;
					dcode = _mail_source.Substring(bcnt, ecnt - bcnt);
				}
			}
			#endregion

			#region 取得資料並解碼
			bcnt = _mail_source.IndexOf("\r\n\r\n", bcnt);
			if (bcnt > 0)
			{
				bcnt += 4;
				if (fmark == "")
					ecnt = _mail_source.Length;
				else
				{
					ecnt = _mail_source.IndexOf("\r\n--" + fmark, bcnt);
					if (ecnt < 0)
						ecnt = _mail_source.Length;
				}
				tmpdata = _mail_source.Substring(bcnt, ecnt - bcnt);
				tmpdata = body_decode(tmpdata, page_name, dcode);

				if (_mail_body_type == "TEXT")
					_mail_body_TEXT = tmpdata.Trim();
				else
					_mail_body_HTML = tmpdata.Trim();
			}
			#endregion

			#endregion
		}
		else
		{
			#region 內文格式為 MIXED 的處理方式
			scnt = _mail_source.IndexOf(boundstr, scnt);

			if (bcnt > 0)
			{
				scnt += boundstr.Length;

				#region 取得 TEXT 的內文
				bcnt = _mail_source.IndexOf("Content-Type: text/plain;", scnt);

				if (bcnt > 0)
				{
					tmpdata = _mail_source.Substring(bcnt + 20, 60);
					icnt = tmpdata.IndexOf("charset");
					if (icnt > 0)
					{
						icnt += 8;

						#region 取得 CodePage
						ecnt = tmpdata.IndexOf("\r\n", icnt);
						page_name = tmpdata.Substring(icnt, ecnt - icnt);
						page_name = page_name.Replace("\"", "").Replace(" ", "").Replace("=", "").Replace("\"", "");
						#endregion

						#region 取得編碼方式
						bcnt += 27;
						bcnt = _mail_source.IndexOf("Content-Transfer-Encoding:", bcnt);
						if (bcnt > 0)
						{
							ecnt = _mail_source.IndexOf("\r\n", bcnt);
							if (ecnt > 0)
							{
								bcnt += 27;
								dcode = _mail_source.Substring(bcnt, ecnt - bcnt);
							}
						}
						else
							dcode = "quoted-printable";
						#endregion

						#region 取得內文，並解碼
						bcnt = _mail_source.IndexOf("\r\n\r\n", ecnt);

						if (bcnt > 0)
						{
							bcnt += 4;
							ecnt = _mail_source.IndexOf("\r\n--" + boundstr, bcnt);

							if (ecnt > bcnt)
							{
								tmpdata = _mail_source.Substring(bcnt, ecnt - bcnt);
								tmpdata = body_decode(tmpdata, page_name, dcode);

								_mail_body_TEXT = tmpdata.Trim();
							}
						}
						#endregion
					}
				}
				#endregion

				#region 取得 HTML 的內文
				bcnt = _mail_source.IndexOf("Content-Type: text/html;", scnt);

				if (bcnt > 0)
				{
					tmpdata = _mail_source.Substring(bcnt + 20, 60);
					icnt = tmpdata.IndexOf("charset=");
					if (icnt < 0)
					{
						icnt = tmpdata.IndexOf("charset = ");
						if (icnt > 0)
							icnt = icnt + 11;
						else
							icnt = -1;
					}
					else
						icnt = icnt + 9;

					if (icnt > 0)
					{
						#region 取得 CodePage
						ecnt = tmpdata.IndexOf("\r\n", icnt);
						page_name = tmpdata.Substring(icnt, ecnt - icnt);
						page_name = page_name.Replace("\"", "");
						#endregion

						#region 取得編碼方式
						bcnt += 27;
						bcnt = _mail_source.IndexOf("Content-Transfer-Encoding:", bcnt);
						if (bcnt > 0)
						{
							ecnt = _mail_source.IndexOf("\r\n", bcnt);
							if (ecnt > 0)
							{
								bcnt += 27;
								dcode = _mail_source.Substring(bcnt, ecnt - bcnt);
							}
						}
						else
							dcode = "quoted-printable";
						#endregion

						#region 取得內文，並解碼
						bcnt = _mail_source.IndexOf("\r\n\r\n", bcnt);
						if (bcnt > 0)
						{
							bcnt += 4;
							ecnt = _mail_source.IndexOf("\r\n--" + boundstr, bcnt);

							if (ecnt > bcnt)
							{
								tmpdata = _mail_source.Substring(bcnt, ecnt - bcnt);
								_mail_body_HTML = tmpdata;

								tmpdata = body_decode(tmpdata, page_name, dcode);

								if (tmpdata != "")
									_mail_body_HTML = tmpdata.Trim();
							}
						}
						#endregion
					}
				}
				#endregion
			}
			#endregion
		}
		#endregion

		#region 附加檔案處理 (計算附檔數量、檔案名稱及起始位置)
		if (fmark != "")
		{
			fcnt = 0;
			fname = "";
			fbegin = "";

			scnt = _mail_source.IndexOf(fmark, bcnt);
			while (scnt > 0)
			{
				scnt += fmark.Length;

				// 計算附檔數量
				fcnt++;

				#region 取得檔案格式
				bcnt = _mail_source.IndexOf("Content-Type: ", scnt);

				if (bcnt > 0)
				{
					bcnt += 14;
					ecnt = _mail_source.IndexOf("\r\n", bcnt);
					ftype += _mail_source.Substring(bcnt, ecnt - bcnt).Replace(";","") + ";";
				}
				else
					ftype += ";";
				#endregion

				#region 取得編碼方式
				bcnt = _mail_source.IndexOf("Content-Transfer-Encoding: ", scnt);
				if (bcnt > 0)
				{
					bcnt += 27;
					ecnt = _mail_source.IndexOf("\r\n", bcnt);
					fencode += _mail_source.Substring(bcnt, ecnt - bcnt) + ";";
				}
				else
					fencode += ";";
				#endregion

				#region 取得檔名及起訖位置
				// OutLook or OutLook Express
				bcnt = _mail_source.IndexOf("\r\n\tfilename=\"", scnt);
				if (bcnt > 0)
					bcnt += 13;
				else
				{
					// Hotmail
					bcnt = _mail_source.IndexOf("; filename=\"", scnt);
					if (bcnt > 0)
						bcnt += 12;
				}

				if (bcnt > 0)
				{
					ecnt = _mail_source.IndexOf("\r\n", bcnt);
					tmpdata = _mail_source.Substring(bcnt, ecnt - bcnt);
					tmpdata = tmpdata.Replace("\"", "");

					fname += tmpdata.Trim() + ";";
					fcodepage += topic_codepage(tmpdata).ToString() + ";";

					ecnt = _mail_source.IndexOf("\r\n\r\n", bcnt);
					fbegin += (ecnt + 4).ToString() + ";";
					fend += _mail_source.IndexOf("\r\n--" + fmark, ecnt + fmark.Length).ToString() + ";";
				}
				else
				{
					fname += ";";
					fcodepage += ";";
					fbegin += "0;";
					fend += "0;";
				}
				#endregion

				scnt = _mail_source.IndexOf(fmark, scnt);
				if (scnt + fmark.Length + 5 >= _mail_source.Length)
					scnt = 0;
			}

			#region 將取得的資料分存到指定位置
			_mail_attach_num = fcnt;
			_mail_attach_type = ftype.Split(';');
			_mail_attach_encode = fencode.Split(';');
			_mail_attach_codepage = fcodepage.Split(';');
			_mail_attach_begin = fbegin.Split(';');
			_mail_attach_end = fend.Split(';');
			_mail_attach_name = fname.Split(';');

			// 檔名解碼
			for (icnt = 0; icnt < fcnt; icnt++)
			{
				_mail_attach_name[icnt] = topic_decode(_mail_attach_name[icnt]).Replace("\0","");
			}
			#endregion
		}
		else
		{
			#region 沒有附加檔案
			_mail_attach_num = 0;
			_mail_attach_type = null;
			_mail_attach_encode = null;
			_mail_attach_codepage = null;
			_mail_attach_begin = null;
			_mail_attach_end = null;
			_mail_attach_name = null;
			#endregion
		}
		#endregion
	}

	private string body_decode(string bodystr, string page_name, string codetype)
	{
		int codepage = cb64.GetCodePage(page_name);

		if (codepage > 65535 || codepage < 0)
			codepage = 65001;

		switch (codetype)
		{
			case "base64":
				bodystr = cb64.DeCodeBase64(codepage, bodystr);
				break;
			case "quoted-printable":
				bodystr = qupr.DeCodeQuoted(codepage, bodystr);
				break;
		}

		return bodystr;
	}

	private string topic_decode(string topstr)
	{
		string page_name = "", codetype = "B";
		int bcnt = 0, ecnt = 0, codepage = 0;

		if (topstr.Length > 5)
		{
			if (topstr.Substring(0, 2) == "=?")
			{
				bcnt = 2;
				ecnt = topstr.IndexOf("?B?");
				if (ecnt < 0)
				{
					ecnt = topstr.IndexOf("?Q?");
					codetype = "Q";
				}

				page_name = topstr.Substring(bcnt, ecnt - bcnt);
				codepage = cb64.GetCodePage(page_name);

				bcnt = ecnt + 3;
				ecnt = topstr.IndexOf("?=", bcnt);

				topstr = topstr.Substring(bcnt, ecnt - bcnt);

				switch (codetype)
				{
					case "B":
						topstr = cb64.DeCodeBase64(codepage, topstr);
						break;
					case "Q":
						topstr = qupr.DeCodeQuoted(codepage, topstr);
						break;
				}
			}
		}

		return topstr;
	}

	private int topic_codepage(string topstr)
	{
		string page_name = "";
		int bcnt = 0, ecnt = 0, codepage = 0;

		if (topstr.Substring(0, 2) == "=?")
		{
			bcnt = 2;
			ecnt = topstr.IndexOf("?B?");
			if (ecnt < 0)
				ecnt = topstr.IndexOf("?Q?");

			if (ecnt > bcnt)
			{
				page_name = topstr.Substring(bcnt, ecnt - bcnt);
				codepage = cb64.GetCodePage(page_name);
			}
			else
				codepage = 65001;

		}
		else
			codepage = 65001;

		return codepage;
	}
}