using Blockchain.Cryptography.EllipticCurve;
using Blockchain.Cryptography.Extenstions;
using Blockchain.Utils;
using System;

namespace Blockchain.Cryptography.Transactions
{
    public class TransactionManager
    {
        private readonly EllipticCurveProcessor _ellipticCurveProcessor;

        public TransactionManager(EllipticCurveProcessor ellipticCurveProcessor)
        {
            _ellipticCurveProcessor = ellipticCurveProcessor;
        }

        public Transaction CreateRawTransaction(string fromAddress,
            string toAddress, long amount,
            long fee,
            TransactionTypesEnum transactionTypes,
            long fromAddressBalance,
            long toAddressBalance)
        {
            //add signiture
            var transactionHash = $"{fromAddress}_{toAddress}_{amount}_{DateTime.UtcNow.ToString()}"
                .Sha256Utf8String()
                .ToHex();

            return new Transaction(transactionHash, fromAddress, toAddress, amount, fee, transactionTypes, fromAddressBalance, toAddressBalance);
        }

        public Transaction SignTransaction(Transaction transaction, byte[] privateKey)
        {
            var publicKey = _ellipticCurveProcessor.GetPublicKeyFromPrivate(privateKey);
            var transactionSigniture = _ellipticCurveProcessor.SignTransactionHash(transaction.Hash.TotUtf8ByteArray(), privateKey);
            transaction.Signiture = transactionSigniture.ToHex();
            transaction.PublicKey = publicKey.ToHex();
            return transaction;
        }
    }
}