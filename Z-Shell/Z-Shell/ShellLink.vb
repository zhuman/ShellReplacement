Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Text

''' <summary>
''' This class provides a wrapper for the IShellLink interface. 
''' This class was originally taken from vbAccelerator.net and 
''' was translated to VB.NET and improved. Most notably, the 
''' methods for retrieving icons had a few problems.
''' </summary>
Public Class ShellLink
	Implements IDisposable

#Region "ComInterop for IShellLink"

#Region "IPersist Interface"
	<ComImportAttribute()> _
	<GuidAttribute("0000010C-0000-0000-C000-000000000046")> _
	<InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
	  Private Interface IPersist
		'[helpstring("Returns the class identifier for the component object")]
		<PreserveSig()> _
		 Sub GetClassID(ByRef pClassID As Guid)
	End Interface
#End Region

#Region "IPersistFile Interface"
	<ComImportAttribute()> _
	<GuidAttribute("0000010B-0000-0000-C000-000000000046")> _
	<InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
	  Private Interface IPersistFile
		' can't get this to go if I extend IPersist, so put it here:
		<PreserveSig()> _
		 Sub GetClassID(ByRef pClassID As Guid)

		'[helpstring("Checks for changes since last file write")]		
		Sub IsDirty()

		'[helpstring("Opens the specified file and initializes the object from its contents")]		
		Sub Load(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszFileName As String, ByVal dwMode As UInteger)

		'[helpstring("Saves the object into the specified file")]		
		Sub Save(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszFileName As String, <MarshalAs(UnmanagedType.Bool)> ByVal fRemember As Boolean)

		'[helpstring("Notifies the object that save is completed")]		
		Sub SaveCompleted(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszFileName As String)

		'[helpstring("Gets the current name of the file associated with the object")]		
		Sub GetCurFile(<MarshalAs(UnmanagedType.LPWStr)> ByRef ppszFileName As String)
	End Interface
#End Region

#Region "IShellLink Interface"
	<ComImportAttribute()> _
	  <GuidAttribute("000214EE-0000-0000-C000-000000000046")> _
	<InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
	Private Interface IShellLinkA
		'[helpstring("Retrieves the path and filename of a shell link object")]
		Sub GetPath(<Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszFile As StringBuilder, ByVal cchMaxPath As Integer, ByRef pfd As _WIN32_FIND_DATAA, ByVal fFlags As UInteger)

		'[helpstring("Retrieves the list of shell link item identifiers")]
		Sub GetIDList(ByRef ppidl As IntPtr)

		'[helpstring("Sets the list of shell link item identifiers")]
		Sub SetIDList(ByVal pidl As IntPtr)

		'[helpstring("Retrieves the shell link description string")]
		Sub GetDescription(<Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszFile As StringBuilder, ByVal cchMaxName As Integer)

		'[helpstring("Sets the shell link description string")]
		Sub SetDescription(<MarshalAs(UnmanagedType.LPStr)> ByVal pszName As String)

		'[helpstring("Retrieves the name of the shell link working directory")]
		Sub GetWorkingDirectory(<Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszDir As StringBuilder, ByVal cchMaxPath As Integer)

		'[helpstring("Sets the name of the shell link working directory")]
		Sub SetWorkingDirectory(<MarshalAs(UnmanagedType.LPStr)> ByVal pszDir As String)

		'[helpstring("Retrieves the shell link command-line arguments")]
		Sub GetArguments(<Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszArgs As StringBuilder, ByVal cchMaxPath As Integer)

		'[helpstring("Sets the shell link command-line arguments")]
		Sub SetArguments(<MarshalAs(UnmanagedType.LPStr)> ByVal pszArgs As String)

		'[propget, helpstring("Retrieves or sets the shell link hot key")]
		Sub GetHotkey(ByRef pwHotkey As Short)
		'[propput, helpstring("Retrieves or sets the shell link hot key")]
		Sub SetHotkey(ByVal pwHotkey As Short)

		'[propget, helpstring("Retrieves or sets the shell link show command")]
		Sub GetShowCmd(ByRef piShowCmd As UInteger)
		'[propput, helpstring("Retrieves or sets the shell link show command")]
		Sub SetShowCmd(ByVal piShowCmd As UInteger)

		'[helpstring("Retrieves the location (path and index) of the shell link icon")]
		Sub GetIconLocation(<Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszIconPath As StringBuilder, ByVal cchIconPath As Integer, ByRef piIcon As Integer)

		'[helpstring("Sets the location (path and index) of the shell link icon")]
		Sub SetIconLocation(<MarshalAs(UnmanagedType.LPStr)> ByVal pszIconPath As String, ByVal iIcon As Integer)

		'[helpstring("Sets the shell link relative path")]
		Sub SetRelativePath(<MarshalAs(UnmanagedType.LPStr)> ByVal pszPathRel As String, ByVal dwReserved As UInteger)

		'[helpstring("Resolves a shell link. The system searches for the shell link object and updates the shell link path and its list of identifiers (if necessary)")]
		Sub Resolve(ByVal hWnd As IntPtr, ByVal fFlags As UInteger)

		'[helpstring("Sets the shell link path and filename")]
		Sub SetPath(<MarshalAs(UnmanagedType.LPStr)> ByVal pszFile As String)
	End Interface


	<ComImportAttribute()> _
	<GuidAttribute("000214F9-0000-0000-C000-000000000046")> _
	<InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
   Private Interface IShellLinkW
		'[helpstring("Retrieves the path and filename of a shell link object")]
		Sub GetPath(<Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszFile As StringBuilder, ByVal cchMaxPath As Integer, ByRef pfd As _WIN32_FIND_DATAW, ByVal fFlags As UInteger)

		'[helpstring("Retrieves the list of shell link item identifiers")]
		Sub GetIDList(ByRef ppidl As IntPtr)

		'[helpstring("Sets the list of shell link item identifiers")]
		Sub SetIDList(ByVal pidl As IntPtr)

		'[helpstring("Retrieves the shell link description string")]
		Sub GetDescription(<Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszFile As StringBuilder, ByVal cchMaxName As Integer)

		'[helpstring("Sets the shell link description string")]
		Sub SetDescription(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszName As String)

		'[helpstring("Retrieves the name of the shell link working directory")]
		Sub GetWorkingDirectory(<Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszDir As StringBuilder, ByVal cchMaxPath As Integer)

		'[helpstring("Sets the name of the shell link working directory")]
		Sub SetWorkingDirectory(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszDir As String)

		'[helpstring("Retrieves the shell link command-line arguments")]
		Sub GetArguments(<Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszArgs As StringBuilder, ByVal cchMaxPath As Integer)

		'[helpstring("Sets the shell link command-line arguments")]
		Sub SetArguments(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszArgs As String)

		'[propget, helpstring("Retrieves or sets the shell link hot key")]
		Sub GetHotkey(ByRef pwHotkey As Short)
		'[propput, helpstring("Retrieves or sets the shell link hot key")]
		Sub SetHotkey(ByVal pwHotkey As Short)

		'[propget, helpstring("Retrieves or sets the shell link show command")]
		Sub GetShowCmd(ByRef piShowCmd As UInteger)
		'[propput, helpstring("Retrieves or sets the shell link show command")]
		Sub SetShowCmd(ByVal piShowCmd As UInteger)

		'[helpstring("Retrieves the location (path and index) of the shell link icon")]
		Sub GetIconLocation(<Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszIconPath As StringBuilder, ByVal cchIconPath As Integer, ByRef piIcon As Integer)

		'[helpstring("Sets the location (path and index) of the shell link icon")]
		Sub SetIconLocation(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszIconPath As String, ByVal iIcon As Integer)

		'[helpstring("Sets the shell link relative path")]
		Sub SetRelativePath(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszPathRel As String, ByVal dwReserved As UInteger)

		'[helpstring("Resolves a shell link. The system searches for the shell link object and updates the shell link path and its list of identifiers (if necessary)")]
		Sub Resolve(ByVal hWnd As IntPtr, ByVal fFlags As UInteger)

		'[helpstring("Sets the shell link path and filename")]
		Sub SetPath(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszFile As String)
	End Interface

#End Region

#Region "ShellLinkCoClass"
	<GuidAttribute("00021401-0000-0000-C000-000000000046")> _
	<ClassInterfaceAttribute(ClassInterfaceType.None)> _
	<ComImportAttribute()> _
	Private Class CShellLink

	End Class

#End Region

#Region "Private IShellLink enumerations"

	Private Enum EShellLinkGP As UInteger
		SLGP_SHORTPATH = 1
		SLGP_UNCPRIORITY = 2
	End Enum

	<Flags()> _
	  Private Enum EShowWindowFlags As UInteger
		SW_HIDE = 0
		SW_SHOWNORMAL = 1
		SW_NORMAL = 1
		SW_SHOWMINIMIZED = 2
		SW_SHOWMAXIMIZED = 3
		SW_MAXIMIZE = 3
		SW_SHOWNOACTIVATE = 4
		SW_SHOW = 5
		SW_MINIMIZE = 6
		SW_SHOWMINNOACTIVE = 7
		SW_SHOWNA = 8
		SW_RESTORE = 9
		SW_SHOWDEFAULT = 10
		SW_MAX = 10
	End Enum

#End Region

#Region "IShellLink Private structs"

	<StructLayoutAttribute(LayoutKind.Sequential, Pack:=4, Size:=0, CharSet:=CharSet.Unicode)> _
	Private Structure _WIN32_FIND_DATAW
		Public dwFileAttributes As UInteger
		Public ftCreationTime As _FILETIME
		Public ftLastAccessTime As _FILETIME
		Public ftLastWriteTime As _FILETIME
		Public nFileSizeHigh As UInteger
		Public nFileSizeLow As UInteger
		Public dwReserved0 As UInteger
		Public dwReserved1 As UInteger
		'260 = MAX_PATH
		<MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
		Public cFileName As String
		<MarshalAs(UnmanagedType.ByValTStr, SizeConst:=14)> _
		Public cAlternateFileName As String
	End Structure

	<StructLayoutAttribute(LayoutKind.Sequential, Pack:=4, Size:=0, CharSet:=CharSet.Ansi)> _
	Private Structure _WIN32_FIND_DATAA
		Public dwFileAttributes As UInteger
		Public ftCreationTime As _FILETIME
		Public ftLastAccessTime As _FILETIME
		Public ftLastWriteTime As _FILETIME
		Public nFileSizeHigh As UInteger
		Public nFileSizeLow As UInteger
		Public dwReserved0 As UInteger
		Public dwReserved1 As UInteger
		'260 = MAX_PATH
		<MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
		Public cFileName As String
		<MarshalAs(UnmanagedType.ByValTStr, SizeConst:=14)> _
		Public cAlternateFileName As String
	End Structure

	<StructLayoutAttribute(LayoutKind.Sequential, Pack:=4, Size:=0)> _
	Private Structure _FILETIME
		Public dwLowDateTime As UInteger
		Public dwHighDateTime As UInteger
	End Structure

#End Region

#Region "UnManaged Methods"
	Private Class UnManagedMethods
		<DllImport("Shell32", CharSet:=CharSet.Auto)> _
		 Friend Shared Function ExtractIconEx(<MarshalAs(UnmanagedType.LPTStr)> ByVal lpszFile As String, ByVal nIconIndex As Integer, ByVal phIconLarge() As IntPtr, ByVal phIconSmall() As IntPtr, ByVal nIcons As Integer) As Integer
		End Function

		<DllImport("user32")> _
		Friend Shared Function DestroyIcon(ByVal hIcon As IntPtr) As Integer
		End Function

	End Class

#End Region

#End Region

#Region "Enumerations"
	''' <summary>
	''' Flags determining how the links with missing
	''' targets are resolved.
	''' </summary>
	<Flags()> _
	Public Enum EShellLinkResolveFlags As UInteger
		''' <summary>
		''' Allow any match during resolution.  Has no effect
		''' on ME/2000 or above, use the other flags instead.
		''' </summary>
		SLR_ANY_MATCH = &H2
		''' <summary>
		''' Call the Microsoft Windows Installer. 
		''' </summary>
		SLR_INVOKE_MSI = &H80
		''' <summary>
		''' Disable distributed link tracking. By default, 
		''' distributed link tracking tracks removable media 
		''' across multiple devices based on the volume name. 
		''' It also uses the UNC path to track remote file 
		''' systems whose drive letter has changed. Setting 
		''' SLR_NOLINKINFO disables both types of tracking.
		''' </summary>
		SLR_NOLINKINFO = &H40
		''' <summary>
		''' Do not display a dialog box if the link cannot be resolved. 
		''' When SLR_NO_UI is set, a time-out value that specifies the 
		''' maximum amount of time to be spent resolving the link can 
		''' be specified in milliseconds. The function returns if the 
		''' link cannot be resolved within the time-out duration. 
		''' If the timeout is not set, the time-out duration will be 
		''' set to the default value of 3,000 milliseconds (3 seconds). 
		''' </summary>										    
		SLR_NO_UI = &H1
		''' <summary>
		''' Not documented in SDK.  Assume same as SLR_NO_UI but 
		''' intended for applications without a hWnd.
		''' </summary>
		SLR_NO_UI_WITH_MSG_PUMP = &H101
		''' <summary>
		''' Do not update the link information. 
		''' </summary>
		SLR_NOUPDATE = &H8
		''' <summary>
		''' Do not execute the search heuristics. 
		''' </summary>																																																																																																																																																																																																														
		SLR_NOSEARCH = &H10
		''' <summary>
		''' Do not use distributed link tracking. 
		''' </summary>
		SLR_NOTRACK = &H20
		''' <summary>
		''' If the link object has changed, update its path and list 
		''' of identifiers. If SLR_UPDATE is set, you do not need to 
		''' call IPersistFile::IsDirty to determine whether or not 
		''' the link object has changed. 
		''' </summary>
		SLR_UPDATE = &H4
	End Enum

	Public Enum LinkDisplayMode As UInteger
		edmNormal = EShowWindowFlags.SW_NORMAL
		edmMinimized = EShowWindowFlags.SW_SHOWMINNOACTIVE
		edmMaximized = EShowWindowFlags.SW_MAXIMIZE
	End Enum

#End Region

#Region "Member Variables"
	' Use Unicode (W) under NT, otherwise use ANSI		
	Private linkW As IShellLinkW
	Private linkA As IShellLinkA
	Private _shortcutFile As String = ""
#End Region

#Region "Constructor"
	''' <summary>
	''' Creates an instance of the Shell Link object.
	''' </summary>
	Public Sub New()
		If (System.Environment.OSVersion.Platform = PlatformID.Win32NT) Then
			linkW = CType(New CShellLink(), IShellLinkW)
		Else
			linkA = CType(New CShellLink(), IShellLinkA)
		End If
	End Sub

	''' <summary>
	''' Creates an instance of a Shell Link object
	''' from the specified link file
	''' </summary>
	''' <param name="linkFile">The Shortcut file to open</param>
	Public Sub New(ByVal linkFile As String)
		Me.New()
		Open(linkFile)
	End Sub

#End Region

#Region "Destructor and Dispose"
	''' <summary>
	''' Call dispose just in case it hasn't happened yet
	''' </summary>
	Protected Overrides Sub Finalize()
		Dispose()
	End Sub

	''' <summary>
	''' Dispose the object, releasing the COM ShellLink object
	''' </summary>
	Public Sub Dispose() Implements IDisposable.Dispose
		If (linkW IsNot Nothing) Then
			Marshal.ReleaseComObject(linkW)
			linkW = Nothing
		End If
		If (linkA IsNot Nothing) Then
			Marshal.ReleaseComObject(linkA)
			linkA = Nothing
		End If
	End Sub
#End Region

#Region "Implementation"

	Public Property ShortCutFile() As String
		Get
			Return Me._shortcutFile
		End Get
		Set(ByVal value As String)
			Me._shortcutFile = value
		End Set
	End Property

	''' <summary>
	''' Gets a System.Drawing.Icon containing the icon for this
	''' ShellLink object.
	''' </summary>
	Public ReadOnly Property LargeIcon() As Icon
		Get
			Return getIcon(True)
		End Get
	End Property

	Public ReadOnly Property SmallIcon() As Icon
		Get
			Return getIcon(False)
		End Get
	End Property

	Private Function getIcon(ByVal large As Boolean) As Icon
		' Get icon index and path:
		Dim iconIndex As Integer = 0
		Dim iconPath As StringBuilder = New StringBuilder(260, 260)
		If (linkA Is Nothing) Then
			linkW.GetIconLocation(iconPath, iconPath.Capacity, iconIndex)
		Else
			linkA.GetIconLocation(iconPath, iconPath.Capacity, iconIndex)
		End If
		Dim iconFile As String = iconPath.ToString()

		' If there are no details set for the icon, then we must use
		' the shell to get the icon for the target:
		If (iconFile.Length = 0) Then
			' Use the FileIcon object to get the icon:
			Dim flags As ShellFileInfo.SHGetFileInfoConstants = ShellFileInfo.SHGetFileInfoConstants.ICON Or ShellFileInfo.SHGetFileInfoConstants.ATTRIBUTES
			If (large) Then
				flags = flags Or ShellFileInfo.SHGetFileInfoConstants.LargeIcon
			Else
				flags = flags Or ShellFileInfo.SHGetFileInfoConstants.SmallIcon
			End If
			Dim _fileIcon As ShellFileInfo = New ShellFileInfo(Target, flags)
			Return _fileIcon.ShellIcon
		Else
			' Use ExtractIconEx to get the icon:
			Dim hIconEx() As IntPtr = New IntPtr() {IntPtr.Zero}
			Dim iconCount As Integer = 0
			If (large) Then
				iconCount = UnManagedMethods.ExtractIconEx(iconFile, iconIndex, hIconEx, Nothing, 1)
			Else
				iconCount = UnManagedMethods.ExtractIconEx(iconFile, iconIndex, Nothing, hIconEx, 1)
			End If
			' If success then return as a GDI+ object
			Dim icon As Icon = Nothing
			If (hIconEx(0) <> IntPtr.Zero) Then
				icon = icon.FromHandle(hIconEx(0))
				'UnManagedMethods.DestroyIcon(hIconEx[0]);
			Else 'Neither the target, nor the icon path actually exist
				' Use the FileIcon object to get the generic shortcut icon:
				Dim flags As ShellFileInfo.SHGetFileInfoConstants = ShellFileInfo.SHGetFileInfoConstants.ICON Or ShellFileInfo.SHGetFileInfoConstants.ATTRIBUTES
				If (large) Then
					flags = flags Or ShellFileInfo.SHGetFileInfoConstants.LargeIcon
				Else
					flags = flags Or ShellFileInfo.SHGetFileInfoConstants.SmallIcon
				End If
				Dim _fileIcon As ShellFileInfo = New ShellFileInfo(Me.ShortCutFile, flags)
				Return _fileIcon.ShellIcon
			End If
			Return icon
		End If
	End Function

	''' <summary>
	''' Gets the path to the file containing the icon for this shortcut.
	''' </summary>
	Public Property IconPath() As String
		Get
			Dim _iconPath As StringBuilder = New StringBuilder(260, 260)
			Dim iconIndex As Integer = 0
			If (linkA Is Nothing) Then
				linkW.GetIconLocation(_iconPath, _iconPath.Capacity, iconIndex)
			Else
				linkA.GetIconLocation(_iconPath, _iconPath.Capacity, iconIndex)
			End If
			Return _iconPath.ToString()
		End Get
		Set(ByVal value As String)
			Dim _iconPath As StringBuilder = New StringBuilder(260, 260)
			Dim iconIndex As Integer = 0
			If (linkA Is Nothing) Then
				linkW.GetIconLocation(_iconPath, _iconPath.Capacity, iconIndex)
			Else
				linkA.GetIconLocation(_iconPath, _iconPath.Capacity, iconIndex)
			End If
			If (linkA Is Nothing) Then
				linkW.SetIconLocation(value, iconIndex)
			Else
				linkA.SetIconLocation(value, iconIndex)
			End If
		End Set
	End Property

	''' <summary>
	''' Gets the index of this icon within the icon path's resources
	''' </summary>
	Public Property IconIndex() As Integer
		Get
			Dim iconPath As StringBuilder = New StringBuilder(260, 260)
			Dim _iconIndex As Integer = 0
			If (linkA Is Nothing) Then
				linkW.GetIconLocation(iconPath, iconPath.Capacity, _iconIndex)
			Else
				linkA.GetIconLocation(iconPath, iconPath.Capacity, _iconIndex)
			End If
			Return _iconIndex
		End Get
		Set(ByVal value As Integer)
			Dim iconPath As StringBuilder = New StringBuilder(260, 260)
			Dim _iconIndex As Integer = 0
			If (linkA Is Nothing) Then
				linkW.GetIconLocation(iconPath, iconPath.Capacity, _iconIndex)
			Else
				linkA.GetIconLocation(iconPath, iconPath.Capacity, _iconIndex)
			End If
			If (linkA Is Nothing) Then
				linkW.SetIconLocation(iconPath.ToString(), value)
			Else
				linkA.SetIconLocation(iconPath.ToString(), value)
			End If
		End Set
	End Property

	''' <summary>
	''' Gets/sets the fully qualified path to the link's target
	''' </summary>
	Public Property Target() As String
		Get
			Dim _target As StringBuilder = New StringBuilder(260, 260)
			If (linkA Is Nothing) Then
				Dim fd As _WIN32_FIND_DATAW = New _WIN32_FIND_DATAW()
				linkW.GetPath(_target, _target.Capacity, fd, CType(EShellLinkGP.SLGP_UNCPRIORITY, UInteger))
			Else
				Dim fd As _WIN32_FIND_DATAA = New _WIN32_FIND_DATAA()
				linkA.GetPath(_target, _target.Capacity, fd, CType(EShellLinkGP.SLGP_UNCPRIORITY, UInteger))
			End If
			Return _target.ToString()
		End Get
		Set(ByVal value As String)
			If (linkA Is Nothing) Then
				linkW.SetPath(value)
			Else
				linkA.SetPath(value)
			End If
		End Set
	End Property

	''' <summary>
	''' Gets/sets the Working Directory for the Link
	''' </summary>
	Public Property WorkingDirectory() As String
		Get
			Dim path As StringBuilder = New StringBuilder(260, 260)
			If (linkA Is Nothing) Then
				linkW.GetWorkingDirectory(path, path.Capacity)
			Else
				linkA.GetWorkingDirectory(path, path.Capacity)
			End If
			Return path.ToString()
		End Get
		Set(ByVal value As String)
			If (linkA Is Nothing) Then
				linkW.SetWorkingDirectory(value)
			Else
				linkA.SetWorkingDirectory(value)
			End If
		End Set
	End Property

	''' <summary>
	''' Gets/sets the description of the link
	''' </summary>
	Public Property Description() As String
		Get
			Dim _description As StringBuilder = New StringBuilder(1024, 1024)
			If (linkA Is Nothing) Then
				linkW.GetDescription(_description, _description.Capacity)
			Else
				linkA.GetDescription(_description, _description.Capacity)
			End If
			Return _description.ToString()
		End Get
		Set(ByVal value As String)
			If (linkA Is Nothing) Then
				linkW.SetDescription(value)
			Else
				linkA.SetDescription(value)
			End If
		End Set
	End Property

	''' <summary>
	''' Gets/sets any command line arguments associated with the link
	''' </summary>
	Public Property Arguments() As String
		Get
			Dim _arguments As StringBuilder = New StringBuilder(260, 260)
			If (linkA Is Nothing) Then
				linkW.GetArguments(_arguments, _arguments.Capacity)
			Else
				linkA.GetArguments(_arguments, _arguments.Capacity)
			End If
			Return _arguments.ToString()
		End Get
		Set(ByVal value As String)
			If (linkA Is Nothing) Then
				linkW.SetArguments(value)
			Else
				linkA.SetArguments(value)
			End If
		End Set
	End Property

	''' <summary>
	''' Gets/sets the initial display mode when the shortcut is
	''' run
	''' </summary>
	Public Property DisplayMode() As LinkDisplayMode
		Get
			Dim cmd As UInteger = 0
			If (linkA Is Nothing) Then
				linkW.GetShowCmd(cmd)
			Else
				linkA.GetShowCmd(cmd)
			End If
			Return CType(cmd, LinkDisplayMode)
		End Get
		Set(ByVal value As LinkDisplayMode)
			If (linkA Is Nothing) Then
				linkW.SetShowCmd(CType(value, UInteger))
			Else
				linkA.SetShowCmd(CType(value, UInteger))
			End If
		End Set
	End Property

	''' <summary>
	''' Gets/sets the HotKey to start the shortcut (if any)
	''' </summary>
	Public Property HotKey() As Keys
		Get
			Dim key As Short = 0
			If (linkA Is Nothing) Then
				linkW.GetHotkey(key)
			Else
				linkA.GetHotkey(key)
			End If
			Return CType(key, Keys)
		End Get
		Set(ByVal value As Keys)
			If (linkA Is Nothing) Then
				linkW.SetHotkey(CType(value, Short))
			Else
				linkA.SetHotkey(CType(value, Short))
			End If
		End Set
	End Property

	''' <summary>
	''' Saves the shortcut to ShortCutFile.
	''' </summary>
	Public Sub Save()
		Save(ShortCutFile)
	End Sub

	''' <summary>
	''' Saves the shortcut to the specified file
	''' </summary>
	''' <param name="linkFile">The shortcut file (.lnk)</param>
	Public Sub Save(ByVal linkFile As String)
		' Save the object to disk
		If (linkA Is Nothing) Then
			CType(linkW, IPersistFile).Save(linkFile, True)
			ShortCutFile = linkFile
		Else
			CType(linkA, IPersistFile).Save(linkFile, True)
			ShortCutFile = linkFile
		End If
	End Sub

	''' <summary>
	''' Loads a shortcut from the specified file
	''' </summary>
	''' <param name="linkFile">The shortcut file (.lnk) to load</param>
	Public Sub Open(ByVal linkFile As String)
		Open(linkFile, IntPtr.Zero, (EShellLinkResolveFlags.SLR_ANY_MATCH Or EShellLinkResolveFlags.SLR_NO_UI), 1)
	End Sub

	''' <summary>
	''' Loads a shortcut from the specified file, and allows flags controlling
	''' the UI behaviour if the shortcut's target isn't found to be set.
	''' </summary>
	''' <param name="linkFile">The shortcut file (.lnk) to load</param>
	''' <param name="hWnd">The window handle of the application's UI, if any</param>
	''' <param name="resolveFlags">Flags controlling resolution behaviour</param>
	Public Sub Open(ByVal linkFile As String, ByVal hWnd As IntPtr, ByVal resolveFlags As EShellLinkResolveFlags)
		Open(linkFile, hWnd, resolveFlags, 1)
	End Sub

	''' <summary>
	''' Loads a shortcut from the specified file, and allows flags controlling
	''' the UI behaviour if the shortcut's target isn't found to be set.  If
	''' no SLR_NO_UI is specified, you can also specify a timeout.
	''' </summary>
	''' <param name="linkFile">The shortcut file (.lnk) to load</param>
	''' <param name="hWnd">The window handle of the application's UI, if any</param>
	''' <param name="resolveFlags">Flags controlling resolution behaviour</param>
	''' <param name="timeOut">Timeout if SLR_NO_UI is specified, in ms.</param>
	Public Sub Open(ByVal linkFile As String, ByVal hWnd As IntPtr, ByVal resolveFlags As EShellLinkResolveFlags, ByVal timeOut As UShort)
		Dim flags As UInteger

		If ((resolveFlags And EShellLinkResolveFlags.SLR_NO_UI) = EShellLinkResolveFlags.SLR_NO_UI) Then
			flags = CType((CInt(resolveFlags) Or (timeOut << 16)), UInteger)
		Else
			flags = CType(resolveFlags, UInteger)
		End If

		If (linkA Is Nothing) Then
			CType(linkW, IPersistFile).Load(linkFile, 0) 'STGM_DIRECT)
			linkW.Resolve(hWnd, flags)
			Me.ShortCutFile = linkFile
		Else
			CType(linkA, IPersistFile).Load(linkFile, 0) 'STGM_DIRECT)
			linkA.Resolve(hWnd, flags)
			Me.ShortCutFile = linkFile
		End If
	End Sub

#End Region

End Class