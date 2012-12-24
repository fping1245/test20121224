<%@ Page Language="C#" AutoEventWireup="true" CodeFile="90021.aspx.cs" Inherits="_90021" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>POP3收信處理</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="1" cellpadding="4" cellspacing="0" style="width:400px; background-color:#EFEFEF">
	<tr><td align="center">
		<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">設定POP3主機</p>
		<table border="1" cellpadding="2" cellspacing="0" style="width:380px; background-color:#F7F7DE">
		<tr><td style="text-align:center; width:120px; height:30px; background-color:#99CCFF; color:#FFFFFF">主機名稱</td>
			<td align="left"><asp:TextBox ID="tb_ppa_host" runat="server" MaxLength="50" Width="150px"></asp:TextBox>(ex:seed.net.tw)</td>
		</tr>
		<tr><td style="text-align:center; width:120px; height:30px; background-color:#99CCFF; color:#FFFFFF">通訊埠</td>
			<td align="left"><asp:TextBox ID="tb_ppa_port" runat="server" MaxLength="5" Width="150px"></asp:TextBox>(ex:110)</td>
		</tr>
		<tr><td style="text-align:center; width:120px; height:30px; background-color:#99CCFF; color:#FFFFFF">登入帳號</td>
			<td align="left"><asp:TextBox ID="tb_ppa_id" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
		</tr>
		<tr><td style="text-align:center; width:120px; height:30px; background-color:#99CCFF; color:#FFFFFF">登入密碼</td>
			<td align="left"><asp:TextBox ID="tb_ppa_pw" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
		</tr>
		<tr><td style="text-align:center; width:120px; height:30px; background-color:#99CCFF; color:#FFFFFF">最後異動時間</td>
			<td align="left"><asp:Label ID="lb_init_time" runat="server"></asp:Label></td>
		</tr>
		</table>
		<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;存檔&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.location.replace('9002.aspx')" class="abtn">&nbsp;取消&nbsp;</a>
		</p>&nbsp;
	</td></tr>
	</table>
	<asp:Label ID="lb_ppa_sid" runat="server" Visible="false"></asp:Label>
	</center>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
