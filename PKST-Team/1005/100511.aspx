<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100511.aspx.cs" Inherits="_100511" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf8" />
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <title>人員資料管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p align="center" class="text18pt" style="margin: 10pt 0pt 5pt 0pt; font-family: 標楷體;">
            人員資料管理</p>
        <p align="center" class="text16pt" style="margin: 0pt 0pt 5pt 0pt; font-family: 標楷體;">
            權限設定</p>
        <center>
            <table cellspacing="0" cellpadding="4" rules="all" border="1" style="background-color: White;
                border-color: #003366; border-width: 1px; border-style: Double; width: 580pt;
                border-collapse: collapse;">
                <tr align="left" style="background-color: #99FF99; height: 18pt">
                    <td align="center" style="width: 90pt">
                        編號
                    </td>
                    <td style="width: 200pt; background-color: #F7F7DE">
                        <asp:Label ID="lb_mg_sid" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="center" style="width: 90pt">
                        帳號
                    </td>
                    <td style="width: 200pt; background-color: #F7F7DE">
                        <asp:Label ID="lb_mg_id" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr align="left" style="background-color: #99FF99; height: 18pt">
                    <td align="center">
                        姓名
                    </td>
                    <td style="background-color: #F7F7DE">
                        <asp:Label ID="lb_mg_name" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="center">
                        暱稱
                    </td>
                    <td style="background-color: #F7F7DE">
                        <asp:Label ID="lb_mg_nike" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <hr style="width: 98%; margin: 5pt 0pt 5pt 0pt" />
            <table border="0" cellpadding="2" cellspacing="0" class="text12pt" style="width: 580pt;
                margin: 0pt 0pt 5pt 0pt">
                <tr>
                    <td style="width: 120pt">
                        &nbsp
                    </td>
                    <td class="text14pt" style="text-align: center; font-family: 標楷體">
                        管理人員權限
                    </td>
                    <td style="width: 120pt; text-align: right">
                        <asp:Button ID="bn_all_open" runat="server" Text="全部開放" CssClass="text9pt" OnClick="bn_all_open_Click" />
                        <asp:Button ID="bn_all_close" runat="server" Text="全部禁止" CssClass="text9pt" OnClick="bn_all_close_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv_Func_Power" runat="server" DataSourceID="sqs_Func_Power" BackColor="White"
                BorderColor="#003366" BorderStyle="Double" BorderWidth="1px" CellPadding="4"
                Width="580pt" EmptyDataText="沒有任何權限的資料！" ForeColor="#333333" CssClass="text9pt"
                AutoGenerateColumns="False" OnRowDataBound="gv_Func_Power_RowDataBound">
                <HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt"
                    CssClass="text9pt" />
                <RowStyle BackColor="#F7F7DE" CssClass="text9pt" />
                <EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle"
                    CssClass="text9pt" />
                <Columns>
                    <asp:BoundField DataField="fi_name1" HeaderText="主功能" SortExpression="fi_name1" ReadOnly="True">
                        <ItemStyle CssClass="text9pt"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="fi_name2" HeaderText="子功能" SortExpression="fi_name2" ReadOnly="True">
                        <ItemStyle CssClass="text9pt"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="權限設定">
                        <ItemTemplate>
                            <asp:Label ID="lb_fi_no1" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lb_fi_no2" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:RadioButton ID="rb_open" runat="server" CssClass="text9pt" GroupName="rb_is_enable"
                                Text="開放" OnCheckedChanged="rb_open_CheckedChanged" />&nbsp;
                            <asp:RadioButton ID="rb_close" runat="server" CssClass="text9pt" GroupName="rb_is_enable"
                                Text="禁止" OnCheckedChanged="rb_close_CheckedChanged" />
                        </ItemTemplate>
                        <HeaderStyle Width="120pt"></HeaderStyle>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    沒有任何權限的資料！</EmptyDataTemplate>
                <FooterStyle CssClass="text9pt" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" CssClass="text9pt" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" CssClass="text9pt" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <p style="margin: 10pt 0pt 10pt 0pt">
                <asp:LinkButton ID="lb_ok" runat="server" CssClass="abtn" OnClick="lb_ok_Click">&nbsp;確定儲存&nbsp;</asp:LinkButton>&nbsp;&nbsp;
                <a href="10051.aspx<%=lb_page.Text%>" class="abtn">&nbsp;回上一頁&nbsp;</a>
            </p>
            &nbsp;
        </center>
        <asp:Literal ID="lt_show" runat="server"></asp:Literal>
        <asp:HiddenField ID="hf_enable" runat="server" Value="2" />
        <asp:Label ID="lb_enable" runat="server" Text="2" Visible="false"></asp:Label>
        <asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lb_pg_mg_sid" runat="server" Text="-1" Visible="false"></asp:Label>
        <asp:SqlDataSource ID="sqs_Func_Power" runat="server" ConnectionString="<%$ ConnectionStrings:AppSysConnectionString %>"
            SelectCommand="Select IsNull(f.is_enable, 0) as is_enable, f2.fi_no1, f2.fi_no2, f1.fi_name1, f2.fi_name2 From Func_Item2 f2
Inner Join Func_Item1 f1 On f2.fi_no1 = f1.fi_no1
Left Outer Join Func_Power f On f2.fi_no2 = f.fi_no2 And f.mg_sid = @mg_sid
Where f2.is_visible = 1 Order by f1.fi_sort1, f2.fi_sort2">
            <SelectParameters>
                <asp:Parameter Name="mg_sid" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
