<%@ Page Language="C#" AutoEventWireup="true" CodeFile="C002.aspx.cs" Inherits="_C002" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>留言板管理</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// 修改留言	(避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("C0021.aspx?sid=" + msid + "&timestamp=" + timestamp, 680, 400);
	}

	// 刪除留言
	function mdel(msid, mtitle, mtime)
	{
		if (confirm("確定要刪除「" + mtitle.replace(/ /g, "") + "(" + mtime + ")」的留言資料?\n"))
		{
			update.location.replace("C0022.ashx?sid=" + msid);
		}
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
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">留言板(管理)</p>
	<table width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="background-color:#EFEFEF; margin:0pt 0pt 5pt 0pt; border-color:#F0F0F0">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" style="width:30pt" rowspan="2"><font color="#FFFFFF">顯示<br />條件</font></td>
		<td class="text9pt"><font color="#FFFFFF">留言內容</font></td>
		<td class="text9pt" style="width:170pt"><font color="#FFFFFF">留言時間</font></td>
		<td class="text9pt" style="width:75pt"><font color="#FFFFFF">姓名</font></td>
		<td class="text9pt" style="width:95pt"><font color="#FFFFFF">郵件信箱</font></td>
		<td class="text9pt" style="width:45pt"><font color="#FFFFFF">條件設定</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_mb_desc" runat="server" Width="98%" MaxLength="100"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_btime" runat="server" Width="70pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分 (yyyy/MM/dd HH:mm)"></asp:TextBox>
			&nbsp;～&nbsp;
			<asp:TextBox ID="tb_etime" runat="server" Width="70pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分 (yyyy/MM/dd) HH:mm"></asp:TextBox>
		</td>
		<td><asp:TextBox ID="tb_mb_name" runat="server" Width="70pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_mb_email" runat="server" Width="90pt" MaxLength="100"></asp:TextBox></td>
		<td><asp:Button ID="btn_Set" runat="server" Text="設定" onclick="btn_Set_Click" /></td>
	</tr>
	</table>

	<asp:ListView ID="lv_Ms_Board" runat="server" DataSourceID="ods_Ms_Board" 
			DataKeyNames="mb_sid" GroupItemCount="10" 
			onitemdatabound="lv_Ms_Board_ItemDataBound">
		<LayoutTemplate>
			<table id="tbl_lv" runat="server" border="0" cellpadding="0" cellspacing="6" style="width:98%">
				<tr id="groupPlaceholder" runat="server"></tr>
				<tr id="tr_page" runat="server">
					<td id="td_page" runat="server" align="center">
						<asp:DataPager ID="dp_Ms_Board" runat="server" PagedControlID="lv_Ms_Board">
							<Fields>
								<asp:NextPreviousPagerField PreviousPageText="[上一頁]" ShowNextPageButton="false"  />
								<asp:NumericPagerField PreviousPageText="<<" NextPageText=">>" ButtonCount="10" />
								<asp:NextPreviousPagerField NextPageText="[下一頁]" ShowPreviousPageButton="false" />
							</Fields>
						</asp:DataPager>
					</td>
				</tr>
			</table>
		</LayoutTemplate>
		<GroupTemplate>
			<tr runat="server" id="itemPlaceholder"></tr>
		</GroupTemplate>
		<ItemTemplate>
			<tr><td><table border="1" cellpadding="4" cellspacing="0" style="width:100%; text-align:left; outline-style:double; outline-width:1pt">
					<tr style="height:20pt; background-color:#FFFFE0">
						<td><table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr><td align="center" style="width:50px; background-color:#FFDD55">
									<asp:Label ID="lb_is_show" runat="server" Text='<%# Eval("is_show") %>'></asp:Label>
								</td>
								<td align="center" style="width:50px; background-color:#FFEE99">
									<asp:Label ID="lb_is_close" runat="server" Text='<%# Eval("is_close") %>'></asp:Label>
								</td>
								<td align="left" style="width:50px">
									<asp:Image ID="img_mb_symbol" runat="server" ImageUrl="~/images/symbol/S00.gif" />
									<asp:Image ID="img_mb_sex" runat="server" ImageUrl="~/images/symbol/woman.gif" ToolTip='<%# Eval("mb_sex") %>' />
								</td>
								<td align="left">
									<asp:Label ID="lb_mb_name" runat="server" Text='<%# Eval("mb_name") %>'></asp:Label>
									(<a href="mailto:<%# Eval("mb_email") %>"><%# Eval("mb_email") %></a>)
								</td>
								<td align="right">
									IP:<asp:Label ID="lb_mb_ip" runat="server" Text='<%# Eval("mb_ip") %>'></asp:Label>&nbsp;｜&nbsp;
									<asp:Label ID="lb_mb_time" runat="server" Text='<%# Eval("mb_time") %>'></asp:Label>
								</td>
								<td align="right" style="width:80pt; vertical-align:top; font-size:12pt">
									<a href="javascript:medit('<%# Eval("mb_sid") %>')" class="abtn">&nbsp;修改&nbsp;</a>&nbsp;
									<a href="javascript:mdel('<%# Eval("mb_sid") %>','<%# Eval("mb_name") %>','<%# Eval("mb_time") %>')" class="abtn">&nbsp;刪除&nbsp;</a>
								</td>
							</tr>
							</table>
						</td>
					</tr>
					<tr style="height:20px; background-color:#FFFFFF">
						<td><asp:Label ID="lb_mb_desc" runat="server" Text='<%# Eval("mb_desc") %>'></asp:Label></td>
					</tr>
					</table>
				</td>
			</tr>
		</ItemTemplate>
		<EmptyDataTemplate>
			<table runat="server" style="">
				<tr><td>目前尚無任何資料</td></tr>
			</table>
		</EmptyDataTemplate>
	</asp:ListView>

	<asp:ObjectDataSource ID="ods_Ms_Board" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Ms_Board" SelectMethod="Select_Ms_Board"
			SortParameterName="SortColumn" TypeName="ODS_Ms_Board_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="is_close" Type="String" />
			<asp:Parameter Name="mb_name" Type="String" />
			<asp:Parameter Name="mb_email" Type="String" />
			<asp:Parameter Name="mb_desc" Type="String" />
			<asp:Parameter Name="btime" Type="String" />
			<asp:Parameter Name="etime" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>

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
