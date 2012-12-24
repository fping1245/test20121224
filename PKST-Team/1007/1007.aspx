<%@ Page Language="C#" AutoEventWireup="true" CodeFile="1007.aspx.cs" Inherits="_1007" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <title>管理人員登入紀錄查詢</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <p align="center" class="text18pt" style="margin: 10pt 0pt 10pt 0pt; font-family: 標楷體;">
                管理人員登入紀錄查詢 (SqlDataSource)</p>
            <table width="98%" border="1" cellspacing="0" cellpadding="4"  bgcolor="#EFEFEF"  class="text9pt"
                align="center" style="margin: 0pt 0pt 5pt 0pt; border-color: #f0f0f0">
                <tr align="center" bgcolor="#99CCFF">
                    <td class="text9pt" style="width: 40pt" rowspan="2">
                        <font color="#FFFFFF">顯示<br />
                            條件</font>
                    </td>
                    <td class="text9pt" style="width: 200pt">
                        <font color="#FFFFFF">時間範圍</font>
                    </td>
                    <td class="text9pt" style="width: 40pt">
                        <font color="#FFFFFF">人員編號</font>
                    </td>
                    <td class="text9pt" style="width: 100pt">
                        <font color="#FFFFFF">姓名</font>
                    </td>
                    <td class="text9pt">
                        <font color="#FFFFFF">主功能</font>
                    </td>
                    <td class="text9pt">
                        <font color="#FFFFFF">子功能</font>
                    </td>
                    <td class="text9pt">
                        <font color="#FFFFFF">IP</font>
                    </td>
                    <td class="text9pt" style="width: 40pt">
                        <font color="#FFFFFF">條件設定</font>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:TextBox ID="tb_btime" Width="86pt" CssClass="text9pt" runat="server" ToolTip="請輸入 西元年/月/日 (yyyy/MM/dd)"></asp:TextBox>
                        ～
                        <asp:TextBox ID="tb_etime" Width="86pt" CssClass="text9pt" runat="server" ToolTip="請輸入 西元年/月/日 (yyyy/MM/dd)"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_mg_sid" runat="server" Width="35pt" CssClass="text9pt" MaxLength="10"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_mg_name" runat="server" Width="65pt" CssClass="text9pt" MaxLength="12"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_fi_name1" runat="server" Width="65pt" CssClass="text9pt" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_fi_name2" runat="server" Width="65pt" CssClass="text9pt" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_lg_ip" runat="server" Width="65pt" CssClass="text9pt" MaxLength="15"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="Btn_Set" runat="server" Text="設定" OnClick="Btn_Set_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv_Mg_Log" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="2" DataSourceID="sds_Mg_Log" Width="98%" BorderStyle="Double" BackColor="White"
                BorderColor="#003366" BorderWidth="1px" CssClass="text9pt" PageSize="20" EmptyDataText="沒有任何管理人員登入紀錄的資料！"
                AllowSorting="True" EnableSortingAndPagingCallbacks="True" EnableModelValidation="True">
                <HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
                <RowStyle BackColor="#F7F7DE" />
                <EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <Columns>
                    <asp:BoundField DataField="lg_time" HeaderText="時間" SortExpression="lg_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}" />
                    <asp:BoundField DataField="mg_sid" HeaderText="人員編號" SortExpression="mg_sid" />
                    <asp:BoundField DataField="mg_name" HeaderText="使用人員" SortExpression="mg_name" />
                    <asp:BoundField DataField="fi_name1" HeaderText="主功能" SortExpression="fi_name1" />
                    <asp:BoundField DataField="fi_name2" HeaderText="子功能" SortExpression="fi_name2" />
                    <asp:BoundField DataField="lg_ip" HeaderText="登入 IP" SortExpression="lg_ip" />
                </Columns>
                <EmptyDataTemplate>
                    目前沒有登入資料</EmptyDataTemplate>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </center>
    </div>
    <asp:SqlDataSource ID="sds_Mg_Log" runat="server" ConnectionString="<%$ ConnectionStrings:AppSysConnectionString %>"
        SelectCommand="Select l.mg_sid, m.mg_name, f1.fi_name1, f2.fi_name2, l.lg_time, l.lg_ip From Mg_Log l
Left Outer Join Manager m On l.mg_sid = m.mg_sid
Left Outer Join Func_Item2 f2 On l.fi_no2 = f2.fi_no2
Left Outer Join Func_Item1 f1 On f2.fi_no1 = f1.fi_no1
Where (lg_time between @btime And @etime) And (l.mg_sid between @mg_sid1 And @mg_sid2) And (mg_name like @mg_name) And (f1.fi_name1 like @fi_name1) And (f2.fi_name2 like @fi_name2) And (l.lg_ip like @lg_ip)
Order by l.lg_time DESC">
        <SelectParameters>
            <asp:Parameter Name="btime" Type="DateTime" />
            <asp:Parameter Name="etime" Type="DateTime" />
            <asp:Parameter Name="mg_sid1" Type="Int32" />
            <asp:Parameter Name="mg_sid2" Type="Int32" />
            <asp:Parameter Name="mg_name" Type="String" />
            <asp:Parameter Name="fi_name1" Type="String" />
            <asp:Parameter Name="fi_name2" Type="String" />
            <asp:Parameter Name="lg_ip" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
