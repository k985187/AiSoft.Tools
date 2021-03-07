using System;
using System.ComponentModel;

namespace AiSoft.Tools.Helpers
{
    public class EnumHelper
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetDescription(Enum en)
        {
            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());
            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }
    }
}