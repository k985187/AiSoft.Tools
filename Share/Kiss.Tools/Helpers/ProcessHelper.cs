using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Kiss.Tools.Helpers
{
    /// <summary>
    /// 进程帮助类
    /// </summary>
    public class ProcessHelper
    {
        /// <summary>
        /// 当前运行实例
        /// </summary>
        /// <param name="assemblyLocation">程序集路径</param>
        /// <returns>返回当前运行实例</returns>
        public static Process RunningInstance(string assemblyLocation)
        {
            var current = Process.GetCurrentProcess();
            var procByNameList = Process.GetProcessesByName(current.ProcessName);
            var procWhere = procByNameList.Where(p => p.Id != current.Id);
            var proc = procWhere.FirstOrDefault(p => assemblyLocation.Replace("/", "\\") == p.MainModule.FileName);
            return proc;
        }

        /// <summary>
        /// 操作运行实例句柄
        /// </summary>
        /// <param name="instance">运行实例</param>
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, IsIconic(instance.MainWindowHandle) ? WindowShowStyle.Restore : WindowShowStyle.Show);
            SetForegroundWindow(instance.MainWindowHandle);
        }

        private enum WindowShowStyle : int
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
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, WindowShowStyle cmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}