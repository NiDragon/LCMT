using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using IllTechLibrary.Controls;
using IllTechLibrary.DataFiles;
using IllTechLibrary.Serialization;
using IllTechLibrary.SharedStructs;

namespace IllTechLibrary.Dialogs
{
    public partial class ItemSelector : Form
    {
        private static ItemSelector m_inst = null;

        public static ItemSelector Instance()
        {
            if (m_inst == null)
                m_inst = new ItemSelector();

            return m_inst;
        }

        /// <summary>
        /// This is now the items ID not the index in the list!
        /// </summary>
        public int SelectedIndex = -1;
        public int SelectedProb = 0;

        private ItemSelector()
        {
            InitializeComponent();

            FormClosing += OnFormClosing;

            if (ItemCache.IsLoading())
            {
                while (ItemCache.IsLoading())
                {
                    Thread.Sleep(1);
                }

                ItemCache.OrderArray();
            }

            cItemsList.Items.AddRange(ItemCache.GetItems().Select(p => p.Text).ToArray());

            cItemsList.SetImages(ItemCache.GetItems());

            cItemsList.SelectedIndex = -1;
            ItemIdx.Text = "-1";
            ItemProb.Text = "0";

            cItemsList.SelectedIndexChanged += OnListIndexChanged;

            Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
        }

        private void OnListIndexChanged(object sender, EventArgs e)
        {
            SelectedIndex = cItemsList.GetSelectedItemIndex();
            ItemIdx.Text = SelectedIndex.ToString();
        }

        public DialogResult Show(Form parent, int Index)
        {
            DialogResult = DialogResult.None;

            SelectedIndex = Index;
            SelectedProb = 0;

            cItemsList.SetSelectedByIndex(SelectedIndex);

            ItemIdx.Text = SelectedIndex.ToString();
            ItemProb.Text = SelectedProb.ToString();

            parent.Enabled = false;
            parent.MdiParent.MainMenuStrip.Enabled = false;

            Show();

            while (Visible)
            {
                Application.DoEvents();
                Thread.Sleep(10);
            }

            parent.Enabled = true;
            parent.MdiParent.MainMenuStrip.Enabled = true;

            return DialogResult;
        }

        public DialogResult Show(Form parent, int Index, int Prob)
        {
            DialogResult = DialogResult.None;

            SelectedIndex = Index;
            SelectedProb = Prob;

            cItemsList.SetSelectedByIndex(SelectedIndex);

            ItemIdx.Text = SelectedIndex.ToString();
            ItemProb.Text = SelectedProb.ToString();

            parent.Enabled = false;
            parent.MdiParent.MainMenuStrip.Enabled = false;

            Show();

            while(Visible)
            {
                Application.DoEvents();
                Thread.Sleep(10);
            }

            parent.Enabled = true;
            parent.MdiParent.MainMenuStrip.Enabled = true;

            return DialogResult;
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void OnItemIndexChange(object sender, EventArgs e)
        {
            SelectedIndex = cItemsList.GetSelectedItemIndex();
        }

        private void OnItemOK(object sender, EventArgs e)
        {
            SelectedProb = Convert.ToInt32(ItemProb.Text);

            if (ItemIdx.Text != SelectedIndex.ToString())
                int.TryParse(ItemIdx.Text, out SelectedIndex);

            DialogResult = DialogResult.OK;
            Close();
        }

        public void Destroy()
        {
            m_inst.Dispose(true);
            m_inst = null;
        }

        #region SEARCH_REGION

        private int FindIndexID(ItemListItem[] items, int idx)
        {
            return items.ToList().FindIndex(p => p.a_index.Equals(idx));
        }

        private int FindIndexStr(ItemListItem[] items, String text)
        {
            return items.ToList().FindIndex(p => p.Text.ToLower().Contains(text));
        }

        private void OnSearchItems(object sender, EventArgs e)
        {
            if (ItemCache.GetItems().Count() <= 0)
                return;

            if (((TextBox)sender).Text == String.Empty)
                return;

            String search_text = SearchItems.Text;

            int idx = FindIndexStr(ItemCache.GetItems(), search_text);

            if(idx == -1)
                return;

            cItemsList.SelectedIndex = idx;
        }

        #endregion
    }
}
