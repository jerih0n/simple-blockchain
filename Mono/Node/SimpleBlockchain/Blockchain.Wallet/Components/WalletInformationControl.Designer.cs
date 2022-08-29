namespace Blockchain.Wallet.Components
{
    partial class WalletInformation
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.balanceLb = new System.Windows.Forms.Label();
            this.addressList = new System.Windows.Forms.ComboBox();
            this.newAddressGenerationBtn = new System.Windows.Forms.Button();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Balance";
            // 
            // balanceLb
            // 
            this.balanceLb.AutoSize = true;
            this.balanceLb.Location = new System.Drawing.Point(57, 19);
            this.balanceLb.Name = "balanceLb";
            this.balanceLb.Size = new System.Drawing.Size(40, 15);
            this.balanceLb.TabIndex = 1;
            this.balanceLb.Text = "0.0000";
            // 
            // addressList
            // 
            this.addressList.FormattingEnabled = true;
            this.addressList.Location = new System.Drawing.Point(131, 16);
            this.addressList.Name = "addressList";
            this.addressList.Size = new System.Drawing.Size(390, 23);
            this.addressList.TabIndex = 2;
            this.addressList.SelectedIndexChanged += new System.EventHandler(this.addressList_SelectedIndexChanged);
            // 
            // newAddressGenerationBtn
            // 
            this.newAddressGenerationBtn.Location = new System.Drawing.Point(538, 16);
            this.newAddressGenerationBtn.Name = "newAddressGenerationBtn";
            this.newAddressGenerationBtn.Size = new System.Drawing.Size(75, 23);
            this.newAddressGenerationBtn.TabIndex = 3;
            this.newAddressGenerationBtn.Text = "Add";
            this.newAddressGenerationBtn.UseVisualStyleBackColor = true;
            this.newAddressGenerationBtn.Click += new System.EventHandler(this.newAddressGenerationBtn_Click);
            // 
            // refreshBtn
            // 
            this.refreshBtn.Location = new System.Drawing.Point(619, 16);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(68, 23);
            this.refreshBtn.TabIndex = 4;
            this.refreshBtn.Text = "Delete";
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // WalletInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.refreshBtn);
            this.Controls.Add(this.newAddressGenerationBtn);
            this.Controls.Add(this.addressList);
            this.Controls.Add(this.balanceLb);
            this.Controls.Add(this.label1);
            this.Name = "WalletInformation";
            this.Size = new System.Drawing.Size(690, 52);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label balanceLb;
        private System.Windows.Forms.ComboBox addressList;
        private System.Windows.Forms.Button newAddressGenerationBtn;
        private System.Windows.Forms.Button refreshBtn;
    }
}
