using System;

namespace AiSoft.Tools.Helpers
{
    public class ProcessThreadInfo
    {
        /// <summary>
        /// 进程ID
        /// </summary>
        public int ProcessId { get; set; }

        /// <summary>
        /// 进程路径
        /// </summary>
        public string ProcessFullPath { get; set; }

        /// <summary>
        /// 目标窗口标题
        /// </summary>
        public string WindowTitle { get; set; }

        /// <summary>
        /// 线程ID
        /// </summary>
        public int ThreadId { get; set; }

        /// <summary>
        /// 窗口句柄
        /// </summary>
        public IntPtr WindowHandle { get; set; }
    }
}