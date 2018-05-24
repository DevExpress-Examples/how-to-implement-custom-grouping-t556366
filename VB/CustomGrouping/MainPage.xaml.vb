Imports CustomGrouping.Data

Public NotInheritable Class MainPage
    Inherits Page
    Public Sub New()
        InitializeComponent()
        grid.ItemsSource = New ProductList()
        grid.Columns("UnitPrice").GroupIndex = 0
    End Sub
    Private Sub GridControl_CustomColumnGroup(sender As Object, e As DevExpress.UI.Xaml.Grid.CustomColumnSortEventArgs)
        If e.Column.FieldName = "UnitPrice" Then
            Dim x As Double = Math.Floor(Convert.ToDouble(e.Value1) / 10)
            Dim y As Double = Math.Floor(Convert.ToDouble(e.Value2) / 10)
            Dim res As Integer = Comparer(Of Double).[Default].Compare(x, y)
            If x > 9 AndAlso y > 9 Then
                res = 0
            End If
            e.Result = res
            e.Handled = True
        End If
    End Sub

    Private Sub GridControl_CustomGroupDisplayText(sender As Object, e As DevExpress.UI.Xaml.Grid.CustomGroupDisplayTextEventArgs)
        If e.Column.FieldName <> "UnitPrice" Then
            Return
        End If
        Dim interval As String = IntervalByValue(e.Value)
        e.DisplayText = interval

    End Sub
    Private Function IntervalByValue(val As Object) As String
        Dim d As Double = Math.Floor(Convert.ToDouble(val) / 10)
        Dim ret As String = String.Format("{0:c} - {1:c} ", d * 10, (d + 1) * 10)
        If d > 9 Then
            ret = String.Format(">= {0:c} ", 100)
        End If
        Return ret
    End Function
End Class
