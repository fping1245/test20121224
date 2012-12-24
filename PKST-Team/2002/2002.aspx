<%@ Page Language="C#" AutoEventWireup="true" CodeFile="2002.aspx.cs" Inherits="_2002" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF8" />
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <title>檔案上傳下載</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <p align="center" class="text18pt" style="margin: 10pt 0pt 10pt 0pt; font-family: 標楷體;">
                檔案上傳下載 (以資料庫存放檔案)</p>
            <table width="98%" border="1" cellspacing="0" cellpadding="4"  bgcolor="#EFEFEF"  class="text9pt"
                align="center" style="margin: 0pt 0pt 5pt 0pt; border-color: #f0f0f0">
                <tr align="center" bgcolor="#99CCFF">
                    <td class="text9pt" width="60" rowspan="2">
                        <font color="#FFFFFF">顯示<br />
                            條件</font>
                    </td>
                    <td class="text9pt">
                        <font color="#FFFFFF">檔案名稱</font>
                    </td>
                    <td class="text9pt">
                        <font color="#FFFFFF">副檔名</font>
                    </td>
                    <td class="text9pt">
                        <font color="#FFFFFF">備註說明</font>
                    </td>
                    <td class="text9pt" width="80">
                        <font color="#FFFFFF">條件設定</font>
                    </td>
                    <td class="text9pt" width="90">
                        <font color="#FFFFFF">上傳檔案</font>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:TextBox ID="tb_fc_name" runat="server" Width="120pt"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_fc_ext" runat="server" Width="90pt" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="tb_fc_desc" runat="server" Width="120pt"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="Btn_Set" runat="server" Text="設定" OnClick="Btn_Set_Click" />
                    </td>
                    <td>
                        <a href="2002_add.aspx?pageid=<%=lb_pageid.Text %>&fc_name=<%=Server.UrlEncode(tb_fc_name.Text)%>&fc_ext=<%=Server.UrlEncode(tb_fc_ext.Text)%>&fc_desc=<%=Server.UrlEncode(tb_fc_desc.Text)%>"
                            class="abtn">&nbsp;上傳檔案&nbsp;</a>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv_Fi_Content" runat="server" AutoGenerateColumns="False" DataKeyNames="fc_sid"
                DataSourceID="ods_Fi_Content" AllowPaging="True" BackColor="White" BorderColor="#003366"
                BorderStyle="Double" BorderWidth="1px" CellPadding="4" Width="98%" EmptyDataText="沒有任何檔案的資料！"
                HorizontalAlign="Center" OnPageIndexChanging="gv_Fi_Content_PageIndexChanged"
                AllowSorting="True" OnRowCreated="gv_Fi_Content_RowCreated" OnRowDataBound="gv_Fi_Content_RowDataBound">
                <HeaderStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" Height="18pt" />
                <RowStyle BackColor="#F7F7DE" />
                <EmptyDataRowStyle BackColor="#FFFFCC" Height="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <Columns>
                    <asp:BoundField DataField="fc_sid" HeaderText="編號" InsertVisible="False" ReadOnly="True"
                        SortExpression="fc_sid">
                        <HeaderStyle HorizontalAlign="Center" Width="40pt" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fc_name" HeaderText="檔案名稱" SortExpression="fc_name">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fc_size" HeaderText="檔案大小" SortExpression="fc_size" DataFormatString="{0:N0}">
                        <HeaderStyle HorizontalAlign="Center" Width="100pt" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fc_ext" HeaderText="副檔名" SortExpression="fc_ext" />
                    <asp:BoundField DataField="fc_type" HeaderText="類型" SortExpression="fc_type">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="up_time" HeaderText="上傳時間" SortExpression="up_time" DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
                        <HeaderStyle HorizontalAlign="Center" Width="100pt" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="下載" ShowHeader="False">
                        <ItemTemplate>
                            <a href="20021.ashx?sid=<%# Eval("fc_sid") %>" target="_blank" class="abtn">&nbsp;下載&nbsp;</a>
                        </ItemTemplate>
                        <HeaderStyle Width="45pt" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lk_del" runat="server" CssClass="abtn" Text="&nbsp;刪除&nbsp;"
                                OnClick="lk_del_Click" ToolTip="使用 .Net 方式來刪除資料"></asp:LinkButton>&nbsp;
                            <asp:Literal ID="lt_table" runat="server"></asp:Literal>
                        </ItemTemplate>
                        <HeaderStyle Width="45pt" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="fc_desc" HeaderText="備註說明">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                    沒有任何檔案的資料！</EmptyDataTemplate>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </center>
        <asp:ObjectDataSource ID="ods_Fi_Content" runat="server" EnablePaging="True" SelectCountMethod="GetCount_Fi_Content"
            SelectMethod="Select_Fi_Content" SortParameterName="SortColumn" TypeName="ODS_Fi_Content_DataReader">
            <SelectParameters>
                <asp:Parameter Name="SortColumn" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
                <asp:Parameter Name="fl_no" Type="Int32" />
                <asp:Parameter Name="fc_name" Type="String" />
                <asp:Parameter Name="fc_ext" Type="String" />
                <asp:Parameter Name="fc_desc" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:Label ID="lb_pageid" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Literal ID="lt_show" runat="server" EnableViewState="False"></asp:Literal>
    </div>
    </form>
</body>
</html>
