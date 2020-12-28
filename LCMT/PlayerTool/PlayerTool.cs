using IllTechLibrary;
using IllTechLibrary.Controls;
using IllTechLibrary.Dialogs;
using IllTechLibrary.DataFiles;
using IllTechLibrary.Serialization;
using IllTechLibrary.Settings;
using IllTechLibrary.SharedStructs;
using IllTechLibrary.Util;
using LCMT.Dialogs;
using LCMT.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LCMT.PlayerTool
{
    public partial class PlayerFrm : LCToolFrm
    {
        public static String PlayerToolID = "PLAYER_TOOL";

        // For Data References
        private List<Item> m_items = new List<Item>();
        private List<CatalogData> m_catalog = new List<CatalogData>();
        private List<GuildData> m_guilds = new List<GuildData>();

        // Per User Data
        private List<AUser> m_users = new List<AUser>();
        private BanUserData m_banData = new BanUserData();
        private List<ACharacter> m_character = new List<ACharacter>();

        // Mall Data
        //private List<CashMallData> m_mallInv = new List<CashMallData>();
        //private List<CashMallData> m_mallHist = new List<CashMallData>();
        //private List<GiftData> m_giftInv = new List<GiftData>();
        //private List<GiftData> m_giftHist = new List<GiftData>();

        private List<FriendData> m_friends = new List<FriendData>();
        private List<IgnoreData> m_ignore = new List<IgnoreData>();

        // Per Character Data
        private List<InventoryRowData> m_charInventory = new List<InventoryRowData>();
        private List<InventoryRowData> m_charExtInventory = new List<InventoryRowData>();
        private List<WearInvenData> m_charWear = new List<WearInvenData>();
        private List<PetData> m_petData = new List<PetData>();

        private GuildMemberData m_memberData = new GuildMemberData();

        class PetObject
        {
            public PetObject(int ItemID, int PetIndex)
            {
                this.ItemID = ItemID;
                this.PetIndex = PetIndex;
            }

            public int ItemID;
            public int PetIndex;
        }

        private String LocaleNameString 
        {
            get
            {
                string code = Core.LangCode;

                if (Core.LangCode == "thai")
                    code = "tld";

                if (Core.LangCode == "usa")
                    return "a_ctname";

                return $"a_ctname_{code}";
            }
        }

        private String LocaleDescString
        {
            get
            {
                string code = Core.LangCode;

                if (Core.LangCode == "thai")
                    code = "tld";

                if (Core.LangCode == "usa")
                    return "a_ctdesc";

                return $"a_ctdesc_{code}";
            }
        }

        // Functions that check this will be delayed until manually called
        private bool DelayLoadMembers = Preferences.OptionalDelayLoad();

        public PlayerFrm() : base(PlayerToolID)
        {
            InitializeComponent();

            tb_userIndex.BackColor = SystemColors.Window;
            tb_userid.BackColor = SystemColors.Window;
            tb_index.BackColor = SystemColors.Window;

            pb_Char.SetDeleteHandler(OnDeleteEquip);
            pb_Inventory.SetDeleteHandler(OnDeleteInventoryItem);

            //StashRe.Visible = DelayLoadMembers;
            InvenRe.Visible = DelayLoadMembers;
            SocialRe.Visible = DelayLoadMembers;
        }

        private void OnDeleteInventoryItem(object sender, EventArgs e)
        {
            MenuItem c = (MenuItem)sender;

            if (c.Tag != null)
            {
                InventoryItemDesc id = (InventoryItemDesc)c.Tag;

                Deserialize<Item> itemDesc = new Deserialize<Item>("t_item");

                itemDesc.SetData(id.RowItem);

                if (MsgDialogs.ShowNoLog("Delete Confirm!",
                    $"Are you sure you wish to delete \"{itemDesc[$"a_name_{Core.LangCode}"]}\"?",
                    "YesNo", MsgDialogs.MsgTypes.WARNING) == DialogResult.Yes)
                {
                    Deserialize<InventoryRowData> invDesc = new Deserialize<InventoryRowData>($"t_inven0{m_character[lb_characters.SelectedIndex].a_index.ToString().Last()}");

                    int idx = m_charInventory.FindIndex(p => p.a_row_idx.Equals((sbyte)id.Row));

                    InventoryRowData row = m_charInventory[idx];

                    invDesc.SetData(row);

                    string idNum = id.Col.ToString();

                    invDesc["a_count" + idNum] = 0;
                    invDesc["a_flag" + idNum] = 0;
                    invDesc["a_item" + idNum + "_option0"] = (Int16)0;
                    invDesc["a_item" + idNum + "_option1"] = (Int16)0;
                    invDesc["a_item" + idNum + "_option2"] = (Int16)0;
                    invDesc["a_item" + idNum + "_option3"] = (Int16)0;
                    invDesc["a_item" + idNum + "_option4"] = (Int16)0;
                    invDesc["a_item_" + idNum + "_origin_var0"] = (Int16)0;
                    invDesc["a_item_" + idNum + "_origin_var1"] = (Int16)0;
                    invDesc["a_item_" + idNum + "_origin_var2"] = (Int16)0;
                    invDesc["a_item_" + idNum + "_origin_var3"] = (Int16)0;
                    invDesc["a_item_" + idNum + "_origin_var4"] = (Int16)0;
                    invDesc["a_item_" + idNum + "_origin_var5"] = (Int16)0;
                    invDesc["a_item_idx" + idNum] = -1;
                    invDesc["a_max_dur_" + idNum] = (ushort)0;
                    invDesc["a_now_dur_" + idNum] = (ushort)0;
                    invDesc["a_plus" + idNum] = 0;
                    invDesc["a_serial" + idNum] = String.Empty;
                    invDesc["a_socket" + idNum] = String.Empty;
                    invDesc["a_used" + idNum] = -1;
                    invDesc["a_used" + idNum + "_2"] = -1;
                    invDesc["a_wear_pos" + idNum] = (SByte)(-1);

                    invDesc.SetConditions($"where a_char_idx = {m_character[lb_characters.SelectedIndex].a_index} AND a_row_idx = {id.Row}");

                    new Transactions<InventoryRowData>(CharCon).ExecuteQuery(invDesc, QUERY_TYPE.UPDATE);

                    m_charInventory[idx] = invDesc.Serialize();

                    pb_Inventory.RemoveItem(id);
                }
            }
        }

        private void OnDeleteEquip(object sender, EventArgs e)
        {
            MenuItem c = (MenuItem)sender;

            if(c.Tag != null)
            {
                WearItemDesc wi = (WearItemDesc)c.Tag;

                Deserialize<Item> itemDesc = new Deserialize<Item>("t_item");

                itemDesc.SetData(wi.RowItem);

                if (MsgDialogs.ShowNoLog("Delete Confirm!",
                    $"Are you sure you wish to delete \"{itemDesc[$"a_name_{Core.LangCode}"]}\"?",
                    "YesNo", MsgDialogs.MsgTypes.WARNING) == DialogResult.Yes)
                {
                    Deserialize<WearInvenData> wearDesc = new Deserialize<WearInvenData>("t_wear_inven");

                    int idx = m_charWear.FindIndex(p => p.a_wear_pos.Equals(wi.WearPos));

                    wearDesc.SetData(m_charWear[idx]);
                    wearDesc.SetConditions($"where a_char_index = {m_charWear[idx].a_char_index} AND a_wear_pos = {m_charWear[idx].a_wear_pos}");

                    new Transactions<WearInvenData>(CharCon).ExecuteQuery(wearDesc, QUERY_TYPE.DELETE);

                    m_charWear.RemoveAt(idx);

                    pb_Char.RemoveItem(wi);
                }
            }
        }

        public override void OnConnect()
        {
            base.OnConnect();

            BackgroundWorker bw = new BackgroundWorker();

            bw.DoWork += DoLoadUsers;
            bw.RunWorkerCompleted += LoadUsersComplete;

            bw.RunWorkerAsync();
        }

        double loadAllTime = 0;

        public void DoLoadUsers(object sender, EventArgs e)
        {
            if (!AuthCon.IsConnected() || !CharCon.IsConnected())
                return;

            Invoke((MethodInvoker)delegate
            {
                ProgSpin1.Visible = true;
            }) ;

            Stopwatch sw = new Stopwatch();

            sw.Start();

            List<System.Threading.Tasks.Task> twait = new List<System.Threading.Tasks.Task>();

            twait.Add(AddTask(delegate()
            {
                Deserialize<AUser> decon = new Deserialize<AUser>("bg_user");
                m_users = new Transactions<AUser>(AuthCon).ExecuteQuery(decon).OrderBy(p => p.user_code).ToList();
            }));

            twait.Add(AddTask(delegate ()
            {
                Deserialize<Item> itemDesc = new Deserialize<Item>("t_item");
                m_items = new Transactions<Item>(DataCon).ExecuteQuery(itemDesc).OrderBy(p => p.a_index).ToList();
            }));

            twait.Add(AddTask(delegate ()
            {
                Deserialize<CatalogData> catDesc = new Deserialize<CatalogData>("t_catalog");
                m_catalog = new Transactions<CatalogData>(DataCon).ExecuteQuery(catDesc).OrderBy(p => p.a_ctid).ToList();
            }));

            twait.Add(AddTask(delegate ()
            {
                Deserialize<GuildData> guildDesc = new Deserialize<GuildData>("t_guild");
                m_guilds = new Transactions<GuildData>(CharCon).ExecuteQuery(guildDesc);
            }));

            twait.Add(AddTask(delegate ()
            {
                Deserialize<Option> optDesc = new Deserialize<Option>("t_option");
                List<Option> ops = new Transactions<Option>(DataCon).ExecuteQuery(optDesc);

                pb_Inventory.SetOptions(ops);
                pb_Char.SetOptions(ops);
                pb_Stash.SetOptions(ops);
                pb_guildStash.SetOptions(ops);
            }));

            twait.Add(AddTask(delegate ()
            {
                Deserialize<RareOption> rareDesc = new Deserialize<RareOption>("t_rareoption");
                List<RareOption> rops = new Transactions<RareOption>(DataCon).ExecuteQuery(rareDesc);

                pb_Inventory.SetRareOptions(rops);
                pb_Char.SetRareOptions(rops);
                pb_Stash.SetRareOptions(rops);
                pb_guildStash.SetRareOptions(rops);
            }));

            AwaitTasks(twait);

            TimeSpan time = sw.Elapsed;
            sw.Stop();

            loadAllTime = time.TotalMilliseconds;
        }

        public void LoadUsersComplete(object sender, EventArgs e)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    lb_users.Items.AddRange(m_users.Select(p => p.user_code + " - " + p.user_id).ToArray());
                    ProgSpin1.Visible = false;

                    GeneralStatsLabel.Text = $"Stats: Total Users : {m_users.Count} Activated : {m_users.Select(p => p.activated).Count()}, Load Time: {loadAllTime}";
                });
            }
            catch (Exception)
            {
                // Object disposed
            }
        }

        public override void OnDisconnect()
        {
            base.OnDisconnect();

            lb_users.SelectedIndex = -1;
            lb_characters.SelectedIndex = -1;

            m_users.Clear();
            lb_users.Items.Clear();

            m_banData = new BanUserData();

            m_items.Clear();

            m_character.Clear();
            lb_characters.Items.Clear();

            GeneralStatsLabel.Text = $"Stats: ";
        }

        private void OnUserSelectionChange(object sender, EventArgs e)
        {
            pb_Stash.Clear();
            pb_guildStash.Clear();

            lb_characters.SelectedIndex = -1;
            lb_characters.Items.Clear();

            if (lb_users.SelectedIndex != -1)
            {
                uint usercode = uint.Parse(lb_users.Items[lb_users.SelectedIndex].ToString().Split(new char[] { '-' })[0]);

                int idx = m_users.FindIndex(p => p.user_code.Equals(usercode));

                if (idx != -1)
                {
                    List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

                    // Do loading here so we can speed it up some what...
                    tasks.Add(AddTask(delegate ()
                    {
                        Deserialize<BanUserData> t_user = new Deserialize<BanUserData>("t_users");
                        t_user.SetConditions($"where a_portal_index = {m_users[idx].user_code}");

                        m_banData = new Transactions<BanUserData>(AuthCon).ExecuteQuery(t_user).FirstOrDefault();
                    }));

                    tasks.Add(AddTask(delegate ()
                    {
                        m_character.Clear();

                        Deserialize<ACharacter> chardesc = new Deserialize<ACharacter>("t_characters");
                        chardesc.SetConditions($"where a_user_index = {usercode}");
                        m_character = new Transactions<ACharacter>(CharCon).ExecuteQuery(chardesc).OrderBy(p => p.a_index).ToList();
                    }));

                    AwaitTasks(tasks);

                    tb_userIndex.Text = usercode.ToString();
                    tb_userid.Text = m_users[idx].user_id;
                    tb_password.Text = m_users[idx].passwd;
                    tb_plainpass.Text = m_users[idx].passwd_plain;
                    tb_email.Text = m_users[idx].email;
                    tb_guid.Text = m_users[idx].guid;
                    tb_cash.Text = m_users[idx].cash.ToString();

                    if (m_banData != null)
                    {
                        cb_banned.Checked = m_banData.a_enable == 0;
                    }
                    else
                    {
                        cb_banned.Checked = false;
                    }

                    cb_active.Checked = m_users[idx].activated == 1;

                    lb_characters.Items.AddRange(m_character.Select(p => p.a_index + " - " + p.a_nick == String.Empty ? p.a_name : p.a_nick).ToArray());

                    LoadMallTab(m_users[idx]);

                    if (!DelayLoadMembers)
                    {
                        // Load User Chat Logs
                        LoadChatUserData(m_users[idx]);

                        // Load Stash Stuff
                        //LoadStash(m_users[idx]);
                    }

                    // Hardware ban stuff
                    LoadHardwareBanStatus(m_users[idx]);
                }
            }
            else
            {
                m_character.Clear();
                lb_characters.Items.Clear();

                // User Basic
                tb_userIndex.ResetText();
                tb_userid.ResetText();
                tb_password.ResetText();
                tb_plainpass.ResetText();
                tb_email.ResetText();
                tb_guid.ResetText();
                tb_cash.ResetText();

                cb_active.Checked = false;
                cb_banned.Checked = false;

                dgPurchaseInv.Rows.Clear();
                dgPurchaseHistory.Rows.Clear();

                dgGiftInv.Rows.Clear();
                dgGiftHistory.Rows.Clear();

                pb_Stash.Clear();
                pb_guildStash.Clear();
            }
        }

        private void LoadChatUserData(AUser aUser)
        {
            // Redacted
        }

        private void LoadHardwareBanStatus(AUser aUser)
        {
            // Redacted
        }

        private void LoadMallTab(AUser obj)
        {
            char ext = obj.user_code.ToString().Last();

            List<System.Threading.Tasks.Task> twait = new List<System.Threading.Tasks.Task>();

            dgGiftInv.Rows.Clear();
            dgGiftHistory.Rows.Clear();
            dgPurchaseInv.Rows.Clear();
            dgPurchaseHistory.Rows.Clear();

            Deserialize<GiftData> giftDesc = new Deserialize<GiftData>("t_gift0" + ext);
            giftDesc.SetConditions($"where a_recv_user_idx = {obj.user_code}");
            Deserialize<CashMallData> mallDesc = new Deserialize<CashMallData>("t_purchase0" + ext);
            mallDesc.SetConditions($"where a_user_idx = {obj.user_code}");

            Deserialize<CatalogData> catDesc = new Deserialize<CatalogData>("t_catalog");

            List<GiftData> m_giftInv = new List<GiftData>();

            twait.Add(AddTask(delegate ()
            {
                // Current Inventory
                m_giftInv = new Transactions<GiftData>(AuthCon).ExecuteQuery(giftDesc);
            }));

            List<CashMallData> m_mallInv = new List<CashMallData>();

            // Current Purchases
            twait.Add(AddTask(delegate ()
            {
                m_mallInv = new Transactions<CashMallData>(AuthCon).ExecuteQuery(mallDesc);
            }));

            AwaitTasks(twait);

            // Gifts not used
            foreach (GiftData g in m_giftInv.Where(p => p.a_use_char_idx.Equals(0)))
            {
                CatalogData refCat = m_catalog.Find(p => p.a_ctid.Equals((uint)g.a_ctid));

                if (refCat == null)
                    continue;

                int rowIdx = 0;

                rowIdx = dgGiftInv.Rows.Add();

                catDesc.SetData(refCat);

                Item refItem = m_items.Find(p => p.a_index.Equals((int)refCat.a_icon));

                if (refItem == null)
                    refItem = m_items.Find(p => p.a_index.Equals(19));

                dgGiftInv[0, rowIdx].Value = (Image)IconCache.GetItemIcon(refItem.a_texture_id, refItem.a_texture_row, refItem.a_texture_col);
                dgGiftInv[1, rowIdx].Value = g.a_index;
                dgGiftInv[2, rowIdx].Value = catDesc[LocaleNameString];
                dgGiftInv[3, rowIdx].Value = g.a_send_char_name;
                dgGiftInv[4, rowIdx].Value = g.a_send_date;
                dgGiftInv[5, rowIdx].Value = g.a_send_msg;
            }

            // Gifts used
            foreach (GiftData g in m_giftInv.Where(p => !p.a_use_char_idx.Equals(0)))
            {
                CatalogData refCat = m_catalog.Find(p => p.a_ctid.Equals((uint)g.a_ctid));

                if (refCat == null)
                    continue;

                int rowIdx = 0;

                rowIdx = dgGiftHistory.Rows.Add();

                catDesc.SetData(refCat);

                Item refItem = m_items.Find(p => p.a_index.Equals((int)refCat.a_icon));

                if (refItem == null)
                    refItem = m_items.Find(p => p.a_index.Equals(19));

                dgGiftHistory[0, rowIdx].Value = (Image)IconCache.GetItemIcon(refItem.a_texture_id, refItem.a_texture_row, refItem.a_texture_col);
                dgGiftHistory[1, rowIdx].Value = g.a_index;
                dgGiftHistory[2, rowIdx].Value = catDesc[LocaleNameString];
                dgGiftHistory[3, rowIdx].Value = g.a_send_char_name;
                dgGiftHistory[4, rowIdx].Value = g.a_send_date;
                dgGiftHistory[5, rowIdx].Value = g.a_use_date;
                dgGiftHistory[6, rowIdx].Value = g.a_send_msg;
            }

            // Purchase Unused
            foreach (CashMallData g in m_mallInv.Where(p => p.a_use_char_idx.Equals(0)))
            {
                CatalogData refCat = m_catalog.Find(p => p.a_ctid.Equals((uint)g.a_ctid));

                if (refCat == null)
                    continue;

                int rowIdx = 0;

                rowIdx = dgPurchaseInv.Rows.Add();

                catDesc.SetData(refCat);

                Item refItem = m_items.Find(p => p.a_index.Equals((int)refCat.a_icon));

                if (refItem == null)
                    refItem = m_items.Find(p => p.a_index.Equals(19));

                dgPurchaseInv[0, rowIdx].Value = (Image)IconCache.GetItemIcon(refItem.a_texture_id, refItem.a_texture_row, refItem.a_texture_col);
                dgPurchaseInv[1, rowIdx].Value = g.a_index;
                dgPurchaseInv[2, rowIdx].Value = catDesc[LocaleNameString];
                dgPurchaseInv[3, rowIdx].Value = g.a_pdate;
                dgPurchaseInv[4, rowIdx].Value = g.a_serial;
                dgPurchaseInv[5, rowIdx].Value = g.a_ip;
            }

            // Purchase Used
            foreach (CashMallData g in m_mallInv.Where(p => !p.a_use_char_idx.Equals(0)))
            {
                CatalogData refCat = m_catalog.Find(p => p.a_ctid.Equals((uint)g.a_ctid));

                if (refCat == null)
                    continue;

                int rowIdx = 0;

                rowIdx = dgPurchaseHistory.Rows.Add();

                catDesc.SetData(refCat);

                Item refItem = m_items.Find(p => p.a_index.Equals((int)refCat.a_icon));

                if (refItem == null)
                    refItem = m_items.Find(p => p.a_index.Equals(19));

                dgPurchaseHistory[0, rowIdx].Value = (Image)IconCache.GetItemIcon(refItem.a_texture_id, refItem.a_texture_row, refItem.a_texture_col);
                dgPurchaseHistory[1, rowIdx].Value = g.a_index;
                dgPurchaseHistory[2, rowIdx].Value = catDesc[LocaleNameString];
                dgPurchaseHistory[3, rowIdx].Value = g.a_use_date;
                dgPurchaseHistory[4, rowIdx].Value = g.a_ip;
            }
        }

        private void LoadStash(AUser obj)
        {
            pb_Stash.Clear();

            // Collect stash data
            Deserialize<StashData> stashDesc = new Deserialize<StashData>($"t_stash0{obj.user_code.ToString().Last()}");
            stashDesc.SetConditions($"where a_user_idx = {obj.user_code}");
            List<StashData> stash = new Transactions<StashData>(CharCon).ExecuteQuery(stashDesc).OrderBy(p => p.a_index).ToList();

            Deserialize<StashMoney> stashMoneyDesc = new Deserialize<StashMoney>("t_stash_money");
            stashMoneyDesc.SetConditions($"where a_user_index = {obj.user_code}");

            StashMoney sm = new Transactions<StashMoney>(CharCon).ExecuteQuery(stashMoneyDesc).FirstOrDefault();

            ulong money = 0;

            if (sm != null)
                money = sm.a_stash_money;

            if(stash.Count == 0)
            {
                MsgDialogs.ShowNoLog("Stash Message", $"No items found when loading stash for user: {obj.user_id}", "ok", MsgDialogs.MsgTypes.INFO);
            }

            pb_Stash.SetItems(money, m_items, stash);
        }

        private void OnCharSelectionChange(object sender, EventArgs e)
        {
            int idx = lb_characters.SelectedIndex;

            if (idx != -1)
            {
                ACharacter obj = m_character[idx];

                tb_index.Text = obj.a_index.ToString();
                tb_name.Text = obj.a_name;
                tb_nick.Text = obj.a_nick;

                cb_enabled.Checked = obj.a_enable == 1 ? true : false;

                tb_admin.Text = obj.a_admin.ToString();

                tb_level.Text = obj.a_level.ToString();
                tb_exp.Text = obj.a_exp.ToString();
                tb_sp.Text = obj.a_skill_point.ToString();
                tb_fame.Text = obj.a_fame.ToString();

                cb_phoenix.Checked = obj.a_phoenix == 1 ? true : false;

                tb_str.Text = obj.a_statpt_str.ToString();
                tb_int.Text = obj.a_statpt_int.ToString();
                tb_con.Text = obj.a_statpt_con.ToString();
                tb_dex.Text = obj.a_statpt_dex.ToString();

                tb_stat.Text = obj.a_statpt_remain.ToString();

                tb_title.Text = obj.a_title_index.ToString();
                tb_nas.Text = obj.a_nas.ToString();
                tb_losexp.Text = obj.a_loseexp.ToString();
                tb_losesp.Text = obj.a_losesp.ToString();

                cbx_job.SelectedIndex = obj.a_job;

                LoadJob2Lists(obj.a_job);

                cbx_job2.SelectedIndex = obj.a_job2;

                cbx_subjob.SelectedIndex = (int)obj.a_subjob;

                if (!DelayLoadMembers)
                {
                    // Load Inventory Items
                    LoadInventory(obj);

                    // Get Wearing Items
                    LoadWearItems(obj);

                    // Find Pet Data
                    LoadPetData(obj);

                    // Friend Data
                    LoadFriends(obj);

                    // Ignore Data
                    LoadIgnore(obj);

                    // Player Guild Info
                    LoadGuildDate(obj);

                    // Char Hack Data
                    LoadHackUserData(obj);
                }
                else
                {
                    m_memberData = new GuildMemberData();

                    lb_friends.Items.Clear();
                    lb_ignore.Items.Clear();

                    tb_guildName.Clear();
                    tb_guildRank.Clear();

                    petListGrid.Rows.Clear();

                    pb_Char.Clear();
                    pb_Inventory.Clear();
                    pb_guildStash.Clear();
                }
            }
            else
            {
                cb_enabled.Checked = false;
                cb_phoenix.Checked = false;

                foreach(Control a in charBasicGroup.Controls)
                {
                    if(a.GetType() == typeof(TextBox))
                    {
                        ((TextBox)a).Clear();
                    }
                    else if(a.GetType() == typeof(ComboBox))
                    {
                        ((ComboBox)a).SelectedIndex = -1;
                    }
                }

                foreach (Control a in charStatGroup.Controls)
                {
                    if (a.GetType() == typeof(TextBox))
                    {
                        ((TextBox)a).Clear();
                    }
                }

                foreach (Control a in charJobGroup.Controls)
                {
                    if (a.GetType() == typeof(ComboBox))
                    {
                        ((ComboBox)a).SelectedIndex = -1;
                    }
                }

                foreach (Control a in charMiscGroup.Controls)
                {
                    if (a.GetType() == typeof(TextBox))
                    {
                        ((TextBox)a).Clear();
                    }
                }

                m_memberData = new GuildMemberData();

                lb_friends.Items.Clear();
                lb_ignore.Items.Clear();

                tb_guildName.Clear();
                tb_guildRank.Clear();

                pb_Char.BackgroundImage = Resources.titan_equip;

                petListGrid.Rows.Clear();

                pb_Char.Clear();
                pb_Inventory.Clear();
                pb_guildStash.Clear();
            }
        }

        private void OnChangeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_job.SelectedIndex != -1)
            {
                LoadJob2Lists((sbyte)cbx_job.SelectedIndex);
                cbx_job2.SelectedIndex = 0;
            }
        }

        private void LoadJob2Lists(sbyte a_job)
        {
            cbx_job2.Items.Clear();

            switch (a_job)
            {
                case CHAR_CLASSES.TITAN:
                    pb_Char.BackgroundImage = Resources.titan_equip;
                    cbx_job2.Items.AddRange(new string[] { "Titan", "War Master", "Highlander" });
                    break;
                case CHAR_CLASSES.KNIGHT:
                    pb_Char.BackgroundImage = Resources.knight_equip;
                    cbx_job2.Items.AddRange(new string[] { "Knight", "Temple Knight", "Royal Knight" });
                    break;
                case CHAR_CLASSES.HEALER:
                    pb_Char.BackgroundImage = Resources.healer_equip;
                    cbx_job2.Items.AddRange(new string[] { "Healer", "Archer", "Cleric" });
                    break;
                case CHAR_CLASSES.MAGE:
                    pb_Char.BackgroundImage = Resources.mage_equip;
                    cbx_job2.Items.AddRange(new string[] { "Mage", "Witch", "Magician" });
                    break;
                case CHAR_CLASSES.ROUGE:
                    pb_Char.BackgroundImage = Resources.rouge_equip;
                    cbx_job2.Items.AddRange(new string[] { "Rogue", "Assasin", "Ranger" });
                    break;
                case CHAR_CLASSES.SORCERER:
                    pb_Char.BackgroundImage = Resources.sorc_equip;
                    cbx_job2.Items.AddRange(new string[] { "Sorc", "Elementalist", "Specialist" });
                    break;
                case CHAR_CLASSES.NIGHTSHADOW:
                    pb_Char.BackgroundImage = Resources.nightshadow_equip;
                    cbx_job2.Items.AddRange(new string[] { "None", "Nightshadow" });
                    break;
                case CHAR_CLASSES.EX_MAGE:
                    pb_Char.BackgroundImage = Resources.mage_equip;
                    cbx_job2.Items.AddRange(new string[] { "EX-Mage", "Witch", "Magician" });
                    break;
                case CHAR_CLASSES.EX_ROUGE:
                    pb_Char.BackgroundImage = Resources.rouge_equip;
                    cbx_job2.Items.AddRange(new string[] { "EX-Rogue", "Assasin", "Ranger" });
                    break;
            }
        }

        private void LoadHackUserData(ACharacter obj)
        {
            // Redacted
        }

        private void LoadGuildDate(ACharacter obj)
        {
            pb_guildStash.Clear();

            m_memberData = new GuildMemberData();

            Deserialize<GuildMemberData> gmd = new Deserialize<GuildMemberData>("t_guildmember");

            gmd.SetConditions($"where a_char_index = {obj.a_index}");

            m_memberData = new Transactions<GuildMemberData>(CharCon).ExecuteQuery(gmd).FirstOrDefault();

            tb_guildName.Text = String.Empty;
            tb_guildRank.Text = String.Empty;

            if(m_memberData != null)
            {
                Deserialize<GuildMemberExtendData> gmed = new Deserialize<GuildMemberExtendData>("t_extend_guildmember");

                gmed.SetConditions($"where a_char_index = {obj.a_index}");

                GuildMemberExtendData extendData = new Transactions<GuildMemberExtendData>(CharCon).ExecuteQuery(gmed).FirstOrDefault();

                tb_guildName.Text = m_guilds.Find(p => p.a_index.Equals(m_memberData.a_guild_index)).a_name;

                if(extendData != null)
                    tb_guildRank.Text = extendData.a_position_name;
            }
        }

        private void LoadIgnore(ACharacter obj)
        {
            lb_ignore.Items.Clear();
            m_ignore.Clear();

            Deserialize<IgnoreData> ignoreDesc = new Deserialize<IgnoreData>("t_block_friend");
            ignoreDesc.SetConditions($"where a_char_idx = {obj.a_index}");

            m_ignore = new Transactions<IgnoreData>(CharCon).ExecuteQuery(ignoreDesc);

            if (m_ignore.Count != 0)
            {
                string[] names = m_ignore[0].a_block_name_list.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (names.Count() > 0)
                {
                    lb_ignore.Items.AddRange(names);
                }
            }
        }

        private void LoadFriends(ACharacter obj)
        {
            lb_friends.Items.Clear();
            m_friends.Clear();

            Deserialize<FriendData> friendDesc = new Deserialize<FriendData>("t_friend0" + obj.a_index.ToString().Last());

            friendDesc.SetConditions($"where a_char_index = {obj.a_index}");

            m_friends = new Transactions<FriendData>(CharCon).ExecuteQuery(friendDesc);

            if (m_friends.Count != 0)
            {
                lb_friends.Items.AddRange(m_friends.Select(p => p.a_friend_name).ToArray());
            }
        }

        private void LoadInventory(ACharacter obj)
        {
            // Clear out the inventory
            m_charInventory.Clear();

            pb_Inventory.Clear();

            // Collect Inventory Data
            Deserialize<InventoryRowData> invDesc = new Deserialize<InventoryRowData>($"t_inven0{obj.a_index.ToString().Last()}");
            invDesc.SetConditions($"where a_char_idx = {obj.a_index}");
            m_charInventory = new Transactions<InventoryRowData>(CharCon).ExecuteQuery(invDesc).OrderBy(p => p.a_tab_idx).ToList();

            // Create array to display inventory data
            List<InventoryItemDesc> tempItems = new List<InventoryItemDesc>();

            for (int i = 0; i < m_charInventory.Count; i++)
            {
                if (m_charInventory[i].a_item_idx0 != -1)
                    tempItems.Add(new InventoryItemDesc(m_items.Find(p => p.a_index.Equals(m_charInventory[i].a_item_idx0)), m_charInventory[i], 0));

                if (m_charInventory[i].a_item_idx1 != -1)
                    tempItems.Add(new InventoryItemDesc(m_items.Find(p => p.a_index.Equals(m_charInventory[i].a_item_idx1)), m_charInventory[i], 1));

                if (m_charInventory[i].a_item_idx2 != -1)
                    tempItems.Add(new InventoryItemDesc(m_items.Find(p => p.a_index.Equals(m_charInventory[i].a_item_idx2)), m_charInventory[i], 2));

                if (m_charInventory[i].a_item_idx3 != -1)
                    tempItems.Add(new InventoryItemDesc(m_items.Find(p => p.a_index.Equals(m_charInventory[i].a_item_idx3)), m_charInventory[i], 3));

                if (m_charInventory[i].a_item_idx4 != -1)
                    tempItems.Add(new InventoryItemDesc(m_items.Find(p => p.a_index.Equals(m_charInventory[i].a_item_idx4)), m_charInventory[i], 4));
            }

            // Set items to display control
            pb_Inventory.SetItems(tempItems);
        }

        private void LoadWearItems(ACharacter obj)
        {
            Deserialize<WearInvenData> wearDesc = new Deserialize<WearInvenData>("t_wear_inven");

            wearDesc.SetConditions($"where a_char_index = {obj.a_index}");

            m_charWear.Clear();

            m_charWear = new Transactions<WearInvenData>(CharCon).ExecuteQuery(wearDesc);

            List<WearItemDesc> wearItems = new List<WearItemDesc>();

            for (int i = 0; i < m_charWear.Count; i++)
            {
                wearItems.Add(new WearItemDesc(m_items.Find(p => p.a_index.Equals(m_charWear[i].a_item_idx)), m_charWear[i], m_charWear[i].a_plus, m_charWear[i].a_wear_pos));
            }

            pb_Char.SetItems(wearItems);
        }

        private void LoadPetData(ACharacter obj)
        {
            petListGrid.Rows.Clear();

            List<PetObject> objects = new List<PetObject>();

            foreach(WearInvenData r in m_charWear)
            {
                if (r.a_wear_pos == 10)
                    objects.Add(new PetObject(r.a_item_idx, r.a_plus));
            }

            foreach(InventoryRowData r in m_charInventory)
            {
                if(r.a_item_idx0 != -1)
                {
                    Item it = m_items.FirstOrDefault(p => p.a_index.Equals(r.a_item_idx0));
                    if (it != null) {
                        if (it.a_wearing == 10) {
                            objects.Add(new PetObject(it.a_index, r.a_plus0));
                        }
                    }
                }

                if (r.a_item_idx1 != -1)
                {
                    Item it = m_items.FirstOrDefault(p => p.a_index.Equals(r.a_item_idx1));
                    if (it != null)
                    {
                        if (it.a_wearing == 10)
                        {
                            objects.Add(new PetObject(it.a_index, r.a_plus1));
                        }
                    }
                }

                if (r.a_item_idx2 != -1)
                {
                    Item it = m_items.FirstOrDefault(p => p.a_index.Equals(r.a_item_idx2));
                    if (it != null)
                    {
                        if (it.a_wearing == 10)
                        {
                            objects.Add(new PetObject(it.a_index, r.a_plus2));
                        }
                    }
                }

                if (r.a_item_idx3 != -1)
                {
                    Item it = m_items.FirstOrDefault(p => p.a_index.Equals(r.a_item_idx3));
                    if (it != null)
                    {
                        if (it.a_wearing == 10)
                        {
                            objects.Add(new PetObject(it.a_index, r.a_plus3));
                        }
                    }
                }

                if (r.a_item_idx4 != -1)
                {
                    Item it = m_items.FirstOrDefault(p => p.a_index.Equals(r.a_item_idx4));
                    if (it != null)
                    {
                        if (it.a_wearing == 10)
                        {
                            objects.Add(new PetObject(it.a_index, r.a_plus4));
                        }
                    }
                }
            }

            Deserialize<PetData> petDesc = new Deserialize<PetData>("t_pet");

            petDesc.SetConditions($"where a_owner = {obj.a_index}");

            m_petData.Clear();

            m_petData = new Transactions<PetData>(CharCon).ExecuteQuery(petDesc);

            foreach(PetData apet in m_petData)
            {
                PetObject o = objects.FirstOrDefault(p => p.PetIndex.Equals(apet.a_index));

                if(o != null)
                {
                    int newRow = petListGrid.Rows.Add();

                    Item aItem = m_items.Find(p => p.a_index.Equals(o.ItemID));

                    petListGrid.Rows[newRow].Cells[0].Value = IconCache.GetItemIcon(aItem.a_texture_id,
                        aItem.a_texture_row, aItem.a_texture_col);

                    petListGrid.Rows[newRow].Cells[1].Value = o.PetIndex;

                    petListGrid.Rows[newRow].Cells[2].Value = apet.a_enable == 1 ? "True" : "False";

                    petListGrid.Rows[newRow].Cells[3].Value = apet.a_level;

                    petListGrid.Rows[newRow].Cells[4].Value = apet.a_hp;

                    petListGrid.Rows[newRow].Cells[5].Value = apet.a_hungry;

                    petListGrid.Rows[newRow].Cells[6].Value = apet.a_sympathy;

                    petListGrid.Rows[newRow].Cells[7].Value = apet.a_exp;
                }
            }
        }

        // Modify this to use the selected objects index for future proof....
        private void DoUpdateUser(object sender, EventArgs e)
        {
            // Check if we can update a user
            if (m_users.Count == 0 || lb_users.SelectedIndex == -1)
                return;

            // Get the selected object user code
            int user_code = int.Parse(lb_users.Items[lb_users.SelectedIndex].ToString().Split(new char[] { '-' })[0]);

            Deserialize<AUser> user = new Deserialize<AUser>("bg_user");

            // Get the index in m_users
            int foundIndex = m_users.FindIndex(p => p.user_code.Equals((uint)user_code));

            if (foundIndex != -1)
            {
                AUser selected = m_users[foundIndex];

                // Set changes
                selected.email = tb_email.Text;
                selected.passwd = tb_password.Text;
                selected.passwd_plain = tb_plainpass.Text;
                selected.guid = tb_guid.Text;
                selected.cash = int.Parse(tb_cash.Text);
                selected.activated = cb_active.Checked ? 1 : 0;

                // Set the serializer
                user.SetData(selected);
                user.SetKey("user_code");

                // Check if we have ban data
                if (m_banData != null)
                {
                    // If ban data does not match do update
                    if (m_banData.a_enable != (cb_banned.Checked ? 0 : 1))
                    {
                        Deserialize<BanUserData> banDesc = new Deserialize<BanUserData>("t_users");

                        m_banData.a_enable = (uint)(cb_banned.Checked ? 0 : 1);

                        banDesc.SetData(m_banData);
                        banDesc.SetKey("a_portal_index");

                        new Transactions<BanUserData>(AuthCon).ExecuteQuery(banDesc, QUERY_TYPE.UPDATE);
                    }
                }

                new Transactions<AUser>(AuthCon).ExecuteQuery(user, QUERY_TYPE.UPDATE);

                // Write the updated changes back (not sure if needed?)
                m_users[foundIndex] = selected;

                MsgDialogs.ShowNoLog("Action Complete!", $"User \"{selected.user_id}\" Has Been Updated!", "OK", MsgDialogs.MsgTypes.INFO);
            }
        }

        private void DoUpdateCharBasic(object sender, EventArgs e)
        {
            int idx = lb_characters.SelectedIndex;
            
            if(idx != -1)
            {
                ACharacter ch = m_character[idx];

                // Basic
                ch.a_name = tb_name.Text;
                ch.a_nick = tb_nick.Text;

                lb_characters.SelectedIndexChanged -= OnCharSelectionChange;
                lb_characters.Items[idx] = (ch.a_nick == "" ? ch.a_name : ch.a_nick);
                lb_characters.SelectedIndexChanged += OnCharSelectionChange;

                ch.a_enable = (sbyte)(cb_enabled.Checked == true ? 1 : 0);

                // Admin
                ch.a_admin = int.Parse(tb_admin.Text);

                // Job Info
                ch.a_job = (sbyte)cbx_job.SelectedIndex;
                ch.a_job2 = (sbyte)cbx_job2.SelectedIndex;

                ch.a_subjob = (uint)cbx_subjob.SelectedIndex;

                // Stats
                ch.a_level = int.Parse(tb_level.Text);
                ch.a_exp = long.Parse(tb_exp.Text);
                ch.a_skill_point = long.Parse(tb_sp.Text);

                ch.a_fame = int.Parse(tb_fame.Text);

                ch.a_phoenix = cb_phoenix.Checked == true ? 1 : 0;

                ch.a_statpt_str = int.Parse(tb_str.Text);
                ch.a_statpt_int = int.Parse(tb_int.Text);
                ch.a_statpt_dex = int.Parse(tb_dex.Text);
                ch.a_statpt_con = int.Parse(tb_con.Text);

                ch.a_statpt_remain = int.Parse(tb_stat.Text);

                ch.a_title_index = short.Parse(tb_title.Text);
                ch.a_nas = ulong.Parse(tb_nas.Text);

                ch.a_loseexp = ulong.Parse(tb_losexp.Text);
                ch.a_losesp = ulong.Parse(tb_losesp.Text);

                m_character[idx] = ch;

                Deserialize<ACharacter> charDesc = new Deserialize<ACharacter>("t_characters");

                charDesc.SetData(ch);
                charDesc.SetKey("a_index");

                new Transactions<ACharacter>(CharCon).ExecuteQuery(charDesc, QUERY_TYPE.UPDATE);

                MsgDialogs.ShowNoLog("Character Updated!", $"Finished Updating Character \"{ch.a_nick}\"", "OK", MsgDialogs.MsgTypes.INFO);
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OnSearchChange(object sender, EventArgs e)
        {
            lb_characters.SelectedIndex = -1;
            lb_users.SelectedIndex = -1;

            if (m_users.Count <= 0)
                return;

            if (tb_search.Text == String.Empty)
            {
                lb_users.Items.Clear();
                lb_users.Items.AddRange(m_users.Select(p => p.user_code + " - " + p.user_id).ToArray());
            }
            else
            {
                List<AUser> itCollection = new List<AUser>();

                itCollection = m_users.FindAll(p => p.user_id.ToLower().Contains(tb_search.Text.ToLower()) || p.user_code.ToString().Equals(tb_search.Text));

                if (itCollection.Count != 0)
                {
                    lb_users.Items.Clear();
                    lb_users.Items.AddRange(itCollection.Select(p => p.user_code + " - " + p.user_id).ToArray());
                }
            }
        }

        private void OnAddGift(object sender, EventArgs e)
        {
            int selectedUser = lb_users.SelectedIndex;
            int selectedChar = lb_characters.SelectedIndex;

            if (selectedUser != -1 && selectedChar != -1)
            {
                try
                {
                    int selectedID = int.Parse(lb_users.Items[selectedUser].ToString().Split(new char[] { '-' })[0]);

                    GiftMaker maker = new GiftMaker();

                    if (maker == null || m_catalog == null || m_items == null)
                    {
                        MsgDialogs.Show("Invalid Data Parameters!", "OnAddGift Could Not Find A Required Componenet.", "OK", MsgDialogs.MsgTypes.ERROR);
                        return;
                    }

                    maker.SetCatalog(m_catalog, m_items);

                    if (maker.ShowDialog() == DialogResult.OK)
                    {
                        ACharacter RecvChar = m_character[selectedChar];

                        if (RecvChar != null)
                        {
                            if (RecvChar.a_user_index == selectedID)
                            {
                                AUser recvUser = m_users.Find(p => p.user_code.Equals((uint)RecvChar.a_user_index));

                                if (recvUser != null)
                                {
                                    new Transactions<GiftData>(AuthCon).ExecuteQuery($"INSERT INTO t_gift0{recvUser.user_code.ToString().Last()} " +
                                        $"(a_server, a_send_user_idx, a_send_char_name, a_send_msg, a_recv_user_idx, a_recv_char_name, a_send_date, a_ctid) " +
                                        $"VALUES " +
                                        $"(1, {1}, \'{maker.Sender}\', \'{maker.Message}\', {recvUser.user_code}, \'{RecvChar.a_nick}\', \'{GetNowDateTimeMySql()}\', {maker.Package});");

                                    LoadMallTab(recvUser);
                                }
                                else
                                {
                                    MsgDialogs.ShowNoLog("Error", "Could not find an intended User.", "OK", MsgDialogs.MsgTypes.ERROR);
                                }
                            }
                            else
                            {
                                MsgDialogs.ShowNoLog("Error", "Target character is not of the selected user", "OK", MsgDialogs.MsgTypes.ERROR);
                            }
                        }
                        else
                        {
                            MsgDialogs.ShowNoLog("Error", "Cound not find an intended Character.", "OK", MsgDialogs.MsgTypes.ERROR);
                        }
                    }
                } catch(Exception exp)
                {
                    MsgDialogs.Show("Exception!", $"An exception encountered in OnAddGift\n{exp.Message}", "OK", MsgDialogs.MsgTypes.ERROR);
                }
            }
        }

        private void DoReloadUsers(object sender, EventArgs e)
        {
            OnDisconnect();
            OnConnect();
        }

        private void DoFindUserByChar(object sender, EventArgs e)
        {
            if(m_users.Count != 0)
            {
                QuestionBox qb = new QuestionBox("User Name To Search");

                if (qb.ShowDialog() == DialogResult.OK)
                {
                    Deserialize<ACharacter> charDesc = new Deserialize<ACharacter>("t_characters");

                    charDesc.SetConditions($"Where `a_name` = \'{qb.Result}\' OR `a_nick` = \'{qb.Result}\'");

                    ACharacter found = new Transactions<ACharacter>(CharCon).ExecuteQuery(charDesc).FirstOrDefault();

                    if(found != null)
                    {
                        int index = m_users.FindIndex(p => p.user_code.Equals((uint)found.a_user_index));

                        if (index != -1)
                        {
                            if (MsgDialogs.ShowNoLog("User Found!", "Go To Found User Now?", "YesNo", MsgDialogs.MsgTypes.INFO) == DialogResult.Yes)
                            {
                                tb_search.Clear();

                                lb_users.SelectedIndex = index;
                            }
                        }
                        else
                        {
                            MsgDialogs.ShowNoLog("Error", "Character But No Matching User Was Found!", "OK", MsgDialogs.MsgTypes.ERROR);
                        }
                    }
                    else
                    {
                        MsgDialogs.ShowNoLog("Error", "No Character Found By That Name!", "OK", MsgDialogs.MsgTypes.ERROR);
                    }
                }
            }
        }

        private void DoDeleteGift(object sender, EventArgs e)
        {
            int listSelection = lb_users.SelectedIndex;

            if (listSelection != -1)
            {
                int selectedID = int.Parse(lb_users.Items[listSelection].ToString().Split(new char[] { '-' })[0]);

                int foundUser = m_users.FindIndex(p => p.user_code.Equals((uint)selectedID));

                if(foundUser != -1)
                {
                    if (dgGiftInv.SelectedRows.Count != 0)
                    {
                        DataGridViewRow dgr = dgGiftInv.SelectedRows[0];

                        int idx = (int)dgr.Cells[1].Value;
                        int userCode = (int)m_users[foundUser].user_code;

                        if (MsgDialogs.ShowNoLog("Warning!", $"Are you sure you wish to remove \"{dgr.Cells[2].Value}\"?",
                        "YesNo", MsgDialogs.MsgTypes.WARNING) == DialogResult.Yes)
                        {
                            String query = $"UPDATE t_gift0{m_users[foundUser].user_code.ToString().Last()} SET `a_use_date` = \'{GetNowDateTimeMySql()}\', `a_use_char_idx` = 1 WHERE a_index = {idx};";

                            new Transactions<GiftData>(AuthCon).ExecuteQuery(query);

                            LoadMallTab(m_users[foundUser]);
                        }
                    }
                }
            }
        }

        private void DoRemovePurch(object sender, EventArgs e)
        {
            int listSelection = lb_users.SelectedIndex;

            if (listSelection != -1)
            {
                int selectedID = int.Parse(lb_users.Items[listSelection].ToString().Split(new char[] { '-' })[0]);

                int foundIndex = m_users.FindIndex(p => p.user_code.Equals((uint)selectedID));

                if (foundIndex != -1)
                {
                    if (dgPurchaseInv.SelectedRows.Count != 0)
                    {
                        DataGridViewRow dgr = dgPurchaseInv.SelectedRows[0];

                        int idx = (int)dgr.Cells[1].Value;
                        int userCode = (int)m_users[foundIndex].user_code;

                        if (MsgDialogs.ShowNoLog("Warning!", $"Are you sure you wish to remove \"{dgr.Cells[2].Value}\"?",
                        "YesNo", MsgDialogs.MsgTypes.WARNING) == DialogResult.Yes)
                        {
                            String query = $"UPDATE t_purchase0{m_users[foundIndex].user_code.ToString().Last()} SET `a_use_date` = \'{GetNowDateTimeMySql()}\', `a_use_char_idx` = 1 WHERE a_index = {idx};";

                            new Transactions<GiftData>(AuthCon).ExecuteQuery(query);

                            LoadMallTab(m_users[foundIndex]);
                        }
                    }
                }
            }
        }

        private void DoGiftRestore(object sender, EventArgs e)
        {
            int listSelection = lb_users.SelectedIndex;

            if (listSelection != -1)
            {
                int selectedID = int.Parse(lb_users.Items[listSelection].ToString().Split(new char[] { '-' })[0]);

                int foundIndex = m_users.FindIndex(p => p.user_code.Equals((uint)selectedID));

                if (foundIndex != -1)
                {
                    if (dgGiftHistory.SelectedRows.Count != 0)
                    {
                        DataGridViewRow dgr = dgGiftHistory.SelectedRows[0];

                        int idx = (int)dgr.Cells[1].Value;
                        int userCode = (int)m_users[foundIndex].user_code;

                        if (MsgDialogs.ShowNoLog("Warning!", $"Are you sure you wish to restore \"{dgr.Cells[2].Value}\"?",
                        "YesNo", MsgDialogs.MsgTypes.WARNING) == DialogResult.Yes)
                        {
                            String query = $"UPDATE t_gift0{m_users[foundIndex].user_code.ToString().Last()} SET `a_use_date` = \'{GetNowDateTimeMySql()}\', `a_use_char_idx` = 0 WHERE a_index = {idx};";

                            new Transactions<GiftData>(AuthCon).ExecuteQuery(query);

                            LoadMallTab(m_users[foundIndex]);
                        }
                    }
                }
            }
        }

        private void DoRestorePurch(object sender, EventArgs e)
        {
            int listSelection = lb_users.SelectedIndex;

            if (listSelection != -1)
            {
                int selectedID = int.Parse(lb_users.Items[listSelection].ToString().Split(new char[] { '-' })[0]);

                int foundIndex = m_users.FindIndex(p => p.user_code.Equals((uint)selectedID));

                if (foundIndex != -1)
                {
                    if (dgPurchaseHistory.SelectedRows.Count != 0)
                    {
                        DataGridViewRow dgr = dgPurchaseHistory.SelectedRows[0];

                        int idx = (int)dgr.Cells[1].Value;
                        int userCode = (int)m_users[foundIndex].user_code;

                        if (MsgDialogs.ShowNoLog("Warning!", $"Are you sure you wish to restore \"{dgr.Cells[2].Value}\"?",
                        "YesNo", MsgDialogs.MsgTypes.WARNING) == DialogResult.Yes)
                        {
                            String query = $"UPDATE t_purchase0{m_users[foundIndex].user_code.ToString().Last()} SET `a_use_date` = \'{GetNowDateTimeMySql()}\', `a_use_char_idx` = 0 WHERE a_index = {idx};";

                            new Transactions<GiftData>(AuthCon).ExecuteQuery(query);

                            LoadMallTab(m_users[foundIndex]);
                        }
                    }
                }
            }
        }

        private string GetNowDateTimeMySql()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void DoUpdateHardwareBan(object sender, EventArgs e)
        {
            int listSelection = lb_users.SelectedIndex;

            /*if (listSelection != -1)
            {
                int selectedID = int.Parse(lb_users.Items[listSelection].ToString().Split(new char[] { '-' })[0]);

                int foundUser = m_users.FindIndex(p => p.user_code.Equals((uint)selectedID));

                if (foundUser != -1)
                {
                    if (MsgDialogs.ShowNoLog("Confirm!", $"Are you sure you wish to update hardware ban status of \"{m_users[foundUser].user_id}\"",
                        "YesNo", MsgDialogs.MsgTypes.WARNING) == DialogResult.Yes)
                    {
                        int state = cbHardwareBanned.Checked == true ? 1 : 0;
                        new Transactions<HardwareBanData>(AuthCon).ExecuteQuery($"UPDATE bg_ban SET `banned` = {state}" +
                            $" WHERE user_id = \'{m_users[foundUser].user_id}\' AND guid = \'{m_users[foundUser].guid}\'");
                    }
                }
            }*/
        }

        private void OnLoadPlayerStash(object sender, EventArgs e)
        {
            if (lb_users.SelectedIndex == -1)
                return;

            uint usercode = uint.Parse(lb_users.Items[lb_users.SelectedIndex].ToString().Split(new char[] { '-' })[0]);

            int idx = m_users.FindIndex(p => p.user_code.Equals(usercode));

            // Load Stash Stuff
            LoadStash(m_users[idx]);
        }

        private void OnLoadPlayerInventory(object sender, EventArgs e)
        {
            if (lb_users.SelectedIndex == -1 || lb_characters.SelectedIndex == -1)
                return;

            int idx = lb_characters.SelectedIndex;
            ACharacter obj = m_character[idx];

            // Load Inventory Items
            LoadInventory(obj);

            // Get Wearing Items
            LoadWearItems(obj);

            // Find Pet Data
            LoadPetData(obj);
        }

        private void OnLoadPlayerSocial(object sender, EventArgs e)
        {
            if (lb_users.SelectedIndex == -1 || lb_characters.SelectedIndex == -1)
                return;

            int idx = lb_characters.SelectedIndex;
            ACharacter obj = m_character[idx];

            // Friend Data
            LoadFriends(obj);

            // Ignore Data
            LoadIgnore(obj);

            // Player Guild Info
            LoadGuildDate(obj);
        }

        private void OnLoadHackData(object sender, EventArgs e)
        {
            if (lb_users.SelectedIndex == -1 || lb_characters.SelectedIndex == -1)
                return;

            int idx = lb_characters.SelectedIndex;
            ACharacter obj = m_character[idx];

            LoadHackUserData(obj);
        }

        private void OnLoadChatData(object sender, EventArgs e)
        {
            if (lb_users.SelectedIndex == -1)
                return;

            uint usercode = uint.Parse(lb_users.Items[lb_users.SelectedIndex].ToString().Split(new char[] { '-' })[0]);

            int idx = m_users.FindIndex(p => p.user_code.Equals(usercode));

            // ChatUserData
            LoadChatUserData(m_users[idx]);
        }

        private void OnLoadGuildStash(object sender, EventArgs e)
        {
            if (lb_users.SelectedIndex == -1 || lb_characters.SelectedIndex == -1)
                return;

            if (m_memberData == null || m_memberData.a_guild_index == 0)
            {
                MsgDialogs.ShowNoLog("Guild Stash", "Character is not member of a guild or info was not loaded.", "ok", MsgDialogs.MsgTypes.INFO);
                return;
            }

            int rows = new Transactions<int>(CharCon).ExecuteScalar($"SELECT * FROM t_guild_stash_info WHERE a_guild_idx = {m_memberData.a_guild_index} AND a_enable = 1");

            if (rows != 0) {
                Deserialize<GuildStashData> gstashDesc = new Deserialize<GuildStashData>("t_guild_stash");

                List<GuildStashData> gstash = new Transactions<GuildStashData>(CharCon).ExecuteQuery(gstashDesc).OrderBy(p => p.a_index).ToList();

                ulong cash = new Transactions<uint>(CharCon).GetInt($"SELECT * FROM t_guild_stash_info WHERE a_guild_idx = {m_memberData.a_guild_index} AND a_enable = 1", "a_nas");

                pb_guildStash.SetItems(cash, m_items, gstash);
            }
            else
            {
                MsgDialogs.ShowNoLog("Guild Stash", $"No guild stash info was found for guild index: {m_memberData.a_guild_index}", "ok", MsgDialogs.MsgTypes.INFO);
            }
        }
    }
}
