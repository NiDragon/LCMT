using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

using IllTechLibrary;
using IllTechLibrary.Util;
using IllTechLibrary.Settings;
using IllTechLibrary.Dialogs;
using IllTechLibrary.DataFiles;
using IllTechLibrary.Localization;
using IllTechLibrary.Serialization;
using IllTechLibrary.SharedStructs;

using MetroFramework.Controls;
using System.Drawing.Imaging;
using System.Collections;

using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using IllTechLibrary.Enums;

namespace LCMT.NpcTool
{
    public partial class NpcFrm : LCToolFrm
    {
        private List<NpcData> data = new List<NpcData>();
        private List<DropItem> items = new List<DropItem>();
        private List<ShopInfo> shops = new List<ShopInfo>();
        private List<ShopItem> itemsdata = new List<ShopItem>();
        private List<NpcDropAll> dropAllItems = new List<NpcDropAll>();

        Deserialize<NpcData> npcDesc = new Deserialize<NpcData>("t_npc");
        Deserialize<NpcData> npcBack = new Deserialize<NpcData>("t_npc");
        Deserialize<DropItem> dropDesc = new Deserialize<DropItem>("t_item");
        Deserialize<ShopInfo> shopDesc = new Deserialize<ShopInfo>("t_shop");
        Deserialize<ShopItem> itemDesc = new Deserialize<ShopItem>("t_shopitem");

        // Renderer Object
        //CRenderer renderer;

        // Error Icon Object
        private Bitmap m_errorIcon = SystemIcons.Error.ToBitmap();

        // Desired Encoding
        private Encoding Enc = Encoding.ASCII;

        // Delegate declarations
        private delegate void LoadStateChanger(bool state);
        private delegate String GetStringDel();
        private delegate void NoReturn();
        private delegate void SetString(String str);

        // Search Delegates
        private GetStringDel GetSearchText;
        private NoReturn ClearNpcList;
        private SetString AddNpcList;

        // Last File Saved
        private String LastSaveFile = "";

        // Current Selected ID
        private int SelectedId = -1;
        private double loadAllTime = 0.0;

        private String LocalNameString
        {
            get { return String.Format("a_name_{0}", Core.LangCode); }
        }

        private String LocalDescString
        {
            get { return String.Format("a_descr_{0}", Core.LangCode); }
        }

        // Tool ID Tag
        public static String NpcToolID = "NPC_TOOL";

        public NpcFrm() : base(NpcToolID)
        {
            InitializeComponent();

            //TrackControls<TextBox>(mainTabControl);
        }

        private static bool IsLoading = true;

        private void OnFormLoad(object sender, EventArgs e)
        {
            GetSearchText = new GetStringDel(GetSearchString);
            ClearNpcList = new NoReturn(NpcClearList);
            AddNpcList = new SetString(AddNpcToList);

            chk3D.Checked = Convert.ToBoolean(Preferences.GetPrefs("View3DEnabled"));

            mainTabControl.SelectedIndex = 0;

            Bitmap target = new Bitmap(m_errorIcon.Size.Width, m_errorIcon.Size.Height);

            Graphics g = Graphics.FromImage(target);

            g.FillRectangle(new SolidBrush(Color.White), 0, 0, target.Width, target.Height);
            g.DrawImage(m_errorIcon, 0, 0);
            g.Dispose();

            m_errorIcon = target;
        }

        private void AddNpcToList(String str)
        {
            if (NpcList.InvokeRequired)
            {
                NpcList.Invoke(new SetString(AddNpcList), new Object[] { str });
            }
            else
            {
                NpcList.Items.Add(str);
            }
        }

        private void NpcClearList()
        {
            if (NpcList.InvokeRequired)
            {
                NpcList.Invoke(new NoReturn(ClearNpcList));
            }
            else
            {
                NpcList.Items.Clear();
            }
        }

        private String GetSearchString()
        {
            if (SearchBox.InvokeRequired)
            {
                return (String)SearchBox.Invoke(new GetStringDel(GetSearchString));
            }
            else
            {
                return SearchBox.Text;
            }
        }

        public override void OnConnect()
        {
            base.OnConnect();

            npcDesc = new Deserialize<NpcData>("t_npc");
            dropDesc = new Deserialize<DropItem>("t_item");
            shopDesc = new Deserialize<ShopInfo>("t_shop");
            itemDesc = new Deserialize<ShopItem>("t_shopitem");

            tbName_USA.Name = String.Format("tbName_{0}", Core.LangCode);

            BeginChangeContent();

            DoResetWindow();

            EndChangeContent();

            DoLoadAll();

            DoSetItemStateConditional(DatabaseContextMenu.DropDown, "EnableOnConnected", true);
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

        public override void OnDisconnect()
        {
            base.OnDisconnect();

            SelectedId = -1;

            DoSetItemStateConditional(DatabaseContextMenu.DropDown, "EnableOnConnected", false);

            BeginChangeContent();

            DoResetWindow();

            EndChangeContent();
        }

        private void DoResetWindow()
        {
            data.Clear();
            items.Clear();
            shops.Clear();
            itemsdata.Clear();

            NpcList.Items.Clear();

            List<Control> textBoxes = ControlUtil.GetAll(this.mainTabControl, typeof(TextBox)).ToList();

            foreach (TextBox a in textBoxes)
            {
                a.Clear();
            }

            textBoxes = ControlUtil.GetAll(this.mainTabControl, typeof(ComboBox)).ToList();

            foreach(ComboBox a in textBoxes)
            {
                a.SelectedIndex = -1;
            }

            textBoxes = ControlUtil.GetAll(this.mainTabControl, typeof(NumericUpDown)).ToList();

            foreach (NumericUpDown a in textBoxes)
            {
                a.Value = 0;
            }

            dgShop.Rows.Clear();
            dropListGrid.Rows.Clear();
        }

        private void DoLoadAll()
        {
            AddTask(OnLoadAll);
        }

        private void OnLoadAll()
        {
            ClearNpcList();

            SpinnerState(true);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            itemDesc.SetWhere("a_local");

            TaskBlock(delegate() {
                data = new Transactions<NpcData>(DataCon).ExecuteQuery(npcDesc);
                data.Sort((x, y) => x.a_index.CompareTo(y.a_index));
            });

            TaskBlock(delegate() {
                items = new Transactions<DropItem>(DataCon).ExecuteQuery(dropDesc);
                items.Sort((x, y) => x.a_index.CompareTo(y.a_index));
            });

            TaskBlock(delegate() {
                shops = new Transactions<ShopInfo>(DataCon).ExecuteQuery(shopDesc);
                shops.Sort((x, y) => x.a_keeper_idx.CompareTo(y.a_keeper_idx));
            });

            TaskBlock(delegate() {
                itemsdata = new Transactions<ShopItem>(DataCon).ExecuteQuery(itemDesc);

                ItemCache.PreloadDropItems(items);
            });

            TimeSpan time = sw.Elapsed;
            sw.Stop();

            loadAllTime = time.TotalMilliseconds;

            TaskBlock(delegate {
                DoRebuildList();
            });

            SpinnerState(false);
        }

        private void DoRebuildList()
        {
            if (NpcList.InvokeRequired)
            {
                NpcList.Invoke(new NoReturn(DoRebuildList));
            }
            else
            {
                NpcList.Items.Clear();

                data.Sort((x, y) => x.a_index.CompareTo(y.a_index));

                NpcList.Items.AddRange(data.Select(p => $"{p.a_index} - {npcDesc.SetData(p)[LocalNameString]}").ToArray());

                NpcList.SelectedIndex = NpcList.FindString(Convert.ToString(SelectedId));

                GeneralStatsLabel.Text = $"Stats: Total Items - {data.Count} Enabled - {data.Count(p => p.a_enable == 1)} Disabled - {data.Count(p => p.a_enable == 0)}, Load Time: {loadAllTime} ms";
            }
        }

        private void OnSearchTextChanged(object sender, EventArgs e)
        {
            if (data.Count <= 0)
                return;

            if (items.Count <= 0)
                return;

            if (SearchBox.Text == String.Empty || SearchBox.Text == "Search...")
            {
                return;
            }

            List<NpcData> itCollection = new List<NpcData>();

            switch (Core.LangCode)
            {
                case "usa":
                    itCollection = data.FindAll(p => p.a_name_usa.ToLower().Contains(SearchBox.Text.ToLower()));

                    if (itCollection.Count != 0)
                    {
                        ClearNpcList();
                        NpcList.Items.AddRange(itCollection.Select(p => p.a_index + " - " + p.a_name_usa).ToArray());
                        NpcList.SelectedIndex = 0;
                    }
                    break;
                case "thai":
                    itCollection = data.FindAll(p => p.a_name_thai.ToLower().Contains(SearchBox.Text.ToLower()));

                    if (itCollection.Count != 0)
                    {
                        ClearNpcList();
                        NpcList.Items.AddRange(itCollection.Select(p => p.a_index + " - " + p.a_name_thai).ToArray());
                        NpcList.SelectedIndex = 0;
                    }
                    break;
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {/*
            try
            {
                renderer.Stop();
                renderer.Dispose();
            }
            catch (Exception)
            {
            }*/
        }

        private void OnReload(object sender, EventArgs e)
        {
            if (DataCon.IsConnected())
            {
                OnDisconnect();
                OnConnect();
            }
        }

        private void DoFillWindow()
        {
            if (data.Count <= 0)
                return;

            WindowFilling = true;

            NpcData d = data.Find(p => p.a_index == SelectedId);
            npcDesc.SetData(d);

            cb_enabled.Checked = d.a_enable == 1 ? true : false;

            List<Control> textBoxes = ControlUtil.GetAll(this.mainTabControl, typeof(TextBox)).ToList();

            foreach (TextBox a in textBoxes)
            {
                String realName = a.Name.Replace("tb", "").Replace("Motion", "motion_").ToLower();
                String[] members = new String[1];

                int anIndex = 0;

                if (realName.Contains("skill0") || realName.Contains("skill1") || realName.Contains("skill2") || realName.Contains("skill3"))
                {
                    anIndex = npcDesc.nameList.FindIndex(p => p.ToLower().Equals("a_" + "skill" + realName.Replace("level", "").Replace("id", "").Replace("skill", "")));

                    if (anIndex != -1)
                        members = Convert.ToString(npcDesc[anIndex]).Split(' ');

                    if (realName.Contains("id") && members.Count() > 0)
                    {
                        String v = Convert.ToString(members[0]);
                        a.Text = (v == String.Empty || v == "0") ? "-1" : v;
                    }
                    else
                    {
                        if (realName.Contains("id"))
                            a.Text = "-1";
                    }

                    if (realName.Contains("level") && members.Count() > 1)
                    {
                        String v = Convert.ToString(members[1]);
                        a.Text = v == "-1" ? "0" : v;
                    }
                    else
                    {
                        if (realName.Contains("level"))
                            a.Text = "0";
                    }

                    continue;
                }

                anIndex = npcDesc.nameList.FindIndex(p => p.ToLower().Equals("a_"+realName.ToLower()));
                if (anIndex != -1)
                    a.Text = Convert.ToString(npcDesc[anIndex]);
            }

            textBoxes = ControlUtil.GetAll(this.mainTabControl, typeof(ComboBox)).ToList();

            foreach (ComboBox a in textBoxes)
            {
                String realName = a.Name.Replace("cb", "");

                int anIndex = npcDesc.nameList.FindIndex(p => p.ToLower().Equals("a_"+realName.ToLower()));
                if (anIndex != -1)
                    a.SelectedIndex = Convert.ToInt32(npcDesc[anIndex]);
            }

            textBoxes = ControlUtil.GetAll(this.mainTabControl, typeof(NumericUpDown)).ToList();

            foreach (NumericUpDown a in textBoxes)
            {
                String realName = a.Name.Replace("tb", "").ToLower();
                String[] members = new String[1];

                if (realName.Contains("skill0") || realName.Contains("skill1") || realName.Contains("skill2") || realName.Contains("skill3"))
                {
                    int anIndex = npcDesc.nameList.FindIndex(p => p.ToLower().Equals("a_" + "skill" + realName.Replace("rate", "").Replace("id", "").Replace("skill", "").ToLower()));

                    if (anIndex != -1)
                        members = Convert.ToString(npcDesc[anIndex]).Split(' ');

                    if (realName.Contains("id") && members.Count() > 0)
                    {
                        String v = Convert.ToString(members[0]);
                        a.Text = (v == String.Empty || v == "0") ? "-1" : v;
                    }
                    else
                    {
                        if (realName.Contains("id"))
                            a.Text = "-1";
                    }

                    if (realName.Contains("level") && members.Count() > 1)
                    {
                        String v = Convert.ToString(members[1]);
                        a.Text = v == "-1" ? "0" : v;
                    }
                    else
                    {
                        if (realName.Contains("level"))
                            a.Text = "0";
                    }

                    if (realName.Contains("rate") && members.Count() >= 3)
                    {
                        a.Value = Convert.ToInt32(members[2]);
                    }
                    else
                    {
                        a.Value = 0;
                    }
                }
            }

            dropListGrid.Rows.Clear();

            for (int i = 0; i < 20; i++)
            {
                dropListGrid.Rows.Add(new DataGridViewRow());

                String itemId = String.Format("a_item_{0}", i);
                String itemPerc = String.Format("a_item_percent_{0}", i);

                dropListGrid.Rows[i].Cells[1].Value = npcDesc[itemId];

                int itemIdx = items.FindIndex(p => p.a_index == (int)npcDesc[itemId]);

                if (itemIdx != -1)
                {
                    dropListGrid.Rows[i].Cells[0].Value = IconCache.GetItemIcon(items[itemIdx].a_texture_id,
                        items[itemIdx].a_texture_row, items[itemIdx].a_texture_col);

                    dropDesc.SetData(items[itemIdx]);

                    dropListGrid.Rows[i].Cells[2].Value = dropDesc[LocalNameString];
                    dropListGrid.Rows[i].Selected = false;
                    dropListGrid.Rows[i].Cells[3].Value = npcDesc[itemPerc];
                }
                else
                {
                    dropListGrid.Rows[i].Cells[0].Value = m_errorIcon;

                    dropListGrid.Rows[i].Cells[2].Value = String.Empty;
                    dropListGrid.Rows[i].Selected = false;
                    dropListGrid.Rows[i].Cells[3].Value = 0;
                }

                dropListGrid.Rows[i].Height = 32;
            }

            // Drop All Items Clear List and Grid
            dropAllGrid.Rows.Clear();
            dropAllItems.Clear();

            Deserialize<NpcDropAll> stDropAll = new Deserialize<NpcDropAll>("t_npc_drop_all");

            stDropAll.SetConditions($"where a_npc_idx = {SelectedId}");

            dropAllItems = new Transactions<NpcDropAll>(DataCon).ExecuteQuery(stDropAll);

            foreach(NpcDropAll aDrop in dropAllItems)
            {
                int nRow = dropAllGrid.Rows.Add();

                DropItem ati = items.Find(p => p.a_index.Equals(aDrop.a_item_idx));

                dropDesc.SetData(ati);

                dropAllGrid.Rows[nRow].Cells[0].Value = IconCache.GetItemIcon(ati.a_texture_id, ati.a_texture_row, ati.a_texture_col);
                dropAllGrid.Rows[nRow].Cells[1].Value = ati.a_index;
                dropAllGrid.Rows[nRow].Cells[2].Value = dropDesc[LocalNameString];
                dropAllGrid.Rows[nRow].Cells[3].Value = aDrop.a_prob;
            }

            // Zone Flag
            for (int i = 0; i < 64; i++)
            {
                clbZone.SetItemChecked(i, BitHelpers.GetBit(i, d.a_zone_flag));
            }

            // Zone flag 2
            for (int i = 0; i < 64; i++)
            {
                clbExtra.SetItemChecked(i, BitHelpers.GetBit(i, d.a_extra_flag));
            }

            int ShopIndex = shops.FindIndex(p => p.a_keeper_idx == d.a_index);

            // If this npc has shop data include this data
            if (ShopIndex != -1)
            {
                dgShop.Rows.Clear();

                ShopInfo currentShop = shops[ShopIndex];

                for (int i = 0; i < mainTabControl.TabPages[4].Controls.Count; i++)
                {
                    mainTabControl.TabPages[4].Controls[i].Enabled = true;
                }

                List<ShopItem> aShop = itemsdata.FindAll(p => p.a_keeper_idx.Equals(shops[ShopIndex].a_keeper_idx));

                for (int i = 0; i < aShop.Count; i++)
                {
                    dgShop.Rows.Add();

                    DropItem aShopItem = items.Find(p => p.a_index.Equals(aShop[i].a_item_idx));

                    dropDesc.SetData(aShopItem);

                    dgShop.Rows[i].Cells[0].Value = IconCache.GetItemIcon(aShopItem.a_texture_id, aShopItem.a_texture_row, aShopItem.a_texture_col);
                    dgShop.Rows[i].Cells[1].Value = aShop[i].a_item_idx;
                    dgShop.Rows[i].Cells[2].Value = dropDesc[LocalNameString];
                    dgShop.Rows[i].Cells[3].Value = aShopItem.a_price;
                }

                merch_h.Text = currentShop.a_pos_h.ToString();
                merch_r.Text = currentShop.a_pos_r.ToString();
                merch_x.Text = currentShop.a_pos_x.ToString();
                merch_z.Text = currentShop.a_pos_z.ToString();
                merch_zone.Text = currentShop.a_zone_num.ToString();

                makeShopNpcBtn.Enabled = false;
            }
            else
            {
                for (int i = 0; i < mainTabControl.TabPages[4].Controls.Count; i++)
                {
                    mainTabControl.TabPages[4].Controls[i].Enabled = false;
                }

                dgShop.Rows.Clear();

                makeShopNpcBtn.Enabled = true;
            }

            WindowFilling = false;

            String fullPath = Preferences.GetData()["CLIENT"]["root"] + d.a_file_smc;

             //if (File.Exists(fullPath))
                //renderer.LoadMesh(fullPath);
        }

        private void SpinnerState(bool show)
        {
            if (ProgSpin1.InvokeRequired)
            {
                ProgSpin1.Invoke(new LoadStateChanger(SpinnerState), new Object[] {
                    show});
            }
            else
            {
                ProgSpin1.Visible = show;
            }
        }
        
        private void DoQueryAll(object arg)
        {
            SpinnerState(true);

            TaskBlock(delegate {
                // Create a structure and set the table
                Deserialize<NpcData> npcDoc = new Deserialize<NpcData>("t_npc");

                // Set a key this is important for editing reasons
                npcDoc.SetKey("a_index");

                // Update all npcs in the db
                new Transactions<NpcData>(DataCon).ExecuteQuery(npcDoc, data, (QUERY_TYPE)arg);
            });

            SpinnerState(false);
        }

        private void DoQuerySingle(object arg)
        {
            if (SelectedId == -1 || data.Count <= 0)
                return;

            SpinnerState(true);

            TaskBlock(delegate
            {
                // Create a structure and set the table
                Deserialize<NpcData> npcDoc = new Deserialize<NpcData>("t_npc");

                // Set a key this is important for editing reasons
                npcDoc.SetKey("a_index");

                int sel = data.FindIndex(p => p.a_index.Equals(SelectedId));

                npcDoc.SetData(data[sel]);

                // Update all npcs in the db
                new Transactions<NpcData>(DataCon).ExecuteQuery(npcDoc, (QUERY_TYPE)arg);

                if ((QUERY_TYPE)arg == QUERY_TYPE.DELETE)
                    data.RemoveAt(data.FindIndex(p => p.a_index == SelectedId));

            });

            SpinnerState(false);
        }

        private void OnQueryUpdateAll(object sender, EventArgs e)
        {
            if (!CheckNpcChanged())
                return;

            AddTask(DoQueryAll, QUERY_TYPE.UPDATE);
        }

        private void OnQueryUpdateSingle(object sender, EventArgs e)
        {
            if (!CheckNpcChanged())
                return;

            AddTask(DoQuerySingle, QUERY_TYPE.UPDATE);
        }

        private void OnQueryInsertAll(object sender, EventArgs e)
        {
            AddTask(DoQueryAll, QUERY_TYPE.INSERT);
        }

        private void OnQueryInsertSingle(object sender, EventArgs e)
        {
            AddTask(DoQuerySingle, QUERY_TYPE.INSERT);
        }

        private void DoDeleteSingle(object sender, EventArgs e)
        {
            AddTask(DoQuerySingle, QUERY_TYPE.DELETE);
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            if (LastSaveFile == String.Empty)
            {
                SaveAsMenuItem_Click(sender, e);
            }
            else
            {
                ExportData();
                MsgDialogs.Show("Export Complete", "Finished Exporting Npc Lod", "ok", MsgDialogs.MsgTypes.INFO);
            }
        }

        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            AddTask(ExportLodSwitch, 0);
        }

        private void ExportNpcNames(object sender, EventArgs e)
        {
            AddTask(ExportLodSwitch, 1);
        }

        private char[] emptySpace = { ' ' };

        private int ParseToken(ref string val, int def)
        {
            int res;
            string parsed = string.Empty;

            if (!string.IsNullOrWhiteSpace(val))
            {
                int charLocation = val.IndexOf(" ", StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    parsed = val.Substring(0, charLocation);
                    val = val.Remove(0, charLocation + 1);
                }
            }

            if (int.TryParse(parsed, out res))
                return res;

            return def;
        }

        private byte ParseToken(ref string val, byte def)
        {
            byte res;
            string parsed = string.Empty;

            if (!string.IsNullOrWhiteSpace(val))
            {
                int charLocation = val.IndexOf(" ", StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    parsed = val.Substring(0, charLocation);
                    val = val.Remove(0, charLocation + 1);
                }
            }

            if (byte.TryParse(parsed, out res))
                return res;

            return def;
        }

        private void ExportData()
        {
            BinaryWriter bw = new BinaryWriter(File.Open(LastSaveFile, FileMode.Create));

            bw.Write(data.Where(p => p.a_enable != 0).ToList().Count);

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].a_enable == 0)
                    continue;

                try
                {
                    // Int values
                    bw.Write(data[i].a_index);
                    bw.Write(data[i].a_level);
                    bw.Write(data[i].a_hp);
                    bw.Write(data[i].a_mp);
                    bw.Write(data[i].a_flag);
                    bw.Write(data[i].a_flag1);
                    bw.Write(data[i].a_attackSpeed);

                    // Float values
                    bw.Write(data[i].a_walk_speed);
                    bw.Write(data[i].a_run_speed);
                    bw.Write(data[i].a_scale);
                    bw.Write(data[i].a_attack_area);
                    bw.Write(data[i].a_size);

                    // Char values
                    bw.Write(data[i].a_skillmaster);
                    bw.Write(data[i].a_sskill_master);

                    // Effect[5] Even the offical exporter loads nothing
                    for (int k = 0; k < 5; k++)
                    {
                        bw.Write(0);
                    }
                    // end effects

                    bw.Write(data[i].a_attackType);
                    bw.Write(data[i].a_fireDelayCount);

                    bw.Write(data[i].a_fireDelay0);
                    bw.Write(data[i].a_fireDelay1);
                    bw.Write(data[i].a_fireDelay2);
                    bw.Write(data[i].a_fireDelay3);

                    bw.Write(data[i].a_fireObject);
                    bw.Write(data[i].a_fireSpeed);

                    string skill0 = data[i].a_skill0.TrimStart(emptySpace);
                    string skill1 = data[i].a_skill1.TrimStart(emptySpace);

                    int skillIndex0 = -1, skillIndex1 = -1;
                    byte level0 = 0, level1 = 0;

                    skillIndex0 = ParseToken(ref skill0, -1);
                    level0 = ParseToken(ref skill0, (byte)0);

                    skillIndex1 = ParseToken(ref skill1, -1);
                    level1 = ParseToken(ref skill1, (byte)0);

                    // Skill0 Index & Level
                    bw.Write(skillIndex0);
                    bw.Write(level0);

                    // Skill1 Index & Level
                    bw.Write(skillIndex1);
                    bw.Write(level1);

                    bw.Write(data[i].a_rvr_grade);
                    bw.Write(data[i].a_rvr_value);

                    bw.Write(data[i].a_bound);

                    byte[] smcBuffer = new byte[128];
                    byte[] tmpBuffer = new byte[64];

                    //bw.Write(Encoding.ASCII.GetByteCount(data[i].a_file_smc));
                    Enc.GetBytes(data[i].a_file_smc.Replace("/", "\\")).CopyTo(smcBuffer, 0);
                    bw.Write(smcBuffer);
                    ResetBuffer(smcBuffer);

                    //bw.Write(Encoding.ASCII.GetByteCount(data[i].a_motion_idle));
                    Enc.GetBytes(data[i].a_motion_idle).CopyTo(tmpBuffer, 0);
                    bw.Write(tmpBuffer);
                    ResetBuffer(tmpBuffer);

                    //bw.Write(Encoding.ASCII.GetByteCount(data[i].a_motion_walk));
                    Enc.GetBytes(data[i].a_motion_walk).CopyTo(tmpBuffer, 0);
                    bw.Write(tmpBuffer);
                    ResetBuffer(tmpBuffer);

                    //bw.Write(Encoding.ASCII.GetByteCount(data[i].a_motion_dam));
                    Enc.GetBytes(data[i].a_motion_dam).CopyTo(tmpBuffer, 0);
                    bw.Write(tmpBuffer);
                    ResetBuffer(tmpBuffer);

                    //bw.Write(Encoding.ASCII.GetByteCount(data[i].a_motion_attack));
                    Enc.GetBytes(data[i].a_motion_attack).CopyTo(tmpBuffer, 0);
                    bw.Write(tmpBuffer);
                    ResetBuffer(tmpBuffer);

                    //bw.Write(Encoding.ASCII.GetByteCount(data[i].a_motion_die));
                    Enc.GetBytes(data[i].a_motion_die).CopyTo(tmpBuffer, 0);
                    bw.Write(tmpBuffer);
                    ResetBuffer(tmpBuffer);

                    //bw.Write(Encoding.ASCII.GetByteCount(data[i].a_motion_run));
                    Enc.GetBytes(data[i].a_motion_run).CopyTo(tmpBuffer, 0);
                    bw.Write(tmpBuffer);
                    ResetBuffer(tmpBuffer);

                    //bw.Write(Encoding.ASCII.GetByteCount(data[i].a_motion_idle2));
                    Enc.GetBytes(data[i].a_motion_idle2).CopyTo(tmpBuffer, 0);
                    bw.Write(tmpBuffer);
                    ResetBuffer(tmpBuffer);

                    //bw.Write(Encoding.ASCII.GetByteCount(data[i].a_motion_attack2));
                    Enc.GetBytes(data[i].a_motion_attack2).CopyTo(tmpBuffer, 0);
                    bw.Write(tmpBuffer);
                    ResetBuffer(tmpBuffer);

                    //bw.Write(Encoding.ASCII.GetByteCount(data[i].a_fireEffect0));
                    Enc.GetBytes(data[i].a_fireEffect0).CopyTo(tmpBuffer, 0);
                    bw.Write(tmpBuffer);
                    ResetBuffer(tmpBuffer);

                    //bw.Write(Encoding.ASCII.GetByteCount(data[i].a_fireEffect1));
                    Enc.GetBytes(data[i].a_fireEffect1).CopyTo(tmpBuffer, 0);
                    bw.Write(tmpBuffer);
                    ResetBuffer(tmpBuffer);

                    //bw.Write(Encoding.ASCII.GetByteCount(data[i].a_fireEffect2));
                    Enc.GetBytes(data[i].a_fireEffect2).CopyTo(tmpBuffer, 0);
                    bw.Write(tmpBuffer);
                    ResetBuffer(tmpBuffer);
                }
                catch (Exception e)
                {
                    MsgDialogs.Show("Error", e.Message, "ok", MsgDialogs.MsgTypes.ERROR);
                }
            }

            bw.Flush();
            bw.Close();
            bw.Dispose();
        }

        private void ResetBuffer(byte[] tmpBuffer)
        {
            for (int i = 0; i < tmpBuffer.Count(); i++)
            {
                tmpBuffer[i] = 0;
            }
        }

        private void ExportLodSwitch(object arg)
        {
            if (data.Count <= 0)
                return;

            SpinnerState(true);

            SaveFileDialog sfd = new SaveFileDialog();

            if ((int)arg == 0)
            {
                sfd.Title = "Save MobAll.lod (*.Lod)";
                sfd.Filter = "MobAll File (*.lod)|*.lod";
                sfd.Tag = "Data";

                Invoke((MethodInvoker)delegate
                {
                   if (sfd.ShowDialog() != DialogResult.OK)
                       return;
                });

                LastSaveFile = sfd.FileName;
            }

            if ((int)arg == 1)
            {
                sfd.Title = "Save Npc Name Lod";
                sfd.Filter = "Npc Name Lod (*.lod)|*.lod";
                sfd.Tag = "Names";

                Invoke((MethodInvoker)delegate
                {
                    if (sfd.ShowDialog() != DialogResult.OK)
                        return;
                });
            }

            if (((String)sfd.Tag) == "Data")
            {
                ExportData();
            }

            if (((String)sfd.Tag) == "Names")
            {
                ExportNames(sfd);
            }

            SpinnerState(false);

            if ((int)arg == 0)
            {
                MsgDialogs.Show("Export Complete", "Finished Exporting Npc Lod", "ok", MsgDialogs.MsgTypes.INFO);
            }
            else
            { 
                MsgDialogs.Show("Export Complete", "Finished Exporting Npc Names", "ok", MsgDialogs.MsgTypes.INFO);
            }
        }

        private void ExportNames(SaveFileDialog sender)
        {
            SaveFileDialog sfd = sender;
            
            BinaryWriter bw = new BinaryWriter(File.Open(sfd.FileName, FileMode.Create));

            // nRow
            bw.Write(data.Count);
            // nMax
            bw.Write(data.Last().a_index);

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].a_enable == 0)
                    continue;

                npcDesc.SetData(data[i]);

                bw.Write(data[i].a_index);

                int len = Core.Encoder.GetByteCount(npcDesc[LocalNameString].ToString());
                bw.Write(len);

                if (len > 0)
                    bw.Write(Core.Encoder.GetBytes(npcDesc[LocalNameString].ToString()));

                int len2 = Core.Encoder.GetByteCount(npcDesc[LocalDescString].ToString());
                bw.Write(len2);

                if (len > 0)
                    bw.Write(Core.Encoder.GetBytes(npcDesc[LocalDescString].ToString()));
            }

            //bw.Write(-9999);

            //CFileSecure sec = new CFileSecure();

            //sec.EncodeFile(bw);

            bw.Flush();
            bw.Close();
            bw.Dispose();
        }

        private bool PauseList = false;


        /// <summary>
        /// Check for data changes prompt user to save discard or cancel.
        /// </summary>
        /// <returns>False if operation was canceled.</returns>
        private bool CheckNpcChanged()
        {
            if (SelectedId != -1)
            {
                int checkNpc = data.FindIndex(p => p.a_index.Equals(SelectedId));

                if (checkNpc != -1)
                {
                    // Check if the data matches previous data or the form has changes
                    if (npcBack != data[checkNpc] || FormHasChanges())
                    {
                        switch(MsgDialogs.ShowNoLog("Changes Detected!",
                            $"Do you wish to save changes to npc: {npcBack[LocalNameString]}?",
                            "yesnocancel", MsgDialogs.MsgTypes.WARNING))
                        {
                            case DialogResult.Yes:
                                {
                                    PauseList = true;
                                    OnSaveButton(null, null);
                                    PauseList = false;
                                }
                                break;
                            case DialogResult.No:
                                {
                                    data[checkNpc] = npcBack.Serialize();
                                }
                                break;
                            case DialogResult.Cancel:
                                return false;
                            default: // No
                                break;
                        }

                        DoFillWindow();
                    }
                }
            }

            return true;
        }
        
        private void OnSelectedNpcChange(object sender, EventArgs e)
        {
            if (PauseList)
                return;

            if (!CheckNpcChanged())
            {
                NpcList.SelectedIndexChanged -= OnSelectedNpcChange;
                NpcList.SelectedIndex = NpcList.FindString($"{SelectedId} - {npcBack[LocalNameString]}");
                ActiveControl = tbName_USA;
                NpcList.SelectedIndexChanged += OnSelectedNpcChange;
                return;
            }

            // If we have deselcted the list
            if (NpcList.SelectedIndex == -1)
                return;

            bool parsed = false;
            int newId = -1;
            parsed = int.TryParse(NpcList.Items[NpcList.SelectedIndex].ToString().Split('-')[0], out newId);

            SelectedId = newId;

            NpcData backData = data.Find(p => p.a_index.Equals(SelectedId));
            npcBack.SetData(backData);

            BeginChangeContent();
            DoFillWindow();
            EndChangeContent();
        }

        private void OnSaveButton(object sender, EventArgs e)
        {
            if (!DataCon.IsConnected())
                return;

            CommitChanges();
            DoUpdateData();
        }

        private bool DoUpdateData()
        {
            if (data.Count <= 0)
                return false;

            bool rebuildList = false;

            Deserialize<NpcData> dstruct = new Deserialize<NpcData>("t_npc");
            NpcData d = data.Find(p => p.a_index == SelectedId);

            d.a_enable = cb_enabled.Checked == true ? 1 : 0;

            UInt64 ZoneFlag = 0;

            // Zone Flag
            DoSetBits(clbZone, out ZoneFlag);

            d.a_zone_flag = ZoneFlag;

            UInt64 ZoneFlagExtra = 0;
            // Zone flag 2
            DoSetBits(clbExtra, out ZoneFlagExtra);

            d.a_extra_flag = ZoneFlagExtra;

            dstruct.SetData(d);

            if (d.a_index != Convert.ToInt32(tbIndex.Text))
            {
                dstruct.SetWhere(d.a_index);
            }

            int ObjectIndex = data.FindIndex(p => p.a_index == SelectedId);

            if (dstruct[LocalNameString].ToString() != tbName_USA.Text)
                rebuildList = true;

            List<Control> textBoxes = ControlUtil.GetAll(this.mainTabControl, typeof(TextBox)).ToList();

            foreach (TextBox a in textBoxes)
            {
                String realName = a.Name.Replace("tb", "").Replace("Motion", "motion_").ToLower();
                String[] members = new String[3];

                int anIndex = 0;

                if (realName.Contains("skill0") || realName.Contains("skill1") || realName.Contains("skill2") || realName.Contains("skill3"))
                {
                    anIndex = dstruct.nameList.FindIndex(p => p.ToLower().Equals("a_" + "skill" + realName.Replace("level", "").Replace("id", "").Replace("skill", "")));

                    if (anIndex != -1)
                        members = Convert.ToString(dstruct[anIndex]).Split(' ');

                    if (realName.Contains("id") && members.Count() > 0)
                    {
                        members[0] = a.Text;
                        dstruct[anIndex] = String.Join(" ", members);
                    }

                    if (realName.Contains("level") && members.Count() > 1)
                    {
                        members[1] = a.Text;
                        dstruct[anIndex] = String.Join(" ", members);
                    }

                    continue;
                }

                anIndex = dstruct.nameList.FindIndex(p => p.ToLower().Equals("a_" + realName.ToLower()));

                if (anIndex != -1)
                    dstruct[anIndex] = Convert.ChangeType(a.Text, dstruct.typeList[anIndex]);
            }

            textBoxes = ControlUtil.GetAll(this.mainTabControl, typeof(ComboBox)).ToList();

            foreach (ComboBox a in textBoxes)
            {
                String realName = a.Name.Replace("cb", "");

                int anIndex = dstruct.nameList.FindIndex(p => p.ToLower().Equals("a_" + realName.ToLower()));
                if (anIndex != -1)
                    dstruct[anIndex] = (SByte)a.SelectedIndex;
            }

            textBoxes = ControlUtil.GetAll(this.mainTabControl, typeof(NumericUpDown)).ToList();

            foreach (NumericUpDown a in textBoxes)
            {
                String realName = a.Name.Replace("tb", "").ToLower();
                String[] members = new String[1];

                if (realName.Contains("skill0") || realName.Contains("skill1") || realName.Contains("skill2") || realName.Contains("skill3"))
                {
                    int anIndex = dstruct.nameList.FindIndex(p => p.ToLower().Equals("a_" + "skill" + realName.Replace("rate", "").Replace("id", "").Replace("skill", "").ToLower()));

                    if (anIndex != -1)
                        members = Convert.ToString(dstruct[anIndex]).Split(' ');

                    if (realName.Contains("rate") && members.Count() >= 3)
                    {
                        members[2] = Convert.ToString(a.Value);
                        dstruct[anIndex] = String.Join(" ", members);
                    }
                }
            }

            foreach (DataGridViewRow a in dropListGrid.Rows)
            {
                String item = String.Format("a_item_{0}", a.Index);
                String precent = String.Format("a_item_percent_{0}", a.Index);

                dstruct[item] = a.Cells[1].Value;
                dstruct[precent] = a.Cells[3].Value;
            }

            // Finalize the class and return
            data[ObjectIndex] = dstruct.Serialize();

            if (SelectedId != data[ObjectIndex].a_index)
                rebuildList = true;

            SelectedId = data[ObjectIndex].a_index;

            if (dstruct.WhereValue != String.Empty)
            {
                dstruct.SetKey("a_index");
                new Transactions<NpcData>(DataCon).ExecuteQuery(dstruct, QUERY_TYPE.UPDATE);
            }

            npcBack.SetData(data[ObjectIndex]);

            if(rebuildList)
                DoRebuildList();

            return true;
        }

        static long doubleInt2long(int a1, int a2)
        {
            long b = a2;
            b = b << 32;
            b = b | (uint)a1;
            return b;
        }

#pragma warning disable 414, 3021
        private void DoSetBits(CheckedListBox clb, out UInt64 ZoneFlags)
        {
            BitArray ba = new BitArray(sizeof(ulong) * 8);

            for (int i = 0; i < clb.Items.Count; i++)
            {
                ba.Set(i, clb.GetItemChecked(i));
            }

            byte[] deps = new byte[sizeof(ulong)];

            ba.CopyTo(deps, 0);

            ZoneFlags = BitConverter.ToUInt64(deps, 0);
        }

        private void DoSetBits(CheckedListBox clb, ItemCheckEventArgs item, out UInt64 ZoneFlags)
        {
            BitArray ba = new BitArray(sizeof(ulong) * 8);

            for (int i = 0; i < clb.Items.Count; i++)
            {
                ba.Set(i, clb.GetItemChecked(i));
            }

            ba.Set(item.Index, item.NewValue == CheckState.Checked ? true : false);

            byte[] deps = new byte[sizeof(ulong)];

            ba.CopyTo(deps, 0);

            ZoneFlags = BitConverter.ToUInt64(deps, 0);
        }
#pragma warning restore 3021

        private void OnRevertButton(object sender, EventArgs e)
        {
            if (SelectedId != -1)
            {
                // Locate the npc by the selected index
                int listIdx = data.FindIndex(p => p.a_index.Equals(SelectedId));

                // Cannot find npc return
                if (listIdx == -1)
                    return;

                // Undo changes and update window
                data[listIdx] = npcBack.Serialize();

                DoFillWindow();
            }
        }

        private void OnNewNpcButton(object sender, EventArgs e)
        {
            if (!CheckNpcChanged())
                return;

            NpcData newNpc = new NpcData();

            data.Sort((x, y) => x.a_index.CompareTo(y.a_index));

            npcDesc.SetData(newNpc);

            npcDesc.SetValue("a_index", data.Last().a_index + 1);
            npcDesc.SetValue(LocalNameString, "New Npc!");
            npcDesc.SetValue(LocalNameString, "New Npc!");

            newNpc = npcDesc.Serialize();

            data.Add(newNpc);

            AddTask(DoQuerySingle, QUERY_TYPE.INSERT);

            DoRebuildList();

            NpcList.SelectedIndex = DoSelectItem(newNpc.a_index);
        }

        private int DoSelectItem(int p)
        {
            for (int i = 0; i < NpcList.Items.Count; i++)
            {
                if (NpcList.Items[i].ToString().ToLower().Split('-')[0].Contains(Convert.ToString(p)))
                {
                    return i;
                }
            }

            return -1;
        }

        private void DoSelectSmc(object sender, EventArgs e)
        {
            IniParser.Model.IniData data = IllTechLibrary.Settings.Preferences.GetData();

            String RootDir = data["CLIENT"]["root"];

            if (!Directory.Exists(RootDir))
            {
                MsgDialogs.Show("Invalid Directory", "Invalid client directory in Config.ini!", "ok", MsgDialogs.MsgTypes.ERROR);
                return;
            }

            String theFile = RootDir + this.tbFile_Smc.Text;

            String Selected = "";

            if (File.Exists(theFile))
            {
                Selected = new FileSelectDialog("Select Smc", "Simple Mesh Container (*.smc)|*.smc", Path.GetDirectoryName(theFile)).GetFile();
            }
            else
            {
                Selected = new FileSelectDialog("Select Smc", "Simple Mesh Container (*.smc)|*.smc", RootDir).GetFile();
            }

            if (!File.Exists(Selected))
                return;

            Selected = Selected.Replace(RootDir, "");

            if (!Selected.Contains("Data"))
            {
                MsgDialogs.Show("Invalid Path", "Smc is not located in data folder!", "ok", MsgDialogs.MsgTypes.ERROR);
            }

            tbFile_Smc.Text = Selected;

            //renderer.LoadMesh(RootDir + Selected);
        }

        private void DoSelectAni(object sender, EventArgs e)
        {
            IniParser.Model.IniData data = IllTechLibrary.Settings.Preferences.GetData();

            String RootDir = data["CLIENT"]["root"];

            if (!Directory.Exists(RootDir))
            {
                MsgDialogs.Show("Invalid Directory", "Invalid client directory in Config.ini!", "ok", MsgDialogs.MsgTypes.ERROR);
                return;
            }

            String Selected = new FileSelectDialog("Select Animation", "Binary Animation (*.ba)|*.ba", RootDir).GetFile();

            if (!File.Exists(Selected))
                return;

            if (!Selected.Contains("Data"))
            {
                MsgDialogs.Show("Invalid Path", "Animation is not located in data folder!", "ok", MsgDialogs.MsgTypes.ERROR);
                return;
            }


            AnimationPicker ap = new AnimationPicker(Selected);
            ap.ShowDialog();
            String animName = ap.GetName();

            if (animName == String.Empty)
                return;

            MetroButton clicked = (MetroButton)sender;

            String SearchStr = clicked.Name.Replace("btnAni", "");

            TextBox tb = (TextBox)this.Controls.Find(String.Format("tbMotion{0}", SearchStr), true)[0];

            tb.Text = animName;
        }

        private void OnEnable3DChanged(object sender, EventArgs e)
        {
            if (IsLoading == true)
                return;

            Preferences.SetPrefs("View3DEnabled", chk3D.Checked.ToString());

            if (chk3D.Enabled)
            {
            }
            else
            {
            }
        }

        private void OnShowFlagBuilder1(object sender, EventArgs e)
        {
            if (tbFlag.Text == String.Empty)
                return;

            NpcFlagBuilder fb = new NpcFlagBuilder(Convert.ToInt32(tbFlag.Text));
            fb.ShowDialog();

            if (fb.DialogResult == DialogResult.OK)
            {
                tbFlag.Text = fb.GetFlag();
            }
        }

        private void OnShowFlagBuilder2(object sender, EventArgs e)
        {
            if (tbFlag1.Text == String.Empty)
                return;

            NpcFlagBuilder2 fb = new NpcFlagBuilder2(Convert.ToInt32(tbFlag1.Text));
            fb.ShowDialog();

            if (fb.DialogResult == DialogResult.OK)
            {
                tbFlag1.Text = fb.GetFlag();
            }
        }

        private void OnZoomChange(object sender, ScrollEventArgs e)
        {
            //renderer.SetZoom(e.NewValue);
        }

        private void OnUpDownChange(object sender, ScrollEventArgs e)
        {
            //renderer.SetScroll(e.NewValue);
        }

        private void OnLeftRightChange(object sender, ScrollEventArgs e)
        {
            //renderer.SetPan(e.NewValue);
        }

        private void OnCopyNew(object sender, EventArgs e)
        {
            if (!CheckNpcChanged())
                return;

            if (SelectedId == -1)
                return;

            int itemToCopy = data.FindIndex(p => p.a_index == SelectedId);

            Deserialize<NpcData> dStruct = new Deserialize<NpcData>("t_npc");
            dStruct.SetData(data[itemToCopy]);

            dStruct["a_index"] = data.Last().a_index + 1;

            data.Add(dStruct.Serialize());

            npcBack.SetData(data.Last());

            SelectedId = (int)dStruct["a_index"];

            AddTask(DoQuerySingle, QUERY_TYPE.INSERT, DoRebuildList);

            BeginChangeContent();
            DoFillWindow();
            EndChangeContent();
        }

        private void OnMakeShopNpc(object sender, EventArgs e)
        {
            ShopInfo newShop = new ShopInfo();

            newShop.a_keeper_idx = SelectedId;
            newShop.a_name = String.Format("New Shop {0}", SelectedId);
            newShop.a_sell_rate = 40;
            newShop.a_buy_rate = 100;
            newShop.a_zone_num = 0;

            shops.Add(newShop);

            Deserialize<ShopInfo> tShopInfo = new Deserialize<ShopInfo>("t_shop");
            tShopInfo.SetKey("a_keeper_idx");
            tShopInfo.SetData(newShop);

            new Transactions<ShopInfo>(DataCon).ExecuteQuery(tShopInfo, QUERY_TYPE.INSERT);

            BeginChangeContent();
            DoFillWindow();
            EndChangeContent();
        }

        private void OnDeleteShop(object sender, EventArgs e)
        {
            if (dgShop.SelectedRows.Count <= 0)
                return;

            int dgIndex = dgShop.SelectedRows[0].Index;
            int itemId = (int)dgShop.Rows[dgIndex].Cells[1].Value;
            int keeper = SelectedId;

            dgShop.Rows.RemoveAt(dgIndex);

            ShopItem anItem = itemsdata.Find(p => p.a_item_idx.Equals(itemId) && p.a_keeper_idx.Equals(keeper));

            Deserialize<ShopItem> itemInfo = new Deserialize<ShopItem>("t_shopitem");
            itemInfo.SetKey("a_keeper_idx");
            itemInfo.SetWhere(String.Format("{0} AND a_item_idx={1}", anItem.a_keeper_idx, anItem.a_item_idx));
            itemInfo.SetData(anItem);

            itemsdata.Remove(anItem);

            new Transactions<ShopItem>(DataCon).ExecuteQuery(itemInfo, QUERY_TYPE.DELETE);
        }

        private void OnShopUp(object sender, EventArgs e)
        {
            OnMoveUp();
        }

        private void OnShopAdd(object sender, EventArgs e)
        {
            ItemSelector sel = ItemSelector.Instance();//new ItemSelector(-1, 0);

            // This instance has been updated
            if (sel.Show(this, -1, 0) == DialogResult.OK)
            {
                // Check if this show already has the item
                int exists = itemsdata.FindIndex(p => p.a_keeper_idx.Equals(SelectedId) && p.a_item_idx.Equals(sel.SelectedIndex));

                if (exists != -1)
                    return;

                ShopItem anItem = new ShopItem();

                anItem.a_keeper_idx = SelectedId;
                anItem.a_item_idx = sel.SelectedIndex;
                anItem.a_national = 0;

                itemsdata.Add(anItem);

                BeginChangeContent();
                DoFillWindow();
                EndChangeContent();

                Deserialize<ShopItem> itemInfo = new Deserialize<ShopItem>("t_shopitem");
                itemInfo.SetKey("a_keeper_idx");
                itemInfo.SetData(anItem);

                new Transactions<ShopItem>(DataCon).ExecuteQuery(itemInfo, QUERY_TYPE.INSERT);
            }
        }

        public bool WindowFilling = false;

        private void OnExportShops(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Save ShopAll.lod";
            sfd.Filter = "Shop lod (*.lod)|*.lod";
            sfd.FileOk += DoShopDataExport;

            sfd.ShowDialog();
        }

        private void DoShopDataExport(object sender, CancelEventArgs e)
        {
            SaveFileDialog sfd = (SaveFileDialog)sender;

            AddTask(delegate()
            {
                SpinnerState(true);

                BinaryWriter bw = new BinaryWriter(File.Open(sfd.FileName, FileMode.Create));

                bw.Write(shops.Last().a_keeper_idx);

                var result = ETaskBlockResult.Complete;

                for (int i = 0; i < shops.Count; i++)
                {
                    result = TaskBlock(delegate
                    {
                        bw.Write(shops[i].a_keeper_idx);

                        // Write name and length
                        bw.Write(Enc.GetByteCount(shops[i].a_name));
                        bw.Write(Enc.GetBytes(shops[i].a_name));

                        bw.Write(shops[i].a_sell_rate);
                        bw.Write(shops[i].a_buy_rate);

                        List<ShopItem> anShopsItems = itemsdata.FindAll(p => p.a_keeper_idx.Equals(shops[i].a_keeper_idx) && p.a_national <= 24684);
                        bw.Write(anShopsItems.Count);

                        for (int j = 0; j < anShopsItems.Count; j++)
                        {
                            bw.Write(anShopsItems[j].a_item_idx);
                        }
                    });

                    if (result == ETaskBlockResult.Canceled)
                        break;
                }

                bw.Flush();
                bw.Close();
                bw.Dispose();

                SpinnerState(false);

                if (result == ETaskBlockResult.Canceled)
                {
                    MsgDialogs.ShowNoLog("Export Canceled", "Export Operation Was Canceled.", "ok", MsgDialogs.MsgTypes.WARNING);

                    if (File.Exists(sfd.FileName))
                        File.Delete(sfd.FileName);
                }
                else
                {
                    MsgDialogs.Show("Export Complete", "Finished Exporting Shops Lod.", "ok", MsgDialogs.MsgTypes.INFO);
                }
            });
        }

        private void OnMoveUp()
        {
            if (dgShop.RowCount > 0)
            {
                if (dgShop.SelectedRows.Count > 0)
                {
                    int rowCount = dgShop.Rows.Count;
                    int index = dgShop.SelectedCells[0].OwningRow.Index;

                    if (index == 0)
                    {
                        return;
                    }

                    DataGridViewRowCollection rows = dgShop.Rows;

                    // remove the previous row and add it behind the selected row.
                    DataGridViewRow prevRow = rows[index - 1];
                    rows.Remove(prevRow);
                    prevRow.Frozen = false;
                    rows.Insert(index, prevRow);
                    dgShop.ClearSelection();
                    dgShop.Rows[index - 1].Selected = true;
                }
            }
        }

        private void OnMoveDown()
        {
            if (dgShop.RowCount > 0)
            {
                if (dgShop.SelectedRows.Count > 0)
                {
                    int rowCount = dgShop.Rows.Count;
                    int index = dgShop.SelectedCells[0].OwningRow.Index;

                    if (index == (rowCount - 2)) // include the header row
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = dgShop.Rows;

                    // remove the next row and add it in front of the selected row.
                    DataGridViewRow nextRow = rows[index + 1];
                    rows.Remove(nextRow);
                    nextRow.Frozen = false;
                    rows.Insert(index, nextRow);
                    dgShop.ClearSelection();
                    dgShop.Rows[index + 1].Selected = true;
                }
            }
        }

        private void OnShopDown(object sender, EventArgs e)
        {
            OnMoveDown();
        }

        private void OnSetLocation(object sender, EventArgs e)
        {
            ShopInfo si = shops.Find(p => p.a_keeper_idx.Equals(SelectedId));

            si.a_zone_num = Convert.ToInt32(merch_zone.Text);
            si.a_pos_x = Convert.ToInt32(merch_x.Text);
            si.a_pos_z = Convert.ToInt32(merch_z.Text);
            si.a_pos_h = Convert.ToInt32(merch_h.Text);
            si.a_pos_r = Convert.ToInt32(merch_r.Text);

            // Find update method for y layer

            Deserialize<ShopInfo> tShopInfo = new Deserialize<ShopInfo>("t_shop");
            tShopInfo.SetKey("a_keeper_idx");
            tShopInfo.SetData(si);

            new Transactions<ShopInfo>(DataCon).ExecuteQuery(tShopInfo, QUERY_TYPE.UPDATE);
        }

        private void OnDropListDoubleClick(object sender, EventArgs e)
        {
            if (dropListGrid.SelectedRows.Count <= 0 || WindowFilling)
            {
                return;
            }

            int Row = dropListGrid.SelectedRows[0].Index;

            String item = String.Format("a_item_{0}", Row);
            String precent = String.Format("a_item_percent_{0}", Row);

            Deserialize<NpcData> npcStruct = new Deserialize<NpcData>("t_npc");

            int idx = data.FindIndex(p => p.a_index == SelectedId);

            npcStruct.SetData(data[idx]);

            ItemSelector sel = ItemSelector.Instance();

            // This instance has been updated!
            if (sel.Show(this, Convert.ToInt32(npcStruct[item]), Convert.ToInt32(npcStruct[precent])) == DialogResult.OK)
            {
                DropItem selected = items.Find(p => p.a_index.Equals(sel.SelectedIndex));

                if (selected != null)
                {
                    npcStruct[item] = selected.a_index;
                    npcStruct[precent] = sel.SelectedProb;

                    data[idx] = npcStruct.Serialize();

                    dropDesc.SetData(selected);

                    dropListGrid.Rows[Row].Cells[0].Value = IconCache.GetItemIcon(selected.a_texture_id, selected.a_texture_row, selected.a_texture_col);
                    dropListGrid.Rows[Row].Cells[1].Value = selected.a_index;
                    dropListGrid.Rows[Row].Cells[2].Value = dropDesc[LocalNameString];
                    dropListGrid.Rows[Row].Cells[3].Value = sel.SelectedProb;
                }
            }
        }

        private void OnClearName(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TextBox ctrl = this.Controls.Find(String.Format("tbName_{0}", Core.LangCode), true).First() as TextBox;
            ctrl.Text = String.Empty;
        }

        private void OnFixNames(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Open offical Lod for name ripping";
            ofd.Filter = "Old Npc Data Lod (*.lod)|*.lod";

            if (ofd.ShowDialog() == DialogResult.OK)
                MatisRipper(ofd);
        }

        private void MatisRipper(OpenFileDialog ofd)
        {
            AddTask(delegate()
            {
                SpinnerState(true);

                BinaryReader br = new BinaryReader(File.Open(ofd.FileName, FileMode.Open, FileAccess.Read));

                int Count = br.ReadInt32();
                br.ReadInt32();

                Dictionary<Int32, String> namesDict = new Dictionary<int, string>();

                for (int i = 0; i < Count; i++)
                {
                    int anIndex = br.ReadInt32();
                    namesDict.Add(anIndex, String.Empty);

                    // Read Npc Name
                    int len = br.ReadInt32();
                    if (len > 0)
                        namesDict[anIndex] = Core.Encoder.GetString(br.ReadBytes(len));

                    int len2 = br.ReadInt32();
                    if (len2 > 0)
                        Core.Encoder.GetString(br.ReadBytes(len2));
                }

                for (int i = 0; i < data.Count; i++)
                {
                    if (!namesDict.Keys.Contains(data[i].a_index))
                        continue;

                    if (namesDict[data[i].a_index] != String.Empty)
                    {
                        npcDesc.SetData(data[i]);
                        npcDesc[LocalNameString] = namesDict[data[i].a_index];
                        data[i] = npcDesc.Serialize();
                    }
                }

                DoRebuildList();

                br.Close();
                br.Dispose();

                Deserialize<NpcData> dStruct = new Deserialize<NpcData>("t_npc");
                dStruct.SetKey("a_index");

                new Transactions<NpcData>(DataCon).ExecuteQuery(dStruct, data, QUERY_TYPE.UPDATE);

                SpinnerState(false);
            });
        }

        private void OnExportShopNames(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Npc Shop Names lod (*.lod)|*.lod";
            sfd.Title = "Save Npc Shop Names lod";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                AddTask(delegate()
                {
                    SpinnerState(true);

                    BinaryWriter bw = new BinaryWriter(File.Open(sfd.FileName, FileMode.Create));

                    // nRow
                    bw.Write(shops.Count);
                    // nMax
                    bw.Write(shops.Last().a_keeper_idx);

                    var result = ETaskBlockResult.Complete;

                    for (int i = 0; i < shops.Count; i++)
                    {
                        result = TaskBlock(delegate
                        {
                            bw.Write(shops[i].a_keeper_idx);

                            int len = Core.Encoder.GetByteCount(shops[i].a_name);
                            bw.Write(len);

                            if (len > 0)
                                bw.Write(Core.Encoder.GetBytes(shops[i].a_name));

                            int len2 = 0;
                            bw.Write(len2);
                        });

                        if (result == ETaskBlockResult.Canceled)
                            break;
                    }

                    bw.Flush();
                    bw.Close();
                    bw.Dispose();

                    SpinnerState(false);

                    if (result == ETaskBlockResult.Canceled)
                    {
                        MsgDialogs.ShowNoLog("Export Canceled", "Export Operation Was Canceled.", "ok", MsgDialogs.MsgTypes.WARNING);

                        if (File.Exists(sfd.FileName))
                            File.Delete(sfd.FileName);
                    }
                    else
                    {
                        MsgDialogs.Show("Export Complete", "Finished Exporting Shop Names.", "ok", MsgDialogs.MsgTypes.INFO);
                    }
                });
            }
        }

        private void SetPrice_Click(object sender, EventArgs e)
        {
            if (dgShop.SelectedRows.Count <= 0)
                return;

            String Result = Interaction.InputBox("New Item Price: ", "Set Item Price", "");

            int val = 0;
            bool ret = int.TryParse(Result, out val);

            if (ret)
            {
                DropItem aItem = items.Find(p => p.a_index == itemsdata.FindAll(t => t.a_keeper_idx.Equals(SelectedId))[dgShop.SelectedRows[0].Index].a_item_idx);

                aItem.a_price = val;

                Deserialize<DropItem> thisItem = new Deserialize<DropItem>("t_item");
                thisItem.SetKey("a_index");
                thisItem.SetData(aItem);

                dgShop.SelectedRows[0].Cells[3].Value = val;

                new Transactions<DropItem>(DataCon).ExecuteQuery(thisItem, QUERY_TYPE.UPDATE);
            }
        }

        private void EmptyDropBtn_Click(object sender, EventArgs e)
        {
            int npcidx = data.FindIndex(p => p.a_index.Equals(SelectedId));

            if (npcidx == -1)
                return;

            Deserialize<NpcData> workingSet = new Deserialize<NpcData>("t_npc");

            workingSet.SetData(data[npcidx]);
            workingSet.SetKey("a_index");

            for (int i = 0; i < 20; i++)
            {
                String col = String.Format("a_item_{0}", i);
                String colProb = String.Format("a_item_percent_{0}", i);

                workingSet[col] = -1;
                workingSet[colProb] = 0;

                dropListGrid.Rows[i].Cells[0].Value = m_errorIcon;
                dropListGrid.Rows[i].Cells[1].Value = -1;
                dropListGrid.Rows[i].Cells[2].Value = String.Empty;
                dropListGrid.Rows[i].Cells[3].Value = 0;
            }

            data[npcidx] = workingSet.Serialize();
        }

        private void DoDeleteDropItem(object sender, EventArgs e)
        {
            if (dropListGrid.SelectedRows.Count <= 0)
                return;

            int rowId = dropListGrid.SelectedRows[0].Index;

            // Find the selected npc index
            int npcidx = data.FindIndex(p => p.a_index.Equals(SelectedId));

            if (npcidx == -1)
                return;

            // break it down and modify data
            Deserialize<NpcData> workingSet = new Deserialize<NpcData>("t_npc");

            workingSet.SetData(data[npcidx]);
            workingSet.SetKey("a_index");

            String col = String.Format("a_item_{0}", rowId);
            String colProb = String.Format("a_item_percent_{0}", rowId);

            workingSet[col] = -1;
            workingSet[colProb] = 0;

            // write npc back to selected index
            data[npcidx] = workingSet.Serialize();

            // clear the data grid row
            dropListGrid.Rows[rowId].Cells[0].Value = m_errorIcon;
            dropListGrid.Rows[rowId].Cells[1].Value = -1;
            dropListGrid.Rows[rowId].Cells[2].Value = String.Empty;
            dropListGrid.Rows[rowId].Cells[3].Value = 0;
        }

        private void OnCheckForUpdates(object sender, EventArgs e)
        {
            MsgDialogs.Show("Updates Disabled", "Updates are disabled for current release.", "ok", MsgDialogs.MsgTypes.INFO);
        }

        private void DoUpdate()
        {
            string file = this.GetType().Assembly.Location;
            string app = System.IO.Path.GetFileNameWithoutExtension(file); 

            ProcessStartInfo P = new ProcessStartInfo("Updater.exe", String.Format("/update http://cdn.illusionistsoftworks.com/updates/ {0} {1}", app, file));
            Process.Start(P);

            Application.Exit();
        }

        private void DoUpdateReady(object sender, EventArgs e)
        {
            DialogResult dr = MsgDialogs.ShowNoLog("Update Found", "New Version Found Update Now?", "yesno", MsgDialogs.MsgTypes.INFO);

            if (dr == DialogResult.Yes)
                DoUpdate();
        }

        private void OnChangeClientPath(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if(fbd.ShowDialog() == DialogResult.Cancel)
                return;

            String path = fbd.SelectedPath;

            if (path.Last() == '\\' || path.Last() == '/')
            {
                IllTechLibrary.Settings.Preferences.SetPrefKey("CLIENT", "root", path);
            }
            else
            {
                IllTechLibrary.Settings.Preferences.SetPrefKey("CLIENT", "root", path + "\\");
            }
        }

        private void On3DResize(object sender, EventArgs e)
        {
        }

        private void OnExportZoneFlag(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Export ZoneFlag.lod";
            sfd.Filter = "ZoneFlag File (*.lod)|*.lod";

            if(sfd.ShowDialog() == DialogResult.OK)
            {
                BinaryWriter bw = new BinaryWriter(sfd.OpenFile());

                int icount = 0;

                for(int i = 0; i < data.Count; i++)
                {
                    if((BitHelpers.GetBit32(4, data[i].a_flag) && BitHelpers.GetBit32(5, data[i].a_flag)))
                    {
                        icount++;
                    }
                }

                bw.Write(icount);

                for (int i = 0; i < data.Count; i++)
                {
                    // 5
                    if ((BitHelpers.GetBit32(4, data[i].a_flag) && BitHelpers.GetBit32(5, data[i].a_flag)))
                    {
                        bw.Write(data[i].a_index);

                        bw.Write(data[i].a_zone_flag);
                        bw.Write(data[i].a_extra_flag+1);
                    }
                }

                bw.Close();
                bw.Dispose();

                MsgDialogs.Show("Export Complete", "Finished Exporting ZoneFlag.", "ok", MsgDialogs.MsgTypes.INFO);
            }
        }

        private void OnFixZoneFlag(object sender, EventArgs e)
        {
            if (!DataCon.IsConnected())
            {
                MessageBox.Show("Requires Active Connection.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(MessageBox.Show("This operation will take a while to complete.\nThe program will fail to respond continue?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            FileSelectDialog fp = new FileSelectDialog("Get Regular LOD", "LastChaos ZoneFlag.lod|*.lod", "");

            String f = fp.GetFile();

            if(File.Exists(f))
            {
                BinaryReader br = new BinaryReader(File.Open(f, FileMode.Open));

                int count = br.ReadInt32();

                List<int> theClean = new List<int>();
                List<UInt64> zlist = new List<ulong>();
                List<UInt64> elist = new List<ulong>();

                for(int i = 0; i < count; i++)
                {
                    int idx = br.ReadInt32();

                    theClean.Add(idx);

                    UInt64 zf = br.ReadUInt64();
                    UInt64 ze = br.ReadUInt64();

                    zlist.Add(zf);
                    elist.Add(ze);
                }

                for(int i = 0; i < theClean.Count; i++)
                {
                    int theFDX = data.FindIndex(p => p.a_index.Equals(theClean[i]));

                    if(theFDX != -1)
                    {
                        data[theFDX].a_zone_flag = zlist[i];
                        data[theFDX].a_extra_flag = elist[i];

                       NpcList.SelectedIndex = theFDX;
                       DoUpdateData();
                    }
                }
            }
        }

        private void ZoneFlagItemCheck(object sender, ItemCheckEventArgs e)
        {
            UInt64 ZoneFlag = 0;

            // Zone Flag
            DoSetBits(clbZone, e, out ZoneFlag);

            tbzf.Text = ZoneFlag.ToString();
        }

        private void ZoneFlagExtraItemCheck(object sender, ItemCheckEventArgs e)
        {
            UInt64 ZoneFlagExtra = 0;

            // Zone Flag
            DoSetBits(clbExtra, e, out ZoneFlagExtra);

            tbze.Text = ZoneFlagExtra.ToString();
        }

        internal void OnFormResizeComplete(object sender, EventArgs e)
        {
            //renderer.Resize(this.panel3DView);

            if (NpcList.SelectedIndex != -1)
            {
                NpcData d = data.Find(p => p.a_index == SelectedId);

                String fullPath = Preferences.GetData()["CLIENT"]["root"] + d.a_file_smc;

                //if (File.Exists(fullPath))
                    //renderer.LoadMesh(fullPath);
            }
        }

        private void OnFormShown(object sender, EventArgs e)
        {
            /*renderer = new CRenderer();

            renderer.SetZoom(slideZoom.Value);
            renderer.SetPan(slideLeftRight.Value);
            renderer.SetScroll(slideUpDown.Value);

            renderer.SetFpsTracker(deviceFPSLabel);
            renderer.SetVertexCounter(VertexCountLabel);
            renderer.SetPolyCounter(PolygonCountLabel);
            renderer.SetClearColor(KnownColor.Wheat);
            renderer.Initialize(this.panel3DView);
            renderer.Start();

            deviceNameLabel.Text = String.Format("Device: {0}", renderer.GetDeviceName());
            DriverNameLabel.Text = String.Format("Driver: {0}", renderer.GetDriverName());

            IsLoading = false;*/
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnScroll3DScroll(object sender, ScrollEventArgs e)
        {
            //renderer.SetRotate(e.NewValue);
        }

        private void AddDropAllItem(object sender, EventArgs e)
        {
            // Get the currently loaded NPC if any
            NpcData anNpc = data.Find(p => p.a_index.Equals(SelectedId));

            if (anNpc == null)
                return;

            ItemSelector sel = ItemSelector.Instance();//new ItemSelector(-1, 0);

            // This instance has been updated!
            if (sel.Show(this, -1, 0) == DialogResult.OK)
            {
                NpcDropAll nda = new NpcDropAll();

                DropItem it = items.Find(p => p.a_index.Equals(sel.SelectedIndex));

                if (it != null)
                {
                    if (dropAllItems.FindIndex(p => p.a_item_idx.Equals(it.a_index)) != -1)
                    {
                        MsgDialogs.ShowNoLog("Cannot Add Item", "Item Already Exists On The Npcs Table!", "OK", MsgDialogs.MsgTypes.ERROR);
                        return;
                    }

                    dropDesc.SetData(it);

                    nda.a_item_idx = it.a_index;
                    nda.a_npc_idx = anNpc.a_index;
                    nda.a_prob = sel.SelectedProb;

                    dropAllItems.Add(nda);

                    int rowId = dropAllGrid.Rows.Add();

                    dropAllGrid.Rows[rowId].Cells[0].Value = IconCache.GetItemIcon(it.a_texture_id, it.a_texture_row, it.a_texture_col);
                    dropAllGrid.Rows[rowId].Cells[1].Value = it.a_index;
                    dropAllGrid.Rows[rowId].Cells[2].Value = dropDesc[LocalNameString];
                    dropAllGrid.Rows[rowId].Cells[3].Value = nda.a_prob;

                    Deserialize<NpcDropAll> stQuery = new Deserialize<NpcDropAll>("t_npc_drop_all");

                    stQuery.SetData(nda);

                    new Transactions<NpcDropAll>(DataCon).ExecuteQuery(stQuery, QUERY_TYPE.INSERT);
                }
            }
        }

        private void DeleteDropAllItem(object sender, EventArgs e)
        {
            // No row selected do nothing
            if (dropAllGrid.SelectedRows.Count <= 0)
                return;

            // Find the index of selected row
            int rowId = dropAllGrid.SelectedRows[0].Index;

            // Get the currently loaded NPC if any
            NpcData anNpc = data.Find(p => p.a_index.Equals(SelectedId));

            if (anNpc == null)
                return;

            // Find drop item related to this row
            int dropIdx = dropAllItems.FindIndex(p => p.a_item_idx.Equals(dropAllGrid.Rows[rowId].Cells[1].Value));

            // Could not find the item shit got goosed
            if (dropIdx == -1)
                return;

            Deserialize<NpcDropAll> stQuery = new Deserialize<NpcDropAll>("t_npc_drop_all");

            stQuery.SetData(dropAllItems[dropIdx]);

            stQuery.SetConditions($"WHERE a_npc_idx = {anNpc.a_index} AND a_item_idx = {dropAllItems[dropIdx].a_item_idx}");

            new Transactions<NpcDropAll>(DataCon).ExecuteQuery(stQuery, QUERY_TYPE.DELETE);

            dropAllItems.RemoveAt(dropIdx);

            dropAllGrid.Rows.RemoveAt(rowId);
        }

        private void DeleteAllDropAllItems(object sender, EventArgs e)
        {
            // Get the currently loaded NPC if any
            NpcData anNpc = data.Find(p => p.a_index.Equals(SelectedId));

            if (anNpc == null)
                return;

            if (MsgDialogs.ShowNoLog("Question", "Are you sure you want to delete the drop all data?\n\nTHIS CANNOT BE UNDONE!", "YesNo", MsgDialogs.MsgTypes.WARNING) == DialogResult.Yes)
            {
                Deserialize<NpcDropAll> stQuery = new Deserialize<NpcDropAll>("t_npc_drop_all");

                stQuery.SetConditions($"where a_npc_idx = {anNpc.a_index}");

                new Transactions<NpcDropAll>(DataCon).ExecuteQuery(stQuery, QUERY_TYPE.DELETE);

                dropAllItems.Clear();
                dropAllGrid.Rows.Clear();
            }
        }

        private void DropAllListDoubleClick(object sender, EventArgs e)
        {
            if (dropAllGrid.SelectedRows.Count <= 0 || WindowFilling)
            {
                return;
            }

            // Get the currently loaded NPC if any
            NpcData anNpc = data.Find(p => p.a_index.Equals(SelectedId));

            if (anNpc == null)
                return;

            int Row = dropAllGrid.SelectedRows[0].Index;

            int idx = data.FindIndex(p => p.a_index == SelectedId);

            int oldIndex = (int)dropAllGrid.Rows[Row].Cells[1].Value;
            int oldProb = (int)dropAllGrid.Rows[Row].Cells[3].Value;

            ItemSelector sel = ItemSelector.Instance();//new ItemSelector(oldIndex, oldProb);

            // This instance has been updated!
            if (sel.Show(this, oldIndex, oldProb) == DialogResult.OK)
            {
                DropItem selected = items.Find(p => p.a_index.Equals(sel.SelectedIndex));

                if (selected != null)
                {
                    dropDesc.SetData(selected);

                    int newIndex = selected.a_index;
                    int newProb = sel.SelectedProb;

                    // Stop them from inserting an item that already exists but not from updating it
                    if (dropAllItems.FindIndex(p => p.a_npc_idx.Equals(anNpc.a_index) && p.a_item_idx.Equals(newIndex)) != -1)
                    {
                        if (oldIndex != newIndex)
                            return;
                    }

                    dropAllGrid.Rows[Row].Cells[0].Value = IconCache.GetItemIcon(selected.a_texture_id, selected.a_texture_row, selected.a_texture_col);
                    dropAllGrid.Rows[Row].Cells[1].Value = selected.a_index;
                    dropAllGrid.Rows[Row].Cells[2].Value = dropDesc[LocalNameString];
                    dropAllGrid.Rows[Row].Cells[3].Value = sel.SelectedProb;

                    new Transactions<NpcDropAll>(DataCon).ExecuteQuery($"UPDATE `t_npc_drop_all` SET a_item_idx = {newIndex}, a_prob = {newProb} WHERE a_npc_idx = {anNpc.a_index} AND a_item_idx = {oldIndex};");
                }
            }
        }

        private void OnSearchEnter(object sender, EventArgs e)
        {
            if (SearchBox.Text == "Search...")
            {
                SearchBox.Text = String.Empty;
            }
        }

        private void OnSearchLeave(object sender, EventArgs e)
        {
            if (SearchBox.Text == String.Empty)
            {
                SearchBox.Text = "Search...";
            }

            if (data.Count > 0)
            {
                switch (Core.LangCode)
                {
                    case "usa":
                        ClearNpcList();
                        NpcList.Items.AddRange(data.Select(p => p.a_index + " - " + p.a_name_usa).ToArray());
                        NpcList.SelectedIndex = 0;
                        break;
                    case "thai":
                        ClearNpcList();
                        NpcList.Items.AddRange(data.Select(p => p.a_index + " - " + p.a_name_thai).ToArray());
                        NpcList.SelectedIndex = 0;
                        break;
                }
            }
        }
    }
}
