<%@ Page Language="C#" AutoEventWireup="true" CodeFile="4001b.aspx.cs" Inherits="_4001b" %>
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
	<p align="center" class="text14pt" style="margin:10pt 0pt 0pt 0pt; font-family:�з���;">����त��Ʀr</p>
	<table width="98%" border="1" cellpadding="4" cellspacing="0" class="text9pt">
	<tr><td rowspan="4" style="width:120pt">����त��Ʀr</td>
		<td style="width:40pt">���A</td>
		<td align="left">string GetChNumberFill(int)�G�� Int32 ������ন����Ʀr
			<br />string GetChNumberFill(Int64)�G�� Int64 ������ন����Ʀr
			<br />string GetChNumberFill(UInt64)�G�� UInt64 ������ন����Ʀr
			<br />string GetChNumberFill(string)�G�� string ������ন����Ʀr
		</td>
	</tr>
	<tr><td>��J</td>
		<td align="left">���w�Ʀr�G<asp:TextBox ID="tb_GetChNumberFill_int" runat="server" Text="10050"></asp:TextBox></td>
	</tr>
	<tr><td><asp:Button ID="bn_GetChNumberFill" runat="server" Text="��X" onclick="bn_GetChNumberFill_Click" /></td>
		<td align="left"><asp:Label ID="lb_GetChNumberFill" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>&nbsp;</td>
	</tr>
	</table>
	<p><a href="4001.aspx" class="abtn">&nbsp;�^�W�@��&nbsp;</a></p>&nbsp;
	</center>
	</div>
	</form>
</body>
</html>
