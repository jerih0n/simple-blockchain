using System;

namespace Blockchain.Utils
{
    public class Transaction
    {
        public Transaction(string hash,
            string fromAddress,
            string toAddress,
            long amount,
            long fee,
            TransactionTypesEnum transactionTypes,
            long fromAddressBalance,
            long toAddressBalance)
        {
            Hash = hash;
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Fee = fee;
            Amount = amount;
            Created = DateTime.UtcNow;
            InitialState = new State(fromAddressBalance, toAddressBalance);
            NewState = new State(fromAddressBalance - amount, toAddressBalance + amount);
            TransactionType = transactionTypes;
        }

        public string Hash { get; }
        public string FromAddress { get; }
        public string ToAddress { get; }
        public long Fee { get; }
        public long Amount { get; }
        public DateTime Created { get; }
        public bool IsVerified { get; set; }
        public string VerificationNodeId { get; set; }
        public TransactionTypesEnum TransactionType { get; }
        public TransactionStatusEnum TransactionStatus { get; set; }
        public long Block { get; set; }
        public string Signiture { get; set; }
        public string PublicKey { get; set; }
        public State InitialState { get; }

        public State NewState { get; }
    }
}