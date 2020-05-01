Imports System.ComponentModel

Public Class InfoLabel
    Inherits Label
    Implements IShellControl

    WithEvents timUpdate As New Timer

    Public Sub New()
        UpdateOnInterval = True
        UpdateInterval = 1000
        Me.TextAlign = ContentAlignment.MiddleCenter
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
    End Sub

    <System.ComponentModel.Browsable(False)> _
    Public Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property

    Dim _pattern As String = "#time#"

    <System.ComponentModel.DefaultValue("#time#")> Public Property Pattern() As String
        Get
            Return _pattern
        End Get
        Set(ByVal value As String)
            _pattern = value
            UpdateText()
        End Set
    End Property

    <System.ComponentModel.DefaultValue(True)> Public Property UpdateOnInterval() As Boolean
        Get
            Return timUpdate.Enabled
        End Get
        Set(ByVal value As Boolean)
            timUpdate.Enabled = value
        End Set
    End Property

    <System.ComponentModel.DefaultValue(1000)> Public Property UpdateInterval() As Integer
        Get
            Return timUpdate.Interval
        End Get
        Set(ByVal value As Integer)
            timUpdate.Interval = value
        End Set
    End Property

    Public Sub UpdateText()
        Dim newtxt As String = Pattern
        newtxt = newtxt.Replace("#time#", FormatDateTime(Now, DateFormat.LongTime))
        newtxt = newtxt.Replace("#date#", FormatDateTime(Now, DateFormat.ShortDate))
        newtxt = newtxt.Replace("#ldate#", FormatDateTime(Now, DateFormat.LongDate))
        newtxt = newtxt.Replace("#day#", Now.DayOfWeek.ToString)
        newtxt = newtxt.Replace("#compname#", My.Computer.Name)
        newtxt = newtxt.Replace("#n#", vbCrLf)

        newtxt = newtxt.Replace("\#", "#")
        Me.Text = newtxt
    End Sub

    Private Sub timUpdate_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timUpdate.Tick
        UpdateText()
    End Sub

    Public Function GetAlphaMask() As System.Drawing.Bitmap Implements ZPixel.IAlphaPaint.GetAlphaMask
        If _mask Is Nothing Then Refresh()
        Return _mask
    End Function

    Public Sub BeginUpdate() Implements IShellControl.BeginUpdate
        _updating = True
    End Sub

    Public Property Config() As ShellControlConfig Implements IShellControl.Config
        Get
            Return ShellControlHelper.GetConfig(Of InfoLabelConfig)(Me)
        End Get
        Set(ByVal value As ShellControlConfig)
            ShellControlHelper.SetConfig(Me, value)
        End Set
    End Property

    Dim _bck As ShellRenderer = New VisualStyleRenderer

    Public Property Background() As ShellRenderer Implements IShellControl.Background
        Get
            Return _bck
        End Get
        Set(ByVal value As ShellRenderer)
            _bck = value
        End Set
    End Property

    Dim _mask As Bitmap

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim b As New Bitmap(Me.Width, Me.Height)
        Dim gr As Graphics = Graphics.FromImage(b)
        If Background IsNot Nothing Then Background.Render(gr, New Rectangle(0, 0, Me.Width, Me.Height))

        'MyBase.OnPaint(New PaintEventArgs(gr, e.ClipRectangle))
        Dim sf As New StringFormat
        Select Case Me.TextAlign
            Case ContentAlignment.BottomCenter, ContentAlignment.BottomLeft, ContentAlignment.BottomRight
                sf.Alignment = StringAlignment.Far
            Case ContentAlignment.MiddleCenter, ContentAlignment.MiddleLeft, ContentAlignment.MiddleRight
                sf.Alignment = StringAlignment.Center
            Case ContentAlignment.TopCenter, ContentAlignment.TopLeft, ContentAlignment.TopRight
                sf.Alignment = StringAlignment.Near
        End Select
        Select Case Me.TextAlign
            Case ContentAlignment.BottomCenter, ContentAlignment.MiddleCenter, ContentAlignment.TopCenter
                sf.LineAlignment = StringAlignment.Center
            Case ContentAlignment.BottomLeft, ContentAlignment.MiddleLeft, ContentAlignment.TopLeft
                sf.LineAlignment = StringAlignment.Far
            Case ContentAlignment.BottomRight, ContentAlignment.MiddleRight, ContentAlignment.TopRight
                sf.LineAlignment = StringAlignment.Near
        End Select

        gr.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
        gr.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), New RectangleF(Me.Padding.Left, Me.Padding.Top, Me.Width - Me.Padding.Horizontal, Me.Height - Me.Padding.Vertical), sf)
        e.Graphics.DrawImage(b, New Rectangle(0, 0, Me.Width, Me.Height))
        _mask = b
    End Sub

    Public Sub EndUpdate() Implements IShellControl.EndUpdate
        _updating = False
    End Sub

    Dim _updating As Boolean = False

    Public Overrides Sub Refresh() Implements IShellControl.Refresh
        If Not _updating Then
            MyBase.Refresh()
        End If
    End Sub

    Public Overrides Function ToString() As String Implements IShellControl.ToString
        Return "Info Label"
    End Function

End Class

Public Class InfoLabelConfig
    Inherits ShellControlConfig

    Dim _pattern As String = "#time#"

    <DefaultValue("#time#"), Description("A string optionally containing special strings that change into information when displayed. Possible information strings are #time#, #date#, #compname#, #ldate#, and #day#.")> _
    Public Property Pattern() As String
        Get
            Return _pattern
        End Get
        Set(ByVal value As String)
            _pattern = value
        End Set
    End Property

    Dim _updateonint As Boolean = True

    <System.ComponentModel.DefaultValue(True)> Public Property Update_On_Interval() As Boolean
        Get
            Return _updateonint
        End Get
        Set(ByVal value As Boolean)
            _updateonint = value
        End Set
    End Property

    Dim _updateint As Integer = 1000

    <System.ComponentModel.DefaultValue(1000)> Public Property Update_Interval() As Integer
        Get
            Return _updateint
        End Get
        Set(ByVal value As Integer)
            _updateint = value
        End Set
    End Property

    Dim _align As ContentAlignment = ContentAlignment.MiddleCenter

    <DefaultValue(GetType(ContentAlignment), "MiddleCenter"), Description("The vertical and horizontal alignment of the text.")> _
    Public Property Text_Align() As ContentAlignment
        Get
            Return _align
        End Get
        Set(ByVal value As ContentAlignment)
            _align = value
        End Set
    End Property

    Dim _font As Font = Font.FromFont(SystemFonts.DefaultFont)

    <Description("The font used to display the text.")> _
    Public Property Font() As Font
        Get
            Return _font
        End Get
        Set(ByVal value As Font)
            _font = value
        End Set
    End Property

    Dim _color As Color = Color.Black

    <Description("The color of the text.")> _
    Public Property Fore_Color() As Color
        Get
            Return _color
        End Get
        Set(ByVal value As Color)
            _color = value
        End Set
    End Property

End Class