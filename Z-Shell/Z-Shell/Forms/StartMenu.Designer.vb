<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StartMenu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StartMenu))
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Pinned Applications", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Recent Applications", System.Windows.Forms.HorizontalAlignment.Left)
        Me.lblUsername = New System.Windows.Forms.Label
        Me.btnAllPrograms = New System.Windows.Forms.Button
        Me.imlMainShortcuts = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.trvAllPrograms = New ZShell.AllProgramsTree
        Me.lstMainShortcuts = New System.Windows.Forms.ListView
        Me.clmName = New System.Windows.Forms.ColumnHeader
        Me.pnlSidebar = New System.Windows.Forms.Panel
        Me.btnRun = New System.Windows.Forms.Button
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.btnLogoff = New System.Windows.Forms.Button
        Me.btnShutdown = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnControlPanel = New System.Windows.Forms.Button
        Me.btnNetwork = New System.Windows.Forms.Button
        Me.btnComputer = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnGames = New System.Windows.Forms.Button
        Me.btnVideos = New System.Windows.Forms.Button
        Me.btnPictures = New System.Windows.Forms.Button
        Me.btnMusic = New System.Windows.Forms.Button
        Me.btnUsername = New System.Windows.Forms.Button
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.picUserImage = New System.Windows.Forms.PictureBox
        Me.mnuPowerOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.LockToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LogoffToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.HibernateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StandbyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.RestartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ShutdownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitZShellToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.pnlSidebar.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.picUserImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuPowerOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblUsername
        '
        Me.lblUsername.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblUsername.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblUsername.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsername.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUsername.Location = New System.Drawing.Point(0, 0)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(427, 35)
        Me.lblUsername.TabIndex = 0
        Me.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAllPrograms
        '
        Me.btnAllPrograms.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnAllPrograms.FlatAppearance.BorderSize = 0
        Me.btnAllPrograms.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllPrograms.Image = CType(resources.GetObject("btnAllPrograms.Image"), System.Drawing.Image)
        Me.btnAllPrograms.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAllPrograms.Location = New System.Drawing.Point(0, 438)
        Me.btnAllPrograms.Name = "btnAllPrograms"
        Me.btnAllPrograms.Size = New System.Drawing.Size(234, 24)
        Me.btnAllPrograms.TabIndex = 1
        Me.btnAllPrograms.Text = "All Programs"
        Me.btnAllPrograms.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAllPrograms.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.btnAllPrograms.UseVisualStyleBackColor = True
        '
        'imlMainShortcuts
        '
        Me.imlMainShortcuts.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imlMainShortcuts.ImageSize = New System.Drawing.Size(32, 32)
        Me.imlMainShortcuts.TransparentColor = System.Drawing.Color.Transparent
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 35)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.trvAllPrograms)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lstMainShortcuts)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAllPrograms)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlSidebar)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.SplitContainer1.Size = New System.Drawing.Size(427, 462)
        Me.SplitContainer1.SplitterDistance = 234
        Me.SplitContainer1.TabIndex = 3
        '
        'trvAllPrograms
        '
        Me.trvAllPrograms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvAllPrograms.FullRowSelect = True
        Me.trvAllPrograms.HotTracking = True
        Me.trvAllPrograms.ImageIndex = 0
        Me.trvAllPrograms.Location = New System.Drawing.Point(0, 0)
        Me.trvAllPrograms.Name = "trvAllPrograms"
        Me.trvAllPrograms.SelectedImageIndex = 0
        Me.trvAllPrograms.ShowLines = False
        Me.trvAllPrograms.ShowNodeToolTips = True
        Me.trvAllPrograms.ShowPlusMinus = False
        Me.trvAllPrograms.ShowRootLines = False
        Me.trvAllPrograms.Size = New System.Drawing.Size(234, 438)
        Me.trvAllPrograms.Sorted = True
        Me.trvAllPrograms.TabIndex = 3
        '
        'lstMainShortcuts
        '
        Me.lstMainShortcuts.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lstMainShortcuts.AllowDrop = True
        Me.lstMainShortcuts.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clmName})
        Me.lstMainShortcuts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstMainShortcuts.FullRowSelect = True
        ListViewGroup1.Header = "Pinned Applications"
        ListViewGroup1.Name = "grpPinnedApps"
        ListViewGroup1.Tag = "grpPinnedApps"
        ListViewGroup2.Header = "Recent Applications"
        ListViewGroup2.Name = "grpRecentApps"
        ListViewGroup2.Tag = "grpRecentApps"
        Me.lstMainShortcuts.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.lstMainShortcuts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstMainShortcuts.HotTracking = True
        Me.lstMainShortcuts.HoverSelection = True
        Me.lstMainShortcuts.LabelEdit = True
        Me.lstMainShortcuts.LargeImageList = Me.imlMainShortcuts
        Me.lstMainShortcuts.Location = New System.Drawing.Point(0, 0)
        Me.lstMainShortcuts.MultiSelect = False
        Me.lstMainShortcuts.Name = "lstMainShortcuts"
        Me.lstMainShortcuts.OwnerDraw = True
        Me.lstMainShortcuts.ShowItemToolTips = True
        Me.lstMainShortcuts.Size = New System.Drawing.Size(234, 438)
        Me.lstMainShortcuts.SmallImageList = Me.imlMainShortcuts
        Me.lstMainShortcuts.TabIndex = 2
        Me.lstMainShortcuts.UseCompatibleStateImageBehavior = False
        Me.lstMainShortcuts.View = System.Windows.Forms.View.Tile
        '
        'clmName
        '
        Me.clmName.Text = "Name"
        Me.clmName.Width = 400
        '
        'pnlSidebar
        '
        Me.pnlSidebar.Controls.Add(Me.btnRun)
        Me.pnlSidebar.Controls.Add(Me.PictureBox2)
        Me.pnlSidebar.Controls.Add(Me.TableLayoutPanel1)
        Me.pnlSidebar.Controls.Add(Me.btnControlPanel)
        Me.pnlSidebar.Controls.Add(Me.btnNetwork)
        Me.pnlSidebar.Controls.Add(Me.btnComputer)
        Me.pnlSidebar.Controls.Add(Me.PictureBox1)
        Me.pnlSidebar.Controls.Add(Me.btnGames)
        Me.pnlSidebar.Controls.Add(Me.btnVideos)
        Me.pnlSidebar.Controls.Add(Me.btnPictures)
        Me.pnlSidebar.Controls.Add(Me.btnMusic)
        Me.pnlSidebar.Controls.Add(Me.btnUsername)
        Me.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSidebar.Location = New System.Drawing.Point(0, 48)
        Me.pnlSidebar.Name = "pnlSidebar"
        Me.pnlSidebar.Padding = New System.Windows.Forms.Padding(5)
        Me.pnlSidebar.Size = New System.Drawing.Size(189, 414)
        Me.pnlSidebar.TabIndex = 1
        '
        'btnRun
        '
        Me.btnRun.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnRun.FlatAppearance.BorderSize = 0
        Me.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRun.Location = New System.Drawing.Point(5, 313)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(179, 35)
        Me.btnRun.TabIndex = 11
        Me.btnRun.Text = "Run..."
        Me.btnRun.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PictureBox2.Location = New System.Drawing.Point(5, 299)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(179, 14)
        Me.PictureBox2.TabIndex = 10
        Me.PictureBox2.TabStop = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnLogoff, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnShutdown, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Button1, 2, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(5, 374)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(179, 35)
        Me.TableLayoutPanel1.TabIndex = 7
        '
        'btnLogoff
        '
        Me.btnLogoff.BackColor = System.Drawing.Color.Yellow
        Me.btnLogoff.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLogoff.FlatAppearance.BorderSize = 0
        Me.btnLogoff.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogoff.ForeColor = System.Drawing.Color.Black
        Me.btnLogoff.Location = New System.Drawing.Point(83, 3)
        Me.btnLogoff.Name = "btnLogoff"
        Me.btnLogoff.Size = New System.Drawing.Size(74, 29)
        Me.btnLogoff.TabIndex = 7
        Me.btnLogoff.Text = "Logoff"
        Me.btnLogoff.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLogoff.UseVisualStyleBackColor = False
        '
        'btnShutdown
        '
        Me.btnShutdown.BackColor = System.Drawing.Color.Firebrick
        Me.btnShutdown.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnShutdown.FlatAppearance.BorderSize = 0
        Me.btnShutdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShutdown.ForeColor = System.Drawing.Color.Black
        Me.btnShutdown.Location = New System.Drawing.Point(3, 3)
        Me.btnShutdown.Name = "btnShutdown"
        Me.btnShutdown.Size = New System.Drawing.Size(74, 29)
        Me.btnShutdown.TabIndex = 6
        Me.btnShutdown.Text = "Shutdown"
        Me.btnShutdown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnShutdown.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(163, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(13, 29)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = ">"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btnControlPanel
        '
        Me.btnControlPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnControlPanel.FlatAppearance.BorderSize = 0
        Me.btnControlPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnControlPanel.Location = New System.Drawing.Point(5, 264)
        Me.btnControlPanel.Name = "btnControlPanel"
        Me.btnControlPanel.Size = New System.Drawing.Size(179, 35)
        Me.btnControlPanel.TabIndex = 5
        Me.btnControlPanel.Text = "Control Panel"
        Me.btnControlPanel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnControlPanel.UseVisualStyleBackColor = True
        '
        'btnNetwork
        '
        Me.btnNetwork.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnNetwork.FlatAppearance.BorderSize = 0
        Me.btnNetwork.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNetwork.Location = New System.Drawing.Point(5, 229)
        Me.btnNetwork.Name = "btnNetwork"
        Me.btnNetwork.Size = New System.Drawing.Size(179, 35)
        Me.btnNetwork.TabIndex = 4
        Me.btnNetwork.Text = "Network"
        Me.btnNetwork.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnNetwork.UseVisualStyleBackColor = True
        '
        'btnComputer
        '
        Me.btnComputer.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnComputer.FlatAppearance.BorderSize = 0
        Me.btnComputer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnComputer.Location = New System.Drawing.Point(5, 194)
        Me.btnComputer.Name = "btnComputer"
        Me.btnComputer.Size = New System.Drawing.Size(179, 35)
        Me.btnComputer.TabIndex = 3
        Me.btnComputer.Text = "Computer"
        Me.btnComputer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnComputer.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PictureBox1.Location = New System.Drawing.Point(5, 180)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(179, 14)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'btnGames
        '
        Me.btnGames.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnGames.FlatAppearance.BorderSize = 0
        Me.btnGames.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGames.Location = New System.Drawing.Point(5, 145)
        Me.btnGames.Name = "btnGames"
        Me.btnGames.Size = New System.Drawing.Size(179, 35)
        Me.btnGames.TabIndex = 2
        Me.btnGames.Text = "Games"
        Me.btnGames.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnGames.UseVisualStyleBackColor = True
        '
        'btnVideos
        '
        Me.btnVideos.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnVideos.FlatAppearance.BorderSize = 0
        Me.btnVideos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVideos.Location = New System.Drawing.Point(5, 110)
        Me.btnVideos.Name = "btnVideos"
        Me.btnVideos.Size = New System.Drawing.Size(179, 35)
        Me.btnVideos.TabIndex = 8
        Me.btnVideos.Text = "Videos"
        Me.btnVideos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnVideos.UseVisualStyleBackColor = True
        '
        'btnPictures
        '
        Me.btnPictures.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnPictures.FlatAppearance.BorderSize = 0
        Me.btnPictures.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPictures.Location = New System.Drawing.Point(5, 75)
        Me.btnPictures.Name = "btnPictures"
        Me.btnPictures.Size = New System.Drawing.Size(179, 35)
        Me.btnPictures.TabIndex = 1
        Me.btnPictures.Text = "Pictures"
        Me.btnPictures.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPictures.UseVisualStyleBackColor = True
        '
        'btnMusic
        '
        Me.btnMusic.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMusic.FlatAppearance.BorderSize = 0
        Me.btnMusic.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMusic.Location = New System.Drawing.Point(5, 40)
        Me.btnMusic.Name = "btnMusic"
        Me.btnMusic.Size = New System.Drawing.Size(179, 35)
        Me.btnMusic.TabIndex = 0
        Me.btnMusic.Text = "Music"
        Me.btnMusic.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnMusic.UseVisualStyleBackColor = True
        '
        'btnUsername
        '
        Me.btnUsername.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnUsername.FlatAppearance.BorderSize = 0
        Me.btnUsername.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUsername.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUsername.Location = New System.Drawing.Point(5, 5)
        Me.btnUsername.Name = "btnUsername"
        Me.btnUsername.Size = New System.Drawing.Size(179, 35)
        Me.btnUsername.TabIndex = 9
        Me.btnUsername.Text = "Username"
        Me.btnUsername.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnUsername.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.picUserImage, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(189, 48)
        Me.TableLayoutPanel2.TabIndex = 11
        '
        'picUserImage
        '
        Me.picUserImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picUserImage.Location = New System.Drawing.Point(73, 3)
        Me.picUserImage.Name = "picUserImage"
        Me.picUserImage.Size = New System.Drawing.Size(42, 42)
        Me.picUserImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picUserImage.TabIndex = 10
        Me.picUserImage.TabStop = False
        '
        'mnuPowerOptions
        '
        Me.mnuPowerOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LockToolStripMenuItem, Me.LogoffToolStripMenuItem, Me.ToolStripSeparator1, Me.HibernateToolStripMenuItem, Me.StandbyToolStripMenuItem, Me.ToolStripSeparator2, Me.RestartToolStripMenuItem, Me.ShutdownToolStripMenuItem, Me.ToolStripSeparator3, Me.ExitZShellToolStripMenuItem})
        Me.mnuPowerOptions.Name = "mnuPowerOptions"
        Me.mnuPowerOptions.Size = New System.Drawing.Size(167, 198)
        '
        'LockToolStripMenuItem
        '
        Me.LockToolStripMenuItem.Name = "LockToolStripMenuItem"
        Me.LockToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.LockToolStripMenuItem.Text = "&Lock/Switch User"
        '
        'LogoffToolStripMenuItem
        '
        Me.LogoffToolStripMenuItem.Name = "LogoffToolStripMenuItem"
        Me.LogoffToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.LogoffToolStripMenuItem.Text = "Lo&goff"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(163, 6)
        '
        'HibernateToolStripMenuItem
        '
        Me.HibernateToolStripMenuItem.Name = "HibernateToolStripMenuItem"
        Me.HibernateToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.HibernateToolStripMenuItem.Text = "&Hibernate"
        '
        'StandbyToolStripMenuItem
        '
        Me.StandbyToolStripMenuItem.Name = "StandbyToolStripMenuItem"
        Me.StandbyToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.StandbyToolStripMenuItem.Text = "Stand &By"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(163, 6)
        '
        'RestartToolStripMenuItem
        '
        Me.RestartToolStripMenuItem.Name = "RestartToolStripMenuItem"
        Me.RestartToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.RestartToolStripMenuItem.Text = "&Restart"
        '
        'ShutdownToolStripMenuItem
        '
        Me.ShutdownToolStripMenuItem.Name = "ShutdownToolStripMenuItem"
        Me.ShutdownToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.ShutdownToolStripMenuItem.Text = "&Shutdown"
        '
        'ExitZShellToolStripMenuItem
        '
        Me.ExitZShellToolStripMenuItem.Name = "ExitZShellToolStripMenuItem"
        Me.ExitZShellToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.ExitZShellToolStripMenuItem.Text = "E&xit Z-Shell"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(163, 6)
        '
        'StartMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(427, 497)
        Me.ControlBox = False
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.lblUsername)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "StartMenu"
        Me.Opacity = 0
        Me.ShowInTaskbar = False
        Me.TopMost = True
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.pnlSidebar.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.picUserImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuPowerOptions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents btnAllPrograms As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents imlMainShortcuts As System.Windows.Forms.ImageList
    Friend WithEvents btnControlPanel As System.Windows.Forms.Button
    Friend WithEvents pnlSidebar As System.Windows.Forms.Panel
    Friend WithEvents btnGames As System.Windows.Forms.Button
    Friend WithEvents btnPictures As System.Windows.Forms.Button
    Friend WithEvents btnMusic As System.Windows.Forms.Button
    Friend WithEvents btnNetwork As System.Windows.Forms.Button
    Friend WithEvents btnComputer As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnLogoff As System.Windows.Forms.Button
    Friend WithEvents btnShutdown As System.Windows.Forms.Button
    Friend WithEvents btnVideos As System.Windows.Forms.Button
    Friend WithEvents picUserImage As System.Windows.Forms.PictureBox
    Friend WithEvents btnUsername As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents lstMainShortcuts As System.Windows.Forms.ListView
    Friend WithEvents clmName As System.Windows.Forms.ColumnHeader
    Friend WithEvents trvAllPrograms As ZShell.AllProgramsTree
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents mnuPowerOptions As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents LockToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogoffToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HibernateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StandbyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RestartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShutdownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitZShellToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
