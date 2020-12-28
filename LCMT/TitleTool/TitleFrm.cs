using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IllTechLibrary;
using IllTechLibrary.Dialogs;
using IllTechLibrary.Settings;
using IllTechLibrary.SharedStructs;
using IllTechLibrary.Serialization;
using System.IO;
using System.Threading;
using IllTechLibrary.Util;
using System.Globalization;
using System.Diagnostics;
using IllTechLibrary.DataFiles;

namespace LCMT.TitleTool
{
    public partial class TitleFrm : LCToolFrm
    {
        public List<Title> titles = new List<Title>();
        public List<Option> basic_seals = new List<Option>();
        public List<RareOption> advance_seals = new List<RareOption>();
        public List<Item> items = new List<Item>();

        private Deserialize<Item> itmDes = new Deserialize<Item>("t_item");
        private Deserialize<Title> tiDes = new Deserialize<Title>("t_title");
        private Deserialize<Option> optDes = new Deserialize<Option>("t_option");
        private Deserialize<RareOption> advDes = new Deserialize<RareOption>("t_rareoption");

        private String lastSaveFile = String.Empty;

        private Encoding Enc = Encoding.ASCII;

        private delegate void LoadStateChanger(bool state);

        // Tool ID Tag
        public static String TitleToolID = "TITLE_TOOL";

        private String LocalNameString
        {
            get { return String.Format("a_name_{0}", Core.LangCode); }
        }

        private static String LocalPrefixString
        {
            get { return String.Format("a_prefix_{0}", Core.LangCode); }
        }

        public TitleFrm() : base(TitleToolID)
        {
            InitializeComponent();
        }

        private void OnExit(object sender, EventArgs e)
        {
            this.Close();
        }

        public override void OnConnect()
        {
            tiDes = new Deserialize<Title>("t_title");
            itmDes = new Deserialize<Item>("t_item");
            optDes = new Deserialize<Option>("t_option");
            advDes = new Deserialize<RareOption>("t_rareoption");

            AddTask(DoLoadAll);
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

        private void DoLoadAll()
        {
            ShowSpinner(true);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            TaskBlock(delegate
            {
                titles = new Transactions<Title>(this.DataCon).ExecuteQuery(tiDes);
                titles = titles.OrderBy(p => p.a_index).ToList();
            });

            TaskBlock(delegate
            {
                basic_seals = new Transactions<Option>(this.DataCon).ExecuteQuery(optDes);
            });

            TaskBlock(delegate
            {
                advance_seals = new Transactions<RareOption>(this.DataCon).ExecuteQuery(advDes);
            });

            TaskBlock(delegate
            {
                items = new Transactions<Item>(this.DataCon).ExecuteQuery(itmDes);
                ItemCache.PreloadItems(items);
            });

            TimeSpan time = sw.Elapsed;
            sw.Stop();

            loadAllTime = time.TotalMilliseconds;

            Invoke((MethodInvoker)delegate
            {
                BuildLists();
            });

            ShowSpinner(false);
        }

        double loadAllTime;

        private void BuildLists()
        {
            TitlesList.Items.Clear();

            for(int i = 0; i < titles.Count; i++)
            {
                Item obj = items.Find(p => p.a_index.Equals(titles[i].a_item_index));

                if (obj != null)
                    itmDes.SetData(obj);

                TitlesList.Items.Add(String.Format("{0} - {1}", titles[i].a_index, obj == null ? "" : itmDes[LocalNameString].ToString()));
            }

            OptionsList.Items.Clear();

            for(int i = 0; i < basic_seals.Count; i++)
            {
                optDes.SetData(basic_seals[i]);

                OptionsList.Items.Add(String.Format("{0} - {1} Power: ({2})", basic_seals[i].a_index,
                    optDes[LocalNameString].ToString(), basic_seals[i].a_level.Replace(" ", ",").Replace(",0", "").Replace("(,", "")));
            }

            for(int i = 0; i < advance_seals.Count; i++)
            {
                advDes.SetData(advance_seals[i]);

                OptionsList.Items.Add(String.Format("{0} - {1} ({2})", advance_seals[i].a_index, advDes[LocalPrefixString].ToString(), "RareOpt"));
            }

            GeneralStatsLabel.Text = $"Stats: Total Titles - {titles.Count} Enabled - {titles.Count(p => p.a_enable == 1)} Disabled - {titles.Count(p => p.a_enable == 0)}, Load Time: {loadAllTime} ms";
        }

        public override void OnDisconnect()
        {
            TitlesList.Items.Clear();
            OptionsList.Items.Clear();

            titles.Clear();
            basic_seals.Clear();
            advance_seals.Clear();
            items.Clear();
        }

        private void OnTitleTextChange(object sender, EventArgs e)
        {
            PreviewText.Text = ((TextBox)sender).Text;
        }

        private void OnForegroundBackColorChanged(object sender, EventArgs e)
        {
            PreviewText.ForeColor = ((Panel)sender).BackColor;
        }

        private void OnBackgroundBackColorChanged(object sender, EventArgs e)
        {
            PreviewText.BackColor = ((Panel)sender).BackColor;
        }

        private void OnBackgroundClicked(object sender, EventArgs e)
        {
            pickColor.Color = ((Panel)sender).BackColor;
            DialogResult dr = pickColor.ShowDialog();

            if (dr == DialogResult.OK)
            {
                BackgroundPanel.BackColor = pickColor.Color;
            }
        }

        private void OnForegroundClicked(object sender, EventArgs e)
        {
            pickColor.Color = ((Panel)sender).BackColor;
            DialogResult dr = pickColor.ShowDialog();

            if (dr == DialogResult.OK)
            {
                ForegroundPanel.BackColor = pickColor.Color;
            }
        }

        private void OnSelectedTitleChanged(object sender, EventArgs e)
        {
            if(TitlesList.SelectedIndex != -1)
            {
                Title t = titles[TitlesList.SelectedIndex];

                TB_TID.Text = t.a_index.ToString();
                TB_ITEMID.Text = t.a_item_index.ToString();
                CB_Eanbled.Checked = t.a_enable == 1 ? true : false;

                Item obj = items.Find(p => p.a_index.Equals(t.a_item_index));

                if (obj != null)
                {
                    itmDes.SetData(obj);
                    TB_TITLE.Text = (itmDes[LocalNameString] == null ? "" : itmDes[LocalNameString].ToString());
                } else
                {
                    TB_TITLE.Text = String.Empty;
                }

                if (t.a_bgcolor != String.Empty && t.a_color != String.Empty)
                {
                    uint num = uint.Parse(t.a_bgcolor, System.Globalization.NumberStyles.AllowHexSpecifier);
                    byte[] colorVals = BitConverter.GetBytes(num);

                    BackgroundPanel.BackColor = Color.FromArgb(colorVals[0], colorVals[3], colorVals[2], colorVals[1]);

                    num = uint.Parse(t.a_color, System.Globalization.NumberStyles.AllowHexSpecifier);
                    colorVals = BitConverter.GetBytes(num);

                    ForegroundPanel.BackColor = Color.FromArgb(colorVals[0], colorVals[3], colorVals[2], colorVals[1]);
                }

                EffectNameValue.Text = t.a_effect_name;
                AttackValue.Text = t.a_attack;
                DamageValue.Text = t.a_damage;

                Seal0Value.Text = MakeString(t.a_option_index0);
                Seal1Value.Text = MakeString(t.a_option_index1);
                Seal2Value.Text = MakeString(t.a_option_index2);
                Seal3Value.Text = MakeString(t.a_option_index3);
                Seal4Value.Text = MakeString(t.a_option_index4);

                Seal0Level.Text = MakeString(t.a_option_level0);
                Seal1Level.Text = MakeString(t.a_option_level1);
                Seal2Level.Text = MakeString(t.a_option_level2);
                Seal3Level.Text = MakeString(t.a_option_level3);
                Seal4Level.Text = MakeString(t.a_option_level4);

                UseTimeValue.Text = MakeString(t.a_time);
                FlagsValue.Text = MakeString(t.a_flag);
                CastleValue.SelectedIndex = GetCastleIndex(t.a_castle_num);
            }
        }

        private int GetCastleIndex(uint zone)
        {
            int index = 0;

            switch (zone)
            {
                case 0:
                    index = 0;
                    break;
                case 4:
                    index = 1;
                    break;
                case 7:
                    index = 2;
                    break;
            }

            return index;
        }

        private String MakeString(uint val)
        {
            return val.ToString();
        }

        private String MakeString(int val)
        {
            return val.ToString();
        }

        private void OnPickItemClicked(object sender, EventArgs e)
        {
            if (!this.DataCon.IsConnected())
                return;

            int CurrentID = int.Parse(TB_ITEMID.Text);

            ItemSelector t = ItemSelector.Instance();//new ItemSelector(CurrentID);

            if (t.Show(this, CurrentID) == DialogResult.OK)
            {
                int idx = TitlesList.SelectedIndex;

                if(idx != -1)
                {
                    int TitleIndex = int.Parse(TitlesList.Items[idx].ToString().Split('-')[0]);

                    int TheTitle = titles.FindIndex(p => p.a_index.Equals(TitleIndex));

                    TB_ITEMID.Text = items[t.SelectedIndex].a_index.ToString();
                }
            }
        }

        #region SEARCH_REGION

        private void TitleSearchText_TextChanged(object sender, EventArgs e)
        {
            BackgroundWorker searchWorker = new BackgroundWorker();

            searchWorker.DoWork += SearchWorkerTitle_DoWork;

            searchWorker.RunWorkerAsync();
        }

        delegate void Del(int x);

        private void SearchWorkerTitle_DoWork(object sender, DoWorkEventArgs e)
        {
            for(int i = 0; i < TitlesList.Items.Count; i++)
            {
                if(TitlesList.Items[i].ToString().ToLower().Contains(TitleSearchText.Text.ToLower()))
                {
                    Del d = delegate (int b) { TitlesList.SelectedIndex = b; };

                    this.Invoke(d, i);

                    goto WORK_DONE;
                }
            }

            WORK_DONE:
            return;
        }

        private void OptionSearchText_TextChanged(object sender, EventArgs e)
        {
            BackgroundWorker searchWorker = new BackgroundWorker();

            searchWorker.DoWork += SearchWorkerOpt_DoWork;

            searchWorker.RunWorkerAsync();
        }

        private void SearchWorkerOpt_DoWork(object sender, DoWorkEventArgs e)
        {
            for(int i = 0; i < OptionsList.Items.Count; i++)
            {
                if(OptionsList.Items[i].ToString().ToLower().Contains(OptionSearchText.Text.ToLower()))
                {
                    Del d = delegate (int b) { OptionsList.SelectedIndex = b; };

                    this.Invoke(d, i);

                    goto WORK_DONE;
                }
            }

            WORK_DONE:
            return;
        }

        #endregion

        private String lastEffectFile = String.Empty;

        private void OnEffectClick(object sender, EventArgs e)
        {
            if (!this.DataCon.IsConnected())
                return;

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
            String effectName = ap.GetName();

            if (effectName == String.Empty)
                return;

            EffectNameValue.Text = effectName;
        }

        private String MakeHexString(Color col)
        {
            String val = String.Empty;

            int TheColor = col.ToArgb();

            byte[] TheBytes = BitConverter.GetBytes(TheColor);

            val = String.Format("{0}{1}{2}{3}", TheBytes[2].ToString("X2"), TheBytes[1].ToString("X2"), TheBytes[0].ToString("X2"), TheBytes[3].ToString("X2"));

            return val;
        }

        private void OnUpdateClick(object sender, EventArgs e)
        {
            if (!this.DataCon.IsConnected() || TitlesList.SelectedIndex == -1)
                return;

            int idx = TitlesList.SelectedIndex;

            // Write to Title struct
            titles[idx].a_index = int.Parse(TB_TID.Text);
            titles[idx].a_item_index = int.Parse(TB_ITEMID.Text);
            titles[idx].a_enable = CB_Eanbled.Checked == true ? 1 : 0;
            titles[idx].a_bgcolor = MakeHexString(BackgroundPanel.BackColor);
            titles[idx].a_color = MakeHexString(ForegroundPanel.BackColor);

            titles[idx].a_option_index0 = int.Parse(Seal0Value.Text);
            titles[idx].a_option_index1 = int.Parse(Seal1Value.Text);
            titles[idx].a_option_index2 = int.Parse(Seal2Value.Text);
            titles[idx].a_option_index3 = int.Parse(Seal3Value.Text);
            titles[idx].a_option_index4 = int.Parse(Seal4Value.Text);

            titles[idx].a_option_level0 = int.Parse(Seal0Level.Text);
            titles[idx].a_option_level1 = int.Parse(Seal1Level.Text);
            titles[idx].a_option_level2 = int.Parse(Seal2Level.Text);
            titles[idx].a_option_level3 = int.Parse(Seal3Level.Text);
            titles[idx].a_option_level4 = int.Parse(Seal4Level.Text);

            titles[idx].a_effect_name = EffectNameValue.Text;
            titles[idx].a_attack = AttackValue.Text;
            titles[idx].a_damage = DamageValue.Text;

            titles[idx].a_time = int.Parse(UseTimeValue.Text);
            titles[idx].a_flag = uint.Parse(FlagsValue.Text);
            titles[idx].a_castle_num = GetCastleNum(CastleValue.SelectedIndex);

            // Update Database
            tiDes.SetData(titles[idx]);
            tiDes.SetKey("a_index");
            new Transactions<Title>(DataCon).ExecuteQuery(tiDes, QUERY_TYPE.UPDATE);

            // Rebuild title lists
            BuildLists();

            // Select previously selected index
            TitlesList.SelectedIndex = idx;
        }

        private uint GetCastleNum(int index)
        {
            switch(index)
            {
                case 0:
                    return 0;
                case 1:
                    return 4;
                case 2:
                    return 7;
            }

            return 0;
        }

        private void OnNewClick(object sender, EventArgs e)
        {
            if (!this.DataCon.IsConnected())
                return;

            // Create new struct add to list refresh and select
            Title newTitle = new Title();

            newTitle.a_index = FindNextOpenIndex();

            titles.Add(newTitle);

            tiDes.SetData(newTitle);
            new Transactions<Title>(DataCon).ExecuteQuery(tiDes, QUERY_TYPE.INSERT);

            BuildLists();

            TitlesList.SelectedIndex = TitlesList.Items.Count-1;
        }

        private int FindNextOpenIndex()
        {
            int idx = titles.Max(p => p.a_index) + 1;

            return idx;
        }

        private void OnRemoveClick(object sender, EventArgs e)
        {
            if (!this.DataCon.IsConnected() || TitlesList.SelectedIndex == -1)
                return;

            int idx = TitlesList.SelectedIndex;

            tiDes.SetData(titles[idx]);
            tiDes.SetKey("a_index");
            new Transactions<Title>(DataCon).ExecuteQuery(tiDes, QUERY_TYPE.DELETE);

            titles.RemoveAt(idx);

            BuildLists();

            TitlesList.SelectedIndex = 0;
        }

        private void OnSaveAs(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Last Chaos Lod Files (*.lod)|*.lod";
            sfd.Title = "Save Title Tool Lod";
            IniParser.Model.IniData data = IllTechLibrary.Settings.Preferences.GetData();

            String rootDir = data["CLIENT"]["root"];

            if (!Directory.Exists(rootDir))
            {
                MessageBox.Show("Please Set The Path To Client Root Directory!");
                FolderBrowserDialog fbd = new FolderBrowserDialog();

                fbd.ShowDialog();

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

            sfd.InitialDirectory = rootDir;

            sfd.FileOk += OnSaveFileOk;

            sfd.ShowDialog();
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (!this.DataCon.IsConnected())
                return;

            if (lastSaveFile == String.Empty)
            {
                OnSaveAs(sender, e);
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "Last Chaos Lod Files (*.lod)|*.lod";
                sfd.Title = "Save Title Tool Lod";

                sfd.FileName = lastSaveFile;

                OnSaveFileOk(sfd, null);
            }
        }

        public uint SwapBytes(uint x)
        {
            // swap adjacent 16-bit blocks
            x = (x >> 16) | (x << 16);
            // swap adjacent 8-bit blocks
            return ((x & 0xFF00FF00) >> 8) | ((x & 0x00FF00FF) << 8);
        }

        private void OnSaveFileOk(object sender, CancelEventArgs e)
        {
            String filename = ((SaveFileDialog)sender).FileName;

            using(BinaryWriter bw = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                bw.Write(titles.Count);

                for(int i = 0; i < titles.Count; i++)
                {
                    bw.Write(titles[i].a_index);

                    byte[] effectData = new byte[64];
                    byte[] attackData = new byte[64];
                    byte[] damageData = new byte[64];

                    byte[] effectReal = Enc.GetBytes(titles[i].a_effect_name);
                    byte[] attackReal = Enc.GetBytes(titles[i].a_attack);
                    byte[] damageReal = Enc.GetBytes(titles[i].a_damage);

                    Array.Copy(effectReal, effectData, effectReal.Length);
                    Array.Copy(attackReal, attackData, attackReal.Length);
                    Array.Copy(damageReal, damageData, damageReal.Length);

                    // Enabled?
                    bw.Write((byte)titles[i].a_enable);

                    // String data
                    bw.Write(effectData);
                    bw.Write(attackData);
                    bw.Write(damageData);

                    // The color info
                    uint foreGround = ConversionClass.HexStringToUInt32(titles[i].a_color);
                    uint backGround = ConversionClass.HexStringToUInt32(titles[i].a_bgcolor);
                    bw.Write(foreGround);
                    bw.Write(backGround);

                    // Option Indecies
                    bw.Write(titles[i].a_option_index0);
                    bw.Write(titles[i].a_option_index1);
                    bw.Write(titles[i].a_option_index2);
                    bw.Write(titles[i].a_option_index3);
                    bw.Write(titles[i].a_option_index4);

                    // Option Levels
                    bw.Write((byte)titles[i].a_option_level0);
                    bw.Write((byte)titles[i].a_option_level1);
                    bw.Write((byte)titles[i].a_option_level2);
                    bw.Write((byte)titles[i].a_option_level3);
                    bw.Write((byte)titles[i].a_option_level4);

                    // The item index
                    bw.Write(titles[i].a_item_index);
                }
            }

            lastSaveFile = filename;
        }
    }
}
