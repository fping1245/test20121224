<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E001.aspx.cs" Inherits="_E001" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>文章繁簡轉換範例</title>
</head>
<body>
<form id="form1" runat="server">
<div>
<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體;">文章繁簡轉換範例</p>
<center>
<table width="98%" border="1" cellpadding="4" cellspacing="0" class="text12pt" style="">
<tr align="center">
	<td width="49%">繁體字</td>
	<td width="2%">←→</td>
	<td>简体字</td>
</tr>
<tr valign="middle" align="left">
	<td><asp:TextBox ID="tb_big5" runat="server" Rows="10" TextMode="MultiLine" ToolTip="請在此處輸入繁體字" Width="100%"></asp:TextBox></td>
	<td align="center">
		<asp:Button ID="bn_togb" runat="server" Text="簡" CssClass="text12pt" onclick="bn_togb_Click" ToolTip="转换成简体字" />
		<br />→<br /><br />←<br />
		<asp:Button ID="bn_tobig5" runat="server" Text="繁" CssClass="text12pt" onclick="bn_tobig5_Click" ToolTip="轉換成繁體字" />
	</td>
	<td><asp:TextBox ID="tb_gb" runat="server" Rows="10" TextMode="MultiLine" ToolTip="请在此处输入简体字" Width="100%"></asp:TextBox></td>
</tr>
</table>
</center>
</div>
</form>
</body>
</html>