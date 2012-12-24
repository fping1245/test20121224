<%@ Page Language="C#" AutoEventWireup="true" CodeFile="90012.aspx.cs" Inherits="_90012" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>廣告信發送管理</title>
<script language="javascript" type="text/javascript" src="../js/innerWindow.js"></script>
<script language="javascript" type="text/javascript">
	function add_display() {
		var tbobj = document.getElementById("tbl_add");

		if (tbobj != null) {
			if (tbobj.style.display == "none")
				tbobj.style.display = "";
			else
				tbobj.style.display = "none";
		}
	}

	// 隱藏郵件清單
	function hide_Ad_List() {
		var divobj = document.getElementById("div_Ad_List");

		if (divobj != null)
			divobj.style.display = "none";
	}

	// 開始發送郵件
	function begin_send_mail() {
		var divobj = document.getElementById("div_process");
		var ifobj = document.getElementById("update");
		var batchobj = document.getElementById("tb_adm_batch");
		var fnameobj = document.getElementById("tb_adm_fname");
		var fmailobj = document.getElementById("tb_adm_fmail");
		var waitobj = document.getElementById("tb_adm_wait");
	
		// 隱藏郵件清單
		hide_Ad_List();

		if (divobj != null) {
			// 覆蓋整個工作頁面，不讓使用者再按其它按鍵
			show_fullwindow();

			// 顯示發送郵件進度視窗，並調整位置
			divobj.style.width = "400px";
			divobj.style.left = String((document.body.clientWidth - 400) / 2) + "px";
			divobj.style.top = "175px";
			divobj.style.display = "";

			// 開始發送郵件
			ifobj.src = "900121.ashx?adm_sid=<%=lb_adm_sid.Text%>&adm_batch=" + batchobj.value + "&adm_fname=" + fnameobj.value + "&adm_fmail=" + fmailobj.value + "&adm_wait=" + waitobj.value;
		}
	}

	// 設定進度表
	function send_process(mtotal, msend, merror) {
		var tobj = document.getElementById("sp_adm_total");
		var sobj = document.getElementById("sp_adm_send");
		var eobj = document.getElementById("sp_adm_error");
		var pobj = document.getElementById("sp_percent");
		var lobj = document.getElementById("tbl_process");
		var fcnt = 1.0, wcnt = 1;

		if (tobj != null && sobj != null && eobj != null && pobj != null && lobj != null)
		{
			if (mtotal == 0)
				fcnt = 0;
			else
				fcnt = (msend + merror) / mtotal * 100.0;
		
			tobj.innerHTML = mtotal;
			sobj.innerHTML = msend;
			eobj.innerHTML = merror;
			pobj.innerHTML = fcnt.toFixed(2);
			
			wcnt = fcnt * 4;
			
			if (wcnt < 1)
				wcnt = 1;
			else if (wcnt > 400)
				wcnt = 400;

			lobj.style.width = wcnt.toFixed() + "px";
		}
	}

	// 更新頁面
	function page_renew() {
		location.replace("90012.aspx<%=lb_page.Text%>");
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
	<p align="center" class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">發送處理</p>

	<table width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#f0f0f0; background-color:#F7F7DE">
	<tr align="left">
		<td class="text9pt" style="width:12%; text-align:center; background-color:#99FF99">郵件標題</td>
		<td class="text9pt" style="width:21%"><asp:Label ID="lb_adm_title" runat="server"></asp:Label></td>
		<td class="text9pt" style="width:12%; text-align:center; background-color:#99FF99">郵件格式</td>
		<td class="text9pt" style="width:21%"><asp:Label ID="lb_adm_type" runat="server"></asp:Label></td>
		<td class="text9pt" style="width:12%; text-align:center; background-color:#99FF99">最後發送時間</td>
		<td class="text9pt"><asp:Label ID="lb_send_time" runat="server"></asp:Label></td>
	</tr>
	<tr align="left">
		<td class="text9pt" style="text-align:center; background-color:#99FF99">預計發送數量</td>
		<td class="text9pt"><asp:Label ID="lb_adm_total" runat="server"></asp:Label></td>
		<td class="text9pt" style="text-align:center; background-color:#99FF99">目前發送數量</td>
		<td class="text9pt"><asp:Label ID="lb_adm_send" runat="server"></asp:Label></td>
		<td class="text9pt" style="text-align:center; background-color:#99FF99">郵箱錯誤數量</td>
		<td class="text9pt"><asp:Label ID="lb_adm_error" runat="server"></asp:Label></td>
	</tr>
	<tr align="left">
		<td class="text9pt" style="text-align:center; background-color:#99FF99">發信者姓名/郵箱</td>
		<td class="text9pt" colspan="5">
			<asp:TextBox ID="tb_adm_fname" runat="server" MaxLength="50" Width="150px"></asp:TextBox>&nbsp;/&nbsp;
			<asp:TextBox ID="tb_adm_fmail" runat="server" MaxLength="100" Width="350px"></asp:TextBox>
		</td>
	</tr>
	<tr align="left">	
		<td class="text9pt" style="text-align:center; background-color:#99FF99">每批發送數量</td>
		<td class="text9pt"><asp:TextBox ID="tb_adm_batch" runat="server" MaxLength="2" Width="40px" Text="10"></asp:TextBox>&nbsp;封</td>
		<td class="text9pt" style="text-align:center; background-color:#99FF99">每批發送遲延</td>
		<td class="text9pt"><asp:TextBox ID="tb_adm_wait" runat="server" MaxLength="2" Width="40px" Text="10"></asp:TextBox>&nbsp;秒</td>
		<td class="text9pt" style="text-align:center; width:100px; background-color:#99FF99">開始發送郵件</td>
		<td class="text9pt" align="left">&nbsp;<input type="button" id="bn_send" value="開始發送" onclick="begin_send_mail()" /></td>
	</tr>
	</table>
	<p style="margin:5px 0px 0px 0px; width:98%; text-align:left">※ 避免IP被鎖定成垃圾信來源，建議每批發送數量不可超過20封。每批遲延秒數的用意也在於此。</p>
	
	<div id="div_Ad_List">
	<p style="margin:10px 0px 0px 0px; text-align:center; font-family:標楷體; font-size:14pt">電子信箱發送名單</p>
	<hr style="width:98%" />
	<table width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#f0f0f0; background-color:#EFEFEF">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" width="40" rowspan="2"><font color="#FFFFFF">顯示<br />條件</font></td>
		<td class="text9pt"><font color="#FFFFFF">電子郵件信箱</font></td>
		<td class="text9pt"><font color="#FFFFFF">最後發信時間範圍</font></td>
		<td class="text9pt" width="70"><font color="#FFFFFF">條件設定</font></td>
		<td class="text9pt" width="80"><font color="#FFFFFF">匯入郵箱</font></td>
		<td class="text9pt" width="80"><font color="#FFFFFF">重新發送</font></td>
		<td class="text9pt" width="80"><font color="#FFFFFF">新增資料</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_adl_email" runat="server" Width="80pt" MaxLength="12"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_ibtime" runat="server" Width="75pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分:秒 (yyyy/MM/dd HH:mm:ss)"></asp:TextBox>&nbsp;～&nbsp;
			<asp:TextBox ID="tb_ietime" runat="server" Width="75pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分:秒 (yyyy/MM/dd) HH:mm:ss"></asp:TextBox>
		</td>
		<td><asp:Button ID="Btn_Set" runat="server" Text="設定" onclick="Btn_Set_Click" /></td>
		<td><asp:LinkButton ID="lk_import" runat="server" CssClass="abtn" ToolTip="由會員名單匯入電子郵件信箱" 
				Text="&nbsp;匯入信箱&nbsp;" onclick="lk_import_Click" OnClientClick="show_msg_wait()"></asp:LinkButton></td>
		<td><asp:LinkButton ID="lk_renew" runat="server" CssClass="abtn" ToolTip="清除全部郵件信箱的已發送旗標"
				Text="&nbsp;重設旗標&nbsp;" OnClientClick="show_msg_wait()" onclick="lk_renew_Click"></asp:LinkButton></td>
		<td><a href="javascript:add_display()" class="abtn">&nbsp;新增資料&nbsp;</a></td>
	</tr>
	</table>
	
	<table id="tbl_add" width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="margin:0pt 0pt 5pt 0pt; border-color:#f0f0f0; background-color:#F7F7DE; display:none">
	<tr align="left">
		<td class="text9pt" style="text-align:center; width:120px; background-color:#99CCFF; color:#FFFFFF">新增資料</td>
		<td class="text9pt" style="text-align:center; width:100px; background-color:#99FF99">電子郵件信箱</td>
		<td class="text9pt"><asp:TextBox ID="tb_email" runat="server" Width="250px" MaxLength="100"></asp:TextBox></td>
		<td class="text9pt" style="text-align:center; width:80px; background-color:#99FF99">
			<asp:Button ID="tb_save" runat="server" CssClass="text9pt" Text="確定存檔" onclick="tb_save_Click" />
		</td>
	</tr>
	</table>

	<asp:GridView ID="gv_Ad_List" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="adl_sid" DataSourceID="ods_Ad_List" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何會員的資料！" 
		HorizontalAlign="Center" onpageindexchanging="gv_Ad_List_PageIndexChanged" 
		AllowSorting="True" onrowdatabound="gv_Ad_List_RowDataBound" 
		onrowupdating="gv_Ad_List_RowUpdating" ondatabound="gv_Ad_List_DataBound" 
		onrowdeleted="gv_Ad_List_RowDeleted">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="rownum" HeaderText="順序" InsertVisible="False" ReadOnly="True">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="是否已發送" SortExpression="adl_send">
			<ItemTemplate>
				<asp:Label ID="lb_adl_send" runat="server"></asp:Label>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:RadioButton ID="rb_adl_send0" runat="server" Text="未發送" GroupName="rb_adl_send"/>
				<asp:RadioButton ID="rb_adl_send1" runat="server" Text="已發送" GroupName="rb_adl_send" />
			</EditItemTemplate>
		</asp:TemplateField>
		<asp:BoundField DataField="adl_email" HeaderText="電子郵件信箱" SortExpression="adl_email" />
		<asp:BoundField DataField="send_time" 
			DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" HeaderText="發送時間" ReadOnly="True" 
			SortExpression="send_time">
		<HeaderStyle HorizontalAlign="Center" Width="100pt" />
		<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="編輯" ShowHeader="False">
			<ItemTemplate>
				<asp:Button ID="Button1" runat="server" CausesValidation="False" 
					CommandName="Edit" Text="修改" />
				&nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
					CommandName="Delete" onclientclick="return confirm('確定要刪除此筆資料？')" Text="刪除" />
			</ItemTemplate>
			<EditItemTemplate>
				<asp:Button ID="Button1" runat="server" CausesValidation="True" 
					CommandName="Update" onclientclick="return confirm('確定要更新此筆資料？')" Text="更新" />
				&nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
					CommandName="Cancel" Text="取消" />
			</EditItemTemplate>
			<HeaderStyle Width="90px" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何會員的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>
	</div>

	<p style=" margin:10px 0px 10px 0px; text-align:center"><a href="9001.aspx<%=lb_page.Text %>" class="abtn">&nbsp;回上一頁&nbsp;</a></p>&nbsp;
	
	<asp:ObjectDataSource ID="ods_Ad_List" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Ad_List" SelectMethod="Select_Ad_List"
			SortParameterName="SortColumn" TypeName="ODS_Ad_List_DataAccess" 
			DeleteMethod="Delete_Ad_List" InsertMethod="Insert_Ad_List" 
			UpdateMethod="Update_Ad_List" ondeleted="ods_Ad_List_Deleted" 
			oninserted="ods_Ad_List_Inserted" onupdated="ods_Ad_List_Updated" 
			oninserting="ods_Ad_List_Inserting" onupdating="ods_Ad_List_Updating" 
			ondeleting="ods_Ad_List_Deleting">
		<DeleteParameters>
			<asp:Parameter Name="adl_sid" Type="Int32" />
		</DeleteParameters>
		<UpdateParameters>
			<asp:Parameter Name="adm_sid" Type="Int32" />
			<asp:Parameter Name="adl_sid" Type="Int32" />
			<asp:Parameter Name="adl_email" Type="String" />
			<asp:Parameter Name="adl_send" Type="Int32" />
		</UpdateParameters>
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="adm_sid" Type="String" />
			<asp:Parameter Name="adl_sid" Type="String" />
			<asp:Parameter Name="adl_email" Type="String" />
			<asp:Parameter Name="adl_send" Type="String" />
			<asp:Parameter Name="btime" Type="String" />
			<asp:Parameter Name="etime" Type="String" />
		</SelectParameters>
		<InsertParameters>
			<asp:Parameter Name="adm_sid" Type="Int32" />
			<asp:Parameter Name="adl_email" Type="String" />
			<asp:Parameter Name="adl_send" Type="Int32" />
		</InsertParameters>
	</asp:ObjectDataSource>
	<asp:Label ID="lb_pageid1" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_adm_sid" runat="server" Text="0" Visible="false"></asp:Label>
	<asp:Label ID="lb_adl_send" runat="server" Text="0" Visible="false"></asp:Label>
	
	<iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
	</center>

	<!-- Begin 發送郵件進度視窗 -->
	<div id="div_process" style="position: absolute; top:0px; left:0px; display:none">
	<table border="1" cellpadding="0" cellspacing="0" style="width:400px; background-color:#E0FFFF">
	<tr><td align="left" colspan="2" style="height:24px">&nbsp;&nbsp;預計發送郵件：<span id="sp_adm_total"></span></td></tr>
	<tr><td align="left" colspan="2" style="height:24px">&nbsp;&nbsp;正確發送郵件：<span id="sp_adm_send"></span></td></tr>
	<tr><td align="left" style="height:24px">&nbsp;&nbsp;郵件格式錯誤：<span id="sp_adm_error"></span></td>
		<td align="right"><span id="sp_percent"></span>%&nbsp;&nbsp;</td>
	</tr>
	<tr><td colspan="2" style="height:5px"></td></tr>
	<tr><td align="left" colspan="2" style="height:10px; background-color:#BBFFEE">
			<table id="tbl_process" border="0" cellpadding="0" cellspacing="0" style="margin:0px 0px 0px 0px; width:1px; background-color:#00FF00">
			<tr><td style="height:10px"></td></tr>
			</table>
		</td>
	</tr>
	<tr><td colspan="2" style="height:5px"></td></tr>
	</table>
	<p style="margin: 10px 0px 0px 0px; text-align:center"><a href="javascript:page_renew()" class="abtn">&nbsp;中斷發送&nbsp;</a></p>&nbsp;
	</div>
	<!-- End -->
	
	<!-- Begin 顯示資料處理訊息 -->
	<div id="div_ban" class="text9pt" style="position:absolute; width:240px; z-index:0; display:none">
	<table id="tb_ban" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_ban.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<div id="div_msg_wait" class="text9pt" style="position:absolute; width:240px; z-index:0; display:none">
	<table border="2" cellpadding="2" cellspacing="2" style="width:240px; height:100px; background-color:#efefef">
	<tr><td align="center" class="text9pt">資料處理中 ........<br />請勿按任何按鍵!</td></tr>
	</table>
	</div>
	<!-- End -->
	</div>
	</form>
</body>
</html>
