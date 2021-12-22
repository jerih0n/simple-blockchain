using Blockchain.Networking.Clients;
using Blockchain.Utils;
using RabbitMQ.Client;
using System;

namespace Blockchain.Networking.Server
{
    public class NetworkConnectionService : IDisposable
    {       
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private NodeServer _nodeServer;

        public NetworkConnectionService(NodeServer nodeServer)
        {
            _nodeServer = nodeServer;
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose(); 
        }

        public void PushNewBlock(Block block) => _nodeServer.PushNewBlock(block);

        public void PushNewValidTransaction(Transaction transaction) => _nodeServer.PushValidatedTransaction(transaction);

    }
}
