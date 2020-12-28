using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using IllTechLibrary.Util;

namespace IllTechLibrary.SharedStructs
{
    public class NpcDropAll : SSClass
    {
        public NpcDropAll()
        {
        }

        public NpcDropAll(List<Object> MembData) : base(MembData) { }

        public int a_npc_idx;
        public int a_item_idx;
        public int a_prob;
    }
}
