namespace Blockchain.Wallet.Components
{
    partial class SetWalletControl
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
            this.setWalletBtn = new System.Windows.Forms.Button();
            this.restoreWalletBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // setWalletBtn
            // 
            this.setWalletBtn.Location = new System.Drawing.Point(3, 3);
            this.setWalletBtn.Name = "setWalletBtn";
            this.setWalletBtn.Size = new System.Drawing.Size(184, 23);
            this.setWalletBtn.TabIndex = 0;
            this.setWalletBtn.Text = "Set New Wallet";
            this.setWalletBtn.UseVisualStyleBackColor = true;
            this.setWalletBtn.Click += new System.EventHandler(this.setWalletBtn_Click);
            // 
            // restoreWalletBtn
            // 
            this.restoreWalletBtn.Location = new System.Drawing.Point(3, 32);
            this.restoreWalletBtn.Name = "restoreWalletBtn";
            this.restoreWalletBtn.Size = new System.Drawing.Size(184, 23);
            this.restoreWalletBtn.TabIndex = 1;
            this.restoreWalletBtn.Text = "Restore Existing Wallet";
            this.restoreWalletBtn.UseVisualStyleBackColor = true;
            this.restoreWalletBtn.Click += new System.EventHandler(this.restoreWalletBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.setWalletBtn);
            this.panel1.Controls.Add(this.restoreWalletBtn);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 59);
            this.panel1.TabIndex = 2;
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.mainPanel.Location = new System.Drawing.Point(233, 6);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(690, 52);
            this.mainPanel.TabIndex = 3;
            // 
            // SetWalletControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.panel1);
            this.Name = "SetWalletControl";
            this.Size = new System.Drawing.Size(936, 65);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button setWalletBtn;
        private System.Windows.Forms.Button restoreWalletBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel mainPanel;
    }
}
