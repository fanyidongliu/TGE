using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LK.Common
{

    #region HTTP模拟浏览器get与post请求
    public class HttpUty
    {
        #region Post请求
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="strUrl">访问地址</param>
        /// <param name="strParameter">提交参数</param>
        /// <returns>返回内容</returns>
        public static string RestfulPost(string strUrl, string strParameter)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接
            string strResult = "";//返回结果
            HttpWebRequest hwrRequest = null;
            HttpWebResponse hwrResponse = null;
            Stream stmRequest = null;
            try
            {
                ServicePointManager.DefaultConnectionLimit = 200;//设置最大连接数
                hwrRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                hwrRequest.Method = "POST";
                hwrRequest.Accept = "application/json";
                hwrRequest.ContentType = "application/json";//设置数据类型和长度
                #region 往服务器写入数据
                if (!string.IsNullOrWhiteSpace(strParameter))
                {
                    byte[] bytRequest = System.Text.Encoding.UTF8.GetBytes(strParameter);
                    hwrRequest.ContentLength = bytRequest.Length;
                    stmRequest = hwrRequest.GetRequestStream();
                    stmRequest.Write(bytRequest, 0, bytRequest.Length);
                    stmRequest.Close();
                }
                #endregion 往服务器写入数据
                hwrResponse = (HttpWebResponse)hwrRequest.GetResponse();//获取服务端返回
                StreamReader smrResponse = new StreamReader(hwrResponse.GetResponseStream(), Encoding.UTF8);//获取服务端返回数据
                strResult = smrResponse.ReadToEnd().Trim();
                smrResponse.Close();
                return strResult;
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                throw ex;
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭连接和流
                if (hwrResponse != null) hwrResponse.Close();
                if (hwrRequest != null) hwrRequest.Abort();
            }
        }
        #endregion Post请求

        #region Post请求数据文件流
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="strUrl">访问地址</param>
        /// <param name="strParameter">提交参数</param>
        /// <returns>返回内容</returns>
        public static MemoryStream RestfulPostStream(string strUrl, string strParameter)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接
            HttpWebRequest hwrRequest = null;
            HttpWebResponse hwrResponse = null;
            Stream stmRequest = null;
            try
            {
                ServicePointManager.DefaultConnectionLimit = 200;//设置最大连接数
                hwrRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                hwrRequest.Method = "POST";
                //hwrRequest.Accept = "application/json";
                hwrRequest.ContentType = "application/json";//设置数据类型和长度
                #region 往服务器写入数据
                if (strParameter != null)
                {
                    byte[] bytRequest = System.Text.Encoding.UTF8.GetBytes(strParameter);
                    hwrRequest.ContentLength = bytRequest.Length;
                    stmRequest = hwrRequest.GetRequestStream();
                    stmRequest.Write(bytRequest, 0, bytRequest.Length);
                    stmRequest.Close();
                }
                #endregion 往服务器写入数据
                hwrResponse = (HttpWebResponse)hwrRequest.GetResponse();//获取服务端返回
                if (hwrResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception("HTTP状态码异常，StatusCode：" + Convert.ToInt32(hwrResponse.StatusCode));

                MemoryStream ms = new MemoryStream();
                using (Stream s = hwrResponse.GetResponseStream())
                {
                    byte[] buffer = new byte[1024];
                    int actual = 0;
                    //先保存到内存流中MemoryStream
                    while ((actual = s.Read(buffer, 0, 1024)) > 0)
                        ms.Write(buffer, 0, actual);

                    ms.Position = 0;
                }
                return ms;
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                throw ex;
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭连接和流
                if (hwrResponse != null) hwrResponse.Close();
                if (hwrRequest != null) hwrRequest.Abort();
            }
        }
        #endregion Post请求

        #region Post请求数据文件【Excel】
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="strUrl">访问地址</param>
        /// <param name="strParameter">提交参数</param>
        /// <returns>返回内容</returns>
        public static HttpResponseMessage RestfulPostStreamExcel(string strUrl, string strParameter)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接
            HttpWebRequest hwrRequest = null;
            HttpWebResponse hwrResponse = null;
            Stream stmRequest = null;
            try
            {
                ServicePointManager.DefaultConnectionLimit = 200;//设置最大连接数
                hwrRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                hwrRequest.Method = "POST";
                //hwrRequest.Accept = "application/json";
                hwrRequest.ContentType = "application/json";//设置数据类型和长度
                #region 往服务器写入数据
                if (strParameter != null)
                {
                    byte[] bytRequest = System.Text.Encoding.UTF8.GetBytes(strParameter);
                    hwrRequest.ContentLength = bytRequest.Length;
                    stmRequest = hwrRequest.GetRequestStream();
                    stmRequest.Write(bytRequest, 0, bytRequest.Length);
                    stmRequest.Close();
                }
                #endregion 往服务器写入数据
                hwrResponse = (HttpWebResponse)hwrRequest.GetResponse();//获取服务端返回
                if (hwrResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception("HTTP状态码异常，StatusCode：" + Convert.ToInt32(hwrResponse.StatusCode));

                MemoryStream ms = new MemoryStream();
                using (Stream s = hwrResponse.GetResponseStream())
                {
                    byte[] buffer = new byte[1024];
                    int actual = 0;
                    //先保存到内存流中MemoryStream
                    while ((actual = s.Read(buffer, 0, 1024)) > 0)
                        ms.Write(buffer, 0, actual);
                    ms.Position = 0;
                }
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StreamContent(ms);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

                return result;
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                throw ex;
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭连接和流
                if (hwrResponse != null) hwrResponse.Close();
                if (hwrRequest != null) hwrRequest.Abort();
            }
        }
        #endregion Post请求

        #region Get请求
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="strUrl">请求地址</param>
        /// <returns>返回内容</returns>
        public static string RestfulGet(string strUrl)
        {
            System.GC.Collect();
            string strResult = "";
            HttpWebRequest hwrRequest = null;
            HttpWebResponse hwrResponse = null;
            //请求strUrl以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                hwrRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                hwrRequest.Method = "GET";
                //获取服务器返回
                hwrResponse = (HttpWebResponse)hwrRequest.GetResponse();
                //获取HTTP返回数据
                StreamReader sr = new StreamReader(hwrResponse.GetResponseStream(), Encoding.UTF8);
                strResult = sr.ReadToEnd().Trim();
                sr.Close();
                return strResult;
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                System.Threading.Thread.ResetAbort();
                throw new Exception(ex.Message.ToString());
            }
            catch (WebException wex)
            {
                throw new Exception(wex.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭连接和流
                if (hwrResponse != null) hwrResponse.Close();
                if (hwrRequest != null) hwrRequest.Abort();
            }

        }
        #endregion Get请求        
    }
    #endregion
}
