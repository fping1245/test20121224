<%@ Page Language="C#" AutoEventWireup="true" CodeFile="5002.aspx.cs" Inherits="_5002" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>行事曆管理</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
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
	<!-- Begin 隱藏視窗 -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	<center>
	<p class="text18pt" style="margin: 10pt 0pt 0pt 0pt; font-family: 標楷體;">行事曆管理</p>
	<table width="99%" border="0" cellpadding="0" cellspacing="0">
	<tr><td style="width: 160pt; height: 480pt" valign="top">
			<asp:Calendar ID="cdr1" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4"
				DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black"
				Height="160pt" Width="160pt" onselectionchanged="cdr1_SelectionChanged" 
				onvisiblemonthchanged="cdr1_VisibleMonthChanged">
				<SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
				<SelectorStyle BackColor="#CCCCCC" />
				<WeekendDayStyle BackColor="#FFFFCC" />
				<TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
				<OtherMonthDayStyle ForeColor="#808080" />
				<NextPrevStyle VerticalAlign="Bottom" />
				<DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
				<TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
			</asp:Calendar>
			<asp:Calendar ID="cdr2" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4"
				DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black"
				Height="160pt" Width="160pt" onselectionchanged="cdr2_SelectionChanged" 
				onvisiblemonthchanged="cdr2_VisibleMonthChanged">
				<SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
				<SelectorStyle BackColor="#CCCCCC" />
				<WeekendDayStyle BackColor="#FFFFCC" />
				<TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
				<OtherMonthDayStyle ForeColor="#808080" />
				<NextPrevStyle VerticalAlign="Bottom" />
				<DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
				<TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
			</asp:Calendar>
			<asp:Calendar ID="cdr3" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4"
				DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black"
				Height="160pt" Width="160pt" onselectionchanged="cdr3_SelectionChanged" 
				onvisiblemonthchanged="cdr3_VisibleMonthChanged">
				<SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
				<SelectorStyle BackColor="#CCCCCC" />
				<WeekendDayStyle BackColor="#FFFFCC" />
				<TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
				<OtherMonthDayStyle ForeColor="#808080" />
				<NextPrevStyle VerticalAlign="Bottom" />
				<DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
				<TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
			</asp:Calendar>
		</td>
		<td align="left" valign="middle">
			<table width="100%" border="1" cellpadding="0" cellspacing="0" style="height:600px; background-color: #FAFAD2">
			<tr style="background-color: #F5DEB3">
				<td align="center" style="width: 60pt; height: 20pt;">日期</td>
				<td align="center">主旨</td>
			</tr>
			<tr style="height: 64pt">
				<td align="center">
					<a href ="javascript:show_win('50021.aspx?dtm=<%=lb_now.Text%>', 450, 600)" title="請按此處新增行事曆">
						<asp:Label ID="lb_wk0" runat="server" Text="" Font-Bold="true"></asp:Label>
					</a>
				</td>
				<td valign="top"><asp:Literal ID="lt_wk0" runat="server" Text="&nbsp;"></asp:Literal></td>
			</tr>
			<tr style="height: 64pt">
				<td align="center">
					<a href ="javascript:show_win('50021.aspx?dtm=<%=DateTime.Parse(lb_now.Text).AddDays(1).ToString("yyyy/MM/dd")%>', 450, 600)" title="請按此處新增行事曆">
						<asp:Label ID="lb_wk1" runat="server" Text="" Font-Bold="true"></asp:Label>
					</a>
				</td>
				<td valign="top"><asp:Literal ID="lt_wk1" runat="server" Text="&nbsp;"></asp:Literal></td>
			</tr>
			<tr style="height: 64pt">
				<td align="center">
					<a href ="javascript:show_win('50021.aspx?dtm=<%=DateTime.Parse(lb_now.Text).AddDays(2).ToString("yyyy/MM/dd")%>', 450, 600)" title="請按此處新增行事曆">
						<asp:Label ID="lb_wk2" runat="server" Text="" Font-Bold="true"></asp:Label>
					</a>	
				</td>
				<td valign="top"><asp:Literal ID="lt_wk2" runat="server" Text="&nbsp;"></asp:Literal></td>
			</tr>
			<tr style="height: 64pt">
				<td align="center">
					<a href ="javascript:show_win('50021.aspx?dtm=<%=DateTime.Parse(lb_now.Text).AddDays(3).ToString("yyyy/MM/dd")%>', 450, 600)" title="請按此處新增行事曆">
						<asp:Label ID="lb_wk3" runat="server" Text="" Font-Bold="true"></asp:Label>
					</a>
				</td>
				<td valign="top"><asp:Literal ID="lt_wk3" runat="server" Text="&nbsp;"></asp:Literal></td>
			</tr>
			<tr style="height: 64pt">
				<td align="center">
					<a href ="javascript:show_win('50021.aspx?dtm=<%=DateTime.Parse(lb_now.Text).AddDays(4).ToString("yyyy/MM/dd")%>', 450, 600)" title="請按此處新增行事曆">
						<asp:Label ID="lb_wk4" runat="server" Text="" Font-Bold="true"></asp:Label>
					</a>
				</td>
				<td valign="top"><asp:Literal ID="lt_wk4" runat="server" Text="&nbsp;"></asp:Literal></td>
			</tr>
			<tr style="height: 64pt">
				<td align="center">
					<a href ="javascript:show_win('50021.aspx?dtm=<%=DateTime.Parse(lb_now.Text).AddDays(5).ToString("yyyy/MM/dd")%>', 450, 600)" title="請按此處新增行事曆">
						<asp:Label ID="lb_wk5" runat="server" Text="" Font-Bold="true"></asp:Label>
					</a>
				</td>
				<td valign="top"><asp:Literal ID="lt_wk5" runat="server" Text="&nbsp;"></asp:Literal></td>
			</tr>
			<tr style="height: 64pt">
				<td align="center">
					<a href ="javascript:show_win('50021.aspx?dtm=<%=DateTime.Parse(lb_now.Text).AddDays(6).ToString("yyyy/MM/dd")%>', 450, 600)" title="請按此處新增行事曆">
						<asp:Label ID="lb_wk6" runat="server" Text="" Font-Bold="true"></asp:Label>
					</a>
				</td>
				<td valign="top"><asp:Literal ID="lt_wk6" runat="server" Text="&nbsp;"></asp:Literal></td>
			</tr>
			</table>
		</td>
	</tr>
	</table>
	<table width="98%" border="0" cellpadding="0" cellspacing="0">
	<tr><td align="left" style="width:20px" valign="t">※</td>
		<td align="left"><img src="../images/ico/important.gif" />：重要、<img src="../images/ico/minus.gif" />：不重要、<img src="../images/ico/clip.gif" />：有附加檔案</td>
	</tr>
	<tr><td align="left" style="width:20px">※</td>
		<td align="left">點選「月曆日期」，可切換行事曆內容。</td>
	</tr>
	<tr><td align="left">※</td>
		<td align="left">點選「日期」，可新增行事曆。</td>
	</tr>
	<tr><td align="left">※</td>
		<td align="left">點選「主旨」文字，可查看詳細內容。</td>
	</tr>
	</table>
	</center>
	<!-- Begin 顯示上傳訊息 -->
	<div id="div_ban" class="text9pt" style="position:absolute; width:240px; z-index:0; display:none">
	<table id="tb_ban" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_ban.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<div id="div_msg_wait" class="text9pt" style="position:absolute; width:240px; z-index:0; display:none">
	<table border="2" cellpadding="2" cellspacing="2" style="width:240px; height:100px; background-color:#efefef">
	<tr><td align="center" class="text9pt">資料上傳中 ........<br />請勿按任何按鍵!</td></tr>
	</table>
	</div>
	<!-- End -->
	<asp:Label ID="lb_now" runat="server" Visible="false"></asp:Label>
	</div>
	</form>
</body>
</html>
