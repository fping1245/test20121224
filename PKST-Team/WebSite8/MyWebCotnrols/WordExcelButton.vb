Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

<DefaultProperty("Text"), ToolboxData("<{0}:WordExcelButton runat=server></{0}:WordExcelButton>")> _
Public Class WordExcelButton
    Inherits WebControl
    Implements INamingContainer

    <Bindable(True), Category("Appearance"), DefaultValue(""), Localizable(True)> Property Text() As String
        Get
            Dim s As String = CStr(ViewState("Text"))
            If s Is Nothing Then
                Return String.Empty
            Else
                Return s
            End If
        End Get

        Set(ByVal Value As String)
            ViewState("Text") = Value
        End Set
    End Property

    Dim WithEvents _btnExcel As ImageButton
    Dim WithEvents _btnWord As ImageButton

    Protected Overrides Sub CreateChildControls()
        Controls.Clear()
        MyBase.CreateChildControls()

        _btnWord = New ImageButton
        _btnWord.ImageUrl = Page.ClientScript.GetWebResourceUrl(GetType(ResourceSetting), "MyWebControls.Word.gif")

        _btnExcel = New ImageButton
        _btnExcel.ImageUrl = Page.ClientScript.GetWebResourceUrl(GetType(ResourceSetting), "MyWebControls.Excel.gif")

        Me.Controls.Add(_btnWord)
        Me.Controls.Add(_btnExcel)
    End Sub

    Protected Sub OnBtnExcelClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) _
                                                                                    Handles _btnExcel.Click
        Me.SetRenderMethodDelegate(New RenderMethod(AddressOf NewRenderMethod_Excel))
    End Sub

    Protected Sub OnBtnWordClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) _
                                                                                    Handles _btnWord.Click
        Me.SetRenderMethodDelegate(New RenderMethod(AddressOf NewRenderMethod_Word))
    End Sub

    '產生EXCEL
    Sub NewRenderMethod_Excel(ByVal writer As HtmlTextWriter, ByVal ctl As Control)
        Context.Response.Clear()
        Context.Response.AddHeader("content-disposition", "attachment;filename=" + Me.Text + ".xls")
        Context.Response.Charset = "UTF8"
        Context.Response.ContentType = "application/vnd.xls"

        Dim sw As New System.IO.StringWriter()
        Dim htmlWrite As New HtmlTextWriter(sw)
        FindControlRecursive(Me.Page, GridView).RenderControl(htmlWrite)
        Context.Response.Write(sw.ToString)
        Context.Response.End()
    End Sub
    '產生WORD
    Sub NewRenderMethod_Word(ByVal writer As HtmlTextWriter, ByVal ctl As Control)
        Context.Response.Clear()



        Context.Response.AddHeader("content-disposition", "attachment;filename=" + Me.Text + ".doc")

        Context.Response.Charset = "UTF8"
        Context.Response.ContentType = "application/vnd.doc"

        Dim sw As New System.IO.StringWriter()
        Dim htmlWrite As New HtmlTextWriter(sw)
        FindControlRecursive(Me.Page, GridView).RenderControl(htmlWrite)
        Context.Response.Write(sw.ToString)
        Context.Response.End()
    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Me.EnsureChildControls()
        MyBase.Render(writer)
    End Sub

    <Category("Behavior"), Themeable(False), DefaultValue(""), IDReferenceProperty(), Description("GridView"), TypeConverter(GetType(ControlIDConverter))> _
        Public Property GridView() As String
        Get
            Dim obj2 As Object = Me.ViewState("GridView")
            If Not (obj2 Is Nothing) Then
                Return CStr(obj2)
            End If
            Return String.Empty
        End Get
        Set(ByVal value As String)
            Me.ViewState("GridView") = value
        End Set
    End Property

    ''' <summary>
    ''' Finds a Control recursively. Note finds the first match and exits
    ''' </summary>
    ''' <param name="ContainerCtl"></param>
    ''' <param name="IdToFind"></param>
    ''' <returns></returns>
    Public Shared Function FindControlRecursive(ByVal ContainerCtl As Control, ByVal IdToFind As String) As Control
        If ContainerCtl.ID = IdToFind Then
            Return ContainerCtl
        End If
        Dim Ctl As Control
        For Each Ctl In ContainerCtl.Controls
            Dim FoundCtl As Control = FindControlRecursive(Ctl, IdToFind)
            If Not (FoundCtl Is Nothing) Then
                Return FoundCtl
            End If
        Next Ctl
        Return Nothing
    End Function 'FindControlRecursive
End Class
