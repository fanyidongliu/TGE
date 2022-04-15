using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK.Tool
{
    /// <summary>
    /// 连接信息
    /// </summary>
    [System.Serializable]
    internal class LinkModel
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string LinkString { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DataBaseType Type { get; set; }

        /// <summary>
        /// 连接名称
        /// </summary>
        public static string Title { get; set; }

        /// <summary>
        /// 数据库帐号
        /// </summary>
        public static string Account { get; set; }

        /// <summary>
        /// 数据库密码
        /// </summary>
        public static string Password { get; set; }

        /// <summary>
        /// MySQL端口号
        /// </summary>
        public static int Port { get; set; }

        /// <summary>
        /// 主机地址
        /// </summary>
        public static string Host { get; set; }
        public static string DBName { get; set; }
        public static string TableDesc { get; set; }
        public static string TableName { get; set; }


        private static string LinkMySqlFormat { get; set; } = "server={0};User Id={1};password={2};port={3};SslMode = None;";
        private static string LinkSqlServerFormat { get; set; } = "Data Source={0};Initial Catalog=master;Persist Security Info=True;User ID={1};Password={2}";

        //server=192.168.50.240;User Id=root;password=123456;Database=mes;port=3306;SslMode = None;
        private static string DBMySqlFormat { get; set; } = "server={0};User Id={1};password={2};Database={3};port={4};SslMode = None;";
        private static string DBSqlServerFormat { get; set; } = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";


        public static void GetConnectString()
        {
            switch (Type)
            {
                case DataBaseType.SQLServer:
                    LinkString = string.Format(LinkSqlServerFormat, Host, Account, Password, DBName, Port);
                    break;
                case DataBaseType.MySQL:
                    LinkString = string.Format(LinkMySqlFormat, Host, Account, Password, Port);
                    break;
                default:
                    break;
            }
        }

        public static void GetDBString()
        {
            switch (Type)
            {
                case DataBaseType.SQLServer:
                    LinkString = string.Format(DBSqlServerFormat, Host, DBName, Account, Password, Port);
                    break;
                case DataBaseType.MySQL:
                    LinkString = string.Format(DBMySqlFormat, Host, Account, Password, DBName, Port);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DataBaseType
    {
        SQLServer = 1,
        MySQL = 2
        //Oracler = 3,
        //SQLite = 4,
        //PostgreSQL = 5
    }

    public class LinkModelInfo
    {
        public DateTime CreateTime { get; set; }
        public string LinkString { get; set; }
        public DataBaseType Type { get; set; }
        public string Title { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string DBName { get; set; }
        public string TableDesc { get; set; }
        public string TableName { get; set; }
    }
}
