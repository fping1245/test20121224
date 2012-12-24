<%@ Page Title="" Language="C#" MasterPageFile="~/thismaster2.master" AutoEventWireup="true" CodeFile="IFRAM_TESS.aspx.cs" Inherits="IFRAM_TESS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
#contents
{
    position:relative;
    height:800px;
    width:1004px;

    }
</style>
    <script src="Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <iframe id="contents" scrolling="yes" frameborder="0" src="2.aspx">  
</iframe>

</asp:Content>

