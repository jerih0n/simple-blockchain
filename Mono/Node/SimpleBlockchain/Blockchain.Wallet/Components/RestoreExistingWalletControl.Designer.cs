namespace Blockchain.Wallet.Components
{
    partial class RestoreExistingWalletControl
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
            this.privateKeyImp = new System.Windows.Forms.TextBox();
            this.restoreWalletBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key-";
            // 
            // privateKeyImp
            // 
            this.privateKeyImp.Location = new System.Drawing.Point(40, 12);
            this.privateKeyImp.Multiline = true;
            this.privateKeyImp.Name = "privateKeyImp";
            this.privateKeyImp.Size = new System.Drawing.Size(561, 23);
            this.privateKeyImp.TabIndex = 1;
            // 
            // restoreWalletBtn
            // 
            this.restoreWalletBtn.Location = new System.Drawing.Point(607, 11);
            this.restoreWalletBtn.Name = "restoreWalletBtn";
            this.restoreWalletBtn.Size = new System.Drawing.Size(90, 23);
            this.restoreWalletBtn.TabIndex = 2;
            this.restoreWalletBtn.Text = "Restore";
            this.restoreWalletBtn.UseVisualStyleBackColor = true;
            this.restoreWalletBtn.Click += new System.EventHandler(this.restoreWalletBtn_Click);
            // 
            // RestoreExistingWalletControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.restoreWalletBtn);
            this.Controls.Add(this.privateKeyImp);
            this.Controls.Add(this.label1);
            this.Name = "RestoreExistingWalletControl";
            this.Size = new System.Drawing.Size(700, 52);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox privateKeyImp;
        private System.Windows.Forms.Button restoreWalletBtn;
    }
}
