<%@ Page Language="C#" AutoEventWireup="true" CodeFile="D0023.aspx.cs" Inherits="_D0023" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�׾º޲z</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// �ק�^�����e	(�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function medit(msid, ff_sid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("D00231.aspx?sid=" + msid + "&ff_sid=" + ff_sid + "&timestamp=" + timestamp, 680, 400);
	}

	// �R���^�����e
	function mdel(msid, ff_sid, mtitle, mtime)
	{
		if (confirm("�T�w�n�R���u" + mtitle.replace(/ /g, "") + "(" + mtime + ")�v���^�����?\n"))
		{
			update.location.replace("D00232.ashx?sid=" + msid + "&ff_sid=" + ff_sid);
		}
	}

	// �d�ݥD�D���e
	function desc_show()
	{
		var dobj = document.getElementById("id2");
		var iobj = document.getElementById("img_desc_show");

		if (dobj != null && iobj != null)
		{
			if (dobj.style.display != "none")
			{
				dobj.style.display = "none";
				iobj.src = "../images/button/down.gif";
				iobj.title = "�d�ݥD�D���e";
				iobj.alt = "�d�ݥD�D���e";
			}
			else
			{
				dobj.style.display = "";
				iobj.src = "../images/button/up.gif";
				iobj.title = "���åD�D���e";
				iobj.alt = "���åD�D���e";
			}
		}
	}
</script>
</head>
<body style="word-spacing:normal">
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
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:�з���; text-align:center">�׾�(�޲z)</p>
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:�з���">�Q�ץD�D�^��</p>
	<table border="1" cellpadding="4" cellspacing="0" style="width:98%; background-color:#F7F7DE; margin-bottom:10px; white-space:normal">
	<tr id="id0">
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">�H���m�W</td>
		<td style="text-align:left; width:38%">
			<table width="100%" border="0" cellpadding="0" cellspacing="0">
			<tr><td style="width:45px">
					<asp:Image ID="img_ff_symbol" runat="server" ImageUrl="~/images/symbol/S01.gif" ToolTip="�L��" AlternateText="�L��" />
					<asp:Image ID="img_ff_sex" runat="server" ImageUrl="~/images/symbol/man.gif" ToolTip="�k��" AlternateText="�k��" />
				</td>
				<td><asp:Label ID="lb_ff_name" runat="server"></asp:Label>
					.. (<asp:Literal ID="lt_ff_email" runat="server"></asp:Literal>)</td>
			</tr>
			</table>
		</td>
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">�ɶ��PIP</td>
		<td style="text-align:left; width:38%"><asp:Label ID="lb_ff_time" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr id="id1">
		<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">�Q�ץD�D</td>
		<td style="text-align:left; width:88%" colspan="3">
			<table width="100%" border="0" cellpadding="0" cellspacing="0">
			<tr><td style="width:25px">
					<img src="../images/button/up.gif" id="img_desc_show" onclick="desc_show()" title="���åD�D���e" alt="���åD�D���e" />
				</td>
				<td><asp:Label ID="lb_ff_topic" runat="server"></asp:Label>&nbsp;</td>
			</tr>
			</table>
		</td>
	</tr>
	<tr id="id2">
		<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">�D�D���e</td>
		<td style="text-align:left" colspan="3"><asp:Label ID="lb_ff_desc" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	</table>
	
	<table width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="background-color:#EFEFEF; margin:0pt 0pt 5pt 0pt; border-color:#F0F0F0">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" style="width:30pt" rowspan="2"><font color="#FFFFFF">���<br />����</font></td>
		<td class="text9pt" style="width:75pt"><font color="#FFFFFF">�m�W</font></td>
		<td class="text9pt"><font color="#FFFFFF">�^�����e</font></td>
		<td class="text9pt" style="width:170pt"><font color="#FFFFFF">�o�_�ɶ�</font></td>
		<td class="text9pt" style="width:45pt"><font color="#FFFFFF">����]�w</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_fr_name" runat="server" Width="70pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_fr_desc" runat="server" Width="98%" MaxLength="100"></asp:TextBox></td>		
		<td><asp:TextBox ID="tb_btime" runat="server" Width="70pt" MaxLength="19" ToolTip="�п�J �褸�~/��/�� ��:�� (yyyy/MM/dd HH:mm)"></asp:TextBox>
			&nbsp;��&nbsp;
			<asp:TextBox ID="tb_etime" runat="server" Width="70pt" MaxLength="19" ToolTip="�п�J �褸�~/��/�� ��:�� (yyyy/MM/dd) HH:mm"></asp:TextBox>
		</td>
		<td><asp:Button ID="btn_Set" runat="server" Text="�]�w" onclick="btn_Set_Click" /></td>
	</tr>
	</table>

	<asp:GridView ID="gv_Fm_Response" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="fr_sid" DataSourceID="ods_Fm_Response" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="�S������^�����e�I" 
		HorizontalAlign="Center"
		AllowSorting="True" onrowdatabound="gv_Fm_Response_RowDataBound" ShowHeader="False" 
		onpageindexchanged="gv_Fm_Response_PageIndexChanged">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="25pt" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:TemplateField HeaderText="�Q�װ�" ShowHeader="False">
				<ItemTemplate>
				<table border="1" cellpadding="4" cellspacing="0" style="width:100%; text-align:left; outline-style:double; outline-width:1pt">
					<tr style="height:18pt; background-color:#FFFFE0">
						<td><table border="0" cellpadding="4" cellspacing="0" width="100%">
							<tr><td align="center" style="width:50px; background-color:#FFDD55">
									<asp:Label ID="lb_is_show" runat="server" Text='<%# Eval("is_show") %>'></asp:Label>
								</td>
								<td align="center" style="width:50px; background-color:#FFEE99">
									<asp:Label ID="lb_is_close" runat="server" Text='<%# Eval("is_close") %>'></asp:Label>
								</td>
								<td align="left" style="width:45px">
									<asp:Image ID="img_fr_symbol" runat="server" ImageUrl="~/images/symbol/S00.gif" />
									<asp:Image ID="img_fr_sex" runat="server" ImageUrl="~/images/symbol/woman.gif" ToolTip='<%# Eval("fr_sex") %>' />
								</td>
								<td align="left">
									<asp:Label ID="lb_fr_name" runat="server" Text='<%# Eval("fr_name") %>'></asp:Label>&nbsp;
									(<a href="mailto:<%# Eval("fr_email") %>"><%# Eval("fr_email") %></a>)
								</td>
								<td align="right">
									IP:<asp:Label ID="lb_fr_ip" runat="server" Text='<%# Eval("fr_ip") %>'></asp:Label>&nbsp;�U&nbsp;
									<asp:Label ID="lb_fr_time" runat="server" Text='<%# Eval("fr_time") %>'></asp:Label>
								</td>
								<td align="right" style="width:80pt; vertical-align:top; font-size:12pt">
									<a href="javascript:medit('<%# Eval("fr_sid") %>', '<%# Eval("ff_sid") %>')" class="abtn">&nbsp;�ק�&nbsp;</a>&nbsp;
									<a href="javascript:mdel('<%# Eval("fr_sid") %>', '<%# Eval("ff_sid") %>','<%# Eval("fr_name") %>','<%# Eval("fr_time") %>')" class="abtn">&nbsp;�R��&nbsp;</a>
								</td>
							</tr>
							</table>
						</td>
					</tr>
					<tr style="background-color:#FFFFFF">
						<td><asp:Label ID="lb_fr_desc" runat="server" Text='<%# Eval("fr_desc") %>'></asp:Label></td>
					</tr>
					</table>

				</ItemTemplate>
				<ItemStyle Width="100%" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>�S������^�����e�I</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	
	<p style="text-align:center; margin:10pt 0pt 0pt 0pt; font-size:18pt"><a href="D002.aspx<%=lb_page.Text%>" class="abtn">&nbsp;�^�׾­���&nbsp;</a></p>&nbsp;

	<asp:ObjectDataSource ID="ods_Fm_Response" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Fm_Response" SelectMethod="Select_Fm_Response"
			SortParameterName="SortColumn" TypeName="ODS_Fm_Response_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="ff_sid" Type="String" />
			<asp:Parameter Name="is_close" Type="String" />
			<asp:Parameter Name="fr_name" Type="String" />
			<asp:Parameter Name="fr_email" Type="String" />
			<asp:Parameter Name="fr_desc" Type="String" />
			<asp:Parameter Name="btime" Type="String" />
			<asp:Parameter Name="etime" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>

	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
	
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
