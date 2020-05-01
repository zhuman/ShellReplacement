# FAQ

### What is a Windows shell?

When you log in to Microsoft Windows, you get a very common interface. There is the desktop which contains your desktop icons. There is also the taskbar, which contains currently running programs, the system tray, and the clock. The start menu appears with links to your programs. Folder windows display with a consistent interface. Programs that have been configured to start when you login do so. All of these things are provided by the shell. So, basically, the shell is the background user interface for Windows. Currently, the shell, called "Explorer" (not to be confused with Internet Explorer) acts as your Windows shell. If you go into Task Manager by pressing Control{"+"}Alt{"+"}Delete and end the process called "explorer.exe", you will see the taskbar, and your desktop icons disappear. If you minimize a window, it simply minimizes to a little toolbar-sized bar hovering over the now-empty desktop. (If you want explorer back now, go back into task manager, go to File->Run, and type "explorer" (no quotes)).



### Why design a new shell if the one with Windows works fine?

The biggest reason someone would want to use a different shell is customization. I have yet to see a replacement shell that actually fully replaces explorer, but they all have features explorer doesn't. Many replacement shells work with explorer to provide both better customization and a full Windows experience. The current vision for Z-Shell is that it will provide a fully customizable environment which means that you can custom build your own shell using the Z-Shell designers and end up with a radically new interface that fits your exact needs.



### What makes Z-Shell different from other replacement shells?

A few things set Z-Shell apart. Firstly, Z-Shell is written entirely in managed code using the .NET framework which means that we can concentrate more on features rather than rebuilding the wheel. Z-Shell's vision is a completely customizable shell that goes beyond what you might think of as "customizable". By version 1.0, you will be able to start with a blank screen in an editor. Then you will be able to start dragging panels, buttons, and menus around.