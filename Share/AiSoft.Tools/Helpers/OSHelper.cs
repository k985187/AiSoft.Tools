using System;
using System.Runtime.InteropServices;

namespace AiSoft.Tools.Helpers
{
    public class OSHelper
    {
        /// <summary>
        /// 是否Windows系统
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        /// <summary>
        /// 是否Linux系统
        /// </summary>
        /// <returns></returns>
        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        /// <summary>
        /// 是否OSX系统
        /// </summary>
        /// <returns></returns>
        public static bool IsOSX()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }
    }
}