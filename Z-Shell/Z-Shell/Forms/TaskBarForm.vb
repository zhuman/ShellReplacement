Public Class TaskBarForm

    Dim activeWindow As WindowInfo
    Dim insForm() As InspectorForm = New InspectorForm() {New InspectorForm(), New InspectorForm()}

    Private Sub Taskbar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Edge = Me.Edge
        Me.Edge = Me.Edge
        insForm(0).SelectedControl = Me.TaskBarControl1
        insForm(1).SelectedControl = Me.NotifyIconBar1
        'insForm(0).Show()
        'insForm(1).Show()
    End Sub

    Overrides Property Edge() As ABEdge
        Get
            Return MyBase.Edge
        End Get
        Set(ByVal value As ABEdge)
            MyBase.Edge = value
            Dim r As Rectangle = Screen.FromControl(Me).Bounds
            If value = ABEdge.Top Or value = ABEdge.Bottom Then
                pnlStart.Dock = DockStyle.Left
                Me.NotifyIconBar1.Dock = DockStyle.Right
                InfoLabel1.Dock = DockStyle.Right
                Me.Height = 32
                r.Height -= Me.Height
                If value = ABEdge.Top Then
                    r.Y += Me.Height
                End If
            Else
                pnlStart.Dock = DockStyle.Top
                Me.NotifyIconBar1.Dock = DockStyle.Bottom
                InfoLabel1.Dock = DockStyle.Bottom
                Me.Width = 160
                r.Width -= Me.Width
                If value = ABEdge.Left Then
                    r.X += Me.Width
                End If
            End If
            StartMenu.FadeOut()
            MyBase.Edge = value
        End Set
    End Property

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        If Not StartMenu.Visible Then
            Dim myPoint As Point
            Select Case Me.Edge
                Case ABEdge.Top
                    myPoint = New Point(btnStart.Left, btnStart.Bottom)
                Case ABEdge.Bottom
                    myPoint = New Point(btnStart.Left, btnStart.Top) - New Point(0, StartMenu.Height)
                Case ABEdge.Left
                    myPoint = New Point(btnStart.Right, btnStart.Top)
                Case ABEdge.Right
                    myPoint = New Point(btnStart.Left, btnStart.Top) - New Point(StartMenu.Width, 0)
            End Select
            StartMenu.FadeIn()
            StartMenu.Location = Me.PointToScreen(myPoint)
        End If
    End Sub

    Private Sub TopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TopToolStripMenuItem.Click
        Me.Edge = ABEdge.Top
    End Sub

    Private Sub LeftToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeftToolStripMenuItem.Click
        Me.Edge = ABEdge.Left
    End Sub

    Private Sub RightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RightToolStripMenuItem.Click
        Me.Edge = ABEdge.Right
    End Sub

    Private Sub BottomToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BottomToolStripMenuItem.Click
        Me.Edge = ABEdge.Bottom
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub pnlWindows_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        StartMenu.FadeOut()
    End Sub

End Class
