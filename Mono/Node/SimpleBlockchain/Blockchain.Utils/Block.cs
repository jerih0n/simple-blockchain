using System.Numerics;

namespace Blockchain.Utils
{
    public class Block
    {       
        public long Id { get; set; }
        public string BlockHash { get; set; }
        public string PreviousBlockHash { get; set; }
        public string NodeHash { get; set; }
    }
}
