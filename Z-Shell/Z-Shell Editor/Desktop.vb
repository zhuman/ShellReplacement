Public Class Desktop

    Private Sub New()

    End Sub

    Private Declare Auto Function SystemParametersInfo Lib "user32.dll" (ByVal uiAction As Integer, ByVal uiParam As Integer, ByVal pvParam As IntPtr, ByVal WinIni As Integer) As Boolean
    Private Declare Auto Function SetDeskWallpaper Lib "user32.dll" Alias "SystemParametersInfo" (ByVal uiAction As Integer, ByVal uiParam As Integer, ByVal pvParam As String, ByVal WinIni As Integer) As Boolean
    Private Declare Auto Function GetDeskWallpaper Lib "user32.dll" Alias "SystemParametersInfo" (ByVal uiAction As Integer, ByVal uiParam As Integer, ByVal pvParam As System.Text.StringBuilder, ByVal winini As Integer) As Boolean

    Private Const SPI_SETDESKWALLPAPER As Integer = &H14
    Private Const SPI_GETDESKWALLPAPER As Integer = &H73

    Private Const SPIF_UPDATEINIFILE As Integer = &H1
    Private Const SPIF_SENDWININICHANGE As Integer = &H2
    Private Const SPIF_SENDCHANGE As Integer = SPIF_SENDWININICHANGE

    Public Shared Property Wallpaper() As String
        Get
            Dim sb As New System.Text.StringBuilder(260)
            If GetDeskWallpaper(SPI_GETDESKWALLPAPER, 260, sb, 0) Then
                Return sb.ToString
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            SetDeskWallpaper(SPI_SETDESKWALLPAPER, 0, value, SPIF_SENDCHANGE)
        End Set
    End Property

    Public Enum WallpaperStyleEnum As Integer
        Tiled
        Centered
        Stretched
    End Enum

    Public Shared Property WallpaperStyle() As WallpaperStyleEnum
        Get
            Dim key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
            Select Case key.GetValue("WallpaperStyle", 2.ToString())
                Case 2.ToString
                    Return WallpaperStyleEnum.Stretched
                Case 1.ToString
                    If key.GetValue("TileWallpaper", 0.ToString) = 0.ToString Then
                        Return WallpaperStyleEnum.Centered
                    Else
                        Return WallpaperStyleEnum.Tiled
                    End If
            End Select
        End Get
        Set(ByVal value As WallpaperStyleEnum)
            Dim key As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
            Select Case value
                Case WallpaperStyleEnum.Stretched
                    key.SetValue("WallpaperStyle", 2.ToString())
                    key.SetValue("TileWallpaper", 0.ToString())
                Case WallpaperStyleEnum.Centered
                    key.SetValue("WallpaperStyle", 1.ToString())
                    key.SetValue("TileWallpaper", 0.ToString())
                Case WallpaperStyleEnum.Tiled
                    key.SetValue("WallpaperStyle", 1.ToString())
                    key.SetValue("TileWallpaper", 1.ToString())
            End Select
            Wallpaper = Wallpaper
        End Set
    End Property

End Class
