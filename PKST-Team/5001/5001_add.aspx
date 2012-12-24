<%@ Page Language="C#" AutoEventWireup="true" CodeFile="5001_add.aspx.cs" Inherits="_5001_add" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>工作類型管理</title>
</head>
<body>
<form id="form1" runat="server">
<div>
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體;">工作類型管理</p>
	<p align="center" class="text16pt" style="margin:0pt 0pt 5pt 0pt; font-family:標楷體;">新增資料</p>
	<center>
	<table cellspacing="0" cellpadding="4" rules="all" border="1" style="background-color:White;border-color:#003366;border-width:1px;border-style:Double;width:580pt;border-collapse:collapse;">
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" style="width:90pt">類型編號</td>
			<td align="left" style="background-color:#F7F7DE; color:Red; font-weight:bold;">&nbsp;系統自動產生</td>
			<td style="width:90pt" align="center">顯示順序</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_cg_sort" Width="98%" Text="32767" runat="server" MaxLength="5" ToolTip="「顯示順序」請輸入 0~32767 的數字，存檔後會自動依數字大小重新編排"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" style="width:90pt">類型名稱</td>
			<td style="width:200pt; background-color:#F7F7DE" colspan="3">
				<asp:TextBox ID="tb_cg_name" Width="98%" runat="server" MaxLength="10" ToolTip="「類型名稱」最多輸入10個字"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「備註說明」最多500個字">備註說明</td>
			<td style="background-color:#F7F7DE" colspan="3">
				<asp:TextBox ID="tb_cg_desc" runat="server" Width="98%" MaxLength="500" 
					TextMode="MultiLine" ToolTip="「備註說明」最多500個字" Columns="5"></asp:TextBox>
			</td>
		</tr>
	</table>
	<table cellspacing="0" cellpadding="2" rules="all" border="0" style="width:580pt;">
	<tr><td align="left" style="width:10pt">※</td>
		<td align="left">「顯示順序」只要填數字即可，系統會自動你所填數字的大小，依序重新編排數字。</td>
	</tr>
	</table>
	<p style="margin:10pt 0pt 10pt 0pt">
		<asp:LinkButton ID="lb_ok" runat="server" CssClass="abtn" onclick="lb_ok_Click">&nbsp;確定儲存&nbsp;</asp:LinkButton>&nbsp;&nbsp;
		<a href="5001.aspx<%=lb_page.Text%>" class="abtn">&nbsp;回瀏覽頁&nbsp;</a></p>&nbsp;
	</center>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
</div>
</form>
</body>
</html>
