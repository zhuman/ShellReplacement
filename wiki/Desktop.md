!! The Desktop



First, let's clarify a few things about desktops. Windows has three different types of elements with the name "desktop".  Firstly, you have NT desktops which contain windows. Each NT desktop is a securable object that is contained inside of an NT window station. For example, the login screen is contained in a separate desktop, as is each logged on user. The screensaver is also on a separate desktop. It's recommended that you read more on NT desktops and window stations on MSDN.



Another thing that holds the name of "desktop" is the very base window that the desktop background is painted on. . You can't normally see this window since it is obscured by the shell's "desktop", which normally also has a desktop background painted on it. Therefore, this is often confused with the shell's "desktop". If you quit your currently running shell, it comes into view. This window does basically nothing except respond to paint events.



The third "desktop" is the shell's desktop, which is of much more concern to you, a shell developer. If you've used Spy++ or the like, you probably familiar with Explorer's "Progman" window. This window is what actually contains your desktop icons. It is actually a very "normal" window except that it has the odd characteristic of staying on the bottom of the z-order. It's like the opposite of the "topmost" window position. However, you don't set this like "topmost" (that is, through SetWindowPos). Instead you must resort to an undocumented API call titled "SetShellWindow". This function is basically the opposite of the documented "GetShellWindow". "GetShellWindow" returns the "Progman" window when Explorer is running and can be used most of the time to figure out whether another shell is currently running (along with checking for a window already with the class name "{"Shell_TrayWnd"}").



*To be continued...*