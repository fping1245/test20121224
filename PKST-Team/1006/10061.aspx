<%@ Page Language="C#" AutoEventWireup="true" CodeFile="10061.aspx.cs" Inherits="_10061" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <title>權限設定管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <table cellspacing="0" cellpadding="4" border="1" style="background-color: White;
                border-color: #003366; border-width: 1px; border-style: Double; width: 580pt;
                border-collapse: collapse;">
                <tr align="center" style="background-color: #99FF99">
                    <td colspan="4">
                        主功能
                    </td>
                    <td colspan="4">
                        子功能
                    </td>
                </tr>
                <tr align="center" style="background-color: #99FF99">
                    <td align="center" style="width: 90pt">
                        編號
                    </td>
                    <td style="width: 40pt; background-color: #F7F7DE">
                        <asp:Label ID="lb_fi_no1" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="center" style="width: 90pt">
                        狀態
                    </td>
                    <td style="width: 70pt; background-color: #F7F7DE">
                        <asp:Label ID="lb_visible1" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="center" style="width: 90pt">
                        編號
                    </td>
                    <td style="width: 40pt; background-color: #F7F7DE">
                        <asp:Label ID="lb_fi_no2" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="center" style="width: 90pt">
                        狀態
                    </td>
                    <td style="width: 70pt; background-color: #F7F7DE">
                        <asp:Label ID="lb_visible2" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr align="center" style="background-color: #99FF99">
                    <td align="center" style="width: 90pt">
                        名稱
                    </td>
                    <td style="width: 200pt; background-color: #F7F7DE" colspan="3">
                        <asp:Label ID="lb_fi_name1" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="center" style="width: 90pt">
                        名稱
                    </td>
                    <td style="width: 200pt; background-color: #F7F7DE" colspan="3">
                        <asp:Label ID="lb_fi_name2" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <hr style="width: 98%; margin: 5pt 0pt 5pt 0pt" />
            <table border="0" cellpadding="2" cellspacing="0" class="text12pt" style="width: 580pt;
                margin: 0pt 0pt 5pt 0pt">
                <tr>
                    <td class="text14pt" style="text-align: center; font-family: 標楷體">
                        管理人員名單
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="4"  bgcolor="#EFEFEF"  class="text9pt"
                align="center" style="margin: 0pt 0pt 5pt 0pt; border-color: #f0f0f0; width: 580pt">
                <tr align="center" bgcolor="#99CCFF">
                    <td class="text9pt" width="60" rowspan="2">
                        <font color="#FFFFFF">顯示<br />
                            條件</font>
                    </td>
                    <td class="text9pt">
                        <font color="#FFFFFF">編號</font>
                    </td>
                    <td class="text9pt">
                        <font color="#FFFFFF">帳號</font>
                    </td>
                    <td class="text9pt">
                        <font color="#FFFFFF">姓名</font>
                    </td>
                    <td class="text9pt">
                        <font color="#FFFFFF">昵稱</font>
                    </td>
                    <td class="text9pt">
                        <font color="#FFFFFF">權限</font>
                    </td>
                    <td class="text9pt" width="80">
                        <font color="#FFFFFF">條件設定</font>
                    </td>
                </tr>
                <tr align="center">
                    <td class="text9pt">
                        <asp:TextBox ID="tb_mg_sid" runat="server" Width="30pt" MaxLength="10"></asp:TextBox>
                    </td>
                    <td class="text9pt">
                        <asp:TextBox ID="tb_mg_id" runat="server" Width="55pt" MaxLength="12"></asp:TextBox>
                    </td>
                    <td class="text9pt">
                        <asp:TextBox ID="tb_mg_name" runat="server" Width="65pt" MaxLength="12"></asp:TextBox>
                    </td>
                    <td class="text9pt">
                        <asp:TextBox ID="tb_mg_nike" runat="server" Width="65pt" MaxLength="12"></asp:TextBox>
                    </td>
                    <td class="text9pt">
                        <asp:RadioButton ID="rb_open" runat="server" Text="開放" Checked CssClass="text9pt"
                            GroupName="rb_enable" />
                        <asp:RadioButton ID="rb_close" runat="server" Text="禁止" CssClass="text9pt" GroupName="rb_enable" />
                        <asp:RadioButton ID="rb_all" runat="server" Text="不限制" CssClass="text9pt" GroupName="rb_enable" />
                    </td>
                    <td>
                        <asp:Button ID="Btn_Set" runat="server" Text="設定" OnClick="Btn_Set_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv_Func_Power" runat="server" AllowPaging="True" AllowSorting="True"
                BackColor="White" BorderColor="#003366" BorderStyle="Double" BorderWidth="1px"
                CellPadding="4" Width="580pt" EmptyDataText="沒有任何人員的資料！" ForeColor="#333333"
                CssClass="text9pt" AutoGenerateColumns="False" DataSourceID="ods_Func_Power"
                OnRowDataBound="gv_Func_Power_RowDataBound" OnRowCreated="gv_Func_Power_RowCreated">
                <HeaderStyle BackColor="#CCCCFF" Font-Bold="False" Height="18pt" CssClass="text9pt" />
                <RowStyle BackColor="#F7F7DE" CssClass="text9pt" />
                <EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle"
                    CssClass="text9pt" />
                <Columns>
                    <asp:BoundField DataField="mg_sid" HeaderText="編號" SortExpression="m.mg_sid" ReadOnly="True">
                        <HeaderStyle CssClass="text9pt" Font-Bold="false" />
                        <ItemStyle CssClass="text9pt"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="mg_id" HeaderText="帳號" SortExpression="mg_id" ReadOnly="True">
                        <HeaderStyle CssClass="text9pt" Font-Bold="false" />
                        <ItemStyle CssClass="text9pt"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="mg_name" HeaderText="姓名" SortExpression="mg_name" ReadOnly="True">
                        <HeaderStyle CssClass="text9pt" Font-Bold="false" />
                        <ItemStyle CssClass="text9pt"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="mg_nike" HeaderText="昵稱" SortExpression="mg_nike" ReadOnly="True">
                        <HeaderStyle CssClass="text9pt" Font-Bold="false" />
                        <ItemStyle CssClass="text9pt"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="權限設定">
                        <ItemTemplate>
                            <asp:RadioButton ID="rb_is_open" runat="server" CssClass="text9pt" GroupName="rb_is_enable"
                                Text="開放" OnCheckedChanged="rb_is_open_CheckedChanged" AutoPostBack="True" />&nbsp;
                            <asp:RadioButton ID="rb_is_close" runat="server" CssClass="text9pt" GroupName="rb_is_enable"
                                Text="禁止" OnCheckedChanged="rb_is_close_CheckedChanged" AutoPostBack="True" />
                        </ItemTemplate>
                        <HeaderStyle Width="120pt" CssClass="text9pt" Font-Bold="false"></HeaderStyle>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    沒有任何人員的資料！</EmptyDataTemplate>
                <FooterStyle CssClass="text9pt" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" CssClass="text9pt" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" CssClass="text9pt" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <p style="margin: 10pt 0pt 10pt 0pt">
                ※ 權限的變更，將會在使用者下次登入時生效 ※</p>
            <p style="margin: 10pt 0pt 10pt 0pt">
                <a href="1006.aspx<%=lb_page.Text%>" class="abtn">&nbsp;回上一頁&nbsp;</a></p>
            <br />
        </center>
        <asp:ObjectDataSource ID="ods_Func_Power" runat="server" SelectCountMethod="GetCount_Func_Power"
            SelectMethod="Select_Func_Power" SortParameterName="SortColumn" TypeName="ODS_Func_Power_DataReader"
            EnablePaging="True">
            <SelectParameters>
                <asp:Parameter Name="SortColumn" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
                <asp:Parameter Name="fi_no1" Type="String" DefaultValue="" />
                <asp:Parameter Name="fi_no2" Type="String" DefaultValue="" />
                <asp:Parameter Name="mg_sid" Type="String" />
                <asp:Parameter Name="mg_id" Type="String" />
                <asp:Parameter Name="mg_name" Type="String" />
                <asp:Parameter Name="mg_nike" Type="String" />
                <asp:Parameter Name="is_enable" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:Literal ID="lt_show" runat="server" EnableViewState="False"></asp:Literal>
        <asp:Label ID="lb_page" runat="server" Text="" Visible="false"></asp:Label>
    </div>
    </form>
</body>
</html>
