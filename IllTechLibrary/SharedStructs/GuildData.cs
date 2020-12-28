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
    public class GuildData : SSClass
    {
        public GuildData()
        {
        }

        public GuildData(List<Object> MembData) : base(MembData) { }

        public int a_index;
        public string a_name;
        public int a_level;
        public sbyte a_enable;
        public DateTime a_createdate;
        public DateTime a_recentdate;
        public int a_battle_index;
        public int a_battle_prize;
        public int a_battle_zone;
        public int a_battle_time;
        public int a_battle_killcount;
        public sbyte a_battle_state;
        public Int64 a_balance;
    }
}
