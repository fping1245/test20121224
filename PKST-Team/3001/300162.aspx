<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300162.aspx.cs" Inherits="_300162" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>相簿管理</title>
<style type="text/css">
.bnNor {
	padding:2px;
	border:1px solid #999999;
	border-top:1px solid #ffffff; 
    border-left:1px solid #ffffff;
    cursor:hand;
    } 

.bnOver {
	padding:2px;
	background-color:#00ffff;
	border:1px solid #999999;
	border-top:1px solid #ffffff;
	border-left:1px solid #ffffff;
	cursor:hand;
	} 
    
.bnDown {
	padding:2px;
	border:1px solid #999999;
	border-top:1px solid #ffffff;
	border-left:1px solid #ffffff;
	cursor:hand;
	}
</style>
<script language="javascript" type="text/javascript">

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
		close_div("div_win");
		close_fullwindow();
	}

	// 清除隱藏 iframe (if_win) 的 src
	function clean_win() {
		ifobj = document.getElementById("if_win");
		if (ifobj != null) {
			ifobj.src = "about:blank";
			close_div("div_win");
		}
	}

	// 更新上層頁面的縮圖視窗
	function thumb_reload() {
		opener.location.reload();
	}

	// 顯示說明文字資料 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function show_doc()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("3001622.aspx?ac_sid=<%=ac_sid%>&timestamp=" + timestamp, 400, 200);
	}

	// 修改說明文字資料 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function pic_edit()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("3001624.aspx?al_sid=<%=al_sid%>&ac_sid=<%=ac_sid%>&timestamp=" + timestamp, 400, 200);
	}
</script>
</head>
<body background="../images/background/s21.jpg">
	<form id="form1" runat="server">
	<div>
	<!-- Begin 覆蓋整個工作頁面，不讓使用者再按其它按鍵 -->
	<div id="fullwindow" style="position: absolute; top:0px; left:0px; width:1024px; height:768px; display:none">
	<table id="fulltable" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_back.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<!-- End -->
	<!-- Begin 隱藏視窗 -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	<table width="100%" border="0" cellpadding="0" cellspacing="0" style="margin:10px 0px 10px 0px">
	<tr><td align="center" valign="middle">
			<table id="tb_image" align="center" border="0" style="height:<%=tb_height%>px; width:<%=tb_width%>px">
			<tr><td align="center">
					<asp:Image ID="img_show" BorderWidth="6px" runat="server" BorderStyle="Outset" />
				</td>
			</tr>
			</table>
		</td>
	</tr>
	<tr><td style="height:10px"></td></tr>
	<tr><td align="center">
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
			<img id="bn_left" src="../images/button/bn1-left.gif" alt="" title="上一個影像" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="goPic(<%=rownum%> - 1)" />
			<img id="bn_right" src="../images/button/bn1-right.gif" alt="" title="下一個影像" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="goPic(<%=rownum%> + 1)" />
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
			<img id="bn_optimal" src="../images/button/optimal.gif" alt="" title="符合最佳大小(視窗開到最大)" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="resize(0)" />
			<img id="bn_source" src="../images/button/source.gif" alt="" title="實際影像大小" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="resize(3)" />
			<img id="bn_projector" src="../images/button/projector.gif" alt="" title="幻燈片播放" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="show_slide(<%=rownum%>)" />
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
			<img id="bn_increase" src="../images/button/increase.gif" alt="" title="放大 10%" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="resize(1)" />
			<img id="bn_reduce" src="../images/button/reduce.gif" alt="" title="縮小 10%" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="resize(2)" />
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
			<img id="bn_f_right" src="../images/button/flip-right.gif" alt="" title="順時鐘旋轉" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="flip(1)" />
			<img id="bn_f_left" src="../images/button/flip-left.gif" alt="" title="逆時鐘旋轉" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="flip(-1)" />
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
			<img id="bn_delete" src="../images/button/delete.gif" alt="" title="刪除" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="show_win('3001623.aspx?al_sid=<%=al_sid%>&ac_sid=<%=ac_sid%>&rownum=<%=rownum%>', 400, 200)"  />
			<img id="bn_modify" src="../images/button/modify.gif" alt="" title="修改" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="pic_edit()"  />
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
			<img id="bn_help" src="../images/button/help.gif" alt="" title="說明" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="show_doc()" />
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
		</td>
	</tr>
	</table>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	</div>
	<script language="javascript" type="text/javascript">
	var ac_swidth = <%=ac_swidth%>;
	var ac_sheight = <%=ac_sheight%>;
	var ac_width = <%=ac_width%>;
	var ac_height = <%=ac_height%>;
	var maxrow = <%=maxrow%>;
	var nowflip = 0;		// 目前旋轉狀態 0:正常 1:左旋 2:上下反轉 3:右旋

	// 重新定義顯示區域的寬及高
	function tb_resize() {
		var tbobj, imgobj;
		var tbh = 0, tbw = 0;

		imgobj = document.getElementById("img_show");
		tbobj = document.getElementById("tb_image");
		
		if (tbobj != null)
		{
			if (document.body.scrollHeight > window.screen.availHeight && imgobj.height < document.body.scrollHeight)
				tbh = (screen.availHeight - 195).toString() + "px";
			else
				tbh = (document.body.scrollHeight - 64).toString() + "px";

			if (document.body.scrollWidth > window.screen.availWidth && imgobj.width < document.body.scrollWidth) {
				tbw = (screen.availWidth - 17).toString() + "px";
			}
			else
				tbw = (document.body.scrollWidth - 17).toString() + "px";

			tbobj.style.width = tbw;
			tbobj.style.height = tbh;		
		}
	}

	// 上下換圖
	function goPic(row) {
		if (row < 1)
			alert("已經是在第一張圖片!\n");
		else if (row > maxrow)
			alert("已經是最後一張圖片!\n");
		else {
			var tbobj, tbh = 600, tbw = 870;
			tbobj = document.getElementById("tb_image");
			if (tbobj != null) {
				tbh = tbobj.style.height.replace("px","");
				tbw = tbobj.style.width.replace("px","");
			}
			location.href = "300162.aspx?al_sid=<%=al_sid%>&ac_sid=0&rownum=" + row + "&maxrow=" + maxrow + "&tbw=" + tbw + "&tbh=" + tbh;
		}
	}

	// 圖形縮放
	function resize(mtype) {
		var imgobj;
		var fCnt = 1.0, fw = 1.0, fh = 1.0;

		imgobj = document.getElementById("img_show");
		
		if (imgobj != null) {
			switch (mtype) {
				case 0:		// 最佳尺寸
					fh = imgobj.height / (window.screen.availHeight - 125);
					fw = imgobj.width / (window.screen.availWidth - 125);
					
					if (fh > fw)
						fCnt = fh;
					else
						fCnt = fw;
	
					imgobj.style.height = (imgobj.height / fCnt).toString() + "px";
					imgobj.style.width = (imgobj.width / fCnt).toString() + "px";

					break;

				case 1:		// 放大 10%
					imgobj.style.height = (imgobj.height * 1.1).toString() + "px";
					imgobj.style.width = (imgobj.width * 1.1).toString() + "px";
					break;
				
				case 2:		// 縮小 10%
					imgobj.style.height = (imgobj.height / 1.1).toString() + "px";
					imgobj.style.width = (imgobj.width / 1.1).toString() + "px";
					break;
					
				case 3:		// 圖形原來尺寸
					imgobj.style.height = ac_sheight.toString() + "px";
					imgobj.style.width = ac_swidth.toString() + "px";
					break;
			}
			
			ac_width = imgobj.width;
			ac_height = imgobj.height;
			
			tb_resize();		// 重新定義顯示區域的寬及高
		}
	}

	// 圖形翻轉
	function flip(sid) {
		var imgobj;
		var fCnt;

		imgobj = document.getElementById("img_show");
		
		if (imgobj != null) {
			if (sid > 0) {
				nowflip++;
				if (nowflip > 3)
					nowflip = 0;
			}
			else {
				nowflip--;
				if (nowflip < 0)
					nowflip = 3;
			}
			
			if (nowflip == 1 || nowflip ==3) {
				if (imgobj.height > imgobj.width)
					fCnt = imgobj.height / imgobj.width;
				else
					fCnt = imgobj.width / imgobj.height;

				imgobj.style.width = (imgobj.width / fCnt).toString() + "px";
				imgobj.style.height = (imgobj.height / fCnt).toString() + "px";
			}
			else {
				imgobj.style.width = ac_width.toString() + "px";
				imgobj.style.height = ac_height.toString() + "px";
			}

			imgobj.style.filter = "progid:DXImageTransform.Microsoft.BasicImage(Rotation=" + nowflip + ")";
		}
	}
	
	//	播放幻燈片
	function show_slide(row) {
		var features;
		var mhref = "";

		features = "width=" + (window.screen.availWidth - 10).toString() + "px"; 		// 視窗寬度
		features += ",height=" + (window.screen.availHeight - 60).toString()  + "px"; 	// 視窗高度
		features += ",top=1px";
		features += ",left=1px";
		features += ",toolbar=no,status=yes,resizable=yes,scrollbars=yes";

		mhref = "30017.aspx?al_sid=<%=al_sid%>&rownum=" + row + "&imgw=" + (window.screen.availWidth - 10).toString();
		mhref += "&imgh=" + (window.screen.availHeight - 60).toString() + "&effect=<%=show_effect%>";

		slide = window.open(mhref, "win_slide", features);
	}
	</script>
	</form>
</body>
</html>
