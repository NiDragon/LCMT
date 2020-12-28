namespace LCMT.StringTool
{
    partial class StringFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StringFrm));
            this.gb_strings = new System.Windows.Forms.GroupBox();
            this.ProgressWheel = new MetroFramework.Controls.MetroProgressSpinner();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.lb_strings = new System.Windows.Forms.ListBox();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.gb_editString = new System.Windows.Forms.GroupBox();
            this.updateBtn = new System.Windows.Forms.Button();
            this.tb_selected = new System.Windows.Forms.TextBox();
            this.AMenuStrip = new System.Windows.Forms.StatusStrip();
            this.GeneralStatsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.FileContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gb_strings.SuspendLayout();
            this.gb_editString.SuspendLayout();
            this.AMenuStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_strings
            // 
            this.gb_strings.Controls.Add(this.ProgressWheel);
            this.gb_strings.Controls.Add(this.tb_search);
            this.gb_strings.Controls.Add(this.lb_strings);
            this.gb_strings.Controls.Add(this.deleteBtn);
            this.gb_strings.Controls.Add(this.addBtn);
            this.gb_strings.Location = new System.Drawing.Point(12, 28);
            this.gb_strings.Name = "gb_strings";
            this.gb_strings.Size = new System.Drawing.Size(287, 381);
            this.gb_strings.TabIndex = 6;
            this.gb_strings.TabStop = false;
            this.gb_strings.Text = "Strings";
            // 
            // ProgressWheel
            // 
            this.ProgressWheel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressWheel.Location = new System.Drawing.Point(126, 178);
            this.ProgressWheel.Maximum = 100;
            this.ProgressWheel.Name = "ProgressWheel";
            this.ProgressWheel.Size = new System.Drawing.Size(35, 35);
            this.ProgressWheel.TabIndex = 15;
            this.ProgressWheel.UseSelectable = true;
            this.ProgressWheel.Value = 80;
            this.ProgressWheel.Visible = false;
            // 
            // tb_search
            // 
            this.tb_search.Location = new System.Drawing.Point(6, 19);
            this.tb_search.Name = "tb_search";
            this.tb_search.Size = new System.Drawing.Size(275, 19);
            this.tb_search.TabIndex = 6;
            this.tb_search.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // lb_strings
            // 
            this.lb_strings.FormattingEnabled = true;
            this.lb_strings.Location = new System.Drawing.Point(6, 44);
            this.lb_strings.Name = "lb_strings";
            this.lb_strings.Size = new System.Drawing.Size(275, 303);
            this.lb_strings.TabIndex = 3;
            this.lb_strings.SelectedIndexChanged += new System.EventHandler(this.OnSelectionChanged);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(205, 352);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(76, 23);
            this.deleteBtn.TabIndex = 5;
            this.deleteBtn.Text = "Delete";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.OnDelete);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(6, 352);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(76, 23);
            this.addBtn.TabIndex = 4;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.OnAddClick);
            // 
            // gb_editString
            // 
            this.gb_editString.Controls.Add(this.updateBtn);
            this.gb_editString.Controls.Add(this.tb_selected);
            this.gb_editString.Location = new System.Drawing.Point(13, 415);
            this.gb_editString.Name = "gb_editString";
            this.gb_editString.Size = new System.Drawing.Size(287, 53);
            this.gb_editString.TabIndex = 2;
            this.gb_editString.TabStop = false;
            this.gb_editString.Text = "String Value";
            // 
            // updateBtn
            // 
            this.updateBtn.Location = new System.Drawing.Point(206, 17);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(75, 23);
            this.updateBtn.TabIndex = 1;
            this.updateBtn.Text = "Update";
            this.updateBtn.UseVisualStyleBackColor = true;
            this.updateBtn.Click += new System.EventHandler(this.OnUpdate);
            // 
            // tb_selected
            // 
            this.tb_selected.Location = new System.Drawing.Point(6, 19);
            this.tb_selected.Name = "tb_selected";
            this.tb_selected.Size = new System.Drawing.Size(194, 19);
            this.tb_selected.TabIndex = 0;
            // 
            // MainMenuStrip
            // 
            this.AMenuStrip.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.AMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GeneralStatsLabel});
            this.AMenuStrip.Location = new System.Drawing.Point(0, 474);
            this.AMenuStrip.Name = "MainMenuStrip";
            this.AMenuStrip.Size = new System.Drawing.Size(312, 22);
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
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileContextMenu});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(312, 25);
            this.toolStrip1.TabIndex = 57;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // FileContextMenu
            // 
            this.FileContextMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.ExitMenuItem});
            this.FileContextMenu.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FileContextMenu.ForeColor = System.Drawing.Color.Teal;
            this.FileContextMenu.Name = "FileContextMenu";
            this.FileContextMenu.Size = new System.Drawing.Size(37, 25);
            this.FileContextMenu.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnSave);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(94, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(97, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.OnExit);
            // 
            // StringFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(312, 496);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.AMenuStrip);
            this.Controls.Add(this.gb_strings);
            this.Controls.Add(this.gb_editString);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(328, 535);
            this.MinimumSize = new System.Drawing.Size(328, 535);
            this.Name = "StringFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Client String Tool";
            this.gb_strings.ResumeLayout(false);
            this.gb_strings.PerformLayout();
            this.gb_editString.ResumeLayout(false);
            this.gb_editString.PerformLayout();
            this.AMenuStrip.ResumeLayout(false);
            this.AMenuStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_selected;
        private System.Windows.Forms.GroupBox gb_editString;
        private System.Windows.Forms.Button updateBtn;
        private System.Windows.Forms.ListBox lb_strings;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.GroupBox gb_strings;
        private System.Windows.Forms.TextBox tb_search;
        private System.Windows.Forms.StatusStrip AMenuStrip;
        private System.Windows.Forms.ToolStripStatusLabel GeneralStatsLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileContextMenu;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private MetroFramework.Controls.MetroProgressSpinner ProgressWheel;
    }
}

