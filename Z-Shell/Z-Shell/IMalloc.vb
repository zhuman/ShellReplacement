Imports System.Runtime.InteropServices

<ComImport()> _
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
<Guid("00000002-0000-0000-C000-000000000046")> _
Public Interface IMalloc

    ''' <summary>
    ''' Allocates a block of memory.
    ''' </summary>
    ''' <param name="cb">Size, in bytes, of the memory block to be allocated.</param>
    ''' <returns>A pointer to the allocated memory block.</returns>
    ''' <remarks></remarks>
    <PreserveSig()> _
     Function Alloc(ByVal cb As UInt32) As IntPtr

    ''' <summary>
    ''' Changes the size of a previously allocated memory block.
    ''' </summary>
    ''' <param name="pv">Pointer to the memory block to be reallocated.</param>
    ''' <param name="cb">Size of the memory block (in bytes) to be reallocated.</param>
    ''' <returns>Reallocated memory block </returns>
    ''' <remarks></remarks>
    <PreserveSig()> _
     Function Realloc(ByVal pv As IntPtr, ByVal cb As UInt32) As IntPtr

    ''' <summary>
    ''' Frees a previously allocated block of memory.
    ''' </summary>
    ''' <param name="pv">Pointer to the memory block to be freed.</param>
    ''' <remarks></remarks>
    <PreserveSig()> _
     Sub Free(ByVal pv As IntPtr)

    ''' <summary>
    ''' This method returns the size (in bytes) of a memory block previously allocated with 
    ''' IMalloc::Alloc or IMalloc::Realloc.
    ''' </summary>
    ''' <param name="pv">Pointer to the memory block for which the size is requested.</param>
    ''' <returns>The size of the allocated memory block in bytes.</returns>
    ''' <remarks></remarks>
    <PreserveSig()> _
     Function GetSize(ByVal pv As IntPtr) As UInt32

    ''' <summary>
    ''' This method determines whether this allocator was used to allocate the specified block of memory.
    ''' </summary>
    ''' <param name="pv">Pointer to the memory block</param>
    ''' <returns>1 - allocated 0 - not allocated by this IMalloc instance.</returns>
    ''' <remarks></remarks>
    <PreserveSig()> _
     Function DidAlloc(ByVal pv As IntPtr) As Int16

    ''' <summary>
    ''' This method minimizes the heap as much as possible by releasing unused memory to the operating system, 
    ''' coalescing adjacent free blocks and committing free pages.
    ''' </summary>
    ''' <remarks></remarks>
    <PreserveSig()> _
     Sub HeapMinimize()

End Interface