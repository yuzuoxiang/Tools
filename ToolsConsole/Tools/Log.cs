using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;

namespace Tools
{
    /// <summary>
    /// 日志处理文件
    /// </summary>
    public class Log
    {
        static private string _logpath;
        static public string LogPath
        {
            get
            {
                _logpath = ConfigurationManager.AppSettings["logpath"];
                if (string.IsNullOrEmpty(_logpath))
                {
                    return "";
                }
                else
                {
                    return _logpath;
                }
            }
            set
            {
                _logpath = value;
            }
        }

        static public void Write(string _classname, string _message, Exception _err)
        {

            if (string.IsNullOrEmpty(LogPath))
            {
                return;
            }

            if (Directory.Exists(LogPath) == false)
            {

                Directory.CreateDirectory(LogPath);
            }
            string filepath;
            try
            {
                if (HttpContext.Current == null)
                {
                    filepath = "WinForm使用";
                }
                else
                {
                    filepath = HttpContext.Current.Request.PhysicalPath + "?" + HttpContext.Current.Request.QueryString.ToString();
                }
            }
            catch
            {
                filepath = "WinForm使用";
            }

            try
            {
                StreamWriter fsoWrite = new StreamWriter(LogPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", true, Encoding.GetEncoding("gb2312"));

                string s1 = "";
                if (_err != null)
                {
                    s1 = _err.ToString();
                }

                fsoWrite.WriteLine("时间:{0}\r\n调用文件:{1}\r\n类名称:{2}\r\n自定义信息:{3}\r\n错误信息:{4}\r\n----------------------------------------------------------------------------------------\r\n\r\n"
                    , DateTime.Now.ToString(), filepath, _classname, _message, s1);
                fsoWrite.Close();
            }
            catch
            {

            }
            finally
            {

            }
        }
    }
}
