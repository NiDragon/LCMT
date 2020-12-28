using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary.Dialogs
{
    public partial class FortuneDataEntry : Form
    {
        public int SkillIdx = -1;
        public int SkillLv = 0;
        public int StrId = -1;
        public int Prob = 0;

        public FortuneDataEntry()
        {
            InitializeComponent();

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
        }

        private void OnCancel(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void OnAdd(object sender, EventArgs e)
        {
            if (tbLevel.Text != String.Empty && tbSkill.Text != String.Empty
                && tbString.Text != String.Empty && tbProb.Text != String.Empty)
            {
                DialogResult = DialogResult.OK;

                try
                {
                    SkillIdx = int.Parse(tbSkill.Text);
                    SkillLv = int.Parse(tbLevel.Text);
                    StrId = int.Parse(tbString.Text);
                    Prob = int.Parse(tbProb.Text);
                }
                catch (Exception)
                {
                    DialogResult = DialogResult.Cancel;
                }
            }

            Close();
        }
    }
}
