<%@ Page Language="C#" AutoEventWireup="true" CodeFile="40047.aspx.cs" Inherits="_40047" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>���Ҩ�ƼҲ�</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 0pt 0pt; font-family:�з���;">���Ҩ�ƼҲ�</p>
	<p align="center" class="text14pt" style="margin:10pt 0pt 0pt 0pt; font-family:�з���;">���Ұ�ڼзǼ��и��X (ISMN)<br />(�ŦX2008�~���᪺�W�d)</p>
	<table width="98%" border="1" cellpadding="4" cellspacing="0" class="text9pt">
	<tr><td rowspan="4" style="width:120pt">���Ұ�ڼзǼ��и��X<br />(ISMN)<br />13�X</td>
		<td style="width:40pt">���A</td>
		<td align="left">int Check_ISMN(string)</td>
	</tr>
	<tr><td>��J</td>
		<td align="left"><asp:TextBox ID="tb_ISMN" runat="server" Text=""></asp:TextBox></td>
	</tr>
	<tr><td><asp:Button ID="bn_ISMN" runat="server" Text="��X" onclick="bn_ISMN_Click" /></td>
		<td align="left"><asp:Label ID="lb_ISMN" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>&nbsp;</td>
	</tr>
	</table>
	<p><a href="4004.aspx" class="abtn">&nbsp;�^�W�@��&nbsp;</a></p>&nbsp;
	</center>
	</div>
	</form>
</body>
</html>
