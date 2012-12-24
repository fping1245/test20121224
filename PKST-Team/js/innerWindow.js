// 顯示上傳訊息視窗
function show_msg_wait() {
	var msgobj;

	msgobj = document.getElementById("div_msg_wait");
	if (msgobj != null) {
		msgobj.style.left = String((document.body.clientWidth - 240) / 2) + "px";
		msgobj.style.top = String((document.body.clientHeight - 240) / 2) + "px";
		msgobj.style.display = "";
	}

	show_ban();
}

// 閥閉上傳訊息視窗
function close_msg_wait() {
	close_div("div_msg_wait");
	close_div("div_ban");
}

// 顯示覆蓋整頁視窗
function show_ban() {
	var banobj, tableobj;

	banobj = document.getElementById("div_ban");
	if (banobj != null) {
		banobj.style.top = "0px";
		banobj.style.left = "0px";
		//banobj.style.width = document.body.clientWidth + "px";
		banobj.style.width = "100%";
		banobj.style.height = document.body.clientHeight + "px";
		banobj.style.display = "";
	}

	tableobj = document.getElementById("tb_ban");
	if (tableobj != null) {
		//tableobj.style.width = document.body.clientWidth + "px";
		tableobj.style.width = "100%";
		tableobj.style.height = document.body.clientHeight + "px";
		tableobj.style.display = "";
	}
}

// 顯示覆蓋整頁視窗
function show_fullwindow() {
	var fullobj, tableobj;

	fullobj = document.getElementById("fullwindow");
	if (fullobj != null) {
		fullobj.style.top = "0px";
		fullobj.style.left = "0px";
		fullobj.style.width = document.body.clientWidth + "px";
		fullobj.style.height = document.body.clientHeight + "px";
		fullobj.style.display = "";
	}

	tableobj = document.getElementById("fulltable");
	if (tableobj != null) {
		//tableobj.style.width = document.body.clientWidth + "px";
		tableobj.style.width = "100%";
		tableobj.style.height = document.body.clientHeight + "px";
		tableobj.style.display = "";
	}
}

// 關閉覆蓋整頁的視窗
function close_fullwindow() {
	var fullobj;

	fullobj = document.getElementById("fullwindow");
	if (fullobj != null)
		fullobj.style.display = "none";
}

// 顯示 iframe 工作視窗，並呼叫指定 iframe 要執行的網頁
function show_win(srcname, mwidth, mheight) {
	var divobj, ifobj;

	divobj = document.getElementById("div_win");
	ifobj = document.getElementById("if_win");
	if (divobj != null && ifobj != null)
	{
		if (mwidth < 0 || mheight < 0)
		{
			// 放大到全螢幕
			divobj.style.width = "100%";
			divobj.style.height = document.body.clientHeight + "px";
			divobj.style.left = "0px";
			divobj.style.top = "0px";
			divobj.style.display = "";

			//ifobj.style.width = document.body.clientWidth + "px";
			ifobj.style.width = "100%";
			ifobj.style.height = document.body.clientHeight + "px";
		}
		else
		{
			// 放大到指定螢幕大小
			divobj.style.width = mwidth + "px";
			divobj.style.height = mheight + "px";
			divobj.style.left = String((document.body.clientWidth - mwidth) / 2) + "px";
			divobj.style.top = "25px";

			divobj.style.display = "";

			ifobj.style.width = mwidth + "px";
			ifobj.style.height = mheight + "px";
		}
		ifobj.src = srcname;
	}

	show_fullwindow();
}

// 關閉 div 工作視窗
function close_div(objname) {
	var divobj;
	divobj = document.getElementById(objname);
	if (divobj != null)
		divobj.style.display = "none";
}

// 清除隱藏 iframe (if_win) 的 src
function clean_win() {
	ifobj = document.getElementById("if_win");
	if (ifobj != null) {
		ifobj.src = "";
		close_div("div_win");
	}
}

// 關閉所有視窗
function close_all() {
	close_div("div_msg_wait");
	close_div("div_ban");
	close_div("div_win");
	clean_win();

	close_fullwindow();
}
