namespace IllTechLibrary
{
    partial class JobBuilder
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
            this.FlagList = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.FlagTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // FlagList
            // 
            this.FlagList.BackColor = System.Drawing.Color.White;
            this.FlagList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FlagList.CheckOnClick = true;
            this.FlagList.Dock = System.Windows.Forms.DockStyle.Top;
            this.FlagList.ForeColor = System.Drawing.Color.Black;
            this.FlagList.FormattingEnabled = true;
            this.FlagList.Items.AddRange(new object[] {
            "0x00000001 - JOB_TITAN",
            "0x00000002 - JOB_KNIGHT",
            "0x00000004 - JOB_HEALER",
            "0x00000008 - JOB_MAGE",
            "0x00000010 - JOB_ROGUE",
            "0x00000020 - JOB_SORCERER",
            "0x00000040 - JOB_NIGHTSHADOW",
            "0x00000080 - JOB_EX_ROGUE",
            "0x00000100 - JOB_EX_MAGE",
            "0x00000200 - UNUSED",
            "0x00000400 - UNUSED",
            "0x00000800 - JOB_P2_PET"});
            this.FlagList.Location = new System.Drawing.Point(0, 0);
            this.FlagList.Name = "FlagList";
            this.FlagList.Size = new System.Drawing.Size(253, 476);
            this.FlagList.TabIndex = 0;
            this.FlagList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckStateChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(163, 485);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Save_Click);
            // 
            // FlagTextBox
            // 
            this.FlagTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.FlagTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FlagTextBox.Location = new System.Drawing.Point(15, 487);
            this.FlagTextBox.Name = "FlagTextBox";
            this.FlagTextBox.ReadOnly = true;
            this.FlagTextBox.Size = new System.Drawing.Size(142, 19);
            this.FlagTextBox.TabIndex = 2;
            // 
            // JobBuilder
            // 
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(253, 518);
            this.Controls.Add(this.FlagTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FlagList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "JobBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FlagBuilder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox FlagList;
        private System.Windows.Forms.TextBox FlagTextBox;
        #endregion
    }
}