Public Class InspectorForm

    Dim _currControl As IShellControl

    Public Property SelectedControl() As IShellControl
        Get
            Return _currControl
        End Get
        Set(ByVal value As IShellControl)
            _currControl = value
            Me.PropertyGrid1.SelectedObject = _currControl.Config
            If value Is Nothing Then
                Me.Text = "Z-Shell Inspector"
            Else
                Me.Text = "Z-Shell Inspector: " & value.ToString
            End If
        End Set
    End Property

    Private Sub InspectorForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PropertyGrid1_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGrid1.PropertyValueChanged
        _currControl.Config = Me.PropertyGrid1.SelectedObject
        _currControl.Refresh()
    End Sub

End Class