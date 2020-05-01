Imports System.Xml, System.Xml.Serialization, System.Xml.XPath, System.Xml.Schema, System.Reflection

Module StyleLoader

    Dim message As String
    Dim valErr As Boolean

    Dim _desc As String

    Public ReadOnly Property Description() As String
        Get
            Return _desc
        End Get
    End Property

    Dim _author As String

    Public ReadOnly Property Author() As String
        Get
            Return _author
        End Get
    End Property

    Dim _copyright As String

    Public ReadOnly Property Copyright() As String
        Get
            Return _copyright
        End Get
    End Property

    Public Sub LoadStyle(ByVal filepath As String)
        Dim xdoc As New XmlDocument()
        xdoc.Load(filepath)
        message = ""
        xdoc.Schemas.Add("http://z-sys.org/Documents/ShellStyle.xsd", IO.Path.Combine(Application.StartupPath, "ShellStyle.xsd"))
        xdoc.Validate(New ValidationEventHandler(AddressOf ValidateEvent))
        If valErr Then message &= vbCrLf & vbCrLf & "Couldn't load shell style."
        If message <> "" Then
            MessageBox.Show(message, "Shell Style Loading Error", MessageBoxButtons.OK, IIf(valErr, MessageBoxIcon.Error, MessageBoxIcon.Warning))
        End If
        If valErr Then Exit Sub
        For Each n As XmlNode In xdoc.DocumentElement.ChildNodes
            Select Case n.Name
                Case "Description"
                    _desc = n.InnerText.Trim
                Case "Author"
                    _author = n.InnerText.Trim
                Case "Copyright"
                    _copyright = n.InnerText.Trim
                Case "Panels"
                    For Each o As XmlNode In n.ChildNodes
                        Dim f As Form = Nothing
                        Select Case o.Attributes("xsi:type").Value
                            Case "Edgebar"
                                f = New StyleEdgeBar
                                f.FormBorderStyle = FormBorderStyle.None
                                CType(f, AppbarForm).Edge = [Enum].Parse(GetType(AppbarForm.ABEdge), o.Attributes("Edge").Value)
                                If CType(f, AppbarForm).Edge = AppbarForm.ABEdge.Bottom Or CType(f, AppbarForm).Edge = AppbarForm.ABEdge.Top Then
                                    f.Height = Integer.Parse(o.Attributes("Size").Value)
                                Else
                                    f.Width = Integer.Parse(o.Attributes("Size").Value)
                                End If
                                f.TopMost = True
                            Case "FloatingWin"
                                f = New StyleFloatingWin
                                f.FormBorderStyle = [Enum].Parse(GetType(FormBorderStyle), o.Attributes("BorderStyle").Value)
                                f.Width = Integer.Parse(o.Attributes("Width").Value)
                                f.Height = Integer.Parse(o.Attributes("Height").Value)
                                f.Left = Integer.Parse(o.Attributes("X").Value)
                                f.Top = Integer.Parse(o.Attributes("Y").Value)
                                f.Opacity = Double.Parse(o.Attributes("Opacity").Value)
                                f.TopMost = True
                            Case "LayeredWin"

                        End Select

                        If f IsNot Nothing Then
                            Dim controlColl as New ObjectModel.Collection(Of Control)

                            For Each child As XmlNode In o.ChildNodes
                                If child.Name = "Controls" Then
                                    For Each c As XmlNode In child.ChildNodes
                                        If c.Name = "ShellControl" Then
                                            Try
                                                Dim controlType As Type = Type.GetType("ZShell." & CStr(c.Attributes("Type").Value))
                                                If controlType Is Nothing Then
                                                    MessageBox.Show(CStr(c.Attributes("Type").Value) & " could not be found.")
                                                    Continue For
                                                End If
                                                Dim controlObj As Control = controlType.GetConstructor(New Type() {}).Invoke(New Object() {})

                                                Dim cfgStr As XmlAttribute = c.Attributes("ConfigFile")
                                                If cfgStr IsNot Nothing Then
                                                    Try
                                                        LoadControlConfig(IO.Path.Combine(IO.Path.GetDirectoryName(filepath), cfgStr.Value), controlObj)
                                                    Catch ex As Exception
                                                        Debug.Print("Error loading control config file. " & ex.Message)
                                                    End Try
                                                End If
                                                f.Controls.Add(controlObj)
                                            Catch ex As Exception
                                                MessageBox.Show(CStr(c.Attributes("Type").Value) & ": " & ex.Message)
                                            End Try
                                        End If
                                    Next
                                End If
                            Next

                            'Dim t As New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf StartFormThread))
                            't.Start(f)
                            AddHandler f.FormClosing, AddressOf StyleFormClosing
                            f.Show()
                        End If
                    Next
            End Select
        Next
    End Sub

    Private Sub LoadControlConfig(ByVal file As String, ByVal control As IShellControl)
        Dim extraTypes As New ObjectModel.Collection(Of Type)
        For Each a As Assembly In AppDomain.CurrentDomain.GetAssemblies
            For Each t As Type In a.GetTypes
                If GetType(ShellControlConfig).IsAssignableFrom(t) Or GetType(ShellRenderer).IsAssignableFrom(t) Then
                    extraTypes.Add(t)
                End If
            Next
        Next
        Dim extraTypesArr(extraTypes.Count - 1) As Type
        extraTypes.CopyTo(extraTypesArr, 0)
        Dim xs As New Xml.Serialization.XmlSerializer(GetType(ConfigContainer), extraTypesArr)
        Dim xr As New XmlTextReader(New IO.FileStream(file, IO.FileMode.Open))
        If xs.CanDeserialize(xr) Then
            Dim cc As ConfigContainer = xs.Deserialize(xr)
            control.Config = cc.Config
        End If
    End Sub

    Private Sub ValidateEvent(ByVal sender As Object, ByVal e As ValidationEventArgs)
        message = message & vbCrLf & e.Severity.ToString & ": " & e.Message
        If e.Severity = XmlSeverityType.Error Then
            valErr = True
        End If
    End Sub

    Private Sub StyleFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)
        If e.CloseReason = CloseReason.UserClosing AndAlso TypeOf (sender) Is Form AndAlso CType(sender, Form).FormBorderStyle = FormBorderStyle.None Then
            e.Cancel = True
            Dim sdb As New ShutdownDialog
            sdb.ShowDialog()
        End If
    End Sub

    'Private Sub StartFormThread(ByVal f As Object)
    '    Application.Run(CType(f, Form))
    'End Sub

End Module

Public Class StyleEdgeBar
    Inherits AppbarForm

    Dim _bck As ShellRenderer

    Public Property Background() As ShellRenderer
        Get
            Return _bck
        End Get
        Set(ByVal value As ShellRenderer)
            _bck = value
        End Set
    End Property

    Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaintBackground(e)
        If _bck IsNot Nothing Then _bck.Render(e.Graphics, New Rectangle(0, 0, Me.Width, Me.Height))
    End Sub

End Class

Public Class StyleFloatingWin
    Inherits Form

    Dim _bck As ShellRenderer

    Public Property Background() As ShellRenderer
        Get
            Return _bck
        End Get
        Set(ByVal value As ShellRenderer)
            _bck = value
        End Set
    End Property

    Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaintBackground(e)
        If _bck IsNot Nothing Then _bck.Render(e.Graphics, New Rectangle(0, 0, Me.Width, Me.Height))
    End Sub

End Class

Public Class ConfigContainer

    Dim _config As ShellControlConfig

    Public Property Config() As ShellControlConfig
        Get
            Return _config
        End Get
        Set(ByVal value As ShellControlConfig)
            _config = value
        End Set
    End Property

End Class