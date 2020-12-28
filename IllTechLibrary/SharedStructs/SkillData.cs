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
    public class SkillData : SSClass
    {
        public SkillData()
        {
        }

        public SkillData(List<Object> MembData) : base (MembData) { }

        public int a_index;

        // Class and Dual Class
        public int a_job;
        public int a_job2;

        [Locale("usa")]
        public String a_name_usa;
        [Locale("thai")]
        public String a_name_thai;

        public int a_type;
        public int a_flag;
        public sbyte a_maxLevel;

        // Ranges
        public float a_appRange;
        public float a_fireRange;
        public float a_fireRange2;
        public float a_minRange;

        // Target Info
        public int a_targetType;
        public int a_targetNum;

        // Use Info & Equipment
        public int a_useState;
        public int a_useWeaponType0 = -1;
        public int a_useWeaponType1 = -1;
        public int a_use_needWearingType = -1;

        // Magic Data
        public int a_useMagicIndex1 = -1;
        public int a_useMagicLevel1;
        public int a_useMagicIndex2 = -1;
        public int a_useMagicLevel2;
        public int a_useMagicIndex3 = -1;
        public int a_useMagicLevel3;

        public int a_appState;
        public int a_appWeaponType0 = -1;
        public int a_appWeaponType1 = -1;
        public int a_app_needWearingType = -1;

        public int a_readyTime;
        public int a_stillTime;

        public int a_fireTime;
        public int a_reuseTime;

        public String a_cd_ra;
        public String a_cd_re;
        public String a_cd_sa;
        public String a_cd_fa;
        public String a_cd_fe0;
        public String a_cd_fe1;
        public String a_cd_fe2;

        public sbyte a_cd_fot;
        public sbyte a_cd_fot2;
        public float a_cd_fos;
        public float a_cd_fos2;
        public float a_cd_ox;
        public float a_cd_ox2;
        public float a_cd_oz;
        public float a_cd_oz2;
        public float a_cd_oh;
        public float a_cd_oc;
        public float a_cd_oh2;
        public float a_cd_oc2;
        public sbyte a_cd_fdc;
        public sbyte a_cd_fdc2;
        public float a_cd_fd0;
        public float a_cd_fd1;
        public float a_cd_fd2;
        public float a_cd_fd3;
        public float a_cd_fd4;
        public float a_cd_fd5;
        public float a_cd_fd6;
        public float a_cd_fd7;
        public float a_cd_dd;
        public float a_cd_dd2;
        public String a_cd_fe_after;
        public String a_cd_fe_after2;

        [Locale("usa")]
        public String a_client_description_usa;
        [Locale("thai")]
        public String a_client_description_thai;
        [Locale("usa")]
        public String a_client_tooltip_usa;
        [Locale("thai")]
        public String a_client_tooltip_thai;

        public int a_client_icon_texid;
        public int a_client_icon_row;
        public int a_client_icon_col;

        public String a_cd_ra2;
        public String a_cd_re2;
        public String a_cd_sa2;
        public String a_cd_fa2;
        public String a_cd_fe3;
        public String a_cd_fe4;
        public String a_cd_fe5;

        public int a_selfparam;
        public int a_targetparam;
        public int a_soul_consum;
        public int a_summon_idx = -1;

        public int a_sorcerer_flag;
        public int a_apet_index;

        public String a_allowzone;
    }
}
