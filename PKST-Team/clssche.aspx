<%@ Page Title="" EnableEventValidation = "false" ResponseEncoding="utf-8" Language="C#" MasterPageFile="~/thismaster2.master" AutoEventWireup="true" CodeFile="clssche.aspx.cs" Inherits="clssche" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 30%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
        <tr>
            <td>
                班級:<asp:DropDownList ID="DropDownList1" 
                    runat="server" AutoPostBack="True" 
                    DataSourceID="SqlDataSource1" DataTextField="id_class" 
                    DataValueField="id_class" 
                    onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                Excel輸出
    <asp:Button ID="Button1" runat="server" Text="課表產生" onclick="Button1_Click" Width="89px" />
            </td>
        </tr>
        <tr>
            <td>
                課程起始日</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                課程結束日</td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
        SelectCommand="SELECT [id_class], [classmentor] FROM [classdetail] WHERE ([classmentor] = @classmentor)">
        <SelectParameters>
            <asp:Parameter DefaultValue="王孝弘" Name="classmentor" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" 
        BorderColor="#FFCC66" BorderWidth="1px" 
        DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
        ForeColor="#663399" Height="947px" ondayrender="Calendar1_DayRender" 
        ShowGridLines="True" Width="956px">
        <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="50px" 
            Font-Size="X-Large" ForeColor="#990000" />
        <DayStyle ForeColor="#009933" Height="100px" BackColor="#FFCC66" 
            BorderColor="#CC6600" BorderWidth="3px" Font-Size="Large" />
        <NextPrevStyle Font-Size="20pt" ForeColor="#FFFFCC" />
        <OtherMonthDayStyle ForeColor="#CC9966" />
        <SelectedDayStyle BackColor="#FFCC66" Font-Bold="False" BorderWidth="3px" 
            Font-Size="Large" ForeColor="#009933" Height="100px" />
        <SelectorStyle BackColor="#FFCC66" />
        <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="12pt" 
            ForeColor="#FFFFCC" />
        <TodayDayStyle BackColor="#FFCC66" BorderWidth="3px" Font-Size="Large" 
            ForeColor="#009933" Width="100px" />
    </asp:Calendar>
    </asp:Content>

