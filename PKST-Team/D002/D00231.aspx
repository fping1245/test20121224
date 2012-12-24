<%@ Page Language="C#" AutoEventWireup="true" CodeFile="D00231.aspx.cs" Inherits="_D00231" %>
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
		<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">修改主題回應</p>

		<table border="1" cellpadding="4" cellspacing="0" style="width:98%; background-color:#F7F7DE; margin-bottom:10px">
		<tr id="id0">
			<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">人員姓名</td>
			<td style="text-align:left; width:38%">
				<table width="100%" border="0" cellpadding="0" cellspacing="0">
				<tr><td style="width:45px">
						<asp:Image ID="img_ff_symbol" runat="server" ImageUrl="~/images/symbol/S01.gif" ToolTip="微笑" AlternateText="微笑" />
						<asp:Image ID="img_ff_sex" runat="server" ImageUrl="~/images/symbol/man.gif" ToolTip="男性" AlternateText="男性" />
					</td>
					<td><asp:Label ID="lb_ff_name" runat="server"></asp:Label>
						.. (<asp:Literal ID="lt_ff_email" runat="server"></asp:Literal>)</td>
				</tr>
				</table>
			</td>
			<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">時間與IP</td>
			<td style="text-align:left; width:38%"><asp:Label ID="lb_ff_time" runat="server"></asp:Label>&nbsp;</td>
		</tr>
		<tr id="id1">
			<td style="text-align:center; width:12%; background-color:#99CCFF; color:#FFFFFF">討論主題</td>
			<td style="text-align:left; width:88%" colspan="3">
				<table width="100%" border="0" cellpadding="0" cellspacing="0">
				<tr><td style="width:25px">
						<img src="../images/button/down.gif" id="img_desc_show" onclick="desc_show()" title="顯示主題內容" alt="顯示主題內容" />
					</td>
					<td><asp:Label ID="lb_ff_topic" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				</table>
			</td>
		</tr>
		<tr id="id2" style="display:none">
			<td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">主題內容</td>
			<td style="text-align:left" colspan="3"><asp:Label ID="lb_ff_desc" runat="server"></asp:Label>&nbsp;</td>
		</tr>
		</table>
		
		<table border="1" cellpadding="4" cellspacing="0" style="width:660px; background-color:#F7F7DE">
		<tr><td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">顯示</td>
			<td align="left" style="width:40%">
				<asp:RadioButton ID="rb_is_show1" runat="server" GroupName="rb_fr_is_show" Text="顯示" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_is_show0" runat="server" GroupName="rb_fr_is_show" 
					Text="隱藏" AutoPostBack="True" oncheckedchanged="rb_is_show0_CheckedChanged" />
			</td>
			<td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">開關</td>
			<td align="left" style="width:40%">
				<asp:RadioButton ID="rb_is_close1" runat="server" GroupName="rb_fr_is_close" Text="開放" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_is_close0" runat="server" GroupName="rb_fr_is_close" Text="關閉" />
			</td>
		</tr>
		<tr><td style="width:10%; text-align:center; background-color:#99CCFF; color:#FFFFFF">替代文字</td>
			<td align="left" style="width:90%;" colspan="3">
				<asp:TextBox ID="tb_instead" runat="server" Text="" MaxLength="50" Width="98%"></asp:TextBox>
			</td>
		</tr>
		<tr><td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">心情</td>
			<td align="left" colspan="3" style="width:90%">
				<asp:RadioButton ID="rb_fr_symbol00" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S00.gif" alt="微笑" title="微笑" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol01" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S01.gif" alt="俏皮" title="俏皮" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol02" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S02.gif" alt="得意" title="得意" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol03" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S03.gif" alt="害羞" title="害羞" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol04" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S04.gif" alt="哭泣" title="哭泣" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol05" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S05.gif" alt="禁言" title="禁言" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol06" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S06.gif" alt="氣憤" title="氣憤" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol07" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S07.gif" alt="鄙視" title="鄙視" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol08" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S08.gif" alt="無言" title="無言" /><br />
				<asp:RadioButton ID="rb_fr_symbol09" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S09.gif" alt="害怕" title="害怕" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol10" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S10.gif" alt="真棒" title="真棒" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol11" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S11.gif" alt="傷心" title="傷心" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol12" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S12.gif" alt="握手" title="握手" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol13" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S13.gif" alt="豬頭" title="豬頭" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol14" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S14.gif" alt="大便" title="大便" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol15" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S15.gif" alt="電話聯絡" title="電話聯絡" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol16" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S16.gif" alt="OK" title="OK" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_symbol17" runat="server" Text="" GroupName="rb_fr_symbol" /><img src="../images/symbol/S17.gif" alt="禮物" title="禮物" />
			</td>
		</tr>
		<tr><td style="width:10%; text-align:center; background-color:#99CCFF; color:#FFFFFF">姓名</td>
			<td align="left" style="width:40%;">
				<asp:TextBox ID="tb_fr_name" runat="server" Text="" MaxLength="12" Width="100pt"></asp:TextBox>
			</td>
			<td style="width:10%; text-align:center; background-color:#99CCFF; color:#FFFFFF">性別</td>
			<td align="left" style="width:40%;">
				<asp:RadioButton ID="rb_fr_sex1" runat="server" Text="" GroupName="rb_fr_sex" /><img src="../images/symbol/man.gif" alt="男性" title="男性" />&nbsp;&nbsp;
				<asp:RadioButton ID="rb_fr_sex2" runat="server" Text="" GroupName="rb_fr_sex" /><img src="../images/symbol/woman.gif" alt="女性" title="女性" />
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">E-Mail</td>
			<td align="left" colspan="3">
				<asp:TextBox ID="tb_fr_email" runat="server" Text="" MaxLength="100" Width="98%"></asp:TextBox>
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">內容</td>
			<td align="left" colspan="3">
				<asp:TextBox ID="tb_fr_desc" Rows="10" TextMode="MultiLine" runat="server" MaxLength="1000" Width="98%"></asp:TextBox>
			</td>
		</tr>
		<tr><td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">回應時間</td>
			<td align="left" style="width:40%"><asp:Label ID="lb_fr_time" runat="server"></asp:Label></td>
			<td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">IP</td>
			<td align="left" style="width:40%"><asp:Label ID="lb_fr_ip" runat="server"></asp:Label></td>
		</tr>
		</table>
		<p style="width:98%; text-align:left; margin:5pt 0pt 0pt 0pt">※ 「顯示」設為[隱藏]時，會將討論回應內容用「替代文字」來取代顯示。</p>
		<p style="width:98%; text-align:left; margin:1pt 0pt 0pt 0pt">※ 「開關」設為[關閉]時，該筆討論回應不會出現在前端的網頁。</p>
		<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;存檔&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.location.reload(true);" class="abtn">&nbsp;取消&nbsp;</a>
		</p>&nbsp;
	</td></tr>
	</table>
	</center>
	
	<script language="javascript" type="text/javascript" src="../js/innerResize.js"></script>
	<script language="javascript" type="text/javascript">
		// 查看主題內容
		function desc_show()
		{
			var dobj = document.getElementById("id2");
			var iobj = document.getElementById("img_desc_show");
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
			resize();
		}
	</script>
	</div>
	</form>
</body>
</html>
