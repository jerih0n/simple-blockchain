using Blockchain.Cryptography.EllipticCurve;
using Blockchain.Cryptography.Extenstions;
using Blockchain.Cryptography.Transactions;
using Blockchain.Utils;
using Blockchain.Wallet.Models;
using Blockchain.Wallet.NodeConnection;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain.Wallet.Components
{
    public partial class SendTransanctionControl : UserControl
    {
        private readonly TransactionManager _transactionManager;
        private readonly EllipticCurveProcessor _ellipticCurveProcessor;
        private readonly WalletDb _walletDb;
        private readonly string _address;
        private readonly long _balance;
        private readonly NodeHttpClient _nodeHttpClient;

        public SendTransanctionControl(string address, long balance, WalletDb walletDb)

        {
            InitializeComponent();
            _address = address;
            _balance = balance;
            _walletDb = walletDb;
            _ellipticCurveProcessor = new EllipticCurveProcessor();
            _transactionManager = new TransactionManager(_ellipticCurveProcessor);
        }

        private async void sendTransactionBtn_Click(object sender, EventArgs e)
        {
            var signedTransaction = CreateTransaction();

            var createTransactionSuccessfull = await SendTransaction(signedTransaction);

            if (createTransactionSuccessfull)
            {
                MessageBox.Show($"Transaction with transaction hash {signedTransaction.Hash} was created!");
            }
        }

        private Transaction CreateTransaction()
        {
            (bool success, string msg) = ValidateInput();

            if (!success)
            {
                MessageBox.Show(msg);
#pragma warning disable CS8603 // Possible null reference return.
                return default;
#pragma warning restore CS8603 // Possible null reference return.
            }

            var toAddress = this.toAddressTxtbox.Text;
            var amount = long.Parse(this.amountTxtbox.Text);
            var fee = long.Parse(this.transactionFeeTxt.Text);

            var rawTransaction = _transactionManager.CreateRawTransaction(_address, toAddress, amount, fee, Utils.TransactionTypesEnum.Normal, _balance, 0);

            var signedTransaction = _transactionManager.SignTransaction(rawTransaction, _walletDb.PrivateKeyEnc.ToByteArray());

            return signedTransaction;
        }

        private (bool, string) ValidateInput()
        {
            if (string.IsNullOrEmpty(this.toAddressTxtbox.Text))
            {
                return (false, "Address not provided");
            }

            if (string.IsNullOrEmpty(this.amountTxtbox.Text))
            {
                return (false, "Amount not provided");
            }

            long amount = 0;
            var sucess = long.TryParse(this.amountTxtbox.Text, out amount);

            if (!sucess)
            {
                return (false, "Invalid amount format");
            }

            if (amount <= 0)
            {
                return (false, "Amount must be greater than 0");
            }

            long fees = 0;

            var feesSuccess = long.TryParse(this.transactionFeeTxt.Text, out fees);

            if (!feesSuccess)
            {
                return (false, "Invalid fee format");
            }

            if (fees <= 0)
            {
                return (false, "Fees must be greater than 0");
            }

            return (true, string.Empty);
        }

        private async Task<bool> SendTransaction(Transaction tranasaction)
        {
            return await _nodeHttpClient.CreateNewTransactionAsync(tranasaction);
        }
    }
}