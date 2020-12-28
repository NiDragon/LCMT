using IllTechLibrary;
using IllTechLibrary.Localization;
using IllTechLibrary.Settings;
using IllTechLibrary.Util;
using LCMT.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#if ASSIMP_CONVERT
using Assimp;
using Assimp.Configs;
#endif
using System.Drawing.Imaging;

using IllTechLibrary.Crypto;
using IllTechLibrary.DataFiles;

namespace LCMT
{
    public partial class MultiFrm : MetroFramework.Forms.MetroForm
    {
        private const int MAX_PATH = 260;
        private static TimeSpan DBConnectTimeout = TimeSpan.FromSeconds(10);

        private String[] TableIds = { "DataDB", "AuthDB", "CharDB", "PostDB" };
        private bool[] m_signal = new bool[4];
        private int s_count = 0;

        [DllImport("ICL.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int BrowseFolder([MarshalAsAttribute(UnmanagedType.LPWStr)] String Title, [MarshalAsAttribute(UnmanagedType.LPWStr)] StringBuilder ResultPath, [MarshalAsAttribute(UnmanagedType.LPWStr)] String InitPath);

        #region DB_CONNECTIONS
        private IllSQL m_db = new IllSQL();
        private IllSQL m_auth = new IllSQL();
        private IllSQL m_data = new IllSQL();
        private IllSQL m_post = new IllSQL();

        public IllSQL GetDataDb()
        {
            return m_data;
        }

        public IllSQL GetAuthDb()
        {
            return m_auth;
        }

        public IllSQL GetCharDb()
        {
            return m_db;
        }

        public IllSQL GetPostDb()
        {
            return m_post;
        }

        private void Connect()
        {
            OnConnect(null, null);
        }

        public void Disconnect()
        {
            if(m_data.IsConnected())
                m_data.Disconnect();

            if (m_auth.IsConnected())
                m_auth.Disconnect();

            if (m_db.IsConnected())
                m_db.Disconnect();

            if (m_post.IsConnected())
                m_post.Disconnect();
        }

#endregion

        private static MultiFrm parent;

        public static MultiFrm GetInstance()
        {
            return parent;
        }

        public MultiFrm()
        {
            Preferences.SetConfig("Config.ini");

            InitializeComponent();

#if CUSTOM_MDI
            IsMdiContainer = false;
            cMdiContainer1.Visible = true;
#endif
            Preferences.SetWindow("MultiTool", this);

            Preferences.LoadLocale();
            StringTable.LoadStrings(Core.UILanguage);
            Translate(StringTable.UIStrings);

            Text = $"LastChaos Multitool - {Application.ProductVersion}";

            /* Store the instance */
            parent = this;

            m_auth.OnConnectionLost = HandleDisconnect;
            m_data.OnConnectionLost = HandleDisconnect;
            m_db.OnConnectionLost = HandleDisconnect;
            m_post.OnConnectionLost = NoAction;
        }

        private void Translate(StringTable st)
        {
            RecurseItems(st, MainMenuStrip.Items.OfType<ToolStripMenuItem>());
        }

        private void RecurseItems(StringTable st, IEnumerable<ToolStripMenuItem> col)
        {
            foreach (ToolStripMenuItem item in col)
            {
                if (st.HasKey(item.Name))
                {
                    item.Text = st.Get(item.Name);
                }

                RecurseItems(st, item.DropDownItems.OfType<ToolStripMenuItem>());
            }
        }

        private void NoAction(String Caption, String Text, String Buttons, MsgDialogs.MsgTypes MsgType)
        {
        }

        private void HandleDisconnect(String Caption, String Text, String Buttons, MsgDialogs.MsgTypes MsgType)
        {
            Invoke((MethodInvoker)delegate
            {
                StatusTSText.ForeColor = Color.Red;
                StatusTSText.Text = "Disconnected";

                m_data.Disconnect();
                m_auth.Disconnect();
                m_db.Disconnect();
                m_post.Disconnect();
            });

            if(!m_auth.IsConnected()&&
               !m_data.IsConnected() &&
               !m_db.IsConnected() &&
               !m_post.IsConnected())
            {
                if (MsgDialogs.Show(Caption, Text, Buttons, MsgType) == DialogResult.OK)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        DataMenuItem_Init.Click -= OnDisconnect;
                        DataMenuItem_Init.Text = StringTable.UIStrings["DataMenuItem_Re"];
                        DataMenuItem_Init.Click += OnReconnect;
                    });
                }
            }
        }

        private void OnFormLoaded(object sender, EventArgs e)
        {
#region MIDI_BACKGROUND
            MdiClient ctlMDI;

            // Loop through all of the form's controls looking
            // for the control of type MdiClient.
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    if(ctl.GetType() != typeof(MdiClient))
                        continue;

                    // Attempt to cast the control to type MdiClient.
                    ctlMDI = (MdiClient)ctl;

                    // Set the BackColor of the MdiClient control.
                    ctlMDI.BackColor = Color.FromArgb(255, 17, 17, 17);
                }
                catch (InvalidCastException)
                {
                    // Catch and ignore the error if casting failed.
                }
            }
#endregion

#region CLIENT_PATH_CHECK
            IniParser.Model.IniData data = Preferences.GetData();
            String RootDir = data["CLIENT"]["root"];

            if (!Directory.Exists(RootDir))
            {
                MessageBox.Show("Please Set The Path To Client Root Directory!", "Configure");

                StringBuilder nPath = new StringBuilder(MAX_PATH);

                int ret = BrowseFolder("Select Client Folder", nPath, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

                String path = nPath.ToString();//fbd.SelectedPath;

                if(path == String.Empty || ret != 0)
                {
                    MessageBox.Show("The Client Path is Required!");
                    Application.Exit();
                    return;
                }

                if (path.Last() == '\\' || path.Last() == '/')
                {
                    Preferences.SetPrefKey("CLIENT", "root", path);
                }
                else
                {
                    Preferences.SetPrefKey("CLIENT", "root", path + "\\");
                }
            }
#endregion

            StyleManager = new MetroFramework.Components.MetroStyleManager();
            StyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
        }

        private void OnShowSettings(object sender, EventArgs e)
        {
            if (Core.ShowSettings() == DialogResult.OK)
            {
                try
                {
                    // Locking is required here because multithread operations
                    m_data.AcquireLock();
                    m_auth.AcquireLock();
                    m_db.AcquireLock();
                    m_post.AcquireLock();

                    if (m_data.IsConnected() || m_auth.IsConnected() ||
                    m_db.IsConnected() || m_post.IsConnected())
                    {
                        OnDisconnect(null, null);
                    }

                    Connect();
                }
                catch (Exception)
                {
                    // Should be specially handled?
                }
                finally
                {
                    m_data.ReleaseLock();
                    m_auth.ReleaseLock();
                    m_db.ReleaseLock();
                    m_post.ReleaseLock();
                }
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Preferences.WriteWindow("MultiTool", this);

            Interrupt.AbortRequested.Set();

            for (int i = 0; i < MdiChildren.Count(); i++)
            {
                LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                fm.Close();
            }

            Disconnect();

#if AUTH_SERVICE
            IllTechLibrary.Maou.NetSession.Instance().Logout();
#endif
        }

        private void OnChangeClientPath(object sender, EventArgs e)
        {
            StringBuilder nPath = new StringBuilder(MAX_PATH);

            int ret = BrowseFolder("Select Client Folder", nPath, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            if (ret != 0)
                return;

            String path = nPath.ToString();

            if (path.Last() == '\\' || path.Last() == '/')
            {
                Preferences.SetPrefKey("CLIENT", "root", path);
            }
            else
            {
                Preferences.SetPrefKey("CLIENT", "root", path + "\\");
            }
        }

        private void OnShowLog(object sender, EventArgs e)
        {
            if (!File.Exists("IllTech.log"))
                File.Create("IllTech.log");

            //Process.Start("IllTech.log");

            LogView.ShowLog();
        }

        private void OnAboutClick(object sender, EventArgs e)
        {
            About ab = new About();
            ab.ShowDialog();
        }

        /// <summary>
        /// Action For Connect Click Performed.
        /// Note: This function should only be called when all connections
        /// are in the disconnected state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnect(object sender, EventArgs e)
        {
            // Reset our signals
            s_count = 0;
            m_signal = new bool[4];

            DataMenuItem_Init.Click -= OnConnect;

            StatusTSText.ForeColor = Color.Green;
            StatusTSText.Text = StringTable.UIStrings["DataMenuItem_Con"];
            DataMenuItem_Init.Text = StringTable.UIStrings["DataMenuItem_Con"];

            DataMenuItem_Tables.Enabled = false;
            DataMenuItem_Settings.Enabled = false;
            ToolMenuItem.Enabled = false;

            new Thread(() =>
            {
                Stopwatch sw = new Stopwatch();

                sw.Start();

                string[] conStrings = Preferences.GetConnectionStrings(TableIds);

                bool failed = false;

                // Keep connection locking so we do not fuck ourselves
                new Task(delegate { if (m_data.Connect(conStrings[0])) { m_signal[0] = true; } else { m_signal[0] = false; failed = true; } s_count++; }).Start();
                new Task(delegate { if (m_auth.Connect(conStrings[1])) { m_signal[1] = true; } else { m_signal[1] = false; failed = true; } s_count++; }).Start();
                new Task(delegate { if (m_db.Connect(conStrings[2])) { m_signal[2] = true; } else { m_signal[2] = false; failed = true; } s_count++; }).Start();
                new Task(delegate { if (m_post.Connect(conStrings[3])) { m_signal[3] = true; } else { m_signal[3] = false; failed = true; } s_count++; }).Start();

                try
                {
                    long StartTime = DateTime.Now.Ticks;

                    // Wait until all procs have signaled
                    while (s_count != 4)
                    {
                        if (failed)
                            break;

                        Thread.Sleep(10);

                        if (DBConnectTimeout <= TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime))
                        {
                            s_count = 4;
                            m_signal[0] = m_signal[1] = m_signal[2] = m_signal[3] = false;
                        }
                        else if(m_signal[0] && m_signal[1] && m_signal[2] && m_signal[3])
                        {
                            s_count = 4;
                        }
                        else if(m_data.IsConnected() && m_auth.IsConnected() && m_db.IsConnected() && m_post.IsConnected())
                        {
                            s_count = 4;
                            m_signal[0] = m_signal[1] = m_signal[2] = m_signal[3] = true;
                        }
                    }

                    bool connected = false;

                    Invoke((MethodInvoker)delegate
                    {
                        if (!(m_signal[0] && m_signal[1] && m_signal[2] && m_signal[3]) /*CONNECT_FAIL*/)
                        {
                            StatusTSText.ForeColor = Color.Red;
                            StatusTSText.Text = "Not Connected";

                            m_data.Disconnect();
                            m_auth.Disconnect();
                            m_db.Disconnect();
                            m_post.Disconnect();

                            DataMenuItem_Init.Text = StringTable.UIStrings["DataMenuItem_Init"];
                            DataMenuItem_Init.Click += OnConnect;

                            MsgDialogs.Show("Database Error!", "Could not connect to DB! Check config and try again.", "ok", MsgDialogs.MsgTypes.ERROR);
                        }
                        else
                        {
                            StatusTSText.ForeColor = Color.Green;
                            StatusTSText.Text = "Connected";

                            DataMenuItem_Init.Text = StringTable.UIStrings["DataMenuItem_On"];
                            DataMenuItem_Init.Click += OnDisconnect;

                            for (int i = 0; i < MdiChildren.Count(); i++)
                            {
                                LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                                fm.OnConnect();
                            }

                            connected = true;
                        }

                        DataMenuItem_Tables.Enabled = true;
                        DataMenuItem_Settings.Enabled = true;
                        ToolMenuItem.Enabled = true;
                    });

                    sw.Stop();

                    if(connected)
                        MsgDialogs.LogInfo($"Connecting to db completed in : {sw.ElapsedMilliseconds}ms");
                }
                catch (Exception) // Application aborted
                {
                    // Do nothing?
                }
            }).Start();
        }

        private void OnDisconnect(object sender, EventArgs e)
        {
            StatusTSText.ForeColor = Color.Red;
            StatusTSText.Text = "Disconnected";

            m_data.Disconnect();
            m_auth.Disconnect();
            m_db.Disconnect();
            m_post.Disconnect();

            DataMenuItem_Init.Click -= OnDisconnect;
            DataMenuItem_Init.Text = StringTable.UIStrings["DataMenuItem_Init"];
            DataMenuItem_Init.Click += OnConnect;

            for(int i = 0; i < MdiChildren.Count(); i++)
            {
                LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                fm.OnDisconnect();
            }
        }

        private void OnReconnect(object sender, EventArgs e)
        {
            // Reset our signals
            s_count = 0;
            m_signal = new bool[4];

            DataMenuItem_Init.Click -= OnReconnect;

            StatusTSText.ForeColor = Color.Green;
            StatusTSText.Text = StringTable.UIStrings["DataMenuItem_Con"];
            DataMenuItem_Init.Text = StringTable.UIStrings["DataMenuItem_Con"];

            DataMenuItem_Tables.Enabled = false;
            DataMenuItem_Settings.Enabled = false;
            ToolMenuItem.Enabled = false;

            new Thread(() =>
            {
                Stopwatch sw = new Stopwatch();

                sw.Start();

                string[] conStrings = Preferences.GetConnectionStrings(TableIds);

                bool failed = false;

                // Keep connection locking so we do not fuck ourselves
                new Task(delegate { if (m_data.Connect(conStrings[0])) { m_signal[0] = true; } else { m_signal[0] = false; failed = true; } s_count++; }).Start();
                new Task(delegate { if (m_auth.Connect(conStrings[1])) { m_signal[1] = true; } else { m_signal[1] = false; failed = true; } s_count++; }).Start();
                new Task(delegate { if (m_db.Connect(conStrings[2])) { m_signal[2] = true; } else { m_signal[2] = false; failed = true; } s_count++; }).Start();
                new Task(delegate { if (m_post.Connect(conStrings[3])) { m_signal[3] = true; } else { m_signal[3] = false; failed = true; } s_count++; }).Start();

                try
                {
                    long StartTime = DateTime.Now.Ticks;

                    // Wait until all procs have signaled
                    while (s_count != 4)
                    {
                        if (failed)
                            break;

                        Thread.Sleep(10);

                        if (DBConnectTimeout <= TimeSpan.FromTicks(DateTime.Now.Ticks - StartTime))
                        {
                            s_count = 4;
                            m_signal[0] = m_signal[1] = m_signal[2] = m_signal[3] = false;
                        }
                        else if (m_signal[0] && m_signal[1] && m_signal[2] && m_signal[3])
                        {
                            s_count = 4;
                        }
                        else if (m_data.IsConnected() && m_auth.IsConnected() && m_db.IsConnected() && m_post.IsConnected())
                        {
                            s_count = 4;
                            m_signal[0] = m_signal[1] = m_signal[2] = m_signal[3] = true;
                        }
                    }

                    bool connected = false;

                    Invoke((MethodInvoker)delegate
                    {
                        if (!(m_signal[0] && m_signal[1] && m_signal[2] && m_signal[3]) /*CONNECT_FAIL*/)
                        {
                            StatusTSText.ForeColor = Color.Red;
                            StatusTSText.Text = "Not Connected";

                            m_data.Disconnect();
                            m_auth.Disconnect();
                            m_db.Disconnect();
                            m_post.Disconnect();

                            DataMenuItem_Init.Text = StringTable.UIStrings["DataMenuItem_Re"];
                            DataMenuItem_Init.Click += OnReconnect;

                            MsgDialogs.Show("Database Error!", "Could not connect to DB! Check config and try again.", "ok", MsgDialogs.MsgTypes.ERROR);
                        }
                        else
                        {
                            StatusTSText.ForeColor = Color.Green;
                            StatusTSText.Text = "Connected";

                            DataMenuItem_Init.Text = StringTable.UIStrings["DataMenuItem_On"];
                            DataMenuItem_Init.Click += OnDisconnect;

                            connected = true;
                        }

                        DataMenuItem_Tables.Enabled = true;
                        DataMenuItem_Settings.Enabled = true;
                        ToolMenuItem.Enabled = true;
                    });

                    sw.Stop();

                    if(connected)
                        MsgDialogs.LogInfo($"Connecting to db completed in : {sw.ElapsedMilliseconds}ms");
                }
                catch (Exception) // Application aborted
                {
                    // Do nothing?
                }
            }).Start();
        }

        private void OnResizeEnd(object sender, EventArgs e)
        {
            /*for (int i = 0; i < MdiChildren.Count(); i++)
            {
                LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                if (fm.GetToolID() == NpcTool.NpcFrm.NpcToolID)
                {
                    ((NpcTool.NpcFrm)fm).OnFormResizeComplete(null, null);
                    return;
                }
            }*/
        }

        private void OnSetDatabases(object sender, EventArgs e)
        {
            Interfaces.DatabaseChanger tc = new Interfaces.DatabaseChanger();

            if (tc.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Locking is required here because multithread operations
                    m_data.AcquireLock();
                    m_auth.AcquireLock();
                    m_db.AcquireLock();
                    m_post.AcquireLock();

                    if (m_data.IsConnected() || m_auth.IsConnected() ||
                    m_db.IsConnected() || m_post.IsConnected())
                    {
                        for (int i = 0; i < MdiChildren.Count(); i++)
                        {
                            LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                            fm.OnDisconnect();
                        }

                        String dDb = IllSQL.GetToolDB("DataDB");
                        String cDb = IllSQL.GetToolDB("CharDB");
                        String aDb = IllSQL.GetToolDB("AuthDB");
                        String pDb = IllSQL.GetToolDB("PostDB");

                        // Roll back if we fail to connect to the new database
                        if(!m_data.SwitchDB(dDb))
                        {
                            IllSQL.SetToolDB("DataDB", tc.dataOld);
                        }

                        if (!m_auth.SwitchDB(cDb))
                        {
                            IllSQL.SetToolDB("AuthDB", tc.authOld);
                        }

                        if (!m_db.SwitchDB(aDb))
                        {
                            IllSQL.SetToolDB("CharDB", tc.charOld);
                        }

                        if (!m_post.SwitchDB(pDb))
                        {
                            IllSQL.SetToolDB("PostDB", tc.postOld);
                        }

                        for (int i = 0; i < MdiChildren.Count(); i++)
                        {
                            LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                            fm.OnConnect();
                        }
                    }
                    else
                    {
                        OnConnect(null, null);
                    }
                }
                catch (Exception)
                {
                    // Should be specially handled?
                }
                finally
                {
                    m_data.ReleaseLock();
                    m_auth.ReleaseLock();
                    m_db.ReleaseLock();
                    m_post.ReleaseLock();
                }
            }
        }

        private void OnOpenNpcTool(object sender, EventArgs e)
        {
            for (int i = 0; i < MdiChildren.Count(); i++)
            {
                LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                if (fm.GetToolID() == NpcTool.NpcFrm.NpcToolID)
                {
                    fm.BringToFront();
                    return;
                }
            }

            LCToolFrm fx = new NpcTool.NpcFrm();
            fx.MdiParent = this;
            fx.Show();
            fx.BringToFront();

            if (GetDataDb().IsConnected())
                ((NpcTool.NpcFrm)fx).OnConnect();
        }

        private void OnOpenItemTool(object sender, EventArgs e)
        {
            for (int i = 0; i < MdiChildren.Count(); i++)
            {
                LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                if (fm.GetToolID() == ItemTool.ItemFrm.ItemToolID)
                {
                    fm.BringToFront();
                    return;
                }
            }

#if CUSTOM_MDI
            LCToolFrm fx = new ItemTool.ItemFrm();
            cMdiContainer1.AddForm(fx);
#else
            LCToolFrm fx = new ItemTool.ItemFrm();
            fx.MdiParent = this;
            fx.Show();
            fx.BringToFront();
#endif

            if (GetDataDb().IsConnected())
                ((ItemTool.ItemFrm)fx).OnConnect();
        }

        private void OnOpenStringTool(object sender, EventArgs e)
        {
            for (int i = 0; i < MdiChildren.Count(); i++)
            {
                LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                if (fm.GetToolID() == StringTool.StringFrm.StringToolID)
                {
                    fm.BringToFront();
                    return;
                }
            }

            LCToolFrm fx = new StringTool.StringFrm();
            fx.MdiParent = this;
            fx.Show();
            fx.BringToFront();

            if (GetDataDb().IsConnected())
                ((StringTool.StringFrm)fx).OnConnect();
        }

        private void OnOpenLuckyDrawTool(object sender, EventArgs e)
        {
            for (int i = 0; i < MdiChildren.Count(); i++)
            {
                LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                if (fm.GetToolID() == LuckyTool.LuckyFrm.LuckyDrawToolID)
                {
                    fm.BringToFront();
                    return;
                }
            }

#if CUSTOM_MDI
            LCToolFrm fx = new LuckyTool.LuckyFrm();
            cMdiContainer1.AddForm(fx);
#else
            LCToolFrm fx = new LuckyTool.LuckyFrm();
            fx.MdiParent = this;
            fx.Show();
            fx.BringToFront();
#endif

            if (GetDataDb().IsConnected())
                ((LuckyTool.LuckyFrm)fx).OnConnect();
        }

        private void OnOpenTitleTool(object sender, EventArgs e)
        {
            for (int i = 0; i < MdiChildren.Count(); i++)
            {
                LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                if (fm.GetToolID() == TitleTool.TitleFrm.TitleToolID)
                {
                    fm.BringToFront();
                    return;
                }
            }

            LCToolFrm fx = new TitleTool.TitleFrm();
            fx.MdiParent = this;
            fx.Show();
            fx.BringToFront();

            if (GetDataDb().IsConnected())
                ((TitleTool.TitleFrm)fx).OnConnect();
        }

        private void OnOpenSkillTool(object sender, EventArgs e)
        {
            for (int i = 0; i < MdiChildren.Count(); i++)
            {
                LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                if (fm.GetToolID() == SkillTool.SkillFrm.SkillToolID)
                {
                    fm.BringToFront();
                    return;
                }
            }

            LCToolFrm fx = new SkillTool.SkillFrm();
            fx.MdiParent = this;
            fx.Show();
            fx.BringToFront();

            if (GetDataDb().IsConnected())
                ((SkillTool.SkillFrm)fx).OnConnect();
        }

        private void OnOpenPlayerTool(object sender, EventArgs e)
        {
            for (int i = 0; i < MdiChildren.Count(); i++)
            {
                LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                if (fm.GetToolID() == PlayerTool.PlayerFrm.PlayerToolID)
                {
                    fm.BringToFront();
                    return;
                }
            }

            LCToolFrm fx = new PlayerTool.PlayerFrm();
            fx.MdiParent = this;
            fx.Show();
            fx.BringToFront();

            if (GetAuthDb().IsConnected() && GetCharDb().IsConnected())
                ((PlayerTool.PlayerFrm)fx).OnConnect();
        }

        private void OnOpenZoneTool(object sender, EventArgs e)
        {
            for (int i = 0; i < MdiChildren.Count(); i++)
            {
                LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                if (fm.GetToolID() == ZoneData.ZoneFrm.ZoneToolID)
                {
                    fm.BringToFront();
                    return;
                }
            }

            LCToolFrm fx = new ZoneData.ZoneFrm();
            fx.MdiParent = this;
            fx.Show();
            fx.BringToFront();

            if (GetDataDb().IsConnected())
                ((ZoneData.ZoneFrm)fx).OnConnect();
        }

        private void OnOpenMagicTool(object sender, EventArgs e)
        {
            for (int i = 0; i < MdiChildren.Count(); i++)
            {
                LCToolFrm fm = (LCToolFrm)MdiChildren[i];

                if (fm.GetToolID() == MagicTool.MagicTool.MagicToolID)
                {
                    fm.BringToFront();
                    return;
                }
            }

            LCToolFrm fx = new MagicTool.MagicTool();
            fx.MdiParent = this;
            fx.Show();
            fx.BringToFront();

            if (GetDataDb().IsConnected())
                ((MagicTool.MagicTool)fx).OnConnect();
        }

        private void OnExitMultiForm(object sender, EventArgs e)
        {
            Close();
        }

#if ASSIMP_CONVERT
        int WriteBitmapFile(string filename, int width, int height, byte[] imageData)
        {
            byte[] newData = new byte[imageData.Length];

            for (int x = 0; x < imageData.Length; x += 4)
            {
                byte[] pixel = new byte[4];
                Array.Copy(imageData, x, pixel, 0, 4);

                byte r = pixel[0];
                byte g = pixel[1];
                byte b = pixel[2];
                byte a = pixel[3];

                byte[] newPixel = new byte[] { b, g, r, a };

                Array.Copy(newPixel, 0, newData, x, 4);
            }

            imageData = newData;

            using (var stream = new MemoryStream(imageData))
            using (var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0,
                                                                bmp.Width,
                                                                bmp.Height),
                                                  ImageLockMode.WriteOnly,
                                                  bmp.PixelFormat);

                IntPtr pNative = bmpData.Scan0;
                Marshal.Copy(imageData, 0, pNative, imageData.Length);

                bmp.UnlockBits(bmpData);

                bmp.Save(filename, ImageFormat.Png);
            }

            return 1;
        }

        private void SMCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string outPath = Path.GetDirectoryName(sfd.FileName);

                    NpcView.Model p = new NpcView.Model();

                    p.readFileRaw(ofd.FileName);

                    GetSupportedExportFormats();

                    LogStream.IsVerboseLoggingEnabled = true;

                    Assimp.LoggingCallback aLogger = new Assimp.LoggingCallback(DoLogAssimp);

                    LogStream aLog = new LogStream(aLogger);

                    aLog.Attach();

                    AssimpContext aiContext = new AssimpContext();

                    Scene aScene = new Scene();

                    Node Root = new Node("RootNode");

                    aScene.RootNode = Root;

                    // Export materials
                    for (int i = 0; i < p.textures.Count; i++)
                    {
                        Material nMat = new Material();

                        TextureSlot ts = new TextureSlot(p.textures[i].name + ".png", TextureType.Diffuse, 0, TextureMapping.Plane, 0, 0, TextureOperation.Add, TextureWrapMode.Wrap, TextureWrapMode.Wrap, 0);

                        nMat.AddMaterialTexture(ref ts);
                        WriteBitmapFile(outPath + "\\" + p.textures[i].name + ".png", p.textures[i].Width, p.textures[i].Height, p.textures[i].GetImage());

                        aScene.Materials.Add(nMat);
                    }

                    // Export meshes
                    for (int i = 0; i < p.models.Count; i++)
                    {
                        Node nNode = new Node(p.models[i].textureNames[0] + i.ToString());

                        Mesh nMesh = new Mesh(PrimitiveType.Triangle);

                        for (int j = 0; j < p.models[i].vertices.Count(); j++)
                        {
                            SlimDX.Vector3 pos = p.models[i].vertices[j].position;
                            nMesh.Vertices.Add(new Vector3D(pos.X, pos.Y, pos.Z));

                            SlimDX.Vector3 norm = p.models[i].vertices[j].normal;
                            nMesh.Normals.Add(new Vector3D(norm.X, norm.Y, norm.Z));

                            SlimDX.Vector2 tex = p.models[i].vertices[j].textCoord;
                            nMesh.TextureCoordinateChannels[0].Add(new Vector3D(tex.X, tex.Y, 1.0f));
                        }

                        // Write triangles
                        for (int j = 0; j < p.models[i].indices.Count(); j += 3)
                        {
                            Face aFace = new Face();

                            aFace.Indices.Add(p.models[i].indices[j]);
                            aFace.Indices.Add(p.models[i].indices[j + 1]);
                            aFace.Indices.Add(p.models[i].indices[j + 2]);

                            nMesh.Faces.Add(aFace);
                        }

                        // Write bones and weights
                        for (int j = 0; j < p.models[i].vertices.Count(); j++)
                        {
                            if (p.models[i].vertices[j].boneId[0] == -1)
                                break;

                            VertexWeight vw = new VertexWeight();

                            vw.VertexID = j;
                            vw.Weight = p.models[i].vertices[j].weight[0];

                            int bidx = nMesh.Bones.FindIndex(g => g.Name == p.models[i].vertices[j].boneName[0]);

                            if (bidx == -1)
                            {
                                Bone aBone = new Bone();

                                aBone.Name = p.models[i].vertices[j].boneName[0];

                                // T POSE Matrix?
                                NpcView.Matrix12 m = p.skeleton.getBoneByName(p.models[i].vertices[j].boneName[0]).absolutePlacementMatrix;

                                aBone.OffsetMatrix = new Matrix4x4(m[0, 0], m[0, 1], m[0, 2], 0f,
                                    m[1, 0], m[1, 1], m[1, 2], 0f,
                                    m[2, 0], m[2, 1], m[2, 2], 0f,
                                    m[3, 0], m[3, 1], m[3, 2], 1.0f);

                                aBone.VertexWeights.Add(vw);

                                nMesh.Bones.Add(aBone);
                            }
                            else
                            {
                                nMesh.Bones[bidx].VertexWeights.Add(vw);
                            }
                        }

                        nMesh.MaterialIndex = FindTexture(p.models[i].textureNames[0], p.textures);

                        aScene.Meshes.Add(nMesh);

                        nNode.MeshIndices.Add(i);

                        Root.Children.Add(nNode);
                    }

                    // Write animations
                    for (int i = 0; i < p.animations.Count; i++)
                    {
                        Animation am = new Animation();

                        am.Name = p.animations[i].name;
                        am.TicksPerSecond = p.animations[i].secondsPerFrame;

                        for (int j = 0; j < p.animations[i].boneEnvelopes.Count; j++)
                        {
                            NpcView.BoneEnvelope bevp = p.animations[i].boneEnvelopes[j];

                            NodeAnimationChannel nac = new NodeAnimationChannel();

                            nac.NodeName = bevp.boneName;

                            for (int k = 0; k < bevp.posKeyFrames.Count; k++)
                            {
                                NpcView.PosKeyFrame pkf = bevp.posKeyFrames[k];

                                nac.PositionKeys.Add(new VectorKey(pkf.frame, new Vector3D(pkf.position.X, pkf.position.Y, pkf.position.Z)));
                            }

                            for (int k = 0; k < bevp.rotKeyFrames.Count; k++)
                            {
                                NpcView.RotKeyFrame rkf = bevp.rotKeyFrames[k];

                                nac.PositionKeys.Add(new VectorKey(rkf.frame, new Vector3D(rkf.rotation.X, rkf.rotation.Y, rkf.rotation.Z)));
                            }

                            am.NodeAnimationChannels.Add(nac);
                        }

                        aScene.Animations.Add(am);
                    }

                    aiContext.ExportFile(aScene, sfd.FileName, m_exportFormats[9].FormatId, PostProcessSteps.FindInvalidData | PostProcessSteps.ValidateDataStructure);
                }
            }
        }

        private void DoLogAssimp(string msg, string userData)
        {
            TextWriter aLogWriter;

            aLogWriter = new StreamWriter(File.Open("AssimLog.log", FileMode.Append));

            aLogWriter.Write(msg);

            aLogWriter.Flush();

            aLogWriter.Close();
        }

        // 0 Collade
        // 1 X
        // 3 wavefront with mlt
        // 9 3ds
        // 16 3mf

        private int FindTexture(string materialName, List<NpcView.LCTex> textures)
        {
            for(int i = 0; i < textures.Count; i++)
            {
                if(textures[i].name == materialName && !textures[i].name.ToLower().Contains("detail") && !textures[i].name.Contains("_N_"))
                {
                    return i;
                }
            }

            return 0;
        }

        private ExportFormatDescription[] m_exportFormats;

        public ExportFormatDescription[] GetSupportedExportFormats()
        {
            if (m_exportFormats == null)
                m_exportFormats = Assimp.Unmanaged.AssimpLibrary.Instance.GetExportFormatDescriptions();

            return (ExportFormatDescription[])m_exportFormats.Clone();
        }
#endif
    }
}
