<%@ Page Language="C#" AutoEventWireup="true" CodeFile="3002622.aspx.cs" Inherits="_3002622" %>
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
			<p style="margin:10px 0px 5px 0px; font-size:11pt; font-weight:bold; color:Blue">相片內容說明</p>
			<table border ="1" cellpadding="2" cellspacing="0" width="100%" bgcolor="#efefef">
			<tr><td style="width:60" align="center">相片名稱</td>
				<td align="left"><asp:Label ID="lb_ac_name" runat="server"></asp:Label></td>
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
			<input type="button" value="關閉" onclick="parent.close_all()" style="height:22pt; margin:5px 0px 10px 0px" />
		</td>
	</tr>
	</table>
	</center>
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
