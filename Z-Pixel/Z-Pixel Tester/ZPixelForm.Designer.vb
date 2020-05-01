<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZPixelForm
    Inherits ZPixel.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZPixelForm))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.NewToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.CutToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.CopyToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.PasteToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.Button1 = New System.Windows.Forms.Button
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.CoolPictureBox1 = New PPAB_Tester.CoolPictureBox
        Me.CoolToolStrip1 = New PPAB_Tester.CoolToolStrip
        Me.NewToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.OpenToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.SaveToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.PrintToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.CutToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.CopyToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.PasteToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.HelpToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton
        Me.CoolMenuItem1ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CoolMenuItem2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CoolMenuItem3ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CoolMenuItem31ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CoolMenuItem3141ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CoolMenuItem32ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CoolMenuItem4ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.CoolPictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CoolToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripButton, Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.toolStripSeparator, Me.CutToolStripButton, Me.CopyToolStripButton, Me.PasteToolStripButton, Me.toolStripSeparator1, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(361, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'NewToolStripButton
        '
        Me.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NewToolStripButton.Image = CType(resources.GetObject("NewToolStripButton.Image"), System.Drawing.Image)
        Me.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewToolStripButton.Name = "NewToolStripButton"
        Me.NewToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.NewToolStripButton.Text = "&New"
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.OpenToolStripButton.Text = "&Open"
        '
        'SaveToolStripButton
        '
        Me.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveToolStripButton.Image = CType(resources.GetObject("SaveToolStripButton.Image"), System.Drawing.Image)
        Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.SaveToolStripButton.Text = "&Save"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'CutToolStripButton
        '
        Me.CutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CutToolStripButton.Image = CType(resources.GetObject("CutToolStripButton.Image"), System.Drawing.Image)
        Me.CutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CutToolStripButton.Name = "CutToolStripButton"
        Me.CutToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.CutToolStripButton.Text = "C&ut"
        '
        'CopyToolStripButton
        '
        Me.CopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CopyToolStripButton.Image = CType(resources.GetObject("CopyToolStripButton.Image"), System.Drawing.Image)
        Me.CopyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CopyToolStripButton.Name = "CopyToolStripButton"
        Me.CopyToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.CopyToolStripButton.Text = "&Copy"
        '
        'PasteToolStripButton
        '
        Me.PasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PasteToolStripButton.Image = CType(resources.GetObject("PasteToolStripButton.Image"), System.Drawing.Image)
        Me.PasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PasteToolStripButton.Name = "PasteToolStripButton"
        Me.PasteToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PasteToolStripButton.Text = "&Paste"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 200)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.SplitContainer1.Panel1.Controls.Add(Me.CheckBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.Red
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer1.Size = New System.Drawing.Size(361, 100)
        Me.SplitContainer1.SplitterDistance = 120
        Me.SplitContainer1.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(54, 17)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(12, 17)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(81, 17)
        Me.CheckBox1.TabIndex = 4
        Me.CheckBox1.Text = "CheckBox1"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CoolPictureBox1
        '
        Me.CoolPictureBox1.Image = Global.PPAB_Tester.My.Resources.Resources.button
        Me.CoolPictureBox1.Location = New System.Drawing.Point(51, 115)
        Me.CoolPictureBox1.Name = "CoolPictureBox1"
        Me.CoolPictureBox1.Size = New System.Drawing.Size(73, 73)
        Me.CoolPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.CoolPictureBox1.TabIndex = 2
        Me.CoolPictureBox1.TabStop = False
        '
        'CoolToolStrip1
        '
        Me.CoolToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripButton1, Me.OpenToolStripButton1, Me.SaveToolStripButton1, Me.PrintToolStripButton1, Me.toolStripSeparator2, Me.CutToolStripButton1, Me.CopyToolStripButton1, Me.PasteToolStripButton1, Me.toolStripSeparator3, Me.HelpToolStripButton1, Me.ToolStripSeparator4, Me.ToolStripDropDownButton1})
        Me.CoolToolStrip1.Location = New System.Drawing.Point(0, 25)
        Me.CoolToolStrip1.Name = "CoolToolStrip1"
        Me.CoolToolStrip1.Size = New System.Drawing.Size(361, 25)
        Me.CoolToolStrip1.TabIndex = 1
        Me.CoolToolStrip1.Text = "CoolToolStrip1"
        '
        'NewToolStripButton1
        '
        Me.NewToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NewToolStripButton1.Image = CType(resources.GetObject("NewToolStripButton1.Image"), System.Drawing.Image)
        Me.NewToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewToolStripButton1.Name = "NewToolStripButton1"
        Me.NewToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.NewToolStripButton1.Text = "&New"
        '
        'OpenToolStripButton1
        '
        Me.OpenToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenToolStripButton1.Image = CType(resources.GetObject("OpenToolStripButton1.Image"), System.Drawing.Image)
        Me.OpenToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton1.Name = "OpenToolStripButton1"
        Me.OpenToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.OpenToolStripButton1.Text = "&Open"
        '
        'SaveToolStripButton1
        '
        Me.SaveToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveToolStripButton1.Image = CType(resources.GetObject("SaveToolStripButton1.Image"), System.Drawing.Image)
        Me.SaveToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripButton1.Name = "SaveToolStripButton1"
        Me.SaveToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.SaveToolStripButton1.Text = "&Save"
        '
        'PrintToolStripButton1
        '
        Me.PrintToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton1.Image = CType(resources.GetObject("PrintToolStripButton1.Image"), System.Drawing.Image)
        Me.PrintToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton1.Name = "PrintToolStripButton1"
        Me.PrintToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton1.Text = "&Print"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'CutToolStripButton1
        '
        Me.CutToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CutToolStripButton1.Image = CType(resources.GetObject("CutToolStripButton1.Image"), System.Drawing.Image)
        Me.CutToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CutToolStripButton1.Name = "CutToolStripButton1"
        Me.CutToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.CutToolStripButton1.Text = "C&ut"
        '
        'CopyToolStripButton1
        '
        Me.CopyToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CopyToolStripButton1.Image = CType(resources.GetObject("CopyToolStripButton1.Image"), System.Drawing.Image)
        Me.CopyToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CopyToolStripButton1.Name = "CopyToolStripButton1"
        Me.CopyToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.CopyToolStripButton1.Text = "&Copy"
        '
        'PasteToolStripButton1
        '
        Me.PasteToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PasteToolStripButton1.Image = CType(resources.GetObject("PasteToolStripButton1.Image"), System.Drawing.Image)
        Me.PasteToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PasteToolStripButton1.Name = "PasteToolStripButton1"
        Me.PasteToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.PasteToolStripButton1.Text = "&Paste"
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'HelpToolStripButton1
        '
        Me.HelpToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton1.Image = CType(resources.GetObject("HelpToolStripButton1.Image"), System.Drawing.Image)
        Me.HelpToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton1.Name = "HelpToolStripButton1"
        Me.HelpToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.HelpToolStripButton1.Text = "He&lp"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CoolMenuItem1ToolStripMenuItem, Me.CoolMenuItem2ToolStripMenuItem, Me.CoolMenuItem3ToolStripMenuItem, Me.CoolMenuItem4ToolStripMenuItem})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(29, 22)
        Me.ToolStripDropDownButton1.Text = "ToolStripDropDownButton1"
        '
        'CoolMenuItem1ToolStripMenuItem
        '
        Me.CoolMenuItem1ToolStripMenuItem.Name = "CoolMenuItem1ToolStripMenuItem"
        Me.CoolMenuItem1ToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.CoolMenuItem1ToolStripMenuItem.Text = "Cool Menu Item 1"
        '
        'CoolMenuItem2ToolStripMenuItem
        '
        Me.CoolMenuItem2ToolStripMenuItem.Name = "CoolMenuItem2ToolStripMenuItem"
        Me.CoolMenuItem2ToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.CoolMenuItem2ToolStripMenuItem.Text = "Cool Menu Item 2"
        '
        'CoolMenuItem3ToolStripMenuItem
        '
        Me.CoolMenuItem3ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CoolMenuItem31ToolStripMenuItem, Me.CoolMenuItem3141ToolStripMenuItem, Me.CoolMenuItem32ToolStripMenuItem})
        Me.CoolMenuItem3ToolStripMenuItem.Name = "CoolMenuItem3ToolStripMenuItem"
        Me.CoolMenuItem3ToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.CoolMenuItem3ToolStripMenuItem.Text = "Cool Menu Item 3"
        '
        'CoolMenuItem31ToolStripMenuItem
        '
        Me.CoolMenuItem31ToolStripMenuItem.Name = "CoolMenuItem31ToolStripMenuItem"
        Me.CoolMenuItem31ToolStripMenuItem.Size = New System.Drawing.Size(365, 22)
        Me.CoolMenuItem31ToolStripMenuItem.Text = "Cool Menu Item 3.1"
        '
        'CoolMenuItem3141ToolStripMenuItem
        '
        Me.CoolMenuItem3141ToolStripMenuItem.Name = "CoolMenuItem3141ToolStripMenuItem"
        Me.CoolMenuItem3141ToolStripMenuItem.Size = New System.Drawing.Size(365, 22)
        Me.CoolMenuItem3141ToolStripMenuItem.Text = "Cool Menu Item 3.14159265358979323846264338327950"
        '
        'CoolMenuItem32ToolStripMenuItem
        '
        Me.CoolMenuItem32ToolStripMenuItem.Name = "CoolMenuItem32ToolStripMenuItem"
        Me.CoolMenuItem32ToolStripMenuItem.Size = New System.Drawing.Size(365, 22)
        Me.CoolMenuItem32ToolStripMenuItem.Text = "Cool Menu Item 3.2"
        '
        'CoolMenuItem4ToolStripMenuItem
        '
        Me.CoolMenuItem4ToolStripMenuItem.Name = "CoolMenuItem4ToolStripMenuItem"
        Me.CoolMenuItem4ToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.CoolMenuItem4ToolStripMenuItem.Text = "Cool Menu Item 4"
        '
        'ZPixelForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(361, 300)
        Me.Controls.Add(Me.CoolPictureBox1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.CoolToolStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Image = Global.PPAB_Tester.My.Resources.Resources.back
        Me.Name = "ZPixelForm"
        Me.Text = "ZPixelForm"
        Me.TopMost = True
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.CoolPictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CoolToolStrip1.ResumeLayout(False)
        Me.CoolToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents NewToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents CopyToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PasteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents CoolToolStrip1 As PPAB_Tester.CoolToolStrip
    Friend WithEvents NewToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents PrintToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents CopyToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents PasteToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents CoolPictureBox1 As PPAB_Tester.CoolPictureBox
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents CoolMenuItem1ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CoolMenuItem2ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CoolMenuItem3ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CoolMenuItem31ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CoolMenuItem3141ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CoolMenuItem32ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CoolMenuItem4ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
