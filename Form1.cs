using LK.Common;
using LK.Tool.ControlHandler;
using LK.Tool.ControlHelper;
using LK.Tool.Dto;
using Newtonsoft.Json;
using SqlSugar.Tools.Model;
using SqlSugar.Tools.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LK.Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Init();
        }
        public void Init()
        {
            var enums = Enum.GetNames(typeof(DataBaseType)).ToList();
            enums.ForEach(c => { cbb_DBType.Items.Add(c); });
            cbb_DBType.SelectedIndex = 1;

            DropListDBTypeHelper.dbList = this.cbb_DB;
            ListBoxDBSaveHelper.box = this.lb_Tables;
            ListBoxDBSaveHelper.lb_Count = this.lb_Count;
            ListBoxDBHelper.box = this.listBox1;
        }
        public void GetUserSet()
        {
            LinkModel.Account = tb_UserName.Text.Trim();
            LinkModel.Password = tb_Pwd.Text.Trim();
            LinkModel.Host = tb_IP.Text.Trim();
            LinkModel.Type = cbb_DBType.Text.ToEnum<DataBaseType>();
            LinkModel.DBName = cbb_DB.Text.Trim();
            LinkModel.Port = GetPort();
        }
        public void SetUserSetShowUI(LinkModelInfo info)
        {
            tb_UserName.Text = info.Account;
            tb_Pwd.Text = info.Password;
            tb_IP.Text = info.Host;
            cbb_DBType.Text = info.Type.ToString();
            cbb_DB.Text = info.DBName;
            info.Port = GetPort();
        }
        int GetPort()
        {
            try
            {
                if (!this.tbPort.Text.Trim().IsNullOrEmpty())
                    return Convert.ToInt32(this.tbPort.Text.Trim());
                else if (DataBaseType.MySQL == LinkModel.Type)
                    return 3306;
                else if (DataBaseType.SQLServer == LinkModel.Type)
                    return 1433;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }


        private async void btn_LoadDB_Click(object sender, EventArgs e)
        {
            if (this.cbb_DB.Text.IsNullOrEmpty())
            {
                MessageBox.Show("请先选择数据库");
                return;
            }
            GetUserSet();
            LinkModel.GetDBString();
            DBSelectIsTrue();

            #region 数据库连接字符串对象
            //加载界面数据到内存集合
            DBStringsHelper.Add();
            //内存集合渲染
            ListBoxDBHelper.Brush(DBStringsHelper.DBLink);
            #endregion



            await Task.Run(async () =>
            {
                string tablesJson = "";
                if (LinkModel.Type == DataBaseType.SQLServer)
                {
                    var tables = await this.LoadingTables(LinkModel.LinkString, DataBaseType.SQLServer);
                    tables.Columns["TableName"].ColumnName = "label";
                    tablesJson = JsonConvert.SerializeObject(tables).Replace("\r\n", "").Replace("\\r\\n", "").Replace("\\", "\\\\");
                    tables.Clear(); tables.Dispose(); tables = null;
                }
                else if (LinkModel.Type == DataBaseType.MySQL)
                {
                    var tables = await this.LoadingTables(LinkModel.LinkString, DataBaseType.MySQL);
                    tables.Columns["TableName"].ColumnName = "label";
                    tablesJson = JsonConvert.SerializeObject(tables).Replace("\r\n", "").Replace("\\r\\n", "").Replace("\\", "\\\\");
                    tables.Clear(); tables.Dispose(); tables = null;
                }
                dbtables = tablesJson.ToJosnObj<List<TableDto>>();
                ListBoxDBSaveHelper.Add(dbtables);
            });

            ShowMsg();
        }

        private List<TableDto> dbtables;

        private async Task<DataTable> LoadingTables(string linkString, DataBaseType type)
        {
            switch (type)
            {
                case DataBaseType.SQLServer:
                    var sql = @"select name as TableName, ISNULL(j.TableDesc, '') as TableDesc  From sysobjects g
left join
(
select * from
(SELECT 
    TableName       = case when a.colorder=1 then d.name else '' end,
    TableDesc     = case when a.colorder=1 then isnull(f.value,'') else '' end
FROM 
    syscolumns a
inner join 
    sysobjects d 
on 
    a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
inner join
sys.extended_properties f
on 
    d.id=f.major_id and f.minor_id=0) t
	where t.TableName!=''
	) j on g.name = j.TableName
	Where g.xtype='U'
	order by TableName ASC";
                    var table1 = await SQLServerHelper.QueryDataTable(linkString, sql);
                    sql = @"select name as TableName,'' as TableDesc   From sysobjects j where j.xtype='V' order by name asc";
                    var table2 = await SQLServerHelper.QueryDataTable(linkString, sql);
                    DataTable newDataTable = table1.Clone();
                    object[] obj = new object[newDataTable.Columns.Count];
                    for (int i = 0; i < table1.Rows.Count; i++)
                    {
                        table1.Rows[i].ItemArray.CopyTo(obj, 0);
                        newDataTable.Rows.Add(obj);
                    }

                    for (int i = 0; i < table2.Rows.Count; i++)
                    {
                        table2.Rows[i].ItemArray.CopyTo(obj, 0);
                        newDataTable.Rows.Add(obj);
                    }

                    return newDataTable;
                case DataBaseType.MySQL:
                    var database = linkString.Substring(linkString.IndexOf("Database=") + 9, linkString.IndexOf(";port=") - linkString.IndexOf("Database=") - 9);
                    var sql1 = $"SELECT TABLE_NAME as TableName, Table_Comment as TableDesc FROM INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA = '{database}' order by TableName asc";
                    return await MySQLHelper.QueryDataTable(linkString, sql1);

                default:
                    return null;
            }
        }

        void DBSelectIsTrue()
        {
            var dbType = this.cbb_DBType.Text.ToEnum<DataBaseType>();
            if (dbType == DataBaseType.MySQL)
            {
                if (!LinkModel.LinkString.Contains("SslMode"))
                {
                    MessageBox.Show("数据库类型选择有问题");
                }
            }
            else if (dbType == DataBaseType.SQLServer)
            {
                if (!LinkModel.LinkString.Contains("Initial Catalog"))
                {
                    MessageBox.Show("数据库类型选择有问题");
                }
            }
            else
            {

            }
        }
        void ShowMsg()
        {
            string currentDB = LinkModel.Type.ToString() + "   服务器： " + LinkModel.Host;
            this.Text = "TGE:    " + currentDB;
        }
        private async void btn_TestConnect_Click(object sender, EventArgs e)
        {
            await LoadAllDb();
        }

        private async Task LoadAllDb()
        {
            try
            {
                //获取界面配置
                GetUserSet();
                //拼接字符串
                LinkModel.GetConnectString();
                DBSelectIsTrue();
                await Task.Run(async () =>
                {
                    List<DBItem> listDB = null;
                    if (LinkModel.Type == DataBaseType.SQLServer)
                    {
                        if (!string.IsNullOrWhiteSpace(LinkModel.LinkString))
                        {
                            if (await SQLServerHelper.TestLink(LinkModel.LinkString))
                            {
                                var dbList = await SQLServerHelper.QueryDataTable(LinkModel.LinkString, "select name from sysdatabases where dbid>4");
                                var dbListJson = JsonConvert.SerializeObject(dbList);
                                dbList.Clear(); dbList.Dispose(); dbList = null;
                                listDB = dbListJson.ToJosnObj<List<DBItem>>();
                            }
                            else
                            {
                                MessageBox.Show("测试连接失败", "测试连接SQL Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if (LinkModel.Type == DataBaseType.MySQL)
                    {
                        if (await MySQLHelper.TestLink(LinkModel.LinkString))
                        {
                            var dbList = await MySQLHelper.QueryDataTable(LinkModel.LinkString, "SELECT `SCHEMA_NAME` as name  FROM `information_schema`.`SCHEMATA` order by `SCHEMA_NAME`");
                            var dbListJson = JsonConvert.SerializeObject(dbList);
                            dbList.Clear(); dbList.Dispose(); dbList = null;
                            listDB = dbListJson.ToJosnObj<List<DBItem>>();
                        }
                        else
                        {
                            MessageBox.Show("测试连接失败", "测试连接MySql", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    DropListDBTypeHelper.Add(listDB);
                });


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ShowMsg();
        }

        private async Task ProduceSQLServerEntityFiles(string path)
        {
            try
            {
                string code = "";
                var ls = lb_Tables.SelectedItems;
                foreach (string item in ls)
                {
                    var TableName = item.Split(',')[0];
                    var TableDesc = item.Split(',')[1];

                    var settings = JsonConvert.DeserializeObject<SettingsModel>(sb);
                    code = await this.GetEntityCode(LinkModel.LinkString, TableName, TableDesc, settings, DataBaseType.SQLServer, false);
                    code = Get(code);
                    using (StreamWriter sw = new StreamWriter(Path.Combine(path, TableName + ".cs"), false))
                    {
                        await sw.WriteLineAsync(code);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存实体类", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GC.Collect();
            }
        }


        private async Task ProduceMySQLEntityFiles(string path)
        {
            try
            {
                string code = "";
                var ls = lb_Tables.SelectedItems;
                foreach (string item in ls)
                {
                    var TableName = item.Split(',')[0];
                    var TableDesc = item.Split(',')[1];

                    var settings = JsonConvert.DeserializeObject<SettingsModel>(sb);
                    code = await this.GetEntityCode(LinkModel.LinkString, TableName, TableDesc, settings, DataBaseType.MySQL, false);
                    code = Get(code);
                    using (StreamWriter sw = new StreamWriter(Path.Combine(path, TableName + ".cs"), false))
                    {
                        await sw.WriteLineAsync(code);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存实体类", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                GC.Collect();
            }
        }

        string sb = "{\"namespace\":\"\",\"entityNamespace\":\"Entitys\",\"baseClassName\":\"\",\"classCapsCount\":0,\"propCapsCount\":0,\"propTrim\":false,\"propDefault\":false,\"sqlSugarPK\":false,\"sqlSugarBZL\":false,\"getCus\":\"return this._属性;\",\"setCus\":\"this._属性 = -value-;\",\"cusAttr\":\"\",\"cusGouZao\":\"\",\"propType\":\"0\"}";
        private async void lb_Tables_DoubleClick(object sender, EventArgs e)
        {
            if (lb_Tables.SelectedItems.Count <= 0)
                return;

            string txt = "";
            if (LinkModel.Type == DataBaseType.SQLServer)
            {
                if (!string.IsNullOrWhiteSpace(LinkModel.LinkString))
                {
                    try
                    {
                        string ls = lb_Tables.SelectedItem.ToString();
                        var settings = JsonConvert.DeserializeObject<SettingsModel>(sb);
                        var TableName = ls.Split(',')[0];
                        var TableDesc = ls.Split(',')[1];
                        var code = await this.GetEntityCode(LinkModel.LinkString, TableName, TableDesc, settings, DataBaseType.SQLServer, true);

                        code = Regex.Replace(code, @"/// <summary>(?<str>.*?)/// </summary>", "<span style=\"color:green\">/// &lt;summary&gt;${str}/// &lt;/summary&gt;</span>");
                        txt = Get(code);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "预览代码", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        GC.Collect();
                    }
                }
                else
                {
                    MessageBox.Show("获取数据库连接字符串错误", "预览代码", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (LinkModel.Type == DataBaseType.MySQL)
            {
                if (!string.IsNullOrWhiteSpace(LinkModel.LinkString))
                {
                    try
                    {
                        string ls = lb_Tables.SelectedItem.ToString();
                        var settings = JsonConvert.DeserializeObject<SettingsModel>(sb);
                        var TableName = ls.Split(',')[0];
                        var TableDesc = ls.Split(',')[1];

                        var code = await GetEntityCode(LinkModel.LinkString, TableName, TableDesc, settings, DataBaseType.MySQL, true);

                        code = Regex.Replace(code, @"/// <summary>(?<str>.*?)/// </summary>", "<span style=\"color:green\">/// &lt;summary&gt;${str}/// &lt;/summary&gt;</span>");
                        txt = Get(code);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "预览代码", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        GC.Collect();
                    }
                }
            }
            else
            {
                MessageBox.Show("获取数据库连接字符串错误", "预览代码", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ListBoxDBSaveHelper.NoSelect();
            richTextBox1.Text = "";
            richTextBox1.Text = txt;
        }


        private async Task<string> GetEntityCode(string linkString, string nodeName, string nodeDesc, SettingsModel settings, DataBaseType type, bool isYuLan)
        {
            StringBuilder codeString = new StringBuilder();
            DataTable tableInfo = null;
            DataTable colsInfos = null;
            if (type == DataBaseType.SQLServer)
            {
                tableInfo = await SQLServerHelper.QueryTableInfo(linkString, $"select * from [{nodeName}] where 1=2");
                colsInfos = await SQLServerHelper.QueryDataTable(linkString, "SELECT objname,value FROM ::fn_listextendedproperty (NULL, 'user', 'dbo', 'table', '" + nodeName + "', 'column', DEFAULT)", null);
                this.GetCode(
                    tableInfo,
                    colsInfos,
                    "OBJNAME",
                    "ColumnName",
                    "VALUE",
                    "IsKey",
                    "IsIdentity",
                    "DataType",
                    "AllowDBNull",
                    linkString,
                    nodeName,
                    nodeDesc,
                    settings,
                    isYuLan,
                    codeString);
            }
            else if (type == DataBaseType.MySQL)
            {
                var database = linkString.Substring(linkString.IndexOf("Database=") + 9, linkString.IndexOf(";port=") - linkString.IndexOf("Database=") - 9);
                tableInfo = await MySQLHelper.QueryTableInfo(linkString, $"select * from `{nodeName}` where 1=2");
                colsInfos = await MySQLHelper.QueryDataTable(linkString, $"select COLUMN_NAME as OBJNAME,column_comment as VALUE from INFORMATION_SCHEMA.Columns where table_name='{nodeName}' and table_schema='{database}'", null);
                this.GetCode(
                    tableInfo,
                    colsInfos,
                    "OBJNAME",
                    "ColumnName",
                    "VALUE",
                    "IsKey",
                    "IsAutoIncrement",
                    "DataType",
                    "AllowDBNull",
                    linkString,
                    nodeName,
                    nodeDesc,
                    settings,
                    isYuLan,
                    codeString);
            }
            tableInfo?.Clear();
            tableInfo?.Dispose();
            colsInfos?.Clear();
            colsInfos?.Dispose();
            GC.Collect();
            return codeString.ToString();
        }

        Type GetTypeByString(string type)
        {
            switch (type.ToLower())
            {
                case "system.boolean":
                    return Type.GetType("System.Boolean", true, true);
                case "system.byte":
                    return Type.GetType("System.Byte", true, true);
                case "system.sbyte":
                    return Type.GetType("System.SByte", true, true);
                case "system.char":
                    return Type.GetType("System.Char", true, true);
                case "system.decimal":
                    return Type.GetType("System.Decimal", true, true);
                case "system.double":
                    return Type.GetType("System.Double", true, true);
                case "system.single":
                    return Type.GetType("System.Single", true, true);
                case "system.int32":
                    return Type.GetType("System.Int32", true, true);
                case "system.uint32":
                    return Type.GetType("System.UInt32", true, true);
                case "system.int64":
                    return Type.GetType("System.Int64", true, true);
                case "system.uint64":
                    return Type.GetType("System.UInt64", true, true);
                case "system.object":
                    return Type.GetType("System.Object", true, true);
                case "system.int16":
                    return Type.GetType("System.Int16", true, true);
                case "system.uint16":
                    return Type.GetType("System.UInt16", true, true);
                case "system.string":
                    return Type.GetType("System.String", true, true);
                case "system.datetime":
                    return Type.GetType("System.DateTime", true, true);
                case "system.guid":
                    return Type.GetType("System.Guid", true, true);
                default:
                    return Type.GetType(type, true, true);
            }
        }
        /// <summary>
        /// 获得实体类代码
        /// </summary>
        /// <param name="tableInfo">表信息</param>
        /// <param name="colsInfos">列信息</param>
        /// <param name="objname">从列信息DataTabel中取列名的key</param>
        /// <param name="columnName">从表信息DataTabel中取列名的key</param>
        /// <param name="zhuShiValueName">从列信息DataTabel中取列注释的key</param>
        /// <param name="isKeyName">从表信息DataTabel中取列名是不是主键的key</param>
        /// <param name="isIdentityName">从表信息DataTabel中取列是不是自增的key</param>
        /// <param name="dataTypeName">从表信息DataTabel中取列名数据类型的key</param>
        /// <param name="allowDBNullName">从表信息DataTabel中取列名是不是允许为null的key</param>
        /// <param name="linkString">连接字符串</param>
        /// <param name="nodeName">表名</param>
        /// <param name="nodeDesc">表注释</param>
        /// <param name="settings">设置信息</param>
        /// <param name="isYuLan">是否是预览</param>
        /// <param name="codeString"></param>
        /// <returns></returns>
        private void GetCode(
            DataTable tableInfo,
            DataTable colsInfos,
            string objname,
            string columnName,
            string zhuShiValueName,
            string isKeyName,
            string isIdentityName,
            string dataTypeName,
            string allowDBNullName,
            string linkString,
            string nodeName,
            string nodeDesc,
            SettingsModel settings,
            bool isYuLan,
            StringBuilder codeString)
        {
            string tableName = (settings.ClassCapsCount > 0 ? nodeName.SetLengthToUpperByStart((int)settings.ClassCapsCount) : nodeName);
            codeString.Append($@"using System;{(string.IsNullOrWhiteSpace(settings.Namespace) ? "" : $"{Environment.NewLine}{settings.Namespace.Trim()}")}

namespace {settings.EntityNamespace.Trim()}
{{
    /// <summary>
    /// {nodeDesc}
    /// </summary>{(string.IsNullOrWhiteSpace(settings.CusAttr) ? "" : $"{Environment.NewLine}    {settings.CusAttr.Trim()}")}
    public class {tableName}{(string.IsNullOrWhiteSpace(settings.BaseClassName) ? "" : $" : {settings.BaseClassName.Trim()}")}
    {{
        /// <summary>
        /// {nodeDesc}
        /// </summary>
        public {tableName}()
        {{{(string.IsNullOrWhiteSpace(settings.CusGouZao) ? "" : Environment.NewLine + "          " + settings.CusGouZao.Trim().Replace("-tableName-", isYuLan ? $"<span style=\"color:yellow\">{tableName}</span>" : tableName))}
        }}
");
            if (settings.PropType == PropType.Easy)  //建议模式, 属性只生成get; set; 属性自定义模版失效
            {
                foreach (DataRow dr in tableInfo.Rows)
                {
                    var zhuShi = string.Empty;//列名注释
                    foreach (DataRow uu in colsInfos.Rows)
                    {
                        if (uu[objname].ToString().ToUpper() == dr[columnName].ToString().ToUpper())
                            zhuShi = uu[zhuShiValueName].ToString();
                    }
                    if ((bool)dr[isKeyName] && !(bool)dr[isIdentityName])
                    {
                        if (settings.SqlSugarPK)
                        {
                            codeString.Append($@"
        /// <summary>
        /// -zhuShi-
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public -dbType- -colName- {{ get; set; }}
");
                        }
                        else
                        {
                            codeString.Append($@"
        /// <summary>
        /// -zhuShi-
        /// </summary>
        public -dbType- -colName- {{ get; set; }}
");
                        }
                    }
                    else if ((bool)dr[isKeyName] && (bool)dr[isIdentityName])
                    {
                        if (settings.SqlSugarPK && settings.SqlSugarBZL)
                        {
                            codeString.Append($@"
        /// <summary>
        /// -zhuShi-
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public -dbType- -colName- {{ get; set; }}
");
                        }
                        else if (settings.SqlSugarPK && !settings.SqlSugarBZL)
                        {
                            codeString.Append($@"
        /// <summary>
        /// -zhuShi-
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public -dbType- -colName- {{ get; set; }}
");
                        }
                        else if (!settings.SqlSugarPK && settings.SqlSugarBZL)
                        {
                            codeString.Append($@"
        /// <summary>
        /// -zhuShi-
        /// </summary>
        [SugarColumn(IsIdentity = true)]
        public -dbType- -colName- {{ get; set; }}
");
                        }
                        else
                        {
                            codeString.Append($@"
        /// <summary>
        /// -zhuShi-
        /// </summary>
        public -dbType- -colName- {{ get; set; }}
");
                        }
                    }
                    else if (!(bool)dr[isKeyName] && (bool)dr[isIdentityName])
                    {
                        if (settings.SqlSugarBZL)
                        {
                            codeString.Append($@"
        /// <summary>
        /// -zhuShi-
        /// </summary>
        [SugarColumn(IsIdentity = true)]
        public -dbType- -colName- {{ get; set; }}
");
                        }
                        else
                        {
                            codeString.Append($@"
        /// <summary>
        /// -zhuShi-
        /// </summary>
        public -dbType- -colName- {{ get; set; }}
");
                        }
                    }
                    else
                    {
                        codeString.Append($@"
        /// <summary>
        /// -zhuShi-
        /// </summary>
        public -dbType- -colName- {{ get; set; }}
");
                    }
                    Type ttttt = this.GetTypeByString(dr[dataTypeName].ToString());
                    if (ttttt.IsValueType && dr[allowDBNullName].ToString() == "True")
                    {
                        codeString.Replace("-dbType-", isYuLan ? $"{dr[dataTypeName].ToString()}" : dr[dataTypeName].ToString() + "?");  //替换数据类型
                        if (settings.PropDefault)
                        {
                            codeString.Replace("-value-", $"value ?? default({(isYuLan ? $"{dr[dataTypeName].ToString()}" : dr[dataTypeName].ToString())})");
                        }
                        else
                        {
                            codeString.Replace("-value-", "value");
                        }
                    }
                    else if (ttttt.IsValueType)
                    {
                        codeString.Replace("-dbType-", isYuLan ? $"{dr[dataTypeName].ToString()}" : dr[dataTypeName].ToString());  //替换数据类型
                        codeString.Replace("-value-", "value");
                    }
                    else
                    {
                        if (dr[dataTypeName].ToString() == "System.String")
                        {
                            codeString.Replace("-dbType-", isYuLan ? $"{dr[dataTypeName].ToString()}" : dr[dataTypeName].ToString());  //替换数据类型
                            if (settings.PropTrim)
                            {
                                codeString.Replace("-value-", "value?.Trim()");
                            }
                            else
                            {
                                codeString.Replace("-value-", "value");
                            }
                        }
                        else
                        {
                            codeString.Replace("-dbType-", isYuLan ? $"{dr[dataTypeName].ToString()}" : dr[dataTypeName].ToString());  //替换数据类型
                            codeString.Replace("-value-", "value");
                        }
                    }
                    codeString.Replace("-colName-", settings.PropCapsCount > 0 ? dr[columnName].ToString().SetLengthToUpperByStart((int)settings.PropCapsCount) : dr[columnName].ToString());  //替换列名（属性名）
                    codeString.Replace("-zhuShi-", zhuShi.Replace("\r\n", "\r\n        ///"));
                }



            }
            else
            {
                var getString = settings.GetCus.Trim();
                if (string.IsNullOrWhiteSpace(getString))
                {
                    getString = "return this._-colName-;";
                }
                else
                {
                    getString = getString.Replace("属性", "-colName-");
                }
                var setString = settings.SetCus.Trim();
                if (string.IsNullOrWhiteSpace(setString))
                {
                    setString = "this._-colName- = -value-;";
                }
                else
                {
                    setString = setString.Replace("属性", "-colName-");
                }
                foreach (DataRow dr in tableInfo.Rows)
                {
                    var zhuShi = string.Empty;//列名注释
                    foreach (DataRow uu in colsInfos.Rows)
                    {
                        if (uu[objname].ToString().ToUpper() == dr[columnName].ToString().ToUpper())
                            zhuShi = uu[zhuShiValueName].ToString();
                    }
                    if ((bool)dr[isKeyName] && !(bool)dr[isIdentityName])
                    {
                        if (settings.SqlSugarPK)
                        {
                            codeString.Append($@"
        private -dbType- _-colName-;
        /// <summary>
        /// -zhuShi-
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public -dbType- -colName- {{ get {{ {getString} }} set {{ {setString} }} }}
");
                        }
                        else
                        {
                            codeString.Append($@"
        private -dbType- _-colName-;
        /// <summary>
        /// -zhuShi-
        /// </summary>
        public -dbType- -colName- {{ get {{ {getString} }} set {{ {setString} }} }}
");
                        }
                    }
                    else if ((bool)dr[isKeyName] && (bool)dr[isIdentityName])
                    {
                        if (settings.SqlSugarPK && settings.SqlSugarBZL)
                        {
                            codeString.Append($@"
        private -dbType- _-colName-;
        /// <summary>
        /// -zhuShi-
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public -dbType- -colName- {{ get {{ {getString} }} set {{ {setString} }} }}
");
                        }
                        else if (settings.SqlSugarPK && !settings.SqlSugarBZL)
                        {
                            codeString.Append($@"
        private -dbType- _-colName-;
        /// <summary>
        /// -zhuShi-
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public -dbType- -colName- {{ get {{ {getString} }} set {{ {setString} }} }}
");
                        }
                        else if (!settings.SqlSugarPK && settings.SqlSugarBZL)
                        {
                            codeString.Append($@"
        private -dbType- _-colName-;
        /// <summary>
        /// -zhuShi-
        /// </summary>
        [SugarColumn(IsIdentity = true)]
        public -dbType- -colName- {{ get {{ {getString} }} set {{ {setString} }} }}
");
                        }
                        else
                        {
                            codeString.Append($@"
        private -dbType- _-colName-;
        /// <summary>
        /// -zhuShi-
        /// </summary>
        public -dbType- -colName- {{ get {{ {getString} }} set {{ {setString} }} }}
");
                        }
                    }
                    else if (!(bool)dr[isKeyName] && (bool)dr[isIdentityName])
                    {
                        if (settings.SqlSugarBZL)
                        {
                            codeString.Append($@"
        private -dbType- _-colName-;
        /// <summary>
        /// -zhuShi-
        /// </summary>
        [SugarColumn(IsIdentity = true)]
        public -dbType- -colName- {{ get {{ {getString} }} set {{ {setString} }} }}
");
                        }
                        else
                        {
                            codeString.Append($@"
        private -dbType- _-colName-;
        /// <summary>
        /// -zhuShi-
        /// </summary>
        public -dbType- -colName- {{ get {{ {getString} }} set {{ {setString} }} }}
");
                        }
                    }
                    else
                    {
                        codeString.Append($@"
        private -dbType- _-colName-;
        /// <summary>
        /// -zhuShi-
        /// </summary>
        public -dbType- -colName- {{ get {{ {getString} }} set {{ {setString} }} }}
");
                    }
                    Type ttttt = this.GetTypeByString(dr[dataTypeName].ToString());
                    if (ttttt.IsValueType && dr[allowDBNullName].ToString() == "True")
                    {
                        codeString.Replace("-dbType-", isYuLan ? $"<span style=\"color:#23C645\">{dr[dataTypeName].ToString()}?</span>" : dr[dataTypeName].ToString() + "?");  //替换数据类型
                        if (settings.PropDefault)
                        {
                            codeString.Replace("-value-", $"value ?? default({(isYuLan ? $"<span style=\"color:#23C645\">{dr[dataTypeName].ToString()}</span>" : dr[dataTypeName].ToString())})");
                        }
                        else
                        {
                            codeString.Replace("-value-", "value");
                        }
                    }
                    else if (ttttt.IsValueType)
                    {
                        codeString.Replace("-dbType-", isYuLan ? $"<span style=\"color:#23C645\">{dr[dataTypeName].ToString()}</span>" : dr[dataTypeName].ToString());  //替换数据类型
                        codeString.Replace("-value-", "value");
                    }
                    else
                    {
                        if (dr[dataTypeName].ToString() == "System.String")
                        {
                            codeString.Replace("-dbType-", isYuLan ? $"<span style=\"color:red\">{dr[dataTypeName].ToString()}</span>" : dr[dataTypeName].ToString());  //替换数据类型
                            if (settings.PropTrim)
                            {
                                codeString.Replace("-value-", "value?.Trim()");
                            }
                            else
                            {
                                codeString.Replace("-value-", "value");
                            }
                        }
                        else
                        {
                            codeString.Replace("-dbType-", isYuLan ? $"<span style=\"color:red\">{dr[dataTypeName].ToString()}</span>" : dr[dataTypeName].ToString());  //替换数据类型
                            codeString.Replace("-value-", "value");
                        }
                    }
                    codeString.Replace("-colName-", settings.PropCapsCount > 0 ? dr[columnName].ToString().SetLengthToUpperByStart((int)settings.PropCapsCount) : dr[columnName].ToString());  //替换列名（属性名）
                    codeString.Replace("-zhuShi-", zhuShi.Replace("\r\n", "\r\n        ///"));
                }
            }
            codeString.Append(@"    }
}");
        }


        string Get(string code)
        {
            code = code.Replace("System.Boolean", "bool");
            code = code.Replace("System.Byte", "byte");
            code = code.Replace("System.SByte", "sbyte");
            code = code.Replace("System.Char", "char");
            code = code.Replace("System.Decimal", "decimal");
            code = code.Replace("System.Double", "double");
            code = code.Replace("System.Single", "single");
            code = code.Replace("System.Int32", "int");
            code = code.Replace("System.UInt32", "int");
            code = code.Replace("System.Int64", "long");
            code = code.Replace("System.UInt64", "long");
            code = code.Replace("System.Object", "object");
            code = code.Replace("System.Int16", "short");
            code = code.Replace("System.UInt16", "ushort");
            code = code.Replace("System.String", "string");
            code = code.Replace("System.Guid", "Guid");
            if (code.Contains("System.DateTime"))
            {
                if (code.Contains("System.DateTime?"))
                    code = code.Replace("System.DateTime?", "DateTime?");
                else
                    code = code.Replace("System.DateTime", "DateTime?");
            }
            return code;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("客官，麻烦移步github赏个star");
            System.Diagnostics.Process.Start("chrome.exe", "https://github.com/fanyidongliu/TGE");
        }

        private async void 生成文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(System.Environment.CurrentDirectory, "Code");
            Directory.CreateDirectory(path);

            switch (this.cbb_DBType.Text.ToEnum<DataBaseType>())
            {
                case DataBaseType.SQLServer:
                    await ProduceSQLServerEntityFiles(path);
                    break;
                case DataBaseType.MySQL:
                    await ProduceMySQLEntityFiles(path);
                    break;
                default:
                    break;

            }
            System.Diagnostics.Process.Start("Explorer.exe", path);
            ListBoxDBSaveHelper.NoSelect();
        }

        private void NewMethod()
        {
            for (int i = 0; i < this.lb_Tables.Items.Count; i++)
                this.lb_Tables.SetSelected(i, false);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DBStringsHelper.SaveDBStr();
        }




        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string context = tb_Context.Text.Trim();
            var t = dbtables.Where(c => c.label.StartsWith(context)).ToList();
            ListBoxDBSaveHelper.Add(t);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var DBLink = DBStringsHelper.LoadDBStr();
            ListBoxDBHelper.Brush(DBLink);
        }

        private async void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var t = ListBoxDBHelper.GetSelectLinkModel();
            SetUserSetShowUI(t);
            await LoadAllDb();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBStringsHelper.DBLink.RemoveAll(c => (c.Type.ToString() + "." + c.Host) == this.listBox1.SelectedItem.ToString());
            this.listBox1.Items.Remove(this.listBox1.SelectedItem);
            ListBoxDBHelper.Brush(DBStringsHelper.DBLink);
        }
    }
}
