using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using IllTechLibrary.Util;

namespace IllTechLibrary.SharedStructs
{
    public class IgnoreData : SSClass
    {
        public IgnoreData()
        {
        }

        public IgnoreData(List<Object> MembData) : base(MembData) { }

        public int a_char_idx;

        public string a_block_idx_list;

        public string a_block_name_list;
    }
}
