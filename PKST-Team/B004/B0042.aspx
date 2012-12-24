<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B0042.aspx.cs" Inherits="_B0042" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>考試成績統計</title>
<script language="javascript" type="text/javascript">
	var title_var = 1; 	// 1:展開 0:縮小

	// 縮放試卷抬頭
	function title_style()
	{
		var id1obj = document.getElementById("id1");
		var id2obj = document.getElementById("id2");
		var id4obj = document.getElementById("id4");
		var id5obj = document.getElementById("id5");
	
		if (title_var == 1)
		{
			// 縮小處理
			title_var = 0;
			title_btn.innerHTML = "&nbsp;展開標題&nbsp;";

			if (id1obj != null)
				id1obj.style.display = "none";

			if (id2obj != null)
				id2obj.style.display = "none";

			if (id4obj != null)
				id4obj.style.display = "none";

			if (id5obj != null)
				id5obj.style.display = "none";
		}
		else
		{
			// 展開處理
			title_var = 1;
			title_btn.innerHTML = "&nbsp;收縮標題&nbsp;";

			if (id1obj != null)
				id1obj.style.display = "";

			if (id2obj != null)
				id2obj.style.display = "";

			if (id4obj != null)
				id4obj.style.display = "";

			if (id5obj != null)
				id5obj.style.display = "";
		}
	}
</script>
</head>
<body style="white-space:normal">
	<form id="form1" runat="server">
	<div>

	<center>
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">考試成績統計</p>
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">試題成績分佈</p>
	<table border="1" cellpadding="4" cellspacing="0" style="width:98%; background-color:#F7F7DE; margin-bottom:10px">
	<tr id="id1">
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">試卷編號</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_tp_sid" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">是否顯示</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_is_show" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr id="id2">
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">開始進入時間</td>
		<td style="text-align:left"><asp:Label ID="lb_b_time" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">截止進入時間</td>
		<td style="text-align:left"><asp:Label ID="lb_e_time" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr id="id3">
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">試卷標題</td>
		<td style="text-align:left" colspan="3"><asp:Label ID="lb_tp_title" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr id="id4"><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">試卷說明</td>
		<td style="text-align:left" colspan="3"><asp:Label ID="lb_tp_desc" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr id="id5">
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">試卷題數</td>
		<td style="text-align:left"><asp:Label ID="lb_tp_question" runat="server"></asp:Label>&nbsp;題</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">試卷總分</td>
		<td style="text-align:left"><asp:Label ID="lb_tp_score" runat="server"></asp:Label>&nbsp;分</td>
	</tr>
	<tr id="id6">
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">參加人數</td>
		<td style="text-align:left"><asp:Label ID="lb_tp_member" runat="server"></asp:Label>&nbsp;人</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">總得分/總平均</td>
		<td style="text-align:left">
			<asp:Label ID="lb_tp_total" runat="server"></asp:Label>&nbsp;分&nbsp;/&nbsp;
			<asp:Label ID="lb_tp_avg" runat="server"></asp:Label>&nbsp;分
		</td>
	</tr>
	<tr id="id7">
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">最後異動時間</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_init_time" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; width:12%; background-color:#888800; color:#FFFFFF">功能選項</td>
		<td style="text-align:left; background-color:#DDDDDD; width:38%">
			<a href="javascript:title_style()" id="title_btn" class="abtn" title="試卷標題縮放處理">&nbsp;收縮標題&nbsp;</a>
		</td>
	</tr>
	</table>
	
	<table border="1" cellpadding="4" cellspacing="0" style="width:98%; background-color:#FFFFE0">
	<tr><td><asp:Panel ID="pl_show" runat="server"></asp:Panel></td></tr>
	</table>

	<p class="text9pt" style="margin:5pt 0pt 2pt 0pt; text-align:center">(可配合 MS Chart 圖表控制項設計多種的圖表，此處僅使用簡單的 Table 方式來繪製長條圖)</p>
	<p style="margin:10pt 0pt 0pt 0pt"><a href="B004.aspx<%=lb_page.Text%>" class="abtn">&nbsp;回試卷清單&nbsp;</a></p>&nbsp;
	
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
	</center>
	<script language="javascript" type="text/javascript">
		title_style();
	</script>
	</div>
	</form>
</body>
</html>
