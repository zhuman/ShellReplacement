Imports System.Reflection

Public Class ShellRendererEditor

    Dim _rends As New ObjectModel.Collection(Of Type)
    Dim _currRend As ShellRenderer

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For Each a As Assembly In My.Application.Info.LoadedAssemblies
            For Each t As Type In a.GetTypes
                If Not t.IsInterface AndAlso Not t.IsAbstract AndAlso GetType(ShellRenderer).IsAssignableFrom(t) Then
                    _rends.Add(t)
                    Me.lstRends.Items.Add(t.Name)
                End If
            Next
        Next
        If _rends.Count > 0 Then
            lstRends.SelectedIndex = 0
            '_currRend = _rends(0).GetConstructor(New Type() {}).Invoke(Nothing, New Object() {})
        End If
        propRend.SelectedObject = _currRend
    End Sub

    Private Sub propRend_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles propRend.PropertyValueChanged
        UpdatePrev()
    End Sub

    Private Sub UpdatePrev()
        If propRend.SelectedObject IsNot Nothing Then
            Dim b As New Bitmap(picPrev.Width, picPrev.Height)
            Dim gr As Graphics = Graphics.FromImage(b)
            gr.FillRectangle(New Drawing2D.HatchBrush(Drawing2D.HatchStyle.LargeCheckerBoard, Color.Gray, Color.White), New Rectangle(0, 0, b.Width, b.Height))
            CType(propRend.SelectedObject, ShellRenderer).Render(gr, New Rectangle(0, 0, b.Width, b.Height))
            gr.Dispose()
            picPrev.Image = b
        End If
    End Sub

    Private Sub lstRends_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRends.SelectedIndexChanged
        If lstRends.SelectedIndex >= 0 Then
            _currRend = _rends(lstRends.SelectedIndex).GetConstructor(New Type() {}).Invoke(New Object() {})
            propRend.SelectedObject = _currRend
        End If
    End Sub

    Private Sub picPrev_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles picPrev.Resize
        UpdatePrev()
        'Debug.Print("Resized.")
    End Sub

    Private Sub propRend_SelectedObjectsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles propRend.SelectedObjectsChanged
        UpdatePrev()
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

End Class