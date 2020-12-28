using IllTechLibrary.Attributes;
using IllTechLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IllTechLibrary.SharedStructs.Strings
{
    public class ClientStrings
    {
        public ClientStrings()
        {
        }

        public ClientStrings(List<Object> MembData)
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
                string message = e.Message;
                MsgDialogs.Show("Exception!", String.Format("{0}\nEntry Name: {1}", e.Message, info[lastIndex].Name), "ok", MsgDialogs.MsgTypes.ERROR);
            }
        }

        public int a_index;
        [Locale("usa")]
        public String a_string_usa;
        [Locale("thai")]
        public String a_string_thai;
    }
}
