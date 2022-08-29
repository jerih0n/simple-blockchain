using Blockchain.Cryptography.Addresses;
using Blockchain.Wallet.Models;
using Blockchain.Wallet.NodeConnection;
using Blockchain.Wallet.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain.Wallet.Components
{
    public partial class WalletInformation : UserControl
    {
        private readonly WalletDb _walletDb;
        private readonly LocalDbService _localDbService;
        private readonly NodeHttpClient _nodeHttpClient;

        public WalletInformation(WalletDb walletDb)
        {
            InitializeComponent();
            _localDbService = new LocalDbService();
            _nodeHttpClient = new NodeHttpClient();
            _walletDb = walletDb;
            addressList.Items.AddRange(_walletDb.GeneratedAddresess.ToArray());
            if (_walletDb.GeneratedAddresess.Count > 0)
            {
                addressList.SelectedIndex = 0;
            }
        }

        private async void addressList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedAddress = (string)addressList.Items[addressList.SelectedIndex];

            try
            {
                var result = await _nodeHttpClient.GetBalanceForAddressAsync(selectedAddress);
                this.balanceLb.Text = result.Amount.ToString();
            }
            catch
            {
                MessageBox.Show("Cannot get balance for address");
            }
        }

        public event EventHandler OnRefreshBtnClick;

        private void newAddressGenerationBtn_Click(object sender, EventArgs e)
        {
            var lastAddress = _walletDb.GeneratedAddresess[_walletDb.GeneratedAddresess.Count - 1];

            var nextAddress = AddressGenerator.GenerateNextAddress(lastAddress);

            addressList.Items.Add(nextAddress);

            _localDbService.RecordNewAddress(nextAddress);
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            _localDbService.Initialize();
            OnRefreshBtnClick(this, null);
        }
    }
}