<%@ Page Language="C#" AutoEventWireup="true" CodeFile="80011.aspx.cs" Inherits="_80011" validateRequest="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<title>HTML�s�边</title>
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" type="text/javascript" src="../js/htmledit.js"></script>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<style type="text/css">
	td
	{
		font-size: 12px;
		font-family: �ө���;
	}
	button
	{
		font-size: 12px;
		font-family: �ө���;
	}
	input
	{
		font-size: 12px;
		font-family: �ө���;
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
			alert("���\�ण�䴩�u" + navigator.appName + "�v�I\n�i��|�����~�εL�k���檺���p�I\n");
		edobj.body.innerHTML = pvobj.value;
	}

	// ��s If_Image
	function Renew_Image(sid) {
		var ifobj = document.getElementById("if_image");

		ifobj.src = "800111.aspx?sid=" + sid;
	}
</script>
</head>
<body onload="fnLoadData()">
	<form id="form1" runat="server">
	<div style="border-width:0px">

	<!-- Begin �л\��Ӥu�@�����A�����ϥΪ̦A���䥦���� -->
	<div id="fullwindow" style="position: absolute; top:0px; left:0px; width:1024px; height:768px; display:none">
	<table id="fulltable" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_back.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<!-- End -->

	<center>
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family: �з���; text-align:center">HTML�s�边-<asp:Label ID="lb_title" runat="server" Font-Size="14pt"></asp:Label> <span style="font-size:12pt; color:Blue">(�A�� IE �s����)</span></p>
	<table border="1" cellspacing="0" cellpadding="0" style="width:98%">
	<tr><td bgcolor="#d9cec4" align="left">
			<table cellspacing="0" cellpadding="0" border="0">
			<tr><td><table cellspacing="1" cellpadding="0" border="0">
					<tr><td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
						<td style="width:60px; text-align:center; font-weight:bold; color:Blue"><span id="stitle">�]�p��</span></td>
						<td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
						<td	align="center" style="width:60px">�����s��</td>
						<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
						<td align="center" style="width:60px; background-color:White; border-width:1px; border-color:Gray; border-style:solid">
							<asp:Literal ID="lt_she_sid" runat="server" Text=""></asp:Literal>
						</td>
						<td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
						<td><div onclick="fnShowHtml();" id="shtml" title="��ܭ�l��">
							<img id="simg" alt="" src="../images/htmlimage/ef37.gif" width="19" height="20" title="��ܭ�l��" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�s��(�ӤH�ؿ�)" onclick="fnSave();" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef01.gif" width="19" height="20" title="�s��(�ӤH�ؿ�)" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�M�����e" onclick="fnClearCode();" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef39.gif" width="19" height="20" title="�M�����e" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�C�L" onclick="fnPrint();" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef02.gif" width="19" height="20" title="�C�L" /></div>
						</td>
						<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
					</tr>
					</table>
				</td>
				<td id="FuncArea1">
					<table cellspacing="1" cellpadding="0" border="0">
					<tr><td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�ŤU" onclick="fnModify('Cut');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef03.gif" width="19" height="20" title="�ŤU" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�ƻs" onclick="fnModify('Copy');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef04.gif" width="19" height="20" title="�ƻs" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�K�W" onclick="fnModify('Paste');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef05.gif" width="19" height="20" title="�K�W" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�R��" onclick="fnModify('Delete');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef06.gif" width="19" height="20" title="�R��" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�_��" onclick="fnModify('Undo');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef07.gif" width="19" height="20" title="�_��" /></div>
						</td>
						<td><img alt="" src="../images/htmlimage/eh01.gif" alt="" width="6" height="20" /></td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�֨����" onclick="fnFormItem('InsertInputCheckbox','');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef28.gif" width="19" height="20" title="�֨����" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�I���" onclick="fnFormItem('InsertInputRadio','');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef29.gif" width="19" height="20" title="�I���" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="��r��J" onclick="fnFormItem('InsertInputText','Y');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef30.gif" width="19" height="20" title="��r��J" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�K�X��J" onclick="fnFormItem('InsertInputPassword','Y');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef31.gif" width="19" height="20" title="�K�X��J" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�����" onclick="fnFormItem('InsertSelectDropdown','');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef32.gif" width="19" height="20" title="�����" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="��r���" onclick="fnFormItem('InsertTextArea','Y');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef33.gif" width="19" height="20" title="��r���" /></div>
						</td>
						<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="button ���s" onclick="fnFormItem('InsertInputButton','Y');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef34.gif" width="19" height="20" title="button ���s" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="submit ���s" onclick="fnFormItem('InsertInputSubmit','Y');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef35.gif" width="19" height="20" title="submit ���s" /></div>
						</td>
						<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="reset ���s" onclick="fnFormItem('InsertInputReset','Y');" onmouseout="fnBnUp(this);">
							<img alt="" src="../images/htmlimage/ef36.gif" width="19" height="20" title="reset ���s" /></div>
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
						<option value="">�r��</option>
						<option value="�s�ө���">�s�ө���</option>
						<option value="�з���">�з���</option>
						<option value="�ө���">�ө���</option>
						<option value="arial">Arial</option>
						<option value="wingdings">Wingdings</option>
					</select>
				</td>
				<td width="71">
					<select onchange="fnFontSize(this[this.selectedIndex].value);this.selectedIndex=0;">
						<option value="">�j�p</option>
						<option value="1">1</option>
						<option value="2">2</option>
						<option value="3">3(�w�])</option>
						<option value="4">4</option>
						<option value="5">5</option>
						<option value="6">6</option>
						<option value="7">7 </option>
					</select>
				</td>
				<td width="6"><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="����r" onclick="fnFontType('Bold');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef08.gif" width="19" height="20" title="����r" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="����r" onclick="fnFontType('Italic');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef09.gif" width="19" height="20" title="����r" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�����u" onclick="fnFontType('Underline');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef10.gif" width="19" height="18" title="�����u" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�R���u" onclick="fnFontType('StrikeThrough');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef11.gif" width="19" height="18" title="�R���u" /></div>
				</td>
				<td width="6"><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20"></td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�W�Цr" onclick="fnFontType('Superscript');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef12.gif" width="19" height="18" title="�W�Цr" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�U�Цr" onclick="fnFontType('Subscript');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef13.gif" width="19" height="18" title="�U�Цr" /></div>
				</td>
				<td width="6"><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="��r�C��" onclick="fnColorType('ForeColor');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef14.gif" width="19" height="20" title="��r�C��" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�I���C��" onclick="fnColorType('BackColor');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef15.gif" width="19" height="20" title="�I���C��" /></div>
				</td>
				<td width="6"><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�a�����" onclick="fnFontType('JustifyLeft');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef16.gif" width="19" height="20" title="�a�����" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�a�����" onclick="fnFontType('JustifyCenter');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef17.gif" width="19" height="20" title="�a�����" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�a�k���" onclick="fnFontType('JustifyRight');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef18.gif" width="19" height="20" title="�a�k���" /></div>
				</td>
				<td width="6"><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�W�[�Y��" onclick="fnFontType('Indent');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef19.gif" width="19" height="20" title="�W�[�Y��" /></div>
				</td>
				<td width="20">
					<div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="����Y��" onclick="fnFontType('Outdent');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef20.gif" width="19" height="20" title="����Y��" /></div>
				</td>
				<td width="6"><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�Ʀr���D" onclick="fnOrderedList('InsertOrderedList');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef21.gif" width="19" height="20" title="�Ʀr���D" /></div>
				</td>
				<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�Ÿ����D" onclick="fnOrderedList('InsertUnorderedList');" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef22.gif" width="19" height="20" title="�Ÿ����D" /></div>
				</td>
				<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="���J���j�u" onclick="fnHorizontalRule();" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef23.gif" width="19" height="20" title="���J���j�u" /></div>
				</td>
				<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="���J���" onclick="fnTableSeting();" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef24.gif" width="19" height="20" title="���J���" /></div>
				</td>
				<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="���J�s��" onclick="fnCreateLink();" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef25.gif" width="19" height="20" title="���J�s��" /></div>
				</td>
				<td><div onmouseup="fnBnDown(this);" onmousedown="fnBnClick(this);" onmouseover="fnBnDown(this);" title="�]���O" onclick="fnMarquee();" onmouseout="fnBnUp(this);">
					<img alt="" src="../images/htmlimage/ef27.gif" width="19" height="20" title="�]���O" /></div>
				</td>
			</tr>
			</table>
		</td>
	</tr>
	<tr><td bgcolor="#d9cec4" align="left">
			<table cellspacing="1" cellpadding="0" border="0">
			<tr><td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
				<td	align="center" style="width:60px">�������D</td>
				<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td align="left"><asp:TextBox ID="tb_he_title" runat="server" Text="" Width="200px"></asp:TextBox>&nbsp;</td>
				<td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
				<td	align="center" style="width:60px">�W���ɮ�</td>
				<td><img alt="" src="../images/htmlimage/es01.gif" width="6" height="20" /></td>
				<td><asp:FileUpload ID="fu_file" runat="server" Height="20px" EnableViewState="False" /></td>
				<td><asp:Button ID="bn_send" runat="server" Text="�W��" Height="20px" onclick="bn_send_Click" OnClientClick="fnSynchronous();show_msg_wait()" /></td>
			</tr>
			</table>
		</td>
	</tr>
	<tr><td bgcolor="#d9cec4" align="left">
			<table cellspacing="1" cellpadding="0" border="0">
			<tr><td><img alt="" src="../images/htmlimage/eh01.gif" width="6" height="20" /></td>
				<td	align="center" style="width:60px">�Ƶ�����</td>
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
		<td style="width:20px; vertical-align:middle">��<br /><br />��<br /><br />��</td>
		<td><iframe id="if_image" src="800111.aspx?sid=<%=lb_he_sid.Text%>" style="width: 100%; height: 150px; background-color:white" marginwidth="1"></iframe></td>
	</tr>
	</table>
	</center>	

	<p style="margin:10px 0pt 10px 0px; text-align:center">
		<asp:Button ID="bn_save" runat="server" Text="�x�s�ɮ�" ToolTip="�s���Ʈw" Height="24px" OnClientClick="fnSynchronous()" onclick="bn_save_Click" />&nbsp;&nbsp;
		<asp:Button ID="bn_leave" runat="server" Text="�������}" ToolTip="���s�����}" Height="24px" OnClientClick="fnReset()" onclick="bn_leave_Click" />
	</p>

	<center>
	<table cellspacing="0" cellpadding="0" border="0" style="width:98%">
	<tr><td style="width:20px; vertical-align:top; text-align:center">��</td>
		<td align="left">�n�s�観�ϧΪ������ɡA�Х��W�ǹ��ɡC���ɤW�ǧ�����A�|�X�{�b���ɰϡA���ɦA�ηƹ��H�즲���覡�A�N�ϧΩ���]�p�ϸ̫��w����m�C�åH�ƹ��I���ԹϧΤW�����A�ӽվ�ϧΪ��j�p�C</td>
	</tr>
	<tr><td style="width:20px; vertical-align:top; text-align:center">��</td>
		<td align="left">���ɦb���ɰϸg�R����A�ӳ]�p�Ϥ������ޥΩ|���R���ɡA�i��|�] IE �� Cache �@�ΡA�٬O�|��ܥX�ӡC</td>
	</tr>
	</table>
	</center>	
	
	<!-- Begin ��ܤW�ǰT�� -->
	<div id="div_ban" class="text9pt" style="position:absolute; width:240px; z-index:0; display:none">
	<table id="tb_ban" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_ban.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<div id="div_msg_wait" class="text9pt" style="position:absolute; width:240px; z-index:0; display:none">
	<table border="2" cellpadding="2" cellspacing="2" style="width:240px; height:100px; background-color:#efefef">
	<tr><td align="center" class="text9pt">��ƤW�Ǥ� ........<br />�Фū��������!</td></tr>
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
			else {						// �D IE
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
