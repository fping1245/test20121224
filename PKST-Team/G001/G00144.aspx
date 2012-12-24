<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G00144.aspx.cs" Inherits="_G00144" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>資料庫規格管理</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// 新增欄位資料 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function madd()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("G001441.aspx?ds_sid=<%=lb_ds_sid.Text%>&dt_sid=<%=lb_dt_sid.Text%>&timestamp=" + timestamp, 650, 250);
	}

	// 修改欄位資料 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("G001442.aspx?dr_sid=" + msid + "&ds_sid=<%=lb_ds_sid.Text%>&dt_sid=<%=lb_dt_sid.Text%>&timestamp=" + timestamp, 650, 250);
	}

	// 刪除欄位資料
	function mdel(msid, mname, msort)
	{
		mname = mname.replace(/ /g, "");
		msort = msort / 10;
	
		if (confirm("確定要刪除「" + msort.toString() + ". " + mname + "」?\n"))
		{
			update.location.replace("G001443.ashx?dr_sid=" + msid + "&ds_sid=<%=lb_ds_sid.Text%>&dt_sid=<%=lb_dt_sid.Text%>");
		}
	}

	// 移動欄位順序 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function msort(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("G001444.aspx?dr_sid=" + msid + "&ds_sid=<%=lb_ds_sid.Text%>&dt_sid=<%=lb_dt_sid.Text%>&timestamp=" + timestamp, 250, 250);
	}
	
	// 查看表格說明
	function table_show()
	{
		var tr1obj = document.getElementById("tr1");
		var tr2obj = document.getElementById("tr2");
		var tr3obj = document.getElementById("tr3");
		var iobj = document.getElementById("img_table_show");

		if (tr1obj != null && tr2obj != null && tr3obj != null && iobj != null)
		{
			if (tr1obj.style.display != "none")
			{
				tr1obj.style.display = "none";
				tr2obj.style.display = "none";
				tr3obj.style.display = "none";
				iobj.src = "../images/button/down.gif";
				iobj.title = "顯示表格詳細說明";
				iobj.alt = "顯示表格詳細說明";
			}
			else
			{
				tr1obj.style.display = "";
				tr2obj.style.display = "";
				tr3obj.style.display = "";
				iobj.src = "../images/button/up.gif";
				iobj.title = "隱藏表格詳細說明";
				iobj.alt = "隱藏表格詳細說明";
			}
		}
	}
	
		
	// 產生報表
	function mprn()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		update.location.replace("G00147.ashx?dt_sid=<%=lb_dt_sid.Text%>&ds_sid=<%=lb_ds_sid.Text%>&timestamp=" + timestamp);
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
	<p align="center" class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體;">資料庫規格管理</p>
	<table border="1" cellpadding="4" cellspacing="0" style="width:98%; background-color:#F7F7DE;border-color:#003366;border-width:1px;border-style:Double;border-collapse:collapse;">
	<tr style="height:12pt; text-align:left">
		<td style="width:40pt; background-color:#99FF99; text-align:center">顯示順序</td> 
		<td style="width:120pt"><asp:Label ID="lb_dt_sort" runat="server"></asp:Label></td>
		<td style="width:40pt; background-color:#99FF99; text-align:center">表格名稱</td>
		<td style="width:140pt"><asp:Label ID="lb_dt_name" runat="server"></asp:Label></td>
		<td style="width:40pt; background-color:#99FF99; text-align:center">中文標題</td>
		<td><asp:Label ID="lb_dt_caption" runat="server"></asp:Label></td>
		<td style="width:20px; background-color:#99FF99; text-align:center"><img src="../images/button/down.gif" id="img_table_show" onclick="table_show()" title="查看表格詳細說明" alt="查看表格詳細說明" /></td>
		<td style="width:110pt" align="center">
			<a href="javascript:madd()" class="abtn" title="新增欄位">&nbsp;新增欄位&nbsp;</a>&nbsp;
			<a href="javascript:mprn()" class="abtn" title="列印表格">&nbsp;列印表格&nbsp;</a>
		</td>
	</tr>
	<tr style="display:none; text-align:left" id="tr1">
		<td style="background-color:#99FF99; text-align:center">修訂人員</td>
		<td><asp:Label ID="lb_dt_modi" runat="server"></asp:Label></td>
		<td style="background-color:#99FF99; text-align:center">功能說明</td>
		<td colspan="5"><asp:Label ID="lb_dt_desc" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr style="display:none; text-align:left" id="tr2">
		<td style="background-color:#99FF99; text-align:center">所在位置</td>
		<td><asp:Label ID="lb_dt_area" runat="server"></asp:Label></td>
		<td rowspan="2" style="background-color:#99FF99; text-align:center">索引鍵值</td>
		<td rowspan="2" colspan="5"><asp:Label ID="lb_dt_index" runat="server"></asp:Label>&nbsp;</td>
	</tr>
	<tr style="display:none; text-align:left" id="tr3">
		<td style="background-color:#99FF99; text-align:center">異動時間</td>
		<td><asp:Label ID="lb_init_time" runat="server"></asp:Label></td>
	</table>
	<hr style="width:98%" />
	<p class="text14pt" style="margin:2pt 0pt 2pt 0pt; font-family:標楷體; text-align:center">表格欄位</p>	
	<asp:GridView ID="gv_Db_Record" runat="server" AutoGenerateColumns="False" 
		DataSourceID="ods_Db_Record" AllowPaging="True" DataKeyNames="dr_sid"
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何表格欄位的資料！" 
		HorizontalAlign="Center" onpageindexchanged="gv_Db_Record_PageIndexChanged"
		AllowSorting="True"	ondatabound="gv_Db_Record_DataBound" 
			onrowdatabound="gv_Db_Record_RowDataBound">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
		<PagerSettings Visible="False" />
	<RowStyle BackColor="#F7F7DE" />
	<Columns>
		<asp:BoundField DataField="dr_sort" HeaderText="順序" SortExpression="dr_sort" >
			<ItemStyle HorizontalAlign="Center" />
			<HeaderStyle Width="25pt"></HeaderStyle>
		</asp:BoundField>
		<asp:BoundField DataField="dr_name" HeaderText="欄位名稱" SortExpression="dr_name">
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:BoundField DataField="dr_caption" HeaderText="中文標題" SortExpression="dr_caption">
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:BoundField DataField="dr_type" HeaderText="型態" SortExpression="dr_type">
			<ItemStyle HorizontalAlign="Center" />
			<HeaderStyle Width="30pt"></HeaderStyle>
		</asp:BoundField>
		<asp:BoundField DataField="dr_len" HeaderText="寬度" SortExpression="dr_len">
			<ItemStyle HorizontalAlign="Right" />
			<HeaderStyle Width="30pt"></HeaderStyle>
		</asp:BoundField>
		<asp:BoundField DataField="dr_point" HeaderText="小數" SortExpression="dr_point" >
			<ItemStyle HorizontalAlign="Right" />
			<HeaderStyle Width="30pt"></HeaderStyle>
		</asp:BoundField>
		<asp:BoundField DataField="dr_default" HeaderText="預設值" SortExpression="dr_default" >
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:BoundField DataField="dr_desc" HeaderText="內容說明" SortExpression="dr_desc" >
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="功能">
			<ItemTemplate>
				<a href="javascript:msort(<%# Eval("dr_sid") %>)" class="abtn" title="移動欄位順序">&nbsp;移動&nbsp;</a>&nbsp;
				<a href="javascript:medit(<%# Eval("dr_sid") %>)" class="abtn" title="修改欄位資料">&nbsp;修改&nbsp;</a>&nbsp;
				<a href="javascript:mdel(<%# Eval("dr_sid") %>,'<%# Eval("dr_name") %>',<%# Eval("dr_sort") %>)" class="abtn" title="刪除欄位資料">&nbsp;刪除&nbsp;</a>
			</ItemTemplate>
			<ItemStyle HorizontalAlign="Center" />
			<HeaderStyle Width="105pt"></HeaderStyle>
		</asp:TemplateField>
	</Columns>
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<EmptyDataTemplate>沒有任何表格欄位的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	<asp:ObjectDataSource ID="ods_Db_Record" runat="server" EnablePaging="True" 
		SelectCountMethod="GetCount_Db_Record" SelectMethod="Select_Db_Record" 
		SortParameterName="SortColumn" TypeName="ODS_Db_Record_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="ds_sid" Type="Int32" />
			<asp:Parameter Name="dt_sid" Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>

	<table border="0" cellpadding="0" cellspacing="0" style="margin:10pt 0pt 10pt 0pt">
	<tr><td colspan="5" style="text-align:center"><asp:Menu ID="mu_page" runat="server" onmenuitemclick="mu_page_MenuItemClick" Orientation="Horizontal"></asp:Menu></td>
	</tr>
	<tr align="center">
		<td style="width:30px"><asp:ImageButton ID="ib_first" runat="server" ImageUrl="~/images/button/bn-first.gif" ToolTip="第一頁" AlternateText="第一頁" onclick="ib_first_Click" /></td>
		<td style="width:30px"><asp:ImageButton ID="ib_prev" runat="server" ImageUrl="~/images/button/bn-prev.gif" ToolTip="上一頁" AlternateText="上一頁" onclick="ib_prev_Click" /></td>
		<td>&nbsp;( 第 <%=gv_Db_Record.PageIndex + 1 %> 頁 / 共 <%=gv_Db_Record.PageCount %> 頁 )&nbsp;</td>
		<td style="width:30px"><asp:ImageButton ID="ib_next" runat="server" ImageUrl="~/images/button/bn-next.gif" ToolTip="下一頁" AlternateText="下一頁" onclick="ib_next_Click" /></td>
		<td style="width:30px"><asp:ImageButton ID="ib_last" runat="server" ImageUrl="~/images/button/bn-last.gif" ToolTip="最末頁" AlternateText="最末頁" onclick="ib_last_Click" /></td>		
	</tr>
	</table>
	<p style="margin:0pt 0pt 0pt 0pt"><a href="G0014.aspx<%=lb_page.Text%>" class="abtn">&nbsp;回上頁&nbsp;</a></p>
	<br />

	<iframe name="update" id="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>

	<!-- Begin 隱藏視窗 -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	
	</center>
	<asp:Label ID="lb_ds_sid" runat="server" Visible="false" Text=""></asp:Label>
	<asp:Label ID="lb_dt_sid" runat="server" Visible="false" Text=""></asp:Label>
	<asp:Label ID="lb_pageid2" runat="server" Visible="false" Text=""></asp:Label>
	<asp:Label ID="lb_page" runat="server" Visible="false" Text=""></asp:Label>
	</div>
</form>
</body>
</html>