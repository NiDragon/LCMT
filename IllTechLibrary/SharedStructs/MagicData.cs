using IllTechLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary.SharedStructs
{
    public class MagicData : SSClass
    {
        public MagicData()
        {
        }

        public MagicData(List<Object> MembData) : base(MembData) { }

        public int a_index;

        public string a_name;

        public sbyte a_maxlevel;
        public sbyte a_type;
        public sbyte a_subtype;
        public sbyte a_damagetype;
        public sbyte a_hittype;
        public sbyte a_attribute;

        // Investigate These!
        public int a_psp;
        public int a_ptp;
        public int a_hsp;
        public int a_htp;

        // Toggle Skill
        public byte a_togle;
    }
}
