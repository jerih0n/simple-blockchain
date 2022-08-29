using System;
using System.Collections.Generic;

namespace Blockchain.Utils
{
    public sealed class Block
    {
        public Block(long Id,
            string BlockHash,
            long Nonce,
            string PreviousBlockHash,
            string Node,
            int Complexity,
            TimeSpan BlockTime,
            int NextComplexity)
        {
            BlockHeader = new BlockHeader
            {
                Id = Id,
                BlockHash = BlockHash,
                Nonce = Nonce,
                PreviousBlockHash = PreviousBlockHash,
                Node = Node,
                Complexity = Complexity,
                BlockTime = BlockTime,
                NextComplexity = NextComplexity
            };
            Transactions = new List<Transaction>();
        }

        public BlockHeader BlockHeader { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}