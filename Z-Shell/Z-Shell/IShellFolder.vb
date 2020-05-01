Imports System.Runtime.InteropServices

<ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214E6-0000-0000-C000-000000000046")> _
Public Interface IShellFolder

	<PreserveSig()> _
	Function ParseDisplayName(ByVal hwnd As IntPtr, ByVal pbc As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByVal pszDisplayName As String, ByVal ByRefpchEaten As Integer, ByRef ppidl As IntPtr, ByRef pdwAttributes As Integer) As Integer

	<PreserveSig()> _
	Function EnumObjects(ByVal hwnd As IntPtr, ByVal grfFlags As SHCONTF, ByRef ppenumIDList As IntPtr) As Int32

	<PreserveSig()> _
	Function BindToObject(ByVal pidl As IntPtr, ByVal pbc As IntPtr, ByRef riid As Guid, ByRef ppv As IntPtr) As Int32

	<PreserveSig()> _
	Function BindToStorage(ByVal pidl As IntPtr, ByVal pbc As IntPtr, ByRef riid As Guid, ByRef ppv As IntPtr) As Int32

	<PreserveSig()> _
	Function CompareIDs(ByVal lParam As Int32, ByVal pidl1 As IntPtr, ByVal pidl2 As IntPtr) As Int32

	<PreserveSig()> _
	Function CreateViewObject(ByVal hwndOwner As IntPtr, ByVal riid As Guid, ByRef ppv As IntPtr) As Int32

	<PreserveSig()> _
	Function GetAttributesOf(ByVal cidl As Integer, ByRef apidl As IntPtr, ByRef rgfInOut As Integer) As Integer

	<PreserveSig()> _
	Function GetUIObjectOf(ByVal hwndOwner As IntPtr, ByVal cidl As UInt32, ByVal apidl() As IntPtr, ByVal riid As Guid, ByRef rgfReserved As UInt32, ByRef ppv As IntPtr) As Int32

	<PreserveSig()> _
	Function GetDisplayNameOf(ByVal pidl As IntPtr, ByVal uFlags As Integer, ByVal pName As IntPtr) As Int32

	<PreserveSig()> _
	Function SetNameOf(ByVal hwnd As IntPtr, ByVal pidl As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByVal pszName As [String], ByVal uFlags As UInt32, ByRef ppidlOut As IntPtr) As Int32

End Interface

Public Enum SHCONTF As Integer
	SHCONTF_FOLDERS
	SHCONTF_NONFOLDERS
	SHCONTF_INCLUDEHIDDEN
	SHCONTF_INIT_ON_FIRST_NEXT
	SHCONTF_NETPRINTERSRCH
	SHCONTF_SHAREABLE
	SHCONTF_STORAGE
	SHCONTF_FASTITEMS
	SHCONTF_FLATLIST
	SHCONTF_ENABLE_ASYNC
End Enum

Friend Class ShellFolders
    Public Declare Auto Function SHGetDesktopFolder Lib "shell32.dll" (ByRef folder As IShellFolder) As IntPtr
End Class