namespace IllTechLibrary.Settings
{
    partial class MySqlSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MySqlSettings));
            this.SqlGroupBox = new System.Windows.Forms.GroupBox();
            this.EP3Transcode = new MetroFramework.Controls.MetroCheckBox();
            this.LangCode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.EncPage = new System.Windows.Forms.ComboBox();
            this.UPortText = new MetroFramework.Controls.MetroTextBox();
            this.UIPText = new MetroFramework.Controls.MetroTextBox();
            this.UPassText = new MetroFramework.Controls.MetroTextBox();
            this.EncryptPwdCheck = new MetroFramework.Controls.MetroCheckBox();
            this.UNameText = new MetroFramework.Controls.MetroTextBox();
            this.UPortLabel = new System.Windows.Forms.Label();
            this.UIPLabel = new System.Windows.Forms.Label();
            this.UPwdLabel = new System.Windows.Forms.Label();
            this.UNameLabel = new System.Windows.Forms.Label();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.testBtn = new MetroFramework.Controls.MetroButton();
            this.ExitBtn = new MetroFramework.Controls.MetroButton();
            this.SaveBtn = new MetroFramework.Controls.MetroButton();
            this.SqlGroupBox.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SqlGroupBox
            // 
            this.SqlGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.SqlGroupBox.Controls.Add(this.EP3Transcode);
            this.SqlGroupBox.Controls.Add(this.LangCode);
            this.SqlGroupBox.Controls.Add(this.label2);
            this.SqlGroupBox.Controls.Add(this.label1);
            this.SqlGroupBox.Controls.Add(this.EncPage);
            this.SqlGroupBox.Controls.Add(this.UPortText);
            this.SqlGroupBox.Controls.Add(this.UIPText);
            this.SqlGroupBox.Controls.Add(this.UPassText);
            this.SqlGroupBox.Controls.Add(this.EncryptPwdCheck);
            this.SqlGroupBox.Controls.Add(this.UNameText);
            this.SqlGroupBox.Controls.Add(this.UPortLabel);
            this.SqlGroupBox.Controls.Add(this.UIPLabel);
            this.SqlGroupBox.Controls.Add(this.UPwdLabel);
            this.SqlGroupBox.Controls.Add(this.UNameLabel);
            this.SqlGroupBox.Location = new System.Drawing.Point(12, 12);
            this.SqlGroupBox.Name = "SqlGroupBox";
            this.SqlGroupBox.Size = new System.Drawing.Size(310, 217);
            this.SqlGroupBox.TabIndex = 0;
            this.SqlGroupBox.TabStop = false;
            this.SqlGroupBox.Text = "SQL Info";
            // 
            // EP3Transcode
            // 
            this.EP3Transcode.AutoSize = true;
            this.EP3Transcode.Location = new System.Drawing.Point(14, 196);
            this.EP3Transcode.Name = "EP3Transcode";
            this.EP3Transcode.Size = new System.Drawing.Size(98, 15);
            this.EP3Transcode.TabIndex = 20;
            this.EP3Transcode.Text = "EP3 Transcode";
            this.EP3Transcode.UseSelectable = true;
            // 
            // LangCode
            // 
            this.LangCode.FormattingEnabled = true;
            this.LangCode.Items.AddRange(new object[] {
            "usa",
            "thai",
            "chn",
            "dev",
            "jpn"});
            this.LangCode.Location = new System.Drawing.Point(83, 138);
            this.LangCode.Name = "LangCode";
            this.LangCode.Size = new System.Drawing.Size(209, 21);
            this.LangCode.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Encoding :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Language :";
            // 
            // EncPage
            // 
            this.EncPage.FormattingEnabled = true;
            this.EncPage.Items.AddRange(new object[] {
            "latin1",
            "cp1250",
            "tis620",
            "euckr",
            "sjis",
            "big5",
            "utf8"});
            this.EncPage.Location = new System.Drawing.Point(83, 169);
            this.EncPage.Name = "EncPage";
            this.EncPage.Size = new System.Drawing.Size(209, 21);
            this.EncPage.TabIndex = 15;
            // 
            // UPortText
            // 
            // 
            // 
            // 
            this.UPortText.CustomButton.Image = null;
            this.UPortText.CustomButton.Location = new System.Drawing.Point(187, 1);
            this.UPortText.CustomButton.Name = "";
            this.UPortText.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.UPortText.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.UPortText.CustomButton.TabIndex = 1;
            this.UPortText.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.UPortText.CustomButton.UseSelectable = true;
            this.UPortText.CustomButton.Visible = false;
            this.UPortText.Lines = new string[0];
            this.UPortText.Location = new System.Drawing.Point(83, 109);
            this.UPortText.MaxLength = 32767;
            this.UPortText.Name = "UPortText";
            this.UPortText.PasswordChar = '\0';
            this.UPortText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.UPortText.SelectedText = "";
            this.UPortText.SelectionLength = 0;
            this.UPortText.SelectionStart = 0;
            this.UPortText.ShortcutsEnabled = true;
            this.UPortText.Size = new System.Drawing.Size(209, 23);
            this.UPortText.TabIndex = 13;
            this.UPortText.UseSelectable = true;
            this.UPortText.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.UPortText.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // UIPText
            // 
            // 
            // 
            // 
            this.UIPText.CustomButton.Image = null;
            this.UIPText.CustomButton.Location = new System.Drawing.Point(187, 1);
            this.UIPText.CustomButton.Name = "";
            this.UIPText.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.UIPText.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.UIPText.CustomButton.TabIndex = 1;
            this.UIPText.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.UIPText.CustomButton.UseSelectable = true;
            this.UIPText.CustomButton.Visible = false;
            this.UIPText.Lines = new string[0];
            this.UIPText.Location = new System.Drawing.Point(83, 80);
            this.UIPText.MaxLength = 32767;
            this.UIPText.Name = "UIPText";
            this.UIPText.PasswordChar = '\0';
            this.UIPText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.UIPText.SelectedText = "";
            this.UIPText.SelectionLength = 0;
            this.UIPText.SelectionStart = 0;
            this.UIPText.ShortcutsEnabled = true;
            this.UIPText.Size = new System.Drawing.Size(209, 23);
            this.UIPText.TabIndex = 12;
            this.UIPText.UseSelectable = true;
            this.UIPText.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.UIPText.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // UPassText
            // 
            // 
            // 
            // 
            this.UPassText.CustomButton.Image = null;
            this.UPassText.CustomButton.Location = new System.Drawing.Point(187, 1);
            this.UPassText.CustomButton.Name = "";
            this.UPassText.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.UPassText.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.UPassText.CustomButton.TabIndex = 1;
            this.UPassText.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.UPassText.CustomButton.UseSelectable = true;
            this.UPassText.CustomButton.Visible = false;
            this.UPassText.Lines = new string[0];
            this.UPassText.Location = new System.Drawing.Point(83, 51);
            this.UPassText.MaxLength = 32767;
            this.UPassText.Name = "UPassText";
            this.UPassText.PasswordChar = '\0';
            this.UPassText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.UPassText.SelectedText = "";
            this.UPassText.SelectionLength = 0;
            this.UPassText.SelectionStart = 0;
            this.UPassText.ShortcutsEnabled = true;
            this.UPassText.Size = new System.Drawing.Size(209, 23);
            this.UPassText.TabIndex = 11;
            this.UPassText.UseSelectable = true;
            this.UPassText.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.UPassText.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // EncryptPwdCheck
            // 
            this.EncryptPwdCheck.AutoSize = true;
            this.EncryptPwdCheck.Location = new System.Drawing.Point(176, 196);
            this.EncryptPwdCheck.Name = "EncryptPwdCheck";
            this.EncryptPwdCheck.Size = new System.Drawing.Size(116, 15);
            this.EncryptPwdCheck.TabIndex = 16;
            this.EncryptPwdCheck.Text = "Encrypt Password";
            this.EncryptPwdCheck.UseSelectable = true;
            // 
            // UNameText
            // 
            // 
            // 
            // 
            this.UNameText.CustomButton.Image = null;
            this.UNameText.CustomButton.Location = new System.Drawing.Point(187, 1);
            this.UNameText.CustomButton.Name = "";
            this.UNameText.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.UNameText.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.UNameText.CustomButton.TabIndex = 1;
            this.UNameText.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.UNameText.CustomButton.UseSelectable = true;
            this.UNameText.CustomButton.Visible = false;
            this.UNameText.Lines = new string[0];
            this.UNameText.Location = new System.Drawing.Point(83, 22);
            this.UNameText.MaxLength = 32767;
            this.UNameText.Name = "UNameText";
            this.UNameText.PasswordChar = '\0';
            this.UNameText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.UNameText.SelectedText = "";
            this.UNameText.SelectionLength = 0;
            this.UNameText.SelectionStart = 0;
            this.UNameText.ShortcutsEnabled = true;
            this.UNameText.Size = new System.Drawing.Size(209, 23);
            this.UNameText.TabIndex = 10;
            this.UNameText.UseSelectable = true;
            this.UNameText.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.UNameText.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // UPortLabel
            // 
            this.UPortLabel.AutoSize = true;
            this.UPortLabel.Location = new System.Drawing.Point(2, 113);
            this.UPortLabel.Name = "UPortLabel";
            this.UPortLabel.Size = new System.Drawing.Size(70, 13);
            this.UPortLabel.TabIndex = 8;
            this.UPortLabel.Text = "MySQL Port :";
            // 
            // UIPLabel
            // 
            this.UIPLabel.AutoSize = true;
            this.UIPLabel.Location = new System.Drawing.Point(11, 83);
            this.UIPLabel.Name = "UIPLabel";
            this.UIPLabel.Size = new System.Drawing.Size(61, 13);
            this.UIPLabel.TabIndex = 5;
            this.UIPLabel.Text = "MySQL IP :";
            // 
            // UPwdLabel
            // 
            this.UPwdLabel.AutoSize = true;
            this.UPwdLabel.Location = new System.Drawing.Point(13, 54);
            this.UPwdLabel.Name = "UPwdLabel";
            this.UPwdLabel.Size = new System.Drawing.Size(59, 13);
            this.UPwdLabel.TabIndex = 1;
            this.UPwdLabel.Text = "Password :";
            // 
            // UNameLabel
            // 
            this.UNameLabel.AutoSize = true;
            this.UNameLabel.Location = new System.Drawing.Point(11, 25);
            this.UNameLabel.Name = "UNameLabel";
            this.UNameLabel.Size = new System.Drawing.Size(61, 13);
            this.UNameLabel.TabIndex = 0;
            this.UNameLabel.Text = "Username :";
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.testBtn);
            this.metroPanel1.Controls.Add(this.ExitBtn);
            this.metroPanel1.Controls.Add(this.SaveBtn);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 0);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(333, 270);
            this.metroPanel1.TabIndex = 3;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // testBtn
            // 
            this.testBtn.Location = new System.Drawing.Point(12, 235);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(132, 23);
            this.testBtn.TabIndex = 5;
            this.testBtn.Text = "Test Connection";
            this.testBtn.UseSelectable = true;
            this.testBtn.Click += new System.EventHandler(this.TestConnection_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ExitBtn.Location = new System.Drawing.Point(246, 235);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(75, 23);
            this.ExitBtn.TabIndex = 4;
            this.ExitBtn.Text = "Exit";
            this.ExitBtn.UseSelectable = true;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(165, 235);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 3;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseSelectable = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // MySqlSettings
            // 
            this.AcceptButton = this.SaveBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.ExitBtn;
            this.ClientSize = new System.Drawing.Size(333, 270);
            this.Controls.Add(this.SqlGroupBox);
            this.Controls.Add(this.metroPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MySqlSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MySQL Connection Info";
            this.Load += new System.EventHandler(this.MySQLSet_Load);
            this.SqlGroupBox.ResumeLayout(false);
            this.SqlGroupBox.PerformLayout();
            this.metroPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SqlGroupBox;
        private System.Windows.Forms.Label UPwdLabel;
        private System.Windows.Forms.Label UNameLabel;
        private System.Windows.Forms.Label UIPLabel;
        private System.Windows.Forms.Label UPortLabel;
        private MetroFramework.Controls.MetroTextBox UNameText;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton ExitBtn;
        private MetroFramework.Controls.MetroButton SaveBtn;
        private MetroFramework.Controls.MetroTextBox UPortText;
        private MetroFramework.Controls.MetroTextBox UIPText;
        private MetroFramework.Controls.MetroTextBox UPassText;
        private MetroFramework.Controls.MetroCheckBox EncryptPwdCheck;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox EncPage;
        private MetroFramework.Controls.MetroButton testBtn;
        private System.Windows.Forms.ComboBox LangCode;
        private MetroFramework.Controls.MetroCheckBox EP3Transcode;
    }
}