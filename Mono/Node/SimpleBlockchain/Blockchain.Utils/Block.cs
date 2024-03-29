﻿using System;
using System.Numerics;

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
            Id = id;
            BlockHash = blockHash;
            Nonce = nonce;
            PreviousBlockHash = previousBlockHash;
            Node = node;
            Complexity = complexity;
            MinedAt = DateTime.UtcNow;
            BlockTime = blockTime;
            NextComplexity = nextComplexity;
            BlockHeader = new BlockHeader();

        }
        public BlockHeader BlockHeader { get; }
        public long Id { get; }
        public string BlockHash { get; }
        public long Nonce { get; }
        public string PreviousBlockHash { get; }
        public string Node { get; }
        public int Complexity { get; }
        public DateTime MinedAt { get; }
        public TimeSpan BlockTime { get; }
        public int NextComplexity { get; }
        public decimal CirculatingSupply { get; }
    }
}
