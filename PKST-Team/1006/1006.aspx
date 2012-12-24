<%@ Page Language="C#" AutoEventWireup="true" CodeFile="1006.aspx.cs" Inherits="_1006" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>權限設定管理</title>
</head>
<body>
<form id="form1" runat="server">
	<div>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體;">權限設定管理</p>
	<table width="98%" border="1" cellspacing="0" cellpadding="4"  bgcolor="#EFEFEF"  class="text9pt" align="center" style="margin:0pt 0pt 5pt 0pt; border-color:#f0f0f0">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" width="60" rowspan="2"><font color="#FFFFFF">顯示<br />條件</font></td>
		<td class="text9pt" colspan="3"><font color="#FFFFFF">主功能</font></td>
		<td class="text9pt" colspan="3"><font color="#FFFFFF">子功能</font></td>
		<td class="text9pt" width="80"><font color="#FFFFFF">條件設定</font></td>
	</tr>
	<tr align="center">
		<td class="text9pt">代碼：<asp:TextBox ID="tb_fi_no1" runat="server" Width="10pt" MaxLength="1" class="text9pt"></asp:TextBox></td>
		<td class="text9pt">名稱：<asp:TextBox ID="tb_fi_name1" runat="server" Width="60pt" MaxLength="10" class="text9pt"></asp:TextBox></td>
		<td class="text9pt">顯示：<asp:DropDownList ID="ddl_visible1" runat="server">
				<asp:ListItem Value="-1">全部</asp:ListItem>
				<asp:ListItem Value="1">顯示</asp:ListItem>
				<asp:ListItem Value="0">隱藏</asp:ListItem>
			</asp:DropDownList>
		</td>
		<td class="text9pt">代碼：<asp:TextBox ID="tb_fi_no2" runat="server" Width="30pt" MaxLength="4" class="text9pt"></asp:TextBox></td>
		<td class="text9pt">名稱：<asp:TextBox ID="tb_fi_name2" runat="server" Width="60pt" MaxLength="10" class="text9pt"></asp:TextBox></td>
		<td class="text9pt">顯示：<asp:DropDownList ID="ddl_visible2" runat="server">
				<asp:ListItem Value="-1">全部</asp:ListItem>
				<asp:ListItem Value="1">顯示</asp:ListItem>
				<asp:ListItem Value="0">隱藏</asp:ListItem>
			</asp:DropDownList>
		</td>
		<td><asp:Button ID="Btn_Set" runat="server" Text="設定" OnClick="Btn_Set_Click" /></td>
	</tr>
	</table>
	<asp:GridView ID="gv_Func_Item" runat="server" AutoGenerateColumns="False" 
		DataSourceID="ods_Func_Item" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何權限項目的資料！" 
		HorizontalAlign="Center" onpageindexchanged="gv_Func_Item_PageIndexChanged"
		AllowSorting="True" onrowdatabound="gv_Func_Item_RowDataBound">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<Columns>
		<asp:BoundField DataField="fi_no1" HeaderText="主編號" SortExpression="f1.fi_no1" >
		<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="fi_name1" HeaderText="主功能名稱" SortExpression="fi_name1" />
		<asp:BoundField DataField="visible1" HeaderText="主功能狀態" 
			SortExpression="f1.is_visible" >
		<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="fi_no2" HeaderText="子編號" SortExpression="fi_no2" >
		<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="fi_name2" HeaderText="子功能名稱" SortExpression="fi_name2" />
		<asp:BoundField DataField="visible2" HeaderText="子功能狀態" 
			SortExpression="f2.is_visible" >
		<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="人員查詢">
			<ItemTemplate>
				<input type="button" onclick="toMember('<%# Eval("fi_no1") %>','<%# Eval("fi_no2") %>')" value="進入" class="text9pt" />
			</ItemTemplate>
			<ItemStyle HorizontalAlign="Center" />
			<HeaderStyle Width="65pt"></HeaderStyle>
		</asp:TemplateField>
	</Columns>
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<EmptyDataTemplate>沒有任何權限項目的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<asp:ObjectDataSource ID="ods_Func_Item" runat="server" EnablePaging="True" 
		SelectCountMethod="GetCount_Func_Item" SelectMethod="Select_Func_Item" 
		SortParameterName="SortColumn" TypeName="ODS_Func_Item_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="fi_no1" Type="String" />
			<asp:Parameter Name="fi_name1" Type="String" />
			<asp:Parameter Name="visible1" Type="String" />
			<asp:Parameter Name="fi_no2" Type="String" />
			<asp:Parameter Name="fi_name2" Type="String" />
			<asp:Parameter Name="visible2" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>
	</center>
	<asp:Label ID="lb_pageid" runat="server" Visible="false" Text=""></asp:Label>
	<asp:Label ID="lb_page" runat="server" Visible="false" Text=""></asp:Label>
	<script language="javascript" type="text/javascript">
		function toMember(no1, no2) {
			location.replace("10061.aspx<%=lb_pageid.Text%><%=lb_page.Text%>&no1=" + no1 + "&no2=" + no2);
		}
	</script>
	</div>
</form>
</body>
</html>