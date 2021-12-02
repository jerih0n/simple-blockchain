using System;

namespace Blockchain.Utils.Cryptography.Node
{
    public static class NodeIdGenerator
    {
        public static string GenerateNodeId()
        {
            return new Guid().ToString(); // TODO:// change this with cryptographic Id base on public key
        }
    }
}
