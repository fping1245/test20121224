<%@ Page Language="C#" AutoEventWireup="true" CodeFile="40018.aspx.cs" Inherits="_40018" %>
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
	<p align="center" class="text14pt" style="margin:10pt 0pt 0pt 0pt; font-family:�з���;">�Ӧ�Ʀr�त��j�g�Ʀr</p>
	<table width="98%" border="1" cellpadding="4" cellspacing="0" class="text9pt">
	<tr><td rowspan="4" style="width:120pt">�Ӧ�Ʀr�त��j�g�Ʀr</td>
		<td style="width:40pt">���A</td>
		<td align="left">string ChCapitalNumber(int)�G�� Int32 ������ন����j�g�Ʀr
			<br />string ChCapitalNumber(string)�G�� string ����Ʀr���ন����j�g�Ʀr
		</td>
	</tr>
	<tr><td>��J</td>
		<td align="left">���w�Ʀr�G<asp:TextBox ID="tb_ChCapitalNumber_int" runat="server" Text="5"></asp:TextBox></td>
	</tr>
	<tr><td><asp:Button ID="bn_ChCapitalNumber" runat="server" Text="��X" onclick="bn_ChCapitalNumber_Click" /></td>
		<td align="left"><asp:Label ID="lb_ChCapitalNumber" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>&nbsp;</td>
	</tr>
	</table>
	<p><a href="4001.aspx" class="abtn">&nbsp;�^�W�@��&nbsp;</a></p>&nbsp;
	</center>
	</div>
	</form>
</body>
</html>
