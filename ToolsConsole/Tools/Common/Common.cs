using System;
using System.Data;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Tools.Common
{
    public class Common
    {
        /// <summary>
        /// 日期值的初始值
        /// </summary>
        /// <returns></returns>
        public static DateTime InitDateTime()
        {
            return DateTime.Parse("1900-01-01");
        }

        /// <summary>
        /// Datable转JSON
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DtToSON(DataTable dt)
        {
            if (dt == null || dt.Rows.Count < 1)
            {
                return "{\"Table\":}";
            }

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName.ToString());
            jsonBuilder.Append("\":[ ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(RepSon(dt.Rows[i][j].ToString()));
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append(" ]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        private static string RepSon(string s1)
        {
            if (string.IsNullOrEmpty(s1))
            {
                return "";
            }
            else
            {
                s1 = s1.Replace("\"", "");
                s1 = s1.Replace("\r\n", "");
                s1 = s1.Replace("  ", "");
                s1 = s1.Replace("\\", "");
                s1 = s1.Replace("{", "『");
                s1 = s1.Replace("}", "』");
                s1 = s1.Replace("'", "‘");
                return s1;
            }
        }

        /// <summary>
        /// 日期格式化
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ForMatTimes(string time)
        {
            try
            {
                string s = "";
                DateTime startDate = DateTime.Parse(time.ToString());
                DateTime endDate = DateTime.Now;
                TimeSpan TS = new TimeSpan(endDate.Ticks - startDate.Ticks);
                double j = Math.Round(TS.TotalMinutes);
                if (j <= 1)
                {
                    s = "刚刚";
                }
                else if (j > 1 && j < 60)
                {
                    s = j + "分钟前";
                }
                else if (j >= 60 && j < 60 * 24)
                {
                    s = Math.Round(TS.TotalHours) + "小时前";
                }
                else if (j >= 60 * 24 && j < 60 * 24 * 30)
                {
                    s = Math.Round(TS.TotalDays) + "天前";
                }
                else if (j >= 60 * 24 * 30 && j < 60 * 24 * 30 * 12)
                {
                    s = Math.Floor(Math.Round(TS.TotalDays) / 30) + "月前";
                }
                else if (j >= 60 * 24 * 30 * 12)
                {
                    s = Math.Floor(Math.Round(TS.TotalDays) / (30 * 12)) + "年前";
                }
                return s;
            }
            catch (Exception)
            {
                return "";
            }
        }

        #region 加密
        /// <summary>
        /// 获取MD5加密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code">16 or 32</param>
        /// <returns></returns>
        public static string Md5(string str, int code)
        {
            if (code == 16) //16位MD5加密（取32位加密的9~25字符）  
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            if (code == 32) //32位加密  
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
            return "00000000000000000000000000000000";
        }

        static public string StrEn(string str)
        {
            if (str == null || str.Length == 0)
            {
                return null;
            }
            else
            {
                return Des.Encrypt(str, "zuoxiang");
            }
        }

        static public string StrDe(string str)
        {


            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            else
            {
                if (str.Length < 4)
                {
                    return string.Empty;
                }

                try
                {
                    return Des.Decrypt(str, "zuoxiang");
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        //static public string StrEn2(string str)
        //{
        //    if (str == null || str.Length == 0)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        str = str.Replace("[-efu-]", "");
        //        return "[-efu-]" + Des2.SEncryptString(str, Des2.dkey);
        //    }
        //}

        //static public string StrDe2(string str)
        //{


        //    if (string.IsNullOrEmpty(str))
        //    {
        //        return string.Empty;
        //    }
        //    else
        //    {
        //        if (str.Length < 4)
        //        {
        //            return string.Empty;
        //        }

        //        try
        //        {
        //            if (StrMid(str, 0, 7) == "[-efu-]")
        //            {
        //                str = StrMid(str, 7, str.Length);
        //                return Des2.SDecryptString(str, Des2.dkey);
        //            }
        //            else
        //            {
        //                return str;
        //            }

        //        }
        //        catch
        //        {
        //            return string.Empty;
        //        }
        //    }
        //}
        #endregion

        #region Cookie和Session
        public static string CC(string a, string b)
        {
            if (HttpContext.Current.Request.Cookies[a] == null)
            {
                return "";
            }
            else
            {
                return System.Web.HttpContext.Current.Request.Cookies[a][b];
            }
        }
        public static string C(string a)
        {
            if (HttpContext.Current.Request.Cookies[a] == null)
            {
                return "";
            }
            else
            {
                return HttpContext.Current.Request.Cookies[a].Value;
            }
        }
        /// <summary>
        /// 保存cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="val"></param>
        public static void SetCookie(string name, string val)
        {
            HttpCookie cookie = new HttpCookie("lanzhou");
            cookie.Values.Add(name, val);
            HttpContext.Current.Response.Cookies.Add(cookie);
            cookie.Expires = DateTime.Now.AddDays(356);
        }

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetCookie(string name)
        {
            var cookie = HttpContext.Current.Request.Cookies["lanzhou"];
            if (cookie == null)
            {
                return string.Empty;
            }
            else
            {
                return cookie[name];
            }
        }

        ///// <summary>
        ///// 保存Session信息
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="value"></param>
        public static void SetSession(string name, object value)
        {
            HttpContext.Current.Session[name] = value;
        }
        /// <summary>
        /// 获取Session信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetSession(string name)
        {
            var session = HttpContext.Current.Session[name];
            return session == null ? "" : session.ToString();
        }


        #endregion


        /// <summary>
        /// 获取客户端的IP
        /// </summary>
        /// <returns></returns>
        public static string GetUserIP()
        {
            string userIP;
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] == null)
            {
                userIP = HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                userIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }

            return userIP;
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        public static string GetRandomTimeSpan()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        #region 获得两个日期的间隔
        /// <summary>
        /// 获得两个日期的间隔
        /// </summary>
        /// <param name="DateTime1">日期一。</param>
        /// <param name="DateTime2">日期二。</param>
        /// <returns>日期间隔TimeSpan。</returns>
        public static TimeSpan DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }
        #endregion

        #region 得到随机日期
        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <param name="time1">起始日期</param>
        /// <param name="time2">结束日期</param>
        /// <returns>间隔日期之间的 随机日期</returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime minTime = new DateTime();
            DateTime maxTime = new DateTime();

            System.TimeSpan ts = new System.TimeSpan(time1.Ticks - time2.Ticks);

            // 获取两个时间相隔的秒数
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds = 0;

            if (dTotalSecontds > System.Int32.MaxValue)
            {
                iTotalSecontds = System.Int32.MaxValue;
            }
            else if (dTotalSecontds < System.Int32.MinValue)
            {
                iTotalSecontds = System.Int32.MinValue;
            }
            else
            {
                iTotalSecontds = (int)dTotalSecontds;
            }

            if (iTotalSecontds > 0)
            {
                minTime = time2;
                maxTime = time1;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = time1;
                maxTime = time2;
            }
            else
            {
                return time1;
            }

            int maxValue = iTotalSecontds;

            if (iTotalSecontds <= System.Int32.MinValue)
                maxValue = System.Int32.MinValue + 1;

            int i = random.Next(System.Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }
        #endregion

        #region 获得当前绝对路径
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        #endregion

        #region  获取网页的HTML内容
        // 获取网页的HTML内容，指定Encoding
        public static string GetHtml(string url, Encoding encoding)
        {
            byte[] buf = new WebClient().DownloadData(url);
            if (encoding != null) return encoding.GetString(buf);
            string html = Encoding.UTF8.GetString(buf);
            encoding = GetEncoding(html);
            if (encoding == null || encoding == Encoding.UTF8) return html;
            return encoding.GetString(buf);
        }
        // 根据网页的HTML内容提取网页的Encoding
        public static Encoding GetEncoding(string html)
        {
            string pattern = @"(?i)\bcharset=(?<charset>[-a-zA-Z_0-9]+)";
            string charset = Regex.Match(html, pattern).Groups["charset"].Value;
            try { return Encoding.GetEncoding(charset); }
            catch (ArgumentException) { return null; }
        }
        #endregion
    }
}
