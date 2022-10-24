using Blockchain.Networking.Clients;
using Blockchain.Networking.EventArgs;
using Blockchain.Utils;
using System;
using System.Threading.Tasks;

namespace Blockchain.Networking.Server
{
    public class NetworkConnectionService : IDisposable
    {
        private readonly NodeServer _nodeServer;
        private readonly NodeClient _nodeClient;
        private string _nodeId;

        public NetworkConnectionService(NodeServer nodeServer, NodeClient nodeClient)
        {
            _nodeServer = nodeServer;
            _nodeClient = nodeClient;
            _nodeClient.NewBlockDataRecieved += _nodeClient_NewBlockDataRecieved;
        }

        public event EventHandler<NewBlockEventArg> NewBlockDataRecieved;

        public void Dispose()
        {
            _nodeServer.Dispose();
            _nodeClient.Dispose();
        }

        public void Init(string nodeId)
        {
            _nodeId = nodeId;
            StartListener();
        }

        public void PushNewBlock(Block block) => _nodeServer.PushNewBlock(block);

        public void PushNewTransaction(Transaction transaction) => _nodeServer.PushNewTransaction(transaction);

        protected void StartListener() => Task.Factory.StartNew(() => _nodeClient.StartListen());

        private void _nodeClient_NewBlockDataRecieved(object sender, NewBlockEventArg e)
        {
            if (e.Block.BlockHeader.Node != _nodeId)
            {
                //resend the new block to the network
                //_nodeServer.PushNewBlock(e.Block);

                //fire event
                NewBlockDataRecieved(sender, e);
            }
        }
    }
}