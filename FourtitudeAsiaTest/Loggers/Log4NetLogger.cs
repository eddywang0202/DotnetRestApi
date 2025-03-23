using log4net;

namespace FourtitudeAsiaTest.Loggers
{
    public class Log4NetLogger<T> : IAppLogger<T>
    {
        private readonly ILog _log;

        public Log4NetLogger()
        {
            _log = LogManager.GetLogger(typeof(T));
        }

        public void LogInfo(string message)
        {
            _log.Info(message);
        }

        public void LogDebug(string message)
        {
            _log.Debug(message);
        }

        public void LogWarning(string message)
        {
            _log.Warn(message);
        }

        public void LogError(string message, Exception ex)
        {
            _log.Error(message, ex);
        }
    }
}
