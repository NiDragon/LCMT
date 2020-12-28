﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using IllTechLibrary.Attributes;
using IllTechLibrary.Util;

namespace IllTechLibrary.SharedStructs
{
    public class WearInvenData
    {
        public WearInvenData()
        {
        }

        public WearInvenData(List<Object> MembData)
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
                MsgDialogs.Show("Exception!", String.Format("{0}\nEntry Name: {1}", e.Message, info[lastIndex].Name), "ok", IllTechLibrary.Util.MsgDialogs.MsgTypes.ERROR);
            }
        }

        public int a_char_index;

        public int a_wear_pos;
        public int a_item_idx;
        public int a_plus;
        public int a_flag;

        public string a_serial;

        public int a_used;
        public int a_used2;

        public short a_item_option0;
        public short a_item_option1;
        public short a_item_option2;
        public short a_item_option3;
        public short a_item_option4;

        public short a_item_origin_var0;
        public short a_item_origin_var1;
        public short a_item_origin_var2;
        public short a_item_origin_var3;
        public short a_item_origin_var4;
        public short a_item_origin_var5;

        public short a_socket0;
        public short a_socket1;
        public short a_socket2;
        public short a_socket3;
        public short a_socket4;
        public short a_socket5;
        public short a_socket6;

        public ushort a_now_dur;
        public ushort a_max_dur;
    }
}