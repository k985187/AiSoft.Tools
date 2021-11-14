using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AiSoft.Tools.Helpers;
using AiSoft.Tools.Strings;

namespace AiSoft.Tools.Extensions
{
    public static class StringExtension
    {
        public static string Join(this IEnumerable<string> strs, string separate = ", ") => string.Join(separate, strs);

        /// <summary>
        /// 字符串转时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value)
        {
            DateTime.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// 字符串转Guid
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string s)
        {
            return Guid.Parse(s);
        }

        /// <summary>
        /// 根据正则替换
        /// </summary>
        /// <param name="input"></param>
        /// <param name="regex">正则表达式</param>
        /// <param name="replacement">新内容</param>
        /// <returns></returns>
        public static string Replace(this string input, Regex regex, string replacement)
        {
            return regex.Replace(input, replacement);
        }

        /// <summary>
        /// 生成唯一短字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chars">可用字符数数量，0-9,a-z,A-Z</param>
        /// <returns></returns>
        public static string CreateShortToken(this string str, byte chars = 36)
        {
            var nf = new NumberFormater(chars);
            return nf.ToString((DateTime.Now.Ticks - 630822816000000000) * 100 + Stopwatch.GetTimestamp() % 100);
        }

        /// <summary>
        /// 任意进制转十进制
        /// </summary>
        /// <param name="str"></param>
        /// <param name="newBase">进制</param>
        /// <returns></returns>
        public static long FromBinary(this string str, byte newBase)
        {
            var nf = new NumberFormater(newBase);
            return nf.FromString(str);
        }

        /// <summary>
        /// 任意进制转大数十进制
        /// </summary>
        /// <param name="str"></param>
        /// <param name="newBase">进制</param>
        /// <returns></returns>
        public static BigInteger FromBinaryBig(this string str, byte newBase)
        {
            var nf = new NumberFormater(newBase);
            return nf.FromStringBig(str);
        }

        #region 检测字符串中是否包含列表中的关键词

        /// <summary>
        /// 检测字符串中是否包含列表中的关键词
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="keys">关键词列表</param>
        /// <param name="ignoreCase">忽略大小写</param>
        /// <returns></returns>
        public static bool Contains(this string s, IEnumerable<string> keys, bool ignoreCase = true)
        {
            if (!keys.Any() || string.IsNullOrEmpty(s))
            {
                return false;
            }
            if (ignoreCase)
            {
                return Regex.IsMatch(s, string.Join("|", keys.Select(Regex.Escape)), RegexOptions.IgnoreCase);
            }
            return Regex.IsMatch(s, string.Join("|", keys.Select(Regex.Escape)));
        }

        /// <summary>
        /// 检测字符串中是否以列表中的关键词结尾
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="keys">关键词列表</param>
        /// <param name="ignoreCase">忽略大小写</param>
        /// <returns></returns>
        public static bool EndsWith(this string s, string[] keys, bool ignoreCase = true)
        {
            if (keys.Length == 0 || string.IsNullOrEmpty(s))
            {
                return false;
            }
            return ignoreCase ? keys.Any(key => s.EndsWith(key, StringComparison.CurrentCultureIgnoreCase)) : keys.Any(s.EndsWith);
        }

        /// <summary>
        /// 检测字符串中是否包含列表中的关键词
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="regex">关键词列表</param>
        /// <param name="ignoreCase">忽略大小写</param>
        /// <returns></returns>
        public static bool RegexMatch(this string s, string regex, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(regex) || string.IsNullOrEmpty(s))
            {
                return false;
            }
            if (ignoreCase)
            {
                return Regex.IsMatch(s, regex, RegexOptions.IgnoreCase);
            }
            return Regex.IsMatch(s, regex);
        }

        /// <summary>
        /// 检测字符串中是否包含列表中的关键词
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="regex">关键词列表</param>
        /// <returns></returns>
        public static bool RegexMatch(this string s, Regex regex) => !string.IsNullOrEmpty(s) && regex.IsMatch(s);

        /// <summary>
        /// 判断是否包含符号
        /// </summary>
        /// <param name="str"></param>
        /// <param name="symbols"></param>
        /// <returns></returns>
        public static bool ContainsSymbol(this string str, params string[] symbols)
        {
            if (str == null || string.IsNullOrEmpty(str) || str == string.Empty)
            {
                return false;
            }
            return symbols.Any(t => str.Contains(t));
        }

        #endregion 检测字符串中是否包含列表中的关键词

        /// <summary>
        /// 判断字符串是否为空或""
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 字符串掩码
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="mask">掩码符</param>
        /// <returns></returns>
        public static string Mask(this string s, char mask = '*')
        {
            if (string.IsNullOrWhiteSpace(s?.Trim()))
            {
                return s;
            }
            s = s.Trim();
            var masks = mask.ToString().PadLeft(4, mask);
            var sLen = s.Length;
            if (sLen >= 11)
            {
                return Regex.Replace(s, "(.{3}).*(.{4})", $"$1{masks}$2");
            }
            switch (sLen)
            {
                case 10:
                    return Regex.Replace(s, "(.{3}).*(.{3})", $"$1{masks}$2");
                case 9:
                    return Regex.Replace(s, "(.{2}).*(.{3})", $"$1{masks}$2");
                case 8:
                    return Regex.Replace(s, "(.{2}).*(.{2})", $"$1{masks}$2");
                case 7:
                    return Regex.Replace(s, "(.{1}).*(.{2})", $"$1{masks}$2");
                case 6:
                    return Regex.Replace(s, "(.{1}).*(.{1})", $"$1{masks}$2");
                default:
                    return Regex.Replace(s, "(.{1}).*", $"$1{masks}");
            }
        }

        public class EmailMatchModel
        {
            public bool IsMatch { get; set; }

            public Match Match { get; set; }
        }

        /// <summary>
        /// 匹配Email
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="valid">是否验证有效性</param>
        /// <returns>匹配对象；是否匹配成功，若返回true，则会得到一个Match对象，否则为null</returns>
        public static EmailMatchModel MatchEmail(this string s, bool valid = false)
        {
            if (string.IsNullOrEmpty(s) || s.Length < 7)
            {
                return new EmailMatchModel{IsMatch = false, Match=null};
            }
            var match = Regex.Match(s, @"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+");
            var isMatch = match.Success;
            if (isMatch && valid)
            {
                //var nslookup = new LookupClient();
                //var task = nslookup.Query(s.Split('@')[1], QueryType.MX).Answers.MxRecords().SelectAsync(r => Dns.GetHostAddressesAsync(r.Exchange.Value).ContinueWith(t =>
                //{
                //    if (t.IsCanceled || t.IsFaulted)
                //    {
                //        return new[] { IPAddress.Loopback };
                //    }
                //    return t.Result;
                //}));
                //isMatch = task.Result.SelectMany(a => a).Any(ip => !ip.IsPrivateIP());
            }
            return new EmailMatchModel{IsMatch = isMatch, Match=match};
        }

#if !NETFRAMEWORK

        /// <summary>
        /// 匹配Email
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="valid">是否验证有效性</param>
        /// <returns>匹配对象；是否匹配成功，若返回true，则会得到一个Match对象，否则为null</returns>
        public static async Task<(bool isMatch, Match match)> MatchEmailAsync(this string s, bool valid = false)
        {
            if (string.IsNullOrEmpty(s) || s.Length < 7)
            {
                return (false, null);
            }
            var match = Regex.Match(s, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            var isMatch = match.Success;
            if (isMatch && valid)
            {
                //var nslookup = new LookupClient();
                //var query = await nslookup.QueryAsync(s.Split('@')[1], QueryType.MX);
                //var result = await query.Answers.MxRecords().SelectAsync(r => Dns.GetHostAddressesAsync(r.Exchange.Value).ContinueWith(t =>
                //{
                //    if (t.IsCanceled || t.IsFaulted)
                //    {
                //        return new[] { IPAddress.Loopback };
                //    }
                //    return t.Result;
                //}));
                //isMatch = result.SelectMany(a => a).Any(ip => !ip.IsPrivateIP());
            }
            return (isMatch, match);
        }

#endif

        /// <summary>
        /// 邮箱掩码
        /// </summary>
        /// <param name="s">邮箱</param>
        /// <param name="mask">掩码</param>
        /// <returns></returns>
        public static string MaskEmail(this string s, char mask = '*')
        {
            var index = s.LastIndexOf("@");
            var oldValue = s.Substring(0, index);
            return !MatchEmail(s).IsMatch ? s : s.Replace(oldValue, Mask(oldValue, mask));
        }

        /// <summary>
        /// 匹配完整格式的URL
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="isMatch">是否匹配成功，若返回true，则会得到一个Match对象，否则为null</param>
        /// <returns>匹配对象</returns>
        public static Uri MatchUrl(this string s, out bool isMatch)
        {
            try
            {
                var uri = new Uri(s);
                isMatch = Dns.GetHostAddresses(uri.Host).Any(ip => !ip.IsPrivateIP());
                return uri;
            }
            catch
            {
                isMatch = false;
                return null;
            }
        }

        /// <summary>
        /// 匹配完整格式的URL
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>是否匹配成功</returns>
        public static bool MatchUrl(this string s)
        {
            MatchUrl(s, out var isMatch);
            return isMatch;
        }

        /// <summary>
        /// 根据GB11643-1999标准权威校验中国身份证号码的合法性
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>是否匹配成功</returns>
        public static bool MatchIdentifyCard(this string s)
        {
            var address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (s.Length == 18)
            {
                if (long.TryParse(s.Remove(17), out var n) == false || n < Math.Pow(10, 16) || long.TryParse(s.Replace('x', '0').Replace('X', '0'), out n) == false)
                {
                    return false; //数字验证
                }
                if (address.IndexOf(s.Remove(2), StringComparison.Ordinal) == -1)
                {
                    return false; //省份验证
                }
                var birth = s.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                if (!DateTime.TryParse(birth, out _))
                {
                    return false; //生日验证
                }
                var arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                var wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                var ai = s.Remove(17).ToCharArray();
                var sum = 0;
                for (var i = 0; i < 17; i++)
                {
                    sum += wi[i].ToInt32() * ai[i].ToString().ToInt32();
                }
                Math.DivRem(sum, 11, out var y);
                return arrVarifyCode[y] == s.Substring(17, 1).ToLower();
            }
            if (s.Length == 15)
            {
                if (long.TryParse(s, out var n) == false || n < Math.Pow(10, 14))
                {
                    return false; //数字验证
                }
                if (address.IndexOf(s.Remove(2), StringComparison.Ordinal) == -1)
                {
                    return false; //省份验证
                }
                var birth = s.Substring(6, 6).Insert(4, "-").Insert(2, "-");
                return DateTime.TryParse(birth, out _);
            }
            return false;
        }

        /// <summary>
        /// 校验IP地址的正确性，同时支持IPv4和IPv6
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="isMatch">是否匹配成功，若返回true，则会得到一个Match对象，否则为null</param>
        /// <returns>匹配对象</returns>
        public static IPAddress MatchInetAddress(this string s, out bool isMatch)
        {
            isMatch = IPAddress.TryParse(s, out var ip);
            return ip;
        }

        /// <summary>
        /// 校验IP地址的正确性，同时支持IPv4和IPv6
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>是否匹配成功</returns>
        public static bool MatchInetAddress(this string s)
        {
            MatchInetAddress(s, out var success);
            return success;
        }

        /// <summary>
        /// IP地址转换成数字
        /// </summary>
        /// <param name="addr">IP地址</param>
        /// <returns>数字,输入无效IP地址返回0</returns>
        public static uint IPToID(this string addr)
        {
            if (!IPAddress.TryParse(addr, out var ip))
            {
                return 0;
            }
            var bInt = ip.GetAddressBytes();
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bInt);
            }
            return BitConverter.ToUInt32(bInt, 0);
        }

        /// <summary>
        /// 判断IP是否是私有地址
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsPrivateIP(this string ip)
        {
            if (MatchInetAddress(ip))
            {
                return IPAddress.Parse(ip).IsPrivateIP();
            }
            throw new ArgumentException(ip + "不是一个合法的ip地址");
        }

        /// <summary>
        /// 判断IP地址在不在某个IP地址段
        /// </summary>
        /// <param name="input">需要判断的IP地址</param>
        /// <param name="begin">起始地址</param>
        /// <param name="ends">结束地址</param>
        /// <returns></returns>
        public static bool IpAddressInRange(this string input, string begin, string ends)
        {
            uint current = input.IPToID();
            return current >= begin.IPToID() && current <= ends.IPToID();
        }

        /// <summary>
        /// 匹配手机号码
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="isMatch">是否匹配成功，若返回true，则会得到一个Match对象，否则为null</param>
        /// <returns>匹配对象</returns>
        public static Match MatchPhoneNumber(this string s, out bool isMatch)
        {
            if (string.IsNullOrEmpty(s))
            {
                isMatch = false;
                return null;
            }
            var match = Regex.Match(s, @"^((1[3,5,6,8][0-9])|(14[5,7])|(17[0,1,3,6,7,8])|(19[8,9]))\d{8}$");
            isMatch = match.Success;
            return isMatch ? match : null;
        }

        /// <summary>
        /// 匹配手机号码
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>是否匹配成功</returns>
        public static bool MatchPhoneNumber(this string s)
        {
            MatchPhoneNumber(s, out bool success);
            return success;
        }

        /// <summary>
        /// 判断url是否是外部地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsExternalAddress(this string url)
        {
            var uri = new Uri(url);
            switch (uri.HostNameType)
            {
                case UriHostNameType.Dns:
                    var ipHostEntry = Dns.GetHostEntry(uri.DnsSafeHost);
                    if (ipHostEntry.AddressList.Where(ipAddress => ipAddress.AddressFamily == AddressFamily.InterNetwork).Any(ipAddress => !ipAddress.IsPrivateIP()))
                    {
                        return true;
                    }
                    break;
                case UriHostNameType.IPv4:
                    return !IPAddress.Parse(uri.DnsSafeHost).IsPrivateIP();
            }
            return false;
        }

        /// <summary>
        /// 转换成字节数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string @this)
        {
            return Encoding.ASCII.GetBytes(@this);
        }

        /// <summary>
        /// 中国专利申请号（授权以后就是专利号）由两种组成
        /// 2003年9月30号以前的9位（不带校验位是8号），校验位之前可能还会有一个点，例如：00262311, 002623110 或 00262311.0
        /// 2003年10月1号以后的13位（不带校验位是12号），校验位之前可能还会有一个点，例如：200410018477, 2004100184779 或200410018477.9
        /// http://www.sipo.gov.cn/docs/pub/old/wxfw/zlwxxxggfw/hlwzljsxt/hlwzljsxtsyzn/201507/P020150713610193194682.pdf
        /// 上面的文档中均不包括校验算法，但是下面的校验算法没有问题
        /// </summary>
        /// <param name="patnum">源字符串</param>
        /// <returns>是否匹配成功</returns>
        public static bool MatchCNPatentNumber(this string patnum)
        {
            var patnumWithCheckbitPattern = new Regex(@"^
(?<!\d)
(?<patentnum>
    (?<basenum>
        (?<year>(?<old>8[5-9]|9[0-9]|0[0-3])|(?<new>[2-9]\d{3}))
        (?<sn>
            (?<patenttype>[12389])
            (?(old)\d{5}|(?(new)\d{7}))
        )
    )
    (?:
    \.?
    (?<checkbit>[0-9X])
    )?
)
(?!\d)
$", RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var m = patnumWithCheckbitPattern.Match(patnum);
            if (!m.Success)
            {
                return false;
            }
            var isPatnumTrue = true;
            patnum = patnum.ToUpper().Replace(".", "");
            if (patnum.Length == 9 || patnum.Length == 8)
            {
                var factors8 = new byte[8] { 2, 3, 4, 5, 6, 7, 8, 9 };
                int year = Convert.ToUInt16(patnum.Substring(0, 2));
                year += (year >= 85) ? (ushort)1900u : (ushort)2000u;
                if (year >= 1985 || year <= 2003)
                {
                    var sum = 0;
                    for (byte i = 0; i < 8; i++)
                    {
                        sum += factors8[i] * (patnum[i] - '0');
                    }
                    var checkbit = "0123456789X"[sum % 11];
                    if (patnum.Length == 9)
                    {
                        if (checkbit != patnum[8])
                        {
                            isPatnumTrue = false;
                        }
                    }
                    else
                    {
                        patnum += checkbit;
                    }
                }
                else
                {
                    isPatnumTrue = false;
                }
            }
            else if (patnum.Length == 13 || patnum.Length == 12)
            {
                var factors12 = new byte[12] { 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5 };
                int year = Convert.ToUInt16(patnum.Substring(0, 4));
                if (year >= 2003 && year <= DateTime.Now.Year)
                {
                    var sum = 0;
                    for (byte i = 0; i < 12; i++)
                    {
                        sum += factors12[i] * (patnum[i] - '0');
                    }
                    var checkbit = "0123456789X"[sum % 11];
                    if (patnum.Length == 13)
                    {
                        if (checkbit != patnum[12])
                        {
                            isPatnumTrue = false;
                        }
                    }
                    else
                    {
                        patnum += checkbit;
                    }
                }
                else
                {
                    isPatnumTrue = false;
                }
            }
            else
            {
                isPatnumTrue = false;
            }
            return isPatnumTrue;
        }

        /// <summary>
        /// 取字符串前{length}个字
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Take(this string s, int length)
        {
            return s.Length > length ? s.Substring(0, length) : s;
        }

        #region 网页抓取

        /// <summary>
        /// 同步获取网页内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGetString(this string url)
        {
            var httpHelper = new HttpHelper();
            var httpItem = new HttpItem
            {
                URL = url
            };
            var html = "";
            try
            {
                var result = httpHelper.GetHtml(httpItem);
                html = result.StatusCode == HttpStatusCode.OK ? result.Html : result.StatusDescription;
            }
            catch (Exception e)
            {
                html = e.Message;
            }
            return html;
        }

        /// <summary>
        /// 同步获取网页内容转为Json
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T HttpGetJson<T>(this string url)
        {
            return url.HttpGetString().JsonDeserialize<T>();
        }

        /// <summary>
        /// 异步获取网页内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> HttpGetStringAsync(this string url)
        {
            return await Task.Run(url.HttpGetString);
        }

        /// <summary>
        /// 同步获取网页内容转为Json
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<T> HttpGetJsonAsync<T>(this string url)
        {
            return (await url.HttpGetStringAsync()).JsonDeserialize<T>();
        }

        #endregion
    }
}