<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B002111.aspx.cs" Inherits="_B002111" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�u�W�Ҹ�(�ۥѰѥ[)</title>
<script language="javascript" type="text/javascript">
	var title_var = 1; 	// 1:�i�} 0:�Y�p

	// �Y��ը����Y
	function title_style()
	{
		var id1obj = document.getElementById("id1");
		var id2obj = document.getElementById("id2");
		var id4obj = document.getElementById("id4");
		var id5obj = document.getElementById("id5");
		var id6obj = document.getElementById("id6");
	
		if (title_var == 1)
		{
			// �Y�p�B�z
			title_var = 0;
			title_btn.innerHTML = "&nbsp;�i�}���D&nbsp;";

			if (id1obj != null)
				id1obj.style.display = "none";

			if (id2obj != null)
				id2obj.style.display = "none";

			if (id4obj != null)
				id4obj.style.display = "none";
		}
		else
		{
			// �i�}�B�z
			title_var = 1;
			title_btn.innerHTML = "&nbsp;���Y���D&nbsp;";

			if (id1obj != null)
				id1obj.style.display = "";

			if (id2obj != null)
				id2obj.style.display = "";

			if (id4obj != null)
				id4obj.style.display = "";
		}
	}
</script>
</head>
<body style="white-space:normal">
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
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:�з���; text-align:center">�u�W�Ҹ�(�ۥѰѥ[)</p>
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:�з���">�ӤH���Z</p>
	<table border="1" cellpadding="4" cellspacing="0" style="width:95%; background-color:#F7F7DE; margin-bottom:10px">
	<tr id="id1">
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">�ҥ͸ը��s��</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_tu_sid" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">�ҥ�IP</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_tu_ip" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�ҥͩm�W</td>
		<td style="text-align:left"><asp:Label ID="lb_tu_name" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�ҥ;Ǹ�</td>
		<td style="text-align:left"><asp:Label ID="lb_tu_no" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr id="id2">
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�@���}�l�ɶ�</td>
		<td style="text-align:left"><asp:Label ID="lb_b_time" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�@�������ɶ�</td>
		<td style="text-align:left"><asp:Label ID="lb_e_time" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr id="id3">
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�ը����D</td>
		<td style="text-align:left" colspan="3"><asp:Label ID="lb_tp_title" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr id="id4"><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�ը�����</td>
		<td style="text-align:left" colspan="3"><asp:Label ID="lb_tp_desc" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr id="id5">
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�ƦW</td>
		<td style="text-align:left">
			��&nbsp;<asp:Label ID="lb_tu_sort" runat="server"></asp:Label>&nbsp;�W / ����&nbsp;<asp:Label ID="lb_tp_member" runat="server"></asp:Label>&nbsp;�W
		</td>		
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�D��/����</td>
		<td style="text-align:left">
			����&nbsp;<asp:Label ID="lb_tu_question" runat="server"></asp:Label>&nbsp;�D / ����&nbsp;<asp:Label ID="lb_tp_question" runat="server"></asp:Label>&nbsp;�D&nbsp;�U&nbsp;
			��o&nbsp;<asp:Label ID="lb_tu_score" runat="server"></asp:Label>&nbsp;�� / ����&nbsp;<asp:Label ID="lb_tp_score" runat="server"></asp:Label>&nbsp;��
		</td>
	</tr>
	<tr id="id6">
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">�����o��</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_tp_avg" runat="server"></asp:Label>&nbsp;��</td>
		<td style="text-align:center; width:12%; background-color:#888800; color:#FFFFFF">�\��ﶵ</td>
		<td style="text-align:left; background-color:#DDDDDD; width:38%">
			<a href="javascript:title_style()" id="title_btn" class="abtn" title="�ը����D�Y��B�z">&nbsp;���Y���D&nbsp;</a>
		</td>
	</tr>
	</table>

	<asp:GridView ID="gv_Ts_QU" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="tq_sid" DataSourceID="ods_Ts_QU" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="95%" EmptyDataText="�S������ը��D�ت���ơI" 
		HorizontalAlign="Center" onpageindexchanging="gv_Ts_QU_PageIndexChanged" 
		AllowSorting="True" onrowdatabound="gv_Ts_QU_RowDataBound" PageSize="5">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="20px" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="60pt" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="tuq_score" HeaderText="���" InsertVisible="False" ReadOnly="True">
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tq_sort" HeaderText="�D��" InsertVisible="False" ReadOnly="True" SortExpression="q.tq_sort">
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tq_score" HeaderText="����" SortExpression="q.tq_score" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="22pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="���D��r" SortExpression="q.tq_desc">
			<ItemTemplate>
				<asp:Label ID="lb_tq_desc" runat="server" Text='<%# Bind("tq_desc") %>'></asp:Label><br />
				<asp:Literal ID="lt_tq_desc" runat="server"></asp:Literal>
			</ItemTemplate>
			<ItemStyle HorizontalAlign="Left" />
		</asp:TemplateField>
		<asp:BoundField DataField="tq_type" HeaderText="���D�覡" SortExpression="q.tq_type">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="init_time" HeaderText="���D�ɶ�"
			SortExpression="u.init_time" DataFormatString="{0:yyyy/MM/dd HH:mm}">
			<HeaderStyle HorizontalAlign="Center" Width="65pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
	</Columns>
	<EmptyDataTemplate>�S������ը��D�ت���ơI</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<p style="margin:10pt 0pt 0pt 0pt"><a href="javascript:location.replace('B002.aspx');" class="abtn">&nbsp;�^�ݨ��M��&nbsp;</a></p>&nbsp;

	<asp:ObjectDataSource ID="ods_Ts_QU" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Ts_QU" SelectMethod="Select_Ts_QU"
			SortParameterName="SortColumn" TypeName="ODS_Ts_QU_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="tu_sid" Type="Int32" />
			<asp:Parameter Name="tp_sid" Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:Label ID="lb_tp_sid" runat="server" Visible="false"></asp:Label>
	<asp:Label ID="lb_pageid" runat="server" Visible="false"></asp:Label>
	<script language="javascript" type="text/javascript">
		title_style();
	</script>
	</div>
	</form>
</body>
</html>
