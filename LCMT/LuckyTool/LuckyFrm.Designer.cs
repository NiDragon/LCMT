namespace LCMT.LuckyTool
{
    partial class LuckyFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LuckyFrm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.FileContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DatabaseContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.oneToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.fixNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.GeneralStatsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lb_box = new System.Windows.Forms.ListBox();
            this.lb_boxresult = new System.Windows.Forms.ListBox();
            this.BtnAddBox = new System.Windows.Forms.Button();
            this.BtnRemoveBox = new System.Windows.Forms.Button();
            this.BtnRemoveResultItem = new System.Windows.Forms.Button();
            this.BtnAddResultItem = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbx_random = new System.Windows.Forms.ComboBox();
            this.cb_enable = new System.Windows.Forms.CheckBox();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnUpdateBox = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ProgressWheel = new MetroFramework.Controls.MetroProgressSpinner();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_flag = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_prob = new System.Windows.Forms.TextBox();
            this.tb_upgrade = new System.Windows.Forms.TextBox();
            this.tb_count = new System.Windows.Forms.TextBox();
            this.BtnUpdateReward = new System.Windows.Forms.Button();
            this.BtnRemoveReqItem = new System.Windows.Forms.Button();
            this.BtnAddReqItem = new System.Windows.Forms.Button();
            this.dg_needitem = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_needitem)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileContextMenu,
            this.DatabaseContextMenu});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(900, 25);
            this.toolStrip1.TabIndex = 51;
            this.toolStrip1.Text = "toolStrip1";
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
            this.ExitMenuItem.Size = new System.Drawing.Size(92, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.OnDoExit);
            // 
            // DatabaseContextMenu
            // 
            this.DatabaseContextMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UpdateMenuItem,
            this.InsertMenuItem,
            this.DeleteMenuItem,
            this.toolStripSeparator1,
            this.fixNamesToolStripMenuItem});
            this.DatabaseContextMenu.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.DatabaseContextMenu.ForeColor = System.Drawing.Color.Teal;
            this.DatabaseContextMenu.Name = "DatabaseContextMenu";
            this.DatabaseContextMenu.Size = new System.Drawing.Size(67, 25);
            this.DatabaseContextMenu.Text = "Database";
            // 
            // UpdateMenuItem
            // 
            this.UpdateMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.oneToolStripMenuItem});
            this.UpdateMenuItem.Enabled = false;
            this.UpdateMenuItem.Name = "UpdateMenuItem";
            this.UpdateMenuItem.Size = new System.Drawing.Size(125, 22);
            this.UpdateMenuItem.Tag = "EnableOnConnected";
            this.UpdateMenuItem.Text = "Update";
            this.UpdateMenuItem.Visible = false;
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.allToolStripMenuItem.Text = "All";
            // 
            // oneToolStripMenuItem
            // 
            this.oneToolStripMenuItem.Name = "oneToolStripMenuItem";
            this.oneToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.oneToolStripMenuItem.Text = "One";
            // 
            // InsertMenuItem
            // 
            this.InsertMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem1,
            this.oneToolStripMenuItem1});
            this.InsertMenuItem.Enabled = false;
            this.InsertMenuItem.Name = "InsertMenuItem";
            this.InsertMenuItem.Size = new System.Drawing.Size(125, 22);
            this.InsertMenuItem.Tag = "EnableOnConnected";
            this.InsertMenuItem.Text = "Insert";
            this.InsertMenuItem.Visible = false;
            // 
            // allToolStripMenuItem1
            // 
            this.allToolStripMenuItem1.Name = "allToolStripMenuItem1";
            this.allToolStripMenuItem1.Size = new System.Drawing.Size(96, 22);
            this.allToolStripMenuItem1.Text = "All";
            // 
            // oneToolStripMenuItem1
            // 
            this.oneToolStripMenuItem1.Name = "oneToolStripMenuItem1";
            this.oneToolStripMenuItem1.Size = new System.Drawing.Size(96, 22);
            this.oneToolStripMenuItem1.Text = "One";
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Enabled = false;
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(125, 22);
            this.DeleteMenuItem.Tag = "EnableOnConnected";
            this.DeleteMenuItem.Text = "Delete";
            this.DeleteMenuItem.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(122, 6);
            this.toolStripSeparator1.Visible = false;
            // 
            // fixNamesToolStripMenuItem
            // 
            this.fixNamesToolStripMenuItem.Enabled = false;
            this.fixNamesToolStripMenuItem.Name = "fixNamesToolStripMenuItem";
            this.fixNamesToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.fixNamesToolStripMenuItem.Tag = "EnableOnConnected";
            this.fixNamesToolStripMenuItem.Text = "Fix Names";
            this.fixNamesToolStripMenuItem.Click += new System.EventHandler(this.OnFixNames);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GeneralStatsLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 466);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(900, 22);
            this.statusStrip1.TabIndex = 53;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // GeneralStatsLabel
            // 
            this.GeneralStatsLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.GeneralStatsLabel.Name = "GeneralStatsLabel";
            this.GeneralStatsLabel.Size = new System.Drawing.Size(38, 17);
            this.GeneralStatsLabel.Text = "Stats: ";
            // 
            // lb_box
            // 
            this.lb_box.FormattingEnabled = true;
            this.lb_box.HorizontalScrollbar = true;
            this.lb_box.Location = new System.Drawing.Point(12, 28);
            this.lb_box.Name = "lb_box";
            this.lb_box.Size = new System.Drawing.Size(200, 394);
            this.lb_box.TabIndex = 54;
            this.lb_box.SelectedIndexChanged += new System.EventHandler(this.OnBoxIndexChange);
            // 
            // lb_boxresult
            // 
            this.lb_boxresult.FormattingEnabled = true;
            this.lb_boxresult.HorizontalScrollbar = true;
            this.lb_boxresult.Location = new System.Drawing.Point(218, 28);
            this.lb_boxresult.Name = "lb_boxresult";
            this.lb_boxresult.Size = new System.Drawing.Size(200, 394);
            this.lb_boxresult.TabIndex = 55;
            this.lb_boxresult.SelectedIndexChanged += new System.EventHandler(this.OnBoxResultIndexChange);
            // 
            // BtnAddBox
            // 
            this.BtnAddBox.Location = new System.Drawing.Point(12, 428);
            this.BtnAddBox.Name = "BtnAddBox";
            this.BtnAddBox.Size = new System.Drawing.Size(96, 23);
            this.BtnAddBox.TabIndex = 56;
            this.BtnAddBox.Text = "Add Box";
            this.BtnAddBox.UseVisualStyleBackColor = true;
            this.BtnAddBox.Click += new System.EventHandler(this.OnAddBox);
            // 
            // BtnRemoveBox
            // 
            this.BtnRemoveBox.Location = new System.Drawing.Point(116, 428);
            this.BtnRemoveBox.Name = "BtnRemoveBox";
            this.BtnRemoveBox.Size = new System.Drawing.Size(96, 23);
            this.BtnRemoveBox.TabIndex = 57;
            this.BtnRemoveBox.Text = "Remove Box";
            this.BtnRemoveBox.UseVisualStyleBackColor = true;
            this.BtnRemoveBox.Click += new System.EventHandler(this.OnRemoveBox);
            // 
            // BtnRemoveResultItem
            // 
            this.BtnRemoveResultItem.Location = new System.Drawing.Point(322, 428);
            this.BtnRemoveResultItem.Name = "BtnRemoveResultItem";
            this.BtnRemoveResultItem.Size = new System.Drawing.Size(96, 23);
            this.BtnRemoveResultItem.TabIndex = 59;
            this.BtnRemoveResultItem.Text = "Remove Reward";
            this.BtnRemoveResultItem.UseVisualStyleBackColor = true;
            this.BtnRemoveResultItem.Click += new System.EventHandler(this.OnRemoveResultItem);
            // 
            // BtnAddResultItem
            // 
            this.BtnAddResultItem.Location = new System.Drawing.Point(220, 428);
            this.BtnAddResultItem.Name = "BtnAddResultItem";
            this.BtnAddResultItem.Size = new System.Drawing.Size(96, 23);
            this.BtnAddResultItem.TabIndex = 58;
            this.BtnAddResultItem.Text = "Add Reward";
            this.BtnAddResultItem.UseVisualStyleBackColor = true;
            this.BtnAddResultItem.Click += new System.EventHandler(this.OnAddResultItem);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbx_random);
            this.groupBox1.Controls.Add(this.cb_enable);
            this.groupBox1.Controls.Add(this.tb_name);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_Id);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BtnUpdateBox);
            this.groupBox1.Location = new System.Drawing.Point(685, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 145);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Box Info";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Random : ";
            // 
            // cbx_random
            // 
            this.cbx_random.FormattingEnabled = true;
            this.cbx_random.Items.AddRange(new object[] {
            "Prob",
            "Random",
            "All"});
            this.cbx_random.Location = new System.Drawing.Point(77, 65);
            this.cbx_random.Name = "cbx_random";
            this.cbx_random.Size = new System.Drawing.Size(107, 21);
            this.cbx_random.TabIndex = 7;
            // 
            // cb_enable
            // 
            this.cb_enable.AutoSize = true;
            this.cb_enable.Location = new System.Drawing.Point(77, 93);
            this.cb_enable.Name = "cb_enable";
            this.cb_enable.Size = new System.Drawing.Size(65, 17);
            this.cb_enable.TabIndex = 6;
            this.cb_enable.Text = "Enabled";
            this.cb_enable.UseVisualStyleBackColor = true;
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(77, 39);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(107, 20);
            this.tb_name.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Box Name : ";
            // 
            // tb_Id
            // 
            this.tb_Id.Location = new System.Drawing.Point(77, 13);
            this.tb_Id.Name = "tb_Id";
            this.tb_Id.ReadOnly = true;
            this.tb_Id.Size = new System.Drawing.Size(107, 20);
            this.tb_Id.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Box ID : ";
            // 
            // BtnUpdateBox
            // 
            this.BtnUpdateBox.Location = new System.Drawing.Point(109, 116);
            this.BtnUpdateBox.Name = "BtnUpdateBox";
            this.BtnUpdateBox.Size = new System.Drawing.Size(75, 23);
            this.BtnUpdateBox.TabIndex = 1;
            this.BtnUpdateBox.Text = "Update Box";
            this.BtnUpdateBox.UseVisualStyleBackColor = true;
            this.BtnUpdateBox.Click += new System.EventHandler(this.OnUpdateBox);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.ProgressWheel);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tb_flag);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tb_prob);
            this.groupBox2.Controls.Add(this.tb_upgrade);
            this.groupBox2.Controls.Add(this.tb_count);
            this.groupBox2.Controls.Add(this.BtnUpdateReward);
            this.groupBox2.Location = new System.Drawing.Point(424, 179);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(469, 272);
            this.groupBox2.TabIndex = 61;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reward Item Info";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(188, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "10000 = 100%";
            // 
            // ProgressWheel
            // 
            this.ProgressWheel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressWheel.Location = new System.Drawing.Point(426, 13);
            this.ProgressWheel.Maximum = 100;
            this.ProgressWheel.Name = "ProgressWheel";
            this.ProgressWheel.Size = new System.Drawing.Size(35, 35);
            this.ProgressWheel.TabIndex = 14;
            this.ProgressWheel.UseSelectable = true;
            this.ProgressWheel.Value = 80;
            this.ProgressWheel.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Flag : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Prob : ";
            // 
            // tb_flag
            // 
            this.tb_flag.Location = new System.Drawing.Point(75, 101);
            this.tb_flag.Name = "tb_flag";
            this.tb_flag.Size = new System.Drawing.Size(107, 20);
            this.tb_flag.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Upgrade : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Count : ";
            // 
            // tb_prob
            // 
            this.tb_prob.Location = new System.Drawing.Point(75, 75);
            this.tb_prob.Name = "tb_prob";
            this.tb_prob.Size = new System.Drawing.Size(107, 20);
            this.tb_prob.TabIndex = 11;
            // 
            // tb_upgrade
            // 
            this.tb_upgrade.Location = new System.Drawing.Point(75, 49);
            this.tb_upgrade.Name = "tb_upgrade";
            this.tb_upgrade.Size = new System.Drawing.Size(107, 20);
            this.tb_upgrade.TabIndex = 10;
            // 
            // tb_count
            // 
            this.tb_count.Location = new System.Drawing.Point(75, 23);
            this.tb_count.Name = "tb_count";
            this.tb_count.Size = new System.Drawing.Size(107, 20);
            this.tb_count.TabIndex = 9;
            // 
            // BtnUpdateReward
            // 
            this.BtnUpdateReward.Location = new System.Drawing.Point(388, 243);
            this.BtnUpdateReward.Name = "BtnUpdateReward";
            this.BtnUpdateReward.Size = new System.Drawing.Size(75, 23);
            this.BtnUpdateReward.TabIndex = 0;
            this.BtnUpdateReward.Text = "Update Item";
            this.BtnUpdateReward.UseVisualStyleBackColor = true;
            this.BtnUpdateReward.Click += new System.EventHandler(this.OnUpdateResultItem);
            // 
            // BtnRemoveReqItem
            // 
            this.BtnRemoveReqItem.Location = new System.Drawing.Point(559, 151);
            this.BtnRemoveReqItem.Name = "BtnRemoveReqItem";
            this.BtnRemoveReqItem.Size = new System.Drawing.Size(120, 23);
            this.BtnRemoveReqItem.TabIndex = 64;
            this.BtnRemoveReqItem.Text = "Remove Require Item";
            this.BtnRemoveReqItem.UseVisualStyleBackColor = true;
            this.BtnRemoveReqItem.Click += new System.EventHandler(this.OnRemoveRequireItem);
            // 
            // BtnAddReqItem
            // 
            this.BtnAddReqItem.Location = new System.Drawing.Point(424, 151);
            this.BtnAddReqItem.Name = "BtnAddReqItem";
            this.BtnAddReqItem.Size = new System.Drawing.Size(120, 23);
            this.BtnAddReqItem.TabIndex = 63;
            this.BtnAddReqItem.Text = "Add Required Item";
            this.BtnAddReqItem.UseVisualStyleBackColor = true;
            this.BtnAddReqItem.Click += new System.EventHandler(this.OnAddRequireItem);
            // 
            // dg_needitem
            // 
            this.dg_needitem.AllowUserToAddRows = false;
            this.dg_needitem.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Purple;
            this.dg_needitem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_needitem.BackgroundColor = System.Drawing.Color.White;
            this.dg_needitem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_needitem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dg_needitem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_needitem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.colID,
            this.Column3,
            this.Column2});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_needitem.DefaultCellStyle = dataGridViewCellStyle3;
            this.dg_needitem.EnableHeadersVisualStyles = false;
            this.dg_needitem.Location = new System.Drawing.Point(424, 28);
            this.dg_needitem.Name = "dg_needitem";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_needitem.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dg_needitem.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Purple;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.dg_needitem.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dg_needitem.RowTemplate.Height = 32;
            this.dg_needitem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_needitem.Size = new System.Drawing.Size(255, 117);
            this.dg_needitem.TabIndex = 65;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 32;
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Width = 75;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Name";
            this.Column3.Name = "Column3";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Count";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 42;
            // 
            // LuckyFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(900, 488);
            this.Controls.Add(this.dg_needitem);
            this.Controls.Add(this.BtnRemoveReqItem);
            this.Controls.Add(this.BtnAddReqItem);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnRemoveResultItem);
            this.Controls.Add(this.BtnAddResultItem);
            this.Controls.Add(this.BtnRemoveBox);
            this.Controls.Add(this.BtnAddBox);
            this.Controls.Add(this.lb_boxresult);
            this.Controls.Add(this.lb_box);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(916, 527);
            this.MinimumSize = new System.Drawing.Size(916, 527);
            this.Name = "LuckyFrm";
            this.Text = "LuckyDraw Tool";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_needitem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DatabaseContextMenu;
        private System.Windows.Forms.ToolStripMenuItem UpdateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InsertMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem oneToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel GeneralStatsLabel;
        private System.Windows.Forms.ListBox lb_box;
        private System.Windows.Forms.ListBox lb_boxresult;
        private System.Windows.Forms.Button BtnAddBox;
        private System.Windows.Forms.Button BtnRemoveBox;
        private System.Windows.Forms.Button BtnRemoveResultItem;
        private System.Windows.Forms.Button BtnAddResultItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbx_random;
        private System.Windows.Forms.CheckBox cb_enable;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_Id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnUpdateBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BtnUpdateReward;
        private System.Windows.Forms.Button BtnRemoveReqItem;
        private System.Windows.Forms.Button BtnAddReqItem;
        private System.Windows.Forms.DataGridView dg_needitem;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_flag;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_prob;
        private System.Windows.Forms.TextBox tb_upgrade;
        private System.Windows.Forms.TextBox tb_count;
        private MetroFramework.Controls.MetroProgressSpinner ProgressWheel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem fixNamesToolStripMenuItem;
    }
}