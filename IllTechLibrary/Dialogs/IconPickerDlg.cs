using IllTechLibrary.Algorithm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary.Dialogs
{
    public partial class IconPickerDlg : Form
    {
        public enum FileType
        {
            ItemBtn,
            SkillBtn,
            ComboBtn,
            ActionBtn,
            EventBtn,
            QuestBtn,
            RemissionBtn
        }

        public struct IconInfo
        {
            public sbyte id, row, col;
        }

        IconInfo retInfo;
        FileType selectedType;

        public IconPickerDlg(FileType filetype)
        {
            this.selectedType = filetype;
            InitializeComponent();
        }

        private void OnCancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            String[] filenames = Directory.GetFiles(".\\Images");

            Array.Sort(filenames, new AlphanumComparatorFast());

            foreach (String a in filenames)
            {
                if(a.Contains(Enum.GetName(typeof(FileType), selectedType)))
                {
                    filesCombo.Items.Add(Path.GetFileName(a));
                }
            }

            filesCombo.SelectedIndex = 0;
        }

        public IconInfo GetInfo()
        {
            return this.retInfo;
        }

        private void OnSelectedFileIndexChanged(object sender, EventArgs e)
        {
            Image file = Image.FromFile(".\\Images\\" + filesCombo.SelectedItem);

            IconBox.Image = file;

            IconBox.Width = file.Width;
            IconBox.Height = file.Height;

            if(IconBox.Width == 512)
            {
                this.MaximumSize = new Size(736, 574);
                this.MinimumSize = new Size(736, 574);
                this.Size = new Size(736, 574);
            }
            else if(IconBox.Height == 512)
            {
                this.MaximumSize = new Size(736 + (IconBox.Width - 512), 574);
                this.MinimumSize = new Size(736 + (IconBox.Width - 512), 574);
                this.Size = new Size(736 + (IconBox.Width - 512), 574);
            }
            else
            {
                this.MaximumSize = new Size(1242, 1080);
                this.MinimumSize = new Size(1242, 1080);
                this.Size = new Size(1242, 1080);
            }
        }

        private void IconBox_Click(object sender, EventArgs e)
        {
            this.retInfo.id = sbyte.Parse(Path.GetFileNameWithoutExtension(filesCombo.SelectedItem.ToString()).Replace(Enum.GetName(typeof(FileType), selectedType), ""));

            Point clickPoint = IconBox.PointToClient(new Point(MousePosition.X, MousePosition.Y));

            this.retInfo.row = (sbyte)(clickPoint.Y / 32);
            this.retInfo.col = (sbyte)(clickPoint.X / 32);

            this.DialogResult = DialogResult.OK;

            this.Close();
        }
    }
}
