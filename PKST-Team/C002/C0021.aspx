<%@ Page Language="C#" AutoEventWireup="true" CodeFile="C0021.aspx.cs" Inherits="_C0021" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�d���O�޲z</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="1" cellpadding="4" cellspacing="0" style="width:100%; background-color:#EFEFEF; white-space:normal">
	<tr><td align="center">
		<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:�з���">�ק�d��</p>
		<table border="1" cellpadding="4" cellspacing="0" style="width:660px; background-color:#F7F7DE">
		<tr><td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">���</td>
			<td align="left" style="width:40%">
				<asp:RadioButton ID="rb_is_show1" runat="server" GroupName="rb_mb_is_show" Text="���" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_is_show0" runat="server" GroupName="rb_mb_is_show" 
					Text="����" AutoPostBack="True" oncheckedchanged="rb_is_show0_CheckedChanged" />
			</td>
			<td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">�}��</td>
			<td align="left" style="width:40%">
				<asp:RadioButton ID="rb_is_close1" runat="server" GroupName="rb_mb_is_close" Text="�}��" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_is_close0" runat="server" GroupName="rb_mb_is_close" Text="����" />
			</td>
		</tr>
		<tr><td style="width:10%; text-align:center; background-color:#99CCFF; color:#FFFFFF">���N��r</td>
			<td align="left" colspan="3" style="width:90%;">
				<asp:TextBox ID="tb_instead" runat="server" Text="" MaxLength="50" Width="98%"></asp:TextBox>
			</td>
		</tr>
		<tr><td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">�߱�</td>
			<td align="left" colspan="3" style="width:90%">
				<asp:RadioButton ID="rb_mb_symbol00" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S00.gif" alt="�L��" title="�L��" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol01" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S01.gif" alt="�N��" title="�N��" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol02" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S02.gif" alt="�o�N" title="�o�N" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol03" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S03.gif" alt="�`��" title="�`��" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol04" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S04.gif" alt="���_" title="���_" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol05" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S05.gif" alt="�T��" title="�T��" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol06" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S06.gif" alt="��" title="��" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol07" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S07.gif" alt="����" title="����" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol08" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S08.gif" alt="�L��" title="�L��" /><br />
				<asp:RadioButton ID="rb_mb_symbol09" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S09.gif" alt="�`��" title="�`��" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol10" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S10.gif" alt="�u��" title="�u��" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol11" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S11.gif" alt="�ˤ�" title="�ˤ�" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol12" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S12.gif" alt="����" title="����" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol13" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S13.gif" alt="���Y" title="���Y" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol14" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S14.gif" alt="�j�K" title="�j�K" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol15" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S15.gif" alt="�q���p��" title="�q���p��" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol16" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S16.gif" alt="OK" title="OK" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_symbol17" runat="server" Text="" GroupName="rb_mb_symbol" /><img src="../images/symbol/S17.gif" alt="§��" title="§��" />
			</td>
		</tr>
		<tr><td style="width:10%; text-align:center; background-color:#99CCFF; color:#FFFFFF">�m�W</td>
			<td align="left" style="width:40%;">
				<asp:TextBox ID="tb_mb_name" runat="server" Text="" MaxLength="12" Width="100pt"></asp:TextBox>
			</td>
			<td style="width:10%; text-align:center; background-color:#99CCFF; color:#FFFFFF">�ʧO</td>
			<td align="left" style="width:40%;">
				<asp:RadioButton ID="rb_mb_sex1" runat="server" Text="" GroupName="rb_mb_sex" /><img src="../images/symbol/man.gif" alt="�k��" title="�k��" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_mb_sex2" runat="server" Text="" GroupName="rb_mb_sex" /><img src="../images/symbol/woman.gif" alt="�k��" title="�k��" />
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">E-Mail</td>
			<td align="left" colspan="3">
				<asp:TextBox ID="tb_mb_email" runat="server" Text="" MaxLength="100" Width="98%"></asp:TextBox>
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">���e</td>
			<td align="left" colspan="3">
				<asp:TextBox ID="tb_mb_desc" Rows="10" TextMode="MultiLine" runat="server" MaxLength="1000" Width="98%"></asp:TextBox>
			</td>
		</tr>
		<tr><td style="width:10%; text-align:center; background-color:#99CCFF; color:#FFFFFF">�d���ɶ�</td>
			<td align="left" style="width:40%;">
				<asp:Label ID="lb_mb_time" runat="server"></asp:Label>
			</td>
			<td style="width:10%; text-align:center; background-color:#99CCFF; color:#FFFFFF">IP</td>
			<td align="left" style="width:40%;">
				<asp:Label ID="lb_mb_ip" runat="server"></asp:Label>
			</td>
		</tr>
		</table>
		<p style="width:98%; text-align:left; margin:5pt 0pt 0pt 0pt">�� �u��ܡv�]��[����]�ɡA�|�N�d�����e�Ρu���N��r�v�Ө��N��ܡC</p>
		<p style="width:98%; text-align:left; margin:1pt 0pt 0pt 0pt">�� �u�}���v�]��[����]�ɡA�ӵ��d�����|�X�{�b�e�ݪ������C</p>
		<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;�s��&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.location.reload(true);" class="abtn">&nbsp;����&nbsp;</a>
		</p>&nbsp;
	</td></tr>
	</table>
	</center>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
