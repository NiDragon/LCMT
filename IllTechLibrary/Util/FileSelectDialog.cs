using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary.Util
{
    public class FileSelectDialog
    {
        private String PickedFile;
        private bool Picked = false;

        public FileSelectDialog(String Title, String Filter, String InitDir)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = Title;
            ofd.Filter = Filter;
            ofd.InitialDirectory = InitDir;

            ofd.FileOk += OnFileOK;

            ofd.ShowDialog();
        }

        void OnFileOK(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.PickedFile = ((OpenFileDialog)sender).FileName;
            Picked = true;
        }

        public String GetFile()
        {
            if (Picked)
            {
                return PickedFile;
            }
            else
            {
                return "File Not Selected";
            }
        }
    }
}
