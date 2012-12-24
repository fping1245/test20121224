<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A0035.aspx.cs" Inherits="_A0035" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>票選資料管理</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// 刪除詢問
	function mdel(msid, mdesc)
	{
		if (confirm("確定要刪除編號「" + mdesc + "」的資料?\n"))
		{
			update.location.replace("A00353.ashx?sid=" + msid + "&bh_sid=<%=lb_bh_sid.Text%>");
		}
	}

	// 修改資料
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("A00352.aspx?sid=" + msid + "&bh_sid=<%=lb_bh_sid.Text%>&timestamp=" + timestamp, 550, 200);
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
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">票選資料管理</p>
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">票選問卷項目處理</p>
	<table border="1" cellpadding="4" cellspacing="0" style="width:98%; background-color:#F7F7DE; margin-bottom:10px">
	<tr><td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">主題編號</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_bh_sid" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">票選方式</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_is_check" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">票選標題</td>
		<td style="text-align:left" colspan="3"><asp:Label ID="lb_bh_title" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">內容說明</td>
		<td style="text-align:left" colspan="3"><asp:Label ID="lb_bh_topic" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">顯示次數</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_scnt" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">回應次數</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_acnt" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">圈選總數</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_total" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">最後投票時間</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_time" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">最後異動時間</td>
		<td style="text-align:left"><asp:Label ID="lb_init_time" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; background-color:#888800; color:#FFFFFF">功能選項</td>
		<td style="text-align:left; background-color:#DDDDDD">
			<a href="javascript:show_win('A00351.aspx?bh_sid=<%=lb_bh_sid.Text%>', 550, 200);" class="abtn">&nbsp;新增項目&nbsp;</a>&nbsp;&nbsp;
		</td>
	</tr>
	</table>

	<asp:GridView ID="gv_Bt_Item" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="bi_sid" DataSourceID="ods_Bt_Item" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何票選問卷項目的資料！" 
		HorizontalAlign="Center" onpageindexchanging="gv_Bt_Item_PageIndexChanged" 
		AllowSorting="True">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="20px" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="bi_sort" HeaderText="順序" InsertVisible="False" ReadOnly="True" SortExpression="bi_sort">
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="bi_desc" HeaderText="項目文字" SortExpression="bi_desc" />
		<asp:BoundField DataField="bi_total" HeaderText="投票總數" SortExpression="bi_total" DataFormatString="{0:N0}" />
		<asp:BoundField DataField="bi_time" HeaderText="最後投票時間"
			SortExpression="bi_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="76pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="init_time" HeaderText="最後異動時間"
			SortExpression="init_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="76pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="功能" ShowHeader="False">
			<ItemTemplate>
				<a href="javascript:medit(<%# Eval("bi_sid") %>)" class="abtn">&nbsp;修改&nbsp;</a>&nbsp;
				<a href="javascript:mdel(<%# Eval("bi_sid") %>,'<%# Eval("bi_desc") %>')" class="abtn">&nbsp;刪除&nbsp;</a>
			</ItemTemplate>
			<HeaderStyle Width="90px" HorizontalAlign="Center" />
			<ItemStyle Height="20px" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何票選問卷項目的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<p style="margin:10pt 0pt 0pt 0pt"><a href="javascript:parent.location.reload(true);" class="abtn">&nbsp;回問卷清單&nbsp;</a></p>&nbsp;

	<asp:ObjectDataSource ID="ods_Bt_Item" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Bt_Item" SelectMethod="Select_Bt_Item"
			SortParameterName="SortColumn" TypeName="ODS_Bt_Item_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="bh_sid" Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	
	<iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
	</center>
	
	<!-- Begin 隱藏視窗 -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->

	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
