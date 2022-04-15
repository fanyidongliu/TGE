using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LK.Common
{
    public static class EncryHelper
    {
        #region MD5
        public static class Md5Uty
        {
            //MD5不可逆加密 
            //32位加密 
            public static string GetMD5_32(string str)
            {
                byte[] result = Encoding.Default.GetBytes(str);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] output = md5.ComputeHash(result);
                return BitConverter.ToString(output).Replace("-", "");
            }
            //16位加密 
            public static string GetMd5_16(string ConvertString)
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
                t2 = t2.Replace("-", "");
                return t2;
            }
        }
        #endregion
    }
}
