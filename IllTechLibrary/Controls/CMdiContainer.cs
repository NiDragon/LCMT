using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace IllTechLibrary.Controls
{
    public partial class CMdiContainer : UserControl
    {
        private const int METRO_FRAME_OFFSET = 31;
        private const int METRO_FRAME_OFFSET_BOTTOM = 34;

        public CMdiContainer()
        {
            InitializeComponent();
        }

        public void AddForm(MetroForm mf)
        {
            mf.FormBorderStyle = FormBorderStyle.None;
            mf.TopLevel = false;
            mf.ShadowType = MetroFramework.Forms.MetroFormShadowType.None;
            mf.DisplayHeader = true;

            // FIX THIS SHIT SOME HOW
            //mf.StyleManager = new MetroFramework.Components.MetroStyleManager();
            //mf.StyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;

            Size formSize = new Size(mf.Size.Width, mf.Size.Height + METRO_FRAME_OFFSET_BOTTOM);

            // Increase max size if needed
            if(formSize.Height > mf.MaximumSize.Height)
                mf.MaximumSize = new Size(formSize.Width, formSize.Height);

            mf.MinimumSize = formSize;
            mf.Size = new Size(formSize.Width, formSize.Height);

            foreach (Control i in mf.Controls)
            {
                i.Location = new Point(i.Location.X, i.Location.Y + METRO_FRAME_OFFSET * 2);
            }

            mf.Show();
            mf.BringToFront();
            Controls.Add(mf);
        }

        public override bool PreProcessMessage(ref Message msg)
        {
            return base.PreProcessMessage(ref msg);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        #region CONTROL_PLACEMENT
        #endregion

        #region DRAW_CODE

        #endregion
    }
}
