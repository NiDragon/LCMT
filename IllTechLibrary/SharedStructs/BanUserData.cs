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
    public class BanUserData : SSClass
    {
        public BanUserData()
        {
        }

        public BanUserData(List<Object> MembData) : base(MembData) { }

        public int a_index;
        public string a_idname;
        public uint a_portal_index;

        public uint a_enable;
    }
}
