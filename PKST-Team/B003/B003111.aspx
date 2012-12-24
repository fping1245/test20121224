<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B003111.aspx.cs" Inherits="_B003111" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>線上考試(限定身份)</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="1" cellpadding="4" cellspacing="0" style="width:100%; background-color:#EFEFEF">
	<tr><td align="center">
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">回答試題</p>
	<table border="0" cellpadding="5" cellspacing="0" class="text9pt" style="width:98%">
	<tr><td align="left" style="width:25%">姓名：<asp:Label ID="lb_tu_name" runat="server" Font-Bold="true"></asp:Label></td>
		<td align="left" style="width:25%">學號：<asp:Label ID="lb_tu_no" runat="server" Font-Bold="true"></asp:Label></td>
		<td align="left" style="width:25%">開始答題時間：<asp:Label ID="lb_b_time" runat="server" Font-Bold="true"></asp:Label></td>
		<td align="left" style="width:25%">最後答題時間：<asp:Label ID="lb_e_time" runat="server" Font-Bold="true"></asp:Label></td>
	</tr>
	</table>
	<table border="2" cellpadding="4" cellspacing="0" bgcolor="#F7F7DE" class="text9pt" style="width:98%; margin:0pt 0pt 5pt 0pt; border-color:#F0F0F0">
	<tr><td style="text-align:center; width:60pt; background-color:#99CCFF; color:#FFFFFF">試卷標題</td>
		<td style="text-align:left" colspan="3"><asp:Label ID="lb_tp_title" runat="server" Text="&nbsp;" Font-Bold="true"></asp:Label></td>
	</tr>
	<tr><td style="text-align:center; width:60pt; background-color:#99CCFF; color:#FFFFFF">填寫方式</td>
		<td style="text-align:left"><asp:Literal ID="lt_tq_type" runat="server" Text="&nbsp;"></asp:Literal></td>
		<td style="text-align:center; width:60pt; background-color:#99CCFF; color:#FFFFFF">本題分數</td>
		<td style="text-align:left"><asp:Label ID="lb_tq_score" runat="server" Text="0"></asp:Label> 分</td>
	</tr>
	<tr><td style="text-align:center; width:60pt; background-color:#99CCFF; color:#FFFFFF">試題</td>
		<td style="text-align:left; white-space:normal" colspan="3">
			<asp:Label ID="lb_tq_sort" runat="server" Text="0" Font-Bold="true"></asp:Label>.&nbsp;
			<asp:Label ID="lb_tq_desc" runat="server" Text="&nbsp;" Font-Bold="true"></asp:Label>
		</td>
	</tr>
	<tr><td style="text-align:center; width:60pt; background-color:#99CCFF; color:#FFFFFF">選擇項目</td>
		<td style="text-align:left" colspan="3"><asp:Panel ID="pl_tq_type" runat="server" Width="98%" HorizontalAlign="Left"></asp:Panel></td>
	</tr>
	</table>
	<p style="margin:10px 0px 0px 0px">
		<asp:LinkButton ID="lk_ok" runat="server" CssClass="abtn" onclick="lk_ok_Click" ToolTip="確定答案，並繼續回答下一題">&nbsp;答案確定&nbsp;</asp:LinkButton>&nbsp;&nbsp;
		<a href="javascript:parent.location.reload(true);" class="abtn" title="停止回答題目">&nbsp;停止答題&nbsp;</a>
	</p>&nbsp
	</td></tr>
	</table>
	</center>
	<asp:Label ID="lb_tp_sid" runat="server" Visible="false" Text="0"></asp:Label>
	<asp:Label ID="lb_tq_sid" runat="server" Visible="false" Text="0"></asp:Label>
	<asp:Label ID="lb_tu_sid" runat="server" Visible="false" Text="0"></asp:Label>
	<asp:Label ID="lb_tq_type" runat="server" Visible="false" Text="0"></asp:Label>
	<asp:Label ID="lb_tq_item" runat="server" Visible="false" Text="0"></asp:Label>
	<asp:Label ID="lb_tp_question" runat="server" Visible="false" Text="0"></asp:Label>

	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
