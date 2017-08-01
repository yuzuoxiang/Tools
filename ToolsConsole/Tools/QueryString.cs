using System.Text.RegularExpressions;
using System.Web;

namespace Tools
{
    /// <summary>
    /// QueryString 地址栏参数
    /// </summary>
    public class QueryString
    {
        #region Request
        /// <summary>
        /// 等于Request.QueryString;如果为null 返回 空“” ，否则返回Request.QueryString[name]
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Q(string name)
        {
            return Request.QueryString[name] == null ? "" : Request.QueryString[name];
        }

        /// <summary>
        /// 等于Request.Form  如果为null 返回 空“” ，否则返回 Request.Form[name]
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string F(string name)
        {
            return Request.Form[name] == null ? "" : Request.Form[name].ToString();
        }

        public static string QF(string name)
        {
            return Request[name] == null ? "" : Request[name].ToString();
        }

        public static string QF(string name, string defaultval)
        {
            string val = QF(name);
            if (string.IsNullOrEmpty(val))
            {
                return defaultval;
            }
            return val;
        }
        public static string P(string name)
        {
            return Request.Params[name] == null ? "" : Request.Params[name].ToString();
        }
        #endregion

        #region 获取url中的id
        /// <summary>
        /// 获取url中的id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int QId(string name)
        {
            return Q(name).ToInt32();
        }
        public static int QId(string name, int defaultval)
        {
            string val = Q(name);
            int iRet;
            if (int.TryParse(val, out iRet) == false)
            {
                iRet = defaultval;
            }
            return iRet;
        }
        public static int FId(string name)
        {
            string str = Request.Form[name] == null ? "" : Request.Form[name];
            return str.ToInt32();
        }
        public static int QFId(string name)
        {
            return QF(name).ToInt32();
        }
        public static int QFId(string name, int defaultval)
        {
            string val = QF(name);
            int iRet;
            if (int.TryParse(val, out iRet) == false)
            {
                iRet = defaultval;
            }
            return iRet;
        }
        #endregion

        #region 获取正确的Id，如果不是正整数，返回0
        /// <summary>
        /// 获取正确的Id，如果不是正整数，返回0
        /// </summary>
        /// <param name="_value"></param>
        /// <returns>返回正确的整数ID，失败返回0</returns>
        public static int StrToId(string _value)
        {
            if (IsNumberId(_value))
                return int.Parse(_value);
            else
                return 0;
        }
        /// <summary>
        /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。
        /// </summary>
        /// <param name="_value">需验证的字符串。。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsNumberId(string _value)
        {
            return StringCheck.IsIntegerByPositive(_value);
        }
        #endregion

        /// <summary>
        /// 获取过滤后的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string StrToStr(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            value = Regex.Replace(value, @";", string.Empty);
            value = Regex.Replace(value, @"'", string.Empty);
            value = Regex.Replace(value, @"&", string.Empty);
            value = Regex.Replace(value, @"%20", string.Empty);
            value = Regex.Replace(value, @"--", string.Empty);
            value = Regex.Replace(value, @"==", string.Empty);
            value = Regex.Replace(value, @"<", string.Empty);
            value = Regex.Replace(value, @">", string.Empty);
            //value = Regex.Replace(value, @"table", string.Empty);
            //value = Regex.Replace(value, @"drop", string.Empty);
            //value = Regex.Replace(value, @"insert", string.Empty);
            return value;
        }

        /// <summary>
        /// FormatNumSerial 格式化数字，以逗号分开
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatNumSerial(string value)
        {
            if (string.IsNullOrEmpty(value)) return "0";
            
            value = value.Replace(" ", "");
            if (value.Length < 1) return "0";
            string[] a = value.Split(new char[] { ',' });
            string ss = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (StrToId(a[i]) != 0) ss += a[i] + ",";
            }
            if (ss.Length > 0)
            {
                ss = ss.Substring(0, ss.Length - 1);
            }
            return ss;
        }


        #region 类内部调用
        /// <summary>
        /// HttpContext Current
        /// </summary>
        public static HttpContext Current
        {
            get { return HttpContext.Current; }
        }
        /// <summary>
        /// HttpContext Current  HttpRequest Request   get { return Current.Request;
        /// </summary>
        public static HttpRequest Request
        {
            get { return Current.Request; }
        }
        /// <summary>
        ///  HttpContext Current  HttpRequest Request   get { return Current.Request; HttpResponse Response  return Current.Response;
        /// </summary>
        public static HttpResponse Response
        {
            get { return Current.Response; }
        }
        #endregion
    }
}
