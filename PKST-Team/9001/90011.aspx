<%@ Page Language="C#" AutoEventWireup="true" CodeFile="90011.aspx.cs" Inherits="_90011" validateRequest="false"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<title>廣告信發送管理</title>
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../js/htmledit.js"></script>
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
</script>
</head>
<body onload="fnLoadData()">
	<form id="form1" runat="server">
	<div style="border-bottom-width:0px">
	<center>
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family: 標楷體; text-align:center">廣告信發送管理-<asp:Label ID="lb_title" runat="server" Font-Size="14pt"></asp:Label> <span style="font-size:12pt; color:Blue">(適用 IE 瀏覽器)</span></p>
	<table border="1" cellspacing="0" cellpadding="0" style="width:98%">
	<tr><td bgcolor="#d9cec4" align="left">
			<table cellspacing="0" cellpadding="0" border="0">
			<tr><td><table cellspacing="1" cellpadding="0" border="0">
					<tr><td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
						<td style="width:60px; text-align:center; font-weight:bold; color:Blue"><span id="stitle">設計區</span></td>
						<td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
						<td	align="center" style="width:60px">郵件編號</td>
						<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
						<td align="center" style="width:60px; background-color:White; border-width:1px; border-color:Gray; border-style:solid">
							<asp:Literal ID="lt_sadm_sid" runat="server" Text=""></asp:Literal>
						</td>
						<td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
						<td	align="center" style="width:40px">格式</td>
						<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
						<td	align="center" style="width:100px; background-color:White; border-width:1px; border-color:Gray; border-style:solid">
							<asp:RadioButton ID="rb_adm_type_2" runat="server" GroupName="rb_adm_type" Text="HTML" Checked="True" />
							<asp:RadioButton ID="rb_adm_type_1" runat="server" GroupName="rb_adm_type" Text="TEXT" />&nbsp;
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
			</tr>
			</table>
		</td>
	</tr>
	<tr><td bgcolor="#d9cec4" align="left">
			<table cellspacing="1" cellpadding="0" border="0">
			<tr><td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
				<td	align="center" style="width:60px">郵件主旨</td>
				<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td align="left"><asp:TextBox ID="tb_adm_title" runat="server" Width="180px"></asp:TextBox>&nbsp;</td>
				<td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
				<td	align="center" style="width:60px">發送者姓名</td>
				<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td align="left"><asp:TextBox ID="tb_adm_fname" runat="server" Text="" Width="80px"></asp:TextBox>&nbsp;</td>
				<td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
				<td	align="center" style="width:60px">發送者郵箱</td>
				<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td align="left"><asp:TextBox ID="tb_adm_fmail" runat="server" Text="" Width="180px"></asp:TextBox>&nbsp;</td>
				<td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
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

	<p style="margin:10px 0pt 10px 0px; text-align:center">
		<asp:Button ID="bn_save" runat="server" Text="儲存檔案" ToolTip="存到資料庫" Height="24px" OnClientClick="fnSynchronous()" onclick="bn_save_Click" />&nbsp;&nbsp;
		<asp:Button ID="bn_leave" runat="server" Text="結束離開" ToolTip="不存檔離開" Height="24px" OnClientClick="fnReset()" onclick="bn_leave_Click" />
	</p>

	<center>
	<table cellspacing="0" cellpadding="0" border="0" style="width:98%">
	<tr><td style="width:20px; vertical-align:top; text-align:center">※</td>
		<td align="left">本範例的廣告郵件 Body 預設使用 HTML 格式。若使用 TEXT 格式發送，郵件內文如有 HTML Code 也會被收件者看到。</td>
	</tr>
	<tr><td style="width:20px; vertical-align:top; text-align:center">※</td>
		<td align="left">廣告信件，不建議使用附加檔案或是圖檔的郵件，這會使網路流量大增。如有需要，建議把圖檔或是檔案放在 Web Server上，廣告郵件中以超連結方式連回 Web Server。</td>
	</tr>
	</table>
	</center>

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
	<asp:Label ID="lb_adm_sid" runat="server" Text="0" Visible="false"></asp:Label>
	</div>
	</form>
</body>
</html>
