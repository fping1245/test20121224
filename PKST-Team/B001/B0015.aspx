<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B0015.aspx.cs" Inherits="_B0015" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>考試題庫管理</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	var title_var = 1; 	// 1:展開 0:縮小

	// 新增考生資料 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function madd()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("B00151.aspx?tp_sid=<%=lb_tp_sid.Text%>&tp_title=<%=Server.UrlEncode(lb_tp_title.Text)%>&timestamp=" + timestamp, 680, 200);
	}

	// 修改考生資料 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("B00152.aspx?sid=" + msid + "&tp_sid=<%=lb_tp_sid.Text%>&tp_title=<%=Server.UrlEncode(lb_tp_title.Text)%>&timestamp=" + timestamp, 680, 200);
	}

	// 刪除考生及答題紀錄
	function mdel(msid, msort, mname)
	{
		if (confirm("確定要刪除第 " + msort + " 名「" + mname.replace(/ /g,"") + "」的考生及答題紀錄?\n"))
		{
			update.location.replace("B00153.ashx?sid=" + msid + "&tp_sid=<%=lb_tp_sid.Text%>");
		}
	}

	// 查看答題內容
	function mdetail(msid)
	{
		var mhref = "";
		mhref = "B00154.aspx<%=lb_page.Text%>&sid=<%=lb_tp_sid.Text%>&tu_name=<%=Server.UrlEncode(tb_tu_name.Text)%>";
		mhref += "&tu_no=<%=Server.UrlEncode(tb_tu_no.Text)%>&tu_ip=<%=Server.UrlEncode(tb_tu_ip.Text)%>&pageid1=<%=lb_pageid1.Text%>";
		mhref += "&tu_sid=" + msid;

		location.href = mhref;
	}

	// 縮放試卷抬頭
	function title_style()
	{
		var id1obj = document.getElementById("id1");
		var id2obj = document.getElementById("id2");
		var id4obj = document.getElementById("id4");
		var id5obj = document.getElementById("id5");
		var id6obj = document.getElementById("id6");
	
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

			if (id6obj != null)
				id6obj.style.display = "none";
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

			if (id6obj != null)
				id6obj.style.display = "";			

		}
	}
</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	
	<!-- Begin 覆蓋整個工作頁面，不讓使用者再按其它按鍵 -->
	<div id="fullwindow" style="position: absolute; top:0px; left:0px; width:1024px; height:768px; display:none">
	<table id="fulltable" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_back.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<!-- End -->

	<center>
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">考試題庫管理</p>
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">考試紀錄</p>
	<table border="1" cellpadding="4" cellspacing="0" style="width:98%; background-color:#F7F7DE; margin-bottom:5px">
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
		<td style="text-align:left"><asp:Label ID="lb_tb_question" runat="server"></asp:Label>&nbsp;題</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">試卷總分</td>
		<td style="text-align:left"><asp:Label ID="lb_tb_score" runat="server"></asp:Label>&nbsp;分</td>
	</tr>
	<tr id="id6">
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">參加人數</td>
		<td style="text-align:left"><asp:Label ID="lb_tb_member" runat="server"></asp:Label>&nbsp;人</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">總得分</td>
		<td style="text-align:left"><asp:Label ID="lb_tb_total" runat="server"></asp:Label>&nbsp;分</td>
	</tr>
	<tr id="id7">
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">最後異動時間</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_init_time" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; width:12%; background-color:#888800; color:#FFFFFF">功能選項</td>
		<td style="text-align:left; background-color:#DDDDDD; width:38%">
			<a href="javascript:madd();" class="abtn">&nbsp;新增考生&nbsp;</a>&nbsp;◆&nbsp;
			<asp:LinkButton ID="lk_sort" runat="server" CssClass="abtn" 
				Text="&nbsp;重新排名&nbsp;" ToolTip="依照考試成績重新編排名次"
				onclick="lk_sort_Click" OnClientClick="show_msg_wait()"></asp:LinkButton>
			<asp:LinkButton ID="lk_score" runat="server" CssClass="abtn" 
				Text="&nbsp;重新計分&nbsp;" ToolTip="全部試卷重新批改分數並重新編排名次"
				onclick="lk_score_Click" OnClientClick="show_msg_wait()"></asp:LinkButton>&nbsp;◆&nbsp;
			<a href="javascript:title_style()" id="title_btn" class="abtn" title="試卷標題縮放處理">&nbsp;收縮標題&nbsp;</a>
		</td>
	</tr>
	</table>
	
	<table width="98%" border="1" cellspacing="0" cellpadding="4"  bgcolor="#EFEFEF"  class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#F0F0F0">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" width="40" rowspan="2"><font color="#FFFFFF">顯示<br />條件</font></td>
		<td class="text9pt"><font color="#FFFFFF">姓名</font></td>
		<td class="text9pt"><font color="#FFFFFF">學號</font></td>
		<td class="text9pt"><font color="#FFFFFF">IP</font></td>
		<td class="text9pt" width="55"><font color="#FFFFFF">條件設定</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_tu_name" runat="server" Width="160pt" MaxLength="50"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_tu_no" runat="server" Width="160pt" MaxLength="50"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_tu_ip" runat="server" Width="160pt" MaxLength="50"></asp:TextBox></td>
		<td><asp:Button ID="btn_Set" runat="server" Text="設定" onclick="btn_Set_Click" /></td>
	</tr>
	</table>

	<asp:GridView ID="gv_Ts_User" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="tu_sid" DataSourceID="ods_Ts_User" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何考生的資料！" 
		HorizontalAlign="Center" onpageindexchanging="gv_Ts_User_PageIndexChanged" 
		AllowSorting="True" onrowdatabound="gv_Ts_User_RowDataBound" PageSize="10">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="20px" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="60pt" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="tu_sort" HeaderText="排名" InsertVisible="False" ReadOnly="True" SortExpression="tu_sort">
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tu_score" HeaderText="獲得分數" SortExpression="tu_score" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="50pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tu_name" HeaderText="姓名" InsertVisible="False" ReadOnly="True" SortExpression="tu_name" />
		<asp:BoundField DataField="tu_no" HeaderText="學號" InsertVisible="False" ReadOnly="True" SortExpression="tu_no" />
		<asp:BoundField DataField="tu_ip" HeaderText="考生IP" InsertVisible="False" ReadOnly="True" SortExpression="tu_ip" />
		<asp:BoundField DataField="tu_question" HeaderText="答對題數" SortExpression="tu_question" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="50pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="is_test" HeaderText="已作答" SortExpression="is_test">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="b_time" HeaderText="開始時間"
			SortExpression="b_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="65pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="e_time" HeaderText="完成時間"
			SortExpression="e_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="65pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="功能" ShowHeader="False">
			<ItemTemplate>
				<a href="javascript:mdetail(<%# Eval("tu_sid") %>)" class="abtn" title="查看考生考卷成績及答題狀況">&nbsp;成績&nbsp;</a>&nbsp;
				<a href="javascript:medit(<%# Eval("tu_sid") %>)" class="abtn" title="修改考生資料">&nbsp;修改&nbsp;</a>
				<a href="javascript:mdel(<%# Eval("tu_sid") %>, '<%# Eval("tu_sort") %>','<%# Eval("tu_name") %>')" class="abtn" title="刪除考生及答題紀錄">&nbsp;刪除&nbsp;</a>
			</ItemTemplate>
			<HeaderStyle Width="90pt" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何考生的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>

	<p style="margin:10pt 0pt 0pt 0pt; text-align:center"><a href="javascript:location.replace('B001.aspx<%=lb_page.Text%>');" class="abtn">&nbsp;回問卷清單&nbsp;</a></p>
	<p style="margin:5pt 0pt 0pt 0pt; text-align:left; width:98%">※ 「重新排名」及「重新計分」功能，為特殊狀況才需要使用(例如：原題目答案有誤，修正後需重新計分排名)；正常狀況下，在考生答題結束後，名次及成績均已計算完成。</p>
	<p style="margin:2pt 0pt 0pt 0pt; text-align:left; width:98%">※ 「線上考試」如採用「自由參加」的方式，不必先建立考生資料，考生在參加考試時填入資料即可。</p>
	<p style="margin:2pt 0pt 0pt 0pt; text-align:left; width:98%">※ 「線上考試」如採用「限定身份」的方式，要先建立考生資料，參加考試時要填入正確的身份資料，才允許進入考試。</p>

	<asp:ObjectDataSource ID="ods_Ts_User" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Ts_User" SelectMethod="Select_Ts_User"
			SortParameterName="SortColumn" TypeName="ODS_Ts_User_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="tp_sid" Type="String" />
			<asp:Parameter Name="tu_name" Type="String" />
			<asp:Parameter Name="tu_no" Type="String" />
			<asp:Parameter Name="tu_ip" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>
	
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_pageid1" runat="server" Text="" Visible="false"></asp:Label>
	
	<iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
	</center>
	
	<!-- Begin 隱藏視窗 -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	
	<!-- Begin 顯示處理訊息 -->
	<div id="div_ban" class="text9pt" style="position:absolute; width:240px; z-index:0; display:none">
	<table id="tb_ban" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_ban.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<div id="div_msg_wait" class="text9pt" style="position:absolute; width:240px; z-index:0; display:none">
	<table border="2" cellpadding="2" cellspacing="2" style="width:240px; height:100px; background-color:#efefef">
	<tr><td align="center" class="text9pt">資料處理中 ........<br />請勿按任何按鍵!</td></tr>
	</table>
	</div>
	<!-- End -->
	
	<script language="javascript" type="text/javascript">
		title_style();
	</script>
	</div>
	</form>
</body>
</html>
