<%@ Page Language="C#" AutoEventWireup="true" CodeFile="30021.aspx.cs" Inherits="_30021" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>¨€√Ø∫ﬁ≤z</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
    <asp:TreeView ID="tv_Al_List" runat="server">
		<ParentNodeStyle Font-Bold="False" />
		<HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
		<SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
		<NodeStyle Font-Names="Tahoma" Font-Size="11pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" ImageUrl="~/images/ico/folderClosed.gif" />
	</asp:TreeView>
	<asp:Label ID="lb_fl_url" runat="server" Visible="false" Text=""></asp:Label>
	<asp:Label ID="lb_path" runat="server" Visible="false" Text=""></asp:Label>
	</div>
	</form>
</body>
</html>
