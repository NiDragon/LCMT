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
    public class PetData
    {
        public PetData()
        {
        }

        public PetData(List<Object> MembData)
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
        public int a_owner;
        public sbyte a_enable;

        public int a_lastupdate;

        public sbyte a_type;
        public int a_level;
        public int a_hp;
        public int a_hungry;
        public int a_sympathy;

        public Int64 a_exp;
        public int a_ability;
        public string a_skill_index;
        public string a_skill_level;
        public int a_time_rebirth;

        public string a_color;
        public int a_pet_turnto_npc;
        public int a_pet_size;
    }
}
