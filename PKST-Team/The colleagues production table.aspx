<%@ Page Title="" Language="C#" MasterPageFile="~/thismaster2.master" AutoEventWireup="true" CodeFile="The colleagues production table.aspx.cs" Inherits="The_colleagues_production_table" %>

<%@ Register Assembly="MyWebControls" Namespace="MyWebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .corm
        {
            word-wrap:break-all;
            }
            .d1{            text-align: center;
        }
    </style>
    <script src="yuimage/jquery-1.8.3.js" type="text/javascript"></script>
    <link href="yuimage/1204/jquery-ui-1.9.2.custom.css" rel="stylesheet" type="text/css" />
    <script src="yuimage/1204/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script>

   
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="ddd">
<table class="style1">
        <tr>
            <td width="15%">
                選擇要查詢的課別:</td>
            <td>
                <asp:DropDownList ID="DropDownList4" runat="server">
                    <asp:ListItem>單門課</asp:ListItem>
                    <asp:ListItem>精修班</asp:ListItem>
                    <asp:ListItem>認證班</asp:ListItem>
                    <asp:ListItem>養成班</asp:ListItem>
                    <asp:ListItem>企業包班</asp:ListItem>
                    <asp:ListItem>會訓</asp:ListItem>
                    <asp:ListItem>研討會</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                選擇查詢方式:</td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" 
                    DataSourceID="SqlDataSource2" DataTextField="startdateY" 
                    DataValueField="startdateY">
                </asp:DropDownList>
                年<asp:DropDownList ID="DropDownList2" runat="server" 
                    DataSourceID="SqlDataSource3" DataTextField="startdate季" 
                    DataValueField="startdate季">
                </asp:DropDownList>
                季<asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                    DataSourceID="SqlDataSource4" DataTextField="startdateM" 
                    DataValueField="startdateM">
                </asp:DropDownList>
                月<asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
                    SelectCommand="SELECT DISTINCT [startdate季] FROM [View_Income1] ORDER BY [startdate季]">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
                    SelectCommand="SELECT DISTINCT [startdateM] FROM [View_Income1] ORDER BY [startdateM]">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
                    SelectCommand="SELECT DISTINCT [startdateY] FROM [View_Income1] ORDER BY [startdateY]">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                要產生的報表類型:</td>
            <td>
                <asp:Button ID="Button2" runat="server" Text="年報表" Width="65px" 
                    onclick="Button2_Click" />

                <asp:Button ID="Button3" runat="server" Text="季報表" Width="65px" 
                    onclick="Button3_Click" />

                <asp:Button ID="Button4" runat="server" Text="月報表" Width="65px" 
                    onclick="Button4_Click" />
        
            </td>
        </tr>
        <tr>
            <td>
                </td>
            <td>
                              <asp:Button ID="Button5" runat="server" onclick="Button5_Click1"  Text="查詢尚未設定之班級" />
            </td>
        </tr>
    </table>
    </div>
        <asp:Panel ID="Panel5" runat="server" Visible="False">
        選擇要輸出的格式:<cc1:WordExcelButton ID="WordExcelButton1" runat="server" 
                GridView="Panel1" />
        </asp:Panel>
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Visible="False">
        <asp:Label ID="Label15" runat="server"></asp:Label>
        <asp:Label ID="Label16" runat="server" Text="年"></asp:Label>
        <asp:Label ID="Label17" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label18" runat="server" Text="收入統計表"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataKeyNames="id_class" 
            DataSourceID="SqlDataSource1" GridLines="None" style="text-align: center">
            <Columns>
                <asp:TemplateField HeaderText="班級代號" SortExpression="id_classNo">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id_classNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("id_classNo") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="序號" SortExpression="id_class">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("id_class") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("id_class") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="班級/課程名稱" SortExpression="classname">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("classname") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("classname") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="300px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="時數" SortExpression="lenght">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("lenght") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("lenght") %>'></asp:Label>
                        (Hr)
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="費用" SortExpression="tuition">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("tuition") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("tuition") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="優惠價" SortExpression="tuition_discount">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" 
                            Text='<%# Bind("tuition_discount") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("tuition_discount") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="開課日期" SortExpression="startdate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("startdate") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("startdate") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="結束日期" SortExpression="enddate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("enddate") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("enddate") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="開課月份" SortExpression="startdateM">
                    <EditItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("startdateM") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <div class="d1">
                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("startdateM") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="組別" SortExpression="groupdeptID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("groupdeptID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("groupdeptID") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="承辦人" SortExpression="maintainer">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("maintainer") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("maintainer") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="報名人" SortExpression="be_student">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("be_student") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" 
                            Text='<%# Bind("be_student", "{0:yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="實際人" SortExpression="ed_student">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ed_student") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("ed_student") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="收入" SortExpression="income">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("income") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("income") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [id_classNo], [id_class], [classname], [lenght], [tuition], [tuition_discount], [startdate], [enddate], [startdateM], [groupdeptID], [maintainer], [be_student], [ed_student], [income] FROM [View_Income1] WHERE (([class_type] = @class_type) AND ([startdateY] = @startdateY))">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList4" Name="class_type" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList1" Name="startdateY" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        </asp:Panel>
        </div>
        <div id="dd2">
            <asp:Panel ID="Panel6" runat="server" Visible="False">
                  選擇要輸出的格式:<cc1:WordExcelButton ID="WordExcelButton2" runat="server" 
                      GridView="Panel2" />
            </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Visible="False">
        <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label20" runat="server" Text="年第"></asp:Label>
        <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label22" runat="server" Text="季"></asp:Label>
        <asp:Label ID="Label23" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label24" runat="server" Text="收入統計表"></asp:Label>
        季報表<br /> 
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataKeyNames="id_class" 
            DataSourceID="SqlDataSource5" GridLines="None" style="text-align: center">
            <Columns>
                <asp:TemplateField HeaderText="班級代號" SortExpression="id_classNo">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id_classNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("id_classNo") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="序號" SortExpression="id_class">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("id_class") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("id_class") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="班級/課程名稱" SortExpression="classname">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("classname") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("classname") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="300px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="時數" SortExpression="lenght">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("lenght") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("lenght") %>'></asp:Label>
                        (Hr)
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="費用" SortExpression="tuition">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("tuition") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("tuition") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="優惠價" SortExpression="tuition_discount">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" 
                            Text='<%# Bind("tuition_discount") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("tuition_discount") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="開始日期" SortExpression="startdate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("startdate") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("startdate") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="結束日期" SortExpression="enddate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("enddate") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("enddate") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="開課月份" SortExpression="startdateM">
                    <EditItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("startdateM") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("startdateM") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="組別" SortExpression="groupdeptID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("groupdeptID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("groupdeptID") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="承辦人" SortExpression="maintainer">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("maintainer") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("maintainer") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="報名人數" SortExpression="be_student">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("be_student") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("be_student") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="實際人數" SortExpression="ed_student">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ed_student") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("ed_student") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:BoundField DataField="income" HeaderText="收入" SortExpression="income">
                <ControlStyle Width="80px" />
                <ItemStyle Wrap="False" />
                </asp:BoundField>
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
            SelectCommand="SELECT [id_classNo], [id_class], [classname], [lenght], [tuition], [tuition_discount], [startdate], [enddate], [startdateM], [groupdeptID], [maintainer], [be_student], [ed_student], [income] FROM [View_Income1] WHERE (([class_type] = @class_type) AND ([startdateY] = @startdateY) AND ([startdate季] = @startdate季))">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList4" Name="class_type" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList2" Name="startdateY" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="startdate季" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </asp:Panel>
   </div>
    <div id="dd3">
    <asp:Panel ID="Panel7" runat="server" Visible="False">
                  選擇要輸出的格式:<cc1:WordExcelButton ID="WordExcelButton3" runat="server" 
                      GridView="Panel3" />
            </asp:Panel>
    <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" Visible="False">
     <asp:Label ID="Label25" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label26" runat="server" Text="年第"></asp:Label>
        <asp:Label ID="Label27" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label28" runat="server" Text="月"></asp:Label>
        <asp:Label ID="Label29" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label30" runat="server" Text="收入統計表"></asp:Label>
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
            CellPadding="3" CellSpacing="1" DataKeyNames="id_class" 
            DataSourceID="SqlDataSource6" GridLines="None" style="text-align: center">
            <Columns>
                <asp:TemplateField HeaderText="班級代號" SortExpression="id_classNo">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id_classNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("id_classNo") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="序號" SortExpression="id_class">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("id_class") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("id_class") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="班級/課程名稱" SortExpression="classname">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("classname") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("classname") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="300px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="時數" SortExpression="lenght">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("lenght") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("lenght") %>'></asp:Label>
                        (Hr)
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="費用" SortExpression="tuition">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("tuition") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("tuition") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="優惠價" SortExpression="tuition_discount">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" 
                            Text='<%# Bind("tuition_discount") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("tuition_discount") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="開課日期" SortExpression="startdate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("startdate") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("startdate") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="結束日期" SortExpression="enddate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("enddate") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("enddate") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="開課月份" SortExpression="startdateM">
                    <EditItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("startdateM") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("startdateM") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="組別" SortExpression="groupdeptID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("groupdeptID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("groupdeptID") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="承辦人" SortExpression="maintainer">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("maintainer") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("maintainer") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="報名人數" SortExpression="be_student">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("be_student") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("be_student") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="實際人數" SortExpression="ed_student">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ed_student") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("ed_student") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="收入" SortExpression="income">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("income") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("income") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
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

        <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [id_classNo], [id_class], [classname], [lenght], [tuition], [tuition_discount], [startdate], [enddate], [startdateM], [groupdeptID], [maintainer], [be_student], [ed_student], [income] FROM [View_Income1] WHERE (([class_type] = @class_type) AND ([startdateY] = @startdateY) AND ([startdateM] = @startdateM))">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList4" Name="class_type" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList1" Name="startdateY" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="DropDownList3" Name="startdateM" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </asp:Panel>
    </div>
    <div id="dd4">
     <asp:Panel ID="Panel8" runat="server" Visible="False">
                  選擇要輸出的格式:<cc1:WordExcelButton ID="WordExcelButton4" runat="server" 
                      GridView="Panel4" />
            </asp:Panel>
    <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto" Visible="False">
      <asp:Label ID="Label31" runat="server" Text="尚未確認收入之統計表"></asp:Label>
        <asp:GridView ID="GridView4" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
            BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" 
            CellSpacing="1" DataKeyNames="id_class" DataSourceID="SqlDataSource7" 
            GridLines="None" style="text-align: center">
            <Columns>
                <asp:TemplateField HeaderText="班級代號" SortExpression="id_classNo">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("id_classNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("id_classNo") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="100px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="序號" SortExpression="id_class">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("id_class") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("id_class") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="課程名稱" SortExpression="classname">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("classname") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("classname") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="300px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="時數" SortExpression="lenght">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("lenght") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("lenght") %>'></asp:Label>
                        (Hr)
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="費用" SortExpression="tuition">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("tuition") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("tuition") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="優惠價" SortExpression="tuition_discount">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" 
                            Text='<%# Bind("tuition_discount") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" 
                            Text='<%# Bind("tuition_discount", "{0:c}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="開始日期" SortExpression="startdate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("startdate") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("startdate") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="結束日期" SortExpression="enddate">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("enddate") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("enddate") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="開課月份" SortExpression="startdateM">
                    <EditItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("startdateM") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("startdateM") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="組別" SortExpression="groupdeptID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("groupdeptID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("groupdeptID") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="承辦人" SortExpression="maintainer">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("maintainer") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("maintainer") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="報名人數" SortExpression="be_student">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("be_student") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("be_student") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="實際人數" SortExpression="ed_student">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("ed_student") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("ed_student") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="收入" SortExpression="income">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("income") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("income") %>'></asp:Label>
                    </ItemTemplate>
                    <ControlStyle Width="80px" />
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
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
        <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
            ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>" 
            SelectCommand="SELECT [id_classNo], [id_class], [classname], [lenght], [tuition], [tuition_discount], [startdate], [enddate], [startdateM], [groupdeptID], [maintainer], [be_student], [ed_student], [income] FROM [View_Income1] WHERE ([class_type] IS NULL)">
        </asp:SqlDataSource>
    </asp:Panel>
    </div>
</asp:Content>

