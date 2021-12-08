using Blockchain.Node.Configuration;
using Blockchain.Node.Logic.LocalConnectors;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blockchain.Node.CLI
{
    public class NodeCLI : IHostedService
    {
        private const string FULL_NODE = "-full";
        private const string LIGHT_NODE = "-light";

        private readonly NodeConfiguration _nodeConfiguration;
        
        private readonly BlockchainLocalDataConnector _blockchainLocalDataConnector;
        private readonly NodeLocalDataConnector _nodeLocalDataConnector;

        public NodeCLI(NodeConfiguration nodeConfiguration, 
            BlockchainLocalDataConnector blockchainLocalDataConnector, 
            NodeLocalDataConnector nodeLocalDataConnector)
        {
            _nodeConfiguration = nodeConfiguration;
            _blockchainLocalDataConnector = blockchainLocalDataConnector;
            _nodeLocalDataConnector = nodeLocalDataConnector;
            Init();
        }

        public async Task StartAsync(CancellationToken cancellationToken) => await StartNode();

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void Init()
        {
            _nodeLocalDataConnector.ReadLocalNodeDb();
            _blockchainLocalDataConnector.ReadLocalDb();
        }
        private async Task StartNode()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("********************************************* NODE STARTED *********************************************");
            // block from local storage is loaded and we are ready for sync

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Selec option to proceed");

            while (true)
            {
                var input = Console.ReadLine();
                if(input == FULL_NODE)
                {
                    await Task.Run(() => RunFullNode());
                    break;
                }
                if(input == LIGHT_NODE)
                {
                    //run full node
                    break;
                }
                else
                {
                    Console.WriteLine("Unknow Command");
                }
            }
        }

        private async Task RunFullNode()
        {
            var lastBlock = _blockchainLocalDataConnector.GetLastBlock();         
            if (lastBlock == null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Last Block is Empty. The blockchain local copy is empty");              
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Last block number in local copy {lastBlock.Id} with block hash {lastBlock.BlockHash} and previous block hash {lastBlock.PreviousBlockHash}");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("******* Connecting to network.....");

        }
    }
}
