using RabbitMQ.Client;
using System;

namespace Blockchain.Networking.Clients
{
    internal abstract class NodeNetworkBase : IDisposable
    {
        protected readonly ConnectionFactory _connectionFactory;
        protected IConnection _connection;

        public NodeNetworkBase()
        {
            _connectionFactory = SetConeectionFactory();
            OpenConnection();
        }
        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }

        public void OpenConnection()
        {
            _connection = _connectionFactory.CreateConnection();
        }

        private ConnectionFactory SetConeectionFactory()
        {
            //TODO : from config
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "chain-node",
                Password = "Password1",
                Port = 5672,
            };

            return connectionFactory;
        }
    }
}
