Public Class UILoadingForm
    Inherits ZPixel.Form

    Private Const imgwidth As Integer = 500
    Private Const imgheight As Integer = 250

    WithEvents timFade As New Timer

    Dim baseImage As Bitmap

    Public Sub New()
        MyBase.New(True)
        Me.TopMost = True
        Me.ShowInTaskbar = False
        baseImage = New Bitmap(imgwidth, imgheight, Imaging.PixelFormat.Format32bppArgb)
        Dim gr As Graphics = Graphics.FromImage(baseImage)
        gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        gr.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
        Dim rrpath As Drawing2D.GraphicsPath = ZPixel.GraphicsRenderer.GetRoundedRect(New Rectangle(5, 5, imgwidth - 10, imgheight - 10), 10)
        gr.FillPath(New SolidBrush(Color.FromArgb(200, Color.Gray)), rrpath)
        gr.DrawPath(Pens.Black, rrpath)
        DrawTextWithShadow(gr, "Z-Shell", New Drawing.Font("Trebuchet MS", 50, FontStyle.Bold, GraphicsUnit.Pixel), 25, 25)
        DrawTextWithShadow(gr, My.Application.Info.Version.ToString, New Drawing.Font("Trebuchet MS", 25, FontStyle.Bold, GraphicsUnit.Pixel), 50, 80)
        DrawTextWithShadow(gr, "Building Shell Interface...", New Drawing.Font("Trebuchet MS", 25, FontStyle.Bold, GraphicsUnit.Pixel), 25, 200)
        gr.Dispose()

        Me.Image = baseImage
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    Public Sub FadeOut()
        Me.StartFade(500)
    End Sub

    Private Sub DrawTextWithShadow(ByVal gr As Graphics, ByVal text As String, ByVal font As Drawing.Font, ByVal x As Integer, ByVal y As Integer, Optional ByVal width As Integer = 0, Optional ByVal height As Integer = 0)
        gr.DrawString(text, font, Brushes.Black, New Rectangle(x + 2, y + 2, width, height))
        gr.DrawString(text, font, Brushes.White, New Rectangle(x, y, width, height))
    End Sub

    Private Delegate Sub TimerTick(ByVal sender As Object, ByVal e As EventArgs)

    Private Sub timFade_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timFade.Tick
        If Me.InvokeRequired Then
            Me.Invoke(New TimerTick(AddressOf timFade_Tick))
        Else
            If Me.Opacity > 10 Then
                Me.Opacity -= 10
            Else
                timFade.Enabled = False
                Me.Close()
                Application.ExitThread()
            End If
        End If
    End Sub

End Class
