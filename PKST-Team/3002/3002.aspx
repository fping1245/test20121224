<%@ Page Language="C#" AutoEventWireup="true" CodeFile="3002.aspx.cs" Inherits="_3002" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>��ï�޲z</title>
<script language="javascript" type="text/javascript">
	var show_effect = 0;
	var slide;

	// ��ܤW�ǰT������
	function show_msg() {
		var msgobj;

		msgobj = document.getElementById("div_msg_wait");
		if (msgobj != null) {
			msgobj.style.left = String((document.body.clientWidth - 240) / 2) + "px";
			msgobj.style.top = String((document.body.clientHeight - 240) / 2) + "px";
			msgobj.style.display = "";
		}

		show_fullwindow();
	}

	// �����W�ǰT������
	function close_msg() {
		var msgobj;

		msgobj = document.getElementById("div_msg_wait");
		if (msgobj != null)
			msgobj.style.display = "none";

		close_fullwindow();
	}

	// ����л\�㭶����
	function show_fullwindow() {
		var fullobj, tableobj;

		fullobj = document.getElementById("fullwindow");
		if (fullobj != null) {
			fullobj.style.width = document.body.clientWidth + "px";
			fullobj.style.height = document.body.clientHeight + "px";
			fullobj.style.display = "";
		}

		tableobj = document.getElementById("fulltable");
		if (tableobj != null) {
			tableobj.style.width = document.body.clientWidth + "px";
			tableobj.style.height = document.body.clientHeight + "px";
			tableobj.style.display = "";
		}
	}

	// �����л\�㭶������
	function close_fullwindow() {
		var fullobj;

		fullobj = document.getElementById("fullwindow");
		if (fullobj != null)
			fullobj.style.display = "none";
	}

	// ��� div �u�@����
	function show_div(objname, mwidth, mheight) {
		var divobj;

		divobj = document.getElementById(objname);
		if (divobj != null) {
			divobj.style.width = mwidth + "px";
			divobj.style.height = mheight + "px";
			divobj.style.left = String((document.body.clientWidth - mwidth) / 2) + "px";
			divobj.style.top = String((document.body.clientHeight - mheight) / 2) + "px";
			divobj.style.display = "";
		}

		show_fullwindow();
	}

	// ��� iframe �u�@�����A�éI�s���w iframe �n���檺����
	function show_win(srcname, mwidth, mheight) {
		var divobj, ifobj;

		divobj = document.getElementById("div_win");
		ifobj = document.getElementById("if_win");
		if (divobj != null && ifobj != null) {
			divobj.style.width = mwidth + "px";
			divobj.style.height = mheight + "px";
			divobj.style.left = String((document.body.clientWidth - mwidth) / 2) + "px";
			divobj.style.top = String((document.body.clientHeight - mheight) / 2) + "px";
			divobj.style.display = "";

			ifobj.style.width = mwidth + "px";
			ifobj.style.height = mheight + "px";
			ifobj.src = srcname;
		}

		show_fullwindow();
	}

	// ���� div �u�@����
	function close_div(objname) {
		var divobj;
		divobj = document.getElementById(objname);
		if (divobj != null)
			divobj.style.display = "none";
	}

	// ���éҦ����ص���
	function close_all() {
		close_div("div_msg_wait");
		close_div("div_effect");
		close_div("div_dir");
		close_div("div_help");
		close_div("div_win");

		close_fullwindow();
	}

	// menu ���}��
	function hide_menu() {
		var frameobj;
		var imgobj;
		frameobj = top.document.getElementById("subframe");
		imgobj = top.mstatus.document.getElementById("hd_menu_pic");
		frameobj.cols = "0,*";
		imgobj.src = "images/t_Right.gif";
		imgobj.alt = "�}�ҥ\����";
	}

	// title ���}��
	function hide_title() {
		var frameobj;
		frameobj = top.document.getElementById("mainframe");
		imgobj = top.mstatus.document.getElementById("hd_title_pic");
		frameobj.rows = "0,32pt,*";
		imgobj.src = "images/t_Down.gif";
		imgobj.alt = "�}�ҩ��Y�ϰ�";
	}

	// ���J�ɡA�������Y�C�ο��C
	window.onload = function() {
		hide_menu();
		hide_title();
	}

	// ��s TreeView
	function tree_reload(sid) {
		if (document.frames) {		// IE
			document.frames["if_tree"].document.location.reload();
		}
		else {						// W3C (FireFox)
			var ifobj;
			ifobj = document.getElementById("if_tree");
			if (ifobj != null) {
				ifobj.contentDocument.location.reload();
			}
		}
	}

	// �M������ iframe (if_win) �� src
	function clean_win() {
		ifobj = document.getElementById("if_win");
		if (ifobj != null) {
			ifobj.src = "about:blank";
			close_div("div_win");
		}
	}

	// ��s �Y�ϵ���
	function thumb_reload() {
		if (document.frames) {		// IE
			document.frames["if_thumb"].document.location.reload(); 
		}
		else {						// W3C (FireFox)
			var ifobj;
			ifobj = document.getElementById("if_thumb");
			if (ifobj != null) {
				ifobj.contentDocument.location.reload();
			}
		}
	}

	// �����ܮĪG
	function ck_effect() {
		var selobj, spanobj;
		selobj  = document.getElementById("sel_effect");
		spanobj = document.getElementById("effect_name");

		if (selobj != null && spanobj != null) {
			show_effect = selobj.selectedIndex;
			switch (show_effect) {
				case 0:
					spanobj.innerHTML = "�H��";
					break;
				case 1:
					spanobj.innerHTML = "����";
					break;
				case 2:
					spanobj.innerHTML = "�Y��";
					break;
				case 3:
					spanobj.innerHTML = "����½��";
					break;
				case 4:
					spanobj.innerHTML = "����½��";
					break;
				case 5:
					spanobj.innerHTML = "�H�J�H�X";
					break;
				case 6:
					spanobj.innerHTML = "���ʭ���";
					break;
				case 7:
					spanobj.innerHTML = "��ʭ���";
					break;
				case 8:
					spanobj.innerHTML = "�﨤���Y";
					break;
				case 9:
					spanobj.innerHTML = "���`";
					break;
				default:
					spanobj.innerHTML = "�H��";
					show_effect = -1;
					break;
			}
		}
	}

	// ����ۿO��
	function show_slide(row) {
		var features;
		var mhref = "";

		features = "width=" + (window.screen.availWidth - 10).toString() + "px"; 		// �����e��
		features += ",height=" + (window.screen.availHeight - 60).toString()  + "px"; 	// ��������
		features += ",top=1px";
		features += ",left=1px";
		features += ",toolbar=no,status=yes,resizable=yes,scrollbars=yes,";

		mhref = "30027.aspx?fl_url=<%=lb_fl_url_encode.Text%>&rownum=" + row + "&imgw=" + (window.screen.availWidth - 10).toString();
		mhref += "&imgh" + (window.screen.availHeight - 60).toString() + "&effect=" + show_effect;

		slide = window.open(mhref, "win_slide", features);
	}
</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
	<!-- Begin �л\��Ӥu�@�����A�����ϥΪ̦A���䥦���� -->
	<div id="fullwindow" style="position: absolute; top:0px; left:0px; width:1024px; height:768px; display:none">
	<table id="fulltable" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_back.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<!-- End -->
	<asp:ScriptManager ID="sm_manager" runat="server">
	</asp:ScriptManager>
	<center>
	<p align="center" class="text18pt" style="margin:10pt 0pt 10pt 0pt; font-family:�з���;">��ï�޲z(�ϥι�����|�x�s����)</p>
	<hr style="width:99%" />
	<table width="95%" border="0" cellpadding="2" cellspacing="0" style="margin:0pt 0pt 0pt 0pt">
	<tr align="left">
		<td class="text11pt">�ثe���|�G<asp:Label ID="lb_show_path" runat="server" Text="" Font-Bold="true" ForeColor="#00008B" CssClass="text11pt"></asp:Label></td>
		<td class="text11pt">�ۿO�������ĪG�G<span id="effect_name" class="text11pt" style="color:#00008B; font-weight:bold">�H��</span>
	</tr>
	</table>
	<hr style="width:99%" />
	<table width="95%" border="0" cellpadding="2" cellspacing="0" style="margin:0pt 0pt 0pt 0pt">
	<tr><td align="left" class="text11pt">
			<asp:UpdatePanel ID="up_button" runat="server" RenderMode="Inline">
				<ContentTemplate>
					<asp:Button ID="bn_go_root" runat="server" Text="��ڥؿ�" Height="22pt" onclick="bn_go_root_Click" />
					<input type="button" value="�ؿ��޲z" style="height:22pt" onclick="show_div('div_dir',150, 240)" />
					<input type="button" value="�W���ɮ�" style="height:22pt" onclick="show_win('30025.aspx?fl_url=<%=lb_fl_url_encode.Text%>', 260, 215)" />
					<input type="button" value="�����ĪG" style="height:22pt" onclick="show_div('div_effect',240, 240)" />
					<asp:Button ID="bn_show" runat="server" Text="���ۿO��" Height="22pt" OnClientClick="show_slide(1)" />
					<asp:Button ID="bn_help" runat="server" Text="�ϥλ���" Height="22pt" OnClientClick="show_div('div_help',300, 240)" />
				</ContentTemplate>
			</asp:UpdatePanel>
		</td>
	</tr>
	</table>
	<!-- Begin ���õ��� -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	<!-- Begin �ؿ��޲z -->
	<div id="div_dir" style="position: absolute; top:0px; left:0px; width:150px; display:none">
	<table border="2" cellpadding="2" cellspacing="2" style="width:150px;" bgcolor="#efefef">
	<tr><td align="center" class="text9pt"><br />
			<input type="button" value="�ؤl�ؿ�" onclick="close_div('div_dir');show_win('30022.aspx?fl_url=<%=lb_fl_url_encode.Text%>', 240, 220)" style="height:22pt" /><br /><br />
			<input type="button" value="�R���ؿ�" onclick="close_div('div_dir');show_win('30023.aspx?fl_url=<%=lb_fl_url_encode.Text%>', 240, 148)" style="height:22pt" /><br /><br />
			<input type="button" value="�ؿ���W" onclick="close_div('div_dir');show_win('30024.aspx?fl_url=<%=lb_fl_url_encode.Text%>', 240, 350)" style="height:22pt" /><br /><br />
			<input type="button" value="����" onclick="close_all()" style="height:22pt" /><br /><br />
		</td>
	</tr>
	</table>
	</div>
	<!-- End -->
	<!-- Begin ��ܮĪG -->
	<div id="div_effect" style="position: absolute; top:0px; left:0px; width:240px; display:none">
	<table border="2" cellpadding="2" cellspacing="2" style="width:240px;" bgcolor="#efefef">
	<tr><td align="center" class="text11pt"><br />�п�ܤۿO�������ĪG<br /><br />
			<select id="sel_effect" title="��ܤۿO�������ĪG">
				<option value="0">�H��</option>
				<option value="1">����</option>
				<option value="2">�Y��</option>
				<option value="3">����½��</option>
				<option value="4">����½��</option>
				<option value="5">�H�J�H�X</option>
				<option value="6">���ʭ���</option>
				<option value="7">��ʭ���</option>
				<option value="8">�﨤���Y</option>
				<option value="9">���`</option>
			</select><br /><br />
			<input type="button" value="�T�w" onclick="ck_effect();close_all()" style="height:22pt" />
			<input type="button" value="����" onclick="close_all()" style="height:22pt" /><br /><br />
		</td>
	</tr>
	</table>
	</div>
	<!-- End -->
	<!-- Begin �ϥλ��� -->
	<div id="div_help" style="position: absolute; top:0px; left:0px; width:300px; display:none">
	<table border="2" cellpadding="0" cellspacing="0" style="width:300px; height:100px;" bgcolor="#efefef">
	<tr><td align="center">
			<table border="0" cellpadding="2" cellspacing="0" width="100%">
			<tr><td align="left" style="background-color:Blue; color:White">&nbsp;�ϥλ���</td>
				<td align="right" style="background-color:Blue; width:30px">
					<a href="javascript:close_all()" style="font-size:11pt; color:White; border-color:White; border-style:inherit; background-color:Red" title="��������">&nbsp;��&nbsp;</a>
				</td>
			</tr>
			</table>
		</td>
	</tr>
	<tr><td align="center" class="text9pt">
			<table border="0" cellpadding="2" cellspacing="2" width="100%" bgcolor="#efefef">
			<tr align="left">
				<td style="width:20px" valign="top">��</td>
				<td>�I��ؿ��A�i�w���ؿ������Y�ϡC</td>
			</tr>
			<tr align="left">
				<td valign="top">��</td>
				<td>�I��w�����ɮסA�|�u�X����ϧε����C</td>
			</tr>
			<tr align="left">
				<td valign="top">��</td>
				<td>�I��u���ۿO���v�A�|�u�X�����A�̷ӫ��w���ĪG�A�`����ܷ�e�ؿ��������ɡC</td>
			</tr>
			<tr align="left">
				<td valign="top">��</td>
				<td>�ۿO������ɡA�Ы� F11 �H���ù��[�ݡA�H��o�̨ήĪG�C</td>
			</tr>
			<tr align="left">
				<td valign="top">��</td>
				<td>�ۿO������i�椤�A�I���ۤ��A�|�۰ʴ���U�@�i�ۤ��C</td>
			</tr>
			</table>
		</td>
	</tr>
	</table>
	</div>
	<!-- End -->
	<!-- Begin ��ܤW�ǰT�� -->
	<div id="div_msg_wait" class="text9pt" style="position:absolute; width:240px; z-index:0; display:none;">
	<table border="2" cellpadding="2" cellspacing="2" style="width:240px; height:100px;" bgcolor="#efefef">
	<tr><td align="center" class="text9pt">��ƤW�Ǥ� ........<br />�Фū��������!</td></tr>
	</table>
	</div>
	<!-- End -->
	<hr style="width:99%" />
	<table width="99%" border="1" cellpadding="0" cellspacing="0">
	<tr><td style="width:250px" valign="top" align="left">
			<iframe id="if_tree" src="30021.aspx?fl_url=<%=lb_fl_url_encode.Text%>" frameborder="0" style="width:250px; height:522px"></iframe>
		</td>
		<td valign="middle" align="center" style="width:100%">
			<iframe id="if_thumb" src="30026.aspx?fl_url=<%=lb_fl_url_encode.Text%>" frameborder="0" style="width:660px"></iframe>
		</td>
	</tr>
	</table>
	</center>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	<asp:Label ID="lb_fl_url" runat="server" Visible="false" Text=""></asp:Label>
	<asp:Label ID="lb_fl_url_encode" runat="server" Text="" Visible="false"></asp:Label>
	<asp:Label ID="lb_path" runat="server" Visible="false"></asp:Label>
	</div>
	</form>	
</body>
</html>