<%@ Page Language="C#" AutoEventWireup="true" CodeFile="D002.aspx.cs" Inherits="_D002" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>論壇管理</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	// 修改討論主題	(避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function medit(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("D0021.aspx?sid=" + msid + "&timestamp=" + timestamp, 680, 400);
	}

	// 刪除討論主題
	function mdel(msid, mtitle, mtime)
	{
		if (confirm("確定要刪除「" + mtitle.replace(/ /g, "") + "(" + mtime + ")」的討論資料?\n"))
		{
			update.location.replace("D0022.ashx?sid=" + msid);
		}
	}

	// 查看回應明細
	function mdetail(msid)
	{
		var mhref = "";

		mhref += "D0023.aspx?sid=" + msid;
		mhref += "&pageid=<%=gv_Fm_Forum.PageIndex.ToString()%>";
		mhref += "&ff_topic=<%=Server.UrlEncode(tb_ff_topic.Text)%>";
		mhref += "&ff_desc=<%=Server.UrlEncode(tb_ff_desc.Text)%>";
		mhref += "&btime=<%=Server.UrlEncode(tb_btime.Text)%>";
		mhref += "&etime=<%=Server.UrlEncode(tb_etime.Text)%>";
		mhref += "&ff_name=<%=Server.UrlEncode(tb_ff_name.Text)%>";
	
		location.replace(mhref);
	}
	
	// 查看主題內容
	function desc_show(msid)
	{
		var dobj = document.getElementById("desc" + msid);
		var iobj = document.getElementById("img_desc_show" + msid);

		if (dobj != null && iobj != null)
		{
			if (dobj.style.display != "none")
			{
				dobj.style.display = "none";
				iobj.src = "../images/button/down.gif";
				iobj.title = "查看主題內容";
				iobj.alt = "查看主題內容";
			}
			else
			{
				dobj.style.display = "";
				iobj.src = "../images/button/up.gif";
				iobj.title = "隱藏主題內容";
				iobj.alt = "隱藏主題內容"; 
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
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">論壇(管理)</p>
	<table width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="background-color:#EFEFEF; margin:0pt 0pt 5pt 0pt; border-color:#F0F0F0">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" style="width:30pt" rowspan="2"><font color="#FFFFFF">顯示<br />條件</font></td>
		<td class="text9pt"><font color="#FFFFFF">討論主題</font></td>
		<td class="text9pt"><font color="#FFFFFF">主題內容</font></td>
		<td class="text9pt" style="width:170pt"><font color="#FFFFFF">發起時間</font></td>
		<td class="text9pt" style="width:75pt"><font color="#FFFFFF">姓名</font></td>
		<td class="text9pt" style="width:45pt"><font color="#FFFFFF">條件設定</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_ff_topic" runat="server" Width="98%" MaxLength="100"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_ff_desc" runat="server" Width="98%" MaxLength="100"></asp:TextBox></td>		
		<td><asp:TextBox ID="tb_btime" runat="server" Width="70pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分 (yyyy/MM/dd HH:mm)"></asp:TextBox>
			&nbsp;～&nbsp;
			<asp:TextBox ID="tb_etime" runat="server" Width="70pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分 (yyyy/MM/dd) HH:mm"></asp:TextBox>
		</td>
		<td><asp:TextBox ID="tb_ff_name" runat="server" Width="70pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:Button ID="btn_Set" runat="server" Text="設定" onclick="btn_Set_Click" /></td>
	</tr>
	</table>

	<asp:GridView ID="gv_Fm_Forum" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="ff_sid" DataSourceID="ods_Fm_Forum" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何討論主題！" 
		HorizontalAlign="Center"
		AllowSorting="True" onrowdatabound="gv_Fm_Forum_RowDataBound" ShowHeader="False" 
		onpageindexchanged="gv_Fm_Forum_PageIndexChanged">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="25pt" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:TemplateField HeaderText="討論區" ShowHeader="False">
				<ItemTemplate>
				<table border="1" cellpadding="0" cellspacing="0" style="width:100%; text-align:left; outline-style:double; outline-width:1pt">
					<tr style="height:18pt; background-color:#FFFFE0">
						<td><table border="0" cellpadding="4" cellspacing="0" width="100%">
							<tr><td align="center" style="width:50px; background-color:#FFDD55">
									<asp:Label ID="lb_is_show" runat="server" Text='<%# Eval("is_show") %>'></asp:Label>
								</td>
								<td align="center" style="width:50px; background-color:#FFEE99">
									<asp:Label ID="lb_is_close" runat="server" Text='<%# Eval("is_close") %>'></asp:Label>
								</td>
								<td align="left" style="width:100px">
									<asp:Image ID="img_ff_symbol" runat="server" ImageUrl="~/images/symbol/S00.gif" />
									<asp:Image ID="img_ff_sex" runat="server" ImageUrl="~/images/symbol/woman.gif" ToolTip='<%# Eval("ff_sex") %>' />
									<img src="../images/button/bn01.gif" onclick="mdetail(<%# Eval("ff_sid") %>)" title="查看回應內容" alt="查看回應內容" />
								</td>
								<td align="center" style="width:80pt">〔回應篇數：<%# Eval("ff_response") %>〕</td>
								<td align="left">
									<asp:Label ID="lb_ff_name" runat="server" Text='<%# Eval("ff_name") %>'></asp:Label>&nbsp;
									(<a href="mailto:<%# Eval("ff_email") %>"><%# Eval("ff_email") %></a>)
								</td>
								<td align="right">
									IP:<asp:Label ID="lb_ff_ip" runat="server" Text='<%# Eval("ff_ip") %>'></asp:Label>&nbsp;｜&nbsp;
									<asp:Label ID="lb_ff_time" runat="server" Text='<%# Eval("ff_time") %>'></asp:Label>
								</td>
								<td align="right" style="width:80pt; vertical-align:top; font-size:12pt">
									<a href="javascript:medit('<%# Eval("ff_sid") %>')" class="abtn">&nbsp;修改&nbsp;</a>&nbsp;
									<a href="javascript:mdel('<%# Eval("ff_sid") %>','<%# Eval("ff_name") %>','<%# Eval("ff_time") %>')" class="abtn">&nbsp;刪除&nbsp;</a>
								</td>
							</tr>
							</table>
						</td>
					</tr>
					<tr style="background-color:#FFFFE0">
						<td><table border="0" cellpadding="4" cellspacing="0" width="100%">
							<tr><td valign="top" style="width:25pt; background-color:#FFFFE0; text-align:center">主題</td>
								<td valign="top" style="width:20pt; background-color:#FFFFFF; text-align:center">
									<img src="../images/button/down.gif" id="img_desc_show<%# Eval("ff_sid") %>" onclick="desc_show('<%# Eval("ff_sid") %>')" title="查看主題內容" alt="查看主題內容" />
								</td>
								<td style="background-color:#FFFFFF"><asp:Label ID="lb_ff_topic" runat="server" Text='<%# Eval("ff_topic") %>'></asp:Label></td>
							</tr>
							</table>
						</td>
					</tr>
					<tr style="background-color:#FFFFE0; display:none" id="desc<%# Eval("ff_sid") %>">
						<td><table border="0" cellpadding="4" cellspacing="0" width="100%">
							<tr><td valign="top" style="width:25pt; background-color:#FFFFE0; text-align:center">內容</td>
								<td style="background-color:#FFFFFF"><asp:Label ID="lb_ff_desc" runat="server" Text='<%# Eval("ff_desc") %>'></asp:Label></td>
							</tr>
							</table>
						</td>
					</tr>
					</table>

				</ItemTemplate>
				<ItemStyle Width="100%" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何討論主題！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>

	<asp:ObjectDataSource ID="ods_Fm_Forum" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Fm_Forum" SelectMethod="Select_Fm_Forum"
			SortParameterName="SortColumn" TypeName="ODS_Fm_Forum_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="is_close" Type="String" />
			<asp:Parameter Name="ff_name" Type="String" />
			<asp:Parameter Name="ff_topic" Type="String" />
			<asp:Parameter Name="ff_desc" Type="String" />
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
