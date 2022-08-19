using System;
using System.Collections.Generic;

namespace Blockchain.Utils
{
    public sealed class Block
    {
        public Block(long id,
            string blockHash,
            long nonce,
            string previousBlockHash,
            string node,
            int complexity,
            TimeSpan blockTime,
            int nextComplexity)
        {
            BlockHeader = new BlockHeader(id, blockHash, nonce, previousBlockHash, node, complexity, blockTime, nextComplexity);
            Transactions = new List<Transaction>();
        }

        public BlockHeader BlockHeader { get; }

        public List<Transaction> Transactions { get; set; }
    }
}