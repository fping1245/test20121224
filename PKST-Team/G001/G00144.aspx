<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G00144.aspx.cs" Inherits="_G00144" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>��Ʈw�W��޲z</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// �s�W����� (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function madd()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("G001441.aspx?ds_sid=<%=lb_ds_sid.Text%>&dt_sid=<%=lb_dt_sid.Text%>&timestamp=" + timestamp, 650, 250);
	}

	// �ק������ (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("G001442.aspx?dr_sid=" + msid + "&ds_sid=<%=lb_ds_sid.Text%>&dt_sid=<%=lb_dt_sid.Text%>&timestamp=" + timestamp, 650, 250);
	}

	// �R�������
	function mdel(msid, mname, msort)
	{
		mname = mname.replace(/ /g, "");
		msort = msort / 10;
	
		if (confirm("�T�w�n�R���u" + msort.toString() + ". " + mname + "�v?\n"))
		{
			update.location.replace("G001443.ashx?dr_sid=" + msid + "&ds_sid=<%=lb_ds_sid.Text%>&dt_sid=<%=lb_dt_sid.Text%>");
		}
	}

	// ������춶�� (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function msort(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("G001444.aspx?dr_sid=" + msid + "&ds_sid=<%=lb_ds_sid.Text%>&dt_sid=<%=lb_dt_sid.Text%>&timestamp=" + timestamp, 250, 250);
	}
	
	// �d�ݪ�满��
	function table_show()
	{
		var tr1obj = document.getElementById("tr1");
		var tr2obj = document.getElementById("tr2");
		var tr3obj = document.getElementById("tr3");
		var iobj = document.getElementById("img_table_show");

		if (tr1obj != null && tr2obj != null && tr3obj != null && iobj != null)
		{
			if (tr1obj.style.display != "none")
			{
				tr1obj.style.display = "none";
				tr2obj.style.display = "none";
				tr3obj.style.display = "none";
				iobj.src = "../images/button/down.gif";
				iobj.title = "��ܪ��Բӻ���";
				iobj.alt = "��ܪ��Բӻ���";
			}
			else
			{
				tr1obj.style.display = "";
				tr2obj.style.display = "";
				tr3obj.style.display = "";
				iobj.src = "../images/button/up.gif";
				iobj.title = "���ê��Բӻ���";
				iobj.alt = "���ê��Բӻ���";
			}
		}
	}
	
		
	// ���ͳ���
	function mprn()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		update.location.replace("G00147.ashx?dt_sid=<%=lb_dt_sid.Text%>&ds_sid=<%=lb_ds_sid.Text%>&timestamp=" + timestamp);
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
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:�з���;">��Ʈw�W��޲z</p>
	<table border="1" cellpadding="4" cellspacing="0" style="width:98%; background-color:#F7F7DE;border-color:#003366;border-width:1px;border-style:Double;border-collapse:collapse;">
	<tr style="height:12pt; text-align:left">
		<td style="width:40pt; background-color:#99FF99; text-align:center">��ܶ���</td> 
		<td style="width:120pt"><asp:Label ID="lb_dt_sort" runat="server"></asp:Label></td>
		<td style="width:40pt; background-color:#99FF99; text-align:center">���W��</td>
		<td style="width:140pt"><asp:Label ID="lb_dt_name" runat="server"></asp:Label></td>
		<td style="width:40pt; background-color:#99FF99; text-align:center">������D</td>
		<td><asp:Label ID="lb_dt_caption" runat="server"></asp:Label></td>
		<td style="width:20px; background-color:#99FF99; text-align:center"><img src="../images/button/down.gif" id="img_table_show" onclick="table_show()" title="�d�ݪ��Բӻ���" alt="�d�ݪ��Բӻ���" /></td>
		<td style="width:110pt" align="center">
			<a href="javascript:madd()" class="abtn" title="�s�W���">&nbsp;�s�W���&nbsp;</a>&nbsp;
			<a href="javascript:mprn()" class="abtn" title="�C�L���">&nbsp;�C�L���&nbsp;</a>
		</td>
	</tr>
	<tr style="display:none; text-align:left" id="tr1">
		<td style="background-color:#99FF99; text-align:center">�׭q�H��</td>
		<td><asp:Label ID="lb_dt_modi" runat="server"></asp:Label></td>
		<td style="background-color:#99FF99; text-align:center">�\�໡��</td>
		<td colspan="5"><asp:Label ID="lb_dt_desc" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr style="display:none; text-align:left" id="tr2">
		<td style="background-color:#99FF99; text-align:center">�Ҧb��m</td>
		<td><asp:Label ID="lb_dt_area" runat="server"></asp:Label></td>
		<td rowspan="2" style="background-color:#99FF99; text-align:center">�������</td>
		<td rowspan="2" colspan="5"><asp:Label ID="lb_dt_index" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr style="display:none; text-align:left" id="tr3">
		<td style="background-color:#99FF99; text-align:center">���ʮɶ�</td>
		<td><asp:Label ID="lb_init_time" runat="server"></asp:Label></td>
	</table>
	<hr style="width:98%" />
	<p class="text14pt" style="margin:2pt 0pt 2pt 0pt; font-family:�з���; text-align:center">������</p>	
	<asp:GridView ID="gv_Db_Record" runat="server" AutoGenerateColumns="False" 
		DataSourceID="ods_Db_Record" AllowPaging="True" DataKeyNames="dr_sid"
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="�S����������쪺��ơI" 
		HorizontalAlign="Center" onpageindexchanged="gv_Db_Record_PageIndexChanged"
		AllowSorting="True"	ondatabound="gv_Db_Record_DataBound" 
			onrowdatabound="gv_Db_Record_RowDataBound">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
		<PagerSettings Visible="False" />
	<RowStyle BackColor="#F7F7DE" />
	<Columns>
		<asp:BoundField DataField="dr_sort" HeaderText="����" SortExpression="dr_sort" >
			<ItemStyle HorizontalAlign="Center" />
			<HeaderStyle Width="25pt"></HeaderStyle>
		</asp:BoundField>
		<asp:BoundField DataField="dr_name" HeaderText="���W��" SortExpression="dr_name">
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:BoundField DataField="dr_caption" HeaderText="������D" SortExpression="dr_caption">
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:BoundField DataField="dr_type" HeaderText="���A" SortExpression="dr_type">
			<ItemStyle HorizontalAlign="Center" />
			<HeaderStyle Width="30pt"></HeaderStyle>
		</asp:BoundField>
		<asp:BoundField DataField="dr_len" HeaderText="�e��" SortExpression="dr_len">
			<ItemStyle HorizontalAlign="Right" />
			<HeaderStyle Width="30pt"></HeaderStyle>
		</asp:BoundField>
		<asp:BoundField DataField="dr_point" HeaderText="�p��" SortExpression="dr_point" >
			<ItemStyle HorizontalAlign="Right" />
			<HeaderStyle Width="30pt"></HeaderStyle>
		</asp:BoundField>
		<asp:BoundField DataField="dr_default" HeaderText="�w�]��" SortExpression="dr_default" >
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:BoundField DataField="dr_desc" HeaderText="���e����" SortExpression="dr_desc" >
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="�\��">
			<ItemTemplate>
				<a href="javascript:msort(<%# Eval("dr_sid") %>)" class="abtn" title="������춶��">&nbsp;����&nbsp;</a>&nbsp;
				<a href="javascript:medit(<%# Eval("dr_sid") %>)" class="abtn" title="�ק������">&nbsp;�ק�&nbsp;</a>&nbsp;
				<a href="javascript:mdel(<%# Eval("dr_sid") %>,'<%# Eval("dr_name") %>',<%# Eval("dr_sort") %>)" class="abtn" title="�R�������">&nbsp;�R��&nbsp;</a>
			</ItemTemplate>
			<ItemStyle HorizontalAlign="Center" />
			<HeaderStyle Width="105pt"></HeaderStyle>
		</asp:TemplateField>
	</Columns>
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<EmptyDataTemplate>�S����������쪺��ơI</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<asp:ObjectDataSource ID="ods_Db_Record" runat="server" EnablePaging="True" 
		SelectCountMethod="GetCount_Db_Record" SelectMethod="Select_Db_Record" 
		SortParameterName="SortColumn" TypeName="ODS_Db_Record_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="ds_sid" Type="Int32" />
			<asp:Parameter Name="dt_sid" Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>

	<table border="0" cellpadding="0" cellspacing="0" style="margin:10pt 0pt 10pt 0pt">
	<tr><td colspan="5" style="text-align:center"><asp:Menu ID="mu_page" runat="server" onmenuitemclick="mu_page_MenuItemClick" Orientation="Horizontal"></asp:Menu></td>
	</tr>
	<tr align="center">
		<td style="width:30px"><asp:ImageButton ID="ib_first" runat="server" ImageUrl="~/images/button/bn-first.gif" ToolTip="�Ĥ@��" AlternateText="�Ĥ@��" onclick="ib_first_Click" /></td>
		<td style="width:30px"><asp:ImageButton ID="ib_prev" runat="server" ImageUrl="~/images/button/bn-prev.gif" ToolTip="�W�@��" AlternateText="�W�@��" onclick="ib_prev_Click" /></td>
		<td>&nbsp;( �� <%=gv_Db_Record.PageIndex + 1 %> �� / �@ <%=gv_Db_Record.PageCount %> �� )&nbsp;</td>
		<td style="width:30px"><asp:ImageButton ID="ib_next" runat="server" ImageUrl="~/images/button/bn-next.gif" ToolTip="�U�@��" AlternateText="�U�@��" onclick="ib_next_Click" /></td>
		<td style="width:30px"><asp:ImageButton ID="ib_last" runat="server" ImageUrl="~/images/button/bn-last.gif" ToolTip="�̥���" AlternateText="�̥���" onclick="ib_last_Click" /></td>		
	</tr>
	</table>
	<p style="margin:0pt 0pt 0pt 0pt"><a href="G0014.aspx<%=lb_page.Text%>" class="abtn">&nbsp;�^�W��&nbsp;</a></p>
	<br />

	<iframe name="update" id="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>

	<!-- Begin ���õ��� -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	
	</center>
	<asp:Label ID="lb_ds_sid" runat="server" Visible="false" Text=""></asp:Label>
	<asp:Label ID="lb_dt_sid" runat="server" Visible="false" Text=""></asp:Label>
	<asp:Label ID="lb_pageid2" runat="server" Visible="false" Text=""></asp:Label>
	<asp:Label ID="lb_page" runat="server" Visible="false" Text=""></asp:Label>
	</div>
</form>
</body>
</html>