<%@ Page Language="C#" AutoEventWireup="true" CodeFile="1005.aspx.cs" Inherits="_1005" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <title>人員資料管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p align="center" class="text18pt" style="margin: 10pt 0pt 5pt 0pt; font-family: 標楷體;">
            人員資料管理</p>
        <table width="98%" border="1" cellspacing="0" cellpadding="4"  bgcolor="#EFEFEF"  class="text9pt"
            align="center" style="margin: 0pt 0pt 5pt 0pt; border-color: #f0f0f0">
            <tr align="center" bgcolor="#99CCFF">
                <td class="text9pt" width="60" rowspan="2">
                    <font color="#FFFFFF">顯示<br />
                        條件</font>
                </td>
                <td class="text9pt" width="60">
                    <font color="#FFFFFF">編號</font>
                </td>
                <td class="text9pt">
                    <font color="#FFFFFF">姓名</font>
                </td>
                <td class="text9pt">
                    <font color="#FFFFFF">暱稱</font>
                </td>
                <td class="text9pt">
                    <font color="#FFFFFF">最後登入時間範圍</font>
                </td>
                <td class="text9pt" width="80">
                    <font color="#FFFFFF">條件設定</font>
                </td>
                <td class="text9pt" width="90">
                    <font color="#FFFFFF">新增資料</font>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:TextBox ID="tb_mg_sid" runat="server" Width="40pt" MaxLength="5"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="tb_mg_name" runat="server" Width="90pt" MaxLength="12"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="tb_mg_nike" runat="server" Width="90pt" MaxLength="12"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="tb_btime" runat="server" Width="90pt" MaxLength="16" ToolTip="請輸入 西元年/月/日 (yyyy/MM/dd)"></asp:TextBox>
                    &nbsp;～&nbsp;
                    <asp:TextBox ID="tb_etime" runat="server" Width="90pt" MaxLength="16" ToolTip="請輸入 西元年/月/日 (yyyy/MM/dd)"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Btn_Set" runat="server" Text="設定" OnClick="Btn_Set_Click" />
                </td>
                <td>
                    <a href="10051_add.aspx?pageid=<%=lb_pageid.Text %>&mg_sid=<%=Server.UrlEncode(tb_mg_sid.Text)%>&mg_name=<%=Server.UrlEncode(tb_mg_name.Text)%>&mg_nike=<%=Server.UrlEncode(tb_mg_nike.Text)%>&btime=<%=Server.UrlEncode(tb_btime.Text)%>&etime=<%=Server.UrlEncode(tb_etime.Text)%>"
                        class="abtn">&nbsp;新增資料&nbsp;</a>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv_Manager" runat="server" AutoGenerateColumns="False" DataKeyNames="mg_sid"
            DataSourceID="ods_Manager" AllowPaging="True" BackColor="White" BorderColor="#003366"
            BorderStyle="Double" BorderWidth="1px" CellPadding="4" Width="98%" EmptyDataText="沒有任何管理人員的資料！"
            HorizontalAlign="Center" OnPageIndexChanging="gv_Manager_PageIndexChanged" AllowSorting="True">
            <HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
            <RowStyle BackColor="#F7F7DE" />
            <EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="mg_sid" HeaderText="編號" InsertVisible="False" ReadOnly="True"
                    SortExpression="mg_sid">
                    <HeaderStyle HorizontalAlign="Center" Width="40pt" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="mg_name" HeaderText="姓名" SortExpression="mg_name" />
                <asp:BoundField DataField="mg_nike" HeaderText="暱稱" SortExpression="mg_nike" />
                <asp:BoundField DataField="mg_unit" HeaderText="單位" SortExpression="mg_unit" />
                <asp:BoundField DataField="mg_id" HeaderText="帳號" SortExpression="mg_id" />
                <asp:BoundField DataField="last_date" HeaderText="最後登入時間" SortExpression="last_date"
                    DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
                    <HeaderStyle HorizontalAlign="Center" Width="100pt" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="init_time" HeaderText="最後修改時間" SortExpression="init_time"
                    DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
                    <HeaderStyle HorizontalAlign="Center" Width="100pt" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="內容" ShowHeader="False">
                    <ItemTemplate>
                        <a href="10051.aspx?sid=<%# Eval("mg_sid") %>&pageid=<%# gv_Manager.PageIndex %>&mg_sid=<%# tb_mg_sid.Text %>&mg_name=<%# Server.UrlEncode(tb_mg_name.Text) %>&mg_nike=<%# Server.UrlEncode(tb_mg_nike.Text) %>&btime=<%# Server.UrlEncode(tb_btime.Text) %>&etime=<%# Server.UrlEncode(tb_etime.Text) %>"
                            class="abtn">&nbsp;內容&nbsp;</a>
                    </ItemTemplate>
                    <HeaderStyle Width="45pt" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                沒有任何管理人員的資料！</EmptyDataTemplate>
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ods_Manager" runat="server" EnablePaging="True" SelectCountMethod="GetCount_Manager"
            SelectMethod="Select_Manager" SortParameterName="SortColumn" TypeName="ODS_Manager_DataReader">
            <SelectParameters>
                <asp:Parameter Name="SortColumn" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
                <asp:Parameter Name="mg_sid" Type="String" />
                <asp:Parameter Name="mg_name" Type="String" />
                <asp:Parameter Name="mg_nike" Type="String" />
                <asp:Parameter Name="btime" Type="String" />
                <asp:Parameter Name="etime" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
    </div>
    </form>
</body>
</html>
