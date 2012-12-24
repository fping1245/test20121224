<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B00142.aspx.cs" Inherits="_B00142"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>考試題庫管理</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="1" cellpadding="4" cellspacing="0" style="width:100%; background-color:#EFEFEF">
	<tr><td align="center">
		<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">修改試題</p>
		<table border="1" cellpadding="4" cellspacing="0" style="width:98%; background-color:#F7F7DE">
		<tr><td style="text-align:center; width:60pt; background-color:#99CCFF; color:#FFFFFF">試卷標題</td>
			<td style="text-align:left" colspan="3"><asp:Label ID="lb_tp_title" runat="server"></asp:Label></td>
		</tr>
		<tr><td style="text-align:center; width:60pt; background-color:#99CCFF; color:#FFFFFF">題號</td>
			<td style="text-align:left"><asp:TextBox ID="tb_tq_sort" runat="server" MaxLength="3" Width="20pt"></asp:TextBox></td>
			<td style="text-align:center; width:60pt; background-color:#99CCFF; color:#FFFFFF">試題分數</td>
			<td style="text-align:left"><asp:TextBox ID="tb_tq_score" runat="server" Text="5" MaxLength="3" Width="20pt"></asp:TextBox>&nbsp;分</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">答題方式</td>
			<td align="left" colspan="3">
				<asp:RadioButton ID="rb_tq_type0" runat="server" Text="單選" CssClass="text9pt" GroupName="rb_tq_type" Checked="true" AutoPostBack="True" oncheckedchanged="rb_tq_type0_CheckedChanged" />
				<asp:RadioButton ID="rb_tq_type1" runat="server" Text="複選" CssClass="text9pt" GroupName="rb_tq_type" AutoPostBack="True" oncheckedchanged="rb_tq_type1_CheckedChanged"/>&nbsp;&nbsp;
				<asp:Literal ID="lt_tq_type" runat="server" Text="複選題數上限：" Visible="false"></asp:Literal>
				<asp:TextBox ID="tb_tq_type" runat="server" Width="30px" Text="2" Visible="false"></asp:TextBox>
				<asp:Literal ID="lt_tq_type_desc" runat="server" Text="(1:複選全部 2~255:限制的複選題數)" Visible="false"></asp:Literal>
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">試題文字</td>
			<td align="left" colspan="3"><asp:TextBox ID="tb_tq_desc" Rows="10" TextMode="MultiLine" runat="server" MaxLength="200" Width="98%"></asp:TextBox></td>
		</tr>
		<tr><td style="text-align:center; width:60pt; background-color:#99CCFF; color:#FFFFFF">最後異動時間</td>
			<td align="left" colspan="3"><asp:Label ID="lb_init_time" runat="server"></asp:Label></td>
		</tr>
		</table>
		<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;存檔&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.location.reload(true);" class="abtn">&nbsp;取消&nbsp;</a>
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
