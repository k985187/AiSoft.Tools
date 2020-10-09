using System;
using System.Diagnostics;

namespace Kiss.Tools.DateTimeExt
{
    /// <summary>
    /// 日期操作工具类
    /// </summary>
    public static class DateUtil
    {
        /// <summary>
        /// 返回相对于当前时间的相对天数
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="relativeday">相对天数</param>
        public static string GetDateTime(this DateTime dt, int relativeday)
        {
            return dt.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTimeF(this DateTime dt) => dt.ToString("yyyy-MM-dd HH:mm:ss:fffffff");

        /// <summary>
        /// 返回标准时间 
        /// </summary>
        /// <param name="fDateTime">日期时间字符串</param>
        /// <param name="formatStr">格式</param>
        public static string GetStandardDateTime(this string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
            {
                return fDateTime;
            }

            var s = Convert.ToDateTime(fDateTime);
            return s.ToString(formatStr);
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="fDateTime">日期时间字符串</param>
        public static string GetStandardDateTime(this string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

#if !NET45

        /// <summary>
        /// 获取该时间相对于1970-01-01 00:00:00的秒数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static double GetTotalSeconds(this DateTime dt) => new DateTimeOffset(dt).ToUnixTimeSeconds();

        /// <summary>
        /// 获取该时间相对于1970-01-01 00:00:00的毫秒数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static double GetTotalMilliseconds(this DateTime dt) => new DateTimeOffset(dt).ToUnixTimeMilliseconds();

#endif

        /// <summary>
        /// 获取该时间相对于1970-01-01 00:00:00的微秒时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long GetTotalMicroseconds(this DateTime dt) => new DateTimeOffset(dt).Ticks / 10;

        /// <summary>
        /// 获取该时间相对于1970-01-01 00:00:00的纳秒时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long GetTotalNanoseconds(this DateTime dt) => new DateTimeOffset(dt).Ticks * 100 + Stopwatch.GetTimestamp() % 100;

        /// <summary>
        /// 获取该时间相对于1970-01-01 00:00:00的分钟数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static double GetTotalMinutes(this DateTime dt) => new DateTimeOffset(dt).Offset.TotalMinutes;

        /// <summary>
        /// 获取该时间相对于1970-01-01 00:00:00的小时数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static double GetTotalHours(this DateTime dt) => new DateTimeOffset(dt).Offset.TotalHours;

        /// <summary>
        /// 获取该时间相对于1970-01-01 00:00:00的天数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static double GetTotalDays(this DateTime dt) => new DateTimeOffset(dt).Offset.TotalDays;

        /// <summary>
        /// 返回本年有多少天
        /// </summary>
        /// <param name="_"></param>
        /// <param name="iYear">年份</param>
        /// <returns>本年的天数</returns>
        public static int GetDaysOfYear(this DateTime _, int iYear)
        {
            return IsRuYear(iYear) ? 366 : 365;
        }

        /// <summary>本年有多少天</summary>
        /// <param name="dt">日期</param>
        /// <returns>本天在当年的天数</returns>
        public static int GetDaysOfYear(this DateTime dt)
        {
            //取得传入参数的年份部分，用来判断是否是闰年
            int n = dt.Year;
            return IsRuYear(n) ? 366 : 365;
        }

        /// <summary>本月有多少天</summary>
        /// <param name="_"></param>
        /// <param name="iYear">年</param>
        /// <param name="month">月</param>
        /// <returns>天数</returns>
        public static int GetDaysOfMonth(this DateTime _, int iYear, int month)
        {
            switch (month)
            {
                case 1:
                    return 31;
                case 2:
                    return (IsRuYear(iYear) ? 29 : 28);
                case 3:
                    return 31;
                case 4:
                    return 30;
                case 5:
                    return 31;
                case 6:
                    return 30;
                case 7:
                case 8:
                    return 31;
                case 9:
                    return 30;
                case 10:
                    return 31;
                case 11:
                    return 30;
                case 12:
                    return 31;
                default:
                    return 0;
            }
        }

        /// <summary>本月有多少天</summary>
        /// <param name="dt">日期</param>
        /// <returns>天数</returns>
        public static int GetDaysOfMonth(this DateTime dt)
        {
            //--利用年月信息，得到当前月的天数信息。
            switch (dt.Month)
            {
                case 1:
                    return 31;
                case 2:
                    return (IsRuYear(dt.Year) ? 29 : 28);
                case 3:
                    return 31;
                case 4:
                    return 30;
                case 5:
                    return 31;
                case 6:
                    return 30;
                case 7:
                case 8:
                    return 31;
                case 9:
                    return 30;
                case 10:
                    return 31;
                case 11:
                    return 30;
                case 12:
                    return 31;
                default:
                    return 0;
            }
        }

        /// <summary>返回当前日期的星期名称</summary>
        /// <param name="idt">日期</param>
        /// <returns>星期名称</returns>
        public static string GetWeekNameOfDay(this DateTime idt)
        {
            switch (idt.DayOfWeek.ToString())
            {
                case "Mondy":
                    return "星期一";
                case "Tuesday":
                    return "星期二";
                case "Wednesday":
                    return "星期三";
                case "Thursday":
                    return "星期四";
                case "Friday":
                    return "星期五";
                case "Saturday":
                    return "星期六";
                case "Sunday":
                    return "星期日";
                default:
                    return "";
            }
        }

        /// <summary>返回当前日期的星期编号</summary>
        /// <param name="idt">日期</param>
        /// <returns>星期数字编号</returns>
        public static string GetWeekNumberOfDay(this DateTime idt)
        {
            switch (idt.DayOfWeek.ToString())
            {
                case "Mondy":
                    return "1";
                case "Tuesday":
                    return "2";
                case "Wednesday":
                    return "3";
                case "Thursday":
                    return "4";
                case "Friday":
                    return "5";
                case "Saturday":
                    return "6";
                case "Sunday":
                    return "7";
                default:
                    return "";
            }
        }

        /// <summary>判断当前年份是否是闰年，私有函数</summary>
        /// <param name="iYear">年份</param>
        /// <returns>是闰年：True ，不是闰年：False</returns>
        private static bool IsRuYear(int iYear)
        {
            //形式参数为年份
            //例如：2003
            var n = iYear;
            return n % 400 == 0 || n % 4 == 0 && n % 100 != 0;
        }

        /// <summary>
        /// 判断是否为合法日期，必须大于1800年1月1日
        /// </summary>
        /// <param name="strDate">输入日期字符串</param>
        /// <returns>True/False</returns>
        public static bool IsDateTime(this string strDate)
        {
            DateTime.TryParse(strDate, out var result);
            return result.CompareTo(DateTime.Parse("1800-1-1")) > 0;
        }

        /// <summary>
        /// 判断时间是否在区间内
        /// </summary>
        /// <param name="this"></param>
        /// <param name="start">开始</param>
        /// <param name="end">结束</param>
        /// <param name="mode">模式</param>
        /// <returns></returns>
        public static bool In(this DateTime @this, DateTime start, DateTime end, RangeMode mode = RangeMode.Close)
        {
            switch (mode)
            {
                case RangeMode.Open:
                    return start < @this && end > @this;
                case RangeMode.Close:
                    return start <= @this && end >= @this;
                case RangeMode.OpenClose:
                    return start < @this && end >= @this;
                case RangeMode.CloseOpen:
                    return start <= @this && end > @this;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }

    /// <summary>
    /// 区间模式
    /// </summary>
    public enum RangeMode
    {
        /// <summary>
        /// 开区间
        /// </summary>
        Open,

        /// <summary>
        /// 闭区间
        /// </summary>
        Close,

        /// <summary>
        /// 左开右闭区间
        /// </summary>
        OpenClose,

        /// <summary>
        /// 左闭右开区间
        /// </summary>
        CloseOpen
    }
}
