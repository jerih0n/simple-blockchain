using Blockchain.Cryptography.Extenstions;
using Blockchain.Cryptography.Transactions;
using Blockchain.Node.Configuration;
using Blockchain.Utils;
using System;

namespace Blockchain.Node.Logic.Algorithms.PoW
{
    public class BlockRewardProccessor
    {
        private readonly TransactionCreator _transactionCreator;

        public BlockRewardProccessor(TransactionCreator transactionCreator)
        {
            _transactionCreator = transactionCreator;
        }

        public Transaction CreateNewRewardTransaction(string blockHash, byte[] privateKey)
        {
            var bytes = blockHash.ToByteArray();
            var newEmitedAmount = BlockchainConstants.DefaultBlockReward * (long) Math.Pow(10, BlockchainConstants.Decimals);
            var transaction =  _transactionCreator.CreateRawTransaction(blockHash, "", newEmitedAmount, 0);
            return transaction;

        }
    }
}
