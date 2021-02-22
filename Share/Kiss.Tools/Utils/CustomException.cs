using System;
using System.Threading.Tasks;
#if !NETSTANDARD
    using System.Windows.Threading;
#endif
using Kiss.Tools.Helpers;

namespace Kiss.Tools.Utils
{
    /// <summary>
    /// 自定义错误类
    /// </summary>
    public class CustomException
    {
#if !NETSTANDARD

        /// <summary>
        /// 处理UI线程中某个异常未被捕获时出现的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogHelper.WriteLog(e.Exception);
            e.Handled = true;
        }

#endif

        /// <summary>
        /// 处理非UI线程中某个异常未被捕获时出的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogHelper.WriteLog(e.ExceptionObject as Exception);
        }

        /// <summary>
        /// 非主线程错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            LogHelper.WriteLog(e.Exception);
            e.SetObserved();
        }
    }
}