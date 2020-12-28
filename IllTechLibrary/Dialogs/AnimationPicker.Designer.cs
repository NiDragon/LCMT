namespace IllTechLibrary.Dialogs
{
    partial class AnimationPicker
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
            this.animSelect = new MetroFramework.Controls.MetroButton();
            this.animListBox = new System.Windows.Forms.ListBox();
            this.search_text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // animSelect
            // 
            this.animSelect.Location = new System.Drawing.Point(12, 308);
            this.animSelect.Name = "animSelect";
            this.animSelect.Size = new System.Drawing.Size(219, 23);
            this.animSelect.TabIndex = 0;
            this.animSelect.Text = "Select Animation";
            this.animSelect.UseSelectable = true;
            this.animSelect.Click += new System.EventHandler(this.animSelect_Click);
            // 
            // animListBox
            // 
            this.animListBox.FormattingEnabled = true;
            this.animListBox.Location = new System.Drawing.Point(12, 38);
            this.animListBox.Name = "animListBox";
            this.animListBox.Size = new System.Drawing.Size(219, 264);
            this.animListBox.TabIndex = 1;
            // 
            // search_text
            // 
            this.search_text.Location = new System.Drawing.Point(12, 12);
            this.search_text.Name = "search_text";
            this.search_text.Size = new System.Drawing.Size(219, 20);
            this.search_text.TabIndex = 3;
            this.search_text.TextChanged += new System.EventHandler(this.search_text_TextChanged);
            // 
            // AnimationPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(243, 343);
            this.Controls.Add(this.search_text);
            this.Controls.Add(this.animListBox);
            this.Controls.Add(this.animSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AnimationPicker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AnimationPicker";
            this.Load += new System.EventHandler(this.AnimationPicker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton animSelect;
        private System.Windows.Forms.ListBox animListBox;
        private System.Windows.Forms.TextBox search_text;
    }
}