<%@ Page Language="C#" AutoEventWireup="true" CodeFile="2003.aspx.cs" Inherits="_2003" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <title>網站檔案管理</title>

    <script language="javascript" type="text/javascript">
	function msg_show() {
		var msgobj, fullobj, tableobj;
		msgobj = document.getElementById("msg_wait");
		if (msgobj != null) {
			msgobj.style.left = String((document.body.clientWidth - 240) / 2) + "px";
			msgobj.style.top = String((document.body.clientHeight - 240) / 2) + "px";
			msgobj.style.display = "";
		}

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

	function msg_close() {
		var msgobj;
		msgobj = document.getElementById("msg_wait");
		if (msgobj != null)
			msgobj.style.display = "none";

		fullobj = document.getElementById("fullwindow");
		if (fullobj != null)
			fullobj.style.display = "none";
	}
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!-- Begin 覆蓋整個工作頁面，不讓使用者再按其它按鈕 -->
        <div id="fullwindow" style="position: absolute; top: 0px; left: 0px; width: 1024px;
            height: 768px; display: none">
            <table id="fulltable" border="0" cellpadding="0" cellspacing="0" style="background-image: url(../images/msg_back.gif)">
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <!-- End -->
        <!-- Begin 顯示上傳訊息 -->
        <div id="msg_wait" class="text9pt" style="position: absolute; width: 240px; z-index: 0;
            display: none;">
            <table border="2" cellpadding="2" cellspacing="2" style="width: 240px; height: 100px;"
                bgcolor="#efefef">
                <tr>
                    <td align="center" class="text9pt">
                        資料上傳中 ........<br />
                        請勿按任何按鍵!
                    </td>
                </tr>
            </table>
        </div>
        <!-- End -->
        <center>
            <p align="center" class="text18pt" style="margin: 10pt 0pt 10pt 0pt; font-family: 標楷體;">
                網站檔案管理</p>
            <table width="95%" border="0" cellpadding="4" cellspacing="0" style="background-color: #efef90">
                <tr>
                    <td align="left" class="text12pt" style="font-family: 標楷體">
                        網站位置：<asp:Label ID="lb_url" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" class="text12pt" style="font-family: 標楷體">
                        實體位置：<asp:Label ID="lb_path" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="95%" border="0" cellpadding="2" cellspacing="0" style="margin: 5pt 0pt 0pt 0pt">
                <tr>
                    <td align="left">
                        <asp:Button ID="bn_go_root" runat="server" Text="到根目錄" Height="22pt" OnClick="bn_go_root_Click" />
                        <asp:Button ID="bn_mk_dir" runat="server" Text="建子目錄" Height="22pt" OnClick="bn_mk_dir_Click" />
                        <asp:Button ID="bn_fu_file" runat="server" Text="上傳檔案" Height="22pt" OnClick="bn_fu_file_Click" />
                    </td>
                    <td align="right" class="text12pt">
                        &nbsp;
                        <asp:Literal ID="lt_mk_dir" runat="server" Text="建立子目錄的名稱：" Visible="False"></asp:Literal>
                        <asp:TextBox ID="tb_dir" runat="server" Visible="false" Height="16pt" CssClass="text9pt"
                            EnableViewState="False"></asp:TextBox>
                        <asp:Button ID="bn_mk_dir_ok" runat="server" Text="確定" Visible="false" Height="22pt"
                            OnClick="bn_mk_dir_ok_Click" />
                        <asp:Literal ID="lt_upfile" runat="server" Text="請選擇上傳的檔案：" Visible="False" EnableViewState="False"></asp:Literal>
                        <asp:FileUpload ID="fu_file" runat="server" Visible="false" Height="22pt" CssClass="text10pt"
                            EnableViewState="False" />
                        <asp:Button ID="bn_fu_file_ok" runat="server" Text="確定" Visible="false" Height="22pt"
                            OnClick="bn_fu_file_ok_Click" OnClientClick="msg_show()" />
                        <asp:Button ID="bn_cancel" runat="server" Text="取消" Visible="false" Height="22pt"
                            OnClick="bn_cancel_Click" EnableViewState="False" />
                    </td>
                </tr>
            </table>
            <hr style="width: 96%" />
            <asp:Literal ID="lt_data" runat="server"></asp:Literal>
            <p style="width: 94%; text-align: left; margin: 5pt 0pt 0pt 0pt">
                ※ 點選「檔案名稱」可下載檔案。</p>
            <p style="width: 94%; text-align: left; margin: 0pt 0pt 0pt 0pt">
                ※ 子目錄有檔案時，不允許刪除。</p>
        </center>
        <iframe name="update" src="" width="0" height="0" scrolling="no" frameborder="0"
            style="display: none"></iframe>

        <script language="javascript" type="text/javascript">
		function delfile(fname) {
			if (confirm("確定要刪除「" + fname + "」?\n")) {
				update.location.replace("20031.ashx?fname=" + escape(fname) + "&furl=<%=lb_url_encode.Text %>");
			}
		}

		function delpath(fpath) {
			if (confirm("確定要刪除子目錄「" + fpath + "」?\n(子目錄要清空才能刪除!)\n")) {
				update.location.replace("20032.ashx?fpath=" + escape(fpath) + "&furl=<%=lb_url_encode.Text %>");
			}
		}

		function renfile(fname) {
			var nfname = "";
			nfname = prompt("更改「" + fname + "」的檔案名稱為", "");
			if (nfname != null) {
				update.location.replace("20033.ashx?fname=" + escape(fname) + "&nfname=" + escape(nfname) + "&furl=<%=lb_url_encode.Text %>");
			}
		}

		function renpath(fpath) {
			var nfpath = "";
			nfpath = prompt("更改「" + fpath + "」的目錄名稱為", "");
			if (nfpath != null) {
				update.location.replace("20034.ashx?fpath=" + escape(fpath) + "&nfpath=" + escape(nfpath) + "&furl=<%=lb_url_encode.Text %>");
			}
		}
        </script>

        <asp:Label ID="lb_fl_url" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lb_url_encode" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lb_fl_url_encode" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Literal ID="lt_show" runat="server" EnableViewState="False"></asp:Literal>
    </div>
    </form>
</body>
</html>
