<%@ Page Language="C#" AutoEventWireup="true" CodeFile="40017.aspx.cs" Inherits="_40017" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�r���ƼҲ�</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 0pt 0pt; font-family:�з���;">�r���ƼҲ�</p>
	<p align="center" class="text14pt" style="margin:10pt 0pt 0pt 0pt; font-family:�з���;">�Ӧ�Ʀr�त��Ʀr</p>
	<table width="98%" border="1" cellpadding="4" cellspacing="0" class="text9pt">
	<tr><td rowspan="4" style="width:120pt">�Ӧ�Ʀr�त��Ʀr</td>
		<td style="width:40pt">���A</td>
		<td align="left">string ChNumber(int)�G�� Int32 ������ন����Ʀr
			<br />string ChNumber(string)�G�� string ����Ʀr���ন����Ʀr
		</td>
	</tr>
	<tr><td>��J</td>
		<td align="left">���w�Ʀr�G<asp:TextBox ID="tb_ChNumber_int" runat="server" Text="5"></asp:TextBox></td>
	</tr>
	<tr><td><asp:Button ID="bn_ChNumber" runat="server" Text="��X" onclick="bn_ChNumber_Click" /></td>
		<td align="left"><asp:Label ID="lb_ChNumber" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>&nbsp;</td>
	</tr>
	</table>
	<p><a href="4001.aspx" class="abtn">&nbsp;�^�W�@��&nbsp;</a></p>&nbsp;
	</center>
	</div>
	</form>
</body>
</html>
