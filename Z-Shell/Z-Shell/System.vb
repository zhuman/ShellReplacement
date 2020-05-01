Imports System.Runtime.InteropServices, System.ComponentModel, Microsoft.Win32

Public Module SystemInformationWritable

	Private Declare Auto Function SystemParametersInfo Lib "user32.dll" (ByVal uiAction As SPI, ByVal uiParam As Integer, ByVal param As IntPtr, ByVal fWinIni As Integer) As Boolean

	''' <summary>
	''' SPI_ System-wide parameter - Used in SystemParametersInfo function 
	''' </summary>
	<Description("SPI_(System-wide parameter - Used in SystemParametersInfo function )")> _
	Public Enum SPI As Integer
		''' <summary>
		''' Determines whether the warning beeper is on. 
		''' The pvParam parameter must point to a BOOL variable that receives TRUE if the beeper is on, or FALSE if it is off.
		''' </summary>
		SPI_GETBEEP = &H1

		''' <summary>
		''' Turns the warning beeper on or off. The uiParam parameter specifies TRUE for on, or FALSE for off.
		''' </summary>
		SPI_SETBEEP = &H2

		''' <summary>
		''' Retrieves the two mouse threshold values and the mouse speed.
		''' </summary>
		SPI_GETMOUSE = &H3

		''' <summary>
		''' Sets the two mouse threshold values and the mouse speed.
		''' </summary>
		SPI_SETMOUSE = &H4

		''' <summary>
		''' Retrieves the border multiplier factor that determines the width of a window's sizing border. 
		''' The pvParam parameter must point to an integer variable that receives this value.
		''' </summary>
		SPI_GETBORDER = &H5

		''' <summary>
		''' Sets the border multiplier factor that determines the width of a window's sizing border. 
		''' The uiParam parameter specifies the new value.
		''' </summary>
		SPI_SETBORDER = &H6

		''' <summary>
		''' Retrieves the keyboard repeat-speed setting, which is a value in the range from 0 (approximately 2.5 repetitions per second) 
		''' through 31 (approximately 30 repetitions per second). The actual repeat rates are hardware-dependent and may vary from 
		''' a linear scale by as much as 20%. The pvParam parameter must point to a DWORD variable that receives the setting
		''' </summary>
		SPI_GETKEYBOARDSPEED = &HA

		''' <summary>
		''' Sets the keyboard repeat-speed setting. The uiParam parameter must specify a value in the range from 0 
		''' (approximately 2.5 repetitions per second) through 31 (approximately 30 repetitions per second). 
		''' The actual repeat rates are hardware-dependent and may vary from a linear scale by as much as 20%. 
		''' If uiParam is greater than 31, the parameter is set to 31.
		''' </summary>
		SPI_SETKEYBOARDSPEED = &HB

		''' <summary>
		''' Not implemented.
		''' </summary>
		SPI_LANGDRIVER = &HC

		''' <summary>
		''' Sets or retrieves the width, in pixels, of an icon cell. The system uses this rectangle to arrange icons in large icon view. 
		''' To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than SM_CXICON.
		''' To retrieve this value, pvParam must point to an integer that receives the current value.
		''' </summary>
		SPI_ICONHORIZONTALSPACING = &HD

		''' <summary>
		''' Retrieves the screen saver time-out value, in seconds. The pvParam parameter must point to an integer variable that receives the value.
		''' </summary>
		SPI_GETSCREENSAVETIMEOUT = &HE

		''' <summary>
		''' Sets the screen saver time-out value to the value of the uiParam parameter. This value is the amount of time, in seconds, 
		''' that the system must be idle before the screen saver activates.
		''' </summary>
		SPI_SETSCREENSAVETIMEOUT = &HF

		''' <summary>
		''' Determines whether screen saving is enabled. The pvParam parameter must point to a bool variable that receives TRUE 
		''' if screen saving is enabled, or FALSE otherwise.
		''' </summary>
		SPI_GETSCREENSAVEACTIVE = &H10

		''' <summary>
		''' Sets the state of the screen saver. The uiParam parameter specifies TRUE to activate screen saving, or FALSE to deactivate it.
		''' </summary>
		SPI_SETSCREENSAVEACTIVE = &H11

		''' <summary>
		''' Retrieves the current granularity value of the desktop sizing grid. The pvParam parameter must point to an integer variable 
		''' that receives the granularity.
		''' </summary>
		SPI_GETGRIDGRANULARITY = &H12

		''' <summary>
		''' Sets the granularity of the desktop sizing grid to the value of the uiParam parameter.
		''' </summary>
		SPI_SETGRIDGRANULARITY = &H13

		''' <summary>
		''' Sets the desktop wallpaper. The value of the pvParam parameter determines the new wallpaper. To specify a wallpaper bitmap, 
		''' set pvParam to point to a null-terminated string containing the name of a bitmap file. Setting pvParam to "" removes the wallpaper. 
		''' Setting pvParam to SETWALLPAPER_DEFAULT or null reverts to the default wallpaper.
		''' </summary>
		SPI_SETDESKWALLPAPER = &H14

		''' <summary>
		''' Sets the current desktop pattern by causing Windows to read the Pattern= setting from the WIN.INI file.
		''' </summary>
		SPI_SETDESKPATTERN = &H15

		''' <summary>
		''' Retrieves the keyboard repeat-delay setting, which is a value in the range from 0 (approximately 250 ms delay) through 3 
		''' (approximately 1 second delay). The actual delay associated with each value may vary depending on the hardware. The pvParam parameter must point to an integer variable that receives the setting.
		''' </summary>
		SPI_GETKEYBOARDDELAY = &H16

		''' <summary>
		''' Sets the keyboard repeat-delay setting. The uiParam parameter must specify 0, 1, 2, or 3, where zero sets the shortest delay 
		''' (approximately 250 ms) and 3 sets the longest delay (approximately 1 second). The actual delay associated with each value may 
		''' vary depending on the hardware.
		''' </summary>
		SPI_SETKEYBOARDDELAY = &H17

		''' <summary>
		''' Sets or retrieves the height, in pixels, of an icon cell. 
		''' To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than SM_CYICON.
		''' To retrieve this value, pvParam must point to an integer that receives the current value.
		''' </summary>
		SPI_ICONVERTICALSPACING = &H18

		''' <summary>
		''' Determines whether icon-title wrapping is enabled. The pvParam parameter must point to a bool variable that receives TRUE 
		''' if enabled, or FALSE otherwise.
		''' </summary>
		SPI_GETICONTITLEWRAP = &H19

		''' <summary>
		''' Turns icon-title wrapping on or off. The uiParam parameter specifies TRUE for on, or FALSE for off.
		''' </summary>
		SPI_SETICONTITLEWRAP = &H1A

		''' <summary>
		''' Determines whether pop-up menus are left-aligned or right-aligned, relative to the corresponding menu-bar item. 
		''' The pvParam parameter must point to a bool variable that receives TRUE if left-aligned, or FALSE otherwise.
		''' </summary>
		SPI_GETMENUDROPALIGNMENT = &H1B

		''' <summary>
		''' Sets the alignment value of pop-up menus. The uiParam parameter specifies TRUE for right alignment, or FALSE for left alignment.
		''' </summary>
		SPI_SETMENUDROPALIGNMENT = &H1C

		''' <summary>
		''' Sets the width of the double-click rectangle to the value of the uiParam parameter. 
		''' The double-click rectangle is the rectangle within which the second click of a double-click must fall for it to be registered 
		''' as a double-click.
		''' To retrieve the width of the double-click rectangle, call GetSystemMetrics with the SM_CXDOUBLECLK flag.
		''' </summary>
		SPI_SETDOUBLECLKWIDTH = &H1D

		''' <summary>
		''' Sets the height of the double-click rectangle to the value of the uiParam parameter. 
		''' The double-click rectangle is the rectangle within which the second click of a double-click must fall for it to be registered 
		''' as a double-click.
		''' To retrieve the height of the double-click rectangle, call GetSystemMetrics with the SM_CYDOUBLECLK flag.
		''' </summary>
		SPI_SETDOUBLECLKHEIGHT = &H1E

		''' <summary>
		''' Retrieves the logical font information for the current icon-title font. The uiParam parameter specifies the size of a LOGFONT structure, 
		''' and the pvParam parameter must point to the LOGFONT structure to fill in.
		''' </summary>
		SPI_GETICONTITLELOGFONT = &H1F

		''' <summary>
		''' Sets the double-click time for the mouse to the value of the uiParam parameter. The double-click time is the maximum number 
		''' of milliseconds that can occur between the first and second clicks of a double-click. You can also call the SetDoubleClickTime 
		''' function to set the double-click time. To get the current double-click time, call the GetDoubleClickTime function.
		''' </summary>
		SPI_SETDOUBLECLICKTIME = &H20

		''' <summary>
		''' Swaps or restores the meaning of the left and right mouse buttons. The uiParam parameter specifies TRUE to swap the meanings 
		''' of the buttons, or FALSE to restore their original meanings.
		''' </summary>
		SPI_SETMOUSEBUTTONSWAP = &H21

		''' <summary>
		''' Sets the font that is used for icon titles. The uiParam parameter specifies the size of a LOGFONT structure, 
		''' and the pvParam parameter must point to a LOGFONT structure.
		''' </summary>
		SPI_SETICONTITLELOGFONT = &H22

		''' <summary>
		''' This flag is obsolete. Previous versions of the system use this flag to determine whether ALT+TAB fast task switching is enabled. 
		''' For Windows 95, Windows 98, and Windows NT version 4.0 and later, fast task switching is always enabled.
		''' </summary>
		SPI_GETFASTTASKSWITCH = &H23

		''' <summary>
		''' This flag is obsolete. Previous versions of the system use this flag to enable or disable ALT+TAB fast task switching. 
		''' For Windows 95, Windows 98, and Windows NT version 4.0 and later, fast task switching is always enabled.
		''' </summary>
		SPI_SETFASTTASKSWITCH = &H24

		''' <summary>
		''' Sets dragging of full windows either on or off. The uiParam parameter specifies TRUE for on, or FALSE for off. 
		''' Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
		''' </summary>
		SPI_SETDRAGFULLWINDOWS = &H25

		''' <summary>
		''' Determines whether dragging of full windows is enabled. The pvParam parameter must point to a BOOL variable that receives TRUE 
		''' if enabled, or FALSE otherwise. 
		''' Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
		''' </summary>
		SPI_GETDRAGFULLWINDOWS = &H26

		''' <summary>
		''' Retrieves the metrics associated with the nonclient area of nonminimized windows. The pvParam parameter must point 
		''' to a NONCLIENTMETRICS structure that receives the information. Set the cbSize member of this structure and the uiParam parameter 
		''' to sizeof(NONCLIENTMETRICS).
		''' </summary>
		SPI_GETNONCLIENTMETRICS = &H29

		''' <summary>
		''' Sets the metrics associated with the nonclient area of nonminimized windows. The pvParam parameter must point 
		''' to a NONCLIENTMETRICS structure that contains the new parameters. Set the cbSize member of this structure 
		''' and the uiParam parameter to sizeof(NONCLIENTMETRICS). Also, the lfHeight member of the LOGFONT structure must be a negative value.
		''' </summary>
		SPI_SETNONCLIENTMETRICS = &H2A

		''' <summary>
		''' Retrieves the metrics associated with minimized windows. The pvParam parameter must point to a MINIMIZEDMETRICS structure 
		''' that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(MINIMIZEDMETRICS).
		''' </summary>
		SPI_GETMINIMIZEDMETRICS = &H2B

		''' <summary>
		''' Sets the metrics associated with minimized windows. The pvParam parameter must point to a MINIMIZEDMETRICS structure 
		''' that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(MINIMIZEDMETRICS).
		''' </summary>
		SPI_SETMINIMIZEDMETRICS = &H2C

		''' <summary>
		''' Retrieves the metrics associated with icons. The pvParam parameter must point to an ICONMETRICS structure that receives 
		''' the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
		''' </summary>
		SPI_GETICONMETRICS = &H2D

		''' <summary>
		''' Sets the metrics associated with icons. The pvParam parameter must point to an ICONMETRICS structure that contains 
		''' the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
		''' </summary>
		SPI_SETICONMETRICS = &H2E

		''' <summary>
		''' Sets the size of the work area. The work area is the portion of the screen not obscured by the system taskbar 
		''' or by application desktop toolbars. The pvParam parameter is a pointer to a RECT structure that specifies the new work area rectangle, 
		''' expressed in virtual screen coordinates. In a system with multiple display monitors, the function sets the work area 
		''' of the monitor that contains the specified rectangle.
		''' </summary>
		SPI_SETWORKAREA = &H2F

		''' <summary>
		''' Retrieves the size of the work area on the primary display monitor. The work area is the portion of the screen not obscured 
		''' by the system taskbar or by application desktop toolbars. The pvParam parameter must point to a RECT structure that receives 
		''' the coordinates of the work area, expressed in virtual screen coordinates. 
		''' To get the work area of a monitor other than the primary display monitor, call the GetMonitorInfo function.
		''' </summary>
		SPI_GETWORKAREA = &H30

		''' <summary>
		''' Windows Me/98/95:  Pen windows is being loaded or unloaded. The uiParam parameter is TRUE when loading and FALSE 
		''' when unloading pen windows. The pvParam parameter is null.
		''' </summary>
		SPI_SETPENWINDOWS = &H31

		''' <summary>
		''' Retrieves information about the HighContrast accessibility feature. The pvParam parameter must point to a HIGHCONTRAST structure 
		''' that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(HIGHCONTRAST). 
		''' For a general discussion, see remarks.
		''' Windows NT:  This value is not supported.
		''' </summary>
		''' <remarks>
		''' There is a difference between the High Contrast color scheme and the High Contrast Mode. The High Contrast color scheme changes 
		''' the system colors to colors that have obvious contrast; you switch to this color scheme by using the Display Options in the control panel. 
		''' The High Contrast Mode, which uses SPI_GETHIGHCONTRAST and SPI_SETHIGHCONTRAST, advises applications to modify their appearance 
		''' for visually-impaired users. It involves such things as audible warning to users and customized color scheme 
		''' (using the Accessibility Options in the control panel). For more information, see HIGHCONTRAST on MSDN.
		''' For more information on general accessibility features, see Accessibility on MSDN.
		''' </remarks>
		SPI_GETHIGHCONTRAST = &H42

		''' <summary>
		''' Sets the parameters of the HighContrast accessibility feature. The pvParam parameter must point to a HIGHCONTRAST structure 
		''' that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(HIGHCONTRAST).
		''' Windows NT:  This value is not supported.
		''' </summary>
		SPI_SETHIGHCONTRAST = &H43

		''' <summary>
		''' Determines whether the user relies on the keyboard instead of the mouse, and wants applications to display keyboard interfaces 
		''' that would otherwise be hidden. The pvParam parameter must point to a BOOL variable that receives TRUE 
		''' if the user relies on the keyboard; or FALSE otherwise.
		''' Windows NT:  This value is not supported.
		''' </summary>
		SPI_GETKEYBOARDPREF = &H44

		''' <summary>
		''' Sets the keyboard preference. The uiParam parameter specifies TRUE if the user relies on the keyboard instead of the mouse, 
		''' and wants applications to display keyboard interfaces that would otherwise be hidden; uiParam is FALSE otherwise.
		''' Windows NT:  This value is not supported.
		''' </summary>
		SPI_SETKEYBOARDPREF = &H45

		''' <summary>
		''' Determines whether a screen reviewer utility is running. A screen reviewer utility directs textual information to an output device, 
		''' such as a speech synthesizer or Braille display. When this flag is set, an application should provide textual information 
		''' in situations where it would otherwise present the information graphically.
		''' The pvParam parameter is a pointer to a BOOL variable that receives TRUE if a screen reviewer utility is running, or FALSE otherwise.
		''' Windows NT:  This value is not supported.
		''' </summary>
		SPI_GETSCREENREADER = &H46

		''' <summary>
		''' Determines whether a screen review utility is running. The uiParam parameter specifies TRUE for on, or FALSE for off.
		''' Windows NT:  This value is not supported.
		''' </summary>
		SPI_SETSCREENREADER = &H47

		''' <summary>
		''' Retrieves the animation effects associated with user actions. The pvParam parameter must point to an ANIMATIONINFO structure 
		''' that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(ANIMATIONINFO).
		''' </summary>
		SPI_GETANIMATION = &H48

		''' <summary>
		''' Sets the animation effects associated with user actions. The pvParam parameter must point to an ANIMATIONINFO structure 
		''' that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(ANIMATIONINFO).
		''' </summary>
		SPI_SETANIMATION = &H49

		''' <summary>
		''' Determines whether the font smoothing feature is enabled. This feature uses font antialiasing to make font curves appear smoother 
		''' by painting pixels at different gray levels. 
		''' The pvParam parameter must point to a BOOL variable that receives TRUE if the feature is enabled, or FALSE if it is not.
		''' Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
		''' </summary>
		SPI_GETFONTSMOOTHING = &H4A

		''' <summary>
		''' Enables or disables the font smoothing feature, which uses font antialiasing to make font curves appear smoother 
		''' by painting pixels at different gray levels. 
		''' To enable the feature, set the uiParam parameter to TRUE. To disable the feature, set uiParam to FALSE.
		''' Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
		''' </summary>
		SPI_SETFONTSMOOTHING = &H4B

		''' <summary>
		''' Sets the width, in pixels, of the rectangle used to detect the start of a drag operation. Set uiParam to the new value. 
		''' To retrieve the drag width, call GetSystemMetrics with the SM_CXDRAG flag.
		''' </summary>
		SPI_SETDRAGWIDTH = &H4C

		''' <summary>
		''' Sets the height, in pixels, of the rectangle used to detect the start of a drag operation. Set uiParam to the new value. 
		''' To retrieve the drag height, call GetSystemMetrics with the SM_CYDRAG flag.
		''' </summary>
		SPI_SETDRAGHEIGHT = &H4D

		''' <summary>
		''' Used internally; applications should not use this value.
		''' </summary>
		SPI_SETHANDHELD = &H4E

		''' <summary>
		''' Retrieves the time-out value for the low-power phase of screen saving. The pvParam parameter must point to an integer variable 
		''' that receives the value. This flag is supported for 32-bit applications only.
		''' Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
		''' Windows 95:  This flag is supported for 16-bit applications only.
		''' </summary>
		SPI_GETLOWPOWERTIMEOUT = &H4F

		''' <summary>
		''' Retrieves the time-out value for the power-off phase of screen saving. The pvParam parameter must point to an integer variable 
		''' that receives the value. This flag is supported for 32-bit applications only.
		''' Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
		''' Windows 95:  This flag is supported for 16-bit applications only.
		''' </summary>
		SPI_GETPOWEROFFTIMEOUT = &H50

		''' <summary>
		''' Sets the time-out value, in seconds, for the low-power phase of screen saving. The uiParam parameter specifies the new value. 
		''' The pvParam parameter must be null. This flag is supported for 32-bit applications only.
		''' Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
		''' Windows 95:  This flag is supported for 16-bit applications only.
		''' </summary>
		SPI_SETLOWPOWERTIMEOUT = &H51

		''' <summary>
		''' Sets the time-out value, in seconds, for the power-off phase of screen saving. The uiParam parameter specifies the new value. 
		''' The pvParam parameter must be null. This flag is supported for 32-bit applications only.
		''' Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
		''' Windows 95:  This flag is supported for 16-bit applications only.
		''' </summary>
		SPI_SETPOWEROFFTIMEOUT = &H52

		''' <summary>
		''' Determines whether the low-power phase of screen saving is enabled. The pvParam parameter must point to a BOOL variable 
		''' that receives TRUE if enabled, or FALSE if disabled. This flag is supported for 32-bit applications only.
		''' Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
		''' Windows 95:  This flag is supported for 16-bit applications only.
		''' </summary>
		SPI_GETLOWPOWERACTIVE = &H53

		''' <summary>
		''' Determines whether the power-off phase of screen saving is enabled. The pvParam parameter must point to a BOOL variable 
		''' that receives TRUE if enabled, or FALSE if disabled. This flag is supported for 32-bit applications only.
		''' Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
		''' Windows 95:  This flag is supported for 16-bit applications only.
		''' </summary>
		SPI_GETPOWEROFFACTIVE = &H54

		''' <summary>
		''' Activates or deactivates the low-power phase of screen saving. Set uiParam to 1 to activate, or zero to deactivate. 
		''' The pvParam parameter must be null. This flag is supported for 32-bit applications only.
		''' Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
		''' Windows 95:  This flag is supported for 16-bit applications only.
		''' </summary>
		SPI_SETLOWPOWERACTIVE = &H55

		''' <summary>
		''' Activates or deactivates the power-off phase of screen saving. Set uiParam to 1 to activate, or zero to deactivate. 
		''' The pvParam parameter must be null. This flag is supported for 32-bit applications only.
		''' Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
		''' Windows 95:  This flag is supported for 16-bit applications only.
		''' </summary>
		SPI_SETPOWEROFFACTIVE = &H56

		''' <summary>
		''' Reloads the system cursors. Set the uiParam parameter to zero and the pvParam parameter to null.
		''' </summary>
		SPI_SETCURSORS = &H57

		''' <summary>
		''' Reloads the system icons. Set the uiParam parameter to zero and the pvParam parameter to null.
		''' </summary>
		SPI_SETICONS = &H58

		''' <summary>
		''' Retrieves the input locale identifier for the system default input language. The pvParam parameter must point 
		''' to an HKL variable that receives this value. For more information, see Languages, Locales, and Keyboard Layouts on MSDN.
		''' </summary>
		SPI_GETDEFAULTINPUTLANG = &H59

		''' <summary>
		''' Sets the default input language for the system shell and applications. The specified language must be displayable 
		''' using the current system character set. The pvParam parameter must point to an HKL variable that contains 
		''' the input locale identifier for the default language. For more information, see Languages, Locales, and Keyboard Layouts on MSDN.
		''' </summary>
		SPI_SETDEFAULTINPUTLANG = &H5A

		''' <summary>
		''' Sets the hot key set for switching between input languages. The uiParam and pvParam parameters are not used. 
		''' The value sets the shortcut keys in the keyboard property sheets by reading the registry again. The registry must be set before this flag is used. the path in the registry is \HKEY_CURRENT_USER\keyboard layout\toggle. Valid values are "1" = ALT+SHIFT, "2" = CTRL+SHIFT, and "3" = none.
		''' </summary>
		SPI_SETLANGTOGGLE = &H5B

		''' <summary>
		''' Windows 95:  Determines whether the Windows extension, Windows Plus!, is installed. Set the uiParam parameter to 1. 
		''' The pvParam parameter is not used. The function returns TRUE if the extension is installed, or FALSE if it is not.
		''' </summary>
		SPI_GETWINDOWSEXTENSION = &H5C

		''' <summary>
		''' Enables or disables the Mouse Trails feature, which improves the visibility of mouse cursor movements by briefly showing 
		''' a trail of cursors and quickly erasing them. 
		''' To disable the feature, set the uiParam parameter to zero or 1. To enable the feature, set uiParam to a value greater than 1 
		''' to indicate the number of cursors drawn in the trail.
		''' Windows 2000/NT:  This value is not supported.
		''' </summary>
		SPI_SETMOUSETRAILS = &H5D

		''' <summary>
		''' Determines whether the Mouse Trails feature is enabled. This feature improves the visibility of mouse cursor movements 
		''' by briefly showing a trail of cursors and quickly erasing them. 
		''' The pvParam parameter must point to an integer variable that receives a value. If the value is zero or 1, the feature is disabled. 
		''' If the value is greater than 1, the feature is enabled and the value indicates the number of cursors drawn in the trail. 
		''' The uiParam parameter is not used.
		''' Windows 2000/NT:  This value is not supported.
		''' </summary>
		SPI_GETMOUSETRAILS = &H5E

		''' <summary>
		''' Windows Me/98:  Used internally; applications should not use this flag.
		''' </summary>
		SPI_SETSCREENSAVERRUNNING = &H61

		''' <summary>
		''' Same as SPI_SETSCREENSAVERRUNNING.
		''' </summary>
		SPI_SCREENSAVERRUNNING = SPI_SETSCREENSAVERRUNNING

		''' <summary>
		''' Retrieves information about the FilterKeys accessibility feature. The pvParam parameter must point to a FILTERKEYS structure 
		''' that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(FILTERKEYS).
		''' </summary>
		SPI_GETFILTERKEYS = &H32

		''' <summary>
		''' Sets the parameters of the FilterKeys accessibility feature. The pvParam parameter must point to a FILTERKEYS structure 
		''' that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(FILTERKEYS).
		''' </summary>
		SPI_SETFILTERKEYS = &H33

		''' <summary>
		''' Retrieves information about the ToggleKeys accessibility feature. The pvParam parameter must point to a TOGGLEKEYS structure 
		''' that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(TOGGLEKEYS).
		''' </summary>
		SPI_GETTOGGLEKEYS = &H34

		''' <summary>
		''' Sets the parameters of the ToggleKeys accessibility feature. The pvParam parameter must point to a TOGGLEKEYS structure 
		''' that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(TOGGLEKEYS).
		''' </summary>
		SPI_SETTOGGLEKEYS = &H35

		''' <summary>
		''' Retrieves information about the MouseKeys accessibility feature. The pvParam parameter must point to a MOUSEKEYS structure 
		''' that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(MOUSEKEYS).
		''' </summary>
		SPI_GETMOUSEKEYS = &H36

		''' <summary>
		''' Sets the parameters of the MouseKeys accessibility feature. The pvParam parameter must point to a MOUSEKEYS structure 
		''' that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(MOUSEKEYS).
		''' </summary>
		SPI_SETMOUSEKEYS = &H37

		''' <summary>
		''' Determines whether the Show Sounds accessibility flag is on or off. If it is on, the user requires an application 
		''' to present information visually in situations where it would otherwise present the information only in audible form. 
		''' The pvParam parameter must point to a BOOL variable that receives TRUE if the feature is on, or FALSE if it is off. 
		''' Using this value is equivalent to calling GetSystemMetrics (SM_SHOWSOUNDS). That is the recommended call.
		''' </summary>
		SPI_GETSHOWSOUNDS = &H38

		''' <summary>
		''' Sets the parameters of the SoundSentry accessibility feature. The pvParam parameter must point to a SOUNDSENTRY structure 
		''' that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(SOUNDSENTRY).
		''' </summary>
		SPI_SETSHOWSOUNDS = &H39

		''' <summary>
		''' Retrieves information about the StickyKeys accessibility feature. The pvParam parameter must point to a STICKYKEYS structure 
		''' that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(STICKYKEYS).
		''' </summary>
		SPI_GETSTICKYKEYS = &H3A

		''' <summary>
		''' Sets the parameters of the StickyKeys accessibility feature. The pvParam parameter must point to a STICKYKEYS structure 
		''' that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(STICKYKEYS).
		''' </summary>
		SPI_SETSTICKYKEYS = &H3B

		''' <summary>
		''' Retrieves information about the time-out period associated with the accessibility features. The pvParam parameter must point 
		''' to an ACCESSTIMEOUT structure that receives the information. Set the cbSize member of this structure and the uiParam parameter 
		''' to sizeof(ACCESSTIMEOUT).
		''' </summary>
		SPI_GETACCESSTIMEOUT = &H3C

		''' <summary>
		''' Sets the time-out period associated with the accessibility features. The pvParam parameter must point to an ACCESSTIMEOUT 
		''' structure that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(ACCESSTIMEOUT).
		''' </summary>
		SPI_SETACCESSTIMEOUT = &H3D

		''' <summary>
		''' Windows Me/98/95:  Retrieves information about the SerialKeys accessibility feature. The pvParam parameter must point 
		''' to a SERIALKEYS structure that receives the information. Set the cbSize member of this structure and the uiParam parameter 
		''' to sizeof(SERIALKEYS).
		''' Windows Server 2003, Windows XP/2000/NT:  Not supported. The user controls this feature through the control panel.
		''' </summary>
		SPI_GETSERIALKEYS = &H3E

		''' <summary>
		''' Windows Me/98/95:  Sets the parameters of the SerialKeys accessibility feature. The pvParam parameter must point 
		''' to a SERIALKEYS structure that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter 
		''' to sizeof(SERIALKEYS). 
		''' Windows Server 2003, Windows XP/2000/NT:  Not supported. The user controls this feature through the control panel.
		''' </summary>
		SPI_SETSERIALKEYS = &H3F

		''' <summary>
		''' Retrieves information about the SoundSentry accessibility feature. The pvParam parameter must point to a SOUNDSENTRY structure 
		''' that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(SOUNDSENTRY).
		''' </summary>
		SPI_GETSOUNDSENTRY = &H40

		''' <summary>
		''' Sets the parameters of the SoundSentry accessibility feature. The pvParam parameter must point to a SOUNDSENTRY structure 
		''' that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(SOUNDSENTRY).
		''' </summary>
		SPI_SETSOUNDSENTRY = &H41

		''' <summary>
		''' Determines whether the snap-to-default-button feature is enabled. If enabled, the mouse cursor automatically moves 
		''' to the default button, such as OK or Apply, of a dialog box. The pvParam parameter must point to a BOOL variable 
		''' that receives TRUE if the feature is on, or FALSE if it is off. 
		''' Windows 95:  Not supported.
		''' </summary>
		SPI_GETSNAPTODEFBUTTON = &H5F

		''' <summary>
		''' Enables or disables the snap-to-default-button feature. If enabled, the mouse cursor automatically moves to the default button, 
		''' such as OK or Apply, of a dialog box. Set the uiParam parameter to TRUE to enable the feature, or FALSE to disable it. 
		''' Applications should use the ShowWindow function when displaying a dialog box so the dialog manager can position the mouse cursor. 
		''' Windows 95:  Not supported.
		''' </summary>
		SPI_SETSNAPTODEFBUTTON = &H60

		''' <summary>
		''' Retrieves the width, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent 
		''' to generate a WM_MOUSEHOVER message. The pvParam parameter must point to a UINT variable that receives the width. 
		''' Windows 95:  Not supported.
		''' </summary>
		SPI_GETMOUSEHOVERWIDTH = &H62

		''' <summary>
		''' Retrieves the width, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent 
		''' to generate a WM_MOUSEHOVER message. The pvParam parameter must point to a UINT variable that receives the width. 
		''' Windows 95:  Not supported.
		''' </summary>
		SPI_SETMOUSEHOVERWIDTH = &H63

		''' <summary>
		''' Retrieves the height, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent 
		''' to generate a WM_MOUSEHOVER message. The pvParam parameter must point to a UINT variable that receives the height. 
		''' Windows 95:  Not supported.
		''' </summary>
		SPI_GETMOUSEHOVERHEIGHT = &H64

		''' <summary>
		''' Sets the height, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent 
		''' to generate a WM_MOUSEHOVER message. Set the uiParam parameter to the new height.
		''' Windows 95:  Not supported.
		''' </summary>
		SPI_SETMOUSEHOVERHEIGHT = &H65

		''' <summary>
		''' Retrieves the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle for TrackMouseEvent 
		''' to generate a WM_MOUSEHOVER message. The pvParam parameter must point to a UINT variable that receives the time. 
		''' Windows 95:  Not supported.
		''' </summary>
		SPI_GETMOUSEHOVERTIME = &H66

		''' <summary>
		''' Sets the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle for TrackMouseEvent 
		''' to generate a WM_MOUSEHOVER message. This is used only if you pass HOVER_DEFAULT in the dwHoverTime parameter in the call to TrackMouseEvent. Set the uiParam parameter to the new time. 
		''' Windows 95:  Not supported.
		''' </summary>
		SPI_SETMOUSEHOVERTIME = &H67

		''' <summary>
		''' Retrieves the number of lines to scroll when the mouse wheel is rotated. The pvParam parameter must point 
		''' to a UINT variable that receives the number of lines. The default value is 3. 
		''' Windows 95:  Not supported.
		''' </summary>
		SPI_GETWHEELSCROLLLINES = &H68

		''' <summary>
		''' Sets the number of lines to scroll when the mouse wheel is rotated. The number of lines is set from the uiParam parameter. 
		''' The number of lines is the suggested number of lines to scroll when the mouse wheel is rolled without using modifier keys. 
		''' If the number is 0, then no scrolling should occur. If the number of lines to scroll is greater than the number of lines viewable, 
		''' and in particular if it is WHEEL_PAGESCROLL (#defined as UINT_MAX), the scroll operation should be interpreted 
		''' as clicking once in the page down or page up regions of the scroll bar.
		''' Windows 95:  Not supported.
		''' </summary>
		SPI_SETWHEELSCROLLLINES = &H69

		''' <summary>
		''' Retrieves the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is 
		''' over a submenu item. The pvParam parameter must point to a DWORD variable that receives the time of the delay. 
		''' Windows 95:  Not supported.
		''' </summary>
		SPI_GETMENUSHOWDELAY = &H6A

		''' <summary>
		''' Sets uiParam to the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is 
		''' over a submenu item. 
		''' Windows 95:  Not supported.
		''' </summary>
		SPI_SETMENUSHOWDELAY = &H6B

		''' <summary>
		''' Determines whether the IME status window is visible (on a per-user basis). The pvParam parameter must point to a BOOL variable 
		''' that receives TRUE if the status window is visible, or FALSE if it is not.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETSHOWIMEUI = &H6E

		''' <summary>
		''' Sets whether the IME status window is visible or not on a per-user basis. The uiParam parameter specifies TRUE for on or FALSE for off.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_SETSHOWIMEUI = &H6F

		''' <summary>
		''' Retrieves the current mouse speed. The mouse speed determines how far the pointer will move based on the distance the mouse moves. 
		''' The pvParam parameter must point to an integer that receives a value which ranges between 1 (slowest) and 20 (fastest). 
		''' A value of 10 is the default. The value can be set by an end user using the mouse control panel application or 
		''' by an application using SPI_SETMOUSESPEED.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETMOUSESPEED = &H70

		''' <summary>
		''' Sets the current mouse speed. The pvParam parameter is an integer between 1 (slowest) and 20 (fastest). A value of 10 is the default. 
		''' This value is typically set using the mouse control panel application.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_SETMOUSESPEED = &H71

		''' <summary>
		''' Determines whether a screen saver is currently running on the window station of the calling process. 
		''' The pvParam parameter must point to a BOOL variable that receives TRUE if a screen saver is currently running, or FALSE otherwise.
		''' Note that only the interactive window station, "WinSta0", can have a screen saver running.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETSCREENSAVERRUNNING = &H72

		''' <summary>
		''' Retrieves the full path of the bitmap file for the desktop wallpaper. The pvParam parameter must point to a buffer 
		''' that receives a null-terminated path string. Set the uiParam parameter to the size, in characters, of the pvParam buffer. The returned string will not exceed MAX_PATH characters. If there is no desktop wallpaper, the returned string is empty.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETDESKWALLPAPER = &H73

		''' <summary>
		''' Determines whether active window tracking (activating the window the mouse is on) is on or off. The pvParam parameter must point 
		''' to a BOOL variable that receives TRUE for on, or FALSE for off.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETACTIVEWINDOWTRACKING = &H1000

		''' <summary>
		''' Sets active window tracking (activating the window the mouse is on) either on or off. Set pvParam to TRUE for on or FALSE for off.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_SETACTIVEWINDOWTRACKING = &H1001

		''' <summary>
		''' Determines whether the menu animation feature is enabled. This master switch must be on to enable menu animation effects. 
		''' The pvParam parameter must point to a BOOL variable that receives TRUE if animation is enabled and FALSE if it is disabled. 
		''' If animation is enabled, SPI_GETMENUFADE indicates whether menus use fade or slide animation.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETMENUANIMATION = &H1002

		''' <summary>
		''' Enables or disables menu animation. This master switch must be on for any menu animation to occur. 
		''' The pvParam parameter is a BOOL variable; set pvParam to TRUE to enable animation and FALSE to disable animation.
		''' If animation is enabled, SPI_GETMENUFADE indicates whether menus use fade or slide animation.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_SETMENUANIMATION = &H1003

		''' <summary>
		''' Determines whether the slide-open effect for combo boxes is enabled. The pvParam parameter must point to a BOOL variable 
		''' that receives TRUE for enabled, or FALSE for disabled.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETCOMBOBOXANIMATION = &H1004

		''' <summary>
		''' Enables or disables the slide-open effect for combo boxes. Set the pvParam parameter to TRUE to enable the gradient effect, 
		''' or FALSE to disable it.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_SETCOMBOBOXANIMATION = &H1005

		''' <summary>
		''' Determines whether the smooth-scrolling effect for list boxes is enabled. The pvParam parameter must point to a BOOL variable 
		''' that receives TRUE for enabled, or FALSE for disabled.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETLISTBOXSMOOTHSCROLLING = &H1006

		''' <summary>
		''' Enables or disables the smooth-scrolling effect for list boxes. Set the pvParam parameter to TRUE to enable the smooth-scrolling effect
		''' or FALSE to disable it.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_SETLISTBOXSMOOTHSCROLLING = &H1007

		''' <summary>
		''' Determines whether the gradient effect for window title bars is enabled. The pvParam parameter must point to a BOOL variable 
		''' that receives TRUE for enabled, or FALSE for disabled. For more information about the gradient effect, see the GetSysColor function.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETGRADIENTCAPTIONS = &H1008

		''' <summary>
		''' Enables or disables the gradient effect for window title bars. Set the pvParam parameter to TRUE to enable it, or FALSE to disable it. 
		''' The gradient effect is possible only if the system has a color depth of more than 256 colors. For more information about 
		''' the gradient effect, see the GetSysColor function.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_SETGRADIENTCAPTIONS = &H1009

		''' <summary>
		''' Determines whether menu access keys are always underlined. The pvParam parameter must point to a BOOL variable that receives TRUE 
		''' if menu access keys are always underlined, and FALSE if they are underlined only when the menu is activated by the keyboard.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETKEYBOARDCUES = &H100A

		''' <summary>
		''' Sets the underlining of menu access key letters. The pvParam parameter is a BOOL variable. Set pvParam to TRUE to always underline menu 
		''' access keys, or FALSE to underline menu access keys only when the menu is activated from the keyboard.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_SETKEYBOARDCUES = &H100B

		''' <summary>
		''' Same as SPI_GETKEYBOARDCUES.
		''' </summary>
		SPI_GETMENUUNDERLINES = SPI_GETKEYBOARDCUES

		''' <summary>
		''' Same as SPI_SETKEYBOARDCUES.
		''' </summary>
		SPI_SETMENUUNDERLINES = SPI_SETKEYBOARDCUES

		''' <summary>
		''' Determines whether windows activated through active window tracking will be brought to the top. The pvParam parameter must point 
		''' to a BOOL variable that receives TRUE for on, or FALSE for off.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETACTIVEWNDTRKZORDER = &H100C

		''' <summary>
		''' Determines whether or not windows activated through active window tracking should be brought to the top. Set pvParam to TRUE 
		''' for on or FALSE for off.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_SETACTIVEWNDTRKZORDER = &H100D

		''' <summary>
		''' Determines whether hot tracking of user-interface elements, such as menu names on menu bars, is enabled. The pvParam parameter 
		''' must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled. 
		''' Hot tracking means that when the cursor moves over an item, it is highlighted but not selected. You can query this value to decide 
		''' whether to use hot tracking in the user interface of your application.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETHOTTRACKING = &H100E

		''' <summary>
		''' Enables or disables hot tracking of user-interface elements such as menu names on menu bars. Set the pvParam parameter to TRUE 
		''' to enable it, or FALSE to disable it.
		''' Hot-tracking means that when the cursor moves over an item, it is highlighted but not selected.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_SETHOTTRACKING = &H100F

		''' <summary>
		''' Determines whether menu fade animation is enabled. The pvParam parameter must point to a BOOL variable that receives TRUE 
		''' when fade animation is enabled and FALSE when it is disabled. If fade animation is disabled, menus use slide animation. 
		''' This flag is ignored unless menu animation is enabled, which you can do using the SPI_SETMENUANIMATION flag. 
		''' For more information, see AnimateWindow.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETMENUFADE = &H1012

		''' <summary>
		''' Enables or disables menu fade animation. Set pvParam to TRUE to enable the menu fade effect or FALSE to disable it. 
		''' If fade animation is disabled, menus use slide animation. he The menu fade effect is possible only if the system 
		''' has a color depth of more than 256 colors. This flag is ignored unless SPI_MENUANIMATION is also set. For more information, 
		''' see AnimateWindow.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_SETMENUFADE = &H1013

		''' <summary>
		''' Determines whether the selection fade effect is enabled. The pvParam parameter must point to a BOOL variable that receives TRUE 
		''' if enabled or FALSE if disabled. 
		''' The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out 
		''' after the menu is dismissed.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETSELECTIONFADE = &H1014

		''' <summary>
		''' Set pvParam to TRUE to enable the selection fade effect or FALSE to disable it.
		''' The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out 
		''' after the menu is dismissed. The selection fade effect is possible only if the system has a color depth of more than 256 colors.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_SETSELECTIONFADE = &H1015

		''' <summary>
		''' Determines whether ToolTip animation is enabled. The pvParam parameter must point to a BOOL variable that receives TRUE 
		''' if enabled or FALSE if disabled. If ToolTip animation is enabled, SPI_GETTOOLTIPFADE indicates whether ToolTips use fade or slide animation.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETTOOLTIPANIMATION = &H1016

		''' <summary>
		''' Set pvParam to TRUE to enable ToolTip animation or FALSE to disable it. If enabled, you can use SPI_SETTOOLTIPFADE 
		''' to specify fade or slide animation.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_SETTOOLTIPANIMATION = &H1017

		''' <summary>
		''' If SPI_SETTOOLTIPANIMATION is enabled, SPI_GETTOOLTIPFADE indicates whether ToolTip animation uses a fade effect or a slide effect.
		'''  The pvParam parameter must point to a BOOL variable that receives TRUE for fade animation or FALSE for slide animation. 
		'''  For more information on slide and fade effects, see AnimateWindow.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETTOOLTIPFADE = &H1018

		''' <summary>
		''' If the SPI_SETTOOLTIPANIMATION flag is enabled, use SPI_SETTOOLTIPFADE to indicate whether ToolTip animation uses a fade effect 
		''' or a slide effect. Set pvParam to TRUE for fade animation or FALSE for slide animation. The tooltip fade effect is possible only 
		''' if the system has a color depth of more than 256 colors. For more information on the slide and fade effects, 
		''' see the AnimateWindow function.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_SETTOOLTIPFADE = &H1019

		''' <summary>
		''' Determines whether the cursor has a shadow around it. The pvParam parameter must point to a BOOL variable that receives TRUE 
		''' if the shadow is enabled, FALSE if it is disabled. This effect appears only if the system has a color depth of more than 256 colors.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETCURSORSHADOW = &H101A

		''' <summary>
		''' Enables or disables a shadow around the cursor. The pvParam parameter is a BOOL variable. Set pvParam to TRUE to enable the shadow 
		''' or FALSE to disable the shadow. This effect appears only if the system has a color depth of more than 256 colors.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_SETCURSORSHADOW = &H101B

		''' <summary>
		''' Retrieves the state of the Mouse Sonar feature. The pvParam parameter must point to a BOOL variable that receives TRUE 
		''' if enabled or FALSE otherwise. For more information, see About Mouse Input on MSDN.
		''' Windows 2000/NT, Windows 98/95:  This value is not supported.
		''' </summary>
		SPI_GETMOUSESONAR = &H101C

		''' <summary>
		''' Turns the Sonar accessibility feature on or off. This feature briefly shows several concentric circles around the mouse pointer 
		''' when the user presses and releases the CTRL key. The pvParam parameter specifies TRUE for on and FALSE for off. The default is off. 
		''' For more information, see About Mouse Input.
		''' Windows 2000/NT, Windows 98/95:  This value is not supported.
		''' </summary>
		SPI_SETMOUSESONAR = &H101D

		''' <summary>
		''' Retrieves the state of the Mouse ClickLock feature. The pvParam parameter must point to a BOOL variable that receives TRUE 
		''' if enabled, or FALSE otherwise. For more information, see About Mouse Input.
		''' Windows 2000/NT, Windows 98/95:  This value is not supported.
		''' </summary>
		SPI_GETMOUSECLICKLOCK = &H101E

		''' <summary>
		''' Turns the Mouse ClickLock accessibility feature on or off. This feature temporarily locks down the primary mouse button 
		''' when that button is clicked and held down for the time specified by SPI_SETMOUSECLICKLOCKTIME. The uiParam parameter specifies 
		''' TRUE for on, 
		''' or FALSE for off. The default is off. For more information, see Remarks and About Mouse Input on MSDN.
		''' Windows 2000/NT, Windows 98/95:  This value is not supported.
		''' </summary>
		SPI_SETMOUSECLICKLOCK = &H101F

		''' <summary>
		''' Retrieves the state of the Mouse Vanish feature. The pvParam parameter must point to a BOOL variable that receives TRUE 
		''' if enabled or FALSE otherwise. For more information, see About Mouse Input on MSDN.
		''' Windows 2000/NT, Windows 98/95:  This value is not supported.
		''' </summary>
		SPI_GETMOUSEVANISH = &H1020

		''' <summary>
		''' Turns the Vanish feature on or off. This feature hides the mouse pointer when the user types; the pointer reappears 
		''' when the user moves the mouse. The pvParam parameter specifies TRUE for on and FALSE for off. The default is off. 
		''' For more information, see About Mouse Input on MSDN.
		''' Windows 2000/NT, Windows 98/95:  This value is not supported.
		''' </summary>
		SPI_SETMOUSEVANISH = &H1021

		''' <summary>
		''' Determines whether native User menus have flat menu appearance. The pvParam parameter must point to a BOOL variable 
		''' that returns TRUE if the flat menu appearance is set, or FALSE otherwise.
		''' Windows 2000/NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETFLATMENU = &H1022

		''' <summary>
		''' Enables or disables flat menu appearance for native User menus. Set pvParam to TRUE to enable flat menu appearance 
		''' or FALSE to disable it. 
		''' When enabled, the menu bar uses COLOR_MENUBAR for the menubar background, COLOR_MENU for the menu-popup background, COLOR_MENUHILIGHT 
		''' for the fill of the current menu selection, and COLOR_HILIGHT for the outline of the current menu selection. 
		''' If disabled, menus are drawn using the same metrics and colors as in Windows 2000 and earlier.
		''' Windows 2000/NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_SETFLATMENU = &H1023

		''' <summary>
		''' Determines whether the drop shadow effect is enabled. The pvParam parameter must point to a BOOL variable that returns TRUE 
		''' if enabled or FALSE if disabled.
		''' Windows 2000/NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETDROPSHADOW = &H1024

		''' <summary>
		''' Enables or disables the drop shadow effect. Set pvParam to TRUE to enable the drop shadow effect or FALSE to disable it. 
		''' You must also have CS_DROPSHADOW in the window class style.
		''' Windows 2000/NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_SETDROPSHADOW = &H1025

		''' <summary>
		''' Retrieves a BOOL indicating whether an application can reset the screensaver's timer by calling the SendInput function 
		''' to simulate keyboard or mouse input. The pvParam parameter must point to a BOOL variable that receives TRUE 
		''' if the simulated input will be blocked, or FALSE otherwise. 
		''' </summary>
		SPI_GETBLOCKSENDINPUTRESETS = &H1026

		''' <summary>
		''' Determines whether an application can reset the screensaver's timer by calling the SendInput function to simulate keyboard 
		''' or mouse input. The uiParam parameter specifies TRUE if the screensaver will not be deactivated by simulated input, 
		''' or FALSE if the screensaver will be deactivated by simulated input.
		''' </summary>
		SPI_SETBLOCKSENDINPUTRESETS = &H1027

		''' <summary>
		''' Determines whether UI effects are enabled or disabled. The pvParam parameter must point to a BOOL variable that receives TRUE 
		''' if all UI effects are enabled, or FALSE if they are disabled.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETUIEFFECTS = &H103E

		''' <summary>
		''' Enables or disables UI effects. Set the pvParam parameter to TRUE to enable all UI effects or FALSE to disable all UI effects.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_SETUIEFFECTS = &H103F

		''' <summary>
		''' Retrieves the amount of time following user input, in milliseconds, during which the system will not allow applications 
		''' to force themselves into the foreground. The pvParam parameter must point to a DWORD variable that receives the time.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETFOREGROUNDLOCKTIMEOUT = &H2000

		''' <summary>
		''' Sets the amount of time following user input, in milliseconds, during which the system does not allow applications 
		''' to force themselves into the foreground. Set pvParam to the new timeout value.
		''' The calling thread must be able to change the foreground window, otherwise the call fails.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_SETFOREGROUNDLOCKTIMEOUT = &H2001

		''' <summary>
		''' Retrieves the active window tracking delay, in milliseconds. The pvParam parameter must point to a DWORD variable 
		''' that receives the time.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETACTIVEWNDTRKTIMEOUT = &H2002

		''' <summary>
		''' Sets the active window tracking delay. Set pvParam to the number of milliseconds to delay before activating the window 
		''' under the mouse pointer.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_SETACTIVEWNDTRKTIMEOUT = &H2003

		''' <summary>
		''' Retrieves the number of times SetForegroundWindow will flash the taskbar button when rejecting a foreground switch request. 
		''' The pvParam parameter must point to a DWORD variable that receives the value.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_GETFOREGROUNDFLASHCOUNT = &H2004

		''' <summary>
		''' Sets the number of times SetForegroundWindow will flash the taskbar button when rejecting a foreground switch request. 
		''' Set pvParam to the number of times to flash.
		''' Windows NT, Windows 95:  This value is not supported.
		''' </summary>
		SPI_SETFOREGROUNDFLASHCOUNT = &H2005

		''' <summary>
		''' Retrieves the caret width in edit controls, in pixels. The pvParam parameter must point to a DWORD that receives this value.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETCARETWIDTH = &H2006

		''' <summary>
		''' Sets the caret width in edit controls. Set pvParam to the desired width, in pixels. The default and minimum value is 1.
		''' Windows NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_SETCARETWIDTH = &H2007

		''' <summary>
		''' Retrieves the time delay before the primary mouse button is locked. The pvParam parameter must point to DWORD that receives 
		''' the time delay. This is only enabled if SPI_SETMOUSECLICKLOCK is set to TRUE. For more information, see About Mouse Input on MSDN.
		''' Windows 2000/NT, Windows 98/95:  This value is not supported.
		''' </summary>
		SPI_GETMOUSECLICKLOCKTIME = &H2008

		''' <summary>
		''' Turns the Mouse ClickLock accessibility feature on or off. This feature temporarily locks down the primary mouse button 
		''' when that button is clicked and held down for the time specified by SPI_SETMOUSECLICKLOCKTIME. The uiParam parameter 
		''' specifies TRUE for on, or FALSE for off. The default is off. For more information, see Remarks and About Mouse Input on MSDN.
		''' Windows 2000/NT, Windows 98/95:  This value is not supported.
		''' </summary>
		SPI_SETMOUSECLICKLOCKTIME = &H2009

		''' <summary>
		''' Retrieves the type of font smoothing. The pvParam parameter must point to a UINT that receives the information.
		''' Windows 2000/NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETFONTSMOOTHINGTYPE = &H200A

		''' <summary>
		''' Sets the font smoothing type. The pvParam parameter points to a UINT that contains either FE_FONTSMOOTHINGSTANDARD, 
		''' if standard anti-aliasing is used, or FE_FONTSMOOTHINGCLEARTYPE, if ClearType is used. The default is FE_FONTSMOOTHINGSTANDARD. 
		''' When using this option, the fWinIni parameter must be set to SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE; otherwise, 
		''' SystemParametersInfo fails.
		''' </summary>
		SPI_SETFONTSMOOTHINGTYPE = &H200B

		''' <summary>
		''' Retrieves a contrast value that is used in ClearType™ smoothing. The pvParam parameter must point to a UINT 
		''' that receives the information.
		''' Windows 2000/NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETFONTSMOOTHINGCONTRAST = &H200C

		''' <summary>
		''' Sets the contrast value used in ClearType smoothing. The pvParam parameter points to a UINT that holds the contrast value. 
		''' Valid contrast values are from 1000 to 2200. The default value is 1400.
		''' When using this option, the fWinIni parameter must be set to SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE; otherwise, 
		''' SystemParametersInfo fails.
		''' SPI_SETFONTSMOOTHINGTYPE must also be set to FE_FONTSMOOTHINGCLEARTYPE.
		''' Windows 2000/NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_SETFONTSMOOTHINGCONTRAST = &H200D

		''' <summary>
		''' Retrieves the width, in pixels, of the left and right edges of the focus rectangle drawn with DrawFocusRect. 
		''' The pvParam parameter must point to a UINT.
		''' Windows 2000/NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETFOCUSBORDERWIDTH = &H200E

		''' <summary>
		''' Sets the height of the left and right edges of the focus rectangle drawn with DrawFocusRect to the value of the pvParam parameter.
		''' Windows 2000/NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_SETFOCUSBORDERWIDTH = &H200F

		''' <summary>
		''' Retrieves the height, in pixels, of the top and bottom edges of the focus rectangle drawn with DrawFocusRect. 
		''' The pvParam parameter must point to a UINT.
		''' Windows 2000/NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_GETFOCUSBORDERHEIGHT = &H2010

		''' <summary>
		''' Sets the height of the top and bottom edges of the focus rectangle drawn with DrawFocusRect to the value of the pvParam parameter.
		''' Windows 2000/NT, Windows Me/98/95:  This value is not supported.
		''' </summary>
		SPI_SETFOCUSBORDERHEIGHT = &H2011

		''' <summary>
		''' Not implemented.
		''' </summary>
		SPI_GETFONTSMOOTHINGORIENTATION = &H2012

		''' <summary>
		''' Not implemented.
		''' </summary>
		SPI_SETFONTSMOOTHINGORIENTATION = &H2013
	End Enum

	Private Enum SystemMetric As Integer
		''' <summary>
		'''  Width of the screen of the primary display monitor in pixels. This is the same values obtained by calling GetDeviceCaps as follows: GetDeviceCaps( hdcPrimaryMonitor HORZRES).
		''' </summary>
		SM_CXSCREEN = 0
		''' <summary>
		''' Height of the screen of the primary display monitor in pixels. This is the same values obtained by calling GetDeviceCaps as follows: GetDeviceCaps( hdcPrimaryMonitor VERTRES).
		''' </summary>
		SM_CYSCREEN = 1
		''' <summary>
		''' Width of a horizontal scroll bar in pixels.
		''' </summary>
		SM_CYVSCROLL = 2
		''' <summary>
		''' Height of a horizontal scroll bar in pixels.
		''' </summary>
		SM_CXVSCROLL = 3
		''' <summary>
		''' Height of a caption area in pixels.
		''' </summary>
		SM_CYCAPTION = 4
		''' <summary>
		''' Width of a window border in pixels. This is equivalent to the SM_CXEDGE value for windows with the 3-D look. 
		''' </summary>
		SM_CXBORDER = 5
		''' <summary>
		''' Height of a window border in pixels. This is equivalent to the SM_CYEDGE value for windows with the 3-D look. 
		''' </summary>
		SM_CYBORDER = 6
		''' <summary>
		''' Thickness of the frame around the perimeter of a window that has a caption but is not sizable in pixels. SM_CXFIXEDFRAME is the height of the horizontal border and SM_CYFIXEDFRAME is the width of the vertical border. 
		''' </summary>
		SM_CXDLGFRAME = 7
		''' <summary>
		''' Thickness of the frame around the perimeter of a window that has a caption but is not sizable in pixels. SM_CXFIXEDFRAME is the height of the horizontal border and SM_CYFIXEDFRAME is the width of the vertical border. 
		''' </summary>
		SM_CYDLGFRAME = 8
		''' <summary>
		''' Height of the thumb box in a vertical scroll bar in pixels
		''' </summary>
		SM_CYVTHUMB = 9
		''' <summary>
		''' Width of the thumb box in a horizontal scroll bar in pixels.
		''' </summary>
		SM_CXHTHUMB = 10
		''' <summary>
		''' Default width of an icon in pixels. The LoadIcon function can load only icons with the dimensions specified by SM_CXICON and SM_CYICON
		''' </summary>
		SM_CXICON = 11
		''' <summary>
		''' Default height of an icon in pixels. The LoadIcon function can load only icons with the dimensions SM_CXICON and SM_CYICON.
		''' </summary>
		SM_CYICON = 12
		''' <summary>
		''' Width of a cursor in pixels. The system cannot create cursors of other sizes.
		''' </summary>
		SM_CXCURSOR = 13
		''' <summary>
		''' Height of a cursor in pixels. The system cannot create cursors of other sizes.
		''' </summary>
		SM_CYCURSOR = 14
		''' <summary>
		''' Height of a single-line menu bar in pixels.
		''' </summary>
		SM_CYMENU = 15
		''' <summary>
		''' Width of the client area for a full-screen window on the primary display monitor in pixels. To get the coordinates of the portion of the screen not obscured by the system taskbar or by application desktop toolbars call the SystemParametersInfo function with the SPI_GETWORKAREA value.
		''' </summary>
		SM_CXFULLSCREEN = 16
		''' <summary>
		''' Height of the client area for a full-screen window on the primary display monitor in pixels. To get the coordinates of the portion of the screen not obscured by the system taskbar or by application desktop toolbars call the SystemParametersInfo function with the SPI_GETWORKAREA value.
		''' </summary>
		SM_CYFULLSCREEN = 17
		''' <summary>
		''' For double byte character set versions of the system this is the height of the Kanji window at the bottom of the screen in pixels
		''' </summary>
		SM_CYKANJIWINDOW = 18
		''' <summary>
		''' Nonzero if a mouse with a wheel is installed; zero otherwise
		''' </summary>
		SM_MOUSEWHEELPRESENT = 75
		''' <summary>
		''' Height of the arrow bitmap on a vertical scroll bar in pixels.
		''' </summary>
		SM_CYHSCROLL = 20
		''' <summary>
		''' Width of the arrow bitmap on a horizontal scroll bar in pixels.
		''' </summary>
		SM_CXHSCROLL = 21
		''' <summary>
		''' Nonzero if the debug version of User.exe is installed; zero otherwise.
		''' </summary>
		SM_DEBUG = 22
		''' <summary>
		''' Nonzero if the left and right mouse buttons are reversed; zero otherwise.
		''' </summary>
		SM_SWAPBUTTON = 23
		''' <summary>
		''' Reserved for future use
		''' </summary>
		SM_RESERVED1 = 24
		''' <summary>
		''' Reserved for future use
		''' </summary>
		SM_RESERVED2 = 25
		''' <summary>
		''' Reserved for future use
		''' </summary>
		SM_RESERVED3 = 26
		''' <summary>
		''' Reserved for future use
		''' </summary>
		SM_RESERVED4 = 27
		''' <summary>
		''' Minimum width of a window in pixels.
		''' </summary>
		SM_CXMIN = 28
		''' <summary>
		''' Minimum height of a window in pixels.
		''' </summary>
		SM_CYMIN = 29
		''' <summary>
		''' Width of a button in a window's caption or title bar in pixels.
		''' </summary>
		SM_CXSIZE = 30
		''' <summary>
		''' Height of a button in a window's caption or title bar in pixels.
		''' </summary>
		SM_CYSIZE = 31
		''' <summary>
		''' Thickness of the sizing border around the perimeter of a window that can be resized in pixels. SM_CXSIZEFRAME is the width of the horizontal border and SM_CYSIZEFRAME is the height of the vertical border. 
		''' </summary>
		SM_CXFRAME = 32
		''' <summary>
		''' Thickness of the sizing border around the perimeter of a window that can be resized in pixels. SM_CXSIZEFRAME is the width of the horizontal border and SM_CYSIZEFRAME is the height of the vertical border. 
		''' </summary>
		SM_CYFRAME = 33
		''' <summary>
		''' Minimum tracking width of a window in pixels. The user cannot drag the window frame to a size smaller than these dimensions. A window can override this value by processing the WM_GETMINMAXINFO message.
		''' </summary>
		SM_CXMINTRACK = 34
		''' <summary>
		''' Minimum tracking height of a window in pixels. The user cannot drag the window frame to a size smaller than these dimensions. A window can override this value by processing the WM_GETMINMAXINFO message
		''' </summary>
		SM_CYMINTRACK = 35
		''' <summary>
		''' Width of the rectangle around the location of a first click in a double-click sequence in pixels. The second click must occur within the rectangle defined by SM_CXDOUBLECLK and SM_CYDOUBLECLK for the system to consider the two clicks a double-click
		''' </summary>
		SM_CXDOUBLECLK = 36
		''' <summary>
		''' Height of the rectangle around the location of a first click in a double-click sequence in pixels. The second click must occur within the rectangle defined by SM_CXDOUBLECLK and SM_CYDOUBLECLK for the system to consider the two clicks a double-click. (The two clicks must also occur within a specified time.) 
		''' </summary>
		SM_CYDOUBLECLK = 37
		''' <summary>
		''' Width of a grid cell for items in large icon view in pixels. Each item fits into a rectangle of size SM_CXICONSPACING by SM_CYICONSPACING when arranged. This value is always greater than or equal to SM_CXICON
		''' </summary>
		SM_CXICONSPACING = 38
		''' <summary>
		''' Height of a grid cell for items in large icon view in pixels. Each item fits into a rectangle of size SM_CXICONSPACING by SM_CYICONSPACING when arranged. This value is always greater than or equal to SM_CYICON.
		''' </summary>
		SM_CYICONSPACING = 39
		''' <summary>
		''' Nonzero if drop-down menus are right-aligned with the corresponding menu-bar item; zero if the menus are left-aligned.
		''' </summary>
		SM_MENUDROPALIGNMENT = 40
		''' <summary>
		''' Nonzero if the Microsoft Windows for Pen computing extensions are installed; zero otherwise.
		''' </summary>
		SM_PENWINDOWS = 41
		''' <summary>
		''' Nonzero if User32.dll supports DBCS; zero otherwise. (WinMe/95/98): Unicode
		''' </summary>
		SM_DBCSENABLED = 42
		''' <summary>
		''' Number of buttons on mouse or zero if no mouse is installed.
		''' </summary>
		SM_CMOUSEBUTTONS = 43
		''' <summary>
		''' Identical Values Changed After Windows NT 4.0  
		''' </summary>
		SM_CXFIXEDFRAME = SM_CXDLGFRAME
		''' <summary>
		''' Identical Values Changed After Windows NT 4.0
		''' </summary>
		SM_CYFIXEDFRAME = SM_CYDLGFRAME
		''' <summary>
		''' Identical Values Changed After Windows NT 4.0
		''' </summary>
		SM_CXSIZEFRAME = SM_CXFRAME
		''' <summary>
		''' Identical Values Changed After Windows NT 4.0
		''' </summary>
		SM_CYSIZEFRAME = SM_CYFRAME
		''' <summary>
		''' Nonzero if security is present; zero otherwise.
		''' </summary>
		SM_SECURE = 44
		''' <summary>
		''' Width of a 3-D border in pixels. This is the 3-D counterpart of SM_CXBORDER
		''' </summary>
		SM_CXEDGE = 45
		''' <summary>
		''' Height of a 3-D border in pixels. This is the 3-D counterpart of SM_CYBORDER
		''' </summary>
		SM_CYEDGE = 46
		''' <summary>
		''' Width of a grid cell for a minimized window in pixels. Each minimized window fits into a rectangle this size when arranged. This value is always greater than or equal to SM_CXMINIMIZED.
		''' </summary>
		SM_CXMINSPACING = 47
		''' <summary>
		''' Height of a grid cell for a minimized window in pixels. Each minimized window fits into a rectangle this size when arranged. This value is always greater than or equal to SM_CYMINIMIZED.
		''' </summary>
		SM_CYMINSPACING = 48
		''' <summary>
		''' Recommended width of a small icon in pixels. Small icons typically appear in window captions and in small icon view
		''' </summary>
		SM_CXSMICON = 49
		''' <summary>
		''' Recommended height of a small icon in pixels. Small icons typically appear in window captions and in small icon view.
		''' </summary>
		SM_CYSMICON = 50
		''' <summary>
		''' Height of a small caption in pixels
		''' </summary>
		SM_CYSMCAPTION = 51
		''' <summary>
		''' Width of small caption buttons in pixels.
		''' </summary>
		SM_CXSMSIZE = 52
		''' <summary>
		''' Height of small caption buttons in pixels.
		''' </summary>
		SM_CYSMSIZE = 53
		''' <summary>
		''' Width of menu bar buttons such as the child window close button used in the multiple document interface in pixels.
		''' </summary>
		SM_CXMENUSIZE = 54
		''' <summary>
		''' Height of menu bar buttons such as the child window close button used in the multiple document interface in pixels.
		''' </summary>
		SM_CYMENUSIZE = 55
		''' <summary>
		''' Flags specifying how the system arranged minimized windows
		''' </summary>
		SM_ARRANGE = 56
		''' <summary>
		''' Width of a minimized window in pixels.
		''' </summary>
		SM_CXMINIMIZED = 57
		''' <summary>
		''' Height of a minimized window in pixels.
		''' </summary>
		SM_CYMINIMIZED = 58
		''' <summary>
		''' Default maximum width of a window that has a caption and sizing borders in pixels. This metric refers to the entire desktop. The user cannot drag the window frame to a size larger than these dimensions. A window can override this value by processing the WM_GETMINMAXINFO message.
		''' </summary>
		SM_CXMAXTRACK = 59
		''' <summary>
		''' Default maximum height of a window that has a caption and sizing borders in pixels. This metric refers to the entire desktop. The user cannot drag the window frame to a size larger than these dimensions. A window can override this value by processing the WM_GETMINMAXINFO message.
		''' </summary>
		SM_CYMAXTRACK = 60
		''' <summary>
		''' Default width in pixels of a maximized top-level window on the primary display monitor.
		''' </summary>
		SM_CXMAXIMIZED = 61
		''' <summary>
		''' Default height in pixels of a maximized top-level window on the primary display monitor.
		''' </summary>
		SM_CYMAXIMIZED = 62
		''' <summary>
		''' Least significant bit is set if a network is present; otherwise it is cleared. The other bits are reserved for future use
		''' </summary>
		SM_NETWORK = 63
		''' <summary>
		''' Value that specifies how the system was started: 0-normal 1-failsafe 2-failsafe /w net
		''' </summary>
		SM_CLEANBOOT = 67
		''' <summary>
		''' Width of a rectangle centered on a drag point to allow for limited movement of the mouse pointer before a drag operation begins in pixels. 
		''' </summary>
		SM_CXDRAG = 68
		''' <summary>
		''' Height of a rectangle centered on a drag point to allow for limited movement of the mouse pointer before a drag operation begins. This value is in pixels. It allows the user to click and release the mouse button easily without unintentionally starting a drag operation.
		''' </summary>
		SM_CYDRAG = 69
		''' <summary>
		''' Nonzero if the user requires an application to present information visually in situations where it would otherwise present the information only in audible form; zero otherwise. 
		''' </summary>
		SM_SHOWSOUNDS = 70
		''' <summary>
		''' Width of the default menu check-mark bitmap in pixels.
		''' </summary>
		SM_CXMENUCHECK = 71
		''' <summary>
		''' Height of the default menu check-mark bitmap in pixels.
		''' </summary>
		SM_CYMENUCHECK = 72
		''' <summary>
		''' Nonzero if the computer has a low-end (slow) processor; zero otherwise
		''' </summary>
		SM_SLOWMACHINE = 73
		''' <summary>
		''' Nonzero if the system is enabled for Hebrew and Arabic languages zero if not.
		''' </summary>
		SM_MIDEASTENABLED = 74
		''' <summary>
		''' Nonzero if a mouse is installed; zero otherwise. This value is rarely zero because of support for virtual mice and because some systems detect the presence of the port instead of the presence of a mouse.
		''' </summary>
		SM_MOUSEPRESENT = 19
		''' <summary>
		''' Windows 2000 (v5.0+) Coordinate of the top of the virtual screen
		''' </summary>
		SM_XVIRTUALSCREEN = 76
		''' <summary>
		''' Windows 2000 (v5.0+) Coordinate of the left of the virtual screen
		''' </summary>
		SM_YVIRTUALSCREEN = 77
		''' <summary>
		''' Windows 2000 (v5.0+) Width of the virtual screen
		''' </summary>
		SM_CXVIRTUALSCREEN = 78
		''' <summary>
		''' Windows 2000 (v5.0+) Height of the virtual screen
		''' </summary>
		SM_CYVIRTUALSCREEN = 79
		''' <summary>
		''' Number of display monitors on the desktop
		''' </summary>
		SM_CMONITORS = 80
		''' <summary>
		''' Windows XP (v5.1+) Nonzero if all the display monitors have the same color format zero otherwise. Note that two displays can have the same bit depth but different color formats. For example the red green and blue pixels can be encoded with different numbers of bits or those bits can be located in different places in a pixel's color value. 
		''' </summary>
		SM_SAMEDISPLAYFORMAT = 81
		''' <summary>
		''' Windows XP (v5.1+) Nonzero if Input Method Manager/Input Method Editor features are enabled; zero otherwise
		''' </summary>
		SM_IMMENABLED = 82
		''' <summary>
		''' Windows XP (v5.1+) Width of the left and right edges of the focus rectangle drawn by DrawFocusRect. This value is in pixels. 
		''' </summary>
		SM_CXFOCUSBORDER = 83
		''' <summary>
		''' Windows XP (v5.1+) Height of the top and bottom edges of the focus rectangle drawn by DrawFocusRect. This value is in pixels. 
		''' </summary>
		SM_CYFOCUSBORDER = 84
		''' <summary>
		''' Nonzero if the current operating system is the Windows XP Tablet PC edition zero if not.
		''' </summary>
		SM_TABLETPC = 86
		''' <summary>
		''' Nonzero if the current operating system is the Windows XP Media Center Edition zero if not.
		''' </summary>
		SM_MEDIACENTER = 87
		''' <summary>
		''' Metrics Other
		''' </summary>
		SM_CMETRICS_OTHER = 76
		''' <summary>
		''' Metrics Windows 2000
		''' </summary>
		SM_CMETRICS_2000 = 83
		''' <summary>
		''' Metrics Windows NT
		''' </summary>
		SM_CMETRICS_NT = 88
		''' <summary>
		''' Windows XP (v5.1+) This system metric is used in a Terminal Services environment. If the calling process is associated with a Terminal Services client session the return value is nonzero. If the calling process is associated with the Terminal Server console session the return value is zero. The console session is not necessarily the physical console - see WTSGetActiveConsoleSessionId for more information. 
		''' </summary>
		SM_REMOTESESSION = &H1000
		''' <summary>
		''' Windows XP (v5.1+) Nonzero if the current session is shutting down; zero otherwise
		''' </summary>
		SM_SHUTTINGDOWN = &H2000
		''' <summary>
		''' Windows XP (v5.1+) This system metric is used in a Terminal Services environment. Its value is nonzero if the current session is remotely controlled; zero otherwise
		''' </summary>
		SM_REMOTECONTROL = &H2001

	End Enum

	<Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> _
	  Private Structure RECT
		Public left As Integer
		Public top As Integer
		Public right As Integer
		Public bottom As Integer
	End Structure

	Public Sub SetWorkingArea(ByVal newArea As Rectangle)
		Dim r As New RECT
		r.top = newArea.Top
		r.bottom = newArea.Bottom
		r.left = newArea.Left
		r.right = newArea.Right
		Dim ptr As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(RECT)))
		'Marshal.Copy(New Integer() {r.left, r.top, r.right, r.bottom}, 0, ptr, 4)
		Marshal.StructureToPtr(r, ptr, True)
		Try
			If SystemParametersInfo(SPI.SPI_SETWORKAREA, 0, ptr, 0) = False Then Throw New Win32Exception()
		Finally
			Marshal.DestroyStructure(ptr, GetType(RECT))
			Marshal.FreeHGlobal(ptr)
		End Try
		For Each w As WindowInfo In WindowInfo.GetWindows
			If w.Maximized Then
				Try
					w.Rectangle = newArea
				Catch ex As Exception
					Debug.Print("There was an error moving the window. " & w.Text)
				End Try
			End If
		Next
		WindowInfo.GetDesktopWindow.Refresh()
	End Sub

	Private Declare Auto Function SetDesktopBackgroundAPI Lib "user32.dll" Alias "SystemParametersInfo" (ByVal uiAction As SPI, ByVal uiParam As Integer, ByVal param As String, ByVal fWinIni As Integer) As Boolean

	Public Sub SetDesktopBackground(ByVal newPath As String)
		SetDesktopBackgroundAPI(SPI.SPI_SETDESKWALLPAPER, 0, newPath, 3)
	End Sub

	Private Structure MINIMIZEDMETRICS
		Public cbSize As Integer
		Public iWidth As Integer
		Public iHorzGap As Integer
		Public iVertGap As Integer
		Public iArrange As Integer
	End Structure

	Private Const ARW_HIDE As Integer = &H8	'Hide minimized windows by moving them off the visible area of the screen. 

	''' <summary>
	''' Sets the minimized metrics to hide all minimized windows.
	''' </summary>
	''' <remarks></remarks>
	Public Sub SetHideMinimizedWindows()
		Dim m As New MINIMIZEDMETRICS
		m.cbSize = Marshal.SizeOf(m)
		Dim ptr As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(MINIMIZEDMETRICS)))
		Try
			Marshal.StructureToPtr(m, ptr, True)
			SystemParametersInfo(SPI.SPI_GETMINIMIZEDMETRICS, Marshal.SizeOf(m), ptr, 0)
			m.iArrange = m.iArrange Or ARW_HIDE
			m.iHorzGap = 3
			m.iVertGap = 3
			m.iWidth = 150
			Marshal.StructureToPtr(m, ptr, True)
			SystemParametersInfo(SPI.SPI_SETMINIMIZEDMETRICS, Marshal.SizeOf(m), ptr, 0)
		Finally
			Marshal.DestroyStructure(ptr, GetType(MINIMIZEDMETRICS))
			Marshal.FreeHGlobal(ptr)
		End Try
	End Sub

End Module

Public Module SystemFunctions

#Region "Process Tokens"

	Private Const SE_PRIVILEGE_ENABLED As Integer = &H2
	Private Const TOKEN_QUERY As Integer = &H8
	Private Const TOKEN_ADJUST_PRIVILEGES As Integer = &H20
	Private Const SE_SHUTDOWN_NAME As String = "SeShutdownPrivilege"

	<StructLayout(LayoutKind.Sequential, Pack:=1)> _
	Private Structure Luid
		Public Count As Integer
		Public Luid As Long
		Public Attr As Integer
	End Structure

	<DllImport("kernel32.dll", ExactSpelling:=True)> _
	Private Function GetCurrentProcess() As IntPtr
	End Function

	<DllImport("advapi32.dll", SetLastError:=True)> _
	Private Function OpenProcessToken(ByVal h As IntPtr, ByVal acc As Integer, ByRef phtok As IntPtr) As Boolean
	End Function

	<DllImport("advapi32.dll", SetLastError:=True)> _
	Private Function LookupPrivilegeValue(ByVal host As String, ByVal name As String, ByRef pluid As Long) As Boolean
	End Function

	<DllImport("advapi32.dll", ExactSpelling:=True, SetLastError:=True)> _
	Private Function AdjustTokenPrivileges(ByVal htok As IntPtr, ByVal disall As Boolean, ByRef newst As Luid, ByVal len As Integer, ByVal prev As IntPtr, ByVal relen As IntPtr) As Boolean
	End Function

    Private Enum TOKEN_INFORMATION_CLASS As Integer
        TokenUser = 1
        TokenGroups
        TokenPrivileges
        TokenOwner
        TokenPrimaryGroup
        TokenDefaultDacl
        TokenSource
        TokenType
        TokenImpersonationLevel
        TokenStatistics
        TokenRestrictedSids
        TokenSessionId
        TokenGroupsAndPrivileges
        TokenSessionReference
        TokenSandBoxInert
        TokenAuditPolicy
        TokenOrigin
        TokenElevationType
        TokenLinkedToken
        TokenElevation
        TokenHasRestrictions
        TokenAccessInformation
        TokenVirtualizationAllowed
        TokenVirtualizationEnabled
        TokenIntegrityLevel
        TokenUIAccess
        TokenMandatoryPolicy
        TokenLogonSid
        MaxTokenInfoClass
    End Enum

    Private Declare Auto Function GetTokenInformation Lib "Advapi32.dll" (ByVal TokenHandle As IntPtr, ByVal TokenInformationClass As TOKEN_INFORMATION_CLASS, ByVal TokenInformation As IntPtr, ByVal TokenInformationLength As Integer, ByRef ReturnLength As Integer) As Boolean

    Public Function GetProcessTokenHandle(ByVal p As Process) As IntPtr
        Dim hproc As IntPtr = GetCurrentProcess()
        Dim htok As IntPtr = IntPtr.Zero

        'Get a token for this process. 
        If OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES Or TOKEN_QUERY, htok) = False Then
            Throw New Win32Exception
        Else
            Return htok
        End If

        'Dim tp As Luid
        'tp.Count = 1
        'tp.Luid = 0
        'tp.Attr = SE_PRIVILEGE_ENABLED

        ''Get the LUID for the shutdown privilege.
        'LookupPrivilegeValue(Nothing, SE_SHUTDOWN_NAME, tp.Luid)

        ''Get the shutdown privilege for this process.
        'AdjustTokenPrivileges(htok, False, tp, 0, IntPtr.Zero, IntPtr.Zero)

    End Function

    Public Function GetAuthenticationId(ByVal token As IntPtr) As String
        Dim tsStats As TOKEN_STATISTICS
        Dim ptr As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(TOKEN_STATISTICS)))
        Marshal.StructureToPtr(tsStats, ptr, True)
        Dim returnSize As Integer
        If GetTokenInformation(token, TOKEN_INFORMATION_CLASS.TokenStatistics, ptr, Marshal.SizeOf(GetType(TOKEN_STATISTICS)), returnSize) = False Then
            Throw New Win32Exception
        End If
        tsStats = Marshal.PtrToStructure(ptr, GetType(TOKEN_STATISTICS))
        Marshal.FreeHGlobal(ptr)
        Return tsStats.AuthenticationId.high.ToString("x08") & tsStats.AuthenticationId.low.ToString("x08")
    End Function

    Private Enum SECURITY_IMPERSONATION_LEVEL As Integer
        SecurityAnonymous
        SecurityIdentification
        SecurityImpersonation
        SecurityDelegation
    End Enum

    Private Enum TOKEN_TYPE As Integer
        TokenPrimary = 1
        TokenImpersonation
    End Enum

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure TOKEN_STATISTICS
        Public TokenId As LUID64
        Public AuthenticationId As LUID64
        Public ExpirationTime As Int64
        Public TokenType As TOKEN_TYPE
        Public ImpersonationLevel As SECURITY_IMPERSONATION_LEVEL
        Public DynamicCharged As Integer
        Public DynamicAvailable As Integer
        Public GroupCount As Integer
        Public PriviledgeCount As Integer
        Public ModifiedId As LUID64
    End Structure

    Private Structure LUID64
        Public low As Integer
        Public high As Integer
    End Structure

#End Region

#Region "Shutdown"

    Public Enum ShutdownType As Integer
        Logoff = 0
        PowerOff = 8
        Reboot = 2
        RestartApps = &H40
        Shutdown = 1
        Force = 4
        ForceIfHung = &H10
    End Enum

    Public Enum ShutdownMajorReason As Integer
        ''' <summary>
        ''' Application issue.
        ''' </summary>
        ''' <remarks></remarks>
        MajorApplication = &H40000
        ''' <summary>
        ''' Hardware issue.
        ''' </summary>
        ''' <remarks></remarks>
        MajorHardware = &H10000
        ''' <summary>
        ''' The InitiateSystemShutdown function was used instead of InitiateSystemShutdownEx.
        ''' </summary>
        ''' <remarks></remarks>
        MajorLegacyAPI = &H70000
        ''' <summary>
        ''' Operating system issue.
        ''' </summary>
        ''' <remarks></remarks>
        MajorOperatingSystem = &H20000
        ''' <summary>
        ''' Other issue.
        ''' </summary>
        ''' <remarks></remarks>
        MajorOther = &H0
        ''' <summary>
        ''' Power failure.
        ''' </summary>
        ''' <remarks></remarks>
        MajorPower = &H60000
        ''' <summary>
        ''' Software issue.
        ''' </summary>
        ''' <remarks></remarks>
        MajorSoftware = &H30000
        ''' <summary>
        ''' System failure.
        ''' </summary>
        ''' <remarks></remarks>
        MajorSystem = &H50000
    End Enum

    Public Enum ShutdownMinorReason As Integer
        ''' <summary>
        ''' Blue screen crash event.
        ''' </summary>
        ''' <remarks></remarks>
        MinorBluescreen = &HF
        ''' <summary>
        ''' Unplugged.
        ''' </summary>
        ''' <remarks></remarks>
        MinorordUnplugged = &HB
        ''' <summary>
        ''' Disk.
        ''' </summary>
        ''' <remarks></remarks>
        MinorDisk = &H7
        ''' <summary>
        ''' Environment.
        ''' </summary>
        ''' <remarks></remarks>
        MinorEnvironment = &HC
        ''' <summary>
        ''' Driver.
        ''' </summary>
        ''' <remarks></remarks>
        MinorHardwareDriver = &HD
        ''' <summary>
        ''' Hot fix.
        ''' </summary>
        ''' <remarks></remarks>
        MinorHotfix = &H11
        ''' <summary>
        ''' Hot fix uninstallation.
        ''' </summary>
        ''' <remarks></remarks>
        MinorHotfixUninstall = &H17
        ''' <summary>
        ''' Unresponsive.
        ''' </summary>
        ''' <remarks></remarks>
        MinorHung = &H5
        ''' <summary>
        ''' Installation.
        ''' </summary>
        ''' <remarks></remarks>
        MinorInstallation = &H2
        ''' <summary>
        ''' Maintenance.
        ''' </summary>
        ''' <remarks></remarks>
        MinorMaintenance = &H1
        ''' <summary>
        ''' MMC issue.
        ''' </summary>
        ''' <remarks></remarks>
        MinorMmc = &H19
        ''' <summary>
        ''' Network connectivity.
        ''' </summary>
        ''' <remarks></remarks>
        MinorNetworkConnectivity = &H14
        ''' <summary>
        ''' Network card.
        ''' </summary>
        ''' <remarks></remarks>
        MinorNetworkCard = &H9
        ''' <summary>
        ''' Other issue.
        ''' </summary>
        ''' <remarks></remarks>
        MinorOther = &H0
        ''' <summary>
        ''' Other driver event.
        ''' </summary>
        ''' <remarks></remarks>
        MinorOtherDriver = &HE
        ''' <summary>
        ''' Power supply.
        ''' </summary>
        ''' <remarks></remarks>
        MinorPowerSupply = &HA
        ''' <summary>
        ''' Processor.
        ''' </summary>
        ''' <remarks></remarks>
        MinorProcessor = &H8
        ''' <summary>
        ''' Reconfigure.
        ''' </summary>
        ''' <remarks></remarks>
        MinorReconfig = &H4
        ''' <summary>
        ''' Security issue.
        ''' </summary>
        ''' <remarks></remarks>
        MinorSecurity = &H13
        ''' <summary>
        ''' Security patch.
        ''' </summary>
        ''' <remarks></remarks>
        MinorSecurityFix = &H12
        ''' <summary>
        ''' Security patch uninstallation.
        ''' </summary>
        ''' <remarks></remarks>
        MinorSecurityFixUninstall = &H18
        ''' <summary>
        ''' Service pack.
        ''' </summary>
        ''' <remarks></remarks>
        MinorServicePack = &H10
        ''' <summary>
        ''' Service pack uninstallation.
        ''' </summary>
        ''' <remarks></remarks>
        MinorServicePackUninstall = &H16
        ''' <summary>
        ''' Terminal Services.
        ''' </summary>
        ''' <remarks></remarks>
        MinorTermSrv = &H20
        ''' <summary>
        ''' Unstable.
        ''' </summary>
        ''' <remarks></remarks>
        MinorUnstable = &H6
        ''' <summary>
        ''' Upgrade.
        ''' </summary>
        ''' <remarks></remarks>
        MinorUpgrade = &H3
        ''' <summary>
        ''' WMI issue.
        ''' </summary>
        ''' <remarks></remarks>
        MinorWmi = &H15
    End Enum

    Private Declare Auto Function ExitWindowsEx Lib "user32.dll" (ByVal uFlags As Integer, ByVal reason As Integer) As Boolean
    Private Declare Auto Function LockWorkStation Lib "user32.dll" () As Boolean

    Public Sub ShutdownWindows(ByVal type As ShutdownType, ByVal majorReason As ShutdownMajorReason, ByVal minorReason As ShutdownMinorReason)
        If ExitWindowsEx(type, majorReason Or minorReason) = False Then
            Throw New System.ComponentModel.Win32Exception
        End If
    End Sub

    Public Sub LockWindows()
        If LockWorkStation = False Then
            Throw New System.ComponentModel.Win32Exception
        End If
    End Sub

#End Region

    '#Region "GetFileIcon"

    '    Private Declare Auto Function SHGetFileInfo Lib "shell32.dll" (ByVal pszPath As String, ByVal dwFileAttributes As Integer, ByRef psfi As _SHFILEINFO, ByVal cbFileInfo As Integer, ByVal uFlags As Integer) As Integer
    '    Private Const SHGFI_LARGEICON As Integer = &H0     ' get large icon
    '    Private Const SHGFI_SMALLICON As Integer = &H1     ' get small icon
    '    Private Const SHGFI_OPENICON As Integer = &H2     ' get open icon
    '    Private Const SHGFI_SHELLICONSIZE As Integer = &H4     ' get shell size icon
    '    Private Const SHGFI_ICON As Integer = &H100     ' get icon
    '    Private Const SHGFI_DISPLAYNAME As Integer = &H200     ' get display name
    '    Private Const SHGFI_TYPENAME As Integer = &H400     ' get type name

    '    <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> _
    '    Private Structure _SHFILEINFO
    '        Public hIcon As Integer
    '        Public iIcon As Integer
    '        Public dwAttributes As Integer
    '        <VBFixedString(260)> Public szDisplayName As String
    '        <VBFixedString(80)> Public szTypeName As String
    '    End Structure

    '    Public Function GetFileIcon(ByVal path As String, ByVal big As Boolean) As Icon
    '        Dim shfi As New _SHFILEINFO
    '        If Not path.Length > 260 Then
    '            SHGetFileInfo(path, 0, shfi, Runtime.InteropServices.Marshal.SizeOf(shfi), SHGFI_ICON Or IIf(big, SHGFI_LARGEICON, SHGFI_SMALLICON))
    '            If shfi.hIcon = 0 Then Return Nothing
    '            Return Icon.FromHandle(shfi.hIcon)
    '        End If
    '        Return Nothing
    '    End Function

    '    Public Function GetDisplayName(ByVal path As String) As String
    '        Dim shfi As New _SHFILEINFO
    '        If Not path.Length > 260 Then
    '            SHGetFileInfo(path, 0, shfi, Runtime.InteropServices.Marshal.SizeOf(shfi), SHGFI_DISPLAYNAME)
    '            Return shfi.szDisplayName
    '        End If
    '        Return ""
    '    End Function

    '    Public Function GetTypeName(ByVal path As String) As String
    '        Dim shfi As New _SHFILEINFO
    '        If Not path.Length > 260 Then
    '            SHGetFileInfo(path, 0, shfi, Runtime.InteropServices.Marshal.SizeOf(shfi), SHGFI_TYPENAME)
    '            Return shfi.szTypeName
    '        End If
    '        Return ""
    '    End Function

    '#End Region

#Region "GetFolderPath"

    Public Enum FolderID As Integer
        Desktop = &H0          ' <desktop>
        Internet = &H1      ' Internet Explorer (icon on desktop)
        Programs = &H2      ' Start Menu\Programs
        Controls = &H3      ' My Computer\Control Panel
        Printers = &H4      ' My Computer\Printers
        Personal = &H5      ' My Documents
        Favorites = &H6      ' <user name>\Favorites
        Startup = &H7          ' Start Menu\Programs\Startup
        Recent = &H8          ' <user name>\Recent
        SendTo = &H9          ' <user name>\SendTo
        BitBucket = &HA      ' <desktop>\Recycle Bin
        StartMenu = &HB      ' <user name>\Start Menu
        MyDocuments = &H5   '  Personal was just a silly name for My Documents
        MyMusic = &HD          ' "My Music" folder
        MyVideo = &HE          ' "My Videos" folder
        DesktopDirectory = &H10      ' <user name>\Desktop
        Drives = &H11          ' My Computer
        Network = &H12      ' Network Neighborhood (My Network Places)
        NetHood = &H13      ' <user name>\nethood
        Fonts = &H14          ' windows\fonts
        Templates = &H15 'In Explorer right-click->new
        CommonStartMenu = &H16       ' All Users\Start Menu
        CommonPrograms = &H17       ' All Users\Start Menu\Programs
        CommonStartup = &H18           ' All Users\Startup
        CommonDesktopDirectory = &H19       ' All Users\Desktop
        AppData = &H1A      ' <user name>\Application Data
        PrintHood = &H1B          ' <user name>\PrintHood
        LocalAppData = &H1C       ' <user name>\Local Settings\Application Data (non roaming)
        AltStartup = &H1D          ' non localized startup
        CommonAltStartup = &H1E       ' non localized common startup
        CommonFavorites = &H1F
        InternetCache = &H20
        Cookies = &H21
        History = &H22
        CommonAppdata = &H23           ' All Users\Application Data
        Windows = &H24      ' GetWindowsDirectory()
        System = &H25          ' GetSystemDirectory()
        ProgramFiles = &H26       ' C:\Program Files
        MyPictures = &H27          ' C:\Program Files\My Pictures
        Profile = &H28      ' USERPROFILE
        SystemX86 = &H29          ' x86 system directory on RISC
        ProgramFilesX86 = &H2A       ' x86 C:\Program Files on RISC
        ProgramFilesCommon = &H2B        ' C:\Program Files\Common
        ProgramFilesCommonX86 = &H2C        ' x86 Program Files\Common on RISC
        CommonTemplates = &H2D       ' All Users\Templates
        CommonDocuments = &H2E       ' All Users\Documents
        CommonAdminTools = &H2F       ' All Users\Start Menu\Programs\Administrative Tools
        AdminTools = &H30          ' <user name>\Start Menu\Programs\Administrative Tools
        Connections = &H31      ' Network and Dial-up Connections
        CommonMusic = &H35       ' All Users\My Music
        CommonPictures = &H36       ' All Users\My Pictures
        CommonVideo = &H37       ' All Users\My Video
        Resources = &H38          ' Resource Direcotry
        ResourcesLocalized = &H39       ' Localized Resource Direcotry
        CommonOEMLinks = &H3A        ' Links to All Users OEM specific apps
        CDBurnArea = &H3B       ' USERPROFILE\Local Settings\Application Data\Microsoft\CD Burning
        'unused = &h003c
        ComputersNearMe = &H3D      ' Computers Near Me (computered from Workgroup membership)
        FlagCreate = &H8000       ' combine with CSIDL_ value to force folder creation in SHGetFolderPath()
        FlagDontVerify = &H4000        ' combine with CSIDL_ value to return an unverified folder path
        FladDontUnexpand = &H2000        ' combine with CSIDL_ value to avoid unexpanding environment variables
        FlagNoAlias = &H1000        ' combine with CSIDL_ value to insure non-alias versions of the pidl
        FlagPerUserInit = &H800     ' combine with CSIDL_ value to indicate per-user init (eg. upgrade)
        FlagMask = &HFF00       ' mask for all possible flag values
    End Enum

    Private Declare Auto Function SHGetFolderPath Lib "shell32.dll" (ByVal hwndOwner As IntPtr, ByVal nFolder As FolderID, ByVal hToken As IntPtr, ByVal dwFlags As Integer, ByVal path As System.Text.StringBuilder) As Integer

    Public Enum FolderType As Integer
        Current = 0
        [Default] = 1
    End Enum

    Public Function GetFolderPath(ByVal folder As FolderID, Optional ByVal type As FolderType = FolderType.Current) As String
        Dim path As New System.Text.StringBuilder(260)
        SHGetFolderPath(0, folder, GetProcessTokenHandle(Process.GetCurrentProcess), type, path)
        Return path.ToString
    End Function

#End Region

#Region "Extract Icon from File"

    Private Declare Auto Function ExtractIconEx Lib "user32.dll" (ByVal lpszFile As String, ByVal nIconIndex As Integer, ByRef largeIcon As IntPtr, ByRef smallIcon As IntPtr, ByVal iconNumber As Integer) As Integer

    ''' <summary>
    ''' Extracts the large version of the icon at the given index in the specified EXE, DLL, or ICO file.
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="iconIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExtractLargeIcon(ByVal filePath As String, ByVal iconIndex As Integer) As Icon
        If IO.File.Exists(filePath) Then
            If IconsInFile(filePath) > 0 Then
                Dim smallIcon As IntPtr
                Dim largeIcon As IntPtr
                ExtractIconEx(filePath, iconIndex, largeIcon, smallIcon, 1)
                Return Icon.FromHandle(largeIcon)
            Else
                Throw New ArgumentException("The specified file does not contain any icons.")
            End If
        Else
            Throw New IO.FileNotFoundException()
        End If
    End Function

    ''' <summary>
    ''' Extracts the small version of the icon at the given index in the specified EXE, DLL, or ICO file.
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="iconIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExtractSmallIcon(ByVal filePath As String, ByVal iconIndex As Integer) As Icon
        If IO.File.Exists(filePath) Then
            If IconsInFile(filePath) > 0 Then
                Dim smallIcon As IntPtr
                Dim largeIcon As IntPtr
                ExtractIconEx(filePath, iconIndex, largeIcon, smallIcon, 1)
                Return Icon.FromHandle(smallIcon)
            Else
                Throw New ArgumentException("The specified file does not contain any icons.")
            End If
        Else
            Throw New IO.FileNotFoundException()
        End If
    End Function

    ''' <summary>
    ''' Returns the number of icons contained in the specified EXE, DLL, or ICO file.
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IconsInFile(ByVal filePath As String) As Integer
        If IO.File.Exists(filePath) Then
            Return ExtractIconEx(filePath, -1, Nothing, Nothing, 0)
        Else
            Throw New IO.FileNotFoundException()
        End If
    End Function

#End Region

#Region "GetUserImage"

    ''' <summary>
    ''' Returns the image of the specified user used for the welcome screen.
    ''' </summary>
    ''' <param name="userName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserImage(ByVal userName As String) As Bitmap
		Try
			If New Version(My.Computer.Info.OSVersion).Major < 6 Then
				Dim hintsKey As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Hints")
				For Each h As String In hintsKey.GetSubKeyNames
					If LCase(h) = LCase(userName) Then
						For Each v As String In hintsKey.OpenSubKey(h).GetValueNames
							If v = "PictureSource" Then
								Return New Bitmap(CStr(hintsKey.OpenSubKey(h).GetValue(v)))
							End If
						Next
						Return Nothing
					End If
				Next
			Else
				Dim userTilePath As String = GetFolderPath(FolderID.AppData) & "\UserTile.png"
				userTilePath = userTilePath.Replace("\\", "\")
				Return New Bitmap(userTilePath)
			End If
			Return Nothing
		Catch ex As Exception
			Return Nothing
		End Try
    End Function

#End Region

End Module

Public Module RecycleBin

	Private Structure SHQUERYRBINFO
		Public cbSize As Integer
		Public i64Size As Long
		Public i64NumItems As Long
	End Structure

	Private Declare Auto Function SHQueryRecycleBin Lib "shell32.dll" (ByVal rootPath As String, ByRef shInfo As SHQUERYRBINFO) As Integer
	Private Declare Auto Function SHEmptyRecycleBin Lib "shell32.dll" (ByVal rootPath As String, ByVal parent As IntPtr, ByVal uFlags As Integer) As Integer

	''' <summary>
	''' Gets the total number of objects in the recycle bin on the specified drive.
	''' </summary>
	''' <param name="drive">An absolute path containing the drive who's recycle bin is to be queried.</param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function GetNumItems(ByVal drive As String) As Long
		Dim qi As SHQUERYRBINFO
		qi.cbSize = Marshal.SizeOf(GetType(SHQUERYRBINFO))
		Dim result As Integer = SHQueryRecycleBin(drive, qi)
		If result <> 0 Then
			Throw New Win32Exception(result)
		Else
			Return qi.i64NumItems
		End If
	End Function

	''' <summary>
	''' Returns the size, in bytes, of all of the files in the recycle bin on the specified drive.
	''' </summary>
	''' <param name="drive">An absolute path containing the drive who's recycle bin is to be queried.</param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function GetSizeInBytes(ByVal drive As String) As Long
		Dim qi As SHQUERYRBINFO
		qi.cbSize = Marshal.SizeOf(GetType(SHQUERYRBINFO))
		Dim result As Integer = SHQueryRecycleBin(drive, qi)
		If result <> 0 Then
			Throw New Win32Exception(result)
		Else
			Return qi.i64Size
		End If
	End Function

	''' <summary>
	''' Empties the recycle bin on the given drive.
	''' </summary>
	''' <param name="drive">An absolute path containing the drive who's recycle bin is to be emptied.</param>
	''' <param name="parentWindow">An optional handle to a window who is to be the parent window of any dialogs that appear. Set to zero/null for no parent window.</param>
	''' <param name="showConfirmation">Whether to display a confirmation to the user.</param>
	''' <param name="showProgress">Whether to show the progress of the action to the user if necessary.</param>
	''' <param name="playSound">Whether to play the system's associated sound after emptying.</param>
	''' <remarks></remarks>
	Public Sub Empty(ByVal drive As String, ByVal parentWindow As IntPtr, Optional ByVal showConfirmation As Boolean = True, Optional ByVal showProgress As Boolean = True, Optional ByVal playSound As Boolean = True)
		Dim flags As Integer
		If showConfirmation Then flags = flags Or 1
		If showProgress Then flags = flags Or 2
		If playSound Then flags = flags Or 4
		Dim result As Integer = SHEmptyRecycleBin(drive, parentWindow, flags)
		If result <> 0 Then
			Throw New Win32Exception(result)
		End If
	End Sub

End Module
