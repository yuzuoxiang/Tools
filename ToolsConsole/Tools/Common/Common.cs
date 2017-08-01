using System;
using System.Data;
using System.Text;
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
    }
}
