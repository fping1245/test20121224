<%@ Page Language="C#" AutoEventWireup="true" CodeFile="4004a.aspx.cs" Inherits="_4004a" %>
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
	<p align="center" class="text14pt" style="margin:10pt 0pt 0pt 0pt; font-family:�з���;">���ҳ̰��h���� (Top Level Domain)</p>
	<table width="98%" border="1" cellpadding="4" cellspacing="0" class="text9pt">
	<tr><td rowspan="4" style="width:120pt">���ҳ̰��h���� (Top Level Domain)</td>
		<td style="width:40pt">���A</td>
		<td align="left">int Check_TopLevelDomain(string)</td>
	</tr>
	<tr><td>��J</td>
		<td align="left"><asp:TextBox ID="tb_TopLevelDomain" runat="server" Text="tw"></asp:TextBox></td>
	</tr>
	<tr><td><asp:Button ID="bn_TopLevelDomain" runat="server" Text="��X" onclick="bn_TopLevelDomain_Click" /></td>
		<td align="left"><asp:Label ID="lb_TopLevelDomain" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>&nbsp;</td>
	</tr>
	</table>
	<p><a href="4004.aspx" class="abtn">&nbsp;�^�W�@��&nbsp;</a></p>&nbsp;
	</center>
	</div>
	</form>
</body>
</html>
