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
    public class Title : SSClass
    {
        public Title()
        {
        }

        public Title(List<Object> MembData) : base(MembData) { }

        public int a_index;
        public String a_name = String.Empty;
        public int a_enable = 0;
        public String a_describe = String.Empty;
        public String a_effect_name = String.Empty;
        public String a_attack = String.Empty;
        public String a_damage = String.Empty;
        public int a_time = 0;
        public String a_bgcolor = "C0C0C0FF";
        public String a_color = "FF8000FF";
        public int a_option_index0 = -1;
        public int a_option_level0 = 0;
        public int a_option_index1 = -1;
        public int a_option_level1 = 0;
        public int a_option_index2 = -1;
        public int a_option_level2 = 0;
        public int a_option_index3 = -1;
        public int a_option_level3 = 0;
        public int a_option_index4 = -1;
        public int a_option_level4 = 0;
        public int a_item_index = 0;
        public uint a_flag = 0;
        public uint a_castle_num = 0;
    }
}
