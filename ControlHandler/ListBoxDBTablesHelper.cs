using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LK.Tool.Dto
{

    public class TableDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TableDesc { get; set; }
    }

    public class ListBoxDBSaveHelper
    {
        private static object locker = new object();
        public static ListBox box { get; set; }
        public static Label lb_Count { get; set; }

        public static void Add(List<TableDto> list)
        {
            if (list == null || list.Count <= 0)
                return;

            lock (locker)
            {
                box.BeginInvoke(new Action(() =>
                {
                    box.Items.Clear();
                    foreach (var item in list)
                    {
                        box.Items.Add(item.label + "," + item.TableDesc);
                    }
                    lb_Count.Text = "总数" + box.Items.Count;
                }));
            }

        }
        public static void NoSelect()
        {
            lock (locker)
            {
                box.BeginInvoke(new Action(() =>
                {
                    for (int i = 0; i < box.Items.Count; i++)
                        box.SetSelected(i, false);
                }));
            }
        }
    }

}
