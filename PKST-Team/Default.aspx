<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
    <link href="css/css.css" rel="stylesheet" type="text/css" />
    <title>系統登入</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <center>
            <p style="font-family: 標楷體; font-size: 18pt; margin: 15pt 0pt 5pt 0pt">
                <b>登入系統</b></p>
            <table width="280" cellpadding="6" cellspacing="0" border="1" class="text12pt" align="center">
                <tr>
                    <td width="60" bgcolor="lightyellow" align="center" class="text12pt">
                        帳號
                    </td>
                    <td align="center">
                        <asp:TextBox ID="tb_id" runat="server" MaxLength="12" CssClass="text12pt" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="lightyellow" align="center" class="text12pt">
                        密碼
                    </td>
                    <td align="center">
                        <asp:TextBox ID="tb_pass" runat="server" MaxLength="12" CssClass="text12pt" Width="150px"
                            TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="lightyellow" align="center" class="text12pt">
                        驗證
                    </td>
                    <td align="center" class="text12pt">
                        請將下方的數字填入<br />
                        <asp:TextBox ID="tb_confirm" runat="server" MaxLength="4" CssClass="text12pt" Width="150px"
                            EnableViewState="False"></asp:TextBox><br />
                        <asp:Image ID="img_confirm" ImageUrl="confirm.ashx" runat="server" Height="54px"
                            Width="200px" /><br />
                        <asp:Button ID="bn_new_confirm" runat="server" Text="重新產生驗證圖示" CssClass="text9pt"
                            OnClick="bn_new_confirm_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" bgcolor="#fff0f0" align="center">
                        <asp:Button ID="bn_ok" runat="server" Text="確定" CssClass="text12pt" OnClick="bn_ok_Click" />&nbsp;
                        <asp:Button ID="bn_reset" runat="server" Text="重輸" CssClass="text12pt" OnClick="bn_reset_Click" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="" cellspacing="0" class="text9pt" style="width: 200px">
                <tr>
                    <td align="left">
                        ID: Admin
                    </td>
                    <td align="left">
                        PW: abc123
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        ID: user1
                    </td>
                    <td align="left">
                        PW: abc123
                    </td>
                </tr>
            </table>
        </center>

        <script language="javascript" type="text/javascript">
            function renew_img() {
                var img, now;
                now = new Date();
                img = document.getElementById("img_confirm");
                img.src = "confirm.ashx?ti=" + now.getSeconds().toString() + now.getMilliseconds().toString();
            }
        </script>

        <asp:Literal ID="lt_show" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
