<%@ Page Language="C#" AutoEventWireup="true" CodeFile="60021_add.aspx.cs" Inherits="_60021_add" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>�޳q�T���޲z</title>
</head>
<body>
<form id="form1" runat="server">
<div>
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:�з���;">�q�T���޲z</p>
	<p align="center" class="text16pt" style="margin:0pt 0pt 5pt 0pt; font-family:�з���;">�s�W���</p>
	<center>
	<table cellspacing="0" cellpadding="4" rules="all" border="1" style="background-color:White;border-color:#003366;border-width:1px;border-style:Double;width:580pt;border-collapse:collapse;">
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" style="width:90pt">�q�T���s��</td>
			<td style="width:200pt; background-color:#F7F7DE; color:Red; font-weight:bold; text-align:center">�t�Φ۰ʲ���</td>
			<td align="center" style="width:90pt" title="�u�s�աv�̦h50�Ӧr">�s��</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:DropDownList ID="ddl_As_Group" runat="server" DataSourceID="sds_As_Group" DataTextField="ag_name" DataValueField="ag_sid">
				</asp:DropDownList>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="�u�m�W�v�̦h50�Ӧr">�m�W</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_name" Width="98%" runat="server" MaxLength="50" ToolTip="�u�m�W�v�̦h50�Ӧr"></asp:TextBox>
			</td>
			<td align="center" title="�u�ʺ١v�̦h50�Ӧr">�ʺ�</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_nike" Width="98%" runat="server" MaxLength="50" ToolTip="�u�ʺ١v�̦h50�Ӧr"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="�u��}�v�̦h150�Ӧr">��}</td>
			<td style="background-color:#F7F7DE" colspan="3">�l���ϸ��G
				<asp:TextBox ID="tb_ab_zipcode" runat="server" Width="40pt" MaxLength="5" ToolTip="�u�l���ϸ��v�̦h5�Ӧr"></asp:TextBox> (5�X)<br />
				<asp:TextBox ID="tb_ab_address" runat="server" Width="98%" MaxLength="150" ToolTip="�u��}�v�̦h150�Ӧr"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="�u��a�q�ܡv�̦h50�Ӧr">��a�q��</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_tel_h" Width="98%" runat="server" MaxLength="50" ToolTip="�u��a�q�ܡv�̦h50�Ӧr"></asp:TextBox>
			</td>
			<td align="center" title="�u�줽�q�ܡv�̦h50�Ӧr">�줽�q��</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_tel_o" Width="98%" runat="server" MaxLength="50" ToolTip="�u�줽�q�ܡv�̦h50�Ӧr"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="�u��ʹq�ܡv�̦h50�Ӧr">��ʹq��</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_mobil" Width="98%" runat="server" MaxLength="50" ToolTip="�u��ʹq�ܡv�̦h50�Ӧr"></asp:TextBox>
			</td>
			<td align="center" title="�u�ǯu���X�v�̦h50�Ӧr">�ǯu���X</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_fax" Width="98%" runat="server" MaxLength="50" ToolTip="�u�ǯu���X�v�̦h50�Ӧr"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="�u�q�l�H�c�v�̦h100�Ӧr">�q�l�H�c</td>
			<td style="background-color:#F7F7DE" colspan="3">
				<asp:TextBox ID="tb_ab_email" Width="98%" runat="server" MaxLength="100" ToolTip="�u�q�l�H�c�v�̦h100�Ӧr"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="�u¾�ȦW�١v�̦h50�Ӧr">¾�ȦW��</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_posit" runat="server" Width="98%" MaxLength="50" ToolTip="�u¾�ȦW�١v�̦h50�Ӧr"></asp:TextBox>
			</td>
			<td align="center" title="�u�u�@���v�̦h50�Ӧr">�u�@���</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_company" runat="server" Width="98%" MaxLength="50" ToolTip="�u�u�@���v�̦h50�Ӧr"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="�u�Ƶ������v�̦h500�Ӧr">�Ƶ�����</td>
			<td style="background-color:#F7F7DE" colspan="3">
				<asp:TextBox ID="tb_ab_desc" runat="server" Width="98%" MaxLength="500" TextMode="MultiLine" ToolTip="�u�Ƶ������v�̦h500�Ӧr"></asp:TextBox>
			</td>
		</tr>
	</table>	
	<p style="margin:10pt 0pt 10pt 0pt">
		<asp:LinkButton ID="lb_ok" runat="server" CssClass="abtn" onclick="lb_ok_Click">&nbsp;�T�w�x�s&nbsp;</asp:LinkButton>&nbsp;&nbsp;
		<a href="javascript:history.go(-1);" class="abtn">&nbsp;�^�W�@��&nbsp;</a></p>&nbsp;
	</center>
	<asp:SqlDataSource ID="sds_As_Group" runat="server" 
		ConnectionString="<%$ ConnectionStrings:AppSysConnectionString %>" 
		SelectCommand="SELECT ag_sid, (RTrim(ag_name) + ' (' + RTrim(ag_attrib) + ')') as ag_name FROM [As_Group] WHERE ([mg_sid] = @mg_sid)">
		<SelectParameters>
			<asp:SessionParameter Name="mg_sid" SessionField="mg_sid" Type="Int32" />
		</SelectParameters>
	</asp:SqlDataSource>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
</div>
</form>
</body>
</html>
