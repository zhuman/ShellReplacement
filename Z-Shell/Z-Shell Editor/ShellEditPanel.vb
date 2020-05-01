Imports System.Xml, System.IO

Public MustInherit Class ShellEditPanel
    Inherits Panel

    Public Sub New()
        Me.AllowDrop = True
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
    End Sub

    Public Sub Save(ByVal xw As XmlTextWriter, ByVal filepath As String, ByVal prgProg As ToolStripProgressBar)
        StartSave(xw)
        xw.WriteStartElement("Controls")

        Dim types(MainForm.configs.Count - 1) As Type
        MainForm.configs.CopyTo(types, 0)

        Dim i As Integer = 0
        For Each c As ShellEditControl In Me.Controls
            xw.WriteStartElement("ShellControl")
            xw.WriteAttributeString("Type", c.ShellControlType.Name)
            xw.WriteAttributeString("ConfigFile", "Configs\" & i.ToString & ".zshconf")

            Dim cont As New ZShell.ConfigContainer
            cont.Config = c.Config

            Dim attroverrides As New Serialization.XmlAttributeOverrides
            'Dim attrFont As New Serialization.XmlAttributes()
            'attroverrides.Add(GetType(Font), attrFont)

            Dim ser As New Serialization.XmlSerializer(GetType(ZShell.ConfigContainer), attroverrides, types, New Serialization.XmlRootAttribute("ConfigContainer"), "")
            IO.Directory.CreateDirectory(IO.Path.Combine(IO.Path.GetDirectoryName(filepath), "Configs"))
            Dim fs As New IO.FileStream(IO.Path.Combine(IO.Path.GetDirectoryName(filepath), "Configs\" & i.ToString & ".zshconf"), IO.FileMode.Create)
            ser.Serialize(fs, cont)
            fs.Close()
            xw.WriteEndElement()
            i += 1
            prgProg.Value += 1
        Next

        xw.WriteEndElement()
        xw.WriteEndElement()
    End Sub

    Public MustOverride Sub StartSave(ByVal xw As XmlTextWriter)
    Public MustOverride Function GetName() As String

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Dim s As New StringFormat
        s.Alignment = StringAlignment.Center
        s.LineAlignment = StringAlignment.Center
        e.Graphics.DrawString(GetName, SystemFonts.DefaultFont, Brushes.Black, New Rectangle(0, 0, Me.Width, Me.Height), s)
        Dim p As New Pen(Color.Black)
        If Not Selected Then p.DashStyle = Drawing2D.DashStyle.Dash
        e.Graphics.DrawRectangle(p, New Rectangle(0, 0, Me.Width - 1, Me.Height - 1))
    End Sub

    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseClick(e)
        MainForm.SelectPanel(Me)
        If e.Button = Windows.Forms.MouseButtons.Right Then
            MainForm.mnuPanel.Show(Me, e.Location)
        End If
    End Sub

    Private Sub ShellEditPanel_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        If (e.AllowedEffect And DragDropEffects.Copy) = DragDropEffects.Copy AndAlso e.Data.GetDataPresent(GetType(String)) AndAlso CStr(e.Data.GetData(GetType(String))).StartsWith("ShellControl:") Then
            Dim data As Integer = CStr(e.Data.GetData(GetType(String))).Substring("ShellControl:".Length)
            If MainForm.tools.Count > data Then
                Dim ctrl As New ShellEditControl(MainForm.tools(data))
                Dim p As Point = Me.PointToClient(New Point(e.X, e.Y))
                ctrl.Location = p
                ctrl.Size = New Size(32, 32)
                Me.Controls.Add(ctrl)
                MainForm.SelectControl(ctrl)
            End If
        End If
    End Sub

    Private Sub ShellEditPanel_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
        If (e.AllowedEffect And DragDropEffects.Copy) = DragDropEffects.Copy AndAlso e.Data.GetDataPresent(GetType(String)) AndAlso CStr(e.Data.GetData(GetType(String))).StartsWith("ShellControl:") Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub ShellEditPanel_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragOver
        ShellEditPanel_DragEnter(sender, e)
    End Sub

    Public MustOverride Property Config() As PanelConfig

    Public MustInherit Class PanelConfig

        'Dim _background As ZShell.ShellRenderer

        'Public Property Background() As ZShell.ShellRenderer
        '    Get

        '    End Get
        '    Set(ByVal value As ZShell.ShellRenderer)

        '    End Set
        'End Property

    End Class

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
