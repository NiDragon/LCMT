using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using IllTechLibrary.Attributes;
using IllTechLibrary.Util;

namespace IllTechLibrary.SharedStructs
{
    public class Option
    {
        public Option()
        {
        }

        public Option(List<Object> MembData)
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
                MsgDialogs.Show("Exception!", e.Message, "ok", IllTechLibrary.Util.MsgDialogs.MsgTypes.ERROR);
            }
        }

        public int a_index;
        public int a_type;

        // Array of ints
        public String a_level;
        public String a_prob;

        public int a_weapon_type;
        public int a_wear_type;
        public int a_accessory_type;

        [Locale("thai")]
        public String a_name_thai;
        [Locale("usa")]
        public String a_name_usa;
        [Locale("jpn")]
        public String a_name_jpn;
    }

    public class RareOption
    {
        public RareOption()
        {
        }

        public RareOption(List<Object> MembData)
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

                    /*if (info[i].Name.Contains("a_name_") || info[i].Name.Contains("a_descr_"))
                    {
                        if (!info[i].Name.Contains(Core.LangCode))
                        {
                            info.RemoveAt(i);
                            i--;
                            continue;
                        }
                    }*/

                    info[i].SetValue(this, MembData[i]);
                }
            }
            catch (Exception e)
            {
                String message = e.Message;
                MsgDialogs.Show("Exception!", String.Format("{0}\nEntry Name: {1}", e.Message, info[lastIndex].Name), "ok", IllTechLibrary.Util.MsgDialogs.MsgTypes.ERROR);
            }
        }

        private List<String> nameList = new List<String>();

        public int a_index;
        public int a_grade;
        public int a_type;
        public int a_attack;
        public int a_defense;
        public int a_magic;
        public int a_resist;
        public int a_option_index0;
        public int a_option_level0;
        public int a_option_prob0;
        public int a_option_index1;
        public int a_option_level1;
        public int a_option_prob1;
        public int a_option_index2;
        public int a_option_level2;
        public int a_option_prob2;
        public int a_option_index3;
        public int a_option_level3;
        public int a_option_prob3;
        public int a_option_index4;
        public int a_option_level4;
        public int a_option_prob4;
        public int a_option_index5;
        public int a_option_level5;
        public int a_option_prob5;
        public int a_option_index6;
        public int a_option_level6;
        public int a_option_prob6;
        public int a_option_index7;
        public int a_option_level7;
        public int a_option_prob7;
        public int a_option_index8;
        public int a_option_level8;
        public int a_option_prob8;
        public int a_option_index9;
        public int a_option_level9;
        public int a_option_prob9;

        [Locale("thai")]
        public String a_prefix_thai;
        [Locale("usa")]
        public String a_prefix_usa;
        //[Locale("chn")]
        //public String a_prefix_chn;
        [Locale("jpn")]
        public String a_prefix_jpn;
        [Locale("thai")]
        public String a_name_thai;
        [Locale("usa")]
        public String a_name_usa;
        //[Locale("chn")]
        //public String a_name_chn;
        [Locale("jpn")]
        public String a_name_jpn;
    }
}
