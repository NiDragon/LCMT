using IllTechLibrary.Util;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary
{
    public partial class LogView : MetroFramework.Forms.MetroForm
    {
        private static LogView m_inst = null;
        public static void ShowLog()
        {
            if(m_inst == null)
            {
                m_inst = new LogView();
                m_inst.Show();
            }
            else
            {
                if (m_inst.Visible == true)
                {
                    m_inst.BringToFront();
                }
            }
        }

        private LogView()
        {
            InitializeComponent();
        }

        private void Log_Load(object sender, EventArgs e)
        {
            StyleManager = new MetroStyleManager();
            StyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;

            Color black = Color.FromArgb(255, 17, 17, 17);

            foreach (Control a in Controls)
            {
                ((IMetroControl)a).UseCustomBackColor = true;
                ((IMetroControl)a).UseCustomForeColor = true;

                a.BackColor = black;
                a.ForeColor = Color.White;

                ((IMetroControl)a).UseStyleColors = false;
            }

            using (FileStream fs = File.Open("IllTech.log", FileMode.Open, FileAccess.Read))
            {
                using (TextReader tr = new StreamReader(fs))
                {
                    DescBox.AppendText(tr.ReadToEnd());
                    DescBox.ScrollBars = ScrollBars.Vertical;

                    PathText.Text = $"Path: {fs.Name}";
                }
            }
        }

        private void LogView_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_inst = null;
        }

        private void ClearLog(object sender, EventArgs e)
        {
            if(MessageBox.Show(this, "Are you sure you want to clear the log?", "Clear Log?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (File.Exists("IllTech.log"))
                {
                    using (FileStream fs = File.Open("IllTech.log", FileMode.Truncate))
                    {
                        DescBox.ResetText();
                    }
                }
            }
        }
    }
}
