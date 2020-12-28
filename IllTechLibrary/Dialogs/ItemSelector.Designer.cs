namespace IllTechLibrary.Dialogs
{
    partial class ItemSelector
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
            this.SelectBtn = new System.Windows.Forms.Button();
            this.SearchItems = new System.Windows.Forms.TextBox();
            this.ItemProb = new System.Windows.Forms.TextBox();
            this.metroLabel1 = new System.Windows.Forms.Label();
            this.cItemsList = new IllTechLibrary.Controls.CustomDrawListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ItemIdx = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SelectBtn
            // 
            this.SelectBtn.Location = new System.Drawing.Point(186, 412);
            this.SelectBtn.Name = "SelectBtn";
            this.SelectBtn.Size = new System.Drawing.Size(62, 23);
            this.SelectBtn.TabIndex = 1;
            this.SelectBtn.Text = "Save";
            this.SelectBtn.Click += new System.EventHandler(this.OnItemOK);
            // 
            // SearchItems
            // 
            this.SearchItems.Location = new System.Drawing.Point(12, 9);
            this.SearchItems.Name = "SearchItems";
            this.SearchItems.Size = new System.Drawing.Size(236, 20);
            this.SearchItems.TabIndex = 2;
            this.SearchItems.TextChanged += new System.EventHandler(this.OnSearchItems);
            // 
            // ItemProb
            // 
            this.ItemProb.Location = new System.Drawing.Point(60, 412);
            this.ItemProb.Name = "ItemProb";
            this.ItemProb.Size = new System.Drawing.Size(120, 20);
            this.ItemProb.TabIndex = 3;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(12, 412);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(36, 13);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Rate :";
            // 
            // cItemsList
            // 
            this.cItemsList.CausesValidation = false;
            this.cItemsList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cItemsList.ItemHeight = 33;
            this.cItemsList.Location = new System.Drawing.Point(12, 38);
            this.cItemsList.Name = "cItemsList";
            this.cItemsList.Size = new System.Drawing.Size(236, 335);
            this.cItemsList.TabIndex = 0;
            this.cItemsList.TabStop = false;
            this.cItemsList.UseTabStops = false;
            this.cItemsList.SelectedIndexChanged += new System.EventHandler(this.OnItemIndexChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 389);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Index :";
            // 
            // ItemIdx
            // 
            this.ItemIdx.Location = new System.Drawing.Point(60, 386);
            this.ItemIdx.Name = "ItemIdx";
            this.ItemIdx.Size = new System.Drawing.Size(120, 20);
            this.ItemIdx.TabIndex = 5;
            // 
            // ItemSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(260, 445);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ItemIdx);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.ItemProb);
            this.Controls.Add(this.SearchItems);
            this.Controls.Add(this.SelectBtn);
            this.Controls.Add(this.cItemsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ItemSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Item";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CustomDrawListBox cItemsList;
        private System.Windows.Forms.Button SelectBtn;
        private System.Windows.Forms.TextBox SearchItems;
        private System.Windows.Forms.TextBox ItemProb;
        private System.Windows.Forms.Label metroLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ItemIdx;
    }
}