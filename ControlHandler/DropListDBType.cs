using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LK.Tool.ControlHelper
{
    public class DBItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
    }
    public class DropListDBTypeHelper
    {
        private static object locker = new object();
        public static ComboBox dbList;

        public static void Add(List<DBItem> list)
        {
            if (list == null || list.Count <= 0)
                return;
            lock (locker)
            {
                dbList.BeginInvoke(new Action(() =>
                {
                    dbList.Items.Clear();
                    dbList.Items.AddRange(list.Select(c => c.name).ToArray());
                    dbList.SelectedIndex = 0;
                    dbList.SelectedIndex = 0;
                }));
            }

        }
    }
}
