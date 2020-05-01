Imports System.Windows.Forms.VisualStyles, System.ComponentModel

Public Class TaskBarControl
    Inherits Control
    Implements IShellControl

    Private Declare Auto Function TrackPopupMenuEx Lib "user32.dll" (ByVal hMenu As IntPtr, ByVal uFlags As Integer, ByVal x As Integer, ByVal y As Integer, ByVal hWnd As IntPtr, ByVal prcRect As IntPtr) As Integer
    Private Declare Auto Function GetSystemMenu Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal bRevert As Boolean) As IntPtr
    Dim tltTip As New ToolTip
    WithEvents tmrAnim As New Timer

    Dim _taskItems As New ObjectModel.Collection(Of TaskItem)
    Dim _activeWindow As WindowInfo

    Public Sub New()
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.AllPaintingInWmPaint, True)
        tltTip.ShowAlways = True

        If Not Me.DesignMode Then
            For Each w As WindowInfo In WindowInfo.GetWindows
                Try
                    If w.ShowInTaskbar Then _taskItems.Add(New TaskItem(w))
                Catch ex As Exception
                    Debug.Print("Error creating taskbar icon. " & ex.Message)
                End Try
            Next
            Refresh()
        End If

        Me.MinimumTaskSize = New Size(100, 24)
        Me.MaximumTaskSize = New Size(150, 24)
        Me.ShowIcon = True
        Me.ShowText = True

        If Not Me.DesignMode Then
            AddHandler ShellEvents.WindowCreated, AddressOf AddWindowIcon
            AddHandler ShellEvents.WindowActivated, AddressOf ActivateWindowIcon
            AddHandler ShellEvents.WindowDestroyed, AddressOf DestroyWindow
            AddHandler ShellEvents.WindowReplaced, AddressOf WindowReplaced
            AddHandler ShellEvents.GetMinimizedRect, AddressOf MinimizeWindow
            AddHandler ShellEvents.WindowTitleChange, AddressOf WindowTitleChanged
        End If
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
        RemoveHandler ShellEvents.WindowCreated, AddressOf AddWindowIcon
        RemoveHandler ShellEvents.WindowActivated, AddressOf ActivateWindowIcon
        RemoveHandler ShellEvents.WindowDestroyed, AddressOf DestroyWindow
        RemoveHandler ShellEvents.WindowReplaced, AddressOf WindowReplaced
        RemoveHandler ShellEvents.GetMinimizedRect, AddressOf MinimizeWindow
        RemoveHandler ShellEvents.WindowTitleChange, AddressOf WindowTitleChanged
    End Sub

    <Browsable(False)> _
    Public Property Config() As ShellControlConfig Implements IShellControl.Config
        Get
            Return ShellControlHelper.GetConfig(Of TaskBarConfig)(Me)
        End Get
        Set(ByVal value As ShellControlConfig)
            ShellControlHelper.SetConfig(Me, value)
        End Set
    End Property

    Dim _updating As Boolean

    Public Sub BeginUpdate() Implements IShellControl.BeginUpdate
        _updating = True
    End Sub

    Public Sub EndUpdate() Implements IShellControl.EndUpdate
        _updating = False
    End Sub

    Public Overrides Sub Refresh() Implements IShellControl.Refresh
        If Not _updating Then MyBase.Refresh()
    End Sub

    Public Sub RefreshItems()
        _taskItems.Clear()
        For Each w As WindowInfo In WindowInfo.GetWindows
            Try
                If w.ShowInTaskbar Then _taskItems.Add(New TaskItem(w))
            Catch ex As Exception
                Debug.Print("Error creating taskbar icon. " & ex.Message)
            End Try
        Next
        Refresh()
    End Sub

    Public Overrides Function ToString() As String Implements IShellControl.ToString
        Return "Task Bar"
    End Function

#Region "Overriden Events"

    Protected Overrides Sub OnDragDrop(ByVal drgevent As System.Windows.Forms.DragEventArgs)
        MyBase.OnDragDrop(drgevent)
        MessageBox.Show("You cannot drag things into the taskbar. However, you can drag things to windows on the taskbar by dragging over the window's taskbar button to activate it.", "Z-Shell", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Protected Overrides Sub OnDragEnter(ByVal drgevent As System.Windows.Forms.DragEventArgs)
        MyBase.OnDragEnter(drgevent)
        OnDragOver(drgevent)
    End Sub

    Protected Overrides Sub OnDragOver(ByVal drgevent As System.Windows.Forms.DragEventArgs)
        MyBase.OnDragOver(drgevent)

        Dim clientPoint As New Point(drgevent.X, drgevent.Y)
        clientPoint = Me.PointToClient(clientPoint)
        Dim selectedItem As TaskItem = GetItemFromPoint(clientPoint)
        If selectedItem IsNot Nothing Then
            If WindowInfo.IsValid(selectedItem.WinHandle) Then
                Dim wi As New WindowInfo(selectedItem.WinHandle)
                Try
                    'wi.SwitchTo(False)
                    wi.BringToFront()
                    If wi.Minimized Then
                        wi.SetWindowState(WindowInfo.WindowState.Restore)
                    End If
                Catch ex As Exception
                    Debug.Print(ex.Message)
                End Try
            End If
        End If
    End Sub

    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        StartMenu.FadeOut()

        'Was a scroll button click?
        If Me.Width > Me.Height Then
            If _currentRow > 0 Then
                If New Rectangle(Me.Width - SystemInformation.VerticalScrollBarWidth, 0, SystemInformation.VerticalScrollBarWidth, SystemInformation.VerticalScrollBarArrowHeight).Contains(e.Location) Then
                    _currentRow -= 1
                    Refresh()
                    Exit Sub
                End If
            End If
            If _currentRow < _rows - 1 Then
                If New Rectangle(Me.Width - SystemInformation.VerticalScrollBarWidth, Me.Height - SystemInformation.VerticalScrollBarArrowHeight, SystemInformation.VerticalScrollBarWidth, SystemInformation.VerticalScrollBarArrowHeight).Contains(e.Location) Then
                    _currentRow += 1
                    Refresh()
                    Exit Sub
                End If
            End If
        Else
            If _currentRow > 0 Then
                If New Rectangle(0, Me.Height - SystemInformation.HorizontalScrollBarArrowWidth, SystemInformation.HorizontalScrollBarArrowWidth, SystemInformation.HorizontalScrollBarHeight).Contains(e.Location) Then
                    _currentRow -= 1
                    Refresh()
                    Exit Sub
                End If
            End If
            If _currentRow < _rows - 1 Then
                If New Rectangle(Me.Width - SystemInformation.HorizontalScrollBarArrowWidth, Me.Height - SystemInformation.HorizontalScrollBarHeight, SystemInformation.HorizontalScrollBarArrowWidth, SystemInformation.HorizontalScrollBarHeight).Contains(e.Location) Then
                    _currentRow += 1
                    Refresh()
                    Exit Sub
                End If
            End If
        End If

        'Was a task item clicked?
        Dim selectedItem As TaskItem = GetItemFromPoint(e.Location)
        If selectedItem IsNot Nothing Then
            If WindowInfo.IsValid(selectedItem.WinHandle) Then
                Dim wi As New WindowInfo(selectedItem.WinHandle)
                If e.Button = Windows.Forms.MouseButtons.Left Then
                    'Switch to the window
                    wi.SwitchTo(False)
                    wi.BringToFront()
                    If wi.Minimized Then
                        wi.SetWindowState(WindowInfo.WindowState.Restore)
                    ElseIf wi = _activeWindow Then
                        wi.SetWindowState(WindowInfo.WindowState.Minimize)
                    End If
                ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso GetSystemMenu(wi.Handle, False) <> IntPtr.Zero Then
                    'Show the system menu associated with the window
                    Dim ret As Integer = TrackPopupMenuEx(GetSystemMenu(wi.Handle, False), 2 Or &H100, PointToScreen(e.Location).X, PointToScreen(e.Location).Y, Me.FindForm.Handle, 0)
                    If ret <> 0 Then wi.PostMessage(&H112, ret, 0) 'WM_SYSCOMMAND
                End If
            Else
                _taskItems.Remove(selectedItem)
            End If
        End If
        Refresh()
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        Refresh()
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        If GetItemFromPoint(e.Location) IsNot selectedItem Then
            selectedItem = GetItemFromPoint(e.Location)
            If selectedItem IsNot Nothing Then
                tltTip.SetToolTip(Me, selectedItem.Text)
            Else
                tltTip.SetToolTip(Me, "")
            End If
        End If
        Refresh()
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        Refresh()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        ArrangeItems()

        Dim b As New Bitmap(Me.Width, Me.Height, Imaging.PixelFormat.Format32bppArgb)
        Dim gr As Graphics = Graphics.FromImage(b)
        gr.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
        gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        Me.Background.Render(gr, New Rectangle(0, 0, Me.Width, Me.Height))

        For Each i As TaskItem In _taskItems
            Try
                If IsItemInRow(i, _currentRow) Then
                    'Figure out which state to draw the button in
                    Dim state As StatesRenderCollection.RenderStateEnum = StatesRenderCollection.RenderStateEnum.Normal
                    If i.Flashing Then
                        Me.TaskButtonFlash.Render(gr, i.Rectangle)
                    Else
                        If i.Rectangle.Contains(PointToClient(Control.MousePosition)) Then
                            state = StatesRenderCollection.RenderStateEnum.Hot
                            If Control.MouseButtons = Windows.Forms.MouseButtons.Left Then
                                state = StatesRenderCollection.RenderStateEnum.Down
                            End If
                        End If
                        IIf(i.Activated, Me.TaskButtonActivated, Me.TaskButton).RenderState(gr, i.Rectangle, state)
                    End If

                    'Now draw it
                    Dim selectedImage As Image = Nothing
                    If GetSystemMenu(i.WinHandle, False) <> IntPtr.Zero Then
                        selectedImage = IIf(LargeIcons, i.BigIcon, i.SmallIcon)
                    End If
                    Dim imageRect As New Rectangle(i.Rectangle.X + 3, i.Rectangle.Y + 3, 0, 0)
                    Dim textRect As Rectangle
                    If Me.ShowIcon AndAlso selectedImage IsNot Nothing Then
                        imageRect = New Rectangle(i.Rectangle.X + 3, i.Rectangle.Y + i.Rectangle.Height / 2 - IIf(LargeIcons, 32, 16) / 2, IIf(LargeIcons, 32, 16), IIf(LargeIcons, 32, 16))
                        gr.DrawImage(selectedImage, imageRect)
                    End If
                    If ShowText Then
                        textRect = New Rectangle(imageRect.Right + 3, i.Rectangle.Y, i.Rectangle.Right - imageRect.Right - 6, i.Rectangle.Height)
                        If textRect.Width > 0 Then
                            Dim txtformat As New StringFormat(StringFormatFlags.NoWrap Or StringFormatFlags.DisplayFormatControl)
                            txtformat.Alignment = StringAlignment.Near
                            txtformat.LineAlignment = StringAlignment.Center
                            txtformat.Trimming = StringTrimming.EllipsisCharacter
                            Dim txtColor As Color
                            If i.Flashing Then
                                txtColor = Me.TaskButtonFlashTextColor
                            ElseIf i.Activated Then
                                txtColor = Me.TaskButtonActivatedTextColors.GetState(state)
                            Else
                                txtColor = Me.TaskButtonTextColors.GetState(state)
                            End If
                            gr.DrawString(i.Text, Me.TaskButtonTextFont.ToFont, New SolidBrush(txtColor), textRect, txtformat)
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.Print("TaskBarControl.OnPaint Error: " & ex.Message)
            End Try
        Next

        'Draw any scroll buttons
        If _rows > 1 Then
            If Me.Width > Me.Height Then
                If _currentRow > 0 Then
                    Dim buttonRect As New Rectangle(Me.Width - SystemInformation.VerticalScrollBarWidth, 0, SystemInformation.VerticalScrollBarWidth, SystemInformation.VerticalScrollBarArrowHeight)
                    Dim state As StatesRenderCollection.RenderStateEnum = StatesRenderCollection.RenderStateEnum.Normal
                    If buttonRect.Contains(PointToClient(Control.MousePosition)) Then
                        state = StatesRenderCollection.RenderStateEnum.Hot
                        If Control.MouseButtons = Windows.Forms.MouseButtons.Left Then
                            state = StatesRenderCollection.RenderStateEnum.Down
                        End If
                    End If
                    Me.ScrollUp.RenderState(gr, buttonRect, state)
                End If
                If _currentRow < _rows - 1 Then
                    Dim buttonRect As New Rectangle(Me.Width - SystemInformation.VerticalScrollBarWidth, Me.Height - SystemInformation.VerticalScrollBarArrowHeight, SystemInformation.VerticalScrollBarWidth, SystemInformation.VerticalScrollBarArrowHeight)
                    Dim state As StatesRenderCollection.RenderStateEnum = StatesRenderCollection.RenderStateEnum.Normal
                    If buttonRect.Contains(PointToClient(Control.MousePosition)) Then
                        state = StatesRenderCollection.RenderStateEnum.Hot
                        If Control.MouseButtons = Windows.Forms.MouseButtons.Left Then
                            state = StatesRenderCollection.RenderStateEnum.Down
                        End If
                    End If
                    Me.ScrollDown.RenderState(gr, buttonRect, state)
                End If
            Else
                If _currentRow > 0 Then
                    Dim buttonRect As New Rectangle(0, Me.Height - SystemInformation.HorizontalScrollBarArrowWidth, SystemInformation.HorizontalScrollBarArrowWidth, SystemInformation.HorizontalScrollBarHeight)
                    Dim state As StatesRenderCollection.RenderStateEnum = StatesRenderCollection.RenderStateEnum.Normal
                    If buttonRect.Contains(PointToClient(Control.MousePosition)) Then
                        state = StatesRenderCollection.RenderStateEnum.Hot
                        If Control.MouseButtons = Windows.Forms.MouseButtons.Left Then
                            state = StatesRenderCollection.RenderStateEnum.Down
                        End If
                    End If
                    Me.ScrollLeft.RenderState(gr, buttonRect, state)
                End If
                If _currentRow < _rows - 1 Then
                    Dim buttonRect As New Rectangle(Me.Width - SystemInformation.HorizontalScrollBarArrowWidth, Me.Height - SystemInformation.HorizontalScrollBarHeight, SystemInformation.HorizontalScrollBarArrowWidth, SystemInformation.HorizontalScrollBarHeight)
                    Dim state As StatesRenderCollection.RenderStateEnum = StatesRenderCollection.RenderStateEnum.Normal
                    If buttonRect.Contains(PointToClient(Control.MousePosition)) Then
                        state = StatesRenderCollection.RenderStateEnum.Hot
                        If Control.MouseButtons = Windows.Forms.MouseButtons.Left Then
                            state = StatesRenderCollection.RenderStateEnum.Down
                        End If
                    End If
                    Me.ScrollRight.RenderState(gr, buttonRect, state)
                End If
            End If
        End If
        gr.Dispose()

        mask = b
        e.Graphics.DrawImage(b, New Point(0, 0))
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        MyBase.OnMouseLeave(e)
        Me.Refresh()
    End Sub

#End Region

#Region "Properties"

    Dim _minSize As New Size(100, 24)

    <System.ComponentModel.DefaultValue(GetType(Size), "100,24")> Public Property MinimumTaskSize() As Size
        Get
            Return _minSize
        End Get
        Set(ByVal value As Size)
            _minSize = value
            Refresh()
        End Set
    End Property

    Dim _maxSize As New Size(150, 24)

    <System.ComponentModel.DefaultValue(GetType(Size), "150,24")> Public Property MaximumTaskSize() As Size
        Get
            Return _maxSize
        End Get
        Set(ByVal value As Size)
            _maxSize = value
            Refresh()
        End Set
    End Property

    Dim _showIcon As Boolean = True

    <System.ComponentModel.DefaultValue(True)> Public Property ShowIcon() As Boolean
        Get
            Return _showIcon
        End Get
        Set(ByVal value As Boolean)
            _showIcon = value
            Refresh()
        End Set
    End Property

    Dim _showText As Boolean = True

    <System.ComponentModel.DefaultValue(True)> Public Property ShowText() As Boolean
        Get
            Return _showText
        End Get
        Set(ByVal value As Boolean)
            _showText = value
            Refresh()
        End Set
    End Property

    Dim _horizSpacing As Integer = 3

    <System.ComponentModel.DefaultValue(3)> Public Property HorizontalSpacing() As Integer
        Get
            Return _horizSpacing
        End Get
        Set(ByVal value As Integer)
            _horizSpacing = value
            Refresh()
        End Set
    End Property

    Dim _vertSpacing As Integer = 3

    <System.ComponentModel.DefaultValue(3)> Public Property VerticalSpacing() As Integer
        Get
            Return _vertSpacing
        End Get
        Set(ByVal value As Integer)
            _vertSpacing = value
            Refresh()
        End Set
    End Property

    Dim _largeIcons As Boolean = False

    <System.ComponentModel.DefaultValue(False)> Public Property LargeIcons() As Boolean
        Get
            Return _largeIcons
        End Get
        Set(ByVal value As Boolean)
            _largeIcons = value
            Refresh()
        End Set
    End Property

    Dim _animspeed As Integer = 5

    <System.ComponentModel.DefaultValue(5)> Public Property AnimationSpeed() As Integer
        Get
            Return _animspeed
        End Get
        Set(ByVal value As Integer)
            If value <= 0 Then Throw New Exception("The animation speed cannot be less than or equal to zero.")
            _animspeed = value
        End Set
    End Property

    Dim _bck As ShellRenderer = New VisualStyleRenderer(VisualStyles.VisualStyleElement.Taskbar.BackgroundBottom.Normal)

    Public Property Background() As ShellRenderer Implements IShellControl.Background
        Get
            Return _bck
        End Get
        Set(ByVal value As ShellRenderer)
            _bck = value
        End Set
    End Property

    Dim _btn As New StatesRenderCollection(New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal), New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Hot), New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed))

    Public Property TaskButton() As StatesRenderCollection
        Get
            Return _btn
        End Get
        Set(ByVal value As StatesRenderCollection)
            _btn = value
        End Set
    End Property

    Dim _btnact As New StatesRenderCollection(New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed), New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed), New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed))

    Public Property TaskButtonActivated() As StatesRenderCollection
        Get
            Return _btnact
        End Get
        Set(ByVal value As StatesRenderCollection)
            _btnact = value
        End Set
    End Property

    Dim _btnflash As ShellRenderer = New VisualStyleRenderer(VisualStyles.VisualStyleElement.TaskBand.FlashButton.Normal)

    Public Property TaskButtonFlash() As ShellRenderer
        Get
            Return _btnflash
        End Get
        Set(ByVal value As ShellRenderer)
            _btnflash = value
        End Set
    End Property

    Dim _btnTxtColors As New StatesColorCollection(Color.Black, Color.Black, Color.Black)

    Public Property TaskButtonTextColors() As StatesColorCollection
        Get
            Return _btnTxtColors
        End Get
        Set(ByVal value As StatesColorCollection)
            _btnTxtColors = value
        End Set
    End Property

    Dim _btnActTxtColors As New StatesColorCollection(Color.Black, Color.Black, Color.Black)

    Public Property TaskButtonActivatedTextColors() As StatesColorCollection
        Get
            Return _btnActTxtColors
        End Get
        Set(ByVal value As StatesColorCollection)
            _btnActTxtColors = value
        End Set
    End Property

    Dim _btnFlashTxtColor As Color = Color.Red

    Public Property TaskButtonFlashTextColor() As Color
        Get
            Return _btnFlashTxtColor
        End Get
        Set(ByVal value As Color)
            _btnFlashTxtColor = value
        End Set
    End Property

    Dim _btnTxtFont As Font = ZShell.Font.FromFont(SystemFonts.IconTitleFont)

    Public Property TaskButtonTextFont() As Font
        Get
            Return _btnTxtFont
        End Get
        Set(ByVal value As Font)
            _btnTxtFont = value
        End Set
    End Property

    Private _scrollup As New StatesRenderCollection(New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.UpNormal), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.UpHot), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.UpPressed))

    Public Property ScrollUp() As StatesRenderCollection
        Get
            Return _scrollup
        End Get
        Set(ByVal value As StatesRenderCollection)
            _scrollup = value
        End Set
    End Property

    Private _scrolldown As New StatesRenderCollection(New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.DownNormal), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.DownHot), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.DownPressed))

    Public Property ScrollDown() As StatesRenderCollection
        Get
            Return _scrolldown
        End Get
        Set(ByVal value As StatesRenderCollection)
            _scrolldown = value
        End Set
    End Property

    Private _scrollleft As New StatesRenderCollection(New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.LeftNormal), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.LeftHot), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.LeftPressed))

    Public Property ScrollLeft() As StatesRenderCollection
        Get
            Return _scrollleft
        End Get
        Set(ByVal value As StatesRenderCollection)
            _scrollleft = value
        End Set
    End Property

    Private _scrollright As New StatesRenderCollection(New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.RightNormal), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.RightHot), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.RightPressed))

    Public Property ScrollRight() As StatesRenderCollection
        Get
            Return _scrollright
        End Get
        Set(ByVal value As StatesRenderCollection)
            _scrollright = value
        End Set
    End Property

#End Region

#Region "Shell Events"

    Private Delegate Sub AddWindowIconDelegate(ByVal window As WindowInfo)

    Private Sub AddWindowIcon(ByVal window As WindowInfo)
        If Me.InvokeRequired Then
            BeginInvoke(New AddWindowIconDelegate(AddressOf AddWindowIcon), New Object() {window})
        Else
            For Each i As TaskItem In _taskItems
                If i.WinHandle = window.Handle Then Exit Sub
            Next
            Dim tbi As New TaskItem(window)
            _taskItems.Add(tbi)
            Refresh()
        End If
    End Sub

    Private Delegate Sub WindowTitleChangedDelegate(ByVal window As WindowInfo, ByVal flash As Boolean)

    Private Sub WindowTitleChanged(ByVal window As WindowInfo, ByVal flash As Boolean)
        If Me.InvokeRequired Then
            BeginInvoke(New WindowTitleChangedDelegate(AddressOf WindowTitleChanged), New Object() {window, flash})
        Else
            For Each i As TaskItem In _taskItems
                If i.WinHandle = window.Handle Then
                    i.RefreshInfo(window)
                    i.Flashing = flash
                End If
            Next
            Refresh()
        End If
    End Sub

    Private Delegate Sub WindowReplacedDelegate(ByVal oldWindows As WindowInfo, ByVal newWindow As WindowInfo)

    Private Sub WindowReplaced(ByVal oldWindow As WindowInfo, ByVal newWindow As WindowInfo)
        If Me.InvokeRequired Then
            BeginInvoke(New WindowReplacedDelegate(AddressOf WindowReplaced), New Object() {oldWindow, newWindow})
        Else
            For Each i As TaskItem In _taskItems
                If i.WinHandle = oldWindow.Handle Then
                    i.RefreshInfo(newWindow)
                    i.Flashing = False
                End If
            Next
            Refresh()
        End If
    End Sub

    Private Delegate Sub ActivateWindowIconDelegate(ByVal window As WindowInfo, ByVal fullscreen As Boolean)

    Private Sub ActivateWindowIcon(ByVal window As WindowInfo, ByVal fullscreen As Boolean)
        If Me.InvokeRequired Then
            BeginInvoke(New ActivateWindowIconDelegate(AddressOf ActivateWindowIcon), New Object() {window, fullscreen})
        Else
            StartMenu.FadeOut()
            For Each i As TaskItem In _taskItems
                i.Activated = (i.WinHandle = window.Handle)
                If i.WinHandle = window.Handle Then
                    _activeWindow = window
                End If
            Next
            Refresh()
        End If
    End Sub

    Private Delegate Sub DestroyWindowDelegate(ByVal windowHandle As IntPtr)

    Private Sub DestroyWindow(ByVal windowHandle As IntPtr)
        If Me.InvokeRequired Then
            BeginInvoke(New DestroyWindowDelegate(AddressOf DestroyWindow), New Object() {windowHandle})
        Else
            Dim deli As TaskItem = Nothing
            For Each i As TaskItem In _taskItems
                If i.WinHandle = windowHandle Then
                    deli = i
                    Exit For
                End If
            Next
            If deli IsNot Nothing Then
                _taskItems.Remove(deli)
            End If
            Refresh()
        End If
    End Sub

    Private Delegate Sub MinimizeWindowDelegate(ByVal w As WindowInfo, ByRef r As Rectangle)

    Private Sub MinimizeWindow(ByVal w As WindowInfo, ByRef r As Rectangle)
        If Me.InvokeRequired Then
            BeginInvoke(New MinimizeWindowDelegate(AddressOf MinimizeWindow), New Object() {w, r})
        Else
            For Each c As TaskItem In _taskItems
                If c.WinHandle = w.Handle Then
                    'r.Location = Me.PointToScreen(c.Rectangle.Location)
                    'r.Size = c.Rectangle.Size
                    'Debug.Print("Minimize rectangle changed! " & c.Text)
                End If
            Next
            Refresh()
        End If
    End Sub

#End Region

#Region "Private Stuff"

    Dim selectedItem As TaskItem
    Dim _rows As Integer
    Dim _maxItemsPerRow As Integer
    Dim _currentRow As Integer

    Private Sub ArrangeItems()
        'First, calculate the size and number of rows, etc
        Dim areaSize As New Size(Me.Width - 2 * HorizontalSpacing, Me.Height - 2 * VerticalSpacing)
        If Me.Width > Me.Height Then
            areaSize.Width -= SystemInformation.VerticalScrollBarWidth
        Else
            areaSize.Height -= SystemInformation.HorizontalScrollBarHeight
        End If
        Dim currentTaskSize As Size
        If Me.Width > Me.Height Then
            For i As Integer = MaximumTaskSize.Width To MinimumTaskSize.Width Step -1
                If i = MinimumTaskSize.Width Then
                    currentTaskSize.Width = i
                    _maxItemsPerRow = Math.Floor(areaSize.Width / (HorizontalSpacing + i))
                    Exit For
                Else
                    If areaSize.Width / (HorizontalSpacing + i) >= _taskItems.Count Then
                        currentTaskSize.Width = i
                        _maxItemsPerRow = Math.Floor(areaSize.Width / (HorizontalSpacing + i))
                        Exit For
                    End If
                End If
            Next
            currentTaskSize.Height = areaSize.Height 'Math.Min(areaSize.Height, MaximumTaskSize.Height)
        Else
            For i As Integer = MaximumTaskSize.Height To MinimumTaskSize.Height Step -1
                If i = MinimumTaskSize.Height Then
                    currentTaskSize.Height = i
                    _maxItemsPerRow = Math.Floor(areaSize.Height / (VerticalSpacing + i))
                    Exit For
                Else
                    If areaSize.Height / (VerticalSpacing + i) >= _taskItems.Count Then
                        currentTaskSize.Height = i
                        _maxItemsPerRow = Math.Floor(areaSize.Height / (VerticalSpacing + i))
                        Exit For
                    End If
                End If
            Next
            currentTaskSize.Width = areaSize.Width 'Math.Min(areaSize.Width, MinimumTaskSize.Width)
        End If

        _rows = Math.Ceiling(_taskItems.Count / Math.Max(1, _maxItemsPerRow))
        If _currentRow > _rows - 1 Then _currentRow = _rows - 1

        'Now loop through each item setting its rectangle
        Dim currentX As Integer = HorizontalSpacing
        Dim currentY As Integer = VerticalSpacing
        Dim needsAnim As Boolean = False
        For Each i As TaskItem In _taskItems
            If IsItemInRow(i, _currentRow) Then
                If i.Rectangle.X - currentX >= AnimationSpeed Then
                    i.Rectangle = New Rectangle(i.Rectangle.X - AnimationSpeed, currentY, i.Rectangle.Width, i.Rectangle.Height)
                    needsAnim = True
                Else
                    i.Rectangle = New Rectangle(currentX, currentY, i.Rectangle.Width, i.Rectangle.Height)
                End If

                If Me.Width > Me.Height Then
                    If i.Rectangle.Width - currentTaskSize.Width <= -AnimationSpeed Then
                        i.Rectangle = New Rectangle(i.Rectangle.X, i.Rectangle.Y, i.Rectangle.Width + AnimationSpeed, currentTaskSize.Height)
                        needsAnim = True
                    ElseIf i.Rectangle.Width - currentTaskSize.Width >= AnimationSpeed Then
                        i.Rectangle = New Rectangle(i.Rectangle.X, i.Rectangle.Y, i.Rectangle.Width - AnimationSpeed, currentTaskSize.Height)
                        needsAnim = True
                    Else
                        i.Rectangle = New Rectangle(i.Rectangle.X, i.Rectangle.Y, currentTaskSize.Width, currentTaskSize.Height)
                    End If

                    currentX = i.Rectangle.X + i.Rectangle.Width + HorizontalSpacing
                Else
                    If i.Rectangle.Y - currentY >= AnimationSpeed Then
                        i.Rectangle = New Rectangle(currentX, i.Rectangle.Y - AnimationSpeed, i.Rectangle.Width, i.Rectangle.Height)
                        needsAnim = True
                    Else
                        i.Rectangle = New Rectangle(currentX, currentY, i.Rectangle.Width, i.Rectangle.Height)
                    End If

                    If i.Rectangle.Height - currentTaskSize.Height <= -AnimationSpeed Then
                        i.Rectangle = New Rectangle(i.Rectangle.X, i.Rectangle.Y, currentTaskSize.Width, i.Rectangle.Height + AnimationSpeed)
                        needsAnim = True
                    ElseIf i.Rectangle.Height - currentTaskSize.Height >= AnimationSpeed Then
                        i.Rectangle = New Rectangle(i.Rectangle.X, i.Rectangle.Y, currentTaskSize.Width, i.Rectangle.Height - AnimationSpeed)
                        needsAnim = True
                    Else
                        i.Rectangle = New Rectangle(i.Rectangle.X, i.Rectangle.Y, currentTaskSize.Width, currentTaskSize.Height)
                    End If

                    currentY = i.Rectangle.Y + i.Rectangle.Height + VerticalSpacing
                End If
            Else
                i.Rectangle = New Rectangle(i.Rectangle.X, i.Rectangle.Y, currentTaskSize.Width, currentTaskSize.Height)
            End If
        Next
        If needsAnim Then
            SetAnimation()
        Else
            tmrAnim.Enabled = False
        End If
    End Sub

    Private Sub SetAnimation()
        If Not tmrAnim.Enabled Then
            tmrAnim.Interval = 10
            tmrAnim.Enabled = True
        End If
    End Sub

    Private Function IsItemInRow(ByVal item As TaskItem, ByVal row As Integer) As Boolean
        Return (Math.Floor(CDbl(_taskItems.IndexOf(item)) / _maxItemsPerRow) = row)
    End Function

    Private Function GetItemFromPoint(ByVal pt As Point) As TaskItem
        For Each i As TaskItem In _taskItems
            If Me.IsItemInRow(i, _currentRow) Then
                If i.Rectangle.Contains(pt) Then
                    Return i
                End If
            End If
        Next
        Return Nothing
    End Function

    Private Class TaskItem

        Dim _bigIcon As Image

        Public Property BigIcon() As Image
            Get
                Return _bigIcon
            End Get
            Set(ByVal value As Image)
                _bigIcon = value
            End Set
        End Property

        Dim _smallIcon As Image

        Public Property SmallIcon() As Image
            Get
                Return _smallIcon
            End Get
            Set(ByVal value As Image)
                _smallIcon = value
            End Set
        End Property

        Dim _text As String

        Public Property Text() As String
            Get
                Return _text
            End Get
            Set(ByVal value As String)
                _text = value
            End Set
        End Property

        Dim _winHandle As IntPtr

        Public Property WinHandle() As IntPtr
            Get
                Return _winHandle
            End Get
            Set(ByVal value As IntPtr)
                _winHandle = value
            End Set
        End Property

        Dim _flashing As Boolean

        Public Property Flashing() As Boolean
            Get
                Return _flashing
            End Get
            Set(ByVal value As Boolean)
                _flashing = value
            End Set
        End Property

        Dim _activated As Boolean

        Public Property Activated() As Boolean
            Get
                Return _activated
            End Get
            Set(ByVal value As Boolean)
                _activated = value
            End Set
        End Property

        Dim _rect As Rectangle

        Public Property Rectangle() As Rectangle
            Get
                Return _rect
            End Get
            Set(ByVal value As Rectangle)
                _rect = value
            End Set
        End Property

        Public Sub New(ByVal window As WindowInfo)
            RefreshInfo(window)
        End Sub

        Public Sub RefreshInfo(ByVal window As WindowInfo)
            Me.Text = window.Text
            Dim bico As Icon = window.Icon(WindowInfo.WindowIconSize.Big)
            If bico IsNot Nothing Then
                Try
                    Me.BigIcon = bico.ToBitmap
                Catch ex As Exception

                End Try
            End If
            Dim sico As Icon = window.Icon(WindowInfo.WindowIconSize.Small2)
            If sico IsNot Nothing Then
                Try
                    Me.SmallIcon = sico.ToBitmap
                Catch ex As Exception

                End Try
            End If
            Me.WinHandle = window.Handle
        End Sub
    End Class

#End Region

    Dim mask As Bitmap

    Public Function GetAlphaMask() As System.Drawing.Bitmap Implements ZPixel.IAlphaPaint.GetAlphaMask
        If mask IsNot Nothing Then
            Return mask
        Else
            Refresh()
            Return GetAlphaMask()
        End If
    End Function

    Private Sub tmrAnim_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrAnim.Tick
        Me.Refresh()
    End Sub

    Dim _ele As VisualStyles.VisualStyleElement

End Class

Public Class TaskBarConfig
    Inherits ShellControlConfig

#Region "Properties"

    Dim _minSize As New Size(100, 24)

    <Description("The smallest size a task button can become. Only the width or height is used depending on the orientation of the taskbar.")> _
    <System.ComponentModel.DefaultValue(GetType(Size), "100,24")> Public Property Minimum_Task_Size() As Size
        Get
            Return _minSize
        End Get
        Set(ByVal value As Size)
            _minSize = value
        End Set
    End Property

    Dim _maxSize As New Size(150, 24)

    <Description("The largest size a task button can become. Only the width or height is used depending on the orientation of the taskbar.")> _
    <System.ComponentModel.DefaultValue(GetType(Size), "150,24")> Public Property Maximum_Task_Size() As Size
        Get
            Return _maxSize
        End Get
        Set(ByVal value As Size)
            _maxSize = value
        End Set
    End Property

    Dim _showIcon As Boolean = True

    <Description("Controls whether each window's icon is displayed on its task button.")> _
    <System.ComponentModel.DefaultValue(True)> Public Property Show_Icon() As Boolean
        Get
            Return _showIcon
        End Get
        Set(ByVal value As Boolean)
            _showIcon = value
        End Set
    End Property

    Dim _showText As Boolean = True

    <Description("Controls whether each window's title is displayed on its task button.")> _
    <System.ComponentModel.DefaultValue(True)> Public Property Show_Text() As Boolean
        Get
            Return _showText
        End Get
        Set(ByVal value As Boolean)
            _showText = value
        End Set
    End Property

    Dim _horizSpacing As Integer = 3

    <Description("The amount of space between task buttons horizontally.")> _
    <System.ComponentModel.DefaultValue(3)> Public Property Horizontal_Spacing() As Integer
        Get
            Return _horizSpacing
        End Get
        Set(ByVal value As Integer)
            _horizSpacing = value
        End Set
    End Property

    Dim _vertSpacing As Integer = 3

    <Description("The amount of space between task buttons vertically.")> _
    <System.ComponentModel.DefaultValue(3)> Public Property Vertical_Spacing() As Integer
        Get
            Return _vertSpacing
        End Get
        Set(ByVal value As Integer)
            _vertSpacing = value
        End Set
    End Property

    Dim _largeIcons As Boolean = False

    <Description("Large icons are those that appear in the alt-tab dialog box. Small ones appear by default.")> _
    <System.ComponentModel.DefaultValue(False)> Public Property Large_Icons() As Boolean
        Get
            Return _largeIcons
        End Get
        Set(ByVal value As Boolean)
            _largeIcons = value
        End Set
    End Property

    Dim _animspeed As Integer = 5

    <Description("The speed of all taskbar animations in pixels per 10 milliseconds.")> _
    <System.ComponentModel.DefaultValue(5)> Public Property Animation_Speed() As Integer
        Get
            Return _animspeed
        End Get
        Set(ByVal value As Integer)
            If value <= 0 Then Throw New Exception("The animation speed cannot be less than or equal to zero.")
            _animspeed = value
        End Set
    End Property

    Dim _btn As New StatesRenderCollection(New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal), New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Hot), New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed))

    <Description("Controls how to render task buttons for each state.")> _
    Public Property Task_Button() As StatesRenderCollection
        Get
            Return _btn
        End Get
        Set(ByVal value As StatesRenderCollection)
            _btn = value
        End Set
    End Property

    Dim _btnact As New StatesRenderCollection(New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed), New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed), New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed))

    <Description("Controls how to render the task button of the active window for each state.")> _
    Public Property Task_Button_Activated() As StatesRenderCollection
        Get
            Return _btnact
        End Get
        Set(ByVal value As StatesRenderCollection)
            _btnact = value
        End Set
    End Property

    Dim _btnflash As ShellRenderer = New VisualStyleRenderer(VisualStyles.VisualStyleElement.TaskBand.FlashButton.Normal)

    <Description("Controls how to render task buttons in a flashing state.")> _
    Public Property Task_Button_Flash() As ShellRenderer
        Get
            Return _btnflash
        End Get
        Set(ByVal value As ShellRenderer)
            _btnflash = value
        End Set
    End Property

    Dim _btnTxtColors As New StatesColorCollection(Color.Black, Color.Black, Color.Black)

    <Description("Controls the color of each task button's text in different button states.")> _
    Public Property Task_Button_Text_Colors() As StatesColorCollection
        Get
            Return _btnTxtColors
        End Get
        Set(ByVal value As StatesColorCollection)
            _btnTxtColors = value
        End Set
    End Property

    Dim _btnFlashTxtColor As Color = Color.Red

    <Description("Controls the color of a task button's text when it is flashing.")> _
    Public Property Task_Button_Flash_Text_Color() As Color
        Get
            Return _btnFlashTxtColor
        End Get
        Set(ByVal value As Color)
            _btnFlashTxtColor = value
        End Set
    End Property

    Dim _btnTxtFont As Font = Font.FromFont(SystemFonts.DefaultFont)

    <Description("The font used for the text on each task button.")> _
    Public Property Task_Button_Text_Font() As Font
        Get
            Return _btnTxtFont
        End Get
        Set(ByVal value As Font)
            _btnTxtFont = value
        End Set
    End Property

    Dim _btnActTxtColors As New StatesColorCollection(Color.Black, Color.Black, Color.Black)

    <Description("Controls the color of the activated task button's text in different button states.")> _
    Public Property Task_Button_Activated_Text_Colors() As StatesColorCollection
        Get
            Return _btnActTxtColors
        End Get
        Set(ByVal value As StatesColorCollection)
            _btnActTxtColors = value
        End Set
    End Property

    Private _scrollup As New StatesRenderCollection(New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.UpNormal), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.UpHot), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.UpPressed))

    <Description("Controls how to render the 'up' scroll button when scrolling through tasks.")> _
    Public Property Scroll_Up() As StatesRenderCollection
        Get
            Return _scrollup
        End Get
        Set(ByVal value As StatesRenderCollection)
            _scrollup = value
        End Set
    End Property

    Private _scrolldown As New StatesRenderCollection(New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.DownNormal), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.DownHot), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.DownPressed))

    <Description("Controls how to render the 'down' scroll button when scrolling through tasks.")> _
    Public Property Scroll_Down() As StatesRenderCollection
        Get
            Return _scrolldown
        End Get
        Set(ByVal value As StatesRenderCollection)
            _scrolldown = value
        End Set
    End Property

    Private _scrollleft As New StatesRenderCollection(New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.LeftNormal), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.LeftHot), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.LeftPressed))

    <Description("Controls how to render the 'left' scroll button when scrolling through tasks.")> _
    Public Property Scroll_Left() As StatesRenderCollection
        Get
            Return _scrollleft
        End Get
        Set(ByVal value As StatesRenderCollection)
            _scrollleft = value
        End Set
    End Property

    Private _scrollright As New StatesRenderCollection(New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.RightNormal), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.RightHot), New VisualStyleRenderer(VisualStyles.VisualStyleElement.ScrollBar.ArrowButton.RightPressed))

    <Description("Controls how to render the 'right' scroll button when scrolling through tasks.")> _
    Public Property Scroll_Right() As StatesRenderCollection
        Get
            Return _scrollright
        End Get
        Set(ByVal value As StatesRenderCollection)
            _scrollright = value
        End Set
    End Property

#End Region

End Class

Public Class TaskBarRenderer

	Public Enum TaskBarLocation
		Top
		Bottom
		Left
		Right
	End Enum

	Public Shared Sub DrawTaskBar(ByVal dest As Graphics, ByVal rect As Rectangle, ByVal location As TaskBarLocation)
		Dim e As VisualStyles.VisualStyleElement
		Select Case location
			Case TaskBarLocation.Bottom
				e = VisualStyles.VisualStyleElement.Taskbar.BackgroundBottom.Normal
			Case TaskBarLocation.Left
				e = VisualStyles.VisualStyleElement.Taskbar.BackgroundLeft.Normal
			Case TaskBarLocation.Right
				e = VisualStyles.VisualStyleElement.Taskbar.BackgroundRight.Normal
			Case TaskBarLocation.Top
				e = VisualStyles.VisualStyleElement.Taskbar.BackgroundTop.Normal
			Case Else
				e = VisualStyles.VisualStyleElement.Taskbar.BackgroundBottom.Normal
		End Select
		If VisualStyles.VisualStyleRenderer.IsElementDefined(e) Then
			Dim vr As New VisualStyles.VisualStyleRenderer(e)
			vr.DrawBackground(dest, rect)
		End If
	End Sub

End Class

Public Class TaskBandRenderer

	Public Enum TaskBandState
		Normal
		Flashing
		Hot
		Down
		DownHot
		DownDown
	End Enum

	Public Shared Sub DrawTaskBand(ByVal dest As Graphics, ByVal rect As Rectangle, ByVal state As TaskBandState)
		If state = TaskBandState.Flashing Then
			DrawTaskBandFlash(dest, rect)
		Else
			dest.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            Dim gp As Drawing2D.GraphicsPath = ZPixel.GraphicsRenderer.GetRoundedRect(rect, 5)
			Dim fillColor As Color = Color.Transparent
			If state = TaskBandState.Hot Then
				fillColor = Color.FromArgb(75, 0, 0, 0)
			ElseIf state = TaskBandState.Down Then
				fillColor = Color.FromArgb(150, 0, 0, 0)
			ElseIf state = TaskBandState.DownHot Then
				fillColor = Color.FromArgb(200, 0, 0, 0)
			ElseIf state = TaskBandState.DownDown Then
				fillColor = Color.Black
			End If
			dest.FillPath(New SolidBrush(fillColor), gp)
			dest.DrawPath(Pens.Black, gp)
        End If
	End Sub

	Private Shared Sub DrawTaskBandFlash(ByVal dest As Graphics, ByVal rect As Rectangle)
		If VisualStyles.VisualStyleRenderer.IsElementDefined(VisualStyles.VisualStyleElement.TaskBand.FlashButton.Normal) Then
			Dim r As New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.TaskBand.FlashButton.Normal)
			r.DrawBackground(dest, rect)
		End If
	End Sub

End Class