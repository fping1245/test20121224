<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A0035.aspx.cs" Inherits="_A0035" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�����ƺ޲z</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// �R���߰�
	function mdel(msid, mdesc)
	{
		if (confirm("�T�w�n�R���s���u" + mdesc + "�v�����?\n"))
		{
			update.location.replace("A00353.ashx?sid=" + msid + "&bh_sid=<%=lb_bh_sid.Text%>");
		}
	}

	// �ק���
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("A00352.aspx?sid=" + msid + "&bh_sid=<%=lb_bh_sid.Text%>&timestamp=" + timestamp, 550, 200);
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
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:�з���; text-align:center">�����ƺ޲z</p>
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:�з���">����ݨ����سB�z</p>
	<table border="1" cellpadding="4" cellspacing="0" style="width:98%; background-color:#F7F7DE; margin-bottom:10px">
	<tr><td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">�D�D�s��</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_bh_sid" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">����覡</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_is_check" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">������D</td>
		<td style="text-align:left" colspan="3"><asp:Label ID="lb_bh_title" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">���e����</td>
		<td style="text-align:left" colspan="3"><asp:Label ID="lb_bh_topic" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">��ܦ���</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_scnt" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�^������</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_acnt" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">����`��</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_total" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�̫�벼�ɶ�</td>
		<td style="text-align:left"><asp:Label ID="lb_bh_time" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�̫Ყ�ʮɶ�</td>
		<td style="text-align:left"><asp:Label ID="lb_init_time" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; background-color:#888800; color:#FFFFFF">�\��ﶵ</td>
		<td style="text-align:left; background-color:#DDDDDD">
			<a href="javascript:show_win('A00351.aspx?bh_sid=<%=lb_bh_sid.Text%>', 550, 200);" class="abtn">&nbsp;�s�W����&nbsp;</a>&nbsp;&nbsp;
		</td>
	</tr>
	</table>

	<asp:GridView ID="gv_Bt_Item" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="bi_sid" DataSourceID="ods_Bt_Item" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="�S�����󲼿�ݨ����ت���ơI" 
		HorizontalAlign="Center" onpageindexchanging="gv_Bt_Item_PageIndexChanged" 
		AllowSorting="True">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="20px" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="bi_sort" HeaderText="����" InsertVisible="False" ReadOnly="True" SortExpression="bi_sort">
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="bi_desc" HeaderText="���ؤ�r" SortExpression="bi_desc" />
		<asp:BoundField DataField="bi_total" HeaderText="�벼�`��" SortExpression="bi_total" DataFormatString="{0:N0}" />
		<asp:BoundField DataField="bi_time" HeaderText="�̫�벼�ɶ�"
			SortExpression="bi_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="76pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="init_time" HeaderText="�̫Ყ�ʮɶ�"
			SortExpression="init_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="76pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="�\��" ShowHeader="False">
			<ItemTemplate>
				<a href="javascript:medit(<%# Eval("bi_sid") %>)" class="abtn">&nbsp;�ק�&nbsp;</a>&nbsp;
				<a href="javascript:mdel(<%# Eval("bi_sid") %>,'<%# Eval("bi_desc") %>')" class="abtn">&nbsp;�R��&nbsp;</a>
			</ItemTemplate>
			<HeaderStyle Width="90px" HorizontalAlign="Center" />
			<ItemStyle Height="20px" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>�S�����󲼿�ݨ����ت���ơI</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<p style="margin:10pt 0pt 0pt 0pt"><a href="javascript:parent.location.reload(true);" class="abtn">&nbsp;�^�ݨ��M��&nbsp;</a></p>&nbsp;

	<asp:ObjectDataSource ID="ods_Bt_Item" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Bt_Item" SelectMethod="Select_Bt_Item"
			SortParameterName="SortColumn" TypeName="ODS_Bt_Item_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="bh_sid" Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	
	<iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
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
