<%@ Page Title="" Language="C#" MasterPageFile="~/thismaster2.master" AutoEventWireup="true" CodeFile="Management_Default.aspx.cs" Inherits="Management_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="aaa.aspx">教室查詢</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="Management of classrooms.aspx">教室規劃</asp:HyperLink>
</asp:Content>

