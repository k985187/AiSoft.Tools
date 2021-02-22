using System;
using System.Diagnostics;
using System.Linq;
using Kiss.Tools.Api;

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
            WinApi.ShowWindowAsync(instance.MainWindowHandle, WinApi.IsIconic(instance.MainWindowHandle) ? WinApi.WindowShowStyle.Restore : WinApi.WindowShowStyle.Show);
            WinApi.SetForegroundWindow(instance.MainWindowHandle);
        }
    }
}