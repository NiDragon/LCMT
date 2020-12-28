using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using IllTechLibrary.Attributes;
using IllTechLibrary.Settings;
using IllTechLibrary.Util;

namespace IllTechLibrary.SharedStructs
{
    public class NpcData : SSClass
    {
        public NpcData()
        {
        }

        public NpcData(List<Object> MembData) : base(MembData) { }

        public int a_index;
        public int a_enable;
        public int a_family;
        public SByte a_skillmaster; // SByte
        public int a_flag;
        public int a_flag1;
        [RequiredVersion(4)]
        public int a_state_flag;
        public int a_level;
        public Int64 a_exp;
        public int a_prize;
        public float a_sight;
        public float a_size;
        public int a_move_area;
        public float a_attack_area;
        public Int64 a_skill_point;
        public SByte a_sskill_master; // SByte
        public int a_str;
        public int a_dex;
        public int a_int;
        public int a_con;
        public int a_attack;
        public int a_magic;
        public int a_defense;
        public int a_resist;
        public int a_attacklevel;
        public int a_defenselevel;
        public int a_hp;
        public int a_mp;
        public SByte a_attackType;
        public int a_attackSpeed;
        public int a_recover_hp;
        public int a_recover_mp;
        public float a_walk_speed;
        public float a_run_speed;
        public String a_skill0;
        public String a_skill1;
        public String a_skill2;
        public String a_skill3;
        public int a_item_0;
        public int a_item_1;
        public int a_item_2;
        public int a_item_3;
        public int a_item_4;
        public int a_item_5;
        public int a_item_6;
        public int a_item_7;
        public int a_item_8;
        public int a_item_9;
        public int a_item_10;
        public int a_item_11;
        public int a_item_12;
        public int a_item_13;
        public int a_item_14;
        public int a_item_15;
        public int a_item_16;
        public int a_item_17;
        public int a_item_18;
        public int a_item_19;
        public int a_item_percent_0;
        public int a_item_percent_1;
        public int a_item_percent_2;
        public int a_item_percent_3;
        public int a_item_percent_4;
        public int a_item_percent_5;
        public int a_item_percent_6;
        public int a_item_percent_7;
        public int a_item_percent_8;
        public int a_item_percent_9;
        public int a_item_percent_10;
        public int a_item_percent_11;
        public int a_item_percent_12;
        public int a_item_percent_13;
        public int a_item_percent_14;
        public int a_item_percent_15;
        public int a_item_percent_16;
        public int a_item_percent_17;
        public int a_item_percent_18;
        public int a_item_percent_19;
        public int a_minplus;
        public int a_maxplus;
        public int a_probplus;
        public int a_product0;
        public int a_product1;
        public int a_product2;
        public int a_product3;
        public int a_product4;
        public String a_file_smc;
        public String a_motion_walk;
        public String a_motion_idle;
        public String a_motion_dam;
        public String a_motion_attack;
        public String a_motion_die;
        public String a_motion_run;
        public String a_motion_idle2;
        public String a_motion_attack2;
        public float a_scale;
        public int a_attribute;
        public SByte a_fireDelayCount;
        public float a_fireDelay0;
        public float a_fireDelay1;
        public float a_fireDelay2;
        public float a_fireDelay3;
        public String a_fireEffect0;
        public String a_fireEffect1;
        public String a_fireEffect2;
        public SByte a_fireObject;
        public float a_fireSpeed;
        public int a_aitype;
        public int a_aiflag;
        public int a_aileader_flag;
        public int a_ai_summonHp;
        public int a_aileader_idx;
        public int a_aileader_count;
        public int a_crafting_category;
        public int a_productIndex;
        [Locale("usa")]
        public String a_name_usa;
        [Locale("usa")]
        public String a_descr_usa;
        [Locale("thai")]
        public String a_name_thai;
        [Locale("thai")]
        public String a_descr_thai;
        public int a_hit;
        public int a_dodge;
        public int a_magicavoid;
        public int a_job_attribute;
        public int a_npc_choice_trigger_count;
        public String a_npc_choice_trigger_ids;
        public int a_npc_kill_trigger_count;
        public String a_npc_kill_trigger_ids;
        public int a_createprob;
        public int a_socketprob_0;
        public int a_socketprob_1;
        public int a_socketprob_2;
        public int a_socketprob_3;
        public int a_jewel_0;
        public int a_jewel_1;
        public int a_jewel_2;
        public int a_jewel_3;
        public int a_jewel_4;
        public int a_jewel_5;
        public int a_jewel_6;
        public int a_jewel_7;
        public int a_jewel_8;
        public int a_jewel_9;
        public int a_jewel_10;
        public int a_jewel_11;
        public int a_jewel_12;
        public int a_jewel_13;
        public int a_jewel_14;
        public int a_jewel_15;
        public int a_jewel_16;
        public int a_jewel_17;
        public int a_jewel_18;
        public int a_jewel_19;
        public int a_jewel_percent_0;
        public int a_jewel_percent_1;
        public int a_jewel_percent_2;
        public int a_jewel_percent_3;
        public int a_jewel_percent_4;
        public int a_jewel_percent_5;
        public int a_jewel_percent_6;
        public int a_jewel_percent_7;
        public int a_jewel_percent_8;
        public int a_jewel_percent_9;
        public int a_jewel_percent_10;
        public int a_jewel_percent_11;
        public int a_jewel_percent_12;
        public int a_jewel_percent_13;
        public int a_jewel_percent_14;
        public int a_jewel_percent_15;
        public int a_jewel_percent_16;
        public int a_jewel_percent_17;
        public int a_jewel_percent_18;
        public int a_jewel_percent_19;
        [RequiredVersion(4)]
        public UInt64 a_zone_flag;
        [RequiredVersion(4)]
        public UInt64 a_extra_flag;
        [RequiredVersion(4)]
        public int a_rvr_value;
        [RequiredVersion(4)]
        public int a_rvr_grade;
        [RequiredVersion(4)]
        public float a_bound;
        //public int a_lifetime;
    }
}
