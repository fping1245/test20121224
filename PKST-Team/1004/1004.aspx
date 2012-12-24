<%@ Page Language="C#" AutoEventWireup="true" CodeFile="1004.aspx.cs" Inherits="_1004" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>個人密碼修改範例</title>
</head>
<body>
<form id="form1" runat="server">
<div>
	<p align="center" class="text18pt" style="margin:10pt 0pt 10pt 0pt; font-family:標楷體;">個人密碼修改範例</p>
	<table width="350" align="center" border="1" cellpadding="4" cellspacing="0" class="text12pt">
	<tr align="center">
		<td bgcolor="lightyellow" width="100" class="text12pt">使用者帳號</td>
		<td><asp:TextBox ID="tb_id" runat="server" Width="95%" CssClass="text12pt"></asp:TextBox></td>
	</tr>
	<tr align="center">
		<td bgcolor="lightyellow" class="text12pt">原登入密碼</td>
		<td><asp:TextBox ID="tb_spass" runat="server" Width="95%" TextMode="Password" CssClass="text12pt"></asp:TextBox></td>
	</tr>
	<tr align="center">
		<td bgcolor="lightyellow" class="text12pt" title="「新登入密碼」請輪入4~12個字">新登入密碼</td>
		<td><asp:TextBox ID="tb_npass" runat="server" Width="95%" TextMode="Password" CssClass="text12pt" ToolTip="「新登入密碼」請輪入4~12個字"></asp:TextBox></td>
	</tr>
	<tr align="center">
		<td bgcolor="lightyellow" class="text12pt" title="「新密碼密碼」請輪入4~12個字">新密碼確認</td>
		<td><asp:TextBox ID="tb_rpass" runat="server" Width="95%" TextMode="Password" CssClass="text12pt" ToolTip="「新密碼密碼」請輪入4~12個字"></asp:TextBox></td>
	</tr>
	</table>
</div>
<p style="text-align:center"><asp:Button ID="bn_ok" runat="server" Text="確定" CssClass="text12pt" onclick="bn_ok_Click" /></p>
<p style="text-align:center; color:Red">僅限登入者本身修改之</p>
</form>
</body>
</html>
