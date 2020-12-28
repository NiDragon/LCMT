namespace LCMT.Dialogs
{
    partial class GiftMaker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GiftMaker));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMessage = new System.Windows.Forms.RichTextBox();
            this.tbSendName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.PackageBox = new IllTechLibrary.Controls.CustomDrawListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbMessage);
            this.groupBox1.Controls.Add(this.tbSendName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 197);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sender";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Message : ";
            // 
            // tbMessage
            // 
            this.tbMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMessage.Location = new System.Drawing.Point(9, 64);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(185, 123);
            this.tbMessage.TabIndex = 2;
            this.tbMessage.Text = "";
            // 
            // tbSendName
            // 
            this.tbSendName.Location = new System.Drawing.Point(56, 17);
            this.tbSendName.Name = "tbSendName";
            this.tbSendName.Size = new System.Drawing.Size(138, 20);
            this.tbSendName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name : ";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.statusStrip1.Location = new System.Drawing.Point(0, 247);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(444, 22);
            this.statusStrip1.TabIndex = 37;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(218, 213);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(105, 23);
            this.BtnOK.TabIndex = 38;
            this.BtnOK.Text = "Send";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(325, 213);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(105, 23);
            this.BtnCancel.TabIndex = 39;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // PackageBox
            // 
            this.PackageBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.PackageBox.FormattingEnabled = true;
            this.PackageBox.ItemHeight = 33;
            this.PackageBox.Location = new System.Drawing.Point(218, 12);
            this.PackageBox.Name = "PackageBox";
            this.PackageBox.Size = new System.Drawing.Size(212, 195);
            this.PackageBox.TabIndex = 2;
            // 
            // GiftMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(444, 269);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.PackageBox);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GiftMaker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GiftMaker";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private IllTechLibrary.Controls.CustomDrawListBox PackageBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox tbMessage;
        private System.Windows.Forms.TextBox tbSendName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
    }
}