<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B00152.aspx.cs" Inherits="_B00152" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>考試題庫管理</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="1" cellpadding="4" cellspacing="0" style="width:100%; background-color:#EFEFEF">
	<tr><td align="center">
		<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">修改考生資料</p>
		<table border="1" cellpadding="4" cellspacing="0" style="width:99%; background-color:#F7F7DE">
		<tr><td style="text-align:center; width:80px; background-color:#99CCFF; color:#FFFFFF">試卷標題</td>
			<td align="left"><asp:Label ID="lb_tp_title" runat="server"></asp:Label></td>
		</tr>
		<tr><td style="text-align:center; width:80px; background-color:#99CCFF; color:#FFFFFF">姓名</td>
			<td align="left"><asp:TextBox ID="tb_tu_name" runat="server" MaxLength="20" Width="70%"></asp:TextBox> (2~20個字)</td>
		</tr>
		<tr><td style="text-align:center; width:80px; background-color:#99CCFF; color:#FFFFFF">學號</td>
			<td align="left"><asp:TextBox ID="tb_tu_no" runat="server" MaxLength="10" Width="70%"></asp:TextBox> (4~10個字)</td>
		</tr>
		<tr><td style="text-align:center; width:80px; background-color:#99CCFF; color:#FFFFFF">登入IP</td>
			<td align="left"><asp:TextBox ID="tb_tu_ip" runat="server" MaxLength="15" Width="70%"></asp:TextBox> (15個字)</td>
		</tr>
		<tr><td style="text-align:center; width:80px; background-color:#99CCFF; color:#FFFFFF">已作答</td>
			<td align="left"><asp:Label ID="lb_is_test" runat="server"></asp:Label></td>
		</tr>
		</table>
		<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;確定存檔&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.close_all();" class="abtn">&nbsp;取消修改&nbsp;</a>
		</p>&nbsp;
	</td></tr>
	</table>
	</center>
	<asp:Label ID="lb_tp_sid" runat="server" Text="0" Visible="false"></asp:Label>
	<asp:Label ID="lb_tu_sid" runat="server" Text="0" Visible="false"></asp:Label>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
