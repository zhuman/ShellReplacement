Public Class ZPixelForm

    Dim dragging As Boolean
    Dim startPoint As Point

    Private Sub ZPixelForm_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            dragging = True
            startPoint = e.Location
        End If
    End Sub

    Private Sub ZPixelForm_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If dragging Then
            Me.Location = Me.Location + New Point(e.X - startPoint.X, e.Y - startPoint.Y)
        End If
    End Sub

    Private Sub ZPixelForm_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        dragging = False
    End Sub

    Private Sub CoolPictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CoolPictureBox1.Click
        MessageBox.Show("You clicked me!")
    End Sub

End Class

Public Class CoolToolStrip
    Inherits ToolStrip
    Implements ZPixel.IAlphaPaint

    Public Sub New()
        MyBase.New()
    End Sub

    Public Function GetAlphaMask() As System.Drawing.Bitmap Implements ZPixel.IAlphaPaint.GetAlphaMask
        Dim b As New Bitmap(Me.Width, Me.Height, Imaging.PixelFormat.Format32bppArgb)
        Dim gr As Graphics = Graphics.FromImage(b)
        gr.FillRectangle(New Drawing2D.LinearGradientBrush(New Point(0, 0), New Point(Me.Width, 0), Color.Black, Color.Transparent), New Rectangle(0, 0, Me.Width, Me.Height))
        gr.Dispose()
        Return b
    End Function

End Class

Public Class CoolPictureBox
    Inherits PictureBox
    Implements ZPixel.IAlphaPaint

    Public Function GetAlphaMask() As System.Drawing.Bitmap Implements ZPixel.IAlphaPaint.GetAlphaMask
        Dim b As New Bitmap(Me.Width, Me.Height, Imaging.PixelFormat.Format32bppArgb)
        Dim gr As Graphics = Graphics.FromImage(b)
        If Me.Image IsNot Nothing Then
            Select Case Me.SizeMode
                Case PictureBoxSizeMode.AutoSize, PictureBoxSizeMode.Normal
                    gr.DrawImage(Me.Image, New Point(0, 0))
                Case PictureBoxSizeMode.StretchImage
                    gr.DrawImage(Me.Image, New Rectangle(0, 0, Me.Width, Me.Height))
                Case PictureBoxSizeMode.CenterImage
                    gr.DrawImage(Me.Image, CSng(Me.Width / 2 - Me.Image.Width / 2), CSng(Me.Height / 2 - Me.Image.Height / 2))
                Case PictureBoxSizeMode.Zoom

            End Select

        End If

        gr.Dispose()
        Return b
    End Function

End Class