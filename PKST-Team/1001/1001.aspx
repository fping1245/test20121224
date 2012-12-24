<%@ Page Language="C#" AutoEventWireup="true" CodeFile="1001.aspx.cs" Inherits="_1001" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>字串加密解密範例</title>
</head>
<body>
<form id="form1" runat="server">
<div>
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體;">字串加密解密範例</p>
	<table width="450" align="center" border="1" cellpadding="6" cellspacing="0" class="text12pt">
	<tr align="center"><td colspan="2" bgcolor="#99CCFF"><font color="#FFFFFF">字串加密</font></td></tr>
	<tr align="center">
		<td width="80" bgcolor="lightyellow">原始字串</td>
		<td><asp:TextBox ID="tb_source" runat="server" Width="330"></asp:TextBox></td>
	</tr>
	<tr align="center">
		<td colspan="2" bgcolor="#fff0f0">↓↓&nbsp;<asp:Button ID="bn_encode" runat="server" Text="加密" CssClass="text11pt" onclick="bn_encode_Click" />&nbsp;↓↓</td>
	</tr>
	<tr align="center">
		<td bgcolor="lightyellow">加密字串</td>
		<td><asp:Label ID="lb_encrypt" runat="server" Text=""></asp:Label>&nbsp;</td>
	</tr>	
    </table>
    <hr style="width:500px" />
	<table width="450" align="center" border="1" cellpadding="6" cellspacing="0" class="text12pt">
	<tr align="center"><td colspan="2" bgcolor="#99CCFF"><font color="#FFFFFF">字串解密</font></td></tr>
	<tr align="center">
		<td width="80" bgcolor="lightyellow">加密字串</td>
		<td><asp:TextBox ID="tb_encrypt" runat="server" Width="330"></asp:TextBox></td>
	</tr>
	<tr align="center">
		<td colspan="2" bgcolor="#fff0f0">↓↓&nbsp;<asp:Button ID="bn_decode" runat="server" Text="解密" CssClass="text11pt" onclick="bn_decode_Click" />&nbsp;↓↓</td>
	</tr>
	<tr align="center">
		<td bgcolor="lightyellow">原始字串</td>
		<td><asp:Label ID="lb_source" runat="server" Text=""></asp:Label>&nbsp;</td>
	</tr>	
    </table>
</div>
</form>
</body>
</html>
