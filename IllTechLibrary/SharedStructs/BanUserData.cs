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
    public class BanUserData
    {
        public BanUserData()
        {
        }

        public BanUserData(List<Object> MembData)
        {
            int lastIndex = 0;

            List<FieldInfo> info = this.GetType().GetFields().ToList();

            try
            {
                for (int i = 0; i < info.Count(); i++)
                {
                    lastIndex = i;

                    if (Attribute.IsDefined(info[i], typeof(LocaleAttribute)))
                    {
                        if (((LocaleAttribute)Attribute.GetCustomAttribute(info[i],
                        typeof(LocaleAttribute))) != Core.LangCode)
                        {
                            info.RemoveAt(i);
                            i--;
                            continue;
                        }
                    }

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
        public string a_idname;
        public uint a_portal_index;

        public uint a_enable;
    }
}
