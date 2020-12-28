namespace LCMT
{
    partial class MultiFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiFrm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenuItem_ClientPath = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FileMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.DataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataMenuItem_Init = new System.Windows.Forms.ToolStripMenuItem();
            this.DataMenuItem_Tables = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.DataMenuItem_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.npcToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientStringToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skillToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.magicToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.luckyDrawToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.titleToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoneDataToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem_Log = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.HelpMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusTSLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusTSText = new System.Windows.Forms.ToolStripStatusLabel();
            this.SpringBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.UpdateReadyLink = new System.Windows.Forms.ToolStripStatusLabel();
            this.cMdiContainer1 = new IllTechLibrary.Controls.CMdiContainer();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.DataMenuItem,
            this.ToolMenuItem,
            this.HelpMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(20, 60);
            this.menuStrip1.MaximumSize = new System.Drawing.Size(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(973, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem_ClientPath,
            this.toolStripSeparator2,
            this.FileMenuItem_Exit});
            this.FileMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FileMenuItem.ForeColor = System.Drawing.Color.Teal;
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "File";
            // 
            // FileMenuItem_ClientPath
            // 
            this.FileMenuItem_ClientPath.BackColor = System.Drawing.SystemColors.Control;
            this.FileMenuItem_ClientPath.Name = "FileMenuItem_ClientPath";
            this.FileMenuItem_ClientPath.Size = new System.Drawing.Size(180, 22);
            this.FileMenuItem_ClientPath.Text = "Change Client Path";
            this.FileMenuItem_ClientPath.Click += new System.EventHandler(this.OnChangeClientPath);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // FileMenuItem_Exit
            // 
            this.FileMenuItem_Exit.Name = "FileMenuItem_Exit";
            this.FileMenuItem_Exit.Size = new System.Drawing.Size(180, 22);
            this.FileMenuItem_Exit.Text = "Exit";
            this.FileMenuItem_Exit.Click += new System.EventHandler(this.OnExitMultiForm);
            // 
            // DataMenuItem
            // 
            this.DataMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataMenuItem_Init,
            this.DataMenuItem_Tables,
            this.toolStripSeparator1,
            this.DataMenuItem_Settings});
            this.DataMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.DataMenuItem.ForeColor = System.Drawing.Color.Teal;
            this.DataMenuItem.Name = "DataMenuItem";
            this.DataMenuItem.Size = new System.Drawing.Size(67, 20);
            this.DataMenuItem.Text = "Database";
            // 
            // DataMenuItem_Init
            // 
            this.DataMenuItem_Init.Name = "DataMenuItem_Init";
            this.DataMenuItem_Init.Size = new System.Drawing.Size(180, 22);
            this.DataMenuItem_Init.Text = "Connect";
            this.DataMenuItem_Init.Click += new System.EventHandler(this.OnConnect);
            // 
            // DataMenuItem_Tables
            // 
            this.DataMenuItem_Tables.Name = "DataMenuItem_Tables";
            this.DataMenuItem_Tables.Size = new System.Drawing.Size(180, 22);
            this.DataMenuItem_Tables.Text = "Set Tables";
            this.DataMenuItem_Tables.Click += new System.EventHandler(this.OnSetDatabases);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // DataMenuItem_Settings
            // 
            this.DataMenuItem_Settings.Name = "DataMenuItem_Settings";
            this.DataMenuItem_Settings.Size = new System.Drawing.Size(180, 22);
            this.DataMenuItem_Settings.Text = "Settings";
            this.DataMenuItem_Settings.Click += new System.EventHandler(this.OnShowSettings);
            // 
            // ToolMenuItem
            // 
            this.ToolMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemToolToolStripMenuItem,
            this.npcToolToolStripMenuItem,
            this.clientStringToolToolStripMenuItem,
            this.skillToolToolStripMenuItem,
            this.magicToolToolStripMenuItem,
            this.luckyDrawToolToolStripMenuItem,
            this.titleToolToolStripMenuItem,
            this.playerToolToolStripMenuItem,
            this.zoneDataToolToolStripMenuItem});
            this.ToolMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ToolMenuItem.ForeColor = System.Drawing.Color.Teal;
            this.ToolMenuItem.Name = "ToolMenuItem";
            this.ToolMenuItem.Size = new System.Drawing.Size(46, 20);
            this.ToolMenuItem.Text = "Tools";
            // 
            // itemToolToolStripMenuItem
            // 
            this.itemToolToolStripMenuItem.Image = global::LCMT.Properties.Resources.it;
            this.itemToolToolStripMenuItem.Name = "itemToolToolStripMenuItem";
            this.itemToolToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.itemToolToolStripMenuItem.Text = "Item Tool";
            this.itemToolToolStripMenuItem.Click += new System.EventHandler(this.OnOpenItemTool);
            // 
            // npcToolToolStripMenuItem
            // 
            this.npcToolToolStripMenuItem.Image = global::LCMT.Properties.Resources.app;
            this.npcToolToolStripMenuItem.Name = "npcToolToolStripMenuItem";
            this.npcToolToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.npcToolToolStripMenuItem.Text = "Npc Tool";
            this.npcToolToolStripMenuItem.Click += new System.EventHandler(this.OnOpenNpcTool);
            // 
            // clientStringToolToolStripMenuItem
            // 
            this.clientStringToolToolStripMenuItem.Image = global::LCMT.Properties.Resources.st;
            this.clientStringToolToolStripMenuItem.Name = "clientStringToolToolStripMenuItem";
            this.clientStringToolToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.clientStringToolToolStripMenuItem.Text = "String Tool";
            this.clientStringToolToolStripMenuItem.Click += new System.EventHandler(this.OnOpenStringTool);
            // 
            // skillToolToolStripMenuItem
            // 
            this.skillToolToolStripMenuItem.Image = global::LCMT.Properties.Resources.SkillIcon1;
            this.skillToolToolStripMenuItem.Name = "skillToolToolStripMenuItem";
            this.skillToolToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.skillToolToolStripMenuItem.Text = "Skill Tool";
            this.skillToolToolStripMenuItem.Click += new System.EventHandler(this.OnOpenSkillTool);
            // 
            // magicToolToolStripMenuItem
            // 
            this.magicToolToolStripMenuItem.Enabled = false;
            this.magicToolToolStripMenuItem.Image = global::LCMT.Properties.Resources.mt;
            this.magicToolToolStripMenuItem.Name = "magicToolToolStripMenuItem";
            this.magicToolToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.magicToolToolStripMenuItem.Text = "Magic Tool";
            this.magicToolToolStripMenuItem.Click += new System.EventHandler(this.OnOpenMagicTool);
            // 
            // luckyDrawToolToolStripMenuItem
            // 
            this.luckyDrawToolToolStripMenuItem.Image = global::LCMT.Properties.Resources.lucky_temp;
            this.luckyDrawToolToolStripMenuItem.Name = "luckyDrawToolToolStripMenuItem";
            this.luckyDrawToolToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.luckyDrawToolToolStripMenuItem.Text = "LuckyDraw Tool";
            this.luckyDrawToolToolStripMenuItem.Click += new System.EventHandler(this.OnOpenLuckyDrawTool);
            // 
            // titleToolToolStripMenuItem
            // 
            this.titleToolToolStripMenuItem.Image = global::LCMT.Properties.Resources.TitleIcon;
            this.titleToolToolStripMenuItem.Name = "titleToolToolStripMenuItem";
            this.titleToolToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.titleToolToolStripMenuItem.Text = "Title Tool";
            this.titleToolToolStripMenuItem.Click += new System.EventHandler(this.OnOpenTitleTool);
            // 
            // playerToolToolStripMenuItem
            // 
            this.playerToolToolStripMenuItem.Image = global::LCMT.Properties.Resources.pt;
            this.playerToolToolStripMenuItem.Name = "playerToolToolStripMenuItem";
            this.playerToolToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.playerToolToolStripMenuItem.Text = "Player Tool";
            this.playerToolToolStripMenuItem.Click += new System.EventHandler(this.OnOpenPlayerTool);
            // 
            // zoneDataToolToolStripMenuItem
            // 
            this.zoneDataToolToolStripMenuItem.Image = global::LCMT.Properties.Resources.zd;
            this.zoneDataToolToolStripMenuItem.Name = "zoneDataToolToolStripMenuItem";
            this.zoneDataToolToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.zoneDataToolToolStripMenuItem.Text = "ZoneData Tool";
            this.zoneDataToolToolStripMenuItem.Click += new System.EventHandler(this.OnOpenZoneTool);
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpMenuItem_Log,
            this.toolStripSeparator3,
            this.HelpMenuItem_About});
            this.HelpMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.HelpMenuItem.ForeColor = System.Drawing.Color.Teal;
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(43, 20);
            this.HelpMenuItem.Text = "Help";
            // 
            // HelpMenuItem_Log
            // 
            this.HelpMenuItem_Log.Name = "HelpMenuItem_Log";
            this.HelpMenuItem_Log.Size = new System.Drawing.Size(125, 22);
            this.HelpMenuItem_Log.Text = "Show Log";
            this.HelpMenuItem_Log.Click += new System.EventHandler(this.OnShowLog);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(122, 6);
            // 
            // HelpMenuItem_About
            // 
            this.HelpMenuItem_About.Name = "HelpMenuItem_About";
            this.HelpMenuItem_About.Size = new System.Drawing.Size(125, 22);
            this.HelpMenuItem_About.Text = "About";
            this.HelpMenuItem_About.Click += new System.EventHandler(this.OnAboutClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusTSLabel,
            this.StatusTSText,
            this.SpringBar,
            this.UpdateReadyLink});
            this.statusStrip1.Location = new System.Drawing.Point(20, 566);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(973, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusTSLabel
            // 
            this.StatusTSLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.StatusTSLabel.Name = "StatusTSLabel";
            this.StatusTSLabel.Size = new System.Drawing.Size(45, 17);
            this.StatusTSLabel.Text = "Status :";
            // 
            // StatusTSText
            // 
            this.StatusTSText.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.StatusTSText.Name = "StatusTSText";
            this.StatusTSText.Size = new System.Drawing.Size(70, 17);
            this.StatusTSText.Text = "Standing By";
            // 
            // SpringBar
            // 
            this.SpringBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.SpringBar.Name = "SpringBar";
            this.SpringBar.Size = new System.Drawing.Size(843, 17);
            this.SpringBar.Spring = true;
            // 
            // UpdateReadyLink
            // 
            this.UpdateReadyLink.ActiveLinkColor = System.Drawing.Color.Green;
            this.UpdateReadyLink.IsLink = true;
            this.UpdateReadyLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.UpdateReadyLink.LinkColor = System.Drawing.Color.Green;
            this.UpdateReadyLink.Name = "UpdateReadyLink";
            this.UpdateReadyLink.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.UpdateReadyLink.RightToLeftAutoMirrorImage = true;
            this.UpdateReadyLink.Size = new System.Drawing.Size(80, 17);
            this.UpdateReadyLink.Text = "Update Ready";
            this.UpdateReadyLink.ToolTipText = "Update Is Ready For Download.";
            this.UpdateReadyLink.Visible = false;
            this.UpdateReadyLink.VisitedLinkColor = System.Drawing.Color.Green;
            // 
            // cMdiContainer1
            // 
            this.cMdiContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cMdiContainer1.Location = new System.Drawing.Point(20, 84);
            this.cMdiContainer1.Name = "cMdiContainer1";
            this.cMdiContainer1.Size = new System.Drawing.Size(973, 482);
            this.cMdiContainer1.TabIndex = 3;
            this.cMdiContainer1.Visible = false;
            // 
            // MultiFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1013, 608);
            this.Controls.Add(this.cMdiContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MultiFrm";
            this.Text = "LastChaos Multitool";
            this.TransparencyKey = System.Drawing.Color.Empty;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoaded);
            this.ResizeEnd += new System.EventHandler(this.OnResizeEnd);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusTSLabel;
        private System.Windows.Forms.ToolStripStatusLabel StatusTSText;
        private System.Windows.Forms.ToolStripStatusLabel SpringBar;
        private System.Windows.Forms.ToolStripStatusLabel UpdateReadyLink;
        private System.Windows.Forms.ToolStripMenuItem DataMenuItem_Init;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem DataMenuItem_Settings;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem_ClientPath;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem_Log;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem_About;
        private System.Windows.Forms.ToolStripMenuItem DataMenuItem_Tables;
        private System.Windows.Forms.ToolStripMenuItem npcToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem titleToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoneDataToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientStringToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem luckyDrawToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skillToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem magicToolToolStripMenuItem;
        private IllTechLibrary.Controls.CMdiContainer cMdiContainer1;
    }
}

