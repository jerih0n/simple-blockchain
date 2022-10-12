namespace Blockchain.Wallet.Components
{
    partial class SendTransanctionControl
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
            this.toAddressTxtbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.amountTxtbox = new System.Windows.Forms.TextBox();
            this.sendTransactionBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.transactionFeeTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // toAddressTxtbox
            // 
            this.toAddressTxtbox.Location = new System.Drawing.Point(106, 12);
            this.toAddressTxtbox.Name = "toAddressTxtbox";
            this.toAddressTxtbox.Size = new System.Drawing.Size(384, 23);
            this.toAddressTxtbox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Amount";
            // 
            // amountTxtbox
            // 
            this.amountTxtbox.Location = new System.Drawing.Point(106, 52);
            this.amountTxtbox.Name = "amountTxtbox";
            this.amountTxtbox.Size = new System.Drawing.Size(144, 23);
            this.amountTxtbox.TabIndex = 3;
            // 
            // sendTransactionBtn
            // 
            this.sendTransactionBtn.Location = new System.Drawing.Point(12, 127);
            this.sendTransactionBtn.Name = "sendTransactionBtn";
            this.sendTransactionBtn.Size = new System.Drawing.Size(478, 30);
            this.sendTransactionBtn.TabIndex = 4;
            this.sendTransactionBtn.Text = "Send";
            this.sendTransactionBtn.UseVisualStyleBackColor = true;
            this.sendTransactionBtn.Click += new System.EventHandler(this.sendTransactionBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Transaction Fee";
            // 
            // transactionFeeTxt
            // 
            this.transactionFeeTxt.Location = new System.Drawing.Point(106, 85);
            this.transactionFeeTxt.Name = "transactionFeeTxt";
            this.transactionFeeTxt.Size = new System.Drawing.Size(144, 23);
            this.transactionFeeTxt.TabIndex = 6;
            this.transactionFeeTxt.Text = "100";
            // 
            // SendTransanctionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.transactionFeeTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sendTransactionBtn);
            this.Controls.Add(this.amountTxtbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toAddressTxtbox);
            this.Name = "SendTransanctionControl";
            this.Size = new System.Drawing.Size(506, 194);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox toAddressTxtbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox amountTxtbox;
        private System.Windows.Forms.Button sendTransactionBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox transactionFeeTxt;
    }
}
