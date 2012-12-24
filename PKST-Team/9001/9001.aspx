<%@ Page Language="C#" AutoEventWireup="true" CodeFile="9001.aspx.cs" Inherits="_9001" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>廣告信發送管理</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// 刪除詢問
	function mdel(msid, hename) {
		if (confirm("確定要刪除「" + hename + "」?\n")) {
			update.location.replace("90013.ashx?sid=" + msid);
		}
	}

	// 開啟郵件主機設定 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function host_set() {
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("90015.aspx?timestamp=" + timestamp, 400, 250);
	}
</script>
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

	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 10pt 0pt; font-family:標楷體;">廣告信發送管理</p>
	<p align="center" class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">廣告信選單</p>
	<table width="98%" border="1" cellspacing="0" cellpadding="4"  bgcolor="#EFEFEF"  class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#f0f0f0">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" width="40" rowspan="2"><font color="#FFFFFF">顯示<br />條件</font></td>
		<td class="text9pt"><font color="#FFFFFF">編號</font></td>
		<td class="text9pt"><font color="#FFFFFF">郵件主旨</font></td>
		<td class="text9pt"><font color="#FFFFFF">發信者姓名</font></td>
		<td class="text9pt"><font color="#FFFFFF">發信者信箱</font></td>
		<td class="text9pt"><font color="#FFFFFF">最後發信時間範圍</font></td>
		<td class="text9pt" width="55"><font color="#FFFFFF">條件設定</font></td>
		<td class="text9pt" width="75"><font color="#FFFFFF">郵件主機</font></td>
		<td class="text9pt" width="75"><font color="#FFFFFF">會員名單</font></td>
		<td class="text9pt" width="80"><font color="#FFFFFF">新增廣告信</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_adm_sid" runat="server" Width="15pt" MaxLength="5"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_adm_title" runat="server" Width="70pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_adm_fname" runat="server" Width="50pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_adm_fmail" runat="server" Width="70pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_btime" runat="server" Width="70pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分:秒 (yyyy/MM/dd HH:mm:ss)"></asp:TextBox>
			&nbsp;～&nbsp;
			<asp:TextBox ID="tb_etime" runat="server" Width="70pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分:秒 (yyyy/MM/dd) HH:mm:ss"></asp:TextBox>
		</td>
		<td><asp:Button ID="Btn_Set" runat="server" Text="設定" onclick="Btn_Set_Click" /></td>
		<td><a href="javascript:host_set()" title="郵件主機、Port、帳號、密碼的設定" class="abtn">&nbsp;主機設定&nbsp;</a></td>
		<td><a href="90014.aspx?md=a&pageid=<%=lb_pageid.Text %>&adm_sid=<%=tb_adm_sid.Text%>&adm_title=<%=Server.UrlEncode(tb_adm_title.Text)%>&adm_fname=<%=Server.UrlEncode(tb_adm_fname.Text)%>&adm_fmail=<%=Server.UrlEncode(tb_adm_fmail.Text)%>&btime=<%=Server.UrlEncode(tb_btime.Text)%>&etime=<%=Server.UrlEncode(tb_etime.Text)%>" class="abtn">&nbsp;會員名單&nbsp;</a></td>
		<td><a href="90011.aspx?md=a&pageid=<%=lb_pageid.Text %>&adm_sid=<%=tb_adm_sid.Text%>&adm_title=<%=Server.UrlEncode(tb_adm_title.Text)%>&adm_fname=<%=Server.UrlEncode(tb_adm_fname.Text)%>&adm_fmail=<%=Server.UrlEncode(tb_adm_fmail.Text)%>&btime=<%=Server.UrlEncode(tb_btime.Text)%>&etime=<%=Server.UrlEncode(tb_etime.Text)%>" class="abtn">&nbsp;新增廣告信&nbsp;</a></td>
	</tr>
	</table>
	<asp:GridView ID="gv_Ad_Mail" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="adm_sid" DataSourceID="ods_Ad_Mail" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何廣告信的資料！" 
		HorizontalAlign="Center" onpageindexchanging="gv_Ad_Mail_PageIndexChanged" 
		AllowSorting="True">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="adm_sid" HeaderText="編號" InsertVisible="False" ReadOnly="True" SortExpression="adm_sid">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="adm_title" HeaderText="郵件主旨" SortExpression="adm_title" />
		<asp:BoundField DataField="adm_fname" HeaderText="發信者姓名" SortExpression="adm_fname" />
		<asp:BoundField DataField="adm_fmail" HeaderText="發信者信箱" SortExpression="adm_fmail" />
		<asp:BoundField DataField="adm_type" HeaderText="格式" SortExpression="adm_type" />
		<asp:BoundField DataField="adm_total" HeaderText="預計數量" SortExpression="adm_total" DataFormatString="{0:N0}" />
		<asp:BoundField DataField="adm_send" HeaderText="已發數量" SortExpression="adm_send" DataFormatString="{0:N0}" />
		<asp:BoundField DataField="adm_error" HeaderText="錯誤數量" SortExpression="adm_error" DataFormatString="{0:N0}" />
		<asp:BoundField DataField="send_time" HeaderText="最後發信時間"
			SortExpression="send_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="80pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="init_time" HeaderText="最後異動時間"
			SortExpression="init_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
			<HeaderStyle HorizontalAlign="Center" Width="80pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="功能" ShowHeader="False">
				<ItemTemplate>
					<a href="90012.aspx?sid=<%# Eval("adm_sid") %>" class="abtn">&nbsp;發送&nbsp;</a>&nbsp;
					<a href="90011.aspx?md=e&sid=<%# Eval("adm_sid") %>&pageid=<%# gv_Ad_Mail.PageIndex %>&adm_sid=<%# tb_adm_sid.Text %>&adm_title=<%# Server.UrlEncode(tb_adm_title.Text) %>&adm_fmail=<%# Server.UrlEncode(tb_adm_fmail.Text) %>&btime=<%# Server.UrlEncode(tb_btime.Text) %>&etime=<%# Server.UrlEncode(tb_etime.Text) %>" class="abtn">&nbsp;修改&nbsp;</a>&nbsp;
					<a href="javascript:mdel(<%# Eval("adm_sid") %>,'<%# Eval("adm_title") %>')" class="abtn">&nbsp;刪除&nbsp;</a>
				</ItemTemplate>
				<HeaderStyle Width="155px" HorizontalAlign="Center" />
				<ItemStyle Height="20px" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何廣告信的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<asp:ObjectDataSource ID="ods_Ad_Mail" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Ad_Mail" SelectMethod="Select_Ad_Mail"
			SortParameterName="SortColumn" TypeName="ODS_Ad_Mail_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="adm_sid" Type="String" />
			<asp:Parameter Name="adm_title" Type="String" />
			<asp:Parameter Name="adm_fname" Type="String" />
			<asp:Parameter Name="adm_fmail" Type="String" />
			<asp:Parameter Name="btime" Type="String" />
			<asp:Parameter Name="etime" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	<iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
	</center>
	
	<!-- Begin 隱藏視窗 -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	
	</div>
	</form>
</body>
</html>
