<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G001.aspx.cs" Inherits="_G001" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>��Ʈw�W��޲z</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// �s�W��Ʈw�W��W��	(�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function madd()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("G0011.aspx?timestamp=" + timestamp, 680, 400);
	}

	// �ק��Ʈw�W��W��	(�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("G0012.aspx?ds_sid=" + msid + "&timestamp=" + timestamp, 680, 400);
	}

	// �R����Ʈw�W��
	function mdel(msid, mname, mcode)
	{
		mcode = mcode.replace(/ /g, "");
		mname = mname.replace(/ /g, "");
	
		if (confirm("�T�w�n�R���u" + "(" + mcode + ")" + mname + "�v����Ʈw�W����?\n"))
		{
			update.location.replace("G0013.ashx?ds_sid=" + msid);
		}
	}

	// �d�ݸ�Ʈw���
	function mdetail(msid)
	{
		var mhref = "";

		mhref += "G0014.aspx?ds_sid=" + msid;
		mhref += "&pageid=<%=gv_Db_Sys.PageIndex.ToString()%>";
		mhref += "&ds_code=<%=Server.UrlEncode(tb_ds_code.Text)%>";
		mhref += "&ds_name=<%=Server.UrlEncode(tb_ds_name.Text)%>";
		mhref += "&ds_database=<%=Server.UrlEncode(tb_ds_database.Text)%>";
		mhref += "&sort=<%=ods_Db_Sys.SelectParameters["SortColumn"].DefaultValue%>";
	
		location.replace(mhref);
	}

	// �d�ݳƵ�����
	function desc_show(msid)
	{
		var tr1obj = document.getElementById("tr1" + msid);
		var tr2obj = document.getElementById("tr2" + msid);
		var iobj = document.getElementById("img_desc_show" + msid);

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
	<%--<table width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="background-color:#EFEFEF; margin:0pt 0pt 4pt 0pt; border-color:#F0F0F0">--%>
    <table width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="background-color:#EFEFEF; margin:0pt 0pt 4pt 0pt; border-color:#F0F0F0">
	<%--<tr align="center" style="background-color:#99CCFF">--%>
    <tr align="center" style="background-color:#99CCFF">
		<td class="text9pt" style="width:30pt" rowspan="2"><font color="#FFFFFF">���<br />����</font></td>
		<td class="text9pt" style="width:75pt"><font color="#FFFFFF">�t�ΥN��</font></td>
		<td class="text9pt" style="width:100pt"><font color="#FFFFFF">�t�ΦW��</font></td>
		<td class="text9pt"><font color="#FFFFFF">��Ʈw�W��</font></td>
		<td class="text9pt" style="width:45pt"><font color="#FFFFFF">����]�w</font></td>
		<td class="text9pt" style="width:55pt"><font color="#FFFFFF">�s�W���</font></td>
		<td class="text9pt" style="width:120pt" colspan="3"><font color="#FFFFFF">�ƧǤ覡</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_ds_code" runat="server" Width="70pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_ds_name" runat="server" Width="94pt" MaxLength="20"></asp:TextBox></td>		
		<td><asp:TextBox ID="tb_ds_database" runat="server" Width="98%" MaxLength="100"></asp:TextBox></td>
		<td><asp:Button ID="btn_Set" runat="server" Text="�]�w" onclick="btn_Set_Click" /></td>
		<td><a href="javascript:madd()" class="abtn">&nbsp;�s�W���&nbsp;</a></td>
		<td class="text9pt" style="width:32pt">
			<asp:LinkButton ID="lk_st_ds_code" runat="server" Text="&nbsp;�N��&nbsp;" onclick="lk_st_ds_code_Click"></asp:LinkButton>
		</td>
		<td class="text9pt" style="width:32pt">
			<asp:LinkButton ID="lk_st_ds_name" runat="server" Text="&nbsp;�W��&nbsp;" onclick="lk_st_ds_name_Click" CssClass="abtn"></asp:LinkButton>
		</td>
		<td class="text9pt" style="width:40pt">
			<asp:LinkButton ID="lk_st_ds_database" runat="server" Text="&nbsp;��Ʈw&nbsp;" onclick="lk_st_ds_database_Click" CssClass="abtn"></asp:LinkButton>
		</td>
	</tr>
	</table>

	<asp:GridView ID="gv_Db_Sys" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="ds_sid" DataSourceID="ods_Db_Sys" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="�S�������Ʈw�W����I" 
		HorizontalAlign="Center" AllowSorting="True" ShowHeader="False" 
		onpageindexchanged="gv_Db_Sys_PageIndexChanged" ondatabound="gv_Db_Sys_DataBound" 
		onrowdatabound="gv_Db_Sys_RowDataBound">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
		<PagerSettings Visible="False" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="25pt" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:TemplateField HeaderText="�t�γW��W��" ShowHeader="False">
			<ItemTemplate>
				<table border="1" cellpadding="4" cellspacing="0" style="width:100%; text-align:left; outline-style:double; outline-width:1pt">
				<tr style="height:12pt; background-color:#FFFFE0">
					<td style="width:36pt; background-color:#BBFF66; text-align:center">�N�@��</td>
					<td style="width:80pt"><asp:Label ID="lb_ds_code" runat="server" Text='<%# Eval("ds_code") %>'></asp:Label></td>
					<td style="width:36pt; background-color:#BBFF66; text-align:center">�W�@��</td>
					<td style="width:150pt"><asp:Label ID="lb_ds_name" runat="server" Text='<%# Eval("ds_name") %>'></asp:Label></td>
					<td style="width:36pt; background-color:#BBFF66; text-align:center">��Ʈw</td> 
					<td><asp:Label ID="lb_ds_database" runat="server" Text='<%# Eval("ds_database") %>'></asp:Label></td>
					<td style="width:20px; background-color:#BBFF66; text-align:center"><img src="../images/button/down.gif" id="img_desc_show<%# Eval("ds_sid") %>" onclick="desc_show('<%# Eval("ds_sid") %>')" title="��ܳƵ�����" alt="��ܳƵ�����" /></td>
					<td style="width:100pt" align="center">
						<a href="javascript:mdetail('<%# Eval("ds_sid") %>')" class="abtn" title="�d�ݸ�Ʈw���">&nbsp;���&nbsp;</a>&nbsp;
						<a href="javascript:medit('<%# Eval("ds_sid") %>')" class="abtn" title="�ק���">&nbsp;�ק�&nbsp;</a>&nbsp;
						<a href="javascript:mdel('<%# Eval("ds_sid") %>','<%# Eval("ds_name") %>','<%# Eval("ds_code") %>')" class="abtn" title="�R�����">&nbsp;�R��&nbsp;</a>
					</td>
				</tr>
				<tr style="background-color:#FFFFE0; display:none" id="tr1<%# Eval("ds_sid") %>">
					<td style="background-color:#BBFF66; text-align:center">�b�@��</td>
					<td><asp:Label ID="lb_ds_id" runat="server" Text='<%# Eval("ds_id") %>'></asp:Label></td>
					<td rowspan="2" style="background-color:#BBFF66; text-align:center">����</td>
					<td rowspan="2" colspan="5"><asp:Label ID="lb_ds_desc" runat="server" Text='<%# Eval("ds_desc") %>'></asp:Label></td>
				</tr>
				<tr style="background-color:#FFFFE0; display:none" id="tr2<%# Eval("ds_sid") %>">
					<td style="background-color:#BBFF66; text-align:center">�K�@�X</td>
					<td><asp:Label ID="lb_ds_pass" runat="server" Text='<%# Eval("ds_pass") %>'></asp:Label></td>
				</tr>
				</table>
			</ItemTemplate>
			<ItemStyle Width="100%" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>�S�������Ʈw�W����I</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>

	<asp:ObjectDataSource ID="ods_Db_Sys" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Db_Sys" SelectMethod="Select_Db_Sys"
			SortParameterName="SortColumn" TypeName="ODS_Db_Sys_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="ds_sid" Type="String" />
			<asp:Parameter Name="ds_code" Type="String" />
			<asp:Parameter Name="ds_name" Type="String" />
			<asp:Parameter Name="ds_database" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>

	<table border="0" cellpadding="0" cellspacing="0" style="margin:10pt 0pt 10pt 0pt">
	<tr><td colspan="5" style="text-align:center"><asp:Menu ID="mu_page" runat="server" onmenuitemclick="mu_page_MenuItemClick" Orientation="Horizontal"></asp:Menu></td>
	</tr>
	<tr align="center">
		<td style="width:30px"><asp:ImageButton ID="ib_first" runat="server" ImageUrl="~/images/button/bn-first.gif" ToolTip="�Ĥ@��" AlternateText="�Ĥ@��" onclick="ib_first_Click" /></td>
		<td style="width:30px"><asp:ImageButton ID="ib_prev" runat="server" ImageUrl="~/images/button/bn-prev.gif" ToolTip="�W�@��" AlternateText="�W�@��" onclick="ib_prev_Click" /></td>
		<td>&nbsp;( �� <%=gv_Db_Sys.PageIndex + 1 %> �� / �@ <%=gv_Db_Sys.PageCount %> �� )&nbsp;</td>
		<td style="width:30px"><asp:ImageButton ID="ib_next" runat="server" ImageUrl="~/images/button/bn-next.gif" ToolTip="�U�@��" AlternateText="�U�@��" onclick="ib_next_Click" /></td>
		<td style="width:30px"><asp:ImageButton ID="ib_last" runat="server" ImageUrl="~/images/button/bn-last.gif" ToolTip="�̥���" AlternateText="�̥���" onclick="ib_last_Click" /></td>		
	</tr>
	</table>
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	
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
