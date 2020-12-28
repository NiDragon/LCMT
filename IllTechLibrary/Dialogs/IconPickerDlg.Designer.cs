namespace IllTechLibrary.Dialogs
{
    partial class IconPickerDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IconPickerDlg));
            this.IconBox = new System.Windows.Forms.PictureBox();
            this.gbIconInfo = new System.Windows.Forms.GroupBox();
            this.bCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.filesCombo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).BeginInit();
            this.gbIconInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // IconBox
            // 
            this.IconBox.Location = new System.Drawing.Point(196, 12);
            this.IconBox.Name = "IconBox";
            this.IconBox.Size = new System.Drawing.Size(512, 512);
            this.IconBox.TabIndex = 0;
            this.IconBox.TabStop = false;
            this.IconBox.Click += new System.EventHandler(this.IconBox_Click);
            // 
            // gbIconInfo
            // 
            this.gbIconInfo.Controls.Add(this.bCancel);
            this.gbIconInfo.Controls.Add(this.label1);
            this.gbIconInfo.Controls.Add(this.filesCombo);
            this.gbIconInfo.Location = new System.Drawing.Point(12, 12);
            this.gbIconInfo.Name = "gbIconInfo";
            this.gbIconInfo.Size = new System.Drawing.Size(178, 509);
            this.gbIconInfo.TabIndex = 1;
            this.gbIconInfo.TabStop = false;
            this.gbIconInfo.Text = "Icon File Info";
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(8, 480);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(164, 23);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Icon File :";
            // 
            // filesCombo
            // 
            this.filesCombo.FormattingEnabled = true;
            this.filesCombo.Location = new System.Drawing.Point(64, 19);
            this.filesCombo.Name = "filesCombo";
            this.filesCombo.Size = new System.Drawing.Size(108, 21);
            this.filesCombo.TabIndex = 0;
            this.filesCombo.SelectedIndexChanged += new System.EventHandler(this.OnSelectedFileIndexChanged);
            // 
            // IconPickerDlg
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(720, 535);
            this.Controls.Add(this.gbIconInfo);
            this.Controls.Add(this.IconBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1242, 1080);
            this.MinimumSize = new System.Drawing.Size(736, 520);
            this.Name = "IconPickerDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Icon Picker";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).EndInit();
            this.gbIconInfo.ResumeLayout(false);
            this.gbIconInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox IconBox;
        private System.Windows.Forms.GroupBox gbIconInfo;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox filesCombo;
    }
}