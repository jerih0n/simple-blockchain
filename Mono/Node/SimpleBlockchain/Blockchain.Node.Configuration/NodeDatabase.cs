using Blockchain.Utils.Nodes;
using System.Collections.Generic;

namespace Blockchain.Node.Configuration
{
    public class NodeDatabase
    {
        public string NodeId { get; set; }
        public string ConnectionIP { get; set; }
        public List<NodeConnectionModel> KnowConnections { get; set; } = new List<NodeConnectionModel>();
        public string PrivateKeyEncrypted { get; set; }
    }
}
