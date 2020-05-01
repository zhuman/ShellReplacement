Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
'Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class Form
    Inherits Windows.Forms.Form
    Implements IMessageFilter

    Dim AlphaBitmap As Bitmap, PaintedBitmap As Bitmap, Translucency As Byte = 255, CollBit As Bitmap
    Dim bClickThrough As Boolean = False

    ''' <summary>
    ''' Gets or sets the background image of the form.
    ''' </summary>
    ''' <value>An image, in an alpha based format.</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Image() As Bitmap
        Get
            Image = AlphaBitmap
        End Get
        Set(ByVal value As Bitmap)
            AlphaBitmap = value
            Refresh()
        End Set
    End Property

    Public Overloads Property Opacity() As Double
        Get
            Return CDbl(Translucency) / 255
        End Get
        Set(ByVal value As Double)
            Translucency = CByte(Math.Max(0, Math.Min(1, value)) * 255)
            SetBitmap(Translucency)
        End Set
    End Property

    Dim _autoUpdate As Boolean = False

    Public Property AutoUpdate() As Boolean
        Get
            Return _autoUpdate
        End Get
        Set(ByVal value As Boolean)
            If value And _autoUpdate Then
                Application.AddMessageFilter(Me)
            ElseIf Not value Then
                Application.RemoveMessageFilter(Me)
            End If
            _autoUpdate = value
        End Set
    End Property

    Public Sub New(ByVal ClickThrough As Boolean)
        FormBorderStyle = Windows.Forms.FormBorderStyle.None
        bClickThrough = ClickThrough
        'Application.AddMessageFilter(Me)
    End Sub

    Public Sub New()
        Me.New(False)
    End Sub

#Region "SetBitmap"

    Protected Sub SetBitmap(ByVal bitmap As Bitmap)
        SetBitmap(bitmap, Translucency)
    End Sub

    Protected Sub SetBitmap(ByVal opacity As Byte)
        If PaintedBitmap Is Nothing Then
            UpdateWindow()
        End If
        SetBitmap(PaintedBitmap, opacity)
    End Sub

    Protected Sub SetBitmap(ByVal bitmap As Bitmap, ByVal opacity As Byte)
        If Not Me.IsDisposed And bitmap IsNot Nothing Then
            If Not (bitmap.PixelFormat = PixelFormat.Format32bppArgb) Then
                Throw New ApplicationException("The bitmap must be 32ppp with alpha-channel.")
            End If
            Dim screenDc As IntPtr = Win32.GetDC(IntPtr.Zero)
            Dim memDc As IntPtr = Win32.CreateCompatibleDC(screenDc)
            Dim hBitmap As IntPtr = IntPtr.Zero
            Dim oldBitmap As IntPtr = IntPtr.Zero
            Try
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0))
                oldBitmap = Win32.SelectObject(memDc, hBitmap)
                Dim size As Win32.Size = New Win32.Size(bitmap.Width, bitmap.Height)
                Dim pointSource As Win32.Point = New Win32.Point(0, 0)
                Dim topPos As Win32.Point = New Win32.Point(Left, Top)
                Dim blend As Win32.BLENDFUNCTION = New Win32.BLENDFUNCTION
                blend.BlendOp = Win32.AC_SRC_OVER
                blend.BlendFlags = 0
                blend.SourceConstantAlpha = opacity
                blend.AlphaFormat = Win32.AC_SRC_ALPHA
                Win32.UpdateLayeredWindow(Handle, screenDc, topPos, size, memDc, pointSource, 0, blend, Win32.ULW_ALPHA)
            Finally
                Win32.ReleaseDC(IntPtr.Zero, screenDc)
                If Not (hBitmap = IntPtr.Zero) Then
                    Win32.SelectObject(memDc, oldBitmap)
                    Win32.DeleteObject(hBitmap)
                End If
                Win32.DeleteDC(memDc)
            End Try
            PaintedBitmap = bitmap
        End If
    End Sub

#End Region

    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Const WS_EX_TRANSPARENT As Integer = &H20&
            Dim cp As CreateParams = MyBase.CreateParams
            If Not Me.DesignMode Then
                cp.ExStyle = cp.ExStyle Or (524288) 'Layered window style constant
                If bClickThrough = True Then cp.ExStyle = cp.ExStyle Or WS_EX_TRANSPARENT
            End If
            Return cp
        End Get
    End Property

    Protected Overrides Sub OnShown(ByVal e As System.EventArgs)
        If Not Me.DesignMode Then
            If Me.StartPosition = FormStartPosition.CenterScreen Then
                Me.Location = New Point((Screen.FromPoint(Me.PointToScreen(New Point(0, 0))).Bounds.Width / 2) - (Me.Width / 2), (Screen.FromPoint(Me.PointToScreen(New Point(0, 0))).Bounds.Height / 2) - (Me.Height / 2))
            End If
        End If
        MyBase.OnShown(e)
    End Sub

    Private Const WM_PAINT As Integer = &HF
    Private Const WM_ERASEBKGND As Integer = &H14
    Private Const WM_NCPAINT As Integer = &H85

    Private Declare Function SendMessage Lib "user32" _
       Alias "SendMessageA" _
      (ByVal hwnd As Integer, _
       ByVal wMsg As Integer, _
       ByVal wParam As Integer, _
    ByVal lParam As Integer) As Integer

    Protected Sub UpdateWindow()
        If Me.Image IsNot Nothing Then
            Dim bit As New Bitmap(Me.Image.Width, Me.Image.Height), gr As Graphics
            gr = Graphics.FromImage(bit)
            gr.DrawImage(Me.Image, New Rectangle(0, 0, Me.Image.Width, Me.Image.Height))
            Me.OnPaint(New PaintEventArgs(gr, New Rectangle(0, 0, Me.Width, Me.Height)))
            For Each ppao As Control In Me.Controls
                If Not ppao.Size = New Size(0, 0) Then
                    Dim cbit As New Bitmap(ppao.Width, ppao.Height, PixelFormat.Format32bppArgb), cgr As Graphics
                    ppao.DrawToBitmap(cbit, New Rectangle(0, 0, ppao.Width, ppao.Height))
                    If GetType(IAlphaPaint).IsAssignableFrom(ppao.GetType) Then
                        Try
                            cbit = GraphicsRenderer.ApplyAlphaMask(cbit, CType(ppao, IAlphaPaint).GetAlphaMask())
                        Catch ex As Exception

                        End Try
                    Else
                        'cbit = GraphicsRenderer.MakeColorTransparent(cbit, Me.BackColor)
                    End If
                    gr.DrawImage(cbit, ppao.Left, ppao.Top)
                End If
            Next
            SetBitmap(bit)
        End If
    End Sub

    WithEvents updateTimer As New Timer

    Private Const WM_PRINT As Integer = &H317
    Private Const WM_PRINTCLIENT As Integer = &H318

    Public Function PreFilterMessage(ByRef m As System.Windows.Forms.Message) As Boolean Implements System.Windows.Forms.IMessageFilter.PreFilterMessage
        For Each c As Control In Me.Controls
            If c.Handle = m.HWnd AndAlso m.Msg <> WM_PRINT AndAlso m.Msg <> WM_PRINTCLIENT Then
                If Not updateTimer.Enabled Then
                    updateTimer.Interval = 20
                    updateTimer.Enabled = True
                End If
                Return False
            End If
        Next
    End Function

    Private Sub updateTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles updateTimer.Tick
        updateTimer.Enabled = False
        UpdateWindow()
    End Sub

    Public Overrides Sub Refresh()
        MyBase.Refresh()
        UpdateWindow()
    End Sub

    Public Overloads ReadOnly Property Width() As Integer
        Get
            If Me.Image Is Nothing Then
                Return 0
            Else
                Return Me.Image.Width
            End If
        End Get
    End Property

    Public Overloads ReadOnly Property Height() As Integer
        Get
            If Me.Image Is Nothing Then
                Return 0
            Else
                Return Me.Image.Height
            End If
        End Get
    End Property

    ''' <summary>
    ''' Fades the form in or out determined by its <seealso cref="Form.Visible" /> property.
    ''' </summary>
    ''' <param name="length">The length of time in milliseconds that it should take the form to fade.</param>
    ''' <remarks></remarks>
    Public Sub StartFade(Optional ByVal length As Integer = 2000)
        fadeTimer.Interval = 5
        If Me.Visible = True Then
            fadeAmount = Me.Opacity
            fadeUp = False
        Else
            fadeAmount = 0
            Me.Opacity = 0
            Me.Show()
            fadeUp = True
        End If

        fadeIncreaseBy = (1 / length) * IIf(fadeUp, 1, Me.Opacity) * 5
        fadeTimer.Enabled = True
    End Sub

    Public Sub StopFade()
        fadeTimer.Enabled = False
    End Sub

    Private Sub fadeTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles fadeTimer.Tick
        If fadeUp = True Then
            If Me.Opacity > 0.99 Then
                fadeTimer.Enabled = False
                Exit Sub
            End If
            fadeAmount += fadeIncreaseBy
            Me.Opacity = fadeAmount
        Else
            If Me.Opacity < 0.01 Then
                fadeTimer.Enabled = False
                Me.Visible = False
                Exit Sub
            End If
            fadeAmount -= fadeIncreaseBy
            Me.Opacity = fadeAmount
        End If
    End Sub

    Private WithEvents fadeTimer As New Timer
    Dim fadeUp As Boolean = True, fadeAmount As Double, fadeIncreaseBy As Double

    ''' <summary>
    ''' Includes declarations for API calls and required data types.
    ''' </summary>
    ''' <remarks></remarks>
    Private Class Win32

        Public Enum Bool
            [False] = 0
            [True]
        End Enum

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure Point
            Public x As Int32
            Public y As Int32

            Public Sub New(ByVal x As Int32, ByVal y As Int32)
                Me.x = x
                Me.y = y
            End Sub
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure Size
            Public cx As Int32
            Public cy As Int32

            Public Sub New(ByVal cx As Int32, ByVal cy As Int32)
                Me.cx = cx
                Me.cy = cy
            End Sub
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Structure ARGB
            Public Blue As Byte
            Public Green As Byte
            Public Red As Byte
            Public Alpha As Byte
        End Structure

        '<StructLayout(LayoutKind.Sequential, , 1)> _
        Public Structure BLENDFUNCTION
            Public BlendOp As Byte
            Public BlendFlags As Byte
            Public SourceConstantAlpha As Byte
            Public AlphaFormat As Byte
        End Structure
        Public Const ULW_COLORKEY As Int32 = 1
        Public Const ULW_ALPHA As Int32 = 2
        Public Const ULW_OPAQUE As Int32 = 4
        Public Const AC_SRC_OVER As Byte = 0
        Public Const AC_SRC_ALPHA As Byte = 1

        <DllImport("user32.dll")> _
        Public Shared Function UpdateLayeredWindow(ByVal hwnd As IntPtr, ByVal hdcDst As IntPtr, ByRef pptDst As Point, ByRef psize As Size, ByVal hdcSrc As IntPtr, ByRef pprSrc As Point, ByVal crKey As Int32, ByRef pblend As BLENDFUNCTION, ByVal dwFlags As Int32) As Bool
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function GetDC(ByVal hWnd As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As Integer
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function CreateCompatibleDC(ByVal hDC As IntPtr) As IntPtr
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function DeleteDC(ByVal hdc As IntPtr) As Bool
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function SelectObject(ByVal hDC As IntPtr, ByVal hObject As IntPtr) As IntPtr
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function DeleteObject(ByVal hObject As IntPtr) As Bool
        End Function
    End Class

End Class