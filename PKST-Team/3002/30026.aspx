<%@ Page Language="C#" AutoEventWireup="true" CodeFile="30026.aspx.cs" Inherits="_30026" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>相簿管理</title>
</head>
<body onload="resize()">
	<form id="form1" runat="server">
	<div>
	<center>
	<table id="tbl_blank" border="0" cellpadding="2" cellspacing="0">
	<tr style="height:120px" valign="middle">
		<td align="center" style="width:120px"><img src="<%=ac_src[0]%>" title="<%=ac_name[0]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[0]%>',<%=rownum[0]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[1]%>" title="<%=ac_name[1]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[1]%>',<%=rownum[1]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[2]%>" title="<%=ac_name[2]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[2]%>',<%=rownum[2]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[3]%>" title="<%=ac_name[3]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[3]%>',<%=rownum[3]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[4]%>" title="<%=ac_name[4]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[4]%>',<%=rownum[4]%>)" /></td>
	</tr>
	<tr style="height:120px">
		<td align="center" style="width:120px"><img src="<%=ac_src[5]%>" title="<%=ac_name[5]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[5]%>',<%=rownum[5]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[6]%>" title="<%=ac_name[6]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[6]%>',<%=rownum[6]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[7]%>" title="<%=ac_name[7]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[7]%>',<%=rownum[7]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[8]%>" title="<%=ac_name[8]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[8]%>',<%=rownum[8]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[9]%>" title="<%=ac_name[9]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[9]%>',<%=rownum[9]%>)" /></td>
	</tr>
	<tr style="height:120px">
		<td align="center" style="width:120px"><img src="<%=ac_src[10]%>" title="<%=ac_name[10]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[10]%>',<%=rownum[10]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[11]%>" title="<%=ac_name[11]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[11]%>',<%=rownum[11]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[12]%>" title="<%=ac_name[12]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[12]%>',<%=rownum[12]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[13]%>" title="<%=ac_name[13]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[13]%>',<%=rownum[13]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[14]%>" title="<%=ac_name[14]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[14]%>',<%=rownum[14]%>)" /></td>
	</tr>
	<tr style="height:120px">
		<td align="center" style="width:120px"><img src="<%=ac_src[15]%>" title="<%=ac_name[15]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[15]%>',<%=rownum[15]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[16]%>" title="<%=ac_name[16]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[16]%>',<%=rownum[16]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[17]%>" title="<%=ac_name[17]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[17]%>',<%=rownum[17]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[18]%>" title="<%=ac_name[18]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[18]%>',<%=rownum[18]%>)" /></td>
		<td align="center" style="width:120px"><img src="<%=ac_src[19]%>" title="<%=ac_name[19]%>" alt="" style="border-style:none" onclick="showimg('<%=ac_name[19]%>',<%=rownum[19]%>)" /></td>
	</tr>
	<tr><td colspan="5" style="background-color:#cfcfcf;" align="center">
			<img src="../images/button/bn1-up.gif" title="上一頁" alt="" style="margin:0px 0px -3px 0px; border-style:none" onclick="javascript:goPage(<%=int.Parse(lb_pageid.Text) - 1 %>);" />
			<asp:Literal ID="lt_button" Text="" runat="server"></asp:Literal>
			<img src="../images/button/bn1-down.gif" title="下一頁" alt="" style="margin:0px 0px -3px 0px; border-style:none" onclick="javascript:goPage(<%=int.Parse(lb_pageid.Text) + 1 %>);" />
		</td>
	</tr>
	</table>
	</center>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	<asp:Label ID="lb_fl_url" runat="server" Visible="false"></asp:Label>
	<asp:Label ID="lb_path" runat="server" Visible="false"></asp:Label>
	<asp:Label ID="lb_fl_url_encode" runat="server" Visible="false"></asp:Label>
	<asp:Label ID="lb_pageid" Text="0" runat="server" Visible="false"></asp:Label>
	<script language="javascript" type="text/javascript">
		var maxpage = <%=maxpage%>;
		var maxrow = <%=maxrow%>;
		var	pageid = <%=pageid %>;
		var imgwin;
	
		// 重新調整母頁框的高度
		function resize() {
			var ifobj;
			ifobj = parent.document.getElementById("if_thumb");
			if (ifobj != null) {
				ifobj.style.height = (document.body.clientHeight + 15) + "px";
			}
		}

		// 換頁處理
		function goPage(mpage) {
			if (mpage < 0) {
				mpage = 0;
				alert("已經在第一頁了!");
			}
			else if (mpage > maxpage) {
				mpage = maxpage;
				alert("已經是最未頁了!");
			}

			if (mpage != pageid)
				location.href = "30026.aspx?fl_url=<%=lb_fl_url_encode.Text%>&pageid=" + mpage;
		}
		
		// 顯示圖檔
		function showimg(fname, row) {
			if (row > 0) {
				var features;

				features = "width=900px";			// 視窗寬度
				features += ",height=675px";		// 視窗高度
				features += ",top=" + ((window.screen.availHeight - 675)/2).toString() + "px";
				features += ",left=" + ((window.screen.availWidth - 900)/2).toString() + "px";
				features += ",toolbar=no,location=no, menubar=no,status=yes,resizable=yes,scrollbars=yes";
				imgwin = window.open("300262.aspx?fl_url=<%=lb_fl_url_encode.Text%>&fname=" + escape(fname) + "&rownum=" + row + "&maxrow=" + maxrow + "&effect=" + parent.show_effect, "win_img", features);
			}
		}
	</script>
	</div>
	</form>
</body>
</html>
