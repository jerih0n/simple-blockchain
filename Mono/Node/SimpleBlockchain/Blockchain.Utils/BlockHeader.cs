using System.Collections.Generic;

namespace Blockchain.Utils
{
    public class BlockHeader
    {
        public BlockHeader()
        {
            Transactions = new List<Transaction>();
        }

        public Transaction RewardTransaction { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
