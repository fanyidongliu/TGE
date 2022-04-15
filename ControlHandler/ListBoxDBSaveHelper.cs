using LK.Tool.ControlHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LK.Tool.Dto
{
    public class ListBoxDBHelper
    {
        private static object locker = new object();
        public static ListBox box { get; set; }

        /// <summary>
        /// 刷新数据库连接字符串集合
        /// </summary>
        /// <param name="list"></param>
        public static void Brush(List<LinkModelInfo> list)
        {
            if (list == null || list.Count <= 0)
                return;

            lock (locker)
            {
                box.BeginInvoke(new Action(() =>
                {
                    box.Items.Clear();
                    List<string> listStr = list.Select(c => c.Type.ToString() + "." + c.Host).ToList();
                    box.Items.AddRange(listStr.ToArray());
                }));
            }
        }

        /// <summary>
        /// 获取选中的数据库连接字符串对象
        /// </summary>
        /// <returns></returns>
        public static LinkModelInfo GetSelectLinkModel()
        {
            var selectItem = box.SelectedItem.ToString();
            var selectLinkModelInfo = DBStringsHelper.DBLink.Where(c => (c.Type.ToString() + "." + c.Host) == selectItem).FirstOrDefault();
            return selectLinkModelInfo;
        }
    }

}
