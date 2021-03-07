using System;
using System.IO;
using AiSoft.Tools.Extensions;

namespace AiSoft.Tools.Helpers
{
    public class JsonConfigHelper
    {
        /// <summary>
        /// 读取Json
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T ReadJson<T>(string filePath) where T : new()
        {
            try
            {
                return File.ReadAllText(filePath).JsonDeserialize<T>();
            }
            catch
            {
            }
            return default;
        }

        /// <summary>
        /// 写入Json
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="jsonObj"></param>
        public static void WriteJson<T>(string filePath, T jsonObj)
        {
            try
            {
                File.WriteAllText(filePath, jsonObj.JsonSerialize(true));
            }
            catch
            {
            }
        }
    }
}