<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B003.aspx.cs" Inherits="_B003" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>線上考試(自由參加)</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// 考生開始答題 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function mans(msid, mtitle)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();

		show_win("B0031.aspx?sid=" + msid + "&tp_title=" + escape(mtitle) + "&timestamp=" + timestamp, 500, 300);
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
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">線上考試(限定身份)</p>
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">▁▂▃▄▅▆▇&nbsp;請選擇要參加的考試&nbsp;▇▆▅▄▃▂▁</p>
	<table width="98%" border="1" cellspacing="0" cellpadding="4"  bgcolor="#EFEFEF"  class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#F0F0F0">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" style="width:30pt" rowspan="2"><font color="#FFFFFF">顯示<br />條件</font></td>
		<td class="text9pt" style="width:40pt"><font color="#FFFFFF">編號</font></td>
		<td class="text9pt"><font color="#FFFFFF">試卷標題</font></td>
		<td class="text9pt" style="width:48pt"><font color="#FFFFFF">條件設定</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_tp_sid" runat="server" Width="30pt" MaxLength="5"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_tp_title" runat="server" Width="98%" MaxLength="50"></asp:TextBox></td>
		<td><asp:Button ID="btn_Set" runat="server" Text="設定" onclick="btn_Set_Click" /></td>
	</tr>
	</table>
	<asp:GridView ID="gv_Ts_Paper" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="tp_sid" DataSourceID="ods_Ts_Paper" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何試卷的資料！" 
		HorizontalAlign="Center" onpageindexchanging="gv_Ts_Paper_PageIndexChanged" 
		AllowSorting="True">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="25pt" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="tp_sid" HeaderText="編號" InsertVisible="False" ReadOnly="True" SortExpression="tp_sid">
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_title" HeaderText="試卷主題" SortExpression="tp_title" >
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:BoundField DataField="b_time" HeaderText="開放進入時間" SortExpression="b_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="70pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="e_time" HeaderText="截止進入時間" SortExpression="e_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="70pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_question" HeaderText="試題總數" SortExpression="tp_question" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_member" HeaderText="參加人數" SortExpression="tp_member" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_score" HeaderText="總分" SortExpression="tp_score" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="功能" ShowHeader="False">
				<ItemTemplate>
					<a href="javascript:mans(<%# Eval("tp_sid") %>,'<%# Eval("tp_title") %>')" class="abtn" title="參加考試，開始答題">&nbsp;開始答題&nbsp;</a>&nbsp;
				</ItemTemplate>
				<HeaderStyle Width="60pt" HorizontalAlign="Center" />
				<ItemStyle Height="18pt" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何試卷的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>

	<asp:ObjectDataSource ID="ods_Ts_Paper" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Ts_Paper" SelectMethod="Select_Ts_Paper"
			SortParameterName="SortColumn" TypeName="ODS_Ts_Paper_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="tp_sid" Type="String" />
			<asp:Parameter Name="tp_title" Type="String" />
			<asp:Parameter Name="is_show" Type="String" />
			<asp:Parameter Name="btime" Type="String" />
			<asp:Parameter Name="etime" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<p class="text9pt" style="margin:5pt 0pt 5pt 0pt; text-align:left; width:98%">※ 在考試時間範圍內，考生可重覆登入，修改原來的答案。</p>
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_is_show" runat="server" Text="" Visible="false"></asp:Label>
	<iframe name="update" id="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
	</center>
	
	<!-- Begin 隱藏視窗 -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	</div>
	</form>
</body>
</html>
