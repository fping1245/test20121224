<%@ Page Language="C#" AutoEventWireup="true" CodeFile="40014.aspx.cs" Inherits="_40014" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�r���ƼҲ�</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 0pt 0pt; font-family:�з���;">�r���ƼҲ�</p>
	<p align="center" class="text14pt" style="margin:10pt 0pt 0pt 0pt; font-family:�з���;">����񺡦r��</p>
	<table width="98%" border="1" cellpadding="4" cellspacing="0" class="text9pt">
	<tr><td rowspan="4" style="width:120pt">����񺡦r��</td>
		<td style="width:40pt">���A</td>
		<td align="left">string FillLeft(string1, int)�G���w���ת��r��A�������ת������ 0
		<br />string FillLeft(string1, int, string2)	���w���ת��r��A�������ת������J�]�w�r��</td>
	</tr>

	<tr><td>��J</td>
		<td align="left">��l�r��G<asp:TextBox ID="tb_FillLeft_str1" runat="server" Text="abcde"></asp:TextBox>
			<br />���w���סG<asp:TextBox ID="tb_FillLeft_int" runat="server" Text="12"></asp:TextBox>&nbsp;&nbsp;
			<br />�񺡦r���G<asp:TextBox ID="tb_FillLeft_str2" runat="server" Text="$"></asp:TextBox>
		</td>
	</tr>
	<tr><td><asp:Button ID="bn_FillLeft" runat="server" Text="��X" onclick="bn_FillLeft_Click" /></td>
		<td align="left"><asp:Label ID="lb_FillLeft" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>&nbsp;</td>
	</tr>
	</table>
	<p><a href="4001.aspx" class="abtn">&nbsp;�^�W�@��&nbsp;</a></p>&nbsp;
	</center>
	</div>
	</form>
</body>
</html>
