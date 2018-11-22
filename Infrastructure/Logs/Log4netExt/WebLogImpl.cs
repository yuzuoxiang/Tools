using System;
using log4net.Core;

namespace Logs.Log4netExt
{
    public class WebLogImpl : LogImpl, IWebLog
    {
        /// <summary>
        /// The fully qualified name of this declaring type not the type of any subclass.
        /// </summary>
        private static readonly Type ThisDeclaringType = typeof(WebLogImpl);

        public WebLogImpl(ILogger logger)
            : base(logger)
        {
        }

        #region Implementation of IWebLog

        public void Info(string clientIp, string requestUrl, object message, int loginId = 0, string loginUserName = "")
        {
            Info(clientIp, requestUrl, message, null, loginId, loginUserName);
        }

        public void Info(string clientIp, string requestUrl, object message, Exception t, int loginId = 0, string loginUserName = "")
        {
            if (!IsInfoEnabled) return;
            var loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, message, t);
            loggingEvent.Properties["clientIP"] = clientIp;
            loggingEvent.Properties["requestUrl"] = requestUrl;
            loggingEvent.Properties["loginUserId"] = loginId;
            loggingEvent.Properties["loginUserName"] = loginUserName;
            Logger.Log(loggingEvent);
        }

        public void Warn(string clientIp, string requestUrl, object message, int loginId = 0, string loginUserName = "")
        {
            Warn(clientIp, requestUrl, message, null, loginId, loginUserName);
        }

        public void Warn(string clientIp, string requestUrl, object message, Exception t, int loginId = 0, string loginUserName = "")
        {
            if (!IsWarnEnabled) return;
            var loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Warn, message, t);
            loggingEvent.Properties["clientIP"] = clientIp;
            loggingEvent.Properties["requestUrl"] = requestUrl;
            loggingEvent.Properties["loginUserId"] = loginId;
            loggingEvent.Properties["loginUserName"] = loginUserName;
            Logger.Log(loggingEvent);
        }

        public void Error(string clientIp, string requestUrl, object message, int loginId = 0, string loginUserName = "")
        {
            Error(clientIp, requestUrl, message, null, loginId, loginUserName);
        }

        public void Error(string clientIp, string requestUrl, object message, Exception t, int loginId = 0, string loginUserName = "")
        {
            if (!IsErrorEnabled) return;
            var loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Error, message, t);
            loggingEvent.Properties["clientIP"] = clientIp;
            loggingEvent.Properties["requestUrl"] = requestUrl;
            loggingEvent.Properties["loginUserId"] = loginId;
            loggingEvent.Properties["loginUserName"] = loginUserName;
            Logger.Log(loggingEvent);
        }

        public void Fatal(string clientIp, string requestUrl, object message, int loginId = 0, string loginUserName = "")
        {
            Fatal(clientIp, requestUrl, message, null, loginId, loginUserName);
        }

        public void Fatal(string clientIp, string requestUrl, object message, Exception t, int loginId = 0, string loginUserName = "")
        {
            if (!IsFatalEnabled) return;
            var loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Fatal, message, t);
            loggingEvent.Properties["clientIP"] = clientIp;
            loggingEvent.Properties["requestUrl"] = requestUrl;
            loggingEvent.Properties["loginUserId"] = loginId;
            loggingEvent.Properties["loginUserName"] = loginUserName;
            Logger.Log(loggingEvent);
        }

        #endregion Implementation of IWebLog
    }
}
