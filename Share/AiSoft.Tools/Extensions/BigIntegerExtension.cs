using System;
using System.Numerics;
using AiSoft.Tools.Strings;

namespace AiSoft.Tools.Extensions
{
    public static class BigIntegerExtension
    {
        /// <summary>
        /// 十进制转任意进制
        /// </summary>
        /// <param name="num"></param>
        /// <param name="newBase">进制</param>
        /// <returns></returns>
        public static string ToBase(this BigInteger num, byte newBase)
        {
            var nf = new NumberFormater(newBase);
            return nf.ToString(num);
        }
    }
}