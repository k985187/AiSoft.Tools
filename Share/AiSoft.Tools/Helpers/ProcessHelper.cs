using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AiSoft.Tools.Api;

namespace AiSoft.Tools.Helpers
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
        /// <param name="msg">进程通信消息(0x8001-0xBFFF)</param>
        public static void HandleRunningInstance(Process instance, uint msg)
        {
            if (msg >= 0x8001 && msg <= 0xBFFF)
            {
                WinApi.PostThreadMessage(instance.Threads[0].Id, msg, IntPtr.Zero, IntPtr.Zero);
            }
            WinApi.ShowWindowAsync(instance.MainWindowHandle, WinApi.IsIconic(instance.MainWindowHandle) ? WinApi.WindowShowStyle.Restore : WinApi.WindowShowStyle.Show);
            WinApi.SetForegroundWindow(instance.MainWindowHandle);
        }

        /// <summary>
        /// 启动进程
        /// </summary>
        /// <param name="fileName">应用程序文件路径</param>
        /// <param name="args">启动参数</param>
        /// <param name="isWaitForHwnd">是否等待句柄</param>
        /// <param name="isWaitForExit">是否等待退出</param>
        /// <param name="isHide">是否隐藏</param>
        public static Process RunProcess(string fileName, string[] args, bool isWaitForHwnd = true, bool isWaitForExit = false, bool isHide = false)
        {
            var pro = new Process
            {
                StartInfo =
                {
                    FileName = fileName,
                    Arguments = args == null ? "" : string.Join(" ", args),
                    WorkingDirectory = Path.GetDirectoryName(fileName),
                    Verb = "runas"
                }
            };
            pro.StartInfo.WindowStyle = isHide ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal;
            try
            {
                pro.Start();
                if (isWaitForHwnd)
                {
                    pro.WaitForInputIdle();
                }
                if (isWaitForExit)
                {
                    pro.WaitForExit();
                }
            }
            catch (Exception e)
            {
                pro = null;
                LogHelper.WriteLog(e);
            }
            return pro;
        }

        /// <summary>
        /// 结束进程
        /// </summary>
        /// <param name="file"></param>
        public static void KillProcess(string file)
        {
            var proList = SearchProcess(file);
            foreach (var pro in proList)
            {
                KillProcess(pro);
            }
            proList.Clear();
        }

        /// <summary>
        /// 结束进程
        /// </summary>
        /// <param name="pro"></param>
        public static void KillProcess(Process pro)
        {
            try
            {
                pro.Kill();
                pro.WaitForExit();
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
            }
        }

        /// <summary>
        /// 结束进程
        /// </summary>
        /// <param name="proId"></param>
        public static void KillProcess(int proId)
        {
            var proList = SearchProcess(proId);
            foreach (var pro in proList)
            {
                KillProcess(pro);
            }
            proList.Clear();
        }

        /// <summary>
        /// 查找进程
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isFullPath">是否全路径</param>
        public static List<Process> SearchProcess(string file, bool isFullPath = true)
        {
            var proList = new List<Process>();
            if (string.IsNullOrEmpty(file))
            {
                return proList;
            }
            var pros = Process.GetProcesses();
            for (var i = 0; i < pros.Length - 1; i++)
            {
                try
                {
                    var pro = pros[i];
                    var runFile = pro.MainModule.FileName;
                    if (isFullPath)
                    {
                        if (runFile.ToLower() == file.ToLower().Replace("/", "\\").Replace(@"\\", @"\"))
                        {
                            proList.Add(pro);
                        }
                    }
                    else
                    {
                        if (runFile.ToLower().EndsWith(file.ToLower().Replace("/", "\\").Replace(@"\\", @"\")))
                        {
                            proList.Add(pro);
                        }
                    }
                }
                catch (Exception e)
                {
                    //LogHelper.WriteLog(e);
                }
            }
            return proList;
        }

        /// <summary>
        /// 查找进程
        /// </summary>
        /// <param name="proId"></param>
        public static List<Process> SearchProcess(int proId)
        {
            var proList = new List<Process>();
            var pros = Process.GetProcesses();
            for (var i = 0; i < pros.Length - 1; i++)
            {
                try
                {
                    var pro = pros[i];
                    if (pro.Id == proId)
                    {
                        proList.Add(pro);
                    }
                }
                catch (Exception e)
                {
                    //LogHelper.WriteLog(e);
                }
            }
            return proList;
        }
    }
}