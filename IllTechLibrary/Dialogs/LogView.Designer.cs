namespace IllTechLibrary
{
    partial class LogView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogView));
            this.DescBox = new MetroFramework.Controls.MetroTextBox();
            this.PathText = new MetroFramework.Controls.MetroTextBox();
            this.clrButton = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // DescBox
            // 
            // 
            // 
            // 
            this.DescBox.CustomButton.Image = null;
            this.DescBox.CustomButton.Location = new System.Drawing.Point(187, 2);
            this.DescBox.CustomButton.Name = "";
            this.DescBox.CustomButton.Size = new System.Drawing.Size(253, 253);
            this.DescBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.DescBox.CustomButton.TabIndex = 1;
            this.DescBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.DescBox.CustomButton.UseSelectable = true;
            this.DescBox.CustomButton.Visible = false;
            this.DescBox.Lines = new string[0];
            this.DescBox.Location = new System.Drawing.Point(23, 63);
            this.DescBox.MaxLength = 32767;
            this.DescBox.Multiline = true;
            this.DescBox.Name = "DescBox";
            this.DescBox.PasswordChar = '\0';
            this.DescBox.ReadOnly = true;
            this.DescBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DescBox.SelectedText = "";
            this.DescBox.SelectionLength = 0;
            this.DescBox.SelectionStart = 0;
            this.DescBox.ShortcutsEnabled = true;
            this.DescBox.Size = new System.Drawing.Size(443, 258);
            this.DescBox.TabIndex = 12;
            this.DescBox.UseSelectable = true;
            this.DescBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.DescBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // PathText
            // 
            // 
            // 
            // 
            this.PathText.CustomButton.Image = null;
            this.PathText.CustomButton.Location = new System.Drawing.Point(419, 1);
            this.PathText.CustomButton.Name = "";
            this.PathText.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.PathText.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.PathText.CustomButton.TabIndex = 1;
            this.PathText.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.PathText.CustomButton.UseSelectable = true;
            this.PathText.CustomButton.Visible = false;
            this.PathText.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.PathText.Lines = new string[] {
        "{0}"};
            this.PathText.Location = new System.Drawing.Point(23, 325);
            this.PathText.MaxLength = 32767;
            this.PathText.Name = "PathText";
            this.PathText.PasswordChar = '\0';
            this.PathText.ReadOnly = true;
            this.PathText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.PathText.SelectedText = "";
            this.PathText.SelectionLength = 0;
            this.PathText.SelectionStart = 0;
            this.PathText.ShortcutsEnabled = true;
            this.PathText.Size = new System.Drawing.Size(443, 25);
            this.PathText.TabIndex = 16;
            this.PathText.Text = "{0}";
            this.PathText.UseSelectable = true;
            this.PathText.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.PathText.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // clrButton
            // 
            this.clrButton.Location = new System.Drawing.Point(391, 34);
            this.clrButton.Name = "clrButton";
            this.clrButton.Size = new System.Drawing.Size(75, 23);
            this.clrButton.TabIndex = 17;
            this.clrButton.Text = "Clear";
            this.clrButton.UseSelectable = true;
            this.clrButton.Click += new System.EventHandler(this.ClearLog);
            // 
            // LogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 357);
            this.Controls.Add(this.clrButton);
            this.Controls.Add(this.PathText);
            this.Controls.Add(this.DescBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogView";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Log Viewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LogView_FormClosed);
            this.Load += new System.EventHandler(this.Log_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox DescBox;
        private MetroFramework.Controls.MetroTextBox PathText;
        private MetroFramework.Controls.MetroButton clrButton;
    }
}