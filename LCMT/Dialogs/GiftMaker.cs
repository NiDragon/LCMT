using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IllTechLibrary.Controls;
using IllTechLibrary.DataFiles;
using IllTechLibrary.Serialization;
using IllTechLibrary.SharedStructs;
using IllTechLibrary.Util;

namespace LCMT.Dialogs
{
    public partial class GiftMaker : Form
    {
        private string m_sender;
        private string m_message;

        private int m_package;

        public GiftMaker()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public string Sender
        {
            get { return m_sender; }
        }

        public string Message
        {
            get { return m_message; }
        }

        public int Package
        {
            get { return m_package; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbSendName.Text == String.Empty)
            {
                MsgDialogs.ShowNoLog("Error", "Required Field Was Blank!", "OK", MsgDialogs.MsgTypes.ERROR);
            }
            else
            {
                if (PackageBox.SelectedIndex != -1)
                {
                    m_sender = tbSendName.Text;
                    m_message = tbMessage.Text;

                    m_package = PackageBox.listItems[PackageBox.SelectedIndex].a_index;

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MsgDialogs.ShowNoLog("Error", "No Package Selected!", "OK", MsgDialogs.MsgTypes.ERROR);
                }
            }
        }

        private string LocaleNameString
        {
            get
            {
                if(IllTechLibrary.Core.LangCode == "thai")
                {
                    return "a_ctname_tld";
                }

                return "a_ctname";
            }            
        }

        internal void SetCatalog(List<CatalogData> catalog, List<Item> items)
        {
            ItemListItem tmpItem = new ItemListItem();
            List<ItemListItem> tmpList = new List<ItemListItem>();

            Deserialize<CatalogData> catDesc = new Deserialize<CatalogData>("t_catalog");

            foreach(CatalogData a in catalog)
            {
                catDesc.SetData(a);

                Item i = items.Find(p => p.a_index.Equals((int)a.a_icon));

                if (i == null)
                    i = items.Find(p => p.a_index.Equals(19));

                tmpItem.a_index = (int)a.a_ctid;
                //tmpItem.Icon = (Bitmap)IconCache.GetItemIcon(i.a_texture_id, i.a_texture_row, i.a_texture_col);
                tmpItem.id = i.a_texture_id;
                tmpItem.row = i.a_texture_row;
                tmpItem.col = i.a_texture_col;
                tmpItem.Text = (string)catDesc[LocaleNameString];

                tmpList.Add(tmpItem);
            }

            PackageBox.Items.AddRange(tmpList.Select(p => p.Text).ToArray());
            PackageBox.listItems = tmpList.ToArray();
        }
    }
}
