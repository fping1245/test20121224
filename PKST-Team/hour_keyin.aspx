<%@ Page Title="" Language="C#" MasterPageFile="~/thismaster2.master" AutoEventWireup="true" CodeFile="hour_keyin.aspx.cs" Inherits="hour_keyin_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 647px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="DropDownList3" runat="server">
    </asp:DropDownList>
    年<asp:DropDownList ID="DropDownList4" runat="server" 
        onselectedindexchanged="DropDownList4_SelectedIndexChanged">
        <asp:ListItem Value="1">1月</asp:ListItem>
        <asp:ListItem Value="2">2月</asp:ListItem>
        <asp:ListItem Value="3">3月</asp:ListItem>
        <asp:ListItem Value="4">4月</asp:ListItem>
        <asp:ListItem Value="5">5月</asp:ListItem>
        <asp:ListItem Value="6">6月</asp:ListItem>
        <asp:ListItem Value="7">7月</asp:ListItem>
        <asp:ListItem Value="8">8月</asp:ListItem>
        <asp:ListItem Value="9">9月</asp:ListItem>
        <asp:ListItem Value="10">10月</asp:ListItem>
        <asp:ListItem Value="11">11月</asp:ListItem>
        <asp:ListItem Value="12">12月</asp:ListItem>
    </asp:DropDownList>
    月&nbsp;&nbsp;&nbsp; 
    <asp:Button ID="Button3" runat="server" Text="確認" onclick="Button3_Click" />
    <table class="style1">
        <tr>
            <td class="style2">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource1" 
                DataTextField="TeacherName" DataValueField="TeacherName" Height="178px" 
                Width="116px" onselectedindexchanged="ListBox1_SelectedIndexChanged"></asp:ListBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="+" 
                Width="21px" />
            <asp:DropDownList ID="DropDownList1" runat="server" 
                DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="Name" 
                Width="90px">
            </asp:DropDownList>
            <br />
            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="-" 
                Width="21px" />
            <asp:DropDownList ID="DropDownList2" runat="server" 
                DataSourceID="SqlDataSource3" DataTextField="TeacherName" 
                DataValueField="TeacherName" Width="90px">
            </asp:DropDownList>
        </ContentTemplate>
    </asp:UpdatePanel>
            </td>
            <td style="text-align: justify">
                <asp:GridView ID="GridView1" runat="server" Height="133px" Width="463px" 
                    AutoGenerateColumns="False" DataKeyNames="TeacherName,year,month" 
                    DataSourceID="SqlDataSource4" CellPadding="4" ForeColor="#333333" 
                    GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="TeacherName" HeaderText="教師姓名" 
                            ReadOnly="True" SortExpression="TeacherName" />
                        <asp:BoundField DataField="year" HeaderText="年份" SortExpression="year" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="month" HeaderText="月份" 
                            SortExpression="month" ReadOnly="True" />
                        <asp:BoundField DataField="hours" HeaderText="專題時數" SortExpression="hours" />
                        <asp:BoundField DataField="id_class" HeaderText="班級編號" 
                            SortExpression="id_class" />
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PKSTConnectionString2 %>" 
                    DeleteCommand="DELETE FROM [hour_project] WHERE [TeacherName] = @original_TeacherName AND [year] = @original_year AND [month] = @original_month" 
                    InsertCommand="INSERT INTO [hour_project] ([TeacherName], [year], [month], [hours], [id_class]) VALUES (@TeacherName, @year, @month, @hours, @id_class)" 
                    OldValuesParameterFormatString="original_{0}" 
                    SelectCommand="SELECT [TeacherName], [year], [month], [hours], [id_class] FROM [hour_project] WHERE (([year] = @year) AND ([month] = @month))" 
                    
                    UpdateCommand="UPDATE [hour_project] SET [hours] = @hours, [id_class] = @id_class WHERE [TeacherName] = @original_TeacherName AND [year] = @original_year AND [month] = @original_month">
                    <DeleteParameters>
                        <asp:Parameter Name="original_TeacherName" Type="String" />
                        <asp:Parameter Name="original_year" Type="String" />
                        <asp:Parameter Name="original_month" Type="String" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="TeacherName" Type="String" />
                        <asp:Parameter Name="year" Type="String" />
                        <asp:Parameter Name="month" Type="String" />
                        <asp:Parameter Name="hours" Type="String" />
                        <asp:Parameter Name="id_class" Type="String" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList3" Name="year" 
                            PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="DropDownList4" Name="month" 
                            PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="hours" Type="String" />
                        <asp:Parameter Name="id_class" Type="String" />
                        <asp:Parameter Name="original_TeacherName" Type="String" />
                        <asp:Parameter Name="original_year" Type="String" />
                        <asp:Parameter Name="original_month" Type="String" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
        SelectCommand="SELECT [TeacherName] FROM [hour_project_teacherlist]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TMSClassroomSelect %>" 
        SelectCommand="SELECT [EmployeeID], [Name] FROM [Tteacher]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
        SelectCommand="SELECT * FROM [hour_project_teacherlist]">
    </asp:SqlDataSource>
    <br />
    <br />
    <br />
    <br />
</asp:Content>

