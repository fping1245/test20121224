<%@ Page Title="" Language="C#" MasterPageFile="~/thismaster2.master" AutoEventWireup="true" CodeFile="QuerySystem.aspx.cs" Inherits="QuerySystem_2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Style-Type" content="text/css" />
    <meta http-equiv="Content-Script-Type" content="text/javascript" />
    <script src="Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
<%--    <link href="components/css/default.css" rel="stylesheet" type="text/css" />--%>



<SCRIPT language=JavaScript> 
<!--
    function openevent(url) {
        edmwin = window.open(url, "edmwindow", "resizable=no, scrollbars=no, menubar=no, toolbar=no, width=650,height=250,left=20,top=20");
    }
    function openedm(url) {
        edmwin = window.open(url, "edmwindow", "resizable=no, scrollbars=no, menubar=no, toolbar=no, width=650,height=410,left=20,top=20");
    }
    function openwinqa(url) {
        var qq;
        qq = form1.qcno.value;
        qawindow = window.open(url + "?qcno=" + qq.replace(/\+/gi, "%2B"), "qawindow", "resizable=yes, scrollbars=yes, menubar=no, toolbar=yes, width=650,height=550,left=40,top=40");
        //qawindow=window.open(url +"?qcno=" + form1.qcno.value,"qawindow", "resizable=yes, scrollbars=yes, menubar=no, toolbar=yes, width=450,height=500,left=40,top=40");
    }
    function openswin(url) {
        swindow = window.open(url, "swindow", "resizable=yes, scrollbars=yes, menubar=no, toolbar=yes, width=900,height=600,left=0,top=0");
    }
    function openswinlocal(url) {

//        alert('start');
        swindow = window.location.href(url);
//        swindow = $('#I1').attr('src', url);
//        alert('end');

    }

    function openswinil(url) {

//        alert('start');
        //swindow = window.location.href(url);
        swindow = $('#I1').attr('src', url);
//        alert('end');

    }

    /* var url = $(this).attr('href');
    $('#contents').attr('src', url); */



    function openswinlocalre(url) {
        swindow = window.location.replace(url);
    }
    function carryemail() {
        document.cookie = "vemail=" + f.vemail.value;
    }
    function carrycode() {
        document.cookie = "qcno=" + form1.qcno.value;
    }

    function MM_preloadImages() { //v3.0
        var d = document; if (d.images) {
            if (!d.MM_p) d.MM_p = new Array();
            var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
        }
    }

    function MM_swapImgRestore() { //v3.0
        var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
    }

    function MM_findObj(n, d) { //v4.01
        var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
            d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
        }
        if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
        for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
        if (!x && d.getElementById) x = d.getElementById(n); return x;
    }

    function MM_swapImage() { //v3.0
        var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
            if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
    }




//-->

<!--
function reSize()
{
	try{	
	var oBody	=	ifrm.document.body;
	var oFrame	=	document.all("ifrm");
		
	oFrame.style.height = oBody.scrollHeight + (oBody.offsetHeight - oBody.clientHeight);
	oFrame.style.width = oBody.scrollWidth + (oBody.offsetWidth - oBody.clientWidth);
	}
	//An error is raised if the IFrame domain != its container's domain
	catch(e)
	{
	window.status =	'Error: ' + e.number + '; ' + e.description;
	}
}
//-->

////有問題
//$(document).ready(function () {
////session要用的變數:
//var id=['<%=Session["id"]%>'];
//$('#dashboard').hover(
//function () {
//$(this).stop().animate(
//{
//left: '0',
//backgroundColor: 'rgb(255,255,255)'
//},
//500,
//'easeInSine'
//); // end animate
//},
//function () {
//$(this).stop().animate(
//{
//left: '-95px',
//backgroundColor: 'rgb(110,138,195)'
//},
//1500,
//'easeOutBounce'
//); // end animate
//}
//); // end hover


//}); // end ready
////有問題

//$(function(){
//$("#Button1").click(function(){
//$("#treeview").toggle();
//})
//})


</SCRIPT>


    <style type="text/css">
        #dashboard
        {
            width: 187px;
        }
        #I1
        {
            width: 946px;
            height: 958px;
        }
        #Button1
        {
            width: 75px;
        }
        
<%--         #treeview{  position: absolute;}  --%>
        
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" onload=reSize()>

<asp:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="Button2" PopupControlID="Panel1"></asp:HoverMenuExtender>

    <div>
        

    <%--<input id="Button1" type="button" value="功能選擇" /><br />--%>
        <asp:Button ID="Button2" runat="server" Text="功能選擇" />


    <div>

                <div id="treeview" style="position: absolute;">
                <asp:Panel ID="Panel1" runat="server" BackColor="#E8E8E8" BorderStyle="Outset" 
                        Width="162px">
                <asp:TreeView ID="tv_func" runat="server" EnableClientScript="False" ExpandDepth="1"
                        AutoGenerateDataBindings="False" 
                        LineImagesFolder="~/Images/TreeLineImages" CssClass="text11pt"
                        Width="12%">
                    <ParentNodeStyle CssClass="text11pt" Width="100%" />
                    <SelectedNodeStyle CssClass="text11pt" Width="100%" />
                    <RootNodeStyle CssClass="text11pt" Width="100%" />
                    <NodeStyle CssClass="text11pt" Width="100%" />
                    <LeafNodeStyle CssClass="text11pt" Width="100%" />
                    </asp:TreeView>    
                </div>
            </asp:Panel>
                <div>
                        

                    <iframe id="I1"  frameborder="0" name="I1" scrolling="auto" ></iframe>
                </div>

    </div>
    <br />

    <table style="width:100%;">
        
        <tr>

            <td >
                &nbsp;</td>
        </tr>


    </table>

</div>


</asp:Content>

