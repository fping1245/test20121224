<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Timetabling.aspx.cs" Inherits="_2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var clsno;
        $(function () {
            //產生課程清單
            clslist()

            $("#conno").blur(function () {
                clsno = $("#conno").val();
            })

            //置入課程代碼到日曆小格子中
            $("td > input").dblclick(function () {
                $(this).val(clsno);
                var timewithduty = $(this).attr("id");
                //alert(timewithduty);
            })

            //產生教室及時數供修改查閱(尚缺where CourseName 就可直接update到timetable)
            $("td > input").click(function () {
                var clsid = $("#DropDownList1").val();
                $.getJSON('clsinfoback.ashx', { id_class: clsid }, function (data) {
                    $("#TextBox1").val(data[0].clsroom);
                });
            })

            $("#saveschedule").click(function () {
                var i = 1;
                $("td :text").each(function () {
                    var classid = $("#DropDownList1").val();
                    var dateid = $(this).attr("id");
                    //讀出sn
                    var sn = $(this).val();
                    if (sn == 'H') {
                        sn = 'H';
                    }
                    else if (sn == 'L') {
                        sn = 'L';
                    }
                    else {
                        sn = clsarr[sn];
                    }
                    
                    $.get('saveschedule.ashx', { id_class: classid, dateid: dateid, sn: sn }, function (data) {
                    })

                    i = i + 1;
                    
                })
            })
        })



        function ff(listno) {
            clsno = listno;
            $("#conno").val(clsno);
        }

        //儲存課程清單課程對到的SN(存課表用)
        var clsarr = new Array();
        //動態生成選課單
        function clslist() {
            var hoho = $("#DropDownList1").val();
            $.getJSON('getclslist.ashx', { id_class: hoho }, function (data) {
                $("#courselist").empty();
                var liststr = "<table><tbody>";
                for (var i = 1; i <= data.length; i++) {
                    liststr = liststr + "<tr><td><input id='selectitem' type='button' value='" + data[i - 1].listno + "' onclick='ff(" + data[i - 1].listno + ")'></input></td>"
                                                          + "<td id=" + "'coursename" + i.toString() + "' >" + data[i - 1].coursename + "</td>"
                                                          + "<td id=" + "'teachername" + i.toString() + "' >" + data[i - 1].teachername + "</td>"
                                                          + "<td id=" + "'courselength" + i.toString() + "'>" + data[i - 1].courselength + "</td>"
                                                          + "<td id=" + "'clsroom" + i.toString() + "'>" + data[i - 1].clsroom + "</td></tr>";
                    //儲存課程清單課程對到的SN
                    clsarr[i] = data[i - 1].sn
                    //alert(clsarr[i]);
                }
                liststr = liststr + "</tbody></table>";
                $("#courselist").append(liststr);
            })
        }

        
      

    </script>
    <style type="text/css">
        #Text1
        {
            width: 46px;
        }
        #conno
        {
            width: 22px;
        }
        .roundselect
        {
            border-bottom-style: solid;
            background-color: Green;
        }
        #Button1
        {
            width: 61px;
        }
        #lablabel
        {
            width: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <contenttemplate>
            <asp:DropDownList ID="DropDownList1" runat="server" 
                DataSourceID="SqlDataSource1" DataTextField="id_class" 
                DataValueField="id_class" AutoPostBack="True" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
            </asp:DropDownList>
        &nbsp;
        </contenttemplate>
    <br />
    <input id="conno" type="text" /><input id="lablabel" type="button" value="L" onclick="ff('L')" /><input
        id="holidylabel" type="button" value="H" onclick="ff('H')" /><input id="Button1"
            type="button" value="Clear" onclick="ff()" /><input id="saveschedule" type="button" value="Save" />
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>"
            
            SelectCommand="SELECT [id_class] FROM [classdetail] WHERE ([ClassMentor] = @ClassMentor) and EndDate &gt; dateadd(month,-6,getdate())">
            <SelectParameters>
                <asp:Parameter DefaultValue="王孝弘" Name="ClassMentor" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <div style="float: left">
            <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" BackColor="White"
                BorderColor="Black" Font-Names="Times New Roman" Font-Size="10pt" 
                ForeColor="Black" Height="220px" Width="400px" SelectionMode="None" 
                DayNameFormat="Shortest" NextPrevFormat="FullMonth" TitleFormat="Month">
                <DayHeaderStyle Font-Bold="True" Font-Size="7pt" BackColor="#CCCCCC" 
                    ForeColor="#333333" Height="10pt" />
                <DayStyle Width="14%" />
                <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" 
                    Font-Size="8pt" ForeColor="#333333" Width="1%" />
                <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" 
                    ForeColor="White" Height="14pt" />
                <TodayDayStyle BackColor="#CCCC99" />
            </asp:Calendar>
        </div>
        <div id="courselist" style="float: left">
        </div>
        <br />
        <div style="clear: both">
        </div>
        classroom:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        dutytime:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
    </div>

    </form>
</body>
</html>
