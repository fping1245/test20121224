<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A00352.aspx.cs" Inherits="_A00352" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>票選資料管理</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="1" cellpadding="4" cellspacing="0" style="width:550px; background-color:#EFEFEF">
	<tr><td align="center">
			<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">修改問卷項目</p>
			<table border="1" cellpadding="4" cellspacing="0" style="width:530px; background-color:#F7F7DE">
			<tr><td style="text-align:center; width:80px; background-color:#99CCFF; color:#FFFFFF">顯示順序</td>
				<td align="left"><asp:TextBox ID="tb_bi_sort" runat="server" Text="255" Width="60px"></asp:TextBox>(0 ~ 255)</td>
			</tr>
			<tr><td style="text-align:center; width:80px; background-color:#99CCFF; color:#FFFFFF">項目文字</td>
				<td align="left"><asp:TextBox ID="tb_bi_desc" runat="server" MaxLength="50" Width="440px"></asp:TextBox></td>
			</tr>
			</table>
			<p style="margin:10pt 0pt 0pt 0pt">
				<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;存檔&nbsp;</asp:LinkButton>&nbsp;&nbsp;
				<a href="javascript:parent.location.reload(true)" class="abtn">&nbsp;取消&nbsp;</a>
			</p>&nbsp;
	</td></tr>
	</table>
	</center>
	<asp:Label ID="lb_bh_sid" runat="server" Text="0" Visible="false"></asp:Label>
	<asp:Label ID="lb_bi_sid" runat="server" Text="0" Visible="false"></asp:Label>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
