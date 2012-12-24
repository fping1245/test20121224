<%@ Page Language="C#" AutoEventWireup="true" CodeFile="800111.aspx.cs" Inherits="_800111" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<title>HTML編輯器</title>
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
	// 依比例重新定義顯示區域的寬及高
	function img_resize(imgobj) {
		var fCnt = 1.0, wCnt = 1.0, hCnt = 1.0;
		var imgw = "", imgh = "";

		if (imgobj.width > 100 || imgobj.height > 100) {
			wCnt = imgobj.width / 100.0;
			hCnt = imgobj.height / 100.0;

			if (wCnt > hCnt)
				fCnt = wCnt;
			else
				fCnt = hCnt;

			imgw = (imgobj.width / fCnt).toString() + "px";
			imgh = (imgobj.height / fCnt).toString() + "px"

			imgobj.style.width = imgw;
			imgobj.style.height = imgh;
		}
	}

	// 刪除詢問
	function mdel(msid, hfname) {
		if (confirm("確定要刪除「" + hfname + "」?\n")) {
			update.location.replace("8001112.ashx?sid=" + msid + "&he_sid=" +<%=lb_he_sid.Text %>);
		}
	}
	
	// 全部影像重新設定尺寸
	function all_img_resize() {
		var lcnt = document.images.length;
		var icnt = 0;
		
		for (icnt = 0; icnt < lcnt; icnt++) {
			img_resize(document.images[icnt]);
		}
	}
</script>
</head>
<body onload="setTimeout('all_img_resize()', 2500)">
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="0" cellpadding="4" cellspacing="0">
	<asp:Literal ID="lt_image" runat="server"></asp:Literal>
	</table>
	<iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
	</center>
	<asp:Label ID="lb_he_sid" runat="server" Text="0" Visible="false"></asp:Label>
	</div>
	</form>
</body>
</html>
