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
    public class GuildMemberExtendData : SSClass
    {
        public GuildMemberExtendData()
        {
        }

        public GuildMemberExtendData(List<Object> MembData) : base(MembData) { }

        public int a_guild_index;
        public int a_char_index;
        public string a_position_name;
        public int a_contribute_exp;
        public int a_contribute_fame;
        public int a_point;
        public sbyte a_stash_auth;
    }
}
