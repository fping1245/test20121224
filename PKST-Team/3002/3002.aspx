<%@ Page Language="C#" AutoEventWireup="true" CodeFile="3002.aspx.cs" Inherits="_3002" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>相簿管理</title>
<script language="javascript" type="text/javascript">
	var show_effect = 0;
	var slide;

	// 顯示上傳訊息視窗
	function show_msg() {
		var msgobj;

		msgobj = document.getElementById("div_msg_wait");
		if (msgobj != null) {
			msgobj.style.left = String((document.body.clientWidth - 240) / 2) + "px";
			msgobj.style.top = String((document.body.clientHeight - 240) / 2) + "px";
			msgobj.style.display = "";
		}

		show_fullwindow();
	}

	// 關閉上傳訊息視窗
	function close_msg() {
		var msgobj;

		msgobj = document.getElementById("div_msg_wait");
		if (msgobj != null)
			msgobj.style.display = "none";

		close_fullwindow();
	}

	// 顯示覆蓋整頁視窗
	function show_fullwindow() {
		var fullobj, tableobj;

		fullobj = document.getElementById("fullwindow");
		if (fullobj != null) {
			fullobj.style.width = document.body.clientWidth + "px";
			fullobj.style.height = document.body.clientHeight + "px";
			fullobj.style.display = "";
		}

		tableobj = document.getElementById("fulltable");
		if (tableobj != null) {
			tableobj.style.width = document.body.clientWidth + "px";
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

	// 顯示 div 工作視窗
	function show_div(objname, mwidth, mheight) {
		var divobj;

		divobj = document.getElementById(objname);
		if (divobj != null) {
			divobj.style.width = mwidth + "px";
			divobj.style.height = mheight + "px";
			divobj.style.left = String((document.body.clientWidth - mwidth) / 2) + "px";
			divobj.style.top = String((document.body.clientHeight - mheight) / 2) + "px";
			divobj.style.display = "";
		}

		show_fullwindow();
	}

	// 顯示 iframe 工作視窗，並呼叫指定 iframe 要執行的網頁
	function show_win(srcname, mwidth, mheight) {
		var divobj, ifobj;

		divobj = document.getElementById("div_win");
		ifobj = document.getElementById("if_win");
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

	// 關閉 div 工作視窗
	function close_div(objname) {
		var divobj;
		divobj = document.getElementById(objname);
		if (divobj != null)
			divobj.style.display = "none";
	}

	// 隱藏所有內建視窗
	function close_all() {
		close_div("div_msg_wait");
		close_div("div_effect");
		close_div("div_dir");
		close_div("div_help");
		close_div("div_win");

		close_fullwindow();
	}

	// menu 選單開關
	function hide_menu() {
		var frameobj;
		var imgobj;
		frameobj = top.document.getElementById("subframe");
		imgobj = top.mstatus.document.getElementById("hd_menu_pic");
		frameobj.cols = "0,*";
		imgobj.src = "images/t_Right.gif";
		imgobj.alt = "開啟功能選單";
	}

	// title 選單開關
	function hide_title() {
		var frameobj;
		frameobj = top.document.getElementById("mainframe");
		imgobj = top.mstatus.document.getElementById("hd_title_pic");
		frameobj.rows = "0,32pt,*";
		imgobj.src = "images/t_Down.gif";
		imgobj.alt = "開啟抬頭區域";
	}

	// 載入時，關閉抬頭列及選單列
	window.onload = function() {
		hide_menu();
		hide_title();
	}

	// 更新 TreeView
	function tree_reload(sid) {
		if (document.frames) {		// IE
			document.frames["if_tree"].document.location.reload();
		}
		else {						// W3C (FireFox)
			var ifobj;
			ifobj = document.getElementById("if_tree");
			if (ifobj != null) {
				ifobj.contentDocument.location.reload();
			}
		}
	}

	// 清除隱藏 iframe (if_win) 的 src
	function clean_win() {
		ifobj = document.getElementById("if_win");
		if (ifobj != null) {
			ifobj.src = "about:blank";
			close_div("div_win");
		}
	}

	// 更新 縮圖視窗
	function thumb_reload() {
		if (document.frames) {		// IE
			document.frames["if_thumb"].document.location.reload(); 
		}
		else {						// W3C (FireFox)
			var ifobj;
			ifobj = document.getElementById("if_thumb");
			if (ifobj != null) {
				ifobj.contentDocument.location.reload();
			}
		}
	}

	// 選擇顯示效果
	function ck_effect() {
		var selobj, spanobj;
		selobj  = document.getElementById("sel_effect");
		spanobj = document.getElementById("effect_name");

		if (selobj != null && spanobj != null) {
			show_effect = selobj.selectedIndex;
			switch (show_effect) {
				case 0:
					spanobj.innerHTML = "隨機";
					break;
				case 1:
					spanobj.innerHTML = "平移";
					break;
				case 2:
					spanobj.innerHTML = "縮放";
					break;
				case 3:
					spanobj.innerHTML = "水平翻轉";
					break;
				case 4:
					spanobj.innerHTML = "垂直翻轉";
					break;
				case 5:
					spanobj.innerHTML = "淡入淡出";
					break;
				case 6:
					spanobj.innerHTML = "直百頁窗";
					break;
				case 7:
					spanobj.innerHTML = "橫百頁窗";
					break;
				case 8:
					spanobj.innerHTML = "對角伸縮";
					break;
				case 9:
					spanobj.innerHTML = "正常";
					break;
				default:
					spanobj.innerHTML = "隨機";
					show_effect = -1;
					break;
			}
		}
	}

	// 播放幻燈片
	function show_slide(row) {
		var features;
		var mhref = "";

		features = "width=" + (window.screen.availWidth - 10).toString() + "px"; 		// 視窗寬度
		features += ",height=" + (window.screen.availHeight - 60).toString()  + "px"; 	// 視窗高度
		features += ",top=1px";
		features += ",left=1px";
		features += ",toolbar=no,status=yes,resizable=yes,scrollbars=yes,";

		mhref = "30027.aspx?fl_url=<%=lb_fl_url_encode.Text%>&rownum=" + row + "&imgw=" + (window.screen.availWidth - 10).toString();
		mhref += "&imgh" + (window.screen.availHeight - 60).toString() + "&effect=" + show_effect;

		slide = window.open(mhref, "win_slide", features);
	}
</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<!-- Begin 覆蓋整個工作頁面，不讓使用者再按其它按鍵 -->
	<div id="fullwindow" style="position: absolute; top:0px; left:0px; width:1024px; height:768px; display:none">
	<table id="fulltable" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_back.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<!-- End -->
	<asp:ScriptManager ID="sm_manager" runat="server">
	</asp:ScriptManager>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 10pt 0pt; font-family:標楷體;">相簿管理(使用實體路徑儲存圖檔)</p>
	<hr style="width:99%" />
	<table width="95%" border="0" cellpadding="2" cellspacing="0" style="margin:0pt 0pt 0pt 0pt">
	<tr align="left">
		<td class="text11pt">目前路徑：<asp:Label ID="lb_show_path" runat="server" Text="" Font-Bold="true" ForeColor="#00008B" CssClass="text11pt"></asp:Label></td>
		<td class="text11pt">幻燈片換景效果：<span id="effect_name" class="text11pt" style="color:#00008B; font-weight:bold">隨機</span>
	</tr>
	</table>
	<hr style="width:99%" />
	<table width="95%" border="0" cellpadding="2" cellspacing="0" style="margin:0pt 0pt 0pt 0pt">
	<tr><td align="left" class="text11pt">
			<asp:UpdatePanel ID="up_button" runat="server" RenderMode="Inline">
				<ContentTemplate>
					<asp:Button ID="bn_go_root" runat="server" Text="到根目錄" Height="22pt" onclick="bn_go_root_Click" />
					<input type="button" value="目錄管理" style="height:22pt" onclick="show_div('div_dir',150, 240)" />
					<input type="button" value="上傳檔案" style="height:22pt" onclick="show_win('30025.aspx?fl_url=<%=lb_fl_url_encode.Text%>', 260, 215)" />
					<input type="button" value="換景效果" style="height:22pt" onclick="show_div('div_effect',240, 240)" />
					<asp:Button ID="bn_show" runat="server" Text="播幻燈片" Height="22pt" OnClientClick="show_slide(1)" />
					<asp:Button ID="bn_help" runat="server" Text="使用說明" Height="22pt" OnClientClick="show_div('div_help',300, 240)" />
				</ContentTemplate>
			</asp:UpdatePanel>
		</td>
	</tr>
	</table>
	<!-- Begin 隱藏視窗 -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	<!-- Begin 目錄管理 -->
	<div id="div_dir" style="position: absolute; top:0px; left:0px; width:150px; display:none">
	<table border="2" cellpadding="2" cellspacing="2" style="width:150px;" bgcolor="#efefef">
	<tr><td align="center" class="text9pt"><br />
			<input type="button" value="建子目錄" onclick="close_div('div_dir');show_win('30022.aspx?fl_url=<%=lb_fl_url_encode.Text%>', 240, 220)" style="height:22pt" /><br /><br />
			<input type="button" value="刪除目錄" onclick="close_div('div_dir');show_win('30023.aspx?fl_url=<%=lb_fl_url_encode.Text%>', 240, 148)" style="height:22pt" /><br /><br />
			<input type="button" value="目錄更名" onclick="close_div('div_dir');show_win('30024.aspx?fl_url=<%=lb_fl_url_encode.Text%>', 240, 350)" style="height:22pt" /><br /><br />
			<input type="button" value="關閉" onclick="close_all()" style="height:22pt" /><br /><br />
		</td>
	</tr>
	</table>
	</div>
	<!-- End -->
	<!-- Begin 顯示效果 -->
	<div id="div_effect" style="position: absolute; top:0px; left:0px; width:240px; display:none">
	<table border="2" cellpadding="2" cellspacing="2" style="width:240px;" bgcolor="#efefef">
	<tr><td align="center" class="text11pt"><br />請選擇幻燈片換景效果<br /><br />
			<select id="sel_effect" title="選擇幻燈片換景效果">
				<option value="0">隨機</option>
				<option value="1">平移</option>
				<option value="2">縮放</option>
				<option value="3">水平翻轉</option>
				<option value="4">垂直翻轉</option>
				<option value="5">淡入淡出</option>
				<option value="6">直百頁窗</option>
				<option value="7">橫百頁窗</option>
				<option value="8">對角伸縮</option>
				<option value="9">正常</option>
			</select><br /><br />
			<input type="button" value="確定" onclick="ck_effect();close_all()" style="height:22pt" />
			<input type="button" value="取消" onclick="close_all()" style="height:22pt" /><br /><br />
		</td>
	</tr>
	</table>
	</div>
	<!-- End -->
	<!-- Begin 使用說明 -->
	<div id="div_help" style="position: absolute; top:0px; left:0px; width:300px; display:none">
	<table border="2" cellpadding="0" cellspacing="0" style="width:300px; height:100px;" bgcolor="#efefef">
	<tr><td align="center">
			<table border="0" cellpadding="2" cellspacing="0" width="100%">
			<tr><td align="left" style="background-color:Blue; color:White">&nbsp;使用說明</td>
				<td align="right" style="background-color:Blue; width:30px">
					<a href="javascript:close_all()" style="font-size:11pt; color:White; border-color:White; border-style:inherit; background-color:Red" title="關閉視窗">&nbsp;×&nbsp;</a>
				</td>
			</tr>
			</table>
		</td>
	</tr>
	<tr><td align="center" class="text9pt">
			<table border="0" cellpadding="2" cellspacing="2" width="100%" bgcolor="#efefef">
			<tr align="left">
				<td style="width:20px" valign="top">˙</td>
				<td>點選目錄，可預覽目錄內的縮圖。</td>
			</tr>
			<tr align="left">
				<td valign="top">˙</td>
				<td>點選預覽的檔案，會彈出完整圖形視窗。</td>
			</tr>
			<tr align="left">
				<td valign="top">˙</td>
				<td>點選「播幻燈片」，會彈出視窗，依照指定的效果，循環顯示當前目錄內的圖檔。</td>
			</tr>
			<tr align="left">
				<td valign="top">˙</td>
				<td>幻燈片播放時，請按 F11 以全螢幕觀看，以獲得最佳效果。</td>
			</tr>
			<tr align="left">
				<td valign="top">˙</td>
				<td>幻燈片播放進行中，點擊相片，會自動換到下一張相片。</td>
			</tr>
			</table>
		</td>
	</tr>
	</table>
	</div>
	<!-- End -->
	<!-- Begin 顯示上傳訊息 -->
	<div id="div_msg_wait" class="text9pt" style="position:absolute; width:240px; z-index:0; display:none;">
	<table border="2" cellpadding="2" cellspacing="2" style="width:240px; height:100px;" bgcolor="#efefef">
	<tr><td align="center" class="text9pt">資料上傳中 ........<br />請勿按任何按鍵!</td></tr>
	</table>
	</div>
	<!-- End -->
	<hr style="width:99%" />
	<table width="99%" border="1" cellpadding="0" cellspacing="0">
	<tr><td style="width:250px" valign="top" align="left">
			<iframe id="if_tree" src="30021.aspx?fl_url=<%=lb_fl_url_encode.Text%>" frameborder="0" style="width:250px; height:522px"></iframe>
		</td>
		<td valign="middle" align="center" style="width:100%">
			<iframe id="if_thumb" src="30026.aspx?fl_url=<%=lb_fl_url_encode.Text%>" frameborder="0" style="width:660px"></iframe>
		</td>
	</tr>
	</table>
	</center>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	<asp:Label ID="lb_fl_url" runat="server" Visible="false" Text=""></asp:Label>
	<asp:Label ID="lb_fl_url_encode" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_path" runat="server" Visible="false"></asp:Label>
	</div>
	</form>	
</body>
</html>