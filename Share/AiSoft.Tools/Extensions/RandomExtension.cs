using System;
using System.Diagnostics;

namespace AiSoft.Tools.Extensions
{
    public static class RandomExtension
    {
        /// <summary>
        /// 生成真正的随机数
        /// </summary>
        /// <param name="r"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int StrictNext(this Random r, int maxValue = int.MaxValue)
        {
            return new Random((int)Stopwatch.GetTimestamp()).Next(maxValue);
        }

        /// <summary>
        /// 生成真正的随机数
        /// </summary>
        /// <param name="r"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int StrictNext(this Random r, int minValue = 0, int maxValue = int.MaxValue)
        {
            return new Random((int)Stopwatch.GetTimestamp()).Next(minValue, maxValue);
        }

        /// <summary>
        /// 产生正态分布的随机数
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="mean">均值</param>
        /// <param name="stdDev">方差</param>
        /// <returns></returns>
        public static double NextGauss(this Random rand, double mean, double stdDev)
        {
            var u1 = 1.0 - rand.NextDouble();
            var u2 = 1.0 - rand.NextDouble();
            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return mean + stdDev * randStdNormal;
        }
    }
}