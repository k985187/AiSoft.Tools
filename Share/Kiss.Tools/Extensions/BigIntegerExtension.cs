using System;
using System.Numerics;
using Kiss.Tools.Strings;

namespace Kiss.Tools.Extensions
{
    public static class BigIntegerExtension
    {
        /// <summary>
        /// 十进制转任意进制
        /// </summary>
        /// <param name="num"></param>
        /// <param name="bin">进制</param>
        /// <returns></returns>
        public static string ToBinary(this BigInteger num, int bin)
        {
            var nf = new NumberFormater(bin);
            return nf.ToString(num);
        }
    }
}