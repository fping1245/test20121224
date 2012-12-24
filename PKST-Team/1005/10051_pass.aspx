<%@ Page Language="C#" AutoEventWireup="true" CodeFile="10051_pass.aspx.cs" Inherits="_10051_pass" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>人員資料管理</title>
</head>
<body>
<form id="form1" runat="server">
<div>
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體;">人員資料管理</p>
	<p align="center" class="text16pt" style="margin:0pt 0pt 5pt 0pt; font-family:標楷體;">變更密碼</p>
	<center>
	<table cellspacing="0" cellpadding="4" rules="all" border="1" style="background-color:White;border-color:#003366;border-width:1px;border-style:Double;width:290pt;border-collapse:collapse;">
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" style="width:90pt">人員編號</td>
			<td style="width:200pt; background-color:#F7F7DE; color:Red; font-weight:bold; text-align:center">
				<asp:Label ID="lb_mg_sid" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" style="width:90pt">登入帳號</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:Label ID="lb_mg_id" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center">姓名</td>
			<td style="background-color:#F7F7DE">
				<asp:Label ID="lb_mg_name" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「新登入密碼」請輪入4~12個字">新登入密碼</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_mg_pass" Width="98%" runat="server" MaxLength="12" TextMode="Password" ToolTip="「新登入密碼」請輪入4~12個字"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「新密碼確認」請輪入4~12個字">新密碼確認</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_mg_pass1" Width="98%" runat="server" MaxLength="12" TextMode="Password" ToolTip="「新密碼確認」請輪入4~12個字"></asp:TextBox>
			</td>
		</tr>
	</table>	
	<p style="margin:10pt 0pt 10pt 0pt">
		<asp:LinkButton ID="lb_ok" runat="server" CssClass="abtn" onclick="lb_ok_Click">&nbsp;確定儲存&nbsp;</asp:LinkButton>&nbsp;&nbsp;
		<a href="10051.aspx<%=lb_page.Text%>" class="abtn">&nbsp;回上一頁&nbsp;</a>
	</p>&nbsp;
	</center>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_pg_mg_sid" runat="server" Text="" Visible="false"></asp:Label>
</div>
</form>
</body>
</html>
