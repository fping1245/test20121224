﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="1003.aspx.cs" Inherits="_1003" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>由資料庫動態產生功能選單</title>
</head>
<body style="font-size:11pt">
<form id="form1" runat="server">
<div>
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體;">由資料庫動態產生功能選單範例</p>
	<center>
	<table border="1" cellpadding="2" cellspacing="2" class="text11pt" style="width:200pt">
	<tr><td align="left">
			<asp:TreeView ID="tv_func" runat="server" EnableClientScript="False" 
			ExpandDepth="1" AutoGenerateDataBindings="False"
			LineImagesFolder="~/images/TreeLineImages" Font-Size="11pt" CssClass="text11pt" 
			EnableTheming="True">
				<ParentNodeStyle CssClass="text11pt" Font-Size="11pt" Width="100%" />
				<SelectedNodeStyle CssClass="text11pt" Font-Size="11pt" Width="100%" />
				<RootNodeStyle CssClass="text11pt" Font-Size="11pt" Width="100%" />
				<NodeStyle CssClass="text11pt" Font-Size="11pt" Width="100%" />
			</asp:TreeView>
		</td>
	</tr>
	</table>
	</center>
</div>
</form>
</body>
</html>
