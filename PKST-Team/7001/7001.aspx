<%@ Page Language="C#" AutoEventWireup="true" CodeFile="7001.aspx.cs" Inherits="_7001" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>客戶使用端</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<asp:ScriptManager ID="sm_manager" runat="server"></asp:ScriptManager>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體;">客戶使用端</p>
	<p align="center" class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體;">提出客服要求</p>
	<table border="1" cellpadding="4" cellspacing="0" style="width:300px">
	<tr style="height:24px">
		<td align="center" style="background-color:#FFFFE0">使用者名稱</td>
		<td align="left"><asp:TextBox ID="tb_cu_name" runat="server" MaxLength="20" Width="160px"></asp:TextBox></td>
	</tr>
	<tr style="height:24px; background-color:#F0F8FF"><td colspan="2" align="left">請輸入10個字以內的名稱，以方便客服人員稱呼。</td></tr>
	</table>
	<p style="margin:10px 0px 10px 0px"><asp:Button ID="bn_ok" runat="server" Text="連絡客服" CssClass="text9pt" Height="24px" onclick="bn_ok_Click" /></p>
	<table id="tab_wait" runat="server" visible="false" border="1" cellpadding="4" cellspacing="0" style="width:300px; background-color:#FFFACD">
	<tr style="height:24px">
		<td align="center" style="background-color:#FFFFE0">要求提出時間</td>
		<td><asp:Label ID="lb_cu_time" runat="server"></asp:Label></td>
	</tr>
	<tr><td align="center" style="background-color:#FFFFE0">等待回應時間</td>
		<td><asp:UpdatePanel ID="up_wait" runat="server" UpdateMode="Conditional">
			<Triggers>
				<asp:AsyncPostBackTrigger ControlID="ti_check" EventName="Tick">
				</asp:AsyncPostBackTrigger>
			</Triggers>
			<ContentTemplate>
				<asp:Label ID="lb_wait_sec" runat="server" Text="1"></asp:Label> sec
				<asp:Literal ID="lt_msg" runat="server" EnableViewState="False" ></asp:Literal>
			</ContentTemplate>
			</asp:UpdatePanel>
		</td>
	</tr>
	<tr style="height:24px; background-color:#F0F8FF"><td colspan="2" align="center">若超過等待時間，請重新提出要求</td></tr>
	</table>
	</center>
	<asp:Timer ID="ti_check" runat="server" Enabled="False" Interval="1100" ontick="ti_check_Tick"></asp:Timer>
	<asp:Label ID="lb_cu_sid" runat="server" Text="0" Visible="false"></asp:Label>
	<asp:Label ID="lb_mg_sid" runat="server" Text="-1" Visible="false"></asp:Label>
	</div>
	</form>
</body>
</html>
