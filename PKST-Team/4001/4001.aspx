<%@ Page Language="C#" AutoEventWireup="true" CodeFile="4001.aspx.cs" Inherits="_4001" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>字串函數模組</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 0pt 0pt; font-family:標楷體;">字串函數模組</p>
	<p align="center" class="text14pt" style="margin:10pt 0pt 0pt 0pt; font-family:標楷體;">請點選名稱查看函數範例</p>
	<table border="1" cellpadding="8" cellspacing="0" class="text9pt" width="660pt" style="background-color: #FFEBCD">
	<tr align="center">
		<td style="width:220pt"><a href="40011.aspx">字串是否為整數<br />bool IsInteger(string)</a></td>
		<td style="width:220pt"><a href="40012.aspx">擷取左側字元<br />string Left(string)<br />string Left(string, int)</a></td>
		<td style="width:220pt"><a href="40013.aspx">擷取右側字元<br />string Right(string)<br />string Right(string, int)</a></td>
	</tr>
	<tr align="center">
		<td><a href="40014.aspx">左方填滿字元<br />string FillLeft(string1, int)<br />string FillLeft(string1, int, string2)</a></td>
		<td><a href="40015.aspx">右方填滿字元<br />string FillRight(string1, int)<br />string FillRight(string1, int, string2)</a></td>
		<td><a href="40016.aspx">產生重覆字串<br />string Duplicate(string, int)</a></td>
	</tr>
	<tr style="height:70pt" align="center">
		<td><a href="40017.aspx">個位數字轉中文數字<br />(零、一、二....)<br />string ChNumber(int)<br />string ChNumber(string)</a></td>
		<td><a href="40018.aspx">個位數字轉中文大寫數字<br />(零、壹、貳...)<br />string ChCapitalNumber(int)<br />string ChCapitalNumber(string)</a></td>
		<td><a href="40019.aspx">每四位數的中文位數字<br />(萬、億、兆...)<br />string GetFourChNumber(int)</a></td>
	</tr>
	<tr align="center">
		<td><a href="4001a.aspx">整數轉中文數字<br />（10050 => 一萬零五十）<br />string GetChNumber(int)<br />string GetChNumber(Int64)<br />string GetChNumber(UInt64)<br />string GetChNumber(string)</a></td>
		<td><a href="4001b.aspx">整數轉中文數字<br />（10050 => 一萬零千零百五十零）<br />string GetChNumberFill(int)<br />string GetChNumberFill(Int64)<br />string GetChNumberFill(UInt64)<br />string GetChNumberFill(string)</a></td>
		<td><a href="4001c.aspx">整數轉簡略中文數字<br />（10050 => 一零零五零）<br />string GetChNumberShort(int)<br />string GetChNumberShort(Int64)<br />string GetChNumberShort(UInt64)<br />string GetChNumberShort(string)</a></td>
	</tr>
	<tr align="center">
		<td><a href="4001d.aspx">整數轉中文大寫數字<br />（10050 => 壹萬零伍拾）<br />string GetChCapitalNumber(int)<br />string GetChCapitalNumber(Int64)<br />string GetChCapitalNumber(UInt64)<br />string GetChCapitalNumber(string)</a></td>
		<td><a href="4001e.aspx">整數轉中文大寫數字<br />（10050 => 壹萬零仟零佰伍拾零）<br />string GetChCapitalNumberFill(int)<br />string GetChCapitalNumberFill(Int64)<br />string GetChCapitalNumberFill(UInt64)<br />string GetChCapitalNumberFill(string)</a></td>
		<td><a href="4001f.aspx">整數轉簡略中文大寫數字<br />（10050 => 壹零零伍零）<br />string GetChCapitalNumberShort(int)<br />string GetChCapitalNumberShort(Int64)<br />string GetChCapitalNumberShort(UInt64)<br />string GetChCapitalNumberShort(string)</a></td>
	</tr>
	</table>
	</center>
	</div>
	</form>
</body>
</html>
