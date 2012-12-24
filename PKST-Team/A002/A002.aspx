<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A002.aspx.cs" Inherits="_A002" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>票選結果統計</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// 統計圖表
	function mstat(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();

		show_win("A0021.aspx?sid=" + msid + "&timestamp=" + timestamp, 700, 400);
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
	<p class="text18pt" style="margin:10pt 0pt 10pt 0pt; font-family:標楷體; text-align:center">票選結果統計</p>
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">票選問卷清單</p>
	<table width="98%" border="1" cellspacing="0" cellpadding="4"  bgcolor="#EFEFEF"  class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#F0F0F0">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" width="40" rowspan="2"><font color="#FFFFFF">顯示<br />條件</font></td>
		<td class="text9pt"><font color="#FFFFFF">編號</font></td>
		<td class="text9pt"><font color="#FFFFFF">主題文字</font></td>
		<td class="text9pt"><font color="#FFFFFF">問卷方式</font></td>
		<td class="text9pt"><font color="#FFFFFF">最後投票時間範圍</font></td>
		<td class="text9pt" width="55"><font color="#FFFFFF">條件設定</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_bh_sid" runat="server" Width="15pt" MaxLength="5"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_bh_title" runat="server" Width="125pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:RadioButton ID="rb_is_check0" runat="server" Text="單選" CssClass="text9pt" GroupName="rb_is_check" />
			<asp:RadioButton ID="rb_is_check1" runat="server" Text="複選" CssClass="text9pt" GroupName="rb_is_check" />
			<asp:RadioButton ID="rb_is_check_all" runat="server" Text="全部" Checked="true" CssClass="text9pt" GroupName="rb_is_check" />
		</td>
		<td><asp:TextBox ID="tb_btime" runat="server" Width="65pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分:秒 (yyyy/MM/dd HH:mm:ss)"></asp:TextBox>
			&nbsp;～&nbsp;
			<asp:TextBox ID="tb_etime" runat="server" Width="65pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分:秒 (yyyy/MM/dd) HH:mm:ss"></asp:TextBox>
		</td>
		<td><asp:Button ID="Btn_Set" runat="server" Text="設定" onclick="Btn_Set_Click" /></td>
	</tr>
	</table>
	<asp:GridView ID="gv_Bt_Head" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="bh_sid" DataSourceID="ods_Bt_Head" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何票選問卷主題的資料！" 
		HorizontalAlign="Center" onpageindexchanging="gv_Bt_Head_PageIndexChanged" 
		AllowSorting="True" onrowdatabound="gv_Bt_Head_RowDataBound">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="bh_sid" HeaderText="編號" InsertVisible="False" ReadOnly="True" SortExpression="bh_sid">
			<HeaderStyle HorizontalAlign="Center" Width="35px" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="bh_title" HeaderText="票選主題" SortExpression="bh_title" >
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:BoundField DataField="is_check" HeaderText="問卷方式" SortExpression="is_check" />
		<asp:BoundField DataField="bh_scnt" HeaderText="顯示次數" SortExpression="bh_scnt" DataFormatString="{0:N0}" />
		<asp:BoundField DataField="bh_acnt" HeaderText="回應次數" SortExpression="bh_acnt" DataFormatString="{0:N0}" />
		<asp:BoundField DataField="bh_total" HeaderText="圈選總數" SortExpression="bh_total" DataFormatString="{0:N0}" />
		<asp:BoundField DataField="bh_time" HeaderText="最後投票時間"
			SortExpression="bh_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="80pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="is_show" HeaderText="排程" InsertVisible="False" ReadOnly="True">
			<HeaderStyle HorizontalAlign="Center" Width="35px" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="功能" ShowHeader="False">
				<ItemTemplate>
					<a href="javascript:mstat(<%# Eval("bh_sid") %>)" class="abtn" title="票選問卷統計明細">&nbsp;統計資料&nbsp;</a>&nbsp;
				</ItemTemplate>
				<HeaderStyle Width="80px" HorizontalAlign="Center" />
				<ItemStyle Height="20px" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何票選問卷主題的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	
	<asp:ObjectDataSource ID="ods_Bt_Head" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Bt_Head" SelectMethod="Select_Bt_Head"
			SortParameterName="SortColumn" TypeName="ODS_Bt_Head_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="bh_sid" Type="String" />
			<asp:Parameter Name="bh_title" Type="String" />
			<asp:Parameter Name="is_check" Type="String" />
			<asp:Parameter Name="btime" Type="String" />
			<asp:Parameter Name="etime" Type="String" />
			<asp:Parameter Name="is_show" Type="String" DefaultValue="" />
			<asp:Parameter Name="now_use" Type="String" DefaultValue="" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
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
