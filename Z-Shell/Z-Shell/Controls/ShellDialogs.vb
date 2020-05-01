''' <summary>
''' Allows the user to select an icon resource.
''' </summary>
''' <remarks></remarks>
Public Class ChangeIconDialog
    Inherits CommonDialog

    Private Declare Auto Function SHChangeIconDialog Lib "shell32" Alias "#62" (ByVal hOwner As Long, ByRef szFilename As System.Text.StringBuilder, ByVal Reserved As Integer, ByRef lpIconIndex As Integer) As Integer
    'hOwner = Dialog owner, specify 0 for desktop 
    '(will be top-level)
    'szFilename = The initially displayed filename, filled 
    '             on selection. Should be allocated to 
    '             MAX_PATH (260) in order to receive the 
    '             selected filename's path.
    'Reserved = ?
    'lpIconIndex = Pointer to the initially displayed filename's 
    '              icon index, and is filled on icon selection.

    'Returns non-zero on select, zero if cancelled.

    Public Overrides Sub Reset()
        Filename = ""
        IconIndex = 0
    End Sub

    Protected Overrides Function RunDialog(ByVal hwndOwner As System.IntPtr) As Boolean
        Dim _newFilePath As New System.Text.StringBuilder(260)
        _newFilePath.Append(Filename)
        Dim _newIconIndex As Integer = IconIndex
        If SHChangeIconDialog(hwndOwner, _newFilePath, 0, _newIconIndex) <> 0 Then
            Filename = _newFilePath.ToString
            IconIndex = _newIconIndex
            Return True
        Else
            Return False
        End If
    End Function

    Dim _filePath As String

    ''' <summary>
    ''' The full path and name of the file that contains the selected icon resource.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Filename() As String
        Get
            Return _filePath
        End Get
        Set(ByVal value As String)
            _filePath = value
        End Set
    End Property

    Dim _iconIndex As Integer

    ''' <summary>
    ''' The index of the selected icon resource.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IconIndex() As Integer
        Get
            Return _iconIndex
        End Get
        Set(ByVal value As Integer)
            _iconIndex = value
        End Set
    End Property

End Class

''' <summary>
''' Used to display the system's Run dialog such as the one used from the start menu.
''' </summary>
''' <remarks></remarks>
Public Class RunDialog
	Inherits CommonDialog

	Public Overrides Sub Reset()
		_prompt = ""
		_title = ""
		_showLastCommand = True
	End Sub

	'----------------------------
	' The Run dialog via the Start menu
    Private Declare Function SHRunDialog Lib "shell32" Alias "#61" _
  (ByVal hOwner As Integer, _
   ByVal Unknown1 As Integer, _
   ByVal Unknown2 As Integer, _
   ByVal szTitle As String, _
   ByVal szPrompt As String, _
   ByVal uFlags As Integer) As Integer

	' hOwner = Dialog owner, specify 0 for desktop (will be top-level)
	' Unknown1 = ?
	' Unknown2 = ?, non-zero causes gpf! strings are ok...(?)
	' szTitle = Dialog title, specify vbNullString for default ("Run")
	' szPrompt = Dialog prompt, specify vbNullString for default ("Type the name...")

	' If uFlags is the following constant, the string from last program run
	' will not appear in the dialog's combo box (that's all I found...)

    Private Const shrdNoMRUString = &H2     ' 2nd bit is set

	' If there is some way to set & rtn the command line, I didn't find it...
	' Always returns 0 (?)

	Protected Overrides Function RunDialog(ByVal hwndOwner As System.IntPtr) As Boolean
		SHRunDialog(hwndOwner, 0, 0, _title, _prompt, IIf(_showLastCommand, 0, shrdNoMRUString))
		Return True
	End Function

	Dim _title As String

	''' <summary>
	''' Gets or sets the title for the dialog. If the string is empty, the system's default title is used (such as "Run...").
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property Title() As String
		Get
			Return _title
		End Get
		Set(ByVal value As String)
			_title = value
		End Set
	End Property

	Dim _prompt As String

	''' <summary>
	''' Gets or sets the prompt shown in the dialog. If the string is empty, the system's default prompt is used (such as "Please enter a command:").
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property Prompt() As String
		Get
			Return _prompt
		End Get
		Set(ByVal value As String)
			_prompt = value
		End Set
	End Property

	Dim _showLastCommand As Boolean = True

	''' <summary>
	''' Gets or sets whether or not to display the last command used in the textbox.
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property ShowLastCommand() As Boolean
		Get
			Return _showLastCommand
		End Get
		Set(ByVal value As Boolean)
			_showLastCommand = value
		End Set
	End Property

End Class

''' <summary>
''' Asks the user about performing a specified shutdown action. If the user consents, the action is performed.
''' </summary>
''' <remarks></remarks>
Public Class SystemSettingsRestartDialog
    Inherits CommonDialog

    Public Overrides Sub Reset()
        _type = SystemFunctions.ShutdownType.PowerOff
        _prompt = ""
    End Sub

    ' The "System Settings Change" message box.
    ' ("You must restart your computer before the new settings will take effect.")
    Private Declare Auto Function SHRestartSystemMB Lib "shell32" Alias "#59" _
     (ByVal hOwner As Integer, _
     ByVal sPrompt As String, _
     ByVal uFlags As Integer) As Integer

    ' hOwner = Message box owner, specify 0 for desktop (will be top-level)
    ' sPrompt = Specified prompt string placed above the default prompt.
    ' uFlags = Can be the following values:

    ' WinNT
    ' Appears to use ExitWindowsEx uFlags values and behave accordingly:
    Private Const EWX_LOGOFF = 0
    Private Const EWX_SHUTDOWN = 1   ' NT: needs SE_SHUTDOWN_NAME privilege (no def prompt)
    Private Const EWX_REBOOT = 2        ' NT: needs SE_SHUTDOWN_NAME privilege
    Private Const EWX_FORCE = 4
    Private Const EWX_POWEROFF = 8   ' NT: needs SE_SHUTDOWN_NAME privilege

    ' Win95
    ' Any Yes selection produces the eqivalent to ExitWindowsEx(EWX_FORCE, 0) (?)
    ' (i.e. no WM_QUERYENDSESSION or WM_ENDSESSION is sent!).
    ' Other than is noted below, it was found that any other value shuts the system down
    ' (no reboot) and includes the default prompt.

    ' Shuts the system down (no reboot) and does not include the default prompt:
    Private Const shrsExitNoDefPrompt = 1
    ' Reboots the system and includes the default prompt.
    Private Const shrsRebootSystem = 2   ' = EWX_REBOOT

    ' Rtn vals: Yes = 6 (vbYes), No = 7 (vbNo)

    Protected Overrides Function RunDialog(ByVal hwndOwner As System.IntPtr) As Boolean
        SHRestartSystemMB(hwndOwner, _prompt, CInt(_type))
        Return True
    End Function

    Dim _type As SystemFunctions.ShutdownType

    ''' <summary>
    ''' The type of shutdown to ask about
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShutDownType() As SystemFunctions.ShutdownType
        Get
            Return _type
        End Get
        Set(ByVal value As SystemFunctions.ShutdownType)
            _type = value
        End Set
    End Property

    Dim _prompt As String

    ''' <summary>
    ''' A string to be displayed above the default prompt.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Prompt() As String
        Get
            Return _prompt
        End Get
        Set(ByVal value As String)
            _prompt = value
        End Set
    End Property

End Class

Public Class ShutdownDialog
    Inherits CommonDialog

    ' The Shut Down dialog via the Start menu
    Private Declare Function SHShutDownDialog Lib "shell32" Alias "#60" _
     (ByVal YourGuess As Integer) As Integer

    Public Overrides Sub Reset()

    End Sub

    Protected Overrides Function RunDialog(ByVal hwndOwner As System.IntPtr) As Boolean
        SHShutDownDialog(0)
        Return True
    End Function

End Class