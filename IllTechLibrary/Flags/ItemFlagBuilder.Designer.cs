namespace IllTechLibrary
{
    partial class ItemFlagBuilder
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
            this.SaveFlagBtn = new System.Windows.Forms.Button();
            this.FlagValueText = new System.Windows.Forms.TextBox();
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
            "ITEM_COUNT",
            "ITEM_DROP",
            "ITEM_UPGRADE",
            "ITEM_NO_EXCHANGE",
            "ITEM_NO_TRADE",
            "ITEM_NO_DELETE",
            "ITEM_MADE",
            "ITEM_MIX",
            "ITEM_CASH",
            "ITEM_LORD",
            "ITEM_NO_STASH",
            "ITEM_CHANGE",
            "ITEM_COMPOSITE",
            "ITEM_DUPLICATION",
            "ITEM_LENT",
            "ITEM_RARE",
            "ITEM_ABS",
            "ITEM_NOT_REFORM",
            "ITEM_ZONEMOVE_DEL",
            "ITEM_ORIGIN",
            "ITEM_TRIGGER",
            "ITEM_RAIDSPE",
            "ITEM_QUEST",
            "ITEM_BOX",
            "ITEM_NOTTRADEAGENT",
            "ITEM_DURABILITY",
            "ITEM_COSTUME2",
            "ITEM_SOCKET",
            "ITEM_SELLER",
            "ITEM_CASTLLAN",
            "ITEM_LETS_PARTY",
            "ITEM_NONRVR",
            "ITEM_QUESTGIVE",
            "ITEM_TOGGLE",
            "ITEM_COMPOSE",
            "ITEM_NOT_SINGLE",
            "ITEM_INVISIBLE_COSTUME"});
            this.FlagList.Location = new System.Drawing.Point(0, 0);
            this.FlagList.Name = "FlagList";
            this.FlagList.Size = new System.Drawing.Size(307, 476);
            this.FlagList.TabIndex = 0;
            this.FlagList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.FlagList_SelectedIndexChanged);
            // 
            // SaveFlagBtn
            // 
            this.SaveFlagBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveFlagBtn.Location = new System.Drawing.Point(175, 491);
            this.SaveFlagBtn.Name = "SaveFlagBtn";
            this.SaveFlagBtn.Size = new System.Drawing.Size(75, 20);
            this.SaveFlagBtn.TabIndex = 1;
            this.SaveFlagBtn.Text = "Save";
            this.SaveFlagBtn.UseVisualStyleBackColor = true;
            this.SaveFlagBtn.Click += new System.EventHandler(this.Save_Click);
            // 
            // FlagValueText
            // 
            this.FlagValueText.BackColor = System.Drawing.SystemColors.Window;
            this.FlagValueText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FlagValueText.Location = new System.Drawing.Point(4, 491);
            this.FlagValueText.Name = "FlagValueText";
            this.FlagValueText.ReadOnly = true;
            this.FlagValueText.Size = new System.Drawing.Size(165, 19);
            this.FlagValueText.TabIndex = 2;
            // 
            // ItemFlagBuilder
            // 
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(307, 518);
            this.Controls.Add(this.FlagValueText);
            this.Controls.Add(this.SaveFlagBtn);
            this.Controls.Add(this.FlagList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ItemFlagBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FlagBuilder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button SaveFlagBtn;
        private System.Windows.Forms.CheckedListBox FlagList;
        private System.Windows.Forms.TextBox FlagValueText;

        #endregion
    }
}