Public Class ShowStartMenu
    Inherits ShellAction

    Dim _corner As ContentAlignment = ContentAlignment.BottomLeft

    Public Property Corner() As ContentAlignment
        Get
            Return _corner
        End Get
        Set(ByVal value As ContentAlignment)
            _corner = value
        End Set
    End Property

    Public Overrides Sub RunAction()
        If Not StartMenu.Visible Then
            Dim screenRect As Rectangle = Screen.PrimaryScreen.WorkingArea
            Dim width As Integer = StartMenu.Width
            Dim height As Integer = StartMenu.Height
            Dim loc As Point
            Select Case Me.Corner
                Case ContentAlignment.TopLeft
                    loc = screenRect.Location
                Case ContentAlignment.TopRight
                    loc = New Point(screenRect.Right - width, screenRect.Y)
                Case ContentAlignment.TopCenter
                    loc = New Point(screenRect.X + screenRect.Width / 2 - width / 2, screenRect.Y)
                Case ContentAlignment.BottomLeft
                    loc = New Point(screenRect.X, screenRect.Bottom - height)
                Case ContentAlignment.BottomRight
                    loc = New Point(screenRect.Right - width, screenRect.Bottom - height)
                Case ContentAlignment.BottomCenter
                    loc = New Point(screenRect.X + screenRect.Width / 2 - width / 2, screenRect.Bottom)
                Case ContentAlignment.MiddleLeft
                    loc = New Point(screenRect.X, screenRect.Y + screenRect.Height / 2 - height / 2)
                Case ContentAlignment.MiddleCenter
                    loc = New Point(screenRect.X + screenRect.Width / 2 - width / 2, screenRect.Y + screenRect.Height / 2 - height / 2)
                Case ContentAlignment.MiddleRight
                    loc = New Point(screenRect.Right - width, screenRect.Y + screenRect.Height / 2 - height / 2)
            End Select
            StartMenu.Location = loc
            StartMenu.FadeIn()

        End If
    End Sub

End Class
