using System;

namespace Blockchain.Utils
{
    public class BlockHeader
    {
        public long Id { get; set; }
        public string BlockHash { get; set; }
        public long Nonce { get; set; }
        public string PreviousBlockHash { get; set; }
        public string Node { get; set; }
        public int Complexity { get; set; }
        public DateTime MinedAt { get; set; }
        public TimeSpan BlockTime { get; set; }
        public int NextComplexity { get; set; }
    }
}