<%@ Page Language="C#" AutoEventWireup="true" CodeFile="30017.aspx.cs" Inherits="_30017" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
<link href="../css/css.css" rel="stylesheet" type="text/css" />
<title>��ï�޲z</title>
</head>
<body style="background-color:Black">
	<form id="form1" runat="server">
	<div>
	<table id="tb_img" width="100%" border="0" cellpadding="0" cellspacing="0">
	<tr><td align="center" valign="middle">
			<div id="div_img" style="position:absolute; top:0px; left:0px; display:none" onclick="NextImage()">
			<asp:Image ID="img_show" runat="server" BorderStyle="None" />
			</div>
		</td>
	</tr>
	</table>
	<asp:Literal ID="lt_show" runat="server"></asp:Literal>
	</div>
	<script language="javascript" type="text/javascript">
		var ac_width = <%=ac_width%>;
		var ac_height = <%=ac_height%>;
		var rownum = <%=rownum%>;
		var show_effect = <%=show_effect%>;
		var swidth = <%=ac_width%>;
		var sheight = <%=ac_height%>;
		var div_left = 0;				// �����ϥ�

		resize();

		effect_show();

		// ��s�ϫ��ؤo		
		function resize() {
			var tbobj, imgobj, divobj;
			var fcnt = 1.0, wcnt = 1.0, hcnt = 1.0;
			var mwidth = 1, mheight = 1;

			tbobj = document.getElementById("tb_img");
			imgobj = document.getElementById("img_show");
			divobj = document.getElementById("div_img");

			if (tbobj != null) {
				tbobj.style.height = (window.screen.availHeight - 60).toString() + "px";
				tbobj.style.width = (window.screen.availWidth - 25).toString() + "px";
			}

			if (imgobj != null) {
				mwidth = Number(tbobj.style.width.replace("px",""));
				mheight = Number(tbobj.style.height.replace("px",""));

				wcnt = ac_width / mwidth;
				hcnt = ac_height / (mheight - 10);

				fcnt = (wcnt > hcnt) ? wcnt : hcnt;
				
				if (fcnt > 1) {
					swidth = ac_width / fcnt;
					sheight = ac_height / fcnt;
				}
				else {
					swidth = ac_width;
					sheight = ac_height;
				}

				imgobj.style.width = swidth.toString() + "px";
				imgobj.style.height = sheight.toString() + "px";
				
				div_img.style.top = ((mheight - sheight) / 2).toString() + "px";
				div_img.style.left = ((mwidth - swidth) / 2).toString() + "px";

				div_left = (mwidth - swidth) / 2;
			}
		}

		// ��ܯS��
		function effect_show() {
			var divobj, imgobj;
			var iCnt = 0, jCnt = 0, effect = 0;

			divobj = document.getElementById("div_img");
			imgobj = document.getElementById("img_show");
			
			// �H������
			if (show_effect == 0)
				effect = Math.floor(Math.random() * 8 + 1)
			else
				effect = show_effect;
			
			if (divobj != null && imgobj != null) {

				switch (effect) {
					case 1:			// ����
						iCnt = Number(imgobj.style.width.replace("px",""));
						
						div_img.style.left = (-1 * iCnt).toString() + "px";
						
						iCnt = (iCnt + div_left) / 40;
						
						if (iCnt < 2)
							iCnt = 2;
						
						divobj.style.position = "absolute";	
						divobj.style.display = "";
						setTimeout("MoveIn(" + iCnt.toString() + ")", 600);
						break;

					case 2:			// �Y��
						iCnt = Number(imgobj.style.width.replace("px","")) / 40;
						jCnt = Number(imgobj.style.height.replace("px","")) / 40;
						
						imgobj.style.width = "1px";
						imgobj.style.height = "1px";
						
						if (iCnt < 2)
							iCnt = 2;

						if (jCnt < 2)
							jCnt = 2;

						divobj.style.position = "static";
						divobj.style.display = "";
						setTimeout("ZoomOut(" + iCnt.toString() + "," + jCnt.toString() + ")", 600);
						break;

					case 3:			// ����½��
						iCnt = Number(imgobj.style.width.replace("px","")) / 40;
						imgobj.style.width = "1px";
						
						if (iCnt < 2)
							iCnt = 2;
							
						divobj.style.position = "static";
						divobj.style.display = "";
						setTimeout("Flip(" + iCnt.toString() + ")", 100);
						break;

					case 4:			// ����½��
						iCnt = Number(imgobj.style.height.replace("px","")) / 40;
						imgobj.style.height = "1px";
						
						if (iCnt < 2)
							iCnt = 2;
							
						divobj.style.position = "static";
						divobj.style.display = "";
						setTimeout("Flop(" + iCnt.toString() + ")", 100);
						break;

					case 5:			// �H�J�H�X
						imgobj.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=0)";

						divobj.style.position = "static";
						divobj.style.display = "";
						
						setTimeout("Fade(0, 2)", 200);
						break;

					case 6:			// ���ʭ����ĪG
						imgobj.style.filter = "progid:DXImageTransform.Microsoft.Blinds(duration=4,bands=30,Direction=left)";
						imgobj.filters(0).apply();
						imgobj.filters(0).play();

						divobj.style.position = "static";
						divobj.style.display = "";
						setTimeout("NextImage()", 8000);
						break;

					case 7:			// ��ʭ����ĪG
						imgobj.style.filter = "progid:DXImageTransform.Microsoft.Blinds(duration=5,bands=-30,Direction=left)";
						imgobj.filters(0).apply();
						imgobj.filters(0).play();

						divobj.style.position = "static";
						divobj.style.display = "";
						setTimeout("NextImage()", 6000);
						break;
						
					case 8:			// �﨤�Y��
						iCnt = Number(imgobj.style.width.replace("px","")) / 40;
						jCnt = Number(imgobj.style.height.replace("px","")) / 40;
						
						imgobj.style.width = "1px";
						imgobj.style.height = "1px";
						
						if (iCnt < 2)
							iCnt = 2;

						if (jCnt < 2)
							jCnt = 2;

						divobj.style.position = "absolute";
						divobj.style.display = "";
						setTimeout("ZoomOut(" + iCnt.toString() + "," + jCnt.toString() + ")", 600);
						break;

					case 9:			// ���`
						divobj.style.display = "";
						setTimeout("NextImage()", 5000);		// �� 5 ���U�@��
						break;
				}
			}
		}
		
		// �H�J�H�X���ĪG
		function Fade(mCnt, dCnt) {
			var imgobj;
		
			imgobj = document.getElementById("img_show");
			
			if (imgobj != null) {
				mCnt = mCnt + dCnt;

				if (dCnt > 0) {
					if (mCnt > 100) {
						imgobj.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=100)";
						dCnt = -dCnt;
						setTimeout("Fade(100," + dCnt + ")", 5000);
					}
					else {
						imgobj.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=" + mCnt.toString() + ")";
						setTimeout("Fade(" + mCnt + "," + dCnt + ")", 150);
					}
				}
				else {
					if (mCnt < 10) {
						NextImage();							// ���U�@��
					}
					else {
						imgobj.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=" + mCnt.toString() + ")";
						setTimeout("Fade(" + mCnt + "," + dCnt + ")", 80);
					}
				}
			}
		}

		// ����½��
		function Flop(wCnt) {
			var imgobj;
			var mheight = 0;

			imgobj = document.getElementById("img_show");

			if (imgobj != null) {
				mheight = Number(imgobj.style.height.replace("px","")) + wCnt;
				
				if (wCnt > 0) {
					if (mheight > (sheight - 10)) {
						imgobj.style.height = sheight.toString() + "px";
						wCnt = -wCnt;
						setTimeout("Flop(" + wCnt.toString() + ")", 5000);		// �� 5 ��A�Y�p
					}
					else {
						imgobj.style.height = mheight.toString() + "px";
						setTimeout("Flop(" + wCnt.toString() + ")", 20);
					}
				}
				else {
					if (mheight < 10) {
						NextImage();							// ���U�@��
					}
					else {
						imgobj.style.height = mheight.toString() + "px";
						setTimeout("Flop(" + wCnt.toString() + ")", 20);
					}
				}				
			}
		}

		// ����½��
		function Flip(wCnt) {
			var imgobj;
			var mwidth = 0;

			imgobj = document.getElementById("img_show");

			if (imgobj != null) {
				mwidth = Number(imgobj.style.width.replace("px","")) + wCnt;
				
				if (wCnt > 0) {
					if (mwidth > (swidth - 10)) {
						imgobj.style.width = swidth.toString() + "px";
						wCnt = -wCnt;
						setTimeout("Flip(" + wCnt.toString() + ")", 5000);		// �� 5 ��A�Y�p
					}
					else {
						imgobj.style.width = mwidth.toString() + "px";
						setTimeout("Flip(" + wCnt.toString() + ")", 20);
					}
				}
				else {
					if (mwidth < 10) {
						NextImage();							// ���U�@��
					}
					else {
						imgobj.style.width = mwidth.toString() + "px";
						setTimeout("Flip(" + wCnt.toString() + ")", 20);
					}
				}				
			}
		}
		
		// ��j
		function ZoomOut(wCnt, hCnt) {
			var imgobj;
			var mwidth = 0, mheight = 0;
			
			imgobj = document.getElementById("img_show");
			
			if (imgobj != null) {
				mwidth = Number(imgobj.style.width.replace("px","")) + wCnt;
				mheight = Number(imgobj.style.height.replace("px","")) + hCnt;
				
				if (mwidth >= (swidth - 10) || mheight >= (sheight - 10)) {
					imgobj.style.width = swidth.toString() + "px";
					imgobj.style.height = sheight.toString() + "px";
					setTimeout("ZoomIn(" + wCnt.toString() + "," + hCnt.toString() + ")", 5000);		// �� 5 ��A�Y�p
				}
				else {
					imgobj.style.width = mwidth.toString() + "px";
					imgobj.style.height = mheight.toString() + "px";
					setTimeout("ZoomOut(" + wCnt.toString() + "," + hCnt.toString() + ")", 20);
				}
			}
		}

		// �Y�p
		function ZoomIn(wCnt, hCnt) {
			var imgobj;
			var mwidth = 0, mheight = 0;
			
			imgobj = document.getElementById("img_show");
			
			if (imgobj != null) {
				mwidth = Number(imgobj.style.width.replace("px","")) - wCnt;
				mheight = Number(imgobj.style.height.replace("px","")) - hCnt;
				
				if (mwidth < 20 || mheight< 20) {
					NextImage();							// ���U�@��
				}
				else {
					imgobj.style.width = mwidth.toString() + "px";
					imgobj.style.height = mheight.toString() + "px";
					setTimeout("ZoomIn(" + wCnt.toString() + "," + hCnt.toString() + ")", 20);
				}
			}
		}
		
		// �����i�J
		function MoveIn(mCnt) {
			var divobj;
			var iCnt = 0;
			
			divobj = document.getElementById("div_img");
		
			if (divobj != null) {
				iCnt = Number(divobj.style.left.replace("px","")) + mCnt;

				if (iCnt > div_left) {
					divobj.style.left = div_left.toString() + "px";
					setTimeout("MoveOut(" + mCnt.toString() + ")", 5000);		// �� 5 ��A�k��
				}
				else {
					divobj.style.left = iCnt.toString() + "px";
					setTimeout("MoveIn(" + mCnt.toString() + ")", 20);
				}
			}
		}
		
		// �����X�h
		function MoveOut(mCnt) {
			var divobj, tbobj;
			var iCnt = 0, wCnt = 1024;
			
			divobj = document.getElementById("div_img");
			tbobj = document.getElementById("tb_img");
			
			if (divobj != null && tbobj != null) {
				iCnt = Number(divobj.style.left.replace("px","")) + mCnt;
				wCnt = Number(tbobj.style.width.replace("px",""))

				if (iCnt > (div_left + wCnt)) {
					NextImage();							// ���U�@��
				}
				else {
					divobj.style.left = iCnt.toString() + "px";
					setTimeout("MoveOut(" + mCnt.toString() + ")", 20);
				}
			}
		}
		
		// ���U�@��
		function NextImage() {
			var mhref = "";
		
			rownum++;
			
			mhref = "30017.aspx?al_sid=<%=al_sid%>&rownum=" + rownum + "&imgw=" + (window.screen.availWidth - 10).toString();
			mhref += "&imgh" + (window.screen.availHeight - 60).toString() + "&effect=" + show_effect;
			
			location.href = mhref;
		}
	</script>
	</form>
</body>
</html>
