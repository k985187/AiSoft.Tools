﻿using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AiSoft.Tools.Api
{
    public class WinApi
    {
        public const UInt32 FLASHW_STOP = 0;
        public const UInt32 FLASHW_CAPTION = 1;
        public const UInt32 FLASHW_TRAY = 2;
        public const UInt32 FLASHW_ALL = 3;
        public const UInt32 FLASHW_TIMER = 4;
        public const UInt32 FLASHW_TIMERNOFG = 12;

        public struct FLASHWINFO
        {

            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        [DllImport("User32.dll")]
        public static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [DllImport("User32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public enum WindowShowStyle : uint
        {
            Hide = 0,
            ShowNormal = 1,
            ShowMinimized = 2,
            ShowMaximized = 3,
            Maximize = 3,
            ShowNormalNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActivate = 7,
            ShowNoActivate = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimized = 11
        }

        [DllImport("User32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("Kernel32.dll")]
        public static extern bool SetLocalTime(ref SystemTime time);

        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public short year;
            public short month;
            public short dayOfWeek;
            public short day;
            public short hour;
            public short minute;
            public short second;
            public short milliseconds;
        }

        [DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, WindowShowStyle cmdShow);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool PostThreadMessage(int threadId, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);


        public const uint WM_GETTEXT = 0x000D;

        [DllImport("User32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, uint lpdwProcessId = 0);

        public delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern bool EnumThreadWindows(int dwThreadId, EnumWindowsDelegate lpfn, IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern bool EnumWindows(EnumWindowsDelegate lpfn, IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsDelegate lpfn, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hWndParent, IntPtr hWndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("User32.dll")]
        public static extern bool AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);

        [DllImport("Kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, StringBuilder lParam);
    }
}