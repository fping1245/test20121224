<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G001441.aspx.cs" Inherits="_G001441" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>��Ʈw�W��޲z</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<table border="1" cellpadding="4" cellspacing="0" style="width:100%; background-color:#EFEFEF; white-space:normal">
	<tr><td align="center">
			<p style="text-align:center; font-size:14pt; margin:10pt 0pt 10pt 0pt">�s�W������</p>
			<table border="1" cellpadding="4" cellspacing="0" style="width:98%">
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">��춶��</td>
				<td align="left" style="width:38%">&nbsp;�̫�@��</td>
				<td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">���W��</td>
				<td align="left" style="width:38%"><asp:TextBox ID="tb_dr_name" runat="server" Width="98%" MaxLength="20"></asp:TextBox></td>
			</tr>
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">������D</td>
				<td align="left" style="width:38%"><asp:TextBox ID="tb_dr_caption" runat="server" Width="98%" MaxLength="20"></asp:TextBox></td>
				<td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">��쫬�A</td>
				<td align="left" style="width:38%"><asp:TextBox ID="tb_dr_type" runat="server" Width="30pt" MaxLength="4"></asp:TextBox></td>
			</tr>
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">���e��</td>
				<td align="left" style="width:38%"><asp:TextBox ID="tb_dr_len" runat="server" Width="40pt" MaxLength="5" Text="0"></asp:TextBox></td>
				<td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">�p�Ʀ��</td>
				<td align="left" style="width:38%"><asp:TextBox ID="tb_dr_point" runat="server" Width="40pt" MaxLength="5" Text="0"></asp:TextBox></td>
			</tr>
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">�w �] ��</td>
				<td align="left" style="width:88%" colspan="3"><asp:TextBox ID="tb_dr_default" runat="server" Width="99%" MaxLength="50"></asp:TextBox></td>
			</tr>
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">���e����</td>
				<td align="left" style="width:88%" colspan="3"><asp:TextBox ID="tb_dr_desc" runat="server" Width="99%" MaxLength="100"></asp:TextBox></td>
			</tr>
			</table>
			<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;�s��&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.close_all();" class="abtn">&nbsp;����&nbsp;</a>
		</p>&nbsp;
		</td>
	</tr>
	</table>
	<asp:Label ID="lb_ds_sid" runat="server" Visible="false"></asp:Label>
	<asp:Label ID="lb_dt_sid" runat="server" Visible="false"></asp:Label>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
