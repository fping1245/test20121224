<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G0011.aspx.cs" Inherits="_G0011" %>
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
			<p style="text-align:center; font-size:14pt; margin:10pt 0pt 10pt 0pt">�s�W�t�γW��</p>
			<table border="1" cellpadding="4" cellspacing="0" style="width:98%">
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">�N�@��</td>
				<td align="left" style="width:38%"><asp:TextBox ID="tb_ds_code" runat="server" Width="98%" MaxLength="12"></asp:TextBox></td>
				<td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">�W�@��</td>
				<td align="left" style="width:38%"><asp:TextBox ID="tb_ds_name" runat="server" Width="98%" MaxLength="20"></asp:TextBox></td>
			</tr>
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">�� �� �w</td>
				<td align="left" style="width:88%" colspan="3"><asp:TextBox ID="tb_ds_database" runat="server" Width="99%" MaxLength="50"></asp:TextBox></td>
			</tr>
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">�n�J�b��</td>
				<td align="left" style="width:38%"><asp:TextBox ID="tb_ds_id" runat="server" Width="98%" MaxLength="12"></asp:TextBox></td>
				<td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">�n�J�K�X</td>
				<td align="left" style="width:38%"><asp:TextBox ID="tb_ds_pass" runat="server" Width="98%" MaxLength="20"></asp:TextBox></td>
			</tr>
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">�Ƶ�����</td>
				<td align="left" style="width:88%" colspan="3"><asp:TextBox ID="tb_ds_desc" runat="server" Width="99%" MaxLength="1000" TextMode="MultiLine" Rows="5"></asp:TextBox></td>
			</tr>
			</table>
			<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;�s��&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.close_all();" class="abtn">&nbsp;����&nbsp;</a>
		</p>&nbsp;
		</td>
	</tr>
	</table>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
