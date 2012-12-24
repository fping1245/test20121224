<%@ Page Language="C#" AutoEventWireup="true" CodeFile="2001_add.aspx.cs" Inherits="_2001_add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <title>檔案上傳下載</title>

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
                檔案上傳下載 (實體路徑存放檔案)</p>
            <p align="center" class="text16pt" style="margin: 0pt 0pt 5pt 0pt; font-family: 標楷體;">
                上傳檔案</p>
            <table width="90%" border="1"  bgcolor="#EFEFEF"  cellpadding="0" cellspacing="2" class="text9pt">
                <tr align="center" style="height: 26pt" bgcolor="#99CCFF">
                    <td style="width: 40pt">
                        <font color="#FFFFFF">順序</font>
                    </td>
                    <td width="45%">
                        <font color="#FFFFFF">檔案選擇</font>
                    </td>
                    <td>
                        <font color="#FFFFFF">檔案說明</font>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="lightyellow" class="text12pt">
                        1
                    </td>
                    <td>
                        <asp:FileUpload ID="fu_file1" Width="96%" Height="18pt" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="tb_file1" Width="96%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="lightyellow" class="text12pt">
                        2
                    </td>
                    <td>
                        <asp:FileUpload ID="fu_file2" Width="96%" Height="18pt" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="tb_file2" Width="96%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="lightyellow" class="text12pt">
                        3
                    </td>
                    <td>
                        <asp:FileUpload ID="fu_file3" Width="96%" Height="18pt" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="tb_file3" Width="96%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="lightyellow" class="text12pt">
                        4
                    </td>
                    <td>
                        <asp:FileUpload ID="fu_file4" Width="96%" Height="18pt" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="tb_file4" Width="96%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="lightyellow" class="text12pt">
                        5
                    </td>
                    <td>
                        <asp:FileUpload ID="fu_file5" Width="96%" Height="18pt" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="tb_file5" Width="96%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="lightyellow" class="text12pt">
                        6
                    </td>
                    <td>
                        <asp:FileUpload ID="fu_file6" Width="96%" Height="18pt" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="tb_file6" Width="96%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="lightyellow" class="text12pt">
                        7
                    </td>
                    <td>
                        <asp:FileUpload ID="fu_file7" Width="96%" Height="18pt" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="tb_file7" Width="96%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="lightyellow" class="text12pt">
                        8
                    </td>
                    <td>
                        <asp:FileUpload ID="fu_file8" Width="96%" Height="18pt" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="tb_file8" Width="96%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="lightyellow" class="text12pt">
                        9
                    </td>
                    <td>
                        <asp:FileUpload ID="fu_file9" Width="96%" Height="18pt" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="tb_file9" Width="96%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="lightyellow" class="text12pt">
                        10
                    </td>
                    <td>
                        <asp:FileUpload ID="fu_file10" Width="96%" Height="18pt" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="tb_file10" Width="96%" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="bn_upload" runat="server" Height="22pt" Text="上傳檔案" OnClick="bn_upload_Click"
                OnClientClick="msg_show()" />&nbsp;
            <asp:Button ID="bn_back" runat="server" Height="22pt" Text="回上一頁" OnClick="bn_back_Click" />
        </center>
        <asp:Literal ID="lt_show" runat="server"></asp:Literal>
        <asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
    </div>
    </form>
</body>
</html>
