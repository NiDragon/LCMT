using IllTechLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary.SharedStructs
{
    public class MagicData
    {
        public MagicData()
        {
        }

        public MagicData(List<Object> MembData)
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

        public string a_name;

        public sbyte a_maxlevel;
        public sbyte a_type;
        public sbyte a_subtype;
        public sbyte a_damagetype;
        public sbyte a_hittype;
        public sbyte a_attribute;

        // Investigate These!
        public int a_psp;
        public int a_ptp;
        public int a_hsp;
        public int a_htp;

        // Toggle Skill
        public byte a_togle;
    }
}
