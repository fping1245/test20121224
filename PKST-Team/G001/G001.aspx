<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G001.aspx.cs" Inherits="_G001" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>資料庫規格管理</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// 新增資料庫規格名稱	(避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function madd()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("G0011.aspx?timestamp=" + timestamp, 680, 400);
	}

	// 修改資料庫規格名稱	(避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("G0012.aspx?ds_sid=" + msid + "&timestamp=" + timestamp, 680, 400);
	}

	// 刪除資料庫規格
	function mdel(msid, mname, mcode)
	{
		mcode = mcode.replace(/ /g, "");
		mname = mname.replace(/ /g, "");
	
		if (confirm("確定要刪除「" + "(" + mcode + ")" + mname + "」的資料庫規格資料?\n"))
		{
			update.location.replace("G0013.ashx?ds_sid=" + msid);
		}
	}

	// 查看資料庫表格
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

	// 查看備註說明
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
				iobj.title = "顯示備註說明";
				iobj.alt = "顯示備註說明";
			}
			else
			{
				tr1obj.style.display = "";
				tr2obj.style.display = "";
				iobj.src = "../images/button/up.gif";
				iobj.title = "隱藏備註說明";
				iobj.alt = "隱藏備註說明";
			}
		}
	}
</script>
</head>
<body style="white-space:normal">
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
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">資料庫規格管理</p>
	<%--<table width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="background-color:#EFEFEF; margin:0pt 0pt 4pt 0pt; border-color:#F0F0F0">--%>
    <table width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="background-color:#EFEFEF; margin:0pt 0pt 4pt 0pt; border-color:#F0F0F0">
	<%--<tr align="center" style="background-color:#99CCFF">--%>
    <tr align="center" style="background-color:#99CCFF">
		<td class="text9pt" style="width:30pt" rowspan="2"><font color="#FFFFFF">顯示<br />條件</font></td>
		<td class="text9pt" style="width:75pt"><font color="#FFFFFF">系統代號</font></td>
		<td class="text9pt" style="width:100pt"><font color="#FFFFFF">系統名稱</font></td>
		<td class="text9pt"><font color="#FFFFFF">資料庫名稱</font></td>
		<td class="text9pt" style="width:45pt"><font color="#FFFFFF">條件設定</font></td>
		<td class="text9pt" style="width:55pt"><font color="#FFFFFF">新增資料</font></td>
		<td class="text9pt" style="width:120pt" colspan="3"><font color="#FFFFFF">排序方式</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_ds_code" runat="server" Width="70pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_ds_name" runat="server" Width="94pt" MaxLength="20"></asp:TextBox></td>		
		<td><asp:TextBox ID="tb_ds_database" runat="server" Width="98%" MaxLength="100"></asp:TextBox></td>
		<td><asp:Button ID="btn_Set" runat="server" Text="設定" onclick="btn_Set_Click" /></td>
		<td><a href="javascript:madd()" class="abtn">&nbsp;新增資料&nbsp;</a></td>
		<td class="text9pt" style="width:32pt">
			<asp:LinkButton ID="lk_st_ds_code" runat="server" Text="&nbsp;代號&nbsp;" onclick="lk_st_ds_code_Click"></asp:LinkButton>
		</td>
		<td class="text9pt" style="width:32pt">
			<asp:LinkButton ID="lk_st_ds_name" runat="server" Text="&nbsp;名稱&nbsp;" onclick="lk_st_ds_name_Click" CssClass="abtn"></asp:LinkButton>
		</td>
		<td class="text9pt" style="width:40pt">
			<asp:LinkButton ID="lk_st_ds_database" runat="server" Text="&nbsp;資料庫&nbsp;" onclick="lk_st_ds_database_Click" CssClass="abtn"></asp:LinkButton>
		</td>
	</tr>
	</table>

	<asp:GridView ID="gv_Db_Sys" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="ds_sid" DataSourceID="ods_Db_Sys" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何資料庫規格文件！" 
		HorizontalAlign="Center" AllowSorting="True" ShowHeader="False" 
		onpageindexchanged="gv_Db_Sys_PageIndexChanged" ondatabound="gv_Db_Sys_DataBound" 
		onrowdatabound="gv_Db_Sys_RowDataBound">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
		<PagerSettings Visible="False" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="25pt" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:TemplateField HeaderText="系統規格名稱" ShowHeader="False">
			<ItemTemplate>
				<table border="1" cellpadding="4" cellspacing="0" style="width:100%; text-align:left; outline-style:double; outline-width:1pt">
				<tr style="height:12pt; background-color:#FFFFE0">
					<td style="width:36pt; background-color:#BBFF66; text-align:center">代　號</td>
					<td style="width:80pt"><asp:Label ID="lb_ds_code" runat="server" Text='<%# Eval("ds_code") %>'></asp:Label></td>
					<td style="width:36pt; background-color:#BBFF66; text-align:center">名　稱</td>
					<td style="width:150pt"><asp:Label ID="lb_ds_name" runat="server" Text='<%# Eval("ds_name") %>'></asp:Label></td>
					<td style="width:36pt; background-color:#BBFF66; text-align:center">資料庫</td> 
					<td><asp:Label ID="lb_ds_database" runat="server" Text='<%# Eval("ds_database") %>'></asp:Label></td>
					<td style="width:20px; background-color:#BBFF66; text-align:center"><img src="../images/button/down.gif" id="img_desc_show<%# Eval("ds_sid") %>" onclick="desc_show('<%# Eval("ds_sid") %>')" title="顯示備註說明" alt="顯示備註說明" /></td>
					<td style="width:100pt" align="center">
						<a href="javascript:mdetail('<%# Eval("ds_sid") %>')" class="abtn" title="查看資料庫表格">&nbsp;表格&nbsp;</a>&nbsp;
						<a href="javascript:medit('<%# Eval("ds_sid") %>')" class="abtn" title="修改資料">&nbsp;修改&nbsp;</a>&nbsp;
						<a href="javascript:mdel('<%# Eval("ds_sid") %>','<%# Eval("ds_name") %>','<%# Eval("ds_code") %>')" class="abtn" title="刪除資料">&nbsp;刪除&nbsp;</a>
					</td>
				</tr>
				<tr style="background-color:#FFFFE0; display:none" id="tr1<%# Eval("ds_sid") %>">
					<td style="background-color:#BBFF66; text-align:center">帳　號</td>
					<td><asp:Label ID="lb_ds_id" runat="server" Text='<%# Eval("ds_id") %>'></asp:Label></td>
					<td rowspan="2" style="background-color:#BBFF66; text-align:center">說明</td>
					<td rowspan="2" colspan="5"><asp:Label ID="lb_ds_desc" runat="server" Text='<%# Eval("ds_desc") %>'></asp:Label></td>
				</tr>
				<tr style="background-color:#FFFFE0; display:none" id="tr2<%# Eval("ds_sid") %>">
					<td style="background-color:#BBFF66; text-align:center">密　碼</td>
					<td><asp:Label ID="lb_ds_pass" runat="server" Text='<%# Eval("ds_pass") %>'></asp:Label></td>
				</tr>
				</table>
			</ItemTemplate>
			<ItemStyle Width="100%" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何資料庫規格文件！</EmptyDataTemplate>
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
		<td style="width:30px"><asp:ImageButton ID="ib_first" runat="server" ImageUrl="~/images/button/bn-first.gif" ToolTip="第一頁" AlternateText="第一頁" onclick="ib_first_Click" /></td>
		<td style="width:30px"><asp:ImageButton ID="ib_prev" runat="server" ImageUrl="~/images/button/bn-prev.gif" ToolTip="上一頁" AlternateText="上一頁" onclick="ib_prev_Click" /></td>
		<td>&nbsp;( 第 <%=gv_Db_Sys.PageIndex + 1 %> 頁 / 共 <%=gv_Db_Sys.PageCount %> 頁 )&nbsp;</td>
		<td style="width:30px"><asp:ImageButton ID="ib_next" runat="server" ImageUrl="~/images/button/bn-next.gif" ToolTip="下一頁" AlternateText="下一頁" onclick="ib_next_Click" /></td>
		<td style="width:30px"><asp:ImageButton ID="ib_last" runat="server" ImageUrl="~/images/button/bn-last.gif" ToolTip="最末頁" AlternateText="最末頁" onclick="ib_last_Click" /></td>		
	</tr>
	</table>
	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	
	<iframe name="update" id="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>

	<!-- Begin 隱藏視窗 -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	
	</center>
	</div>	
	</form>
</body>
</html>
