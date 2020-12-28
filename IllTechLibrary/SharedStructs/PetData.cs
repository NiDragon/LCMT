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
    public class PetData : SSClass
    {
        public PetData()
        {
        }

        public PetData(List<Object> MembData) : base(MembData) { }

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
