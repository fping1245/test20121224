<%@ Page Language="C#" AutoEventWireup="true" CodeFile="40026.aspx.cs" Inherits="_40026" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>曆法函數模組</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 0pt 0pt; font-family:標楷體;">曆法函數模組</p>
	<p align="center" class="text14pt" style="margin:10pt 0pt 0pt 0pt; font-family:標楷體;">以小時對映農曆時辰</p>
	<table width="98%" border="1" cellpadding="4" cellspacing="0" class="text9pt">
	<tr><td rowspan="5" style="width:120pt">以小時對映農曆時辰</td>
		<td style="width:40pt">型態</td>
		<td align="left">string GetChHour(int)</td>
	</tr>
	<tr><td>輸入</td>
		<td align="left"><asp:TextBox ID="tb_GetChHour_int" runat="server" Text="1"></asp:TextBox></td>
	</tr>
	<tr><td><asp:Button ID="bn_GetChHour" runat="server" Text="輸出" onclick="bn_GetChHour_Click" /></td>
		<td align="left"><asp:Label ID="lb_GetChHour" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>&nbsp;</td>
	</tr>

	</table>
	<p><a href="4002.aspx" class="abtn">&nbsp;回上一頁&nbsp;</a></p>&nbsp;
	</center>
	</div>
	</form>
</body>
</html>
