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
using IllTechLibrary;
using IllTechLibrary.DataFiles;
using IllTechLibrary.Dialogs;
using IllTechLibrary.Serialization;
using IllTechLibrary.Settings;
using IllTechLibrary.SharedStructs;
using IllTechLibrary.Util;

namespace LCMT.LuckyTool
{
    public partial class LuckyFrm : LCToolFrm
    {
        public static String LuckyDrawToolID = "LUCKY_TOOL";

        public List<LuckyDrawBox> m_box = new List<LuckyDrawBox>();
        public List<LuckyDrawBoxNeed> m_boxneed = new List<LuckyDrawBoxNeed>();
        public List<LuckyDrawResult> m_results = new List<LuckyDrawResult>();

        public List<Item> m_items = new List<Item>();

        private Deserialize<LuckyDrawResult> resultDesc = new Deserialize<LuckyDrawResult>("t_luckydrawresult");
        private Deserialize<LuckyDrawBoxNeed> needDesc = new Deserialize<LuckyDrawBoxNeed>("t_luckydrawneed");
        private Deserialize<LuckyDrawBox> boxDesc = new Deserialize<LuckyDrawBox>("t_luckydrawbox");
        private Deserialize<Item> itemDesc = new Deserialize<Item>("t_item");

        private delegate void NoReturn();
        private delegate void LoadStateChanger(bool state);

        // Current Selected ID
        private int SelectedId = -1;

        private double loadAllTime;

        private String LocalNameString
        {
            get { return String.Format("a_name_{0}", Core.LangCode); }
        }

        public LuckyFrm() : base(LuckyDrawToolID)
        {
            InitializeComponent();
        }

        // Overloads
        public override void OnConnect()
        {
            resultDesc = new Deserialize<LuckyDrawResult>("t_luckydrawresult");
            needDesc = new Deserialize<LuckyDrawBoxNeed>("t_luckydrawneed");
            boxDesc = new Deserialize<LuckyDrawBox>("t_luckydrawbox");
            itemDesc = new Deserialize<Item>("t_item");

            DoLoadAll();

            DoSetItemStateConditional(DatabaseContextMenu.DropDown, "EnableOnConnected", true);
        }

        public override void OnDisconnect()
        {
            DoSetItemStateConditional(DatabaseContextMenu.DropDown, "EnableOnConnected", false);

            DoClearContainers();
        }

        private void DoClearContainers()
        {
            SelectedId = -1;

            m_box.Clear();
            m_items.Clear();
            m_boxneed.Clear();
            m_results.Clear();

            lb_box.Items.Clear();
            lb_boxresult.Items.Clear();
            dg_needitem.Rows.Clear();

            tb_count.Clear();
            tb_upgrade.Clear();
            tb_prob.Clear();
            tb_flag.Clear();

            tb_Id.Clear();
            tb_name.Clear();
            cbx_random.SelectedIndex = -1;

            cb_enable.Checked = false;
        }

        private void DoClearNoRefresh()
        {
            lb_boxresult.Items.Clear();
            dg_needitem.Rows.Clear();

            tb_count.Clear();
            tb_upgrade.Clear();
            tb_prob.Clear();
            tb_flag.Clear();

            tb_Id.Clear();
            tb_name.Clear();
            cbx_random.SelectedIndex = -1;

            cb_enable.Checked = false;
        }

        private void DoSetItemStateConditional(ToolStripDropDown toolStripDropDown, string p1, bool p2)
        {
            foreach (ToolStripItem item in toolStripDropDown.Items)
            {
                if ((String)item.Tag == p1)
                {
                    item.Enabled = p2;
                }
            }
        }

        private void DoLoadAll()
        {
            ShowSpinner(true);

            DoClearContainers();

            BackgroundWorker loadAll = new BackgroundWorker();

            loadAll.DoWork += OnLoadAll;
            loadAll.RunWorkerCompleted += OnLoadAllComplete;
            loadAll.RunWorkerAsync();
        }

        private void OnLoadAllComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            DoRebuildList();

            ShowSpinner(false);
        }

        private void DoRebuildList()
        {
            if (lb_box.InvokeRequired)
            {
                lb_box.Invoke(new NoReturn(DoRebuildList));
            }
            else
            {
                lb_box.Items.Clear();

                m_box.Sort((x, y) => x.a_index.CompareTo(y.a_index));

                String[] listBoxEntries = new String[m_box.Count];

                for (int i = 0; i < m_box.Count; i++)
                {
                    boxDesc.SetData(m_box[i]);
                    listBoxEntries[i] = (String.Format("{0} - {1}", m_box[i].a_index, m_box[i].a_name));
                }

                lb_box.Items.AddRange(listBoxEntries);

                lb_box.SelectedIndex = lb_box.FindString(Convert.ToString(SelectedId));
            }

            GeneralStatsLabel.Text = $"Stats: Total Box - {m_box.Count} Enabled - {m_box.Count(p => p.a_enable == 1)} Disabled - {m_box.Count(p => p.a_enable == 0)}, Load Time: {loadAllTime} ms";
        }

        private void OnLoadAll(object sender, DoWorkEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            m_box = new Transactions<LuckyDrawBox>(DataCon).ExecuteQuery(boxDesc);
            m_box.Sort((x, y) => x.a_index.CompareTo(y.a_index));

            m_boxneed = new Transactions<LuckyDrawBoxNeed>(DataCon).ExecuteQuery(needDesc);
            m_results = new Transactions<LuckyDrawResult>(DataCon).ExecuteQuery(resultDesc);
            
            m_items = new Transactions<Item>(DataCon).ExecuteQuery(itemDesc);
            m_items.Sort((x, y) => x.a_index.CompareTo(y.a_index));

            ItemCache.PreloadItems(m_items);

            TimeSpan time = sw.Elapsed;
            sw.Stop();

            loadAllTime = time.TotalMilliseconds;
        }

        private void DoFillWindow()
        {
        }

        private void OnDoExit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowSpinner(bool show)
        {
            if (ProgressWheel.InvokeRequired)
            {
                ProgressWheel.Invoke(new LoadStateChanger(ShowSpinner), new Object[] {
                    show});
            }
            else
            {
                ProgressWheel.Visible = show;
            }
        }

        private void OnBoxIndexChange(object sender, EventArgs e)
        {
            DoClearNoRefresh();

            int idx = lb_box.SelectedIndex;

            if (idx != -1)
            {
                List<LuckyDrawResult> res = m_results.FindAll(p => p.a_luckydraw_idx == m_box[idx].a_index).ToList();

                tb_Id.Text = m_box[idx].a_index.ToString();
                tb_name.Text = m_box[idx].a_name;
                cb_enable.Checked = m_box[idx].a_enable == 1 ? true : false;
                cbx_random.SelectedIndex = m_box[idx].a_random;

                foreach(LuckyDrawResult b in res)
                {
                    Item item = m_items.Find(p => p.a_index == b.a_item_idx);

                    if (item != null)
                    {
                        itemDesc.SetData(item);

                        lb_boxresult.Items.Add($"{b.a_index} - {itemDesc[LocalNameString].ToString()}");
                    }
                }

                List<LuckyDrawBoxNeed> needs = m_boxneed.FindAll(p => p.a_luckydraw_idx == m_box[idx].a_index).ToList();

                foreach(LuckyDrawBoxNeed b in needs)
                {
                    Item item = m_items.Find(p => p.a_index == b.a_item_idx);

                    if (item != null)
                    {
                        itemDesc.SetData(item);

                        int index = dg_needitem.Rows.Add(1);

                        DataGridViewRow aRow = dg_needitem.Rows[index];

                        aRow.Cells[0].Value = IconCache.GetItemIcon(item.a_texture_id, item.a_texture_row, item.a_texture_col);
                        aRow.Cells[1].Value = item.a_index;
                        aRow.Cells[2].Value = itemDesc[LocalNameString];
                        aRow.Cells[3].Value = b.a_count;
                    }
                }

                SelectedId = idx;

                tb_count.Text = "";
                tb_upgrade.Text = "";
                tb_prob.Text = "";
                tb_flag.Text = "";
            }
        }

        private void OnBoxResultIndexChange(object sender, EventArgs e)
        {
            int idx = lb_boxresult.SelectedIndex;

            if (idx != -1)
            {
                if (SelectedId != -1 && m_box.Count != 0)
                {
                    int val = int.Parse(lb_boxresult.Items[idx].ToString().Split(new char[] { '-' })[0]);

                    LuckyDrawResult item = m_results.Find(p => p.a_index == val);

                    if (item != null)
                    {
                        tb_count.Text = item.a_count.ToString();
                        tb_upgrade.Text = item.a_upgrade.ToString();
                        tb_prob.Text = item.a_prob.ToString();
                        tb_flag.Text = item.a_flag.ToString();
                    }
                }
            }
        }

        private void OnFixNames(object sender, EventArgs e)
        {
            if ((m_box.Count == 0 || m_items.Count == 0) || true)
            {
                MsgDialogs.ShowNoLog("Not Enabled!", "This Feature Is Not Enabled!", "OK", MsgDialogs.MsgTypes.INFO);
                return;
            }

            Transactions<LuckyDrawBox> transBox = new Transactions<LuckyDrawBox>(DataCon);

            for (int i = 0; i < m_box.Count; i++)
            {
                LuckyDrawBox box = m_box[i];

                Item item = m_items.Find(p => p.a_num_0 == box.a_index && p.a_type_idx == 2 && p.a_subtype_idx == 3);

                if (item != null)
                {
                    itemDesc.SetData(item);

                    box.a_name = itemDesc[LocalNameString].ToString();

                    boxDesc.SetData(box);
                    boxDesc.SetKey("a_index");

                    transBox.ExecuteQuery(boxDesc, QUERY_TYPE.UPDATE);
                }
                else
                {
                    box.a_name = "No Name";

                    boxDesc.SetData(box);
                    boxDesc.SetKey("a_index");

                    transBox.ExecuteQuery(boxDesc, QUERY_TYPE.UPDATE);
                }
            }

            DoRebuildList();
        }

        private void OnAddBox(object sender, EventArgs e)
        {
            if (m_box.Count == 0)
                return;

            LuckyDrawBox nb = new LuckyDrawBox();

            nb.a_enable = 1;
            nb.a_index = m_box.Max(p => p.a_index) + 1;
            nb.a_name = "New Box";
            nb.a_random = 0;

            m_box.Add(nb);

            Transactions<LuckyDrawBox> trans = new Transactions<LuckyDrawBox>(DataCon);

            boxDesc.SetData(nb);
            trans.ExecuteQuery(boxDesc, QUERY_TYPE.INSERT);

            DoRebuildList();

            int idx = m_box.FindIndex(p => p.a_index == nb.a_index);

            lb_box.SelectedIndex = idx;
        }

        private void OnRemoveBox(object sender, EventArgs e)
        {
            if (lb_box.SelectedIndex == -1)
                return;

            LuckyDrawBox box = m_box[lb_box.SelectedIndex];

            // Remove Require Items
            Transactions<LuckyDrawBoxNeed> need = new Transactions<LuckyDrawBoxNeed>(DataCon);

            LuckyDrawBoxNeed lbn = new LuckyDrawBoxNeed();
            lbn.a_luckydraw_idx = box.a_index;

            needDesc.SetData(lbn);
            needDesc.SetKey("a_luckydraw_idx");

            need.ExecuteQuery(needDesc, QUERY_TYPE.DELETE);

            // Remove Reward Items
            Transactions<LuckyDrawResult> result = new Transactions<LuckyDrawResult>(DataCon);

            LuckyDrawResult ldr = new LuckyDrawResult();
            ldr.a_luckydraw_idx = box.a_index;

            resultDesc.SetData(ldr);
            resultDesc.SetKey("a_luckydraw_idx");

            result.ExecuteQuery(resultDesc, QUERY_TYPE.DELETE);

            // Remove Box
            Transactions<LuckyDrawBox> trans = new Transactions<LuckyDrawBox>(DataCon);

            boxDesc.SetData(box);
            boxDesc.SetKey("a_index");

            trans.ExecuteQuery(boxDesc, QUERY_TYPE.DELETE);

            m_box.RemoveAt(lb_box.SelectedIndex);

            DoClearNoRefresh();
            DoRebuildList();
        }

        private void OnUpdateBox(object sender, EventArgs e)
        {
            if (lb_box.SelectedIndex == -1)
                return;

            int idx = lb_box.SelectedIndex;

            // Set the new box info
            LuckyDrawBox box = m_box[idx];

            box.a_enable = cb_enable.Checked ? 1 : 0;
            box.a_name = tb_name.Text;
            box.a_random = cbx_random.SelectedIndex;

            // Perform Update
            Transactions<LuckyDrawBox> trans = new Transactions<LuckyDrawBox>(DataCon);

            boxDesc.SetData(box);
            boxDesc.SetKey("a_index");

            trans.ExecuteQuery(boxDesc, QUERY_TYPE.UPDATE);

            // Rebuild List Reset Index
            DoRebuildList();

            lb_box.SelectedIndex = idx;
        }

        private void OnAddResultItem(object sender, EventArgs e)
        {
            if (lb_box.SelectedIndex == -1)
                return;

            LuckyDrawBox box = m_box[lb_box.SelectedIndex];

            ItemSelector selector = ItemSelector.Instance();//new ItemSelector(-1, 0);

            // This instance has been updated!
            if(selector.Show(this, -1, 1) == DialogResult.OK)
            {
                // Check if this item is not already part of this box
                if(m_results.FindIndex(p => p.a_luckydraw_idx == box.a_index && p.a_item_idx == selector.SelectedIndex) == -1)
                {
                    // Require at least 1 count
                    if (selector.SelectedProb > 0)
                    {
                        // Create Result Item
                        LuckyDrawResult ldr = new LuckyDrawResult();

                        ldr.a_index = m_results.Max(p => p.a_index) + 1;
                        ldr.a_count = 1;
                        ldr.a_flag = 0;
                        ldr.a_item_idx = selector.SelectedIndex;
                        ldr.a_luckydraw_idx = box.a_index;
                        ldr.a_prob = selector.SelectedProb;
                        ldr.a_upgrade = 0;

                        Item item = m_items.Find(p => p.a_index.Equals(selector.SelectedIndex));

                        // Make sure this item is an existing item
                        if (item != null)
                        {
                            m_results.Add(ldr);

                            itemDesc.SetData(item);

                            resultDesc.SetData(ldr);

                            // Insert into db
                            Transactions<LuckyDrawResult> resultTrans = new Transactions<LuckyDrawResult>(DataCon);

                            resultTrans.ExecuteQuery(resultDesc, QUERY_TYPE.INSERT);

                            // Add to listbox and update
                            int sel = lb_boxresult.Items.Add($"{ldr.a_index} - {itemDesc[LocalNameString].ToString()}");
                            lb_boxresult.SelectedIndex = sel;
                        }
                    }
                }
                else
                {
                    MsgDialogs.ShowNoLog("Invalid Item!", "Box Already Has Reward Item.", "OK", MsgDialogs.MsgTypes.ERROR);
                }
            }
        }

        private void OnRemoveResultItem(object sender, EventArgs e)
        {
            if (lb_box.SelectedIndex == -1 || lb_boxresult.SelectedIndex == -1)
                return;

            LuckyDrawBox box = m_box[lb_box.SelectedIndex];

            int itemIdx = int.Parse(lb_boxresult.Items[lb_boxresult.SelectedIndex].ToString().Split(new char[] { '-' })[0]);

            int idx = m_results.FindIndex(p => p.a_index == itemIdx);

            if (idx != -1)
            {
                LuckyDrawResult result = m_results[idx];

                resultDesc.SetData(result);
                resultDesc.SetKey("a_index");

                Transactions<LuckyDrawResult> trans = new Transactions<LuckyDrawResult>(DataCon);

                trans.ExecuteQuery(resultDesc, QUERY_TYPE.DELETE);

                m_results.RemoveAt(idx);
                lb_boxresult.Items.RemoveAt(lb_boxresult.SelectedIndex);

                lb_boxresult.SelectedIndex = -1;

                tb_count.Text = "";
                tb_upgrade.Text = "";
                tb_prob.Text = "";
                tb_flag.Text = "";
            }
        }

        private void OnUpdateResultItem(object sender, EventArgs e)
        {
            if (lb_box.SelectedIndex == -1 || lb_boxresult.SelectedIndex == -1)
                return;

            LuckyDrawBox box = m_box[lb_box.SelectedIndex];

            int itemIdx = int.Parse(lb_boxresult.Items[lb_boxresult.SelectedIndex].ToString().Split(new char[] { '-' })[0]);

            int idx = m_results.FindIndex(p => p.a_index == itemIdx);

            if(idx != -1)
            {
                LuckyDrawResult result = m_results[idx];

                ulong.TryParse(tb_count.Text, out result.a_count);
                int.TryParse(tb_flag.Text, out result.a_flag);
                int.TryParse(tb_prob.Text, out result.a_prob);
                int.TryParse(tb_upgrade.Text, out result.a_upgrade);

                resultDesc.SetData(result);
                resultDesc.SetKey("a_index");

                Transactions<LuckyDrawResult> trans = new Transactions<LuckyDrawResult>(DataCon);

                trans.ExecuteQuery(resultDesc, QUERY_TYPE.UPDATE);
            }
        }

        private void OnAddRequireItem(object sender, EventArgs e)
        {
            if (lb_box.SelectedIndex == -1)
                return;

            ItemSelector selector = ItemSelector.Instance();

            // This instance has been updated!
            if (selector.Show(this, -1, 1) == DialogResult.OK)
            {
                LuckyDrawBox box = m_box[lb_box.SelectedIndex];

                // Create new need
                LuckyDrawBoxNeed needItem = new LuckyDrawBoxNeed();

                // Make sure this item exists in the collection and the count is at least 1
                if (m_items.FindIndex(p => p.a_index.Equals(selector.SelectedIndex)) != -1 && selector.SelectedProb > 0)
                {
                    needItem.a_index = m_boxneed.Max(p => p.a_index) + 1;
                    needItem.a_luckydraw_idx = box.a_index;
                    needItem.a_item_idx = selector.SelectedIndex;
                    needItem.a_count = (ulong)selector.SelectedProb;

                    // Check to make sure the box does not already have this reward
                    if (m_boxneed.FindIndex(p => p.a_luckydraw_idx == box.a_index && p.a_item_idx == needItem.a_item_idx) == -1)
                    {
                        m_boxneed.Add(needItem);

                        // Update Database
                        Transactions<LuckyDrawBoxNeed> trans = new Transactions<LuckyDrawBoxNeed>(DataCon);

                        needDesc.SetData(needItem);
                        trans.ExecuteQuery(needDesc, QUERY_TYPE.INSERT);

                        // Update view
                        int last = lb_box.SelectedIndex;

                        lb_box.SelectedIndex = -1;
                        lb_box.SelectedIndex = last;
                    }
                    else
                    {
                        MsgDialogs.ShowNoLog("Invalid Item!", "Box Already Has Required Item.", "OK", MsgDialogs.MsgTypes.ERROR);
                    }
                }
            }
        }

        private void OnRemoveRequireItem(object sender, EventArgs e)
        {
            if (lb_box.SelectedIndex == -1 || dg_needitem.SelectedRows.Count == 0)
                return;

            LuckyDrawBox box = m_box[lb_box.SelectedIndex];
            DataGridViewRow dgvr = dg_needitem.SelectedRows[0];

            int item_idx = (int)dgvr.Cells[1].Value;

            int idx = m_boxneed.FindIndex(p => p.a_luckydraw_idx == box.a_index && p.a_item_idx == item_idx);

            if(idx != -1)
            {
                LuckyDrawBoxNeed need = m_boxneed[idx];

                Transactions<LuckyDrawBoxNeed> trans = new Transactions<LuckyDrawBoxNeed>(DataCon);

                needDesc.SetData(need);
                needDesc.SetKey("a_index");

                trans.ExecuteQuery(needDesc, QUERY_TYPE.DELETE);

                m_boxneed.RemoveAt(idx);

                int last = lb_box.SelectedIndex;

                lb_box.SelectedIndex = -1;
                lb_box.SelectedIndex = last;
            }
        }
    }
}
