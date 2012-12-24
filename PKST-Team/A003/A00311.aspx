<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A00311.aspx.cs" Inherits="_A00311" %>
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
	<table border="1" cellpadding="4" cellspacing="0" style="width:500px; background-color:#EFEFEF">
	<tr><td align="center">
		<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">修改問卷排程</p>
		<table border="1" cellpadding="4" cellspacing="0" style="width:480px; background-color:#F7F7DE">
		<tr><td style="text-align:center; width:80px; background-color:#99CCFF; color:#FFFFFF">票選標題</td>
			<td align="left"><asp:Label ID="lb_bh_title" runat="server"></asp:Label></td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">是否顯示</td>
			<td align="left">
				<asp:RadioButton ID="rb_is_show1" runat="server" Text="顯示" CssClass="text9pt" GroupName="rb_is_show" Checked="true" />
				<asp:RadioButton ID="rb_is_show0" runat="server" Text="關閉" CssClass="text9pt" GroupName="rb_is_show" />&nbsp;&nbsp;
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">顯示順序</td>
			<td align="left"><asp:TextBox ID="tb_bs_sort" runat="server" MaxLength="10" Text="255"></asp:TextBox></td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">開始時間</td>
			<td align="left"><asp:TextBox ID="tb_s_time" runat="server" MaxLength="19"></asp:TextBox>&nbsp;(格式:yyyy/MM/dd HH:mm:ss)</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">結束時間</td>
			<td align="left"><asp:TextBox ID="tb_e_time" runat="server" MaxLength="19"></asp:TextBox>&nbsp;(格式:yyyy/MM/dd HH:mm:ss)</td>
		</tr>
		</table>
		<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;存檔&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.location.reload(true)" class="abtn">&nbsp;取消&nbsp;</a>
		</p>&nbsp;
		</td>
	</tr>
	</table>
	</center>
	<asp:Label ID="lb_bs_sid" runat="server" Text="0" Visible="false"></asp:Label>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
