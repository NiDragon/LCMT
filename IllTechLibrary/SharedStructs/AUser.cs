using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using IllTechLibrary.Util;

namespace IllTechLibrary.SharedStructs
{
    public class AUser : SSClass
    {
        public AUser()
        {
        }

        public AUser(List<Object> MembData) : base(MembData) { }

        public uint user_code;

        public string user_id;
        public string email;
        public string passwd;
        public string passwd_plain;

        public string guid;

        public int activated;
        public int cash;
    }
}
