# Sub-Projects

Z-Shell is actually more than just a shell replacement. It also features a managed library wrapping a very large portion of the Win32 API (mostly user32 and shell32 stuff). Some of the larger classes and namespaces represent sub-projects that could use some development on their own. They are listed below. Remeber that this list will grow as new sections of Z-Shell grow.

## Shell Related Classes

* A shell namespace based file browsing library is needed to allow for folder windows, desktop icons, etc.

* `ShellEvents` provides window manager events such as window creation, deletion, activation, etc. The HSHELL_GETMINRECT message doesn't work.

* `SystemTray` gives us notify icons, and eventually we should handle AppBar messages as well

## User32 Related Classes

* The `WindowInfo` class provides a base for just about everything that can be considered in this category. It wraps around a native window's handle and provides just about every type of information about the window you could ever want. It can continue to expand with new features targeting every usage nitch.

* The HotKey class allows registering hotkeys with the system and provides events for handling them.

## Shell Controls

* The `AllProgramsTree` provides a start menu's all programs list. It functions very similarly to the Vista all programs tree in many ways, and actually improves in other ways.

* The `TaskBarControl` provides a highly customizable taskbar type control using the ShellEvents class. It can take on the look of your system's current visual style to various extents.

* `NotifyIconBar` displays notify icons and handles user input for them.