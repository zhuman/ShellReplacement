Imports System.Reflection, System.Xml, System.Xml.Schema, System.Xml.Serialization, ZShell

Public Class MainForm

#Region "Selection"

	Dim _controlSelected As Boolean
	Dim _selectedControl As ShellEditControl
	Dim _selectedPanel As ShellEditPanel

	Dim _selectingSub As Boolean = False

	Public Sub SelectControl(ByVal ctrl As ShellEditControl)
		_selectingSub = True
		_controlSelected = True
		_selectedControl = ctrl
		_selectedPanel = CType(ctrl.Parent, ShellEditPanel)
		lstControls.Items.Clear()
		For Each c As ShellEditControl In SelectedPanel.Controls
			lstControls.Items.Add(c.ShellControlType.Name)
			c.Selected = (c Is ctrl)
			If ctrl Is c Then
				lstControls.SelectedIndex = lstControls.Items.Count - 1
			End If
		Next
		For Each p As ShellEditPanel In pnlScreen.Controls
			p.Selected = False
		Next
		Me.PropertyGrid1.SelectedObject = _selectedControl.Config
		_selectingSub = False
	End Sub

	Public Sub SelectPanel(ByVal pnl As ShellEditPanel)
		_selectingSub = True
		_controlSelected = False
		_selectedControl = Nothing
		_selectedPanel = pnl
		lstControls.ClearSelected()
		lstControls.Items.Clear()
		For Each c As ShellEditControl In SelectedPanel.Controls
			lstControls.Items.Add(c.ShellControlType.Name)
			c.Selected = False
		Next
		For Each p As ShellEditPanel In pnlScreen.Controls
			p.Selected = (p Is pnl)
		Next
		Me.PropertyGrid1.SelectedObject = _selectedPanel.Config
		_selectingSub = False
	End Sub

	Public ReadOnly Property ControlSelected() As Boolean
		Get
			Return _controlSelected
		End Get
	End Property

	Public ReadOnly Property SelectedPanel() As ShellEditPanel
		Get
			Return _selectedPanel
		End Get
	End Property

	Public ReadOnly Property SelectedControl() As ShellEditControl
		Get
			Return _selectedControl
		End Get
	End Property

#End Region

#Region "UI Events"

	Public tools As New ObjectModel.Collection(Of Type)
	Public configs As New ObjectModel.Collection(Of Type)

	Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		ToolStripManager.LoadSettings(Me)

		'Load all available IShellControl types
		For Each a As Assembly In My.Application.Info.LoadedAssemblies
			For Each t As Type In a.GetTypes
				If GetType(Control).IsAssignableFrom(t) AndAlso GetType(ZShell.IShellControl).IsAssignableFrom(t) Then
					Me.lstTools.Items.Add(t.Name)
					tools.Add(t)
				End If
			Next
		Next

		'Load all available ShellControlConfig and ShellRenderer types
		For Each a As Assembly In My.Application.Info.LoadedAssemblies
			For Each t As Type In a.GetTypes
				If GetType(ZShell.ShellControlConfig).IsAssignableFrom(t) Or GetType(ZShell.ShellRenderer).IsAssignableFrom(t) Then
					configs.Add(t)
				End If
			Next
		Next

		'Take the desktop background and apply it to the main panel
		If IO.File.Exists(Desktop.Wallpaper) Then
			pnlScreen.BackgroundImage = New Bitmap(Desktop.Wallpaper)
			Select Case Desktop.WallpaperStyle
				Case Desktop.WallpaperStyleEnum.Centered
					pnlScreen.BackgroundImageLayout = ImageLayout.Center
				Case Desktop.WallpaperStyleEnum.Stretched
					pnlScreen.BackgroundImageLayout = ImageLayout.Stretch
				Case Desktop.WallpaperStyleEnum.Tiled
					pnlScreen.BackgroundImageLayout = ImageLayout.Tile
			End Select
		End If
	End Sub

	Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		ToolStripManager.SaveSettings(Me)
	End Sub

	Private Sub PropertyGrid1_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGrid1.PropertyValueChanged
		If ControlSelected Then
			If SelectedControl IsNot Nothing Then
				SelectedControl.Config = Me.PropertyGrid1.SelectedObject
			End If
		Else
			If SelectedPanel IsNot Nothing Then
				SelectedPanel.Config = Me.PropertyGrid1.SelectedObject
			End If
		End If
	End Sub

	Private Sub ListBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstTools.MouseMove
		If e.Button = Windows.Forms.MouseButtons.Left AndAlso lstTools.SelectedIndex >= 0 Then
			lstTools.DoDragDrop("ShellControl:" & lstTools.SelectedIndex.ToString, DragDropEffects.Copy)
		End If
	End Sub

	Private Sub TableLayoutPanel1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TableLayoutPanel1.SizeChanged
		Dim wRat As Double = Me.TableLayoutPanel1.Width / Screen.PrimaryScreen.Bounds.Width
		Me.TableLayoutPanel1.RowStyles(1).Height = Screen.PrimaryScreen.Bounds.Height * wRat
	End Sub

	Private Sub pnlScreen_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles pnlScreen.DragDrop
		If (e.AllowedEffect And DragDropEffects.Copy) = DragDropEffects.Copy AndAlso e.Data.GetDataPresent(GetType(String)) AndAlso CStr(e.Data.GetData(GetType(String))).StartsWith("ShellPanel:") Then
			Dim data As Integer = Integer.Parse(CStr(e.Data.GetData(GetType(String))).Substring("ShellPanel:".Length))
			Dim p As Point = pnlScreen.PointToClient(New Point(e.X, e.Y))
			Select Case data
				Case 0
					Dim eb As New EdgebarPanel
					Dim closest As Integer = Math.Min(Math.Min(p.X, pnlScreen.Width - p.X), Math.Min(p.Y, pnlScreen.Height - p.Y))
					Select Case closest
						Case p.X
							eb.Dock = DockStyle.Left
						Case pnlScreen.Width - p.X
							eb.Dock = DockStyle.Right
						Case p.Y
							eb.Dock = DockStyle.Top
						Case Else
							eb.Dock = DockStyle.Bottom
					End Select
					Select Case eb.Dock
						Case DockStyle.Top, DockStyle.Bottom
							eb.Height = 32
						Case Else
							eb.Width = 32
					End Select
					pnlScreen.Controls.Add(eb)
					pnlScreen.Controls.SetChildIndex(eb, 0)
					SelectPanel(eb)
				Case 1
					Dim fp As New FloatingWinPanel
					fp.Location = p
					fp.Size = New Size(32, 32)
					pnlScreen.Controls.Add(fp)
					pnlScreen.Controls.SetChildIndex(fp, 0)
					SelectPanel(fp)
			End Select
		End If
	End Sub

	Private Sub pnlScreen_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles pnlScreen.DragEnter
		If (e.AllowedEffect And DragDropEffects.Copy) = DragDropEffects.Copy AndAlso e.Data.GetDataPresent(GetType(String)) AndAlso CStr(e.Data.GetData(GetType(String))).StartsWith("ShellPanel:") Then
			e.Effect = DragDropEffects.Copy
		End If
	End Sub

	Private Sub pnlScreen_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles pnlScreen.DragOver
		pnlScreen_DragEnter(sender, e)
	End Sub

	Private Sub pnlScreen_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlScreen.Paint

	End Sub

	Private Sub lstPanels_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstPanels.MouseMove
		If e.Button = Windows.Forms.MouseButtons.Left AndAlso lstPanels.SelectedIndex >= 0 Then
			lstTools.DoDragDrop("ShellPanel:" & lstPanels.SelectedIndex.ToString, DragDropEffects.Copy)
		End If
	End Sub

	Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click, SaveToolStripButton.Click
		Save()
	End Sub

	Private Sub lstControls_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstControls.SelectedIndexChanged
		btnConUp.Enabled = lstControls.SelectedIndex >= 0
		btnConDown.Enabled = lstControls.SelectedIndex >= 0
		btnConDel.Enabled = lstControls.SelectedIndex >= 0
		If Not _selectingSub AndAlso lstControls.SelectedIndex >= 0 Then
			If SelectedPanel IsNot Nothing Then
				SelectControl(SelectedPanel.Controls(lstControls.SelectedIndex))
			End If
		End If
	End Sub

	Private Sub ClearAllControlsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearAllControlsToolStripMenuItem.Click
		If SelectedPanel IsNot Nothing Then
			SelectedPanel.Controls.Clear()
			lstControls.Items.Clear()
		End If
	End Sub

	Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
		If SelectedPanel IsNot Nothing Then
			pnlScreen.Controls.Remove(SelectedPanel)
			_selectedPanel = Nothing
			lstControls.ClearSelected()
			lstControls.Items.Clear()
			Me.PropertyGrid1.SelectedObject = Nothing
		End If
	End Sub

	Private Sub btnConUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConUp.Click
		If lstControls.SelectedIndex >= 0 Then
			If SelectedPanel IsNot Nothing And SelectedControl IsNot Nothing Then
				If SelectedPanel.Controls.IndexOf(SelectedControl) = 0 Then
					SelectedPanel.Controls.SetChildIndex(SelectedControl, SelectedPanel.Controls.Count - 1)
				Else
					SelectedPanel.Controls.SetChildIndex(SelectedControl, SelectedPanel.Controls.IndexOf(SelectedControl) - 1)
				End If
				SelectControl(SelectedControl)
			End If
		End If
	End Sub

	Private Sub btnConDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConDown.Click
		If lstControls.SelectedIndex >= 0 Then
			If SelectedPanel IsNot Nothing And SelectedControl IsNot Nothing Then
				If SelectedPanel.Controls.IndexOf(SelectedControl) = SelectedPanel.Controls.Count - 1 Then
					SelectedPanel.Controls.SetChildIndex(SelectedControl, 0)
				Else
					SelectedPanel.Controls.SetChildIndex(SelectedControl, SelectedPanel.Controls.IndexOf(SelectedControl) + 1)
				End If
				SelectControl(SelectedControl)
			End If
		End If
	End Sub

	Private Sub btnConDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConDel.Click
		If SelectedControl IsNot Nothing And SelectedPanel IsNot Nothing Then
			SelectedPanel.Controls.Remove(SelectedControl)
			SelectPanel(SelectedPanel)
		End If
	End Sub

	Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
		Save(True)
	End Sub

	Private Sub DeleteToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem1.Click
		If SelectedControl IsNot Nothing And SelectedPanel IsNot Nothing Then
			SelectedPanel.Controls.Remove(SelectedControl)
			SelectPanel(SelectedPanel)
		End If
	End Sub

	Private Sub SelectParentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectParentToolStripMenuItem.Click
		If SelectedPanel IsNot Nothing Then
			SelectPanel(SelectedPanel)
		End If
	End Sub

#End Region

#Region "File Open/Save"

	Dim _currentFile As String

	Public Sub Save(Optional ByVal forceNew As Boolean = False)
		Try
			'If the user hasn't chosen a location to save yet, let them
			If _currentFile = "" OrElse forceNew Then
				If sfdSaveStyle.ShowDialog = Windows.Forms.DialogResult.OK Then
					_currentFile = sfdSaveStyle.FileName
				Else
					Exit Sub
				End If
			End If
			'Set up the file
			If IO.File.Exists(_currentFile) Then IO.File.Delete(_currentFile)
			Dim fs As New IO.FileStream(_currentFile, IO.FileMode.Create)
			Dim xw As New XmlTextWriter(fs, System.Text.Encoding.Unicode)
			xw.Formatting = Formatting.Indented

			'Start writing the style file
			xw.WriteStartDocument()
			xw.WriteStartElement("ShellStyle", "http://z-sys.org/Documents/ShellStyle.xsd")
			xw.WriteAttributeString("FormatVersion", "1")
			xw.WriteElementString("Description", txtDesc.Text)
			xw.WriteElementString("Copyright", txtCopyright.Text)
			xw.WriteElementString("Author", txtAuthor.Text)
			xw.WriteStartElement("Panels")

			'Write out all of the panels
			For Each p As ShellEditPanel In pnlScreen.Controls
				p.Save(xw, _currentFile, Me.prgProg)
			Next

			'End the file and close up
			xw.WriteEndDocument()
			xw.Flush()
			xw.Close()
			fs.Close()

		Catch ex As Exception
			MessageBox.Show("Error saving file. " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Dim xmlParseErrorMessage As String
	Dim valErr As Boolean

	Public Sub Open()


		pnlScreen.Controls.Clear()

		Dim xdoc As New XmlDocument()
		xdoc.Load(_currentFile)
		xmlParseErrorMessage = ""
		xdoc.Schemas.Add("http://z-sys.org/Documents/ShellStyle.xsd", IO.Path.Combine(Application.StartupPath, "ShellStyle.xsd"))
		xdoc.Validate(New Schema.ValidationEventHandler(AddressOf ValidateEvent))
		If valErr Then xmlParseErrorMessage &= vbCrLf & vbCrLf & "Couldn't load shell style."
		If xmlParseErrorMessage <> "" Then
			MessageBox.Show(xmlParseErrorMessage, "Shell Style Loading Error", MessageBoxButtons.OK, IIf(valErr, MessageBoxIcon.Error, MessageBoxIcon.Warning))
		End If
		If valErr Then Exit Sub
		For Each n As XmlNode In xdoc.DocumentElement.ChildNodes
			Select Case n.Name
				Case "Description"
					txtDesc.Text = n.InnerText.Trim
				Case "Author"
					txtAuthor.Text = n.InnerText.Trim
				Case "Copyright"
					txtCopyright.Text = n.InnerText.Trim
				Case "Panels"
					For Each o As XmlNode In n.ChildNodes
						Dim f As ShellEditPanel = Nothing
						Select Case o.Attributes("xsi:type").Value
							Case "Edgebar"
								f = New EdgebarPanel
								f.Dock = [Enum].Parse(GetType(DockStyle), o.Attributes("Edge").Value)
								If f.Dock = DockStyle.Bottom Or f.Dock = DockStyle.Top Then
									f.Height = Integer.Parse(o.Attributes("Size").Value)
								Else
									f.Width = Integer.Parse(o.Attributes("Size").Value)
								End If
							Case "FloatingWin"
								f = New FloatingWinPanel
								CType(f.Config, FloatingWinPanel.FloatingWinPanelConfig).Border = [Enum].Parse(GetType(FormBorderStyle), o.Attributes("BorderStyle").Value)
								f.Width = Integer.Parse(o.Attributes("Width").Value)
								f.Height = Integer.Parse(o.Attributes("Height").Value)
								f.Left = Integer.Parse(o.Attributes("X").Value)
								f.Top = Integer.Parse(o.Attributes("Y").Value)
								CType(f.Config, FloatingWinPanel.FloatingWinPanelConfig).Opacity = Double.Parse(o.Attributes("Opacity").Value)
							Case "LayeredWin"

						End Select

						If f IsNot Nothing Then
							Dim controlColl As New ObjectModel.Collection(Of Control)

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
												Dim controlObj As New ShellEditControl(controlType)

												Dim cfgStr As XmlAttribute = c.Attributes("ConfigFile")
												If cfgStr IsNot Nothing Then
													Try
														LoadControlConfig(IO.Path.Combine(IO.Path.GetDirectoryName(_currentFile), cfgStr.Value), controlObj)
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

							pnlscreen.Controls.Add(f)
						End If
					Next
			End Select
		Next
	End Sub

	Private Sub LoadControlConfig(ByVal file As String, ByVal control As ShellEditControl)
		Dim extraTypes As New ObjectModel.Collection(Of Type)
		For Each a As Assembly In AppDomain.CurrentDomain.GetAssemblies
			For Each t As Type In a.GetTypes
				If GetType(ZShell.ShellControlConfig).IsAssignableFrom(t) Or GetType(ZShell.ShellRenderer).IsAssignableFrom(t) Then
					extraTypes.Add(t)
				End If
			Next
		Next
		Dim extraTypesArr(extraTypes.Count - 1) As Type
		extraTypes.CopyTo(extraTypesArr, 0)
		Dim xs As New Xml.Serialization.XmlSerializer(GetType(ZShell.ConfigContainer), extraTypesArr)
		Dim xr As New XmlTextReader(New IO.FileStream(file, IO.FileMode.Open))
		If xs.CanDeserialize(xr) Then
			Dim cc As ZShell.ConfigContainer = xs.Deserialize(xr)
			control.Config = cc.Config
		End If
	End Sub

	Private Sub ValidateEvent(ByVal sender As Object, ByVal e As Schema.ValidationEventArgs)
		xmlParseErrorMessage = xmlParseErrorMessage & vbCrLf & e.Severity.ToString & ": " & e.Message
		If e.Severity = XmlSeverityType.Error Then
			valErr = True
		End If
	End Sub

#End Region

End Class

'Public MustInherit Class ShellEditPanel
'    Inherits Panel

'    Public Sub New()
'        Me.AllowDrop = True
'        Me.SetStyle(ControlStyles.ResizeRedraw, True)
'    End Sub

'    Public Sub Save(ByVal xw As XmlTextWriter, ByVal filepath As String)
'        StartSave(xw)
'        xw.WriteStartElement("Controls")

'        Dim types(MainForm.configs.Count - 1) As Type
'        MainForm.configs.CopyTo(types, 0)

'        Dim i As Integer = 0
'        For Each c As ShellEditControl In Me.Controls
'            xw.WriteStartElement("ShellControl")
'            xw.WriteAttributeString("Type", c.ShellControlType.Name)
'            xw.WriteAttributeString("ConfigFile", "Configs\" & i.ToString & ".zshconf")

'            Dim cont As New ZShell.ConfigContainer
'            cont.Config = c.Config

'            Dim attroverrides As New Serialization.XmlAttributeOverrides
'            'Dim attrFont As New Serialization.XmlAttributes()
'            'attroverrides.Add(GetType(Font), attrFont)

'            Dim ser As New Serialization.XmlSerializer(GetType(ZShell.ConfigContainer), attroverrides, types, New Serialization.XmlRootAttribute("ConfigContainer"), "")
'            IO.Directory.CreateDirectory(IO.Path.Combine(IO.Path.GetDirectoryName(filepath), "Configs"))
'            Dim fs As New IO.FileStream(IO.Path.Combine(IO.Path.GetDirectoryName(filepath), "Configs\" & i.ToString & ".zshconf"), IO.FileMode.Create)
'            ser.Serialize(fs, cont)
'            fs.Close()
'            xw.WriteEndElement()
'            i += 1
'        Next

'        xw.WriteEndElement()
'        xw.WriteEndElement()
'    End Sub

'    Public MustOverride Sub StartSave(ByVal xw As XmlTextWriter)
'    Public MustOverride Function GetName() As String

'    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
'        MyBase.OnPaint(e)
'        Dim s As New StringFormat
'        s.Alignment = StringAlignment.Center
'        s.LineAlignment = StringAlignment.Center
'        e.Graphics.DrawString(GetName, SystemFonts.DefaultFont, Brushes.Black, New Rectangle(0, 0, Me.Width, Me.Height), s)
'        Dim p As New Pen(Color.Black)
'        If Not Selected Then p.DashStyle = Drawing2D.DashStyle.Dash
'        e.Graphics.DrawRectangle(p, New Rectangle(0, 0, Me.Width - 1, Me.Height - 1))
'    End Sub

'    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
'        MyBase.OnMouseClick(e)
'        MainForm.SelectPanel(Me)
'        If e.Button = Windows.Forms.MouseButtons.Right Then
'            MainForm.mnuPanel.Show(Me, e.Location)
'        End If
'    End Sub

'    Private Sub ShellEditPanel_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
'        If (e.AllowedEffect And DragDropEffects.Copy) = DragDropEffects.Copy AndAlso e.Data.GetDataPresent(GetType(String)) AndAlso CStr(e.Data.GetData(GetType(String))).StartsWith("ShellControl:") Then
'            Dim data As Integer = CStr(e.Data.GetData(GetType(String))).Substring("ShellControl:".Length)
'            If MainForm.tools.Count > data Then
'                Dim ctrl As New ShellEditControl(MainForm.tools(data))
'                Dim p As Point = Me.PointToClient(New Point(e.X, e.Y))
'                ctrl.Location = p
'                ctrl.Size = New Size(32, 32)
'                Me.Controls.Add(ctrl)
'                MainForm.SelectControl(ctrl)
'            End If
'        End If
'    End Sub

'    Private Sub ShellEditPanel_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
'        If (e.AllowedEffect And DragDropEffects.Copy) = DragDropEffects.Copy AndAlso e.Data.GetDataPresent(GetType(String)) AndAlso CStr(e.Data.GetData(GetType(String))).StartsWith("ShellControl:") Then
'            e.Effect = DragDropEffects.Copy
'        End If
'    End Sub

'    Private Sub ShellEditPanel_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragOver
'        ShellEditPanel_DragEnter(sender, e)
'    End Sub

'    Public MustOverride Property Config() As PanelConfig

'    Public MustInherit Class PanelConfig

'        'Dim _background As ZShell.ShellRenderer

'        'Public Property Background() As ZShell.ShellRenderer
'        '    Get

'        '    End Get
'        '    Set(ByVal value As ZShell.ShellRenderer)

'        '    End Set
'        'End Property

'    End Class

'    Dim _selected As Boolean

'    Public Property Selected() As Boolean
'        Get
'            Return _selected
'        End Get
'        Set(ByVal value As Boolean)
'            _selected = value
'            Refresh()
'        End Set
'    End Property

'End Class

'Public Class FloatingWinPanel
'    Inherits ShellEditPanel

'    Public Overrides Function GetName() As String
'        Return "Floating Window"
'    End Function

'    Public Overrides Property Config() As ShellEditPanel.PanelConfig
'        Get
'            Dim fwc As New FloatingWinPanelConfig
'            fwc.Location = Me.Location
'            fwc.Size = Me.Size
'            Return fwc
'        End Get
'        Set(ByVal value As ShellEditPanel.PanelConfig)
'            Me.Location = CType(value, FloatingWinPanelConfig).Location
'            Me.Size = CType(value, FloatingWinPanelConfig).Size
'        End Set
'    End Property

'    Public Class FloatingWinPanelConfig
'        Inherits ShellEditPanel.PanelConfig

'        Dim _size As Size

'        Public Property Size() As Size
'            Get
'                Return _size
'            End Get
'            Set(ByVal value As Size)
'                _size = value
'            End Set
'        End Property

'        Dim _location As Point

'        Public Property Location() As Point
'            Get
'                Return _location
'            End Get
'            Set(ByVal value As Point)
'                _location = value
'            End Set
'        End Property

'        Dim _opacity As Double

'        Public Property Opacity() As Double
'            Get
'                Return _opacity
'            End Get
'            Set(ByVal value As Double)
'                If value > 1 Or value < 0 Then Throw New Exception("The edgebar's opacity must be from 0 to 1.")
'                _opacity = value
'            End Set
'        End Property

'        Dim _topmost As Boolean

'        Public Property Topmost() As Boolean
'            Get
'                Return _topmost
'            End Get
'            Set(ByVal value As Boolean)
'                _topmost = value
'            End Set
'        End Property

'        Dim _border As FormBorderStyle

'        Public Property Border() As FormBorderStyle
'            Get
'                Return _border
'            End Get
'            Set(ByVal value As FormBorderStyle)
'                _border = value
'            End Set
'        End Property

'    End Class

'    Dim _opacity As Double = 1

'    Public Property Opacity() As Double
'        Get
'            Return _opacity
'        End Get
'        Set(ByVal value As Double)
'            _opacity = value
'        End Set
'    End Property

'    Dim _topmost As Boolean = True

'    Public Property Topmost() As Boolean
'        Get
'            Return _topmost
'        End Get
'        Set(ByVal value As Boolean)
'            _topmost = value
'        End Set
'    End Property

'    Dim _border As FormBorderStyle

'    Public Property Border() As FormBorderStyle
'        Get
'            Return _border
'        End Get
'        Set(ByVal value As FormBorderStyle)
'            _border = value
'        End Set
'    End Property

'    Public Overrides Sub StartSave(ByVal xw As System.Xml.XmlTextWriter)
'        xw.WriteStartElement("Panel")
'        xw.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
'        xw.WriteAttributeString("xsi:type", "FloatingWin")
'        xw.WriteAttributeString("X", Me.Left)
'        xw.WriteAttributeString("Y", Me.Top)
'        xw.WriteAttributeString("Width", Me.Width)
'        xw.WriteAttributeString("Height", Me.Height)
'        xw.WriteAttributeString("Topmost", Me.topmost)
'        xw.WriteAttributeString("Opacity", Me.Opacity.ToString)
'    End Sub

'End Class

'Public Class EdgebarPanel
'    Inherits ShellEditPanel

'    Public Overrides Property Dock() As System.Windows.Forms.DockStyle
'        Get
'            Return MyBase.Dock
'        End Get
'        Set(ByVal value As System.Windows.Forms.DockStyle)
'            If value <> DockStyle.None Then MyBase.Dock = value
'        End Set
'    End Property

'    Dim _opacity As Double = 1

'    Public Property Opacity() As Double
'        Get
'            Return _opacity
'        End Get
'        Set(ByVal value As Double)
'            _opacity = value
'        End Set
'    End Property

'    Public Overrides Function GetName() As String
'        Return "Edgebar"
'    End Function

'    Public Overrides Property Config() As ShellEditPanel.PanelConfig
'        Get
'            Dim ebc As New EdgebarPanelConfig
'            ebc.Dock = Me.Dock
'            ebc.Size = IIf(Me.Dock = DockStyle.Bottom Or Me.Dock = DockStyle.Top, Me.Height, Me.Width)
'            ebc.Opacity = Me.Opacity
'            Return ebc
'        End Get
'        Set(ByVal value As ShellEditPanel.PanelConfig)
'            Me.Dock = CType(value, EdgebarPanelConfig).Dock
'            If Me.Dock = DockStyle.Bottom Or Me.Dock = DockStyle.Top Then
'                Me.Height = CType(value, EdgebarPanelConfig).Size
'            Else
'                Me.Width = CType(value, EdgebarPanelConfig).Size
'            End If
'            Opacity = CType(value, EdgebarPanelConfig).Opacity
'        End Set
'    End Property

'    Public Class EdgebarPanelConfig
'        Inherits ShellEditPanel.PanelConfig

'        Dim _dock As DockStyle

'        Public Property Dock() As DockStyle
'            Get
'                Return _dock
'            End Get
'            Set(ByVal value As DockStyle)
'                If value = DockStyle.None Then Throw New Exception("The edgebar must be docked.")
'                _dock = value
'            End Set
'        End Property

'        Dim _size As Integer

'        Public Property Size() As Integer
'            Get
'                Return _size
'            End Get
'            Set(ByVal value As Integer)
'                If value <= 0 Then Throw New Exception("The edgebar's size must be greater than zero.")
'                _size = value
'            End Set
'        End Property

'        Dim _opacity As Double

'        Public Property Opacity() As Double
'            Get
'                Return _opacity
'            End Get
'            Set(ByVal value As Double)
'                If value > 1 Or value < 0 Then Throw New Exception("The edgebar's opacity must be from 0 to 1.")
'                _opacity = value
'            End Set
'        End Property

'    End Class

'    Public Overrides Sub StartSave(ByVal xw As System.Xml.XmlTextWriter)
'        xw.WriteStartElement("Panel")
'        xw.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
'        xw.WriteAttributeString("xsi:type", "Edgebar")
'        xw.WriteAttributeString("Edge", Me.Dock.ToString)
'        xw.WriteAttributeString("Size", IIf(Me.Dock = DockStyle.Bottom Or Me.Dock = DockStyle.Top, Me.Height, Me.Width))
'        xw.WriteAttributeString("Opacity", Me.Opacity.ToString)
'    End Sub

'End Class

'Public Class ShellEditControl
'    Inherits Control

'    Dim _t As Type

'    Public Sub New(ByVal t As Type)
'        _t = t
'        _testControl = _t.GetConstructor(New Type() {}).Invoke(New Object() {})
'    End Sub

'    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
'        MyBase.OnPaint(e)
'        Dim s As New StringFormat
'        s.Alignment = StringAlignment.Center
'        s.LineAlignment = StringAlignment.Center
'        'e.Graphics.DrawString(_t.Name, SystemFonts.DefaultFont, Brushes.Black, New Rectangle(0, 0, Me.Width, Me.Height), s)
'        CType(_testControl, Control).Size = Me.Size
'        Dim b As New Bitmap(Me.Width, Me.Height)
'        CType(_testControl, Control).DrawToBitmap(b, New Rectangle(0, 0, b.Width, b.Height))
'        e.Graphics.DrawImage(b, 0, 0)
'        Dim p As New Pen(Color.Black)
'        If Not Selected Then p.DashStyle = Drawing2D.DashStyle.Dash
'        e.Graphics.DrawRectangle(p, New Rectangle(0, 0, Me.Width - 1, Me.Height - 1))
'    End Sub

'    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
'        MyBase.OnMouseClick(e)
'        MainForm.SelectControl(Me)
'        If e.Button = Windows.Forms.MouseButtons.Right Then
'            MainForm.mnuControl.Show(Me, e.Location)
'        End If
'    End Sub

'    Dim _testControl As ZShell.IShellControl

'    Public Property Config() As ZShell.ShellControlConfig
'        Get
'            Dim c As ZShell.ShellControlConfig = _testControl.Config
'            c.Location = Me.Location
'            c.Dock = Me.Dock
'            c.Size = Me.Size
'            c.Anchor = Me.Anchor
'            Return c
'        End Get
'        Set(ByVal value As ZShell.ShellControlConfig)
'            _testControl.Config = value
'            Me.Location = value.Location
'            Me.Size = value.Size
'            Me.Anchor = value.Anchor
'            Me.Dock = value.Dock
'            Me.Refresh()
'        End Set
'    End Property

'    Public ReadOnly Property ShellControlType() As Type
'        Get
'            Return _t
'        End Get
'    End Property

'    Dim _selected As Boolean

'    Public Property Selected() As Boolean
'        Get
'            Return _selected
'        End Get
'        Set(ByVal value As Boolean)
'            _selected = value
'            Refresh()
'        End Set
'    End Property

'End Class