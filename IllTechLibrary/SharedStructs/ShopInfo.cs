using IllTechLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary.SharedStructs
{
    public class ShopInfo : SSClass
    {   
        public ShopInfo()
        {
        }

        public ShopInfo(List<Object> MembData) : base(MembData) { }

        public int a_keeper_idx;
        public int a_zone_num;
        public String a_name = String.Empty;
        public int a_sell_rate;
        public int a_buy_rate;
        public float a_pos_x;
        public float a_pos_z;
        public float a_pos_h;
        public float a_pos_r;
        public int a_y_layer;
    }

    public class ShopItem : SSClass
    {
        public ShopItem()
        {
        }

        public ShopItem(List<Object> MembData) : base(MembData) { }

        private List<String> nameList = new List<String>();

        public int a_keeper_idx;
        public int a_item_idx;
        public UInt64 a_national;
    }
}
