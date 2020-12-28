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
    public class InventoryRowData : SSClass
    {
        public InventoryRowData()
        {
        }

        public InventoryRowData(List<Object> MembData) : base(MembData) { }

        // Owner
        public int a_char_idx;

        // Tab type and row
        public sbyte a_tab_idx;
        public sbyte a_row_idx;

        public int a_item_idx0;
        public int a_plus0;
        public sbyte a_wear_pos0;
        public int a_flag0;
        public string a_serial0;
        public Int64 a_count0;
        public int a_used0;

        public int a_item_idx1;
        public int a_plus1;
        public sbyte a_wear_pos1;
        public int a_flag1;
        public string a_serial1;
        public Int64 a_count1;
        public int a_used1;

        public int a_item_idx2;
        public int a_plus2;
        public sbyte a_wear_pos2;
        public int a_flag2;
        public string a_serial2;
        public Int64 a_count2;
        public int a_used2;

        public int a_item_idx3;
        public int a_plus3;
        public sbyte a_wear_pos3;
        public int a_flag3;
        public string a_serial3;
        public Int64 a_count3;
        public int a_used3;

        public int a_item_idx4;
        public int a_plus4;
        public sbyte a_wear_pos4;
        public int a_flag4;
        public string a_serial4;
        public Int64 a_count4;

        public int a_used4;

        public short a_item0_option0;
        public short a_item0_option1;
        public short a_item0_option2;
        public short a_item0_option3;
        public short a_item0_option4;

        public short a_item1_option0;
        public short a_item1_option1;
        public short a_item1_option2;
        public short a_item1_option3;
        public short a_item1_option4;

        public short a_item2_option0;
        public short a_item2_option1;
        public short a_item2_option2;
        public short a_item2_option3;
        public short a_item2_option4;

        public short a_item3_option0;
        public short a_item3_option1;
        public short a_item3_option2;
        public short a_item3_option3;
        public short a_item3_option4;

        public short a_item4_option0;
        public short a_item4_option1;
        public short a_item4_option2;
        public short a_item4_option3;
        public short a_item4_option4;

        public int a_used0_2;
        public int a_used1_2;
        public int a_used2_2;
        public int a_used3_2;
        public int a_used4_2;

        public string a_socket0;
        public string a_socket1;
        public string a_socket2;
        public string a_socket3;
        public string a_socket4;

        public short a_item_0_origin_var0;
        public short a_item_0_origin_var1;
        public short a_item_0_origin_var2;
        public short a_item_0_origin_var3;
        public short a_item_0_origin_var4;
        public short a_item_0_origin_var5;

        public short a_item_1_origin_var0;
        public short a_item_1_origin_var1;
        public short a_item_1_origin_var2;
        public short a_item_1_origin_var3;
        public short a_item_1_origin_var4;
        public short a_item_1_origin_var5;

        public short a_item_2_origin_var0;
        public short a_item_2_origin_var1;
        public short a_item_2_origin_var2;
        public short a_item_2_origin_var3;
        public short a_item_2_origin_var4;
        public short a_item_2_origin_var5;

        public short a_item_3_origin_var0;
        public short a_item_3_origin_var1;
        public short a_item_3_origin_var2;
        public short a_item_3_origin_var3;
        public short a_item_3_origin_var4;
        public short a_item_3_origin_var5;

        public short a_item_4_origin_var0;
        public short a_item_4_origin_var1;
        public short a_item_4_origin_var2;
        public short a_item_4_origin_var3;
        public short a_item_4_origin_var4;
        public short a_item_4_origin_var5;

        public ushort a_now_dur_0;
        public ushort a_max_dur_0;

        public ushort a_now_dur_1;
        public ushort a_max_dur_1;
    
        public ushort a_now_dur_2;
        public ushort a_max_dur_2;

        public ushort a_now_dur_3;
        public ushort a_max_dur_3;

        public ushort a_now_dur_4;
        public ushort a_max_dur_4;
    }
}
