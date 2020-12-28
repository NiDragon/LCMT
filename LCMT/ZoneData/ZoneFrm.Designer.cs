using System.Windows.Forms;

namespace LCMT.ZoneData
{
    partial class ZoneFrm
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZoneFrm));
            this.lb_zoneList = new System.Windows.Forms.ListBox();
            this.addZoneBtn = new System.Windows.Forms.Button();
            this.delZoneBtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.ToolStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenuItem_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FileMenuItem_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenuItem_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolMenuItem_ReloadStr = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_zoneFile = new System.Windows.Forms.TextBox();
            this.tb_texName1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.findZoneBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_texName2 = new System.Windows.Forms.TextBox();
            this.lb_zoneExtra = new System.Windows.Forms.ListBox();
            this.delSubZoneBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_zoneType = new System.Windows.Forms.TextBox();
            this.addSubZoneBtn = new System.Windows.Forms.Button();
            this.selectExtraNameBtn = new System.Windows.Forms.Button();
            this.selectZoneNameBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_loadMul = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_loadSteps = new System.Windows.Forms.TextBox();
            this.updateBtn = new System.Windows.Forms.Button();
            this.findTex1Btn = new System.Windows.Forms.Button();
            this.findTex2Btn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_zoneList
            // 
            this.lb_zoneList.FormattingEnabled = true;
            this.lb_zoneList.Location = new System.Drawing.Point(187, 30);
            this.lb_zoneList.Name = "lb_zoneList";
            this.lb_zoneList.Size = new System.Drawing.Size(235, 355);
            this.lb_zoneList.TabIndex = 1;
            this.lb_zoneList.SelectedIndexChanged += new System.EventHandler(this.OnZoneListSelectionChange);
            this.lb_zoneList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnZoneListKeyDown);
            this.lb_zoneList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnZoneListDblClick);
            // 
            // addZoneBtn
            // 
            this.addZoneBtn.Location = new System.Drawing.Point(187, 391);
            this.addZoneBtn.Name = "addZoneBtn";
            this.addZoneBtn.Size = new System.Drawing.Size(75, 23);
            this.addZoneBtn.TabIndex = 2;
            this.addZoneBtn.Text = "Add";
            this.addZoneBtn.UseVisualStyleBackColor = true;
            this.addZoneBtn.Click += new System.EventHandler(this.OnDoAddZone);
            // 
            // delZoneBtn
            // 
            this.delZoneBtn.Location = new System.Drawing.Point(347, 391);
            this.delZoneBtn.Name = "delZoneBtn";
            this.delZoneBtn.Size = new System.Drawing.Size(75, 23);
            this.delZoneBtn.TabIndex = 3;
            this.delZoneBtn.Text = "Delete";
            this.delZoneBtn.UseVisualStyleBackColor = true;
            this.delZoneBtn.Click += new System.EventHandler(this.OnDoDelete);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.ToolMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem_Open,
            this.toolStripSeparator2,
            this.FileMenuItem_SaveAs,
            this.FileMenuItem_Save,
            this.toolStripSeparator1,
            this.FileMenuItem_Exit});
            this.FileMenuItem.ForeColor = System.Drawing.Color.Teal;
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 25);
            this.FileMenuItem.Text = "File";
            // 
            // FileMenuItem_Open
            // 
            this.FileMenuItem_Open.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FileMenuItem_Open.Name = "FileMenuItem_Open";
            this.FileMenuItem_Open.Size = new System.Drawing.Size(180, 22);
            this.FileMenuItem_Open.Text = "Open";
            this.FileMenuItem_Open.Click += new System.EventHandler(this.OnOpenZoneData);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // FileMenuItem_SaveAs
            // 
            this.FileMenuItem_SaveAs.Name = "FileMenuItem_SaveAs";
            this.FileMenuItem_SaveAs.Size = new System.Drawing.Size(180, 22);
            this.FileMenuItem_SaveAs.Text = "Save As";
            this.FileMenuItem_SaveAs.Click += new System.EventHandler(this.OnSaveAs);
            // 
            // FileMenuItem_Save
            // 
            this.FileMenuItem_Save.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FileMenuItem_Save.Name = "FileMenuItem_Save";
            this.FileMenuItem_Save.Size = new System.Drawing.Size(180, 22);
            this.FileMenuItem_Save.Text = "Save";
            this.FileMenuItem_Save.Click += new System.EventHandler(this.OnSave);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // FileMenuItem_Exit
            // 
            this.FileMenuItem_Exit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FileMenuItem_Exit.Name = "FileMenuItem_Exit";
            this.FileMenuItem_Exit.Size = new System.Drawing.Size(180, 22);
            this.FileMenuItem_Exit.Text = "Exit";
            this.FileMenuItem_Exit.Click += new System.EventHandler(this.OnExit);
            // 
            // ToolMenuItem
            // 
            this.ToolMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolMenuItem_ReloadStr});
            this.ToolMenuItem.ForeColor = System.Drawing.Color.Teal;
            this.ToolMenuItem.Name = "ToolMenuItem";
            this.ToolMenuItem.Size = new System.Drawing.Size(46, 25);
            this.ToolMenuItem.Text = "Tools";
            // 
            // ToolMenuItem_ReloadStr
            // 
            this.ToolMenuItem_ReloadStr.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ToolMenuItem_ReloadStr.Name = "ToolMenuItem_ReloadStr";
            this.ToolMenuItem_ReloadStr.Size = new System.Drawing.Size(180, 22);
            this.ToolMenuItem_ReloadStr.Text = "Reload Strings";
            this.ToolMenuItem_ReloadStr.Click += new System.EventHandler(this.OnReloadStrings);
            // 
            // tb_zoneFile
            // 
            this.tb_zoneFile.Location = new System.Drawing.Point(12, 85);
            this.tb_zoneFile.Name = "tb_zoneFile";
            this.tb_zoneFile.Size = new System.Drawing.Size(138, 19);
            this.tb_zoneFile.TabIndex = 5;
            // 
            // tb_texName1
            // 
            this.tb_texName1.Location = new System.Drawing.Point(12, 125);
            this.tb_texName1.Name = "tb_texName1";
            this.tb_texName1.Size = new System.Drawing.Size(138, 19);
            this.tb_texName1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Zone File :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Texture 1:";
            // 
            // findZoneBtn
            // 
            this.findZoneBtn.BackColor = System.Drawing.Color.White;
            this.findZoneBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findZoneBtn.Image = ((System.Drawing.Image)(resources.GetObject("findZoneBtn.Image")));
            this.findZoneBtn.Location = new System.Drawing.Point(157, 82);
            this.findZoneBtn.Name = "findZoneBtn";
            this.findZoneBtn.Size = new System.Drawing.Size(26, 26);
            this.findZoneBtn.TabIndex = 9;
            this.findZoneBtn.UseVisualStyleBackColor = false;
            this.findZoneBtn.Click += new System.EventHandler(this.OnFindZone);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Texture 2:";
            // 
            // tb_texName2
            // 
            this.tb_texName2.Location = new System.Drawing.Point(12, 166);
            this.tb_texName2.Name = "tb_texName2";
            this.tb_texName2.Size = new System.Drawing.Size(138, 19);
            this.tb_texName2.TabIndex = 11;
            // 
            // lb_zoneExtra
            // 
            this.lb_zoneExtra.FormattingEnabled = true;
            this.lb_zoneExtra.Location = new System.Drawing.Point(437, 30);
            this.lb_zoneExtra.Name = "lb_zoneExtra";
            this.lb_zoneExtra.Size = new System.Drawing.Size(235, 355);
            this.lb_zoneExtra.TabIndex = 14;
            this.lb_zoneExtra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnZoneExtraKeyDown);
            this.lb_zoneExtra.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnZoneExtraDblClick);
            // 
            // delSubZoneBtn
            // 
            this.delSubZoneBtn.Location = new System.Drawing.Point(597, 391);
            this.delSubZoneBtn.Name = "delSubZoneBtn";
            this.delSubZoneBtn.Size = new System.Drawing.Size(75, 23);
            this.delSubZoneBtn.TabIndex = 16;
            this.delSubZoneBtn.Text = "Delete";
            this.delSubZoneBtn.UseVisualStyleBackColor = true;
            this.delSubZoneBtn.Click += new System.EventHandler(this.OnDoDeleteSubZone);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Zone Type:";
            // 
            // tb_zoneType
            // 
            this.tb_zoneType.Location = new System.Drawing.Point(12, 46);
            this.tb_zoneType.Name = "tb_zoneType";
            this.tb_zoneType.Size = new System.Drawing.Size(138, 19);
            this.tb_zoneType.TabIndex = 17;
            // 
            // addSubZoneBtn
            // 
            this.addSubZoneBtn.Location = new System.Drawing.Point(437, 391);
            this.addSubZoneBtn.Name = "addSubZoneBtn";
            this.addSubZoneBtn.Size = new System.Drawing.Size(75, 23);
            this.addSubZoneBtn.TabIndex = 15;
            this.addSubZoneBtn.Text = "Add";
            this.addSubZoneBtn.UseVisualStyleBackColor = true;
            this.addSubZoneBtn.Click += new System.EventHandler(this.OnDoAddSubZone);
            // 
            // selectExtraNameBtn
            // 
            this.selectExtraNameBtn.Location = new System.Drawing.Point(517, 391);
            this.selectExtraNameBtn.Name = "selectExtraNameBtn";
            this.selectExtraNameBtn.Size = new System.Drawing.Size(75, 23);
            this.selectExtraNameBtn.TabIndex = 19;
            this.selectExtraNameBtn.Text = "Set Name";
            this.selectExtraNameBtn.UseVisualStyleBackColor = true;
            this.selectExtraNameBtn.Click += new System.EventHandler(this.OnSelectExtraName);
            // 
            // selectZoneNameBtn
            // 
            this.selectZoneNameBtn.Location = new System.Drawing.Point(267, 391);
            this.selectZoneNameBtn.Name = "selectZoneNameBtn";
            this.selectZoneNameBtn.Size = new System.Drawing.Size(75, 23);
            this.selectZoneNameBtn.TabIndex = 20;
            this.selectZoneNameBtn.Text = "Set Name";
            this.selectZoneNameBtn.UseVisualStyleBackColor = true;
            this.selectZoneNameBtn.Click += new System.EventHandler(this.OnSelectZoneName);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 233);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Load Mul :";
            // 
            // tb_loadMul
            // 
            this.tb_loadMul.Location = new System.Drawing.Point(12, 249);
            this.tb_loadMul.Name = "tb_loadMul";
            this.tb_loadMul.Size = new System.Drawing.Size(138, 19);
            this.tb_loadMul.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Loading Steps :";
            // 
            // tb_loadSteps
            // 
            this.tb_loadSteps.Location = new System.Drawing.Point(12, 208);
            this.tb_loadSteps.Name = "tb_loadSteps";
            this.tb_loadSteps.Size = new System.Drawing.Size(138, 19);
            this.tb_loadSteps.TabIndex = 21;
            // 
            // updateBtn
            // 
            this.updateBtn.Location = new System.Drawing.Point(12, 276);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(138, 22);
            this.updateBtn.TabIndex = 25;
            this.updateBtn.Text = "Update";
            this.updateBtn.UseVisualStyleBackColor = true;
            this.updateBtn.Click += new System.EventHandler(this.OnDoUpdate);
            // 
            // findTex1Btn
            // 
            this.findTex1Btn.BackColor = System.Drawing.Color.White;
            this.findTex1Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findTex1Btn.Image = ((System.Drawing.Image)(resources.GetObject("findTex1Btn.Image")));
            this.findTex1Btn.Location = new System.Drawing.Point(157, 122);
            this.findTex1Btn.Name = "findTex1Btn";
            this.findTex1Btn.Size = new System.Drawing.Size(26, 26);
            this.findTex1Btn.TabIndex = 26;
            this.findTex1Btn.UseVisualStyleBackColor = false;
            this.findTex1Btn.Click += new System.EventHandler(this.OnFindTex1Btn);
            // 
            // findTex2Btn
            // 
            this.findTex2Btn.BackColor = System.Drawing.Color.White;
            this.findTex2Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findTex2Btn.Image = ((System.Drawing.Image)(resources.GetObject("findTex2Btn.Image")));
            this.findTex2Btn.Location = new System.Drawing.Point(157, 162);
            this.findTex2Btn.Name = "findTex2Btn";
            this.findTex2Btn.Size = new System.Drawing.Size(26, 26);
            this.findTex2Btn.TabIndex = 27;
            this.findTex2Btn.UseVisualStyleBackColor = false;
            this.findTex2Btn.Click += new System.EventHandler(this.OnFindTex2Btn);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.statusStrip1.Location = new System.Drawing.Point(0, 422);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(684, 22);
            this.statusStrip1.TabIndex = 54;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ZoneFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(684, 444);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.findTex2Btn);
            this.Controls.Add(this.findTex1Btn);
            this.Controls.Add(this.updateBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_loadMul);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_loadSteps);
            this.Controls.Add(this.selectZoneNameBtn);
            this.Controls.Add(this.selectExtraNameBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_zoneType);
            this.Controls.Add(this.delSubZoneBtn);
            this.Controls.Add(this.addSubZoneBtn);
            this.Controls.Add(this.lb_zoneExtra);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_texName2);
            this.Controls.Add(this.findZoneBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_texName1);
            this.Controls.Add(this.tb_zoneFile);
            this.Controls.Add(this.delZoneBtn);
            this.Controls.Add(this.addZoneBtn);
            this.Controls.Add(this.lb_zoneList);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 483);
            this.MinimumSize = new System.Drawing.Size(700, 483);
            this.Name = "ZoneFrm";
            this.Text = "Zone Data Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private ListBox lb_zoneList;
        private Button addZoneBtn;
        private Button delZoneBtn;
        private ToolStrip menuStrip1;
        private ToolStripMenuItem FileMenuItem;
        private ToolStripMenuItem FileMenuItem_Open;
        private ToolStripMenuItem FileMenuItem_Save;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem FileMenuItem_Exit;
        private TextBox tb_zoneFile;
        private TextBox tb_texName1;
        private Label label1;
        private Label label2;
        private Button findZoneBtn;
        private Label label3;
        private TextBox tb_texName2;
        private ListBox lb_zoneExtra;
        private Button delSubZoneBtn;
        private Label label4;
        private TextBox tb_zoneType;
        #endregion

        private Button addSubZoneBtn;
        private Button selectExtraNameBtn;
        private Button selectZoneNameBtn;
        private Label label5;
        private TextBox tb_loadMul;
        private Label label6;
        private TextBox tb_loadSteps;
        private Button updateBtn;
        private Button findTex1Btn;
        private Button findTex2Btn;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem FileMenuItem_SaveAs;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem ToolMenuItem;
        private ToolStripMenuItem ToolMenuItem_ReloadStr;
    }
}

