<%@ Page Language="C#" AutoEventWireup="true" CodeFile="9002.aspx.cs" Inherits="_9002" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>POP3���H�B�z</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// �R���߰�
	function mdel(msid, msubject) {
		if (confirm("�T�w�n�R���u" + msubject + "�v?\n")) {
			update.location.replace("90027.ashx?sid=" + msid + "&ppa_sid=<%=lb_ppa_sid.Text%>");
		}
	}

	// �}�Ҷl��D���]�w (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function host_set()	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("90021.aspx?sid=<%=lb_ppa_sid.Text%>&timestamp=" + timestamp, 400, 250);
	}

	// �}�ҫH����� (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function read_mail(sid, ppa)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("90026.aspx?sid=" + sid + "&ppa_sid=" + ppa + "&timestamp=" + timestamp, 550, 50);
	}
	
	// �}�l�����l��
	function begin_rece_mail()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();

		var divobj = document.getElementById("div_process");
		var ifobj = document.getElementById("update");
	
		if (divobj != null) {
			// �л\��Ӥu�@�����A�����ϥΪ̦A���䥦����
			show_fullwindow();

			// ��ܱ����l��i�׵����A�ýվ��m
			divobj.style.width = "400px";
			divobj.style.left = String((document.body.clientWidth - 400) / 2) + "px";
			divobj.style.top = "175px";
			divobj.style.display = "";

			// �}�l�����l��
			ifobj.src = "90022.ashx?sid=<%=lb_ppa_sid.Text%>&timestamp=" + timestamp;
		}
	}

	// �]�w�i�ת�
	function rece_process(mtotal, mrece, msg) {
		var tobj = document.getElementById("sp_total");
		var robj = document.getElementById("sp_receive");
		var pobj = document.getElementById("sp_percent");
		var mobj = document.getElementById("sp_msg");
		var lobj = document.getElementById("tbl_process");
		var fcnt = 1.0, wcnt = 1;

		if (tobj != null && robj != null && pobj != null && lobj != null)
		{
			if (mtotal == 0)
				fcnt = 0;
			else
				fcnt = mrece / mtotal * 100.0;

			tobj.innerHTML = mtotal.toString();
			robj.innerHTML = mrece.toString();
			pobj.innerHTML = fcnt.toFixed(2);
			mobj.innerHTML = unescape(msg);		// msg�r��b�ǤJ�ɽХ��H escape �s�X�A�H�w���S��r���b������ǰe�ɵo�Ϳ��~

			wcnt = fcnt * 4;

			if (wcnt < 1)
				wcnt = 1;
			else if (wcnt > 400)
				wcnt = 400;

			lobj.style.width = wcnt.toFixed() + "px";
		}
	}

	// ������������
	function rece_close()
	{
		var divobj = document.getElementById("div_process");
		var updateobj = document.getElementById("update");
		if (divobj != null)
		{
			divobj.style.display = "none";
			updateobj.src = "";
		}
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
	<p class="text18pt" style="margin:10pt 0pt 10pt 0pt; font-family:�з���; text-align:center">POP3���H�B�z</p>
	<table width="98%" border="1" cellpadding="4" cellspacing="0" style="margin:0pt 0pt 5pt 0pt; border-color:#f0f0f0; background-color:#F7F7DE">
	<tr align="left" style="height:24px">
		<td style="width:12%; text-align:center; background-color:#99FF99">�D���W��</td>
		<td style="width:21%"><asp:Label ID="lb_ppa_host" runat="server"></asp:Label>&nbsp;</td>
		<td style="width:12%; text-align:center; background-color:#99FF99">�q�T��</td>
		<td style="width:21%"><asp:Label ID="lb_ppa_port" runat="server"></asp:Label>&nbsp;</td>
		<td style="width:12%; text-align:center; background-color:#99FF99">�D���]�w</td>
		<td style="text-align:center"><a href="javascript:host_set()" class="abtn">&nbsp;�]�w POP3 �D��&nbsp;</a></td>
	</tr>
	<tr align="left" style="height:24px">
		<td style="text-align:center; background-color:#99FF99">�l��ƶq/�j�p</td>
		<td style="text-align:center"><asp:Label ID="lb_ppa_num" Text="0" runat="server"></asp:Label> ��&nbsp; / &nbsp;<asp:Label ID="lb_ppa_size" Text="0" runat="server"></asp:Label> bytes</td>
		<td style="text-align:center; background-color:#99FF99">�̫᦬�H�ɶ�</td>
		<td><asp:Label ID="lb_get_time" runat="server"></asp:Label>&nbsp;</td>
		<td style="text-align:center; background-color:#99FF99">�}�l���H</td>
		<td style="text-align:center"><a href="javascript:begin_rece_mail()" class="abtn">&nbsp;�}�l�����l��&nbsp;</a></td>	
	</tr>
	</table>
	
	<div id="div_mail_list">
	<asp:GridView ID="gv_POP3_Mail" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="ppm_sid" DataSourceID="ods_POP3_Mail" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="�ثe�S������l��I" 
		HorizontalAlign="Center" onpageindexchanging="gv_POP3_Mail_PageIndexChanged" 
		AllowSorting="True">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="rownum" HeaderText="����" InsertVisible="False" ReadOnly="True">
			<HeaderStyle HorizontalAlign="Center" Width="30px" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="s_name" HeaderText="�H�H��" SortExpression="s_name" >
		<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:BoundField DataField="ppm_subject" HeaderText="�l��D��" SortExpression="ppm_subject" >
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:BoundField DataField="ppm_size" HeaderText="�j�p" SortExpression="ppm_size" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="50px" />
			<ItemStyle HorizontalAlign="Right" />
		</asp:BoundField>
		<asp:BoundField DataField="s_time" HeaderText="�H�H�ɶ�" SortExpression="s_time" >
			<HeaderStyle HorizontalAlign="Center" Width="135px" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="�\��" ShowHeader="False">
				<ItemTemplate>
					<a href="90028.ashx?sid=<%# Eval("ppm_sid") %>&ppa_sid=<%# Eval("ppa_sid") %>" target="_blank" class="abtn">&nbsp;�ץX&nbsp;</a>&nbsp;
					<a href="javascript:read_mail(<%# Eval("ppm_sid") %>,<%# Eval("ppa_sid") %>)" class="abtn">&nbsp;�d��&nbsp;</a>&nbsp;
					<a href="javascript:mdel(<%# Eval("ppm_sid") %>,'<%# Eval("ppm_subject") %>')" class="abtn">&nbsp;�R��&nbsp;</a>
				</ItemTemplate>
				<HeaderStyle Width="145px" HorizontalAlign="Center" />
				<ItemStyle Height="20px" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>�ثe�S������l��I</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<asp:ObjectDataSource ID="ods_POP3_Mail" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_POP3_Mail" SelectMethod="Select_POP3_Mail"
			SortParameterName="SortColumn" TypeName="ODS_POP3_Mail_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="ppa_sid" DefaultValue="0" Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<p style="text-align:left; width:98%; margin:10px 0px 0px 0px">�� �l�󱵦��ɡA���|�R��POP3�l��D���W���l��C�n�����u�R���v�\��ɡA�~�|�P�B�R���l��D���W���l�� ��</p>
	<p style="text-align:left; width:98%; margin:5px 0px 0px 0px">�� ���\��Ҧ��쪺�l��A�i�ץX�� eml �榡�A��� Outlook Express �Ψ䥦�䴩���l��n��Ӿ\Ū ��</p>	
	</div>

	<asp:Label ID="lb_pageid" runat="server" Text="0" Visible="false"></asp:Label>
	<asp:Label ID="lb_ppa_sid" runat="server" Text="0" Visible="false"></asp:Label>

	<iframe name="update" id="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>

	</center>
	
	<!-- Begin �����l��i�׵��� -->
	<div id="div_process" style="position: absolute; top:0px; left:0px; display:none">
	<table border="1" cellpadding="0" cellspacing="0" style="width:400px; background-color:#E0FFFF">
	<tr><td align="left" style="width:50%; height:24px">&nbsp;&nbsp;�H�c�l��ƶq�G<span id="sp_total">....</span> ��</td>
		<td align="left" style="width:50%; height:24px">&nbsp;&nbsp;�����l��ƶq�G<span id="sp_receive">....</span> ��</td></tr>
	<tr><td align="left" colspan="2" style="height:24px">&nbsp;&nbsp;���H�i�פ�ȡG<span id="sp_percent">0.00</span> %</td></tr>
	<tr><td align="left" colspan="2" style="height:24px">&nbsp;&nbsp;�ثe�B�z���p�G<span id="sp_msg">POP3�D�����Ū����....</span></td></tr>
	<tr><td colspan="2" style="height:5px"></td></tr>
	<tr><td align="left" colspan="2" style="height:10px; background-color:#BBFFEE">
			<table id="tbl_process" border="0" cellpadding="0" cellspacing="0" style="margin:0px 0px 0px 0px; width:1px; background-color:#00FF00">
			<tr><td style="height:10px"></td></tr>
			</table>
		</td>
	</tr>
	<tr><td colspan="2" style="height:5px"></td></tr>
	</table>
	<p style="margin: 10px 0px 0px 0px; text-align:center"><a href="javascript:location.reload(true);" class="abtn">&nbsp;���_����&nbsp;</a></p>&nbsp;
	</div>
	<!-- End -->

	<!-- Begin ���õ��� -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->

	</div>
	</form>
</body>
</html>
