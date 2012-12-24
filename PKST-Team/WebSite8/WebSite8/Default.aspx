<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="MyWebControls" Namespace="MyWebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:WordExcelButton ID="WordExcelButton1" runat="server" GridView="GridView1" 
            Text="11111" />
        &nbsp;
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
