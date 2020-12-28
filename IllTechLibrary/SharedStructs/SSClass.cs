using IllTechLibrary.Attributes;
using IllTechLibrary.Settings;
using IllTechLibrary.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary.SharedStructs
{
    public class SSClass
    {
        private class StringLocals
        {
            [Locale("dev")]
            public string a_name_dev;
            [Locale("dev")]
            public string a_descr_dev;
            [Locale("dev")]
            public string a_prefix_dev;
        }

        public SSClass()
        {
            // Ignore this
        }

        public SSClass(List<Object> MembData)
        {
            if (MembData.Count == 0)
                return;

            int lastIndex = 0;

            List<FieldInfo> info = this.GetType().GetFields().ToList();

            try
            {
                for (int i = 0; i < info.Count(); i++)
                {
                    lastIndex = i;

                    if (AttributeMethods.Validate<LocaleAttribute>(info[i], Core.LangCode) == ValidateState.Remove)
                    {
                        info.RemoveAt(i);
                        i--;
                        continue;
                    }

                    if (AttributeMethods.Validate<RequiredVersion>(info[i], Preferences.TargetVersion()) == ValidateState.Remove)
                    {
                        info.RemoveAt(i);
                        i--;
                        continue;
                    }

                    try
                    {
                        if (info[i].FieldType == typeof(UInt64))
                        {
                            info[i].SetValue(this, Convert.ChangeType(MembData[i], typeof(UInt64)));
                        }
                        else
                        {
                            info[i].SetValue(this, MembData[i]);
                        }
                    }
                    catch (Exception)
                    {
                        info[i].SetValue(this, Convert.ChangeType(MembData[i], info[i].FieldType));
                    }
                }
            }
            catch (Exception e)
            {
                String message = e.Message;
                MsgDialogs.Show("Exception!", String.Format("{0}\nEntry Name: {1}", e.Message, info[lastIndex].Name), "ok", IllTechLibrary.Util.MsgDialogs.MsgTypes.ERROR);
            }
        }
    }
}
