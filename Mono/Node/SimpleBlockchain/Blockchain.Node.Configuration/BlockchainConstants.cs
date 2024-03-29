﻿
namespace Blockchain.Node.Configuration
{
    public static class BlockchainConstants
    {
        public static readonly string GenesisBlockInitialHash = "546B99B24C23F6F3A7FF3624E7806AAE3BC8360637777B6DEFD5D3FD89FB31CB";
        public static readonly int DefaultComplexity =23; // how many bits need to be zero for solution 25 is good start 
        public static readonly long DefaultBlockReward = 50;
        public static readonly short Decimals = 4;
    }
}
