using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtension
    {
        public static DateTime FormatDateTime = DateTime.Parse("1900-01-01 00:00:00");

        /// <summary>
        /// 获取字符串长度(中文按两个长度计算)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ByteLength(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                byte[] bytes = Encoding.Default.GetBytes(s);
                return bytes.Length;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// /// <summary>
        /// 去除HTML及空格 常用于标题输出
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToSafe(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return "";
            s = StrClass.RemoveHtml(s);
            s = s.Replace("　", "");
            s = s.Replace("\r\n", " ");
            string text1 = "\\s+";
            Regex regex1 = new Regex(text1);
            s = regex1.Replace(s, " ");
            s = System.Web.HttpUtility.HtmlEncode(s);
            return s.Trim();
        }

        /// <summary>
        /// 转换INT类型
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defautValue"></param>
        /// <returns></returns>
        public static int ToInt32(this string s, int defautValue)
        {
            int result = defautValue;
            if (int.TryParse(s, out result))
            {
                return result;
            }
            return defautValue;
        }

        /// <summary>
        /// 转换INT类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt32(this string s)
        {
            return ToInt32(s, 0);
        }

        /// <summary>
        /// 转换成FLOAT类型
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defautValue"></param>
        /// <returns></returns>
        public static float ToFloat(this string s, float defautValue)
        {
            float result = defautValue;
            float.TryParse(s, out result);
            return result;
        }

        /// <summary>
        /// 转换成FLOAT类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float ToFloat(this string s)
        {

            return ToFloat(s, 0);
        }

        /// <summary>
        /// 转换成Double类型
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this string s, double defaultValue)
        {
            double result = 0;
            if (double.TryParse(s, out result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 转换成Double类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double ToDouble(this string s)
        {
            return ToDouble(s, 0);
        }

        /// <summary>
        /// 转换成Decimal类型
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string s, decimal defaultValue)
        {

            decimal result = 0;
            if (decimal.TryParse(s, out result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 转换成Decimal类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string s)
        {

            return ToDecimal(s, 0);
        }

        /// <summary>
        /// 转换成Byte类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte ToByte(this string s)
        {
            byte b = 0;
            byte.TryParse(s, out b);
            return b;
        }

        /// <summary>
        /// 转换为Bool类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ToBool(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            s = s.ToLower();
            if (s == "true" || s == "1")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 转换日期格式 ,格式不合格则返回默认日期 1900-1-1
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s)
        {
            DateTime time;
            if (DateTime.TryParse(s, out time))
            {
                return time;
            }
            else
            {
                return Common.Common.InitDateTime();
            }
        }

        /// <summary>
        /// 返回字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToString2(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            return s;
        }

        /// <summary>
        /// 去除空格
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Trim2(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            return s.Trim();
        }

        /// <summary>
        /// 是否是空字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
    }
}
