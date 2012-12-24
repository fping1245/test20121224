<%@ Page Language="C#" AutoEventWireup="true" CodeFile="90026.aspx.cs" Inherits="_90026" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>POP3收信處理</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="2" cellpadding="0" cellspacing="0" style="width:100%; background-color:#EFEFEF">
	<tr><td align="center" valign="middle" class="text14pt" style="height:60pt;">資料處理中，請稍候...</td></tr>
	</table>
	</center>
	</div>
	<iframe name="update" id="update" src="<%=lb_iframe.Text%>" width="0" height="0" scrolling="no" frameborder="1" style="display:none"></iframe>
	<asp:Label id="lb_iframe" runat="server" Visible="false" Text="#"></asp:Label>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</form>
</body>
</html>
