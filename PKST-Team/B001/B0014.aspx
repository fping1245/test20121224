<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B0014.aspx.cs" Inherits="_B0014" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�Ҹ��D�w�޲z</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	var title_var = 1; 	// 1:�i�} 0:�Y�p

	// �s�W�D�� (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function madd()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("B00141.aspx?tp_sid=<%=lb_tp_sid.Text%>&tp_title=<%=Server.UrlEncode(lb_tp_title.Text)%>&timestamp=" + timestamp, 680, 550);
	}

	// �ק��D�� (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("B00142.aspx?sid=" + msid + "&tp_sid=<%=lb_tp_sid.Text%>&tp_title=<%=Server.UrlEncode(lb_tp_title.Text)%>&timestamp=" + timestamp, 680, 550);
	}

	// �R���D��
	function mdel(msid, msort, mdesc)
	{
		if (confirm("�T�w�n�R���u" + msort + ". " + mdesc + "�v���D��?\n"))
		{
			update.location.replace("B00143.ashx?sid=" + msid + "&tp_sid=<%=lb_tp_sid.Text%>");
		}
	}

	// �s�W���� (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function add_item(msid, tq_sort, tq_desc)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("B00144.aspx?sid=" + msid + "&tp_sid=<%=lb_tp_sid.Text%>&tq_sort=" + tq_sort + "&tq_desc=" + tq_desc + "&timestamp=" + timestamp, 550, 250);
	}

	// �קﵪ�� (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function mod_item(msid, tq_sid, tq_sort, tq_desc)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("B00145.aspx?sid=" + msid + "&tp_sid=<%=lb_tp_sid.Text%>&tq_sort=" + tq_sort + "&tq_desc=" + tq_desc + "&tq_sid=" + tq_sid + "&timestamp=" + timestamp, 550, 250);
	}

	// �R������
	function del_item(msid, tq_sid, ti_sort, ti_desc)
	{
		if (confirm("�T�w�n�R���u(" + ti_sort + ") " + ti_desc + "�v�����׿ﶵ?\n"))
		{
			update.location.replace("B00146.ashx?sid=" + msid + "&tp_sid=<%=lb_tp_sid.Text%>&tq_sid=" + tq_sid);
		}
	}

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

			if (id5obj != null)
				id5obj.style.display = "none";

			if (id6obj != null)
				id6obj.style.display = "none";
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

			if (id5obj != null)
				id5obj.style.display = "";

			if (id6obj != null)
				id6obj.style.display = "";			

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
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:�з���; text-align:center">�Ҹ��D�w�޲z</p>
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:�з���">���D�B�z</p>
	<table border="1" cellpadding="4" cellspacing="0" style="width:98%; background-color:#F7F7DE; margin-bottom:10px">
	<tr id="id1">
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">�ը��s��</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_tp_sid" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">�O�_���</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_is_show" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr id="id2">
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�}�l�i�J�ɶ�</td>
		<td style="text-align:left"><asp:Label ID="lb_b_time" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�I��i�J�ɶ�</td>
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
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�ը��D��</td>
		<td style="text-align:left"><asp:Label ID="lb_tp_question" runat="server"></asp:Label>&nbsp;�D</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�ը��`��</td>
		<td style="text-align:left"><asp:Label ID="lb_tp_score" runat="server"></asp:Label>&nbsp;��</td>
	</tr>
	<tr id="id6">
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�ѥ[�H��</td>
		<td style="text-align:left"><asp:Label ID="lb_tp_member" runat="server"></asp:Label>&nbsp;�H</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�`�o��</td>
		<td style="text-align:left"><asp:Label ID="lb_tp_total" runat="server"></asp:Label>&nbsp;��</td>
	</tr>
	<tr id="id7">
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">�̫Ყ�ʮɶ�</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_init_time" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; width:12%; background-color:#888800; color:#FFFFFF">�\��ﶵ</td>
		<td style="text-align:left; background-color:#DDDDDD; width:38%">
			<a href="javascript:madd();" class="abtn">&nbsp;�s�W���D&nbsp;</a>&nbsp;��&nbsp;
			<a href="javascript:title_style()" id="title_btn" class="abtn" title="�ը����D�Y��B�z">&nbsp;���Y���D&nbsp;</a>
		</td>
	</tr>
	</table>

	<asp:GridView ID="gv_Ts_Question" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="tq_sid" DataSourceID="ods_Ts_Question" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="�S������ը��D�ت���ơI" 
		HorizontalAlign="Center" onpageindexchanging="gv_Ts_Question_PageIndexChanged" 
		AllowSorting="True" onrowdatabound="gv_Ts_Question_RowDataBound" PageSize="5">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="20px" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="60pt" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="tq_sort" HeaderText="�D��" InsertVisible="False" ReadOnly="True" SortExpression="tq_sort">
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="���D��r" SortExpression="tq_desc">
			<ItemTemplate>
				<asp:Label ID="lb_tq_desc" runat="server"></asp:Label><br />
				<asp:Literal ID="lt_tq_desc" runat="server"></asp:Literal>
			</ItemTemplate>
			<ItemStyle HorizontalAlign="Left" />
		</asp:TemplateField>
		<asp:BoundField DataField="tq_type" HeaderText="���D�覡" SortExpression="tq_type">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tq_score" HeaderText="����" SortExpression="tq_score" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="22pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="init_time" HeaderText="�̫Ყ�ʮɶ�"
			SortExpression="init_time" DataFormatString="{0:yyyy/MM/dd HH:mm}">
			<HeaderStyle HorizontalAlign="Center" Width="65pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="�\��" ShowHeader="False">
			<ItemTemplate>
				<p style="font-size:18pt; margin:2pt 0pt 7pt 0pt"><a href="javascript:medit(<%# Eval("tq_sid") %>)" class="abtn" title="�ק���D">&nbsp;�ק�&nbsp;</a></p>
				<p style="font-size:18pt; margin:7pt 0pt 2pt 0pt"><a href="javascript:mdel(<%# Eval("tq_sid") %>, '<%# Eval("tq_sort") %>','<%# Eval("tq_desc") %>')" class="abtn" title="�R�����D (�]�t���׿ﶵ)">&nbsp;�R��&nbsp;</a></p>
			</ItemTemplate>
			<HeaderStyle Width="32pt" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>�S������ը��D�ت���ơI</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<p style="margin:10pt 0pt 0pt 0pt"><a href="javascript:location.replace('B001.aspx<%=lb_page.Text%>');" class="abtn">&nbsp;�^�ը��M��&nbsp;</a></p>&nbsp;

	<asp:ObjectDataSource ID="ods_Ts_Question" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Ts_Question" SelectMethod="Select_Ts_Question"
			SortParameterName="SortColumn" TypeName="ODS_Ts_Question_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="tp_sid" Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
	
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	
	<iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
	</center>
	
	<!-- Begin ���õ��� -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->

	<script language="javascript" type="text/javascript">
		title_style();
	</script>
	</div>
	</form>
</body>
</html>
