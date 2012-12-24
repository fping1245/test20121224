<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B0011.aspx.cs" Inherits="_B0011" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>考試題庫管理</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript" src="../js/innerCalendar.js"></script>
</head>
<body>
	<form id="form1" runat="server">
	<div>

	<!-- Begin 覆蓋整個工作頁面，不讓使用者再按其它按鍵 -->
	<div id="fullwindow" style="position: absolute; top:0px; left:0px; width:1024px; height:768px; display:none">
	<table id="fulltable" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_back.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<!-- End -->
	
	<!-- Begin 日曆視窗 -->
	<div id="div_calendar" style="position: absolute; top:0px; left:0px; display:none">
	<table border="2" cellpadding="0" cellspacing="0" style="width:204px" bgcolor="#efefef">
	<tr><td align="center">
			<table border="0" cellpadding="2" cellspacing="0" style="width:100%">
			<tr style="background-color:Blue">
				<td align="left" style=" font-size:9pt; color:White; cursor:move" onmousedown="EventDm()" onmouseup="EventUp()" onmousemove="EventMv(event)">&nbsp;日期選擇</td>
				<td align="right" style="background-color:Blue; width:30px">
					<a href="javascript:close_calendar()" style="font-size:11pt; color:White; border-color:White; border-style:inherit; background-color:Red; text-decoration:none" title="關閉視窗">&nbsp;×&nbsp;</a>
				</td>
			</tr>
			</table>
		</td>
	</tr>
	<tr style="background-color:#FFCCFF">
		<td><iframe id="if_calendar" src="" scrolling="no" frameborder="0" style="width:260px"></iframe></td>
	</tr>
	</table>
	</div>
	<!-- End -->

	<center>
	<table border="1" cellpadding="4" cellspacing="0" style="width:680px; background-color:#EFEFEF">
	<tr><td align="center">
		<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">新增試卷</p>
		<table border="1" cellpadding="4" cellspacing="0" style="width:660px; background-color:#F7F7DE">
		<tr><td style="text-align:center; width:80px; background-color:#99CCFF; color:#FFFFFF">試卷標題</td>
			<td align="left"><asp:TextBox ID="tb_tp_title" runat="server" MaxLength="50" Width="98%"></asp:TextBox></td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">是否顯示</td>
			<td align="left">
				<asp:RadioButton ID="rb_is_show1" runat="server" Text="顯示" CssClass="text9pt" GroupName="rb_is_show" Checked="true" />
				<asp:RadioButton ID="rb_is_show0" runat="server" Text="隱藏" CssClass="text9pt" GroupName="rb_is_show" />
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">開放進入時間</td>
			<td align="left">
				<asp:TextBox ID="tb_b_date" runat="server" MaxLength="10" Width="60pt"></asp:TextBox>
				<img src="../images/button/calendar.gif" alt="開啟日曆視窗選取日期" onclick="getDate('tb_b_date')" style="border-bottom-width: 0pt" />&nbsp;&nbsp;
				<asp:TextBox ID="tb_b_hour" runat="server" MaxLength="2" Width="20pt" Text="09" ToolTip="小時"></asp:TextBox>：
				<asp:TextBox ID="tb_b_min" runat="server" MaxLength="2" Width="20pt" Text="00" ToolTip="分鐘"></asp:TextBox>
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">截止進入時間</td>
			<td align="left">
				<asp:TextBox ID="tb_e_date" runat="server" MaxLength="10" Width="60pt"></asp:TextBox>
				<img src="../images/button/calendar.gif" alt="開啟日曆視窗選取日期" onclick="getDate('tb_e_date')" style="border-bottom-width: 0pt" />&nbsp;&nbsp;
				<asp:TextBox ID="tb_e_hour" runat="server" MaxLength="2" Width="20pt" Text="18" ToolTip="小時"></asp:TextBox>：
				<asp:TextBox ID="tb_e_min" runat="server" MaxLength="2" Width="20pt" Text="00" ToolTip="分鐘"></asp:TextBox>
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">試卷說明</td>
			<td align="left"><asp:TextBox ID="tb_tp_desc" Rows="10" TextMode="MultiLine" runat="server" MaxLength="1000" Width="556px"></asp:TextBox></td>
		</tr>
		</table>
		<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;存檔&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.location.reload(true);" class="abtn">&nbsp;取消&nbsp;</a>
		</p>&nbsp;
	</td></tr>
	</table>
	</center>
	<asp:Label ID="lb_tp_sid" runat="server" Text="0" Visible="false"></asp:Label>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
