<%@ Page Language="C#" AutoEventWireup="true" CodeFile="900262.aspx.cs" Inherits="_900262" validateRequest=false %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>POP3收信處理</title>
</head>
<body style="white-space:normal">
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="1" cellpadding="0" cellspacing="0" style="width:100%; background-color:#EFEFEF">
	<tr><td><p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體; text-align:center">顯示郵件內容</p>
			<table border="1" cellpadding="4" cellspacing="0" style="width:100%; background-color:#EFEFEF">
			<tr><td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">郵件識別碼</td>
				<td style="text-align:left; width:23%">&nbsp;<asp:Label ID="lb_ppm_id" runat="server"></asp:Label></td>
				<td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">收件序號</td>
				<td style="text-align:left; width:23%">&nbsp;<asp:Label ID="lb_ppm_sn" runat="server"></asp:Label></td>
				<td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">郵件大小</td>
				<td style="text-align:left; width:24%">&nbsp;<asp:Label ID="lb_ppm_size" runat="server"></asp:Label> bytes</td>
			</tr>
			<tr><td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">異動時間</td>
				<td style="text-align:left; width:23%">&nbsp;<asp:Label ID="lb_init_time" runat="server"></asp:Label></td>
				<td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">郵件編號</td>
				<td style="text-align:left; width:23%">&nbsp;<asp:Label ID="lb_ppm_sid" runat="server"></asp:Label></td>
				<td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">帳戶編號</td>
				<td style="text-align:left; width:24%">&nbsp;[&nbsp;<asp:Label ID="lb_ppa_sid" runat="server"></asp:Label>&nbsp;]</td>
			</tr>
			<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">寄件者</td>
				<td style="text-align:left" colspan="3">&nbsp;<asp:Label ID="lb_s_name" runat="server"></asp:Label></td>
				<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">寄件時間</td>
				<td style="text-align:left">&nbsp;<asp:Label ID="lb_s_time" runat="server"></asp:Label></td>
			</tr>
			<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">收件者</td>
				<td style="text-align:left" colspan="3">&nbsp;<asp:Label ID="lb_r_name" runat="server"></asp:Label></td>
				<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">收件時間</td>
				<td style="text-align:left">&nbsp;<asp:Label ID="lb_r_time" runat="server"></asp:Label></td>
			</tr>
			<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">主旨</td>
				<td style="text-align:left" colspan="5">&nbsp;<asp:TextBox ID="tb_ppm_subject" runat="server" ReadOnly="true" Width="99%"></asp:TextBox></td>
			</tr>
			<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">附加檔案</td>
				<td style="text-align:left; white-space:normal" colspan="5">&nbsp;<asp:Literal ID="lt_attach" runat="server" Text=""></asp:Literal></td>
			</tr>
			<tr><td style="text-align:left" class="text18pt" colspan="6">
				<asp:LinkButton ID="lk_body" runat="server" Text="&nbsp;郵件內文&nbsp;" CssClass="abtn" onclick="lk_body_Click" ToolTip="切換到檢視「郵件內文」"></asp:LinkButton>&nbsp;
				<asp:LinkButton ID="lk_source" runat="server" Text="&nbsp;原始資料&nbsp;" CssClass="abtn" onclick="lk_source_Click" ToolTip="切換到檢視「原始資料」"></asp:LinkButton>
				<p style="margin:4pt 0pt 0pt 0pt"></p>
				<asp:MultiView ID="mv_content" runat="server" ActiveViewIndex="0">
					<asp:View ID="vw_body" runat="server">
						<iframe id="if_body" src="<%=lb_iframe.Text%>" frameborder="1" style="width:100%; height:200pt"></iframe>
					</asp:View>
					<asp:View ID="vw_scource" runat="server">
						<asp:TextBox ID="tb_ppm_content" runat="server" ReadOnly="true" Rows="12" Width="100%" Height="200pt" TextMode="MultiLine"></asp:TextBox>
					</asp:View>
				</asp:MultiView>
				</td>
			</tr>
			</table>
			<p style="margin:10pt 0pt 0pt 0pt; text-align:center"><a href="javascript:parent.close_all()" class="abtn">&nbsp;關閉&nbsp;</a></p>&nbsp;
		</td>
	</tr>
	</table>
	</center>
	<asp:Label ID="lb_iframe" runat="server" Visible="false" Text=""></asp:Label>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
