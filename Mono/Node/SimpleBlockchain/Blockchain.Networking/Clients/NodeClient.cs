using Blockchain.Networking.EventArgs;
using Blockchain.Utils;
using Blockchain.Utils.Helpers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blockchain.Networking.Clients
{
    public class NodeClient : NodeNetworkBase
    {
        private readonly int _sex;
        private string _newBlockExchangeName = "blocks_{0}";
        public NodeClient(int nodeSex)
        {
            _sex = nodeSex;
            Console.WriteLine("DEGUT Node Client SEX IS " + _sex);
            _newBlockExchangeName = string.Format(_newBlockExchangeName, 1 -_sex);
        }
        
        public event EventHandler<NewBlockEventArg> NewBlockDataRecieved;

        public void StartListen()
        {
            ListenForNewBlock();
        }

        private Task ListenForNewBlock()
        {
            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: _newBlockExchangeName, type: ExchangeType.Fanout);

                var queueName = channel.QueueDeclare().QueueName;

                channel.QueueBind(queue: queueName,
                                  exchange: _newBlockExchangeName,
                                  routingKey: "");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, block) =>
                {
                    var body = block.Body.ToArray();

                    var message = Encoding.UTF8.GetString(body);
                    Block blockModel = null;
                    try
                    {
                        blockModel = ConverterHelper.Deserialize<Block>(message);
                        NewBlockDataRecieved(this, new NewBlockEventArg(blockModel));
                    }
                    catch
                    {

                    }
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                while(true)
                {
                    Thread.Sleep(1000);
                }

            }
        }

        private void ReadPendingTransactions()
        {

        }

        private void ListenForConfirmedTransactions()
        {

        }
    }
}
