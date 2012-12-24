<%@ Page Language="C#" AutoEventWireup="true" CodeFile="60021.aspx.cs" Inherits="_60021" %>
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
	<p align="center" class="text16pt" style="margin:0pt 0pt 5pt 0pt; font-family:標楷體;">詳細內容</p>
	<center>
	<table cellspacing="0" cellpadding="4" border="0" style="width:580pt; margin:5pt 0pt 5pt 0pt">
	<tr><td align="right">
			<a href="60021_add.aspx<%=lb_page.Text%>" class="abtn" title="新增連絡人資料">&nbsp;新增資料&nbsp;</a>&nbsp;&nbsp;
			<a href="60021_edit.aspx<%=lb_page.Text%>&sid=<%=lb_ab_sid.Text%>" class="abtn" title="修改連絡人資料">&nbsp;修改內容&nbsp;</a>&nbsp;&nbsp;
			<a href="60021_photo.aspx<%=lb_page.Text%>&sid=<%=lb_ab_sid.Text%>" class="abtn" title="相片處理">&nbsp;相片處理&nbsp;</a>&nbsp;&nbsp;
			<a href="javascript:mdel()" class="abtn" title="刪除連絡人資料">&nbsp;刪除資料&nbsp;</a>&nbsp;&nbsp;
			<a href="6002.aspx<%=lb_page.Text%>" class="abtn">&nbsp;回瀏覽頁&nbsp;</a>
		</td>
	</tr>
	</table>
	<table cellspacing="0" cellpadding="4" border="1" style="background-color:White;border-color:#003366;border-width:1px;border-style:Double;width:580pt;border-collapse:collapse;">
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" style="width:90pt">通訊錄編號</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:Label ID="lb_ab_sid" runat="server"></asp:Label>
			</td>
			<td align="center" valign="middle" colspan="2" rowspan="4" style="background-color:#F7F7DE">
				<asp:Image ID="img_ab_photo" ImageUrl="~/images/ico/no_photo.gif" Width="120" Height="120" BorderWidth="1px" runat="server" />
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center" style="width:90pt">群組</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:Label ID="lb_ag_name" runat="server"></asp:Label>
			</td>
		</tr>
		<tr align="left" style="width:90pt; background-color:#99FF99;height:18pt">
			<td align="center">姓名</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:Label ID="lb_ab_name" runat="server"></asp:Label>
			</td>
		</tr>
		<tr align="left" style="width:90pt; background-color:#99FF99;height:18pt">
			<td align="center">暱稱</td>
			<td style="width:200pt; background-color:#F7F7DE">
				<asp:Label ID="lb_ab_nike" runat="server"></asp:Label>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center">住址</td>
			<td style="background-color:#F7F7DE" colspan="3">郵遞區號：
				<asp:Label ID="lb_ab_zipcode" runat="server" Width="40pt"></asp:Label><br />
				<asp:Label ID="lb_ab_address" runat="server"></asp:Label>
			</td>
		</tr>
		<tr align="left" style="width:90pt; background-color:#99FF99;height:18pt">
			<td align="center">住家電話</td>
			<td style="background-color:#F7F7DE">
				<asp:Label ID="lb_ab_tel_h" runat="server"></asp:Label>
			</td>
			<td align="center">辦公電話</td>
			<td style="width:90pt; background-color:#F7F7DE">
				<asp:Label ID="lb_ab_tel_o" runat="server"></asp:Label>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center">行動電話</td>
			<td style="background-color:#F7F7DE">
				<asp:Label ID="lb_ab_mobil" runat="server"></asp:Label>
			</td>
			<td align="center">傳真號碼</td>
			<td style="background-color:#F7F7DE">
				<asp:Label ID="lb_ab_fax" runat="server"></asp:Label>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center">電子信箱</td>
			<td style="background-color:#F7F7DE" colspan="3">
				<asp:Label ID="lb_ab_email" runat="server"></asp:Label>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center">職務名稱</td>
			<td style="background-color:#F7F7DE">
				<asp:Label ID="lb_ab_posit" runat="server"></asp:Label>
			</td>
			<td align="center">工作單位</td>
			<td style="background-color:#F7F7DE">
				<asp:Label ID="lb_ab_company" runat="server"></asp:Label>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center">備註說明</td>
			<td style="background-color:#F7F7DE" colspan="3">
				<asp:Label ID="lb_ab_desc" runat="server"></asp:Label>
			</td>
		</tr>
		<tr align="left" style="background-color:#99FF99;height:18pt">
			<td align="center">最後異動時間</td>
			<td style="background-color:#F7F7DE" colspan="3">
				<asp:Label ID="lb_init_time" runat="server"></asp:Label>
			</td>
		</tr>
	</table>	
	<p style="margin:10pt 0pt 10pt 0pt">
		<a href="javascript:goPage(-1);" class="abtn">&nbsp;前一筆&nbsp;</a>&nbsp;&nbsp;
		<a href="6002.aspx<%=lb_page.Text%>" class="abtn">&nbsp;回瀏覽頁&nbsp;</a>&nbsp;&nbsp;
		<a href="javascript:goPage(1);" class="abtn">&nbsp;下一筆&nbsp;</a>
	</p>&nbsp;
	<iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0" style="display:none"></iframe>
	</center>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	<asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_sort" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_row" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_maxrow" runat="server" Text="" Visible="false"></asp:Label>
    <script language="javascript" type="text/javascript">
    	function mdel() {
    		if (confirm("確定要刪除？"))
    		{
    			update.location.replace("60021_del.ashx<%=lb_page.Text%>&sid=<%=lb_ab_sid.Text%>");
    		}
    	}

    	// 上下換圖
    	function goPage(row) {
    		row = <%=lb_row.Text%> + row;
    		if (row < 1)
    			alert("已經是在第一筆資料!\n");
    		else if (row > <% = lb_maxrow.Text %>)
    			alert("已經是最後一筆資料!\n");
    		else {
    			location.href = "60021.aspx<%=lb_page.Text%>&sid=0&row=" + row.toString();
    		}
    	}
    </script>
</div>
</form>
</body>
</html>
