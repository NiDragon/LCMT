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
    public class ACharacter : SSClass
    {
        public ACharacter()
        {
        }

        public ACharacter(List<Object> MembData) : base(MembData) { }

        // Auto Key Index
        public int a_index;
        
        // Who Owns this Character
        public int a_user_index;

        public short a_server;

        // Name Info
        public string a_name;
        public string a_nick;
        
        public sbyte a_enable;

        public int a_deletedelay;
        public int a_safeGm;
        public int a_admin;

        // Character Type
        public uint a_flag;
        public sbyte a_job;

        // Apperance
        public sbyte a_hair_style;
        public sbyte a_face_style;

        // Level
        public int a_level;
        public Int64 a_exp;

        // Stats
        public int a_str;
        public int a_dex;
        public int a_int;
        public int a_con;

        public int a_hp;
        public int a_max_hp;
        public int a_mp;
        public int a_max_mp;

        public int a_statpt_remain;
        public int a_statpt_str;
        public int a_statpt_dex;
        public int a_statpt_con;
        public int a_statpt_int;

        public Int64 a_skill_point;
        public int a_blood_point;

        public string a_active_skill_index;
        public string a_active_skill_level;
        public string a_passive_skill_index;
        public string a_passive_skill_level;
        public string a_etc_skill_index;
        public string a_etc_skill_level;
        public string a_seal_skill_index;
        public string a_seal_skill_exp;
        public string a_quest_index;
        public string a_quest_value;
        public string a_quest_complete;
        public string a_quest_abandon;

        // GM Position 
        public uint a_save_pos;

        public float a_was_x;
        public float a_was_z;
        public float a_was_h;
        public float a_was_r;

        public int a_was_yLayer;
        public int a_was_zone;
        public int a_was_area;

        // Wearing
        public string a_wearing;
        public int a_silence_pulse;
        public string a_sskill;

        // PK Side Effects
        public int a_pkpenalty;
        public int a_pkcount;
        public int a_pkrecover;
        public int a_pkpenaltyhp;
        public int a_pkpenaltymp;

        // Guardian Info
        public string a_teach_idx;
        public string a_teach_sec;
        public sbyte a_teach_type;

        public int a_fame;
        public sbyte a_teach_list;
        public int a_teach_complete;
        public int a_teach_fail;

        // SP Info
        public int a_use_sp;
        public int a_total_sp;

        public UInt64 a_loseexp;
        public UInt64 a_losesp;

        // More Job/Guardian Shit
        public sbyte a_job2;
        public uint a_subjob;
        public int a_guardian;

        // Money
        public UInt64 a_nas;

        // Misc...
        public uint a_index_old;
        public ushort a_server_old;
        public int a_phoenix;
        public short a_title_index;
        public int a_newtuto_complete;
        public UInt64 a_exp_weekly;
        public short a_attendance_assure;
        public int a_reborn;
        public string a_color;
    }
}
