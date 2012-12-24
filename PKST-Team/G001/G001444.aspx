<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G001444.aspx.cs" Inherits="_G001444" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>資料庫規格管理</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<table border="1" cellpadding="4" cellspacing="0" style="width:100%; background-color:#EFEFEF; white-space:normal">
	<tr><td align="center">
			<p style="text-align:center; font-size:14pt; margin:10pt 0pt 10pt 0pt">調整欄位順序</p>
			<table border="1" cellpadding="4" cellspacing="0" style="width:98%">
			<tr><td align="left">&nbsp;原來順序：<asp:TextBox ID="tb_s_sort" runat="server" Width="40pt" MaxLength="4"></asp:TextBox></td></tr>
			<tr><td align="left">&nbsp;插入位置：<asp:TextBox ID="tb_t_sort" runat="server" Width="40pt" MaxLength="4"></asp:TextBox>&nbsp;之前</td>
			</tr>
			</table>
			<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;存檔&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.close_all();" class="abtn">&nbsp;取消&nbsp;</a>
		</p>&nbsp;
		</td>
	</tr>
	</table>
	<asp:Label ID="lb_ds_sid" runat="server" Visible="false"></asp:Label>
	<asp:Label ID="lb_dt_sid" runat="server" Visible="false"></asp:Label>
	<asp:Label ID="lb_dr_sid" runat="server" Visible="false"></asp:Label>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
