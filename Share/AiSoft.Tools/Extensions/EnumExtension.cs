using System;
using AiSoft.Tools.Helpers;

namespace AiSoft.Tools.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum em)
        {
            return EnumHelper.GetDescription(em);
        }
    }
}