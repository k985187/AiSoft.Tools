using System;
using System.Diagnostics;
using System.Reflection;
using log4net;
using log4net.Repository;

namespace AiSoft.Tools.Helpers
{
    /// <summary>
    /// log4记录帮助类
    /// </summary>
    public class LogHelper
    {
        private static ILoggerRepository _loggerRepository;

        private static ILog _log;

        /// <summary>
        /// 静态初始化
        /// </summary>
        static LogHelper()
        {
            _loggerRepository = LogManager.CreateRepository("Kiss.Tools");
            var fileName = Assembly.GetExecutingAssembly().GetName().Name + ".Log4Net.config";
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName);
            log4net.Config.XmlConfigurator.Configure(_loggerRepository, stream);
            _log = LogManager.GetLogger(_loggerRepository.Name, typeof(LogHelper));
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="e"></param>
        public static void WriteLog(Exception e)
        {
            var st = new StackTrace(e, true);
            var sf = st.GetFrame(1);
            var mb = sf.GetMethod();
            var log = $"{mb.DeclaringType.FullName}:{mb}:{sf.GetFileLineNumber()}";
            if (_log.IsErrorEnabled)
            {
                _log.Error(log, e);
            }
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(string info)
        {
            if (_log.IsInfoEnabled)
            {
                var st = new StackTrace(true);
                var sf = st.GetFrame(1);
                var mb = sf.GetMethod();
                var log = $"{mb.DeclaringType.FullName}:{mb}:{sf.GetFileLineNumber()}";
                _log.Info($"{log}\r\n{info}");
            }
        }
    }
}