using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AiSoft.Tools.Api;

namespace AiSoft.Tools.Helpers
{
    public class ThreadHelper
    {
        /// <summary>
        /// 获取线程信息
        /// </summary>
        /// <param name="pro"></param>
        /// <param name="windowName"></param>
        /// <param name="className"></param>
        /// <param name="isSearchAllProcess"></param>
        /// <returns></returns>
        public static ProcessThreadInfo GetWindowProcessThread(Process pro, string windowName, string className, bool isSearchAllProcess = false)
        {
            if (pro == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(windowName))
            {
                var proThreadInfo = new ProcessThreadInfo
                {
                    ProcessId = pro.Id,
                    ProcessFullPath = pro.MainModule.FileName,
                    WindowTitle = pro.MainWindowTitle,
                    ThreadId = WinApi.GetWindowThreadProcessId(pro.MainWindowHandle),
                    WindowHandle = pro.MainWindowHandle
                };
                return proThreadInfo;
            }
            var handles = new List<IntPtr>();
            foreach (ProcessThread thread in pro.Threads)
            {
                WinApi.EnumThreadWindows(thread.Id, (hWnd, lParam) =>
                {
                    handles.Add(hWnd);
                    WinApi.EnumChildWindows(hWnd, (h, l) =>
                    {
                        handles.Add(h);
                        return true;
                    }, IntPtr.Zero);
                    return true;
                }, IntPtr.Zero);
            }
            if (isSearchAllProcess)
            {
                WinApi.EnumWindows((hWnd, lParam) =>
                {
                    handles.Add(hWnd);
                    WinApi.EnumChildWindows(hWnd, (h, l) =>
                    {
                        handles.Add(h);
                        return true;
                    }, IntPtr.Zero);
                    return true;
                }, IntPtr.Zero);
            }
            var nameBuffer = new StringBuilder(256);
            foreach (var hWnd in handles)
            {
                nameBuffer.Clear();
                var hw = hWnd;
                WinApi.SendMessage(hw, WinApi.WM_GETTEXT, nameBuffer.Capacity, nameBuffer);
                if (!nameBuffer.ToString().Contains(windowName))
                {
                    continue;
                }
                if (!string.IsNullOrWhiteSpace(className))
                {
                    hw = WinApi.FindWindowEx(hw, IntPtr.Zero, className, null);
                    if (hw == IntPtr.Zero)
                    {
                        continue;
                    }
                }
                var proThreadInfo = new ProcessThreadInfo
                {
                    ProcessId = pro.Id,
                    ProcessFullPath = pro.MainModule.FileName,
                    WindowTitle = nameBuffer.ToString(),
                    ThreadId = WinApi.GetWindowThreadProcessId(hw),
                    WindowHandle = hw
                };
                return proThreadInfo;
            }
            handles.Clear();
            return null;
        }
    }
}