<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A0031.aspx.cs" Inherits="_A0031" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�����ƺ޲z</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// �ק���
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();

		show_win("A00311.aspx?sid=" + msid + "&timestamp=" + timestamp, 500, 250);
	}

	// �R���߰�
	function mdel(msid, mbh_sid, mtitle)
	{
		if (confirm("�T�w�n�R���u" + mtitle + "�v���Ƶ{���?\n"))
		{
			update.location.replace("A00312.ashx?sid=" + msid + "&bh_sid=" + mbh_sid);
		}
	}

	// ���s�s�ƶ���
	function resort()
	{
		update.location.replace("A00313.ashx");
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
	<table border="2" cellpadding="4" cellspacing="2" style="width:680px; height:300px; background-color:#EFEFEF">
	<tr><td align="center" valign="top">
			<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:�з���; text-align:center">�ݨ��Ƶ{�B�z</p>
			<asp:GridView ID="gv_Bt_Schedule" runat="server" AutoGenerateColumns="False" 
				DataKeyNames="bs_sid" DataSourceID="ods_Bt_Schedule" AllowPaging="True" 
				BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
				CellPadding="4" Width="660px" EmptyDataText="�S�����󲼿�Ƶ{����ơI" 
				HorizontalAlign="Center" onpageindexchanging="gv_Bt_Schedule_PageIndexChanged" 
				AllowSorting="True" onrowdatabound="gv_Bt_Schedule_RowDataBound">
			<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
			<RowStyle BackColor="#F7F7DE" />
			<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
			<Columns>
				<asp:BoundField DataField="is_show" HeaderText="���" InsertVisible="False" ReadOnly="True">
					<HeaderStyle HorizontalAlign="Center" Width="30px" />
					<ItemStyle HorizontalAlign="Center" />
				</asp:BoundField>
				<asp:BoundField DataField="now_use" HeaderText="����" InsertVisible="False" ReadOnly="True" SortExpression="now_use">
					<HeaderStyle HorizontalAlign="Center" Width="30px" />
					<ItemStyle HorizontalAlign="Center" />
				</asp:BoundField>
				<asp:BoundField DataField="bs_sort" HeaderText="����" SortExpression="bs_sort">
					<HeaderStyle HorizontalAlign="Center" Width="30px" />
					<ItemStyle HorizontalAlign="Center" />
				</asp:BoundField>
				<asp:BoundField DataField="bh_title" HeaderText="����D�D" SortExpression="h.bh_title">
					<ItemStyle HorizontalAlign="Left" />
				</asp:BoundField>
				<asp:BoundField DataField="s_time" HeaderText="�}�l�ɶ�"
					SortExpression="s_time" DataFormatString="{0:yyyy/MM/dd HH:mm}">
					<HeaderStyle HorizontalAlign="Center" Width="65pt" />
					<ItemStyle HorizontalAlign="Center" />
				</asp:BoundField>
				<asp:BoundField DataField="e_time" HeaderText="�����ɶ�"
					SortExpression="e_time" DataFormatString="{0:yyyy/MM/dd HH:mm}">
					<HeaderStyle HorizontalAlign="Center" Width="65pt" />
					<ItemStyle HorizontalAlign="Center" />
				</asp:BoundField>
				<asp:BoundField DataField="init_time" HeaderText="�̫Ყ��"
					SortExpression="init_time" DataFormatString="{0:yyyy/MM/dd}">
					<HeaderStyle HorizontalAlign="Center" Width="45pt" />
					<ItemStyle HorizontalAlign="Center" />
				</asp:BoundField>
				<asp:TemplateField HeaderText="�\��" ShowHeader="False">
					<ItemTemplate>
						<a href="javascript:medit(<%# Eval("bs_sid") %>)" class="abtn" title="����Ƶ{�ק�">&nbsp;�ק�&nbsp;</a>&nbsp;
						<a href="javascript:mdel(<%# Eval("bs_sid") %>,<%# Eval("bh_sid") %>,'<%# Eval("bh_title") %>')" class="abtn" title="�R���Ƶ{">&nbsp;�R��&nbsp;</a>
					</ItemTemplate>
					<HeaderStyle Width="90px" HorizontalAlign="Center" />
					<ItemStyle Height="20px" HorizontalAlign="Center" />
				</asp:TemplateField>
			</Columns>
			<EmptyDataTemplate>�S�����󲼿�Ƶ{����ơI</EmptyDataTemplate>
			<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
			<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
			<AlternatingRowStyle BackColor="White" />
			</asp:GridView>
			<p style="text-align:left; margin:5pt 0pt 0pt 0pt">�� �ХѥD�D�M��N�D�D�[�J�Ƶ{�C</p>
			<p style="text-align:center; margin:10pt 0pt 0pt 0pt">
				<a href="javascript:resort();" class="abtn" title="���s�s�ƶ���">&nbsp;�Ƨ�&nbsp;</a>
				<a href="javascript:parent.location.reload(true);" class="abtn">&nbsp;����&nbsp;</a>
			</p>&nbsp;
			
			<iframe name="update" id="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
		</td>
	</tr>
	</table>
	
	<asp:ObjectDataSource ID="ods_Bt_Schedule" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Bt_Schedule" SelectMethod="Select_Bt_Schedule"
			SortParameterName="SortColumn" TypeName="ODS_Bt_Schedule_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="now_use" Type="String" DefaultValue="" />
			<asp:Parameter Name="is_show" Type="String" DefaultValue="" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	</center>
	
	<!-- Begin ���õ��� -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
