<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A002.aspx.cs" Inherits="_A002" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>���ﵲ�G�έp</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// �έp�Ϫ�
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

	<!-- Begin �л\��Ӥu�@�����A�����ϥΪ̦A���䥦���� -->
	<div id="fullwindow" style="position: absolute; top:0px; left:0px; width:1024px; height:768px; display:none">
	<table id="fulltable" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_back.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<!-- End -->
	
	<center>
	<p class="text18pt" style="margin:10pt 0pt 10pt 0pt; font-family:�з���; text-align:center">���ﵲ�G�έp</p>
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:�з���; text-align:center">����ݨ��M��</p>
	<table width="98%" border="1" cellspacing="0" cellpadding="4"  bgcolor="#EFEFEF"  class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#F0F0F0">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" width="40" rowspan="2"><font color="#FFFFFF">���<br />����</font></td>
		<td class="text9pt"><font color="#FFFFFF">�s��</font></td>
		<td class="text9pt"><font color="#FFFFFF">�D�D��r</font></td>
		<td class="text9pt"><font color="#FFFFFF">�ݨ��覡</font></td>
		<td class="text9pt"><font color="#FFFFFF">�̫�벼�ɶ��d��</font></td>
		<td class="text9pt" width="55"><font color="#FFFFFF">����]�w</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_bh_sid" runat="server" Width="15pt" MaxLength="5"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_bh_title" runat="server" Width="125pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:RadioButton ID="rb_is_check0" runat="server" Text="���" CssClass="text9pt" GroupName="rb_is_check" />
			<asp:RadioButton ID="rb_is_check1" runat="server" Text="�ƿ�" CssClass="text9pt" GroupName="rb_is_check" />
			<asp:RadioButton ID="rb_is_check_all" runat="server" Text="����" Checked="true" CssClass="text9pt" GroupName="rb_is_check" />
		</td>
		<td><asp:TextBox ID="tb_btime" runat="server" Width="65pt" MaxLength="19" ToolTip="�п�J �褸�~/��/�� ��:��:�� (yyyy/MM/dd HH:mm:ss)"></asp:TextBox>
			&nbsp;��&nbsp;
			<asp:TextBox ID="tb_etime" runat="server" Width="65pt" MaxLength="19" ToolTip="�п�J �褸�~/��/�� ��:��:�� (yyyy/MM/dd) HH:mm:ss"></asp:TextBox>
		</td>
		<td><asp:Button ID="Btn_Set" runat="server" Text="�]�w" onclick="Btn_Set_Click" /></td>
	</tr>
	</table>
	<asp:GridView ID="gv_Bt_Head" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="bh_sid" DataSourceID="ods_Bt_Head" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="�S�����󲼿�ݨ��D�D����ơI" 
		HorizontalAlign="Center" onpageindexchanging="gv_Bt_Head_PageIndexChanged" 
		AllowSorting="True" onrowdatabound="gv_Bt_Head_RowDataBound">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="bh_sid" HeaderText="�s��" InsertVisible="False" ReadOnly="True" SortExpression="bh_sid">
			<HeaderStyle HorizontalAlign="Center" Width="35px" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="bh_title" HeaderText="����D�D" SortExpression="bh_title" >
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:BoundField DataField="is_check" HeaderText="�ݨ��覡" SortExpression="is_check" />
		<asp:BoundField DataField="bh_scnt" HeaderText="��ܦ���" SortExpression="bh_scnt" DataFormatString="{0:N0}" />
		<asp:BoundField DataField="bh_acnt" HeaderText="�^������" SortExpression="bh_acnt" DataFormatString="{0:N0}" />
		<asp:BoundField DataField="bh_total" HeaderText="����`��" SortExpression="bh_total" DataFormatString="{0:N0}" />
		<asp:BoundField DataField="bh_time" HeaderText="�̫�벼�ɶ�"
			SortExpression="bh_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="80pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="is_show" HeaderText="�Ƶ{" InsertVisible="False" ReadOnly="True">
			<HeaderStyle HorizontalAlign="Center" Width="35px" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="�\��" ShowHeader="False">
				<ItemTemplate>
					<a href="javascript:mstat(<%# Eval("bh_sid") %>)" class="abtn" title="����ݨ��έp����">&nbsp;�έp���&nbsp;</a>&nbsp;
				</ItemTemplate>
				<HeaderStyle Width="80px" HorizontalAlign="Center" />
				<ItemStyle Height="20px" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>�S�����󲼿�ݨ��D�D����ơI</EmptyDataTemplate>
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
	
	<!-- Begin ���õ��� -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	</div>
	</form>
</body>
</html>
