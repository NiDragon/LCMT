using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary.Dialogs
{
    public partial class AnimationPicker : Form
    {
        private String filename = String.Empty;
        private String selectedAnimation = String.Empty;
        private List<String> animNames = new List<String>();

        public AnimationPicker(String filename)
        {
            InitializeComponent();
            this.filename = filename;

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
        }

        private void AnimationPicker_Load(object sender, EventArgs e)
        {
            BinaryReader br = new BinaryReader(File.Open(filename, FileMode.Open));

            String tag = Encoding.ASCII.GetString(br.ReadBytes(4));

            if (tag != "ANIM")
                return;

            int ver = br.ReadInt32();

            if (ver != 14)
                return;

            int animCount = br.ReadInt32();
            
            for (int i = 0; i < animCount; i++)
            {
                String Ska = Encoding.ASCII.GetString(br.ReadBytes(br.ReadInt32()));

                // Add animation name
                animNames.Add(Encoding.ASCII.GetString(br.ReadBytes(br.ReadInt32())));

                // Name ID
                br.ReadInt32();
                
                // Secs Per Frame
                br.ReadInt32();

                // Num of Frames
                br.ReadInt32();
                
                // Threshold
                br.ReadInt32();

                // Compression
                br.ReadInt16();
                // Custom Speed
                br.ReadInt16();

                int JointCount = br.ReadInt32();

                for (int j = 0; j < JointCount; j++)
                {
                    int len = br.ReadInt32();
                    String boneName = Encoding.ASCII.GetString(br.ReadBytes(len));

                    br.ReadBytes(sizeof(float) * 12);

                    int ctp = br.ReadInt32();

                    for (int k = 0; k < ctp; k++)
                    {
                        br.ReadInt32();
                        br.ReadSingle();
                        br.ReadSingle();
                        br.ReadSingle();
                    }

                    int ctRots = br.ReadInt32();

                    for (int k = 0; k < ctRots; k++)
                    {
                        br.ReadInt32();
                        br.ReadSingle();
                        br.ReadSingle();
                        br.ReadSingle();
                        br.ReadSingle();
                    }

                    int OptRots = br.ReadInt32();
                }

                int ctme = br.ReadInt32();

                for (int j = 0; j < ctme; j++)
                {
                    br.ReadBytes(br.ReadInt32());

                    int ctmf = br.ReadInt32();

                    br.ReadBytes(ctmf * sizeof(float));
                }
            }

            br.Close();
            br.Dispose();

            foreach (String a in animNames)
            {
                animListBox.Items.Add(a);
            }
        }

        public String GetName()
        {
            return selectedAnimation;
        }

        private void animSelect_Click(object sender, EventArgs e)
        {
            if (animListBox.SelectedIndex == -1)
                return;

            selectedAnimation = animListBox.Items[animListBox.SelectedIndex].ToString();
            this.Close();
        }

        private void search_text_TextChanged(object sender, EventArgs e)
        {
            if (animListBox.Items.Count == 0)
                return;


            if (search_text.Text == String.Empty)
            {
                animListBox.Items.Clear();
                animListBox.Items.AddRange(animNames.ToArray());
                return;
            }

            List<String> items = animNames.FindAll(p => p.Contains(search_text.Text));

            if (items.Count != 0)
            {
                animListBox.Items.Clear();
                animListBox.Items.AddRange(items.ToArray());

                animListBox.SelectedIndex = 0;
            }
        }
    }
}
