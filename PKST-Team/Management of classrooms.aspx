<%@ Page Title="" Language="C#" MasterPageFile="~/thismaster2.master" AutoEventWireup="true" CodeFile="Management of classrooms.aspx.cs" Inherits="Management_of_classrooms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="yuimage/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script src="yuimage/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="yuimage/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 800px;
        }
    </style>
    <script>
        $(function () {
            $("#ctl00_ContentPlaceHolder1_Button2").click(function () {
                if ($("#ctl00_ContentPlaceHolder1_DropDownList2").val() == "無") {
                    alert("請選擇教室");
                    return false;
                }
            })
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="divyu1" style="position:relative; left: 30%" >

    <table class="style1">
        <tr>
            <td>
                選擇班級:</td>
            <td>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:Button ID="Button1" runat="server" Text="帶入下半年班級" 
        onclick="Button1_Click" />
            </td>
        </tr>
        <tr>
            <td>
                起始日期:</td>
            <td>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                結束日期:</td>
            <td>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                上課週期:</td>
            <td>
    <asp:CheckBox ID="CheckBox1" runat="server" Text="星期一" />、<asp:CheckBox ID="CheckBox2" runat="server" Text="星期二" />
                、<asp:CheckBox ID="CheckBox3" runat="server" Text="星期三" />、<asp:CheckBox ID="CheckBox4" runat="server" Text="星期四" />
                、<br />
    <asp:CheckBox ID="CheckBox5" runat="server" Text="星期五"  />、<asp:CheckBox ID="CheckBox6" runat="server" Text="星期六" />
                、<asp:CheckBox ID="CheckBox7" runat="server" Text="星期日"  />、</td>
        </tr>
        <tr>
            <td>
                上課時間:</td>
            <td>
    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
        <asp:ListItem Value="白天">白天</asp:ListItem>
        <asp:ListItem>晚間</asp:ListItem>
    </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                使用教室</td>
            <td>
    <asp:DropDownList ID="DropDownList2" runat="server" 
        DataSourceID="SqlDataSource2" DataTextField="id_classroom" 
        DataValueField="id_classroom" 
                    onselectedindexchanged="DropDownList2_SelectedIndexChanged" 
                    AutoPostBack="True">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
        SelectCommand="SELECT [id_classroom] FROM [classroom]"></asp:SqlDataSource>

            </td>
        </tr>
        <tr><td>查詢教室使用(月)</td><td>
            <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" 
                onselectedindexchanged="DropDownList4_SelectedIndexChanged" 
                >
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True">
                <asp:ListItem Value="01">一月</asp:ListItem>
                <asp:ListItem Value="02">二月</asp:ListItem>
                <asp:ListItem Value="03">三月</asp:ListItem>
                <asp:ListItem Value="04">四月</asp:ListItem>
                <asp:ListItem Value="05">五月</asp:ListItem>
                <asp:ListItem Value="06">六月</asp:ListItem>
                <asp:ListItem Value="07">七月</asp:ListItem>
                <asp:ListItem Value="08">八月</asp:ListItem>
                <asp:ListItem Value="09">九月</asp:ListItem>
                <asp:ListItem Value="10">十月</asp:ListItem>
                <asp:ListItem Value="11">十一月</asp:ListItem>
                <asp:ListItem Value="12">十二月</asp:ListItem>
            </asp:DropDownList>
            </td></tr>
    </table>

    <asp:Button ID="Button2" runat="server" Text="確定加入" onclick="Button2_Click" />
      </div>
    <br /><div id="g1" style=" float:left">201教室
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="date,id_classroom" DataSourceID="SqlDataSource1" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" GridLines="None" Font-Size="Medium">
        <Columns>
            <asp:BoundField DataField="date" HeaderText="日期" ReadOnly="True" 
                SortExpression="date" DataFormatString="{0:d}" />
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" Font-Size="XX-Large" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />
    </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            
            SelectCommand="SELECT [date], [mon], [aft], [nig], [id_classroom] FROM [Classroom planning] WHERE (([month] = @month) AND ([id_classroom] = @id_classroom) AND ([years] = @years))">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:Parameter DefaultValue="201" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
  
        </asp:SqlDataSource>
    </div><div id="g2" style=" float:left">202教室
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource3" 
            GridLines="None" Font-Size="Medium">
        <Columns>
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" >
            <FooterStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />
    </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([years] = @years) AND ([month] = @month))">
            <SelectParameters>
                <asp:Parameter DefaultValue="202" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div><div id="g3" style=" float:left">203教室
    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource4" 
            GridLines="None" Font-Size="Medium">
        <Columns>
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />
    </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([years] = @years) AND ([month] = @month))">
            <SelectParameters>
                <asp:Parameter DefaultValue="203" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div><div id="g4" style=" float:left">204教室
    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource5" 
            GridLines="None" Font-Size="Medium">
        <Columns>
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />
    </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([years] = @years) AND ([month] = @month))">
            <SelectParameters>
                <asp:Parameter DefaultValue="204" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div><div id="g5" style=" float:left">205教室
    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource6" 
            GridLines="None" Font-Size="Medium">
        <Columns>
            <asp:BoundField DataField="date" HeaderText="日期" SortExpression="date" 
                DataFormatString="{0:d}" />
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />
    </asp:GridView>
    </div><div id="g6" style=" float:left">206教室
    <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource7" 
            GridLines="None" Font-Size="Medium">
        <Columns>
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />
    </asp:GridView>
    </div><div id="g7" style=" float:left">301教室
    <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource8" 
            Font-Size="Medium" GridLines="None">
        <Columns>
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />
    </asp:GridView>
    </div><div id="g8" style=" float:left">302教室
    <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource9" 
            Font-Size="Medium" GridLines="None">
        <Columns>
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />

    </asp:GridView>
    </div><div id="g9" style=" float:left">303教室
    <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource10" 
            Font-Size="Medium" GridLines="None">
        <Columns>
            <asp:BoundField DataField="date" HeaderText="日期" SortExpression="date" 
                DataFormatString="{0:d}" />
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />

    </asp:GridView>
    </div><div id="g10" style=" float:left">304教室
    <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource11" 
            Font-Size="Medium" GridLines="None">
        <Columns>
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />

    </asp:GridView>
    </div><div id="g11" style=" float:left">305教室
    <asp:GridView ID="GridView11" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource11" 
            Font-Size="Medium" GridLines="None">
        <Columns>
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />

    </asp:GridView>
    </div><div id="g12" style=" float:left">306教室
    <asp:GridView ID="GridView12" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource11" 
            Font-Size="Medium" GridLines="None">
        <Columns>
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />

    </asp:GridView>
    </div><div id="g13" style=" float:left">307教室
    <asp:GridView ID="GridView13" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource12" 
            Font-Size="Medium" GridLines="None">
        <Columns>
            <asp:BoundField DataField="date" DataFormatString="{0:d}" HeaderText="日期" 
                SortExpression="date" />
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />

    </asp:GridView>
    </div><div id="g14" style=" float:left">308教室
    <asp:GridView ID="GridView14" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource13" 
            Font-Size="Medium" GridLines="None">
        <Columns>
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="晚上" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />

    </asp:GridView>
    </div><div id="g15" style=" float:left">309教室
    <asp:GridView ID="GridView15" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource14" 
            Font-Size="Medium" GridLines="None">
        <Columns>
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />

    </asp:GridView>
    </div><div id="g16" style=" float:left">310教室
    <asp:GridView ID="GridView16" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource15" 
            Font-Size="Medium" GridLines="None">
        <Columns>
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />

    </asp:GridView>
    </div><div id="g17" style=" float:left">311教室
    <asp:GridView ID="GridView17" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataSourceID="SqlDataSource16" 
            Font-Size="Medium" GridLines="None">
        <Columns>
            <asp:BoundField DataField="date" DataFormatString="{0:d}" HeaderText="日期" 
                SortExpression="date" />
            <asp:BoundField DataField="mon" HeaderText="早上" SortExpression="mon" />
            <asp:BoundField DataField="aft" HeaderText="下午" SortExpression="aft" />
            <asp:BoundField DataField="nig" HeaderText="夜間" SortExpression="nig" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />

    </asp:GridView>
    </div>
        <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            
        SelectCommand="SELECT [date], [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="205" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([years] = @years) AND ([month] = @month))">
            <SelectParameters>
                <asp:Parameter DefaultValue="206" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource8" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="301" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource9" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="302" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
  <asp:SqlDataSource ID="SqlDataSource10" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            
        SelectCommand="SELECT [date], [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="303" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource11" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="304" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource12" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            
        SelectCommand="SELECT [date], [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="307" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource13" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="306" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource14" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="307" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource15" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="308" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource16" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            
        SelectCommand="SELECT [date], [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="311" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource17" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="310" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource18" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="311" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource19" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="302" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource20" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [mon], [aft], [nig] FROM [Classroom planning] WHERE (([id_classroom] = @id_classroom) AND ([month] = @month) AND ([years] = @years))">
            <SelectParameters>
                <asp:Parameter DefaultValue="302" Name="id_classroom" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="month" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList4" Name="years" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
</asp:Content>

