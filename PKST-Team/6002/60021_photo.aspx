<%@ Page Language="C#" AutoEventWireup="true" CodeFile="60021_photo.aspx.cs" Inherits="_60021_photo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�޳q�T���޲z</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
</head>
<body>
<form id="form1" runat="server">
<div>
	<!-- Begin �л\��Ӥu�@�����A�����ϥΪ̦A���䥦���� -->
	<div id="fullwindow" style="position: absolute; top:0px; left:0px; width:1024px; height:768px; display:none">
	<table id="fulltable" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_back.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<!-- End -->
	<!-- Begin ���õ��� -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:�з���;">�q�T���޲z</p>
	<p align="center" class="text16pt" style="margin:0pt 0pt 5pt 0pt; font-family:�з���;">�ۤ��B�z</p>
	<center>
	<table cellspacing="0" cellpadding="4" border="1" style="background-color:White;border-color:#003366;border-width:1px;border-style:Double;width:580pt;border-collapse:collapse;">
		<tr align="left" style="background-color:#99FF99;height:24pt">
			<td align="center" style="width:90pt">�q�T���s��</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:Label ID="lb_ab_sid" runat="server"></asp:Label>
			</td>
			<td align="center" valign="middle" colspan="2" rowspan="4" style="background-color:#F7F7DE">
				<asp:Image ID="img_ab_photo" ImageUrl="~/images/ico/no_photo.gif" Width="120" 
					Height="120" BorderWidth="1px" runat="server" />
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:24pt">
			<td align="center" style="width:90pt">�s��</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:Label ID="lb_ag_name" runat="server"></asp:Label>
			</td>
		</tr>
		<tr align="left" style="width:90pt; background-color:#99FF99;height:24pt">
			<td align="center">�m�W</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:Label ID="lb_ab_name" runat="server"></asp:Label>
			</td>
		</tr>
		<tr align="left" style="width:90pt; background-color:#99FF99;height:24pt">
			<td align="center">�ʺ�</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:Label ID="lb_ab_nike" runat="server"></asp:Label>
			</td>
		</tr>
		<tr align="left" style="width:90pt; background-color:#99FF99;height:24pt">
			<td align="center">�W�ǷӤ�</td>
			<td style="width:200pt; background-color:#F7F7DE" align="center">
				<asp:FileUpload ID="fu_file" Width="140pt" Height="24px" runat="server" />
				<asp:Button ID="bn_send" runat="server" Height="24px" CssClass="text10pt" Text="�T�w" onclick="bn_send_Click" OnClientClick="show_msg_wait()" />
			</td>
			<td align="center">�R���Ӥ�</td>
			<td style="width:200pt; background-color:#F7F7DE" align="center">
				<asp:Button ID="bn_del" runat="server" Height="24px" CssClass="text10pt" Text="�T�w�R��" onclick="bn_del_Click" />
			</td>
		</tr>
	</table>	
	<p style="margin:10pt 0pt 10pt 0pt"><a href="60021.aspx<%=lb_page.Text%>" class="abtn">&nbsp;�^�W�@��&nbsp;</a></p>&nbsp;
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
	</center>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
</div>
</form>
</body>
</html>
