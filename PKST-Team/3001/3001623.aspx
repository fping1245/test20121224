<%@ Page Language="C#" AutoEventWireup="true" CodeFile="3001623.aspx.cs" Inherits="_3001623" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>��ï�޲z</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="2" cellpadding="2" cellspacing="2" style="width:400px" bgcolor="#efefef">
	<tr><td align="center">
			<p style="margin:10px 0px 5px 0px; font-size:11pt; font-weight:bold; color:Red">�ۤ��R���T�{</p>
			<table border ="1" cellpadding="2" cellspacing="0" width="100%" bgcolor="#efefef">
			<tr><td style="width:60" align="center">�ۤ��W��</td>
				<td align="left"><asp:Label ID="lb_ac_name" runat="server"></asp:Label></td>
			</tr>
			<tr><td style="width:60" align="center">�Ƶ�����</td>
				<td align="left"><asp:Label ID="lb_ac_desc" runat="server"></asp:Label></td>
			</tr>
			<tr><td style="width:60" align="center">�ۤ��j�p</td>
				<td align="left"><asp:Label ID="lb_ac_size" runat="server"></asp:Label></td>
			</tr>
			<tr><td style="width:60" align="center">�ۤ��ؤo</td>
				<td align="left"><asp:Label ID="lb_ac_wh" runat="server"></asp:Label></td>
			</tr>
			<tr><td style="width:60" align="center">�ɮ׫���</td>
				<td align="left"><asp:Label ID="lb_ac_type" runat="server"></asp:Label></td>
			</tr>
			<tr><td style="width:60" align="center">�W�Ǯɶ�</td>
				<td align="left"><asp:Label ID="lb_init_time" runat="server"></asp:Label></td>
			</tr>
			</table>
			<p style="margin:5px 0px 10px 0px">
			<asp:Button ID="bn_ok" runat="server" Height="22pt" Text="�T�w" 
					ToolTip="���U�T�w����A�ɮ״N�����F�A�ФT���I" onclick="bn_ok_Click" />&nbsp;
			<input type="button" value="����" onclick="parent.close_all()" style="height:22pt" />
			</p>
		</td>
	</tr>
	</table>
	</center>
	<asp:Label ID="lb_al_sid" runat="server" Visible="false"></asp:Label>
	<asp:Label ID="lb_ac_sid" runat="server" Visible="false"></asp:Label>
	<asp:Label ID="lb_rownum" runat="server" Visible="false"></asp:Label>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	</div>
	<script language="javascript" type="text/javascript">
		resize();

		// ���s�վ�����ت�����
		function resize() {
			var ifobj;
			ifobj = parent.document.getElementById("if_win");
			if (ifobj != null) {
				ifobj.style.height = (document.body.clientHeight) + "px";
			}

			ifobj = parent.document.getElementById("div_win");
			if (ifobj != null) {
				ifobj.style.height = (document.body.clientHeight) + "px";
			}
		}
	</script>
	</form>
</body>
</html>
