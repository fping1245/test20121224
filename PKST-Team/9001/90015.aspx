<%@ Page Language="C#" AutoEventWireup="true" CodeFile="90015.aspx.cs" Inherits="_90015" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�s�i�H�o�e�޲z</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="1" cellpadding="4" cellspacing="0" style="width:400px; background-color:#EFEFEF">
	<tr><td align="center">
		<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:�з���">SMTP �l����A���]�w</p>
		<table border="1" cellpadding="2" cellspacing="0" style="width:380px; background-color:#F7F7DE">
		<tr><td style="text-align:center; width:120px; background-color:#99CCFF; color:#FFFFFF">�D���W��</td>
			<td align="left"><asp:TextBox ID="tb_host" runat="server" MaxLength="50" Width="150px"></asp:TextBox>(ex:seed.net.tw)</td>
		</tr>
		<tr><td style="text-align:center; width:120px; background-color:#99CCFF; color:#FFFFFF">�q�T Port</td>
			<td align="left"><asp:TextBox ID="tb_port" runat="server" MaxLength="5" Width="150px"></asp:TextBox>(ex:25)</td>
		</tr>
		<tr><td style="text-align:center; width:120px; background-color:#99CCFF; color:#FFFFFF">�n�J�b��</td>
			<td align="left"><asp:TextBox ID="tb_id" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
		</tr>
		<tr><td style="text-align:center; width:120px; background-color:#99CCFF; color:#FFFFFF">�n�J�K�X</td>
			<td align="left"><asp:TextBox ID="tb_pw" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
		</tr>
		</table>
		<p style="margin:10pt 0pt 10pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" 
				onclick="lk_save_Click">&nbsp;�s��&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.close_all()" class="abtn">&nbsp;����&nbsp;</a>
		</p>&nbsp;
	</td></tr>
	</table>
	</center>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
