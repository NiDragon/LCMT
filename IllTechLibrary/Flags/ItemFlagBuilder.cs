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
    public partial class ItemFlagBuilder : Form
    {
        private UInt64 ItemFlag;

        public ItemFlagBuilder(long flag)
        {
            InitializeComponent();
            BuildFlag(flag);
            FlagValueText.Text = flag.ToString();

            PopulateList();
        }

        private void BuildFlag(long flag)
        {
            for (int index = 0; index < this.FlagList.Items.Count; ++index)
            {
                if ((flag & (long)1 << index) != 0)
                    this.FlagList.SetItemChecked(index, true);
                else
                    this.FlagList.SetItemChecked(index, false);
            }
        }

        private void PopulateList()
        {
            for (int i = 0; i < FlagList.Items.Count; i++)
            {
                String addOn = $"{BuildLong(i)} - {FlagList.Items[i]}";
                FlagList.Items[i] = addOn;
            }
        }

        private String BuildLong(int val)
        {
            return "0x" + ((long)1 << val).ToString("X16");
        }

        private void Save_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FlagList_SelectedIndexChanged(object sender, ItemCheckEventArgs e)
        {
            DoSetBits((sender as CheckedListBox), e);
            FlagValueText.Text = this.ItemFlag.ToString();
        }

        public String GetFlag()
        {
            return ItemFlag.ToString();
        }

#pragma warning disable 414, 3021
        private void DoSetBits(CheckedListBox clb)
        {
            BitArray ba = new BitArray(sizeof(UInt64) * 8);

            for (int i = 0; i < clb.Items.Count; i++)
            {
                ba.Set(i, clb.GetItemChecked(i));
            }

            byte[] deps = new byte[sizeof(UInt64)];

            ba.CopyTo(deps, 0);

            ItemFlag = BitConverter.ToUInt64(deps, 0);
        }

        private void DoSetBits(CheckedListBox clb, ItemCheckEventArgs item)
        {
            BitArray ba = new BitArray(sizeof(UInt64) * 8);

            for (int i = 0; i < clb.Items.Count; i++)
            {
                ba.Set(i, clb.GetItemChecked(i));
            }

            ba.Set(item.Index, item.NewValue == CheckState.Checked ? true : false);

            byte[] deps = new byte[sizeof(UInt64)];

            ba.CopyTo(deps, 0);

            ItemFlag = BitConverter.ToUInt64(deps, 0);
        }
#pragma warning restore 3021
    }
}
