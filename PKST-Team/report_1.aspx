<%@ Page Title="" Language="C#" MasterPageFile="~/thismaster2.master" AutoEventWireup="true" CodeFile="report_1.aspx.cs" Inherits="report_1" %>

<%@ Register assembly="MyWebControls" namespace="MyWebControls" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" 
        DataTextField="enddateY" DataValueField="enddateY">
</asp:DropDownList>
    年<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:PKSTConnectionString_report_1 %>" 
        SelectCommand="SELECT DISTINCT [enddateY] FROM [View_3]">
    </asp:SqlDataSource><asp:DropDownList ID="DropDownList2" runat="server" 
        DataSourceID="SqlDataSource_report_2" DataTextField="enddateM" 
        DataValueField="enddateM">
    </asp:DropDownList>
    
    月<asp:SqlDataSource ID="SqlDataSource_report_2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:PKSTConnectionString_report_1 %>" 
        
        SelectCommand="SELECT DISTINCT [enddateM] FROM [View_3] WHERE ([enddateY] = @enddateY)">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="enddateY" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <cc1:WordExcelButton ID="WordExcelButton1" runat="server" 
    GridView="GridView1" />
    
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource_report_3" CellPadding="4" 
    ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="enddateY" HeaderText="年" ReadOnly="True" 
                SortExpression="enddateY" />
            <asp:BoundField DataField="enddateM" HeaderText="月" ReadOnly="True" 
                SortExpression="enddateM" />
            <asp:BoundField DataField="TeacherName" HeaderText="教師姓名" 
                SortExpression="TeacherName" />
            <asp:BoundField DataField="CourseName" HeaderText="課程" 
                SortExpression="CourseName" />
            <asp:BoundField DataField="date" HeaderText="時段" SortExpression="date" />
            <asp:BoundField DataField="tms_timesType" HeaderText="時段類別" 
                SortExpression="tms_timesType" />
            <asp:TemplateField HeaderText="收入"></asp:TemplateField>
            <asp:BoundField DataField="length" HeaderText="時數" 
                SortExpression="length" />
            <asp:BoundField DataField="總時數" HeaderText="總時數" SortExpression="總時數" />
            <asp:BoundField DataField="總時數" HeaderText="總時數" 
                SortExpression="總時數" />
            <asp:TemplateField HeaderText="金額"></asp:TemplateField>
            <asp:TemplateField HeaderText="合計"></asp:TemplateField>
            <asp:BoundField DataField="classmentor" HeaderText="導師" 
                SortExpression="classmentor" />
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
    <asp:SqlDataSource ID="SqlDataSource_report_3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:PKSTConnectionString_report_1 %>" 
        SelectCommand="SELECT [enddateY], [enddateM], [TeacherName], [CourseName], [date], [tms_timesType], [length], [總時數], [classmentor] FROM [View_3]">
    </asp:SqlDataSource>
    <br />
    
</asp:Content>


