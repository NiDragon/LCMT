using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using IllTechLibrary.Util;

namespace IllTechLibrary.SharedStructs
{
    public class MagicLevel
    {
        public MagicLevel()
        {
        }

        public MagicLevel(List<Object> MembData)
        {
            int lastIndex = 0;

            List<FieldInfo> info = this.GetType().GetFields().ToList();

            try
            {
                for (int i = 0; i < info.Count(); i++)
                {
                    lastIndex = i;

                    info[i].SetValue(this, MembData[i]);
                }
            }
            catch (Exception e)
            {
                String message = e.Message;
                MsgDialogs.Show("Exception!", String.Format("{0}\nEntry Name: {1}", e.Message, info[lastIndex].Name), "ok", IllTechLibrary.Util.MsgDialogs.MsgTypes.ERROR);
            }
        }

        public int a_index;
        public sbyte a_level;
        public int a_power;
        public int a_hitrate;
    }
}
