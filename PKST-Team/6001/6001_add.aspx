<%@ Page Language="C#" AutoEventWireup="true" CodeFile="6001_add.aspx.cs" Inherits="_6001_add" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�s���H�s�պ޲z</title>
</head>
<body>
<form id="form1" runat="server">
<div>
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:�з���;">�s���H�s�պ޲z</p>
	<p align="center" class="text16pt" style="margin:0pt 0pt 5pt 0pt; font-family:�з���;">�s�W���</p>
	<center>
	<table cellspacing="0" cellpadding="4" rules="all" border="1" style="background-color:White;border-color:#003366;border-width:1px;border-style:Double;width:580pt;border-collapse:collapse;">
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" style="width:90pt">�s�սs��</td>
			<td align="left" colspan="3" style="background-color:#F7F7DE; color:Red; font-weight:bold;">&nbsp;�t�Φ۰ʲ���</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" style="width:90pt">�s�զW��</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:TextBox ID="tb_ag_name" Width="98%" runat="server" MaxLength="50" ToolTip="�u�s�զW�١v�̦h��J50�Ӧr"></asp:TextBox>
			</td>
			<td style="width:90pt" align="center">�s���ݩ�</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ag_attrib" Width="98%" runat="server" MaxLength="50" ToolTip="�u�s���ݩʡv�̦h��J50�Ӧr"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="�u�Ƶ������v�̦h500�Ӧr">�Ƶ�����</td>
			<td style="background-color:#F7F7DE" colspan="3">
				<asp:TextBox ID="tb_ag_desc" runat="server" Width="98%" MaxLength="500" 
					TextMode="MultiLine" ToolTip="�u�Ƶ������v�̦h500�Ӧr" Columns="5"></asp:TextBox>
			</td>
		</tr>
	</table>	
	<p style="margin:10pt 0pt 10pt 0pt">
		<asp:LinkButton ID="lb_ok" runat="server" CssClass="abtn" onclick="lb_ok_Click">&nbsp;�T�w�x�s&nbsp;</asp:LinkButton>&nbsp;&nbsp;
		<a href="6001.aspx<%=lb_page.Text%>" class="abtn">&nbsp;�^�s����&nbsp;</a></p>&nbsp;
	</center>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
</div>
</form>
</body>
</html>
