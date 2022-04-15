using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace LK.Common
{
    #region Json与对象转化
    public static class JsonUty
    {
        public static Dictionary<string, object> GetDictionaryByJson(string strJson)
        {
            JsonSerializer jssUtility = null;
            StringReader srdUtilit = null;
            Dictionary<string, object> dicJson = null;
            try
            {
                jssUtility = new JsonSerializer();
                srdUtilit = new StringReader(strJson);
                dicJson = (Dictionary<string, object>)jssUtility.Deserialize(new JsonTextReader(srdUtilit), typeof(Dictionary<string, object>));
                return dicJson;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                jssUtility = null; srdUtilit = null;
            }
        }


        /// 对象转为json  
        /// </summary>  
        /// <typeparam name="ObjType"></typeparam>  
        /// <param name="obj"></param>  
        /// <returns></returns>  
        public static string ObjToJsonString<ObjType>(ObjType obj) where ObjType : class
        {
            string s = JsonConvert.SerializeObject(obj);
            return s;
        }
        /// <summary>  
        /// json转为对象  
        /// </summary>  
        /// <typeparam name="ObjType"></typeparam>  
        /// <param name="JsonString"></param>  
        /// <returns></returns>  
        public static ObjType JsonStringToObj<ObjType>(string JsonString) where ObjType : class
        {
            ObjType o = JsonConvert.DeserializeObject<ObjType>(JsonString);
            return o;
        }
    }
    #endregion
}
