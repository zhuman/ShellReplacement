Public Class ShellEditControl
    Inherits Control

    Dim _t As Type

    Public Sub New(ByVal t As Type)
        _t = t
        _testControl = _t.GetConstructor(New Type() {}).Invoke(New Object() {})
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Dim s As New StringFormat
        s.Alignment = StringAlignment.Center
        s.LineAlignment = StringAlignment.Center
        'e.Graphics.DrawString(_t.Name, SystemFonts.DefaultFont, Brushes.Black, New Rectangle(0, 0, Me.Width, Me.Height), s)
        CType(_testControl, Control).Size = Me.Size
        Dim b As New Bitmap(Me.Width, Me.Height)
        CType(_testControl, Control).DrawToBitmap(b, New Rectangle(0, 0, b.Width, b.Height))
        e.Graphics.DrawImage(b, 0, 0)
        Dim p As New Pen(Color.Black)
        If Not Selected Then p.DashStyle = Drawing2D.DashStyle.Dash
        e.Graphics.DrawRectangle(p, New Rectangle(0, 0, Me.Width - 1, Me.Height - 1))
    End Sub

    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseClick(e)
        MainForm.SelectControl(Me)
        If e.Button = Windows.Forms.MouseButtons.Right Then
            MainForm.mnuControl.Show(Me, e.Location)
        End If
    End Sub

    Dim _testControl As ZShell.IShellControl

    Public Property Config() As ZShell.ShellControlConfig
        Get
            Dim c As ZShell.ShellControlConfig = _testControl.Config
            c.Location = Me.Location
            c.Dock = Me.Dock
            c.Size = Me.Size
            c.Anchor = Me.Anchor
            Return c
        End Get
        Set(ByVal value As ZShell.ShellControlConfig)
            _testControl.Config = value
            Me.Location = value.Location
            Me.Size = value.Size
            Me.Anchor = value.Anchor
            Me.Dock = value.Dock
            Me.Refresh()
        End Set
    End Property

    Public ReadOnly Property ShellControlType() As Type
        Get
            Return _t
        End Get
    End Property

    Dim _selected As Boolean

    Public Property Selected() As Boolean
        Get
            Return _selected
        End Get
        Set(ByVal value As Boolean)
            _selected = value
            Refresh()
        End Set
    End Property

End Class