# The System Tray

If you've ever spent much time in Spy++ or any other UI spying program, you are probably familiar with the window `Shell_TrayWnd`. That's the class name for the Explorer taskbar. What most people don't know is that this class name is special to the system. Even though it's not documented anywhere on MSDN, any window with this class name is responsible for quite a few things. Firstly, it handles notify icon messages. Secondly, it handles appbar messages. I'd recommend reading up [the Shell_NotifyIcon function](http://msdn2.microsoft.com/en-us/library/ms647738.aspx) and [the SHAppBarMessage function](http://msdn2.microsoft.com/en-us/library/ms647647.aspx).

## Notify Icons

When you call Shell_NotifyIcon, the code inside of Shell32.dll doesn't actually handle the message at all. It checks the cbSize value of the NOTIFYICONDATA structure you pass to it and, if needed, converts it to the latest version before packaging it into a {"WM_COPYDATA"} message. Where does it send it? It calls FindWindow and looks for a window with the class name of {"Shell_TrayWnd"}. If it can't find one, the function fails. Otherwise, it sends the message and simply returns the message result. Your shell program is in charge of handling all notify icon events and the display. This gives you a lot of choice in exactly how you implement the system tray.

### Shell Service Objects



### Appbars
