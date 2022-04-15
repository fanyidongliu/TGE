
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace LK.Common
{
    public static class DataConvert
    {
        #region Exception
        public static string GetExceptionMessage(this Exception ex)
        {
            string s = "空间名：" + ex.Source + "；" + '\n' +
                       "方法名：" + ex.TargetSite + '\n' +
                       "故障点：" + ex.StackTrace + '\n' +
                       "错误提示：" + ex.Message;
            return s;
        } 
        #endregion

        #region String

        /// <summary>
        ///  将字符串转换为Bool
        /// </summary>
        /// <param name="StringValue"></param>
        /// <returns>当转换失败时返回false</returns>
        public static bool IsNullOrEmpty(this string StringValue)
        {
            if (string.IsNullOrEmpty(StringValue))
                return true;
            else
                return false;

        }

        /// <summary>
        ///  将字符串转换为Bool
        /// </summary>
        /// <param name="StringValue"></param>
        /// <returns>当转换失败时返回false</returns>
        public static bool ToBool(this string StringValue)
        {
            bool id;
            bool.TryParse(StringValue, out id);
            return id;
        }

        /// <summary>
        /// 将字符串转换为Int
        /// </summary>
        /// <param name="StringValue"></param>
        /// <returns>当转换失败时返回0</returns>
        public static byte ToByte(this string StringValue)
        {
            byte id;
            byte.TryParse(StringValue, out id);
            return id;
        }

        /// <summary>
        /// 将字符串转换为Int
        /// </summary>
        /// <param name="StringValue"></param>
        /// <returns>当转换失败时返回0</returns>
        public static int ToInt(this string StringValue)
        {
            int id;
            int.TryParse(StringValue, out id);
            return id;
        }
        /// <summary>
        /// 将字符串转换为Int
        /// </summary>
        /// <param name="StringValue"></param>
        /// <returns>当转换失败时返回0</returns>
        public static long ToLong(this string StringValue)
        {
            long id;
            long.TryParse(StringValue, out id);
            return id;
        }
        /// <summary>
        /// 将字符串转换为float
        /// </summary>
        /// <param name="StringValue"></param>
        /// <returns>当转换失败时返回0</returns>
        public static float ToFloat(this string StringValue)
        {
            float id;
            float.TryParse(StringValue.Replace(",", ""), out id);
            return id;
        }

        /// <summary>
        /// 将字符串转换为decimal
        /// </summary>
        /// <param name="StringValue"></param>
        /// <returns>当转换失败时返回0</returns>
        public static decimal ToDecimal(this string StringValue)
        {
            decimal id;
            decimal.TryParse(StringValue.Replace(",", ""), out id);
            return id;
        }

        /// <summary>
        /// 将字符串转换为string
        /// </summary>
        /// <param name="DoubleValue"></param>
        /// <returns>当转换失败时返回0</returns>
        public static double ToDouble(this string StringValue)
        {
            double id;
            double.TryParse(StringValue.Replace(",", ""), out id);
            return id;
        }


        /// <summary>
        /// 将字符型转换为Guid.
        /// </summary>
        /// <param name="StringValue"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string StringValue)
        {
            if (string.IsNullOrEmpty(StringValue))
                return Guid.Empty;

            return new Guid(StringValue.Trim());
        }

        /// <summary>
        /// 将字符串转换为DateTime
        /// </summary>
        /// <param name="StringValue"></param>
        /// <returns>当转换失败时返回0</returns>
        public static DateTime ToDateTime(this string StringValue)
        {
            DateTime date;
            DateTime.TryParse(StringValue, out date);
            return date;
        }

        /// <summary>
        /// 将字符串转换成枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StringValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string StringValue)
        {
            return (T)Enum.Parse(typeof(T), StringValue);
        }
        #endregion

        #region Byte
        /// <summary>
        /// 将字符串转换成枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ByteValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this byte ByteValue)
        {
            return (T)Enum.ToObject(typeof(T), ByteValue);
        }
        #endregion

        #region Decimal
        /// <summary>
        /// 将字符串转换为string
        /// </summary>
        /// <param name="DecimalValue"></param>
        /// <returns>当转换失败时返回0</returns>
        public static string ToMoneyString(this decimal DecimalValue)
        {
            return DecimalValue.ToString("n2");
        }

        /// <summary>
        /// 将字符串转换为string
        /// </summary>
        /// <param name="DoubleValue"></param>
        /// <returns>当转换失败时返回0</returns>
        public static double ToDouble(this decimal DecimalValue)
        {
            return Convert.ToDouble(DecimalValue);
        }

        /// <summary>
        /// 将字符串转换成枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="DecimalValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this decimal DecimalValue)
        {
            return (T)Enum.ToObject(typeof(T), DecimalValue);
        }
        #endregion

        #region Int
        /// <summary>
        /// 将字符串转换为string
        /// </summary>
        /// <param name="IntValue"></param>
        /// <returns>当转换失败时返回0</returns>
        public static string ToMoneyString(this int IntValue)
        {
            return IntValue.ToString("n2");
        }

        /// <summary>
        /// 将字符串转换成枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="IntValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int IntValue)
        {
            return (T)Enum.ToObject(typeof(T), IntValue);
        }

        /// <summary>
        /// 将时间戳转换为DateTime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this int timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.AddSeconds(timeStamp);
        }
        #endregion

        #region Double
        /// <summary>
        /// 将字符串转换为string
        /// </summary>
        /// <param name="DoubleValue"></param>
        /// <returns>当转换失败时返回0</returns>
        public static string ToMoneyString(this double DoubleValue)
        {
            return DoubleValue.ToString("n2");
        }

        /// <summary>
        /// 将字符串转换为string
        /// </summary>
        /// <param name="DoubleValue"></param>
        /// <returns>当转换失败时返回0</returns>
        public static decimal ToDecimal(this double DoubleValue)
        {
            return Convert.ToDecimal(DoubleValue);
        }

        /// <summary>
        /// 将字符串转换成枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="DoubleValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this double DoubleValue)
        {
            return (T)Enum.ToObject(typeof(T), DoubleValue);
        }
        #endregion

        #region Float
        /// <summary>
        /// 将字符串转换为string
        /// </summary>
        /// <param name="FloatValue"></param>
        /// <returns>当转换失败时返回0</returns>
        public static string ToMoneyString(this float FloatValue)
        {
            return FloatValue.ToString("n2");
        }

        /// <summary>
        /// 将字符串转换成枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FloatValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this float FloatValue)
        {
            return (T)Enum.ToObject(typeof(T), FloatValue);
        }
        #endregion

        #region DateTime
        /// <summary>
        /// 将字符串转换为string
        /// </summary>
        /// <param name="t"></param>
        /// <returns>当转换失败时返回0</returns>
        public static string ToTimeSamp(this DateTime value)
        {
            if (value <= System.Data.SqlTypes.SqlDateTime.MinValue.Value)
            {
                return "";
            }
            else
            {
                return value.ToString("yyyy-MM-dd HH:mm");
            }
        }

        /// <summary>
        /// 将字符串转换为string
        /// </summary>
        /// <param name="t"></param>
        /// <returns>当转换失败时返回0</returns>
        public static string ToDateTimeString(this DateTime value)
        {
            if (value <= System.Data.SqlTypes.SqlDateTime.MinValue.Value)
            {
                return "";
            }
            else
            {
                return value.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 将字符串转换为string
        /// </summary>
        /// <param name="t"></param>
        /// <returns>当转换失败时返回0</returns>
        public static string ToDateString(this DateTime value)
        {
            if (value <= System.Data.SqlTypes.SqlDateTime.MinValue.Value)
            {
                return "";
            }
            else
            {
                return value.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 将字符串转换为string
        /// </summary>
        /// <param name="t"></param>
        /// <returns>当转换失败时返回0</returns>
        public static string ToDateCNString(this DateTime value)
        {
            if (value <= System.Data.SqlTypes.SqlDateTime.MinValue.Value)
            {
                return "";
            }
            else
            {
                return value.ToString("yyyy年MM月dd日");
            }
        }
        /// <summary>
        /// To Min DateTime String
        /// </summary>
        public static string ToMinDateTimeString(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd 00:00:00");
        }

        /// <summary>
        /// To Max DateTime String
        /// </summary>
        public static string ToMaxDateTimeString(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd 23:59:59");
        }

        /// <summary>
        /// 将日期转换成时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ToTimeStamp(this DateTime time)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - dtStart).TotalSeconds;
        }


        public static long ToTimeSam(this DateTime value)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (value.Ticks - startTime.Ticks) / 10000;  //除10000调整为13位   
            return t;
        }

        /// <summary>
        /// 时间戳转成时间参数【注：形参已 + 000000】
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(this string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp);
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return dtStart.Add(toNow);
        }
        #endregion

        #region JSON字符串与对象之间
        /// <summary>
        /// 对象转json字符串
        /// </summary>
        /// <typeparam name="ObjType"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj)
        {
            string s = JsonConvert.SerializeObject(obj);
            return s;
        }
        /// <summary>
        /// 字符串转json对象
        /// </summary>
        /// <typeparam name="ObjType"></typeparam>
        /// <param name="JsonString"></param>
        /// <returns></returns>
        public static ObjType ToJosnObj<ObjType>(this string JsonString) where ObjType : class
        {
            ObjType o = JsonConvert.DeserializeObject<ObjType>(JsonString);
            return o;
        }
        #endregion

        #region List泛型集合与DataTable

        #region DataTableToList
        /// <summary>
        /// DataTableToList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt)
            where T : class, new()
        {
            List<T> ts = new List<T>();
            Type type = typeof(T);
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
        #endregion

        #region 把DataTable的第一行数据转化成对象
        /// <summary>
        /// 把DataTable的第一行数据转化成对象
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> DataTableFirstRowToObj(this System.Data.DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
                throw new Exception("请确保所传DataTable对象参数为空或者没有数据！！！");
            Dictionary<string, object> drDic = new Dictionary<string, object>();
            DataRow dr =  dt.Rows[0];
            foreach (DataColumn dc in dt.Columns)
            {
                drDic.Add(dc.ColumnName, dr[dc.ColumnName]);
            }
            return drDic;
        }
        #endregion

        #region 泛型集合转DataTable
        /// <summary>
        /// 泛型集合转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                return new DataTable();
            }

            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable("dt");
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }

            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);

                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }
        #endregion
        
        #endregion
    }
}
