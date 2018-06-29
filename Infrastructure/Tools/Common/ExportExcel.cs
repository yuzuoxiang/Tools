using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tools.Common
{
    public class ExportExcel
    {
        static public void ExportData(GridView obj)
        {
            try
            {
                string style = "";
                if (obj.Rows.Count > 0)
                {
                    style = @"<style> .text { mso-number-format:\@; } </script> ";
                }
                else
                {
                    style = "no data.";
                }

                HttpContext.Current.Response.ClearContent();
                DateTime dt = DateTime.Now;
                string filename = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Efu_" + filename + ".xls");
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Charset = "GB2312";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                obj.RenderControl(htw);
                HttpContext.Current.Response.Write(style);
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
            catch
            {
            }
        }
        /// <summary> 
        /// 以流的形式，可以设置很丰富复杂的样式 
        /// </summary> 
        /// <param name="content">Excel中内容(Table格式)</param> 
        /// <param name="filename">文件名</param> 
        /// <param name="cssText">样式内容</param> 
        public static void ExportToExcel(string filename, string content, string cssText)
        {
            var res = HttpContext.Current.Response;
            //content = String.Format("<style type='text/css'>{0}</style>{1}", cssText, content);

            res.Clear();
            res.Buffer = true;
            res.Charset = "UTF-8";
            res.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            res.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            res.ContentType = "application/ms-excel;charset=UTF-8";
            res.Write(content);
            res.Flush();
            res.End();
        }
    }
}
