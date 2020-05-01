# Shell Startup

Although you may not realize it, the Windows shell is in charge of many things often taken for granted by users. The following is a list of actions a Windows shell should and/or usually does perform at startup.

## Startup Programs

One of the first things a shell must do is run the user's autorun programs. These programs can be found in 6 places:

* `HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run`
* `HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\RunOnce`
* `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run`
* `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\RunOnce`
* `%USERPROFILE%\Start Menu\Startup`
* `%ALLUSERSPROFILE%\Start Menu\Startup`

Of the six startup locations, the last two (...\Start Menu\Startup) are folder locations that can be configured by the user. Therefore, you should use SHGetFolderPath or the like to retrieve the actual locations of the folders. These folders are the easiest to startup. Simply use ShellExecute with the default verb for each file in each folder. Exclude hidden files.

The registry paths are a little more complex. Two of them (the "RunOnce" keys) must be cleared of all values after you run them. All four of them can contain some rather odd formatting since any application is free to put anything they want into the values. You must first split the values into sections. The first section is the application path. The rest are arguments. This may seem simple at first, but you must take into account a few different possibilities. For example:

```
C:\Program Files\Example Company\Example Program\Example App.exe" -startup -input "Example File.txt
```

Ouch, that was harsh. You must parse through the string looking for quotes first. Then, you must look for spaces in the rest of the string.

Believe it or not, there are actually more locations than that list. Starting with Internet Explorer 5.5 there are also RunOnceEx registry keys. However, you don't  handle those manually. In order to handle these your code must do the following. First, check to see if the file "iernonce.dll" exists in the system folder (use SHGetFolderPath or the like). If it does, call ShellExecute with "rundll32.exe" as the file and "iernonce.dll,runonceexprocess" as the arguments. The Internet Explorer DLL handles the rest. Note also that on Windows 95, installing Internet Explorer 5.5 installs this command into the local machine's "run" key since the Windows 95 shell doesn't handle it otherwise. Therefore, you don't have to run it on Windows 95 (of course, if you are developing a shell for Windows 95, this guide isn't really for you).

One more thing, it is possible for a system administrator to restrict a user's rights towards startup programs. You should respect this by calling SHRestricted four times with the following values:

* REST_NOLOCALMACHINERUN
* REST_NOCURRENTUSERRUN
* REST_NOLOCALMACHINERUNONCE
* REST_NOCURRENTUSERRUNONCE

SHRestricted will return a boolean value indicating whether or not to run the associated key (true = don't run it, false = run it).

## Hide Minimized Windows

If you can remember back to the days of Windows 3.1, Windows had no taskbar to speak of. After you minimized a window, it became a small floating titlebar in the corner of your screen. Windows still do this today. The difference is that Explorer tells the "window manager" to hide them. In truth, they are still visible, but simply have been pushed off of the screen. To test this out, try calling GetWindowPos on a minimized window. It returns something around -3000 or so in both the X and the Y.

If your Windows shell replacement provides some task switching functionality, you're going to want to hide the minimized windows just like Explorer does. In fact, you can't create a shell hook window until minimized windows have been hidden (see [Shell Events](ShellEvents.md)). This makes sense, since you generally only create shell hooks to manage open windows anyway.

In order to actually hide the minimized windows, you call SystemParametersInfo with `SPI_SETMINIMIZEDMETRICS`. You must pass a MINIMIZEDMETRICS structure to this function with the iArrange field set to `ARW_HIDE` (0x8). The first time you do this you may notice some odd problems when you start back up Explorer. All of the taskbar buttons are really small!! This is because you left all of the other fields of the MINIMIZEDMETRICS structure at 0x0. I'd recommend calling SystemParametersInfo with `SPI_GETMINIMIZEDMETRICS` and then simply changing the iArrange field before passing it back to `SPI_SETMINIMIZEDMETRICS`. You can also fill in your own values.

*To be continued...*