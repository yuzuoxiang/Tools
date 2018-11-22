using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Logs
{
    /// <summary>
    /// 日志处理文件
    /// </summary>
    public class Log
    {
        //日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        private static readonly int LogLevel = int.Parse(ConfigurationManager.AppSettings["LogLevel"]);
        private static string LogPath => (HttpContext.Current != null)
            //WebForm时的路径
            ? HttpContext.Current.Request.PhysicalApplicationPath + "/logs"
            //WinForm时的路径
            : AppDomain.CurrentDomain.BaseDirectory;
        //: ConfigurationManager.AppSettings["LogPath"];
        //private static string Log_Path =  +ConfigurationManager.AppSettings["LogPath"];

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="message"></param>
        /// <param name="err"></param>
        public static void Debug(string message, string filePath = "", Exception err = null)
        {
            if (LogLevel >= 3)
                WriteLog("DEBUG", filePath, message, err);
        }

        /// <summary>
        /// 正常信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="message"></param>
        /// <param name="err"></param>
        public static void Info(string message, string filePath = "", Exception err = null)
        {
            if (LogLevel >= 2)
                WriteLog("INFO", filePath, message, err);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="message"></param>
        /// <param name="err"></param>
        public static void Error(string message, string filePath = "", Exception err = null)
        {
            if (LogLevel >= 1)
                WriteLog("ERROR", filePath, message, err);
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="level"></param>
        /// <param name="filePath"></param>
        /// <param name="message"></param>
        /// <param name="err"></param>
        public static void WriteLog(string level, string filePath, string message, Exception err)
        {
            //如果日志目录不存在就创建
            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);

            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
            var filename = LogPath + "/" + DateTime.Now.ToString("yyyyMMdd") + ".log";//用日期格式对日志命名

            //创建或打开日志文件，向日志文件末尾追加记录
            using (var mySw = File.AppendText(filename))
            {
                //当前请求原始URL
                var requestUrl = HttpContext.Current != null ? HttpContext.Current.Request.RawUrl : "";

                //向日志文件写入内容
                var writeContent =
                    $"时间：{time}\r\n调用文件：{filePath}\r\n日志级别：{level}\r\n请求原始URL：{requestUrl}\r\n自定义信息：{message}\r\n错误信息：{(err != null ? err.ToString() : "")}\r\n----------------------------------------------------------------------------------------\r\n\r\n";

                mySw.WriteLine(writeContent);
            }
        }
    }
}
