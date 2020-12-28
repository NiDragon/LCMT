using IllTechLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary.SharedStructs
{
    public class ItemJewelData : SSClass
    {
        public ItemJewelData()
        {
        }

        public ItemJewelData(List<Object> MembData) : base(MembData) { }

        public int a_index;
        public Int64 a_normal_compose_neednas;
        public Int64 a_chaos_compose_neednas;
        public int a_normal_compose_prob;
        public int a_chaos_compose_prob;
        public int a_compose_normalToChaos_prob;
        public int a_normal_plus2_prob;
        public int a_normal_plus3_prob;
        public int a_chaos_plus2_prob;
        public int a_chaos_plus3_prob;
        public int a_normal_minus1_prob;
        public int a_normal_minus2_prob;
        public int a_normal_minus3_prob;
        public int a_chaos_minus1_prob;
        public int a_chaos_minus2_prob;
        public int a_chaos_minus3_prob;
    }
}
