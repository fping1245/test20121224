<%@ Page Language="C#" AutoEventWireup="true" CodeFile="4004.aspx.cs" Inherits="_4004" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>驗證函數模組</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 0pt 0pt; font-family:標楷體;">驗證函數模組</p>
	<p align="center" class="text14pt" style="margin:10pt 0pt 0pt 0pt; font-family:標楷體;">請點選名稱查看函數範例</p>
	<table border="1" cellpadding="8" cellspacing="0" class="text9pt" width="660pt" style="background-color: #FFEBCD">
	<tr style="height:1px">
		<td style="width:220pt"></td>
		<td style="width:110pt"></td>
		<td style="width:110pt"></td>
		<td style="width:220pt"></td>
	</tr>
	<tr style="height:70pt; background-color:#FFEBDD" align="center">
		<td><a href="40048.aspx">驗證電子郵件信箱<br /><br />int Check_Email(string)</a></td>
		<td colspan="2"><a href="40049.aspx">伺服器位址(網域名稱)驗證<br /><br />int Check_Host(string)</a></td>
		<td><a href="4004a.aspx">驗證最高層網域 (Top Level Domain)<br /><br />int Check_TopLevelDomain((string)</a></td>
	</tr>
	<tr style="height:70pt" align="center">
		<td colspan="2"><a href="40041.aspx">驗證台灣身分證<br />(10碼)<br />int Check_TW_ID(string)</a></td>
		<td colspan="2"><a href="40044.aspx">驗證台灣營利事業統一編號<br />(8碼)<br />int Check_TW_INV(string)</a></td>
	</tr>
	<tr style="height:70pt" align="center">	
		<td colspan="2"><a href="40042.aspx">驗證大陸公民身份編號<br />(15碼及18碼)<br />int Check_CN_ID(string)</a></td>
		<td colspan="2"><a href="40043.aspx">驗證香港身份證號碼<br />(8碼)<br />int Check_HK_ID(string)</a></td>
	</tr>
	<tr style="height:70pt" align="center">
		<td><a href="40045.aspx">驗證國際標準期刊號碼 (ISSN)<br />(13碼及8碼)<br />int Check_ISSN(string)<br />int Check_ISSN8(string)</a></td>
		<td colspan="2"><a href="40046.aspx">驗證國際標準書籍號碼 (ISBN)<br />(13碼)<br />int Check_ISBN(string)</a></td>
		<td><a href="40047.aspx">驗證國際標準樂譜號碼 (ISMN)<br />(13碼)<br />int Check_ISMN(string)</a></td>
	</tr>
	</table>
	</center>
	</div>
	</form>
</body>
</html>
