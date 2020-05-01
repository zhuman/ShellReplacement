Public Class Conversions

#Region "BitmapToBase64"
    Shared Function BitmapToBase64(ByVal image As Bitmap) As String
        Return BitmapToBase64(image, Imaging.ImageFormat.Png)
    End Function

    Shared Function BitmapToBase64(ByVal image As System.Drawing.Bitmap, ByVal imagingformat As Imaging.ImageFormat) As String
        Dim base64 As String
        Dim memory As New System.IO.MemoryStream()
        image.Save(memory, imagingformat)
        base64 = System.Convert.ToBase64String(memory.ToArray)
        memory.Close()
        memory = Nothing
        Return base64
    End Function

    Shared Function BitmapFromBase64(ByVal base64 As String) As System.Drawing.Bitmap
        Dim oBitmap As System.Drawing.Bitmap
        Dim memory As New System.IO.MemoryStream(Convert.FromBase64String(base64))
        oBitmap = New System.Drawing.Bitmap(memory)
        memory.Close()
        memory = Nothing
        Return oBitmap
    End Function
#End Region

    Public Shared Function CompressString(ByVal str As String) As String
        Dim msOrig As New IO.MemoryStream
        Dim buffer(str.Length - 1) As Byte, chars(str.Length - 1) As Char, i As Integer

        For i = 0 To str.Length - 1
            chars(i) = str.Chars(i)
        Next

        System.Text.Encoding.Unicode.GetEncoder().Convert(chars, 0, str.Length, buffer, 0, str.Length, True, Nothing, Nothing, Nothing)

        Dim msOut As New IO.MemoryStream
        Dim cmp As New IO.Compression.GZipStream(msOut, IO.Compression.CompressionMode.Compress)

        cmp.Write(buffer, 0, buffer.Length)

        Dim stringReader As New IO.StreamReader(msOut)
        Return stringReader.ReadToEnd()
    End Function

    Public Shared Function DecompressString(ByVal str As String) As String
        Return str
    End Function

    Public Shared Function CursorToBitmap(ByVal c As Cursor) As Bitmap
        Dim b As New Bitmap(c.Size.Width, c.Size.Height)
        Dim gr As Graphics = Graphics.FromImage(b)
        c.Draw(gr, New Rectangle(0, 0, c.Size.Width, c.Size.Height))
        gr.Dispose()
        Return b
    End Function

End Class
