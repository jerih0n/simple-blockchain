using Blockchain.Cryptography.Addresses;
using Blockchain.Cryptography.EllipticCurve;
using Blockchain.Wallet.Models;
using Newtonsoft.Json;
using System.IO;

namespace Blockchain.Wallet.Services
{
    public class LocalDbService
    {
        private const string LocalDbFileName = "WalletData.json";

        private WalletDb _walletDb;
        private readonly EllipticCurveProcessor _ellipticCurveProcessor;

        public LocalDbService()
        {
            _ellipticCurveProcessor = new EllipticCurveProcessor();
            LoadData();
        }

        public void RecordNewPrivateKey(string privateKey)
        {
            //TODO:add password
            _walletDb.PrivateKeyEnc = privateKey;
            //generate first address
            var publicKey = _ellipticCurveProcessor.GetPublicKeyFromPrivate(privateKey);

            var firstAddress = AddressGenerator.GenerateFirstAddress(publicKey);

            RecordNewWalletData(privateKey, firstAddress);
        }

        public void RecordNewAddress(string address)
        {
            _walletDb.GeneratedAddresess.Add(address);

            using (var streamWriter = new StreamWriter(LocalDbFileName))
            {
                streamWriter.WriteLine(JsonConvert.SerializeObject(_walletDb));
            }
        }

        public void Initialize()
        {
            _walletDb = new WalletDb();
            using (var streamWriter = new StreamWriter(LocalDbFileName))
            {
                streamWriter.WriteLine(JsonConvert.SerializeObject(_walletDb));
            }
        }

        public WalletDb Wallet
        {
            get
            {
                return _walletDb;
            }
        }

        private void LoadData()
        {
            if (File.Exists(LocalDbFileName))
            {
                using (var streamReader = new StreamReader(LocalDbFileName))
                {
                    var data = streamReader.ReadToEnd();
                    _walletDb = JsonConvert.DeserializeObject<WalletDb>(data);
                }

                return;
            }
            InitializeNewWalletDb();
        }

        private void InitializeNewWalletDb()
        {
            using (var streamWritter = new StreamWriter(LocalDbFileName))
            {
                _walletDb = new WalletDb();
                streamWritter.WriteLine(JsonConvert.SerializeObject(_walletDb));
            }
        }

        private void RecordNewWalletData(string privateKey, string firstAddress)
        {
            var newWallet = new WalletDb
            {
                PrivateKeyEnc = privateKey,
            };
            newWallet.GeneratedAddresess.Add(firstAddress);

            using (var streamWriter = new StreamWriter(LocalDbFileName))
            {
                streamWriter.WriteLine(JsonConvert.SerializeObject(newWallet));
            }
        }
    }
}