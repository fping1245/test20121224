<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A0021_old.aspx.cs" Inherits="_A0021" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>���ﵲ�G�έp</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<table border="1" cellpadding="4" cellspacing="0" style="width:100%; background-color:#EFEFEF">
	<tr><td align="center">
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:�з���; text-align:center">����έp���G</p>

	<table border="1" cellpadding="4" cellspacing="0" style="width:100%; background-color:#F7F7DE; margin-bottom:5pt">
	<tr><td style="text-align:center; width:15%; background-color:#99CCFF; color:#FFFFFF">����覡</td>
		<td style="text-align:left; width:35%"><asp:Label ID="lb_is_check" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; width:15%; background-color:#99CCFF; color:#FFFFFF">�D�D�s��</td>
		<td style="text-align:left; width:35%">[ <asp:Label ID="lb_bh_sid" runat="server"></asp:Label> ]&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">������D</td>
		<td style="text-align:left" colspan="3"><asp:Label ID="lb_bh_title" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">���e����</td>
		<td style="text-align:left" colspan="3"><asp:Label ID="lb_bh_topic" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">��ܦ���</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_scnt" runat="server"></asp:Label>&nbsp;��</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�^������</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_acnt" runat="server"></asp:Label>&nbsp;��</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">����`��</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_total" runat="server"></asp:Label>&nbsp;��</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�̫�벼�ɶ�</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_time" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	</table>
	<p class="text11pt" style="margin:5pt 0pt 2pt 0pt; text-align:center">���ﶵ�زέp��</p>
	<asp:Literal ID="lt_stat" runat="server"></asp:Literal>
	<p class="text9pt" style="margin:5pt 0pt 2pt 0pt; text-align:center">(�i�t�X MS Chart �Ϫ���]�p�h�ت��Ϫ�A���B�Ȩϥ�²�檺 Table �覡��ø�s������)</p>
	<p style="margin:10pt 0pt 0pt 0pt; text-align:center"><a href="javascript:parent.close_all();" class="abtn">&nbsp;����&nbsp;</a></p>&nbsp;
		</td>
	</tr>
	</table>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
