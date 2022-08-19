using Blockchain.Cryptography.Addresses;
using Blockchain.Cryptography.EllipticCurve;
using Blockchain.Cryptography.Transactions;
using Blockchain.Node.Configuration;
using Blockchain.Node.Logic.LocalConnectors;
using Blockchain.Utils;
using System;

namespace Blockchain.Node.Logic.Algorithms.PoW
{
    public class BlockRewardProccessor
    {
        private readonly TransactionManager _transactionCreator;
        private readonly TransactionManager _transactionManager;
        private readonly EllipticCurveProcessor _ellipticCurveProcessor;
        private readonly BlockchainLocalDataConnector _blockchainLocalDataConnector;

        public BlockRewardProccessor(TransactionManager transactionCreator,
            TransactionManager transactionManager,
            EllipticCurveProcessor ellipticCurveProcessor,
            BlockchainLocalDataConnector blockchainLocalDataConnector)
        {
            _transactionCreator = transactionCreator;
            _transactionManager = transactionManager;
            _ellipticCurveProcessor = ellipticCurveProcessor;
            _blockchainLocalDataConnector = blockchainLocalDataConnector;
        }

        public Transaction CreateNewRewardTransaction(string blockHash, byte[] privateKey, TransactionTypesEnum transactionTypes)
        {
            var newEmitedAmount = BlockchainConstants.DefaultBlockReward * (long)Math.Pow(10, BlockchainConstants.Decimals);
            var publicKey = _ellipticCurveProcessor.GetPublicKeyFromPrivate(privateKey);
            var address = AddressGenerator.GenerateFirstAddress(publicKey);
            var lastStateForAddress = _blockchainLocalDataConnector.GetLastTransactionForAddress(address);
            long addressBalacne = 0;
            if (lastStateForAddress != null)
            {
                addressBalacne = lastStateForAddress.NewState.RecieverAmount;
            }
            var transaction = _transactionCreator.CreateRawTransaction(blockHash, address, newEmitedAmount, 0, transactionTypes, 0, addressBalacne);
            var signedTransaction = _transactionManager.SignTransaction(transaction, privateKey);
            return signedTransaction;
        }
    }
}