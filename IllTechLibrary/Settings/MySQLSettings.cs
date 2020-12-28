using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IllTechLibrary.Util;
using System.Runtime.InteropServices;
using IniParser;
using System.IO;
using IniParser.Model;
using IllTechLibrary.Localization;

namespace IllTechLibrary.Settings
{
    public partial class MySqlSettings : Form
    {

#if !MONO
        [DllImport("ICL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Decrypt_Password(StringBuilder crypt, uint AuthKey);

        [DllImport("ICL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Encrypt_Password(StringBuilder crypt, uint AuthKey);
#else 
        const uint InternalKey = 0x49434C30;
        static char[] m_LookUp = { 'a', 'c', 'd', 'c', 'i', 'd', 'g', 'x', 'o', 'b', 'i', 'n', 't', 'g', 'c',  };

        public static bool Decrypt_Password(String val, out String valout, uint AuthKey) {
            valout = String.Empty;
            
            if(AuthKey != InternalKey)
		        return false;

            byte[] crypt = Encoding.ASCII.GetBytes(val);

	        int len = crypt.Length;

	        for(int i = 0; i < len; i++)
	        {
		        crypt[i] = Convert.ToByte((crypt[i]-12) ^ m_LookUp[i%m_LookUp.Length]);
	        }

            valout = Encoding.ASCII.GetString(crypt);

	        return true;
        }

        public static bool Encrypt_Password(String val, out String valout, uint AuthKey)
        {
            valout = String.Empty;

            if(AuthKey != InternalKey)
		        return false;

            byte[] crypt = Encoding.ASCII.GetBytes(val);

	        int len = crypt.Length;

	        for(int i = 0; i < len; i++)
	        {
		        crypt[i] = Convert.ToByte((crypt[i] ^ m_LookUp[i%m_LookUp.Length])+12);
	        }

            valout = BitConverter.ToString(crypt).Replace("-", "");

	        return true;
        }
#endif

        public MySqlSettings()
        {
            InitializeComponent();
        }

        private bool SaveInfo()
        {
            String password = "";

            if (UNameText.Text == String.Empty ||
                UIPText.Text == String.Empty ||
                UPortText.Text == String.Empty)
                return false;

            if (EncryptPwdCheck.Checked)
            {
                StringBuilder sb = new StringBuilder(UPassText.Text);

#if !MONO
                if (Encrypt_Password(sb, 0x49434C30))
#else
                if(Encrypt_Password(UPassText.Text, out password, 0x49434C30))
#endif
                {
#if !MONO
                    password = BitConverter.ToString(Encoding.ASCII.GetBytes(sb.ToString())).Replace("-", "");
#endif
                }
                else
                {
                    return false;
                }
            }
            else
            {
                password = UPassText.Text;
            }

            //Create an instance of a ini file parser
            FileIniDataParser fileIniData = new FileIniDataParser();

            if (!File.Exists(Preferences.GetConfig()))
            {
                return false;
            }

            fileIniData.Parser.Configuration.CommentString = "#";

            //Parse the ini file
            IniData parsedData = fileIniData.ReadFile(Preferences.GetConfig());

            parsedData["HOST"]["IP"] = UIPText.Text;
            parsedData["HOST"]["Port"] = UPortText.Text;

            parsedData["HOST"]["User"] = UNameText.Text;
            parsedData["HOST"]["Pass"] = password;
            parsedData["HOST"]["Encrypt"] = Convert.ToString(EncryptPwdCheck.Checked);

            parsedData["PREFS"]["LangCode"] = LangCode.SelectedItem.ToString();
            parsedData["PREFS"]["Encoding"] = EncPage.SelectedItem.ToString();

            parsedData["PREFS"]["EP3Transcode"] = Convert.ToString(EP3Transcode.Checked);

            Preferences.WriteData(parsedData);

            Preferences.SetLangCode(LangCode.SelectedItem.ToString());
            Preferences.SetEncoding(EncPage.SelectedItem.ToString());

            return true;
        }

        // Save the sql to the ini file [HOST] section
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (SaveInfo())
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MsgDialogs.Show(StringTable.UIStrings["ErrorDialogTitle"], String.Format(StringTable.UIStrings["FileSaveError"], Preferences.GetConfig()), "ok", MsgDialogs.MsgTypes.ERROR);
            }
        }

        // Exit without saving
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public static byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        public static String Decrypt(String Password)
        {
            String asOther = Encoding.ASCII.GetString(FromHex(Password));
            StringBuilder sb = new StringBuilder(asOther);
#if !MONO
            Decrypt_Password(sb, 0x49434C30);
            return sb.ToString();
#else
            String ret = String.Empty;
            Decrypt_Password(asOther, out ret, 0x49434C30);
            return ret;
#endif
        }

        private void MySQLSet_Load(object sender, EventArgs e)
        {
            UPassText.UseSystemPasswordChar = true;

            //Create an instance of a ini file parser
            FileIniDataParser fileIniData = new FileIniDataParser();

            if (!File.Exists(Preferences.GetConfig()))
            {
                return;
            }

            fileIniData.Parser.Configuration.CommentString = "#";

            //Parse the ini file
            IniData parsedData = fileIniData.ReadFile(Preferences.GetConfig());

            UIPText.Text = parsedData["HOST"]["IP"];
            UPortText.Text = parsedData["HOST"]["Port"];

            UNameText.Text = parsedData["HOST"]["User"];

            EncryptPwdCheck.Checked = Convert.ToBoolean(parsedData["HOST"]["Encrypt"]);

            if (EncryptPwdCheck.Checked)
            {
                String asHex = parsedData["HOST"]["Pass"];

                UPassText.Text = Decrypt(asHex);
            }
            else
            {
                UPassText.Text = parsedData["HOST"]["Pass"];
            }

            LangCode.SelectedIndex = LangCode.FindString(parsedData["PREFS"]["LangCode"].ToLower());

            if (LangCode.SelectedIndex == -1)
                LangCode.SelectedIndex = 0;

            EncPage.SelectedIndex = EncPage.FindString(parsedData["PREFS"]["Encoding"]);

            // Patch Fix EP3
            EP3Transcode.Checked = Convert.ToBoolean(parsedData["PREFS"]["EP3Transcode"]);
        }

        private void TestConnection_Click(object sender, EventArgs e)
        {
            IllSQL con = new IllSQL();

            if(con.ConnectTest(UIPText.Text, UNameText.Text, UPassText.Text, UPortText.Text))
            {
                MessageBox.Show("Connection Success!", "Message", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Connection Failed!", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
