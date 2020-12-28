using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using IllTechLibrary.Util;

namespace IllTechLibrary.SharedStructs
{
    public class LuckyDrawBox : SSClass
    {
        public LuckyDrawBox()
        {
        }

        public LuckyDrawBox(List<Object> MembData) : base(MembData) { }

        public int a_index;
        public string a_name;
        public int a_enable;
        public int a_random;
    }

    public class LuckyDrawBoxNeed : SSClass
    {
        public LuckyDrawBoxNeed()
        {
        }

        public LuckyDrawBoxNeed(List<Object> MembData) : base(MembData) { }

        public int a_index;
        public int a_luckydraw_idx;
        public int a_item_idx;
        public ulong a_count;
    }

    public class LuckyDrawResult : SSClass
    {
        public LuckyDrawResult()
        {
        }

        public LuckyDrawResult(List<Object> MembData) : base(MembData) { }

        public int a_index;
        public int a_luckydraw_idx;
        public int a_item_idx;
        public ulong a_count;
        public int a_upgrade;
        public int a_prob;
        public int a_flag;
    }
}
