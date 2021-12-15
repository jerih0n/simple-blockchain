using Blockchain.Cryptography.Extenstions;
using Blockchain.Utils;
using System;

namespace Blockchain.Cryptography.Transactions
{
    public class TransactionCreator
    {
        public Transaction CreateRawTransaction(string fromAddress, string toAddress, long amount, long fee)
        {
            //add signiture
            var transactionHash = $"{fromAddress}_{toAddress}_{amount}_{DateTime.UtcNow.ToString()}"
                .Sha256()
                .ToHex();

            return new Transaction(transactionHash, fromAddress, toAddress, amount, fee);
        }
    }
}
