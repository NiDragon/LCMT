using IllTechLibrary.Controls;
using IllTechLibrary.Serialization;
using IllTechLibrary.SharedStructs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IllTechLibrary.DataFiles
{
    public class ItemCache
    {
        private static ItemListItem[] maps = new ItemListItem[0];

        private static int ToBeLoaded = -1;

        private static String LocalNameString
        {
            get { return String.Format("a_name_{0}", Core.LangCode); }
        }

        public static void PreloadDropItems(IReadOnlyCollection<DropItem> items)
        {
            if (maps.Count() != items.Count)
            {
                maps = new ItemListItem[items.Count];
            }
            else
            {
                return;
            }

            ToBeLoaded = items.Count;

            DoLoad_DropItem(items, 0, items.Count);
        }

        public static void PreloadItems(IReadOnlyCollection<Item> items)
        {
            if (maps.Count() != items.Count)
            {
                maps = new ItemListItem[items.Count];
            }
            else
            {
                return;
            }

            ToBeLoaded = items.Count;

            DoLoad_Item(items, 0, items.Count);
        }

        public static void DoLoad_DropItem(object data, int startIdx, int endIdx)
        {
            List<DropItem> refData = (List<DropItem>)data;

            Parallel.For(startIdx, endIdx, i =>
            //for (int i = (int)startIdx; i < (int)endIdx; i++)
            {
                Deserialize<DropItem> dep = new Deserialize<DropItem>("t_item");

                dep.SetData(refData[i]);

                maps[i].a_index = refData[i].a_index;
                //maps[i].Icon = (Bitmap)IconCache.GetItemIcon(refData[i].a_texture_id, refData[i].a_texture_row, refData[i].a_texture_col);
                maps[i].id = refData[i].a_texture_id;
                maps[i].row = refData[i].a_texture_row;
                maps[i].col = refData[i].a_texture_col;
                maps[i].Text = $"{refData[i].a_index} - {dep[LocalNameString]}";

                Interlocked.Decrement(ref ToBeLoaded);
            });
        }

        public static void DoLoad_Item(object data, int startIdx, int endIdx)
        {
            List<Item> refData = (List<Item>)data;

            //for (int i = (int)startIdx; i < (int)endIdx; i++)
            Parallel.For(startIdx, endIdx, i =>
            {
                Deserialize<Item> dep = new Deserialize<Item>("t_item");

                dep.SetData(refData[i]);

                maps[i].a_index = refData[i].a_index;
                //maps[i].Icon = (Bitmap)IconCache.GetItemIcon((sbyte)refData[i].a_texture_id, (sbyte)refData[i].a_texture_row, (sbyte)refData[i].a_texture_col);
                maps[i].id = refData[i].a_texture_id;
                maps[i].row = refData[i].a_texture_row;
                maps[i].col = refData[i].a_texture_col;
                maps[i].Text = $"{refData[i].a_index} - {dep[LocalNameString]}";
         
                Interlocked.Decrement(ref ToBeLoaded);
            });
        }

        public static bool IsLoading()
        {
            return ToBeLoaded > 0;
        }

        public static void OrderArray()
        {
            maps = maps.ToList().OrderBy(item => item.a_index).ToArray();
        }

        public static ItemListItem[] GetItems()
        {
            return maps;
        }

        public static void Clear()
        {
            maps = new ItemListItem[0];
            ToBeLoaded = -1;

            Dialogs.ItemSelector.Instance().Destroy();

            GC.Collect();
        }
    }
}
