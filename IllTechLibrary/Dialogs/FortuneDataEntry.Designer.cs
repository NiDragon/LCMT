namespace IllTechLibrary.Dialogs
{
    partial class FortuneDataEntry
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AddBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSkill = new System.Windows.Forms.TextBox();
            this.tbLevel = new System.Windows.Forms.TextBox();
            this.tbString = new System.Windows.Forms.TextBox();
            this.tbProb = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbProb);
            this.groupBox1.Controls.Add(this.tbString);
            this.groupBox1.Controls.Add(this.tbLevel);
            this.groupBox1.Controls.Add(this.tbSkill);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 151);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fortune Item Info";
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(69, 169);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(75, 23);
            this.AddBtn.TabIndex = 1;
            this.AddBtn.Text = "Add";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.OnAdd);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(150, 169);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.OnCancel);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Skill Index";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Skill Level";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "String Index";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Prob";
            // 
            // tbSkill
            // 
            this.tbSkill.Location = new System.Drawing.Point(86, 29);
            this.tbSkill.Name = "tbSkill";
            this.tbSkill.Size = new System.Drawing.Size(100, 20);
            this.tbSkill.TabIndex = 4;
            // 
            // tbLevel
            // 
            this.tbLevel.Location = new System.Drawing.Point(86, 55);
            this.tbLevel.Name = "tbLevel";
            this.tbLevel.Size = new System.Drawing.Size(100, 20);
            this.tbLevel.TabIndex = 5;
            // 
            // tbString
            // 
            this.tbString.Location = new System.Drawing.Point(86, 81);
            this.tbString.Name = "tbString";
            this.tbString.Size = new System.Drawing.Size(100, 20);
            this.tbString.TabIndex = 6;
            // 
            // tbProb
            // 
            this.tbProb.Location = new System.Drawing.Point(86, 107);
            this.tbProb.Name = "tbProb";
            this.tbProb.Size = new System.Drawing.Size(100, 20);
            this.tbProb.TabIndex = 7;
            // 
            // FortuneDataEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 198);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FortuneDataEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FortuneDataEntry";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbProb;
        private System.Windows.Forms.TextBox tbString;
        private System.Windows.Forms.TextBox tbLevel;
        private System.Windows.Forms.TextBox tbSkill;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Button CancelBtn;
    }
}