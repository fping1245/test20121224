<%@ Page Language="C#" AutoEventWireup="true" CodeFile="D0021.aspx.cs" Inherits="_D0021" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>論壇管理</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="1" cellpadding="4" cellspacing="0" style="width:100%; background-color:#EFEFEF; white-space:normal">
	<tr><td align="center">
		<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">修改討論主題</p>
		<table border="1" cellpadding="4" cellspacing="0" style="width:660px; background-color:#F7F7DE">
		<tr><td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">顯示</td>
			<td align="left" style="width:40%">
				<asp:RadioButton ID="rb_is_show1" runat="server" GroupName="rb_ff_is_show" Text="顯示" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_is_show0" runat="server" GroupName="rb_ff_is_show" 
					Text="隱藏" AutoPostBack="True" oncheckedchanged="rb_is_show0_CheckedChanged" />
			</td>
			<td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">開關</td>
			<td align="left" style="width:40%">
				<asp:RadioButton ID="rb_is_close1" runat="server" GroupName="rb_ff_is_close" Text="開放" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_is_close0" runat="server" GroupName="rb_ff_is_close" Text="關閉" />
			</td>
		</tr>
		<tr><td style="width:10%; text-align:center; background-color:#99CCFF; color:#FFFFFF">替代文字</td>
			<td align="left" style="width:40%;">
				<asp:TextBox ID="tb_instead" runat="server" Text="" MaxLength="50" Width="98%"></asp:TextBox>
			</td>
			<td style="width:10%; text-align:center; background-color:#99CCFF; color:#FFFFFF">優先順序</td>
			<td align="left" style="width:40%;">
				<asp:TextBox ID="tb_ff_top" runat="server" Text="0" MaxLength="3" Width="22pt"></asp:TextBox> (0:普通 255:置頂；數字越大越上面)
			</td>
		</tr>
		<tr><td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">心情</td>
			<td align="left" colspan="3" style="width:90%">
				<asp:RadioButton ID="rb_ff_symbol00" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S00.gif" alt="微笑" title="微笑" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol01" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S01.gif" alt="俏皮" title="俏皮" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol02" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S02.gif" alt="得意" title="得意" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol03" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S03.gif" alt="害羞" title="害羞" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol04" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S04.gif" alt="哭泣" title="哭泣" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol05" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S05.gif" alt="禁言" title="禁言" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol06" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S06.gif" alt="氣憤" title="氣憤" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol07" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S07.gif" alt="鄙視" title="鄙視" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol08" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S08.gif" alt="無言" title="無言" /><br />
				<asp:RadioButton ID="rb_ff_symbol09" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S09.gif" alt="害怕" title="害怕" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol10" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S10.gif" alt="真棒" title="真棒" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol11" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S11.gif" alt="傷心" title="傷心" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol12" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S12.gif" alt="握手" title="握手" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol13" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S13.gif" alt="豬頭" title="豬頭" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol14" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S14.gif" alt="大便" title="大便" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol15" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S15.gif" alt="電話聯絡" title="電話聯絡" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol16" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S16.gif" alt="OK" title="OK" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_symbol17" runat="server" Text="" GroupName="rb_ff_symbol" /><img src="../images/symbol/S17.gif" alt="禮物" title="禮物" />
			</td>
		</tr>
		<tr><td style="width:10%; text-align:center; background-color:#99CCFF; color:#FFFFFF">姓名</td>
			<td align="left" style="width:40%;">
				<asp:TextBox ID="tb_ff_name" runat="server" Text="" MaxLength="12" Width="100pt"></asp:TextBox>
			</td>
			<td style="width:10%; text-align:center; background-color:#99CCFF; color:#FFFFFF">性別</td>
			<td align="left" style="width:40%;">
				<asp:RadioButton ID="rb_ff_sex1" runat="server" Text="" GroupName="rb_ff_sex" /><img src="../images/symbol/man.gif" alt="男性" title="男性" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_ff_sex2" runat="server" Text="" GroupName="rb_ff_sex" /><img src="../images/symbol/woman.gif" alt="女性" title="女性" />
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">E-Mail</td>
			<td align="left" colspan="3">
				<asp:TextBox ID="tb_ff_email" runat="server" Text="" MaxLength="100" Width="98%"></asp:TextBox>
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">主題</td>
			<td align="left" colspan="3">
				<asp:TextBox ID="tb_ff_topic" runat="server" Text="" MaxLength="50" Width="98%"></asp:TextBox>
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">內容</td>
			<td align="left" colspan="3">
				<asp:TextBox ID="tb_ff_desc" Rows="10" TextMode="MultiLine" runat="server" MaxLength="1000" Width="98%"></asp:TextBox>
			</td>
		</tr>
		<tr><td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">時間/IP</td>
			<td align="left" style="width:40%">
				<asp:Label ID="lb_ff_time" runat="server"></asp:Label> ( IP:<asp:Label ID="lb_ff_ip" runat="server"></asp:Label> )
			</td>
			<td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">回應篇數</td>
			<td align="left" style="width:40%">
				<asp:Label ID="lb_response" runat="server"></asp:Label>&nbsp;篇
			</td>		
		</tr>
		</table>
		<p style="width:98%; text-align:left; margin:5pt 0pt 0pt 0pt">※ 「顯示」設為[隱藏]時，會將討論內容用「替代文字」來取代顯示。</p>
		<p style="width:98%; text-align:left; margin:1pt 0pt 0pt 0pt">※ 「開關」設為[關閉]時，該筆討論不會出現在前端的網頁。</p>
		<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;存檔&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.location.reload(true);" class="abtn">&nbsp;取消&nbsp;</a>
		</p>&nbsp;
	</td></tr>
	</table>
	</center>
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	</div>
	</form>
</body>
</html>
