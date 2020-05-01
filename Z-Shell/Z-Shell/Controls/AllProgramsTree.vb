''' <summary>
''' This class provides a working start menu treeview control 
''' that can be simply dropped into place.
''' </summary>
''' <remarks></remarks>
Public Class AllProgramsTree
	Inherits TreeView

	WithEvents fswUserSM As IO.FileSystemWatcher
	WithEvents fswAllSM As IO.FileSystemWatcher
	Dim needProgramsUpdate As Boolean = True
	Dim imlAllPrograms As New ImageList

	Public Sub New()
		'Set up the image list
		Me.imlAllPrograms.ColorDepth = ColorDepth.Depth32Bit
		Me.imlAllPrograms.ImageSize = New Size(16, 16)
		'Set up me
		Me.ShowLines = False
		Me.ShowNodeToolTips = True
		Me.ShowPlusMinus = False
		Me.ShowRootLines = False
		Me.HotTracking = True
		Me.FullRowSelect = True
		Me.ImageList = Me.imlAllPrograms
		'Set up the filesystem watchers
		fswUserSM = New IO.FileSystemWatcher(GetFolderPath(FolderID.StartMenu), "*")
		fswAllSM = New IO.FileSystemWatcher(GetFolderPath(FolderID.CommonStartMenu), "*")
		fswUserSM.EnableRaisingEvents = True
		fswAllSM.EnableRaisingEvents = True
		'Set up the treeview sorter
		Me.TreeViewNodeSorter = New StartMenuSorter
	End Sub

	Public Sub PopulatePrograms()
		If needProgramsUpdate Then
			Me.Cursor = Cursors.WaitCursor
			'Stop painting the treeview
			Me.BeginUpdate()
			'Clear all images and tree nodes
			Me.Nodes.Clear()
			Me.imlAllPrograms.Images.Clear()
			'Add folder icons based on the system's real folder icon
            Me.imlAllPrograms.Images.Add("Folder", New ShellFileInfo(GetFolderPath(FolderID.StartMenu), ShellFileInfo.SHGetFileInfoConstants.Icon Or ShellFileInfo.SHGetFileInfoConstants.SmallIcon).ShellIcon.ToBitmap)
            Me.imlAllPrograms.Images.Add("FolderSelected", New ShellFileInfo(GetFolderPath(FolderID.StartMenu), ShellFileInfo.SHGetFileInfoConstants.Icon Or ShellFileInfo.SHGetFileInfoConstants.SmallIcon).ShellIcon.ToBitmap)
			'First, populate all of the shortcut icons in the root folders of the start menu
			PopulatePrograms(Me.Nodes, New IO.DirectoryInfo(GetFolderPath(FolderID.StartMenu)), False)
			PopulatePrograms(Me.Nodes, New IO.DirectoryInfo(GetFolderPath(FolderID.CommonStartMenu)), False)
			PopulatePrograms(Me.Nodes, New IO.DirectoryInfo(GetFolderPath(FolderID.Programs)), False)
			PopulatePrograms(Me.Nodes, New IO.DirectoryInfo(GetFolderPath(FolderID.CommonPrograms)), False)
			'Next, populate all of the directories
			PopulatePrograms(Me.Nodes, New IO.DirectoryInfo(GetFolderPath(FolderID.Programs)), , False)
			PopulatePrograms(Me.Nodes, New IO.DirectoryInfo(GetFolderPath(FolderID.CommonPrograms)), , False)
			'Sort all of the items
			Me.Sort()
			'Allow the treeview to continue normal operation
			Me.EndUpdate()
			'Select the first node to scroll to the top
			Me.SelectedNode = Me.Nodes(0)
			'Set the cursor back to its normal default
			Me.Cursor = Cursors.Default
			'Don't update all items again unless necessary
			needProgramsUpdate = False
		End If
	End Sub

	Private Sub PopulatePrograms(ByVal nodeCollection As TreeNodeCollection, ByVal directory As IO.DirectoryInfo, Optional ByVal dirs As Boolean = True, Optional ByVal files As Boolean = True)
		If files Then
			For Each f As IO.FileInfo In directory.GetFiles
				Try
					If (f.Attributes And IO.FileAttributes.Hidden) > 0 Then Continue For
					If f.Extension = ".lnk" Then 'If the file is a shortcut
						Dim link As New ShellLink(f.FullName)
						Me.imlAllPrograms.Images.Add(f.FullName, link.SmallIcon.ToBitmap)
						Dim friendlyName As String = f.Name
						friendlyName = friendlyName.Remove(f.Name.Length - f.Extension.Length, f.Extension.Length)
						Dim n As TreeNode = nodeCollection.Add(f.FullName, friendlyName, f.FullName, f.FullName)
						n.ToolTipText = link.Description
					Else 'Otherwise, use the default icon, etc.
                        Me.imlAllPrograms.Images.Add(f.FullName, New ShellFileInfo(f.FullName, ShellFileInfo.SHGetFileInfoConstants.Icon Or ShellFileInfo.SHGetFileInfoConstants.SmallIcon).ShellIcon.ToBitmap)
						Dim friendlyName As String = f.Name
						friendlyName = friendlyName.Remove(f.Name.Length - f.Extension.Length, f.Extension.Length)
						nodeCollection.Add(f.FullName, friendlyName, f.FullName, f.FullName)
					End If
				Catch ex As Exception
					Debug.Print("An error occurred while adding " & f.Name & " to the start menu. " & ex.Message)
					'MessageBox.Show("An error occurred while adding " & f.Name & " to the start menu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				End Try
			Next
		End If
		If dirs Then
			For Each d As IO.DirectoryInfo In directory.GetDirectories
				If (d.Attributes And IO.FileAttributes.Hidden) > 0 Then Continue For
				'If d.Name = "Skype" Then Stop
				Dim itemExists As Boolean = False
				For Each n As TreeNode In nodeCollection
					If n.Text = d.Name AndAlso n.Nodes.Count > 0 Then
						n.Name = n.Name & "|" & d.FullName
						itemExists = True
						Exit For
					End If
				Next
				If Not itemExists Then
					Dim folderNode As New TreeNode(d.Name)
					folderNode.ImageKey = "Folder"
					folderNode.Name = "Folder-" & d.FullName
					folderNode.SelectedImageKey = "FolderSelected"
					nodeCollection.Add(folderNode)
					Dim dummyNode As New TreeNode("Dummy-" & d.Name)
					folderNode.Nodes.Add(dummyNode)
					'PopulatePrograms(folderNode.Nodes, d)
				End If
			Next
		End If
	End Sub

	Private Sub trvAllPrograms_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles Me.BeforeExpand
        Dim directoryNames As String = e.Node.Name
        directoryNames = directoryNames.Replace("Folder-", "")
        Dim directoryNameArr() As String
        If directoryNames.Contains("|") Then
            directoryNameArr = directoryNames.Split(New Char() {"|"})
        Else
            directoryNameArr = New String() {directoryNames}
        End If
        Me.BeginUpdate()
        e.Node.Nodes.Clear()
        For Each d As String In directoryNameArr
            Try
                PopulatePrograms(e.Node.Nodes, New IO.DirectoryInfo(d))
            Catch ex As Exception
                Debug.Print("Error expanding " & e.Node.Name & ". " & ex.Message)
            End Try
        Next

        Me.EndUpdate()

        If Not e.Node.Nodes.Count = 0 Then
            For Each n As TreeNode In e.Node.Nodes
                If n.Nodes.Count = 0 Then Exit Sub
            Next

            'The node only contains folder nodes, so expand the first one
            e.Node.Nodes(0).Expand()
        End If
    End Sub

	Private Sub trvAllPrograms_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
		Try
			If Not e.Node.Name.StartsWith("Folder-") Then
                Dim t As New Threading.Thread(AddressOf ShellExecute)
                t.Start(e.Node.Name)
				StartMenu.FadeOut()
			Else
				If e.Node.IsExpanded Then e.Node.Collapse() Else e.Node.Expand()
			End If
		Catch ex As Exception
			MessageBox.Show("An error occurred while opening the file. Make sure the file actually exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		End Try
	End Sub

	Private Sub fswAllSM_Changed(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles fswAllSM.Changed, fswAllSM.Created, fswAllSM.Deleted, fswUserSM.Changed, fswUserSM.Created, fswUserSM.Deleted
		needProgramsUpdate = True
	End Sub

	Private Sub fswAllSM_Renamed(ByVal sender As Object, ByVal e As System.IO.RenamedEventArgs) Handles fswAllSM.Renamed, fswUserSM.Renamed
		needProgramsUpdate = True
	End Sub

	Private Class StartMenuSorter
		Implements IComparer

		Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
			Dim node1 As TreeNode = CType(x, TreeNode)
			Dim node2 As TreeNode = CType(y, TreeNode)
			If (node1.Nodes.Count > 0 AndAlso node2.Nodes.Count > 0) OrElse (node1.Nodes.Count = 0 And node2.Nodes.Count = 0) Then
				Return StringComparer.CurrentCulture.Compare(node1.Text, node2.Text)
			ElseIf node1.Nodes.Count = 0 AndAlso node2.Nodes.Count > 0 Then
				Return -1
			Else
				Return 1
			End If
		End Function

	End Class

End Class
