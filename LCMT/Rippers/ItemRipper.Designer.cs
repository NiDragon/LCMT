namespace LCMT
{
    partial class ItemRipper
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
            this.RipItemList = new System.Windows.Forms.CheckedListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileEp1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileEp2 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileEp3 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileEp4 = new System.Windows.Forms.ToolStripMenuItem();
            this.playParkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfirmOkBtn = new System.Windows.Forms.Button();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RipItemList
            // 
            this.RipItemList.CheckOnClick = true;
            this.RipItemList.Location = new System.Drawing.Point(12, 37);
            this.RipItemList.Name = "RipItemList";
            this.RipItemList.Size = new System.Drawing.Size(225, 364);
            this.RipItemList.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(249, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileEp1,
            this.OpenFileEp2,
            this.OpenFileEp3,
            this.OpenFileEp4,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // OpenFileEp1
            // 
            this.OpenFileEp1.Name = "OpenFileEp1";
            this.OpenFileEp1.Size = new System.Drawing.Size(180, 22);
            this.OpenFileEp1.Text = "Open EP1";
            this.OpenFileEp1.Click += new System.EventHandler(this.OpenFileEp1_Click);
            // 
            // OpenFileEp2
            // 
            this.OpenFileEp2.Name = "OpenFileEp2";
            this.OpenFileEp2.Size = new System.Drawing.Size(180, 22);
            this.OpenFileEp2.Text = "Open EP2";
            this.OpenFileEp2.Click += new System.EventHandler(this.OpenFileEp2_Click);
            // 
            // OpenFileEp3
            // 
            this.OpenFileEp3.Name = "OpenFileEp3";
            this.OpenFileEp3.Size = new System.Drawing.Size(180, 22);
            this.OpenFileEp3.Text = "Open EP3";
            this.OpenFileEp3.Click += new System.EventHandler(this.OpenFileEp3_Click);
            // 
            // OpenFileEp4
            // 
            this.OpenFileEp4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalToolStripMenuItem,
            this.playParkToolStripMenuItem});
            this.OpenFileEp4.Name = "OpenFileEp4";
            this.OpenFileEp4.Size = new System.Drawing.Size(180, 22);
            this.OpenFileEp4.Tag = "none";
            this.OpenFileEp4.Text = "Open EP4";
            // 
            // playParkToolStripMenuItem
            // 
            this.playParkToolStripMenuItem.Name = "playParkToolStripMenuItem";
            this.playParkToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.playParkToolStripMenuItem.Tag = "playpark";
            this.playParkToolStripMenuItem.Text = "Play Park";
            this.playParkToolStripMenuItem.Click += new System.EventHandler(this.OpenFileEp4_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExit);
            // 
            // ConfirmOkBtn
            // 
            this.ConfirmOkBtn.Location = new System.Drawing.Point(12, 407);
            this.ConfirmOkBtn.Name = "ConfirmOkBtn";
            this.ConfirmOkBtn.Size = new System.Drawing.Size(225, 23);
            this.ConfirmOkBtn.TabIndex = 2;
            this.ConfirmOkBtn.Text = "Confirm Rip";
            this.ConfirmOkBtn.UseVisualStyleBackColor = true;
            this.ConfirmOkBtn.Click += new System.EventHandler(this.ConfirmOkBtn_Click);
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.normalToolStripMenuItem.Text = "Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.OpenFileEp4_Click);
            // 
            // ItemRipper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 437);
            this.Controls.Add(this.ConfirmOkBtn);
            this.Controls.Add(this.RipItemList);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ItemRipper";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rip Items";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox RipItemList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFileEp2;
        private System.Windows.Forms.ToolStripMenuItem OpenFileEp3;
        private System.Windows.Forms.ToolStripMenuItem OpenFileEp4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button ConfirmOkBtn;
        private System.Windows.Forms.ToolStripMenuItem OpenFileEp1;
        private System.Windows.Forms.ToolStripMenuItem playParkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
    }
}