﻿using System;
using AiSoft.Tools.Strings;

namespace AiSoft.Tools.Extensions
{
    public static class LongExtension
    {
        /// <summary>
        /// 十进制转任意进制
        /// </summary>
        /// <param name="num"></param>
        /// <param name="newBase">进制</param>
        /// <returns></returns>
        public static string ToBase(this long num, byte newBase)
        {
            var nf = new NumberFormater(newBase);
            return nf.ToString(num);
        }

        /// <summary>
        /// 转换成字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this long value)
        {
            return BitConverter.GetBytes(value);
        }
    }
}