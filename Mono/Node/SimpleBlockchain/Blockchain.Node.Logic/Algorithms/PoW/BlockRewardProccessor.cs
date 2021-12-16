using Blockchain.Cryptography.Addresses;
using Blockchain.Cryptography.EllipticCurve;
using Blockchain.Cryptography.Transactions;
using Blockchain.Node.Configuration;
using Blockchain.Utils;
using System;

namespace Blockchain.Node.Logic.Algorithms.PoW
{
    public class BlockRewardProccessor
    {
        private readonly TransactionManager _transactionCreator;
        private readonly TransactionManager _transactionManager;
        private readonly EllipticCurveProcessor _ellipticCurveProcessor;
        public BlockRewardProccessor(TransactionManager transactionCreator, 
            TransactionManager transactionManager,
            EllipticCurveProcessor ellipticCurveProcessor)
        {
            _transactionCreator = transactionCreator;
            _transactionManager = transactionManager;
            _ellipticCurveProcessor = ellipticCurveProcessor;
        }

        public Transaction CreateNewRewardTransaction(string blockHash, byte[] privateKey)
        {
            var newEmitedAmount = BlockchainConstants.DefaultBlockReward * (long) Math.Pow(10, BlockchainConstants.Decimals);
            var publicKey = _ellipticCurveProcessor.GetPublicKeyFromPrivate(privateKey);
            var address = AddressGenerator.GenerateFirstAddress(publicKey);
            var transaction =  _transactionCreator.CreateRawTransaction(blockHash, address, newEmitedAmount, 0);
            var signedTransaction = _transactionManager.SignTransaction(transaction, privateKey);
            return signedTransaction;
        }
    }
}
