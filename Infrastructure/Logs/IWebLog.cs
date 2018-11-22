using log4net;
using System;

namespace Logs
{
    /// <summary>
    /// 日志模块需要继承该接口才能使用
    /// </summary>
    public interface IWebLog : ILog
    {
        void Info(string clientIp, string requestUrl, object message, int loginId = 0, string loginUserName = "");
        void Info(string clientIp, string requestUrl, object message, Exception t, int loginId = 0, string loginUserName = "");
        void Warn(string clientIp, string requestUrl, object message, int loginId = 0, string loginUserName = "");
        void Warn(string clientIp, string requestUrl, object message, Exception t, int loginId = 0, string loginUserName = "");
        void Error(string clientIp, string requestUrl, object message, int loginId = 0, string loginUserName = "");
        void Error(string clientIp, string requestUrl, object message, Exception t, int loginId = 0, string loginUserName = "");
        void Fatal(string clientIp, string requestUrl, object message, int loginId = 0, string loginUserName = "");
        void Fatal(string clientIp, string requestUrl, object message, Exception t, int loginId = 0, string loginUserName = "");
    }
}
