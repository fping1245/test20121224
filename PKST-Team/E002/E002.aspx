<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E002.aspx.cs" Inherits="_E002" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>網頁繁簡轉換範例</title>
</head>
<body>
<form id="form1" runat="server">
<div>
<p align="center" class="text18pt" style="margin:10pt 0pt 15pt 0pt; font-family:標楷體;">網頁繁簡轉換範例</p>
<center>
<asp:Button ID="bn_tobig5" runat="server" Text="轉換成繁體網頁" CssClass="text12pt" onclick="bn_tobig5_Click" ToolTip="轉換成繁體網頁" />
<br />
<br />
<asp:Button ID="bn_togb" runat="server" Text="转换成简体网页" CssClass="text12pt" onclick="bn_togb_Click" ToolTip="转换成简体网页" />
</center>
</div>
</form>
<script language="javascript" type="text/javascript">
	// 更新 frame 內各個網頁
	function frame_reload() {
		top.location.reload();
	}
</script>
</body>
</html>
