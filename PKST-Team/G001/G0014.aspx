<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G0014.aspx.cs" Inherits="_G0014" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>��Ʈw�W��޲z</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// �s�W��Ʈw���	(�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function madd()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("G00141.aspx?ds_sid=<%=lb_ds_sid.Text%>&timestamp=" + timestamp, 680, 400);
	}

	// �ק��Ʈw���	(�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("G00142.aspx?dt_sid=" + msid + "&ds_sid=<%=lb_ds_sid.Text%>&timestamp=" + timestamp, 680, 400);
	}

	// �R����Ʈw���
	function mdel(msid, mcode, mname)
	{
		mcode = mcode.replace(/ /g, "");
		mname = mname.replace(/ /g, "");
	
		if (confirm("�T�w�n�R���u" + "(" + mcode + ")" + mname + "�v����Ʈw�����?\n"))
		{
			update.location.replace("G00143.ashx?dt_sid=" + msid + "&ds_sid=<%=lb_ds_sid.Text%>");
		}
	}

	// �d�ݪ�����
	function mdetail(msid)
	{
		var mhref = "";

		mhref += "G00144.aspx<%=lb_page.Text%>";
		mhref += "&dt_sid=" + msid + "&ds_sid=<%=lb_ds_sid.Text%>";
		mhref += "&pageid1=<%=gv_Db_Table.PageIndex.ToString()%>";
		mhref += "&dt_name=<%=Server.UrlEncode(tb_dt_name.Text)%>";
		mhref += "&dt_caption=<%=Server.UrlEncode(tb_dt_caption.Text)%>";
		mhref += "&dt_area=<%=Server.UrlEncode(tb_dt_area.Text)%>";
		mhref += "&sort1=<%=ods_Db_Table.SelectParameters["SortColumn"].DefaultValue%>";
	
		location.replace(mhref);
	}

	// �d�ݳƵ�����
	function desc_show()
	{
		var tr1obj = document.getElementById("tr1");
		var tr2obj = document.getElementById("tr2");
		var iobj = document.getElementById("img_desc_show");

		if (tr1obj != null && tr2obj != null && iobj != null)
		{
			if (tr1obj.style.display != "none")
			{
				tr1obj.style.display = "none";
				tr2obj.style.display = "none";
				iobj.src = "../images/button/down.gif";
				iobj.title = "��ܳƵ�����";
				iobj.alt = "��ܳƵ�����";
			}
			else
			{
				tr1obj.style.display = "";
				tr2obj.style.display = "";
				iobj.src = "../images/button/up.gif";
				iobj.title = "���óƵ�����";
				iobj.alt = "���óƵ�����";
			}
		}
	}
	
	// �d�ݪ��Բӻ���
	function table_show(msid)
	{
		var tr1obj = document.getElementById("tr1" + msid);
		var tr2obj = document.getElementById("tr2" + msid);
		var tr3obj = document.getElementById("tr3" + msid);
		var iobj = document.getElementById("img_table_show" + msid);

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
	function mprn(msid, msel)
	{	
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		
		switch (msel)
		{
			case 1:			// ��Ʈw�M��
				update.location.replace("G00145.ashx?ds_sid=<%=lb_ds_sid.Text%>&timestamp=" + timestamp);
				break;
			case 2:			// �������
				update.location.replace("G00146.ashx?ds_sid=<%=lb_ds_sid.Text%>&timestamp=" + timestamp);
				break;
			case 3:			// ��@���
				update.location.replace("G00147.ashx?dt_sid=" + msid + "&ds_sid=<%=lb_ds_sid.Text%>&timestamp=" + timestamp);
				break;
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
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:�з���; text-align:center">��Ʈw�W��޲z</p>
	<table border="1" cellpadding="4" cellspacing="0" width="98%" style="background-color:#F7F7DE;border-color:#003366;border-width:1px;border-style:Double;border-collapse:collapse;">
	<tr style="height:12pt; text-align:left">
		<td style="width:36pt; background-color:#99FF99; text-align:center">�N�@��</td>
		<td style="width:80pt"><asp:Label ID="lb_ds_code" runat="server"></asp:Label></td>
		<td style="width:36pt; background-color:#99FF99; text-align:center">�W�@��</td>
		<td style="width:150pt"><asp:Label ID="lb_ds_name" runat="server"></asp:Label></td>
		<td style="width:36pt; background-color:#99FF99; text-align:center">��Ʈw</td> 
		<td><asp:Label ID="lb_ds_database" runat="server"></asp:Label></td>
		<td style="width:20px; background-color:#99FF99; text-align:center"><img src="../images/button/down.gif" id="img_desc_show" onclick="desc_show()" title="��ܳƵ�����" alt="��ܳƵ�����" /></td>
		<td style="width:110pt" align="center">
			<a href="javascript:mprn('<%=lb_ds_sid.Text %>', 1)" class="abtn" title="���͸�Ʈw���M��(Word �榡�ɮ�)">&nbsp;�C�L�M��&nbsp;</a>&nbsp;
			<a href="javascript:mprn('<%=lb_ds_sid.Text %>', 2)" class="abtn" title="�C�L������椺�e(Word �榡�ɮ�)">&nbsp;�C�L����&nbsp;</a>
		</td>
	</tr>
	<tr style="display:none; text-align:left" id="tr1">
		<td style="background-color:#99FF99; text-align:center">�b�@��</td>
		<td><asp:Label ID="lb_ds_id" runat="server"></asp:Label></td>
		<td rowspan="2" style="background-color:#99FF99; text-align:center">���@��</td>
		<td rowspan="2" colspan="5"><asp:Label ID="lb_ds_desc" runat="server"></asp:Label></td>
	</tr>
	<tr style="display:none; text-align:left" id="tr2">
		<td style="background-color:#99FF99; text-align:center">�K�@�X</td>
		<td><asp:Label ID="lb_ds_pass" runat="server"></asp:Label></td>
	</tr>
	</table>
	<hr style="width:98%" />
	<p class="text14pt" style="margin:2pt 0pt 2pt 0pt; font-family:�з���; text-align:center">���޲z</p>	
	<table width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="background-color:#EFEFEF; margin:2pt 0pt 4pt 0pt; border-color:#F0F0F0">
	<tr align="center" style="background-color:#99CCFF">
		<td class="text9pt" style="width:30pt" rowspan="2"><font color="#FFFFFF">���<br />����</font></td>
		<td class="text9pt" style="width:75pt"><font color="#FFFFFF">���W��</font></td>
		<td class="text9pt" style="width:100pt"><font color="#FFFFFF">������D</font></td>
		<td class="text9pt"><font color="#FFFFFF">�Ҧb��m</font></td>
		<td class="text9pt" style="width:45pt"><font color="#FFFFFF">����]�w</font></td>
		<td class="text9pt" style="width:56pt"><font color="#FFFFFF">�s�W���</font></td>
		<td class="text9pt" style="width:120pt" colspan="3"><font color="#FFFFFF">�ƧǤ覡</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_dt_name" runat="server" Width="70pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_dt_caption" runat="server" Width="94pt" MaxLength="20"></asp:TextBox></td>		
		<td><asp:TextBox ID="tb_dt_area" runat="server" Width="98%" MaxLength="100"></asp:TextBox></td>
		<td><asp:Button ID="btn_Set" runat="server" Text="�]�w" onclick="btn_Set_Click" /></td>
		<td><a href="javascript:madd()" class="abtn">&nbsp;�s�W���&nbsp;</a></td>
		<td class="text9pt" style="width:32pt">
			<asp:LinkButton ID="lk_st_dt_sort" runat="server" Text="&nbsp;����&nbsp;" onclick="lk_st_dt_sort_Click"></asp:LinkButton>
		</td>
		<td class="text9pt" style="width:32pt">
			<asp:LinkButton ID="lk_st_dt_name" runat="server" Text="&nbsp;�W��&nbsp;" onclick="lk_st_dt_name_Click" CssClass="abtn"></asp:LinkButton>
		</td>
		<td class="text9pt" style="width:32pt">
			<asp:LinkButton ID="lk_st_dt_caption" runat="server" Text="&nbsp;���D&nbsp;" onclick="lk_st_dt_caption_Click" CssClass="abtn"></asp:LinkButton>
		</td>
	</tr>
	</table>

	<asp:GridView ID="gv_Db_Table" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="ds_sid" DataSourceID="ods_Db_Table" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="�S�������Ʈw���I" 
		HorizontalAlign="Center" AllowSorting="True" ShowHeader="False" 
		onrowdatabound="gv_Db_Table_RowDataBound" 
		onpageindexchanged="gv_Db_Table_PageIndexChanged" 
		ondatabound="gv_Db_Table_DataBound">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
		<PagerSettings Visible="False" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="25pt" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:TemplateField HeaderText="��Ʈw���" ShowHeader="False">
			<ItemTemplate>
				<table border="1" cellpadding="4" cellspacing="0" style="width:100%; text-align:left; outline-style:double; outline-width:1pt">
				<tr style="height:12pt; background-color:#FFFFE0">
					<td style="width:40pt; background-color:#BBFF66; text-align:center">��ܶ���</td> 
					<td style="width:120pt"><asp:Label ID="lb_dt_sort" runat="server"></asp:Label></td>
					<td style="width:40pt; background-color:#BBFF66; text-align:center">���W��</td>
					<td style="width:150pt"><asp:Label ID="lb_dt_name" runat="server" Text='<%# Eval("dt_name") %>'></asp:Label></td>
					<td style="width:40pt; background-color:#BBFF66; text-align:center">������D</td>
					<td><asp:Label ID="lb_dt_caption" runat="server" Text='<%# Eval("dt_caption") %>'></asp:Label></td>
					<td style="width:20px; background-color:#BBFF66; text-align:center"><img src="../images/button/down.gif" id="img_table_show<%# Eval("dt_sid") %>" onclick="table_show('<%# Eval("dt_sid") %>')" title="�d�ݪ��Բӻ���" alt="�d�ݪ��Բӻ���" /></td>
					<td style="width:135pt" align="center">
						<a href="javascript:mdetail('<%# Eval("dt_sid") %>')" class="abtn" title="�d�ݪ�����">&nbsp;���&nbsp;</a>&nbsp;
						<a href="javascript:mprn('<%# Eval("dt_sid") %>', 3)" class="abtn" title="�C�L��@���">&nbsp;�C�L&nbsp;</a>&nbsp;
						<a href="javascript:medit('<%# Eval("dt_sid") %>')" class="abtn" title="�ק���">&nbsp;�ק�&nbsp;</a>&nbsp;
						<a href="javascript:mdel('<%# Eval("dt_sid") %>','<%# Eval("dt_name") %>','<%# Eval("dt_caption") %>')" class="abtn" title="�R�����">&nbsp;�R��&nbsp;</a>
					</td>
				</tr>
				<tr style="background-color:#FFFFE0; display:none" id="tr1<%# Eval("dt_sid") %>">
					<td style="background-color:#BBFF66; text-align:center">�׭q�H��</td>
					<td><asp:Label ID="lb_dt_modi" runat="server" Text='<%# Eval("dt_modi") %>'></asp:Label></td>
					<td style="background-color:#BBFF66; text-align:center">�\�໡��</td>
					<td colspan="5"><asp:Label ID="lb_dt_desc" runat="server" Text='<%# Eval("dt_desc") %>'></asp:Label>&nbsp;</td>
				</tr>
				<tr style="background-color:#FFFFE0; display:none" id="tr2<%# Eval("dt_sid") %>">
					<td style="background-color:#BBFF66; text-align:center">�Ҧb��m</td>
					<td><asp:Label ID="lb_dt_area" runat="server" Text='<%# Eval("dt_area") %>'></asp:Label></td>
					<td rowspan="2" style="background-color:#BBFF66; text-align:center">�������</td>
					<td rowspan="2" colspan="5"><asp:Label ID="lb_dt_index" runat="server" Text='<%# Eval("dt_index") %>'></asp:Label>&nbsp;</td>
				</tr>
				<tr style="background-color:#FFFFE0; display:none" id="tr3<%# Eval("dt_sid") %>">
					<td style="background-color:#BBFF66; text-align:center">���ʮɶ�</td>
					<td><asp:Label ID="lb_init_time" runat="server" Text='<%# Eval("init_time") %>'></asp:Label></td>
				</table>
			</ItemTemplate>
			<ItemStyle Width="100%" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>�S�������Ʈw���I</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>

	<asp:ObjectDataSource ID="ods_Db_Table" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Db_Table" SelectMethod="Select_Db_Table"
			SortParameterName="SortColumn" TypeName="ODS_Db_Table_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="ds_sid" Type="Int32" />
			<asp:Parameter Name="dt_name" Type="String" />
			<asp:Parameter Name="dt_caption" Type="String" />
			<asp:Parameter Name="dt_area" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>
	
	<table border="0" cellpadding="0" cellspacing="0" style="margin:10pt 0pt 10pt 0pt">
	<tr><td colspan="5" style="text-align:center"><asp:Menu ID="mu_page" runat="server" onmenuitemclick="mu_page_MenuItemClick" Orientation="Horizontal"></asp:Menu></td>
	</tr>
	<tr align="center">
		<td style="width:30px"><asp:ImageButton ID="ib_first" runat="server" ImageUrl="~/images/button/bn-first.gif" ToolTip="�Ĥ@��" AlternateText="�Ĥ@��" onclick="ib_first_Click" /></td>
		<td style="width:30px"><asp:ImageButton ID="ib_prev" runat="server" ImageUrl="~/images/button/bn-prev.gif" ToolTip="�W�@��" AlternateText="�W�@��" onclick="ib_prev_Click" /></td>
		<td>&nbsp;( �� <%=gv_Db_Table.PageIndex + 1 %> �� / �@ <%=gv_Db_Table.PageCount %> �� )&nbsp;</td>
		<td style="width:30px"><asp:ImageButton ID="ib_next" runat="server" ImageUrl="~/images/button/bn-next.gif" ToolTip="�U�@��" AlternateText="�U�@��" onclick="ib_next_Click" /></td>
		<td style="width:30px"><asp:ImageButton ID="ib_last" runat="server" ImageUrl="~/images/button/bn-last.gif" ToolTip="�̥���" AlternateText="�̥���" onclick="ib_last_Click" /></td>		
	</tr>
	</table>
	<p style="margin:0pt 0pt 0pt 0pt"><a href="G001.aspx<%=lb_page.Text%>" class="abtn">&nbsp;�^�W��&nbsp;</a></p>
	<br />

	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_pageid1" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_ds_sid" runat="server" Text="" Visible="false"></asp:Label>
	
	<iframe name="update" id="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>

	<!-- Begin ���õ��� -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	
	</center>
	</div>	
	</form>
</body>
</html>
