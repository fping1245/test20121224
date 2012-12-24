<%@ Page Language="C#" AutoEventWireup="true" CodeFile="60021_add.aspx.cs" Inherits="_60021_add" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>管通訊錄管理</title>
</head>
<body>
<form id="form1" runat="server">
<div>
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體;">通訊錄管理</p>
	<p align="center" class="text16pt" style="margin:0pt 0pt 5pt 0pt; font-family:標楷體;">新增資料</p>
	<center>
	<table cellspacing="0" cellpadding="4" rules="all" border="1" style="background-color:White;border-color:#003366;border-width:1px;border-style:Double;width:580pt;border-collapse:collapse;">
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" style="width:90pt">通訊錄編號</td>
			<td style="width:200pt; background-color:#F7F7DE; color:Red; font-weight:bold; text-align:center">系統自動產生</td>
			<td align="center" style="width:90pt" title="「群組」最多50個字">群組</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:DropDownList ID="ddl_As_Group" runat="server" DataSourceID="sds_As_Group" DataTextField="ag_name" DataValueField="ag_sid">
				</asp:DropDownList>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「姓名」最多50個字">姓名</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_name" Width="98%" runat="server" MaxLength="50" ToolTip="「姓名」最多50個字"></asp:TextBox>
			</td>
			<td align="center" title="「暱稱」最多50個字">暱稱</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_nike" Width="98%" runat="server" MaxLength="50" ToolTip="「暱稱」最多50個字"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「住址」最多150個字">住址</td>
			<td style="background-color:#F7F7DE" colspan="3">郵遞區號：
				<asp:TextBox ID="tb_ab_zipcode" runat="server" Width="40pt" MaxLength="5" ToolTip="「郵遞區號」最多5個字"></asp:TextBox> (5碼)<br />
				<asp:TextBox ID="tb_ab_address" runat="server" Width="98%" MaxLength="150" ToolTip="「住址」最多150個字"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「住家電話」最多50個字">住家電話</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_tel_h" Width="98%" runat="server" MaxLength="50" ToolTip="「住家電話」最多50個字"></asp:TextBox>
			</td>
			<td align="center" title="「辦公電話」最多50個字">辦公電話</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_tel_o" Width="98%" runat="server" MaxLength="50" ToolTip="「辦公電話」最多50個字"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「行動電話」最多50個字">行動電話</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_mobil" Width="98%" runat="server" MaxLength="50" ToolTip="「行動電話」最多50個字"></asp:TextBox>
			</td>
			<td align="center" title="「傳真號碼」最多50個字">傳真號碼</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_fax" Width="98%" runat="server" MaxLength="50" ToolTip="「傳真號碼」最多50個字"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「電子信箱」最多100個字">電子信箱</td>
			<td style="background-color:#F7F7DE" colspan="3">
				<asp:TextBox ID="tb_ab_email" Width="98%" runat="server" MaxLength="100" ToolTip="「電子信箱」最多100個字"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「職務名稱」最多50個字">職務名稱</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_posit" runat="server" Width="98%" MaxLength="50" ToolTip="「職務名稱」最多50個字"></asp:TextBox>
			</td>
			<td align="center" title="「工作單位」最多50個字">工作單位</td>
			<td style="background-color:#F7F7DE">
				<asp:TextBox ID="tb_ab_company" runat="server" Width="98%" MaxLength="50" ToolTip="「工作單位」最多50個字"></asp:TextBox>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" title="「備註說明」最多500個字">備註說明</td>
			<td style="background-color:#F7F7DE" colspan="3">
				<asp:TextBox ID="tb_ab_desc" runat="server" Width="98%" MaxLength="500" TextMode="MultiLine" ToolTip="「備註說明」最多500個字"></asp:TextBox>
			</td>
		</tr>
	</table>	
	<p style="margin:10pt 0pt 10pt 0pt">
		<asp:LinkButton ID="lb_ok" runat="server" CssClass="abtn" onclick="lb_ok_Click">&nbsp;確定儲存&nbsp;</asp:LinkButton>&nbsp;&nbsp;
		<a href="javascript:history.go(-1);" class="abtn">&nbsp;回上一頁&nbsp;</a></p>&nbsp;
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
