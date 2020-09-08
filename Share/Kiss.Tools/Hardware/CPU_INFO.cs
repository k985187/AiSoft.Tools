using System;
using System.Runtime.InteropServices;

namespace Kiss.Tools.Hardware
{

    public static partial class SystemInfo
    {
        /// <summary>
        /// 定义CPU的信息结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CPU_INFO
        {
            public uint dwOemId;
            public uint dwPageSize;
            public uint lpMinimumApplicationAddress;
            public uint lpMaximumApplicationAddress;
            public uint dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public uint dwProcessorLevel;
            public uint dwProcessorRevision;
        }
    }
}