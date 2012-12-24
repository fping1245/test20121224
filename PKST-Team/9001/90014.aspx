<%@ Page Language="C#" AutoEventWireup="true" CodeFile="90014.aspx.cs" Inherits="_90014" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>廣告信發送管理</title>
<script language="javascript" type="text/javascript">
	function add_display() {
		var tbobj = document.getElementById("tbl_add");

		if (tbobj != null) {
			if (tbobj.style.display == "none")
				tbobj.style.display = "";
			else
				tbobj.style.display = "none";
		}
	}

	function add_cancel() {
		var tbobj = document.getElementById("tbl_add");
		var emobj = document.getElementById("tb_email");
		var naobj = document.getElementById("tb_name");

		if (tbobj != null)
			tbobj.style.display = "none";

		if (emobj != null)
			emobj.value = "";

		if (naobj != null)
			naobj.value = "";
	}
</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 10pt 0pt; font-family:標楷體;">廣告信發送管理</p>
	<p align="center" class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">廣告信會員信箱</p>
	<table width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#f0f0f0; background-color:#EFEFEF">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" width="40" rowspan="2"><font color="#FFFFFF">顯示<br />條件</font></td>
		<td class="text9pt"><font color="#FFFFFF">編號</font></td>
		<td class="text9pt"><font color="#FFFFFF">電子郵件信箱</font></td>
		<td class="text9pt"><font color="#FFFFFF">會員姓名</font></td>
		<td class="text9pt"><font color="#FFFFFF">最後最動時間範圍</font></td>
		<td class="text9pt" width="70"><font color="#FFFFFF">條件設定</font></td>
		<td class="text9pt" width="80"><font color="#FFFFFF">新增資料</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_adb_sid" runat="server" Width="20pt" MaxLength="5"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_adb_email" runat="server" Width="80pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_adb_name" runat="server" Width="60pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_ibtime" runat="server" Width="75pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分:秒 (yyyy/MM/dd HH:mm:ss)"></asp:TextBox>&nbsp;～&nbsp;
			<asp:TextBox ID="tb_ietime" runat="server" Width="75pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分:秒 (yyyy/MM/dd) HH:mm:ss"></asp:TextBox>
		</td>
		<td><asp:Button ID="Btn_Set" runat="server" Text="設定" onclick="Btn_Set_Click" /></td>
		<td><a href="javascript:add_display()" class="abtn">&nbsp;新增資料&nbsp;</a></td>
	</tr>
	</table>

	<table id="tbl_add" width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#f0f0f0; background-color:#F7F7DE; display:none">
	<tr align="left">
		<td class="text9pt" style="text-align:center; width:120px; background-color:#99CCFF; color:#FFFFFF">新增資料</td>
		<td class="text9pt" style="text-align:center; width:100px; background-color:#99FF99">電子郵件信箱</td>
		<td class="text9pt"><asp:TextBox ID="tb_email" runat="server" Width="180px" MaxLength="100"></asp:TextBox></td>
		<td class="text9pt" style="text-align:center; width:100px; background-color:#99FF99">會員姓名</td>
		<td class="text9pt"><asp:TextBox ID="tb_name" runat="server" Width="120px" MaxLength="20"></asp:TextBox></td>
		<td class="text9pt" style="text-align:center; width:125px; background-color:#99FF99">
			<asp:Button ID="tb_save" runat="server" CssClass="text9pt" Text="確定存檔" onclick="tb_save_Click" />
			<input type="button" value="取消" class="text9pt" onclick="add_cancel()" />
		</td>
	</tr>
	</table>

	<asp:GridView ID="gv_Ad_Member" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="adb_sid" DataSourceID="ods_Ad_Member" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何會員的資料！" 
		HorizontalAlign="Center" onpageindexchanging="gv_Ad_Member_PageIndexChanged" 
		AllowSorting="True" onrowdeleted="gv_Ad_Member_RowDeleted">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="adb_sid" HeaderText="編號" InsertVisible="False" ReadOnly="True" SortExpression="adb_sid">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="adb_email" HeaderText="電子郵件信箱" SortExpression="adb_email" />
		<asp:BoundField DataField="adb_name" HeaderText="會員姓名" SortExpression="adb_name" />
		<asp:BoundField DataField="init_time" HeaderText="最後異動時間"
			SortExpression="init_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" ReadOnly>
			<HeaderStyle HorizontalAlign="Center" Width="100pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="編輯" ShowHeader="False">
			<ItemTemplate>
				<asp:Button ID="Button1" runat="server" CausesValidation="False" 
					CommandName="Edit" Text="修改"></asp:Button>
				&nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
					CommandName="Delete" onclientclick="return confirm('確定要刪除此筆資料？')" Text="刪除"></asp:Button>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:Button ID="Button1" runat="server" CausesValidation="True" 
					CommandName="Update" onclientclick="return confirm('確定要更新此筆資料？')" Text="更新"></asp:Button>
				&nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
					CommandName="Cancel" Text="取消"></asp:Button>
			</EditItemTemplate>
			<HeaderStyle Width="90px" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何會員的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<p style=" margin:10px 0px 10px 0px; text-align:center"><a href="9001.aspx<%=lb_page.Text %>" class="abtn">&nbsp;回上一頁&nbsp;</a></p>&nbsp;
	
	<asp:ObjectDataSource ID="ods_Ad_Member" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Ad_Member" SelectMethod="Select_Ad_Member"
			SortParameterName="SortColumn" TypeName="ODS_Ad_Member_DataAccess" 
			DeleteMethod="Delete_Ad_Member" InsertMethod="Insert_Ad_Member" 
			UpdateMethod="Update_Ad_Member" ondeleted="ods_Ad_Member_Deleted" 
			oninserted="ods_Ad_Member_Inserted" onupdated="ods_Ad_Member_Updated" 
			oninserting="ods_Ad_Member_Inserting" onupdating="ods_Ad_Member_Updating">
		<DeleteParameters>
			<asp:Parameter Name="adb_sid" Type="Int32" />
		</DeleteParameters>
		<UpdateParameters>
			<asp:Parameter Name="adb_sid" Type="Int32" />
			<asp:Parameter Name="adb_name" Type="String" />
			<asp:Parameter Name="adb_email" Type="String" />
		</UpdateParameters>
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="adb_sid" Type="String" />
			<asp:Parameter Name="adb_name" Type="String" />
			<asp:Parameter Name="adb_email" Type="String" />
			<asp:Parameter Name="btime" Type="String" />
			<asp:Parameter Name="etime" Type="String" />
		</SelectParameters>
		<InsertParameters>
			<asp:Parameter Name="adb_name" Type="String" />
			<asp:Parameter Name="adb_email" Type="String" />
		</InsertParameters>
	</asp:ObjectDataSource>
	<asp:Label ID="lb_pageid1" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
	</center>
	</div>
	</form>
</body>
</html>
