<%@ Page Language="C#" AutoEventWireup="true" CodeFile="700111.aspx.cs" Inherits="_700111" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�Ȥ�ϥκ�</title>
<script language="javascript" type="text/javascript">
	function send_msg() {
		var bnobj = document.getElementById("bn_smsg");
		
		if (bnobj != null)
			bnobj.click();
	}
</script>
</head>
<body style="margin:5px 0px 5px 0px" onkeydown="if(!(window.event.shiftKey) && window.event.keyCode==13) {send_msg();}">
	<form id="form1" runat="server">
	<div>
	<center>
	<table width="100%" border="0" cellpadding="2" cellspacing="0">
	<tr><td colspan="3" align="center">
			<asp:TextBox ID="tb_cm_desc" runat="server" MaxLength="1000" Rows="4" TextMode="MultiLine" Width="98%" ToolTip="�@�ӰT���̦h1000�Ӧr�A�W�L�|�I�_�I"></asp:TextBox>
		</td>
	</tr>
	<tr><td align="left">&nbsp;<asp:Button ID="bn_smsg" runat="server" Text="�ǰe�T��" Height="24px" onclick="bn_smsg_Click" /></td>
		<td align="center">�T����J�ɽХ� Shift-Enter ����C���U Enter �Y�o�e�T���C</td>
		<td align="right">
			<asp:FileUpload ID="fu_file" runat="server" Height="24px" ToolTip="��@�ɮפW�ǳ̤j�Ȭ� 4096K bytes" />
			<asp:Button ID="bn_sfile" runat="server" Text="�W���ɮ�" Height="24px" onclick="bn_sfile_Click" />&nbsp;&nbsp;
		</td>
	</tr>
	</table>
	</center>
	<asp:Label ID="lb_cu_sid" runat="server" Text="0" Visible="false"></asp:Label>
	<asp:Label ID="lb_mg_sid" runat="server" Text="-1" Visible="false"></asp:Label>
	</div>
	<script language="javascript" type="text/javascript">
		resize();

		// ���s�վ�����ت�����
		function resize() {
			var ifobj;
			ifobj = parent.document.getElementById("if_send");
			if (ifobj != null) {
				ifobj.style.height = (document.body.clientHeight + 10) + "px";
			}
		}
	</script>
	</form>
</body>
</html>
