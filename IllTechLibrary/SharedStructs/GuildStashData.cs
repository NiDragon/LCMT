using IllTechLibrary.Attributes;
using IllTechLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary.SharedStructs
{
    public class GuildStashData
    {
        public GuildStashData()
        {
        }

        public GuildStashData(List<Object> MembData)
        {
            int lastIndex = 0;

            List<FieldInfo> info = this.GetType().GetFields().ToList();

            try
            {
                for (int i = 0; i < info.Count(); i++)
                {
                    lastIndex = i;

                    if (Attribute.IsDefined(info[i], typeof(LocaleAttribute)))
                    {
                        if (((LocaleAttribute)Attribute.GetCustomAttribute(info[i],
                        typeof(LocaleAttribute))) != Core.LangCode)
                        {
                            info.RemoveAt(i);
                            i--;
                            continue;
                        }
                    }

                    info[i].SetValue(this, MembData[i]);
                }
            }
            catch (Exception e)
            {
                String message = e.Message;
                MsgDialogs.Show("Exception!", String.Format("{0}\nEntry Name: {1}", e.Message, info[lastIndex].Name), "ok", MsgDialogs.MsgTypes.ERROR);
            }
        }

        public int a_index;
        public int a_guild_idx;
        public int a_item_idx;
        public int a_plus;
        public int a_flag;
        public string a_serial;
        public Int64 a_count;
        public int a_used;

        public short a_item_option0;
        public short a_item_option1;
        public short a_item_option2;
        public short a_item_option3;
        public short a_item_option4;

        public int a_used_2;

        public string a_socket;

        public short a_item_origin_0;
        public short a_item_origin_1;
        public short a_item_origin_2;
        public short a_item_origin_3;
        public short a_item_origin_4;
        public short a_item_origin_5;

        public short a_now_dur;
        public short a_max_dur;
    }
}
