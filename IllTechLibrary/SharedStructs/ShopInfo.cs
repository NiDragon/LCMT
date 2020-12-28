using IllTechLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary.SharedStructs
{
    public class ShopInfo
    {   
        public ShopInfo()
        {
        }

        public ShopInfo(List<Object> MembData)
        {
            int lastIndex = 0;

            List<FieldInfo> info = this.GetType().GetFields().ToList();

            try
            {
                for (int i = 0; i < info.Count(); i++)
                {
                    lastIndex = i;

                    info[i].SetValue(this, MembData[i]);
                }
            }
            catch (Exception e)
            {
                String message = e.Message;
                MsgDialogs.Show("Exception!", String.Format("{0}\nEntry Name: {1}", e.Message, info[lastIndex].Name), "ok", IllTechLibrary.Util.MsgDialogs.MsgTypes.ERROR);
            }
        }

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

    public class ShopItem
    {
        public ShopItem()
        {
        }

        public ShopItem(List<Object> MembData)
        {
            FieldInfo[] info = this.GetType().GetFields();

            try
            {
                for (int i = 0; i < info.Count(); i++)
                {
                    info[i].SetValue(this, MembData[i]);
                }
            }
            catch (Exception e)
            {
                String message = e.Message;
                MsgDialogs.Show("Exception!", e.Message, "ok", IllTechLibrary.Util.MsgDialogs.MsgTypes.ERROR);
            }
        }

        private List<String> nameList = new List<String>();

        public int a_keeper_idx;
        public int a_item_idx;
        public UInt64 a_national;
    }
}
