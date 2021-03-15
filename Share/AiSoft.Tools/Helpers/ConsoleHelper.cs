using System;

namespace AiSoft.Tools.Helpers
{
    public class ConsoleHelper
    {
        /// <summary>
        /// 控制台输出
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="color"></param>
        private static void WriteColorLine(object obj, ConsoleColor color)
        {
            var currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}：{obj}");
            Console.ForegroundColor = currentForeColor;
        }

        /// <summary>
        /// 打印错误信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="color"></param>
        public static void WriteErrorLine(object obj, ConsoleColor color = ConsoleColor.Red)
        {
            WriteColorLine(obj, color);
        }

        /// <summary>
        /// 打印警告信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="color"></param>
        public static void WriteWarningLine(object obj, ConsoleColor color = ConsoleColor.Yellow)
        {
            WriteColorLine(obj, color);
        }

        /// <summary>
        /// 打印正常信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="color"></param>
        public static void WriteInfoLine(object obj, ConsoleColor color = ConsoleColor.White)
        {
            WriteColorLine(obj, color);
        }

        /// <summary>
        /// 打印成功的信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="color"></param>
        public static void WriteSuccessLine(object obj, ConsoleColor color = ConsoleColor.Green)
        {
            WriteColorLine(obj, color);
        }
    }
}