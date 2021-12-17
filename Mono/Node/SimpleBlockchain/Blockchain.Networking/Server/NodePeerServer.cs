using Blockchain.Node.Configuration;
using Blockchain.Utils;
using Blockchain.Utils.Helpers;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Blockchain.Networking.Server
{
    public class NodePeerServer : IDisposable
    {
        
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;

        public NodePeerServer()
        {
            _connectionFactory = SetConeectionFactory();
            OpenConnection();
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose(); 
        }


        public void PushNewBlock(Block block)
        {
            using(var channel = _connection.CreateModel())
            {
                var blockJson = ConverterHelper.Serialize(block);

                //TODO:
                channel.QueueDeclare(queue: NetworkConstants.BlocksQueue,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var blockAsByteArray = Encoding.UTF8.GetBytes(blockJson);

                channel.BasicPublish(exchange: "",
                                 routingKey: NetworkConstants.BlocksQueue,
                                 basicProperties: null,
                                 body: blockAsByteArray);
            }
            
        }

        public void PushNewValidTransaction(Transaction transaction)
        {

        }

        public void OpenConnection()
        {
            _connection = _connectionFactory.CreateConnection();
        }


        private ConnectionFactory SetConeectionFactory()
        {
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
