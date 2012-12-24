//---------------------------------------------------------------------------- 
// 程式功能	公用函數
// 各個屬性與函數的語法說明，請參閱第 25 章。
//---------------------------------------------------------------------------- 
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class Build_Word
{
	#region Word 檔案格式預設值
	private string _author = "陸士權";				// 作者
	private string _company = "章立民研究室";		// 公司
	private string _title = "專題實作範例-Word";	// 標題
	private string _width = "29.7cm";				// 寬 A3:29.7cm、A4:21.0cm、A5:14.8cm、B4(JIS):25.7cm、B5(JIS):18.2cm、Letter:21.59cm
	private string _height = "42.0cm";				// 高 A3:42.0cm、A4:29.7cm、A5:21.0cm、B4(JIS):36.4cm、B5(JIS):25.7cm、Letter:27.94cm
	private string _top = "2cm";					// 上邊界
	private string _bottom = "2cm";					// 下邊界
	private string _left = "2cm";					// 左邊界
	private string _right = "2cm";					// 右邊界
	private string _head = "1.5cm";					// 頁首距離
	private string _foot = "1.75cm";				// 頁尾距離
	private string _customerstyle = "";				// 自定格式 CSS
	private string _fsize = "12.0";					// 預設字型大小 (pt)
	private string _fname = "新細明體";				// 預設中文字型名稱
	private string _fename = "Times New Roman";		// 預設英文字型名稱

	private string _hstring = "專題實作範例";		// 頁首文字
	private string _hfsize = "8.0";					// 頁首字型大小 (pt)
	private string _hfname = "新細明體";			// 頁首中文字型名稱
	private string _hfename = "Times New Roman";	// 頁首英文字型名稱
	private string _halign = "right";				// 頁首文字位置

	private string _fstring = "專題實作範例";		// 頁尾文字
	private string _ffsize = "8.0";					// 頁尾字型大小 (pt)
	private string _ffname = "新細明體";			// 頁尾中文字型名稱
	private string _ffename = "Times New Roman";	// 頁尾英文字型名稱
	private string _falign = "right";				// 頁尾文字位置
	#endregion

	public string Author
	{
		set
		{
			this._author = value;
		}
		get
		{
			return _author;
		}
	}

	public string Company
	{
		set
		{
			this._company = value;
		}
		get
		{
			return _company;
		}
	}

	public string Title
	{
		set
		{
			this._title = value;
		}
		get
		{
			return _title;
		}
	}

	public string Width
	{
		get
		{
			return _width;
		}
	}

	public string Height
	{
		get
		{
			return _height;
		}
	}

	public string Top
	{
		get
		{
			return _top;
		}
	}

	public string Left
	{
		get
		{
			return _left;
		}
	}

	public string Bottom
	{
		get
		{
			return _bottom;
		}
	}

	public string Right
	{
		get
		{
			return _right;
		}
	}

	public string HeadString
	{
		get
		{
			return _hstring;
		}
	}

	public string HeadFontName
	{
		get
		{
			return _hfname;
		}
	}

	public string HeadFontEName
	{
		get
		{
			return _hfename;
		}
	}

	public float HeadFontSize
	{
		get
		{
			return float.Parse(_hfsize);
		}
	}

	public string HeadAlign
	{
		get
		{
			return _halign;
		}
	}

	public string HeadHeight
	{
		get
		{
			return _head;
		}
	}

	public string FootString
	{
		get
		{
			return _fstring;
		}
	}

	public string FootFontName
	{
		get
		{
			return _ffname;
		}
	}

	public string FootFontEName
	{
		get
		{
			return _ffename;
		}
	}

	public float FootFontSize
	{
		get
		{
			return float.Parse(_ffsize);
		}
	}

	public string FootAlign
	{
		get
		{
			return _falign;
		}
	}

	public string FootHeight
	{
		get
		{
			return _foot;
		}
	}

	public string DefineStyle
	{
		set
		{
			this._customerstyle = value;
		}
		get
		{
			return _customerstyle;
		}
	}

	public float FontSize
	{
		set
		{
			this._fsize = value.ToString();
		}
		get
		{
			return float.Parse(_fsize);
		}
	}

	public string FontName
	{
		set
		{
			this._fname = value;
		}
		get
		{
			return _fname;
		}
	}

	public string FontEName
	{
		set
		{
			this._fename = value;
		}
		get
		{
			return _fename;
		}
	}

	public bool SetPaper(string ptype, string pstyle) {
		bool ckbool = true;

		ptype = ptype.ToUpper();
		pstyle = pstyle.ToUpper();

		if (pstyle == "H" || pstyle == "S")
		{
			switch (ptype)
			{
				case "A3":
					if (pstyle == "S")	// 直式
					{
						_width = "29.7cm";
						_height = "42.0cm";
					}
					else
					{
						_width = "42.0cm";
						_height = "29.7cm";
					}
					break;

				case "A4":
					if (pstyle == "S")	// 直式
					{
						_width = "21.0cm";
						_height = "29.7cm";
					}
					else
					{
						_width = "29.7cm";
						_height = "21.0cm";
					}
					break;

				case "A5":
					if (pstyle == "S")	// 直式
					{
						_width = "14.8cm";
						_height = "21.0cm";
					}
					else
					{
						_width = "21.0cm";
						_height = "14.8cm";
					}
					break;

				case "B4":
					if (pstyle == "S")	// 直式
					{
						_width = "25.7cm";
						_height = "36.4cm";
					}
					else
					{
						_width = "36.4cm";
						_height = "25.7cm";
					}
					break;

				case "B5":
					if (pstyle == "S")	// 直式
					{
						_width = "18.2cm";
						_height = "26.7cm";
					}
					else
					{
						_width = "26.7cm";
						_height = "18.2cm";
					}
					break;

				case "LETTER":
					if (pstyle == "S")	// 直式
					{
						_width = "21.59cm";
						_height = "27.94cm";
					}
					else
					{
						_width = "27.94cm";
						_height = "21.59cm";
					}
					break;

				default:
					ckbool = false;
					break;
			}
		}
		else
			ckbool = false;

		return ckbool;
	}

	public bool CustomerPaper(float cwidth, float cheight, string cunit)
	{
		bool ckbool = true;

		cunit = cunit.ToLower();

		if (cunit == "in" || cunit == "cm" || cunit == "mm" || cunit == "pt")
		{
			_width = cwidth.ToString() + cunit;
			_height = cheight.ToString() + cunit;
		}
		else
			ckbool = false;

		return ckbool;
	}

	public void SetHeadString(string hstring, string hfname, string hfename, float hfsize, string halign)
	{
		_hstring = hstring;
		_hfname = hfname;
		_hfename = hfename;
		_hfsize = hfsize.ToString();

		halign = halign.ToLower();
		switch (halign)
		{
			case "center":
				_halign = "center";
				break;
			case "left":
				_halign = "left";
				break;
			case "right":
				_halign = "right";
				break;
			default:
				_halign = "right";
				break;
		}
	}

	public void SetFootString(string fstring, string ffname, string ffename, float ffsize, string falign)
	{
		_fstring = fstring;
		_ffname = ffname;
		_ffename = ffename;
		_ffsize = ffsize.ToString();

		falign = falign.ToLower();
		switch (falign)
		{
			case "center":
				_falign = "center";
				break;
			case "left":
				_falign = "left";
				break;
			case "right":
				_falign = "right";
				break;
			default:
				_falign = "right";
				break;
		}
	}

	public bool SetHeadHeight(float hheight, string hunit)
	{
		bool ckbool = false;

		hunit = hunit.ToLower();

		if (hunit == "in" || hunit == "cm" || hunit == "mm" || hunit == "pt")
		{
			_head = hheight.ToString() + hunit;
		}
		else
			ckbool = false;

		return ckbool;
	}

	public bool SetFootHeight(float fheight, string funit)
	{
		bool ckbool = false;

		funit = funit.ToLower();

		if (funit == "in" || funit == "cm" || funit == "mm" || funit == "pt")
		{
			_foot = fheight.ToString() + funit;
		}
		else
			ckbool = false;

		return ckbool;
	}

	public bool SetMagin(float stop, float sleft, float sbottom, float sright, string sunit)
	{
		bool ckbool = false;

		sunit = sunit.ToLower();

		if (sunit == "in" || sunit == "cm" || sunit == "mm" || sunit == "pt")
		{
			_top = stop.ToString() + sunit;
			_left = sleft.ToString() + sunit;
			_bottom = sbottom.ToString() + sunit;
			_right = sright.ToString() + sunit;
		}
		else
			ckbool = false;
		return ckbool;
	}

	public string HeadData()
	{
		StringBuilder hstr = new StringBuilder();

		hstr.AppendLine("MIME-Version: 1.0");
		hstr.AppendLine("Content-Type: multipart/related; boundary=\"----=_NextPart_01C9C813.F54FA0A0\"\r\n\r\n\r\n");
		hstr.AppendLine("------=_NextPart_01C9C813.F54FA0A0");
		hstr.AppendLine("Content-Location: file:///C:/AC484DE5/WordSample.htm");
		hstr.AppendLine("Content-Transfer-Encoding: quoted-printable");
		hstr.AppendLine("Content-Type: text/html; charset=\"utf-8\"\r\n");
		hstr.AppendLine("<html xmlns:v=3D\"urn:schemas-microsoft-com:vml\"");
		hstr.AppendLine("xmlns:o=3D\"urn:schemas-microsoft-com:office:office\"");
		hstr.AppendLine("xmlns:w=3D\"urn:schemas-microsoft-com:office:word\"");
		hstr.AppendLine("xmlns=3D\"http://www.w3.org/TR/REC-html40\">\r\n");
		hstr.AppendLine("<head>");
		hstr.AppendLine("<meta http-equiv=3DContent-Type content=3D\"text/html; charset=3Dutf-8\">");
		hstr.AppendLine("<meta name=3DProgId content=3DWord.Document>");
		hstr.AppendLine("<meta name=3DGenerator content=3D\"Microsoft Word 11\">");
		hstr.AppendLine("<meta name=3DOriginator content=3D\"Microsoft Word 11\">");
		hstr.AppendLine("<link rel=3DFile-List href=3D\"WordSample.files/filelist.xml\">");
		hstr.AppendLine("<title>" + _title + "</title>");
		hstr.AppendLine("<!--[if gte mso 9]><xml>");
		hstr.AppendLine("<o:DocumentProperties>");
		hstr.AppendLine("<o:Author>" + _author + "</o:Author>");
		hstr.AppendLine("<o:Company>" + _company + "</o:Company>");
		hstr.AppendLine("<o:Version>11.9999</o:Version>");
		hstr.AppendLine("</o:DocumentProperties>");
		hstr.AppendLine("<o:OfficeDocumentSettings>");
		hstr.AppendLine("<o:RelyOnVML/>");
		hstr.AppendLine("<o:AllowPNG/>");
		hstr.AppendLine("</o:OfficeDocumentSettings>");
		hstr.AppendLine("</xml><![endif]--><!--[if gte mso 9]><xml>");
		hstr.AppendLine("<w:WordDocument>");
		hstr.AppendLine("<w:View>Print</w:View>");
		hstr.AppendLine("<w:Zoom>BestFit</w:Zoom>");
		hstr.AppendLine("<w:PunctuationKerning/>");
		hstr.AppendLine("<w:DrawingGridHorizontalSpacing>6 pt</w:DrawingGridHorizontalSpacing>");
		hstr.AppendLine("<w:DisplayHorizontalDrawingGridEvery>0</w:DisplayHorizontalDrawingGridEvery>");
		hstr.AppendLine("<w:DisplayVerticalDrawingGridEvery>2</w:DisplayVerticalDrawingGridEvery>");
		hstr.AppendLine("<w:ValidateAgainstSchemas/>");
		hstr.AppendLine("<w:SaveIfXMLInvalid>false</w:SaveIfXMLInvalid>");
		hstr.AppendLine("<w:IgnoreMixedContent>false</w:IgnoreMixedContent>");
		hstr.AppendLine("<w:AlwaysShowPlaceholderText>false</w:AlwaysShowPlaceholderText>");
		hstr.AppendLine("<w:Compatibility>");
		hstr.AppendLine("<w:SpaceForUL/>");
		hstr.AppendLine("<w:BalanceSingleByteDoubleByteWidth/>");
		hstr.AppendLine("<w:DoNotLeaveBackslashAlone/>");
		hstr.AppendLine("<w:ULTrailSpace/>");
		hstr.AppendLine("<w:DoNotExpandShiftReturn/>");
		hstr.AppendLine("<w:AdjustLineHeightInTable/>");
		hstr.AppendLine("<w:BreakWrappedTables/>");
		hstr.AppendLine("<w:SnapToGridInCell/>");
		hstr.AppendLine("<w:WrapTextWithPunct/>");
		hstr.AppendLine("<w:UseAsianBreakRules/>");
		hstr.AppendLine("<w:DontGrowAutofit/>");
		hstr.AppendLine("<w:UseFELayout/>");
		hstr.AppendLine("</w:Compatibility>");
		hstr.AppendLine("<w:BrowserLevel>MicrosoftInternetExplorer4</w:BrowserLevel>");
		hstr.AppendLine("</w:WordDocument>");
		hstr.AppendLine("</xml><![endif]--><!--[if gte mso 9]><xml>");
		hstr.AppendLine("<w:LatentStyles DefLockedState=3D\"false\" LatentStyleCount=3D\"156\">");
		hstr.AppendLine("</w:LatentStyles>");
		hstr.AppendLine("</xml><![endif]-->");
		hstr.AppendLine("<style>");
		hstr.AppendLine("<!--");

		#region Font Definitions
		hstr.AppendLine("@font-face");
		hstr.AppendLine("{font-family:新細明體;");
		hstr.AppendLine("panose-1:2 2 3 0 0 0 0 0 0 0;");
		hstr.AppendLine("mso-font-alt:PMingLiU;");
		hstr.AppendLine("mso-font-charset:136;");
		hstr.AppendLine("mso-generic-font-family:roman;");
		hstr.AppendLine("mso-font-pitch:variable;");
		hstr.AppendLine("mso-font-signature:3 137232384 22 0 1048577 0;}");
		hstr.AppendLine("@font-face");
		hstr.AppendLine("{font-family:\"\\@新細明體\";");
		hstr.AppendLine("panose-1:2 2 3 0 0 0 0 0 0 0;");
		hstr.AppendLine("mso-font-charset:136;");
		hstr.AppendLine("mso-generic-font-family:roman;");
		hstr.AppendLine("mso-font-pitch:variable;");
		hstr.AppendLine("mso-font-signature:3 137232384 22 0 1048577 0;}");
		#endregion

		#region Style Definitions
		hstr.AppendLine("p.MsoNormal, li.MsoNormal, div.MsoNormal");
		hstr.AppendLine("{mso-style-parent:\"\";");
		hstr.AppendLine("margin:0cm;");
		hstr.AppendLine("margin-bottom:.0001pt;");
		hstr.AppendLine("mso-pagination:none;");
		hstr.AppendLine("font-size:" + _fsize+ "pt;");
		hstr.AppendLine("font-family:\"" + _fename + "\";");
		hstr.AppendLine("mso-fareast-font-family:" + _fname + ";");
		hstr.AppendLine("mso-font-kerning:1.0pt;}");
		hstr.AppendLine("p.MsoFooter, li.MsoFooter, div.MsoFooter");
		hstr.AppendLine("{margin:0cm;");
		hstr.AppendLine("margin-bottom:.0001pt;");
		hstr.AppendLine("mso-pagination:none;");
		hstr.AppendLine("tab-stops:center 207.65pt right 415.3pt;");
		hstr.AppendLine("layout-grid-mode:char;");
		hstr.AppendLine("font-size:10.0pt;");
		hstr.AppendLine("font-family:\"" + _fename + "\";");
		hstr.AppendLine("mso-fareast-font-family:" + _fname + ";");
		hstr.AppendLine("mso-font-kerning:1.0pt;}");
		if (_customerstyle != "")
			hstr.AppendLine(_customerstyle);
		#endregion

		#region Page Definitions
		hstr.AppendLine("@page");
		hstr.AppendLine("{mso-page-border-surround-header:no;");
		hstr.AppendLine("mso-page-border-surround-footer:no;");
		hstr.AppendLine("mso-footnote-separator:url(\"WordSample.files/header.htm\") fs;");
		hstr.AppendLine("mso-footnote-continuation-separator:url(\"WordSample.files/header.htm\") fcs;");
		hstr.AppendLine("mso-endnote-separator:url(\"WordSample.files/header.htm\") es;");
		hstr.AppendLine("mso-endnote-continuation-separator:url(\"WordSample.files/header.htm\") ecs;}");
		hstr.AppendLine("@page Section1");
		hstr.AppendLine("{size:" + _width + " " + _height + ";");
		hstr.AppendLine("margin:" + _top + " " + _left + " " + _bottom  + " " + _right + ";");
		hstr.AppendLine("mso-header-margin:" + _head + ";");
		hstr.AppendLine("mso-footer-margin:" + _foot + ";");
		hstr.AppendLine("mso-even-header:url(\"WordSample.files/header.htm\") eh1;");
		hstr.AppendLine("mso-header:url(\"WordSample.files/header.htm\") h1;");
		hstr.AppendLine("mso-even-footer:url(\"WordSample.files/header.htm\") ef1;");
		hstr.AppendLine("mso-footer:url(\"WordSample.files/header.htm\") f1;");
		hstr.AppendLine("mso-paper-source:0;}");
		hstr.AppendLine("div.Section1");
		hstr.AppendLine("{page:Section1;}");
		#endregion

		hstr.AppendLine("-->");
		hstr.AppendLine("</style>");
		hstr.AppendLine("<!--[if gte mso 10]>");

		#region Style Definitions
		hstr.AppendLine("<style>");
		hstr.AppendLine("table.MsoNormalTable");
		hstr.AppendLine("{mso-style-name:表格內文;");
		hstr.AppendLine("mso-tstyle-rowband-size:0;");
		hstr.AppendLine("mso-tstyle-colband-size:0;");
		hstr.AppendLine("mso-style-noshow:yes;");
		hstr.AppendLine("mso-style-parent:\"\";");
		hstr.AppendLine("mso-padding-alt:0cm 5.4pt 0cm 5.4pt;");
		hstr.AppendLine("mso-para-margin:0cm;");
		hstr.AppendLine("mso-para-margin-bottom:.0001pt;");
		hstr.AppendLine("mso-pagination:widow-orphan;");
		hstr.AppendLine("font-size:10.0pt;");
		hstr.AppendLine("font-family:\"Times New Roman\";");
		hstr.AppendLine("mso-fareast-font-family:\"Times New Roman\";");
		hstr.AppendLine("mso-ansi-language:#0400;");
		hstr.AppendLine("mso-fareast-language:#0400;");
		hstr.AppendLine("mso-bidi-language:#0400;}");
		hstr.AppendLine("</style>");
		#endregion

		hstr.AppendLine("<![endif]--><!--[if gte mso 9]><xml>");
		hstr.AppendLine("<o:shapedefaults v:ext=3D\"edit\" spidmax=3D\"3074\"/>");
		hstr.AppendLine("</xml><![endif]--><!--[if gte mso 9]><xml>");
		hstr.AppendLine("<o:shapelayout v:ext=3D\"edit\">");
		hstr.AppendLine("<o:idmap v:ext=3D\"edit\" data=3D\"1\"/>");
		hstr.AppendLine("</o:shapelayout></xml><![endif]-->");
		hstr.AppendLine("</head>");

		hstr.AppendLine("<body lang=3DZH-TW style=3D'tab-interval:24.0pt;text-justify-trim:punctuation'>");
		hstr.AppendLine("<div class=3DSection1>");

		return hstr.ToString();
	}

	public string FootData()
	{
		StringBuilder fstr = new StringBuilder();

		fstr.AppendLine("</div>");
		fstr.AppendLine("</body>");

		fstr.AppendLine("</html>\r\n");

		fstr.AppendLine("------=_NextPart_01C9C813.F54FA0A0");
		fstr.AppendLine("Content-Location: file:///C:/AC484DE5/WordSample.files/header.htm");
		fstr.AppendLine("Content-Transfer-Encoding: quoted-printable");
		fstr.AppendLine("Content-Type: text/html; charset=\"utf-8\"\r\n");
		fstr.AppendLine("<html xmlns:v=3D\"urn:schemas-microsoft-com:vml\"");
		fstr.AppendLine("xmlns:o=3D\"urn:schemas-microsoft-com:office:office\"");
		fstr.AppendLine("xmlns:w=3D\"urn:schemas-microsoft-com:office:word\"");
		fstr.AppendLine("xmlns=3D\"http://www.w3.org/TR/REC-html40\">\r\n");
		fstr.AppendLine("<head>");
		fstr.AppendLine("<meta http-equiv=3DContent-Type content=3D\"text/html; charset=3Dutf-8\">");
		fstr.AppendLine("<meta name=3DProgId content=3DWord.Document>");
		fstr.AppendLine("<meta name=3DGenerator content=3D\"Microsoft Word 11\">");
		fstr.AppendLine("<meta name=3DOriginator content=3D\"Microsoft Word 11\">");
		fstr.AppendLine("<link id=3DMain-File rel=3DMain-File href=3D\"../WordSample.htm\">");
		fstr.AppendLine("<![if IE]>");
		fstr.AppendLine("<base href=3D\"file:///C:\\AC484DE5\\WordSample.files\\header.htm\"");
		fstr.AppendLine("id=3D\"webarch_temp_base_tag\">");
		fstr.AppendLine("<![endif]>");
		fstr.AppendLine("</head>");
		fstr.AppendLine("<body lang=3DZH-TW>");
		fstr.AppendLine("<div style=3D'mso-element:footnote-separator' id=3Dfs>");
		fstr.AppendLine("<p class=3DMsoNormal><span lang=3DEN-US><span style=3D'mso-special-characte=");
		fstr.AppendLine("r:footnote-separator'><![if !supportFootnotes]>");
		fstr.AppendLine("<hr align=3Dleft size=3D1 width=3D\"33%\">");
		fstr.AppendLine("<![endif]></span></span></p>");
		fstr.AppendLine("</div>");
		fstr.AppendLine("<div style=3D'mso-element:footnote-continuation-separator' id=3Dfcs>");
		fstr.AppendLine("<p class=3DMsoNormal><span lang=3DEN-US><span style=3D'mso-special-characte=");
		fstr.AppendLine("r:footnote-continuation-separator'><![if !supportFootnotes]>");
		fstr.AppendLine("<hr align=3Dleft size=3D1>");
		fstr.AppendLine("<![endif]></span></span></p>");
		fstr.AppendLine("</div>");
		fstr.AppendLine("<div style=3D'mso-element:endnote-separator' id=3Des>");
		fstr.AppendLine("<p class=3DMsoNormal><span lang=3DEN-US><span style=3D'mso-special-characte=");
		fstr.AppendLine("r:footnote-separator'><![if !supportFootnotes]>");
		fstr.AppendLine("<hr align=3Dleft size=3D1 width=3D\"33%\">");
		fstr.AppendLine("<![endif]></span></span></p>");
		fstr.AppendLine("</div>");
		fstr.AppendLine("<div style=3D'mso-element:endnote-continuation-separator' id=3Decs>");
		fstr.AppendLine("<p class=3DMsoNormal><span lang=3DEN-US><span style=3D'mso-special-characte=");
		fstr.AppendLine("r:footnote-continuation-separator'><![if !supportFootnotes]>");
		fstr.AppendLine("<hr align=3Dleft size=3D1>");
		fstr.AppendLine("<![endif]></span></span></p>");
		fstr.AppendLine("</div>");
		fstr.AppendLine("<div style=3D'mso-element:header' id=3Deh1>");
		fstr.AppendLine("<p class=3DMsoHeader><span lang=3DEN-US><o:p>&nbsp;</o:p></span></p>");
		fstr.AppendLine("</div>");
		fstr.AppendLine("<div style=3D'mso-element:header' id=3Dh1>");
		fstr.AppendLine("<p class=3DMsoHeader align=3Dright style=3D'text-align:" + _halign + "'><span style=");
		fstr.AppendLine("=3D'font-size:");
		fstr.AppendLine(_hfsize + "pt;font-family:" + _hfname + ";mso-ascii-font-family:\"" + _hfename + "\";mso-=");
		fstr.AppendLine("hansi-font-family:");
		fstr.AppendLine("\"" + _hfename + "\"'>" + _hstring + "</span><span lang=3DEN-US style=3D'font-size:=");
		fstr.AppendLine(_hfsize + "pt'><o:p></o:p></span></p>");
		fstr.AppendLine("</div>");
		fstr.AppendLine("<div style=3D'mso-element:footer' id=3Def1>");
		fstr.AppendLine("<div style=3D'mso-element:frame;mso-element-wrap:around;mso-element-anchor-=");
		fstr.AppendLine("vertical:");
		fstr.AppendLine("paragraph;mso-element-anchor-horizontal:margin;mso-element-left:center;");
		fstr.AppendLine("mso-element-top:.05pt;mso-height-rule:exactly'>");
		fstr.AppendLine("<table cellspacing=3D0 cellpadding=3D0 hspace=3D0 vspace=3D0 align=3Dcenter>");
		fstr.AppendLine("<tr>");
		fstr.AppendLine("<td valign=3Dtop align=3Dleft style=3D'padding-top:0cm;padding-right:0cm;");
		fstr.AppendLine("padding-bottom:0cm;padding-left:0cm'>");
		fstr.AppendLine("<p class=3DMsoFooter style=3D'mso-element:frame;mso-element-wrap:around;");
		fstr.AppendLine("mso-element-anchor-vertical:paragraph;mso-element-anchor-horizontal:margin;");
		fstr.AppendLine("mso-element-left:center;mso-element-top:.05pt;mso-height-rule:exactly'><!=");
		fstr.AppendLine("--[if supportFields]><span");
		fstr.AppendLine("class=3DMsoPageNumber><span lang=3DEN-US><span style=3D'mso-element:field=");
		fstr.AppendLine("-begin'></span>PAGE<span");
		fstr.AppendLine("style=3D'mso-spacerun:yes'>  </span></span></span><![endif]--><!--[if su=");
		fstr.AppendLine("pportFields]><span");
		fstr.AppendLine("class=3DMsoPageNumber><span lang=3DEN-US><span style=3D'mso-element:field=");
		fstr.AppendLine("-end'></span></span></span><![endif]--><span");
		fstr.AppendLine("class=3DMsoPageNumber><span lang=3DEN-US><o:p></o:p></span></span></p>");
		fstr.AppendLine("</td>");
		fstr.AppendLine("</tr>");
		fstr.AppendLine("</table>");
		fstr.AppendLine("</div>");
		fstr.AppendLine("<p class=3DMsoFooter><span lang=3DEN-US><o:p>&nbsp;</o:p></span></p>");
		fstr.AppendLine("</div>");
		fstr.AppendLine("<div style=3D'mso-element:footer' id=3Df1>");
		fstr.AppendLine("<p class=3DMsoFooter align=3Dright style=3D'text-align:" + _falign + "'><span style=");
		fstr.AppendLine("=3D'font-size:");
		fstr.AppendLine(_ffsize + "pt;font-family:" + _ffname + ";mso-ascii-font-family:\"" + _ffename + "\";mso-=");
		fstr.AppendLine("hansi-font-family:");
		fstr.AppendLine("\"" + _ffename + "\"'>" + _fstring + "</span><span lang=3DEN-US style=3D'font-size:=");
		fstr.AppendLine(_ffsize + "pt'><o:p></o:p></span></p>");
		fstr.AppendLine("</div>");
		fstr.AppendLine("<div style=3D'mso-element:header' id=3Dfh1>");
		fstr.AppendLine("<p class=3DMsoHeader><span lang=3DEN-US><o:p>&nbsp;</o:p></span></p>");
		fstr.AppendLine("</div>");
		fstr.AppendLine("<div style=3D'mso-element:footer' id=3Dff1>");
		fstr.AppendLine("<p class=3DMsoFooter><span lang=3DEN-US><o:p>&nbsp;</o:p></span></p>");
		fstr.AppendLine("</div>");
		fstr.AppendLine("</body>");
		fstr.AppendLine("</html>");

		fstr.AppendLine("------=_NextPart_01C9C813.F54FA0A0");
		fstr.AppendLine("Content-Location: file:///C:/AC484DE5/WordSample.files/filelist.xml");
		fstr.AppendLine("Content-Transfer-Encoding: quoted-printable");
		fstr.AppendLine("Content-Type: text/xml; charset=\"utf-8\"\r\n");
		fstr.AppendLine("<xml xmlns:o=3D\"urn:schemas-microsoft-com:office:office\">");
		fstr.AppendLine("<o:MainFile HRef=3D\"../WordSample.htm\"/>");
		fstr.AppendLine("<o:File HRef=3D\"header.htm\"/>");
		fstr.AppendLine("<o:File HRef=3D\"filelist.xml\"/>");
		fstr.AppendLine("</xml>");
		fstr.AppendLine("------=_NextPart_01C9C813.F54FA0A0--");

		return fstr.ToString();
	}
}
