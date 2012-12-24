<%@ Page Language="C#" AutoEventWireup="true" CodeFile="F003.aspx.cs" Inherits="_F003" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>產生 Excel 檔案</title>
<script language="javascript" type="text/javascript">
	// 產生空白試卷 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function mprn(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();

		update.location.href = "F0031.ashx?sid=" + msid + "&timestamp=" + timestamp;
	}
</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">產生 Excel 檔案 (OleDb)</p>
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">▁▂▃▄▅▆▇&nbsp;以線上考試成績分佈為例&nbsp;▇▆▅▄▃▂▁</p>
	<table width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="background-color:#EFEFEF; margin:0pt 0pt 5pt 0pt; border-color:#F0F0F0">
	<tr align="center" style="background-color:#99CCFF">
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
			<HeaderStyle HorizontalAlign="Center" Width="80pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="e_time" HeaderText="截止進入時間" SortExpression="e_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="80pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_question" HeaderText="試題總數" SortExpression="tp_question" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_score" HeaderText="總分" SortExpression="tp_score" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="功能" ShowHeader="False">
				<ItemTemplate>
					<a href="javascript:mprn(<%# Eval("tp_sid") %>)" class="abtn" title="產生 Excel 格式的成績分佈統計圖表">&nbsp;成績分佈統計&nbsp;</a>&nbsp;
				</ItemTemplate>
				<HeaderStyle Width="82pt" HorizontalAlign="Center" />
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
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_is_show" runat="server" Text="" Visible="false"></asp:Label>
	<iframe name="update" id="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
	</center>
	</div>
	</form>
</body>
</html>
