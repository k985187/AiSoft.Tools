﻿using System;
using AiSoft.Tools.Strings;

namespace AiSoft.Tools.Extensions
{
    public static class IntExtension
    {
        /// <summary>
        /// 转换成字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this int value)
        {
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        /// 十进制转任意进制
        /// </summary>
        /// <param name="num"></param>
        /// <param name="newBase">进制</param>
        /// <returns></returns>
        public static string ToBinary(this int num, byte newBase)
        {
            var nf = new NumberFormater(newBase);
            return nf.ToString(num);
        }
    }
}