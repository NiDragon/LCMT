using IllTechLibrary.Util;
using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary.Localization
{
    public class StringTable
    {
        public static StringTable UIStrings;
        
        public Dictionary<String, String> TranslationByName = new Dictionary<String, String>();

        public string this[string name]
        {
            get 
            {
                if (!TranslationByName.ContainsKey(name))
                    throw new ArgumentOutOfRangeException();

                return TranslationByName[name]; 
            }
            //set { TranslationByName[name] = value; }
        }

        public static void LoadStrings(String lang)
        {
            UIStrings = new StringTable();

            String filename = $"Locale//{lang}.txt";

            if (!File.Exists(filename))
            {
                // Lets just default to english strings
                filename = $"Locale//en.txt";
            }

            FileIniDataParser si = new FileIniDataParser();

            si.Parser.Configuration.CommentString = "#";
            
            IniData data = si.ReadFile(filename, Encoding.UTF8);

            foreach(KeyData kd in data.Global)
            {
                UIStrings.TranslationByName.Add(kd.KeyName, kd.Value);
            }
        }

        public bool HasKey(string name)
        {
            return TranslationByName.ContainsKey(name);
        }

        public string Get(string name)
        {
            return TranslationByName[name];
        }
    }
}
