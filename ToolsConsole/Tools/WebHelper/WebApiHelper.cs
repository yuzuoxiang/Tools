using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tools.WebHelper
{
    public class WebApiHelper
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
        public string PostWebRequest(string postUrl, string paramData)
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

        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T Post<T>(string url, IDictionary<string, string> parameters)
        {
            var strResult = PostResponseStr(url, parameters);
            try
            {
                var res = JsonConvert.DeserializeObject<T>(strResult);
                return res;
            }
            catch (Exception ex)
            {
                Tool.LogHelper.WriteLog(ex);
            }
            return default(T);
        }

        /// <summary>
        /// Post 请求 string 返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string PostResponseStr(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            StringBuilder buffer = new StringBuilder();
            int i = 0;
            foreach (string key in parameters.Keys)
            {
                if (i > 0)
                    buffer.Append("&");

                buffer.AppendFormat("{0}={1}", key, parameters[key]);
                i++;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(buffer.ToString());

            //写数据
            request.Method = "POST";
            request.ContentLength = bytes.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            //   request.Proxy = null;
            Stream reqstream = request.GetRequestStream();
            reqstream.Write(bytes, 0, bytes.Length);

            //读数据
            request.Timeout = 300000;
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = null;
            string strResult = string.Empty;
            Stream streamReceive = null;
            StreamReader streamReader = null;

            try
            {
                //请求数据
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    streamReceive = response.GetResponseStream();
                    streamReader = new StreamReader(streamReceive, Encoding.UTF8);
                    strResult = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (response = (HttpWebResponse)ex.Response)
                    {
                        streamReceive = response.GetResponseStream();
                        streamReader = new StreamReader(streamReceive, Encoding.UTF8);
                        strResult = streamReader.ReadToEnd();
                        Tool.Log.Write(strResult);
                    }
                }
                else
                {
                    strResult = ex.Message;
                }
            }
            finally
            {
                //关闭流
                reqstream.Close();
                streamReader.Close();
                streamReceive.Close();
                request.Abort();
                response.Close();
            }

            return (strResult);
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="paraQuery"></param>
        /// <param name="paraHeader"></param>
        /// <returns></returns>
        public static T Get<T>(string url, IDictionary<string, string> paraQuery, IDictionary<string, string> paraHeader)
        {
            var strResult = GetResponseStr(url, paraQuery, paraHeader);
            return JsonConvert.DeserializeObject<T>(strResult);
        }

        /// <summary>
        /// Get WebApi 内容 string类型返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paraQuery"></param>
        /// <param name="paraHeader"></param>
        /// <returns></returns>
        public static string GetResponseStr(string url, IDictionary<string, string> paraQuery, IDictionary<string, string> paraHeader)
        {
            //加入参数
            int i = 0;
            bool isExistPara = url.Contains('?') ? true : false;//判断是否存在参数
            if (paraQuery != null)
                foreach (string key in paraQuery.Keys)
                {
                    url += ((i == 0 && !isExistPara) ? "?" : "&");
                    url += string.Format("{0}={1}", key, paraQuery[key]);
                    i++;
                }
            Tool.Log.Write(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            //加入头信息
            if (paraHeader != null)
                foreach (string key in paraHeader.Keys)
                    request.Headers.Add(key, paraHeader[key]);

            request.Method = "GET";
            request.ContentType = "application/json";
            request.Timeout = 90000;
            request.Headers.Set("Pragma", "no-cache");


            HttpWebResponse response = null;
            Stream streamReceive = null;
            StreamReader streamReader = null;
            string strResult = string.Empty;
            try
            {
                //请求数据
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    streamReceive = response.GetResponseStream();
                    streamReader = new StreamReader(streamReceive, Encoding.UTF8);
                    strResult = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (response = (HttpWebResponse)ex.Response)
                    {
                        streamReceive = response.GetResponseStream();
                        streamReader = new StreamReader(streamReceive, Encoding.UTF8);
                        strResult = streamReader.ReadToEnd();
                    }
                }
                else
                {
                    strResult = ex.Message;
                }
            }
            finally
            {
                streamReader.Close();
                streamReceive.Close();
                request.Abort();
                response.Close();
            }

            return strResult;
        }
    }
}
