<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B004.aspx.cs" Inherits="_B004" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>考試成績統計</title>

<script language="javascript" type="text/javascript">
	// 考生成績分佈 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function muser(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		var mhref = "B0041.aspx?sid=" + msid;

		mhref += "&pageid=<%=lb_pageid.Text%>&tp_sid=<%=tb_tp_sid.Text%>&tp_title=<%=Server.UrlEncode(tb_tp_title.Text)%>";
		mhref += "&is_show=<%=lb_is_show.Text%>&btime=<%=Server.UrlEncode(tb_btime.Text)%>&etime=<%=Server.UrlEncode(tb_etime.Text)%>";
		mhref += "&timestamp=" + timestamp;

		location.href = mhref;
	}

	// 試題成績分佈 (避免 IE Cache 的作用，後面加上 timestamp 的變數，實際上沒有用到)
	function mquest(msid)
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();

		var mhref = "B0042.aspx?sid=" + msid;

		mhref += "&pageid=<%=lb_pageid.Text%>&tp_sid=<%=tb_tp_sid.Text%>&tp_title=<%=Server.UrlEncode(tb_tp_title.Text)%>";
		mhref += "&is_show=<%=lb_is_show.Text%>&btime=<%=Server.UrlEncode(tb_btime.Text)%>&etime=<%=Server.UrlEncode(tb_etime.Text)%>";
		mhref += "&timestamp=" + timestamp;

		location.href = mhref;
	}
</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<p class="text18pt" style="margin:10pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">考試成績統計</p>
	<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">試卷清單</p>
	<table width="98%" border="1" cellspacing="0" cellpadding="4" class="text9pt" style="background-color:#EFEFEF; margin:0pt 0pt 5pt 0pt; border-color:#F0F0F0">
	<tr align="center" bgcolor="#99CCFF">
		<td class="text9pt" width="40" rowspan="2"><font color="#FFFFFF">顯示<br />條件</font></td>
		<td class="text9pt"><font color="#FFFFFF">編號</font></td>
		<td class="text9pt"><font color="#FFFFFF">試卷標題</font></td>
		<td class="text9pt"><font color="#FFFFFF">是否顯示</font></td>
		<td class="text9pt"><font color="#FFFFFF">考試開放時間</font></td>
		<td class="text9pt" width="55"><font color="#FFFFFF">條件設定</font></td>
	</tr>
	<tr align="center">
		<td><asp:TextBox ID="tb_tp_sid" runat="server" Width="15pt" MaxLength="5"></asp:TextBox></td>
		<td><asp:TextBox ID="tb_tp_title" runat="server" Width="160pt" MaxLength="50"></asp:TextBox></td>
		<td><asp:RadioButton ID="rb_is_show0" runat="server" Text="隱藏" CssClass="text9pt" GroupName="rb_is_show" />
			<asp:RadioButton ID="rb_is_show1" runat="server" Text="顯示" CssClass="text9pt" GroupName="rb_is_show" />
			<asp:RadioButton ID="rb_is_show_all" runat="server" Text="全部" CssClass="text9pt" GroupName="rb_is_show" />
		</td>
		<td><asp:TextBox ID="tb_btime" runat="server" Width="70pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分 (yyyy/MM/dd HH:mm)"></asp:TextBox>
			&nbsp;～&nbsp;
			<asp:TextBox ID="tb_etime" runat="server" Width="70pt" MaxLength="19" ToolTip="請輸入 西元年/月/日 時:分 (yyyy/MM/dd) HH:mm"></asp:TextBox>
		</td>
		<td><asp:Button ID="btn_Set" runat="server" Text="設定" onclick="btn_Set_Click" /></td>
	</tr>
	</table>
	<asp:GridView ID="gv_Ts_Paper" runat="server" AutoGenerateColumns="False" 
		DataKeyNames="tp_sid" DataSourceID="ods_Ts_Paper" AllowPaging="True" 
		BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" 
		CellPadding="4" Width="98%" EmptyDataText="沒有任何試卷的資料！" 
		HorizontalAlign="Center" onpageindexchanging="gv_Ts_Paper_PageIndexChanged" 
		AllowSorting="True" onrowdatabound="gv_Ts_Paper_RowDataBound">
	<HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
	<RowStyle BackColor="#F7F7DE" />
	<EmptyDataRowStyle BackColor="#FFFFCC" Height="25pt" HorizontalAlign="Center" VerticalAlign="Middle" />
	<Columns>
		<asp:BoundField DataField="tp_sid" HeaderText="編號" InsertVisible="False" ReadOnly="True" SortExpression="tp_sid">
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_title" HeaderText="試卷主題" SortExpression="tp_title" >
			<ItemStyle HorizontalAlign="Left" />
		</asp:BoundField>
		<asp:BoundField DataField="is_show" HeaderText="顯示" SortExpression="is_show">
			<HeaderStyle HorizontalAlign="Center" Width="25pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="b_time" HeaderText="開放進入" SortExpression="b_time" DataFormatString="{0:yyyy/MM/dd HH:mm}">
			<HeaderStyle HorizontalAlign="Center" Width="65pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="e_time" HeaderText="截止進入" SortExpression="e_time" DataFormatString="{0:yyyy/MM/dd HH:mm}">
			<HeaderStyle HorizontalAlign="Center" Width="65pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_question" HeaderText="試題總數" SortExpression="tp_question" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_member" HeaderText="參加人數" SortExpression="tp_member" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_score" HeaderText="總分" SortExpression="tp_score" DataFormatString="{0:N0}">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Center" />
		</asp:BoundField>
		<asp:BoundField DataField="tp_total" HeaderText="總平均" SortExpression="tp_total" DataFormatString="{0:F4}">
			<HeaderStyle HorizontalAlign="Center" Width="40pt" />
			<ItemStyle HorizontalAlign="Right" />
		</asp:BoundField>
		<asp:TemplateField HeaderText="功能" ShowHeader="False">
				<ItemTemplate>
					<a href="javascript:muser(<%# Eval("tp_sid") %>)" class="abtn" title="考生成績分佈">&nbsp;成績&nbsp;</a>&nbsp;
					<a href="javascript:mquest(<%# Eval("tp_sid") %>)" class="abtn" title="試題成績分佈">&nbsp;試題&nbsp;</a>&nbsp;
				</ItemTemplate>
				<HeaderStyle Width="70pt" HorizontalAlign="Center" />
				<ItemStyle Height="18pt" HorizontalAlign="Center" />
		</asp:TemplateField>
	</Columns>
	<EmptyDataTemplate>沒有任何試卷的資料！</EmptyDataTemplate>
	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
	<AlternatingRowStyle BackColor="White" />
	</asp:GridView>

	<asp:ObjectDataSource ID="ods_Ts_Paper" runat="server" EnablePaging="True" 
			SelectCountMethod="GetCount_Ts_Paper" SelectMethod="Select_Ts_Paper"
			SortParameterName="SortColumn" TypeName="ODS_Ts_Paper_DataReader">
		<SelectParameters>
			<asp:Parameter Name="SortColumn" Type="String" />
			<asp:Parameter Name="startRowIndex" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" />
			<asp:Parameter Name="tp_sid" Type="String" />
			<asp:Parameter Name="tp_title" Type="String" />
			<asp:Parameter Name="is_show" Type="String" />
			<asp:Parameter Name="btime" Type="String" />
			<asp:Parameter Name="etime" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>

	<p style="margin:5pt 0pt 0pt 0pt; text-align:left; width:98%">※ 「線上考試」如採用「自由參加」的方式，不必先建立考生資料，考生在參加考試時填入資料即可。</p>
	<p style="margin:2pt 0pt 0pt 0pt; text-align:left; width:98%">※ 「線上考試」如採用「限定身份」的方式，要在「紀錄」功能裡先建立考生資料，參加考試時要填入正確的身份資料，才允許進入考試。</p>

	<asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_is_show" runat="server" Text="" Visible="false"></asp:Label>
	</center>
	</div>
	</form>
</body>
</html>
