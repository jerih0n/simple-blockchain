using Blockchain.Networking.EventArgs;
using System;

namespace Blockchain.Networking.Clients
{
    public class NodeClient : NodeNetworkBase
    {
        private readonly int _sex;
        private string _newBlockExchangeName = "blocks_{0}";
        public NodeClient(int nodeSex)
        {
            _sex = nodeSex;
            _newBlockExchangeName = string.Format(_newBlockExchangeName, 1 -_sex);
        }
        
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
