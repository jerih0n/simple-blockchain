using Blockchain.Cryptography.Extenstions;
using Blockchain.Cryptography.Keys;
using Blockchain.Wallet.Services;
using System;
using System.Windows.Forms;

namespace Blockchain.Wallet.Components
{
    public partial class CreateNewWalletControl : UserControl
    {
        private readonly LocalDbService _localDbService;

        public CreateNewWalletControl()
        {
            InitializeComponent();
            _localDbService = new LocalDbService();
            OnLoad();
        }

        public event EventHandler OnOkBtnPressed;

        public void OnLoad()

        {
            var privateKey = PrivateKeyGenerator.GeneratePrivateKey().ToHex();
            this.pkTextBox.Text = privateKey;
            _localDbService.RecordNewPrivateKey(privateKey);
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            OnOkBtnPressed(this, null);
        }
    }
}