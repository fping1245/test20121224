<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B0014.aspx.cs" Inherits="_B0014" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>考試題庫管理</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	var title_var = 1; 	// 1:展開 0:縮小

	// 新增題目 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function madd()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("B00141.aspx?tp_sid=<%=lb_tp_sid.Text%>&tp_title=<%=Server.UrlEncode(lb_tp_title.Text)%>&timestamp=" + timestamp, 680, 550);
	}

	// 修改題目 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("B00142.aspx?sid=" + msid + "&tp_sid=<%=lb_tp_sid.Text%>&tp_title=<%=Server.UrlEncode(lb_tp_title.Text)%>&timestamp=" + timestamp, 680, 550);
	}

	// 刪除題目
	function mdel(msid, msort, mdesc)
	{
		if (confirm("確定要刪除「" + msort + ". " + mdesc + "」的題目?\n"))
		{
			update.location.replace("B00143.ashx?sid=" + msid + "&tp_sid=<%=lb_tp_sid.Text%>");
		}
	}

	// 新增答案 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function add_item(msid, tq_sort, tq_desc)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("B00144.aspx?sid=" + msid + "&tp_sid=<%=lb_tp_sid.Text%>&tq_sort=" + tq_sort + "&tq_desc=" + tq_desc + "&timestamp=" + timestamp, 550, 250);
	}

	// 修改答案 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function mod_item(msid, tq_sid, tq_sort, tq_desc)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("B00145.aspx?sid=" + msid + "&tp_sid=<%=lb_tp_sid.Text%>&tq_sort=" + tq_sort + "&tq_desc=" + tq_desc + "&tq_sid=" + tq_sid + "&timestamp=" + timestamp, 550, 250);
	}

	// 刪除答案
	function del_item(msid, tq_sid, ti_sort, ti_desc)
	{
		if (confirm("確定要刪除「(" + ti_sort + ") " + ti_desc + "」的答案選項?\n"))
		{
			update.location.replace("B00146.ashx?sid=" + msid + "&tp_sid=<%=lb_tp_sid.Text%>&tq_sid=" + tq_sid);
		}
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
<body style="white-space:normal">
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
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">試題處理</p>
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
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">總得分</td>
		<td style="text-align:left"><asp:Label ID="lb_tp_total" runat="server"></asp:Label>&nbsp;分</td>
	</tr>
	<tr id="id7">
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">最後異動時間</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_init_time" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; width:12%; background-color:#888800; color:#FFFFFF">功能選項</td>
		<td style="text-align:left; background-color:#DDDDDD; width:38%">
			<a href="javascript:madd();" class="abtn">&nbsp;新增試題&nbsp;</a>&nbsp;◆&nbsp;
			<a href="javascript:title_style()" id="title_btn" class="abtn" title="試卷標題縮放處理">&nbsp;收縮標題&nbsp;</a>
		</td>
	</tr>
	</table>

	<asp:GridView ID="gv_Ts_Question" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="tq_sid" DataSourceID="ods_Ts_Question" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何試卷題目的資料！" 
		HorizontalAlign="Center" onpageindexchanging="gv_Ts_Question_PageIndexChanged" 
		AllowSorting="True" onrowdatabound="gv_Ts_Question_RowDataBound" PageSize="5">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="20px" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="60pt" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="tq_sort" HeaderText="題號" InsertVisible="False" ReadOnly="True" SortExpression="tq_sort">
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="試題文字" SortExpression="tq_desc">
			<ItemTemplate>
				<asp:Label ID="lb_tq_desc" runat="server"></asp:Label><br />
				<asp:Literal ID="lt_tq_desc" runat="server"></asp:Literal>
			</ItemTemplate>
			<ItemStyle HorizontalAlign="Left" />
		</asp:TemplateField>
		<asp:BoundField DataField="tq_type" HeaderText="答題方式" SortExpression="tq_type">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tq_score" HeaderText="分數" SortExpression="tq_score" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="22pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="init_time" HeaderText="最後異動時間"
			SortExpression="init_time" DataFormatString="{0:yyyy/MM/dd HH:mm}">
			<HeaderStyle HorizontalAlign="Center" Width="65pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="功能" ShowHeader="False">
			<ItemTemplate>
				<p style="font-size:18pt; margin:2pt 0pt 7pt 0pt"><a href="javascript:medit(<%# Eval("tq_sid") %>)" class="abtn" title="修改試題">&nbsp;修改&nbsp;</a></p>
				<p style="font-size:18pt; margin:7pt 0pt 2pt 0pt"><a href="javascript:mdel(<%# Eval("tq_sid") %>, '<%# Eval("tq_sort") %>','<%# Eval("tq_desc") %>')" class="abtn" title="刪除試題 (包含答案選項)">&nbsp;刪除&nbsp;</a></p>
			</ItemTemplate>
			<HeaderStyle Width="32pt" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何試卷題目的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<p style="margin:10pt 0pt 0pt 0pt"><a href="javascript:location.replace('B001.aspx<%=lb_page.Text%>');" class="abtn">&nbsp;回試卷清單&nbsp;</a></p>&nbsp;

	<asp:ObjectDataSource ID="ods_Ts_Question" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Ts_Question" SelectMethod="Select_Ts_Question"
			SortParameterName="SortColumn" TypeName="ODS_Ts_Question_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="tp_sid" Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
	
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	
	<iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
	</center>
	
	<!-- Begin 隱藏視窗 -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->

	<script language="javascript" type="text/javascript">
		title_style();
	</script>
	</div>
	</form>
</body>
</html>
