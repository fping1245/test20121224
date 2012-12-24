<%@ Page Language="C#" AutoEventWireup="true" CodeFile="4002.aspx.cs" Inherits="_4002" %>
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
	<p align="center" class="text14pt" style="margin:10pt 0pt 0pt 0pt; font-family:標楷體;">請點選名稱查看函數範例</p>
	<table border="1" cellpadding="8" cellspacing="0" class="text9pt" width="660pt" style="background-color: #FFEBCD">
	<tr align="center">
		<td style="width:220pt"><a href="40021.aspx">以數字取得天干<br />(甲、乙、丙...)<br />string GetHeavenlyStem(int)</a></td>
		<td style="width:220pt"><a href="40022.aspx">以數字取地支<br />(子、丑、寅...)<br />string GetEarthlyBranch(int)</a></td>
		<td style="width:220pt"><a href="40023.aspx">以數字取生肖<br />(鼠、牛、虎...)<br />string GetLunarZodiac(int)</a></td>
	</tr>
	<tr align="center">
		<td><a href="40024.aspx">以數字取得農曆月份<br />(正、一、二....)<br />string GetChMonth(int)</a></td>
		<td><a href="40025.aspx">以數字取得中文日期<br />(初一、十一、廿二、三十....)<br />string GetChDay(int)</a></td>
		<td><a href="40026.aspx">以小時對映農曆時辰<br />(子、丑、寅....)<br />string GetChHour(int)</a></td>
	</tr>
		<tr style="height:70pt" align="center">
		<td><a href="40027.aspx">以西元日期時間換算農曆日期時間<br />(甲子年十月廿一日辰時十二分三十秒)<br />string GetLunarDate()<br />string GetLunarDate(string)<br />string GetLunarDate(DateTime)<br />string GetLunarDate(DateTime, string)</a></td>
		<td><a href="40028.aspx">以西元日期取得農曆生肖<br />(鼠、牛、虎、兔 ...)<br />string GetDateLunarZodiac()<br />string GetDateLunarZodiac(DateTime)</a></td>
		<td><a href="40029.aspx">以西元日期取得西洋星座<br />(牡羊座、金牛座 ...)<br />string GetConstellation()<br />string GetConstellation(DateTime)</a></td>
	</tr>
	</table>
	</center>
	</div>
	</form>
</body>
</html>
