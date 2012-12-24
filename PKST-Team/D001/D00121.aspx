<%@ Page Language="C#" AutoEventWireup="true" CodeFile="D00121.aspx.cs" Inherits="_D00121" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>論壇前端</title>
</head>
<body style="word-spacing:normal">
	<form id="form1" runat="server">
	<div>
	<center>
	<table border="1" cellpadding="4" cellspacing="0" style="width:100%; background-color:#EFEFEF; white-space:normal">
	<tr><td align="center">
		<p class="text14pt" style="margin:5pt 0pt 5pt 0pt; font-family:標楷體">回應討論主題</p>

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
	
		<table border="1" cellpadding="4" cellspacing="0" style="width:98%; background-color:#F7F7DE">
		<tr><td style="text-align:center; width:10%; background-color:#99CCFF; color:#FFFFFF">心情</td>
			<td align="left" colspan="3" style="width:90%">
				<asp:RadioButton ID="rb_fr_symbol00" runat="server" Text="" GroupName="rb_fr_symbol" Checked="true" /><img src="../images/symbol/S00.gif" alt="微笑" title="微笑" />&nbsp;&nbsp;
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
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">回應內容</td>
			<td align="left" colspan="3">
				<asp:TextBox ID="tb_fr_desc" Rows="10" TextMode="MultiLine" runat="server" MaxLength="1000" Width="98%"></asp:TextBox>
			</td>
		</tr>
		<tr><td style="text-align:center; background-color:#99CCFF; color:#FFFFFF">驗證</td>
			<td align="center">請將右方的數字填入 〉〉〉〉〉<br />
				<asp:TextBox ID="tb_confirm" runat="server" MaxLength="4" Width="110px" EnableViewState="False"></asp:TextBox>&nbsp;
				<asp:Button ID="bn_new_confirm" runat="server" ToolTip="重新產生驗證圖示" Text="更換圖示" CssClass="text9pt" onclick="bn_new_confirm_Click" /><br />
			</td>
			<td align="center" colspan="2"><asp:Image ID="img_confirm" ImageUrl="D00111.ashx" runat="server" Height="54px" Width="200px" /></td>
		</tr>
		</table>
		<p style="margin:10pt 0pt 0pt 0pt">
			<asp:LinkButton ID="lk_save" runat="server" CssClass="abtn" onclick="lk_save_Click">&nbsp;存檔&nbsp;</asp:LinkButton>&nbsp;&nbsp;
			<a href="javascript:parent.location.reload(true);" class="abtn">&nbsp;取消&nbsp;</a>
		</p>&nbsp;
	</td></tr>
	</table>
	</center>
	
	<asp:Label ID="lb_ff_sid" runat="server" Visible="false"></asp:Label>
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
