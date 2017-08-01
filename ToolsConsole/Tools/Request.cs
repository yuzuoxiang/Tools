using System;
using System.IO;
using System.Net;
using System.Text;

namespace Tools
{
    public class Request
    {
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public string Web_Get(string uri)
        {
            string rl;
            try
            {
                WebRequest request = WebRequest.Create(uri);
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream, System.Text.Encoding.GetEncoding("UTF-8"));
                StringBuilder sb = new StringBuilder();

                while ((rl = sr.ReadLine()) != null)
                {
                    sb.Append(rl);
                }
                response.Close();
                return sb.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="postUrl">地址</param>
        /// <param name="paramData">参数</param>
        /// <returns></returns>
        private string PostWebRequest(string postUrl, string paramData)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception)
            {
                return "0";
            }
            return ret;
        }
    }
}
