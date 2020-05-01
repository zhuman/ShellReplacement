Imports Microsoft.Win32
Imports System.Runtime.InteropServices
Imports System.ComponentModel

Public Module FileActions

	Public Class RegistryFileInfo

		Dim _actions As New Specialized.StringDictionary

		Public ReadOnly Property Actions() As Specialized.StringDictionary
			Get
				Return _actions
			End Get
		End Property

		Dim _defAct As String

		Public Property DefaultAction() As String
			Get
				Return _defAct
			End Get
			Set(ByVal value As String)
				_defAct = value
			End Set
		End Property

		Dim _name As String

		Public Property Name() As String
			Get
				Return _name
			End Get
			Set(ByVal value As String)
				_name = value
			End Set
		End Property

		Dim _defIcon As String

		Public Property DefaultIcon() As String
			Get
				Return _defIcon
			End Get
			Set(ByVal value As String)
				_defIcon = value
			End Set
		End Property

		Private Declare Auto Function ExtractIcon Lib "shell32.dll" (ByVal hInst As IntPtr, ByVal lpszExeFileName As String, ByVal nIconIndex As Integer) As Integer

		Public ReadOnly Property Icon() As Icon
			Get
				If IconFilePath = "" Then Return SystemIcons.Application
				Return Drawing.Icon.FromHandle(ExtractIcon(Runtime.InteropServices.Marshal.GetHINSTANCE(GetType(FileActions).Module), IconFilePath, IconIndex))
			End Get
		End Property

		Public ReadOnly Property IconFilePath() As String
			Get
				Try
					If DefaultIcon = "" Then
						Return ParseProgramArgList(Actions(DefaultAction))(0)
					End If
					Dim lastSep As Integer = DefaultIcon.LastIndexOfAny(New Char() {","})
					If lastSep = -1 Then Return DefaultIcon
					Return DefaultIcon.Substring(0, lastSep)
				Catch ex As Exception
					Return ""
				End Try
			End Get
		End Property

		Public ReadOnly Property IconIndex() As Integer
			Get
				Try
					If DefaultIcon = "" Then Return 0
					Dim lastSep As Integer = DefaultIcon.LastIndexOfAny(New Char() {",", "-"})
					If lastSep = -1 Then Return 0
					Return Integer.Parse(DefaultIcon.Substring(lastSep + 1))
				Catch ex As Exception
					Return 0
				End Try
			End Get
		End Property

	End Class

	Public Function GetRegKeyActions(ByVal key As RegistryKey) As RegistryFileInfo
		Dim fInfo As New RegistryFileInfo
		fInfo.Name = key.GetValue("")
		Dim defIconKey As RegistryKey = key.OpenSubKey("DefaultIcon")
		If defIconKey IsNot Nothing Then fInfo.DefaultIcon = defIconKey.GetValue("")

		Dim shellKey As RegistryKey = key.OpenSubKey("shell")
		Dim actions() As String = shellKey.GetSubKeyNames()
		For Each s As String In actions
			Dim actionKey As RegistryKey = shellKey.OpenSubKey(s)
			Dim commandKey As RegistryKey = actionKey.OpenSubKey("command")
			If commandKey IsNot Nothing Then
				If shellKey.GetValue("", Nothing) IsNot Nothing Then
					If shellKey.GetValue("") = s Then
						fInfo.DefaultAction = s
					End If
				End If
				If fInfo.DefaultAction = "" Then
					fInfo.DefaultAction = s
				End If
				Dim commandString As String = commandKey.GetValue("")
				fInfo.Actions.Add(s, commandString)
			End If
		Next
		If fInfo.DefaultIcon = "" Then
			Try
				'fInfo.DefaultIcon = ParseProgramArgList(fInfo.DefaultAction)(0)
			Catch ex As Exception

			End Try
		End If
		Return fInfo
	End Function

	Public Function GetDefaultMailProgram() As RegistryFileInfo
		Dim userMailClients As RegistryKey = Nothing
		Dim defProg As String = ""

		userMailClients = Registry.CurrentUser.OpenSubKey("Software\Clients\Mail")
		If userMailClients IsNot Nothing Then defProg = userMailClients.GetValue("")

		Dim localMailClients As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Clients\Mail")
		If localMailClients IsNot Nothing Then
			If defProg = "" Then defProg = localMailClients.GetValue("")
			For Each s As String In localMailClients.GetSubKeyNames
				If s = defProg Then
					Return GetRegKeyActions(localMailClients.OpenSubKey(s))
				End If
			Next
		End If
		Return Nothing 'Nothing was found
	End Function

	Public Function GetDefaultInternetProgram() As RegistryFileInfo
		Dim userInternetClients As RegistryKey = Nothing
		Dim defProg As String = ""

		userInternetClients = Registry.CurrentUser.OpenSubKey("Software\Clients\StartMenuInternet")
		If userInternetClients IsNot Nothing Then defProg = userInternetClients.GetValue("")

		Dim localInternetClients As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Clients\StartMenuInternet")
		If localInternetClients IsNot Nothing Then
			If defProg = "" Then defProg = localInternetClients.GetValue("")
			For Each s As String In localInternetClients.GetSubKeyNames
				If s = defProg Then
					Return GetRegKeyActions(localInternetClients.OpenSubKey(s))
				End If
			Next
		End If
		Return Nothing 'Nothing was found
	End Function

	Public Function ParseProgramArgList(ByVal cmd As String) As String()
		Dim list As New Specialized.StringCollection
		cmd = cmd.Trim() & " "
		'cmd = cmd.Replace(""" ", """")
		'cmd = cmd.Replace(" ", "  ")
		Dim lastQuote As Integer = -1
		Dim selectedChar As Char
		While FirstIndexOf(cmd, New Char() {"""", " "}, lastQuote + 1, selectedChar) + 1 > lastQuote
			If cmd.IndexOf(selectedChar, cmd.IndexOf(selectedChar, lastQuote + 1) + 1) > lastQuote Then
				Dim openQuote As Integer = cmd.IndexOf(selectedChar, lastQuote + 1) + 1
				Dim closeQuote As Integer = cmd.IndexOf(selectedChar, cmd.IndexOf(selectedChar, lastQuote + 1) + 1)
				list.Add(cmd.Substring(openQuote, closeQuote - openQuote))
				lastQuote = closeQuote
			Else
				Exit While
			End If
		End While
		If list.Count = 0 Then Return New String() {cmd}
		Dim ret(list.Count - 1) As String
		list.CopyTo(ret, 0)
		Return ret
	End Function

	Public Function MakeArgList(ByVal args() As String, ByVal startIndex As Integer, ByVal count As Integer) As String
		Dim s As New System.Text.StringBuilder()
		For i As Integer = startIndex To startIndex + count - 1
			If args(i).Contains(" ") Then
				s.Append("""")
				s.Append(args(i))
				s.Append(""" ")
			Else
				s.Append(" ")
				s.Append(args(i))
				s.Append(" ")
			End If
		Next
		Return s.ToString
	End Function

	Private Function FirstIndexOf(ByVal searchString As String, ByVal chars() As Char, ByVal startIndex As Integer, ByRef selectedChar As Char) As Integer
		Dim minChar As Char
		Dim minIndex As Integer = startIndex
		For Each c As Char In chars
			Dim ind As Integer = searchString.IndexOf(c, startIndex)
			If ind >= 0 AndAlso minIndex >= ind Then
				minIndex = ind
				minChar = c
			End If
		Next
		selectedChar = minChar
		Return minIndex
	End Function

	<DllImport("Shell32", CharSet:=CharSet.Auto, SetLastError:=True)> _
	Private Function ShellExecuteEx(ByRef lpExecInfo As SHELLEXECUTEINFO) As Boolean
	End Function

	Private Structure SHELLEXECUTEINFO
		Public cbSize As Integer
		Public fMask As Integer
		Public hwnd As IntPtr
		<MarshalAs(UnmanagedType.LPTStr)> Public lpVerb As String
		<MarshalAs(UnmanagedType.LPTStr)> Public lpFile As String
		<MarshalAs(UnmanagedType.LPTStr)> Public lpParameters As String
		<MarshalAs(UnmanagedType.LPTStr)> Public lpDirectory As String
		Dim nShow As Integer
		Dim hInstApp As IntPtr
		Dim lpIDList As IntPtr
		<MarshalAs(UnmanagedType.LPTStr)> Public lpClass As String
		Public hkeyClass As IntPtr
		Public dwHotKey As Integer
		Public hIcon As IntPtr
		Public hProcess As IntPtr
	End Structure

	Public Function ShellExecute(ByVal file As String, ByVal parameters As String, ByVal ownerWindow As Integer, ByVal verb As String, ByVal directory As String, ByVal showCommand As WindowInfo.WindowState) As Process
		Dim sei As New SHELLEXECUTEINFO
		sei.cbSize = Marshal.SizeOf(GetType(SHELLEXECUTEINFO))
		sei.lpFile = file
		sei.lpDirectory = directory
		sei.hwnd = ownerWindow
		sei.lpParameters = parameters
		sei.nShow = showCommand

		If Not ShellExecuteEx(sei) Then
			Throw New Win32Exception
		Else
			Return Process.GetProcessById(sei.hProcess)
		End If
	End Function

	Public Function ShellExecute(ByVal file As String) As Process
		Return ShellExecute(file, Nothing, 0, Nothing, Nothing, WindowInfo.WindowState.ShowDefault)
	End Function

	Public Function ShellExecute(ByVal file As String, ByVal parameters As String) As Process
		Return ShellExecute(file, parameters, 0, Nothing, Nothing, WindowInfo.WindowState.ShowDefault)
	End Function

	Public Sub ShellExecute(ByVal file As Object)
		ShellExecute(CStr(file))
	End Sub

End Module
