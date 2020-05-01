Public Class HotKey

    WithEvents hotKeyWin As NativeWindowEx

    Public Sub New()
        hotKeyWin = New NativeWindowEx
        hotKeyWin.CreateHandle(New CreateParams)
    End Sub

	Private Sub hotKeyWin_MessageRecieved(ByRef m As System.Windows.Forms.Message) Handles hotKeyWin.MessageRecieved
		If m.Msg = WM_HOTKEY Then RaiseEvent HotKeyPressed(m.WParam)
	End Sub

    Public Event HotKeyPressed(ByVal id As Integer)

    Const WM_HOTKEY As Integer = &H312

    Private Declare Function RegisterHotKeyAPI Lib "user32.dll" Alias "RegisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal acc As UInteger, ByVal keys As UInteger) As Integer
	Private Declare Function UnregisterHotKeyAPI Lib "user32.dll" Alias "UnregisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean

    Public Sub RegisterHotKey(ByVal id As Integer, ByVal modifiers As ModifierKeys, ByVal keyCode As Keys)
        If id < 0 Or id > &HBFFF Then
            Throw New Exception("The key code id must be from 0 to 0xBFFF.")
        End If
        If modifiers = 0 Then
            Throw New Exception("You must have one or more modifier keys in a hot key.")
        End If
        If RegisterHotKeyAPI(hotKeyWin.Handle, id, CInt(modifiers), CInt(keyCode)) = 0 Then
			Throw New System.ComponentModel.Win32Exception()
			'Throw New Exception("An error occured while registering the hot key.")
        End If
    End Sub

    Public Function TryRegisterHotKey(ByVal id As Integer, ByVal modifiers As ModifierKeys, ByVal keyCode As Keys) As Boolean
        Try
            RegisterHotKey(id, modifiers, keyCode)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

	Public Sub UnregisterHotKey(ByVal id As Integer)
		If UnregisterHotKeyAPI(hotKeyWin.Handle, id) = 0 Then
			Throw New System.ComponentModel.Win32Exception()
		End If
    End Sub

    Public Function TryUnregisterHotKey(ByVal id As Integer) As Boolean
        Try
            UnregisterHotKey(id)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

	<Flags()> _
	Public Enum ModifierKeys
		None = 0
		Alt = 1
		Control = 2
		Shift = 4
		Windows = 8
	End Enum

End Class
