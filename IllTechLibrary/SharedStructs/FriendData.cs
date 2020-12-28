using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using IllTechLibrary.Util;

namespace IllTechLibrary.SharedStructs
{
    public class FriendData : SSClass
    {
        public FriendData()
        {
        }

        public FriendData(List<Object> MembData) : base(MembData) { }

        public int a_char_index;
        public int a_friend_index;

        public string a_friend_name;

        public int a_friend_job;
        public int a_group_index;
    }
}
