Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Text

''' <summary>
''' Enables extraction of icons for any file type from
''' the Shell.
''' </summary>
Public Class ShellFileInfo

#Region "UnmanagedCode"

    Private Const MAX_PATH As Integer = 260

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure SHFILEINFO
        Public hIcon As IntPtr
        Public iIcon As Integer
        Public dwAttributes As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)> _
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
        Public szTypeName As String
    End Structure

    <DllImport("shell32")> _
    Private Shared Function SHGetFileInfo(ByVal pszPath As String, ByVal dwFileAttributes As Integer, ByRef psfi As SHFILEINFO, ByVal cbFileInfo As UInteger, ByVal uFlags As UInteger) As Integer
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function DestroyIcon(ByVal hIcon As IntPtr) As Integer
    End Function

    Private Const FORMAT_MESSAGE_ALLOCATE_BUFFER As Integer = &H100
    Private Const FORMAT_MESSAGE_ARGUMENT_ARRAY As Integer = &H2000
    Private Const FORMAT_MESSAGE_FROM_HMODULE As Integer = &H800
    Private Const FORMAT_MESSAGE_FROM_STRING As Integer = &H400
    Private Const FORMAT_MESSAGE_FROM_SYSTEM As Integer = &H1000
    Private Const FORMAT_MESSAGE_IGNORE_INSERTS As Integer = &H200
    Private Const FORMAT_MESSAGE_MAX_WIDTH_MASK As Integer = &HFF

    <DllImport("kernel32")> _
    Private Shared Function FormatMessage(ByVal dwFlags As Integer, ByVal lpSource As IntPtr, ByVal dwMessageId As Integer, ByVal dwLanguageId As Integer, ByVal lpBuffer As String, ByVal nSize As UInteger, ByVal argumentsLong As Integer) As Integer
    End Function

    <DllImport("kernel32")> _
    Private Shared Function GetLastError() As Integer
    End Function

#End Region

#Region "Member Variables"
	Private _fileName As String
	Private _displayName As String
	Private _typeName As String
	Private _flags As SHGetFileInfoConstants
	Private _fileIcon As Icon
#End Region

#Region "Enumerations"

	<Flags()> _
	 Public Enum SHGetFileInfoConstants As Integer
		Icon = &H100				  ' get icon 
		DisplayName = &H200		  ' get display name 
		TypeName = &H400			  ' get type name 
		Attributes = &H800		  ' get attributes 
		IconLocation = &H1000		  ' get icon location 
		ExeType = &H2000			  ' return exe type 
		SystemIconIndex = &H4000		  ' get system icon index 
		LinkOverlay = &H8000		  ' put a link overlay on icon 
		Selected = &H10000		  ' show icon in selected state 
		AttributesSpecified = &H20000	  ' get only specified attributes 
		LargeIcon = &H0			  ' get large icon 
		SmallIcon = &H1			  ' get small icon 
		OpenIcon = &H2			  ' get open icon 
		ShellIconSize = &H4		  ' get shell size icon 
		'SHGFI_PIDL = &h8                  ' pszPath is a pidl 
		UseFileAttributes = &H10	   ' use passed dwFileAttribute 
		AddOverlays = &H20	 ' apply the appropriate overlays
		OverlayIndex = &H40	  ' Get the index of the overlay
	End Enum

#End Region

#Region "Implementation"

    ''' <summary>
    ''' Gets/sets the flags used to extract the icon
    ''' </summary>
    Public Property Flags() As ShellFileInfo.SHGetFileInfoConstants
        Get
            Return _flags
        End Get
        Set(ByVal Value As ShellFileInfo.SHGetFileInfoConstants)
            _flags = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets/sets the filename to get the icon for
    ''' </summary>
    Public Property FileName() As String
        Get
            Return _fileName
        End Get
        Set(ByVal Value As String)
            _fileName = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets the icon for the chosen file
    ''' </summary>
    Public ReadOnly Property ShellIcon() As Icon
        Get
            Return _fileIcon
        End Get
    End Property

    ''' <summary>
    ''' Gets the display name for the selected file
    ''' if the SHGFI_DISPLAYNAME flag was set.
    ''' </summary>
    Public ReadOnly Property DisplayName() As String
        Get
            Return _displayName
        End Get
    End Property

    ''' <summary>
    ''' Gets the type name for the selected file
    ''' if the SHGFI_TYPENAME flag was set.
    ''' </summary>
    Public ReadOnly Property TypeName() As String
        Get
            Return _typeName
        End Get
    End Property

    ''' <summary>
    '''  Gets the information for the specified 
    '''  file name and flags.
    ''' </summary>
    Public Sub GetInfo()
        _fileIcon = Nothing
        _typeName = ""
        _displayName = ""

        Dim shfi As SHFILEINFO = New SHFILEINFO()
        Dim shfiSize As UInteger = CType(Marshal.SizeOf(shfi.GetType()), UInteger)

        Dim ret As Integer = SHGetFileInfo(_fileName, 0, shfi, shfiSize, CType(_flags, UInteger))
        If (ret <> 0) Then
            If (shfi.hIcon <> IntPtr.Zero) Then
                _fileIcon = System.Drawing.Icon.FromHandle(shfi.hIcon)
                ' Now owned by the GDI+ object
                'DestroyIcon(shfi.hIcon);
            End If
            _typeName = shfi.szTypeName
            _displayName = shfi.szDisplayName
        Else
            Dim err As Integer = GetLastError()
            Console.WriteLine("Error {0}", err)
            Dim txtS As String = New String("\0", 256)
            Dim len As Integer = FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM Or FORMAT_MESSAGE_IGNORE_INSERTS, IntPtr.Zero, err, 0, txtS, 256, 0)
            Console.WriteLine("Len {0} text {1}", len, txtS)
            'throw exception

        End If
    End Sub

    ''' <summary>
    ''' Constructs a new, default instance of the FileIcon
    ''' class.  Specify the filename and call GetInfo()
    ''' to retrieve an icon.
    ''' </summary>
    Public Sub New()
        _flags = SHGetFileInfoConstants.Icon Or SHGetFileInfoConstants.DisplayName Or SHGetFileInfoConstants.TypeName Or SHGetFileInfoConstants.Attributes Or SHGetFileInfoConstants.ExeType
    End Sub

    ''' <summary>
    ''' Constructs a new instance of the FileIcon class
    ''' and retrieves the icon, display name and type name
    ''' for the specified file.		
    ''' </summary>
    ''' <param name="fileName">The filename to get the icon, 
    ''' display name and type name for</param>
    Public Sub New(ByVal fileName As String)
        Me.New()
        Me._fileName = fileName
        GetInfo()
    End Sub

    ''' <summary>
    ''' Constructs a new instance of the FileIcon class
    ''' and retrieves the information specified in the 
    ''' flags.
    ''' </summary>
    ''' <param name="fileName">The filename to get information
    ''' for</param>
    ''' <param name="flags">The flags to use when extracting the
    ''' icon and other shell information.</param>
    Public Sub New(ByVal fileName As String, ByVal flags As ShellFileInfo.SHGetFileInfoConstants)
        Me._fileName = fileName
        Me._flags = flags
        GetInfo()
    End Sub

#End Region

End Class