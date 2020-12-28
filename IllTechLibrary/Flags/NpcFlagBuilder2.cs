using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IllTechLibrary
{
    public partial class NpcFlagBuilder2 : Form
    {
        private uint ItemFlag;

        public NpcFlagBuilder2(int flag)
        {
            InitializeComponent();

            BuildFlagList(flag);
            FlagTextBox.Text = flag.ToString();
        }

        private void CheckStateChanged(object sender, ItemCheckEventArgs e)
        {
            DoSetBits((sender as CheckedListBox), e);
            this.FlagTextBox.Text = this.ItemFlag.ToString();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public String GetFlag()
        {
            return ItemFlag.ToString();
        }

        private void BuildFlagList(int flag)
        {
            for (int index = 0; index < this.FlagList.Items.Count; ++index)
            {
                if ((flag & 1 << index) != 0)
                    FlagList.SetItemChecked(index, true);
                else
                    FlagList.SetItemChecked(index, false);
            }
        }

#pragma warning disable 414, 3021
        private void DoSetBits(CheckedListBox clb)
        {
            BitArray ba = new BitArray(sizeof(uint) * 8);

            for (int i = 0; i < clb.Items.Count; i++)
            {
                ba.Set(i, clb.GetItemChecked(i));
            }

            byte[] deps = new byte[sizeof(uint)];

            ba.CopyTo(deps, 0);

            ItemFlag = BitConverter.ToUInt32(deps, 0);
        }

        private void DoSetBits(CheckedListBox clb, ItemCheckEventArgs item)
        {
            BitArray ba = new BitArray(sizeof(uint) * 8);

            for (int i = 0; i < clb.Items.Count; i++)
            {
                ba.Set(i, clb.GetItemChecked(i));
            }

            ba.Set(item.Index, item.NewValue == CheckState.Checked ? true : false);

            byte[] deps = new byte[sizeof(uint)];

            ba.CopyTo(deps, 0);

            ItemFlag = BitConverter.ToUInt32(deps, 0);
        }
#pragma warning restore 3021
    }
}
