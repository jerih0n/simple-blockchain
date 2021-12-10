using Blockchain.Node.CLI.Processors;
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

        private readonly NodeConfiguration _nodeConfiguration;

        private bool _isNodeSet;
        private readonly BlockchainLocalDataConnector _blockchainLocalDataConnector;
        private readonly NodeLocalDataConnector _nodeLocalDataConnector;
        private readonly NodeProcessor _commandProcessor;

        public NodeCLI(NodeConfiguration nodeConfiguration, 
            BlockchainLocalDataConnector blockchainLocalDataConnector, 
            NodeLocalDataConnector nodeLocalDataConnector, 
            NodeProcessor commandProcessor)
        {
            _nodeConfiguration = nodeConfiguration;
            _blockchainLocalDataConnector = blockchainLocalDataConnector;
            _nodeLocalDataConnector = nodeLocalDataConnector;
            _commandProcessor = commandProcessor;

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
            _isNodeSet = _nodeLocalDataConnector.IsNodeSet;
        }
        private async Task StartNode()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("********************************************* NODE STARTED *********************************************");
            // block from local storage is loaded and we are ready for sync
            Console.WriteLine("********************************************************************************************************");
            Console.WriteLine("Chose command");
            Console.WriteLine(_commandProcessor.ListAllCommands());
        }
    }
}
