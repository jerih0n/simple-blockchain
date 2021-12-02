using Blockchain.Node.Configuration;
using Blockchain.Node.Logic.LocalConnectors;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blockchain.Node.CLI
{
    public class NodeCLI : IHostedService
    {
        private readonly NodeConfiguration _nodeConfiguration;
        private readonly BlockchainLocalDataConnector _blockchainLocalDataConnector;

        public NodeCLI(NodeConfiguration nodeConfiguration, BlockchainLocalDataConnector blockchainLocalDataConnector)
        {
            _nodeConfiguration = nodeConfiguration;
            _blockchainLocalDataConnector = blockchainLocalDataConnector;
            Init();
        }

        public async Task StartAsync(CancellationToken cancellationToken) => await RunNode();

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void Init()
        {
            _blockchainLocalDataConnector.ReadLocalDb();
        }
        private async Task RunNode()
        {
            Console.WriteLine("********************************************* NODE STARTED *********************************************");
        }
    }
}
