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
using IllTechLibrary.Dialogs;
using IllTechLibrary.Settings;
using IllTechLibrary.DataFiles;
using IllTechLibrary.Localization;
using IllTechLibrary.Serialization;
using IllTechLibrary.SharedStructs;

using MetroFramework.Controls;
using System.Drawing.Imaging;
using System.Collections;

using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace LCMT.ItemTool
{
    public enum ItemSubTypes
    {
        WEAPON = 0,
        ARMOR,
        USE_ONCE,
        BULLET,
        ETC,
        ACCESSORY,
        POTION
    }

    public partial class ItemFrm : LCToolFrm
    {
        // Storage
        private List<Item> items = new List<Item>();
        private List<Option> standardOpt = new List<Option>();
        private List<RareOption> rareOpt = new List<RareOption>();
        private List<ItemFortuneLOD> fortData = new List<ItemFortuneLOD>();
        private List<ItemJewelData> jewelData = new List<ItemJewelData>();

        // Deserializers
        private Deserialize<Item> itemDesc = new Deserialize<Item>("t_item");
        private Deserialize<Option> optDesc = new Deserialize<Option>("t_option");
        private Deserialize<RareOption> rareDesc = new Deserialize<RareOption>("t_rareoption");
        private Deserialize<ItemFortuneLOD> fortDesc = new Deserialize<ItemFortuneLOD>("t_fortune_data");

        private bool bOptionsCreated = false;

        private List<String> rareOpt0 = new List<string>();
        private List<String> rareOpt1 = new List<string>();
        private List<String> rareOpt2 = new List<string>();
        private List<String> rareOpt3 = new List<string>();
        private List<String> rareOpt4 = new List<string>();
        private List<String> rareOpt5 = new List<string>();
        private List<String> rareOpt6 = new List<string>();
        private List<String> rareOpt7 = new List<string>();
        private List<String> rareOpt8 = new List<string>();
        private List<String> rareOpt9 = new List<string>();

        private List<String> standardOpt0 = new List<string>();
        private List<String> standardOpt1 = new List<string>();
        private List<String> standardOpt2 = new List<string>();
        private List<String> standardOpt3 = new List<string>();
        private List<String> standardOpt4 = new List<string>();
        private List<String> standardOpt5 = new List<string>();
        private List<String> standardOpt6 = new List<string>();
        private List<String> standardOpt7 = new List<string>();
        private List<String> standardOpt8 = new List<string>();
        private List<String> standardOpt9 = new List<string>();


        // Render Device
        //private CRenderer renderer;

        // ASCII Encoding
        private Encoding Enc = Encoding.ASCII;

        // Delegate declarations
        private delegate void NoReturn();
        private delegate String GetStringDel();
        private delegate void SetString(String str);
        private delegate void LoadStateChanger(bool state);

        // Delegate vars
        private SetString AddItemList;
        private NoReturn ClearItemList;
        private GetStringDel GetSearchText;
        private LoadStateChanger LoadState;

        // Last File Names
        private String LastSaveFile = String.Empty;
        private String lastEffectFile = String.Empty;

        // Current Selected ID
        private int SelectedId = -1;

        // Tool ID Tag
        public static String ItemToolID = "ITEM_TOOL";

        public ItemFrm() : base(ItemToolID)
        {
            InitializeComponent();
        }

        // Properties
        private String LocalPrefixString
        {
            get { return String.Format("a_prefix_{0}", Core.LangCode); }
        }

        private String LocalNameString
        {
            get { return String.Format("a_name_{0}", Core.LangCode); }
        }

        private String LocalDescString
        {
            get { return String.Format("a_descr_{0}", Core.LangCode); }
        }

        // Delegate Methods
        private void DelAddItemList(String str)
        {
            if (ItemList.InvokeRequired)
            {
                ItemList.Invoke(new SetString(DelAddItemList), new Object[] { str });
            }
            else
            {
                ItemList.Items.Add(str);
            }
        }

        private void DelClearItemList()
        {
            if (ItemList.InvokeRequired)
            {
                ItemList.Invoke(new NoReturn(DelClearItemList));
            }
            else
            {
                ItemList.Items.Clear();
            }
        }

        private String DelGetSearchString()
        {
            if (SearchBox.InvokeRequired)
            {
                return (String)SearchBox.Invoke(new GetStringDel(DelGetSearchString));
            }
            else
            {
                return SearchBox.Text;
            }
        }

        // Overloads
        public override void OnConnect()
        {
            itemDesc = new Deserialize<Item>("t_item");
            optDesc = new Deserialize<Option>("t_option");
            rareDesc = new Deserialize<RareOption>("t_rareoption");
            fortDesc = new Deserialize<ItemFortuneLOD>("t_fortune_data");

            tbName_USA.Name = String.Format("tbName_{0}", Core.LangCode);
            tbdescr_usa.Name = String.Format("tbdescr_{0}", Core.LangCode);

            cbRareIndex0.Items.Clear();
            cbRareIndex1.Items.Clear();
            cbRareIndex2.Items.Clear();
            cbRareIndex3.Items.Clear();
            cbRareIndex4.Items.Clear();
            cbRareIndex5.Items.Clear();
            cbRareIndex6.Items.Clear();
            cbRareIndex7.Items.Clear();
            cbRareIndex8.Items.Clear();
            cbRareIndex9.Items.Clear();

            AddTask(DoLoadAll);

            DoSetItemStateConditional(DatabaseContextMenu.DropDown, "EnableOnConnected", true);
        }

        public override void OnDisconnect()
        {
            //renderer.Dispose();

            ItemList.Items.Clear();

            items.Clear();
            rareOpt.Clear();
            standardOpt.Clear();
            fortData.Clear();

            bOptionsCreated = false;

            rareOpt0.Clear();
            rareOpt1.Clear();
            rareOpt2.Clear();
            rareOpt3.Clear();
            rareOpt4.Clear();
            rareOpt5.Clear();
            rareOpt6.Clear();
            rareOpt7.Clear();
            rareOpt8.Clear();
            rareOpt9.Clear();

            standardOpt0.Clear();
            standardOpt1.Clear();
            standardOpt2.Clear();
            standardOpt3.Clear();
            standardOpt4.Clear();
            standardOpt5.Clear();
            standardOpt6.Clear();
            standardOpt7.Clear();
            standardOpt8.Clear();
            standardOpt9.Clear();

            DoSetItemStateConditional(DatabaseContextMenu.DropDown, "EnableOnConnected", false);
        }

        private void DoFillWindow()
        {
            if (!bOptionsCreated)
            {
                for (int i = 0; i < rareOpt.Count; i++)
                {
                    rareDesc.SetData(rareOpt[i]);
                    rareOpt0.Add(String.Format("{0} - {1}", rareOpt[i].a_index, rareDesc[LocalPrefixString]));
                    rareOpt1.Add(String.Format("{0} - {1}", rareOpt[i].a_index, rareDesc[LocalPrefixString]));
                    rareOpt2.Add(String.Format("{0} - {1}", rareOpt[i].a_index, rareDesc[LocalPrefixString]));
                    rareOpt3.Add(String.Format("{0} - {1}", rareOpt[i].a_index, rareDesc[LocalPrefixString]));
                    rareOpt4.Add(String.Format("{0} - {1}", rareOpt[i].a_index, rareDesc[LocalPrefixString]));
                    rareOpt5.Add(String.Format("{0} - {1}", rareOpt[i].a_index, rareDesc[LocalPrefixString]));
                    rareOpt6.Add(String.Format("{0} - {1}", rareOpt[i].a_index, rareDesc[LocalPrefixString]));
                    rareOpt7.Add(String.Format("{0} - {1}", rareOpt[i].a_index, rareDesc[LocalPrefixString]));
                    rareOpt8.Add(String.Format("{0} - {1}", rareOpt[i].a_index, rareDesc[LocalPrefixString]));
                    rareOpt9.Add(String.Format("{0} - {1}", rareOpt[i].a_index, rareDesc[LocalPrefixString]));
                }

                for(int i = 0; i < standardOpt.Count; i++)
                {
                    optDesc.SetData(standardOpt[i]);
                    standardOpt0.Add(String.Format("{0} - {1}", standardOpt[i].a_type, optDesc[LocalNameString]));
                    standardOpt1.Add(String.Format("{0} - {1}", standardOpt[i].a_type, optDesc[LocalNameString]));
                    standardOpt2.Add(String.Format("{0} - {1}", standardOpt[i].a_type, optDesc[LocalNameString]));
                    standardOpt3.Add(String.Format("{0} - {1}", standardOpt[i].a_type, optDesc[LocalNameString]));
                    standardOpt4.Add(String.Format("{0} - {1}", standardOpt[i].a_type, optDesc[LocalNameString]));
                    standardOpt5.Add(String.Format("{0} - {1}", standardOpt[i].a_type, optDesc[LocalNameString]));
                    standardOpt6.Add(String.Format("{0} - {1}", standardOpt[i].a_type, optDesc[LocalNameString]));
                    standardOpt7.Add(String.Format("{0} - {1}", standardOpt[i].a_type, optDesc[LocalNameString]));
                    standardOpt8.Add(String.Format("{0} - {1}", standardOpt[i].a_type, optDesc[LocalNameString]));
                    standardOpt9.Add(String.Format("{0} - {1}", standardOpt[i].a_type, optDesc[LocalNameString]));
                }

                bOptionsCreated = true;
            }

            if (items.Count <= 0)
                return;

            //WindowFilling = true;

            Item d = items.Find(p => p.a_index == SelectedId);
            itemDesc.SetData(d);

            if ((d.a_flag & 0x8000) >= 1)
            {
                cbRareIndex0.Items.Clear();
                cbRareIndex0.Items.AddRange(rareOpt0.ToArray());
                cbRareIndex1.Items.Clear();
                cbRareIndex1.Items.AddRange(rareOpt1.ToArray());
                cbRareIndex2.Items.Clear();
                cbRareIndex2.Items.AddRange(rareOpt2.ToArray());
                cbRareIndex3.Items.Clear();
                cbRareIndex3.Items.AddRange(rareOpt3.ToArray());
                cbRareIndex4.Items.Clear();
                cbRareIndex4.Items.AddRange(rareOpt4.ToArray());
                cbRareIndex5.Items.Clear();
                cbRareIndex5.Items.AddRange(rareOpt5.ToArray());
                cbRareIndex6.Items.Clear();
                cbRareIndex6.Items.AddRange(rareOpt6.ToArray());
                cbRareIndex7.Items.Clear();
                cbRareIndex7.Items.AddRange(rareOpt7.ToArray());
                cbRareIndex8.Items.Clear();
                cbRareIndex8.Items.AddRange(rareOpt8.ToArray());
                cbRareIndex9.Items.Clear();
                cbRareIndex9.Items.AddRange(rareOpt9.ToArray());
            }
            else
            {
                cbRareIndex0.Items.Clear();
                cbRareIndex0.Items.AddRange(standardOpt0.ToArray());
                cbRareIndex1.Items.Clear();
                cbRareIndex1.Items.AddRange(standardOpt1.ToArray());
                cbRareIndex2.Items.Clear();
                cbRareIndex2.Items.AddRange(standardOpt2.ToArray());
                cbRareIndex3.Items.Clear();
                cbRareIndex3.Items.AddRange(standardOpt3.ToArray());
                cbRareIndex4.Items.Clear();
                cbRareIndex4.Items.AddRange(standardOpt4.ToArray());
                cbRareIndex5.Items.Clear();
                cbRareIndex5.Items.AddRange(standardOpt5.ToArray());
                cbRareIndex6.Items.Clear();
                cbRareIndex6.Items.AddRange(standardOpt6.ToArray());
                cbRareIndex7.Items.Clear();
                cbRareIndex7.Items.AddRange(standardOpt7.ToArray());
                cbRareIndex8.Items.Clear();
                cbRareIndex8.Items.AddRange(standardOpt8.ToArray());
                cbRareIndex9.Items.Clear();
                cbRareIndex9.Items.AddRange(standardOpt9.ToArray());
            }

            cb_enabled.Checked = d.a_enable == 1 ? true : false;
            pbIcon.Image = IconCache.GetItemIcon(d.a_texture_id, d.a_texture_row, d.a_texture_col) as Image;

            pbIcon.Tag = String.Format("{0} {1} {2}", d.a_texture_id, d.a_texture_row, d.a_texture_col);

            iconFileID.Text = d.a_texture_id.ToString();
            iconRow.Text = d.a_texture_row.ToString();
            iconCol.Text = d.a_texture_col.ToString();

            cb_castle_war.Checked = d.a_castle_war == 1 ? true : false;

            if (d.a_wearing < 0)
            {
                cbWearType.SelectedIndex = 0;
            }
            else
            {
                cbWearType.SelectedIndex = d.a_wearing + 1;
            }

            List<ItemFortuneLOD> fortunes = fortData.FindAll(p => p.a_item_idx.Equals(d.a_index));

            fortuneGrid.Rows.Clear();

            if (fortunes.Count != 0)
            {
                for (int i = 0; i < fortunes.Count; i++)
                {
                    DataGridViewRow dgv = fortuneGrid.Rows[fortuneGrid.Rows.Add()];

                    dgv.Cells[0].Value = fortunes[i].a_skill_index.ToString();
                    dgv.Cells[1].Value = fortunes[i].a_skill_level.ToString();
                    dgv.Cells[2].Value = fortunes[i].a_string_index.ToString();
                    dgv.Cells[3].Value = fortunes[i].a_prob.ToString();
                }

                btnShopAdd.Enabled = true;
                btnShopDelete.Enabled = true;
            }
            else
            {
                btnShopAdd.Enabled = true;
                btnShopDelete.Enabled = false;
            }

            cbType.SelectedIndex = d.a_type_idx;
            cbSubType.SelectedIndex = d.a_subtype_idx;

            List<Control> textBoxes = ControlUtil.GetAll(this.tabControl1, typeof(TextBox)).ToList();

            foreach (TextBox a in textBoxes)
            {
                String realName = a.Name.Replace("tb", "");

                int anIndex = itemDesc.nameList.FindIndex(p => p.ToLower().Equals("a_" + realName.ToLower()));

                if (anIndex != -1)
                    a.Text = Convert.ToString(itemDesc[anIndex]);
            }

            // Fill in any missing items to prevent loss of rareopt index data
            for (int i = 0; i < 10; i++)
            {
                ComboBox aTmpBox = (ComboBox)groupBox3.Controls.Find($"cbRareIndex{i}", false).FirstOrDefault();

                if(aTmpBox != null)
                {
                    // The option index we are searching
                    string itemStr = $"a_rare_index_{i}";

                    // Try to find the existing item
                    aTmpBox.SelectedIndex = SearchComboBox(aTmpBox, (int)itemDesc[itemStr]);

                    // If finding an existing item fails add it to the list
                    if (aTmpBox.SelectedIndex == -1 && ((int)itemDesc[itemStr]) != -1)
                    {
                        // Index of an item cause its not a fucking seal
                        int otherIndex = (int)itemDesc[itemStr];

                        int oItemIdx = items.FindIndex(p => p.a_index == otherIndex);
                        
                        if(oItemIdx != -1)
                        {
                            // Get the name of the other part item
                            Deserialize<Item> otherItem = new Deserialize<Item>("t_item");
                            otherItem.SetData(items[oItemIdx]);

                            int anewItem = aTmpBox.Items.Add(String.Format("{0} - {1}", otherIndex, otherItem[LocalNameString].ToString()));
                            aTmpBox.SelectedIndex = anewItem;
                        }
                    }

                    if(aTmpBox.SelectedIndex == -1)
                        aTmpBox.ResetText();
                }
            }

            //WindowFilling = false;

            String fullPath = Preferences.GetData()["CLIENT"]["root"] + d.a_file_smc;

            if (File.Exists(fullPath))
            {
                smcExists.Checked = true;
                //renderer.LoadMesh(fullPath);
            }
            else
            {
                smcExists.Checked = false;
            }
        }

        private int SearchComboBox(ComboBox cbBox, int a_rare_index)
        {
            if (a_rare_index == -1)
                return -1;

            for(int i = 0; i < cbBox.Items.Count; i++)
            {
                string[] split = cbBox.Items[i].ToString().Split(new char[] { '-' });

                if (split.Count() != 0)
                {
                    if (int.Parse(split[0]) == a_rare_index)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        private void DoRebuildList()
        {
            if (ItemList.InvokeRequired)
            {
                ItemList.Invoke(new NoReturn(DoRebuildList));
            }
            else
            {
                ItemList.Items.Clear();

                items.Sort((x, y) => x.a_index.CompareTo(y.a_index));

                String[] listBoxEntries = new String[items.Count];

                for (int i = 0; i < items.Count; i++)
                {
                    itemDesc.SetData(items[i]);
                    listBoxEntries[i] = (String.Format("{0} - {1}", items[i].a_index, itemDesc[LocalNameString]));
                }

                ItemList.Items.AddRange(listBoxEntries);

                ItemList.SelectedIndex = ItemList.FindString(Convert.ToString(SelectedId));
            }

            GeneralStatsLabel.Text = $"Stats: Total Items - {items.Count} Enabled - {items.Count(p => p.a_enable == 1)} Disabled - {items.Count(p => p.a_enable == 0)}, Load Time: {loadAllTime} ms";
        }

        private int DoSelectItem(int p)
        {
            for (int i = 0; i < ItemList.Items.Count; i++)
            {
                if (ItemList.Items[i].ToString().ToLower().Split('-')[0].Contains(Convert.ToString(p)))
                {
                    return i;
                }
            }

            return -1;
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

        // Form Load
        private void OnFormLoad(object sender, EventArgs e)
        {
            String demo = String.Empty;

            SearchBox.Enter += OnSearchEnter;
            SearchBox.Leave += OnSearchLeave;

            LoadState = new LoadStateChanger(ShowSpinner);

            GetSearchText = new GetStringDel(DelGetSearchString);
            ClearItemList = new NoReturn(DelClearItemList);
            AddItemList = new SetString(DelAddItemList);

            chk3D.Checked = Convert.ToBoolean(Preferences.GetPrefs("View3DEnabled"));

            this.tabControl1.SelectedIndex = 0;

            effectNameBtn.Tag = tbeffect_name;
            attackEffectBtn.Tag = tbattack_effect_name;
            damageEffectBtn.Tag = tbdamage_effect_name;
        }

        // Editor Events
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
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            /*if (renderer != null)
                renderer.Dispose();*/
        }

        double loadAllTime;

        private void DoLoadAll()
        {
            Invoke((MethodInvoker)delegate
            {
                ItemList.Items.Clear();
                ProgressWheel.Visible = true;
            });

            Stopwatch sw = new Stopwatch();
            sw.Start();

            TaskBlock(delegate
            {
                items = new Transactions<Item>(this.DataCon).ExecuteQuery(itemDesc);
                items = items.OrderBy(p => p.a_index).ToList();
            });

            TaskBlock(delegate
            {
               rareOpt = new Transactions<RareOption>(this.DataCon).ExecuteQuery(rareDesc);
               rareOpt = rareOpt.OrderBy(p => p.a_index).ToList();
            });

            TaskBlock(delegate
            {
                standardOpt = new Transactions<Option>(this.DataCon).ExecuteQuery(optDesc);
                standardOpt = standardOpt.OrderBy(p => p.a_index).ToList();
            });

            TaskBlock(delegate
            {
                fortData = new Transactions<ItemFortuneLOD>(this.DataCon).ExecuteQuery(fortDesc);
                fortData = fortData.OrderBy(p => p.a_item_idx).ToList();
            });

            TimeSpan time = sw.Elapsed;
            sw.Stop();

            loadAllTime = time.TotalMilliseconds;

            Invoke((MethodInvoker)delegate
            {
               DoRebuildList();
               ProgressWheel.Visible = false;
            });
        }

        private void OnItemListIndexChanged(object sender, EventArgs e)
        {
            if (ItemList.SelectedIndex == -1)
                return;

            bool parsed = false;
            int newId = -1;
            parsed = int.TryParse(ItemList.Items[ItemList.SelectedIndex].ToString().Split('-')[0], out newId);

            if (SelectedId == newId)
                return;

            SelectedId = newId;

            DoFillWindow();
        }

        private void OnZoomChange(object sender, ScrollEventArgs e)
        {
            //renderer.SetZoom(this.slideZoom.Value);
        }

        private void OnUpDownChange(object sender, ScrollEventArgs e)
        {
            //renderer.SetScroll(this.slideUpDown.Value);
        }

        private void OnLeftRightChange(object sender, ScrollEventArgs e)
        {
            //renderer.SetPan(this.slideLeftRight.Value);
        }

        private void OnItemFormShown(object sender, EventArgs e)
        {/*
            renderer = new CRenderer();

            renderer.SetZoom(slideZoom.Value);
            renderer.SetPan(slideLeftRight.Value);
            renderer.SetScroll(slideUpDown.Value);

            renderer.SetClearColor(KnownColor.Wheat);
            renderer.Initialize(this.panel3DView);
            renderer.Start();*/
        }

        private void OnIconClick(object sender, EventArgs e)
        {
            IconPickerDlg ipd = new IconPickerDlg(IconPickerDlg.FileType.ItemBtn);

            if (ipd.ShowDialog() == DialogResult.OK)
            {
                IconPickerDlg.IconInfo info = ipd.GetInfo();

                this.pbIcon.Image = IconCache.GetItemIcon(info.id, info.row, info.col) as Image;

                this.pbIcon.Tag = String.Format("{0} {1} {2}", info.id, info.row, info.col);

                iconFileID.Text = info.id.ToString();
                iconRow.Text = info.row.ToString();
                iconCol.Text = info.col.ToString();
            }
        }

        private void OnSearchTextChanged(object sender, EventArgs e)
        {
            if (items.Count <= 0)
                return;

            if (SearchBox.Text == String.Empty || SearchBox.Text == "Search...")
            {
                switch (Core.LangCode)
                {
                    case "usa":
                        ClearItemList();
                        ItemList.Items.AddRange(items.Select(p => p.a_index + " - " + p.a_name_usa).ToArray());
                        ItemList.SelectedIndex = 0;
                        break;
                    case "thai":
                        ClearItemList();
                        ItemList.Items.AddRange(items.Select(p => p.a_index + " - " + p.a_name_thai).ToArray());
                        ItemList.SelectedIndex = 0;
                        break;
                }
                return;
            }

            List<Item> itCollection = new List<Item>();

            switch(Core.LangCode)
            {
                case "usa":
                    itCollection = items.FindAll(p => p.a_name_usa.ToLower().Contains(SearchBox.Text.ToLower()));

                    if(itCollection.Count != 0)
                    {
                        ClearItemList();
                        ItemList.Items.AddRange(itCollection.Select(p => p.a_index + " - " + p.a_name_usa).ToArray());
                        ItemList.SelectedIndex = 0;
                    }
                    break;
                case "thai":
                    itCollection = items.FindAll(p => p.a_name_thai.ToLower().Contains(SearchBox.Text.ToLower()));

                    if(itCollection.Count != 0)
                    {
                        ClearItemList();
                        ItemList.Items.AddRange(itCollection.Select(p => p.a_index + " - " + p.a_name_thai).ToArray());
                        ItemList.SelectedIndex = 0;
                    }
                    break;
            }
        }

        private void OnSelectSmcClick(object sender, EventArgs e)
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

            smcExists.Checked = true;

            //renderer.LoadMesh(RootDir + Selected);
        }

        private void OnSelectEffectClick(object sender, EventArgs e)
        {
            IniParser.Model.IniData data = IllTechLibrary.Settings.Preferences.GetData();

            String RootDir = data["CLIENT"]["root"];

            if (!Directory.Exists(RootDir))
            {
                MsgDialogs.Show("Invalid Directory", "Invalid client directory in Config.ini!", "ok", MsgDialogs.MsgTypes.ERROR);
                return;
            }

            String Selected = String.Empty;

            if (lastEffectFile == String.Empty)
            {
                Selected = new FileSelectDialog("Select Effect", "Effects (*.dat)|*.dat", RootDir).GetFile();

                if (!File.Exists(Selected))
                    return;

                if (!Selected.Contains("Data"))
                {
                    MsgDialogs.Show("Invalid Path", "Effect file is not located in data folder!", "ok", MsgDialogs.MsgTypes.ERROR);
                    return;
                }

                lastEffectFile = Selected;
            }

            EffectPicker ap = new EffectPicker(Selected);
            ap.ShowDialog();
            String animName = ap.GetName();

            if (animName == String.Empty)
                return;

            TextBox tb = (TextBox)((MetroButton)sender).Tag;

            tb.Text = animName;
        }

        private void OnFlagBuilderClick(object sender, EventArgs e)
        {
            if (tbFlag.Text == String.Empty)
                tbFlag.Text = "0";

            ItemFlagBuilder fb2 = new ItemFlagBuilder(long.Parse(tbFlag.Text));

            if (fb2.ShowDialog() == DialogResult.OK)
            {
                tbFlag.Text = fb2.GetFlag();
            }
        }

        private void OnItemTypeChanged(object sender, EventArgs e)
        {
            cbSubType.Items.Clear();

            ItemSubTypes idx = (ItemSubTypes)((ComboBox)sender).SelectedIndex;

            switch (idx)
            {
                case ItemSubTypes.WEAPON:
                    cbSubType.Items.AddRange(new object[] {
                    "Single Sword",
                    "Crossbow",
                    "Staff",
                    "Big Sword",
                    "Axe",
                    "Sorc Staff",
                    "Bow",
                    "Dagger",
                    "Mining",
                    "Gathering",
                    "Charge",
                    "Dual Sword",
                    "Wand",
                    "Scythe",
                    "Polearm",
                    "Soul"
                    });
                    break;
                case ItemSubTypes.ARMOR:
                    cbSubType.Items.AddRange(new object[] {
                    "Helmet",
                    "Jacket",
                    "Pants",
                    "Gloves",
                    "Boots",
                    "Shield",
                    "Backwing",
                    "One Suit"
                    });
                    break;
                case ItemSubTypes.USE_ONCE:
                    cbSubType.Items.AddRange(new object[] {
                    "Warp",
                    "Process Doc",
                    "Make Type Doc",
                    "Box",
                    "Make Potion Doc",
                    "Change Doc",
                    "Quest Scroll",
                    "Cash Item",
                    "Summon",
                    "Etc",
                    "Target",
                    "Title",
                    "Jumping Package",
                    "Jumping Potion",
                    "Char Slot Extend",
                    "Char Server Move",
                    "Express Remote",
                    "Jewel Pocket",
                    "Chaos Jewel Pocket",
                    "Cash Bag Key",
                    "Pet Stash",
                    "GPS",
                    "Holy Water",
                    "Protect PVP"
                    });
                    break;
                case ItemSubTypes.BULLET:
                    cbSubType.Items.AddRange(new object[] {
                    "Attack",
                    "Mana",
                    "Arrow"
                    });
                    break;
                case ItemSubTypes.ETC:
                    cbSubType.Items.AddRange(new object[] {
                    "Quest",
                    "Event",
                    "Skill",
                    "Refine",
                    "Material",
                    "Money",
                    "Product",
                    "Process",
                    "Texture",
                    "Option",
                    "Sample",
                    "Mix Type 1",
                    "Mix Type 2",
                    "Mix Type 3",
                    "Ai",
                    "Quest Trigger",
                    "Jewel",
                    "Stabilizer",
                    "Protect Scroll (Sockets)",
                    "Mercenary Card",
                    "Guild Mark",
                    "Reformer",
                    "Chaos Jewel",
                    "Functions",
                    "Syndicate Jewel"
                    });
                    break;
                case ItemSubTypes.ACCESSORY:
                    cbSubType.Items.AddRange(new object[] {
                    "Charm",
                    "Magic Stone",
                    "Light Stone",
                    "Earing",
                    "Ring",
                    "Necklace",
                    "Pet",
                    "Wild Pet",
                    "Relic"
                    });
                    break;
                case ItemSubTypes.POTION:
                    cbSubType.Items.AddRange(new object[] {
                    "State",
                    "HP",
                    "MP",
                    "Dual",
                    "Stat",
                    "Etc",
                    "UP",
                    "Tears",
                    "Crystal",
                    "Portal Scroll",
                    "Increase HP",
                    "Increase MP",
                    "Pet Heal HP",
                    "Pet Move Speed",
                    "Totem",
                    "Pet Heal MP"
                    });
                    break;
            }
        }

        private void OnItemSubTypeChanged(object sender, EventArgs e)
        {
            ItemSubTypes ctype = (ItemSubTypes)(cbType.SelectedIndex);
            int subSwitch = cbSubType.SelectedIndex;

            switch (ctype)
            {
                case ItemSubTypes.WEAPON:
                    NumLabel0.Text = "Physical Atk";
                    NumLabel1.Text = "Magic Atk";
                    NumLabel2.Text = "Atk Speed";
                    NumLabel3.Text = "Num 3";
                    NumLabel4.Text = "Time (Days)";
                    break;
                case ItemSubTypes.ARMOR:
                    NumLabel0.Text = "Physical Def";
                    NumLabel1.Text = "Magic Def";
                    NumLabel2.Text = "Num 2";
                    NumLabel3.Text = "Num 3";
                    NumLabel4.Text = "Time (Days)";
                    break;
                case ItemSubTypes.USE_ONCE:
                    switch (subSwitch)
                    {
                        case 0:
                            NumLabel0.Text = "Kind";
                            NumLabel1.Text = "Zone";
                            NumLabel2.Text = "Extra";
                            NumLabel3.Text = "Num 3";
                            NumLabel4.Text = "Num 4";
                            break;
                        case 1:
                            NumLabel0.Text = "Part 1";
                            NumLabel1.Text = "Part 2";
                            NumLabel2.Text = "Num 2";
                            NumLabel3.Text = "Num 3";
                            NumLabel4.Text = "Num 4";
                            break;
                        case 2:
                            NumLabel0.Text = "Item Type 1";
                            NumLabel1.Text = "Item Type 2";
                            NumLabel2.Text = "Num 2";
                            NumLabel3.Text = "Num 3";
                            NumLabel4.Text = "Num 4";
                            break;
                        case 3:
                            NumLabel0.Text = "Kind";
                            NumLabel1.Text = "Num 1";
                            NumLabel2.Text = "Num 2";
                            NumLabel3.Text = "Num 3";
                            NumLabel4.Text = "Num 4";
                            break;
                        default:
                            NumLabel0.Text = "Num 0";
                            NumLabel1.Text = "Num 1";
                            NumLabel2.Text = "Num 2";
                            NumLabel3.Text = "Num 3";
                            NumLabel4.Text = "Num 4";
                            break;
                    }
                    break;
                case ItemSubTypes.BULLET:
                    NumLabel0.Text = "Num 0";
                    NumLabel1.Text = "Num 1";
                    NumLabel2.Text = "Num 2";
                    NumLabel3.Text = "Num 3";
                    NumLabel4.Text = "Num 4";
                    break;
                case ItemSubTypes.ETC:
                    if (subSwitch != 7)
                    {
                        NumLabel0.Text = "Num 0";
                        NumLabel1.Text = "Num 1";
                        NumLabel2.Text = "Num 2";
                        NumLabel3.Text = "Num 3";
                        NumLabel4.Text = "Num 4";
                    }
                    else
                    {
                        NumLabel0.Text = "Type 1";
                        NumLabel1.Text = "Type 2";
                        NumLabel2.Text = "Num 2";
                        NumLabel3.Text = "Num 3";
                        NumLabel4.Text = "Num 4";
                    }
                    break;
                case ItemSubTypes.ACCESSORY:
                    NumLabel0.Text = "Num 0";
                    NumLabel1.Text = "Num 1";
                    NumLabel2.Text = "Num 2";
                    NumLabel3.Text = "Num 3";
                    NumLabel4.Text = "Num 4";
                    break;
                case ItemSubTypes.POTION:
                    NumLabel0.Text = "Skill Num";
                    NumLabel1.Text = "Skill Lv";
                    NumLabel2.Text = "Rating";
                    NumLabel3.Text = "Num 3";
                    NumLabel4.Text = "Num 4";
                    if (subSwitch == 5 || subSwitch == 6)
                    {
                        NumLabel1.Text = "Num 1";
                        NumLabel2.Text = "Num 2";
                    }
                    break;
            }
        }

        private void OnItemRevert(object sender, EventArgs e)
        {
            if (SelectedId != -1)
                DoFillWindow();
        }

        private void OnCreateNew(object sender, EventArgs e)
        {
            Item itm = new Item();

            Deserialize<Item> itd = new Deserialize<Item>("t_item");

            items.Sort((x, y) => x.a_index.CompareTo(y.a_index));

            itd.SetData(itm);

            itd.SetValue("a_index", items.Last().a_index + 1);
            itd.SetValue(LocalNameString, "New Item");
            itd.SetValue(LocalNameString, "New Item");
            itd.SetValue("a_file_smc", "Data/Item/Common/ITEM_treasure02.smc");
            itd.SetValue("a_max_use", -1);
            itd.SetValue("a_level2", 999);

            for (int i = 0; i < 10; i++)
            {
                itd.SetValue(String.Format("a_rare_index_{0}", i), -1);
                itd.SetValue(String.Format("a_need_item{0}", i), -1);
            }

            itm = itd.Serialize();

            itm.a_job_flag = 511;
            itm.a_type_idx = 0;
            itm.a_subtype_idx = 0;

            items.Add(itm);

            ItemList.Items.Add(String.Format("{0} - {1}", itm.a_index, itd[LocalNameString]));

            ItemList.SelectedIndex = DoSelectItem(itm.a_index);

            BackgroundWorker singleQuery = new BackgroundWorker();
            singleQuery.DoWork += OnSingleQuery;
            singleQuery.RunWorkerCompleted += OnQueryComplete;
            singleQuery.RunWorkerAsync(QUERY_TYPE.INSERT);
        }

        private void OnCreateCopy(object sender, EventArgs e)
        {
            if (SelectedId == -1)
                return;

            int itemToCopy = items.FindIndex(p => p.a_index == SelectedId);

            if (itemToCopy == -1)
                return;

            Deserialize<Item> dStruct = new Deserialize<Item>("t_item");
            dStruct.SetData(items[itemToCopy]);

            dStruct["a_index"] = items.Last().a_index + 1;

            Item itm = dStruct.Serialize();

            items.Add(itm);

            ItemList.Items.Add(String.Format("{0} - {1}", itm.a_index, dStruct[LocalNameString]));

            ItemList.SelectedIndex = DoSelectItem(itm.a_index);

            BackgroundWorker singleQuery = new BackgroundWorker();
            singleQuery.DoWork += OnSingleQuery;
            singleQuery.RunWorkerCompleted += OnQueryComplete;
            singleQuery.RunWorkerAsync(QUERY_TYPE.INSERT);
        }

        private void OnUpdateItem(object sender, EventArgs e)
        {
            bool ListChanged = false;

            // Get index of selectd item
            int idx = items.FindIndex(p => p.a_index == SelectedId);

            itemDesc.SetData(items[idx]);

            int parsedID = int.Parse(tbIndex.Text);

            // if the current value id is not the selected id
            if (parsedID != items[idx].a_index)
            {
                // Does the new ID exist?
                if (items.FindIndex(p => p.a_index == int.Parse(tbIndex.Text)) != -1)
                {
                    MessageBox.Show(String.Format("Selected Index {0} Already In Use.", "Error", int.Parse(tbIndex.Text), MessageBoxButtons.OK));
                    return;
                }
                else
                {
                    itemDesc.SetWhere(items[idx].a_index);
                    itemDesc.SetKey("a_index");

                    itemDesc.SetValue("a_index", parsedID);
                    items[idx].a_index = parsedID;

                    new Transactions<Item>(DataCon).ExecuteQuery(itemDesc, QUERY_TYPE.UPDATE);

                    SelectedId = int.Parse(tbIndex.Text);

                    items[idx].a_index = SelectedId;

                    ListChanged = true;
                }
            }

            if(itemDesc[LocalNameString].ToString() != tbName_USA.Text)
            {
                itemDesc.SetWhere(items[idx].a_index);
                itemDesc.SetKey("a_index");

                itemDesc.SetValue(LocalNameString, tbName_USA.Text);

                items[idx] = itemDesc.Serialize();

                new Transactions<Item>(DataCon).ExecuteQuery(itemDesc, QUERY_TYPE.UPDATE);

                ListChanged = true;
            }

            itemDesc.SetValue("a_enable", cb_enabled.Checked ? 1 : 0);
            itemDesc.SetValue("a_job_flag", int.Parse(tbJob_Flag.Text));
            itemDesc.SetValue("a_wearing", cbWearType.SelectedIndex == 0 ? -1 : cbWearType.SelectedIndex - 1);

            itemDesc.SetValue("a_type_idx", cbType.SelectedIndex);
            itemDesc.SetValue("a_subtype_idx", cbSubType.SelectedIndex);
            itemDesc.SetValue("a_castle_war", cb_castle_war.Checked ? 1 : 0);

            String[] iconShit = ((String)pbIcon.Tag).Split(' ');

            itemDesc.SetValue("a_texture_id", int.Parse(iconShit[0]));
            itemDesc.SetValue("a_texture_row", int.Parse(iconShit[1]));
            itemDesc.SetValue("a_texture_col", int.Parse(iconShit[2]));

            List<ComboBox> ctrls = new List<ComboBox>();

            ctrls.Add(cbRareIndex0);
            ctrls.Add(cbRareIndex1);
            ctrls.Add(cbRareIndex2);
            ctrls.Add(cbRareIndex3);
            ctrls.Add(cbRareIndex4);
            ctrls.Add(cbRareIndex5);
            ctrls.Add(cbRareIndex6);
            ctrls.Add(cbRareIndex7);
            ctrls.Add(cbRareIndex8);
            ctrls.Add(cbRareIndex9);

            for (int i = 0; i < ctrls.Count; i++)
            {
                String rareOpt = String.Format("a_rare_index_{0}", i.ToString());

                if (ctrls[i].SelectedIndex != -1)
                {
                    itemDesc.SetValue(rareOpt, int.Parse(ctrls[i].SelectedItem.ToString().Split(new char[] { '-' })[0]));
                }
                else
                {
                    // Attempt to validate the string
                    if (ctrls[i].Text != String.Empty)
                    {
                        int res = -1;
                        bool parsed = int.TryParse(ctrls[i].Text, out res);

                        if(parsed)
                        { 
                            int ires = SearchComboBox(ctrls[i], res);

                            // If this object does not already exists in the combo box
                            if (ires == -1)
                            {
                                int foundidx = items.FindIndex(p => p.a_index == res);

                                if (foundidx != -1)
                                {
                                    // Get the name of the other part item
                                    Deserialize<Item> otherItem = new Deserialize<Item>("t_item");
                                    otherItem.SetData(items[foundidx]);

                                    // Create the new row item
                                    string insertItem = String.Format("{0} - {1}", items[foundidx].a_index, otherItem[LocalNameString].ToString());

                                    // Lets not let them do this for rare option items
                                    if ((items[foundidx].a_flag & 0x8000) > 0)
                                    {
                                        ctrls[i].Text = String.Empty;
                                        itemDesc.SetValue(rareOpt, -1);

                                        continue;
                                    }
                                    else
                                    {
                                        #region UPDATE_STANDARD_OPTS_LISTS
                                        switch (i)
                                        {
                                            case 0:
                                                standardOpt0.Add(insertItem);
                                                break;
                                            case 1:
                                                standardOpt1.Add(insertItem);
                                                break;
                                            case 2:
                                                standardOpt2.Add(insertItem);
                                                break;
                                            case 3:
                                                standardOpt3.Add(insertItem);
                                                break;
                                            case 4:
                                                standardOpt4.Add(insertItem);
                                                break;
                                            case 5:
                                                standardOpt5.Add(insertItem);
                                                break;
                                            case 6:
                                                standardOpt6.Add(insertItem);
                                                break;
                                            case 7:
                                                standardOpt7.Add(insertItem);
                                                break;
                                            case 8:
                                                standardOpt8.Add(insertItem);
                                                break;
                                            case 9:
                                                standardOpt9.Add(insertItem);
                                                break;
                                        }
                                        #endregion
                                    }

                                    // Add the new item to the control
                                    int anewItem = ctrls[i].Items.Add(insertItem);
                                    ctrls[i].SelectedIndex = anewItem;

                                    itemDesc.SetValue(rareOpt, int.Parse(ctrls[i].SelectedItem.ToString().Split(new char[] { '-' })[0]));
                                }
                                else // Could not find the item this index points to
                                {
                                    ctrls[i].Text = String.Empty;
                                    itemDesc.SetValue(rareOpt, -1);
                                }
                            }
                            else // Parsed == True but we already have this index
                            {
                                ctrls[i].SelectedIndex = ires;
                                itemDesc.SetValue(rareOpt, int.Parse(ctrls[i].SelectedItem.ToString().Split(new char[] { '-' })[0]));
                            }
                        }
                        else // Prased == False
                        {
                            ctrls[i].Text = String.Empty;
                            itemDesc.SetValue(rareOpt, -1);
                        }
                    }
                    else // Text was Empty
                    {
                        itemDesc.SetValue(rareOpt, -1);
                    }
                }
            }

            List<Control> textBoxes = ControlUtil.GetAll(this.tabControl1, typeof(TextBox)).ToList();

            foreach (TextBox a in textBoxes)
            {
                String realName = a.Name.Replace("tb", "");

                int anIndex = itemDesc.nameList.FindIndex(p => p.ToLower().Equals("a_" + realName.ToLower()));

                if (anIndex != -1)
                {
                    itemDesc[anIndex] = Convert.ChangeType(a.Text, itemDesc.typeList[anIndex]);
                }
            }

            // Finalize the class and return
            items[idx] = itemDesc.Serialize();

            SelectedId = items[idx].a_index;

            if(ListChanged)
                DoRebuildList();
        }

        private void OnClearName(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tbName_USA.Text = String.Empty;
        }

        private void OnFortuneAdd(object sender, EventArgs e)
        {
            FortuneDataEntry fde = new FortuneDataEntry();

            if (fde.ShowDialog() == DialogResult.OK)
            {
                int idx = fortData.FindIndex(p => p.a_item_idx == SelectedId && p.a_skill_index == fde.SkillIdx);

                if (idx != -1)
                    return;

                fortuneGrid.Rows.Add(new object[] { fde.SkillIdx, fde.SkillLv, fde.StrId, fde.Prob });

                ItemFortuneLOD ifl = new ItemFortuneLOD();

                ifl.a_item_idx = SelectedId;
                ifl.a_skill_index = fde.SkillIdx;
                ifl.a_skill_level = fde.SkillLv;
                ifl.a_string_index = fde.StrId;
                ifl.a_prob = fde.Prob;

                fortData.Add(ifl);

                Deserialize<ItemFortuneLOD> fortDesc = new Deserialize<ItemFortuneLOD>("t_fortune_data");

                fortDesc.SetData(ifl);

                new Transactions<ItemFortuneLOD>(this.DataCon).ExecuteQuery(fortDesc, QUERY_TYPE.INSERT);

                btnShopDelete.Enabled = true;
            }
        }

        private void OnFortuneDelete(object sender, EventArgs e)
        {
            if (fortuneGrid.SelectedRows == null)
                return;

            int selectedIndex = -1;

            for (int i = 0; i < fortuneGrid.Rows.Count; i++)
            {
                if (fortuneGrid.Rows[i].Selected)
                {
                    selectedIndex = i;
                    break;
                }
            }

            if (selectedIndex == -1)
                return;

            DataGridViewRow dgv = fortuneGrid.Rows[selectedIndex];

            int idx = fortData.FindIndex(p => p.a_item_idx == SelectedId && p.a_skill_index == int.Parse(dgv.Cells[0].Value.ToString()));

            if (idx != -1)
            {
                Deserialize<ItemFortuneLOD> fortDesc = new Deserialize<ItemFortuneLOD>("t_fortune_data");
                fortDesc.SetData(fortData[idx]);
                fortDesc.SetKey("a_item_idx");

                fortDesc.SetWhere(String.Format("{0} AND a_skill_index = {1}",
                    fortData[idx].a_item_idx, fortData[idx].a_skill_index));

                new Transactions<ItemFortuneLOD>(this.DataCon).ExecuteQuery(fortDesc, QUERY_TYPE.DELETE);

                fortData.RemoveAt(idx);
                fortuneGrid.Rows.RemoveAt(selectedIndex);
            }
        }

        // Query Callbacks
        private void OnAllQuery(object sender, DoWorkEventArgs e)
        {
            this.BeginInvoke(LoadState, true);

            // Create a structure and set the table
            Deserialize<Item> npcDoc = new Deserialize<Item>("t_item");

            // Set a key this is important for editing reasons
            npcDoc.SetKey("a_index");

            // Update all npcs in the db
            new Transactions<Item>(DataCon).ExecuteQuery(npcDoc, items, (QUERY_TYPE)e.Argument);
        }

        private void OnItemDelete(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Delete This?", "Question",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BackgroundWorker singleQuery = new BackgroundWorker();
                singleQuery.DoWork += OnSingleQuery;
                singleQuery.RunWorkerCompleted += OnQueryComplete;
                singleQuery.RunWorkerAsync(QUERY_TYPE.DELETE);
            }
        }

        private void OnSingleQuery(object sender, DoWorkEventArgs e)
        {
            if (SelectedId == -1 || items.Count <= 0)
                return;

            if (items.FindIndex(p => p.a_index == SelectedId) == -1)
                return;

            this.BeginInvoke(LoadState, true);

            // Create a structure and set the table
            Deserialize<Item> npcDoc = new Deserialize<Item>("t_item");

            // Set a key this is important for editing reasons
            npcDoc.SetKey("a_index");

            int sel = items.FindIndex(p => p.a_index.Equals(SelectedId));

            npcDoc.SetData(items[sel]);

            // Update all npcs in the db
            new Transactions<Item>(DataCon).ExecuteQuery(npcDoc, (QUERY_TYPE)e.Argument);

            if ((QUERY_TYPE)e.Argument == QUERY_TYPE.DELETE)
                items.RemoveAt(items.FindIndex(p => p.a_index == SelectedId));

            DoRebuildList();
        }

        private void OnInsertAll(object sender, EventArgs e)
        {
            BackgroundWorker allQuery = new BackgroundWorker();
            allQuery.DoWork += OnAllQuery;
            allQuery.RunWorkerCompleted += OnQueryComplete;
            allQuery.RunWorkerAsync(QUERY_TYPE.INSERT);
        }

        private void OnInsertOne(object sender, EventArgs e)
        {
            BackgroundWorker singleQuery = new BackgroundWorker();
            singleQuery.DoWork += OnSingleQuery;
            singleQuery.RunWorkerCompleted += OnQueryComplete;
            singleQuery.RunWorkerAsync(QUERY_TYPE.INSERT);
        }

        private void OnUpdateAll(object sender, EventArgs e)
        {
            BackgroundWorker allQuery = new BackgroundWorker();
            allQuery.DoWork += OnAllQuery;
            allQuery.RunWorkerCompleted += OnQueryComplete;
            allQuery.RunWorkerAsync(QUERY_TYPE.UPDATE);
        }

        private void OnUpdateOne(object sender, EventArgs e)
        {
            BackgroundWorker singleQuery = new BackgroundWorker();
            singleQuery.DoWork += OnSingleQuery;
            singleQuery.RunWorkerCompleted += OnQueryComplete;
            singleQuery.RunWorkerAsync(QUERY_TYPE.UPDATE);
        }

        private void OnQueryComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            this.BeginInvoke(LoadState, false);
        }

        // Menu Functions
        private void OnExitClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnFixItemNames(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Open Item Name File .Lod";
            ofd.Filter = "Item Name Files (*.lod)|*.lod";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ShowSpinner(true);

                ThreadStart ts = delegate ()
                {
                    BinaryReader br = new BinaryReader(File.Open(ofd.FileName, FileMode.Open, FileAccess.Read));

                    int Count = br.ReadInt32();
                    br.ReadInt32();

                    Dictionary<Int32, String> namesDict = new Dictionary<Int32, String>();
                    Dictionary<Int32, String> descDict = new Dictionary<Int32, String>();

                    for (int i = 0; i < Count; i++)
                    {
                        int anIndex = br.ReadInt32();
                        namesDict.Add(anIndex, String.Empty);
                        descDict.Add(anIndex, String.Empty);

                        // Read Item Name
                        int len = br.ReadInt32();
                        if (len > 0)
                            namesDict[anIndex] = Core.Encoder.GetString(br.ReadBytes(len));

                        // Read Item Description
                        int len2 = br.ReadInt32();
                        if (len2 > 0)
                            descDict[anIndex] = Core.Encoder.GetString(br.ReadBytes(len2));
                    }

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (!namesDict.Keys.Contains(items[i].a_index))
                            continue;

                        itemDesc.SetData(items[i]);

                        if (namesDict[items[i].a_index] != String.Empty /*&& items[i].a_name_usa == String.Empty*/)
                        {
                            itemDesc[LocalNameString] = namesDict[items[i].a_index];
                        }

                        if (descDict[items[i].a_index] != String.Empty /*&& items[i].a_descr_usa == String.Empty*/)
                        {
                            itemDesc[LocalDescString] = descDict[items[i].a_index];
                        }

                        items[i] = itemDesc.Serialize();
                    }

                    ItemCache.PreloadItems(items);
                    DoRebuildList();

                    br.Close();
                    br.Dispose();

                    Deserialize<Item> dStruct = new Deserialize<Item>("t_item");
                    dStruct.SetKey("a_index");

                    new Transactions<Item>(DataCon).ExecuteQuery(dStruct, items, QUERY_TYPE.UPDATE);

                    ShowSpinner(false);
                };

                Thread t = new Thread(ts);
                t.Start();
            }
        }

        private void OnSaveAs(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Title = "Save ItemAll.lod";
            ofd.Filter = "ItemAll lod (*.lod)|*.lod";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LastSaveFile = ofd.FileName;
                OnExportData();
            }
        }

        private void OnExportData()
        {
            if (items.Count <= 0)
                return;

            ShowSpinner(true);

            ThreadStart ts = delegate ()
            {
                List<Item> ItemFileData = items.Where(p => p.a_enable == 1).ToList();

                ItemFileData = ItemFileData.OrderBy(p => p.a_index).ToList();

                List<ItemFortuneLOD> ItemFileFortune = fortData;

                BinaryWriter bw = new BinaryWriter(File.Open(LastSaveFile, FileMode.Create));

                bw.Write(ItemFileData.Count);

                byte[] tempSmcBuffer = new byte[64];
                byte[] tempEffectBuffer = new byte[32];

                for (int i = 0; i < ItemFileData.Count; i++)
                {
                    bw.Write(ItemFileData[i].a_index);
                    bw.Write(ItemFileData[i].a_job_flag);
                    bw.Write(ItemFileData[i].a_weight);
                    bw.Write(ItemFileData[i].a_fame);
                    bw.Write(ItemFileData[i].a_level);
                    bw.Write(ItemFileData[i].a_flag);
                    bw.Write(ItemFileData[i].a_wearing);
                    bw.Write(ItemFileData[i].a_type_idx);
                    bw.Write(ItemFileData[i].a_subtype_idx);

                    bw.Write(ItemFileData[i].a_need_item0);
                    bw.Write(ItemFileData[i].a_need_item1);
                    bw.Write(ItemFileData[i].a_need_item2);
                    bw.Write(ItemFileData[i].a_need_item3);
                    bw.Write(ItemFileData[i].a_need_item4);
                    bw.Write(ItemFileData[i].a_need_item5);
                    bw.Write(ItemFileData[i].a_need_item6);
                    bw.Write(ItemFileData[i].a_need_item7);
                    bw.Write(ItemFileData[i].a_need_item8);
                    bw.Write(ItemFileData[i].a_need_item9);

                    bw.Write(ItemFileData[i].a_need_item_count0);
                    bw.Write(ItemFileData[i].a_need_item_count1);
                    bw.Write(ItemFileData[i].a_need_item_count2);
                    bw.Write(ItemFileData[i].a_need_item_count3);
                    bw.Write(ItemFileData[i].a_need_item_count4);
                    bw.Write(ItemFileData[i].a_need_item_count5);
                    bw.Write(ItemFileData[i].a_need_item_count6);
                    bw.Write(ItemFileData[i].a_need_item_count7);
                    bw.Write(ItemFileData[i].a_need_item_count8);
                    bw.Write(ItemFileData[i].a_need_item_count9);

                    bw.Write(ItemFileData[i].a_need_sskill);
                    bw.Write(ItemFileData[i].a_need_sskill_level);
                    bw.Write(ItemFileData[i].a_need_sskill2);
                    bw.Write(ItemFileData[i].a_need_sskill_level2);
                    bw.Write(ItemFileData[i].a_texture_id);
                    bw.Write(ItemFileData[i].a_texture_row);
                    bw.Write(ItemFileData[i].a_texture_col);
                    bw.Write(ItemFileData[i].a_num_0);
                    bw.Write(ItemFileData[i].a_num_1);
                    bw.Write(ItemFileData[i].a_num_2);
                    bw.Write(ItemFileData[i].a_num_3);
                    bw.Write(ItemFileData[i].a_price);

                    bw.Write(ItemFileData[i].a_set_0);
                    bw.Write(ItemFileData[i].a_set_1);
                    bw.Write(ItemFileData[i].a_set_2);
                    bw.Write(ItemFileData[i].a_set_3);
                    bw.Write(ItemFileData[i].a_set_4);

                    /* Write Some Strings */
                    int len0 = Enc.GetByteCount(ItemFileData[i].a_file_smc);
                    int len1 = Enc.GetByteCount(ItemFileData[i].a_effect_name);
                    int len2 = Enc.GetByteCount(ItemFileData[i].a_attack_effect_name);
                    int len3 = Enc.GetByteCount(ItemFileData[i].a_damage_effect_name);

                    Array.Copy(Enc.GetBytes(ItemFileData[i].a_file_smc), tempSmcBuffer, len0);
                    bw.Write(tempSmcBuffer, 0, 64);
                    Array.Clear(tempSmcBuffer, 0, 64);

                    Array.Copy(Enc.GetBytes(ItemFileData[i].a_effect_name), tempEffectBuffer, len1);
                    bw.Write(tempEffectBuffer, 0, 32);
                    Array.Clear(tempEffectBuffer, 0, 32);

                    Array.Copy(Enc.GetBytes(ItemFileData[i].a_attack_effect_name), tempEffectBuffer, len2);
                    bw.Write(tempEffectBuffer, 0, 32);
                    Array.Clear(tempEffectBuffer, 0, 32);

                    Array.Copy(Enc.GetBytes(ItemFileData[i].a_damage_effect_name), tempEffectBuffer, len3);
                    bw.Write(tempEffectBuffer, 0, 32);
                    Array.Clear(tempEffectBuffer, 0, 32);
                    /* End String Section */

                    bw.Write(ItemFileData[i].a_rare_index_0);
                    bw.Write(ItemFileData[i].a_rare_prob_0);

                    bw.Write(ItemFileData[i].a_rare_index_0);
                    bw.Write(ItemFileData[i].a_rare_index_1);
                    bw.Write(ItemFileData[i].a_rare_index_2);
                    bw.Write(ItemFileData[i].a_rare_index_3);
                    bw.Write(ItemFileData[i].a_rare_index_4);
                    bw.Write(ItemFileData[i].a_rare_index_5);
                    bw.Write(ItemFileData[i].a_rare_index_6);
                    bw.Write(ItemFileData[i].a_rare_index_7);
                    bw.Write(ItemFileData[i].a_rare_index_8);
                    bw.Write(ItemFileData[i].a_rare_index_9);
                    bw.Write(ItemFileData[i].a_rare_prob_0);
                    bw.Write(ItemFileData[i].a_rare_prob_1);
                    bw.Write(ItemFileData[i].a_rare_prob_2);
                    bw.Write(ItemFileData[i].a_rare_prob_3);
                    bw.Write(ItemFileData[i].a_rare_prob_4);
                    bw.Write(ItemFileData[i].a_rare_prob_5);
                    bw.Write(ItemFileData[i].a_rare_prob_6);
                    bw.Write(ItemFileData[i].a_rare_prob_7);
                    bw.Write(ItemFileData[i].a_rare_prob_8);
                    bw.Write(ItemFileData[i].a_rare_prob_9);

                    bw.Write(ItemFileData[i].a_rvr_value);
                    bw.Write(ItemFileData[i].a_rvr_grade);

                    int hasFortune = ItemFileFortune.FindIndex(p => p.a_item_idx.Equals(ItemFileData[i].a_index));

                    if (hasFortune != -1)
                    {
                        bw.Write(1);
                    }
                    else
                    {
                        bw.Write(0);
                    }

                    bw.Write((byte)ItemFileData[i].a_castle_war);
                }

                bw.Flush();
                bw.Close();
                bw.Dispose();

                ShowSpinner(false);

                MsgDialogs.Show("Export Complete", "Finished Exporting ItemAll.", "ok", MsgDialogs.MsgTypes.INFO);
            };

            new Thread(ts).Start();
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (LastSaveFile == String.Empty)
            {
                OnSaveAs(sender, e);
            }
            else
            {
                OnExportData();
            }
        }

        private void OnExportItemNames(object sender, EventArgs e)
        {
            if (items.Count <= 0)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Item Name lod (*.lod)|*.lod";
            sfd.Title = "Save Item Name lod";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ShowSpinner(true);

                ThreadStart ts = delegate ()
                {
                    BinaryWriter bw = new BinaryWriter(File.Open(sfd.FileName, FileMode.Create));

                    items = items.OrderBy(p => p.a_index).ToList();

                    // nRow
                    bw.Write(items.Count);
                    // nMax
                    bw.Write(items.Last().a_index);

                    for (int i = 0; i < items.Count; i++)
                    {
                        itemDesc.SetData(items[i]);

                        bw.Write(items[i].a_index);

                        int len = Core.Encoder.GetByteCount(itemDesc[LocalNameString].ToString());
                        bw.Write(len);

                        bw.Write(Core.Encoder.GetBytes(itemDesc[LocalNameString].ToString()));

                        int len2 = Core.Encoder.GetByteCount(itemDesc[LocalDescString].ToString());
                        bw.Write(len2);

                        bw.Write(Core.Encoder.GetBytes(itemDesc[LocalDescString].ToString()));
                    }

                    bw.Flush();
                    bw.Close();
                    bw.Dispose();

                    ShowSpinner(false);

                    MsgDialogs.Show("Export Complete", "Finished Exporting ItemNames.", "ok", MsgDialogs.MsgTypes.INFO);
                };

                new Thread(ts).Start();
            }
        }

        private void OnExportSmc(object sender, EventArgs e)
        {
            if (items.Count <= 0)
                return;
        
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Item Smc lod (*.lod)|*.lod";
            sfd.Title = "Save Item Smc lod";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ShowSpinner(true);

                ThreadStart ts = delegate ()
                {
                    String rootDir = Preferences.GetData()["CLIENT"]["root"];

                    // Query all item data
                    List<Item> itemLod = items.Where(p => p.a_enable == 1).ToList();
                    itemLod.Sort((x, y) => x.a_index.CompareTo(y.a_index));

                    BinaryWriter bw = new BinaryWriter(File.Open(sfd.FileName, FileMode.Create));

                    bw.Write(itemLod.Last(p => !p.a_file_smc.Equals(String.Empty)).a_index);

                    for (int o = 0; o < itemLod.Last().a_index; o++) // (ItemLOD anLod in itemLod)
                    {
                        int idx = itemLod.FindIndex(p => p.a_index.Equals(o));

                        if (idx == -1)
                        {
                            bw.Write(0);
                            continue;
                        }

                        Item anLod = itemLod[idx];

                        String fullPath = rootDir + anLod.a_file_smc;

                        if (!fullPath.Contains(".smc") && !fullPath.Contains(".bmc"))
                        {
                            fullPath = fullPath + ".smc";
                        }

                        if (!File.Exists(fullPath))
                        {
                            bw.Write(0);
                            continue;
                        }

                        fullPath = fullPath.Replace(".bmc", ".smc");

                        String SmcName = "";

                        if (FSUtil.FilePathHasInvalidChars(fullPath))
                            MsgDialogs.LogError(String.Format("Invalid File Path: {0}", fullPath));

                        // Populate the list of meshes in the smc
                        List<smcMesh> meshes = SMCReader.ReadFile(fullPath, out SmcName);

                        if (SmcName == String.Empty)
                        {
                            bw.Write(0);
                            continue;
                        }

                        bw.Write(o + 1);

                        SmcName = Regex.Replace(SmcName, @"[^\u0000-\u007F]", string.Empty);

                        bw.Write((Int16)Enc.GetByteCount(SmcName));
                        bw.Write(Enc.GetBytes(SmcName));

                        // The number of meshes this smc contains
                        bw.Write(meshes.Count);

                        for (int i = 0; i < meshes.Count; i++)
                        {
                            if (i < 0 || i > meshes.Count)
                                break;

                            bw.Write(i + 1);

                            String aMeshName = Regex.Replace(meshes[i].FileName, @"[^\u0000-\u007F]", string.Empty);

                            // Get and write mesh path length
                            Int16 bCount = (Int16)Enc.GetByteCount(aMeshName.Replace(rootDir, ""));
                            bw.Write(bCount);

                            // Write mesh path
                            bw.Write(Enc.GetBytes(aMeshName.Replace(rootDir, "")), 0, bCount);

                            // Write number of textures
                            bw.Write(meshes[i].Object.Count);

                            // Begin Texture data
                            for (int j = 0; j < meshes[i].Object.Count; j++)
                            {
                                String aTexName = Regex.Replace(meshes[i].Object[j].Name, @"[^\u0000-\u007F]", string.Empty);

                                Int16 bCount1 = (Int16)Enc.GetByteCount(aTexName);
                                bw.Write(bCount1);

                                if (bCount1 > 0)
                                    bw.Write(Enc.GetBytes(aTexName), 0, bCount1);

                                String aTexPath = Regex.Replace(meshes[i].Object[j].Texture, @"[^\u0000-\u007F]", string.Empty);

                                Int16 bCount2 = (Int16)Enc.GetByteCount(aTexPath);
                                bw.Write(bCount2);

                                if (bCount2 > 0)
                                    bw.Write(Enc.GetBytes(aTexPath), 0, bCount2);
                            }
                        }
                    }

                    bw.Flush();
                    bw.Close();
                    bw.Dispose();

                    ShowSpinner(false);

                    MsgDialogs.Show("Export Complete", "Finished Exporting ItemSmc.", "ok", MsgDialogs.MsgTypes.INFO);
                };
                new Thread(ts).Start();
            }
        }

        private void OnExportSmcChecked(object sender, EventArgs e)
        {
            if (items.Count <= 0)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Item Smc lod (*.lod)|*.lod";
            sfd.Title = "Save Item Smc lod";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ShowSpinner(true);

                ThreadStart ts = delegate ()
                {
                    String rootDir = Preferences.GetData()["CLIENT"]["root"];

                    // Query all item data
                    List<Item> itemLod = items.Where(p => p.a_enable == 1).ToList();
                    itemLod.Sort((x, y) => x.a_index.CompareTo(y.a_index));

                    BinaryWriter bw = new BinaryWriter(File.Open(sfd.FileName, FileMode.Create));

                    bw.Write(itemLod.Last(p => !p.a_file_smc.Equals(String.Empty)).a_index);

                    for (int o = 0; o < itemLod.Last().a_index; o++) // (ItemLOD anLod in itemLod)
                    {
                        int idx = itemLod.FindIndex(p => p.a_index.Equals(o));

                        if (idx == -1)
                        {
                            bw.Write(0);
                            continue;
                        }

                        Item anLod = itemLod[idx];

                        String fullPath = rootDir + anLod.a_file_smc;

                        if (!fullPath.Contains(".smc") && !fullPath.Contains(".bmc"))
                        {
                            fullPath = fullPath + ".smc";
                        }

                        if (!File.Exists(fullPath))
                        {
                            bw.Write(0);
                            continue;
                        }

                        fullPath = fullPath.Replace(".bmc", ".smc");

                        String SmcName = "";

                        if (FSUtil.FilePathHasInvalidChars(fullPath))
                            MsgDialogs.LogError(String.Format("Invalid File Path: {0}", fullPath));

                        // Populate the list of meshes in the smc
                        List<smcMesh> meshes = SMCReader.ReadFileChecked(rootDir, fullPath, out SmcName);

                        if (SmcName == String.Empty)
                        {
                            bw.Write(0);
                            continue;
                        }

                        bw.Write(o + 1);

                        SmcName = Regex.Replace(SmcName, @"[^\u0000-\u007F]", string.Empty);

                        bw.Write((Int16)Enc.GetByteCount(SmcName));
                        bw.Write(Enc.GetBytes(SmcName));

                        // The number of meshes this smc contains
                        bw.Write(meshes.Count);

                        for (int i = 0; i < meshes.Count; i++)
                        {
                            if (i < 0 || i > meshes.Count)
                                break;

                            bw.Write(i + 1);

                            String aMeshName = Regex.Replace(meshes[i].FileName, @"[^\u0000-\u007F]", string.Empty);

                            // Get and write mesh path length
                            Int16 bCount = (Int16)Enc.GetByteCount(aMeshName.Replace(rootDir, ""));
                            bw.Write(bCount);

                            // Write mesh path
                            bw.Write(Enc.GetBytes(aMeshName.Replace(rootDir, "")), 0, bCount);

                            // Write number of textures
                            bw.Write(meshes[i].Object.Count);

                            // Begin Texture data
                            for (int j = 0; j < meshes[i].Object.Count; j++)
                            {
                                String aTexName = Regex.Replace(meshes[i].Object[j].Name, @"[^\u0000-\u007F]", string.Empty);

                                Int16 bCount1 = (Int16)Enc.GetByteCount(aTexName);
                                bw.Write(bCount1);

                                if (bCount1 > 0)
                                    bw.Write(Enc.GetBytes(aTexName), 0, bCount1);

                                if (meshes[i].Object[j].Texture == null)
                                    meshes[i].Object[j].Texture = String.Empty;

                                String aTexPath = Regex.Replace(meshes[i].Object[j].Texture, @"[^\u0000-\u007F]", string.Empty);

                                Int16 bCount2 = (Int16)Enc.GetByteCount(aTexPath);
                                bw.Write(bCount2);

                                if (bCount2 > 0)
                                    bw.Write(Enc.GetBytes(aTexPath), 0, bCount2);
                            }
                        }
                    }

                    bw.Flush();
                    bw.Close();
                    bw.Dispose();

                    ShowSpinner(false);

                    MsgDialogs.Show("Export Complete", "Finished Exporting ItemSmc.", "ok", MsgDialogs.MsgTypes.INFO);
                };
                new Thread(ts).Start();
            }
        }

        private void OnExportFortune(object sender, EventArgs e)
        {
            if (items.Count <= 0)
                return;

            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Title = "Save ItemFortune.lod";
            ofd.Filter = "ItemFortune lod (*.lod)|*.lod";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ShowSpinner(true);

                ThreadStart ts = delegate ()
                {
                    List<ItemFortuneLOD> ItemFileFortune = fortData;

                    BinaryWriter bw = new BinaryWriter(File.Open(ofd.FileName, FileMode.Create));

                    bw.Write(ItemFileFortune.Count);

                    for (int i = 0; i < ItemFileFortune.Count; i++)
                    {
                        bw.Write(ItemFileFortune[i].a_skill_index);
                        bw.Write(ItemFileFortune[i].a_skill_level);
                        bw.Write(ItemFileFortune[i].a_string_index);
                        bw.Write(ItemFileFortune[i].a_prob);
                    }

                    bw.Flush();
                    bw.Close();
                    bw.Dispose();

                    ShowSpinner(false);

                    MsgDialogs.Show("Export Complete", "Finished Exporting ItemFortune.", "ok", MsgDialogs.MsgTypes.INFO);
                };

                new Thread(ts).Start();
            }
        }

        private void OnIconFileIDChanged(object sender, EventArgs e)
        {
            int result = 0;

            bool valid = int.TryParse(((TextBox)sender).Text, out result);

            if (valid)
            {
                int idx = items.FindIndex(p => p.a_index == SelectedId);

                if (idx != -1)
                {
                    items[idx].a_texture_id = result;

                    pbIcon.Image = IconCache.GetItemIcon(items[idx].a_texture_id, items[idx].a_texture_row, items[idx].a_texture_col) as Image;

                    pbIcon.Tag = String.Format("{0} {1} {2}", items[idx].a_texture_id, items[idx].a_texture_row, items[idx].a_texture_col);
                }
            }
        }

        private void OnIconRowChanged(object sender, EventArgs e)
        {
            int result = 0;

            bool valid = int.TryParse(((TextBox)sender).Text, out result);

            if (valid)
            {
                int idx = items.FindIndex(p => p.a_index == SelectedId);

                if (idx != -1)
                {
                    items[idx].a_texture_row = result;

                    pbIcon.Image = IconCache.GetItemIcon(items[idx].a_texture_id, items[idx].a_texture_row, items[idx].a_texture_col) as Image;

                    pbIcon.Tag = String.Format("{0} {1} {2}", items[idx].a_texture_id, items[idx].a_texture_row, items[idx].a_texture_col);
                }
            }
        }

        private void OnIconColChanged(object sender, EventArgs e)
        {
            int result = 0;

            bool valid = int.TryParse(((TextBox)sender).Text, out result);

            if (valid)
            {
                int idx = items.FindIndex(p => p.a_index == SelectedId);

                if (idx != -1)
                {
                    items[idx].a_texture_col = result;

                    pbIcon.Image = IconCache.GetItemIcon(items[idx].a_texture_id, items[idx].a_texture_row, items[idx].a_texture_col) as Image;

                    pbIcon.Tag = String.Format("{0} {1} {2}", items[idx].a_texture_id, items[idx].a_texture_row, items[idx].a_texture_col);
                }
            }
        }

        private void OnBuildJobFlag_Click(object sender, EventArgs e)
        {
            if (tbJob_Flag.Text == String.Empty)
                tbJob_Flag.Text = "0";

            JobBuilder fb2 = new JobBuilder(int.Parse(tbJob_Flag.Text));

            if (fb2.ShowDialog() == DialogResult.OK)
            {
                tbJob_Flag.Text = fb2.GetFlag();
            }
        }

        private void DoRipItemsMenu(object sender, EventArgs e)
        {
            if (items.Count == 0)
                return;

            ItemRipper ir = new ItemRipper();

            if(ir.ShowDialog() == DialogResult.OK)
            {
                List<Item> newItems = ir.GetItems();

                foreach(Item it in newItems)
                {
                    it.a_index = items.Max(p => p.a_index) + 1;

                    items.Add(it);

                    itemDesc.SetData(it);

                    new Transactions<Item>(DataCon).ExecuteQuery(itemDesc, QUERY_TYPE.INSERT);
                }

                DoRebuildList();
            }
        }
    }
}
