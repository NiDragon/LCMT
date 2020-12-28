using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary.Dialogs
{
    public partial class QuestionBox : Form
    {
        private string m_result = String.Empty;

        public string Result
        {
            get { return m_result; }
        }

        public QuestionBox(String Question)
        {
            InitializeComponent();

            Text = $"Question - {Question}";
        }

        private void DoConfirm(object sender, EventArgs e)
        {
            m_result = tbReply.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void tbReply_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoConfirm(this, new EventArgs());
            }
        }
    }
}
