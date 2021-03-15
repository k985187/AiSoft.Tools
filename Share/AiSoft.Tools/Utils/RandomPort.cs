using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace AiSoft.Tools.Utils
{
    /// <summary>
    /// 随机端口
    /// </summary>
    public class RandomPort
    { 
        /// <summary>
        /// 随机
        /// </summary>
        private static Random _random;

        /// <summary>
        /// 静态初始化
        /// </summary>
        static RandomPort()
        {
            var tick = DateTime.Now.Ticks;
            _random = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
        }

        /// <summary>
        /// 返回可用端口
        /// </summary>
        /// <returns></returns>
        public static int GetRandAvailablePort()
        {
            var port = _random.Next(1050, 65500);
            if (PortIsAvailable(port))
            {
                return port;
            }
            return GetRandAvailablePort();
        }

        /// <summary>
        /// 检测端口是否被用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private static bool PortIsAvailable(int port)
        {
            var portUsed = PortIsUsed();
            return portUsed.All(p => p != port);
        }

        /// <summary>
        /// 获取有用端口
        /// </summary>
        /// <returns></returns>
        private static List<int> PortIsUsed()
        {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var ipsTcp = ipGlobalProperties.GetActiveTcpListeners();
            var ipsUdp = ipGlobalProperties.GetActiveUdpListeners();
            var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            var allPorts = ipsTcp.Select(ep => ep.Port).ToList();
            allPorts.AddRange(ipsUdp.Select(ep => ep.Port));
            allPorts.AddRange(tcpConnInfoArray.Select(conn => conn.LocalEndPoint.Port));
            return allPorts;
        }
    }
}