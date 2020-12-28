namespace LCMT.MagicTool
{
    partial class MagicTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MagicTool));
            this.menuStrip1 = new System.Windows.Forms.ToolStrip();
            this.FileContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AMenuStrip = new System.Windows.Forms.StatusStrip();
            this.GeneralStatsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnDeleteLevel = new System.Windows.Forms.Button();
            this.BtnAddLevel = new System.Windows.Forms.Button();
            this.lb_magicLevels = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ProgSpin = new MetroFramework.Controls.MetroProgressSpinner();
            this.BtnRemoveSkill = new System.Windows.Forms.Button();
            this.BtnAddSkill = new System.Windows.Forms.Button();
            this.lb_magic = new System.Windows.Forms.ListBox();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tb_htp = new System.Windows.Forms.TextBox();
            this.tb_hsp = new System.Windows.Forms.TextBox();
            this.tb_ptp = new System.Windows.Forms.TextBox();
            this.tb_psp = new System.Windows.Forms.TextBox();
            this.cb_atkAttr = new System.Windows.Forms.ComboBox();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.tb_maxLevel = new System.Windows.Forms.TextBox();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.cb_subType = new System.Windows.Forms.ComboBox();
            this.cb_damageType = new System.Windows.Forms.ComboBox();
            this.cb_hitType = new System.Windows.Forms.ComboBox();
            this.chk_toggle = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BtnRevert = new System.Windows.Forms.Button();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.tb_power = new System.Windows.Forms.TextBox();
            this.tb_hitrate = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.AMenuStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileContextMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(744, 25);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileContextMenu
            // 
            this.FileContextMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitMenuItem});
            this.FileContextMenu.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FileContextMenu.ForeColor = System.Drawing.Color.Teal;
            this.FileContextMenu.Name = "FileContextMenu";
            this.FileContextMenu.Size = new System.Drawing.Size(37, 25);
            this.FileContextMenu.Text = "File";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // AMenuStrip
            // 
            this.AMenuStrip.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.AMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GeneralStatsLabel});
            this.AMenuStrip.Location = new System.Drawing.Point(0, 572);
            this.AMenuStrip.Name = "AMenuStrip";
            this.AMenuStrip.Size = new System.Drawing.Size(744, 22);
            this.AMenuStrip.TabIndex = 56;
            this.AMenuStrip.Text = "statusStrip1";
            // 
            // GeneralStatsLabel
            // 
            this.GeneralStatsLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.GeneralStatsLabel.Name = "GeneralStatsLabel";
            this.GeneralStatsLabel.Size = new System.Drawing.Size(38, 17);
            this.GeneralStatsLabel.Text = "Stats: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.BtnDeleteLevel);
            this.groupBox2.Controls.Add(this.BtnAddLevel);
            this.groupBox2.Controls.Add(this.lb_magicLevels);
            this.groupBox2.Location = new System.Drawing.Point(242, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(224, 539);
            this.groupBox2.TabIndex = 61;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Magic Level";
            // 
            // BtnDeleteLevel
            // 
            this.BtnDeleteLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnDeleteLevel.Location = new System.Drawing.Point(143, 510);
            this.BtnDeleteLevel.Name = "BtnDeleteLevel";
            this.BtnDeleteLevel.Size = new System.Drawing.Size(75, 23);
            this.BtnDeleteLevel.TabIndex = 5;
            this.BtnDeleteLevel.Text = "Delete Level";
            this.BtnDeleteLevel.UseVisualStyleBackColor = true;
            // 
            // BtnAddLevel
            // 
            this.BtnAddLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnAddLevel.Location = new System.Drawing.Point(6, 510);
            this.BtnAddLevel.Name = "BtnAddLevel";
            this.BtnAddLevel.Size = new System.Drawing.Size(75, 23);
            this.BtnAddLevel.TabIndex = 4;
            this.BtnAddLevel.Text = "New Level";
            this.BtnAddLevel.UseVisualStyleBackColor = true;
            // 
            // lb_magicLevels
            // 
            this.lb_magicLevels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_magicLevels.FormattingEnabled = true;
            this.lb_magicLevels.Location = new System.Drawing.Point(6, 19);
            this.lb_magicLevels.Name = "lb_magicLevels";
            this.lb_magicLevels.Size = new System.Drawing.Size(212, 485);
            this.lb_magicLevels.TabIndex = 1;
            this.lb_magicLevels.SelectedIndexChanged += new System.EventHandler(this.MagicLevelsSelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.ProgSpin);
            this.groupBox1.Controls.Add(this.BtnRemoveSkill);
            this.groupBox1.Controls.Add(this.BtnAddSkill);
            this.groupBox1.Controls.Add(this.lb_magic);
            this.groupBox1.Controls.Add(this.tb_search);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 539);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Magic";
            // 
            // ProgSpin
            // 
            this.ProgSpin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgSpin.Location = new System.Drawing.Point(95, 244);
            this.ProgSpin.Maximum = 100;
            this.ProgSpin.Name = "ProgSpin";
            this.ProgSpin.Size = new System.Drawing.Size(35, 35);
            this.ProgSpin.TabIndex = 60;
            this.ProgSpin.UseSelectable = true;
            this.ProgSpin.Value = 80;
            this.ProgSpin.Visible = false;
            // 
            // BtnRemoveSkill
            // 
            this.BtnRemoveSkill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnRemoveSkill.Location = new System.Drawing.Point(143, 510);
            this.BtnRemoveSkill.Name = "BtnRemoveSkill";
            this.BtnRemoveSkill.Size = new System.Drawing.Size(75, 23);
            this.BtnRemoveSkill.TabIndex = 3;
            this.BtnRemoveSkill.Text = "Delete Magic";
            this.BtnRemoveSkill.UseVisualStyleBackColor = true;
            // 
            // BtnAddSkill
            // 
            this.BtnAddSkill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnAddSkill.Location = new System.Drawing.Point(6, 510);
            this.BtnAddSkill.Name = "BtnAddSkill";
            this.BtnAddSkill.Size = new System.Drawing.Size(75, 23);
            this.BtnAddSkill.TabIndex = 2;
            this.BtnAddSkill.Text = "New Magic";
            this.BtnAddSkill.UseVisualStyleBackColor = true;
            // 
            // lb_magic
            // 
            this.lb_magic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_magic.FormattingEnabled = true;
            this.lb_magic.Location = new System.Drawing.Point(6, 45);
            this.lb_magic.Name = "lb_magic";
            this.lb_magic.Size = new System.Drawing.Size(212, 459);
            this.lb_magic.TabIndex = 1;
            this.lb_magic.SelectedIndexChanged += new System.EventHandler(this.MaigcsSelectedIndexChanged);
            // 
            // tb_search
            // 
            this.tb_search.Location = new System.Drawing.Point(6, 19);
            this.tb_search.Name = "tb_search";
            this.tb_search.Size = new System.Drawing.Size(212, 20);
            this.tb_search.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tb_htp);
            this.groupBox3.Controls.Add(this.tb_hsp);
            this.groupBox3.Controls.Add(this.tb_ptp);
            this.groupBox3.Controls.Add(this.tb_psp);
            this.groupBox3.Controls.Add(this.cb_atkAttr);
            this.groupBox3.Controls.Add(this.tb_name);
            this.groupBox3.Controls.Add(this.tb_maxLevel);
            this.groupBox3.Controls.Add(this.cb_type);
            this.groupBox3.Controls.Add(this.cb_subType);
            this.groupBox3.Controls.Add(this.cb_damageType);
            this.groupBox3.Controls.Add(this.cb_hitType);
            this.groupBox3.Controls.Add(this.chk_toggle);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(472, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(260, 333);
            this.groupBox3.TabIndex = 62;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Magic Info";
            // 
            // tb_htp
            // 
            this.tb_htp.Location = new System.Drawing.Point(96, 284);
            this.tb_htp.Name = "tb_htp";
            this.tb_htp.Size = new System.Drawing.Size(143, 20);
            this.tb_htp.TabIndex = 24;
            // 
            // tb_hsp
            // 
            this.tb_hsp.Location = new System.Drawing.Point(96, 258);
            this.tb_hsp.Name = "tb_hsp";
            this.tb_hsp.Size = new System.Drawing.Size(143, 20);
            this.tb_hsp.TabIndex = 23;
            // 
            // tb_ptp
            // 
            this.tb_ptp.Location = new System.Drawing.Point(96, 232);
            this.tb_ptp.Name = "tb_ptp";
            this.tb_ptp.Size = new System.Drawing.Size(143, 20);
            this.tb_ptp.TabIndex = 22;
            // 
            // tb_psp
            // 
            this.tb_psp.Location = new System.Drawing.Point(96, 207);
            this.tb_psp.Name = "tb_psp";
            this.tb_psp.Size = new System.Drawing.Size(143, 20);
            this.tb_psp.TabIndex = 21;
            // 
            // cb_atkAttr
            // 
            this.cb_atkAttr.FormattingEnabled = true;
            this.cb_atkAttr.Items.AddRange(new object[] {
            "0 - None",
            "1 - Fire",
            "2 - Water",
            "3 - Earth",
            "4 - Wind",
            "5 - Dark",
            "6 - Light",
            "7 - Random"});
            this.cb_atkAttr.Location = new System.Drawing.Point(96, 180);
            this.cb_atkAttr.Name = "cb_atkAttr";
            this.cb_atkAttr.Size = new System.Drawing.Size(143, 21);
            this.cb_atkAttr.TabIndex = 19;
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(96, 23);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(143, 20);
            this.tb_name.TabIndex = 18;
            // 
            // tb_maxLevel
            // 
            this.tb_maxLevel.Location = new System.Drawing.Point(96, 47);
            this.tb_maxLevel.Name = "tb_maxLevel";
            this.tb_maxLevel.Size = new System.Drawing.Size(143, 20);
            this.tb_maxLevel.TabIndex = 17;
            // 
            // cb_type
            // 
            this.cb_type.FormattingEnabled = true;
            this.cb_type.Items.AddRange(new object[] {
            "0 - Stat",
            "1 - Attribute",
            "2 - Assist",
            "3 - Attack",
            "4 - Recover",
            "5 - Cure",
            "6 - Other",
            "7 - Reduce",
            "8 - Immune",
            "9 - Castle War",
            "10 - Money"});
            this.cb_type.Location = new System.Drawing.Point(96, 73);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(143, 21);
            this.cb_type.TabIndex = 16;
            this.cb_type.SelectedIndexChanged += new System.EventHandler(this.TypeSelectedIndexChanged);
            // 
            // cb_subType
            // 
            this.cb_subType.FormattingEnabled = true;
            this.cb_subType.Location = new System.Drawing.Point(96, 99);
            this.cb_subType.Name = "cb_subType";
            this.cb_subType.Size = new System.Drawing.Size(143, 21);
            this.cb_subType.TabIndex = 15;
            // 
            // cb_damageType
            // 
            this.cb_damageType.FormattingEnabled = true;
            this.cb_damageType.Location = new System.Drawing.Point(96, 127);
            this.cb_damageType.Name = "cb_damageType";
            this.cb_damageType.Size = new System.Drawing.Size(143, 21);
            this.cb_damageType.TabIndex = 14;
            // 
            // cb_hitType
            // 
            this.cb_hitType.FormattingEnabled = true;
            this.cb_hitType.Items.AddRange(new object[] {
            "0 - Constant",
            "1 - Variable"});
            this.cb_hitType.Location = new System.Drawing.Point(96, 154);
            this.cb_hitType.Name = "cb_hitType";
            this.cb_hitType.Size = new System.Drawing.Size(143, 21);
            this.cb_hitType.TabIndex = 13;
            // 
            // chk_toggle
            // 
            this.chk_toggle.AutoSize = true;
            this.chk_toggle.Location = new System.Drawing.Point(96, 310);
            this.chk_toggle.Name = "chk_toggle";
            this.chk_toggle.Size = new System.Drawing.Size(59, 17);
            this.chk_toggle.TabIndex = 12;
            this.chk_toggle.Text = "Toggle";
            this.chk_toggle.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(59, 287);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "htp : ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(57, 261);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "hsp : ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(59, 235);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "ptp : ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(57, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "psp : ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Attribute : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Hit Type : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Damage Type : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Sub Type : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Type : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Max Level : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name : ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BtnRevert);
            this.groupBox4.Controls.Add(this.BtnUpdate);
            this.groupBox4.Controls.Add(this.tb_power);
            this.groupBox4.Controls.Add(this.tb_hitrate);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Location = new System.Drawing.Point(472, 367);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 173);
            this.groupBox4.TabIndex = 63;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Level Info";
            // 
            // BtnRevert
            // 
            this.BtnRevert.Location = new System.Drawing.Point(98, 144);
            this.BtnRevert.Name = "BtnRevert";
            this.BtnRevert.Size = new System.Drawing.Size(75, 23);
            this.BtnRevert.TabIndex = 32;
            this.BtnRevert.Text = "Revert";
            this.BtnRevert.UseVisualStyleBackColor = true;
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Location = new System.Drawing.Point(179, 144);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(75, 23);
            this.BtnUpdate.TabIndex = 31;
            this.BtnUpdate.Text = "Update";
            this.BtnUpdate.UseVisualStyleBackColor = true;
            // 
            // tb_power
            // 
            this.tb_power.Location = new System.Drawing.Point(96, 19);
            this.tb_power.Name = "tb_power";
            this.tb_power.Size = new System.Drawing.Size(143, 20);
            this.tb_power.TabIndex = 30;
            // 
            // tb_hitrate
            // 
            this.tb_hitrate.Location = new System.Drawing.Point(96, 43);
            this.tb_hitrate.Name = "tb_hitrate";
            this.tb_hitrate.Size = new System.Drawing.Size(143, 20);
            this.tb_hitrate.TabIndex = 29;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(44, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Power : ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(43, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Hitrate : ";
            // 
            // MagicTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(744, 594);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AMenuStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(760, 633);
            this.MinimumSize = new System.Drawing.Size(760, 633);
            this.Name = "MagicTool";
            this.Text = "Magic Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MagicTool_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.AMenuStrip.ResumeLayout(false);
            this.AMenuStrip.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.StatusStrip AMenuStrip;
        private System.Windows.Forms.ToolStripStatusLabel GeneralStatsLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BtnDeleteLevel;
        private System.Windows.Forms.Button BtnAddLevel;
        private System.Windows.Forms.ListBox lb_magicLevels;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroProgressSpinner ProgSpin;
        private System.Windows.Forms.Button BtnRemoveSkill;
        private System.Windows.Forms.Button BtnAddSkill;
        private System.Windows.Forms.ListBox lb_magic;
        private System.Windows.Forms.TextBox tb_search;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tb_htp;
        private System.Windows.Forms.TextBox tb_hsp;
        private System.Windows.Forms.TextBox tb_ptp;
        private System.Windows.Forms.TextBox tb_psp;
        private System.Windows.Forms.ComboBox cb_atkAttr;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.TextBox tb_maxLevel;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.ComboBox cb_subType;
        private System.Windows.Forms.ComboBox cb_damageType;
        private System.Windows.Forms.ComboBox cb_hitType;
        private System.Windows.Forms.CheckBox chk_toggle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnRevert;
        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.TextBox tb_power;
        private System.Windows.Forms.TextBox tb_hitrate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
    }
}