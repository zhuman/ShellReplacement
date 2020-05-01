Public Class EdgebarPanel
    Inherits ShellEditPanel

    Public Overrides Property Dock() As System.Windows.Forms.DockStyle
        Get
            Return MyBase.Dock
        End Get
        Set(ByVal value As System.Windows.Forms.DockStyle)
            If value <> DockStyle.None Then MyBase.Dock = value
        End Set
    End Property

    Dim _opacity As Double = 1

    Public Property Opacity() As Double
        Get
            Return _opacity
        End Get
        Set(ByVal value As Double)
            _opacity = value
        End Set
    End Property

    Public Overrides Function GetName() As String
        Return "Edgebar"
    End Function

    Dim ebc As New EdgebarPanelConfig

    Public Overrides Property Config() As ShellEditPanel.PanelConfig
        Get
            ebc.Dock = Me.Dock
            ebc.Size = IIf(Me.Dock = DockStyle.Bottom Or Me.Dock = DockStyle.Top, Me.Height, Me.Width)
            ebc.Opacity = Me.Opacity
            Return ebc
        End Get
        Set(ByVal value As ShellEditPanel.PanelConfig)
            ebc = value
            Me.Dock = CType(value, EdgebarPanelConfig).Dock
            If Me.Dock = DockStyle.Bottom Or Me.Dock = DockStyle.Top Then
                Me.Height = CType(value, EdgebarPanelConfig).Size
            Else
                Me.Width = CType(value, EdgebarPanelConfig).Size
            End If
            Opacity = CType(value, EdgebarPanelConfig).Opacity
        End Set
    End Property

    Public Class EdgebarPanelConfig
        Inherits ShellEditPanel.PanelConfig

        Dim _dock As DockStyle

        Public Property Dock() As DockStyle
            Get
                Return _dock
            End Get
            Set(ByVal value As DockStyle)
                If value = DockStyle.None Then Throw New Exception("The edgebar must be docked.")
                _dock = value
            End Set
        End Property

        Dim _size As Integer

        Public Property Size() As Integer
            Get
                Return _size
            End Get
            Set(ByVal value As Integer)
                If value <= 0 Then Throw New Exception("The edgebar's size must be greater than zero.")
                _size = value
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

    End Class

    Public Overrides Sub StartSave(ByVal xw As System.Xml.XmlTextWriter)
        xw.WriteStartElement("Panel")
        xw.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        xw.WriteAttributeString("xsi:type", "Edgebar")
        xw.WriteAttributeString("Edge", Me.Dock.ToString)
        xw.WriteAttributeString("Size", IIf(Me.Dock = DockStyle.Bottom Or Me.Dock = DockStyle.Top, Me.Height, Me.Width))
        xw.WriteAttributeString("Opacity", Me.Opacity.ToString)
    End Sub

End Class
