<%@ Page Language="C#" AutoEventWireup="true" CodeFile="10051_add.aspx.cs" Inherits="_10051_add" %>
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
	<p align="center" class="text16pt" style="margin:0pt 0pt 5pt 0pt; font-family:標楷體;">新增資料</p>
	<center>
	<table cellspacing="0" cellpadding="4" rules="all" border="1" style="background-color:White;border-color:#003366;border-width:1px;border-style:Double;width:580pt;border-collapse:collapse;">
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" style="width:90pt">人員編號</td>
			<td style="width:200pt; background-color:#F7F7DE; color:Red; font-weight:bold; text-align:center">系統自動產生</td>
			<td align="center" style="width:90pt" title="「登入帳號」最多12個字，不允許重覆">登入帳號</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:TextBox ID="tb_mg_id" Width="98%" runat="server" MaxLength="12" ToolTip="「登入帳號」最多12個字，不允許重覆"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「登入密碼」請輪入4~12個字">登入密碼</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_mg_pass" Width="98%" runat="server" MaxLength="12" TextMode="Password" ToolTip="「登入密碼」請輪入4~12個字"></asp:TextBox>
			</td>
			<td align="center" title="「密碼確認」請輪入4~12個字">密碼確認</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_mg_pass1" Width="98%" runat="server" MaxLength="12" TextMode="Password" ToolTip="「密碼確認」請輪入4~12個字"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「姓名」最多12個字">姓名</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_mg_name" Width="98%" runat="server" MaxLength="12" ToolTip="「姓名」最多12個字"></asp:TextBox>
			</td>
			<td align="center" title="「暱稱」最多12個字">暱稱</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_mg_nike" Width="98%" runat="server" MaxLength="12" ToolTip="「暱稱」最多12個字"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「單位」最多50個字">單位</td>
			<td style="background-color:#F7F7DE" colspan="3">
				<asp:TextBox ID="tb_mg_unit" runat="server" Width="98%" MaxLength="50" ToolTip="「單位」最多50個字"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「說明」最多1000個字">說明</td>
			<td style="background-color:#F7F7DE" colspan="3">
				<asp:TextBox ID="tb_mg_desc" runat="server" Width="98%" MaxLength="1000" TextMode="MultiLine" ToolTip="「說明」最多1000個字"></asp:TextBox>
			</td>
		</tr>
	</table>	
	<p style="margin:10pt 0pt 10pt 0pt">
		<asp:LinkButton ID="lb_ok" runat="server" CssClass="abtn" onclick="lb_ok_Click">&nbsp;確定儲存&nbsp;</asp:LinkButton>&nbsp;&nbsp;
		<a href="1005.aspx<%=lb_page.Text%>" class="abtn">&nbsp;回瀏覽頁&nbsp;</a></p>&nbsp;
	</center>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
</div>
</form>
</body>
</html>
