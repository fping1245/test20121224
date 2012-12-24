<%@ Page Language="C#" AutoEventWireup="true" CodeFile="80011.aspx.cs" Inherits="_80011" validateRequest="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<title>HTML編輯器</title>
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../js/htmledit.js"></script>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<style type="text/css">
	td
	{
		font-size: 12px;
		font-family: 細明體;
	}
	button
	{
		font-size: 12px;
		font-family: 細明體;
	}
	input
	{
		font-size: 12px;
		font-family: 細明體;
	}
	div
	{
		border-right: #d9cec4 1px solid;
		border-top: #d9cec4 1px solid;
		border-left: #d9cec4 1px solid;
		border-bottom: #d9cec4 1px solid;
		text-align: center;
		cursor: default;
	}
	.function
	{
		width: 80px;
	}
</style>
<script language="javascript" type="text/javascript">
	function fnLoadData() {
		var appName = navigator.appName.toString().toLowerCase();
		if (!appName.match("microsoft internet explorer"))
			alert("本功能不支援「" + navigator.appName + "」！\n可能會有錯誤或無法執行的狀況！\n");
		edobj.body.innerHTML = pvobj.value;
	}

	// 更新 If_Image
	function Renew_Image(sid) {
		var ifobj = document.getElementById("if_image");

		ifobj.src = "800111.aspx?sid=" + sid;
	}
</script>
</head>
<body onload="fnLoadData()">
	<form id="form1" runat="server">
	<div style="border-width:0px">

	<!-- Begin 覆蓋整個工作頁面，不讓使用者再按其它按鍵 -->
	<div id="fullwindow" style="position: absolute; top:0px; left:0px; width:1024px; height:768px; display:none">
	<table id="fulltable" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_back.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<!-- End -->

	<center>
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family: 標楷體; text-align:center">HTML編輯器-<asp:Label ID="lb_title" runat="server" Font-Size="14pt"></asp:Label> <span style="font-size:12pt; color:Blue">(適用 IE 瀏覽器)</span></p>
	<table border="1" cellspacing="0" cellpadding="0" style="width:98%">
	<tr><td bgcolor="#d9cec4" align="left">
			<table cellspacing="0" cellpadding="0" border="0">
			<tr><td><table cellspacing="1" cellpadding="0" border="0">
					<tr><td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
						<td style="width:60px; text-align:center; font-weight:bold; color:Blue"><span id="stitle">設計區</span></td>
						<td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
						<td	align="center" style="width:60px">網頁編號</td>
						<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
						<td align="center" style="width:60px; background-color:White; border-width:1px; border-color:Gray; border-style:solid">
							<asp:Literal ID="lt_she_sid" runat="server" Text=""></asp:Literal>
						</td>
						<td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
						<td><div onclick="fnShowHtml();" id="shtml" title="顯示原始檔">
							<img id="simg" alt="" src="../images/htmlimage/ef37.gif" width="19" height="20" title="顯示原始檔" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="存檔(個人目錄)" onclick="fnSave();" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef01.gif" width="19" height="20" title="存檔(個人目錄)" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="清除內容" onclick="fnClearCode();" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef39.gif" width="19" height="20" title="清除內容" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="列印" onclick="fnPrint();" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef02.gif" width="19" height="20" title="列印" /></div>
						</td>
						<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
					</tr>
					</table>
				</td>
				<td id="FuncArea1">
					<table cellspacing="1" cellpadding="0" border="0">
					<tr><td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="剪下" onclick="fnModify('Cut');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef03.gif" width="19" height="20" title="剪下" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="複製" onclick="fnModify('Copy');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef04.gif" width="19" height="20" title="複製" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="貼上" onclick="fnModify('Paste');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef05.gif" width="19" height="20" title="貼上" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="刪除" onclick="fnModify('Delete');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef06.gif" width="19" height="20" title="刪除" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="復原" onclick="fnModify('Undo');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef07.gif" width="19" height="20" title="復原" /></div>
						</td>
						<td><img alt="" src="../images/htmlimage/eh01.gif" alt="" width="6" height="20" /></td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="核取方塊" onclick="fnFormItem('InsertInputCheckbox','');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef28.gif" width="19" height="20" title="核取方塊" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="點選圓" onclick="fnFormItem('InsertInputRadio','');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef29.gif" width="19" height="20" title="點選圓" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="文字輸入" onclick="fnFormItem('InsertInputText','Y');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef30.gif" width="19" height="20" title="文字輸入" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="密碼輸入" onclick="fnFormItem('InsertInputPassword','Y');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef31.gif" width="19" height="20" title="密碼輸入" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="選單方塊" onclick="fnFormItem('InsertSelectDropdown','');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef32.gif" width="19" height="20" title="選單方塊" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="文字方塊" onclick="fnFormItem('InsertTextArea','Y');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef33.gif" width="19" height="20" title="文字方塊" /></div>
						</td>
						<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="button 按鈕" onclick="fnFormItem('InsertInputButton','Y');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef34.gif" width="19" height="20" title="button 按鈕" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="submit 按鈕" onclick="fnFormItem('InsertInputSubmit','Y');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef35.gif" width="19" height="20" title="submit 按鈕" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="reset 按鈕" onclick="fnFormItem('InsertInputReset','Y');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef36.gif" width="19" height="20" title="reset 按鈕" /></div>
						</td>
					</tr>
					</table>
				</td>
			</tr>
			</table>
		</td>
	</tr>
	<tr id="FuncArea2" style="width:100%">
		<td bgcolor="#d9cec4" align="left">
			<table cellspacing="1" cellpadding="0" border="0">
			<tr><td width="6"><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
				<td width="84">
					<select onchange="fnFontName(this[this.selectedIndex].value);this.selectedIndex=0;">
						<option value="">字型</option>
						<option value="新細明體">新細明體</option>
						<option value="標楷體">標楷體</option>
						<option value="細明體">細明體</option>
						<option value="arial">Arial</option>
						<option value="wingdings">Wingdings</option>
					</select>
				</td>
				<td width="71">
					<select onchange="fnFontSize(this[this.selectedIndex].value);this.selectedIndex=0;">
						<option value="">大小</option>
						<option value="1">1</option>
						<option value="2">2</option>
						<option value="3">3(預設)</option>
						<option value="4">4</option>
						<option value="5">5</option>
						<option value="6">6</option>
						<option value="7">7 </option>
					</select>
				</td>
				<td width="6"><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="粗體字" onclick="fnFontType('Bold');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef08.gif" width="19" height="20" title="粗體字" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="斜體字" onclick="fnFontType('Italic');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef09.gif" width="19" height="20" title="斜體字" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="劃底線" onclick="fnFontType('Underline');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef10.gif" width="19" height="18" title="劃底線" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="刪除線" onclick="fnFontType('StrikeThrough');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef11.gif" width="19" height="18" title="刪除線" /></div>
				</td>
				<td width="6"><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20"></td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="上標字" onclick="fnFontType('Superscript');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef12.gif" width="19" height="18" title="上標字" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="下標字" onclick="fnFontType('Subscript');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef13.gif" width="19" height="18" title="下標字" /></div>
				</td>
				<td width="6"><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="文字顏色" onclick="fnColorType('ForeColor');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef14.gif" width="19" height="20" title="文字顏色" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="背景顏色" onclick="fnColorType('BackColor');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef15.gif" width="19" height="20" title="背景顏色" /></div>
				</td>
				<td width="6"><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="靠左對齊" onclick="fnFontType('JustifyLeft');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef16.gif" width="19" height="20" title="靠左對齊" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="靠中對齊" onclick="fnFontType('JustifyCenter');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef17.gif" width="19" height="20" title="靠中對齊" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="靠右對齊" onclick="fnFontType('JustifyRight');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef18.gif" width="19" height="20" title="靠右對齊" /></div>
				</td>
				<td width="6"><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="增加縮排" onclick="fnFontType('Indent');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef19.gif" width="19" height="20" title="增加縮排" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="減少縮排" onclick="fnFontType('Outdent');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef20.gif" width="19" height="20" title="減少縮排" /></div>
				</td>
				<td width="6"><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="數字標題" onclick="fnOrderedList('InsertOrderedList');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef21.gif" width="19" height="20" title="數字標題" /></div>
				</td>
				<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="符號標題" onclick="fnOrderedList('InsertUnorderedList');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef22.gif" width="19" height="20" title="符號標題" /></div>
				</td>
				<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="插入分隔線" onclick="fnHorizontalRule();" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef23.gif" width="19" height="20" title="插入分隔線" /></div>
				</td>
				<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="插入表格" onclick="fnTableSeting();" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef24.gif" width="19" height="20" title="插入表格" /></div>
				</td>
				<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="插入連結" onclick="fnCreateLink();" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef25.gif" width="19" height="20" title="插入連結" /></div>
				</td>
				<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="跑馬燈" onclick="fnMarquee();" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef27.gif" width="19" height="20" title="跑馬燈" /></div>
				</td>
			</tr>
			</table>
		</td>
	</tr>
	<tr><td bgcolor="#d9cec4" align="left">
			<table cellspacing="1" cellpadding="0" border="0">
			<tr><td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
				<td	align="center" style="width:60px">網頁標題</td>
				<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td align="left"><asp:TextBox ID="tb_he_title" runat="server" Text="" Width="200px"></asp:TextBox>&nbsp;</td>
				<td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
				<td	align="center" style="width:60px">上傳檔案</td>
				<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td><asp:FileUpload ID="fu_file" runat="server" Height="20px" EnableViewState="False" /></td>
				<td><asp:Button ID="bn_send" runat="server" Text="上傳" Height="20px" onclick="bn_send_Click" OnClientClick="fnSynchronous();show_msg_wait()" /></td>
			</tr>
			</table>
		</td>
	</tr>
	<tr><td bgcolor="#d9cec4" align="left">
			<table cellspacing="1" cellpadding="0" border="0">
			<tr><td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
				<td	align="center" style="width:60px">備註說明</td>
				<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td align="left"><asp:TextBox ID="tb_he_desc" runat="server" Text="" Width="550px"></asp:TextBox>&nbsp;</td>
			</tr>
			</table>
		</td>
	</tr>
	</table>
	</center>

	<div id="EditArea" style="border-width:0px">
	<center>
	<table cellspacing="0" cellpadding="0" border="1" style="width:98%">
	<tr><td><iframe id="if_editor" style="width:100%; height:350px; background-color:white" marginwidth="1" src=""></iframe></td>		</tr>
	</table>
	</center>
	</div>

	<div id="HtmlArea" style="display:none; border-width:0px">
	<center>
	<table cellspacing="0" cellpadding="0" border="1" style="width:98%">
	<tr><td><asp:TextBox ID="tb_preview" CssClass="text9pt" runat="server" Rows="20" Width="100%" Height="350px" TextMode="MultiLine"></asp:TextBox></td></tr>
	</table>
	</center>
	</div>

	<center>
	<table cellspacing="0" cellpadding="0" border="1" style="width:98%">
	<tr bgcolor="#d9cec4">
		<td style="width:20px; vertical-align:middle">圖<br /><br />檔<br /><br />區</td>
		<td><iframe id="if_image" src="800111.aspx?sid=<%=lb_he_sid.Text%>" style="width: 100%; height: 150px; background-color:white" marginwidth="1"></iframe></td>
	</tr>
	</table>
	</center>	

	<p style="margin:10px 0pt 10px 0px; text-align:center">
		<asp:Button ID="bn_save" runat="server" Text="儲存檔案" ToolTip="存到資料庫" Height="24px" OnClientClick="fnSynchronous()" onclick="bn_save_Click" />&nbsp;&nbsp;
		<asp:Button ID="bn_leave" runat="server" Text="結束離開" ToolTip="不存檔離開" Height="24px" OnClientClick="fnReset()" onclick="bn_leave_Click" />
	</p>

	<center>
	<table cellspacing="0" cellpadding="0" border="0" style="width:98%">
	<tr><td style="width:20px; vertical-align:top; text-align:center">※</td>
		<td align="left">要編輯有圖形的網頁時，請先上傳圖檔。圖檔上傳完成後，會出現在圖檔區，此時再用滑鼠以拖曳的方式，將圖形拖放到設計區裡指定的位置。並以滑鼠點選拖拉圖形上的方格，來調整圖形的大小。</td>
	</tr>
	<tr><td style="width:20px; vertical-align:top; text-align:center">※</td>
		<td align="left">圖檔在圖檔區經刪除後，而設計區內仍有引用尚未刪除時，可能會因 IE 的 Cache 作用，還是會顯示出來。</td>
	</tr>
	</table>
	</center>	
	
	<!-- Begin 顯示上傳訊息 -->
	<div id="div_ban" class="text9pt" style="position:absolute; width:240px; z-index:0; display:none">
	<table id="tb_ban" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_ban.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<div id="div_msg_wait" class="text9pt" style="position:absolute; width:240px; z-index:0; display:none">
	<table border="2" cellpadding="2" cellspacing="2" style="width:240px; height:100px; background-color:#efefef">
	<tr><td align="center" class="text9pt">資料上傳中 ........<br />請勿按任何按鍵!</td></tr>
	</table>
	</div>
	<!-- End -->
	<script language="javascript" type="text/javascript">
		var edobj;
		var pvobj;
		start_edit();
			
		function start_edit() {
			if (document.frames) {		// IE
				edobj = document.frames["if_editor"].document;
				edobj.designMode = "On";
			}
			else {						// 非 IE
				var editobj = document.getElementById("if_editor");
				if (editobj != null) {
					editobj = editobj.document;
					editobj.designMode = "On";
				}
			}
			pvobj = document.getElementById("tb_preview");
		}
	</script>
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_md" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_he_sid" runat="server" Text="0" Visible="false"></asp:Label>
	</div>
	</form>
</body>
</html>
