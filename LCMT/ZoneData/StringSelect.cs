using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCMT.ZoneData
{
    public partial class StringSelect : Form
    {
        Dictionary<int, string> m_strings;

        public StringSelect(Dictionary<int, string> strings)
        {
            InitializeComponent();

            /* Store the strings */
            m_strings = strings;
     
            /* Populate List Box */
            foreach(KeyValuePair<int, string> a in m_strings)
            {
                lb_stringView.Items.Add(String.Format("{0} - {1}", a.Key.ToString(), a.Value));
            }
        }

        bool hasSelection = false;

        public string Selected()
        {
            if (!hasSelection)
                lb_stringView.SelectedIndex = -1;

            if (lb_stringView.Items.Count == 0 || lb_stringView.SelectedIndex == -1)
                return "NONE";

            return lb_stringView.Items[lb_stringView.SelectedIndex].ToString().Split('-')[0].TrimEnd(' ');
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            hasSelection = true;

            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int idx = -1;

            try
            {
                for(int i = 0; i < lb_stringView.Items.Count; i++)
                {
                    if (lb_stringView.Items[i].ToString().ToLower().Split('-')[1].TrimStart(' ').Contains(textBox1.Text.ToLower()))
                    {
                        idx = i;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                return;
            }

            if (idx == -1)
                return;

            lb_stringView.SelectedIndex = idx;
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            if (lb_stringView.SelectedIndex == -1)
                lb_stringView.SelectedIndex = 0;

            int idx = -1;

            for (int i = lb_stringView.SelectedIndex+1; i < lb_stringView.Items.Count; i++)
            {
                if (lb_stringView.Items[i].ToString().ToLower().Split('-')[1].TrimStart(' ').Contains(textBox1.Text.ToLower()))
                {
                    idx = i;
                    break;
                }
            }

            if (idx == -1)
                return;

            lb_stringView.SelectedIndex = idx;
        }

        private void StringSelect_Load(object sender, EventArgs e)
        {

        }
    }
}
