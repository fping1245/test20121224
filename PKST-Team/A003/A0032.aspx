<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A0032.aspx.cs" Inherits="_A0032" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�����ƺ޲z</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="1" cellpadding="4" cellspacing="0" style="width:680px; background-color:#EFEFEF">
	<tr><td align="center">
		<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:�з���">�s�W����ݨ�</p>
		<table border="1" cellpadding="4" cellspacing="0" style="width:660px; background-color:#F7F7DE">
		<tr><td style="text-align:center; width:80px; background-color:#99CCFF; color:#FFFFFF">������D</td>
			<td align="left"><asp:TextBox ID="tb_bh_title" runat="server" MaxLength="50" Width="556px"></asp:TextBox></td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">����覡</td>
			<td align="left">
				<asp:RadioButton ID="rb_is_check0" runat="server" Text="���" CssClass="text9pt" 
					GroupName="rb_is_check" Checked="true" AutoPostBack="True" 
					oncheckedchanged="rb_is_check0_CheckedChanged" />
				<asp:RadioButton ID="rb_is_check1" runat="server" Text="�ƿ�" CssClass="text9pt" 
					GroupName="rb_is_check" AutoPostBack="True" 
					oncheckedchanged="rb_is_check1_CheckedChanged"/>&nbsp;&nbsp;
				<asp:Literal ID="lt_is_check" runat="server" Text="�ƿ��D�ƤW���G" Visible="false"></asp:Literal>
				<asp:TextBox ID="tb_is_check" runat="server" Width="30px" Text="2" Visible="false"></asp:TextBox>
				<asp:Literal ID="lt_is_check_desc" runat="server" Text="(1:�ƿ���� 2~255:����ƿ��D��)" Visible="false"></asp:Literal>
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">���e����</td>
			<td align="left"><asp:TextBox ID="tb_bh_topic" Rows="5" TextMode="MultiLine" runat="server" MaxLength="1000" Width="556px"></asp:TextBox></td>
		</tr>
		</table>
		<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;�s��&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.location.reload(true);" class="abtn">&nbsp;����&nbsp;</a>
		</p>&nbsp;
	</td></tr>
	</table>
	</center>
	<asp:Label ID="lb_bh_sid" runat="server" Text="0" Visible="false"></asp:Label>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
