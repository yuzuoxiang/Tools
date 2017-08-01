using System;
using System.Data;
using System.Text;

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


    }
}
