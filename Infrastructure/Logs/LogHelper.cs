using System;
using System.Web;
using Logs.Log4netExt;

namespace Logs
{
    /// <summary>
    /// 使用开源日志库
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 记录完整错误信息
        /// by:willian date:2016-9-28
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        /// <param name="msg"></param>
        /// <param name="loginId"></param>
        /// <param name="loginUserName"></param>
        public static void WriteError(Exception ex, Type t, string msg = "", int loginId = 0, string loginUserName = "")
        {
            var ip = GetIp();
            var requestUrl = GetNowPage();
            var webLog = WebLogHelper.GetLogger(t);
            webLog.Error(ip, requestUrl, msg, ex, loginId, loginUserName);
        }

        /// <summary>
        /// 记录完整错误信息
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        /// <param name="loginId"></param>
        /// <param name="loginUserName"></param>
        public static void WriteError(Exception ex, string msg = "", string type = "loginfo", int loginId = 0, string loginUserName = "")
        {
            var ip = GetIp();
            var requestUrl = GetNowPage();
            var webLog = WebLogHelper.GetLogger(type);
            webLog.Error(ip, requestUrl, msg, ex, loginId, loginUserName);
        }

        /// <summary>
        /// 记录完整错误信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        /// <param name="loginId"></param>
        /// <param name="loginUserName"></param>
        public static void WriteError(string msg = "", string type = "loginfo", int loginId = 0, string loginUserName = "")
        {
            var ip = GetIp();
            var requestUrl = GetNowPage();
            var webLog = WebLogHelper.GetLogger(type);
            webLog.Error(ip, requestUrl, msg, loginId, loginUserName);
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        /// <param name="loginId"></param>
        /// <param name="loginUserName"></param>
        public static void WriteWarn(Exception ex, string msg = "", string type = "loginfo", int loginId = 0, string loginUserName = "")
        {
            var ip = GetIp();
            var requestUrl = GetNowPage();
            var webLog = WebLogHelper.GetLogger(type);
            webLog.Warn(ip, requestUrl, msg, ex, loginId, loginUserName);
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        /// <param name="loginId"></param>
        /// <param name="loginUserName"></param>
        public static void WriteWarn(string msg = "", string type = "loginfo", int loginId = 0, string loginUserName = "")
        {
            var ip = GetIp();
            var requestUrl = GetNowPage();
            var webLog = WebLogHelper.GetLogger(type);
            webLog.Warn(ip, requestUrl, msg, loginId, loginUserName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        /// <param name="loginId"></param>
        /// <param name="loginUserName"></param>
        public static void WriteInfo(Exception ex, string msg = "", string type = "loginfo", int loginId = 0, string loginUserName = "")
        {
            var ip = GetIp();
            var requestUrl = GetNowPage();
            var webLog = WebLogHelper.GetLogger(type);
            webLog.Info(ip, requestUrl, msg, ex, loginId, loginUserName);
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="type"></param>
        /// <param name="loginId"></param>
        /// <param name="loginUserName"></param>
        public static void WriteInfo(string info, string type = "loginfo", int loginId = 0, string loginUserName = "")
        {
            var ip = GetIp();
            var requestUrl = GetNowPage();
            var webLog = WebLogHelper.GetLogger(type);
            webLog.Info(ip, requestUrl, info, loginId, loginUserName);
        }

        #region 辅助方法
        /// <summary>  
        /// 获取客户端IP地址  
        /// </summary>  
        /// <returns></returns>  
        private static string GetIp()
        {
            if (HttpContext.Current == null)
                return "0.0.0.0";

            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(result))
            {
                return "0.0.0.0";
            }
            return result;
        }
        /// <summary>
        /// 获取当前页
        /// </summary>
        /// <returns></returns>
        public static string GetNowPage()
        {
            if (HttpContext.Current == null)
                return "0.0.0.0";

            return HttpContext.Current.Request.Url.ToString();
        }
        #endregion
    }
}
