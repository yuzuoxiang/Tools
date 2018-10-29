using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs.Log4netExt
{
    /// <summary>
    /// 日志模块需要继承该接口才能使用
    /// </summary>
    public interface IWebLog : ILog
    {
        void Info(string clientIP, string requestUrl, object message, int loginId = 0, string loginUserName = "");
        void Info(string clientIP, string requestUrl, object message, Exception t, int loginId = 0, string loginUserName = "");
        void Warn(string clientIP, string requestUrl, object message, int loginId = 0, string loginUserName = "");
        void Warn(string clientIP, string requestUrl, object message, Exception t, int loginId = 0, string loginUserName = "");
        void Error(string clientIP, string requestUrl, object message, int loginId = 0, string loginUserName = "");
        void Error(string clientIP, string requestUrl, object message, Exception t, int loginId = 0, string loginUserName = "");
        void Fatal(string clientIP, string requestUrl, object message, int loginId = 0, string loginUserName = "");
        void Fatal(string clientIP, string requestUrl, object message, Exception t, int loginId = 0, string loginUserName = "");
    }
}
