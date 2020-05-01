Public Class StartMenu

	WithEvents timFade As New Timer

	Public Sub New()

		' This call is required by the Windows Form Designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.

	End Sub

	Public Sub FadeIn()
        Me.Opacity = 0
        Me.Show()
        Me.Region = New Region(ZPixel.GraphicsRenderer.GetRoundedRect(New Rectangle(0, 0, Me.Width, Me.Height), 5))
		fadingIn = True
		timFade.Interval = 10
		timFade.Enabled = True

		picUserImage.Image = GetUserImage(Environment.UserName)
		btnUsername.Text = Environment.UserName
		lblUsername.BackColor = ControlPaint.Light(Me.BackColor, 0.5)

		Me.trvAllPrograms.Visible = False
		Me.lstMainShortcuts.Visible = True
		Me.btnAllPrograms.Text = "All Programs"
		Me.btnAllPrograms.Image = My.Resources.arrow_forward_16
		Me.btnAllPrograms.ImageAlign = ContentAlignment.MiddleLeft
		Me.btnAllPrograms.TextAlign = ContentAlignment.MiddleRight
		Me.btnAllPrograms.TextImageRelation = TextImageRelation.TextBeforeImage

		Me.lstMainShortcuts.Items.Clear()
		Me.imlMainShortcuts.Images.Clear()
		AddPinnedItems()
		AddRecentPrograms()
	End Sub

	Public Sub FadeOut()
		If Me.Visible Then
			fadingIn = False
			timFade.Interval = 10
			timFade.Enabled = True
		End If
	End Sub

	Dim fadingIn As Boolean

	Private Sub timFade_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timFade.Tick
		If fadingIn Then
			If Me.Opacity < 1 Then
				Me.Opacity = Me.Opacity + 0.1
			Else
				timFade.Enabled = False
			End If
		Else
			If Me.Opacity > 0 Then
				Me.Opacity = Me.Opacity - 0.1
			Else
				timFade.Enabled = False
				Me.Hide()
			End If
		End If
	End Sub

	Private Declare Auto Function ExtractIcon Lib "shell32.dll" (ByVal hInst As IntPtr, ByVal lpszExeFileName As String, ByVal nIconIndex As Integer) As Integer

	Private Sub AddPinnedItems()
		'Add the user's internet browser icon
		Dim internetIcon As New ListViewItem
		Dim internetInfo As RegistryFileInfo = FileActions.GetDefaultInternetProgram()
		internetIcon.Text = internetInfo.Name
		internetIcon.Name = "Internet"
		internetIcon.Tag = internetInfo
		'internetIcon.Font = New Font(internetIcon.Font, FontStyle.Bold)
		internetIcon.Group = Me.lstMainShortcuts.Groups("grpPinnedApps")
        internetIcon.ImageKey = "Internet"
        Dim iIco As Icon = internetInfo.Icon 'New ShellFileInfo(internetInfo.IconFilePath, ShellFileInfo.SHGetFileInfoConstants.Icon Or ShellFileInfo.SHGetFileInfoConstants.LargeIcon).ShellIcon
        If iIco IsNot Nothing Then Me.imlMainShortcuts.Images.Add("Internet", iIco)

		Me.lstMainShortcuts.Items.Add(internetIcon)

		'Add the user's mail program icon
		Dim mailIcon As New ListViewItem
		Dim mailInfo As RegistryFileInfo = FileActions.GetDefaultMailProgram()
		mailIcon.Text = mailInfo.Name
		mailIcon.Name = "Mail"
		mailIcon.Tag = mailInfo
		'mailIcon.Font = New Font(mailIcon.Font, FontStyle.Bold)
		mailIcon.Group = Me.lstMainShortcuts.Groups("grpPinnedApps")
        mailIcon.ImageKey = "Mail"
        Dim mIco As Icon = mailInfo.Icon 'New ShellFileInfo(mailInfo.IconFilePath, ShellFileInfo.SHGetFileInfoConstants.Icon Or ShellFileInfo.SHGetFileInfoConstants.LargeIcon).ShellIcon
        If mIco IsNot Nothing Then Me.imlMainShortcuts.Images.Add("Mail", mIco)
		Me.lstMainShortcuts.Items.Add(mailIcon)
	End Sub

	Private Sub AddRecentPrograms()

	End Sub

	Private Sub btnMusic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMusic.Click
		Dim f As New FolderWindow(My.Computer.FileSystem.SpecialDirectories.MyMusic)
		f.Show()
		'ShellExecute(My.Computer.FileSystem.SpecialDirectories.MyMusic)
		Me.FadeOut()
	End Sub

	Private Sub btnPictures_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPictures.Click
		'Dim f As New FolderWindow(My.Computer.FileSystem.SpecialDirectories.MyPictures)
		'f.Show()
		ShellExecute(My.Computer.FileSystem.SpecialDirectories.MyPictures)
		Me.FadeOut()
	End Sub

	Private Sub btnAllPrograms_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllPrograms.Click
		If Me.trvAllPrograms.Visible Then
			Me.trvAllPrograms.Visible = False
			Me.lstMainShortcuts.Visible = True
			Me.btnAllPrograms.Text = "All Programs"
			Me.btnAllPrograms.Image = My.Resources.arrow_forward_16
			Me.btnAllPrograms.ImageAlign = ContentAlignment.MiddleLeft
			Me.btnAllPrograms.TextAlign = ContentAlignment.MiddleRight
			Me.btnAllPrograms.TextImageRelation = TextImageRelation.TextBeforeImage
		Else
			Me.trvAllPrograms.Visible = True
			Me.lstMainShortcuts.Visible = False
			Me.btnAllPrograms.Text = "Back"
			Me.btnAllPrograms.Image = My.Resources.arrow_back_16
			Me.btnAllPrograms.ImageAlign = ContentAlignment.MiddleRight
			Me.btnAllPrograms.TextAlign = ContentAlignment.MiddleLeft
			Me.btnAllPrograms.TextImageRelation = TextImageRelation.ImageBeforeText
			Me.trvAllPrograms.PopulatePrograms()
			Me.trvAllPrograms.Focus()
		End If
	End Sub

	Private Sub lstMainShortcuts_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewItemEventArgs) Handles lstMainShortcuts.DrawItem
		'Dim rrect As Drawing2D.GraphicsPath = PerPixelAlpha.GraphicsRenderer.GetRoundedRect(e.Bounds, 3)
		'e.Graphics.FillPath(New Drawing2D.LinearGradientBrush(e.Bounds.Location, e.Bounds.Location + New Point(0, e.Bounds.Height), Color.White, SystemColors.ButtonHighlight), rrect)
		'e.DrawText()
		e.DrawDefault = True
	End Sub

	Private Sub lstMainShortcuts_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lstMainShortcuts.ItemSelectionChanged
		If e.IsSelected Then e.Item.Selected = False
	End Sub

	Private Sub lstMainShortcuts_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstMainShortcuts.MouseClick
		Dim item As ListViewItem = Me.lstMainShortcuts.GetItemAt(e.X, e.Y)
		If item IsNot Nothing Then
			If item.Tag IsNot Nothing Then
				If CType(item.Tag, RegistryFileInfo).Actions.ContainsKey("open") Then
                    ShellExecute(CType(item.Tag, RegistryFileInfo).Actions("open"))
				End If
			End If
		End If
		Me.FadeOut()
	End Sub

	Private Sub btnShutdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShutdown.Click
		ShutdownWindows(ShutdownType.PowerOff, ShutdownMajorReason.MajorOther, ShutdownMinorReason.MinorOther)
	End Sub

	Private Sub btnLogoff_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogoff.Click
        ShutdownWindows(ShutdownType.Logoff, ShutdownMajorReason.MajorOther, ShutdownMinorReason.MinorOther)
	End Sub

	Private Sub btnControlPanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnControlPanel.Click
		ShellExecute("control.exe")
		Me.FadeOut()
	End Sub

	Private Sub btnVideos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVideos.Click
		ShellExecute(GetFolderPath(FolderID.MyVideo))
	End Sub

	Private Sub btnUsername_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsername.Click
		ShellExecute(GetFolderPath(FolderID.Profile))
	End Sub

	Private Sub btnRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRun.Click
		Dim r As New RunDialog
		r.ShowDialog()
	End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        mnuPowerOptions.Show(Button1, New Point(Button1.Width / 2, Button1.Height / 2))
    End Sub

    Private Sub LockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LockToolStripMenuItem.Click
        LockWindows()
    End Sub

    Private Sub LogoffToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoffToolStripMenuItem.Click
        ShutdownWindows(ShutdownType.Logoff, ShutdownMajorReason.MajorOther, ShutdownMinorReason.MinorOther)
    End Sub

    Private Sub HibernateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HibernateToolStripMenuItem.Click
        Application.SetSuspendState(PowerState.Hibernate, True, False)
    End Sub

    Private Sub StandbyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StandbyToolStripMenuItem.Click
        Application.SetSuspendState(PowerState.Suspend, True, False)
    End Sub

    Private Sub RestartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestartToolStripMenuItem.Click
        ShutdownWindows(ShutdownType.Reboot, ShutdownMajorReason.MajorOther, ShutdownMinorReason.MinorOther)
    End Sub

    Private Sub ShutdownToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShutdownToolStripMenuItem.Click
        ShutdownWindows(ShutdownType.PowerOff, ShutdownMajorReason.MajorOther, ShutdownMinorReason.MinorOther)
    End Sub

    Private Sub ExitZShellToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitZShellToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub StartMenu_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            Me.FadeOut()
        End If
    End Sub

End Class