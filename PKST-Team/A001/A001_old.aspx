<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A001_old.aspx.cs" Inherits="_A001" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>票選活動(循序)</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">票選活動(循序)</p>
	<p class="text9pt" style="margin:0pt 0pt 5pt 0pt; text-align:center">(問卷依序平均顯示給上網的使用者填寫)</p>
	<table border="2" cellpadding="4" cellspacing="0" bgcolor="#F7F7DE" class="text9pt" style="width:98%; margin:0pt 0pt 5pt 0pt; border-color:#F0F0F0">
	<tr><td style="text-align:center; width:60pt; background-color:#99CCFF; color:#FFFFFF">問卷標題</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_title" runat="server" Text="&nbsp;" Font-Bold="true"></asp:Label></td></tr>
	<tr><td style="text-align:center; width:60pt; background-color:#99CCFF; color:#FFFFFF">內容說明</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_topic" runat="server" Text="&nbsp;"></asp:Label></td>
	</tr>
	<tr><td style="text-align:center; width:60pt; background-color:#99CCFF; color:#FFFFFF">填寫方式</td>
		<td style="text-align:left"><asp:Literal ID="lt_is_check" runat="server" Text="&nbsp;"></asp:Literal></td>
	</tr>
	<tr><td style="text-align:center; width:60pt; background-color:#99CCFF; color:#FFFFFF">選擇項目</td>
		<td style="text-align:left"><asp:Panel ID="pl_is_check" runat="server" Width="98%" HorizontalAlign="Left"></asp:Panel></td>
	</tr>
	</table>
	<p style="margin:10px 0px 0px 0px"><asp:LinkButton ID="lk_ok" runat="server" CssClass="abtn" onclick="lk_ok_Click">&nbsp;確定&nbsp;</asp:LinkButton></p>&nbsp;
	<asp:Label ID="lb_bh_sid" runat="server" Visible="false" Text="0"></asp:Label>
	<asp:Label ID="lb_is_check" runat="server" Visible="false" Text="0"></asp:Label>
	<asp:Label ID="lb_item_num" runat="server" Visible="false" Text="0"></asp:Label>
	</center>
	</div>
	</form>
</body>
</html>
