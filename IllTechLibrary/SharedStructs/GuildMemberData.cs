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
    public class GuildMemberData : SSClass
    {
        public GuildMemberData()
        {
        }

        public GuildMemberData(List<Object> MembData) : base(MembData) { }

        public int a_guild_index;
        public int a_char_index;
        public string a_char_name;
        public int a_pos;
        public DateTime a_regdate;
    }
}
