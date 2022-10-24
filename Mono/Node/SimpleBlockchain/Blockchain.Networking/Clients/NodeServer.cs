using Blockchain.Utils;
using Blockchain.Utils.Helpers;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Blockchain.Networking.Clients
{
    public class NodeServer : NodeNetworkBase
    {
        private readonly int _sex;
        private string _newBlockExchangeName = "blocks_{0}";
        private string _transactionsExchangeName = "transactions";

        public NodeServer(int nodeSex)
        {
            _sex = nodeSex;
            Console.WriteLine("DEGUT Node Server SEX IS " + _sex);
            _newBlockExchangeName = string.Format(_newBlockExchangeName, _sex);
        }

        public void PushNewBlock(Block block) => PushToNetwork(block, _newBlockExchangeName, NetworkPushType.Exchange, "");

        public void PushNewTransaction(Transaction transaction) => PushToNetwork(transaction, _transactionsExchangeName, NetworkPushType.Exchange, "");

        private void PushToNetwork<T>(T data, string exchangeName, NetworkPushType networkPushType, string notifyMessage = null)
        {
            using (var channel = _connection.CreateModel())
            {
                var dataAsJson = ConverterHelper.Serialize(data);

                var body = Encoding.UTF8.GetBytes(dataAsJson);

                if (networkPushType == NetworkPushType.Exchange)
                {
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);

                    channel.BasicPublish(exchange: exchangeName,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                }
                else
                {
                    channel.QueueDeclare(queue: exchangeName,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    channel.BasicPublish(exchange: "",
                                     routingKey: exchangeName,
                                     basicProperties: null,
                                     body: body);
                }

                if (!string.IsNullOrEmpty(notifyMessage))
                {
                    Console.WriteLine(notifyMessage);
                }
            }
        }
    }
}