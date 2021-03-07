using System;
using Newtonsoft.Json;

namespace AiSoft.Tools.Extensions
{
    public static class JsonExtension
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="expended"></param>
        /// <returns></returns>
        public static string JsonSerialize(this object obj, bool expended = false)
        {
            var settings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                DateFormatString = "yyyy-MM-dd HH:mm:ss.fff"
            };
            if (expended)
            {
                settings.Formatting = Formatting.Indented;
            }
            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                DateFormatString = "yyyy-MM-dd HH:mm:ss.fff"
            });
        }
    }
}