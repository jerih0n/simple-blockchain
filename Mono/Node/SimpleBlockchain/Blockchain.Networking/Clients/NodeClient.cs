using Blockchain.Networking.EventArgs;
using System;

namespace Blockchain.Networking.Clients
{
    internal class NodeClient : NodeNetworkBase
    {
        public event EventHandler<NewBlockEventArg> NewBlockDataRecieved;

        public void StartListen()
        {
            ListenForNewBlock();
        }

        private void ListenForNewBlock()
        {
            
        }

        private void ReadPendingTransactions()
        {

        }

        private void ListenForConfirmedTransactions()
        {

        }
    }
}
