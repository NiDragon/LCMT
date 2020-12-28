using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCMT.Interfaces
{
    public partial class DatabaseChanger : Form
    {
        public String dataOld;
        public String charOld;
        public String authOld;
        public String postOld;

        public DatabaseChanger()
        {
            InitializeComponent();
        }

        private void TableChanger_Load(object sender, EventArgs e)
        {
            tbData.Text = IllTechLibrary.IllSQL.GetToolDB("DataDB");
            tbChar.Text = IllTechLibrary.IllSQL.GetToolDB("CharDB");
            tbAuth.Text = IllTechLibrary.IllSQL.GetToolDB("AuthDB");
            tbPost.Text = IllTechLibrary.IllSQL.GetToolDB("PostDB");

            dataOld = tbData.Text;
            charOld = tbChar.Text;
            authOld = tbAuth.Text;
            postOld = tbPost.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool changed = false;

            changed = (dataOld != tbData.Text ||
                charOld != tbChar.Text ||
                authOld != tbAuth.Text ||
                postOld != tbPost.Text);

            if(changed)
            {
                FileIniDataParser i = new FileIniDataParser();

                i.Parser.Configuration.CommentString = "#";
                i.Parser.Configuration.CaseInsensitive = true;

                IniData data = i.ReadFile(IllTechLibrary.Settings.Preferences.GetConfig());

                data["DS"]["DataDB"] = tbData.Text;
                data["DS"]["AuthDB"] = tbAuth.Text;
                data["DS"]["CharDB"] = tbChar.Text;
                data["DS"]["PostDB"] = tbPost.Text;

                IllTechLibrary.Settings.Preferences.WriteData(data);

                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Ignore;
            }
        }
    }
}
