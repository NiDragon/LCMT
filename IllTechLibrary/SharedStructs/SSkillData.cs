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
    class SSkillData
    {
        public SSkillData()
        {
        }

        public SSkillData(List<Object> MembData)
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

        public int a_index;
        public int a_enable;

        // Name Info
        [Locale("usa")]
        public string a_name_usa;
        [Locale("thai")]
        public string a_name_thai;

        [Locale("thai")]
        public string a_desc_thai;
        [Locale("usa")]
        public string a_desc_usa;

        public int a_job;
        public byte a_type;
        public byte a_need_sskill;
        public byte a_need_sskill_level;
        public byte a_max_level;
        public byte a_preference;

        public byte a_level0_need_level;
        public short a_level0_need_sp;

        public int a_level0_num0;
        public int a_level0_num1;
        public int a_level0_num2;
        public int a_level0_num3;
        public int a_level0_num4;
        public int a_level0_num5;
        public int a_level0_num6;
        public int a_level0_num7;
        public int a_level0_num8;
        public int a_level0_num9;

        public byte a_level1_need_level;
        public short a_level1_need_sp;

        public int a_level1_num0;
        public int a_level1_num1;
        public int a_level1_num2;
        public int a_level1_num3;
        public int a_level1_num4;
        public int a_level1_num5;
        public int a_level1_num6;
        public int a_level1_num7;
        public int a_level1_num8;
        public int a_level1_num9;

        public byte a_level2_need_level;
        public short a_level2_need_sp;

        public int a_level2_num0;
        public int a_level2_num1;
        public int a_level2_num2;
        public int a_level2_num3;
        public int a_level2_num4;
        public int a_level2_num5;
        public int a_level2_num6;
        public int a_level2_num7;
        public int a_level2_num8;
        public int a_level2_num9;

        public byte a_level3_need_level;
        public short a_level3_need_sp;

        public int a_level3_num0;
        public int a_level3_num1;
        public int a_level3_num2;
        public int a_level3_num3;
        public int a_level3_num4;
        public int a_level3_num5;
        public int a_level3_num6;
        public int a_level3_num7;
        public int a_level3_num8;
        public int a_level3_num9;

        public byte a_level4_need_level;
        public short a_level4_need_sp;

        public int a_level4_num0;
        public int a_level4_num1;
        public int a_level4_num2;
        public int a_level4_num3;
        public int a_level4_num4;
        public int a_level4_num5;
        public int a_level4_num6;
        public int a_level4_num7;
        public int a_level4_num8;
        public int a_level4_num9;

        public byte a_texture_id;
        public byte a_texture_row;
        public byte a_texture_col;
    }
}
