Public Class HotkeyManager

	Shared WithEvents hk As New HotKey

    Shared hotKeyCount As Integer = 0

    Public Shared Sub InitHotkeys()
        'Run
        hk.TryRegisterHotKey(1, HotKey.ModifierKeys.Windows, Keys.R)
        'Start Menu
        hk.TryRegisterHotKey(2, HotKey.ModifierKeys.Windows, 0)
        hk.TryRegisterHotKey(3, HotKey.ModifierKeys.Control, Keys.Escape)
        hotKeyCount = 3
    End Sub

    Public Delegate Sub HotKeyProc()

    Shared hotkeys As New ObjectModel.Collection(Of HotKeyProc)

    Public Shared Sub AddHotKey(ByVal proc As HotKeyProc, ByVal modifiers As HotKey.ModifierKeys, ByVal keycode As Keys)
        If hotKeyCount = 0 Then InitHotkeys()
        hk.TryRegisterHotKey(hotKeyCount + 1, modifiers, keycode)
        hotKeyCount += 1
        hotkeys.Add(proc)
    End Sub

    Public Shared Sub UninitHotkeys()
        For i As Integer = 1 To hotKeyCount
            hk.TryUnregisterHotKey(i)
        Next
        hotKeyCount = 0
        hotkeys.Clear()
    End Sub

    Private Shared Sub hk_HotKeyPressed(ByVal id As Integer) Handles hk.HotKeyPressed
        Select Case id
            Case 1 'Run
                Dim t As New Threading.Thread(New Threading.ThreadStart(AddressOf RunDialogHotKey))
                t.Start()
            Case 2, 3
                Dim h As IntPtr = StartMenu.Handle 'Forces creation of the form
                StartMenu.BeginInvoke(New Threading.ThreadStart(AddressOf StartMenu.FadeIn))
            Case Else
                If Not hotkeys.Count < id Then
                    hotkeys(id - 1).BeginInvoke(Nothing, Nothing)
                End If
        End Select
    End Sub

    Private Shared Sub RunDialogHotKey()
        Dim runDialog As New RunDialog
        runDialog.ShowDialog(Nothing)
    End Sub

End Class