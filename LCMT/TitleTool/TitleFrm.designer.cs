namespace LCMT.TitleTool
{
    partial class TitleFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TitleFrm));
            this.FileDropDown = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TitlesList = new System.Windows.Forms.ListBox();
            this.OptionsList = new System.Windows.Forms.ListBox();
            this.BasicBox = new System.Windows.Forms.GroupBox();
            this.PickItem = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.PreviewText = new System.Windows.Forms.Label();
            this.SampleLabel = new System.Windows.Forms.Label();
            this.ForegroundSelectLabel = new System.Windows.Forms.Label();
            this.BackgroundSelectLabel = new System.Windows.Forms.Label();
            this.ForegroundPanel = new System.Windows.Forms.Panel();
            this.BackgroundPanel = new System.Windows.Forms.Panel();
            this.ItemIDLabel = new System.Windows.Forms.Label();
            this.TitleIDLabel = new System.Windows.Forms.Label();
            this.TB_ITEMID = new System.Windows.Forms.TextBox();
            this.TB_TID = new System.Windows.Forms.TextBox();
            this.CB_Eanbled = new System.Windows.Forms.CheckBox();
            this.TB_TITLE = new System.Windows.Forms.TextBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.EffectsBox = new System.Windows.Forms.GroupBox();
            this.FindEffectBtn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DamageValue = new System.Windows.Forms.TextBox();
            this.AttackValue = new System.Windows.Forms.TextBox();
            this.EffectNameValue = new System.Windows.Forms.TextBox();
            this.StatsBox = new System.Windows.Forms.GroupBox();
            this.Seal4Label = new System.Windows.Forms.Label();
            this.Seal3Label = new System.Windows.Forms.Label();
            this.Seal2Label = new System.Windows.Forms.Label();
            this.Seal1Label = new System.Windows.Forms.Label();
            this.Seal0Label = new System.Windows.Forms.Label();
            this.Seal4Level = new System.Windows.Forms.TextBox();
            this.Seal4Value = new System.Windows.Forms.TextBox();
            this.Seal3Level = new System.Windows.Forms.TextBox();
            this.Seal3Value = new System.Windows.Forms.TextBox();
            this.Seal2Level = new System.Windows.Forms.TextBox();
            this.Seal2Value = new System.Windows.Forms.TextBox();
            this.Seal1Level = new System.Windows.Forms.TextBox();
            this.Seal1Value = new System.Windows.Forms.TextBox();
            this.Seal0Level = new System.Windows.Forms.TextBox();
            this.Seal0Value = new System.Windows.Forms.TextBox();
            this.FlagsBox = new System.Windows.Forms.GroupBox();
            this.CastleLabel = new System.Windows.Forms.Label();
            this.FlagsLabel = new System.Windows.Forms.Label();
            this.CastleValue = new System.Windows.Forms.ComboBox();
            this.UseTimeLabel = new System.Windows.Forms.Label();
            this.FlagsValue = new System.Windows.Forms.TextBox();
            this.UseTimeValue = new System.Windows.Forms.TextBox();
            this.UpdateBtn = new System.Windows.Forms.Button();
            this.NewTitleBtn = new System.Windows.Forms.Button();
            this.TitleSearchText = new System.Windows.Forms.TextBox();
            this.OptionSearchText = new System.Windows.Forms.TextBox();
            this.pickColor = new System.Windows.Forms.ColorDialog();
            this.RemoveBtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.ToolStrip();
            this.FileContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AMenuStrip = new System.Windows.Forms.StatusStrip();
            this.GeneralStatsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressWheel = new MetroFramework.Controls.MetroProgressSpinner();
            this.BasicBox.SuspendLayout();
            this.panel3.SuspendLayout();
            this.EffectsBox.SuspendLayout();
            this.StatsBox.SuspendLayout();
            this.FlagsBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.AMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileDropDown
            // 
            this.FileDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveBtn,
            this.toolStripSeparator2,
            this.ExitBtn});
            this.FileDropDown.Name = "FileDropDown";
            this.FileDropDown.Size = new System.Drawing.Size(37, 25);
            this.FileDropDown.Text = "File";
            // 
            // SaveBtn
            // 
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(98, 22);
            this.SaveBtn.Text = "Save";
            this.SaveBtn.Click += new System.EventHandler(this.OnSave);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(95, 6);
            // 
            // ExitBtn
            // 
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(98, 22);
            this.ExitBtn.Text = "Exit";
            this.ExitBtn.Click += new System.EventHandler(this.OnExit);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // TitlesList
            // 
            this.TitlesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TitlesList.FormattingEnabled = true;
            this.TitlesList.HorizontalScrollbar = true;
            this.TitlesList.Location = new System.Drawing.Point(12, 66);
            this.TitlesList.Name = "TitlesList";
            this.TitlesList.Size = new System.Drawing.Size(175, 394);
            this.TitlesList.TabIndex = 2;
            this.TitlesList.SelectedIndexChanged += new System.EventHandler(this.OnSelectedTitleChanged);
            // 
            // OptionsList
            // 
            this.OptionsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionsList.FormattingEnabled = true;
            this.OptionsList.HorizontalScrollbar = true;
            this.OptionsList.Location = new System.Drawing.Point(734, 66);
            this.OptionsList.Name = "OptionsList";
            this.OptionsList.Size = new System.Drawing.Size(173, 394);
            this.OptionsList.TabIndex = 3;
            // 
            // BasicBox
            // 
            this.BasicBox.Controls.Add(this.PickItem);
            this.BasicBox.Controls.Add(this.panel3);
            this.BasicBox.Controls.Add(this.SampleLabel);
            this.BasicBox.Controls.Add(this.ForegroundSelectLabel);
            this.BasicBox.Controls.Add(this.BackgroundSelectLabel);
            this.BasicBox.Controls.Add(this.ForegroundPanel);
            this.BasicBox.Controls.Add(this.BackgroundPanel);
            this.BasicBox.Controls.Add(this.ItemIDLabel);
            this.BasicBox.Controls.Add(this.TitleIDLabel);
            this.BasicBox.Controls.Add(this.TB_ITEMID);
            this.BasicBox.Controls.Add(this.TB_TID);
            this.BasicBox.Controls.Add(this.CB_Eanbled);
            this.BasicBox.Controls.Add(this.TB_TITLE);
            this.BasicBox.Controls.Add(this.TitleLabel);
            this.BasicBox.Location = new System.Drawing.Point(193, 40);
            this.BasicBox.Name = "BasicBox";
            this.BasicBox.Size = new System.Drawing.Size(281, 211);
            this.BasicBox.TabIndex = 5;
            this.BasicBox.TabStop = false;
            this.BasicBox.Text = "Basic";
            // 
            // PickItem
            // 
            this.PickItem.Location = new System.Drawing.Point(165, 52);
            this.PickItem.Name = "PickItem";
            this.PickItem.Size = new System.Drawing.Size(53, 20);
            this.PickItem.TabIndex = 12;
            this.PickItem.Text = "Pick";
            this.PickItem.UseVisualStyleBackColor = true;
            this.PickItem.Click += new System.EventHandler(this.OnPickItemClicked);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.PreviewText);
            this.panel3.Location = new System.Drawing.Point(61, 170);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(205, 23);
            this.panel3.TabIndex = 8;
            // 
            // PreviewText
            // 
            this.PreviewText.Location = new System.Drawing.Point(-1, -1);
            this.PreviewText.Name = "PreviewText";
            this.PreviewText.Size = new System.Drawing.Size(205, 23);
            this.PreviewText.TabIndex = 0;
            this.PreviewText.Text = "Preview";
            this.PreviewText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SampleLabel
            // 
            this.SampleLabel.AutoSize = true;
            this.SampleLabel.Location = new System.Drawing.Point(7, 173);
            this.SampleLabel.Name = "SampleLabel";
            this.SampleLabel.Size = new System.Drawing.Size(48, 13);
            this.SampleLabel.TabIndex = 11;
            this.SampleLabel.Text = "Sample :";
            // 
            // ForegroundSelectLabel
            // 
            this.ForegroundSelectLabel.Location = new System.Drawing.Point(166, 127);
            this.ForegroundSelectLabel.Name = "ForegroundSelectLabel";
            this.ForegroundSelectLabel.Size = new System.Drawing.Size(74, 23);
            this.ForegroundSelectLabel.TabIndex = 10;
            this.ForegroundSelectLabel.Text = "Foreground";
            // 
            // BackgroundSelectLabel
            // 
            this.BackgroundSelectLabel.Location = new System.Drawing.Point(48, 127);
            this.BackgroundSelectLabel.Name = "BackgroundSelectLabel";
            this.BackgroundSelectLabel.Size = new System.Drawing.Size(74, 23);
            this.BackgroundSelectLabel.TabIndex = 9;
            this.BackgroundSelectLabel.Text = "Background";
            // 
            // ForegroundPanel
            // 
            this.ForegroundPanel.BackColor = System.Drawing.Color.Yellow;
            this.ForegroundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ForegroundPanel.Location = new System.Drawing.Point(128, 127);
            this.ForegroundPanel.Name = "ForegroundPanel";
            this.ForegroundPanel.Size = new System.Drawing.Size(32, 32);
            this.ForegroundPanel.TabIndex = 8;
            this.ForegroundPanel.BackColorChanged += new System.EventHandler(this.OnForegroundBackColorChanged);
            this.ForegroundPanel.Click += new System.EventHandler(this.OnForegroundClicked);
            // 
            // BackgroundPanel
            // 
            this.BackgroundPanel.BackColor = System.Drawing.Color.Black;
            this.BackgroundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BackgroundPanel.Location = new System.Drawing.Point(10, 127);
            this.BackgroundPanel.Name = "BackgroundPanel";
            this.BackgroundPanel.Size = new System.Drawing.Size(32, 32);
            this.BackgroundPanel.TabIndex = 7;
            this.BackgroundPanel.BackColorChanged += new System.EventHandler(this.OnBackgroundBackColorChanged);
            this.BackgroundPanel.Click += new System.EventHandler(this.OnBackgroundClicked);
            // 
            // ItemIDLabel
            // 
            this.ItemIDLabel.AutoSize = true;
            this.ItemIDLabel.Location = new System.Drawing.Point(7, 55);
            this.ItemIDLabel.Name = "ItemIDLabel";
            this.ItemIDLabel.Size = new System.Drawing.Size(53, 13);
            this.ItemIDLabel.TabIndex = 6;
            this.ItemIDLabel.Text = "ITEM ID :";
            // 
            // TitleIDLabel
            // 
            this.TitleIDLabel.AutoSize = true;
            this.TitleIDLabel.Location = new System.Drawing.Point(7, 29);
            this.TitleIDLabel.Name = "TitleIDLabel";
            this.TitleIDLabel.Size = new System.Drawing.Size(24, 13);
            this.TitleIDLabel.TabIndex = 5;
            this.TitleIDLabel.Text = "ID :";
            // 
            // TB_ITEMID
            // 
            this.TB_ITEMID.Location = new System.Drawing.Point(66, 52);
            this.TB_ITEMID.Name = "TB_ITEMID";
            this.TB_ITEMID.Size = new System.Drawing.Size(94, 19);
            this.TB_ITEMID.TabIndex = 4;
            // 
            // TB_TID
            // 
            this.TB_TID.Location = new System.Drawing.Point(41, 26);
            this.TB_TID.Name = "TB_TID";
            this.TB_TID.Size = new System.Drawing.Size(119, 19);
            this.TB_TID.TabIndex = 3;
            // 
            // CB_Eanbled
            // 
            this.CB_Eanbled.AutoSize = true;
            this.CB_Eanbled.Location = new System.Drawing.Point(9, 104);
            this.CB_Eanbled.Name = "CB_Eanbled";
            this.CB_Eanbled.Size = new System.Drawing.Size(59, 17);
            this.CB_Eanbled.TabIndex = 2;
            this.CB_Eanbled.Text = "Enable";
            this.CB_Eanbled.UseVisualStyleBackColor = true;
            // 
            // TB_TITLE
            // 
            this.TB_TITLE.Location = new System.Drawing.Point(41, 78);
            this.TB_TITLE.Name = "TB_TITLE";
            this.TB_TITLE.Size = new System.Drawing.Size(225, 19);
            this.TB_TITLE.TabIndex = 1;
            this.TB_TITLE.TextChanged += new System.EventHandler(this.OnTitleTextChange);
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(6, 81);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(36, 13);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Title : ";
            // 
            // EffectsBox
            // 
            this.EffectsBox.Controls.Add(this.FindEffectBtn);
            this.EffectsBox.Controls.Add(this.label9);
            this.EffectsBox.Controls.Add(this.label8);
            this.EffectsBox.Controls.Add(this.label7);
            this.EffectsBox.Controls.Add(this.DamageValue);
            this.EffectsBox.Controls.Add(this.AttackValue);
            this.EffectsBox.Controls.Add(this.EffectNameValue);
            this.EffectsBox.Location = new System.Drawing.Point(480, 40);
            this.EffectsBox.Name = "EffectsBox";
            this.EffectsBox.Size = new System.Drawing.Size(248, 211);
            this.EffectsBox.TabIndex = 6;
            this.EffectsBox.TabStop = false;
            this.EffectsBox.Text = "Effects";
            // 
            // FindEffectBtn
            // 
            this.FindEffectBtn.Location = new System.Drawing.Point(206, 23);
            this.FindEffectBtn.Name = "FindEffectBtn";
            this.FindEffectBtn.Size = new System.Drawing.Size(36, 20);
            this.FindEffectBtn.TabIndex = 6;
            this.FindEffectBtn.Text = "Find";
            this.FindEffectBtn.UseVisualStyleBackColor = true;
            this.FindEffectBtn.Click += new System.EventHandler(this.OnEffectClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Damage :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Attack :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Effect Name :";
            // 
            // DamageValue
            // 
            this.DamageValue.Location = new System.Drawing.Point(81, 84);
            this.DamageValue.Name = "DamageValue";
            this.DamageValue.Size = new System.Drawing.Size(119, 19);
            this.DamageValue.TabIndex = 2;
            // 
            // AttackValue
            // 
            this.AttackValue.Location = new System.Drawing.Point(81, 54);
            this.AttackValue.Name = "AttackValue";
            this.AttackValue.Size = new System.Drawing.Size(119, 19);
            this.AttackValue.TabIndex = 1;
            // 
            // EffectNameValue
            // 
            this.EffectNameValue.Location = new System.Drawing.Point(81, 23);
            this.EffectNameValue.Name = "EffectNameValue";
            this.EffectNameValue.Size = new System.Drawing.Size(119, 19);
            this.EffectNameValue.TabIndex = 0;
            // 
            // StatsBox
            // 
            this.StatsBox.Controls.Add(this.ProgressWheel);
            this.StatsBox.Controls.Add(this.Seal4Label);
            this.StatsBox.Controls.Add(this.Seal3Label);
            this.StatsBox.Controls.Add(this.Seal2Label);
            this.StatsBox.Controls.Add(this.Seal1Label);
            this.StatsBox.Controls.Add(this.Seal0Label);
            this.StatsBox.Controls.Add(this.Seal4Level);
            this.StatsBox.Controls.Add(this.Seal4Value);
            this.StatsBox.Controls.Add(this.Seal3Level);
            this.StatsBox.Controls.Add(this.Seal3Value);
            this.StatsBox.Controls.Add(this.Seal2Level);
            this.StatsBox.Controls.Add(this.Seal2Value);
            this.StatsBox.Controls.Add(this.Seal1Level);
            this.StatsBox.Controls.Add(this.Seal1Value);
            this.StatsBox.Controls.Add(this.Seal0Level);
            this.StatsBox.Controls.Add(this.Seal0Value);
            this.StatsBox.Controls.Add(this.FlagsBox);
            this.StatsBox.Location = new System.Drawing.Point(193, 257);
            this.StatsBox.Name = "StatsBox";
            this.StatsBox.Size = new System.Drawing.Size(535, 174);
            this.StatsBox.TabIndex = 7;
            this.StatsBox.TabStop = false;
            this.StatsBox.Text = "Stats";
            // 
            // Seal4Label
            // 
            this.Seal4Label.AutoSize = true;
            this.Seal4Label.Location = new System.Drawing.Point(20, 137);
            this.Seal4Label.Name = "Seal4Label";
            this.Seal4Label.Size = new System.Drawing.Size(40, 13);
            this.Seal4Label.TabIndex = 15;
            this.Seal4Label.Text = "Seal 4:";
            // 
            // Seal3Label
            // 
            this.Seal3Label.AutoSize = true;
            this.Seal3Label.Location = new System.Drawing.Point(20, 111);
            this.Seal3Label.Name = "Seal3Label";
            this.Seal3Label.Size = new System.Drawing.Size(40, 13);
            this.Seal3Label.TabIndex = 14;
            this.Seal3Label.Text = "Seal 3:";
            // 
            // Seal2Label
            // 
            this.Seal2Label.AutoSize = true;
            this.Seal2Label.Location = new System.Drawing.Point(20, 85);
            this.Seal2Label.Name = "Seal2Label";
            this.Seal2Label.Size = new System.Drawing.Size(40, 13);
            this.Seal2Label.TabIndex = 13;
            this.Seal2Label.Text = "Seal 2:";
            // 
            // Seal1Label
            // 
            this.Seal1Label.AutoSize = true;
            this.Seal1Label.Location = new System.Drawing.Point(20, 59);
            this.Seal1Label.Name = "Seal1Label";
            this.Seal1Label.Size = new System.Drawing.Size(40, 13);
            this.Seal1Label.TabIndex = 12;
            this.Seal1Label.Text = "Seal 1:";
            // 
            // Seal0Label
            // 
            this.Seal0Label.AutoSize = true;
            this.Seal0Label.Location = new System.Drawing.Point(20, 33);
            this.Seal0Label.Name = "Seal0Label";
            this.Seal0Label.Size = new System.Drawing.Size(40, 13);
            this.Seal0Label.TabIndex = 11;
            this.Seal0Label.Text = "Seal 0:";
            // 
            // Seal4Level
            // 
            this.Seal4Level.Location = new System.Drawing.Point(226, 134);
            this.Seal4Level.Name = "Seal4Level";
            this.Seal4Level.Size = new System.Drawing.Size(27, 19);
            this.Seal4Level.TabIndex = 10;
            // 
            // Seal4Value
            // 
            this.Seal4Value.Location = new System.Drawing.Point(66, 134);
            this.Seal4Value.Name = "Seal4Value";
            this.Seal4Value.Size = new System.Drawing.Size(154, 19);
            this.Seal4Value.TabIndex = 9;
            // 
            // Seal3Level
            // 
            this.Seal3Level.Location = new System.Drawing.Point(226, 108);
            this.Seal3Level.Name = "Seal3Level";
            this.Seal3Level.Size = new System.Drawing.Size(27, 19);
            this.Seal3Level.TabIndex = 8;
            // 
            // Seal3Value
            // 
            this.Seal3Value.Location = new System.Drawing.Point(66, 108);
            this.Seal3Value.Name = "Seal3Value";
            this.Seal3Value.Size = new System.Drawing.Size(154, 19);
            this.Seal3Value.TabIndex = 7;
            // 
            // Seal2Level
            // 
            this.Seal2Level.Location = new System.Drawing.Point(226, 82);
            this.Seal2Level.Name = "Seal2Level";
            this.Seal2Level.Size = new System.Drawing.Size(27, 19);
            this.Seal2Level.TabIndex = 6;
            // 
            // Seal2Value
            // 
            this.Seal2Value.Location = new System.Drawing.Point(66, 82);
            this.Seal2Value.Name = "Seal2Value";
            this.Seal2Value.Size = new System.Drawing.Size(154, 19);
            this.Seal2Value.TabIndex = 5;
            // 
            // Seal1Level
            // 
            this.Seal1Level.Location = new System.Drawing.Point(226, 56);
            this.Seal1Level.Name = "Seal1Level";
            this.Seal1Level.Size = new System.Drawing.Size(27, 19);
            this.Seal1Level.TabIndex = 4;
            // 
            // Seal1Value
            // 
            this.Seal1Value.Location = new System.Drawing.Point(66, 56);
            this.Seal1Value.Name = "Seal1Value";
            this.Seal1Value.Size = new System.Drawing.Size(154, 19);
            this.Seal1Value.TabIndex = 3;
            // 
            // Seal0Level
            // 
            this.Seal0Level.Location = new System.Drawing.Point(226, 30);
            this.Seal0Level.Name = "Seal0Level";
            this.Seal0Level.Size = new System.Drawing.Size(27, 19);
            this.Seal0Level.TabIndex = 2;
            // 
            // Seal0Value
            // 
            this.Seal0Value.Location = new System.Drawing.Point(66, 30);
            this.Seal0Value.Name = "Seal0Value";
            this.Seal0Value.Size = new System.Drawing.Size(154, 19);
            this.Seal0Value.TabIndex = 1;
            // 
            // FlagsBox
            // 
            this.FlagsBox.Controls.Add(this.CastleLabel);
            this.FlagsBox.Controls.Add(this.FlagsLabel);
            this.FlagsBox.Controls.Add(this.CastleValue);
            this.FlagsBox.Controls.Add(this.UseTimeLabel);
            this.FlagsBox.Controls.Add(this.FlagsValue);
            this.FlagsBox.Controls.Add(this.UseTimeValue);
            this.FlagsBox.Location = new System.Drawing.Point(287, 10);
            this.FlagsBox.Name = "FlagsBox";
            this.FlagsBox.Size = new System.Drawing.Size(242, 113);
            this.FlagsBox.TabIndex = 0;
            this.FlagsBox.TabStop = false;
            this.FlagsBox.Text = "Flags";
            // 
            // CastleLabel
            // 
            this.CastleLabel.AutoSize = true;
            this.CastleLabel.Location = new System.Drawing.Point(35, 77);
            this.CastleLabel.Name = "CastleLabel";
            this.CastleLabel.Size = new System.Drawing.Size(42, 13);
            this.CastleLabel.TabIndex = 9;
            this.CastleLabel.Text = "Castle :";
            // 
            // FlagsLabel
            // 
            this.FlagsLabel.AutoSize = true;
            this.FlagsLabel.Location = new System.Drawing.Point(39, 51);
            this.FlagsLabel.Name = "FlagsLabel";
            this.FlagsLabel.Size = new System.Drawing.Size(38, 13);
            this.FlagsLabel.TabIndex = 8;
            this.FlagsLabel.Text = "Flags :";
            // 
            // CastleValue
            // 
            this.CastleValue.FormattingEnabled = true;
            this.CastleValue.Items.AddRange(new object[] {
            "0 - No Castle",
            "4 - Dratan Castle",
            "7 - Merac Castle"});
            this.CastleValue.Location = new System.Drawing.Point(83, 74);
            this.CastleValue.Name = "CastleValue";
            this.CastleValue.Size = new System.Drawing.Size(121, 21);
            this.CastleValue.TabIndex = 2;
            // 
            // UseTimeLabel
            // 
            this.UseTimeLabel.AutoSize = true;
            this.UseTimeLabel.Location = new System.Drawing.Point(20, 25);
            this.UseTimeLabel.Name = "UseTimeLabel";
            this.UseTimeLabel.Size = new System.Drawing.Size(58, 13);
            this.UseTimeLabel.TabIndex = 7;
            this.UseTimeLabel.Text = "Use Time :";
            // 
            // FlagsValue
            // 
            this.FlagsValue.Location = new System.Drawing.Point(83, 48);
            this.FlagsValue.Name = "FlagsValue";
            this.FlagsValue.Size = new System.Drawing.Size(121, 19);
            this.FlagsValue.TabIndex = 1;
            // 
            // UseTimeValue
            // 
            this.UseTimeValue.Location = new System.Drawing.Point(83, 22);
            this.UseTimeValue.Name = "UseTimeValue";
            this.UseTimeValue.Size = new System.Drawing.Size(121, 19);
            this.UseTimeValue.TabIndex = 0;
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UpdateBtn.Location = new System.Drawing.Point(653, 437);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(75, 23);
            this.UpdateBtn.TabIndex = 8;
            this.UpdateBtn.Text = "Update";
            this.UpdateBtn.UseVisualStyleBackColor = true;
            this.UpdateBtn.Click += new System.EventHandler(this.OnUpdateClick);
            // 
            // NewTitleBtn
            // 
            this.NewTitleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewTitleBtn.Location = new System.Drawing.Point(572, 437);
            this.NewTitleBtn.Name = "NewTitleBtn";
            this.NewTitleBtn.Size = new System.Drawing.Size(75, 23);
            this.NewTitleBtn.TabIndex = 9;
            this.NewTitleBtn.Text = "New";
            this.NewTitleBtn.UseVisualStyleBackColor = true;
            this.NewTitleBtn.Click += new System.EventHandler(this.OnNewClick);
            // 
            // TitleSearchText
            // 
            this.TitleSearchText.Location = new System.Drawing.Point(12, 40);
            this.TitleSearchText.Name = "TitleSearchText";
            this.TitleSearchText.Size = new System.Drawing.Size(175, 19);
            this.TitleSearchText.TabIndex = 10;
            this.TitleSearchText.TextChanged += new System.EventHandler(this.TitleSearchText_TextChanged);
            // 
            // OptionSearchText
            // 
            this.OptionSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionSearchText.Location = new System.Drawing.Point(734, 40);
            this.OptionSearchText.Name = "OptionSearchText";
            this.OptionSearchText.Size = new System.Drawing.Size(173, 19);
            this.OptionSearchText.TabIndex = 11;
            this.OptionSearchText.TextChanged += new System.EventHandler(this.OptionSearchText_TextChanged);
            // 
            // RemoveBtn
            // 
            this.RemoveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RemoveBtn.Location = new System.Drawing.Point(491, 437);
            this.RemoveBtn.Name = "RemoveBtn";
            this.RemoveBtn.Size = new System.Drawing.Size(75, 23);
            this.RemoveBtn.TabIndex = 12;
            this.RemoveBtn.Text = "Remove";
            this.RemoveBtn.UseVisualStyleBackColor = true;
            this.RemoveBtn.Click += new System.EventHandler(this.OnRemoveClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileContextMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(919, 25);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileContextMenu
            // 
            this.FileContextMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveMenuItem,
            this.SaveAsMenuItem,
            this.toolStripSeparator4,
            this.ExitMenuItem});
            this.FileContextMenu.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FileContextMenu.ForeColor = System.Drawing.Color.Teal;
            this.FileContextMenu.Name = "FileContextMenu";
            this.FileContextMenu.Size = new System.Drawing.Size(37, 25);
            this.FileContextMenu.Text = "File";
            // 
            // SaveMenuItem
            // 
            this.SaveMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SaveMenuItem.Name = "SaveMenuItem";
            this.SaveMenuItem.Size = new System.Drawing.Size(112, 22);
            this.SaveMenuItem.Text = "Save";
            this.SaveMenuItem.Click += new System.EventHandler(this.OnSave);
            // 
            // SaveAsMenuItem
            // 
            this.SaveAsMenuItem.Name = "SaveAsMenuItem";
            this.SaveAsMenuItem.Size = new System.Drawing.Size(112, 22);
            this.SaveAsMenuItem.Text = "Save As";
            this.SaveAsMenuItem.Click += new System.EventHandler(this.OnSaveAs);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(109, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(112, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.OnExit);
            // 
            // MainMenuStrip
            // 
            this.AMenuStrip.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.AMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GeneralStatsLabel});
            this.AMenuStrip.Location = new System.Drawing.Point(0, 474);
            this.AMenuStrip.Name = "MainMenuStrip";
            this.AMenuStrip.Size = new System.Drawing.Size(919, 22);
            this.AMenuStrip.TabIndex = 54;
            this.AMenuStrip.Text = "statusStrip1";
            // 
            // GeneralStatsLabel
            // 
            this.GeneralStatsLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.GeneralStatsLabel.Name = "GeneralStatsLabel";
            this.GeneralStatsLabel.Size = new System.Drawing.Size(38, 17);
            this.GeneralStatsLabel.Text = "Stats: ";
            // 
            // ProgressWheel
            // 
            this.ProgressWheel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressWheel.Location = new System.Drawing.Point(493, 129);
            this.ProgressWheel.Maximum = 100;
            this.ProgressWheel.Name = "ProgressWheel";
            this.ProgressWheel.Size = new System.Drawing.Size(35, 35);
            this.ProgressWheel.TabIndex = 55;
            this.ProgressWheel.UseSelectable = true;
            this.ProgressWheel.Value = 80;
            this.ProgressWheel.Visible = false;
            // 
            // MainFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(919, 496);
            this.Controls.Add(this.AMenuStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.RemoveBtn);
            this.Controls.Add(this.OptionSearchText);
            this.Controls.Add(this.TitleSearchText);
            this.Controls.Add(this.NewTitleBtn);
            this.Controls.Add(this.UpdateBtn);
            this.Controls.Add(this.StatsBox);
            this.Controls.Add(this.EffectsBox);
            this.Controls.Add(this.BasicBox);
            this.Controls.Add(this.OptionsList);
            this.Controls.Add(this.TitlesList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(935, 535);
            this.MinimumSize = new System.Drawing.Size(935, 535);
            this.Name = "MainFrm";
            this.Text = "Title Tool";
            this.BasicBox.ResumeLayout(false);
            this.BasicBox.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.EffectsBox.ResumeLayout(false);
            this.EffectsBox.PerformLayout();
            this.StatsBox.ResumeLayout(false);
            this.StatsBox.PerformLayout();
            this.FlagsBox.ResumeLayout(false);
            this.FlagsBox.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.AMenuStrip.ResumeLayout(false);
            this.AMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem FileDropDown;
        private System.Windows.Forms.ToolStripMenuItem SaveBtn;
        private System.Windows.Forms.ToolStripMenuItem ExitBtn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ListBox TitlesList;
        private System.Windows.Forms.ListBox OptionsList;
        private System.Windows.Forms.GroupBox BasicBox;
        private System.Windows.Forms.CheckBox CB_Eanbled;
        private System.Windows.Forms.TextBox TB_TITLE;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.GroupBox EffectsBox;
        private System.Windows.Forms.GroupBox StatsBox;
        private System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.Button NewTitleBtn;
        private System.Windows.Forms.TextBox TitleSearchText;
        private System.Windows.Forms.TextBox OptionSearchText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label PreviewText;
        private System.Windows.Forms.Label SampleLabel;
        private System.Windows.Forms.Label ForegroundSelectLabel;
        private System.Windows.Forms.Label BackgroundSelectLabel;
        private System.Windows.Forms.Panel ForegroundPanel;
        private System.Windows.Forms.Panel BackgroundPanel;
        private System.Windows.Forms.Label ItemIDLabel;
        private System.Windows.Forms.Label TitleIDLabel;
        private System.Windows.Forms.TextBox TB_ITEMID;
        private System.Windows.Forms.TextBox TB_TID;
        private System.Windows.Forms.ColorDialog pickColor;
        private System.Windows.Forms.Button PickItem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox DamageValue;
        private System.Windows.Forms.TextBox AttackValue;
        private System.Windows.Forms.TextBox EffectNameValue;
        private System.Windows.Forms.Button FindEffectBtn;
        private System.Windows.Forms.GroupBox FlagsBox;
        private System.Windows.Forms.Label CastleLabel;
        private System.Windows.Forms.Label FlagsLabel;
        private System.Windows.Forms.ComboBox CastleValue;
        private System.Windows.Forms.Label UseTimeLabel;
        private System.Windows.Forms.TextBox FlagsValue;
        private System.Windows.Forms.TextBox UseTimeValue;
        private System.Windows.Forms.Label Seal4Label;
        private System.Windows.Forms.Label Seal3Label;
        private System.Windows.Forms.Label Seal2Label;
        private System.Windows.Forms.Label Seal1Label;
        private System.Windows.Forms.Label Seal0Label;
        private System.Windows.Forms.TextBox Seal4Level;
        private System.Windows.Forms.TextBox Seal4Value;
        private System.Windows.Forms.TextBox Seal3Level;
        private System.Windows.Forms.TextBox Seal3Value;
        private System.Windows.Forms.TextBox Seal2Level;
        private System.Windows.Forms.TextBox Seal2Value;
        private System.Windows.Forms.TextBox Seal1Level;
        private System.Windows.Forms.TextBox Seal1Value;
        private System.Windows.Forms.TextBox Seal0Level;
        private System.Windows.Forms.TextBox Seal0Value;
        private System.Windows.Forms.Button RemoveBtn;
        private System.Windows.Forms.ToolStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileContextMenu;
        private System.Windows.Forms.ToolStripMenuItem SaveMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsMenuItem;
        private System.Windows.Forms.StatusStrip AMenuStrip;
        private System.Windows.Forms.ToolStripStatusLabel GeneralStatsLabel;
        private MetroFramework.Controls.MetroProgressSpinner ProgressWheel;
    }
}

