function fnBnDown(btn) {
	btn.style.borderTopColor = "#efece8";
	btn.style.borderBottomColor = "#888070";
	btn.style.borderLeftColor = "#efece8";
	btn.style.borderRightColor = "#888070";
}

function fnBnClick(btn) {
	btn.style.borderTopColor = "#888070";
	btn.style.borderBottomColor = "#efece8";
	btn.style.borderLeftColor = "#888070";
	btn.style.borderRightColor = "#efece8";
}

function fnBnUp(btn) {
	btn.style.borderColor = "#d9cec4";
}

// 觸發編輯區
function Editor_focus() {
	var ifobj = document.getElementById("if_editor");
	ifobj.contentWindow.focus();
}

// 剪下、複製、刪除、貼上、還原
function fnModify(mAction) {
	Editor_focus();
	edobj.execCommand(mAction);
}

// 字型名稱
function fnFontName(mfna) {
	edobj.execCommand("FontName", false, mfna);
}

// 字型尺寸
function fnFontSize(mfsz) {
	edobj.execCommand("FontSize", false, mfsz);
}

// 字型格式 （粗、斜、底、刪）
function fnFontType(mFontType) {
	edobj.execCommand(mFontType);
}

// 設定顏色
function fnColorType(mType) {
	var mcolor = showModalDialog("../common/colorselect.htm", false, "center:yes; dialogWidth:245px; dialogHeight:245px;status:0;");
	edobj.execCommand(mType, false, mcolor);
}

// 建立表格
function fnTableSeting() {
	var tbdata = showModalDialog("../common/tableseting.htm", false, "center:yes; dialogWidth:245px;dialogHeight:300px;status:0;");
	if (tbdata != undefined) {
		edobj.body.innerHTML = edobj.body.innerHTML + tbdata;
	}
}

// 標題 (數字、符號、縮排)
function fnOrderedList(mType) {
	Editor_focus();
	edobj.execCommand(mType);
}

// 超連結
function fnCreateLink() {
	Editor_focus();
	edobj.execCommand("CreateLink");
}

// 跑馬燈
function fnMarquee() {
	Editor_focus();
	edobj.execCommand("InsertMarquee");
	alert("請連按物件兩下，編輯物件內容");
}

// 加入水平線
function fnHorizontalRule() {
	Editor_focus();
	edobj.execCommand("InsertHorizontalRule");
}

// 核取方塊、點選圓、文字輸入、密碼輸入、選單方塊、文字方塊、button、submit、reset
function fnFormItem(mType, mAt) {
	Editor_focus();
	edobj.execCommand(mType);
	if (mAt != "") {
		alert("請連按物件兩下，編輯物件內容");
	}
}

// 列印
function fnPrint() {
	if (ckArea() == 0)
		pvobj.value = edobj.body.innerHTML;
	else
		edobj.body.innerHTML = pvobj.value;

	edobj.execCommand("Print");
}

// 存檔(個人目錄)
function fnSave() {
	fnSynchronous()		// 設計區與原始檔資料同步

	edobj.execCommand("SaveAs", 0, "None");
}

// 清除資料
function fnClearCode() {
	if (confirm("確定要清除目前所編輯的資料？")) {
		pvobj.value = "";
		edobj.body.innerHTML = "";
	}
}

// 顯示區切換
function fnShowHtml() {
	var eobj, hobj, iobj, sobj, f1obj, f2obj, tobj;

	eobj = document.getElementById("EditArea");
	hobj = document.getElementById("HtmlArea");
	sobj = document.getElementById("shtml");
	iobj = document.getElementById("simg");
	f1obj = document.getElementById("FuncArea1");
	f2obj = document.getElementById("FuncArea2");
	tobj = document.getElementById("stitle")

	if (hobj.style.display == "none") {
		pvobj.value = edobj.body.innerHTML;

		f1obj.style.display = "none";
		f2obj.style.display = "none";
		eobj.style.display = "none";
		hobj.style.display = "";
		sobj.title = "顯示設計區";
		iobj.title = "顯示設計區";
		stitle.innerHTML = "原始檔";
	}
	else {
		edobj.body.innerHTML = pvobj.value;

		f1obj.style.display = "";
		f2obj.style.display = "";
		eobj.style.display = "";
		hobj.style.display = "none";
		sobj.title = "顯示原始檔";
		iobj.title = "顯示原始檔";
		stitle.innerHTML = "設計區";
	}
}

// 設計區與原始檔資料同步
function fnSynchronous() {
	if (ckArea() == 0)
		pvobj.value = edobj.body.innerHTML;
	else
		edobj.body.innerHTML = pvobj.value;
}


// 取得目前顯示區域
function ckArea() {
	var ckvalue = 0;
	var hobj = document.getElementById("HtmlArea");
	if (hobj.style.display == "none")
		ckvalue = 0; // 目前顯示設計區
	else
		ckvalue = 1; // 日前顯示原始檔

	return ckvalue;
}

// 恢復原值
function fnReset() {
	var fmobj = document.getElementById("form1");

	if (fmobj != null)
		fmobj.reset();
}
