using Blockchain.Cryptography.EllipticCurve;
using Blockchain.Wallet.Services;
using System;
using System.Windows.Forms;

namespace Blockchain.Wallet.Components
{
    public partial class RestoreExistingWalletControl : UserControl
    {
        private readonly EllipticCurveProcessor _ellipticCurveProcessor;
        private readonly LocalDbService _localDbService;

        public RestoreExistingWalletControl()
        {
            InitializeComponent();
            _ellipticCurveProcessor = new EllipticCurveProcessor();
            _localDbService = new LocalDbService();
        }

        public event EventHandler RestoreExistingWalletFinished;

        private void restoreWalletBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(privateKeyImp.Text))
            {
                MessageBox.Show("Private key must be presented!");
                return;
            }

            try
            {
                var publicKey = _ellipticCurveProcessor.GetPublicKeyFromPrivate(privateKeyImp.Text);
            }
            catch
            {
                MessageBox.Show("Invalid Private Key!!");
                return;
            }
            _localDbService.RecordNewPrivateKey(privateKeyImp.Text);
            RestoreExistingWalletFinished(this, null);
        }
    }
}