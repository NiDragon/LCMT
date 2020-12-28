using IllTechLibrary.Attributes;
using IllTechLibrary.Util;
using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary.Settings
{
    public class Preferences
    {
        internal static String g_configFile = "";

        private static IniData m_config = null;

        public static IniData GetData()
        {
            FileIniDataParser i = new FileIniDataParser();

            i.Parser.Configuration.CommentString = "#";
            i.Parser.Configuration.CaseInsensitive = true;

            IniData data = i.ReadFile(GetConfig());

            return data;
        }

        public static void WriteData(IniData data)
        {
            FileIniDataParser i = new FileIniDataParser();

            i.Parser.Configuration.CommentString = "#";
            i.Parser.Configuration.CaseInsensitive = true;

            i.WriteFile(GetConfig(), data);

            m_config = data;
        }

        public static void WriteWindow(String AppID, Form wnd)
        {
            Size size = wnd.Size;
            Point pos = wnd.Location;

            string cleaned = AppID.Replace("_", "");

            SetSize(cleaned, wnd);
            SetPosition(cleaned, pos);
            SetMax(cleaned, wnd.WindowState);
        }

        public static void SetWindow(String AppID, Form wnd)
        {
            string cleaned = AppID.Replace("_", "");

            wnd.Size = GetSize(cleaned);
            wnd.Location = GetPosition(cleaned);
            wnd.WindowState = GetMax(cleaned);
        }

        public static void LoadLocale()
        {
            String EncName = GetString("PREFS", "Encoding");
            Core.SqlEncoder = EncName;

            Core.LangCode = GetString("PREFS", "LangCode");

            Core.UILanguage = GetString("PREFS", "UILang");

            Core.Encoder = Encoding.GetEncoding(GetWinEncoding(EncName));
        }

        public static void SetLangCode(String langCode)
        {
            IniData data = GetData();

            // If our key does not exist we must add it!
            if (data["PREFS"].GetKeyData("LangCode") == null)
            {
                data["PREFS"].AddKey("LangCode");
            }

            data["PREFS"]["LangCode"] = langCode;

            WriteData(data);

            Core.LangCode = langCode;
        }

        public static void SetEncoding(String encoding)
        {
            IniData data = GetData();

            // If our key does not exist we must add it!
            if (data["PREFS"].GetKeyData("Encoding") == null)
            {
                data["PREFS"].AddKey("Encoding");
            }

            data["PREFS"]["Encoding"] = encoding;

            WriteData(data);

            Core.Encoder = Encoding.GetEncoding(GetWinEncoding(encoding));
        }

        /*
            latin1
            cp1250
            tis620
            euckr
            sjis
            big5
            utf8
        */

        private static String GetWinEncoding(String enc)
        {
            switch (enc)
            {
                case "latin1":
                    return "windows-1252";
                case "cp1250":
                    return "windows-1250";
                case "tis620":
                    return "windows-874";
                case "euckr":
                    return "ks_c_5601-1987";
                case "sjis":
                    return "shift_jis";
                case "big5":
                    return "big5";
                case "utf8":
                    return "windows-65001";
            }

            return "windows-1252";
        }

        public static bool EP3Transcode()
        {
            if(m_config == null) m_config = GetData();

            return Convert.ToBoolean(m_config["PREFS"]["EP3Transcode"]);
        }

        internal static RequiredVersion TargetVersion()
        {
            if (EP3Transcode())
                return new RequiredVersion(3);
            else
                return new RequiredVersion(4);
        }

        public static FormWindowState GetMax(String AppID)
        {
            FormWindowState ret = FormWindowState.Normal;
            IniData data = GetData();

            String key = String.Format("{0}_Maxed", AppID);

            if (data["PREFS"].GetKeyData(key) == null)
            {
                data["PREFS"].AddKey(key);
                return FormWindowState.Normal;
            }

            ret = Convert.ToBoolean(data["PREFS"][key]) == true ? FormWindowState.Maximized : FormWindowState.Normal;

            return ret;
        }

        public static string GetString(string section, string key)
        {
            return GetData()[section][key];
        }

        public static void SetMax(String AppID, FormWindowState State)
        {
            IniData data = GetData();

            String key = String.Format("{0}_Maxed", AppID);

            // If our key does not exist we must add it!
            if (data["PREFS"].GetKeyData(key) == null)
            {
                data["PREFS"].AddKey(key);
            }

            data["PREFS"][key] = Convert.ToString(State == FormWindowState.Maximized);

            WriteData(data);
        }

        public static Size GetSize(String AppID)
        {
            Size size = new Size(800, 600);
            IniData data = GetData();

            String key = String.Format("{0}_Size", AppID);

            if (data["PREFS"].GetKeyData(key) == null)
            {
                data["PREFS"].AddKey(key);
                return size;
            }

            String[] sizes = data["PREFS"][key].Split(',');

            size.Width = int.Parse(sizes[0]);
            size.Height = int.Parse(sizes[1]);

            return size;
        }

        public static Point GetPosition(String AppID)
        {
            Point point = new Point(50, 50);
            IniData data = GetData();

            String key = String.Format("{0}_Pos", AppID);

            if (data["PREFS"].GetKeyData(key) == null)
            {
                data["PREFS"].AddKey(key);
                return point;
            }

            String[] sizes = data["PREFS"][key].Split(',');

            point.X = int.Parse(sizes[0]);
            point.Y = int.Parse(sizes[1]);

            if (point.X < 0 || point.Y < 0)
                return new Point(50, 50);

            return point;
        }

        public static void SetSize(String AppID, Form wnd)
        {
            if (GetMax(AppID) == FormWindowState.Maximized)
                return;

            IniData data = GetData();

            String key = String.Format("{0}_Size", AppID);

            // If our key does not exist we must add it!
            if (data["PREFS"].GetKeyData(key) == null)
            {
                data["PREFS"].AddKey(key);
            }

            data["PREFS"][key] = String.Format("{0}, {1}", wnd.Size.Width, wnd.Size.Height);

            WriteData(data);
        }

        public static void SetPosition(String AppID, Point point)
        {
            if (point.X < 0 || point.Y < 0)
                return;

            IniData data = GetData();

            String key = String.Format("{0}_Pos", AppID);

            // If our key does not exist we must add it!
            if (data["PREFS"].GetKeyData(key) == null)
            {
                data["PREFS"].AddKey(key);
            }

            data["PREFS"][key] = String.Format("{0}, {1}", point.X, point.Y);

            WriteData(data);
        }

        public static void SetPrefs(String Key, String Value)
        {
            IniData data = GetData();

            data["PREFS"][Key] = Value;

            WriteData(data);
        }

        public static String GetPrefs(String Key)
        {
            IniData data = GetData();

            return data["PREFS"][Key];
        }

        public static void SetPrefKey(String Header, String Key, String Value)
        {
            IniData data = GetData();

            data[Header][Key] = Value;

            WriteData(data);
        }

        public static string GetConfig()
        {
            if (g_configFile == String.Empty)
            {
                MsgDialogs.Show("Error", "No Config file set please use SetConfig(String filename)", "ok", MsgDialogs.MsgTypes.ERROR);
                Application.Exit();
            }
            return g_configFile;
        }

        public static void SetConfig(String filename)
        {
            g_configFile = filename;
        }

        public static string[] GetConnectionStrings(string[] TableNames)
        {
            bool decrypt = false;

            IniData data = GetData();

            if (Convert.ToBoolean(data["HOST"]["Encrypt"]))
            {
                decrypt = true;
            }

            string[] myConnectionStrings = new string[4];

            for (int i = 0; i < 4; i++)
            {
                if (decrypt)
                {
                    myConnectionStrings[i] = String.Format("server={0};uid={1}; pwd={2}; port={3}; database={4}; CharSet={5}; Connect Timeout=3; UseCompression=true; Convert Zero Datetime = true;",
                        data["HOST"]["IP"], data["HOST"]["User"], MySqlSettings.Decrypt(data["HOST"]["Pass"]), data["HOST"]["Port"], data["DS"][TableNames[i]], data["PREFS"]["Encoding"]);
                }
                else
                {
                    if (!Preferences.EP3Transcode())
                    {
                        myConnectionStrings[i] = String.Format("server={0};uid={1}; pwd={2}; port={3}; database={4}; CharSet={5}; Connect Timeout=3; UseCompression=true; Convert Zero Datetime = true;",
                            data["HOST"]["IP"], data["HOST"]["User"], data["HOST"]["Pass"], data["HOST"]["Port"], data["DS"][TableNames[i]], data["PREFS"]["Encoding"]);
                    }
                    else
                    {
                        myConnectionStrings[i] = String.Format("server={0};uid={1}; pwd={2}; port={3}; database={4}; Connect Timeout=3; UseCompression=true; Convert Zero Datetime = true;",
                                data["HOST"]["IP"], data["HOST"]["User"], data["HOST"]["Pass"], data["HOST"]["Port"], data["DS"][TableNames[i]]);
                    }
                }

                myConnectionStrings[i] += "TreatTinyAsBoolean=false;";
            }

            return myConnectionStrings;
        }

        public static bool OptionalDelayLoad()
        {
            IniData data = GetData();

            if (data["PREFS"].GetKeyData("DelayLoadOption") == null)
            {
                return false;
            }

            return Convert.ToBoolean(data["PREFS"]["DelayLoadOption"]);
        }
    }
}
