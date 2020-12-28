using IllTechLibrary.Settings;
using IllTechLibrary.Util;
using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IllTechLibrary.Runtime;
using System.Windows.Forms;
using System.Reflection;

namespace IllTechLibrary
{
    public class Core
    {
        public static DialogResult ShowSettings()
        {
            return new MySqlSettings().ShowDialog();
        }

        public static String LangCode
        {
            get;
            set;
        }

        public static String SqlEncoder
        {
            get;
            set;
        }

        public static Encoding Encoder
        {
            get;
            set;
        }

        public static String UILanguage 
        { 
            get; 
            set; 
        }
    }
}
