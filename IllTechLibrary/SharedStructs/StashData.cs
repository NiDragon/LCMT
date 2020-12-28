using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using IllTechLibrary.Attributes;
using IllTechLibrary.Util;

namespace IllTechLibrary.SharedStructs
{
    public class StashData : SSClass
    {
        public StashData()
        {
        }

        public StashData(List<Object> MembData) : base(MembData) { }

        public int a_index;
        public int a_user_idx;
        public int a_item_idx;
        public int a_plus;
        public sbyte a_wear_pos;
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

        public short a_item_origin_var0;
        public short a_item_origin_var1;
        public short a_item_origin_var2;
        public short a_item_origin_var3;
        public short a_item_origin_var4;
        public short a_item_origin_var5;

        public ushort a_now_dur;
        public ushort a_max_dur;
    }

    public class StashMoney : SSClass
    {
        public StashMoney()
        {
        }

        public StashMoney(List<Object> MembData) : base(MembData) { }

        public uint a_user_index;
        public UInt64 a_stash_money;
    }
}
