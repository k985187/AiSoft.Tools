using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Kiss.Tools.Extensions
{
    public static class IPAddressExtension
    {
        /// <summary>
        /// 判断IP是否是私有地址
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsPrivateIP(this IPAddress ip)
        {
            if (IPAddress.IsLoopback(ip))
            {
                return true;
            }
            ip = ip.IsIPv4MappedToIPv6 ? ip.MapToIPv4() : ip;
            var bytes = ip.GetAddressBytes();
            switch (ip.AddressFamily)
            {
                case AddressFamily.InterNetwork when bytes[0] == 10:
                case AddressFamily.InterNetwork when bytes[0] == 100 && bytes[1] >= 64 && bytes[1] <= 127:
                case AddressFamily.InterNetwork when bytes[0] == 169 && bytes[1] == 254:
                case AddressFamily.InterNetwork when bytes[0] == 172 && bytes[1] == 16:
                case AddressFamily.InterNetwork when bytes[0] == 192 && bytes[1] == 88 && bytes[2] == 99:
                case AddressFamily.InterNetwork when bytes[0] == 192 && bytes[1] == 168:
                case AddressFamily.InterNetwork when bytes[0] == 198 && bytes[1] == 18:
                case AddressFamily.InterNetwork when bytes[0] == 198 && bytes[1] == 51 && bytes[2] == 100:
                case AddressFamily.InterNetwork when bytes[0] == 203 && bytes[1] == 0 && bytes[2] == 113:
                case AddressFamily.InterNetworkV6 when ip.IsIPv6Teredo || ip.IsIPv6LinkLocal || ip.IsIPv6Multicast || ip.IsIPv6SiteLocal:
                case AddressFamily.InterNetworkV6 when ip.ToString().StartsWith("::"):
                case AddressFamily.InterNetworkV6 when ip.ToString().StartsWith("64:ff9b::"):
                case AddressFamily.InterNetworkV6 when ip.ToString().StartsWith("100::"):
                case AddressFamily.InterNetworkV6 when ip.ToString().StartsWith("2001::"):
                case AddressFamily.InterNetworkV6 when ip.ToString().StartsWith("2001:2"):
                case AddressFamily.InterNetworkV6 when ip.ToString().StartsWith("2001:db8:"):
                case AddressFamily.InterNetworkV6 when ip.ToString().StartsWith("2002:"):
                case AddressFamily.InterNetworkV6 when ip.ToString().StartsWith("fc"):
                case AddressFamily.InterNetworkV6 when ip.ToString().StartsWith("fd"):
                case AddressFamily.InterNetworkV6 when ip.ToString().StartsWith("fe"):
                case AddressFamily.InterNetworkV6 when bytes[0] == 255:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 获取域名的DNS IP
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetDnsIpAddress(this string ip)
        {
            try
            {
                var ipAddresses = Dns.GetHostAddressesAsync(ip).Result;
                return ipAddresses.Length > 0 ? ipAddresses[0].ToString() : ip;
            }
            catch
            {
                return ip;
            }
        }

        /// <summary>
        /// 获取域名的DNS IP
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static async Task<string> GetDnsIpAddressAsync(this string ip) => await Task.Run(ip.GetDnsIpAddress);
    }
}