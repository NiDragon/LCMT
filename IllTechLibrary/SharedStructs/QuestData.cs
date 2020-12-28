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
    public class QuestData : SSClass
    {
        public QuestData()
        {
        }

        public QuestData(List<Object> MembData) : base(MembData) { }

        public int a_index;

        [Locale("usa")]
        public String a_name_usa;
        [Locale("usa")]
        public String a_desc_usa;
        [Locale("usa")]
        public String a_desc2_usa;
        [Locale("usa")]
        public String a_desc3_usa;

        [Locale("thai")]
        public String a_name_thai;
        [Locale("thai")]
        public String a_desc_thai;
        [Locale("thai")]
        public String a_desc2_thai;
        [Locale("thai")]
        public String a_desc3_thai;

        public sbyte a_type1;
        public sbyte a_type2;

        public int a_enable;

        public int a_prequest_num;

        public sbyte a_start_type;

        public int a_start_data;
        public sbyte a_start_npc_zone_num;
        public int a_prize_npc;
        public sbyte a_prize_npc_zone_num;

        public int a_need_exp;
        public int a_need_min_level;
        public int a_need_max_level;

        public sbyte a_need_job;

        public int a_need_item0;
        public int a_need_item1;
        public int a_need_item2;
        public int a_need_item3;
        public int a_need_item4;

        public int a_need_item_count0;
        public int a_need_item_count1;
        public int a_need_item_count2;
        public int a_need_item_count3;
        public int a_need_item_count4;

        public int a_need_rvr_type;
        public int a_need_rvr_grade;

        public sbyte a_condition0_type;
        public sbyte a_condition1_type;
        public sbyte a_condition2_type;

        public int a_condition0_index;
        public int a_condition1_index;
        public int a_condition2_index;

        public int a_condition0_num;
        public int a_condition1_num;
        public int a_condition2_num;

        public int a_condition0_data0;
        public int a_condition0_data1;
        public int a_condition0_data2;
        public int a_condition0_data3;

        public int a_condition1_data0;
        public int a_condition1_data1;
        public int a_condition1_data2;
        public int a_condition1_data3;

        public int a_condition2_data0;
        public int a_condition2_data1;
        public int a_condition2_data2;
        public int a_condition2_data3;

        public sbyte a_prize_type0;
        public sbyte a_prize_type1;
        public sbyte a_prize_type2;
        public sbyte a_prize_type3;
        public sbyte a_prize_type4;

        public int a_prize_index0;
        public int a_prize_index1;
        public int a_prize_index2;
        public int a_prize_index3;
        public int a_prize_index4;

        public long a_prize_data0;
        public long a_prize_data1;
        public long a_prize_data2;
        public long a_prize_data3;
        public long a_prize_data4;

        public int a_option_prize;

        public sbyte a_opt_prize_type0;
        public sbyte a_opt_prize_type1;
        public sbyte a_opt_prize_type2;
        public sbyte a_opt_prize_type3;
        public sbyte a_opt_prize_type4;
        public sbyte a_opt_prize_type5;
        public sbyte a_opt_prize_type6;

        public int a_opt_prize_index0;
        public int a_opt_prize_index1;
        public int a_opt_prize_index2;
        public int a_opt_prize_index3;
        public int a_opt_prize_index4;
        public int a_opt_prize_index5;
        public int a_opt_prize_index6;

        public int a_opt_prize_data0;
        public int a_opt_prize_data1;
        public int a_opt_prize_data2;
        public int a_opt_prize_data3;
        public int a_opt_prize_data4;
        public int a_opt_prize_data5;
        public int a_opt_prize_data6;

        public int a_opt_prize_plus0;
        public int a_opt_prize_plus1;
        public int a_opt_prize_plus2;
        public int a_opt_prize_plus3;
        public int a_opt_prize_plus4;
        public int a_opt_prize_plus5;
        public int a_opt_prize_plus6;

        public int a_only_opt_prize;

        public int a_failvalue;
        public int a_partyscale;

        public String a_start_give_item;
        public int a_start_give_kindcount;
        public String a_start_give_numcount;
        public int a_start_trigger_id;
        public int a_quest_flag;

        public int NeedMinPinalty;
        public int NeedMaxPinalty;
    }
}
