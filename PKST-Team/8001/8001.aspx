<%@ Page Language="C#" AutoEventWireup="true" CodeFile="8001.aspx.cs" Inherits="_8001" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>HTML編輯器</title>
<script language="javascript" type="text/javascript">
	// 刪除詢問
	function mdel(msid, hename) {
		if (confirm("確定要刪除「" + hename + "」?\n")) {
			update.location.replace("80013.ashx?sid=" + msid);
		}
	}
</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 10pt 0pt; font-family:標楷體;">HTML編輯器</p>
	<p align="center" class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">編頁選單</p>
	<table width="98%" border="1" cellspacing="0" cellpadding="4"  bgcolor="#EFEFEF"  class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#f0f0f0">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" width="60" rowspan="2"><font color="#FFFFFF">顯示<br />條件</font></td>
		<td class="text9pt" width="60"><font color="#FFFFFF">編號</font></td>
		<td class="text9pt"><font color="#FFFFFF">標題</font></td>
		<td class="text9pt"><font color="#FFFFFF">備註說明</font></td>
		<td class="text9pt"><font color="#FFFFFF">最後異動時間範圍</font></td>
		<td class="text9pt" width="80"><font color="#FFFFFF">條件設定</font></td>
		<td class="text9pt" width="90"><font color="#FFFFFF">新增資料</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_he_sid" runat="server" Width="40pt" MaxLength="5"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_he_title" runat="server" Width="90pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_he_desc" runat="server" Width="90pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_btime" runat="server" Width="80pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分:秒 (yyyy/MM/dd HH:mm:ss)"></asp:TextBox>
			&nbsp;～&nbsp;
			<asp:TextBox ID="tb_etime" runat="server" Width="80pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分:秒 (yyyy/MM/dd) HH:mm:ss"></asp:TextBox>
		</td>
		<td><asp:Button ID="Btn_Set" runat="server" Text="設定" onclick="Btn_Set_Click" /></td>
		<td><a href="80011.aspx?md=a&pageid=<%=lb_pageid.Text %>&he_sid=<%=tb_he_sid.Text%>&he_title=<%=Server.UrlEncode(tb_he_title.Text)%>&he_desc=<%=Server.UrlEncode(tb_he_desc.Text)%>&btime=<%=Server.UrlEncode(tb_btime.Text)%>&etime=<%=Server.UrlEncode(tb_etime.Text)%>" class="abtn">&nbsp;新增資料&nbsp;</a></td>
	</tr>
	</table>
	<asp:GridView ID="gv_Html_Edit" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="he_sid" DataSourceID="ods_Html_Edit" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何網頁的資料！" 
		HorizontalAlign="Center" onpageindexchanging="gv_Html_Edit_PageIndexChanged" 
		AllowSorting="True">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="he_sid" HeaderText="編號" InsertVisible="False" ReadOnly="True" SortExpression="he_sid">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="he_title" HeaderText="標題" SortExpression="he_title" />
		<asp:BoundField DataField="he_desc" HeaderText="備註說明" SortExpression="he_desc" />
		<asp:BoundField DataField="is_attach" HeaderText="附檔" SortExpression="is_attach" />
		<asp:BoundField DataField="init_time" HeaderText="最後異動時間"
			SortExpression="init_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="100pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="功能" ShowHeader="False">
				<ItemTemplate>
					<a href="80012.ashx?sid=<%# Eval("he_sid") %>" target="_blank" class="abtn">&nbsp;預覽&nbsp;</a>&nbsp;
					<a href="80011.aspx?md=e&sid=<%# Eval("he_sid") %>&pageid=<%# gv_Html_Edit.PageIndex %>&he_sid=<%# tb_he_sid.Text %>&he_title=<%# Server.UrlEncode(tb_he_title.Text) %>&he_desc=<%# Server.UrlEncode(tb_he_desc.Text) %>&btime=<%# Server.UrlEncode(tb_btime.Text) %>&etime=<%# Server.UrlEncode(tb_etime.Text) %>" class="abtn">&nbsp;修改&nbsp;</a>&nbsp;
					<a href="javascript:mdel(<%# Eval("he_sid") %>,'<%# Eval("he_title") %>')" class="abtn">&nbsp;刪除&nbsp;</a>
				</ItemTemplate>
				<HeaderStyle Width="135px" HorizontalAlign="Center" />
				<ItemStyle Height="20px" HorizontalAlign="Center" />
			</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何網頁的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<asp:ObjectDataSource ID="ods_Html_Edit" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Html_Edit" SelectMethod="Select_Html_Edit" 
			SortParameterName="SortColumn" TypeName="ODS_Html_Edit_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="he_sid" Type="String" />
			<asp:Parameter Name="he_title" Type="String" />
			<asp:Parameter Name="he_desc" Type="String" />
			<asp:Parameter Name="btime" Type="String" />
			<asp:Parameter Name="etime" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	</center>
	</div>
	</form>
</body>
</html>
