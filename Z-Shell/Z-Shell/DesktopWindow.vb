Imports System.Runtime.InteropServices
Imports System.ComponentModel

Public Module DesktopWindow

    Private Declare Auto Function SetShellWindow Lib "user32.dll" (ByVal hwnd As IntPtr) As Integer
    Private Declare Auto Function SetShellWindowEx Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal lsview As IntPtr) As Integer
    Private Declare Auto Function RegisterShellHook Lib "shell32.dll" Alias "#181" (ByVal hwnd As IntPtr, ByVal flags As Integer) As Integer
    Private Declare Auto Function GetShellWindow Lib "user32.dll" () As IntPtr
    Private Declare Auto Function RegisterShellHookWindow Lib "user32.dll" (ByVal hwnd As IntPtr) As Boolean
    Private Declare Auto Function GetTaskmanWindow Lib "user32.dll" () As Integer
    Private Declare Auto Function DefWindowProc Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    Private Declare Auto Function SetProgmanWindow Lib "user32.dll" (ByVal hwnd As IntPtr) As Boolean

    Private Const RSH_UNREGISTER As Integer = 0
    Private Const RSH_REGISTER As Integer = 1
    Private Const RSH_REGISTER_PROGMAN As Integer = 2
    Private Const RSH_REGISTER_TASKMAN As Integer = 3

    Dim desktopNativeWindow As New NativeWindowEx
    Dim desktopWindowInfo As WindowInfo

    Public Sub UninitDesktop()
        'w.SetWindowState(WindowInfo.WindowState.Hide)
        Try
            desktopWindowInfo.Close()
            desktopNativeWindow.UnRegisterClass("Progman")
        Catch ex As Exception

        End Try
        SetShellWindow(0)
    End Sub

    Dim wndprocdel As New NativeWindowEx.WndProcDelegate(AddressOf DeskProc)
    Dim d As DesktopForm

    Public Sub InitDesktop()
        'Create the Progman class
        Dim ccp As New ClassCreateParams
        ccp.Style = ClassCreateParams.ClassStyles.DoubleClicks
        ccp.Name = "Progman"
        ccp.WndProc = wndprocdel
        ccp.Cursor = Cursors.Default
        desktopNativeWindow.RegisterClass(ccp)

        'Create the window
        Dim cp As New CreateParamsEx
        cp.Caption = "Program Manager"
        cp.ClassName = "Progman"
        cp.ExStyle = ExtendedWindowStyles.WS_EX_TOOLWINDOW
        cp.Style = WindowStyles.WS_POPUP
        cp.Width = Screen.PrimaryScreen.Bounds.Width
        cp.Height = Screen.PrimaryScreen.Bounds.Height
        cp.X = Screen.PrimaryScreen.Bounds.X
        cp.Y = Screen.PrimaryScreen.Bounds.Y
        desktopNativeWindow.CreateHandle(cp)
        desktopWindowInfo = New WindowInfo(desktopNativeWindow.Handle)

        'Show the window
        desktopWindowInfo.SetWindowState(WindowInfo.WindowState.Show)

        'Set the shell window
        SetShellWindow(desktopWindowInfo.Handle)

        d = New DesktopForm
        d.Show()
        'SetProgmanWindow(desktopWindowInfo.Handle)
    End Sub

    Public ReadOnly Property Handle() As IntPtr
        Get
            If desktopWindowInfo IsNot Nothing Then
                Return desktopWindowInfo.Handle
            Else
                Return 0
            End If
        End Get
    End Property

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure PAINTSTRUCT
        Public hdc As IntPtr
        Public fErase As Boolean
        Public rcPaint As Boolean
        Public fRestore As Boolean
        Public fIncUpdate As Boolean
        Public rgbReserved() As Byte
    End Structure

    Private Function DeskProc(ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wparam As IntPtr, ByVal lparam As IntPtr) As IntPtr
        Select Case msg
            Case Else
                Return DefWindowProc(hwnd, msg, wparam, lparam)
        End Select
    End Function

End Module