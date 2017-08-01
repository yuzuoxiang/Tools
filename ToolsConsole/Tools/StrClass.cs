using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
    /// <summary>
    /// 一般的字符串操作
    /// </summary>
    public static class StrClass
    {
        static void Main(string[] args)
        {
            Console.Read();
        }

        /// <summary>
        /// 为A标签添加nofollow属性(这个属性的意义是告诉搜索引擎"不要追踪此网页上的链接或不要追踪此特定链接")
        /// </summary>
        /// <param name="s1"></param>
        /// <returns></returns>
        public static string Anofollow(string s1)
        {
            if (string.IsNullOrEmpty(s1))
                return string.Empty;

            string p = @"(?s)<a[\s][^>]*href\s*=\s*[\""\']?(?<url>([^\""\'>\s]*))[\""\']?[^>]*>(?<title>([^<]+|.*?)?)</a\s*>";
            s1 = Regex.Replace(s1, p, "<a href=\"$1\" rel=\"nofollow\" target=\"_blank\">$2</a>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return s1;
        }

        static public string StrToHtml(string Readme)
        {
            if (string.IsNullOrEmpty(Readme))
                return string.Empty;

            Readme = Readme.Replace("<", "&lt;");
            Readme = Readme.Replace(">", "&gt;");
            Readme = Readme.Replace("\r\n", "<br>");
            Readme = Readme.Replace("  ", "&nbsp;&nbsp;");
            Readme = Readme.Replace("@", "&#64;");
            return Readme;
        }

        static public string HtmlToStr(string Readme)
        {
            if (string.IsNullOrEmpty(Readme))
                return string.Empty;

            Readme = Readme.Replace("&lt;", "<");
            Readme = Readme.Replace("&gt;", ">");
            Readme = Readme.Replace("<br>", "\r\n");
            Readme = Readme.Replace("&nbsp;&nbsp;", "  ");
            Readme = Readme.Replace("&#64;", "@");
            return Readme;
        }

        static public string RepEnter(string Temp_str)
        {
            if (string.IsNullOrEmpty(Temp_str))
                return string.Empty;

            Temp_str = Temp_str.Replace("\"", "'");
            Temp_str = Temp_str.Replace(">", "&gt;");
            Temp_str = Temp_str.Replace("<", "&lt;");
            Temp_str = Temp_str.Replace("\n", "");
            Temp_str = Temp_str.Replace("\r", "");
            Temp_str = Temp_str.Replace("\t", "");
            Temp_str = Temp_str.Replace("@", "&#64;");
            return Temp_str;
        }

        /// <summary>
        /// 从某个位置开始截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="x">起始截取位置</param>
        /// <param name="y">截取的字符数量</param>
        /// <returns></returns>
        static public string StrMid(string str, int x, int y)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            int sl = str.Length;
            if (sl < x)
                return string.Empty;
            int jl = sl - x;

            return jl < y ? str.Substring(x, jl) : str.Substring(x, y);
        }

        /// <summary>
        /// 获取字符串字节的长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ByteLength(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            byte[] bytes = Encoding.Default.GetBytes(str);
            return bytes.Length;
        }

        /// <summary>
        /// SQL检查(防止SQL注入)
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        static public string SqlCheck(string _str)
        {
            if (string.IsNullOrEmpty(_str))
                return string.Empty;

            _str = _str.ToLower();
            _str = _str.Replace("\"", "");
            _str = _str.Replace("&", "");
            _str = _str.Replace("'", "");
            _str = _str.Replace(";", "");
            _str = _str.Replace("%", "");
            _str = _str.Replace("--", "");
            _str = _str.Replace("()", "");
            _str = _str.Replace("select ", "");
            _str = _str.Replace("delete ", "");
            _str = _str.Replace("update ", "");
            _str = _str.Replace("from ", "");
            _str = _str.Replace("where ", "");
            _str = _str.Replace("=", "");
            _str = _str.Replace("<", "");
            _str = _str.Replace(">", "");
            _str = _str.Replace("insert ", "");
            _str = _str.Replace("and ", "");
            _str = _str.Replace(")and( ", "");
            _str = _str.Replace(" or ", "");
            _str = _str.Replace(")or(", "");
            _str = _str.Replace("asc(", "");
            _str = _str.Replace("chr(", "");
            _str = _str.Replace("mid(", "");
            _str = _str.Replace("len(", "");
            _str = _str.Replace(" desc", "");
            _str = _str.Replace("xp_cmdshell", "");//0x
            _str = _str.Replace("sysobjects", "");
            _str = _str.Replace("0x", "０x");
            return _str;
        }

        

        static public string Extract_HZ(string HZ)
        {
            if (string.IsNullOrEmpty(HZ))
                return string.Empty;

            byte[] ZW = new byte[2];
            long HZ_INT;
            ZW = System.Text.Encoding.Default.GetBytes(HZ);

            if (ZW.Length <= 1)
            {
                return HZ;
            }
            int i1 = (short)(ZW[0]);
            int i2 = (short)(ZW[1]);
            HZ_INT = i1 * 256 + i2; //  expresstion


            if ((HZ_INT >= 45217) && (HZ_INT <= 45252))
            {
                return "A";
            }
            if ((HZ_INT >= 45253) && (HZ_INT <= 45760))
            {
                return "B";
            }
            if ((HZ_INT >= 45761) && (HZ_INT <= 46317))
            {
                return "C";

            }
            if ((HZ_INT >= 46318) && (HZ_INT <= 46825))
            {
                return "D";
            }
            if ((HZ_INT >= 46826) && (HZ_INT <= 47009))
            {
                return "E";
            }
            if ((HZ_INT >= 47010) && (HZ_INT <= 47296))
            {
                return "F";
            }
            if ((HZ_INT >= 47297) && (HZ_INT <= 47613))
            {
                return "G";
            }

            if ((HZ_INT >= 47614) && (HZ_INT <= 48118))
            {

                return "H";
            }

            if ((HZ_INT >= 48119) && (HZ_INT <= 49061))
            {
                return "J";
            }
            if ((HZ_INT >= 49062) && (HZ_INT <= 49323))
            {
                return "K";
            }
            if ((HZ_INT >= 49324) && (HZ_INT <= 49895))
            {
                return "L";
            }
            if ((HZ_INT >= 49896) && (HZ_INT <= 50370))
            {
                return "M";
            }

            if ((HZ_INT >= 50371) && (HZ_INT <= 50613))
            {
                return "N";

            }
            if ((HZ_INT >= 50614) && (HZ_INT <= 50621))
            {
                return "O";
            }
            if ((HZ_INT >= 50622) && (HZ_INT <= 50905))
            {
                return "P";

            }
            if ((HZ_INT >= 50906) && (HZ_INT <= 51386))
            {
                return "Q";

            }

            if ((HZ_INT >= 51387) && (HZ_INT <= 51445))
            {
                return "R";
            }
            if ((HZ_INT >= 51446) && (HZ_INT <= 52217))
            {
                return "S";
            }
            if ((HZ_INT >= 52218) && (HZ_INT <= 52697))
            {
                return "T";
            }
            if ((HZ_INT >= 52698) && (HZ_INT <= 52979))
            {
                return "W";
            }
            if ((HZ_INT >= 52980) && (HZ_INT <= 53640))
            {
                return "X";
            }
            if ((HZ_INT >= 53689) && (HZ_INT <= 54480))
            {
                return "Y";
            }
            if ((HZ_INT >= 54481) && (HZ_INT <= 55289))
            {
                return "Z";
            }
            return ("");
        }

        static public string ASCII(string unicodeString)
        {
            if (string.IsNullOrEmpty(unicodeString))
                return string.Empty;

            string str = "";
            if (unicodeString != null && unicodeString.Length > 0)
            {
                for (int i = 0; i < unicodeString.Length; i++)
                {
                    char[] chars = unicodeString.Substring(i, 1).ToCharArray();
                    foreach (char code in chars)
                    {
                        str += Math.Abs((int)code).ToString();
                    }
                    if (i < unicodeString.Length - 1)
                    {
                        str += ",";
                    }
                }
            }
            return str;
        }

        /// <summary>
        /// 是否有效用户名
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool IsUserNameOk(string username)
        {
            Regex Reg = new Regex("^[a-zA-Z][a-zA-Z0-9_]{5,12}$");
            return Reg.IsMatch(username);
        }

        /// <summary>
        /// 是否有效密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool IsPwdOk(string pwd)
        {
            Regex Reg = new Regex("^([a-z]|[0-9]){5,11}$");
            return Reg.IsMatch(pwd);
        }

        /// <summary>
        /// 随机获取一个十位的，字符串包含字母或数字
        /// </summary>
        /// <returns></returns>
        public static string GetBoundary()
        {
            string pattern = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder boundaryBuilder = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                var index = rnd.Next(pattern.Length);
                boundaryBuilder.Append(pattern[index]);
            }
            return boundaryBuilder.ToString();
        }

        /// <summary>
        /// 判断是否是目录名称，并返回目录名称 目录只能是字母(大小写)数字和下划线(_)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string strToDirname(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            Regex regex = new Regex(@"^[a-z0-9_]+$", RegexOptions.IgnoreCase);
            if (regex.IsMatch(str))
            {
                return str;
            }
            else
            {
                return string.Empty;
            }
        }

        #region 清除HTML标记
        /// <summary>
        /// 删除所有HTML表单标签
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        static public string RepHtml2(string Content)
        {
            if (string.IsNullOrEmpty(Content))
                return string.Empty;

            Content = Regex.Replace(Content, @"<form[\S\s ]*>[\S\s ]*</form *>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<input[\S\s ]*?>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<select[\S\s ]*>[\S\s ]*</select *>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<style[\S\s ]*>[\S\s ]*</style *>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<textarea[\S\s ]*>[\S\s ]*</textarea *>", "", RegexOptions.IgnoreCase);
            return Content;
        }

        /// <summary>
        /// 删除HTML标签
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        static public string RepHtml3(string Content)
        {
            if (string.IsNullOrEmpty(Content))
                return string.Empty;

            Content = Regex.Replace(Content, @"<style[\S\s ]*>[\S\s ]*</style>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<script[\S\s ]*>[\S\s ]*</script *>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<iframe[\S\s ]*>[\S\s ]*</iframe *>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<iframe[\S\s ]*>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<frameset[\s\S]+</frameset *>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"href *= *[\s\S]*script *:", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"on([a-zA-Z]*)([ ]*)\=", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"(?i)<Object([^>])*>(\w|\W)*</Object([^>])*>", "", RegexOptions.IgnoreCase);
            return Content;
        }

        /// <summary>
        /// 删除HTML标签
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        static public string RepHtml4(string Content)
        {
            if (string.IsNullOrEmpty(Content))
                return string.Empty;

            Content = Regex.Replace(Content, @"<style[\S\s ]*>[\S\s ]*</style>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<script[\S\s ]*>[\S\s ]*</script *>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<iframe[\S\s ]*>[\S\s ]*</iframe *>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<iframe[\S\s ]*>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<frameset[\s\S]+</frameset *>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"href *= *[\s\S]*script *:", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"on([a-zA-Z]*)([ ]*)\=", "", RegexOptions.IgnoreCase);
            return Content;
        }

        static public string RepHtml5(string Content)
        {
            if (string.IsNullOrEmpty(Content))
                return string.Empty;

            Content = Regex.Replace(Content, @"<style[\S\s ]*>[\S\s ]*</style>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<script[\S\s ]*>[\S\s ]*</script *>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<iframe[\S\s ]*>[\S\s ]*</iframe *>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<iframe[\S\s ]*>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"<frameset[\s\S]+</frameset *>", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"href *= *[\s\S]*script *:", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"on([a-zA-Z]*)([ ]*)\=", "", RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, @"irame", "iframe", RegexOptions.IgnoreCase);
            return Content;
        }

        ///<summary>   
        ///清除HTML标记   
        ///</summary>   
        ///<param name="NoHTML">包括HTML的源码</param>   
        ///<returns>已经去除后的文字</returns>   
        public static string NoHTML(string Htmlstring)
        {
            if (string.IsNullOrEmpty(Htmlstring))
                return string.Empty;

            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);

            //删除HTML   
            Regex regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
            Htmlstring = regex.Replace(Htmlstring, "");
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");

            return Htmlstring;
        }

        public static string ClearNull(string Htmlstring)
        {
            if (string.IsNullOrEmpty(Htmlstring) == true)
            {
                return "";
            }
            Htmlstring = Regex.Replace(Htmlstring, "[\f\n\r\t\v]", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "[\\s]+", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "[\\s]+", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "[\\s]+", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "　", "", RegexOptions.IgnoreCase);
            Htmlstring = Htmlstring.Trim();
            return Htmlstring;
        }

        //删除所有A标签
        public static string NoHTML2(string Htmlstring)
        {
            if (string.IsNullOrEmpty(Htmlstring))
            {
                return string.Empty;
            }
            Htmlstring = Regex.Replace(Htmlstring, @"<a\s*[^>]*>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"</a>", "", RegexOptions.IgnoreCase);
            return Htmlstring;
        }

        /// <summary>
        /// 删除HTML标签
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string RemoveHtml(string Htmlstring)//取出html标签 
        {
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"-->", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<!--.*", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(amp|#38);", "&", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(lt|#60);", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(gt|#62);", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&#(\d+);", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Htmlstring.Replace("<br />", "");
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            return Htmlstring;
        }
        #endregion

        /// <summary>
        /// 验证手机号码是否正确
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool Ifshouji(string v)
        {
            Regex mobilereg = new Regex("^(133|134|135|136|137|138|139|150|151|152|157|158|159|186|187|188|189)[0-9]{8,8}$");
            return (mobilereg.IsMatch(v));
        }

        /// <summary>
        /// 去除HTML及空格 常用于标题 Keywords 输出
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToLineText(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = RemoveHtml(str);
            str = str.Replace("　", "");
            str = str.Replace("\r\n", " ");
            string text1 = "\\s+";
            Regex regex1 = new Regex(text1);
            str = regex1.Replace(str, " ");
            str = System.Web.HttpUtility.HtmlEncode(str);
            return str.Trim();
        }

        /// <summary>
        /// 文本框内容输出成html显示
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToHtmlText(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            StringBuilder builder1 = new StringBuilder();
            builder1.Append(str);
            builder1.Replace("&", "&amp;");
            builder1.Replace("<", "&lt;");
            builder1.Replace(">", "&gt;");
            builder1.Replace("\"", "&quot;");
            builder1.Replace("\r", "<br>");
            builder1.Replace(" ", "&nbsp;");
            return builder1.ToString();
        }

        /// <summary>
        /// 截取字符串函数 
        /// </summary>
        /// <param name="strOriginal">字符串</param>
        /// <param name="strFirst">起始字符</param>
        /// <param name="strLast">结束字符</param>
        /// <param name="t">0：不需要加起始和结束字符，1：相反</param>
        /// <returns></returns>
        public static string GetContent(string strOriginal, string strFirst, string strLast, string t)
        {
            if (string.IsNullOrEmpty(strOriginal) == true)
            {
                return "";
            }
            string s = "";
            int t1, t2, t3;
            if (t == "0")
            {
                string strOriginal1 = strOriginal, strFirst1 = strFirst, strLast1 = strLast;
                t1 = strOriginal1.IndexOf(strFirst1);
                if (t1 >= 0)
                {
                    t2 = strOriginal1.Length;
                    t3 = t1 + strFirst1.Length;
                    strOriginal1 = strOriginal1.Substring(t3);

                    t1 = strOriginal1.IndexOf(strLast1);
                    t3 = t1;
                    if (t3 > 0)
                    {
                        s = strOriginal1.Substring(0, t3);
                    }
                }
            }
            else if (t == "1")
            {
                s = GetContent(strOriginal, strFirst, strLast, "0");
                s = strFirst + s + strLast;
            }
            return s;
        }
    }
}
