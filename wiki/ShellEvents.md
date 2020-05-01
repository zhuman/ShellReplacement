# Shell Events (a.k.a. Shell Hooks)

You may have wondered before about how Explorer's taskbar knows when a window is opened, closed, or activated. It even knows when window titles and icons have been changed. One way to accomplish this is a simple loop that constantly enumerates all open windows, looking for changes. The much better way to accomplish this task is to set up a _shell hook_. If you look at the documentation for `SetWindowsHookEx` you will see the WH_SHELL option. Looking through the list of hook messages that a ShellProc can receive, you can easily see that this is how Explorer does it.

But wait! Microsoft has made it even easier! Explorer doesn't actually call `SetWindowsHookEx`. Normally, to set up a system wide hook requires that you create a separate DLL in a non-managed language and set up the hook through that. This is all a little more complex than it needs to be. All you really want to do is to receive some notifications. Therefore, Microsoft created the function `RegisterShellHookWindow`. This function takes a handle to a window that you have created. Every time a shell event occurs, it packages the notification into the lParam and wParam of a message it sends to your window. You must check the message code to see whether or not the message is a shell notification. If it is, the message code will be the value of `RegisterWindowMessage("SHELLHOOK")`. The wParam will be one of the following:

* `HSHELL_GETMINRECT`
* `HSHELL_WINDOWACTIVATEED`
* `HSHELL_RUDEAPPACTIVATEED`
* `HSHELL_WINDOWREPLACING`
* `HSHELL_WINDOWREPLACED`
* `HSHELL_WINDOWCREATED`
* `HSHELL_WINDOWDESTROYED`
* `HSHELL_ACTIVATESHELLWINDOW`
* `HSHELL_TASKMAN`
* `HSHELL_REDRAW`
* `HSHELL_FLASH`
* `HSHELL_ENDTASK`
* `HSHELL_APPCOMMAND`

It is worth clarifying a few things about some of the events. Firstly, `HSHELL_REDRAW` means that either a window's title text has changed, or the window's icon has changed. `HSHELL_FLASH` should be treated similarily to `HSHELL_REDRAW`, except that you should redraw it in a flashed state (normally orange on the Explorer taskbar). The system handles all flashing/unflashing timings so you just have to respond to the two notifications. An `HSHELL_REDRAW` event is automatically called later to then redraw it back in a normal state, creating the flashing animation you are used to.

Secondly, the two notification `HSHELL_WINDOWREPLACING` and `HSHELL_WINDOWREPLACED` are sent one after another. As soon as you receive an `HSHELL_WINDOWREPLACING` message, store the hWnd of the old window. Then, when you receive the `HSHELL_WINDOWREPLACED` message, anywhere you have the old hWnd in your task manager replace it with the new hWnd. You should also update the icon and text to reflect the new window as if an `HSHELL_REDRAW` message had occurred.

The `HSHELL_GETMINRECT` tells you that a window is being minimized. Your job is to send it the coordinates of a rectangle used to create the minimize animation. For example, Explorer gives it the rectangle of the window's task button. This gives the user a visual indication of where the window has gone. One thing you may notice about this message is that you receive a handle to a SHELLHOOKINFO structure, but this structure is not documented! You have to open WinUser.h and find the declaration if you aren't using C++. To save you some time, here it is:

```vb
typedef struct
{
    HWND    hwnd;
    RECT    rc;
} SHELLHOOKINFO, *LPSHELLHOOKINFO;
```

To tell you the truth, I don't yet know exactly how to send back the minimize rectangle properly. It might require that your application have a registered "shell window". This will be updated when I figure it out.

### Show In Taskbar?
You've probably noticed that there are some windows that don't show up in the taskbar. Why is this? Microsoft gives some guidelines on MSDN as to which windows show up and which don't. Here are the guidelines summarized:
* The window must be visible (duh)
* The window must be top level (GetParent(hWnd)=0)
* The window must meet *one* of these conditions:
** The window doesn't have the `WS_EX_TOOLWINDOW` extended style and the window has no owner
** The window has the `WS_EX_APPWINDOW` extended style and the window is owned
Notice that if the window has the `WS_EX_APPWINDOW` extended style applied it still can't have a parent to show up, but it can have an owner. Tool windows (`WS_EX_TOOLWINDOW`) only show up if they have neither a parent nor an owner. It all boils down to the following code in Z-Shell:

```vb
If IsWindowVisible(hWnd) And (GetParent(hWnd) = 0) Then
     Dim exStyles As Integer = GetWindowLong(hWnd, GWL_EXSTYLE)
     Dim ownerWin As Integer = GetWindow(hWnd, GW_OWNER)
     If ((exStyles And WS_EX_TOOLWINDOW) = 0) And (ownerWin = 0)) Or ((exStyles And WS_EX_APPWINDOW) And (ownerWin <> 0)) Then
          Return True
     End If
End If
Return False
```

Although this code is in VB.NET, it should be easily translated to other languages.