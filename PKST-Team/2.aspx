<%@ Page Language="C#" AutoEventWireup="true" CodeFile="2.aspx.cs" Inherits="_2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="Scripts/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.progressbar.js" type="text/javascript"></script>
    <link href="Scripts/demos.css" rel="stylesheet" type="text/css" />
    <style>
        .ui-progressbar .ui-progressbar-value
        {
            background-image: url(img/pbar-ani.gif);
        }
    </style>
    <script type="text/javascript">
        var clsno;
        $(function () {

            //progressbar初始化
            $("#progressbar").progressbar({ max: 100 });
            $("#progressbar").progressbar("destroy");
            //存檔dialog初始化
            $("#dialog1").dialog({ autoOpen: false });
            $("#dialog2").dialog({ autoOpen: false });
            //存檔dialog初<p>內容隱藏
            $("#dialog1").dialog("destroy");
            $("#dialog2").dialog("destroy");
            //產生課程清單
            clslist()

            $("#conno").blur(function () {
                clsno = $("#conno").val();
            })


            var datememory;
            $("#saveclsroomdutylength").click(function () {
                var clsid = $("#DropDownList1").val();
                var dateid = datememory;
                var clsroom = $("#TextBox1").val();
                var duty_length = $("#TextBox2").val();
                $.get('saveclsroomdutylength.ashx', { id_class: clsid, dateid: dateid, clsroom: clsroom, duty_length: duty_length }, function (data) {
                    $("#dialog2").dialog({ autoOpen: true, show: "blind", hide: "clip", modal: true, buttons: { Ok: function () { $(this).dialog("close"); } } });
                });
            })



            //置入課程代碼到日曆小格子中
            $("td > :text").dblclick(function () {
                $(this).val(clsno);
                var timewithduty = $(this).attr("id");
            })

            //產生教室及時數供修改查閱(尚缺where CourseName 就可直接update到timetable)
            $("td > :text").click(function () {
                var clsid = $("#DropDownList1").val();
                var dateid = $(this).attr("id");
 
                datememory = dateid;
                $.getJSON('clsinfoback.ashx', { id_class: clsid, dateid: dateid }, function (data) {
                    $("#TextBox1").val(data[0].clsroom);
                    $("#TextBox2").val(data[0].duty_length);
                });
            })

            var k;
            $("#saveschedule").click(function () {
                $("#dialog1").dialog({ autoOpen: true, show: "blind", hide: "explode", modal: true });
                document.body.style.cursor = 'wait';
                $("#progressbar").progressbar({ max: 100 });
                var i = 1;
                var j = 1+5;
                k = 1;
                $("td :text").each(function () {
                    i = i + 1;
                })

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
                        j = j + 1;
                        k = j / i * 100;
                        $("#progressbar").progressbar("option", "value", k);

                        if (k == 100) {
                            document.body.style.cursor = 'default';
                            //自動關閉儲存中視窗
                            $("#dialog1").dialog("destroy");
                            //跳出儲存成功確認視窗(scale,explode,clip,fade)
                            $("#dialog2").dialog({ autoOpen: true, show: "blind", hide: "clip", modal: true, buttons: { Ok: function () { $(this).dialog("close"); } } });

                            $("#progressbar").progressbar("destroy");
                        }
                    })
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
            width: 20px;
        }
        .roundselect
        {
            border-bottom-style: solid;
            background-color: Green;
        }
        #Button1
        {
            width: 48px;
        }
        #lablabel
        {
            width: 25px;
        }
        .style1
        {
            width: 40%;
        }
        #holidylabel
        {
            width: 25px;
        }
        #saveschedule
        {
            width: 44px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >


    <contenttemplate>
            <asp:DropDownList ID="DropDownList1" runat="server" 
                DataSourceID="SqlDataSource1" DataTextField="id_class" 
                DataValueField="id_class" AutoPostBack="True" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
            </asp:DropDownList>
        &nbsp;
        <br />
        </contenttemplate>
    <table class="style1">
        <tr>
            <td>
                課程起始日</td>
            <td>
                 <span><asp:TextBox ID="TextBox3" runat="server" Width="125px" ReadOnly="True"></asp:TextBox></span>
            </td>
            <td>
                時段教室</td>
            <td>
                <span><asp:TextBox ID="TextBox1" runat="server" Width="44px"></asp:TextBox></span>
                
            </td>
        </tr>
        <tr>
            <td>
                課程結束日</td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" Width="125px" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                時段時數</td>
            <td>
               <asp:TextBox ID="TextBox2" runat="server" Width="44px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                班級排課儲存</td>
            <td>
                 <input id="conno" type="text" /> <input id="lablabel" type="button" value="L" onclick="ff('L')" /> <input
        id="holidylabel" type="button" value="H" onclick="ff('H')" /> 
                 <input id="saveschedule" type="button"
                value="儲存" />
            </td>
            <td>
                時段資料儲存
            </td>
            <td>
                <input id="saveclsroomdutylength" type="button" value="儲存" />
            </td>

        </tr>
    </table>
    <br />
   
    
    </div>
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PKSTConnectionString %>"
            SelectCommand="SELECT [id_class] FROM [classdetail] WHERE ([ClassMentor] = @ClassMentor) and EndDate &gt; dateadd(month,-6,getdate())">
            <SelectParameters>
                <asp:Parameter DefaultValue="王孝弘" Name="ClassMentor" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <div style="float: left">
            <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" BackColor="White"
                BorderColor="White" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black"
                Height="190px" Width="432px" SelectionMode="None" 
                NextPrevFormat="FullMonth" BorderWidth="1px">
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                <NextPrevStyle Font-Size="8pt" ForeColor="#333333" Font-Bold="True" 
                    VerticalAlign="Bottom" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                <TitleStyle BackColor="White" Font-Bold="True" Font-Size="12pt" 
                    ForeColor="#333399" BorderColor="Black" BorderWidth="4px" />
                <TodayDayStyle BackColor="#CCCCCC" />
            </asp:Calendar>
        </div>
        <div id="courselist" style="float: left">
        </div>
        <br />
        <div style="clear: both">
        </div>
<%--        classroom:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        dutytime:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />--%>




            <div id="dialog1" title="Basic dialog"> 
            <p>儲存中請稍後...</p> 
            <div id="progressbar" style=" float: inherit; width:270px"> 
            </div>
             
            <div id="dialog2" title="Basic dialog2"> 
            <p>儲存成功!</p> 
            </div>
    
    
    
    </div>
    </form>
</body>
</html>
