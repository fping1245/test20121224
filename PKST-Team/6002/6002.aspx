<%@ Page Language="C#" AutoEventWireup="true" CodeFile="6002.aspx.cs" Inherits="_6002" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>通訊錄管理</title>
</head>
<body>
<form id="form1" runat="server">
<div>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體;">通訊錄管理</p>
	<table width="98%" border="1" cellspacing="0" cellpadding="4"  bgcolor="#EFEFEF"  class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#f0f0f0">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" width="60" rowspan="2"><font color="#FFFFFF">顯示<br />條件</font></td>
		<td class="text9pt"><font color="#FFFFFF">姓名</font></td>
		<td class="text9pt"><font color="#FFFFFF">暱稱</font></td>
		<td class="text9pt"><font color="#FFFFFF">工作單位</font></td>
		<td class="text9pt"><font color="#FFFFFF">群組</font></td>
		<td class="text9pt"><font color="#FFFFFF">群組屬性</font></td>
		<td class="text9pt" width="70"><font color="#FFFFFF">條件設定</font></td>
		<td class="text9pt" width="90"><font color="#FFFFFF">新增資料</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_ab_name" runat="server" Width="70pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_ab_nike" runat="server" Width="70pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_ab_company" runat="server" Width="80pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_ag_name" runat="server" Width="80pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_ag_attrib" runat="server" Width="80pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:Button ID="Btn_Set" runat="server" Text="設定" onclick="Btn_Set_Click" /></td>
		<td><a href="60021_add.aspx?pageid=<%# gv_As_Book.PageIndex %>&ab_name=<%# Server.UrlEncode(tb_ab_name.Text) %>&ab_nike=<%# Server.UrlEncode(tb_ab_nike.Text) %>&ab_company=<%# Server.UrlEncode(tb_ab_company.Text) %>&ag_name=<%# Server.UrlEncode(tb_ag_name.Text) %>&ag_attrib=<%# Server.UrlEncode(tb_ag_attrib.Text) %>" class="abtn">&nbsp;新增資料&nbsp;</a></td>
	</tr>
	</table>
	<asp:GridView ID="gv_As_Book" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="ab_sid" DataSourceID="ods_As_Book" AllowPaging="True"
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何通訊錄資料！" 
		HorizontalAlign="Center" onpageindexchanging="gv_As_Book_PageIndexChanged" 
		AllowSorting="True">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="ab_name" HeaderText="姓名" SortExpression="ab_name" />
		<asp:BoundField DataField="ab_nike" HeaderText="暱稱" SortExpression="ab_nike" />
		<asp:BoundField DataField="ab_company" HeaderText="工作單位" SortExpression="ab_company" />
		<asp:BoundField DataField="ab_mobil" HeaderText="行動電話" SortExpression="ab_mobil" />
		<asp:BoundField DataField="ab_email" HeaderText="電子信箱" SortExpression="ab_email" />
		<asp:BoundField DataField="ag_name" HeaderText="群組" SortExpression="ag_name" />
		<asp:BoundField DataField="ag_attrib" HeaderText="群組屬性" SortExpression="ag_attrib" />
		<asp:BoundField DataField="ab_sid" HeaderText="編號" InsertVisible="False" ReadOnly="True" SortExpression="ab_sid">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="內容" ShowHeader="False">
				<ItemTemplate>
					<a href="60021.aspx?sid=<%# Eval("ab_sid") %>&pageid=<%# gv_As_Book.PageIndex %>&ab_name=<%# Server.UrlEncode(tb_ab_name.Text) %>&ab_nike=<%# Server.UrlEncode(tb_ab_nike.Text) %>&ab_company=<%# Server.UrlEncode(tb_ab_company.Text) %>&ag_name=<%# Server.UrlEncode(tb_ag_name.Text) %>&ag_attrib=<%# Server.UrlEncode(tb_ag_attrib.Text) %>&sort=<%# gv_As_Book.SortExpression%>&row=<%# Eval("rownum") %>" class="abtn">&nbsp;內容&nbsp;</a>
				</ItemTemplate>
				<HeaderStyle Width="40pt" HorizontalAlign="Center" />
				<ItemStyle HorizontalAlign="Center" />
			</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何通訊錄資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<asp:ObjectDataSource ID="ods_As_Book" runat="server" EnablePaging="True" 
		SelectCountMethod="GetCount_As_Book" SelectMethod="Select_As_Book" 
		SortParameterName="SortColumn" TypeName="ODS_As_Book_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="mg_sid" Type="Int32" />
			<asp:Parameter Name="ab_name" Type="String" />
			<asp:Parameter Name="ab_nike" Type="String" />
			<asp:Parameter Name="ab_company" Type="String" />
			<asp:Parameter Name="ag_name" Type="String" />
			<asp:Parameter Name="ag_attrib" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	</center>
</div>
</form>
</body>
</html>