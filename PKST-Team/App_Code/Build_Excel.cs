//---------------------------------------------------------------------------- 
//程式功能	公用函數(各個屬性與函數的語法說明，請參閱第 25 章。)
//備註說明	支援 MS Excel XP 及以上版本 (MS Excel 2000 SP1 即可使用，但有部份設定無效)
//			本範例以 Excel 2003 為標準範本設計
//			產生以 UniCode 編碼的 Excel 檔案
//---------------------------------------------------------------------------- 
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class Build_Excel
{
	#region Excel 檔案格式預設值
	private string _author = "陸士權";				// 作者
	private string _company = "章立民工作室";		// 公司
	private string _title = "專題實作範例-Excel";	// 標題
	private string _subject = "專題實作範例-Excel";	// 主旨
	private string _sheet = "專題實作範例";			// 工作表名稱 (Sheet)

	private string _top = ".79in";					// 上邊界
	private string _bottom = ".79in";				// 下邊界
	private string _left = ".79in";					// 左邊界
	private string _right = ".79in";				// 右邊界
	private string _head = ".59in";					// 頁首距離
	private string _foot = ".71in";					// 頁尾距離

	private string _customerstyle = "";				// 自定格式 CSS
	private string _fsize = "12.0";					// 預設字型大小 (pt)
	private string _fname = "新細明體";				// 預設中文字型名稱

	private string _paper = "A4";					// 紙張格式
	private string _paperindex = "9";				// 紙張格式 Letter=L A3=8 A4=9 A5=11 B4=12 B5=13
	private bool _landscape = false;				// 橫印 (true:橫印 false:直印)
	private bool _pagealign  = true;				// 頁面水平置中
	private bool _pagevalign = false;				// 頁面垂直置中

	private string _hstring = "專題實作範例";		// 頁首文字
	private string _hfsize = "8";					// 頁首字型大小 (pt)
	private string _hfname = "新細明體";			// 頁首中文字型名稱
	private string _halign = "R";					// 頁首文字位置

	private string _fstring = "&P\\/&N";			// 頁尾文字 (頁碼/總頁碼) 
	private string _ffsize = "8";					// 頁尾字型大小 (pt)
	private string _ffname = "新細明體";			// 頁尾中文字型名稱
	private string _falign = "C";					// 頁尾文字位置
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

	public string Subject
	{
		set
		{
			this._subject = value;
		}
		get
		{
			return _subject;
		}
	}

	public string Sheet
	{
		set
		{
			this._sheet = value;
		}
		get
		{
			return _sheet;
		}
	}

	public bool Page_Align
	{
		set
		{
			this._pagealign = value;
		}
		get
		{
			return _pagealign;
		}
	}

	public bool Page_VAlign
	{
		set
		{
			this._pagevalign = value;
		}
		get
		{
			return _pagevalign;
		}
	}

	public string Paper
	{
		get
		{
			return _paper;
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

	public bool SetPaper(string ptype, string pstyle) {
		bool ckbool = true;

		ptype = ptype.ToUpper();
		pstyle = pstyle.ToUpper();

		if (pstyle == "H" || pstyle == "S")
		{
			switch (ptype)
			{
				case "A3":
					_paper = "A3";
					_paperindex = "8";
					break;

				case "A4":
					_paper = "A4";
					_paperindex = "9";
					break;

				case "A5":
					_paper = "A5";
					_paperindex = "10";
					break;

				case "B4":
					_paper = "B4";
					_paperindex = "12";
					break;

				case "B5":
					_paper = "B5";
					_paperindex = "13";
					break;

				case "LETTER":
					_paper = "LETTER";
					_paperindex = "L";
					break;

				default:
					ckbool = false;
					break;
			}

			if (ckbool)
			{
				if (pstyle == "H")
					_landscape = true;
				else
					_landscape = false;
			}
		}
		else
			ckbool = false;

		return ckbool;
	}

	public void SetHeadString(string hstring, string hfname, float hfsize, string halign)
	{
		_hstring = hstring;
		_hfname = hfname;
		_hfsize = hfsize.ToString();

		halign = halign.ToLower();
		switch (halign)
		{
			case "center":
				_halign = "C";
				break;
			case "left":
				_halign = "L";
				break;
			case "right":
				_halign = "R";
				break;
			default:
				_halign = "R";
				break;
		}
	}

	public void SetFootString(string fstring, string ffname, float ffsize, string falign)
	{
		_fstring = fstring;
		_ffname = ffname;
		_ffsize = ffsize.ToString();

		falign = falign.ToLower();
		switch (falign)
		{
			case "center":
				_falign = "C";
				break;
			case "left":
				_falign = "L";
				break;
			case "right":
				_falign = "R";
				break;
			default:
				_falign = "R";
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

		hstr.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
		hstr.AppendLine("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
		hstr.AppendLine("xmlns=\"http://www.w3.org/TR/REC-html40\">");
		hstr.AppendLine("<head>");
		hstr.AppendLine("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
		hstr.AppendLine("<meta name=ProgId content=Excel.Sheet>");
		hstr.AppendLine("<meta name=Generator content=\"Microsoft Excel 11\">");
		hstr.AppendLine("<title>" + _title +"</title>");
		hstr.AppendLine("<!--[if gte mso 9]><xml>");
 		hstr.AppendLine("<o:DocumentProperties>");
 		hstr.AppendLine("<o:Subject>" + _subject + "</o:Subject>");
 		hstr.AppendLine("<o:Author>" + _author + "</o:Author>");
 		hstr.AppendLine("<o:Company>" + _company + "</o:Company>");
 		hstr.AppendLine("<o:Version>11.9999</o:Version>");
 		hstr.AppendLine("</o:DocumentProperties>");
 		hstr.AppendLine("<o:OfficeDocumentSettings>");
 		hstr.AppendLine("<o:RelyOnVML/>");
 		hstr.AppendLine("<o:AllowPNG/>");
 		hstr.AppendLine("</o:OfficeDocumentSettings>");
 		hstr.AppendLine("</xml><![endif]-->");
 		hstr.AppendLine("<style>");
 		hstr.AppendLine("<!--table");
 		hstr.AppendLine("{mso-displayed-decimal-separator:\"\\.\";");
 		hstr.AppendLine("mso-displayed-thousand-separator:\"\\,\";}");
 		hstr.AppendLine("@page");

		if (_hstring != "")
		{
			// 頁首文字
			hstr.AppendLine("{mso-header-data:\"&" + _halign + "&\\0022" + _hfname + "\\,標準\\0022&" + _hfsize + _hstring + "\";");
		}

		if (_fstring != "")
		{
			// 頁尾文字
			hstr.AppendLine("mso-footer-data:\"&" + _falign + "&\\0022" + _ffname + "\\,標準\\0022&" + _ffsize + _fstring + "\";");
		}

 		hstr.AppendLine("margin:" + _top + " " + _left + " " + _bottom + " " + _right + ";\");");

		if (_landscape)
		{
			hstr.AppendLine("mso-page-orientation:landscape;");			// 橫印
		}

		if (_pagealign) {
 			hstr.AppendLine("mso-horizontal-page-align:center;");		// 水平置中
		}

		if (_pagevalign) {
			hstr.AppendLine("mso-vertical-page-align:center;");			// 垂直置中
		}

		hstr.AppendLine("mso-header-margin:" + _head);
 		hstr.AppendLine("mso-footer-margin:" + _foot + "}");
 		hstr.AppendLine("tr");
 		hstr.AppendLine("{mso-height-source:auto;");
 		hstr.AppendLine("mso-ruby-visibility:none;}");
 		hstr.AppendLine("col");
 		hstr.AppendLine("{mso-width-source:auto;");
 		hstr.AppendLine("mso-ruby-visibility:none;}");
 		hstr.AppendLine("br");
 		hstr.AppendLine("{mso-data-placement:same-cell;}");
 		hstr.AppendLine(".style0");
 		hstr.AppendLine("{mso-number-format:General;");
 		hstr.AppendLine("text-align:general;");
 		hstr.AppendLine("vertical-align:middle;");
 		hstr.AppendLine("white-space:nowrap;");
 		hstr.AppendLine("mso-rotate:0;");
 		hstr.AppendLine("mso-background-source:auto;");
 		hstr.AppendLine("mso-pattern:auto;");
 		hstr.AppendLine("color:windowtext;");
 		hstr.AppendLine("font-size:" + _fsize + "pt;");
 		hstr.AppendLine("font-weight:400;");
 		hstr.AppendLine("font-style:normal;");
 		hstr.AppendLine("text-decoration:none;");
 		hstr.AppendLine("font-family:" + _fname + ", serif;");
 		hstr.AppendLine("mso-font-charset:136;");
 		hstr.AppendLine("border:none;");
 		hstr.AppendLine("mso-protection:locked visible;");
 		hstr.AppendLine("mso-style-name:一般;");
 		hstr.AppendLine("mso-style-id:0;}");
 		hstr.AppendLine("td");
 		hstr.AppendLine("{mso-style-parent:style0;");
 		hstr.AppendLine("padding-top:1px;");
 		hstr.AppendLine("padding-right:1px;");
 		hstr.AppendLine("padding-left:1px;");
 		hstr.AppendLine("mso-ignore:padding;");
 		hstr.AppendLine("color:windowtext;");
 		hstr.AppendLine("font-size:" + _fsize + "pt;");
 		hstr.AppendLine("font-weight:400;");
 		hstr.AppendLine("font-style:normal;");
 		hstr.AppendLine("text-decoration:none;");
 		hstr.AppendLine("font-family:" + _fname + ", serif;");
 		hstr.AppendLine("mso-font-charset:136;");
 		hstr.AppendLine("mso-number-format:General;");
 		hstr.AppendLine("text-align:general;");
 		hstr.AppendLine("vertical-align:middle;");
 		hstr.AppendLine("border:none;");
 		hstr.AppendLine("mso-background-source:auto;");
 		hstr.AppendLine("mso-pattern:auto;");
 		hstr.AppendLine("mso-protection:locked visible;");
 		hstr.AppendLine("white-space:nowrap;");
 		hstr.AppendLine("mso-rotate:0;}");
		hstr.AppendLine(_customerstyle);			// 自訂 CSS 格式
 		hstr.AppendLine("ruby");
 		hstr.AppendLine("{ruby-align:left;}");
 		hstr.AppendLine("rt");
 		hstr.AppendLine("{color:windowtext;");
 		hstr.AppendLine("font-size:9.0pt;");
 		hstr.AppendLine("font-weight:400;");
 		hstr.AppendLine("font-style:normal;");
 		hstr.AppendLine("text-decoration:none;");
 		hstr.AppendLine("font-family:" + _fname + ", serif;");
 		hstr.AppendLine("mso-font-charset:136;");
 		hstr.AppendLine("mso-char-type:none;");
 		hstr.AppendLine("display:none;}");
 		hstr.AppendLine("-->");
 		hstr.AppendLine("</style>");
 		hstr.AppendLine("<!--[if gte mso 9]><xml>");
 		hstr.AppendLine("<x:ExcelWorkbook>");
 		hstr.AppendLine("<x:ExcelWorksheets>");
 		hstr.AppendLine("<x:ExcelWorksheet>");
 		hstr.AppendLine("<x:Name>" + _sheet + "</x:Name>");
 		hstr.AppendLine("<x:WorksheetOptions>");
 		hstr.AppendLine("<x:DefaultRowHeight>330</x:DefaultRowHeight>");
 		hstr.AppendLine("<x:FitToPage/>");
 		hstr.AppendLine("<x:FitToPage/>");
 		hstr.AppendLine("<x:Print>");
 		hstr.AppendLine("<x:FitHeight>1000</x:FitHeight>");
 		hstr.AppendLine("<x:ValidPrinterInfo/>");

		if (_paperindex != "L")
		{
			hstr.AppendLine("<x:PaperSizeIndex>" + _paperindex + "</x:PaperSizeIndex>");			// 紙張格式
		}

 		hstr.AppendLine("<x:HorizontalResolution>600</x:HorizontalResolution>");
 		hstr.AppendLine("<x:VerticalResolution>600</x:VerticalResolution>");
 		hstr.AppendLine("<x:NumberofCopies>0</x:NumberofCopies>");
 		hstr.AppendLine("</x:Print>");
 		hstr.AppendLine("<x:Selected/>");
 		hstr.AppendLine("<x:Panes>");
 		hstr.AppendLine("<x:Pane>");
 		hstr.AppendLine("<x:Number>3</x:Number>");
 		//hstr.AppendLine("<x:RangeSelection>$A$1:$A$1</x:RangeSelection>");
 		hstr.AppendLine("</x:Pane>");
 		hstr.AppendLine("</x:Panes>");
 		hstr.AppendLine("<x:ProtectContents>False</x:ProtectContents>");
 		hstr.AppendLine("<x:ProtectObjects>False</x:ProtectObjects>");
 		hstr.AppendLine("<x:ProtectScenarios>False</x:ProtectScenarios>");
 		hstr.AppendLine("</x:WorksheetOptions>");
 		hstr.AppendLine("</x:ExcelWorksheet>");
 		hstr.AppendLine("</x:ExcelWorksheets>");
 		//hstr.AppendLine("<x:WindowHeight>12690</x:WindowHeight>");
 		//hstr.AppendLine("<x:WindowWidth>18180</x:WindowWidth>");
 		//hstr.AppendLine("<x:WindowTopX>480</x:WindowTopX>");
 		//hstr.AppendLine("<x:WindowTopY>105</x:WindowTopY>");
 		hstr.AppendLine("<x:ProtectStructure>False</x:ProtectStructure>");
 		hstr.AppendLine("<x:ProtectWindows>False</x:ProtectWindows>");
 		hstr.AppendLine("</x:ExcelWorkbook>");
 		hstr.AppendLine("</xml><![endif]-->");
 		hstr.AppendLine("</head>");
 		hstr.AppendLine("<body link=blue vlink=purple>");

		return hstr.ToString();
	}

	public string FootData()
	{
		StringBuilder fstr = new StringBuilder();

		fstr.AppendLine("</body>");
		fstr.AppendLine("</html>");

		return fstr.ToString();
	}
}
