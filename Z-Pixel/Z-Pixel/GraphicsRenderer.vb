Public Class GraphicsRenderer

    ''' <summary>
    ''' Contructs an image containing the shadow of the specified graphics path.
    ''' </summary>
    ''' <param name="p">The path to make a shadow of.</param>
    ''' <param name="ShadowColor">The color of the shadow.</param>
    ''' <param name="bitWidth">The width of the new bitmap.</param>
    ''' <param name="bitHeight">The height of the new bitmap.</param>
    ''' <param name="ShadowMargin">The size of the shadow.</param>
    ''' <param name="clipRegion">A region to clip the shadow with, if needed. If not specified, the shadow is clipped only by the path.</param>
    ''' <returns>A bitmap containing the shadow of the specified graphics path.</returns>
    ''' <remarks></remarks>
    Shared Function CalculateShadow(ByVal p As Drawing2D.GraphicsPath, ByVal ShadowColor As Color, ByVal bitWidth As Integer, ByVal bitHeight As Integer, Optional ByVal ShadowMargin As Integer = 3, Optional ByVal clipRegion As Region = Nothing) As Bitmap
        Dim img As Bitmap = New Bitmap(bitWidth, bitHeight)
        Dim path As Drawing2D.GraphicsPath = p
        Dim grx As Graphics = Graphics.FromImage(img)
        grx.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Dim scaleFactor As Single = 1.0F - (CType(ShadowMargin, Single) * 2 / CType(bitWidth, Single))
        Dim backStyle As Drawing2D.PathGradientBrush = New Drawing2D.PathGradientBrush(path)
        backStyle.CenterPoint = New Point(0, 0)
        backStyle.CenterColor = ShadowColor
        backStyle.FocusScales = New PointF(scaleFactor, scaleFactor)
        backStyle.SurroundColors = New Color() {Color.Transparent}
        Dim region As Region
        If clipRegion Is Nothing Then
            region = New Region(path)
        Else
            region = clipRegion.Clone
        End If
        region.Translate(-ShadowMargin, -ShadowMargin)
        grx.SetClip(region, Drawing2D.CombineMode.Xor)
        grx.FillRegion(backStyle, New Region(path))
        Return img
    End Function

    ''' <summary>
    ''' Contructs a graphics path containing a rounded rectangle.
    ''' </summary>
    ''' <param name="baseRect">The rectangle to fit the rounded rectangle into.</param>
    ''' <param name="radius">The radius of the corner arcs.</param>
    ''' <returns>A graphics path contaning the rounded rectangle.</returns>
    ''' <remarks></remarks>
    Public Shared Function GetRoundedRect(ByVal baseRect As RectangleF, ByVal radius As Single) As Drawing2D.GraphicsPath
        If radius <= 0.0F Then
            Dim mPath As New Drawing2D.GraphicsPath
            mPath.AddRectangle(baseRect)
            mPath.CloseFigure()
            Return mPath
        End If
        If radius >= (Math.Min(baseRect.Width, baseRect.Height)) / 2 Then
            Return GetCapsule(baseRect)
        End If
        Dim diameter As Single = radius * 2.0F
        Dim sizeF As SizeF = New SizeF(diameter, diameter)
        Dim arc As RectangleF = New RectangleF(baseRect.Location, sizeF)
        Dim path As Drawing2D.GraphicsPath = New Drawing2D.GraphicsPath
        path.AddArc(arc, 180, 90)
        arc.X = baseRect.Right - diameter
        path.AddArc(arc, 270, 90)
        arc.Y = baseRect.Bottom - diameter
        path.AddArc(arc, 0, 90)
        arc.X = baseRect.Left
        path.AddArc(arc, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    Private Shared Function GetCapsule(ByVal baseRect As RectangleF) As Drawing2D.GraphicsPath
        Dim diameter As Single
        Dim arc As RectangleF
        Dim path As New Drawing2D.GraphicsPath
        Try
            If baseRect.Width > baseRect.Height Then
                diameter = baseRect.Height
                Dim sizeF As SizeF = New SizeF(diameter, diameter)
                arc = New RectangleF(baseRect.Location, sizeF)
                path.AddArc(arc, 90, 180)
                arc.X = baseRect.Right - diameter
                path.AddArc(arc, 270, 180)
            Else
                If baseRect.Width < baseRect.Height Then
                    diameter = baseRect.Width
                    Dim sizeF As SizeF = New SizeF(diameter, diameter)
                    arc = New RectangleF(baseRect.Location, sizeF)
                    path.AddArc(arc, 180, 180)
                    arc.Y = baseRect.Bottom - diameter
                    path.AddArc(arc, 0, 180)
                Else
                    path.AddEllipse(baseRect)
                End If
            End If
        Catch ex As Exception
            path.AddEllipse(baseRect)
        Finally
            path.CloseFigure()
        End Try
        Return path
    End Function

    ''' <summary>
    ''' Multiplies the alpha value of every pixel by the specified amount.
    ''' </summary>
    ''' <param name="b"></param>
    ''' <param name="a"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MultiplyAlpha(ByVal b As Bitmap, ByVal a As Single) As Bitmap
        If b Is Nothing Then Throw New ArgumentNullException

        Dim b2 As New Bitmap(b.Width, b.Height, Imaging.PixelFormat.Format32bppArgb)
        Dim ia As New Imaging.ImageAttributes()
        Dim colorMatrixElements As Single()() = { _
            New Single() {1, 0, 0, 0, 0}, _
            New Single() {0, 1, 0, 0, 0}, _
            New Single() {0, 0, 1, 0, 0}, _
            New Single() {0, 0, 0, a, 0}, _
            New Single() {0, 0, 0, 0, 1}}
        Dim cm As New Imaging.ColorMatrix(colorMatrixElements)
        ia.SetColorMatrix(cm)
        Dim gr As Graphics = Graphics.FromImage(b2)
        gr.DrawImage(b, New Rectangle(0, 0, b.Width, b.Height), 0, 0, b.Width, b.Height, GraphicsUnit.Pixel, ia)
        gr.Dispose()
        Return b2
    End Function

    ''' <summary>
    ''' Causes a particular color to become transparent in the specified bitmap.
    ''' </summary>
    ''' <param name="b"></param>
    ''' <param name="c"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MakeColorTransparent(ByVal b As Bitmap, ByVal c As Color) As Bitmap
        If b Is Nothing Then Throw New ArgumentNullException

        Dim b2 As New Bitmap(b.Width, b.Height)
        Dim ia As New Imaging.ImageAttributes()
        ia.SetColorKey(c, c)
        Dim gr As Graphics = Graphics.FromImage(b2)
        gr.DrawImage(b, New Rectangle(0, 0, b.Width, b.Height), 0, 0, b.Width, b.Height, GraphicsUnit.Pixel, ia)
        Return b2
    End Function

    ''' <summary>
    ''' Rotates a bitmap by the specified degree.
    ''' </summary>
    ''' <param name="b">The original bitmap to rotate.</param>
    ''' <param name="angle">A value in degrees specifying how much to rotate the bitmap.</param>
    ''' <returns>A bitmap rotated by the specified degree.</returns>
    ''' <remarks></remarks>
    Public Shared Function Rotate(ByVal b As Bitmap, ByVal angle As Integer) As Bitmap
        If b Is Nothing Then Throw New ArgumentNullException

        Dim i As New Bitmap(b.Width, b.Height)
        Dim gr As Graphics = Graphics.FromImage(i)
        gr.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        gr.TranslateTransform(i.Width / 2, i.Height / 2)
        gr.RotateTransform(angle)
        gr.DrawImage(b, New Point(b.Width / 2 * -1, b.Height / 2 * -1))
        gr.Dispose()
        Return i
    End Function

    ''' <summary>
    ''' Scales a bitmap by the specified percentages.
    ''' </summary>
    ''' <param name="b">The original bitmap.</param>
    ''' <param name="percentx">The percentage to scale the width of the bitmap.</param>
    ''' <param name="percenty">The percentage to scale the height of the bitmap.</param>
    ''' <returns>The scaled bitmap.</returns>
    ''' <remarks></remarks>
    Public Shared Function Scale(ByVal b As Bitmap, ByVal percentx As Single, ByVal percenty As Single) As Bitmap
        Return Scale(b, CInt(b.Width * percentx), CInt(b.Height * percenty))
    End Function

    ''' <summary>
    ''' Scales a bitmap to the specified width and height using high quality bicubic scaling.
    ''' </summary>
    ''' <param name="b">The original bitmap.</param>
    ''' <param name="width">The new width of the bitmap.</param>
    ''' <param name="height">The new height of the bitmap.</param>
    ''' <returns>The scaled bitmap.</returns>
    ''' <remarks></remarks>
    Public Shared Function Scale(ByVal b As Bitmap, ByVal width As Integer, ByVal height As Integer) As Bitmap
        If b Is Nothing Then Throw New ArgumentNullException
        If width <= 0 Or height <= 0 Then Throw New ArgumentException("The width and height must be greater than zero.")

        Dim sb As New Bitmap(width, height, b.PixelFormat)
        Dim gr As Graphics = Graphics.FromImage(sb)
        gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        gr.DrawImage(b, New Rectangle(0, 0, width, height))
        gr.Dispose()
        Return sb
    End Function

    Public Shared Function ApplyAlphaMask(ByVal orig As Bitmap, ByVal mask As Bitmap) As Bitmap
        If orig Is Nothing Or mask Is Nothing Then Throw New ArgumentNullException("Neither bitmaps can be null.")
        If orig.Size <> mask.Size Then Throw New ArgumentException("The bitmap and its mask must be the same size.")
        If orig.PixelFormat <> Imaging.PixelFormat.Format32bppArgb Or mask.PixelFormat <> Imaging.PixelFormat.Format32bppArgb Then Throw New ArgumentException("Both bitmaps must be in 32 bit ARGB format.")

        Dim newb As New Bitmap(orig.Width, orig.Height, Imaging.PixelFormat.Format32bppArgb)
        'Copy the original bitmap into the new bitmap
        Graphics.FromImage(newb).DrawImage(orig, New Point(0, 0))

        'Create an array of pixel data for the new bitmap
        Dim data As Imaging.BitmapData = newb.LockBits(New Rectangle(0, 0, newb.Width, newb.Height), Imaging.ImageLockMode.ReadWrite, Imaging.PixelFormat.Format32bppArgb)
        Dim pixels(data.Stride * data.Height - 1) As Byte
        Runtime.InteropServices.Marshal.Copy(data.Scan0, pixels, 0, pixels.Length)

        'Create an array of pixels for the mask
        Dim maskData As Imaging.BitmapData = mask.LockBits(New Rectangle(0, 0, mask.Width, mask.Height), Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format32bppArgb)
        Dim maskPixels(maskData.Stride * maskData.Height - 1) As Byte
        Runtime.InteropServices.Marshal.Copy(maskData.Scan0, maskPixels, 0, maskPixels.Length)

        'Loop through the pixels, multiplying the original alpha by the mask's alpha
        For x As Integer = 0 To data.Width - 1
            For y As Integer = 0 To data.Height - 1
                pixels(data.Stride * y + x * 4 + 3) = CByte(pixels(data.Stride * y + x * 4 + 3) * (maskPixels(maskData.Stride * y + x * 4 + 3) / 255))
            Next
        Next

        'Unlock the mask
        mask.UnlockBits(maskData)

        'Copy the new pixel data back into the bitmap
        Runtime.InteropServices.Marshal.Copy(pixels, 0, data.Scan0, pixels.Length)

        'Unlock the new bitmap
        newb.UnlockBits(data)

        Return newb
    End Function

End Class