using System;

namespace Blockchain.Utils
{
    public class Transaction
    {
        public Transaction(string hash, 
            string fromAddress, 
            string toAddress,
            long amount,
            long fee)
        {
            Hash = hash;
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Fee = fee;
            Amount = amount;
            Created = DateTime.UtcNow;
            TransactionStatus = TransactionStatusEnum.Pending;
        }
        public string Hash { get; }
        public string FromAddress { get; }
        public string ToAddress { get; }
        public long Fee { get; }
        public long Amount { get; }
        public DateTime Created { get; }
        public bool IsVerified { get; set; }
        public string VerificationNodeId { get; set; }
        public TransactionStatusEnum TransactionStatus { get; set; }
        public long Block { get; set; }
        public string Signiture { get; set; }
        public string PublicKey { get; set; }
    }
}
