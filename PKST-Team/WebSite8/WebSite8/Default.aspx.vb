
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim dt As New Data.DataTable
            dt.Columns.Add("col1")
            dt.Columns.Add("col2")
            For i As Integer = 0 To 20
                dt.Rows.Add(New String() {"A" & i, "B" & i})
            Next
            GridView1.DataSource = dt
            GridView1.DataBind()
        End If
    End Sub
End Class
