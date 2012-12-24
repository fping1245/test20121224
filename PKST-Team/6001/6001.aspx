<%@ Page Language="C#" AutoEventWireup="true" CodeFile="6001.aspx.cs" Inherits="_6001" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>連絡人群組管理</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 0pt 0pt; font-family:標楷體;">連絡人群組管理</p>
	<table width="98%" border="0" cellpadding="0" cellspacing="0" class="text14pt" style="margin:0pt 0pt 2pt 0pt">
	<tr><td align="right" style="height:20pt"><a href="6001_add.aspx?pageid=<%=lb_pageid.Text %>" class="abtn">&nbsp;新增資料&nbsp;</a></td></tr>
	</table>
	<asp:GridView ID="gv_As_Group" runat="server"
		AutoGenerateColumns="False" 
		DataKeyNames="ag_sid" DataSourceID="ods_As_Group" AllowPaging="True"
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有連絡人群組的資料！" 
		HorizontalAlign="Center" onpageindexchanging="gv_As_Group_PageIndexChanged" 
		AllowSorting="True">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="rownum" HeaderText="順序" InsertVisible="False" ReadOnly="True">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="ag_name" HeaderText="群組名稱" SortExpression="ag_name" />
		<asp:BoundField DataField="ag_attrib" HeaderText="群組屬性" SortExpression="ag_attrib" />
		<asp:BoundField DataField="ag_desc" HeaderText="備註說明" SortExpression="ag_desc" />
		<asp:BoundField DataField="ag_sid" HeaderText="編號" InsertVisible="False" ReadOnly="True" SortExpression="ag_sid">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="init_time" HeaderText="最後修改時間"
			SortExpression="init_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="100pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="異動" ShowHeader="False">
				<ItemTemplate>
					<a href="6001_edit.aspx?sid=<%# Eval("ag_sid") %>&pageid=<%# gv_As_Group.PageIndex %>" class="abtn">&nbsp;修改&nbsp;</a>&nbsp;
					<a href="javascript:mdel(<%# Eval("ag_sid") %>,'<%# Eval("ag_name").ToString().Trim() %>')" class="abtn">&nbsp;刪除&nbsp;</a>
				</ItemTemplate>
				<HeaderStyle Width="90pt" HorizontalAlign="Center" />
				<ItemStyle HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有連絡人群組的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	</center>
	<asp:ObjectDataSource 
		ID="ods_As_Group" runat="server" SelectCountMethod="GetCount_As_Group" 
		SelectMethod="Select_As_Group" SortParameterName="SortColumn" 
		TypeName="ODS_As_Group_DataReader" EnablePaging="True">
	<SelectParameters>
		<asp:Parameter Name="SortColumn" Type="String" />
		<asp:Parameter Name="startRowIndex" Type="Int32" />
		<asp:Parameter Name="maximumRows" Type="Int32" />
		<asp:Parameter Name="mg_sid" Type="Int32" />
	</SelectParameters>
	</asp:ObjectDataSource>
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
    <iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
    <script language="javascript" type="text/javascript">
    	function mdel(sid, aname) {
    		if (confirm("確定要刪除「" + aname + "」？"))
    		{
    			update.location.replace("6001_del.ashx?sid=" + sid);
    		}
    	}
    </script>
	</div>
	</form>
</body>
</html>
