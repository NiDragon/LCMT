namespace MiniCrash
{
    partial class MainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.beginDump = new System.Windows.Forms.Button();
            this.cancelDump = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dumpProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.descBox = new System.Windows.Forms.TextBox();
            this.labelHeader = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.errorMsg = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // beginDump
            // 
            this.beginDump.Location = new System.Drawing.Point(232, 428);
            this.beginDump.Name = "beginDump";
            this.beginDump.Size = new System.Drawing.Size(75, 23);
            this.beginDump.TabIndex = 0;
            this.beginDump.Text = "Generate";
            this.beginDump.UseVisualStyleBackColor = true;
            this.beginDump.Click += new System.EventHandler(this.OnBeginDump);
            // 
            // cancelDump
            // 
            this.cancelDump.Location = new System.Drawing.Point(313, 428);
            this.cancelDump.Name = "cancelDump";
            this.cancelDump.Size = new System.Drawing.Size(75, 23);
            this.cancelDump.TabIndex = 1;
            this.cancelDump.Text = "Cancel";
            this.cancelDump.UseVisualStyleBackColor = true;
            this.cancelDump.Click += new System.EventHandler(this.OnCancelDump);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dumpProgress});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 458);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(400, 23);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dumpProgress
            // 
            this.dumpProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.dumpProgress.Name = "dumpProgress";
            this.dumpProgress.Size = new System.Drawing.Size(100, 17);
            this.dumpProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.descBox);
            this.groupBox2.Location = new System.Drawing.Point(6, 181);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 227);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tell Us More :";
            // 
            // descBox
            // 
            this.descBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descBox.Location = new System.Drawing.Point(3, 16);
            this.descBox.Multiline = true;
            this.descBox.Name = "descBox";
            this.descBox.Size = new System.Drawing.Size(358, 208);
            this.descBox.TabIndex = 0;
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeader.Location = new System.Drawing.Point(6, 15);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(190, 20);
            this.labelHeader.TabIndex = 2;
            this.labelHeader.Text = "Looks Like We Crashed :(";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.errorMsg);
            this.groupBox3.Location = new System.Drawing.Point(6, 37);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(364, 141);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Application Message :";
            // 
            // errorMsg
            // 
            this.errorMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorMsg.Location = new System.Drawing.Point(3, 16);
            this.errorMsg.Multiline = true;
            this.errorMsg.Name = "errorMsg";
            this.errorMsg.ReadOnly = true;
            this.errorMsg.Size = new System.Drawing.Size(358, 122);
            this.errorMsg.TabIndex = 3;
            this.errorMsg.WordWrap = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelHeader);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 415);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Crash Report";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(400, 481);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cancelDump);
            this.Controls.Add(this.beginDump);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crash Report - illusionist Softworks";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button beginDump;
        private System.Windows.Forms.Button cancelDump;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar dumpProgress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox descBox;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox errorMsg;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

