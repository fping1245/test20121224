<%@ Page Language="C#" AutoEventWireup="true" CodeFile="40027.aspx.cs" Inherits="_40027" %>
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
	<p align="center" class="text14pt" style="margin:10pt 0pt 0pt 0pt; font-family:標楷體;">以西元日期時間換算農曆日期時間</p>
	<table width="98%" border="1" cellpadding="4" cellspacing="0" class="text9pt">
	<tr><td rowspan="6" style="width:120pt">以西元日期時間換算農曆日期時間</td>
		<td style="width:40pt">型態</td>
		<td align="left">string GetLunarDate()：今天的完整農曆日期時間
			<br />string GetLunarDate(string)：今天的指定格式農曆日期時間
			<br />string GetLunarDate(DateTime)：指定日期的完整農曆日期時間
			<br />string GetLunarDate(DateTime, string)：指定日期及格式的農曆日期時間
		</td>
	</tr>
	<tr><td>輸入</td>
		<td align="left">日期：<asp:TextBox ID="tb_GetLunarDate_datetime" runat="server" Text="1"></asp:TextBox>
			<br />格式：<asp:TextBox ID="tb_GetLunarDate_format" runat="server" Text="1"></asp:TextBox>
		</td>
	</tr>
	<tr><td><asp:Button ID="bn_GetLunarDate" runat="server" Text="輸出" onclick="bn_GetLunarDate_Click" /></td>
		<td align="left"><asp:Label ID="lb_GetLunarDate" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td>格式</td>
		<td align="left">
			<li>y:年&nbsp;&nbsp;例：甲子年</li>
			<li>M:月&nbsp;&nbsp;例：十月</li>
			<li>d:日&nbsp;&nbsp;例：廿一日</li>
			<li>H:時&nbsp;&nbsp;例：辰時</li>
			<li>h:時&nbsp;&nbsp;例：十一時</li>
			<li>m:分&nbsp;&nbsp;例：十二分</li>
			<li>s:秒&nbsp;&nbsp;例：三十秒</li>
		</td>
	</tr>

	</table>
	<p><a href="4002.aspx" class="abtn">&nbsp;回上一頁&nbsp;</a></p>&nbsp;
	</center>
	</div>
	</form>
</body>
</html>
