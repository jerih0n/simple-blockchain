using Blockchain.Utils;

namespace Blockchain.Networking.EventArgs
{
    public class NewBlockEventArg
    {
        public NewBlockEventArg(Block block)
        {
            Block = block;
        }
        public Block Block { get; }
    }
}
