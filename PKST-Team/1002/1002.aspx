<%@ Page Language="C#" AutoEventWireup="true" CodeFile="1002.aspx.cs" Inherits="_1002" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<title>圖文驗證模組範例</title>
<link href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<div>
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體;">圖文驗證範例</p>
	<table width="500" align="center" border="1" cellpadding="4" cellspacing="0" class="text12pt">
	<tr align="center">
		<td width="160" bgcolor="lightyellow">重新產生驗證圖文</td>
		<td><asp:Button ID="bn_rebuild" runat="server" Text="重新產生驗證圖文" CssClass="text11pt" onclick="bn_rebuild_Click" Width="120pt" /></td>
	</tr>
	<tr align="center">
		<td bgcolor="lightyellow">文字圖檔產生驗證</td>
		<td><asp:Image ID="img_pw" ImageUrl="10021.ashx" runat="server" Height="54px" Width="200px" /></td>
	</tr>
	<tr align="center">
		<td bgcolor="lightyellow">系統文字產生驗證</td>
		<td><asp:Image ID="img_text" ImageUrl="10022.ashx" runat="server" Height="54px" Width="200px" /></td>
	</tr>
	</table>
	<hr style="width:500px" />
	<table width="500" align="center" border="1" cellpadding="4" cellspacing="0" class="text12pt">
	<tr align="center">
		<td width="160" bgcolor="lightyellow">輸入所看到的圖文</td>
		<td><asp:TextBox ID="tb_pw" runat="server" Width="250"></asp:TextBox></td>
	</tr>
	<tr align="center">
		<td colspan="2"><asp:Button ID="bn_ok" runat="server" Text="確認輸入文字" CssClass="text11pt" Width="90pt" onclick="bn_ok_Click" /></td>
	</tr>
	</table>
	<p style="text-align:center" class="text12pt"><asp:Label ID="lb_msg" runat="server" Text="" Visible="false"></asp:Label></p>
</div>
</form>
</body>
</html>