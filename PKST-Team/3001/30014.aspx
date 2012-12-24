<%@ Page Language="C#" AutoEventWireup="true" CodeFile="30014.aspx.cs" Inherits="_30014" %>
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
	<table border="2" cellpadding="2" cellspacing="2" style="width:240px" bgcolor="#efefef">
	<tr><td align="center" class="text9pt"><br />
			<asp:Label ID="lb_stitle" runat="server" Text="原目錄名稱" CssClass="text11pt"></asp:Label><br />
			<asp:TextBox ID="tb_s_al_name" runat="server" Height="16pt" ForeColor="Red" Font-Bold="true" CssClass="text9pt" EnableViewState="true" ReadOnly="true"></asp:TextBox><br /><br />
			<asp:Label ID="lb_sdesc" runat="server" Text="原備註說明" CssClass="text11pt"></asp:Label><br />
			<asp:TextBox ID="tb_s_al_desc" runat="server" Height="16pt" ForeColor="Red" Font-Bold="true" CssClass="text9pt" EnableViewState="true" ReadOnly="true"></asp:TextBox>
			<br /><hr style="width:95%" /><br />
			<asp:Label ID="lb_ntitle" runat="server" Text="新目錄名稱" CssClass="text11pt"></asp:Label><br />
			<asp:TextBox ID="tb_al_name" runat="server" Height="16pt" CssClass="text9pt" EnableViewState="False"></asp:TextBox><br /><br />
			<asp:Label ID="lb_ndesc" runat="server" Text="新備註說明" CssClass="text11pt"></asp:Label><br />
			<asp:TextBox ID="tb_al_desc" runat="server" Height="16pt" CssClass="text9pt" EnableViewState="False"></asp:TextBox><br /><br />
			<asp:Button ID="bn_rndir_ok" runat="server" Text="確定" Height="22pt" EnableViewState="true" onclick="bn_rndir_ok_Click" OnClientClick="parent.close_all()" />
			<input type="button" value="取消" onclick="parent.close_all()" style="height:22pt" /><br /><br />
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
	</script>
	</form>
</body>
</html>
