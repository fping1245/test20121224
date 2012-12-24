<%@ Page Title="" Language="C#" MasterPageFile="~/thismaster2.master" AutoEventWireup="true" CodeFile="out_in_ok.aspx.cs" Inherits="out_in_ok" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
    
        SelectCommand="SELECT [employeeType], [name] FROM [teacher] WHERE ([employeeType] = @employeeType)">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList4" Name="employeeType" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropDownList4" runat="server" 
        AutoPostBack="True">
                    <asp:ListItem Value="1">正式員工</asp:ListItem>
                    <asp:ListItem Value="5">外聘講師</asp:ListItem>
                </asp:DropDownList>
    &nbsp;&nbsp;
    <asp:DropDownList ID="DropDownList1" runat="server" 
    DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="name" 
        AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
        >
</asp:DropDownList>
            <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" DataKeyNames="user_name,CourseID,CourseName" 
        DataSourceID="SqlDataSource3" GridLines="None" Height="88px" Width="581px" 
        style="text-align: center" ForeColor="#333333">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="user_name" HeaderText="教師姓名" ReadOnly="True" 
                SortExpression="user_name" />
            <asp:BoundField DataField="CourseID" HeaderText="課程代號" ReadOnly="True" 
                SortExpression="CourseID" />
            <asp:BoundField DataField="CourseName" HeaderText="課程名稱" ReadOnly="True" 
                SortExpression="CourseName" />
            <asp:BoundField DataField="Length" HeaderText="時   數" SortExpression="Length" />
            <asp:BoundField DataField="hr_money" HeaderText="鐘點費" 
                SortExpression="hr_money" />
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:PKST_gridview %>" 
        DeleteCommand="DELETE FROM [out_teacher] WHERE [user_name] = @original_user_name AND [CourseName] = @original_CourseName AND [CourseID] = @original_CourseID AND (([Length] = @original_Length) OR ([Length] IS NULL AND @original_Length IS NULL)) AND (([hr_money] = @original_hr_money) OR ([hr_money] IS NULL AND @original_hr_money IS NULL))" 
        InsertCommand="INSERT INTO [out_teacher] ([user_name], [CourseID], [CourseName], [Length], [hr_money]) VALUES (@user_name, @CourseID, @CourseName, @Length, @hr_money)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT [user_name], [CourseID], [CourseName], [Length], [hr_money] FROM [out_teacher] WHERE ([user_name] = @user_name)" 
        UpdateCommand="UPDATE [out_teacher] SET [CourseID] = @CourseID, [Length] = @Length, [hr_money] = @hr_money WHERE [user_name] = @original_user_name AND [CourseName] = @original_CourseName AND [CourseID] = @original_CourseID AND (([Length] = @original_Length) OR ([Length] IS NULL AND @original_Length IS NULL)) AND (([hr_money] = @original_hr_money) OR ([hr_money] IS NULL AND @original_hr_money IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_user_name" Type="String" />
            <asp:Parameter Name="original_CourseName" Type="String" />
            <asp:Parameter Name="original_CourseID" Type="String" />
            <asp:Parameter Name="original_Length" Type="String" />
            <asp:Parameter Name="original_hr_money" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="user_name" Type="String" />
            <asp:Parameter Name="CourseID" Type="String" />
            <asp:Parameter Name="CourseName" Type="String" />
            <asp:Parameter Name="Length" Type="String" />
            <asp:Parameter Name="hr_money" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="user_name" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="CourseID" Type="String" />
            <asp:Parameter Name="Length" Type="String" />
            <asp:Parameter Name="hr_money" Type="String" />
            <asp:Parameter Name="original_user_name" Type="String" />
            <asp:Parameter Name="original_CourseName" Type="String" />
            <asp:Parameter Name="original_CourseID" Type="String" />
            <asp:Parameter Name="original_Length" Type="String" />
            <asp:Parameter Name="original_hr_money" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
        DeleteCommand="DELETE FROM [out_teacher] WHERE [user_name] = @original_user_name AND [CourseName] = @original_CourseName AND (([CourseID] = @original_CourseID) OR ([CourseID] IS NULL AND @original_CourseID IS NULL)) AND (([Length] = @original_Length) OR ([Length] IS NULL AND @original_Length IS NULL)) AND (([hr_money] = @original_hr_money) OR ([hr_money] IS NULL AND @original_hr_money IS NULL))" 
        InsertCommand="INSERT INTO [out_teacher] ([user_name], [CourseID], [CourseName], [Length], [hr_money]) VALUES (@user_name, @CourseID, @CourseName, @Length, @hr_money)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT [user_name], [CourseID], [CourseName], [Length], [hr_money] FROM [out_teacher]" 
        UpdateCommand="UPDATE [out_teacher] SET [CourseID] = @CourseID, [Length] = @Length, [hr_money] = @hr_money WHERE [user_name] = @original_user_name AND [CourseName] = @original_CourseName AND (([CourseID] = @original_CourseID) OR ([CourseID] IS NULL AND @original_CourseID IS NULL)) AND (([Length] = @original_Length) OR ([Length] IS NULL AND @original_Length IS NULL)) AND (([hr_money] = @original_hr_money) OR ([hr_money] IS NULL AND @original_hr_money IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_user_name" Type="String" />
            <asp:Parameter Name="original_CourseName" Type="String" />
            <asp:Parameter Name="original_CourseID" Type="String" />
            <asp:Parameter Name="original_Length" Type="String" />
            <asp:Parameter Name="original_hr_money" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="user_name" Type="String" />
            <asp:Parameter Name="CourseID" Type="String" />
            <asp:Parameter Name="CourseName" Type="String" />
            <asp:Parameter Name="Length" Type="String" />
            <asp:Parameter Name="hr_money" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="CourseID" Type="String" />
            <asp:Parameter Name="Length" Type="String" />
            <asp:Parameter Name="hr_money" Type="String" />
            <asp:Parameter Name="original_user_name" Type="String" />
            <asp:Parameter Name="original_CourseName" Type="String" />
            <asp:Parameter Name="original_CourseID" Type="String" />
            <asp:Parameter Name="original_Length" Type="String" />
            <asp:Parameter Name="original_hr_money" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
<br />
</asp:Content>

