<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B001.aspx.cs" Inherits="_B001" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�Ҹ��D�w�޲z</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// �s�W���
	function madd()
	{
		show_win("B0011.aspx", 680, 400);
	}

	// �ק��� (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();

		show_win("B0012.aspx?sid=" + msid + "&timestamp=" + timestamp, 680, 400);
	}

	// �R���߰�
	function mdel(msid, mtitle)
	{
		if (confirm("�T�w�n�R���u" + mtitle + "�v���ը����?\n"))
		{
			update.location.replace("B0013.ashx?sid=" + msid);
		}
	}

	// ���D�ε��׳B�z (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function mdetail(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		var mhref = "B0014.aspx?sid=" + msid;

		mhref += "&pageid=<%=lb_pageid.Text%>&tp_sid=<%=tb_tp_sid.Text%>&tp_title=<%=Server.UrlEncode(tb_tp_title.Text)%>";
		mhref += "&is_show=<%=lb_is_show.Text%>&btime=<%=Server.UrlEncode(tb_btime.Text)%>&etime=<%=Server.UrlEncode(tb_etime.Text)%>";
		mhref += "&timestamp=" + timestamp;

		location.href = mhref;
	}

	// �ҥ͵��D���� (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function mans(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();

		var mhref = "B0015.aspx?sid=" + msid;

		mhref += "&pageid=<%=lb_pageid.Text%>&tp_sid=<%=tb_tp_sid.Text%>&tp_title=<%=Server.UrlEncode(tb_tp_title.Text)%>";
		mhref += "&is_show=<%=lb_is_show.Text%>&btime=<%=Server.UrlEncode(tb_btime.Text)%>&etime=<%=Server.UrlEncode(tb_etime.Text)%>";
		mhref += "&timestamp=" + timestamp;

		location.href = mhref;
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
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:�з���; text-align:center">�ը��M��</p>
<%--    	<table width="98%" border="1" cellspacing="0" cellpadding="4"  bgcolor="#EFEFEF"  class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#F0F0F0">
	<tr align="center" bgcolor="#99CCFF">--%>
	<table width="98%" border="1" cellspacing="0" cellpadding="4" bgcolor="#EFEFEF" 
            class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#F0F0F0">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" width="40" rowspan="2"><font color="#FFFFFF">���<br />����</font></td>
		<td class="text9pt"><font color="#FFFFFF">�s��</font></td>
		<td class="text9pt"><font color="#FFFFFF">�ը����D</font></td>
		<td class="text9pt"><font color="#FFFFFF">�O�_���</font></td>
		<td class="text9pt"><font color="#FFFFFF">�Ҹն}��ɶ�</font></td>
		<td class="text9pt" width="55"><font color="#FFFFFF">����]�w</font></td>
		<td class="text9pt" width="75"><font color="#FFFFFF">�s�W�ը�</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_tp_sid" runat="server" Width="15pt" MaxLength="5"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_tp_title" runat="server" Width="160pt" MaxLength="50"></asp:TextBox></td>
		<td><asp:RadioButton ID="rb_is_show0" runat="server" Text="����" CssClass="text9pt" GroupName="rb_is_show" />
			<asp:RadioButton ID="rb_is_show1" runat="server" Text="���" CssClass="text9pt" GroupName="rb_is_show" />
			<asp:RadioButton ID="rb_is_show_all" runat="server" Text="����" CssClass="text9pt" GroupName="rb_is_show" />
		</td>
		<td><asp:TextBox ID="tb_btime" runat="server" Width="70pt" MaxLength="19" ToolTip="�п�J �褸�~/��/�� ��:�� (yyyy/MM/dd HH:mm)"></asp:TextBox>
			&nbsp;��&nbsp;
			<asp:TextBox ID="tb_etime" runat="server" Width="70pt" MaxLength="19" ToolTip="�п�J �褸�~/��/�� ��:�� (yyyy/MM/dd) HH:mm"></asp:TextBox>
		</td>
		<td><asp:Button ID="btn_Set" runat="server" Text="�]�w" onclick="btn_Set_Click" /></td>
		<td><a href="javascript:madd()" class="abtn">&nbsp;�s�W�ը�&nbsp;</a></td>
	</tr>
	</table>
	<asp:GridView ID="gv_Ts_Paper" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="tp_sid" DataSourceID="ods_Ts_Paper" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="�S������ը�����ơI" 
		HorizontalAlign="Center" onpageindexchanging="gv_Ts_Paper_PageIndexChanged" 
		AllowSorting="True" onrowdatabound="gv_Ts_Paper_RowDataBound">
	<%--<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />--%>
    <HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<%--<RowStyle BackColor="#F7F7DE" />--%>
    <RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="25pt" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="tp_sid" HeaderText="�s��" InsertVisible="False" ReadOnly="True" SortExpression="tp_sid">
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_title" HeaderText="�ը��D�D" SortExpression="tp_title" >
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:BoundField DataField="is_show" HeaderText="���" SortExpression="is_show">
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="b_time" HeaderText="�}��i�J�ɶ�" SortExpression="b_time" DataFormatString="{0:yyyy/MM/dd HH:mm}">
			<HeaderStyle HorizontalAlign="Center" Width="65pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="e_time" HeaderText="�I��i�J�ɶ�" SortExpression="e_time" DataFormatString="{0:yyyy/MM/dd HH:mm}">
			<HeaderStyle HorizontalAlign="Center" Width="65pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_question" HeaderText="���D�`��" SortExpression="tp_question" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_member" HeaderText="�ѥ[�H��" SortExpression="tp_member" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_score" HeaderText="�`��" SortExpression="tp_score" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="�\��" ShowHeader="False">
				<ItemTemplate>
					<a href="javascript:mdetail(<%# Eval("tp_sid") %>)" class="abtn" title="���D�ε��׳B�z">&nbsp;���D&nbsp;</a>&nbsp;
					<a href="javascript:mans(<%# Eval("tp_sid") %>)" class="abtn" title="�ҥ͵��D����">&nbsp;����&nbsp;</a>&nbsp;
					<a href="javascript:medit(<%# Eval("tp_sid") %>)" class="abtn" title="�ը��D�D�ק�">&nbsp;�ק�&nbsp;</a>&nbsp;
					<a href="javascript:mdel(<%# Eval("tp_sid") %>,'<%# Eval("tp_title") %>')" class="abtn" title="�R���ը�">&nbsp;�R��&nbsp;</a>
				</ItemTemplate>
				<HeaderStyle Width="135pt" HorizontalAlign="Center" />
				<ItemStyle Height="18pt" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>�S������ը�����ơI</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>

	<asp:ObjectDataSource ID="ods_Ts_Paper" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Ts_Paper" SelectMethod="Select_Ts_Paper"
			SortParameterName="SortColumn" TypeName="ODS_Ts_Paper_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="tp_sid" Type="String" />
			<asp:Parameter Name="tp_title" Type="String" />
			<asp:Parameter Name="is_show" Type="String" />
			<asp:Parameter Name="btime" Type="String" />
			<asp:Parameter Name="etime" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>

	<p style="margin:5pt 0pt 0pt 0pt; text-align:left; width:98%">�� �u�u�W�Ҹաv�p�ĥΡu�ۥѰѥ[�v���覡�A�������إߦҥ͸�ơA�ҥͦb�ѥ[�Ҹծɶ�J��ƧY�i�C</p>
	<p style="margin:2pt 0pt 0pt 0pt; text-align:left; width:98%">�� �u�u�W�Ҹաv�p�ĥΡu���w�����v���覡�A�n�b�u�����v�\��̥��إߦҥ͸�ơA�ѥ[�Ҹծɭn��J���T��������ơA�~���\�i�J�ҸաC</p>

	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_is_show" runat="server" Text="" Visible="false"></asp:Label>
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
