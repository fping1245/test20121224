<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300162.aspx.cs" Inherits="_300162" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>��ï�޲z</title>
<style type="text/css">
.bnNor {
	padding:2px;
	border:1px solid #999999;
	border-top:1px solid #ffffff; 
    border-left:1px solid #ffffff;
    cursor:hand;
    } 

.bnOver {
	padding:2px;
	background-color:#00ffff;
	border:1px solid #999999;
	border-top:1px solid #ffffff;
	border-left:1px solid #ffffff;
	cursor:hand;
	} 
    
.bnDown {
	padding:2px;
	border:1px solid #999999;
	border-top:1px solid #ffffff;
	border-left:1px solid #ffffff;
	cursor:hand;
	}
</style>
<script language="javascript" type="text/javascript">

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
		close_div("div_win");
		close_fullwindow();
	}

	// �M������ iframe (if_win) �� src
	function clean_win() {
		ifobj = document.getElementById("if_win");
		if (ifobj != null) {
			ifobj.src = "about:blank";
			close_div("div_win");
		}
	}

	// ��s�W�h�������Y�ϵ���
	function thumb_reload() {
		opener.location.reload();
	}

	// ��ܻ�����r��� (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function show_doc()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("3001622.aspx?ac_sid=<%=ac_sid%>&timestamp=" + timestamp, 400, 200);
	}

	// �קﻡ����r��� (�קK IE Cache ���@�ΡA�᭱�[�W timestamp ���ܼơA��ڤW�S���Ψ�)
	function pic_edit()
	{
		var now = new Date();
		var timestamp = now.getMinutes() + now.getSeconds() + now.getMilliseconds();
		show_win("3001624.aspx?al_sid=<%=al_sid%>&ac_sid=<%=ac_sid%>&timestamp=" + timestamp, 400, 200);
	}
</script>
</head>
<body background="../images/background/s21.jpg">
	<form id="form1" runat="server">
	<div>
	<!-- Begin �л\��Ӥu�@�����A�����ϥΪ̦A���䥦���� -->
	<div id="fullwindow" style="position: absolute; top:0px; left:0px; width:1024px; height:768px; display:none">
	<table id="fulltable" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/msg_back.gif);">
	<tr><td></td></tr>
	</table>
	</div>
	<!-- End -->
	<!-- Begin ���õ��� -->
	<div id="div_win" style="position: absolute; top:0px; left:0px; display:none">
	<iframe id="if_win" src="" frameborder="0" style="width:260px"></iframe>
	</div>
	<!-- End -->
	<table width="100%" border="0" cellpadding="0" cellspacing="0" style="margin:10px 0px 10px 0px">
	<tr><td align="center" valign="middle">
			<table id="tb_image" align="center" border="0" style="height:<%=tb_height%>px; width:<%=tb_width%>px">
			<tr><td align="center">
					<asp:Image ID="img_show" BorderWidth="6px" runat="server" BorderStyle="Outset" />
				</td>
			</tr>
			</table>
		</td>
	</tr>
	<tr><td style="height:10px"></td></tr>
	<tr><td align="center">
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
			<img id="bn_left" src="../images/button/bn1-left.gif" alt="" title="�W�@�Ӽv��" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="goPic(<%=rownum%> - 1)" />
			<img id="bn_right" src="../images/button/bn1-right.gif" alt="" title="�U�@�Ӽv��" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="goPic(<%=rownum%> + 1)" />
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
			<img id="bn_optimal" src="../images/button/optimal.gif" alt="" title="�ŦX�̨Τj�p(�����}��̤j)" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="resize(0)" />
			<img id="bn_source" src="../images/button/source.gif" alt="" title="��ڼv���j�p" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="resize(3)" />
			<img id="bn_projector" src="../images/button/projector.gif" alt="" title="�ۿO������" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="show_slide(<%=rownum%>)" />
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
			<img id="bn_increase" src="../images/button/increase.gif" alt="" title="��j 10%" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="resize(1)" />
			<img id="bn_reduce" src="../images/button/reduce.gif" alt="" title="�Y�p 10%" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="resize(2)" />
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
			<img id="bn_f_right" src="../images/button/flip-right.gif" alt="" title="����������" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="flip(1)" />
			<img id="bn_f_left" src="../images/button/flip-left.gif" alt="" title="�f��������" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="flip(-1)" />
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
			<img id="bn_delete" src="../images/button/delete.gif" alt="" title="�R��" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="show_win('3001623.aspx?al_sid=<%=al_sid%>&ac_sid=<%=ac_sid%>&rownum=<%=rownum%>', 400, 200)"  />
			<img id="bn_modify" src="../images/button/modify.gif" alt="" title="�ק�" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="pic_edit()"  />
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
			<img id="bn_help" src="../images/button/help.gif" alt="" title="����" class="bnNor" onmouseover="this.className='bnOver'" onmouseout="this.className='bnNor'" onmousedown="this.className='bnDown'" style="border-style:none; margin:0px 5px 0px 5px" onclick="show_doc()" />
			<img src="../images/line/v-line.gif" alt="" style="border-style:none" />
		</td>
	</tr>
	</table>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	</div>
	<script language="javascript" type="text/javascript">
	var ac_swidth = <%=ac_swidth%>;
	var ac_sheight = <%=ac_sheight%>;
	var ac_width = <%=ac_width%>;
	var ac_height = <%=ac_height%>;
	var maxrow = <%=maxrow%>;
	var nowflip = 0;		// �ثe���બ�A 0:���` 1:���� 2:�W�U���� 3:�k��

	// ���s�w�q��ܰϰ쪺�e�ΰ�
	function tb_resize() {
		var tbobj, imgobj;
		var tbh = 0, tbw = 0;

		imgobj = document.getElementById("img_show");
		tbobj = document.getElementById("tb_image");
		
		if (tbobj != null)
		{
			if (document.body.scrollHeight > window.screen.availHeight && imgobj.height < document.body.scrollHeight)
				tbh = (screen.availHeight - 195).toString() + "px";
			else
				tbh = (document.body.scrollHeight - 64).toString() + "px";

			if (document.body.scrollWidth > window.screen.availWidth && imgobj.width < document.body.scrollWidth) {
				tbw = (screen.availWidth - 17).toString() + "px";
			}
			else
				tbw = (document.body.scrollWidth - 17).toString() + "px";

			tbobj.style.width = tbw;
			tbobj.style.height = tbh;		
		}
	}

	// �W�U����
	function goPic(row) {
		if (row < 1)
			alert("�w�g�O�b�Ĥ@�i�Ϥ�!\n");
		else if (row > maxrow)
			alert("�w�g�O�̫�@�i�Ϥ�!\n");
		else {
			var tbobj, tbh = 600, tbw = 870;
			tbobj = document.getElementById("tb_image");
			if (tbobj != null) {
				tbh = tbobj.style.height.replace("px","");
				tbw = tbobj.style.width.replace("px","");
			}
			location.href = "300162.aspx?al_sid=<%=al_sid%>&ac_sid=0&rownum=" + row + "&maxrow=" + maxrow + "&tbw=" + tbw + "&tbh=" + tbh;
		}
	}

	// �ϧ��Y��
	function resize(mtype) {
		var imgobj;
		var fCnt = 1.0, fw = 1.0, fh = 1.0;

		imgobj = document.getElementById("img_show");
		
		if (imgobj != null) {
			switch (mtype) {
				case 0:		// �̨Τؤo
					fh = imgobj.height / (window.screen.availHeight - 125);
					fw = imgobj.width / (window.screen.availWidth - 125);
					
					if (fh > fw)
						fCnt = fh;
					else
						fCnt = fw;
	
					imgobj.style.height = (imgobj.height / fCnt).toString() + "px";
					imgobj.style.width = (imgobj.width / fCnt).toString() + "px";

					break;

				case 1:		// ��j 10%
					imgobj.style.height = (imgobj.height * 1.1).toString() + "px";
					imgobj.style.width = (imgobj.width * 1.1).toString() + "px";
					break;
				
				case 2:		// �Y�p 10%
					imgobj.style.height = (imgobj.height / 1.1).toString() + "px";
					imgobj.style.width = (imgobj.width / 1.1).toString() + "px";
					break;
					
				case 3:		// �ϧέ�Ӥؤo
					imgobj.style.height = ac_sheight.toString() + "px";
					imgobj.style.width = ac_swidth.toString() + "px";
					break;
			}
			
			ac_width = imgobj.width;
			ac_height = imgobj.height;
			
			tb_resize();		// ���s�w�q��ܰϰ쪺�e�ΰ�
		}
	}

	// �ϧ�½��
	function flip(sid) {
		var imgobj;
		var fCnt;

		imgobj = document.getElementById("img_show");
		
		if (imgobj != null) {
			if (sid > 0) {
				nowflip++;
				if (nowflip > 3)
					nowflip = 0;
			}
			else {
				nowflip--;
				if (nowflip < 0)
					nowflip = 3;
			}
			
			if (nowflip == 1 || nowflip ==3) {
				if (imgobj.height > imgobj.width)
					fCnt = imgobj.height / imgobj.width;
				else
					fCnt = imgobj.width / imgobj.height;

				imgobj.style.width = (imgobj.width / fCnt).toString() + "px";
				imgobj.style.height = (imgobj.height / fCnt).toString() + "px";
			}
			else {
				imgobj.style.width = ac_width.toString() + "px";
				imgobj.style.height = ac_height.toString() + "px";
			}

			imgobj.style.filter = "progid:DXImageTransform.Microsoft.BasicImage(Rotation=" + nowflip + ")";
		}
	}
	
	//	����ۿO��
	function show_slide(row) {
		var features;
		var mhref = "";

		features = "width=" + (window.screen.availWidth - 10).toString() + "px"; 		// �����e��
		features += ",height=" + (window.screen.availHeight - 60).toString()  + "px"; 	// ��������
		features += ",top=1px";
		features += ",left=1px";
		features += ",toolbar=no,status=yes,resizable=yes,scrollbars=yes";

		mhref = "30017.aspx?al_sid=<%=al_sid%>&rownum=" + row + "&imgw=" + (window.screen.availWidth - 10).toString();
		mhref += "&imgh=" + (window.screen.availHeight - 60).toString() + "&effect=<%=show_effect%>";

		slide = window.open(mhref, "win_slide", features);
	}
	</script>
	</form>
</body>
</html>
