<%@ Page Language="C#" AutoEventWireup="true" CodeFile="40032.aspx.cs" Inherits="_40032" validateRequest=false %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�s�X��ƼҲ�</title>
<script language="javascript" type="text/javascript">
	function chg_codepage(mval) {
		var tbobj = document.getElementById("tb_codepage");
		tbobj.value = mval;
	}
</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 0pt 0pt; font-family:�з���;">�s�X��ƼҲ�</p>
	<p align="center" class="text14pt" style="margin:10pt 0pt 0pt 0pt; font-family:�з���;">Quoted Printable�s�X/�ѽX</p>
	<table width="98%" border="1" cellpadding="4" cellspacing="0" class="text9pt">
	<tr><td rowspan="7" style="width:120pt">Quoted Printable �s�X/�ѽX</td>
		<td align="center" style="width:50pt">����</td>
		<td align="left">&nbsp;</td>
	</tr>
	<tr><td align="center">CodePage</td>
		<td align="left"><asp:TextBox ID="tb_codepage" runat="server" Text="65001" Width="50px"></asp:TextBox>
			<input type="radio" name="rdo_cp" value="65001" onclick="chg_codepage(this.value)" />UTF8
			<input type="radio" name="rdo_cp"  value="950" onclick="chg_codepage(this.value)" />Big5
			<input type="radio" name="rdo_cp"  value="936" onclick="chg_codepage(this.value)" />GB2312
		</td>
	</tr>
	<tr><td align="center">�s�X�_��</td>
		<td align="left"><asp:TextBox ID="tb_linebreaks" runat="server" Text="1" Width="20px"></asp:TextBox>&nbsp;(�]�w�s�X�ɨC75�r�O�_�n�_��A0:���_�� 1:�_��C�s�X�ɤ~���@��)</td>
	</tr>
	<tr><td align="center">��l�r��</td>
		<td align="left"><asp:TextBox ID="tb_dcode" runat="server" Columns="80" Rows="10" TextMode="MultiLine"></asp:TextBox></td>
	</tr>
	<tr><td align="center" colspan="2">
			<asp:Button ID="bn_ecode" runat="server" Text="���s�X" onclick="bn_ecode_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Button ID="bn_dcode" runat="server" Text="�ѽX��" onclick="bn_dcode_Click" />			
		</td>
	</tr>
	<tr><td align="center">�s�X�r��</td>
		<td align="left"><asp:TextBox ID="tb_ecode" runat="server" Columns="80" Rows="10" TextMode="MultiLine"></asp:TextBox></td>
	</tr>
	</table>
	<p><a href="4003.aspx" class="abtn">&nbsp;�^�W�@��&nbsp;</a></p>&nbsp;
	</center>
	</div>
	</form>
</body>
</html>
