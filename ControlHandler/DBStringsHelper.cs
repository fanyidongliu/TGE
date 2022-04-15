using LK.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK.Tool.ControlHandler
{
    public class DBStringsHelper
    {
        public static List<LinkModelInfo> DBLink { get; set; }

        /// <summary>
        /// 保存当前界面的配置数据到数据库
        /// </summary>
        public static void SaveDBStr()
        {
            string fileName = "DbStr.txt"; string content = "";
            string path = Directory.GetCurrentDirectory() + "//" + fileName;
            if (File.Exists(path))
                File.Delete(path);

            if (DBLink == null)
                DBLink = new List<LinkModelInfo>();
            content = DBLink.ToJsonString();
            File.AppendAllText(path, content);
        }

        /// <summary>
        /// 加载数据库数据到内存集合
        /// </summary>
        /// <returns></returns>
        public static List<LinkModelInfo> LoadDBStr()
        {
            string fileName = "DbStr.txt"; string content = "";
            string path = Directory.GetCurrentDirectory() + "//" + fileName;
            if (File.Exists(path))
            {
                content = File.ReadAllText(path);
                DBLink = JsonConvert.DeserializeObject<List<LinkModelInfo>>(content);
            }

            if (DBLink == null)
                DBLink = new List<LinkModelInfo>();
            return DBLink;
        }


        /// <summary>
        /// 获取当前使用配置数据加载到内存集合
        /// </summary>
        public static void Add()
        {
            var info = GetMap();
            if (DBLink.Where(c => (c.Type.ToString() + "." + c.Host) == info.Type.ToString() + "." + info.Host).Any()) return;
            DBLink.Add(info);
        }

        private static LinkModelInfo GetMap()
        {
            LinkModelInfo info = new LinkModelInfo();
            info.LinkString = LinkModel.LinkString;
            info.Type = LinkModel.Type;
            info.Title = LinkModel.Title;
            info.Account = LinkModel.Account;
            info.Password = LinkModel.Password;
            info.Host = LinkModel.Host;
            info.DBName = LinkModel.DBName;
            info.Account = LinkModel.Account;
            return info;
        }
    }
}
