<%@ Page Title="" Language="C#" MasterPageFile="~/thismaster2.master" AutoEventWireup="true"
    CodeFile="classmanagementOK.aspx.cs" Inherits="classmanagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--    <script src="js/js1203/jquery-1.8.2.js" type="text/javascript"></script>--%>
    <script src="js/jquery-1.8.3.js" type="text/javascript"></script>
    <link href="yuimage/1204/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script src="js/js1203/jquery-ui-1.9.1.custom.min.js" type="text/javascript"></script>
    <!--預設mane開始-->
    <!--預設mane結束-->
    <script>
        $(function () {
            $("#ctl00_ContentPlaceHolder1_TextBox1").datepicker({ dateFormat: 'yy/m/d' });
            $("#ctl00_ContentPlaceHolder1_TextBox2").datepicker({ dateFormat: 'yy/m/d' });
            //            $.getJSON('yu/TMSClass_Course.ashx', {}, function (data) {
            //                for (var i = 0; i < data.length; i++) {
            //                    $('#select11051').append("<option value='" + data[i].CourseName + "'>" + data[i].CourseName + "</option>");
            //                }
            //            });
            $("#select11051").change(function () {
                $("#select11052").empty();
                var cousname = $(this).val();
                var cousname2 = $(this).text();

                $.getJSON('yu/TMSTeachername.ashx', { cosname: cousname }, function (data) {
                    for (var i = 0; i < data.length; i++) {
                        $('#select11052').append("<option value='" + data[i].TeacherName + "'>" + data[i].TeacherName + "</option>");
                    }
                });
            });
            $("#select11052").change(function () {
                var cousname1 = $('#select11051').val();
                var leg = $(this).val();
                //alert(cousname);
                //alert(leg);

                $.getJSON('yu/TMSleg.ashx', { cousname: cousname1, TeacherName: leg }, function (data) {
                    $("#ctl00_ContentPlaceHolder1_TextBox3").val(data[0].Length);
                    $("#ctl00_ContentPlaceHolder1_TextBox4").val(data[0].iduser);

                });
            });
            $("#ctl00_ContentPlaceHolder1_Button4").click(function () {
                $("#divshow2").dialog("open");
                $.getJSON("yu/TMSClass_Course.ashx", {}, function (data) {
                    for (var i = 0; i < data.length; i++) {
                        $('#select11051').append("<option value='" + data[i].CourseName + "'>" + data[i].CourseName + "</option>");
                    }
                });
                return false;
            });
            $("#divshow2").dialog({
                autoOpen: false,
                show: "blind",
                hide: "explode",
                resizable: false,
                height: 180,
                width: 700,
                modal: true,
                buttons: {
                    關閉: function () {
                        $(this).dialog("close");
                        window.location.href = window.location.href;
                    }
                }
            });
            //            $.getJSON("yu/selectclassroom.ashx", {}, function (data) {
            //                for (var i = 0; i < data.length; i++) {
            //                    $('#ContentPlaceHolder1_DropDownList2').append("<option value='" + data[i].classroom + "'>" + data[i].classroom + "</option>");
            //                }
            //            });
            //            $("#ContentPlaceHolder1_GridView1_DropDownList3_0").change(function () {
            //                var aaa = $("#ContentPlaceHolder1_GridView1_DropDownList3_0").val();
            //                //                $("#ContentPlaceHolder1_DropDownList3")
            //                alert(aaa);
            //            }); 
            //            var l1 = false;
            //            $("#ContentPlaceHolder1_Button2").click(function () {
            //                var id_class = $("#ContentPlaceHolder1_DropDownList1").val();
            //                var id_classroom = $("#ContentPlaceHolder1_DropDownList2").val();
            //                //var lab1 = $("#ContentPlaceHolder1_CheckBox1").check();
            //               

            //                alert(l1);
            //                //                $.getJSON("yu/Updateclassdetail.ashx", { id_classroom: id_classroom, startdate: startdate, enddate: enddate, lab1: lab1, lab2: lab2, lab3: lab3, lab4: lab4, lab5: lab5, id_class: id_class }, function (data) {
            //                //                    for (var i = 0; i < data.length; i++) {
            //                //                        $('#select11051').append("<option value='" + data[i].CourseName + "'>" + data[i].CourseName + "</option>");
            //                //                    }
            //                //                });
            //                return false;
            //            });
            //            //ContentPlaceHolder1_GridView1_Label1_1
            $("#ctl00_ContentPlaceHolder1_Button2").click(function () {
                alert("修改成功!");
            });
            $("#ctl00_ContentPlaceHolder1_ButtonUpdate").click(function () {
                alert("修改成功!");
            });
            
        });
    </script>
    <script>
        $(function () {
            $("#ctl00_ContentPlaceHolder1_Button1")
            .click(function () {
                var a1 = $("#select11051").val(); //課程名稱  
                var a2 = $("#select11052").val(); //老師姓名    
                var a3 = $("#ctl00_ContentPlaceHolder1_DropDownList1").val(); //班級 
                var a4 = $("#ctl00_ContentPlaceHolder1_TextBox3").val(); //時間  
                var a5 = $("#ctl00_ContentPlaceHolder1_DropDownList2").val(); //使用的教室預設帶入
                var a6 = $("#ctl00_ContentPlaceHolder1_TextBox4").val(); //員工編號
                $.get("yu/InserC2.ashx", { id_class1: a3, CourseName2: a1, TeacherName3: a2, Length4: a4, clsroom: a5, id_user: a6 }, function (data) {
                    var i = data[0].Length;
                    //parent.$("#ContentPlaceHolder1_GridView1").load(window.parent.location.href + " #ContentPlaceHolder1_GridView1");
                    //parent.$("#ContentPlaceHolder1_DropDownList1").load(window.parent.location.href + " #ContentPlaceHolder1_DropDownList1");

                });
                alert("新增成功");
            });
        });
    
    </script>
    <style type="text/css">
        .style1
        {
            width: 380px;
            height: 150px;
        }
        .style2
        {
            height: 17px;
        }
        .style3
        {
            height: 18px;
        }
        .style4
        {
            width: 100%;
        }
        .cc
        {
            margin: 0 0 0 300px;
        }
        #divshow1
        {
            width: 390px;
            float: left;
        }
        #divshowgr
        {
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="dddd">
        <div id="divshow1">
            請選擇班級:&nbsp;&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="TOMS匯入" />
            <table class="style1">
                <tr>
                    <td class="style2" width="80px">
                        開始日期:
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        結束日期:
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        使用教室:
                    </td>
                    <td class="style3">
                        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource1"
                            DataTextField="id_classroom" DataValueField="id_classroom">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>"
                            SelectCommand="SELECT [id_classroom] FROM [classroom]"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        LAB時間:
                    </td>
                    <td>
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="星期一" />
                        、<asp:CheckBox ID="CheckBox2" runat="server" Text="星期二" />
                        <br />
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="星期三" />
                        、<asp:CheckBox ID="CheckBox4" runat="server" Text="星期四" />
                        <br />
                        <asp:CheckBox ID="CheckBox5" runat="server" Text="星期五" />
                        <asp:Button ID="Button2" runat="server" Style="text-align: left" Text="存檔" OnClick="Button2_Click"
                            Visible="False" />
                    </td>
                </tr>
            </table>
            <asp:SqlDataSource ID="yuPKST" runat="server" ConnectionString="<%$ ConnectionStrings:YUPKSTString %>"
                SelectCommand="SELECT [id_class], [id_classroom], [startdate], [enddate], [lab1], [lab2], [lab4], [lab3], [lab5] FROM [classdetail]">
            </asp:SqlDataSource>
            <asp:Label ID="Label3" runat="server"></asp:Label>
            <br />
        </div>
        <asp:SqlDataSource ID="yusps" runat="server" ConnectionString="<%$ ConnectionStrings:YUPKSTString %>"
            SelectCommand="SELECT [CourseName], [TeacherName], [Length], [clsroom] FROM [special_model] WHERE ([id_class] = @id_class)"
            OnSelecting="yusps_Selecting">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="id_class" PropertyName="SelectedValue"
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <div id="divshowgr">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataSourceID="yusps" GridLines="None" Style="text-align: center" 
                ForeColor="#333333">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="課程名稱" SortExpression="CourseName">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("CourseName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("CourseName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="老師姓名" SortExpression="TeacherName">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("TeacherName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:SqlDataSource ID="teachername1" runat="server" ConnectionString="<%$ ConnectionStrings:TMSConnectionString %>"
                                SelectCommand="SELECT [TeacherName] FROM [Class_Course]"></asp:SqlDataSource>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("TeacherName") %>' Visible="False"></asp:Label>
                            <asp:DropDownList ID="DropDownList3" runat="server">
                                <asp:ListItem>請選擇</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="時數" SortExpression="Length">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Length") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Height="22px" Text='<%# Bind("Length") %>'
                                Width="51px"></asp:TextBox>
                            (h)
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="使用教室" SortExpression="clsroom">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("clsroom") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="SqlDataSource1"
                                DataTextField="id_classroom" DataValueField="id_classroom">
                            </asp:DropDownList>
                            <asp:Label ID="Label5" runat="server" Text="<%# Bind('clsroom') %>" Visible="False"></asp:Label>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>"
                                SelectCommand="SELECT [id_classroom] FROM [classroom]"></asp:SqlDataSource>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox6" runat="server" Text="刪除" />
                        </ItemTemplate>
                    </asp:TemplateField>
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
            <asp:Button ID="ButtonUpdate" runat="server" OnClick="ButtonUpdate_Click" Text="修改"
                Style="height: 21px" />
            <asp:Button ID="Button4" runat="server" Text="新增課程" OnClick="Button4_Click" Visible="False" />
            <div id="divshow2" title="新增課程">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table class="style4">
                            <tr>
                                <td class="style3">
                                    請選擇要新增的課程:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <select id="select11051" name="D1">
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                請選擇老師:<select id="select11052" name="D2">
                    <option>請選擇課程</option>
                </select>老師編號:<asp:TextBox ID="TextBox4" runat="server" ReadOnly="True" Width="60px"></asp:TextBox>
                時數<asp:TextBox ID="TextBox3" runat="server" Width="50px"></asp:TextBox>
                (h)<asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="確定新增" />
            </div>
        </div>
    </div>
</asp:Content>
