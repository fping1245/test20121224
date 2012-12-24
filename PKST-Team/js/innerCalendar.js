var IsDown = 0; 	// 滑鼠左鍵按下旗標 0:未按 1:按下

// 顯示 iframe 工作視窗，並呼叫指定 iframe 要執行的網頁
function show_calendar(srcname, mwidth, mheight) {
	var divobj, ifobj;

	divobj = document.getElementById("div_calendar");
	ifobj = document.getElementById("if_calendar");
	if (divobj != null && ifobj != null) {
		divobj.style.width = mwidth + "px";
		divobj.style.height = mheight + "px";
		divobj.style.left = String((document.body.clientWidth - mwidth) / 2) + "px";
		divobj.style.top = String((document.body.clientHeight - mheight) / 2) + "px";
		divobj.style.display = "";

		ifobj.style.width = mwidth + "px";
		ifobj.style.height = mheight + "px";
		ifobj.src = srcname;
	}

	show_fullwindow();
}

// 關閉日曆視窗
function close_calendar() {
	var divobj;
	divobj = document.getElementById("div_calendar");
	if (divobj != null)
		divobj.style.display = "none";

	close_fullwindow();
}

// 按下滑鼠左鍵
function EventDm() {
	IsDown = 1;
}

// 放開滑鼠左鍵
function EventUp() {
	IsDown = 0;
}

// 移動日曆視窗
function EventMv() {
	var tbobj = document.getElementById("div_calendar");
	var intx, inty;

	if (IsDown == 1 && tbobj.style.display != "none") {
		intx = event.clientX;
		inty = event.clientY;

		intx = intx - 100;
		inty = inty - 10;

		if (intx < 0)
			intx = 0;
		if (intx > (document.body.clientWidth - 216))
			intx = document.body.clientWidth - 216;

		if (inty < 0)
			inty = 0;
		if (inty > (document.body.clientHeight - 230))
			inty = document.body.clientHeight - 230;

		tbobj.style.left = intx + "px";
		tbobj.style.top = inty + "px";
	}
	else {
		IsDown = 0;
	}
}

// 開啟日曆選擇視窗(iframe)
function getDate(mtag) {
	var rtobj;
	var mdate = "";
	rtobj = document.getElementById(mtag);
	if (rtobj != null) {
		mdate = rtobj.value;
		show_calendar("../common/calendar.aspx?rtobj=" + escape(mtag) + "&ndt=" + escape(mdate), 206, 220);
	}
}

// 傳回指定欄位的值 (iframe)
function rt_parent(objname, mdate) {
	var rtobj;
	rtobj = document.getElementById(objname);
	if (rtobj != null)
		rtobj.value = mdate;
	close_calendar();
}
