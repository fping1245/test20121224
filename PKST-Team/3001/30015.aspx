<%@ Page Language="C#" AutoEventWireup="true" CodeFile="30015.aspx.cs" Inherits="_30015" %>
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
	<table border="2" cellpadding="2" cellspacing="2" style="width:260px" bgcolor="#efefef">
	<tr><td align="center" class="text9pt"><br />
			<asp:Label ID="lb_upfile" runat="server" Text="請選擇上傳的檔案" CssClass="text11pt"></asp:Label><br /><br />
			<table width="90%" border="0" cellpadding="0" cellspacing="0">
			<tr><td><asp:FileUpload ID="fu_upfile" runat="server" Height="20pt" Width="160px" CssClass="text10pt" EnableViewState="False" /></td></tr>
			</table>
			<br />
			備註說明<br />
			<asp:TextBox ID="tb_ac_desc" runat="server"></asp:TextBox>
			<br /><br />
			<asp:Button ID="bn_upfile_ok" runat="server" Text="確定" Height="22pt" OnClick="bn_upfile_ok_Click" OnClientClick="min_window();parent.show_msg()" />
			<input type="button" value="取消" onclick="parent.close_all()" style="height:22pt" />
			<br /><br />
		</td>
	</tr>
	</table>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	<asp:Label ID="lb_al_sid" runat="server" Visible="false"></asp:Label>
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
	
		// 為配合 FireFox，使用此方式縮小工作視窗		
		function min_window() {
			var ifobj;
			ifobj = parent.document.getElementById("if_win");

			if (ifobj != null) {
				ifobj.style.width = "1px";
				ifobj.style.height = "1px";
			}
		}
	</script>
	</form>
</body>
</html>
