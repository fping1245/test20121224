<%@ Page Language="C#" AutoEventWireup="true" CodeFile="40031.aspx.cs" Inherits="_40031" validateRequest=false %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>編碼函數模組</title>
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
	<p align="center" class="text18pt" style="margin:10pt 0pt 0pt 0pt; font-family:標楷體;">編碼函數模組</p>
	<p align="center" class="text14pt" style="margin:10pt 0pt 0pt 0pt; font-family:標楷體;">Base64編碼/解碼</p>
	<table width="98%" border="1" cellpadding="4" cellspacing="0" class="text9pt">
	<tr><td rowspan="6" style="width:120pt">Base64編碼/解碼</td>
		<td align="center" style="width:50pt">說明</td>
		<td align="left">&nbsp;</td>
	</tr>
	<tr><td align="center">CodePage</td>
		<td align="left"><asp:TextBox ID="tb_codepage" runat="server" Text="65001" Width="50px"></asp:TextBox>
			<input type="radio" name="rdo_cp" value="65001" onclick="chg_codepage(this.value)" />UTF8
			<input type="radio" name="rdo_cp"  value="950" onclick="chg_codepage(this.value)" />Big5
			<input type="radio" name="rdo_cp"  value="936" onclick="chg_codepage(this.value)" />GB2312
		</td>
	</tr>
	<tr><td align="center">原始字串</td>
		<td align="left"><asp:TextBox ID="tb_dcode" runat="server" Columns="80" Rows="10" TextMode="MultiLine"></asp:TextBox></td>
	</tr>
	<tr><td align="center" colspan="2">
			<asp:Button ID="bn_ecode" runat="server" Text="↓編碼" onclick="bn_ecode_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Button ID="bn_dcode" runat="server" Text="解碼↑" onclick="bn_dcode_Click" />			
		</td>
	</tr>
	<tr><td align="center">編碼字串</td>
		<td align="left"><asp:TextBox ID="tb_ecode" runat="server" Columns="80" Rows="10" TextMode="MultiLine"></asp:TextBox></td>
	</tr>
	</table>
	<p><a href="4003.aspx" class="abtn">&nbsp;回上一頁&nbsp;</a></p>&nbsp;
	</center>
	</div>
	</form>
</body>
</html>
