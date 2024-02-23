﻿using System.Runtime.InteropServices;

namespace ScreenLib;

/// <summary>
/// FxCop requires all Marshalled functions to be in a class called NativeMethods.
/// </summary>
internal static class NativeMethods
{
    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool DeleteObject(IntPtr hObject);
}
