<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B00311.aspx.cs" Inherits="_B00311" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>線上考試(限定身份)</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	var title_var = 1; 	// 1:展開 0:縮小

	// 考生開始答題 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function mans(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();

		show_win("B003111.aspx?tq_sort=" + msid + "&tu_sid=<%=lb_tu_sid.Text%>&tp_sid=<%=lb_tp_sid.Text%>&timestamp=" + timestamp, 680, 500);
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
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">線上考試(限定身份)</p>
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">試題清單</p>
	<table border="1" cellpadding="4" cellspacing="0" style="width:95%; background-color:#F7F7DE; margin-bottom:10px">
	<tr id="id1">
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">考生試卷編號</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_tu_sid" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">考生IP</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_tu_ip" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">考生姓名</td>
		<td style="text-align:left"><asp:Label ID="lb_tu_name" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">考生學號</td>
		<td style="text-align:left"><asp:Label ID="lb_tu_no" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr id="id2">
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">作答開始時間</td>
		<td style="text-align:left"><asp:Label ID="lb_b_time" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">作答結束時間</td>
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
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">試卷總分</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_tp_score" runat="server"></asp:Label>&nbsp;分</td>
		<td style="text-align:center; width:12%; background-color:#888800; color:#FFFFFF">功能選項</td>
		<td style="text-align:left; background-color:#DDDDDD; width:38%">
			<a href="javascript:title_style()" id="title_btn" class="abtn" title="試卷標題縮放處理">&nbsp;收縮標題&nbsp;</a>
		</td>
	</tr>
	</table>

	<asp:GridView ID="gv_Ts_QU" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="tq_sid" DataSourceID="ods_Ts_QU" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="95%" EmptyDataText="沒有任何試卷題目的資料！" 
		HorizontalAlign="Center" onpageindexchanging="gv_Ts_QU_PageIndexChanged" 
		AllowSorting="True" onrowdatabound="gv_Ts_QU_RowDataBound" PageSize="10">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="20px" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="60pt" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="tq_sort" HeaderText="題號" InsertVisible="False" ReadOnly="True" SortExpression="q.tq_sort">
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tq_score" HeaderText="分數" SortExpression="q.tq_score" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="22pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="試題文字" SortExpression="q.tq_desc">
			<ItemTemplate>
				<asp:Label ID="lb_tq_desc" runat="server" Text='<%# Bind("tq_desc") %>'></asp:Label><br />
			</ItemTemplate>
			<ItemStyle HorizontalAlign="Left" />
		</asp:TemplateField>
		<asp:BoundField DataField="tq_type" HeaderText="答題方式" SortExpression="q.tq_type">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="init_time" HeaderText="答題時間"
			SortExpression="u.init_time" DataFormatString="{0:yyyy/MM/dd HH:mm}">
			<HeaderStyle HorizontalAlign="Center" Width="65pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="作答" SortExpression="u.init_time">
			<ItemTemplate>
				<asp:Label ID="lb_is_ans" runat="server" Text="－"></asp:Label>
			</ItemTemplate>
			<ItemStyle HorizontalAlign="Center" />
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
		</asp:TemplateField>
		<asp:TemplateField HeaderText="回答題目">
			<ItemTemplate>
				<a href ="javascript:mans(<%# Eval("tq_sort") %>)" class="abtn">&nbsp;回答試題&nbsp;</a>
			</ItemTemplate>
			<ItemStyle HorizontalAlign="Center" />
			<HeaderStyle HorizontalAlign="Center" Width="55pt" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何試卷題目的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<p style="margin:10pt 0pt 0pt 0pt"><a href="B003.aspx" class="abtn">&nbsp;結束考試&nbsp;</a></p>&nbsp;

	<asp:ObjectDataSource ID="ods_Ts_QU" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Ts_QU" SelectMethod="Select_Ts_QU"
			SortParameterName="SortColumn" TypeName="ODS_Ts_QU_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="tu_sid" Type="Int32" />
			<asp:Parameter Name="tp_sid" Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:Label ID="lb_tp_sid" runat="server" Visible="false"></asp:Label>
	<asp:Label ID="lb_pageid2" runat="server" Visible="false"></asp:Label>

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
