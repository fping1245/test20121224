<%@ Page Language="C#" AutoEventWireup="true" CodeFile="7002.aspx.cs" Inherits="_7002" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>客服人員端</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<asp:ScriptManager ID="sm_manager" runat="server"></asp:ScriptManager>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 10pt 0pt; font-family:標楷體;">客服人員端</p>
	<table border="1" cellpadding="4" cellspacing="0" style="width:650px">
	<tr><td align="center" class="text12pt" style="background-color:#99FF99"><b>目前客戶的狀況</b></td></tr>
	<tr><td align="left" valign="top">
			<asp:UpdatePanel ID="up_list" runat="server" UpdateMode="Conditional">
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="ti_Cs_User" EventName="Tick" />
				</Triggers>
				<ContentTemplate>
					<asp:Literal ID="lt_list" runat="server"></asp:Literal>
				</ContentTemplate>
			</asp:UpdatePanel>
		</td>
	</tr>
	</table>
	</center>
	<asp:Timer ID="ti_Cs_User" runat="server" Interval="3000" ontick="ti_Cs_User_Tick"></asp:Timer>
	<asp:Label ID="lb_tb_title" runat="server" Visible="false"></asp:Label>
	<asp:Label ID="lb_Sql_Cs_User" runat="server" Visible="false"></asp:Label>
	</div>
	</form>
</body>
</html>
