<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G00142.aspx.cs" Inherits="_G00142" %>
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
			<p style="text-align:center; font-size:14pt; margin:10pt 0pt 10pt 0pt">�ק��ƪ��</p>
			<table border="1" cellpadding="4" cellspacing="0" style="width:98%">
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">���W��</td>
				<td align="left" style="width:38%"><asp:TextBox ID="tb_dt_name" runat="server" Width="98%" MaxLength="20"></asp:TextBox></td>
				<td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">��ܶ���</td>
				<td align="left" style="width:38%"><asp:TextBox ID="tb_dt_sort" runat="server" Width="30pt" MaxLength="4" Text="3275"></asp:TextBox>&nbsp;(�п�J 0 ~ 3275 ���Ʀr)</td>
			</tr>
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">������D</td>
				<td align="left" style="width:38%"><asp:TextBox ID="tb_dt_caption" runat="server" Width="98%" MaxLength="50"></asp:TextBox></td>
				<td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">�׭q�H��</td>
				<td align="left" style="width:38%"><asp:TextBox ID="tb_dt_modi" runat="server" Width="98%" MaxLength="50"></asp:TextBox></td>
			</tr>
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">�Ҧb��m</td>
				<td align="left" style="width:88%" colspan="3"><asp:TextBox ID="tb_dt_area" runat="server" Width="99%" MaxLength="50"></asp:TextBox></td>
			</tr>
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">�\�໡��</td>
				<td align="left" style="width:88%" colspan="3"><asp:TextBox ID="tb_dt_desc" runat="server" Width="99%" MaxLength="50"></asp:TextBox></td>
			</tr>
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">�������</td>
				<td align="left" style="width:88%" colspan="3"><asp:TextBox ID="tb_dt_index" runat="server" Width="99%" MaxLength="1000" TextMode="MultiLine" Rows="5"></asp:TextBox></td>
			</tr>
			<tr><td align="center" style="width:12%; background-color:#99CCFF; color:#FFFFFF"">���ʮɶ�</td>
				<td align="left" style="width:88%" colspan="3"><asp:Label ID="lb_init_time" runat="server"></asp:Label></td>
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
