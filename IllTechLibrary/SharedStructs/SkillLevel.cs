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
    public class SkillLevel
    {
        public SkillLevel()
        {
        }

        public SkillLevel(List<Object> MembData)
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
        public sbyte a_level;

        public int a_needHP;
        public int a_needMP;
        public int a_needGP;

        public int a_durtime;

        public int a_dummypower;

        public int a_needItemIndex1 = -1;
        public int a_needItemCount1;
        public int a_needItemIndex2 = -1;
        public int a_needItemCount2;

        public int a_learnLevel;
        public int a_learnSP;

        public int a_learnSkillIndex1 = -1;
        public sbyte a_learnSkillLevel1;
        public int a_learnSkillIndex2 = -1;
        public sbyte a_learnSkillLevel2;
        public int a_learnSkillIndex3 = -1;
        public sbyte a_learnSkillLevel3;

        public int a_learnItemIndex1 = -1;
        public int a_learnItemCount1;
        public int a_learnItemIndex2 = -1;
        public int a_learnItemCount2;
        public int a_learnItemIndex3 = -1;
        public int a_learnItemCount3;

        public int a_appMagicIndex1 = -1;
        public sbyte a_appMagicLevel1;
        public int a_appMagicIndex2 = -1;
        public sbyte a_appMagicLevel2;
        public int a_appMagicIndex3 = -1;
        public sbyte a_appMagicLevel3;

        public int a_magicIndex1 = -1;
        public sbyte a_magicLevel1;
        public int a_magicIndex2 = -1;
        public sbyte a_magicLevel2;
        public int a_magicIndex3 = -1;
        public sbyte a_magicLevel3;

        public int a_learnstr;
        public int a_learndex;
        public int a_learnint;
        public int a_learncon;

        public int a_hate;

        public int a_learnGP;
        public int a_use_count;
        public int a_targetNum;
    }
}
