<%@ Page Language="C#" AutoEventWireup="true" CodeFile="3002624.aspx.cs" Inherits="_3002624" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>相簿管理</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="2" cellpadding="2" cellspacing="2" style="width:400px" bgcolor="#efefef">
	<tr><td align="center">
			<p style="margin:10px 0px 5px 0px; font-size:11pt; font-weight:bold; color:Green">相片名稱修改</p>
			<table border ="1" cellpadding="2" cellspacing="0" width="100%" bgcolor="#efefef">
			<tr><td style="width:60" align="center">原相片名稱</td>
				<td align="left"><asp:Label ID="lb_fl_name" runat="server" Width="100%"></asp:Label></td>
			</tr>
			<tr><td style="width:60" align="center">新相片名稱</td>
				<td align="left"><asp:TextBox ID="tb_ac_name" runat="server" Width="100%"></asp:TextBox></td>
			</tr>
			<tr><td style="width:60" align="center">相片大小</td>
				<td align="left"><asp:Label ID="lb_ac_size" runat="server"></asp:Label></td>
			</tr>
			<tr><td style="width:60" align="center">相片尺寸</td>
				<td align="left"><asp:Label ID="lb_ac_wh" runat="server"></asp:Label></td>
			</tr>
			<tr><td style="width:60" align="center">檔案型式</td>
				<td align="left"><asp:Label ID="lb_ac_type" runat="server"></asp:Label></td>
			</tr>
			<tr><td style="width:60" align="center">上傳時間</td>
				<td align="left"><asp:Label ID="lb_init_time" runat="server"></asp:Label></td>
			</tr>
			</table>
			<p style="margin:5px 0px 10px 0px">
			<asp:Button ID="bn_ok" runat="server" Height="22pt" Text="確定" onclick="bn_ok_Click" />&nbsp;
			<input type="button" value="取消" onclick="parent.close_all()" style="height:22pt" />
			</p>
		</td>
	</tr>
	</table>
	</center>
	<asp:Label ID="lb_fl_url" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_fl_url_encode" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_path" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	</div>
	<script language="javascript" type="text/javascript">
		resize();

		// 重新調整母頁框的高度
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
