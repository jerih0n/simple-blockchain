using System;

using System.Windows.Forms;

namespace Blockchain.Wallet.Components
{
    public partial class SetWalletControl : UserControl
    {
        private readonly CreateNewWalletControl _createNewWalletControl;
        private readonly RestoreExistingWalletControl _existingWalletControl;

        public SetWalletControl()
        {
            InitializeComponent();
            _createNewWalletControl = new CreateNewWalletControl();
            _existingWalletControl = new RestoreExistingWalletControl();
            _createNewWalletControl.OnOkBtnPressed += _createNewWalletControl_OnOkBtnPressed;
            _existingWalletControl.RestoreExistingWalletFinished += _existingWalletControl_RestoreExistingWalletFinished;
        }

        public event EventHandler OnWalletCreation;

        private void _existingWalletControl_RestoreExistingWalletFinished(object? sender, EventArgs e)
        {
            OnWalletCreation(this, e);
        }

        private void _createNewWalletControl_OnOkBtnPressed(object? sender, EventArgs e)
        {
            OnWalletCreation(this, e);
        }

        private void setWalletBtn_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(_createNewWalletControl);
        }

        private void restoreWalletBtn_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(_existingWalletControl);
        }
    }
}