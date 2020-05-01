Imports Microsoft.Win32
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Security.AccessControl

Public Module StartupShell

    Dim _explorerMutex As Threading.Mutex

	''' <summary>
    ''' Checks to see if Explorer is running, and stop it from being started over top of us.
	''' </summary>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function ExplorerRunning() As Boolean
        Dim init As Boolean
        _explorerMutex = New Threading.Mutex(True, "ExplorerIsShellMutex", init)
        Return Not init
    End Function

    ''' <summary>
    ''' This sets a setting in the registry allowing us to open Explorer without it
    ''' trying to start up its own desktop and taskbar.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDesktopProcess()
        Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "DesktopProcess", 1)
        Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "DesktopProcess", 1)
    End Sub

#Region "Run Startup Programs"

    Private Declare Auto Function SHRestricted Lib "shell32.dll" (ByVal restriction As RestrictionsEnum) As Integer

    Public Enum RestrictionsEnum As Integer
        REST_NONE
        REST_NORUN
        REST_NOCLOSE
        REST_NOSAVESET
        REST_NOFILEMENU
        REST_NOSETFOLDERS
        REST_NOSETTASKBAR
        REST_NODESKTOP
        REST_NOFIND
        REST_NODRIVES
        REST_NODRIVEAUTORUN
        REST_NODRIVETYPEAUTORUN
        REST_NONETHOOD
        REST_STARTBANNER
        REST_RESTRICTRUN
        REST_NOPRINTERTABS
        REST_NOPRINTERDELETE
        REST_NOPRINTERADD
        REST_NOSTARTMENUSUBFOLDERS
        REST_MYDOCSONNET
        REST_NOEXITTODOS
        REST_ENFORCESHELLEXTSECURITY
        REST_LINKRESOLVEIGNORELINKINFO
        REST_NOCOMMONGROUPS
        REST_SEPARATEDESKTOPPROCESS
        REST_NOWEB
        REST_NOTRAYCONTEXTMENU
        REST_NOVIEWCONTEXTMENU
        REST_NONETCONNECTDISCONNECT
        REST_STARTMENULOGOFF
        REST_NOSETTINGSASSIST
        REST_NOINTERNETICON
        REST_NORECENTDOCSHISTORY
        REST_NORECENTDOCSMENU
        REST_NOACTIVEDESKTOP
        REST_NOACTIVEDESKTOPCHANGES
        REST_NOFAVORITESMENU
        REST_CLEARRECENTDOCSONEXIT
        REST_CLASSICSHELL
        REST_NOCUSTOMIZEWEBVIEW
        REST_NOHTMLWALLPAPER
        REST_NOCHANGINGWALLPAPER
        REST_NODESKCOMP
        REST_NOADDDESKCOMP
        REST_NODELDESKCOMP
        REST_NOCLOSEDESKCOMP
        REST_NOCLOSE_DRAGDROPBAND
        REST_NOMOVINGBAND
        REST_NOEDITDESKCOMP
        REST_NORESOLVESEARCH
        REST_NORESOLVETRACK
        REST_FORCECOPYACLWITHFILE
        REST_NOLOGO3CHANNELNOTIFY
        REST_NOFORGETSOFTWAREUPDATE
        REST_NOSETACTIVEDESKTOP
        REST_NOUPDATEWINDOWS
        REST_NOCHANGESTARMENU
        REST_NOFOLDEROPTIONS
        REST_HASFINDCOMPUTERS
        REST_INTELLIMENUS
        REST_RUNDLGMEMCHECKBOX
        REST_ARP_ShowPostSetup
        REST_NOCSC
        REST_NOCONTROLPANEL
        REST_ENUMWORKGROUP
        REST_ARP_NOARP
        REST_ARP_NOREMOVEPAGE
        REST_ARP_NOADDPAGE
        REST_ARP_NOWINSETUPPAGE
        REST_GREYMSIADS
        REST_NOCHANGEMAPPEDDRIVELABEL
        REST_NOCHANGEMAPPEDDRIVECOMMENT
        REST_MaxRecentDocs
        REST_NONETWORKCONNECTIONS
        REST_FORCESTARTMENULOGOFF
        REST_NOWEBVIEW
        REST_NOCUSTOMIZETHISFOLDER
        REST_NOENCRYPTION
        REST_DONTSHOWSUPERHIDDEN
        REST_NOSHELLSEARCHBUTTON
        REST_NOHARDWARETAB
        REST_NORUNASINSTALLPROMPT
        REST_PROMPTRUNASINSTALLNETPATH
        REST_NOMANAGEMYCOMPUTERVERB
        REST_NORECENTDOCSNETHOOD
        REST_DISALLOWRUN
        REST_NOWELCOMESCREEN
        REST_RESTRICTCPL
        REST_DISALLOWCPL
        REST_NOSMBALLOONTIP
        REST_NOSMHELP
        REST_NOWINKEYS
        REST_NOENCRYPTONMOVE
        REST_NOLOCALMACHINERUN
        REST_NOCURRENTUSERRUN
        REST_NOLOCALMACHINERUNONCE
        REST_NOCURRENTUSERRUNONCE
        REST_FORCEACTIVEDESKTOPON
        REST_NOCOMPUTERSNEARME
        REST_NOVIEWONDRIVE
        REST_NONETCRAWL
        REST_NOSHAREDDOCUMENTS
        REST_NOSMMYDOCS
        REST_NOSMMYPICS
        REST_ALLOWBITBUCKDRIVES
        REST_NONLEGACYSHELLMODE
        REST_NOCONTROLPANELBARRICADE
        REST_NOSTARTPAGE
        REST_NOAUTOTRAYNOTIFY
        REST_NOTASKGROUPING
        REST_NOCDBURNING
        REST_MYCOMPNOPROP
        REST_MYDOCSNOPROP
        REST_NOSTARTPANEL
        REST_NODISPLAYAPPEARANCEPAGE
        REST_NOTHEMESTAB
        REST_NOVISUALSTYLECHOICE
        REST_NOSIZECHOICE
        REST_NOCOLORCHOICE
        REST_SETVISUALSTYLE
        REST_STARTRUNNOHOMEPATH
        REST_NOUSERNAMEINSTARTPANEL
        REST_NOMYCOMPUTERICON
        REST_NOSMNETWORKPLACES
        REST_NOSMPINNEDLIST
        REST_NOSMMYMUSIC
        REST_NOSMEJECTPC
        REST_NOSMMOREPROGRAMS
        REST_NOSMMFUPROGRAMS
        REST_NOTRAYITEMSDISPLAY
        REST_NOTOOLBARSONTASKBAR
        REST_NOSMCONFIGUREPROGRAMS
        REST_HIDECLOCK
        REST_NOLOWDISKSPACECHECKS
        REST_NOENTIRENETWORK
        REST_NODESKTOPCLEANUP
        REST_BITBUCKNUKEONDELETE
        REST_BITBUCKCONFIRMDELETE
        REST_BITBUCKNOPROP
        REST_NODISPBACKGROUND
        REST_NODISPSCREENSAVEPG
        REST_NODISPSETTINGSPG
        REST_NODISPSCREENSAVEPREVIEW
        REST_NODISPLAYCPL
        REST_HIDERUNASVERB
        REST_NOTHUMBNAILCACHE
        REST_NOSTRCMPLOGICAL
        REST_NOPUBLISHWIZARD
        REST_NOONLINEPRINTSWIZARD
        REST_NOWEBSERVICES
        REST_ALLOWUNHASHEDWEBVIEW
        REST_ALLOWLEGACYWEBVIEW
        REST_REVERTWEBVIEWSECURITY
        REST_INHERITCONSOLEHANDLES
        REST_SORTMAXITEMCOUNT
        REST_NOREMOTERECURSIVEEVENTS
        REST_NOREMOTECHANGENOTIFY
        REST_NOSIMPLENETIDLIST
        REST_NOENUMENTIRENETWORK
        REST_NODETAILSTHUMBNAILONNETWORK
        REST_NOINTERNETOPENWITH
        REST_ALLOWLEGACYLMZBEHAVIOR
        REST_DONTRETRYBADNETNAME
        REST_ALLOWFILECLSIDJUNCTIONS
        REST_NOUPNPINSTALL
        REST_ARP_DONTGROUPPATCHES
        REST_ARP_NOCHOOSEPROGRAMSPAGE
        REST_NODISCONNECT
        REST_NOSECURITY
        REST_NOFILEASSOCIATE
        REST_ALLOWCOMMENTTOGGLE
        REST_USEDESKTOPINICACHE
    End Enum

    ''' <summary>
    ''' Checks whether or not the startup programs have been run.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HasStartupBeenRun() As Boolean
        Dim key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\SessionInfo\" & GetAuthenticationId(GetProcessTokenHandle(Process.GetCurrentProcess)) & "\StartupHasBeenRun")
        If key Is Nothing Then
            Return Registry.CurrentUser.OpenSubKey("Software\Z-Systems\Z-Shell\SessionInfo\" & GetAuthenticationId(GetProcessTokenHandle(Process.GetCurrentProcess)) & "\StartupHasBeenRun") IsNot Nothing
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Runs all startup programs if they haven't already been run based on the user's security settings.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RunStartupPrograms()
        If Not HasStartupBeenRun() Then
            Dim HKLMRun As Boolean = Not SHRestricted(RestrictionsEnum.REST_NOLOCALMACHINERUN)
            Dim HKCURun As Boolean = Not SHRestricted(RestrictionsEnum.REST_NOCURRENTUSERRUN)
            Dim HKLMRunOnce As Boolean = Not SHRestricted(RestrictionsEnum.REST_NOLOCALMACHINERUNONCE)
            Dim HKCURunOnce As Boolean = Not SHRestricted(RestrictionsEnum.REST_NOCURRENTUSERRUNONCE)

            If HKLMRunOnce Then
                RunStartupPrograms(Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\RunOnce"), True, True, True)
            End If

            RunOnceEx()

            If HKLMRun Then
                RunStartupPrograms(Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run"))
            End If

            RunStartupPrograms(Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\policies\Explorer\Run"))
            RunStartupPrograms(Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\policies\Explorer\Run"))

            If HKCURun Then
                RunStartupPrograms(Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run"))
            End If

            RunStartupPrograms(New IO.DirectoryInfo(GetFolderPath(FolderID.Startup)))

            If HKCURunOnce Then
                RunStartupPrograms(Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\RunOnce"), True, True)
            End If

            StartupHasBeenRun()
        Else
            Debug.Print("Startup has already been run.")
        End If
    End Sub

    ''' <summary>
    ''' Tells the system that the startup programs have been run.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StartupHasBeenRun()
        Try
            Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer").CreateSubKey("SessionInfo").CreateSubKey(GetAuthenticationId(GetProcessTokenHandle(Process.GetCurrentProcess))).CreateSubKey("StartupHasBeenRun")
            Registry.CurrentUser.OpenSubKey("Software").CreateSubKey("Z-Systems").CreateSubKey("Z-Shell").CreateSubKey("SessionInfo").CreateSubKey(GetAuthenticationId(GetProcessTokenHandle(Process.GetCurrentProcess))).CreateSubKey("StartupHasBeenRun")
        Catch ex As Exception
            Debug.Print("Couldn't notify the system that startup programs finished starting. " & ex.Message)
        End Try
    End Sub

    'From LiteStep:
    '
    ' The following cases need to be supported:
    '
    ' 1. "C:\Program Files\App\App.exe" -params
    ' 2. C:\Program Files\App\App.exe -params
    ' 3. App.exe -params   (app.exe is in %path% or HKLM->REGSTR_PATH_APPPATHS)
    ' and all the above cases without arguments.
    '
    ' CreateProcess handles 1 and 2, ShellExecuteEx handles 1 and 3.
    ' So if the first token doesn't contain path characters (':' or '\')
    ' ShellExecuteEx is used. That's really ugly but it *should* work.

    ''' <summary>
    ''' Runs all startup programs pointed to in the given registry key.
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="delete"></param>
    ''' <remarks></remarks>
    Public Sub RunStartupPrograms(ByVal key As RegistryKey, Optional ByVal delete As Boolean = False, Optional ByVal runSubKeys As Boolean = False, Optional ByVal waitForExit As Boolean = False)
        Try
            If key IsNot Nothing Then
                Dim values() As String = key.GetValueNames()
                For Each v As String In values
                    If (key.GetValueKind(v) = RegistryValueKind.String OrElse key.GetValueKind(v) = RegistryValueKind.ExpandString) AndAlso (key.GetValue(v) <> "" AndAlso key.GetValue(v) IsNot Nothing) Then
                        Try
                            'Get the command string from the registry
                            Dim command As String = key.GetValue(v)

                            'First, check if we are in safe mode. Only run the item in safe mode if it has a "*" prefix.
                            If SystemInformation.BootMode <> BootMode.Normal Then
                                If delete AndAlso command.StartsWith("*") Then
                                    command = command.TrimStart("*")
                                Else
                                    Continue For
                                End If
                            End If

                            'Next, check if we have rights to delete the registry key
                            'For Each ar As Security.AccessControl.RegistryAccessRule In key.GetAccessControl(Security.AccessControl.AccessControlSections.Access).GetAccessRules(True, True, GetType(Security.Principal.NTAccount))
                            '    If ar.AccessControlType = Security.AccessControl.AccessControlType.Deny AndAlso ((ar.RegistryRights Or RegistryRights.WriteKey) = RegistryRights.WriteKey) Then
                            '        Continue For
                            '    End If
                            'Next

                            'Expand any env variables if necessary
                            If key.GetValueKind(v) = RegistryValueKind.ExpandString Then command = Environment.ExpandEnvironmentVariables(command)

                            'If we are going to delete the key, check to see if the command
                            'contains a "!" prefix, meaning that we are going to delete it after we run it
                            Dim deleteAfter As Boolean = delete AndAlso command.StartsWith("!")
                            If deleteAfter Then command = command.TrimStart("!")

                            'If it didn't have the "!" prefix, delete it now
                            If delete AndAlso Not deleteAfter Then key.DeleteValue(v)

                            Dim p As Process = Nothing
                            Dim args() As String = FileActions.ParseProgramArgList(command)

                            Try

                                'Decide whether to use CreateProcess or ShellExecute
                                If command.Contains("\") OrElse command.Contains(":") Then
                                    If args.Length > 1 Then
                                        p = Process.Start(args(0), MakeArgList(args, 1, args.Length - 1))
                                    Else
                                        p = Process.Start(args(0))
                                    End If
                                Else
                                    If args.Length > 1 Then
                                        ShellExecute(args(0), MakeArgList(args, 1, args.Length - 1))
                                    Else
                                        ShellExecute(args(0))
                                    End If
                                End If

                            Catch ex As Exception
                                Debug.Print("An error occurred while running " & command & ". " & ex.Message)
                            End Try

                            'Wait for the program to exit if necessary
                            If waitForExit AndAlso p IsNot Nothing Then p.WaitForExit()

                            'Delete the registry key now if it wasn't deleted before
                            If deleteAfter Then
                                key.DeleteValue(v)
                            End If

                        Catch ex As Exception
                            Debug.Print("Error running startup program: " & CStr(key.GetValue(v)))
                        End Try
                    End If
                Next

                'If we were asked to run any subkeys of this key, do so.
                If runSubKeys Then
                    For Each sk As String In key.GetSubKeyNames
                        RunStartupPrograms(key.OpenSubKey(sk, delete), delete, runSubKeys)
                    Next
                End If
            End If
        Catch ex As Exception
            Debug.Print("Error running startup programs. " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Runs all files in the specified directory using ShellExecute.
    ''' </summary>
    ''' <param name="dir"></param>
    ''' <remarks></remarks>
    Public Sub RunStartupPrograms(ByVal dir As IO.DirectoryInfo)
        If SystemInformation.BootMode = BootMode.Normal Then
            For Each f As IO.FileInfo In dir.GetFiles
                Try
                    If f.Attributes And IO.FileAttributes.Hidden = 0 Then
                        ShellExecute(f.FullName)
                    End If
                Catch ex As Exception
                    Debug.Print("There was an error running the startup program: " & f.FullName)
                End Try
            Next
        End If
    End Sub

    Public Sub RunOnceEx()
        Dim args As String = "iernonce.dll,RunOnceExProcess"
        If IO.File.Exists(IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "iernonce.dll")) Then
            'The file doesn't exist on NT4
            ShellExecute("rundll32.exe", args)
        End If
    End Sub

#End Region

    ''' <summary>
    ''' Reads the user's desktop background settings from the registry and applies them.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ApplyDesktopBackground()
        Try
            Dim deskCpl As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop")
            SystemInformationWritable.SetDesktopBackground(CStr(deskCpl.GetValue("WallPaper")))
        Catch ex As Exception
            Debug.Print("Error setting desktop wallpaper. " & ex.Message)
        End Try
    End Sub

    Private Declare Auto Function OpenEvent Lib "kernel32.dll" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Boolean, ByVal lpName As String) As IntPtr
    Private Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Boolean
    Private Declare Auto Function SetEvent Lib "kernel32.dll" (ByVal handle As IntPtr) As Boolean

    Private Const EVENT_MODIFY_STATE As Integer = 2

    ''' <summary>
    ''' Tell the welcome screen to close before and windows can be displayed. Otherwise, windows will pop up below the welcome screen and won't be visible.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CloseWelcomeScreen()
        Dim eventHandle As IntPtr
        If Environment.OSVersion.Platform = PlatformID.Win32NT AndAlso Environment.OSVersion.Version.Major >= 5 Then
            eventHandle = OpenEvent(EVENT_MODIFY_STATE, True, "Global\msgina: ShellReadyEvent")
        Else
            eventHandle = OpenEvent(EVENT_MODIFY_STATE, False, "msgina: ShellReadyEvent")
        End If
        If Not eventHandle = IntPtr.Zero Then
            SetEvent(eventHandle)
            CloseHandle(eventHandle)
        End If
    End Sub

    ''' <summary>
    ''' Undocumented: Send a message that the loading is finished.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SendShellLoadingFinished()
        WindowInfo.GetDesktopWindow.SendMessage(&H400, 0, 0) 'WM_USER
    End Sub

    Dim _zshellMutex As Threading.Mutex

    ''' <summary>
    ''' Check to see if Z-Shell is already running.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AlreadyRunning() As Boolean
        Dim init As Boolean
        _zshellMutex = New Threading.Mutex(True, "Z-Shell", init)
        Return Not init
    End Function

    ''' <summary>
    ''' Sets an event that signals to MSGINA to show the welcome screen again.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowWelcomeScreen()
        Dim hShellReadyEvent As IntPtr
        If Environment.OSVersion.Platform = PlatformID.Win32NT AndAlso Environment.OSVersion.Version.Major >= 5 Then
            hShellReadyEvent = OpenEvent(EVENT_MODIFY_STATE, True, "Global\msgina: ReturnToWelcome")
        Else
            hShellReadyEvent = OpenEvent(EVENT_MODIFY_STATE, False, "msgina: ReturnToWelcome")
        End If
        If hShellReadyEvent <> 0 Then
            SetEvent(hShellReadyEvent)
            CloseHandle(hShellReadyEvent)
        End If
    End Sub

    Private Declare Auto Function SetProcessShutdownParameters Lib "kernel32.dll" (ByVal dwLevel As Integer, ByVal dwFlags As Integer) As Boolean

    ''' <summary>
    ''' Instructs CSRSS to shut us down last (actually, second to last, before Task Manager).
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetShutdownPriority()
        SetProcessShutdownParameters(2, 0)
    End Sub

#Region "Message Pump Threads"

    Private Sub StartSystemTray()
        SystemTray.InitSystemTray()
        Application.Run()
    End Sub

    Public Sub StartShellEvents()
        ShellEvents.InitShellEvents()
        Application.Run()
    End Sub

    Private Sub StartLoadingUI()
        UILoadingForm.Show()
        Application.Run()
    End Sub

    Private Delegate Sub NoParamsDelegate()

    Private Sub StartUIEngine()
        'Show the Loading form to indicate progress
        Application.DoEvents()
        UILoadingForm.Show()
        Application.DoEvents()

        'Load the default style
        Dim ofd As New OpenFileDialog
        ofd.ShowDialog()
        'StyleLoader.LoadStyle(IO.Path.Combine(Application.StartupPath, "DefaultStyle\DefaultStyle.xml"))
        StyleLoader.LoadStyle(ofd.FileName)

        'Get rid of the loading progress form
        UILoadingForm.FadeOut()

        'MessageBox.Show(StyleLoader.Description & vbCrLf & StyleLoader.Author & vbCrLf & StyleLoader.Copyright)

        'For now, just start the default taskbar configuration
        Application.Run(TaskBarForm)
    End Sub

#End Region

    <STAThread()> _
    Public Sub Main()
        'Tell the Welcome Screen to close before any windows can be displayed
        CloseWelcomeScreen()

        'Enable visual styles' skinning for this app
        Application.EnableVisualStyles()
        'Render text with ClearType, if available
        Application.SetCompatibleTextRenderingDefault(False)

        'Check to see if Z-Shell is already running and close if so.
        If AlreadyRunning() Then
            MessageBox.Show("Z-Shell is already running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        'Check to see if another shell is running and close if so.
        If ExplorerRunning() Then
            MessageBox.Show("Another shell is already running. You must exit your current shell before Z-Shell can run.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        'Make us shutdown last
        SetShutdownPriority()
        'Tell the window manager to hide minimized windows.
        SystemInformationWritable.SetHideMinimizedWindows()
        'Tell Explorer to only start up folder windows, as opposed to starting folder windows and the desktop
        SetDesktopProcess()
        'Apply the correct desktop background
        ApplyDesktopBackground()

        'First, intialize the system tray to listen for appbars, icons, etc.
        Dim trayThread As New Threading.Thread(AddressOf StartSystemTray)
        trayThread.Start()

        'Start listening for shell events
        Dim eventsThread As New Threading.Thread(AddressOf StartShellEvents)
        eventsThread.Start()

        'Start listening for hotkeys
        HotkeyManager.InitHotkeys()

        'Start handling unhandled exceptions
        AddHandler Application.ThreadException, AddressOf ErrorOccurred
        AddHandler Application.ApplicationExit, AddressOf ApplicationExit

        'Run all programs set to autorun at startup
        Dim startupThread As New System.Threading.Thread(AddressOf RunStartupPrograms)
        startupThread.Start()

        'Start the taskbar
        Dim t As New Threading.Thread(AddressOf StartUIEngine)
        t.SetApartmentState(Threading.ApartmentState.STA)
        t.Start()

        'Tell Windows that we have finished loading our shell
        SendShellLoadingFinished()

        'Start the desktop
        DesktopWindow.InitDesktop()
        Application.Run()
        'Application.Run(DesktopForm) 'This function now just waits for the app to shutdown

        '-----------------------------------
        'When the program finishes, run shutdown stuff here:

        Try
            DesktopWindow.UninitDesktop()
            SetWorkingArea(Screen.PrimaryScreen.Bounds)
            HotkeyManager.UninitHotkeys()
            ShellEvents.UninitShellEvents()
            SystemTray.UninitSystemTray()
            'Delete the mutex used to check for multiple instances
            _zshellMutex.ReleaseMutex()
            'Allow Explorer to start now
            _explorerMutex.ReleaseMutex()
        Catch ex As Exception
            Debug.Print("Error running shutdown. " & ex.Message)
        End Try
    End Sub

    Private Sub ErrorOccurred(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
        MessageBox.Show("The following error occurred: " & e.Exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub ApplicationExit(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

End Module