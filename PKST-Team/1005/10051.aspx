<%@ Page Language="C#" AutoEventWireup="true" CodeFile="10051.aspx.cs" Inherits="_10051" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>人員資料管理</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體;">人員資料管理</p>
	<p align="center" class="text16pt" style="margin:0pt 0pt 5pt 0pt; font-family:標楷體;">內容顯示</p>
	<center>
	<table border="0" cellpadding="2" cellspacing="0" class="text12pt" style="margin:5pt 0pt 5pt 0pt; width:580pt">
	<tr><td align="right" style="height:16pt">
			<a href="10051_add.aspx<%=lb_page.Text%>&sid=<%=lb_pg_mg_sid.Text%>" class="abtn" title="新增人員基本資料">&nbsp;新增資料&nbsp;</a>&nbsp;&nbsp;
			<a href="10051_edit.aspx<%=lb_page.Text%>&sid=<%=lb_pg_mg_sid.Text%>" class="abtn" title="修改人員基本資料">&nbsp;修改內容&nbsp;</a>&nbsp;&nbsp;
			<a href="10051_pass.aspx<%=lb_page.Text%>&sid=<%=lb_pg_mg_sid.Text%>" class="abtn" title="變更使用者密碼">&nbsp;變更密碼&nbsp;</a>&nbsp;&nbsp
			<asp:LinkButton ID="lk_del" runat="server" CssClass="abtn" Text="&nbsp;刪除資料&nbsp;" ToolTip="刪除人員及各項相關紀錄" OnClientClick="mdel()"></asp:LinkButton>&nbsp;&nbsp;
			<a href="1005.aspx<%=lb_page.Text%>" class="abtn">&nbsp;回上一頁&nbsp;</a>
		</td>
	</tr>
    </table>
	<table cellspacing="0" cellpadding="4" rules="all" border="1" style="background-color:White;border-color:#003366;border-width:1px;border-style:Double;width:580pt;border-collapse:collapse;">
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" style="width:90pt">編號</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:Label ID="lb_mg_sid" runat="server" Text=""></asp:Label>
			</td>
			<td align="center" style="width:90pt">帳號</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:Label ID="lb_mg_id" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center">姓名</td>
			<td style="background-color:#F7F7DE">
				<asp:Label ID="lb_mg_name" runat="server" Text=""></asp:Label>
			</td>
			<td align="center">暱稱</td>
			<td style="background-color:#F7F7DE">
				<asp:Label ID="lb_mg_nike" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center">單位</td>
			<td style="background-color:#F7F7DE" colspan="3">
				<asp:Label ID="lb_mg_unit" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center">說明</td>
			<td style="background-color:#F7F7DE" colspan="3">
				<asp:Label ID="lb_mg_desc" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center">最後登入時間</td>
			<td style="background-color:#F7F7DE">
				<asp:Label ID="lb_last_date" runat="server" Text=""></asp:Label>
			</td>
			<td align="center">最後修改時間</td>
			<td style="background-color:#F7F7DE">
				<asp:Label ID="lb_init_time" runat="server" Text=""></asp:Label>
			</td>
		</tr>
	</table>
	<hr style="width:98%; margin:5pt 0pt 5pt 0pt" />
    <table border="0" cellpadding="2" cellspacing="0" class="text12pt" style="width:580pt;margin:0pt 0pt 5pt 0pt">
    <tr><td style="width:120pt">&nbsp;</td>
		<td class="text14pt" style="text-align:center; font-family:標楷體">管理人員權限</td>
		<td align="right" style="width:120pt;height:16pt">
			<asp:LinkButton ID="lk_power" runat="server" CssClass="abtn" onclick="lk_power_Click">&nbsp;權限設定&nbsp;</asp:LinkButton>
		</td>
	</tr>
    </table>
	<asp:Literal ID="lt_power" runat="server" EnableViewState="False"></asp:Literal>
	<p style="margin:10pt 0pt 10pt 0pt"><a href="1005.aspx<%=lb_page.Text%>" class="abtn">&nbsp;回上一頁&nbsp;</a></p><br />
	</center>
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_pg_mg_sid" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Literal ID="lt_show" runat="server" EnableViewState="False"></asp:Literal>
    </div>
    <iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
    <script language="javascript" type="text/javascript">
    	function mdel() {
    		if (confirm("確定要刪除本筆人員及相關各項資料？"))
    		{
    			update.location.replace("10051_del.ashx<%=lb_page.Text%>&sid=<%=lb_pg_mg_sid.Text%>");
    		}
    	}
    </script>
    </form>
</body>
</html>
