Public Class DesktopForm

    Private Declare Auto Function PaintDesktop Lib "user32.dll" (ByVal gc As IntPtr) As Integer

    Private Sub DesktopForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Dim sdb As New ShutdownDialog
            sdb.ShowDialog()
        End If
    End Sub

    Private Sub DesktopForm_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        'PaintDesktop(e.Graphics.GetHdc)
        'e.Graphics.ReleaseHdc()
        'e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        'e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
        'e.Graphics.DrawString("Z-Shell", New Drawing.Font("Trebuchet MS", 50, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Black, 50, 50)
        'e.Graphics.DrawString("Z-Shell", New Drawing.Font("Trebuchet MS", 50, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.White, 48, 48)
        'e.Graphics.DrawString(My.Application.Info.Version.ToString, New Drawing.Font("Trebuchet MS", 25, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Black, 75, 105)
        'e.Graphics.DrawString(My.Application.Info.Version.ToString, New Drawing.Font("Trebuchet MS", 25, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.White, 73, 103)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.Caption = ""
            'cp.ClassName = "SHELLDLL_DefView"
            cp.ClassStyle = ClassCreateParams.ClassStyles.ParentDC
            cp.ExStyle = ExtendedWindowStyles.WS_EX_LEFT Or ExtendedWindowStyles.WS_EX_LTRREADING Or ExtendedWindowStyles.WS_EX_RIGHTSCROLLBAR
            cp.Style = WindowStyles.WS_CHILD Or WindowStyles.WS_VISIBLE Or WindowStyles.WS_CLIPCHILDREN Or WindowStyles.WS_CLIPSIBLINGS
            cp.Width = Screen.PrimaryScreen.Bounds.Width
            cp.Height = Screen.PrimaryScreen.Bounds.Height
            cp.X = 0
            cp.Y = 0
            cp.Parent = DesktopWindow.Handle
            Return cp
        End Get
    End Property

    Private Sub DesktopForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim desktopFolder As IShellFolder = Nothing
        ShellFolders.SHGetDesktopFolder(desktopFolder)
        If desktopFolder IsNot Nothing Then
            Dim handle As IntPtr = IntPtr.Zero
            Dim g As Guid = New Guid("000214e3-0000-0000-c000-000000000046")
            desktopFolder.CreateViewObject(Me.Handle, g, handle)
        End If
    End Sub

End Class