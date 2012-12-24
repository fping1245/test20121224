<%@ Page Language="C#" AutoEventWireup="true" CodeFile="calendar.aspx.cs" Inherits="_calendar" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>日曆選擇</title>
</head>
<body style="margin:0pt 0pt 0pt 0pt; border-width:0pt; background-color:#EEFFBB">
	<form id="form1" runat="server">
	<div>
	<center>
	<asp:Calendar ID="cdr1" runat="server" BackColor="#FFFFCC" DayNameFormat="Shortest" 
		Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" 
		ShowGridLines="True" onselectionchanged="cdr1_SelectionChanged" ToolTip="請選擇所需要的日期" 
		Height="200px" Width="200px">
		<SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
		<SelectorStyle BackColor="#FFCC66" />
		<TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
		<OtherMonthDayStyle ForeColor="#CC9966" />
		<NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
		<DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
		<TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
	</asp:Calendar>
	</center>
	<script language="javascript" type="text/javascript">
		resize();

		// 重新調整母頁框的高度
		function resize() {
			var ifobj;
			ifobj = parent.document.getElementById("if_calendar");
			if (ifobj != null) {
				ifobj.style.height = (document.body.clientHeight) + "px";
			}

			ifobj = parent.document.getElementById("div_calendar");
			if (ifobj != null) {
				ifobj.style.height = (document.body.clientHeight) + "px";
			}
		}
	</script>
	<asp:Label ID="lb_rtobj" runat="server" Visible="false"></asp:Label>
	</div>
	</form>
</body>
</html>
