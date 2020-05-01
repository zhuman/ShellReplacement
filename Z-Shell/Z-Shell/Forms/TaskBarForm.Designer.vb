<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TaskBarForm
    Inherits AppbarForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim VisualStyleRenderer1 As ZShell.VisualStyleRenderer = New ZShell.VisualStyleRenderer
        Dim NotifyIconBarConfig1 As ZShell.NotifyIconBarConfig = New ZShell.NotifyIconBarConfig
        Dim VisualStyleRenderer2 As ZShell.VisualStyleRenderer = New ZShell.VisualStyleRenderer
        Dim VisualStyleRenderer3 As ZShell.VisualStyleRenderer = New ZShell.VisualStyleRenderer
        Me.imlWindows = New System.Windows.Forms.ImageList(Me.components)
        Me.btnStart = New System.Windows.Forms.Button
        Me.mnuContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LocationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LeftToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BottomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.pnlStart = New System.Windows.Forms.Panel
        Me.tltTooltips = New System.Windows.Forms.ToolTip(Me.components)
        Me.NotifyIconBar1 = New ZShell.NotifyIconBar
        Me.TaskBarControl1 = New ZShell.TaskBarControl
        Me.InfoLabel1 = New ZShell.InfoLabel
        Me.mnuContext.SuspendLayout()
        Me.pnlStart.SuspendLayout()
        Me.SuspendLayout()
        '
        'imlWindows
        '
        Me.imlWindows.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imlWindows.ImageSize = New System.Drawing.Size(48, 48)
        Me.imlWindows.TransparentColor = System.Drawing.Color.Transparent
        '
        'btnStart
        '
        Me.btnStart.ContextMenuStrip = Me.mnuContext
        Me.btnStart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnStart.Location = New System.Drawing.Point(2, 2)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(60, 28)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'mnuContext
        '
        Me.mnuContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OptionsToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.mnuContext.Name = "ContextMenuStrip1"
        Me.mnuContext.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.mnuContext.Size = New System.Drawing.Size(123, 48)
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LocationToolStripMenuItem})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.OptionsToolStripMenuItem.Text = "&Options"
        '
        'LocationToolStripMenuItem
        '
        Me.LocationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TopToolStripMenuItem, Me.LeftToolStripMenuItem, Me.RightToolStripMenuItem, Me.BottomToolStripMenuItem})
        Me.LocationToolStripMenuItem.Name = "LocationToolStripMenuItem"
        Me.LocationToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.LocationToolStripMenuItem.Text = "&Location"
        '
        'TopToolStripMenuItem
        '
        Me.TopToolStripMenuItem.Name = "TopToolStripMenuItem"
        Me.TopToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.TopToolStripMenuItem.Text = "&Top"
        '
        'LeftToolStripMenuItem
        '
        Me.LeftToolStripMenuItem.Name = "LeftToolStripMenuItem"
        Me.LeftToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.LeftToolStripMenuItem.Text = "&Left"
        '
        'RightToolStripMenuItem
        '
        Me.RightToolStripMenuItem.Name = "RightToolStripMenuItem"
        Me.RightToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.RightToolStripMenuItem.Text = "&Right"
        '
        'BottomToolStripMenuItem
        '
        Me.BottomToolStripMenuItem.Name = "BottomToolStripMenuItem"
        Me.BottomToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.BottomToolStripMenuItem.Text = "&Bottom"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.ExitToolStripMenuItem.Text = "&Exit"
        '
        'pnlStart
        '
        Me.pnlStart.Controls.Add(Me.btnStart)
        Me.pnlStart.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlStart.Location = New System.Drawing.Point(0, 0)
        Me.pnlStart.Name = "pnlStart"
        Me.pnlStart.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlStart.Size = New System.Drawing.Size(64, 32)
        Me.pnlStart.TabIndex = 0
        '
        'NotifyIconBar1
        '
        Me.NotifyIconBar1.Background = VisualStyleRenderer1
        NotifyIconBarConfig1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        NotifyIconBarConfig1.Background = VisualStyleRenderer1
        NotifyIconBarConfig1.Dock = System.Windows.Forms.DockStyle.Right
        NotifyIconBarConfig1.HorizontalSpacing = 3
        NotifyIconBarConfig1.VerticalSpacing = 3
        Me.NotifyIconBar1.Config = NotifyIconBarConfig1
        Me.NotifyIconBar1.Dock = System.Windows.Forms.DockStyle.Right
        Me.NotifyIconBar1.Location = New System.Drawing.Point(528, 0)
        Me.NotifyIconBar1.Name = "NotifyIconBar1"
        Me.NotifyIconBar1.Size = New System.Drawing.Size(224, 32)
        Me.NotifyIconBar1.TabIndex = 2
        '
        'TaskBarControl1
        '
        Me.TaskBarControl1.Background = VisualStyleRenderer2
        Me.TaskBarControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TaskBarControl1.Location = New System.Drawing.Point(64, 0)
        Me.TaskBarControl1.MinimumTaskSize = New System.Drawing.Size(60, 24)
        Me.TaskBarControl1.Name = "TaskBarControl1"
        Me.TaskBarControl1.Size = New System.Drawing.Size(464, 32)
        Me.TaskBarControl1.TabIndex = 3
        Me.TaskBarControl1.TaskButtonFlash = VisualStyleRenderer3
        Me.TaskBarControl1.TaskButtonFlashTextColor = System.Drawing.Color.Red
        Me.TaskBarControl1.TaskButtonTextFont = New Font("Tahoma", 8.25!)
        Me.TaskBarControl1.Text = "TaskBarControl1"
        '
        'InfoLabel1
        '
        Me.InfoLabel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.InfoLabel1.Location = New System.Drawing.Point(752, 0)
        Me.InfoLabel1.Name = "InfoLabel1"
        Me.InfoLabel1.Pattern = "#date# #time#"
        Me.InfoLabel1.Size = New System.Drawing.Size(60, 32)
        Me.InfoLabel1.TabIndex = 4
        Me.InfoLabel1.Text = "8/6/2007 11:33:30"
        '
        'TaskBarForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(852, 32)
        Me.Controls.Add(Me.TaskBarControl1)
        Me.Controls.Add(Me.pnlStart)
        Me.Controls.Add(Me.NotifyIconBar1)
        Me.Controls.Add(Me.InfoLabel1)
        Me.Edge = ZShell.AppbarForm.ABEdge.Bottom
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "TaskBarForm"
        Me.ShowInTaskbar = False
        Me.Text = "Z-Shell"
        Me.TopMost = True
        Me.mnuContext.ResumeLayout(False)
        Me.pnlStart.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents imlWindows As System.Windows.Forms.ImageList
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents pnlStart As System.Windows.Forms.Panel
    Friend WithEvents mnuContext As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LocationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LeftToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BottomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tltTooltips As System.Windows.Forms.ToolTip
    Friend WithEvents NotifyIconBar1 As ZShell.NotifyIconBar
    Friend WithEvents TaskBarControl1 As ZShell.TaskBarControl
    Friend WithEvents InfoLabel1 As ZShell.InfoLabel

End Class
