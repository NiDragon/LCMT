using MetroFramework.Components;
using MetroFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary
{
    public partial class About : MetroFramework.Forms.MetroForm
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            StyleManager = new MetroStyleManager();
            StyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;

            Color black = Color.FromArgb(255, 17, 17, 17);

            metroGrid1.BackgroundColor = black;

            foreach (Control a in Controls)
            {
                ((IMetroControl)a).UseCustomBackColor = true;
                ((IMetroControl)a).UseCustomForeColor = true;

                a.BackColor = black;
                a.ForeColor = Color.White;

                ((IMetroControl)a).UseStyleColors = false;
            }

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            String name = fvi.ProductName;
            String prod = fvi.ProductName;
            String desc = fvi.Comments;
            String copyright = fvi.LegalCopyright;
            String version = fvi.FileVersion;

            desc += Environment.NewLine + Environment.NewLine;

            desc += "Loaded Modules:" + Environment.NewLine + Environment.NewLine;

            foreach (System.Reflection.Assembly item in AppDomain.CurrentDomain.GetAssemblies().OrderBy(p => p.FullName))
            {
                FileVersionInfo fasm = FileVersionInfo.GetVersionInfo(item.Location);

                if (System.IO.Path.GetFileName(fasm.FileName).Contains(".exe"))
                    continue;

                desc += $"{System.IO.Path.GetFileName(fasm.FileName)} - {fasm.ProductVersion}{Environment.NewLine}";
            }

            this.Text = String.Format(this.Text, name);
            DescBox.Text = desc;
            copyLabel.Text = copyright;
            verLabel.Text = version;
            prodLabel.Text = prod;
        }
    }
}
