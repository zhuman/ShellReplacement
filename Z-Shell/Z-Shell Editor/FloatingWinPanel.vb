Public Class FloatingWinPanel
    Inherits ShellEditPanel

    Public Overrides Function GetName() As String
        Return "Floating Window"
    End Function

    Dim fwc As New FloatingWinPanelConfig

    Public Overrides Property Config() As ShellEditPanel.PanelConfig
        Get
            fwc.Location = Me.Location
            fwc.Size = Me.Size
            Return fwc
        End Get
        Set(ByVal value As ShellEditPanel.PanelConfig)
            fwc = value
            Me.Location = CType(value, FloatingWinPanelConfig).Location
            Me.Size = CType(value, FloatingWinPanelConfig).Size
        End Set
    End Property

    Public Class FloatingWinPanelConfig
        Inherits ShellEditPanel.PanelConfig

        Dim _size As Size

        Public Property Size() As Size
            Get
                Return _size
            End Get
            Set(ByVal value As Size)
                _size = value
            End Set
        End Property

        Dim _location As Point

        Public Property Location() As Point
            Get
                Return _location
            End Get
            Set(ByVal value As Point)
                _location = value
            End Set
        End Property

        Dim _opacity As Double

        Public Property Opacity() As Double
            Get
                Return _opacity
            End Get
            Set(ByVal value As Double)
                If value > 1 Or value < 0 Then Throw New Exception("The edgebar's opacity must be from 0 to 1.")
                _opacity = value
            End Set
        End Property

        Dim _topmost As Boolean

        Public Property Topmost() As Boolean
            Get
                Return _topmost
            End Get
            Set(ByVal value As Boolean)
                _topmost = value
            End Set
        End Property

        Dim _border As FormBorderStyle

        Public Property Border() As FormBorderStyle
            Get
                Return _border
            End Get
            Set(ByVal value As FormBorderStyle)
                _border = value
            End Set
        End Property

    End Class

    Dim _opacity As Double = 1

    Public Property Opacity() As Double
        Get
            Return _opacity
        End Get
        Set(ByVal value As Double)
            _opacity = value
        End Set
    End Property

    Dim _topmost As Boolean = True

    Public Property Topmost() As Boolean
        Get
            Return _topmost
        End Get
        Set(ByVal value As Boolean)
            _topmost = value
        End Set
    End Property

    Dim _border As FormBorderStyle

    Public Property Border() As FormBorderStyle
        Get
            Return _border
        End Get
        Set(ByVal value As FormBorderStyle)
            _border = value
        End Set
    End Property

    Public Overrides Sub StartSave(ByVal xw As System.Xml.XmlTextWriter)
        xw.WriteStartElement("Panel")
        xw.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        xw.WriteAttributeString("xsi:type", "FloatingWin")
        xw.WriteAttributeString("X", Me.Left)
        xw.WriteAttributeString("Y", Me.Top)
        xw.WriteAttributeString("Width", Me.Width)
        xw.WriteAttributeString("Height", Me.Height)
        xw.WriteAttributeString("TopMost", LCase(Me.Topmost.ToString))
        xw.WriteAttributeString("Opacity", Me.Opacity.ToString)
        xw.WriteAttributeString("BorderStyle", Me.BorderStyle.ToString)
    End Sub

End Class
