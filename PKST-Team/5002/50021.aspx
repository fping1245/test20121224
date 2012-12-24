<%@ Page Language="C#" AutoEventWireup="true" CodeFile="50021.aspx.cs" Inherits="_50021" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>行事曆管理</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript" src="../js/innerCalendar.js"></script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<!-- Begin 覆蓋整個工作頁面，不讓使用者再按其它按鍵 -->
	<div id="fullwindow" style="position: absolute; top:0px; left:0px; width:1024px; height:768px; display:none">
	<table id="fulltable" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_back.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<!-- End -->

	<!-- Begin 日曆視窗 -->
	<div id="div_calendar" style="position: absolute; top:0px; left:0px; display:none">
	<table border="2" cellpadding="0" cellspacing="0" style="width:204px" bgcolor="#efefef">
	<tr><td align="center">
			<table border="0" cellpadding="2" cellspacing="0" style="width:100%">
			<tr style="background-color:Blue">
				<td align="left" style=" font-size:9pt; color:White; cursor:move" onmousedown="EventDm()" onmouseup="EventUp()" onmousemove="EventMv(event)">&nbsp;日期選擇</td>
				<td align="right" style="background-color:Blue; width:30px">
					<a href="javascript:close_calendar()" style="font-size:11pt; color:White; border-color:White; border-style:inherit; background-color:Red; text-decoration:none" title="關閉視窗">&nbsp;×&nbsp;</a>
				</td>
			</tr>
			</table>
		</td>
	</tr>
	<tr style="background-color:#FFCCFF">
		<td><iframe id="if_calendar" src="" scrolling="no" frameborder="0" style="width:260px"></iframe></td>
	</tr>
	</table>
	</div>
	<!-- End -->

	<center>
	<table width="100%" border="1" cellpadding="0" cellspacing="2" style="background-color:#efefef">
	<tr><td><p align="center" class="text14pt" style="margin: 5pt 0pt 5pt 0pt; font-family: 標楷體;">
				<asp:Literal ID="lt_title" runat="server" Text="新增"></asp:Literal>行事曆資料
			</p>
			<table width="100%" border="1" cellpadding="4" cellspacing="0" style="background-color: #F7F7DE;">
			<tr><td align="center" style="width: 90px; background-color: #99FF99">主旨</td>
				<td align="left" colspan="3"><asp:TextBox ID="tb_ca_subject" runat="server" MaxLength="50" Width="95%"></asp:TextBox></td>
			</tr>
			<tr><td align="center" style="background-color: #99FF99">地點</td>
				<td align="left" colspan="3"><asp:TextBox ID="tb_ca_area" runat="server" MaxLength="100" Width="95%"></asp:TextBox></td>
			</tr>
			<tr><td align="center" style="background-color: #99FF99">開始時間</td>
				<td align="left" colspan="3">
					<asp:TextBox ID="tb_ca_bdate" runat="server" MaxLength="10" Width="60pt"></asp:TextBox>
					<img src="../images/button/calendar.gif" alt="開啟日曆視窗選取日期" onclick="getDate('tb_ca_bdate')" style="border-bottom-width: 0pt" />&nbsp;&nbsp;
					<asp:TextBox ID="tb_ca_bhour" runat="server" MaxLength="2" Width="20pt" ToolTip="小時"></asp:TextBox>：
					<asp:TextBox ID="tb_ca_bmin" runat="server" MaxLength="2" Width="20pt" ToolTip="分鐘"></asp:TextBox>
				</td>
			</tr>
			<tr><td align="center" style="background-color: #99FF99">結束時間</td>
				<td align="left" colspan="3">
					<asp:TextBox ID="tb_ca_edate" runat="server" MaxLength="10" Width="60pt"></asp:TextBox>
					<img src="../images/button/calendar.gif" alt="開啟日曆視窗選取日期" onclick="getDate('tb_ca_edate')" style="border-bottom-width: 0pt" />&nbsp;&nbsp;
					<asp:TextBox ID="tb_ca_ehour" runat="server" MaxLength="2" Width="20pt" ToolTip="小時"></asp:TextBox>：
					<asp:TextBox ID="tb_ca_emin" runat="server" MaxLength="2" Width="20pt" ToolTip="分鐘"></asp:TextBox>
				</td>
			</tr>
			<tr><td align="center" style="width: 90px;background-color: #99FF99">重要程度</td>
				<td align="left" style="width: 155px">
					<asp:DropDownList ID="ddl_ca_class" runat="server" DataSourceID="ods_ca_class" DataTextField="Ca_Class_Name" DataValueField="Ca_Class_Value">
					</asp:DropDownList>
				</td>
				<td align="center"  style="width: 90px;background-color: #99FF99">工作類型</td>
				<td align="left">
					<asp:DropDownList ID="ddl_cg_sid" runat="server" DataSourceID="ods_Ca_Group" DataTextField="cg_name" DataValueField="cg_sid">
					</asp:DropDownList>
				</td>
			</tr>
			<tr><td align="center" style="background-color: #99FF99">內容說明</td>
				<td align="left" colspan="3">
					<asp:TextBox ID="tb_ca_desc" runat="server" MaxLength="1000" TextMode="MultiLine" Rows="10" Width="95%"></asp:TextBox>
				</td>
			</tr>
			<tr><td align="center" style="background-color: #99FF99">附加檔案</td>
				<td align="left" colspan="3">
					<asp:FileUpload ID="fu_file" runat="server" Height="20pt" />
					<asp:Button ID="bn_save" runat="server" Text="&nbsp;上傳&nbsp;" Height="20pt" onclick="bn_save_Click" OnClientClick="parent.show_msg_wait()" />
					<asp:Literal ID="lt_file" runat="server"></asp:Literal>
				</td>
			</tr>
			<tr><td align="center" style="background-color: #99FF99">最後更新時間</td>
				<td align="left" colspan="3">
					<asp:Label ID="lb_init_time" runat="server" Text="&nbsp;" ForeColor="Blue" Font-Bold="true"></asp:Label>
				</td>
			</table>
			<p style="margin:5pt 0pt 10pt 0pt; text-align:center">
				<asp:LinkButton ID="lbk_ok" CssClass="abtn" runat="server" onclick="lbk_ok_Click">&nbsp;存檔&nbsp;</asp:LinkButton>&nbsp;&nbsp;&nbsp;
				<asp:LinkButton ID="lbk_del" CssClass="abtn" runat="server" Visible="false" onclick="lbk_del_Click">&nbsp;刪除&nbsp;</asp:LinkButton>&nbsp;&nbsp;&nbsp;
				<a href="javascript:<%=lb_return.Text%>" class="abtn">&nbsp;關閉&nbsp;</a>
			</p>
		</td>
	</tr>
	</table>
	<iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
	</center>
	<asp:ObjectDataSource ID="ods_ca_class" runat="server" SelectMethod="GetCa_Class" TypeName="ODS_Ca_Class_DataReader"></asp:ObjectDataSource>
	<asp:ObjectDataSource ID="ods_Ca_Group" runat="server" 
			SelectCountMethod="GetCount_Ca_Group" 
			SelectMethod="Select_Ca_Group"
			TypeName="ODS_Ca_Group_DataReader" SortParameterName="SortColumn">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" DefaultValue="" />
			<asp:Parameter Name="startRowIndex" Type="Int32" DefaultValue="0" />
			<asp:Parameter Name="maximumRows" Type="Int32" DefaultValue="32767" />
			<asp:Parameter Name="mg_sid" Type="Int32" DefaultValue="-1" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:Label ID="lb_ca_sid" runat="server" Visible="false"></asp:Label>
	<asp:Label ID="lb_is_attach" runat="server" Visible="false" Text="0"></asp:Label>
	<asp:Label ID="lb_return" runat="server" Visible="false" Text="parent.close_all()"></asp:Label>
	<script language="javascript" type="text/javascript">
		resize();

		// 重新調整母頁框的高度
		function resize() {
			var ifobj;
			ifobj = parent.document.getElementById("if_win");
			if (ifobj != null) {
				ifobj.style.height = (document.body.clientHeight) + "px";
			}

			ifobj = parent.document.getElementById("div_win");
			if (ifobj != null) {
				ifobj.style.height = (document.body.clientHeight) + "px";
			}
		}

		// 刪除資料
		function mdel(sid, mfile) {
			if (confirm("確定要刪除「" + mfile + "」？")) {
				update.location.replace("50021_del.ashx?sid=" + sid + "&ca_sid=<%=lb_ca_sid.Text%>");
			}
		}
	</script>
	</div>
	</form>
</body>
</html>
