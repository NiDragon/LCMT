using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using IllTechLibrary.Util;

namespace IllTechLibrary.SharedStructs
{
    public class MagicLevel : SSClass
    {
        public MagicLevel()
        {
        }

        public MagicLevel(List<Object> MembData) : base(MembData) { }

        public int a_index;
        public sbyte a_level;
        public int a_power;
        public int a_hitrate;
    }
}
