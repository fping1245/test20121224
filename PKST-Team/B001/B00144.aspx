<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B00144.aspx.cs" Inherits="_B00144" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�Ҹ��D�w�޲z</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="1" cellpadding="4" cellspacing="0" style="width:100%; background-color:#EFEFEF">
	<tr><td align="center">
		<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:�з���">�s�W���׶���</p>
		<table border="1" cellpadding="4" cellspacing="0" style="width:98%; background-color:#F7F7DE">
		<tr><td style="text-align:center; width:50pt; background-color:#99CCFF; color:#FFFFFF">�ը��D��</td>
			<td align="left" style="white-space:normal">
				<asp:Label ID="lb_tq_sort" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>&nbsp;
				<asp:Label ID="lb_tq_desc" runat="server"></asp:Label></td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">���׿ﶵ</td>
			<td align="left">
				<table width="100%" border="1" cellpadding="2" cellspacing="0">
				<tr align="center" style="background-color:#E0E0E0">
					<td style="width:30pt">����</td>
					<td>���ؤ�r</td>
					<td style="width:50pt">���T����</td>
				</tr>
				<tr align="center">
					<td><asp:TextBox ID="tb_ti_sort" runat="server" Text="1" Width="18pt" MaxLength="3"></asp:TextBox></td>
					<td><asp:TextBox ID="tb_ti_desc" runat="server" Text="" Width="98%" MaxLength="100"></asp:TextBox></td>
					<td><asp:RadioButton ID="rb_ti_correct0" runat="server" GroupName="rb_ti_correct" Text="��" Font-Names="�ө���" Checked="true" />
						<asp:RadioButton ID="rb_ti_correct1" runat="server" GroupName="rb_ti_correct" Text="��" Font-Names="�ө���" />
					</td>
				</tr>
				</table>
			</td>
		</tr>
		</table>
		<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save_again" runat="server" CssClass="abtn" onclick="lk_save_again_Click" ToolTip="�s�ɫ��~��A�W�[�s�����׿ﶵ">&nbsp;�s���~��&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click" ToolTip="�s�ɫ�Y����">&nbsp;�s�ɵ���&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.location.reload(true);" class="abtn">&nbsp;�����s�W&nbsp;</a>
		</p>&nbsp;
	</td></tr>
	</table>
	</center>
	<asp:Label ID="lb_tp_sid" runat="server" Text="0" Visible="false"></asp:Label>
	<asp:Label ID="lb_tq_sid" runat="server" Text="0" Visible="false"></asp:Label>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
